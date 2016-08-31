using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;

namespace gov.dol.vets.utilities
{
    public partial class External : Form
    {
        // delegates used to pass to main thread
        private delegate void statusMessageDelegate(string message);
        private delegate void pdfReportsCompleteDelegate();
        private delegate void evaluateFlatFileCompletedDelegate();
        private delegate void fixFlatFileCompletedDelegate();
        private delegate void verificationFileCompletedDelegate(List<ReportInformation> ReportData, string message);

        public External()
        {
            InitializeComponent();
        }

        private void tsbGeneratePDFs_Click(object sender, EventArgs e)
        {
            try
            {
                // use stringBuilder for error messages
                StringBuilder message = new StringBuilder();

                // verify and validate parameters
                Regex re = new Regex(@"^[\d]{4}$");
                if (!re.IsMatch(filingCycle.Text))
                    message.AppendLine("Invalid Filing Cycle Value.");

                // make sure we have a company number
                if (string.IsNullOrWhiteSpace(companyNumber.Text))
                    message.AppendLine("No Company Number was entered.");

                // make sure we have a username
                if (string.IsNullOrWhiteSpace(username.Text))
                    message.AppendLine("No Username was entered.");

                // make sure we have a password
                if (string.IsNullOrWhiteSpace(password.Text))
                    message.AppendLine("No Password was entered.");

                // did we have any errors
                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    MessageBox.Show(message.ToString(), "Invalid data");
                    return;
                }

                // create the PDF file for all reports
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "pdf";
                sfd.Filter = "PDF|*.pdf";
                sfd.Title = "Name of PDF";
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Generate PDF's has been cancelled.", "PDF Process Cancelled");
                    return;
                }

                // disable the generate pdf button so they don't double tap
                tsbGeneratePDFs.Enabled = false;

                // get value of internalAccess in appConfig
                bool internalAccess = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["internalAccess"], out internalAccess)) internalAccess = false;

                // get URL's from App.Config
                string webAddress = ConfigurationManager.AppSettings["webAddress"];
                string pdfAddress = ConfigurationManager.AppSettings["pdfWebAddress"];
                string searchAddress = ConfigurationManager.AppSettings["searchAddress"];
                if (string.IsNullOrWhiteSpace(webAddress) || string.IsNullOrWhiteSpace(pdfAddress))
                {
                    MessageBox.Show("Generating PDF's has been cancelled due to invalid configuration", "Error in configuration");
                    return;
                }
                // correct PDF address for formatting
                if (pdfAddress.Substring(pdfAddress.Length - 1, 1) != "/")
                    pdfAddress += "/{0}";
                else
                    pdfAddress += "{0}";

                // create a pdfStateObject
                pdfStateObject reportInformation = new pdfStateObject(string.Empty, username.Text, password.Text, sfd.FileName, webAddress, pdfAddress,
                    searchAddress, companyName.Text, filingCycle.Text, ReportType.Text, companyNumber.Text, internalAccess);

                // create handler for process
                bool logEnabled = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["logEnabled"], out logEnabled)) logEnabled = false;
                vets4212 handler = new vets4212(logEnabled);

                // add event handlers
                handler.Message += new vets4212.MessageEventHandler(Vets4212_Message);
                handler.PDFReportsCompleted += new vets4212.PDFReportsCompletedEventHandler(Vets4212_PDFReportsCompleted);

                // run process on a thread
                ThreadPool.QueueUserWorkItem(handler.getReports_QueueUserWorkItem, reportInformation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void pdfReportsCompleted()
        {
            try
            {
                // reenable PDF generate button
                tsbGeneratePDFs.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Vets4212_PDFReportsCompleted(object sender, PDFEventArgs e)
        {
            try
            {
                // we completed the process show message on completion
                MessageBox.Show(e.Message, "PDF Generation Completed");

                // reenable the button
                this.Invoke(new pdfReportsCompleteDelegate(pdfReportsCompleted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void updateStatusBarMessage(string Message)
        {
            try
            {
                // update status bar message
                StatusBar.Text = Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Vets4212_Message(object sender, MessageEventArgs e)
        {
            try
            {
                // show message on status bar
                this.Invoke(new statusMessageDelegate(updateStatusBarMessage), new object[] { e.Message });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void tsbEvaluateFlatFile_Click(object sender, EventArgs e)
        {
            try
            {
                // use stringBuilder for error messages
                StringBuilder message = new StringBuilder();

                // make sure we have a username
                if (string.IsNullOrWhiteSpace(username.Text))
                    message.AppendLine("No Username was entered.");

                // make sure we have a password
                if (string.IsNullOrWhiteSpace(password.Text))
                    message.AppendLine("No Password was entered.");

                // make sure we have a data file
                if (string.IsNullOrWhiteSpace(dataFilePath.Text))
                    message.AppendLine("No data file path was entered.");

                // did we have any errors
                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    MessageBox.Show(message.ToString(), "Invalid data");
                    return;
                }

                // create vets4212 handler
                bool logEnabled = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["logEnabled"], out logEnabled)) logEnabled = false;
                vets4212 handler = new vets4212(logEnabled);

                // get value of internalAccess in appConfig
                bool internalAccess = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["internalAccess"], out internalAccess)) internalAccess = false;

                // get URL's from App.Config
                string webAddress = ConfigurationManager.AppSettings["webAddress"];
                string companyInformationAddress = ConfigurationManager.AppSettings["companyInformationAddress"];
                if (string.IsNullOrWhiteSpace(webAddress) || string.IsNullOrWhiteSpace(companyInformationAddress))
                {
                    MessageBox.Show("Flat file evaluation cancelled due to invalid configuration.");
                    return;
                }
                if (companyInformationAddress.Substring(companyInformationAddress.Length - 1, 1) != "/")
                    companyInformationAddress += "/{0}";
                else
                    companyInformationAddress += "{0}";

                // add Message event handler
                handler.Message += new vets4212.MessageEventHandler(Vets4212_Message);
                handler.EvaluateFlatFileCompleted += new vets4212.EvaluateFlatFileCompletedEventHandler(Vets4212_EvaluateFlatFileCompleted);

                // disable button
                tsbEvaluateFlatFile.Enabled = false;

                // create state object for threading handoff
                vets4212StateObject data = new vets4212StateObject(dataFilePath.Text, mscDataFilePath.Text, username.Text, password.Text, webAddress, companyInformationAddress, internalAccess);
                ThreadPool.QueueUserWorkItem(handler.evalFlatFile_QueueUserWorkItem, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void evaluateFlatFileCompleted()
        {
            try
            {
                // reenable button
                tsbEvaluateFlatFile.Enabled = true;
                StatusBar.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Vets4212_EvaluateFlatFileCompleted(object sender, EvaluateFlatFileEventArgs e)
        {
            try
            {
                // show message and reenable button
                MessageBox.Show(e.Message, "Evaluation Completed");
                this.Invoke(new evaluateFlatFileCompletedDelegate(evaluateFlatFileCompleted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void tsbFixFlatFile_Click(object sender, EventArgs e)
        {
            try
            {
                // use stringBuilder for error messages
                StringBuilder message = new StringBuilder();

                // make sure we have a username
                if (string.IsNullOrWhiteSpace(username.Text))
                    message.AppendLine("No Username was entered.");

                // make sure we have a password
                if (string.IsNullOrWhiteSpace(password.Text))
                    message.AppendLine("No Password was entered.");

                // make sure we have a data file
                if (string.IsNullOrWhiteSpace(dataFilePath.Text))
                    message.AppendLine("No data file path was entered.");

                // did we have any errors
                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    MessageBox.Show(message.ToString(), "Invalid data");
                    return;
                }

                // create vets4212 handler
                bool logEnabled = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["logEnabled"], out logEnabled)) logEnabled = false;
                vets4212 handler = new vets4212(logEnabled);

                // get value of internalAccess in appConfig
                bool internalAccess = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["internalAccess"], out internalAccess)) internalAccess = false;

                // get URL's from App.Config
                string webAddress = ConfigurationManager.AppSettings["webAddress"];
                string companyInformationAddress = ConfigurationManager.AppSettings["companyInformationAddress"];
                if (string.IsNullOrWhiteSpace(webAddress) || string.IsNullOrWhiteSpace(companyInformationAddress))
                {
                    MessageBox.Show("Flat file evaluation cancelled due to invalid configuration.");
                    return;
                }
                if (companyInformationAddress.Substring(companyInformationAddress.Length - 1, 1) != "/")
                    companyInformationAddress += "/{0}";
                else
                    companyInformationAddress += "{0}";

                // add Message event handler
                handler.Message += new vets4212.MessageEventHandler(Vets4212_Message);
                handler.FixFlatFileCompleted +=new vets4212.FixFlatFileCompletedEventHandler(Vets4212_FixFlatFileCompleted);

                // disable fix flat file button
                tsbFixFlatFile.Enabled = false;

                // create state object for threading handoff
                vets4212StateObject data = new vets4212StateObject(dataFilePath.Text, mscDataFilePath.Text, username.Text, password.Text, webAddress, companyInformationAddress, internalAccess);
                ThreadPool.QueueUserWorkItem(handler.fixFlatFile_QueueUserWorkItem, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void FixFlatFileCompleted()
        {
            try
            {
                // enable button
                tsbFixFlatFile.Enabled = true;
                StatusBar.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Vets4212_FixFlatFileCompleted(object sender, FixFlatFileEventArgs e)
        {
            try
            {
                // show message and reenable button
                MessageBox.Show(e.Message, "Fix Flat File Completed");
                this.Invoke(new fixFlatFileCompletedDelegate(FixFlatFileCompleted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // browse for files needed during evaluation and fixing
        private void dataFileButton_Click(object sender, EventArgs e)
        {
            // browse and locate flat file to use
            try
            {
                // use open file dialog to open existing CSV file
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "csv";
                ofd.Filter = "CSV|*.csv|TXT|*.txt|ALL|*.*";
                ofd.Title = "Locate 4212 Data File...";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    dataFilePath.Text = ofd.FileName;
                ofd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void mscDataFileButton_Click(object sender, EventArgs e)
        {
            // browse and locate MSC flat file to use
            try
            {
                // use open file dialog to open existing hiring location file
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "csv";
                ofd.Filter = "CSV|*.csv|TXT|*.txt|ALL|*.*";
                ofd.Title = "Locate MSC Hiring Location File, click cancel if not needed.";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    mscDataFilePath.Text = ofd.FileName;
                ofd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void tsbReportData_Click(object sender, EventArgs e)
        {
            try
            {
                // we need to make sure we ahve a filing cycle and report type
                // use stringBuilder for error messages
                StringBuilder message = new StringBuilder();

                // make sure we have a username
                if (string.IsNullOrWhiteSpace(ReportType.Text))
                    message.AppendLine("No Report Type was selected.");

                // make sure we have a password
                if (string.IsNullOrWhiteSpace(filingCycle.Text))
                    message.AppendLine("No Filing Cycle was entered.");

                // did we have any errors
                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    MessageBox.Show(message.ToString(), "Invalid required data");
                    return;
                }

                // get the url for dataDotGov Querries
                string dataDotGovUrl = ConfigurationManager.AppSettings["dolApiAddress"];
                if (string.IsNullOrWhiteSpace(dataDotGovUrl))
                {
                    MessageBox.Show("Configuration Error, missing dataDotGov URL, process cancelled.", "Invalid Configuration");
                    return;
                }

                // we need to get the data file location to save
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "csv";
                sfd.Filter = "CSV|*.csv";
                sfd.Title = "Name and location to save CSV File...";
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("CVS process canceled...", "Process Cancelled");
                    return;
                }

                // create state object
                dataDotGovStateObject data = new dataDotGovStateObject(dataDotGovUrl, companyName.Text, ReportType.Text, filingCycle.Text, naics.Text, sfd.FileName);

                // create handler for vets4212
                bool logEnabled = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["logEnabled"], out logEnabled)) logEnabled = false;
                vets4212 handler = new vets4212(logEnabled);

                // add event handlers
                handler.Message += new vets4212.MessageEventHandler(Vets4212_Message);
                handler.DataDotGovCompleted += new vets4212.DataDotGovCompletedEventHandler(Vets4212_DataDotGovCompleted);

                // queue up for thread pool
                ThreadPool.QueueUserWorkItem(handler.getDataFromDataDotGov_QueueUserWorkItem, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void ReportDataCompleted()
        {
            try
            {
                // reenable button and clear messages
                tsbReportData.Enabled = true;
                StatusBar.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Vets4212_DataDotGovCompleted(object sender, DataDotGovEventArgs e)
        {
            try
            {
                // show message and reenable button
                MessageBox.Show(e.Message, "Completed DataDotGov Process");
                this.Invoke(new pdfReportsCompleteDelegate(ReportDataCompleted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void tsbVerificationFile_Click(object sender, EventArgs e)
        {
            try
            {
                // use stringBuilder for error messages
                StringBuilder message = new StringBuilder();

                // verify and validate parameters
                Regex re = new Regex(@"^[\d]{4}$");
                if (!re.IsMatch(filingCycle.Text))
                    message.AppendLine("Invalid Filing Cycle Value.");

                // make sure we have a username
                if (string.IsNullOrWhiteSpace(username.Text))
                    message.AppendLine("No Username was entered.");

                // make sure we have a password
                if (string.IsNullOrWhiteSpace(password.Text))
                    message.AppendLine("No Password was entered.");

                // did we have any errors
                if (!string.IsNullOrWhiteSpace(message.ToString()))
                {
                    MessageBox.Show(message.ToString(), "Invalid data");
                    return;
                }

                // disable the generate pdf button so they don't double tap
                tsbGeneratePDFs.Enabled = false;

                // get value of internalAccess in appConfig
                bool internalAccess = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["internalAccess"], out internalAccess)) internalAccess = false;

                // get URL's from App.Config
                string loginAddress = ConfigurationManager.AppSettings["webAddress"];
                string reportInformationAddress = ConfigurationManager.AppSettings["reportInformationAddress"];
                string reportSearchAddress = ConfigurationManager.AppSettings["searchAddress"];
                if (string.IsNullOrWhiteSpace(loginAddress) || string.IsNullOrWhiteSpace(reportInformationAddress) || 
                    string.IsNullOrWhiteSpace(reportSearchAddress))
                {
                    MessageBox.Show("Validation File has been cancelled due to invalid configuration", "Error in configuration");
                    return;
                }
                // correct report information address for formatting
                if (reportInformationAddress.Substring(reportInformationAddress.Length - 1, 1) != "/")
                    reportInformationAddress += "/{0}";
                else
                    reportInformationAddress += "{0}";

                // create a pdfStateObject
                pdfStateObject reportInformation = new pdfStateObject(username.Text, password.Text, loginAddress, reportInformationAddress, 
                    reportSearchAddress, filingCycle.Text, internalAccess);

                // create handler for process
                bool logEnabled = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["logEnabled"], out logEnabled)) logEnabled = false;
                vets4212 handler = new vets4212(logEnabled);

                // add event handlers
                handler.Message += new vets4212.MessageEventHandler(Vets4212_Message);
                handler.VerificationFileCompleted += new vets4212.VerificationFileCompletedEventHandler(Handler_VerificationFileCompleted);

                // run process on a thread
                ThreadPool.QueueUserWorkItem(handler.getValidationFileInformation_QueueUserWorkItem, reportInformation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Handler_VerificationFileCompleted(object sender, VerificationFileEventArgs e)
        {
            try
            {
                // we completed the process show message on completion
                MessageBox.Show(e.Message, "Verification File Completed");

                // reenable the button
                this.Invoke(new verificationFileCompletedDelegate(VerificationFileCompleted), new object[] { e.ReportData, e.Message });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void VerificationFileCompleted(List<ReportInformation> ReportData, string message)
        {
            try
            {
                // update message
                StatusBar.Text = message;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
