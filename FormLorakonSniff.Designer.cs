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
            this.tblSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbSettingsWatchDirectory = new System.Windows.Forms.TextBox();
            this.btnSettingsBrowseWatchDirectory = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSettingsConnectionString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSettingsSpectrumFilter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbSettingsImportedDirectory = new System.Windows.Forms.TextBox();
            this.btnSettingsBrowseImportedDirectory = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbSettingsFailedDirectory = new System.Windows.Forms.TextBox();
            this.btnSettingsBrowseFailedDirectory = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbSettingsOldDirectory = new System.Windows.Forms.TextBox();
            this.btnSettingsBrowseOldDirectory = new System.Windows.Forms.Button();
            this.cbSettingsDeleteOldSpectrums = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.pageLog = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnLogUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnSettingsBrowseWatchDirectory2 = new System.Windows.Forms.Button();
            this.tbSettingsWatchDirectory2 = new System.Windows.Forms.TextBox();
            this.cbSettingsDeleteImportedSpectrums = new System.Windows.Forms.CheckBox();
            this.tbAbout = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.pageAbout.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.tblSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pageLog.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(0, 588);
            this.status.Name = "status";
            this.status.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.status.Size = new System.Drawing.Size(937, 22);
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
            this.tabs.Size = new System.Drawing.Size(937, 588);
            this.tabs.TabIndex = 3;
            // 
            // pageAbout
            // 
            this.pageAbout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageAbout.Controls.Add(this.tbAbout);
            this.pageAbout.Location = new System.Drawing.Point(4, 25);
            this.pageAbout.Name = "pageAbout";
            this.pageAbout.Padding = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.pageAbout.Size = new System.Drawing.Size(929, 559);
            this.pageAbout.TabIndex = 0;
            this.pageAbout.Text = "Informasjon";
            // 
            // pageSettings
            // 
            this.pageSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageSettings.Controls.Add(this.tblSettings);
            this.pageSettings.Controls.Add(this.panel2);
            this.pageSettings.Location = new System.Drawing.Point(4, 25);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.pageSettings.Size = new System.Drawing.Size(929, 559);
            this.pageSettings.TabIndex = 1;
            this.pageSettings.Text = "Innstillinger";
            // 
            // tblSettings
            // 
            this.tblSettings.ColumnCount = 2;
            this.tblSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblSettings.Controls.Add(this.label1, 0, 1);
            this.tblSettings.Controls.Add(this.panel1, 1, 1);
            this.tblSettings.Controls.Add(this.label2, 0, 6);
            this.tblSettings.Controls.Add(this.tbSettingsConnectionString, 1, 6);
            this.tblSettings.Controls.Add(this.label3, 0, 7);
            this.tblSettings.Controls.Add(this.tbSettingsSpectrumFilter, 1, 7);
            this.tblSettings.Controls.Add(this.label6, 0, 3);
            this.tblSettings.Controls.Add(this.label7, 0, 5);
            this.tblSettings.Controls.Add(this.panel3, 1, 3);
            this.tblSettings.Controls.Add(this.panel4, 1, 5);
            this.tblSettings.Controls.Add(this.label8, 0, 4);
            this.tblSettings.Controls.Add(this.panel5, 1, 4);
            this.tblSettings.Controls.Add(this.cbSettingsDeleteOldSpectrums, 1, 8);
            this.tblSettings.Controls.Add(this.label4, 0, 2);
            this.tblSettings.Controls.Add(this.panel6, 1, 2);
            this.tblSettings.Controls.Add(this.cbSettingsDeleteImportedSpectrums, 1, 9);
            this.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSettings.Location = new System.Drawing.Point(3, 3);
            this.tblSettings.Name = "tblSettings";
            this.tblSettings.RowCount = 11;
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSettings.Size = new System.Drawing.Size(923, 520);
            this.tblSettings.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(159, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Innkommende spekter 1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbSettingsWatchDirectory);
            this.panel1.Controls.Add(this.btnSettingsBrowseWatchDirectory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(187, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 26);
            this.panel1.TabIndex = 1;
            // 
            // tbSettingsWatchDirectory
            // 
            this.tbSettingsWatchDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsWatchDirectory.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsWatchDirectory.Name = "tbSettingsWatchDirectory";
            this.tbSettingsWatchDirectory.ReadOnly = true;
            this.tbSettingsWatchDirectory.Size = new System.Drawing.Size(647, 23);
            this.tbSettingsWatchDirectory.TabIndex = 1;
            // 
            // btnSettingsBrowseWatchDirectory
            // 
            this.btnSettingsBrowseWatchDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseWatchDirectory.Location = new System.Drawing.Point(647, 0);
            this.btnSettingsBrowseWatchDirectory.Name = "btnSettingsBrowseWatchDirectory";
            this.btnSettingsBrowseWatchDirectory.Size = new System.Drawing.Size(86, 26);
            this.btnSettingsBrowseWatchDirectory.TabIndex = 0;
            this.btnSettingsBrowseWatchDirectory.Text = "...";
            this.btnSettingsBrowseWatchDirectory.UseVisualStyleBackColor = true;
            this.btnSettingsBrowseWatchDirectory.Click += new System.EventHandler(this.btnSettingsBrowseWatchDirectory_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 192);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(133, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Forbindelses streng";
            // 
            // tbSettingsConnectionString
            // 
            this.tbSettingsConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsConnectionString.Location = new System.Drawing.Point(187, 195);
            this.tbSettingsConnectionString.Name = "tbSettingsConnectionString";
            this.tbSettingsConnectionString.Size = new System.Drawing.Size(733, 23);
            this.tbSettingsConnectionString.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 224);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(99, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Spektrum filter";
            // 
            // tbSettingsSpectrumFilter
            // 
            this.tbSettingsSpectrumFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsSpectrumFilter.Location = new System.Drawing.Point(187, 227);
            this.tbSettingsSpectrumFilter.Name = "tbSettingsSpectrumFilter";
            this.tbSettingsSpectrumFilter.Size = new System.Drawing.Size(733, 23);
            this.tbSettingsSpectrumFilter.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 96);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label6.Size = new System.Drawing.Size(123, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Importerte spekter";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 160);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label7.Size = new System.Drawing.Size(105, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "Feilede spekter";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbSettingsImportedDirectory);
            this.panel3.Controls.Add(this.btnSettingsBrowseImportedDirectory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(187, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(733, 26);
            this.panel3.TabIndex = 8;
            // 
            // tbSettingsImportedDirectory
            // 
            this.tbSettingsImportedDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsImportedDirectory.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsImportedDirectory.Name = "tbSettingsImportedDirectory";
            this.tbSettingsImportedDirectory.ReadOnly = true;
            this.tbSettingsImportedDirectory.Size = new System.Drawing.Size(647, 23);
            this.tbSettingsImportedDirectory.TabIndex = 1;
            // 
            // btnSettingsBrowseImportedDirectory
            // 
            this.btnSettingsBrowseImportedDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseImportedDirectory.Location = new System.Drawing.Point(647, 0);
            this.btnSettingsBrowseImportedDirectory.Name = "btnSettingsBrowseImportedDirectory";
            this.btnSettingsBrowseImportedDirectory.Size = new System.Drawing.Size(86, 26);
            this.btnSettingsBrowseImportedDirectory.TabIndex = 0;
            this.btnSettingsBrowseImportedDirectory.Text = "...";
            this.btnSettingsBrowseImportedDirectory.UseVisualStyleBackColor = true;
            this.btnSettingsBrowseImportedDirectory.Click += new System.EventHandler(this.btnSettingsBrowseImportedDirectory_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbSettingsFailedDirectory);
            this.panel4.Controls.Add(this.btnSettingsBrowseFailedDirectory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(187, 163);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(733, 26);
            this.panel4.TabIndex = 9;
            // 
            // tbSettingsFailedDirectory
            // 
            this.tbSettingsFailedDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsFailedDirectory.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsFailedDirectory.Name = "tbSettingsFailedDirectory";
            this.tbSettingsFailedDirectory.ReadOnly = true;
            this.tbSettingsFailedDirectory.Size = new System.Drawing.Size(647, 23);
            this.tbSettingsFailedDirectory.TabIndex = 1;
            // 
            // btnSettingsBrowseFailedDirectory
            // 
            this.btnSettingsBrowseFailedDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseFailedDirectory.Location = new System.Drawing.Point(647, 0);
            this.btnSettingsBrowseFailedDirectory.Name = "btnSettingsBrowseFailedDirectory";
            this.btnSettingsBrowseFailedDirectory.Size = new System.Drawing.Size(86, 26);
            this.btnSettingsBrowseFailedDirectory.TabIndex = 0;
            this.btnSettingsBrowseFailedDirectory.Text = "...";
            this.btnSettingsBrowseFailedDirectory.UseVisualStyleBackColor = true;
            this.btnSettingsBrowseFailedDirectory.Click += new System.EventHandler(this.btnSettingsBrowseFailedDirectory_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 128);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label8.Size = new System.Drawing.Size(120, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Allerede importert";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbSettingsOldDirectory);
            this.panel5.Controls.Add(this.btnSettingsBrowseOldDirectory);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(187, 131);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(733, 26);
            this.panel5.TabIndex = 11;
            // 
            // tbSettingsOldDirectory
            // 
            this.tbSettingsOldDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsOldDirectory.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsOldDirectory.Name = "tbSettingsOldDirectory";
            this.tbSettingsOldDirectory.ReadOnly = true;
            this.tbSettingsOldDirectory.Size = new System.Drawing.Size(647, 23);
            this.tbSettingsOldDirectory.TabIndex = 1;
            // 
            // btnSettingsBrowseOldDirectory
            // 
            this.btnSettingsBrowseOldDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseOldDirectory.Location = new System.Drawing.Point(647, 0);
            this.btnSettingsBrowseOldDirectory.Name = "btnSettingsBrowseOldDirectory";
            this.btnSettingsBrowseOldDirectory.Size = new System.Drawing.Size(86, 26);
            this.btnSettingsBrowseOldDirectory.TabIndex = 0;
            this.btnSettingsBrowseOldDirectory.Text = "...";
            this.btnSettingsBrowseOldDirectory.UseVisualStyleBackColor = true;
            this.btnSettingsBrowseOldDirectory.Click += new System.EventHandler(this.btnSettingsBrowseOldDirectory_Click);
            // 
            // cbSettingsDeleteOldSpectrums
            // 
            this.cbSettingsDeleteOldSpectrums.AutoSize = true;
            this.cbSettingsDeleteOldSpectrums.Checked = true;
            this.cbSettingsDeleteOldSpectrums.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSettingsDeleteOldSpectrums.Location = new System.Drawing.Point(187, 259);
            this.cbSettingsDeleteOldSpectrums.Name = "cbSettingsDeleteOldSpectrums";
            this.cbSettingsDeleteOldSpectrums.Size = new System.Drawing.Size(268, 21);
            this.cbSettingsDeleteOldSpectrums.TabIndex = 12;
            this.cbSettingsDeleteOldSpectrums.Text = "Slett spekter som allerede er importert";
            this.cbSettingsDeleteOldSpectrums.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSettingsSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 523);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(923, 33);
            this.panel2.TabIndex = 1;
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsSave.Location = new System.Drawing.Point(763, 0);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(160, 33);
            this.btnSettingsSave.TabIndex = 0;
            this.btnSettingsSave.Text = "Lagre";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // pageLog
            // 
            this.pageLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pageLog.Controls.Add(this.tbLog);
            this.pageLog.Controls.Add(this.btnLogUpdate);
            this.pageLog.Location = new System.Drawing.Point(4, 25);
            this.pageLog.Name = "pageLog";
            this.pageLog.Padding = new System.Windows.Forms.Padding(3);
            this.pageLog.Size = new System.Drawing.Size(929, 559);
            this.pageLog.TabIndex = 2;
            this.pageLog.Text = "Logg";
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(3, 41);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(923, 515);
            this.tbLog.TabIndex = 2;
            this.tbLog.WordWrap = false;
            // 
            // btnLogUpdate
            // 
            this.btnLogUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogUpdate.Location = new System.Drawing.Point(3, 3);
            this.btnLogUpdate.Name = "btnLogUpdate";
            this.btnLogUpdate.Size = new System.Drawing.Size(923, 38);
            this.btnLogUpdate.TabIndex = 4;
            this.btnLogUpdate.Text = "Vis logg";
            this.btnLogUpdate.UseVisualStyleBackColor = true;
            this.btnLogUpdate.Click += new System.EventHandler(this.btnLogUpdate_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 64);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(159, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "Innkommende spekter 2";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tbSettingsWatchDirectory2);
            this.panel6.Controls.Add(this.btnSettingsBrowseWatchDirectory2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(187, 67);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(733, 26);
            this.panel6.TabIndex = 14;
            // 
            // btnSettingsBrowseWatchDirectory2
            // 
            this.btnSettingsBrowseWatchDirectory2.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettingsBrowseWatchDirectory2.Location = new System.Drawing.Point(647, 0);
            this.btnSettingsBrowseWatchDirectory2.Name = "btnSettingsBrowseWatchDirectory2";
            this.btnSettingsBrowseWatchDirectory2.Size = new System.Drawing.Size(86, 26);
            this.btnSettingsBrowseWatchDirectory2.TabIndex = 0;
            this.btnSettingsBrowseWatchDirectory2.Text = "...";
            this.btnSettingsBrowseWatchDirectory2.UseVisualStyleBackColor = true;
            this.btnSettingsBrowseWatchDirectory2.Click += new System.EventHandler(this.btnSettingsBrowseWatchDirectory2_Click);
            // 
            // tbSettingsWatchDirectory2
            // 
            this.tbSettingsWatchDirectory2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSettingsWatchDirectory2.Location = new System.Drawing.Point(0, 0);
            this.tbSettingsWatchDirectory2.Name = "tbSettingsWatchDirectory2";
            this.tbSettingsWatchDirectory2.ReadOnly = true;
            this.tbSettingsWatchDirectory2.Size = new System.Drawing.Size(647, 23);
            this.tbSettingsWatchDirectory2.TabIndex = 1;
            // 
            // cbSettingsDeleteImportedSpectrums
            // 
            this.cbSettingsDeleteImportedSpectrums.AutoSize = true;
            this.cbSettingsDeleteImportedSpectrums.Checked = true;
            this.cbSettingsDeleteImportedSpectrums.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSettingsDeleteImportedSpectrums.Location = new System.Drawing.Point(187, 291);
            this.cbSettingsDeleteImportedSpectrums.Name = "cbSettingsDeleteImportedSpectrums";
            this.cbSettingsDeleteImportedSpectrums.Size = new System.Drawing.Size(182, 21);
            this.cbSettingsDeleteImportedSpectrums.TabIndex = 15;
            this.cbSettingsDeleteImportedSpectrums.Text = "Slett spekter etter import";
            this.cbSettingsDeleteImportedSpectrums.UseVisualStyleBackColor = true;
            // 
            // tbAbout
            // 
            this.tbAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAbout.Location = new System.Drawing.Point(14, 13);
            this.tbAbout.Multiline = true;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.ReadOnly = true;
            this.tbAbout.Size = new System.Drawing.Size(901, 533);
            this.tbAbout.TabIndex = 0;
            // 
            // FormLorakonSniff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 610);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLorakonSniff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lorakon Sniff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLorakonSniff_FormClosing);
            this.Load += new System.EventHandler(this.FormLorakonSniff_Load);
            this.tabs.ResumeLayout(false);
            this.pageAbout.ResumeLayout(false);
            this.pageAbout.PerformLayout();
            this.pageSettings.ResumeLayout(false);
            this.tblSettings.ResumeLayout(false);
            this.tblSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pageLog.ResumeLayout(false);
            this.pageLog.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage pageAbout;
        private System.Windows.Forms.TabPage pageSettings;
        private System.Windows.Forms.TabPage pageLog;
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
        private System.Windows.Forms.Button btnLogUpdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSettingsBrowseImportedDirectory;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSettingsBrowseFailedDirectory;
        private System.Windows.Forms.TextBox tbSettingsImportedDirectory;
        private System.Windows.Forms.TextBox tbSettingsFailedDirectory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbSettingsOldDirectory;
        private System.Windows.Forms.Button btnSettingsBrowseOldDirectory;
        private System.Windows.Forms.CheckBox cbSettingsDeleteOldSpectrums;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox tbSettingsWatchDirectory2;
        private System.Windows.Forms.Button btnSettingsBrowseWatchDirectory2;
        private System.Windows.Forms.CheckBox cbSettingsDeleteImportedSpectrums;
        private System.Windows.Forms.TextBox tbAbout;
    }
}

