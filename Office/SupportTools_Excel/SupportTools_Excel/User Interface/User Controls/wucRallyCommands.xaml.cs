using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
    /// Interaction logic for wucRallyCommands.xaml
    /// </summary>
    public partial class wucRallyCommands : UserControl
    {


        // Rally Stuff

        string _Rally_username = "crhodes@harborsys.com";
        string _Rally_password = "H0jnacki!";
        string _Rally_serverUrl = "https://rally1.rallydev.com";
        private RallyRestApi _Rally_RestApi;

        public wucRallyCommands()
        {
            InitializeComponent();
        }

        private void btnFindUserStories_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                Worksheet ws = Globals.ThisAddIn.Application.ActiveSheet;
                Range currentCell = Globals.ThisAddIn.Application.ActiveCell;

                XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: currentCell.Row, column: currentCell.Column, orientVertical: orientVertical);

                RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);

                insertAt = Find_UserStories(insertAt, restApi, teSalesForceID.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnGetProjects_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                // Put Actions Here.  If Creating a Worksheet it would look like one of these.
                // TODO(crhodes): Maybe can do this with a delegate passed into a routine that does all this boilerplate stuff.

                RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);
                var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems

                CreateWorksheet_GetListOfProjects(restApi, workspaceRef, orientVertical);

                //CreateWorksheet_All_TP_Developers(orientVertical);
                //CreateWorksheet_All_TP_AreaCheck(cbeAreas.Text, orientVertical);
                //CreateWorksheet_VCS_Branches(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnGetUserStories_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                // Put Actions Here.  If Creating a Worksheet it would look like one of these.
                // TODO(crhodes): Maybe can do this with a delegate passed into a routine that does all this boilerplate stuff.

                RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);

                CreateWorksheet_GetListOfUserStories(restApi, (int)seDays.Value, orientVertical);

                //CreateWorksheet_All_TP_Developers(orientVertical);
                //CreateWorksheet_All_TP_AreaCheck(cbeAreas.Text, orientVertical);
                //CreateWorksheet_VCS_Branches(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnGetWorkspaces_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                // Put Actions Here.  If Creating a Worksheet it would look like one of these.
                // TODO(crhodes): Maybe can do this with a delegate passed into a routine that does all this boilerplate stuff.

                CreateWorksheet_GetWorkspaces(orientVertical);

                //CreateWorksheet_All_TP_Developers(orientVertical);
                //CreateWorksheet_All_TP_AreaCheck(cbeAreas.Text, orientVertical);
                //CreateWorksheet_VCS_Branches(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnShowUserStory_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                XlHlp.DisplayInWatchWindow(string.Format("{0}",
                    System.Reflection.MethodInfo.GetCurrentMethod().Name));

                string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}{2}", "UserStories", "", ""));
                Worksheet ws = Globals.ThisAddIn.Application.ActiveSheet;
                Range currentCell = Globals.ThisAddIn.Application.ActiveCell;

                XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: currentCell.Row, column: currentCell.Column, orientVertical: orientVertical);

                insertAt = Display_UserStory(insertAt, teUserStoryID.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private bool GetDisplayOrientation()
        {
            return (bool)cbOrientOutputVertically.IsChecked;
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

        private XlHlp.XlLocation Find_UserStories(XlHlp.XlLocation insertAt, RallyRestApi restApi, string salesforceID)
        {
            Worksheet ws = insertAt.workSheet;


            var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems
            var projectRef = "/project/36782545694";    // Development

            if (insertAt.OrientVertical)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "UserStories", "X");
                //XlHlp.AddContentToCell(insertAt.AddRow(), "UserStories");
            }
            else
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                //insertAt.IncrementColumns();
            }

            //insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormattedID");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Product");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Project");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Release");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Iteration");


            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Plan Estimate");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ScheduleState");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "InProgress Date");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Accepted Date");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Blocked");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "BA Status");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Urgency");

            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Tasks");


            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Estiamte");

            insertAt.IncrementRows();

            try
            {
                Request request = new Request("HierarchicalRequirement");

                request.Workspace = workspaceRef;
                request.Project = projectRef;
                request.ProjectScopeUp = false;
                request.ProjectScopeDown = true;

                request.Fetch = new List<string>()
                {
                    "c_Product",
                    "Project",
                    "Release",
                    "Iteration",
                    "Name",
                    "FormattedID",
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

                request.Query = new Query("Name", Query.Operator.Contains, salesforceID);

                QueryResult queryResult = restApi.Query(request);

                foreach (var rslt in queryResult.Results)
                {
                    insertAt.ClearOffsets();

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["FormattedID"] != null ? rslt["FormattedID"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Name"] != null ? rslt["Name"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Product"] != null ? rslt["c_Product"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Project"] != null ? rslt["Project"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Release"] != null ? rslt["Release"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Iteration"] != null ? rslt["Iteration"]._refObjectName : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["PlanEstimate"] != null ? rslt["PlanEstimate"].ToString() : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["ScheduleState"] != null ? rslt["ScheduleState"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["InProgressDate"] != null ? rslt["InProgressDate"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["AcceptedDate"] != null ? rslt["AcceptedDate"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Blocked"].ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_BAStatus"] != null ? rslt["c_BAStatus"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Urgency"] != null ? rslt["c_Urgency"] : "");

                    insertAt.IncrementRows();
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }

            //insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblUS_{0}", ws.Name));

            //insertAt.Group(insertAt.OrientVertical, hide: true);

            //insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;

        }

        #region CreateWorksheet_*

        private void CreateWorksheet_GetListOfProjects(RallyRestApi restApi, string workspaceRef, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}{2}", "Projects", "", ""));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_Projects(insertAt, restApi, workspaceRef);
        }

        private void CreateWorksheet_GetListOfUserStories(RallyRestApi restApi, int daysToSearch, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}{2}", "UserStories", "", ""));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_UserStories(insertAt, daysToSearch, restApi);
        }

        private void CreateWorksheet_GetWorkspaces(bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}{2}", "Workspaces", "", ""));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_Workspaces(insertAt);
        }

        #endregion

        #region AddSection_*

        private XlHlp.XlLocation AddSection_Projects(XlHlp.XlLocation insertAt, RallyRestApi restApi, string workspaceRef)
        {
            Worksheet ws = insertAt.workSheet;

            if (insertAt.OrientVertical)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "UserStories", "X");
                XlHlp.AddContentToCell(insertAt.AddRow(), "Projects");
            }
            else
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                //insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Description");

            insertAt.IncrementRows();

            try
            {
                Request prjRequest = new Request("project");
                prjRequest.Workspace = workspaceRef;
                prjRequest.Fetch = new List<string>() { "Name", "Description", "Parent", "Children" };
                //prjRequest.Query = new Query("project");

                QueryResult prjResults = restApi.Query(prjRequest);

                foreach (var project in prjResults.Results)
                {
                    var name = project.Name;
                    var name2 = project["Name"];
                    var parent = project["Parent"];
                    var children = project["Children"];

                    insertAt.ClearOffsets();
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), project["Name"]);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), project["Description"]);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parent.Fields.Count.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), children.Fields.Count.ToString());

                    insertAt.IncrementRows();
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblWorkSpace_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_UserStories(XlHlp.XlLocation insertAt, int daysToSearch, RallyRestApi restApi)
        {
            Worksheet ws = insertAt.workSheet;


            var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems
            var projectRef = "/project/36782545694";    // Development

            if (insertAt.OrientVertical)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "UserStories", "X");
                XlHlp.AddContentToCell(insertAt.AddRow(), "UserStories");
            }
            else
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                //insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormattedID");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Product");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Project");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Release");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Iteration");


            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Plan Estimate");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ScheduleState");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "InProgress Date");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Accepted Date");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Blocked");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "BA Status");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Urgency");

            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Tasks");


            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Estiamte");

            insertAt.IncrementRows();

            try
            {
                Request request = new Request("HierarchicalRequirement");

                request.Workspace = workspaceRef;
                request.Project = projectRef;
                request.ProjectScopeUp = false;
                request.ProjectScopeDown = true;

                request.Fetch = new List<string>()
                {
                    "c_Product",
                    "Project",
                    "Release",
                    "Iteration",
                    "Name",
                    "FormattedID",
                    "PlanEstimate",
                    "ScheduleState",
                    "InprogressDate",
                    "AcceptedDate",
                    "Blocked",
                    "c_BAStatus",
                    "c_Urgency",
                    "Tasks"
                };

                string lastWeek = DateTime.Now.AddDays(-daysToSearch).ToString("yyyy-MM-dd");

                //request.Query = new Query("Project.ObjectID", Query.Operator.Equals, "22222");
                request.Query = new Query("LastUpdateDate", Query.Operator.GreaterThan, lastWeek);

                QueryResult queryResult = restApi.Query(request);

                foreach (var rslt in queryResult.Results)
                {
                    insertAt.ClearOffsets();

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["FormattedID"] != null ? rslt["FormattedID"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Name"] != null ? rslt["Name"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Product"] != null ? rslt["c_Product"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Project"] != null ? rslt["Project"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Release"] != null ? rslt["Release"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Iteration"] != null ? rslt["Iteration"]._refObjectName : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["PlanEstimate"] != null ? rslt["PlanEstimate"].ToString() : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["ScheduleState"] != null ? rslt["ScheduleState"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["InProgressDate"] != null ? rslt["InProgressDate"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["AcceptedDate"] != null ? rslt["AcceptedDate"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Blocked"].ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_BAStatus"] != null ? rslt["c_BAStatus"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Urgency"] != null ? rslt["c_Urgency"] : "");

                    insertAt.IncrementRows();
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblUS_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;

        }

        private XlHlp.XlLocation AddSection_Workspaces(XlHlp.XlLocation insertAt)
        {
            Worksheet ws = insertAt.workSheet;

            RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);

            if (insertAt.OrientVertical)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "UserStories", "X");
                XlHlp.AddContentToCell(insertAt.AddRow(), "Workspaces");
            }
            else
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                //insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Reference");

            insertAt.IncrementRows();

            try
            {
                QueryResult queryResult = VNCRally.Helper.GetWorkspaces(restApi);

                foreach (var rslt in queryResult.Results)
                {
                    insertAt.ClearOffsets();
                    var workspaceRef = rslt["_ref"];
                    var workspaceName = rslt["Name"];

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspaceName != null ? workspaceName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspaceRef != null ? workspaceRef : "");


                    this.cbeWorkspaces.Items.Add(workspaceRef);
                    insertAt.IncrementRows();
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblWorkSpace_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #region Display_*

        private XlHlp.XlLocation Display_UserStory(XlHlp.XlLocation insertAt, string userStoryID)
        {
            Worksheet ws = insertAt.workSheet;

            RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);

            var workspaceRef = "/workspace/7446359356"; // Harbor Health Systems
            var projectRef = "/project/36782545694";    // Development

            if (insertAt.OrientVertical)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "UserStories", "X");
                //XlHlp.AddContentToCell(insertAt.AddRow(), "UserStories");
            }
            else
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                //insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormattedID");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Product");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Project");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Release");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Iteration");


            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Plan Estimate");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ScheduleState");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "InProgress Date");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Accepted Date");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Blocked");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "BA Status");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Urgency");

            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Tasks");


            //XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Estiamte");

            insertAt.IncrementRows();

            try
            {
                Request request = new Request("HierarchicalRequirement");

                request.Workspace = workspaceRef;
                request.Project = projectRef;
                request.ProjectScopeUp = false;
                request.ProjectScopeDown = true;

                request.Fetch = new List<string>()
                {
                    "c_Product",
                    "Project",
                    "Release",
                    "Iteration",
                    "Name",
                    "FormattedID",
                    "PlanEstimate",
                    "ScheduleState",
                    "InprogressDate",
                    "AcceptedDate",
                    "Blocked",
                    "c_BAStatus",
                    "c_Urgency",
                    "Tasks"
                };

                //request.Query = new Query("Project.ObjectID", Query.Operator.Equals, "22222");
                //request.Query = new Query("LastUpdateDate", Query.Operator.GreaterThan, "2016-03-01");

                request.Query = new Query("FormattedID", Query.Operator.Equals, userStoryID);

                QueryResult queryResult = restApi.Query(request);

                foreach (var rslt in queryResult.Results)
                {
                    insertAt.ClearOffsets();

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["FormattedID"] != null ? rslt["FormattedID"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Name"] != null ? rslt["Name"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Product"] != null ? rslt["c_Product"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Project"] != null ? rslt["Project"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Release"] != null ? rslt["Release"]._refObjectName : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Iteration"] != null ? rslt["Iteration"]._refObjectName : "");


                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["PlanEstimate"] != null ? rslt["PlanEstimate"].ToString() : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["ScheduleState"] != null ? rslt["ScheduleState"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["InProgressDate"] != null ? rslt["InProgressDate"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["AcceptedDate"] != null ? rslt["AcceptedDate"] : "");

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["Blocked"].ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_BAStatus"] != null ? rslt["c_BAStatus"] : "");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), rslt["c_Urgency"] != null ? rslt["c_Urgency"] : "");

                    insertAt.IncrementRows();
                }
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
            }

            //insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblUS_{0}", ws.Name));

            //insertAt.Group(insertAt.OrientVertical, hide: true);

            //insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;

        }

        #endregion

    }
}
