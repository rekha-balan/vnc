using System;
using System.Collections.Generic;
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

using Rally.RestApi;
using Rally.RestApi.Json;
using Rally.RestApi.Response;

using Microsoft.Office.Interop.Excel;

using SF = SupportTools_Excel.SalesforceSR;

using VNCRally = VNC.Rally;
using VNCXaml = VNC.Xaml;
using XlHlp = VNC.AddinHelper.Excel;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucLogonLogoff.xaml
    /// </summary>
    public partial class wucTriageCommands : UserControl
    {
        #region Constructors and Load
        public wucTriageCommands()

        {
            InitializeComponent();
        }

        #endregion

        #region Enumerations
        enum ExcelColumnOffset : int
        {
            CreatedDate = 1,
            LastModifiedDate = 2,
            Account_Name = 3,
            Case_Owner = 4,
            Type = 5,
            CaseReason = 6,
            Subject = 7,
            Priority = 8,
            DesiredCompletionDate = 9,
            Status = 10,
            ProductStatus = 11,
            ResourceName = 12,
            EstimatedDeliveryDate = 13,
            RevisedDeliveryDate = 14,
            Executive_Leadership_Team = 15,
            Product_Management = 16,
            Development = 17,
            ETL_Dev = 18,
            ETL_Support = 19,
            HHS_Dev = 20,
            HHS_Support = 21,
            Core1_Dev = 22,
            Core2_Dev = 23,
            Sedgwick_Dev = 24,
            Sedgwick_Support = 25,
            Production_Support_New = 26,
            Quality_Assurance = 27,
            Reporting = 28,
            DevOps = 29,    // Was Product_Support
            Data_Analytics = 30, 
            Triage_Notes = 31,
            Was_Discussed = 32
        }

        #endregion

        #region Fields and Properties


        // Rally Stuff

        //string _Rally_username = "crhodes@harborsys.com";
        //string _Rally_password = "H0jnacki!";
        //string _Rally_serverUrl = "https://rally1.rallydev.com";
        private RallyRestApi _Rally_RestApi;

        // Salesforce Stuff

        //SF.SoapClient _Salesforce_loginClient = null;
        string _Salesforce_SessionId = null;
        //string _Salesforce_ServerUrl = null;
        EndpointAddress _Salesforce_apiAddr = null;
        public RallyRestApi Rally_RestApi
        {
            get
            {
                if (null ==_Rally_RestApi)
                {
                    
                }
                return _Rally_RestApi;
            }
            set
            {
                _Rally_RestApi = value;
            }
        }

        public EndpointAddress Salesforce_apiAddr
        {
            get
            {
                return _Salesforce_apiAddr;
            }
            set
            {
                _Salesforce_apiAddr = value;
            }
        }

        public string Salesforce_SessionId
        {
            get
            {
                return _Salesforce_SessionId;
            }
            set
            {
                _Salesforce_SessionId = value;
            }
        }

        #endregion
        
        #region Event Handlers

        private void btnRetrieveInfo_Rally_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                Range selection = Globals.ThisAddIn.Application.Selection;

                // TODO(crhodes):
                // Should get this from UI

                var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems
                var projectRef = "/project/36782545694";    // Development

                foreach (Range currentCell in selection.Rows)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("Row:{0}  Col:{1}  Value:>{2}<",
                        currentCell.Row,
                        currentCell.Column,
                        currentCell.Value));

                    UpdateTriageRowAndLoadDataSet_WithRallyInformation(Rally_RestApi, currentCell, workspaceRef, projectRef);
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnRetrieveInfo_Salesforce_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                SF.SessionHeader header = new SF.SessionHeader();
                header.sessionId = Salesforce_SessionId;

                Range selection = Globals.ThisAddIn.Application.Selection;

                foreach (Range currentCell in selection.Rows)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("Row:{0}  Col:{1}  Value:>{2}<",
                        currentCell.Row,
                        currentCell.Column,
                        currentCell.Value));

                    var notes = currentCell.Offset[0, ExcelColumnOffset.Triage_Notes].Value;

                    UpdateTriageRowAndLoadDataSet_WithSalesforceInformation(header, currentCell, notes);

                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnUpdateRows_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                SF.SessionHeader header = new SF.SessionHeader();
                header.sessionId = Salesforce_SessionId;

                Range selection = Globals.ThisAddIn.Application.Selection;

                // TODO(crhodes):
                // Should pass this in!

                var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems
                var projectRef = "/project/36782545694";    // Development

                foreach (Range currentCell in selection.Rows)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("Row:{0}  Col:{1}  Value:>{2}<",
                        currentCell.Row,
                        currentCell.Column,
                        currentCell.Value));

                    var notes = currentCell.Offset[0, 25].Value;

                    UpdateTriageRowAndUpdateDataSet_WithSalesforceInformation(header, currentCell, notes);
                    UpdateTriageRowAndUpdateDataSet_WithRallyInformation(Rally_RestApi, currentCell, workspaceRef, projectRef);

                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        #endregion

        #region Main Function Routines

        private void UpdateTriageRowAndLoadDataSet_WithRallyInformation(RallyRestApi restApi, Range currentCell, string workspaceRef, string projectRef)
        {                                  
            // TODO(crhodes):
            // Fix this to do proper leading 0's so always have 8 digits, 00001234, 00012345, 00123456

            string salesforceRallyID = string.Format("SF{0}", currentCell.Value);
            string salesforceID = string.Format("0000{0}", currentCell.Value);

            try
            {
                QueryResult queryResult = GetUserStoriesAssociatedWithSalesforceCase(restApi, workspaceRef, projectRef, salesforceRallyID);

                // Zap the current contents so we can detect if something already exists below

                ClearCurrentWorksheetContents_RallyInfo(currentCell);

                var rallyUserStories = Common.ApplicationDS.RallyUserStory;
                var rallyTasks = Common.ApplicationDS.RallyTask;

                foreach (var rslt in queryResult.Results)
                {
                    string project = rslt["Project"]._refObjectName;
                    bool blocked = bool.Parse(rslt["Blocked"].ToString());
                    //bool.TryParse(rslt["Blocked"].ToString(), out blocked);

                    string userStoryID = rslt["FormattedID"];
                    string scheduleState = rslt["ScheduleState"];
                    int updateColumn = 0;

                    updateColumn = GetWorksheetColumnToUpdate(project);

                    if (updateColumn > 0)
                    {
                        UpdateCellContents(currentCell.Offset[0, updateColumn], userStoryID, scheduleState, blocked);
                    }
                    else
                    {
                        XlHlp.DisplayInWatchWindow(string.Format("Unknown Rally Project >{0}< for US {1} - Column not updated", project, userStoryID));
                    }

                    // Add the User Story to the DataSet but check first to ensure it has not already been added.
                    // NB. This can happen when a User Story has been linked to multiple Salesforce Cases.
                    // When the second SF case is processed the US already exists, and the associated tasks, so just skip it.

                    var existingRow = Common.ApplicationDS.RallyUserStory.Where(id => id.FormattedID == userStoryID);

                    if (existingRow.Count() == 0)
                    {
                        AddToDataSet_RallyUserStory(salesforceID, rallyUserStories, rslt, userStoryID);

                        AddToDataSet_RallyTasks(restApi, rallyTasks, rslt, userStoryID);
                    }
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void UpdateTriageRowAndUpdateDataSet_WithRallyInformation(RallyRestApi restApi, Range currentCell, string workspaceRef, string projectRef)
        {
            try
            {
                // TODO(crhodes):
                // Fix this to do proper leading 0's

                string salesforceRallyID = string.Format("SF{0}", currentCell.Value);
                string salesforceID = string.Format("0000{0}", currentCell.Value);
                //request.Query = new Query("Project.ObjectID", Query.Operator.Equals, "22222");
                //request.Query = new Query("LastUpdateDate", Query.Operator.GreaterThan, "2016-03-01");

                DeleteRallyInformationForUserStory(salesforceID);

                // Zap the current contents so we can detect if something already exists below

                ClearCurrentWorksheetContents_RallyInfo(currentCell);

                QueryResult queryResult = GetUserStoriesAssociatedWithSalesforceCase(restApi, workspaceRef, projectRef, salesforceRallyID);

                var rallyUserStories = Common.ApplicationDS.RallyUserStory;
                var rallyTasks = Common.ApplicationDS.RallyTask;

                foreach (var rslt in queryResult.Results)
                {
                    string project = rslt["Project"]._refObjectName;
                    bool blocked = bool.Parse(rslt["Blocked"].ToString());

                    //bool.TryParse(rslt["Blocked"].ToString(), out blocked);

                    string userStoryID = rslt["FormattedID"];
                    string scheduleState = rslt["ScheduleState"];
                    int updateColumn = 0;

                    updateColumn = GetWorksheetColumnToUpdate(project);

                    if (updateColumn > 0)
                    {
                        UpdateCellContents(currentCell.Offset[0, updateColumn], userStoryID, scheduleState, blocked);
                    }
                    else
                    {
                        XlHlp.DisplayInWatchWindow(string.Format("Unknown Rally Project >{0}< for US {1} - Column not updated", project, userStoryID));
                    }

                    // Now go get the current information.

                    AddToDataSet_RallyUserStory(salesforceID, rallyUserStories, rslt, userStoryID);

                    Request taskRequest = new Request(rslt["Tasks"]);

                    QueryResult queryTaskResult = restApi.Query(taskRequest);

                    if (queryTaskResult.TotalResultCount > 0)
                    {
                        foreach (var task in queryTaskResult.Results)
                        {
                            AddToDataSet_RallyTask(rallyTasks, userStoryID, task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void UpdateTriageRowAndLoadDataSet_WithSalesforceInformation(SF.SessionHeader header, Range currentCell, string triageNotes)
        {
            try
            {
                using (SF.SoapClient queryClient = new SF.SoapClient("Soap", Salesforce_apiAddr))
                {
                    string sfCaseNumber = currentCell.Value.ToString();

                    if (IsValidSalesforceCaseNumber(sfCaseNumber))
                    {
                        IEnumerable<SF.Case> caseList = GetSalesforceCaseInfo(header, queryClient, sfCaseNumber);

                        foreach (var caseInfo in caseList)
                        {
                            //currentCell.Offset[0, 1].Value = caseInfo.Account;
                            currentCell.Offset[0, ExcelColumnOffset.CreatedDate].Value = caseInfo.CreatedDate;
                            currentCell.Offset[0, ExcelColumnOffset.LastModifiedDate].Value = caseInfo.LastModifiedDate;
                            currentCell.Offset[0, ExcelColumnOffset.Account_Name].Value = caseInfo.Account != null ? caseInfo.Account.Name : "<None>";
                            currentCell.Offset[0, ExcelColumnOffset.Case_Owner].Value = caseInfo.Owner.Name1;
                            currentCell.Offset[0, ExcelColumnOffset.Type].Value = caseInfo.Type;
                            currentCell.Offset[0, ExcelColumnOffset.CaseReason].Value = caseInfo.Case_Reason__c;
                            currentCell.Offset[0, ExcelColumnOffset.Subject].Value = caseInfo.Subject;
                            currentCell.Offset[0, ExcelColumnOffset.Priority].Value = caseInfo.Priority; // Urgency
                            currentCell.Offset[0, ExcelColumnOffset.DesiredCompletionDate].Value = caseInfo.Desired_Completion_Date__c;
                            currentCell.Offset[0, ExcelColumnOffset.Status].Value = caseInfo.Status;
                            currentCell.Offset[0, ExcelColumnOffset.ProductStatus].Value = caseInfo.Product_Status__c;
                            currentCell.Offset[0, ExcelColumnOffset.ResourceName].Value = caseInfo.Resource_Name__c;
                            currentCell.Offset[0, ExcelColumnOffset.EstimatedDeliveryDate].Value = caseInfo.Estimated_Delivery_Date__c.ToString().Length > 0 ? caseInfo.Estimated_Delivery_Date__c.ToString() : "<None>";
                            currentCell.Offset[0, ExcelColumnOffset.RevisedDeliveryDate].Value = caseInfo.Revised_Delivery_Date__c.ToString().Length > 0 ? caseInfo.Revised_Delivery_Date__c.ToString() : "<None>";

                            // TODO(crhodes):
                            // Get this out of here.  Have Spreasheet updates and DataSet updates separate.

                            AddToDataSet_SalesforceCase(header, triageNotes, queryClient, Common.ApplicationDS.SalesforceCase, caseInfo);
                        }
                    }
                    else
                    {
                        currentCell.Offset[0, 1].Value = "Invalid Salesforce Case#";
                        currentCell.Offset[0, 1].Font.Color = XlRgbColor.rgbRed;
                    }
                }
            }
            catch (Exception ex)
            {
                currentCell.Offset[0, 1].Value = ex.Message;
                currentCell.Offset[0, 1].Font.Color = XlRgbColor.rgbRed;
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void UpdateTriageRowAndUpdateDataSet_WithSalesforceInformation(SF.SessionHeader header, Range currentCell, string triageNotes)
        {
            try
            {
                using (SF.SoapClient queryClient = new SF.SoapClient("Soap", Salesforce_apiAddr))
                {
                    string sfCaseNumber = currentCell.Value.ToString();

                    if (IsValidSalesforceCaseNumber(sfCaseNumber))
                    {
                        IEnumerable<SF.Case> caseList = GetSalesforceCaseInfo(header, queryClient, sfCaseNumber);

                        foreach (var caseInfo in caseList)
                        {
                            //currentCell.Offset[0, 1].Value = caseInfo.Account;
                            currentCell.Offset[0, ExcelColumnOffset.CreatedDate].Value = caseInfo.CreatedDate;
                            currentCell.Offset[0, ExcelColumnOffset.LastModifiedDate].Value = caseInfo.LastModifiedDate;
                            currentCell.Offset[0, ExcelColumnOffset.Type].Value = caseInfo.Type;
                            currentCell.Offset[0, ExcelColumnOffset.CaseReason].Value = caseInfo.Case_Reason__c;
                            currentCell.Offset[0, ExcelColumnOffset.Subject].Value = caseInfo.Subject;
                            currentCell.Offset[0, ExcelColumnOffset.Priority].Value = caseInfo.Priority; // Urgency
                            currentCell.Offset[0, ExcelColumnOffset.DesiredCompletionDate].Value = caseInfo.Desired_Completion_Date__c;
                            currentCell.Offset[0, ExcelColumnOffset.Status].Value = caseInfo.Status;
                            currentCell.Offset[0, ExcelColumnOffset.ProductStatus].Value = caseInfo.Product_Status__c;
                            currentCell.Offset[0, ExcelColumnOffset.ResourceName].Value = caseInfo.Resource_Name__c;
                            currentCell.Offset[0, ExcelColumnOffset.EstimatedDeliveryDate].Value = caseInfo.Estimated_Delivery_Date__c;
                            currentCell.Offset[0, ExcelColumnOffset.RevisedDeliveryDate].Value = caseInfo.Revised_Delivery_Date__c;

                            // TODO(crhodes):
                            // Find the existing Salesforce Row in the DataSet and update the Salesforce Case information.

                            //        var totalSpaceAvailable = Common.ApplicationDS.Databases
                            //.Where(db => db.Instance_ID == dataRow.ID)
                            //.Where(db => db.NotFound != true)
                            //.Select(db => db.SpaceAvailable)
                            //.Sum();

                            Data.ApplicationDS.SalesforceCaseRow existingRow = 
                                (Data.ApplicationDS.SalesforceCaseRow)Common.ApplicationDS.SalesforceCase
                                .Where(id => id.CaseNumber == caseInfo.CaseNumber).First();

                            UpdateExistingSalesforceCase(header, triageNotes, queryClient, caseInfo, existingRow);
                        }
                    }
                    else
                    {
                        currentCell.Offset[0, 1].Value = "Invalid Salesforce Case#";
                        currentCell.Offset[0, 1].Font.Color = XlRgbColor.rgbRed;
                    }
                }
            }
            catch (Exception ex)
            {
                currentCell.Offset[0, 1].Value = ex.Message;
                currentCell.Offset[0, 1].Font.Color = XlRgbColor.rgbRed;
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        #endregion

        #region Private Methods

        private string AddStateInfoToUserStory(string userStoryID, string scheduleState, bool blocked)
        {
            string result = userStoryID;
            string blockedFlag = (true == blocked ? "-B" : "");

            switch (scheduleState)
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

        private void AddToDataSet_RallyTask(SupportTools_Excel.Data.ApplicationDS.RallyTaskDataTable rallyTasks, string userStoryID, dynamic task)
        {
            var newTaskRow = Common.ApplicationDS.RallyTask.NewRallyTaskRow();

            newTaskRow.FormattedID = task["FormattedID"];
            newTaskRow.Name = task["Name"];
            newTaskRow.State = task["State"];
            newTaskRow.CreationDate = task["CreationDate"];
            newTaskRow.Description = task["Description"];
            newTaskRow.LastUpdateDate = task["LastUpdateDate"];
            newTaskRow.Ready = task["Ready"].ToString();
            newTaskRow.Actuals = task["Actuals"] != null ? task["Actuals"].ToString() : "";
            newTaskRow.Blocked = task["Blocked"].ToString();
            newTaskRow.Estimate = task["Estimate"] != null ? task["Estimate"].ToString() : "";
            newTaskRow.TimeSpent = task["TimeSpent"] != null ? task["TimeSpent"].ToString() : "";
            newTaskRow.USFormattedID = userStoryID;  // For the relationship

            rallyTasks.AddRallyTaskRow(newTaskRow);
        }

        private static void AddToDataSet_RallyTasks(RallyRestApi restApi, Data.ApplicationDS.RallyTaskDataTable rallyTasks, dynamic rslt, string userStoryID)
        {
            Request taskRequest = new Request(rslt["Tasks"]);

            QueryResult queryTaskResult = restApi.Query(taskRequest);

            if (queryTaskResult.TotalResultCount > 0)
            {
                foreach (var task in queryTaskResult.Results)
                {
                    var newTaskRow = Common.ApplicationDS.RallyTask.NewRallyTaskRow();

                    newTaskRow.FormattedID = task["FormattedID"];
                    newTaskRow.Name = task["Name"];
                    newTaskRow.State = task["State"];
                    newTaskRow.CreationDate = task["CreationDate"];
                    newTaskRow.Description = task["Description"];
                    newTaskRow.LastUpdateDate = task["LastUpdateDate"];
                    newTaskRow.Ready = task["Ready"].ToString();
                    newTaskRow.Actuals = task["Actuals"] != null ? task["Actuals"].ToString() : "";
                    newTaskRow.Blocked = task["Blocked"].ToString();
                    newTaskRow.Estimate = task["Estimate"] != null ? task["Estimate"].ToString() : "";
                    newTaskRow.TimeSpent = task["TimeSpent"] != null ? task["TimeSpent"].ToString() : "";
                    newTaskRow.USFormattedID = userStoryID;  // For the relationship

                    rallyTasks.AddRallyTaskRow(newTaskRow);
                }
            }
        }

        private void AddToDataSet_RallyUserStory(string salesforceID, SupportTools_Excel.Data.ApplicationDS.RallyUserStoryDataTable rallyUserStories, dynamic rslt, string userStoryID)
        {
            var newRow = Common.ApplicationDS.RallyUserStory.NewRallyUserStoryRow();

            newRow.FormattedID = userStoryID;

            newRow.Name = rslt["Name"];
            newRow.Description = ScrubHtml(rslt["Description"]);
            newRow.Product = rslt["c_Product"] != null ? rslt["c_Product"] : "";
            newRow.Project = rslt["Project"] != null ? rslt["Project"]._refObjectName : "";
            newRow.Release = rslt["Release"] != null ? rslt["Release"]._refObjectName : "";
            newRow.Iteration = rslt["Iteration"] != null ? rslt["Iteration"]._refObjectName : "";
            newRow.PlanEstimate = rslt["PlanEstimate"] != null ? rslt["PlanEstimate"].ToString() : "";
            newRow.ScheduleState = rslt["ScheduleState"] != null ? rslt["ScheduleState"] : "";
            newRow.InprogressDate = rslt["InProgressDate"] != null ? rslt["InProgressDate"] : "";
            newRow.AcceptedDate = rslt["AcceptedDate"] != null ? rslt["AcceptedDate"] : "";
            newRow.Blocked = rslt["Blocked"].ToString();
            newRow.BlockedReason = rslt["BlockedReason"] != null ? rslt["BlockedReason"] : "";
            newRow.BAStatus = rslt["c_BAStatus"] != null ? rslt["c_BAStatus"] : "";
            newRow.Feature = rslt["Feature"] != null ? rslt["Feature"]._refObjectName : "";
            newRow.PortfolioItem = rslt["PortfolioItem"] != null ? rslt["PortfolioItem"]._refObjectName : "";
            newRow.HasParent = rslt["HasParent"].ToString();
            newRow.DirectChildrenCount = rslt["DirectChildrenCount"] != null ? rslt["DirectChildrenCount"].ToString() : "";
            newRow.Urgency = rslt["c_Urgency"] != null ? rslt["c_Urgency"] : "";
            newRow.SFCaseNumber = salesforceID;

            rallyUserStories.AddRallyUserStoryRow(newRow);
        }

        private void AddToDataSet_SalesforceCase(SF.SessionHeader header, string triageNotes, SF.SoapClient queryClient, Data.ApplicationDS.SalesforceCaseDataTable sfCases, SupportTools_Excel.SalesforceSR.Case caseInfo)
        {
            var newRow = Common.ApplicationDS.SalesforceCase.NewSalesforceCaseRow();

            newRow.CaseNumber = caseInfo.CaseNumber;

            if (null != caseInfo.Account)
            {
                newRow.Account = caseInfo.Account.Name;
            }
            else
            {
                newRow.Account = "";
            }

            // newRow.Account = RetriveAccountName(caseInfo.AccountId, header, queryClient);
            newRow.CreatedDate = caseInfo.CreatedDate.ToString();
            newRow.Last_Modified_Date = caseInfo.LastModifiedDate.ToString();
            newRow.Type = caseInfo.Type;
            newRow.Case_Reason = caseInfo.Case_Reason__c;
            newRow.Subject = caseInfo.Subject;
            newRow.Description = caseInfo.Description;
            newRow.Priority = caseInfo.Priority;
            newRow.Desired_Completion_Date = caseInfo.Desired_Completion_Date__c.ToString();
            newRow.Status = caseInfo.Status;
            newRow.Product_Status = caseInfo.Product_Status__c;
            newRow.Resource_Name = caseInfo.Resource_Name__c;
            newRow.Estimated_Delivery_Date = caseInfo.Estimated_Delivery_Date__c.ToString().Length > 0 ? caseInfo.Estimated_Delivery_Date__c.ToString() : "<None>";
            newRow.Revised_Delivery_Date = caseInfo.Revised_Delivery_Date__c.ToString().Length > 0 ? caseInfo.Revised_Delivery_Date__c.ToString() : "<None>";
            newRow.Owner = RetriveOwnerName(caseInfo.OwnerId, header, queryClient);
            newRow.Owner = caseInfo.OwnerId;
            //newRow.Owner = caseInfo.Owner.ToString();

            newRow.TriageNotes = triageNotes;

            sfCases.AddSalesforceCaseRow(newRow);
        }
        /// <summary>
        /// ClearCurrentWorksheetContents_RallyInfo
        /// 
        /// Remove the existing values from the current row so we can
        /// add new values.  A Salesforce case may have more than one
        /// Rally US assigned to a given Project(Team).
        /// </summary>
        /// <param name="currentCell"></param>
        private void ClearCurrentWorksheetContents_RallyInfo(Range currentCell)
        {
            currentCell.Offset[0, ExcelColumnOffset.Development].Clear();
            currentCell.Offset[0, ExcelColumnOffset.ETL_Dev].Clear();
            currentCell.Offset[0, ExcelColumnOffset.ETL_Support].Clear();
            currentCell.Offset[0, ExcelColumnOffset.HHS_Dev].Clear();
            currentCell.Offset[0, ExcelColumnOffset.HHS_Support].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Core1_Dev].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Core2_Dev].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Sedgwick_Dev].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Sedgwick_Support].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Production_Support_New].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Quality_Assurance].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Reporting].Clear();
            currentCell.Offset[0, ExcelColumnOffset.DevOps].Clear();
            currentCell.Offset[0, ExcelColumnOffset.Data_Analytics].Clear();
        }

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

        private void DeleteRallyInformationForUserStory(string salesforceID)
        {
            try
            {
                var existingRows = Common.ApplicationDS.RallyUserStory.Where(id => id.SFCaseNumber == salesforceID).ToList();

                foreach (var userStory in existingRows)
                {
                    XlHlp.DisplayInWatchWindow(userStory.FormattedID);
                    Common.ApplicationDS.RallyUserStory.Where(id => id.FormattedID == userStory.FormattedID).First().Delete();
                }

                Common.ApplicationDS.RallyUserStory.AcceptChanges();
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private static IEnumerable<SF.Case> GetSalesforceCaseInfo(SF.SessionHeader header, SF.SoapClient queryClient, string sfCaseNumber)
        {
            string query = @"
                SELECT 
                    AccountId, 
                    Account.Name, 
                    CaseNumber, 
                    CreatedDate, 
                    LastModifiedDate,
                    Type, 
                    Case_Reason__c, 
                    Subject, 
                    Description, 
                    Priority, 
                    Desired_Completion_Date__c, 
                    Status, OwnerId, 
                    Product_Status__c, 
                    Resource_Name__c, 
                    Estimated_Delivery_Date__c,
                    Revised_Delivery_Date__c,
                    Owner.Name
                FROM 
                    Case 
                WHERE 
                    CaseNumber = '0000" + sfCaseNumber + "'";

            SF.QueryResult qResult;

            var limitInfo = queryClient.query(
                                header,
                                null,
                                null,
                                null,
                                query,
                                out qResult);

            IEnumerable<SF.Case> caseList = qResult.records.Cast<SF.Case>();

            return caseList;
        }

        private static QueryResult GetUserStoriesAssociatedWithSalesforceCase(RallyRestApi restApi, string workspaceRef, string projectRef, string salesforceRallyID)
        {
            Request request = new Request("HierarchicalRequirement");

            request.Workspace = workspaceRef;
            request.Project = projectRef;
            request.ProjectScopeUp = false;
            request.ProjectScopeDown = true;

            request.Fetch = new List<string>()
                                {
                                    "Name",
                                    "Description",
                                    "FormattedID",
                                    "c_Product",
                                    "Project",
                                    "Release",
                                    "Iteration",
                                    "PlanEstimate",
                                    "ScheduleState",
                                    "InprogressDate",
                                    "AcceptedDate",
                                    "Blocked",
                                    "BlockedReason",
                                    "c_BAStatus",
                                    "c_Urgency",
                                    "c_AssignedTo",
                                    "Tasks",
                                    "HasParent",
                                    "DirectChildrenCount",
                                    "Feature",
                                    "PortfolioItem"
                                };

            //request.Query = new Query("Project.ObjectID", Query.Operator.Equals, "22222");
            //request.Query = new Query("LastUpdateDate", Query.Operator.GreaterThan, "2016-03-01");

            request.Query = new Query("Name", Query.Operator.Contains, salesforceRallyID);

            QueryResult queryResult = restApi.Query(request);

            return queryResult;
        }

        private static int GetWorksheetColumnToUpdate(string projectName)
        {
            int updateColumn = 0;

            switch (projectName)
            {
                case "Development":
                    updateColumn = (int)ExcelColumnOffset.Development;
                    break;

                case "ETL Dev":
                    updateColumn = (int)ExcelColumnOffset.ETL_Dev;
                    break;

                case "ETL Support":
                    updateColumn = (int)ExcelColumnOffset.ETL_Support;
                    break;

                case "HHS Dev":
                    updateColumn = (int)ExcelColumnOffset.HHS_Dev;
                    break;

                case "HHS Support":
                    updateColumn = (int)ExcelColumnOffset.HHS_Support;
                    break;

                case "Core1 Dev(Offshore)":
                    updateColumn = (int)ExcelColumnOffset.Core1_Dev;
                    break;

                case "Core2 Dev(Offshore)":
                    updateColumn = (int)ExcelColumnOffset.Core2_Dev;
                    break;

                case "Sedgwick Dev(Offshore)":
                    updateColumn = (int)ExcelColumnOffset.Sedgwick_Dev;
                    break;

                case "Sedgwick Support(Offshore)":
                    updateColumn = (int)ExcelColumnOffset.Sedgwick_Support;
                    break;

                case "Production Support(Offshore)":
                    updateColumn = (int)ExcelColumnOffset.Production_Support_New;
                    break;

                case "DevOps":
                    updateColumn = (int)ExcelColumnOffset.DevOps;
                    break;

                case "Data Analytics":
                    updateColumn = (int)ExcelColumnOffset.Data_Analytics;
                    break;

                case "Reporting":
                    updateColumn = (int)ExcelColumnOffset.Reporting;
                    //currentCell.Offset[0, 23].Value = rslt["FormattedID"];
                    //ColorCodeCellByState(currentCell.Offset[0, 23], rslt["ScheduleState"], blocked);
                    break;

                default:
                    updateColumn = -1;
                    break;
            }

            return updateColumn;
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
        
        private void UpdateExistingSalesforceCase(SF.SessionHeader header, string triageNotes, SF.SoapClient queryClient, SupportTools_Excel.SalesforceSR.Case caseInfo, Data.ApplicationDS.SalesforceCaseRow existingRow)
        {
            if (null != caseInfo.Account)
            {
                existingRow.Account = caseInfo.Account.Name;
            }
            else
            {
                existingRow.Account = "";
            }

            // newRow.Account = RetriveAccountName(caseInfo.AccountId, header, queryClient);
            existingRow.CreatedDate = caseInfo.CreatedDate.ToString();
            existingRow.Last_Modified_Date = caseInfo.LastModifiedDate.ToString();
            existingRow.Type = caseInfo.Type;
            existingRow.Case_Reason = caseInfo.Case_Reason__c;
            existingRow.Subject = caseInfo.Subject;
            existingRow.Description = caseInfo.Description;
            existingRow.Priority = caseInfo.Priority;
            existingRow.Desired_Completion_Date = caseInfo.Desired_Completion_Date__c.ToString();
            existingRow.Status = caseInfo.Status;
            existingRow.Product_Status = caseInfo.Product_Status__c;
            existingRow.Resource_Name = caseInfo.Resource_Name__c;
            existingRow.Estimated_Delivery_Date = caseInfo.Estimated_Delivery_Date__c.ToString();
            existingRow.Revised_Delivery_Date = caseInfo.Revised_Delivery_Date__c.ToString();
            existingRow.Owner = RetriveOwnerName(caseInfo.OwnerId, header, queryClient);
            existingRow.Owner = caseInfo.OwnerId;
            //newRow.Owner = caseInfo.Owner.ToString();

            existingRow.TriageNotes = triageNotes;
        }
        
        #endregion

        #region Utility Routines

        private bool IsValidSalesforceCaseNumber(string sfCaseNumber)
        {
            // TODO(crhodes):
            // Validate we have a legitmate SF case #

            return true;
        }

        private string RetriveAccountName(string accountId, SF.SessionHeader header, SF.SoapClient queryClient)
        {
            string result = "??";

            string query = "Select Name FROM Account WHERE AccountId = '" + accountId + "'";
            //string query = "Select Name FROM AccountCleanInfo WHERE AccountId = '" + accountId + "'";

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

        public string ScrubHtml(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }

        #endregion

    }
}
