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
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using CTimer = System.Windows.Forms.Timer;

namespace LorakonSniff
{
    public partial class FormLorakonSniff : Form
    {        
        private ContextMenu trayMenu = null;
        private Log log = null;
        private Settings settings = null;                
        private CTimer timer = new CTimer();
        private Hashes hashes = null;
        List<string> newFiles = new List<string>();
        private string ReportExecutable;
        private string ReportTemplate;        

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
            try
            {
                log = new Log();
                log.AddMessage("STARTER SPEKTRUM IMPORT");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }

            try
            {
                hashes = new Hashes();

                string InstallationDirectory = 
                    Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + 
                    Path.DirectorySeparatorChar;
                ReportExecutable = InstallationDirectory + "report.exe";
                if (!File.Exists(ReportExecutable))
                {
                    log.AddMessage("Finner ikke filen: " + ReportExecutable);
                    Application.Exit();
                }

                ReportTemplate = InstallationDirectory + "report_template.tpl";
                if (!File.Exists(ReportTemplate))
                {
                    log.AddMessage("Finner ikke filen: " + ReportTemplate);
                    Application.Exit();
                }

                Visible = false;
                ShowInTaskbar = false;

                // Set default window layout
                Rectangle rect = Screen.FromControl(this).Bounds;
                Width = (rect.Right - rect.Left) / 2;
                Height = (rect.Bottom - rect.Top) / 2;
                Left = rect.Left + Width / 2;
                Top = rect.Top + Height / 2;

                // Create environment and load settings
                if (!Directory.Exists(LorakonEnvironment.SettingsPath))
                    Directory.CreateDirectory(LorakonEnvironment.SettingsPath);
                settings = new Settings();
                LoadSettings();                

                if (!Directory.Exists(settings.RootDirectory))
                    Directory.CreateDirectory(settings.RootDirectory);

                if (!Directory.Exists(settings.WatchDirectory))
                    Directory.CreateDirectory(settings.WatchDirectory);

                if (!Directory.Exists(settings.WatchDirectory2))
                    Directory.CreateDirectory(settings.WatchDirectory2);

                if (!Directory.Exists(settings.ImportedDirectory))
                    Directory.CreateDirectory(settings.ImportedDirectory);

                if (!Directory.Exists(settings.OldDirectory))
                    Directory.CreateDirectory(settings.OldDirectory);

                if (!Directory.Exists(settings.FailedDirectory))
                    Directory.CreateDirectory(settings.FailedDirectory);

                tbSettingsWatchDirectory.Text = settings.WatchDirectory;
                tbSettingsWatchDirectory2.Text = settings.WatchDirectory2;
                tbSettingsImportedDirectory.Text = settings.ImportedDirectory;
                tbSettingsOldDirectory.Text = settings.OldDirectory;
                tbSettingsFailedDirectory.Text = settings.FailedDirectory;
                tbSettingsConnectionString.Text = settings.ConnectionString;
                tbSettingsSpectrumFilter.Text = settings.FileFilter;
                cbSettingsDeleteOldSpectrums.Checked = settings.DeleteOldFiles;

                tbAbout.Text = "Lorakon Sniff" + Environment.NewLine + Environment.NewLine + 
                    "Import av innkommende spekterfiler";

                // Start import timer
                timer.Interval = 5000;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private bool IsFileReadyForRead(string filename)
        {
            try
            {
                using (FileStream fstream = File.Open(
                    filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return fstream.Length > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool WaitForReadAccess(string filename, int timeout)
        {
            int elapsed = 0, interval = 500;

            while (!IsFileReadyForRead(filename))
            {
                Thread.Sleep(interval);
                elapsed += interval;
                if (elapsed > timeout)
                    return false;
            }

            return true;            
        }

        private bool TryDeleteFile(string filename)
        {
            try
            {
                if(File.Exists(filename))
                    File.Delete(filename);
            }
            catch (Exception ex)
            {
                log.AddMessage("FEIL: " + ex.Message);
                return false;
            }

            return true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (newFiles.Count > 0)
                return;

            SqlConnection connection = null;

            try
            {
                if(Directory.Exists(settings.WatchDirectory))
                    newFiles.AddRange(Directory.GetFiles(
                        settings.WatchDirectory, 
                        settings.FileFilter, 
                        SearchOption.AllDirectories));

                if (Directory.Exists(settings.WatchDirectory2) && 
                    settings.WatchDirectory != settings.WatchDirectory2)
                    newFiles.AddRange(Directory.GetFiles(
                        settings.WatchDirectory2, 
                        settings.FileFilter, 
                        SearchOption.AllDirectories));

                if (newFiles.Count <= 0)
                    return;
                
                connection = new SqlConnection(settings.ConnectionString);
                connection.Open();

                foreach (string fname in newFiles)
                {
                    if (!WaitForReadAccess(fname, 10000))
                    {
                        log.AddMessage("FEIL: Får ikke tilgang til filen: " + fname);
                        continue;
                    }

                    string timestampString = DateTime.Now.Ticks.ToString() + "-";
                    byte[] spectrum = File.ReadAllBytes(fname);
                    string checksum = hashes.CalculateChecksum(spectrum);

                    if (!hashes.LookupChecksum(connection, checksum))
                    {
                        log.AddMessage("Genererer rapport: " + fname + " [" + checksum + "]");

                        string reportString = GenerateReport(fname);
                        SpectrumReport report = ParseReport(reportString);

                        string errorString;
                        if (ValidateReport(connection, report, out errorString))
                        {
                            log.AddMessage("Importerer: " + fname + " [" + checksum + "]");

                            Guid specId = StoreReport(connection, report, reportString, spectrum, 
                                Path.GetExtension(fname).ToUpper());
                            if (specId != Guid.Empty)
                            {
                                hashes.StoreChecksum(connection, checksum, specId);
                                if (cbSettingsDeleteImportedSpectrums.Checked)                                
                                    TryDeleteFile(fname);                                
                                else                                
                                    File.Move(fname, settings.ImportedDirectory + 
                                        Path.DirectorySeparatorChar + timestampString + 
                                        Path.GetFileName(fname));
                            }
                            else
                            {
                                File.Move(fname, settings.FailedDirectory + Path.DirectorySeparatorChar + 
                                    timestampString + Path.GetFileName(fname));
                            }
                        }
                        else
                        {
                            log.AddMessage("Ugyldig rapport: " + errorString + ": " + fname + " [" + checksum + "]");
                            File.Move(fname, settings.FailedDirectory + Path.DirectorySeparatorChar + 
                                timestampString + Path.GetFileName(fname));
                        }                                                
                    }
                    else
                    {
                        if (settings.DeleteOldFiles)
                        {
                            log.AddMessage("Sletter allerede importert: " + fname + " [" + checksum + "]");
                            TryDeleteFile(fname);
                        }
                        else
                        {
                            log.AddMessage("Flytter allerede importert: " + fname + " [" + checksum + "]");
                            File.Move(fname, settings.OldDirectory + Path.DirectorySeparatorChar + 
                                timestampString + Path.GetFileName(fname));
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                log.AddMessage("FEIL: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();

                newFiles.Clear();
            }
        }                
       
        private bool ValidateReport(SqlConnection connection, SpectrumReport report, out string errorString)
        {
            errorString = String.Empty;

            if (String.IsNullOrEmpty(report.SampleType))
            {
                errorString = "Mangler prøvetype";
                return false;
            }

            if (report.AcquisitionTime == DateTime.MinValue)
            {
                errorString = "Mangler prøvetakningsdato";
                return false;
            }

            if (report.Livetime <= 0)
            {
                errorString = "Ugyldig prøvetakningstid";
                return false;
            }

            if (String.IsNullOrEmpty(report.AccountIdentification))
            {
                errorString = "Mangler konto id";
                return false;
            }

            Guid guid = Guid.Empty;
            try
            {
                guid = Guid.Parse(report.AccountIdentification);
            }
            catch
            {
                errorString = "Ugyldig konto id";
                return false;
            }

            SqlCommand command = new SqlCommand("select count(*) from Account where ID = @ID", connection);
            command.Parameters.AddWithValue("@ID", MakeQueryParam(guid));
            object o = command.ExecuteScalar();
            if (o == null || o == DBNull.Value)
            {
                errorString = "Konto id finnes ikke";
                return false;
            }

            if (Convert.ToInt32(o) <= 0)
            {
                errorString = "Konto id finnes ikke";
                return false;
            }

            return true;
        }

        private string GenerateReport(string specfile)
        {            
            string args = "\"" + specfile + "\" /TEMPLATE=\"" + 
                ReportTemplate + "\" /SECTION=\"\" /NEWFILE /SCREEN";
            Process p = new Process();
            p.StartInfo.FileName = ReportExecutable;
            p.StartInfo.Arguments = args;                        
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            string cout = p.StandardOutput.ReadToEnd();
            string cerr = p.StandardError.ReadToEnd();

            p.WaitForExit();

            if(p.ExitCode != 0)            
                log.AddMessage("FEIL: report.exe: " + cerr);            

            return cout;            
        }

        private string JoinStringArray(string[] array, int startIndex)
        {        
            StringBuilder builder = new StringBuilder();
            for(int i = startIndex; i < array.Length; i++)            
            {
                builder.Append(array[i]);
                builder.Append(' ');
            }

            return builder.ToString();
        }

        private string ParseReport_ExtractParameter(string tag, string line)
        {
            if (line.StartsWith(tag))
            {
                string[] mainDelim = new string[] { ":::" };
                string[] items = line.Split(mainDelim, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length > 1)                    
                    return items[1].Replace('?', ' ').Trim();
            }

            return String.Empty;
        }

        private SpectrumReport ParseReport(string rep)
        {
            SpectrumReport report = new SpectrumReport();            
            
            StringReader reader = new StringReader(rep);
            string line, param;
            while((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                if ((param = ParseReport_ExtractParameter("Account Identification", line)) != String.Empty)
                    report.AccountIdentification = param;

                if ((param = ParseReport_ExtractParameter("Laboratory", line)) != String.Empty)
                    report.Laboratory = param;

                else if ((param = ParseReport_ExtractParameter("Operator", line)) != String.Empty)
                    report.Operator = param;

                else if ((param = ParseReport_ExtractParameter("Sample Title", line)) != String.Empty)
                    report.SampleTitle = param;

                else if ((param = ParseReport_ExtractParameter("Sample Identification", line)) != String.Empty)
                    report.SampleIdentification = param;

                else if ((param = ParseReport_ExtractParameter("Sample Type", line)) != String.Empty)
                    report.SampleType = param;

                else if ((param = ParseReport_ExtractParameter("Sample Component", line)) != String.Empty)
                    report.SampleComponent = param;

                else if ((param = ParseReport_ExtractParameter("Sample Geometry", line)) != String.Empty)
                    report.SampleGeometry = param;

                else if ((param = ParseReport_ExtractParameter("Sample Location Type", line)) != String.Empty)
                    report.SampleLocationType = param;

                else if ((param = ParseReport_ExtractParameter("Sample Location", line)) != String.Empty)
                    report.SampleLocation = param;

                else if ((param = ParseReport_ExtractParameter("Sample Community/County", line)) != String.Empty)
                    report.SampleCommunityCounty = param;

                else if ((param = ParseReport_ExtractParameter("Sample Coordinates", line)) != String.Empty)
                {
                    char[] wspace = new char[] { ' ', '\t' };
                    string[] coords = param.Split(wspace, StringSplitOptions.RemoveEmptyEntries);
                    if (coords.Length > 0)
                        report.SampleLatitude = Convert.ToDouble(coords[0], CultureInfo.InvariantCulture);
                    if (coords.Length > 1)
                        report.SampleLongitude = Convert.ToDouble(coords[1], CultureInfo.InvariantCulture);
                    if (coords.Length > 2)
                        report.SampleAltitude = Convert.ToDouble(coords[2], CultureInfo.InvariantCulture);
                }

                else if ((param = ParseReport_ExtractParameter("Sample Comment", line)) != String.Empty)
                    report.Comment = param;

                else if ((param = ParseReport_ExtractParameter("Sample Size/Error", line)) != String.Empty)
                {
                    char[] wspace = new char[] { ' ', '\t' };
                    string[] items = param.Split(wspace, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length > 0)
                        report.SampleSize = Convert.ToDouble(items[0], CultureInfo.InvariantCulture);
                    if (items.Length > 1)
                        report.SampleError = Convert.ToDouble(items[1], CultureInfo.InvariantCulture);
                    if (items.Length > 2)
                        report.SampleUnit = JoinStringArray(items, 2);
                }

                else if ((param = ParseReport_ExtractParameter("Sample Taken On", line)) != String.Empty)
                    report.SampleTime = Convert.ToDateTime(param);

                else if ((param = ParseReport_ExtractParameter("Acquisition Started", line)) != String.Empty)
                    report.AcquisitionTime = Convert.ToDateTime(param);

                else if ((param = ParseReport_ExtractParameter("Live Time", line)) != String.Empty)
                    report.Livetime = Convert.ToDouble(param, CultureInfo.InvariantCulture);

                else if ((param = ParseReport_ExtractParameter("Real Time", line)) != String.Empty)
                    report.Realtime = Convert.ToDouble(param, CultureInfo.InvariantCulture);

                else if ((param = ParseReport_ExtractParameter("Dead Time", line)) != String.Empty)
                    report.Deadtime = Convert.ToDouble(param, CultureInfo.InvariantCulture);

                else if ((param = ParseReport_ExtractParameter("Sigma", line)) != String.Empty)
                    report.Sigma = Convert.ToDouble(param, CultureInfo.InvariantCulture);

                else if ((param = ParseReport_ExtractParameter("Filename", line)) != String.Empty)
                    report.Filename = param;

                else if ((param = ParseReport_ExtractParameter("Background File", line)) != String.Empty)
                    report.BackgroundFile = param;

                else if ((param = ParseReport_ExtractParameter("Nuclide Library Used", line)) != String.Empty)
                    report.NuclideLibrary = param;

                else if (line.StartsWith("+++BKG+++"))
                    ParseReport_BKG(reader, report);

                else if (line.StartsWith("+++INTR+++"))
                    ParseReport_INTR(reader, report);

                else if (line.StartsWith("+++MDA+++"))
                    ParseReport_MDA(reader, report);                
            }            

            return report;
        }

        private void ParseReport_BKG(StringReader reader, SpectrumReport report)
        {
            report.Backgrounds.Clear();
            string line;
            char[] wspace = new char[] { ' ', '\t' };
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("---BKG---"))
                    return;

                string[] items = line.Split(wspace, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 5)
                {
                    SpectrumBackground background = new SpectrumBackground();
                    background.Energy = Convert.ToDouble(items[0].Trim(), CultureInfo.InvariantCulture);
                    background.OrigArea = Convert.ToDouble(items[1].Trim(), CultureInfo.InvariantCulture);
                    background.OrigAreaUncertainty = Convert.ToDouble(items[2].Trim(), CultureInfo.InvariantCulture);
                    background.SubtractedArea = Convert.ToDouble(items[3].Trim(), CultureInfo.InvariantCulture);
                    background.SubtractedAreaUncertainty = Convert.ToDouble(items[4].Trim(), CultureInfo.InvariantCulture);
                    report.Backgrounds.Add(background);
                }
            }
        }

        private void ParseReport_INTR(StringReader reader, SpectrumReport report)
        {
            report.Results.Clear();
            string line;
            char[] wspace = new char[] { ' ', '\t' };
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("---INTR---"))
                    return;
                
                string[] items = line.Split(wspace, StringSplitOptions.RemoveEmptyEntries);
                if(items.Length == 6)
                {
                    SpectrumResult result = new SpectrumResult();
                    result.NuclideName = items[0].Trim();
                    result.Activity = Convert.ToDouble(items[4], CultureInfo.InvariantCulture);
                    result.ActivityUncertainty = Convert.ToDouble(items[5], CultureInfo.InvariantCulture);
                    report.Results.Add(result);
                }
            }
        }

        private void ParseReport_MDA(StringReader reader, SpectrumReport report)
        {
            string line;
            char[] wspace = new char[] { ' ', '\t' };
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("---MDA---"))
                    return;

                string[] items = line.Split(wspace, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 7)
                {
                    string nuclname = items[0].Trim();
                    SpectrumResult r = report.Results.Find(x => x.NuclideName == nuclname);
                    if (r != null)
                        r.MDA = Convert.ToDouble(items[4].Trim(), CultureInfo.InvariantCulture);
                }
            }
        }        

        private object MakeQueryParam(object o)
        {
            if (o == null)
                return DBNull.Value;

            if(o.GetType() == typeof(DateTime))            
                if ((DateTime)o == DateTime.MinValue)
                    return DBNull.Value;

            return o;
        }

        private Guid StoreReport(SqlConnection connection, 
            SpectrumReport report, 
            string reportString, 
            byte[] spectrum, 
            string spectrumFileExtension)
        {
            SqlCommand command = null;
            Guid specId = Guid.Empty;

            try
            {
                specId = Guid.NewGuid();
                Guid accountId = Guid.Parse(report.AccountIdentification);
                DateTime now = DateTime.Now;

                command = new SqlCommand("proc_spectrum_info_insert", connection);
                command.Transaction = connection.BeginTransaction();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", specId);
                command.Parameters.AddWithValue("@AccountID", MakeQueryParam(accountId));
                command.Parameters.AddWithValue("@CreateDate", now);
                command.Parameters.AddWithValue("@UpdateDate", now);
                command.Parameters.AddWithValue("@AcquisitionDate", MakeQueryParam(report.AcquisitionTime));
                command.Parameters.AddWithValue("@ReferenceDate", MakeQueryParam(report.SampleTime));
                command.Parameters.AddWithValue("@Filename", MakeQueryParam(report.Filename));
                command.Parameters.AddWithValue("@BackgroundFile", MakeQueryParam(report.BackgroundFile));
                command.Parameters.AddWithValue("@LibraryFile", MakeQueryParam(report.NuclideLibrary));
                command.Parameters.AddWithValue("@Sigma", MakeQueryParam(report.Sigma));
                command.Parameters.AddWithValue("@SampleType", MakeQueryParam(report.SampleType));
                command.Parameters.AddWithValue("@Livetime", MakeQueryParam(report.Livetime));
                command.Parameters.AddWithValue("@Laberatory", MakeQueryParam(report.Laboratory));
                command.Parameters.AddWithValue("@Operator", MakeQueryParam(report.Operator));
                command.Parameters.AddWithValue("@SampleComponent", MakeQueryParam(report.SampleComponent));
                command.Parameters.AddWithValue("@Latitude", MakeQueryParam(report.SampleLatitude));
                command.Parameters.AddWithValue("@Longitude", MakeQueryParam(report.SampleLongitude));
                command.Parameters.AddWithValue("@Altitude", MakeQueryParam(report.SampleAltitude));
                command.Parameters.AddWithValue("@LocationType", MakeQueryParam(report.SampleLocationType));
                command.Parameters.AddWithValue("@Location", MakeQueryParam(report.SampleLocation));
                command.Parameters.AddWithValue("@Community", MakeQueryParam(report.SampleCommunityCounty));
                command.Parameters.AddWithValue("@SampleWeight", MakeQueryParam(report.SampleSize));
                command.Parameters.AddWithValue("@SampleWeightUnit", MakeQueryParam(report.SampleUnit));
                command.Parameters.AddWithValue("@SampleGeometry", MakeQueryParam(report.SampleGeometry));
                command.Parameters.AddWithValue("@ExternalID", MakeQueryParam(report.SampleIdentification));
                command.Parameters.AddWithValue("@Comment", MakeQueryParam(report.Comment));

                command.ExecuteNonQuery();

                command.CommandText = "proc_spectrum_background_insert";
                foreach (SpectrumBackground background in report.Backgrounds)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    command.Parameters.AddWithValue("@SpectrumInfoID", specId);
                    command.Parameters.AddWithValue("@CreateDate", now);
                    command.Parameters.AddWithValue("@UpdateDate", now);
                    command.Parameters.AddWithValue("@Energy", MakeQueryParam(background.Energy));
                    command.Parameters.AddWithValue("@OrigArea", MakeQueryParam(background.OrigArea));
                    command.Parameters.AddWithValue("@OrigAreaUncertainty", MakeQueryParam(background.OrigAreaUncertainty));
                    command.Parameters.AddWithValue("@SubtractedArea", MakeQueryParam(background.SubtractedArea));
                    command.Parameters.AddWithValue("@SubtractedAreaUncertainty", MakeQueryParam(background.SubtractedAreaUncertainty));

                    command.ExecuteNonQuery();
                }

                command.CommandText = "proc_spectrum_result_insert";
                foreach (SpectrumResult result in report.Results)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    command.Parameters.AddWithValue("@SpectrumInfoID", specId);
                    command.Parameters.AddWithValue("@CreateDate", now);
                    command.Parameters.AddWithValue("@UpdateDate", now);
                    command.Parameters.AddWithValue("@NuclideName", MakeQueryParam(result.NuclideName));
                    command.Parameters.AddWithValue("@Activity", MakeQueryParam(result.Activity));
                    command.Parameters.AddWithValue("@ActivityUncertainty", MakeQueryParam(result.ActivityUncertainty));
                    command.Parameters.AddWithValue("@MDA", MakeQueryParam(result.MDA));
                    command.Parameters.AddWithValue("@Evaluated", 0);
                    command.Parameters.AddWithValue("@Approved", 0);
                    command.Parameters.AddWithValue("@Comment", "");

                    command.ExecuteNonQuery();
                }

                command.CommandText = "proc_spectrum_file_insert";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                command.Parameters.AddWithValue("@SpectrumInfoID", specId);
                command.Parameters.AddWithValue("@CreateDate", now);
                command.Parameters.AddWithValue("@UpdateDate", now);
                command.Parameters.AddWithValue("@SpectrumFileExtension", spectrumFileExtension);
                command.Parameters.AddWithValue("@SpectrumFileContent", spectrum);
                command.Parameters.AddWithValue("@ReportFileContent", reportString);

                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch(Exception ex)
            {
                if(command != null && command.Transaction != null)
                    command.Transaction.Rollback();

                log.AddMessage("FEIL: " + ex.Message);
                return Guid.Empty;
            }            

            return specId;
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
            if (MessageBox.Show("Er du sikker på at du vil stoppe mottak av spekterfiler?", "Informasjon", 
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            settings.LastShutdownTime = DateTime.Now;
            SaveSettings();

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
            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.ApplicationExitCall || 
                e.CloseReason == CloseReason.TaskManagerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSettingsConnectionString.Text))
            {
                MessageBox.Show("Forbindelses streng må fylles inn");
                return;
            }

            if (String.IsNullOrEmpty(tbSettingsWatchDirectory.Text))
            {
                MessageBox.Show("Katalog for nye spekter må fylles inn");
                return;
            }

            if(!Directory.Exists(tbSettingsWatchDirectory.Text))
            {
                MessageBox.Show("Katalog for nye spekter finnes ikke");
                return;
            }

            if (!Directory.Exists(tbSettingsWatchDirectory2.Text))
            {
                MessageBox.Show("Katalog for nye spekter 2 finnes ikke");
                return;
            }

            if (tbSettingsWatchDirectory.Text == tbSettingsWatchDirectory2.Text)
            {
                tbSettingsWatchDirectory2.Text = String.Empty;
            }

            if (String.IsNullOrEmpty(tbSettingsImportedDirectory.Text))
            {
                MessageBox.Show("Katalog for importerte spekter må fylles inn");
                return;
            }

            if (!Directory.Exists(tbSettingsImportedDirectory.Text))
                Directory.CreateDirectory(tbSettingsImportedDirectory.Text);

            if (String.IsNullOrEmpty(tbSettingsOldDirectory.Text))
            {
                MessageBox.Show("Katalog for allerede eksisterende spekter må fylles inn");
                return;
            }

            if (!Directory.Exists(tbSettingsOldDirectory.Text))
                Directory.CreateDirectory(tbSettingsOldDirectory.Text);

            if (String.IsNullOrEmpty(tbSettingsFailedDirectory.Text))
            {
                MessageBox.Show("Katalog for feilede spekter må fylles inn");
                return;
            }

            if (!Directory.Exists(tbSettingsFailedDirectory.Text))
                Directory.CreateDirectory(tbSettingsFailedDirectory.Text);            

            settings.WatchDirectory = tbSettingsWatchDirectory.Text;
            settings.WatchDirectory2 = tbSettingsWatchDirectory2.Text;
            settings.ImportedDirectory = tbSettingsImportedDirectory.Text;
            settings.OldDirectory = tbSettingsOldDirectory.Text;
            settings.FailedDirectory = tbSettingsFailedDirectory.Text;
            settings.ConnectionString = tbSettingsConnectionString.Text;
            settings.FileFilter = tbSettingsSpectrumFilter.Text;
            settings.DeleteOldFiles = cbSettingsDeleteOldSpectrums.Checked;

            SaveSettings();
        }

        private void btnLogUpdate_Click(object sender, EventArgs e)
        {            
            tbLog.Text = File.ReadAllText(LorakonEnvironment.LogFile);
        }

        private void btnSettingsBrowseWatchDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            tbSettingsWatchDirectory.Text = diag.SelectedPath;
        }

        private void btnSettingsBrowseWatchDirectory2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            tbSettingsWatchDirectory2.Text = diag.SelectedPath;
        }

        private void btnSettingsBrowseImportedDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            tbSettingsImportedDirectory.Text = diag.SelectedPath;
        }

        private void btnSettingsBrowseFailedDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            tbSettingsFailedDirectory.Text = diag.SelectedPath;
        }

        private void btnSettingsBrowseOldDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            tbSettingsOldDirectory.Text = diag.SelectedPath;
        }        
    }
}
