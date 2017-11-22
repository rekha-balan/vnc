namespace SupportTools_Excel.User_Interface.Forms
{
    partial class frmExcel_FolderMaps
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.gbContents = new System.Windows.Forms.GroupBox();
            this.pnlPatternMatchFileColor = new System.Windows.Forms.Panel();
            this.Label16 = new System.Windows.Forms.Label();
            this.pnlFolderHighlightColor = new System.Windows.Forms.Panel();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtFolderMatchPattern = new System.Windows.Forms.TextBox();
            this.chkPatternMatchFolderHighlight = new System.Windows.Forms.CheckBox();
            this.chkSkipFoldersWithNoFiles = new System.Windows.Forms.CheckBox();
            this.chkPatternMatchFileOutput = new System.Windows.Forms.CheckBox();
            this.txtFileMatchPattern = new System.Windows.Forms.TextBox();
            this.pnlNoAccessColor = new System.Windows.Forms.Panel();
            this.Label14 = new System.Windows.Forms.Label();
            this.chkShowFolders = new System.Windows.Forms.CheckBox();
            this.pnlPathTooLongColor = new System.Windows.Forms.Panel();
            this.spnLimitLevel = new System.Windows.Forms.VScrollBar();
            this.spnGroupLevel = new System.Windows.Forms.VScrollBar();
            this.chkShowFiles = new System.Windows.Forms.CheckBox();
            this.chkGroupResults = new System.Windows.Forms.CheckBox();
            this.chkLimitLevels = new System.Windows.Forms.CheckBox();
            this.txtLimitLevel = new System.Windows.Forms.TextBox();
            this.txtGroupLevel = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnlMonthAccessedColor = new System.Windows.Forms.Panel();
            this.Label13 = new System.Windows.Forms.Label();
            this.chkFileNameLength = new System.Windows.Forms.CheckBox();
            this.pnlMonthWrittenColor = new System.Windows.Forms.Panel();
            this.pnlMonthCreatedColor = new System.Windows.Forms.Panel();
            this.pnlDefaultColor = new System.Windows.Forms.Panel();
            this.pnlIllegalCharactersColor = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.pnlIllegalFileNameLengthColor = new System.Windows.Forms.Panel();
            this.Label12 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMaxFileNameLength = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.txtIllegalFolderCharacters = new System.Windows.Forms.TextBox();
            this.chkIllegalCharacters = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtIllegalFileCharacters = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtStartingFolder = new System.Windows.Forms.TextBox();
            this.gbStartingFolder = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMonthsSinceCreated = new System.Windows.Forms.TextBox();
            this.spnMonthsSinceCreated = new System.Windows.Forms.VScrollBar();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtMonthsSinceAccessed = new System.Windows.Forms.TextBox();
            this.txtMonthsSinceWritten = new System.Windows.Forms.TextBox();
            this.spnMonthsSinceAccessed = new System.Windows.Forms.VScrollBar();
            this.spnMonthsSinceWritten = new System.Windows.Forms.VScrollBar();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkColorCodeDates = new System.Windows.Forms.CheckBox();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdCreateFolderMap = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.chkTableStyleOutput = new System.Windows.Forms.CheckBox();
            this.gbContents.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.gbStartingFolder.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbContents
            // 
            this.gbContents.Controls.Add(this.chkTableStyleOutput);
            this.gbContents.Controls.Add(this.pnlPatternMatchFileColor);
            this.gbContents.Controls.Add(this.Label16);
            this.gbContents.Controls.Add(this.pnlFolderHighlightColor);
            this.gbContents.Controls.Add(this.Label15);
            this.gbContents.Controls.Add(this.txtFolderMatchPattern);
            this.gbContents.Controls.Add(this.chkPatternMatchFolderHighlight);
            this.gbContents.Controls.Add(this.chkSkipFoldersWithNoFiles);
            this.gbContents.Controls.Add(this.chkPatternMatchFileOutput);
            this.gbContents.Controls.Add(this.txtFileMatchPattern);
            this.gbContents.Controls.Add(this.pnlNoAccessColor);
            this.gbContents.Controls.Add(this.Label14);
            this.gbContents.Controls.Add(this.chkShowFolders);
            this.gbContents.Controls.Add(this.pnlPathTooLongColor);
            this.gbContents.Controls.Add(this.spnLimitLevel);
            this.gbContents.Controls.Add(this.spnGroupLevel);
            this.gbContents.Controls.Add(this.chkShowFiles);
            this.gbContents.Controls.Add(this.chkGroupResults);
            this.gbContents.Controls.Add(this.chkLimitLevels);
            this.gbContents.Controls.Add(this.txtLimitLevel);
            this.gbContents.Controls.Add(this.txtGroupLevel);
            this.gbContents.Controls.Add(this.Label8);
            this.gbContents.Location = new System.Drawing.Point(20, 127);
            this.gbContents.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbContents.Name = "gbContents";
            this.gbContents.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbContents.Size = new System.Drawing.Size(724, 453);
            this.gbContents.TabIndex = 11;
            this.gbContents.TabStop = false;
            this.gbContents.Text = "Contents";
            // 
            // pnlPatternMatchFileColor
            // 
            this.pnlPatternMatchFileColor.BackColor = System.Drawing.Color.Lime;
            this.pnlPatternMatchFileColor.Location = new System.Drawing.Point(652, 315);
            this.pnlPatternMatchFileColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlPatternMatchFileColor.Name = "pnlPatternMatchFileColor";
            this.pnlPatternMatchFileColor.Size = new System.Drawing.Size(50, 33);
            this.pnlPatternMatchFileColor.TabIndex = 29;
            this.pnlPatternMatchFileColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(420, 321);
            this.Label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(220, 25);
            this.Label16.TabIndex = 30;
            this.Label16.Text = "Folder Highlight Color";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFolderHighlightColor
            // 
            this.pnlFolderHighlightColor.BackColor = System.Drawing.Color.Lime;
            this.pnlFolderHighlightColor.Location = new System.Drawing.Point(652, 198);
            this.pnlFolderHighlightColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlFolderHighlightColor.Name = "pnlFolderHighlightColor";
            this.pnlFolderHighlightColor.Size = new System.Drawing.Size(50, 33);
            this.pnlFolderHighlightColor.TabIndex = 27;
            this.pnlFolderHighlightColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(420, 206);
            this.Label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(220, 25);
            this.Label15.TabIndex = 28;
            this.Label15.Text = "Folder Highlight Color";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolderMatchPattern
            // 
            this.txtFolderMatchPattern.Location = new System.Drawing.Point(12, 248);
            this.txtFolderMatchPattern.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFolderMatchPattern.Name = "txtFolderMatchPattern";
            this.txtFolderMatchPattern.Size = new System.Drawing.Size(686, 31);
            this.txtFolderMatchPattern.TabIndex = 26;
            this.ToolTips.SetToolTip(this.txtFolderMatchPattern, "Regular Expression to match folders");
            // 
            // chkPatternMatchFolderHighlight
            // 
            this.chkPatternMatchFolderHighlight.Location = new System.Drawing.Point(16, 196);
            this.chkPatternMatchFolderHighlight.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkPatternMatchFolderHighlight.Name = "chkPatternMatchFolderHighlight";
            this.chkPatternMatchFolderHighlight.Size = new System.Drawing.Size(420, 46);
            this.chkPatternMatchFolderHighlight.TabIndex = 25;
            this.chkPatternMatchFolderHighlight.Text = "RegEx Pattern Match Folder Highlight";
            this.chkPatternMatchFolderHighlight.CheckedChanged += new System.EventHandler(this.chkPatternMatchFolderHighlight_CheckedChanged);
            // 
            // chkSkipFoldersWithNoFiles
            // 
            this.chkSkipFoldersWithNoFiles.Location = new System.Drawing.Point(382, 138);
            this.chkSkipFoldersWithNoFiles.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSkipFoldersWithNoFiles.Name = "chkSkipFoldersWithNoFiles";
            this.chkSkipFoldersWithNoFiles.Size = new System.Drawing.Size(324, 46);
            this.chkSkipFoldersWithNoFiles.TabIndex = 24;
            this.chkSkipFoldersWithNoFiles.Text = "Skip Folders with No Files";
            this.chkSkipFoldersWithNoFiles.CheckedChanged += new System.EventHandler(this.chkSkipFoldersWithNoFiles_CheckedChanged);
            // 
            // chkPatternMatchFileOutput
            // 
            this.chkPatternMatchFileOutput.Enabled = false;
            this.chkPatternMatchFileOutput.Location = new System.Drawing.Point(16, 312);
            this.chkPatternMatchFileOutput.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkPatternMatchFileOutput.Name = "chkPatternMatchFileOutput";
            this.chkPatternMatchFileOutput.Size = new System.Drawing.Size(390, 46);
            this.chkPatternMatchFileOutput.TabIndex = 23;
            this.chkPatternMatchFileOutput.Text = "RegEx Pattern Match File Output";
            this.chkPatternMatchFileOutput.CheckedChanged += new System.EventHandler(this.chkPatternMatchFileOutput_CheckedChanged);
            // 
            // txtFileMatchPattern
            // 
            this.txtFileMatchPattern.Enabled = false;
            this.txtFileMatchPattern.Location = new System.Drawing.Point(16, 363);
            this.txtFileMatchPattern.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFileMatchPattern.Name = "txtFileMatchPattern";
            this.txtFileMatchPattern.Size = new System.Drawing.Size(682, 31);
            this.txtFileMatchPattern.TabIndex = 2;
            this.ToolTips.SetToolTip(this.txtFileMatchPattern, "Regular Expression to match files");
            // 
            // pnlNoAccessColor
            // 
            this.pnlNoAccessColor.BackColor = System.Drawing.Color.Violet;
            this.pnlNoAccessColor.Location = new System.Drawing.Point(202, 27);
            this.pnlNoAccessColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlNoAccessColor.Name = "pnlNoAccessColor";
            this.pnlNoAccessColor.Size = new System.Drawing.Size(50, 33);
            this.pnlNoAccessColor.TabIndex = 20;
            this.pnlNoAccessColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(18, 31);
            this.Label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(166, 25);
            this.Label14.TabIndex = 21;
            this.Label14.Text = "NoAccess Color";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkShowFolders
            // 
            this.chkShowFolders.Checked = true;
            this.chkShowFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowFolders.Location = new System.Drawing.Point(16, 138);
            this.chkShowFolders.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkShowFolders.Name = "chkShowFolders";
            this.chkShowFolders.Size = new System.Drawing.Size(186, 46);
            this.chkShowFolders.TabIndex = 20;
            this.chkShowFolders.Text = "Show Folders";
            this.chkShowFolders.CheckedChanged += new System.EventHandler(this.chkShowFolders_CheckedChanged);
            // 
            // pnlPathTooLongColor
            // 
            this.pnlPathTooLongColor.BackColor = System.Drawing.Color.Cyan;
            this.pnlPathTooLongColor.Location = new System.Drawing.Point(502, 23);
            this.pnlPathTooLongColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlPathTooLongColor.Name = "pnlPathTooLongColor";
            this.pnlPathTooLongColor.Size = new System.Drawing.Size(50, 33);
            this.pnlPathTooLongColor.TabIndex = 19;
            this.pnlPathTooLongColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // spnLimitLevel
            // 
            this.spnLimitLevel.Location = new System.Drawing.Point(278, 83);
            this.spnLimitLevel.Name = "spnLimitLevel";
            this.spnLimitLevel.Size = new System.Drawing.Size(16, 38);
            this.spnLimitLevel.TabIndex = 5;
            this.spnLimitLevel.ValueChanged += new System.EventHandler(this.spnLimitLevel_ValueChanged);
            // 
            // spnGroupLevel
            // 
            this.spnGroupLevel.Location = new System.Drawing.Point(660, 87);
            this.spnGroupLevel.Name = "spnGroupLevel";
            this.spnGroupLevel.Size = new System.Drawing.Size(16, 38);
            this.spnGroupLevel.TabIndex = 4;
            this.spnGroupLevel.ValueChanged += new System.EventHandler(this.spnGroupLevel_ValueChanged);
            // 
            // chkShowFiles
            // 
            this.chkShowFiles.Location = new System.Drawing.Point(214, 138);
            this.chkShowFiles.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkShowFiles.Name = "chkShowFiles";
            this.chkShowFiles.Size = new System.Drawing.Size(208, 46);
            this.chkShowFiles.TabIndex = 3;
            this.chkShowFiles.Text = "Show Files";
            this.ToolTips.SetToolTip(this.chkShowFiles, "Output contains files");
            this.chkShowFiles.CheckedChanged += new System.EventHandler(this.chkShowFiles_CheckedChanged);
            // 
            // chkGroupResults
            // 
            this.chkGroupResults.Location = new System.Drawing.Point(382, 79);
            this.chkGroupResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkGroupResults.Name = "chkGroupResults";
            this.chkGroupResults.Size = new System.Drawing.Size(208, 46);
            this.chkGroupResults.TabIndex = 2;
            this.chkGroupResults.Text = "Group Results";
            this.chkGroupResults.CheckedChanged += new System.EventHandler(this.chkGroupResults_CheckedChanged);
            // 
            // chkLimitLevels
            // 
            this.chkLimitLevels.Location = new System.Drawing.Point(16, 79);
            this.chkLimitLevels.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkLimitLevels.Name = "chkLimitLevels";
            this.chkLimitLevels.Size = new System.Drawing.Size(178, 46);
            this.chkLimitLevels.TabIndex = 1;
            this.chkLimitLevels.Text = "Limit Levels";
            this.chkLimitLevels.CheckedChanged += new System.EventHandler(this.chkLimitLevels_CheckedChanged);
            // 
            // txtLimitLevel
            // 
            this.txtLimitLevel.Location = new System.Drawing.Point(214, 83);
            this.txtLimitLevel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLimitLevel.Name = "txtLimitLevel";
            this.txtLimitLevel.Size = new System.Drawing.Size(44, 31);
            this.txtLimitLevel.TabIndex = 2;
            this.txtLimitLevel.Text = "1";
            this.txtLimitLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGroupLevel
            // 
            this.txtGroupLevel.Location = new System.Drawing.Point(596, 87);
            this.txtGroupLevel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtGroupLevel.Name = "txtGroupLevel";
            this.txtGroupLevel.Size = new System.Drawing.Size(44, 31);
            this.txtGroupLevel.TabIndex = 1;
            this.txtGroupLevel.Text = "1";
            this.txtGroupLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(286, 31);
            this.Label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(198, 25);
            this.Label8.TabIndex = 8;
            this.Label8.Text = "PathTooLong Color";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMonthAccessedColor
            // 
            this.pnlMonthAccessedColor.BackColor = System.Drawing.Color.Blue;
            this.pnlMonthAccessedColor.Location = new System.Drawing.Point(652, 219);
            this.pnlMonthAccessedColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlMonthAccessedColor.Name = "pnlMonthAccessedColor";
            this.pnlMonthAccessedColor.Size = new System.Drawing.Size(50, 33);
            this.pnlMonthAccessedColor.TabIndex = 19;
            this.pnlMonthAccessedColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(346, 188);
            this.Label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(294, 25);
            this.Label13.TabIndex = 20;
            this.Label13.Text = "Illegal FileName Length Color";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFileNameLength
            // 
            this.chkFileNameLength.AutoSize = true;
            this.chkFileNameLength.Location = new System.Drawing.Point(16, 187);
            this.chkFileNameLength.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkFileNameLength.Name = "chkFileNameLength";
            this.chkFileNameLength.Size = new System.Drawing.Size(274, 29);
            this.chkFileNameLength.TabIndex = 18;
            this.chkFileNameLength.Text = "Check FileName Length";
            this.chkFileNameLength.UseVisualStyleBackColor = true;
            this.chkFileNameLength.CheckedChanged += new System.EventHandler(this.chkFileNameLength_CheckedChanged);
            // 
            // pnlMonthWrittenColor
            // 
            this.pnlMonthWrittenColor.BackColor = System.Drawing.Color.Green;
            this.pnlMonthWrittenColor.Location = new System.Drawing.Point(652, 150);
            this.pnlMonthWrittenColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlMonthWrittenColor.Name = "pnlMonthWrittenColor";
            this.pnlMonthWrittenColor.Size = new System.Drawing.Size(50, 33);
            this.pnlMonthWrittenColor.TabIndex = 19;
            this.pnlMonthWrittenColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // pnlMonthCreatedColor
            // 
            this.pnlMonthCreatedColor.BackColor = System.Drawing.Color.Red;
            this.pnlMonthCreatedColor.Location = new System.Drawing.Point(652, 92);
            this.pnlMonthCreatedColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlMonthCreatedColor.Name = "pnlMonthCreatedColor";
            this.pnlMonthCreatedColor.Size = new System.Drawing.Size(50, 33);
            this.pnlMonthCreatedColor.TabIndex = 19;
            this.pnlMonthCreatedColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // pnlDefaultColor
            // 
            this.pnlDefaultColor.BackColor = System.Drawing.Color.Black;
            this.pnlDefaultColor.Location = new System.Drawing.Point(652, 29);
            this.pnlDefaultColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlDefaultColor.Name = "pnlDefaultColor";
            this.pnlDefaultColor.Size = new System.Drawing.Size(50, 33);
            this.pnlDefaultColor.TabIndex = 18;
            this.pnlDefaultColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // pnlIllegalCharactersColor
            // 
            this.pnlIllegalCharactersColor.BackColor = System.Drawing.Color.Orange;
            this.pnlIllegalCharactersColor.Location = new System.Drawing.Point(652, 27);
            this.pnlIllegalCharactersColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlIllegalCharactersColor.Name = "pnlIllegalCharactersColor";
            this.pnlIllegalCharactersColor.Size = new System.Drawing.Size(50, 33);
            this.pnlIllegalCharactersColor.TabIndex = 19;
            this.pnlIllegalCharactersColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(30, 100);
            this.Label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(225, 25);
            this.Label6.TabIndex = 17;
            this.Label6.Text = "Months Since Created";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlIllegalFileNameLengthColor
            // 
            this.pnlIllegalFileNameLengthColor.BackColor = System.Drawing.Color.Cyan;
            this.pnlIllegalFileNameLengthColor.Location = new System.Drawing.Point(652, 181);
            this.pnlIllegalFileNameLengthColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlIllegalFileNameLengthColor.Name = "pnlIllegalFileNameLengthColor";
            this.pnlIllegalFileNameLengthColor.Size = new System.Drawing.Size(50, 33);
            this.pnlIllegalFileNameLengthColor.TabIndex = 19;
            this.pnlIllegalFileNameLengthColor.DoubleClick += new System.EventHandler(this.ColorBox_DoubleClick);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(14, 237);
            this.Label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(279, 25);
            this.Label12.TabIndex = 17;
            this.Label12.Text = "Maximum File Name Length";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.pnlIllegalFileNameLengthColor);
            this.GroupBox2.Controls.Add(this.pnlIllegalCharactersColor);
            this.GroupBox2.Controls.Add(this.Label13);
            this.GroupBox2.Controls.Add(this.chkFileNameLength);
            this.GroupBox2.Controls.Add(this.Label12);
            this.GroupBox2.Controls.Add(this.txtMaxFileNameLength);
            this.GroupBox2.Controls.Add(this.Label11);
            this.GroupBox2.Controls.Add(this.txtIllegalFolderCharacters);
            this.GroupBox2.Controls.Add(this.chkIllegalCharacters);
            this.GroupBox2.Controls.Add(this.Label10);
            this.GroupBox2.Controls.Add(this.txtIllegalFileCharacters);
            this.GroupBox2.Controls.Add(this.Label9);
            this.GroupBox2.Location = new System.Drawing.Point(20, 880);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GroupBox2.Size = new System.Drawing.Size(724, 281);
            this.GroupBox2.TabIndex = 15;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "SharePoint Information";
            // 
            // txtMaxFileNameLength
            // 
            this.txtMaxFileNameLength.Location = new System.Drawing.Point(310, 231);
            this.txtMaxFileNameLength.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMaxFileNameLength.Name = "txtMaxFileNameLength";
            this.txtMaxFileNameLength.Size = new System.Drawing.Size(98, 31);
            this.txtMaxFileNameLength.TabIndex = 16;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(48, 127);
            this.Label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(246, 25);
            this.Label11.TabIndex = 15;
            this.Label11.Text = "Illegal Folder Characters";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIllegalFolderCharacters
            // 
            this.txtIllegalFolderCharacters.Location = new System.Drawing.Point(310, 121);
            this.txtIllegalFolderCharacters.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtIllegalFolderCharacters.Name = "txtIllegalFolderCharacters";
            this.txtIllegalFolderCharacters.Size = new System.Drawing.Size(266, 31);
            this.txtIllegalFolderCharacters.TabIndex = 14;
            // 
            // chkIllegalCharacters
            // 
            this.chkIllegalCharacters.AutoSize = true;
            this.chkIllegalCharacters.Location = new System.Drawing.Point(16, 33);
            this.chkIllegalCharacters.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkIllegalCharacters.Name = "chkIllegalCharacters";
            this.chkIllegalCharacters.Size = new System.Drawing.Size(309, 29);
            this.chkIllegalCharacters.TabIndex = 13;
            this.chkIllegalCharacters.Text = "Check for Illegal Characters";
            this.chkIllegalCharacters.UseVisualStyleBackColor = true;
            this.chkIllegalCharacters.CheckedChanged += new System.EventHandler(this.chkIllegalCharacters_CheckedChanged);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(74, 79);
            this.Label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(220, 25);
            this.Label10.TabIndex = 12;
            this.Label10.Text = "Illegal File Characters";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIllegalFileCharacters
            // 
            this.txtIllegalFileCharacters.Location = new System.Drawing.Point(310, 73);
            this.txtIllegalFileCharacters.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtIllegalFileCharacters.Name = "txtIllegalFileCharacters";
            this.txtIllegalFileCharacters.Size = new System.Drawing.Size(266, 31);
            this.txtIllegalFileCharacters.TabIndex = 11;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(404, 35);
            this.Label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(236, 25);
            this.Label9.TabIndex = 10;
            this.Label9.Text = "Illegal Characters Color";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartingFolder
            // 
            this.txtStartingFolder.Location = new System.Drawing.Point(16, 37);
            this.txtStartingFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtStartingFolder.Name = "txtStartingFolder";
            this.txtStartingFolder.Size = new System.Drawing.Size(552, 31);
            this.txtStartingFolder.TabIndex = 0;
            this.ToolTips.SetToolTip(this.txtStartingFolder, "Enter folder or click Pick.  Pick starts from what is entered.");
            // 
            // gbStartingFolder
            // 
            this.gbStartingFolder.Controls.Add(this.btnSelectFolder);
            this.gbStartingFolder.Controls.Add(this.txtStartingFolder);
            this.gbStartingFolder.Location = new System.Drawing.Point(20, 21);
            this.gbStartingFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbStartingFolder.Name = "gbStartingFolder";
            this.gbStartingFolder.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbStartingFolder.Size = new System.Drawing.Size(718, 94);
            this.gbStartingFolder.TabIndex = 16;
            this.gbStartingFolder.TabStop = false;
            this.gbStartingFolder.Text = "Starting Folder";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(596, 33);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(106, 44);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Pick";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.pnlMonthAccessedColor);
            this.GroupBox1.Controls.Add(this.pnlMonthWrittenColor);
            this.GroupBox1.Controls.Add(this.pnlMonthCreatedColor);
            this.GroupBox1.Controls.Add(this.pnlDefaultColor);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.txtMonthsSinceCreated);
            this.GroupBox1.Controls.Add(this.spnMonthsSinceCreated);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.txtMonthsSinceAccessed);
            this.GroupBox1.Controls.Add(this.txtMonthsSinceWritten);
            this.GroupBox1.Controls.Add(this.spnMonthsSinceAccessed);
            this.GroupBox1.Controls.Add(this.spnMonthsSinceWritten);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.chkColorCodeDates);
            this.GroupBox1.Location = new System.Drawing.Point(20, 592);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GroupBox1.Size = new System.Drawing.Size(724, 279);
            this.GroupBox1.TabIndex = 14;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Date Information";
            // 
            // txtMonthsSinceCreated
            // 
            this.txtMonthsSinceCreated.Location = new System.Drawing.Point(274, 94);
            this.txtMonthsSinceCreated.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMonthsSinceCreated.Name = "txtMonthsSinceCreated";
            this.txtMonthsSinceCreated.Size = new System.Drawing.Size(44, 31);
            this.txtMonthsSinceCreated.TabIndex = 16;
            this.txtMonthsSinceCreated.Text = "1";
            this.txtMonthsSinceCreated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // spnMonthsSinceCreated
            // 
            this.spnMonthsSinceCreated.Location = new System.Drawing.Point(338, 92);
            this.spnMonthsSinceCreated.Name = "spnMonthsSinceCreated";
            this.spnMonthsSinceCreated.Size = new System.Drawing.Size(16, 40);
            this.spnMonthsSinceCreated.TabIndex = 15;
            this.spnMonthsSinceCreated.ValueChanged += new System.EventHandler(this.spnMonthsSinceCreated_ValueChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(492, 100);
            this.Label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(145, 25);
            this.Label7.TabIndex = 14;
            this.Label7.Text = "Created Color";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(30, 163);
            this.Label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(217, 25);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "Months Since Written";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(18, 227);
            this.Label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(243, 25);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Months Since Accessed";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMonthsSinceAccessed
            // 
            this.txtMonthsSinceAccessed.Location = new System.Drawing.Point(274, 221);
            this.txtMonthsSinceAccessed.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMonthsSinceAccessed.Name = "txtMonthsSinceAccessed";
            this.txtMonthsSinceAccessed.Size = new System.Drawing.Size(44, 31);
            this.txtMonthsSinceAccessed.TabIndex = 10;
            this.txtMonthsSinceAccessed.Text = "1";
            this.txtMonthsSinceAccessed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMonthsSinceWritten
            // 
            this.txtMonthsSinceWritten.Location = new System.Drawing.Point(274, 158);
            this.txtMonthsSinceWritten.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMonthsSinceWritten.Name = "txtMonthsSinceWritten";
            this.txtMonthsSinceWritten.Size = new System.Drawing.Size(44, 31);
            this.txtMonthsSinceWritten.TabIndex = 9;
            this.txtMonthsSinceWritten.Text = "1";
            this.txtMonthsSinceWritten.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // spnMonthsSinceAccessed
            // 
            this.spnMonthsSinceAccessed.Location = new System.Drawing.Point(338, 221);
            this.spnMonthsSinceAccessed.Name = "spnMonthsSinceAccessed";
            this.spnMonthsSinceAccessed.Size = new System.Drawing.Size(16, 38);
            this.spnMonthsSinceAccessed.TabIndex = 8;
            this.spnMonthsSinceAccessed.ValueChanged += new System.EventHandler(this.spnMonthsSinceAccessed_ValueChanged);
            // 
            // spnMonthsSinceWritten
            // 
            this.spnMonthsSinceWritten.Location = new System.Drawing.Point(338, 158);
            this.spnMonthsSinceWritten.Name = "spnMonthsSinceWritten";
            this.spnMonthsSinceWritten.Size = new System.Drawing.Size(16, 38);
            this.spnMonthsSinceWritten.TabIndex = 7;
            this.spnMonthsSinceWritten.ValueChanged += new System.EventHandler(this.spnMonthsSinceWritten_ValueChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(472, 227);
            this.Label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(163, 25);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "Accessed Color";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(498, 158);
            this.Label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(137, 25);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Written Color";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(510, 37);
            this.Label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(125, 25);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Defalt Color";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkColorCodeDates
            // 
            this.chkColorCodeDates.AutoSize = true;
            this.chkColorCodeDates.Location = new System.Drawing.Point(16, 37);
            this.chkColorCodeDates.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkColorCodeDates.Name = "chkColorCodeDates";
            this.chkColorCodeDates.Size = new System.Drawing.Size(214, 29);
            this.chkColorCodeDates.TabIndex = 0;
            this.chkColorCodeDates.Text = "Color Code Dates";
            this.chkColorCodeDates.UseVisualStyleBackColor = true;
            this.chkColorCodeDates.CheckedChanged += new System.EventHandler(this.chkColorCodeDates_CheckedChanged);
            // 
            // cmdCreateFolderMap
            // 
            this.cmdCreateFolderMap.Location = new System.Drawing.Point(516, 1177);
            this.cmdCreateFolderMap.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdCreateFolderMap.Name = "cmdCreateFolderMap";
            this.cmdCreateFolderMap.Size = new System.Drawing.Size(224, 44);
            this.cmdCreateFolderMap.TabIndex = 13;
            this.cmdCreateFolderMap.Text = "Create Folder Map";
            this.cmdCreateFolderMap.Click += new System.EventHandler(this.cmdCreateFolderMap_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(376, 1177);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(128, 44);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // chkTableStyleOutput
            // 
            this.chkTableStyleOutput.Location = new System.Drawing.Point(15, 402);
            this.chkTableStyleOutput.Margin = new System.Windows.Forms.Padding(6);
            this.chkTableStyleOutput.Name = "chkTableStyleOutput";
            this.chkTableStyleOutput.Size = new System.Drawing.Size(242, 46);
            this.chkTableStyleOutput.TabIndex = 31;
            this.chkTableStyleOutput.Text = "Table Style Output";
            this.ToolTips.SetToolTip(this.chkTableStyleOutput, "Format output for table");
            // 
            // frmExcel_FolderMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 1273);
            this.Controls.Add(this.gbContents);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.gbStartingFolder);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cmdCreateFolderMap);
            this.Controls.Add(this.cmdCancel);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmExcel_FolderMaps";
            this.Text = "frmExcel_FolderMaps";
            this.Load += new System.EventHandler(this.frmExcel_FolderMaps_Load);
            this.gbContents.ResumeLayout(false);
            this.gbContents.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.gbStartingFolder.ResumeLayout(false);
            this.gbStartingFolder.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbContents;
        internal System.Windows.Forms.Panel pnlPatternMatchFileColor;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Panel pnlFolderHighlightColor;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtFolderMatchPattern;
        internal System.Windows.Forms.ToolTip ToolTips;
        internal System.Windows.Forms.CheckBox chkPatternMatchFolderHighlight;
        internal System.Windows.Forms.CheckBox chkSkipFoldersWithNoFiles;
        internal System.Windows.Forms.CheckBox chkPatternMatchFileOutput;
        internal System.Windows.Forms.TextBox txtFileMatchPattern;
        internal System.Windows.Forms.Panel pnlNoAccessColor;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.CheckBox chkShowFolders;
        internal System.Windows.Forms.Panel pnlPathTooLongColor;
        internal System.Windows.Forms.VScrollBar spnLimitLevel;
        internal System.Windows.Forms.VScrollBar spnGroupLevel;
        internal System.Windows.Forms.CheckBox chkShowFiles;
        internal System.Windows.Forms.CheckBox chkGroupResults;
        internal System.Windows.Forms.CheckBox chkLimitLevels;
        internal System.Windows.Forms.TextBox txtLimitLevel;
        internal System.Windows.Forms.TextBox txtGroupLevel;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Panel pnlMonthAccessedColor;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.CheckBox chkFileNameLength;
        internal System.Windows.Forms.Panel pnlMonthWrittenColor;
        internal System.Windows.Forms.Panel pnlMonthCreatedColor;
        internal System.Windows.Forms.Panel pnlDefaultColor;
        internal System.Windows.Forms.Panel pnlIllegalCharactersColor;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Panel pnlIllegalFileNameLengthColor;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.TextBox txtMaxFileNameLength;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox txtIllegalFolderCharacters;
        internal System.Windows.Forms.CheckBox chkIllegalCharacters;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.TextBox txtIllegalFileCharacters;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        internal System.Windows.Forms.TextBox txtStartingFolder;
        internal System.Windows.Forms.GroupBox gbStartingFolder;
        internal System.Windows.Forms.Button btnSelectFolder;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtMonthsSinceCreated;
        internal System.Windows.Forms.VScrollBar spnMonthsSinceCreated;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtMonthsSinceAccessed;
        internal System.Windows.Forms.TextBox txtMonthsSinceWritten;
        internal System.Windows.Forms.VScrollBar spnMonthsSinceAccessed;
        internal System.Windows.Forms.VScrollBar spnMonthsSinceWritten;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkColorCodeDates;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal System.Windows.Forms.Button cmdCreateFolderMap;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.CheckBox chkTableStyleOutput;
    }
}