namespace LorakonSniff
{
    partial class FormLorakonSniff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLorakonSniff));
            this.status = new System.Windows.Forms.StatusStrip();
            this.tabs = new System.Windows.Forms.TabControl();
            this.pageAbout = new System.Windows.Forms.TabPage();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.pageLog = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tblSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSettingsBrowseWatchDirectory = new System.Windows.Forms.Button();
            this.tbSettingsWatchDirectory = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSettingsConnectionString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSettingsSpectrumFilter = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtLogFrom = new System.Windows.Forms.DateTimePicker();
            this.dtLogTo = new System.Windows.Forms.DateTimePicker();
            this.btnLogUpdate = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.pageLog.SuspendLayout();
            this.tblSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(0, 550);
            this.status.Name = "status";
            this.status.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.status.Size = new System.Drawing.Size(820, 22);
            this.status.TabIndex = 0;
            this.status.Text = "statusStrip1";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.pageAbout);
            this.tabs.Controls.Add(this.pageSettings);
            this.tabs.Controls.Add(this.pageLog);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(820, 550);
            this.tabs.TabIndex = 3;
            // 
            // pageAbout
            // 
            this.pageAbout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageAbout.Location = new System.Drawing.Point(4, 24);
            this.pageAbout.Name = "pageAbout";
            this.pageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.pageAbout.Size = new System.Drawing.Size(812, 522);
            this.pageAbout.TabIndex = 0;
            this.pageAbout.Text = "Lorakon Sniff";
            // 
            // pageSettings
            // 
            this.pageSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageSettings.Controls.Add(this.tblSettings);
            this.pageSettings.Controls.Add(this.panel2);
            this.pageSettings.Location = new System.Drawing.Point(4, 24);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.pageSettings.Size = new System.Drawing.Size(812, 522);
            this.pageSettings.TabIndex = 1;
            this.pageSettings.Text = "Innstillinger";
            // 
            // pageLog
            // 
            this.pageLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageLog.Controls.Add(this.lbLog);
            this.pageLog.Controls.Add(this.tableLayoutPanel1);
            this.pageLog.Location = new System.Drawing.Point(4, 24);
            this.pageLog.Name = "pageLog";
            this.pageLog.Padding = new System.Windows.Forms.Padding(3);
            this.pageLog.Size = new System.Drawing.Size(812, 522);
            this.pageLog.TabIndex = 2;
            this.pageLog.Text = "Logg";
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 15;
            this.lbLog.Location = new System.Drawing.Point(3, 98);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(806, 421);
            this.lbLog.TabIndex = 0;
            // 
            // tblSettings
            // 
            this.tblSettings.ColumnCount = 2;
            this.tblSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblSettings.Controls.Add(this.label1, 0, 1);
            this.tblSettings.Controls.Add(this.panel1, 1, 1);
            this.tblSettings.Controls.Add(this.label2, 0, 2);
            this.tblSettings.Controls.Add(this.tbSettingsConnectionString, 1, 2);
            this.tblSettings.Controls.Add(this.label3, 0, 3);
            this.tblSettings.Controls.Add(this.tbSettingsSpectrumFilter, 1, 3);
            this.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSettings.Location = new System.Drawing.Point(3, 3);
            this.tblSettings.Name = "tblSettings";
            this.tblSettings.RowCount = 5;
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSettings.Size = new System.Drawing.Size(806, 485);
            this.tblSettings.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Spektrum katalog";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbSettingsWatchDirectory);
            this.panel1.Controls.Add(this.btnSettingsBrowseWatchDirectory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(164, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 24);
            this.panel1.TabIndex = 1;
            // 
            // btnSettingsBrowseWatchDirectory
            // 
            this.btnSettingsBrowseWatchDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseWatchDirectory.Location = new System.Drawing.Point(564, 0);
            this.btnSettingsBrowseWatchDirectory.Name = "btnSettingsBrowseWatchDirectory";
            this.btnSettingsBrowseWatchDirectory.Size = new System.Drawing.Size(75, 24);
            this.btnSettingsBrowseWatchDirectory.TabIndex = 0;
            this.btnSettingsBrowseWatchDirectory.Text = "...";
            this.btnSettingsBrowseWatchDirectory.UseVisualStyleBackColor = true;
            // 
            // tbSettingsWatchDirectory
            // 
            this.tbSettingsWatchDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsWatchDirectory.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsWatchDirectory.Name = "tbSettingsWatchDirectory";
            this.tbSettingsWatchDirectory.ReadOnly = true;
            this.tbSettingsWatchDirectory.Size = new System.Drawing.Size(564, 21);
            this.tbSettingsWatchDirectory.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSettingsSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 488);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 31);
            this.panel2.TabIndex = 1;
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsSave.Location = new System.Drawing.Point(666, 0);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(140, 31);
            this.btnSettingsSave.TabIndex = 0;
            this.btnSettingsSave.Text = "Lagre";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Forbindelses streng";
            // 
            // tbSettingsConnectionString
            // 
            this.tbSettingsConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsConnectionString.Location = new System.Drawing.Point(164, 63);
            this.tbSettingsConnectionString.Name = "tbSettingsConnectionString";
            this.tbSettingsConnectionString.Size = new System.Drawing.Size(639, 21);
            this.tbSettingsConnectionString.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Spektrum filter";
            // 
            // tbSettingsSpectrumFilter
            // 
            this.tbSettingsSpectrumFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsSpectrumFilter.Location = new System.Drawing.Point(164, 93);
            this.tbSettingsSpectrumFilter.Name = "tbSettingsSpectrumFilter";
            this.tbSettingsSpectrumFilter.Size = new System.Drawing.Size(639, 21);
            this.tbSettingsSpectrumFilter.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtLogFrom, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtLogTo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLogUpdate, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(806, 95);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Til";
            // 
            // dtLogFrom
            // 
            this.dtLogFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtLogFrom.Location = new System.Drawing.Point(123, 33);
            this.dtLogFrom.Name = "dtLogFrom";
            this.dtLogFrom.Size = new System.Drawing.Size(276, 21);
            this.dtLogFrom.TabIndex = 2;
            // 
            // dtLogTo
            // 
            this.dtLogTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtLogTo.Location = new System.Drawing.Point(525, 33);
            this.dtLogTo.Name = "dtLogTo";
            this.dtLogTo.Size = new System.Drawing.Size(278, 21);
            this.dtLogTo.TabIndex = 3;
            // 
            // btnLogUpdate
            // 
            this.btnLogUpdate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLogUpdate.Location = new System.Drawing.Point(668, 63);
            this.btnLogUpdate.Name = "btnLogUpdate";
            this.btnLogUpdate.Size = new System.Drawing.Size(135, 29);
            this.btnLogUpdate.TabIndex = 4;
            this.btnLogUpdate.Text = "Oppdater";
            this.btnLogUpdate.UseVisualStyleBackColor = true;
            this.btnLogUpdate.Click += new System.EventHandler(this.btnLogUpdate_Click);
            // 
            // FormLorakonSniff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 572);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLorakonSniff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lorakon Sniff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLorakonSniff_FormClosing);
            this.Load += new System.EventHandler(this.FormLorakonSniff_Load);
            this.tabs.ResumeLayout(false);
            this.pageSettings.ResumeLayout(false);
            this.pageLog.ResumeLayout(false);
            this.tblSettings.ResumeLayout(false);
            this.tblSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage pageAbout;
        private System.Windows.Forms.TabPage pageSettings;
        private System.Windows.Forms.TabPage pageLog;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TableLayoutPanel tblSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSettingsWatchDirectory;
        private System.Windows.Forms.Button btnSettingsBrowseWatchDirectory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSettingsConnectionString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSettingsSpectrumFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtLogFrom;
        private System.Windows.Forms.DateTimePicker dtLogTo;
        private System.Windows.Forms.Button btnLogUpdate;
    }
}

