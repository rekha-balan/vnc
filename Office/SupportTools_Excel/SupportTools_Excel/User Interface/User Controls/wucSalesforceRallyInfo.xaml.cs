using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.ServiceModel;

using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Office.Interop.Excel;

using Rally.RestApi;
using Rally.RestApi.Json;
using Rally.RestApi.Response;

using SF = SupportTools_Excel.SalesforceSR;

using VNCRally = VNC.Rally;
using XlHlp = VNC.AddinHelper.Excel;
using System.IO;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSalesforceRallyInfo.xaml
    /// </summary>
    public partial class wucSalesforceRallyInfo : wucDX_Base
    {

        #region Fields and Properties

        // Rally Stuff

        string _Rally_username = "crhodes@harborsys.com";
        string _Rally_password = "H0jnacki!";
        string _Rally_serverUrl = "https://rally1.rallydev.com";
        private RallyRestApi _Rally_RestApi = null;

        enum ExcelColumnOffset : int
        {
            Development = 13,
            ETL_Dev = 14,
            ETL_Support = 15,
            HHS_Dev = 16,
            HHS_Support = 17,
            Core1_Dev = 18,
            Core2_Dev = 19,
            Sedgwick_Dev = 20,
            Production_Support_New = 21,
            Quality_Assurance = 22,
            Reporting = 23,
            Product_Support =24
        }


        // Salesforce Stuff

        SF.SoapClient _Salesforce_loginClient = null;
        string _Salesforce_SessionId = null;
        string _Salesforce_ServerUrl = null;
        EndpointAddress _Salesforce_apiAddr = null;

        Data.ApplicationDS.SalesforceCaseRow _SalesforceRow;


        public Data.ApplicationDS.SalesforceCaseRow SalesforceRow
        {
            get
            {
                return _SalesforceRow;
            }
            set
            {
                _SalesforceRow = value;
            }
        }

        #endregion

        #region Constructors and Load

        public wucSalesforceRallyInfo()
        {
            try
            {
                InitializeComponent();
                LoadControlContents();
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void LoadControlContents()
        {
            try
            {
                TriageCommands.Rally_RestApi = AccessCredentials.Rally_RestApi;
                TriageCommands.Salesforce_apiAddr = AccessCredentials.Salesforce_apiAddr;
                //Binding bind = new Binding();
                //bind.Source = SalesforceCases.dataGridTriage_Salesforce;
                //bind.Path = new PropertyPath(DataGrid.SelectedItemProperty);
                //lg_body_dlm_lc1.SetBinding(DevExpress.Xpf.LayoutControl.LayoutControl.DataContextProperty, bind);


                //wucTFSProvider_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
                //tfsQuery_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void SetInitialControlState()
        {
            //EnableAndShow(btnLogon_Salesforce);
            //EnableAndShow(btnLogon_Rally);

            //DisableAndHide(btnLogoff_Salesforce);
            //DisableAndHide(btnLogoff_Rally);

            //// TODO(crhodes):
            //// May want to turn off entire tab

            //DisableAndHide(btnRetrieveInfo_Salesforce);
            //DisableAndHide(btnRetrieveInfo_Rally);
            ////DisableAndHide(lg_Triage);

            ////DisableAndHide(lg_SalesforceTriageInfo);
            ////DisableAndHide(lg_RallyTriageInfo);
            //DisableAndHide(lg_Salesforce);
            //DisableAndHide(lg_Rally);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
//#if TRACE
//            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
//#endif
//            EyeOnLife.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    ((CollectionViewSource)this.Resources["salesforceViewSource"]).Source = Common.ApplicationDS.SalesforceCase;

                    //dataGridSalesforce.ItemsSource = Common.ApplicationDS.SalesforceCase;

                    ((CollectionViewSource)this.Resources["rallyViewSource"]).Source = Common.ApplicationDS.RallyUserStory;

                    //dataGridRally.ItemsSource = Common.ApplicationDS.RallyUserStory;

                    SetInitialControlState();
                }

                //base.dataGrid = dataGridSalesforce;

            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
            //#if TRACE
            //            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
            //#endif
        }

        #endregion

        #region Event Handlers

        #region Logon/Logoff handling

        private void btnLogonHack_Click(object sender, RoutedEventArgs e)
        {
            TriageCommands.Rally_RestApi = AccessCredentials.Rally_RestApi;
            TriageCommands.Salesforce_apiAddr = AccessCredentials.Salesforce_apiAddr;
            TriageCommands.Salesforce_SessionId = AccessCredentials.Salesforce_SessionId;
        }

        #endregion




        private void btnRebind_Click(object sender, RoutedEventArgs e)
        {
            ((CollectionViewSource)this.Resources["salesforceViewSource"]).Source = Common.ApplicationDS.SalesforceCase;

            //dataGridSalesforce.ItemsSource = Common.ApplicationDS.SalesforceCase;
            //SalesforceCases.dataGridTriage_Salesforce.ItemsSource = Common.ApplicationDS.SalesforceCase;
        }

        private void btnRefreshInformation_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes):
            // Call something that does what the code on wucTriageCommands does when it updates
            // Selected cells.  This will be just for the current case.
        }

        #endregion

        #region Main Function Routines

        private void ClearCurrentContents_RallyInfo(Range currentCell)
        {
            // TODO(crhodes):
            // Make this simpler using a range or at least get rid of magic numbers

            currentCell.Offset[0, ExcelColumnOffset.Development].Clear();    
            currentCell.Offset[0, ExcelColumnOffset.ETL_Dev].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.ETL_Support].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.HHS_Dev].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.HHS_Support].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Core1_Dev].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Core2_Dev].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Sedgwick_Dev].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Production_Support_New].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Quality_Assurance].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Reporting].Clear(); 
            currentCell.Offset[0, ExcelColumnOffset.Product_Support].Clear();
        }

        private object AddStateInfoToUserStory(string userStoryID, string scheduleState, bool blocked)
        {
            string result = userStoryID;
            string blockedFlag = (true == blocked ? "-B" : "");

            switch(scheduleState)
            {
                case "Defined":
                    result = result + string.Format("({0}{1})", "D", blockedFlag);
                    break;

                case "In-Progress":
                    result = result + string.Format("({0}{1})", "P", blockedFlag);
                    break;

                case "Completed":
                    result = result + string.Format("({0}{1})", "C", blockedFlag);
                    break;

                case "Accepted":
                    result = result + string.Format("({0}{1})", "A", blockedFlag);
                    break;

            }

            return result;
        }

        private void UpdateCellContents(Range currentCell, string userStoryID, string scheduleState, bool blocked)
        {
            if (null != currentCell.Value)
            {
                // Already have something in the cell!

                currentCell.Value = currentCell.Value + " " + AddStateInfoToUserStory(userStoryID, scheduleState, blocked);
            }
            else
            {
                currentCell.Value = AddStateInfoToUserStory(userStoryID, scheduleState, blocked);
            }
        }

        private string RetriveAccountName(string accountId, SF.SessionHeader header, SF.SoapClient queryClient)
        {
            string result = "??";

            string query = "Select Name FROM AccountCleanInfo WHERE AccountId = '" + accountId + "'";

            try
            {
                SF.QueryResult qResult;

                var limitInfo = queryClient.query(
                header,
                null,
                null,
                null,
                query,
                out qResult);

                IEnumerable<SF.Account> resultList = qResult.records.Cast<SF.Account>();
                //IEnumerable<SF.AccountCleanInfo> resultList = qResult.records.Cast<SF.AccountCleanInfo>();

                foreach (var account in resultList)
                {
                    result = account.Name;
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }

            return result;
        }

        private string RetriveOwnerName(string ownerId, SF.SessionHeader header, SF.SoapClient queryClient)
        {
            string result = "??";
            string query = "Select Name FROM User WHERE Id = '" + ownerId + "'";

            //try
            //{
            //    SF.QueryResult qResult;

            //    var limitInfo = queryClient.query(
            //    header,
            //    null,
            //    null,
            //    null,
            //    query,
            //    out qResult);

            //    IEnumerable<SF.Group> resultList = qResult.records.Cast<SF.Group>();

            //    foreach (var owner in resultList)
            //    {
            //        result = owner.Name;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XlHlp.DisplayInWatchWindow(ex.ToString());
            //    XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            //}

            return result;
        }



        #endregion

        #region Utility Routines

        private void ColorCodeCellByState(Range currentCell, string scheduleState, bool blocked)
        {
            try
            {
                if (blocked)
                {
                    currentCell.Font.Color = XlRgbColor.rgbRed;
                    return;
                }

                switch (scheduleState)
                {
                    case "Defined":
                        currentCell.Font.Color = XlRgbColor.rgbOrange;
                        break;

                    case "InProgress":
                        currentCell.Font.Color = XlRgbColor.rgbBlack;
                        break;

                    case "Completed":
                        currentCell.Font.Color = XlRgbColor.rgbGreen; ;
                        break;

                    case "Accepted":
                        currentCell.Font.Color = XlRgbColor.rgbBlue;
                        break;

                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }
        }

        private void DisableAndHide(System.Windows.Controls.Button button)
        {
            button.IsEnabled = false;
            button.Visibility = Visibility.Hidden;
        }

        private void DisableAndHide(DevExpress.Xpf.LayoutControl.LayoutGroup control)
        {
            control.IsEnabled = false;
            control.Visibility = Visibility.Hidden;
        }

        private void EnableAndShow(System.Windows.Controls.Button button)
        {
            button.IsEnabled = true;
            button.Visibility = Visibility.Visible;
        }

        private void EnableAndShow(DevExpress.Xpf.LayoutControl.LayoutGroup control)
        {
            control.IsEnabled = true;
            control.Visibility = Visibility.Visible;
        }

        private bool GetDisplayOrientation()
        {
            return (bool)cbOrientOutputVertically.IsChecked;
        }

        private bool IsValidSalesforceCaseNumber(string sfCaseNumber)
        {
            // TODO(crhodes):
            // Validate we have a legitmate SF case #

            return true;
        }

        public string ScrubHtml(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }

        private bool ValidUISelections()
        {
            //    if (cbeTeamProjectCollections.SelectedText.Length > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Must Select Team Project Collection first", "UI Selection Incomplete");
            //        return false;
            //    }
            return true;
        }

        public static void CreateBitmapFromVisual(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (Stream stm = File.Create(fileName))
            {
                bitmapEncoder.Save(stm);
            }
        }
        #endregion
    }
}
