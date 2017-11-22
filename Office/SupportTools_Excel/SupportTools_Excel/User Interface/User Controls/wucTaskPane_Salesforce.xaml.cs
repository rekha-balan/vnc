using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.ServiceModel;
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

using XlHlp = VNC.AddinHelper.Excel;
using SF = SupportTools_Excel.SalesforceSR;
using VNCTFS = VNC.TFS;

using System.Text.RegularExpressions;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for TaskPane_Salesforce_WPF.xaml
    /// </summary>
    public partial class wucTaskPane_Salesforce : UserControl
    {
        #region Fields and Properties

        SF.SoapClient _loginClient = null;
        EndpointAddress _apiAddr = null;
        string _sessionId = null;
        string _serverUrl = null;

        #endregion

        #region Constructors and Load

        public wucTaskPane_Salesforce()
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

        private void btnProcessCells_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                SF.SessionHeader header = new SF.SessionHeader();
                header.sessionId = _sessionId;

                Range selection = Globals.ThisAddIn.Application.Selection;

                foreach (Range currentCell in selection.Rows)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("Row:{0}  Col:{1}  Value:>{2}<",
                        currentCell.Row,
                        currentCell.Column,
                        currentCell.Value));

                    UpdateTriageRow(header, currentCell);

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

        private void btnProcessCells2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                SF.SessionHeader header = new SF.SessionHeader();
                header.sessionId = _sessionId;

                Range selection = Globals.ThisAddIn.Application.Selection;

                foreach (Range currentCell in selection.Rows)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("Row:{0}  Col:{1}  Value:>{2}<",
                        currentCell.Row,
                        currentCell.Column,
                        currentCell.Value));

                    //UpdateTriageRow2(header, currentCell);

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

        private bool IsValidSalesforceCaseNumber(string sfCaseNumber)
        {
            // TODO(crhodes):
            // Validate we have a legitmate SF case #

            return true;
        }

        private void btnDo_Something_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            //bool orientVertical = GetDisplayOrientation();
            // Sometimes we know better
            //orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                try
                {
                    SF.SessionHeader header = new SF.SessionHeader();
                    header.sessionId = _sessionId;

                    Range currentCell = Globals.ThisAddIn.Application.ActiveCell;

                    UpdateTriageRow(header, currentCell);
                }
                catch (Exception ex)
                {
                    XlHlp.DisplayInWatchWindow(ex.ToString());
                }
                //}
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

        private void btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            if (null != _loginClient)
            {
                try
                {
                    _loginClient.Close();

                    _loginClient = null;
                    _serverUrl = null;
                    _sessionId = null;
                    _apiAddr = null;

                    this.btnLogon.IsEnabled = true;
                    this.btnLogon.Visibility = Visibility.Visible;
                    this.btnDo_Something.IsEnabled = false;
                    this.btnDo_Something.Visibility = Visibility.Hidden;
                    this.btnLogoff.IsEnabled = false;
                    this.btnLogoff.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnLogon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //XlHlp.ScreenUpdatesOff();

                _loginClient = new SF.SoapClient("Soap");

                string sfPassword = "n4IJHNH1ZZ8t";
                string sfToken = "iiQMXOoX0xiUQ5JHqDM62jsNr";

                string sfUserName = "crhodes@harborsys.com";
                string loginPassword = sfPassword + sfToken;

                SF.LoginResult result = _loginClient.login(null, sfUserName, loginPassword);

                _sessionId = result.sessionId;
                _serverUrl = result.serverUrl;

                XlHlp.DisplayInWatchWindow(string.Format("Session ID is >{0}<   Server URL is >{1}<", _sessionId, _serverUrl));

                _apiAddr = new EndpointAddress(_serverUrl);

                // If we got this far, enable the processing buttons

                this.btnLogon.IsEnabled = false;
                this.btnLogon.Visibility = Visibility.Hidden;
                this.btnDo_Something.IsEnabled = true;
                this.btnDo_Something.Visibility = Visibility.Visible;
                this.btnLogoff.IsEnabled = true;
                this.btnLogoff.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //XlHlp.ScreenUpdatesOn(true);
            }
        }



        //private void btnGet_TFS_Info_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!ValidUISelections()) { return; }

        //    bool orientVertical = GetDisplayOrientation();

        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();

        //        TfsConfigurationServer configurationServer = VNCTFS.Helper.Get_ConfigurationServer(wucTFSProvider_Picker.Uri);

        //        CreateWorksheet_ConfigurationServer_Info(configurationServer, false, orientVertical);

        //        // Get the Team Project Collections

        //        ReadOnlyCollection<CatalogNode> projectCollectionNodes = VNCTFS.Helper.Get_TeamProjectCollectionNodes(configurationServer);

        //        // Add a sheet for each TeamProjectCollection

        //        foreach (CatalogNode teamProjectCollectionNode in projectCollectionNodes)
        //        {
        //            TfsTeamProjectCollection teamProjectCollection = VNCTFS.Helper.Get_TeamProjectCollection(configurationServer, teamProjectCollectionNode);

        //            CreateWorksheet_TPC_Info(teamProjectCollectionNode, teamProjectCollection, false, orientVertical);
        //        }
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

        //private void btnGet_TP_Info_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!ValidUISelections()) { return; }

        //    bool orientVertical = GetDisplayOrientation();

        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();

        //        char[] splitChars = { ',' };

        //        foreach (string tp in cbeTeamProjects.Text.Split(splitChars, StringSplitOptions.None))
        //        {
        //            try
        //            {
        //                TeamProject teamProject = VNCTFS.Helper.Get_TeamProject(_versionControlServer, tp.Trim());
        //                CreateWorksheet_TP_Info(teamProject, cbeSections.Text, orientVertical);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.ToString());
        //            }
        //        }
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

        private void teLabelFor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

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

        //private void CreateWorksheet_All_TP_Developers(bool orientVertical)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name));

        //    string sheetName = XlHlp.SafeSheetName(string.Format("{0}", "All_TP_Devs"));
        //    Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

        //    XlHlp.AddTitledInfo(insertAt.AddRow(), "All Developers All TeamProjects", _tfsTeamProjectCollection.Name);

        //    insertAt.MarkStart(XlHlp.MarkType.GroupTable);

        //    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "TFS Team Project");
        //    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Developer");
        //    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Changeset Count");
        //    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Earliest Date");
        //    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Latest Date");

        //    insertAt.IncrementRows();

        //    foreach (TeamProject teamProject in _versionControlServer.GetAllTeamProjects(refresh: true))
        //    {
        //        insertAt.ClearOffsets();

        //        XlHlp.AddContentToCell(insertAt.AddRow(), teamProject.Name);

        //        insertAt = AddSection_VCS_TP_Developers(insertAt, teamProject, true, teamProject.Name);
        //    }

        //    insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tbl_{0}", ws.Name));

        //    insertAt.Group(insertAt.OrientVertical, hide: true);
        //}

        //private void CreateWorksheet_TPC_Info(CatalogNode teamProjectCollectionNode, TfsTeamProjectCollection teamProjectCollection, bool showDetails, bool orientVertical)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name));

        //    string sheetName = XlHlp.SafeSheetName("TPC_" + GetTeamProjectCollectionName(teamProjectCollection));
        //    Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

        //    insertAt = AddSection_TPC_Info(insertAt, teamProjectCollection, showDetails);

        //    // Get a catalog of team projects for the collection

        //    ReadOnlyCollection<CatalogNode> teamProjects = teamProjectCollectionNode.QueryChildren(
        //        new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None);

        //    insertAt = AddSection_TP(insertAt, teamProjects);
        //}

        //private void CreateWorksheet_TP_Info(TeamProject teamProject, string sectionsToDisplay, bool orientVertical)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}({1})",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name, teamProject.Name));

        //    string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}", "TP_", teamProject.ServerItem));
        //    Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    Project project = _workItemStore.Projects[teamProject.Name];

        //    XlHlp.AddTitledInfo(ws, 2, 1, "TP Name", teamProject.Name, titleLocation: XlHlp.TitleLocation.Top);

        //    XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

        //    //insertAt = AddSection_TP_Info(insertAt, teamProject);

        //    insertAt = AddSection_VCS(insertAt, teamProject, sectionsToDisplay);

        //    insertAt = AddSection_WIS(insertAt, project, sectionsToDisplay);
        //}

        //private static void CreateWorkSheet_VCS_ChangeSetInfo(int changesetId, bool orientVertical)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}  ChangesetId: {1}",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name, changesetId));

        //    Changeset changeSet = _versionControlServer.GetChangeset(changesetId);

        //    Display_VCS_Changeset_Info(changeSet);

        //    Display_VCS_ChangeSet_Changes(changeSet);

        //    Display_VCS_ChangeSet_AssociatedWorkItems(changeSet);

        //    Display_VCS_Changeset_WorkItems(changeSet);
        //}

        //private void CreateWorksheet_Workspaces(string tfsUri, string teamProjectCollection, bool orientVertical)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name));

        //    string sheetName = XlHlp.SafeSheetName(string.Format("{0}_{1}", teamProjectCollection, "WorkSpaces"));
        //    Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

        //    insertAt = AddSection_VCS_Workspaces(insertAt);
        //}

        #endregion

        #region AddSection_*

        //private XlHlp.XlLocation AddSection_CatalogNode(XlHlp.XlLocation insertAt, CatalogNode catalogNode)
        //{
        //    //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

        //    XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "FullPath:", catalogNode.FullPath);
        //    XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "IsDefault:", catalogNode.IsDefault.ToString());

        //    insertAt.ColumnsAdded = 2;

        //    if (!insertAt.OrientVertical)
        //    {
        //        // Skip past the info just added.
        //        insertAt.SetLocation(insertAt.RowStart(), insertAt.TableEndColumn + 1);
        //    }

        //    return insertAt;
        //}

        //private XlHlp.XlLocation AddSection_ConfigurationServer_Info(XlHlp.XlLocation insertAt, object showDetails)
        //{
        //    //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

        //    insertAt.MarkStart(XlHlp.MarkType.None);

        //    XlHlp.AddTitledInfo(insertAt.AddRow(2), "Name:", _configurationServer.Name);
        //    XlHlp.AddTitledInfo(insertAt.AddRow(2), "HasAuthenticated:", _configurationServer.HasAuthenticated.ToString());
        //    XlHlp.AddTitledInfo(insertAt.AddRow(2), "IsHostedServer:", _configurationServer.IsHostedServer.ToString());
        //    XlHlp.AddTitledInfo(insertAt.AddRow(2), "ClientCacheDirectoryForInstance:", _configurationServer.ClientCacheDirectoryForInstance);
        //    XlHlp.AddTitledInfo(insertAt.AddRow(2), "ClientCacheDirectoryForUser:", _configurationServer.ClientCacheDirectoryForUser);

        //    insertAt.MarkEnd(XlHlp.MarkType.None);

        //    if (!insertAt.OrientVertical)
        //    {
        //        // Skip past the info just added.
        //        insertAt.SetLocation(insertAt.RowStart(), insertAt.MarkEndColumn + 1);
        //    }

        //    //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

        //    return insertAt;
        //}

       #endregion

        #region Display_*

        //private static void Display_VCS_ChangeSet_Changes(Changeset changeSet)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("  Changes.Count: {0}", changeSet.Changes.Count()));

        //    foreach (Change change in changeSet.Changes)
        //    {
        //        XlHlp.DisplayInWatchWindow(string.Format("    ChangeType: {0}  CheckinDate: {1}  IsBranch: {2}  ItemType: {3}",
        //            change.ChangeType,
        //            change.Item.CheckinDate,
        //            change.Item.IsBranch,
        //            change.Item.ItemType));

        //        XlHlp.DisplayInWatchWindow(string.Format("      ServerItem: {0}", change.Item.ServerItem));

        //        if (change.MergeSources != null)
        //        {
        //            XlHlp.DisplayInWatchWindow(string.Format("      MergeSources.Count: {0}", change.MergeSources.Count()));
        //        }
        //    }
        //}

        //private static void Display_VCS_Changeset_Info(Changeset changeSet)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format(" Committer: {0}", changeSet.Committer));
        //    XlHlp.DisplayInWatchWindow(string.Format(" CreationDate: {0}", changeSet.CreationDate));
        //    XlHlp.DisplayInWatchWindow(string.Format(" Owner: {0}", changeSet.Owner));
        //    XlHlp.DisplayInWatchWindow(string.Format(" CheckinNote: {0}", changeSet.CheckinNote));
        //    XlHlp.DisplayInWatchWindow(string.Format(" Comment: {0}", changeSet.Comment));
        //}

        //private XlHlp.XlLocation DisplayListOf_Branches(XlHlp.XlLocation insertAt, BranchObject[] branches, bool displayDataOnly, string tableSuffix)
        //{
        //    XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

        //    Worksheet ws = insertAt.workSheet;

        //    if (!displayDataOnly)
        //    {
        //        insertAt.MarkStart(XlHlp.MarkType.GroupTable);

        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "RootItem.Item");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 30, "ParentBranch.Item");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "DateCreated");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner DisplayName");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "ChangeType");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Version");
        //        XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Related Branches");

        //        insertAt.IncrementRows();
        //    }

        //    foreach (BranchObject branch in branches)
        //    {
        //        insertAt.ClearOffsets();

        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Item);

        //        string parentBranch = branch.Properties.ParentBranch != null ? branch.Properties.ParentBranch.Item : "<none>";

        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parentBranch);
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.DateCreated.ToString());
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.Owner);
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.OwnerDisplayName);
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.ChangeType.ToString());
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Version.DisplayString);
        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.RelatedBranches.Count().ToString());

        //        insertAt.IncrementRows();
        //    }

        //    if (!displayDataOnly)
        //    {
        //        insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblRootBranches_{0}", tableSuffix));

        //        insertAt.Group(insertAt.OrientVertical, hide: true);
        //    }

        //    XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

        //    return insertAt;
        //}

        //private XlHlp.XlLocation DisplayListOf_TeamProjects(XlHlp.XlLocation insertAt, ReadOnlyCollection<CatalogNode> projectNodes)
        //{
        //    XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

        //    // The columns in this method need to be kept in sync with CreateTeamProjectsInfo()

        //    foreach (CatalogNode projectNode in projectNodes.OrderBy(tp => tp.Resource.DisplayName))
        //    {
        //        insertAt.ClearOffsets();

        //        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), projectNode.Resource.DisplayName);

        //        insertAt.IncrementRows();
        //    }

        //    XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

        //    return insertAt;
        //}

        #endregion


        private void UpdateTriageRow(SF.SessionHeader header, Range currentCell)
        {
            try
            {
                using (SF.SoapClient queryClient = new SF.SoapClient("Soap", _apiAddr))
                {
                    string sfCaseNumber = currentCell.Value.ToString();

                    if (IsValidSalesforceCaseNumber(sfCaseNumber))
                    {
                        string query = "SELECT CaseNumber, CreatedDate, Type, Case_Reason__c, Subject, Priority, Desired_Completion_Date__c, Status, Product_Status__c, Resource_Name__c, Estimated_Delivery_Date__c FROM Case WHERE CaseNumber = '0000" + sfCaseNumber + "'";

                        SF.QueryResult qResult;

                        var limitInfo = queryClient.query(
                        header,
                        null,
                        null,
                        null,
                        query,
                        out qResult);

                        IEnumerable<SF.Case> caseList = qResult.records.Cast<SF.Case>();

                        foreach (var caseInfo in caseList)
                        {
                            currentCell.Offset[0, 1].Value = caseInfo.CreatedDate;
                            currentCell.Offset[0, 2].Value = caseInfo.Type;
                            currentCell.Offset[0, 3].Value = caseInfo.Case_Reason__c;
                            currentCell.Offset[0, 4].Value = caseInfo.Subject;
                            currentCell.Offset[0, 5].Value = caseInfo.Priority; // Urgency
                            currentCell.Offset[0, 6].Value = caseInfo.Desired_Completion_Date__c;
                            currentCell.Offset[0, 7].Value = caseInfo.Status;
                            currentCell.Offset[0, 8].Value = caseInfo.Product_Status__c;
                            currentCell.Offset[0, 9].Value = caseInfo.Resource_Name__c;
                            currentCell.Offset[0, 10].Value = caseInfo.Estimated_Delivery_Date__c;
                            currentCell.Offset[0, 10].Value = caseInfo.Revised_Delivery_Date__c;
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
            }
        }

        private void UpdateTriageRow2(SF.SessionHeader header, Range currentCell)
        {
            try
            {
                using (SF.SoapClient queryClient = new SF.SoapClient("Soap", _apiAddr))
                {
                    string sfCaseNumber = currentCell.Value.ToString();

                    if (IsValidSalesforceCaseNumber(sfCaseNumber))
                    {
                        string query = "SELECT CaseNumber, CreatedDate, Type, Case_Reason__c FROM Case WHERE CaseNumber = '0000" + sfCaseNumber + "'";

                        SF.QueryResult qResult;

                        var limitInfo = queryClient.query(
                        header,
                        null,
                        null,
                        null,
                        query,
                        out qResult);

                        IEnumerable<SF.Case> caseList = qResult.records.Cast<SF.Case>();

                        foreach (var caseInfo in caseList)
                        {
                            currentCell.Offset[0, 1].Value = caseInfo.CreatedDate;
                            currentCell.Offset[0, 2].Value = caseInfo.Type;
                            currentCell.Offset[0, 3].Value = caseInfo.Case_Reason__c;
                            //currentCell.Offset[0, 4].Value = caseInfo.Subject;
                            //currentCell.Offset[0, 5].Value = caseInfo.Priority; // Urgency
                            //currentCell.Offset[0, 6].Value = caseInfo.Desired_Completion_Date__c;
                            //currentCell.Offset[0, 7].Value = caseInfo.Status;
                            //currentCell.Offset[0, 8].Value = caseInfo.Product_Status__c;
                            //currentCell.Offset[0, 9].Value = caseInfo.Reasource_Name__c;
                            //currentCell.Offset[0, 11].Value = caseInfo.Estimated_Delivery_Date__c;
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
            }
        }
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
            //if (cbeTeamProjectCollections.SelectedText.Length > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("Must Select Team Project Collection first", "UI Selection Incomplete");
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
