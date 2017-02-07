/*	
	Lorakon Sniff - Extract data from gamma spectrums and insert into database
    Copyright (C) 2017  Norwegian Radiation Protection Authority

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
// Authors: Dag Robole,

using System;
using System.IO;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Data.SQLite;
using CTimer = System.Windows.Forms.Timer;

namespace LorakonSniff
{
    public partial class FormLorakonSniff : Form
    {
        private ContextMenu trayMenu = null;
        private Settings settings = null;
        private Monitor monitor = null;
        private ConcurrentQueue<FileEvent> events = null;
        SQLiteConnection hashes = null;
        SQLiteConnection log = null;
        private CTimer timer = null;

        public FormLorakonSniff(NotifyIcon trayIcon)
        {
            InitializeComponent();

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Avslutt", OnExit);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Logg", OnLog);
            trayMenu.MenuItems.Add("Innstillinger", OnSettings);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Informasjon", OnAbout);

            trayIcon.Text = "Lorakon Sniff";
            trayIcon.Icon = Properties.Resources.LorakonIcon;

            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }        

        private void FormLorakonSniff_Load(object sender, EventArgs e)
        {
            log = Log.Create();
            Log.Open(log);
            Log.AddMessage(log, "STARTED");

            Visible = false;
            ShowInTaskbar = false;

            // Set default window layout
            Rectangle rect = Screen.FromControl(this).Bounds;
            Width = (rect.Right - rect.Left) / 2;
            Height = (rect.Bottom - rect.Top) / 2;
            Left = rect.Left + Width / 2;
            Top = rect.Top + Height / 2;
            
            dtLogFrom.Value = DateTime.Now - new TimeSpan(1, 0, 0, 0);
            dtLogTo.Value = DateTime.Now;

            // Create environment and load settings
            if (!Directory.Exists(LorakonEnvironment.SettingsPath))
                Directory.CreateDirectory(LorakonEnvironment.SettingsPath);
            settings = new Settings();
            LoadSettings();

            tbSettingsWatchDirectory.Text = settings.WatchDirectory;
            tbSettingsConnectionString.Text = settings.ConnectionString;
            tbSettingsSpectrumFilter.Text = settings.FileFilter;

            if (!Directory.Exists(settings.WatchDirectory))
                Directory.CreateDirectory(settings.WatchDirectory);

            events = new ConcurrentQueue<FileEvent>();
            hashes = Hashes.Create();

            // Handle files that has been created after last shutdown and has not been handled before
            Hashes.Open(hashes);
            foreach (string fname in Directory.EnumerateFiles(settings.WatchDirectory, settings.FileFilter, SearchOption.AllDirectories))
            {
                DateTime ctime = File.GetCreationTime(fname);
                DateTime wtime = File.GetLastWriteTime(fname);
                if (ctime.CompareTo(settings.LastShutdownTime) < 0 && wtime.CompareTo(settings.LastShutdownTime) < 0)
                {
                    Log.AddMessage(log, "Skipping " + fname + ", not modified since last shutdown");
                    continue;
                }

                string sum = FileOps.GetChecksum(fname);
                if (!Hashes.HasChecksum(hashes, sum))
                {
                    Log.AddMessage(log, "Importing " + fname + " [" + sum + "]");

                    Report report = GetReport(fname);
                    StoreReport(report);

                    Hashes.InsertChecksum(hashes, sum);
                }
                else
                {
                    Log.AddMessage(log, "File " + fname + " is already imported");
                }
            }
            Hashes.Close(ref hashes);

            // Start timer for processing file events
            timer = new CTimer();
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            timer.Start();

            // Start monitoring file events
            monitor = new Monitor(settings, events);
            monitor.Start();

            Log.Close(ref log);
        }

        void timer_Tick(object sender, EventArgs e)
        {            
            while (!events.IsEmpty)
            {
                FileEvent evt;
                if (events.TryDequeue(out evt))
                {
                    if (!File.Exists(evt.FullPath)) // This happens when the same event are reported more than once
                        continue;
                    
                    string sum = FileOps.GetChecksum(evt.FullPath);

                    Hashes.Open(hashes);
                    if (!Hashes.HasChecksum(hashes, sum))                    
                    {
                        Log.AddMessage(log, "Importing " + evt.FullPath + " [" + sum + "]");

                        Report report = GetReport(evt.FullPath);
                        StoreReport(report);

                        Hashes.InsertChecksum(hashes, sum);
                    }
                    else
                    {
                        Log.AddMessage(log, "File " + evt.FullPath + " is already imported");
                    }
                    Hashes.Close(ref hashes);
                }
            }
        } 
       
        private Report GetReport(string filename)
        {
            Report report = new Report();

            // Parse spectrum            

            return report;
        }

        private void StoreReport(Report report)
        {
            // Store report in database            
        }

        public void LoadSettings()
        {
            if (!File.Exists(LorakonEnvironment.SettingsFile))
                return;

            // Deserialize settings from file
            using (StreamReader sr = new StreamReader(LorakonEnvironment.SettingsFile))
            {
                XmlSerializer x = new XmlSerializer(settings.GetType());
                settings = x.Deserialize(sr) as Settings;
            }
        }

        private void SaveSettings()
        {
            // Serialize settings to file
            using (StreamWriter sw = new StreamWriter(LorakonEnvironment.SettingsFile))
            {
                XmlSerializer x = new XmlSerializer(settings.GetType());
                x.Serialize(sw, settings);
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (MessageBox.Show("Er du sikker på at du vil stoppe mottak av spekterfiler?", "Informasjon", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            settings.LastShutdownTime = DateTime.Now;
            SaveSettings();

            monitor.Stop();
            timer.Stop();

            Log.Open(log);
            Log.AddMessage(log, "STOPPED");
            Log.Close(ref log);

            Application.Exit();
        }

        private void OnAbout(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Visible = true;
            tabs.SelectedTab = pageAbout;
        }

        private void OnSettings(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Visible = true;
            tabs.SelectedTab = pageSettings;
        }

        private void OnLog(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Visible = true;
            tabs.SelectedTab = pageLog;
        }

        private void FormLorakonSniff_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.TaskManagerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(tbSettingsWatchDirectory.Text))
            {
                MessageBox.Show("Kan ikke lagre en tom spectrum katalog");
                return;
            }

            if(!Directory.Exists(tbSettingsWatchDirectory.Text))
            {
                MessageBox.Show("Valgt spectrum katalog finnes ikke");
                return;
            }

            if (String.IsNullOrEmpty(tbSettingsConnectionString.Text))
            {
                MessageBox.Show("Kan ikke lagre en tom forbindelses streng");
                return;
            }

            settings.WatchDirectory = tbSettingsWatchDirectory.Text;
            settings.ConnectionString = tbSettingsConnectionString.Text;
            settings.FileFilter = tbSettingsSpectrumFilter.Text;

            SaveSettings();

            monitor = new Monitor(settings, events);
        }

        private void btnLogUpdate_Click(object sender, EventArgs e)
        {
            Log.Open(log);
            lbLog.DataSource = Log.GetEntries(log, dtLogFrom.Value, dtLogTo.Value);
            Log.Close(ref log);
        }
    }
}
