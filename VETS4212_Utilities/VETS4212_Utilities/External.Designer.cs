namespace gov.dol.vets.utilities
{
    partial class External
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(External));
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbGeneratePDFs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEvaluateFlatFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFixFlatFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReportData = new System.Windows.Forms.ToolStripButton();
            this.sbMain = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ReportType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.companyNumber = new System.Windows.Forms.TextBox();
            this.filingCycle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filingCycleLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mscDataFilePath = new System.Windows.Forms.TextBox();
            this.dataFileButton = new System.Windows.Forms.Button();
            this.mscDataFileButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.companyName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.naics = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVerificationFile = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip.SuspendLayout();
            this.sbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip
            // 
            this.ToolStrip.AccessibleDescription = "Contains menu items to generate PDF, evaluate and fix batch files, and access Dat" +
    "aDotGov";
            this.ToolStrip.AccessibleName = "Main Menu";
            this.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGeneratePDFs,
            this.toolStripSeparator1,
            this.tsbEvaluateFlatFile,
            this.toolStripSeparator2,
            this.tsbFixFlatFile,
            this.toolStripSeparator3,
            this.tsbReportData,
            this.toolStripSeparator4,
            this.tsbVerificationFile});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(504, 25);
            this.ToolStrip.TabIndex = 0;
            this.ToolStrip.Text = "toolStrip1";
            // 
            // tsbGeneratePDFs
            // 
            this.tsbGeneratePDFs.AccessibleDescription = "Click this button to generate all the PDF documents for a given company number an" +
    "d filing cycle";
            this.tsbGeneratePDFs.AccessibleName = "Generate PDF reports";
            this.tsbGeneratePDFs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGeneratePDFs.Image = ((System.Drawing.Image)(resources.GetObject("tsbGeneratePDFs.Image")));
            this.tsbGeneratePDFs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGeneratePDFs.Name = "tsbGeneratePDFs";
            this.tsbGeneratePDFs.Size = new System.Drawing.Size(90, 22);
            this.tsbGeneratePDFs.Text = "&Generate PDF\'s";
            this.tsbGeneratePDFs.ToolTipText = "Generate PDF Reports based on Report Type, Filing Cycle, and Company Number";
            this.tsbGeneratePDFs.Click += new System.EventHandler(this.tsbGeneratePDFs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEvaluateFlatFile
            // 
            this.tsbEvaluateFlatFile.AccessibleDescription = "Click this button to evaluate a VETS-4212 batch file. Any errors will be saved to" +
    " a file.";
            this.tsbEvaluateFlatFile.AccessibleName = "Evaluate VETS-4212 batch file";
            this.tsbEvaluateFlatFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEvaluateFlatFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbEvaluateFlatFile.Image")));
            this.tsbEvaluateFlatFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEvaluateFlatFile.Name = "tsbEvaluateFlatFile";
            this.tsbEvaluateFlatFile.Size = new System.Drawing.Size(98, 22);
            this.tsbEvaluateFlatFile.Text = "&Evaluate Flat File";
            this.tsbEvaluateFlatFile.ToolTipText = "Evaluate a Flat File prior to submitting";
            this.tsbEvaluateFlatFile.Click += new System.EventHandler(this.tsbEvaluateFlatFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFixFlatFile
            // 
            this.tsbFixFlatFile.AccessibleDescription = "Click this button to fix major errors found during evaluation of VETS-4212 batch " +
    "file";
            this.tsbFixFlatFile.AccessibleName = "Fix VETS-4212 batch file";
            this.tsbFixFlatFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFixFlatFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbFixFlatFile.Image")));
            this.tsbFixFlatFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFixFlatFile.Name = "tsbFixFlatFile";
            this.tsbFixFlatFile.Size = new System.Drawing.Size(68, 22);
            this.tsbFixFlatFile.Text = "&Fix Flat File";
            this.tsbFixFlatFile.ToolTipText = "Fix Flat File issues prior to submitting";
            this.tsbFixFlatFile.Click += new System.EventHandler(this.tsbFixFlatFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbReportData
            // 
            this.tsbReportData.AccessibleDescription = "Click this button to retrieve VETS-4212/100/100A data from DataDotGov.";
            this.tsbReportData.AccessibleName = "VETS-4212/100/100A report data from  DataDotGov";
            this.tsbReportData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbReportData.Image = ((System.Drawing.Image)(resources.GetObject("tsbReportData.Image")));
            this.tsbReportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReportData.Name = "tsbReportData";
            this.tsbReportData.Size = new System.Drawing.Size(75, 22);
            this.tsbReportData.Text = "&DataDotGov";
            this.tsbReportData.ToolTipText = "Get VETS-4212, VETS-100, or VETS-100A report data from DataDotGov";
            this.tsbReportData.Click += new System.EventHandler(this.tsbReportData_Click);
            // 
            // sbMain
            // 
            this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
            this.sbMain.Location = new System.Drawing.Point(0, 290);
            this.sbMain.Name = "sbMain";
            this.sbMain.Size = new System.Drawing.Size(504, 22);
            this.sbMain.TabIndex = 1;
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // password
            // 
            this.password.AccessibleDescription = "Enter your password you use to access the VETS-4212/100/100A website";
            this.password.AccessibleName = "Password";
            this.password.Location = new System.Drawing.Point(117, 202);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(188, 20);
            this.password.TabIndex = 7;
            // 
            // username
            // 
            this.username.AccessibleDescription = "Enter your username you use to access the VETS-4212/100/100A website.";
            this.username.AccessibleName = "Username";
            this.username.Location = new System.Drawing.Point(117, 175);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(188, 20);
            this.username.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Username";
            // 
            // ReportType
            // 
            this.ReportType.AccessibleDescription = "Select the report type for the lookup";
            this.ReportType.AccessibleName = "Report Type";
            this.ReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportType.FormattingEnabled = true;
            this.ReportType.Items.AddRange(new object[] {
            "VETS-4212",
            "VETS-100",
            "VETS-100A"});
            this.ReportType.Location = new System.Drawing.Point(117, 39);
            this.ReportType.Name = "ReportType";
            this.ReportType.Size = new System.Drawing.Size(99, 21);
            this.ReportType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Report Type";
            // 
            // companyNumber
            // 
            this.companyNumber.AccessibleDescription = "Enter the Company Number for PDF\'s";
            this.companyNumber.AccessibleName = "Company Number";
            this.companyNumber.Location = new System.Drawing.Point(117, 94);
            this.companyNumber.Name = "companyNumber";
            this.companyNumber.Size = new System.Drawing.Size(100, 20);
            this.companyNumber.TabIndex = 3;
            // 
            // filingCycle
            // 
            this.filingCycle.AccessibleDescription = "Enter the VETS-4212/100/100A filing cycle to generate reports.";
            this.filingCycle.AccessibleName = "Filing cycle";
            this.filingCycle.Location = new System.Drawing.Point(117, 67);
            this.filingCycle.Name = "filingCycle";
            this.filingCycle.Size = new System.Drawing.Size(100, 20);
            this.filingCycle.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Company Number";
            // 
            // filingCycleLabel
            // 
            this.filingCycleLabel.AutoSize = true;
            this.filingCycleLabel.Location = new System.Drawing.Point(49, 70);
            this.filingCycleLabel.Name = "filingCycleLabel";
            this.filingCycleLabel.Size = new System.Drawing.Size(62, 13);
            this.filingCycleLabel.TabIndex = 44;
            this.filingCycleLabel.Text = "Filling Cycle";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Data File";
            // 
            // dataFilePath
            // 
            this.dataFilePath.AccessibleDescription = "Enter the path to the flat file containing the report data for the VETS-4212 repo" +
    "rts";
            this.dataFilePath.AccessibleName = "Flat file report data path";
            this.dataFilePath.Location = new System.Drawing.Point(117, 228);
            this.dataFilePath.Name = "dataFilePath";
            this.dataFilePath.Size = new System.Drawing.Size(319, 20);
            this.dataFilePath.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "MSC Data File";
            // 
            // mscDataFilePath
            // 
            this.mscDataFilePath.AccessibleDescription = "Enter the path to the MSC Hiring Locations flat file.";
            this.mscDataFilePath.AccessibleName = "MSC Hiring Locations Flat File Path";
            this.mscDataFilePath.Location = new System.Drawing.Point(117, 255);
            this.mscDataFilePath.Name = "mscDataFilePath";
            this.mscDataFilePath.Size = new System.Drawing.Size(319, 20);
            this.mscDataFilePath.TabIndex = 10;
            // 
            // dataFileButton
            // 
            this.dataFileButton.AccessibleDescription = "Click to search for flat file containing reporting data";
            this.dataFileButton.AccessibleName = "Report data flat file browser";
            this.dataFileButton.Location = new System.Drawing.Point(442, 226);
            this.dataFileButton.Name = "dataFileButton";
            this.dataFileButton.Size = new System.Drawing.Size(28, 23);
            this.dataFileButton.TabIndex = 9;
            this.dataFileButton.Text = "...";
            this.dataFileButton.UseVisualStyleBackColor = true;
            this.dataFileButton.Click += new System.EventHandler(this.dataFileButton_Click);
            // 
            // mscDataFileButton
            // 
            this.mscDataFileButton.AccessibleDescription = "Click to browse to the location for the MSC Hiring Locations flat file.";
            this.mscDataFileButton.AccessibleName = "MSC Hiring Locations flat file data browser";
            this.mscDataFileButton.Location = new System.Drawing.Point(442, 253);
            this.mscDataFileButton.Name = "mscDataFileButton";
            this.mscDataFileButton.Size = new System.Drawing.Size(28, 23);
            this.mscDataFileButton.TabIndex = 11;
            this.mscDataFileButton.Text = "...";
            this.mscDataFileButton.UseVisualStyleBackColor = true;
            this.mscDataFileButton.Click += new System.EventHandler(this.mscDataFileButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Company Name";
            // 
            // companyName
            // 
            this.companyName.AccessibleDescription = "Enter the Company Name for searching.";
            this.companyName.AccessibleName = "Company Name";
            this.companyName.Location = new System.Drawing.Point(117, 122);
            this.companyName.Name = "companyName";
            this.companyName.Size = new System.Drawing.Size(263, 20);
            this.companyName.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(222, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 13);
            this.label8.TabIndex = 56;
            this.label8.Text = "Used for Report Data ONLY";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(223, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Used for Generate PDF\'s ONLY";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(233, 13);
            this.label10.TabIndex = 58;
            this.label10.Text = "Used for both Generate PDF\'s and DataDotGov";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(311, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "* Required except for DataDotGov";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(311, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "* Required except for Report Data";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(386, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 61;
            this.label13.Text = "DataDotGov ONLY";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(72, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "NAICS";
            // 
            // naics
            // 
            this.naics.AccessibleDescription = "Enter the NAICS for searching.";
            this.naics.AccessibleName = "NAICS";
            this.naics.Location = new System.Drawing.Point(117, 148);
            this.naics.Name = "naics";
            this.naics.Size = new System.Drawing.Size(100, 20);
            this.naics.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(222, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 63;
            this.label15.Text = "DataDotGov ONLY";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVerificationFile
            // 
            this.tsbVerificationFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbVerificationFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbVerificationFile.Image")));
            this.tsbVerificationFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVerificationFile.Name = "tsbVerificationFile";
            this.tsbVerificationFile.Size = new System.Drawing.Size(92, 22);
            this.tsbVerificationFile.Text = "Verification File";
            this.tsbVerificationFile.Click += new System.EventHandler(this.tsbVerificationFile_Click);
            // 
            // External
            // 
            this.AccessibleDescription = "Application used to work VETS-4212/100/100A reports and data";
            this.AccessibleName = "Main VETS-4212 Utility Screen";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 312);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.naics);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.companyName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mscDataFileButton);
            this.Controls.Add(this.dataFileButton);
            this.Controls.Add(this.mscDataFilePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataFilePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReportType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.companyNumber);
            this.Controls.Add(this.filingCycle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filingCycleLabel);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.ToolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "External";
            this.Text = "VETS-4212 Utilities";
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.sbMain.ResumeLayout(false);
            this.sbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.StatusStrip sbMain;
        private System.Windows.Forms.ToolStripButton tsbGeneratePDFs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEvaluateFlatFile;
        private System.Windows.Forms.ToolStripButton tsbFixFlatFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ReportType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox companyNumber;
        private System.Windows.Forms.TextBox filingCycle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label filingCycleLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbReportData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dataFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mscDataFilePath;
        private System.Windows.Forms.Button dataFileButton;
        private System.Windows.Forms.Button mscDataFileButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox companyName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripStatusLabel StatusBar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox naics;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbVerificationFile;
    }
}

