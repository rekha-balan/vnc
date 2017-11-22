using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Microsoft.Office.Interop.Excel;

using Rally.RestApi;
using Rally.RestApi.Json;
using Rally.RestApi.Response;

using XlHlp = VNC.AddinHelper.Excel;
using VNCTFS = VNC.TFS;
using VNCRally = VNC.Rally;

using System.Text.RegularExpressions;
using System.Globalization;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for TaskPane_Rally_WPF.xaml
    /// </summary>
    public partial class wucTaskPane_Rally : UserControl
    {
        #region Fields and Properties

        // Rally Stuff

        string _Rally_username = "crhodes@harborsys.com";
        string _Rally_password = "H0jnacki!";
        string _Rally_serverUrl = "https://rally1.rallydev.com";

        #endregion

        #region Constructors and Load

        public wucTaskPane_Rally()
        {
            InitializeComponent();
            LoadControlContents();
        }

        private void LoadControlContents()
        {
            try
            {
                //wucTFSProvider_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
                //tfsQuery_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //wucTFSProvider_Picker.ControlChanged += tfsProvider_Picker1_ControlChanged;
        }

        #endregion

        #region Event Handlers

        private void btnDisplayDXForm_Click(object sender, RoutedEventArgs e)
        {
            var dxFrm = new User_Interface.Forms.DXWindow1();
            
        }

        private void btnDisplayForm_Click(object sender, RoutedEventArgs e)
        {
            var frm = new User_Interface.Forms.frmWPFHost();
            frm.Show();
        }

        private void btnDo_Something_Click(object sender, RoutedEventArgs e)
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

                insertAt = Find_UserStories(insertAt, teSalesForceID.Text);
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

                CreateWorksheet_GetListOfUserStories(restApi, orientVertical);

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

        private void cbeTeamProjectCollections_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                //_tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(cbeTeamProjectCollections.SelectedText));

                //if (_tfsTeamProjectCollection == null)
                //{
                //    MessageBox.Show("Cannot GetTeamProjectCollection");
                //    return;
                //}

                //// Each time we change TeamProjectCollection, go get all the services we will use
                //// and put in _<member> variables.

                //Populate_TPC_Services(_tfsTeamProjectCollection);

                //PopulateTeamProjects(_tfsTeamProjectCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbeTeamProjectCollections_SelectedValueChanged(object sender, EventArgs e)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                //_tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(cbeTeamProjectCollections.SelectedText));

                //if (_tfsTeamProjectCollection == null)
                //{
                //    MessageBox.Show("Cannot GetTeamProjectCollection");
                //    return;
                //}

                //// Each time we change TeamProjectCollection, go get all the services we will use
                //// and put in _<member> variables.

                //Populate_TPC_Services(_tfsTeamProjectCollection);

                //PopulateTeamProjects(_tfsTeamProjectCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void teChangeSetID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    double changeSetID = 0;
        //    string changeSet = (string)Globals.ThisAddIn.Application.ActiveCell.Value.ToString();

        //    if (double.TryParse(changeSet, out changeSetID))
        //    {
        //        teChangeSetID.Text = int.Parse(changeSetID.ToString()).ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("ChangeSetID not a number", "Error");
        //    }
        //}

        //private void tfsProvider_Picker1_ControlChanged()
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();

        //        PopulateTeamProjectCollections(wucTFSProvider_Picker.Uri);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        #endregion

        #region Main Function Routines

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

        private void CreateWorksheet_GetListOfUserStories(RallyRestApi restApi, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}{2}", "UserStories", "", ""));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_UserStories(insertAt);
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

            //RallyRestApi restApi = VNCRally.Helper.GetRestApi();

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

        private XlHlp.XlLocation AddSection_UserStories(XlHlp.XlLocation insertAt)
        {
            Worksheet ws = insertAt.workSheet;

            RallyRestApi restApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_username, _Rally_password);

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

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormatedID");
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

                string lastWeek = DateTime.Now.AddDays(-3.0).ToString("yyyy-MM-dd");

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

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormatedID");
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

        private XlHlp.XlLocation Find_UserStories(XlHlp.XlLocation insertAt, string salesforceID)
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

            //insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "FormatedID");
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

        

        #endregion

        #endregion

        #region Utility Routines

        //private string GetChangeInfo(Change change)
        //{
        //    StringBuilder changeInfo = new StringBuilder();

        //    changeInfo.AppendFormat("ChangeType: {0}  CheckinDate: {1}  IsBranch: {2}  ItemType: {3}",
        //            change.ChangeType,
        //            change.Item.CheckinDate,
        //            change.Item.IsBranch,
        //            change.Item.ItemType);

        //    changeInfo.AppendFormat("  ServerItem: {0}", change.Item.ServerItem);

        //    if (change.MergeSources != null)
        //    {
        //        changeInfo.AppendFormat("  MergeSources.Count: {0}", change.MergeSources.Count());
        //    }

        //    return changeInfo.ToString();
        //}


        //private void PopulateTeamProjectCollections(string tfsServerUri)
        //{
        //    // TODO (crhodes): Update List of Team Projects

        //    _configurationServer = VNCTFS.Helper.Get_ConfigurationServer(tfsServerUri);

        //    // Get the Team Project Collections

        //    ReadOnlyCollection<CatalogNode> projectCollectionNodes = VNCTFS.Helper.Get_TeamProjectCollectionNodes(_configurationServer);

        //    // Populate

        //    DevExpress.Xpf.Editors.ListItemCollection itemCol = cbeTeamProjectCollections.Items;
        //    //ComboBoxItemCollection itemCol = cbeTeamProjectCollections.Properties.Items;


        //    itemCol.BeginUpdate();

        //    itemCol.Clear();

        //    foreach (CatalogNode teamProjectCollectionNode in projectCollectionNodes)
        //    {
        //        TfsTeamProjectCollection teamProjectCollection = VNCTFS.Helper.Get_TeamProjectCollection(_configurationServer, teamProjectCollectionNode);

        //        // TODO (crhodes): Maybe a class that contains a friendly name and a URI so populating Team Projects is easier.

        //        itemCol.Add(teamProjectCollection.Uri);
        //        //itemCol.Add(GetTeamProjectCollectionName(teamProjectCollection));
        //    }

        //    cbeTeamProjectCollections.SelectedIndex = -1;
        //    itemCol.EndUpdate();
        //}

        //private void PopulateTeamProjects(TfsTeamProjectCollection tpc)
        //{
        //    var projectList = (from Project prj in _workItemStore.Projects select prj.Name).ToList();

        //    DevExpress.Xpf.Editors.ListItemCollection itemCol = cbeTeamProjects.Items;
        //    //CheckedListBoxItemCollection itemCol = cbeTeamProjects.Properties.Items;

        //    itemCol.BeginUpdate();
        //    itemCol.Clear();

        //    foreach (var item in projectList)
        //    {
        //        itemCol.Add(item);
        //    }

        //    itemCol.EndUpdate();
        //}

        #endregion

        #region Private Methods

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

        #endregion
    }
}
