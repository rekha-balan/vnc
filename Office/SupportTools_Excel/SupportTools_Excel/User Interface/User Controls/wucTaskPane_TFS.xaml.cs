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
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

using Microsoft.TeamFoundation.Server;

using XlHlp = VNC.AddinHelper.Excel;
using VNCTFS = VNC.TFS;

using System.Text.RegularExpressions;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for TaskPane_TFS_WPF.xaml
    /// </summary>
    public partial class wucTaskPane_TFS : UserControl
    {
        static IIdentityManagementService _identityManagementService;

        static List<TeamFoundationIdentity> _groups = new List<TeamFoundationIdentity>();
        static Dictionary<IdentityDescriptor, TeamFoundationIdentity> _identities =
            new Dictionary<IdentityDescriptor, TeamFoundationIdentity>(IdentityDescriptorComparer.Instance);

        // This is updated with the TFS Server changes

        TfsConfigurationServer _configurationServer;
        // These are updated when the Team Project Collection Changes

        static ICommonStructureService _commonStructureService;
        //static TeamProjectCollection _teamProjectCollection;
        static TfsTeamProjectCollection _tfsTeamProjectCollection;
        static VersionControlServer _versionControlServer;
        static WorkItemStore _workItemStore;

        #region Constructors and Load

        public wucTaskPane_TFS()
        {
            InitializeComponent();
            LoadControlContents();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wucTFSProvider_Picker.ControlChanged += tfsProvider_Picker1_ControlChanged;
        }

        #endregion
        
        #region Event Handlers
        
        private void btnCodeChurn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();
                //TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer("http://WhateverServerUrl");
                //IBuildServer buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
                //VersionControlServer VsServer = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));
                //IBuildDetail build = buildServer.GetAllBuildDetails(new Uri("http://WhateverBuildUrl"));

                //List<IChangesetsummary> associatedChangesets = InformationNodeConverters.GetAssociatedChangesets(build);


                //foreach (IChangesetsummary changeSetData in associatedChangesets)
                //{
                //    Changeset changeSet = VsServer.GetChangeset(changeSetData.ChangesetId);
                //    foreach (Change change in changeSet.Changes)
                //    {
                //        bool a = change.Item.IsContentDestroyed;
                //        long b = change.Item.ContentLength;
                //    }
                //} 
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

        private void btnGet_All_TP_AreaPathCheck_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_All_TP_AreaCheck(cbeAreas.Text, orientVertical);
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

        private void btnGet_All_TP_Developers_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_All_TP_Developers(orientVertical);
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

        private void btnGet_Branches_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            orientVertical = true;  // This sheet works better vertically.

            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_VCS_Branches(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);
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

        private void btnGet_ChangeSetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                int changeSetID = int.Parse(teChangeSetID.Text);

                CreateWorkSheet_VCS_ChangeSetInfo(changeSetID, orientVertical);
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

        private void btnGet_TemplateType_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();
            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_TP_TemplateType(orientVertical);
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

        private void btnGet_TFS_Info_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();


                //var aa = Microsoft.TeamFoundation.OperationalDatabaseNames.AnalysisCube;
                //var bb = Microsoft.TeamFoundation.OperationalDatabaseNames.CoreServices;
                //var cc = Microsoft.TeamFoundation.OperationalDatabaseNames.DeploymentRig;
                //var dd = Microsoft.TeamFoundation.OperationalDatabaseNames.LabExecution;
                //var ee = Microsoft.TeamFoundation.OperationalDatabaseNames.TeamBuild;
                //var ff = Microsoft.TeamFoundation.OperationalDatabaseNames.TestRig;
                //var gg = Microsoft.TeamFoundation.OperationalDatabaseNames.VersionControl;
                //var hh = Microsoft.TeamFoundation.OperationalDatabaseNames.Warehouse;
                //var ii = Microsoft.TeamFoundation.OperationalDatabaseNames.WorkItemTracking;
                //var jj = Microsoft.TeamFoundation.OperationalDatabaseNames.WorkItemTrackingAttachments;

                //System.Diagnostics.Debug.WriteLine(aa);
                //System.Diagnostics.Debug.WriteLine(bb);
                //System.Diagnostics.Debug.WriteLine(cc);
                //System.Diagnostics.Debug.WriteLine(dd);
                //System.Diagnostics.Debug.WriteLine(ee);
                //System.Diagnostics.Debug.WriteLine(ff);

                //System.Diagnostics.Debug.WriteLine(gg);
                //System.Diagnostics.Debug.WriteLine(hh);
                //System.Diagnostics.Debug.WriteLine(ii);
                //System.Diagnostics.Debug.WriteLine(jj);

                TfsConfigurationServer configurationServer = VNCTFS.Helper.Get_ConfigurationServer(wucTFSProvider_Picker.Uri);

                CreateWorksheet_ConfigurationServer_Info(configurationServer, false, orientVertical);

                // Get the Team Project Collections

                ReadOnlyCollection<CatalogNode> projectCollectionNodes = VNCTFS.Helper.Get_TeamProjectCollectionNodes(configurationServer);

                // Add a sheet for each TeamProjectCollection

                foreach (CatalogNode teamProjectCollectionNode in projectCollectionNodes)
                {
                    TfsTeamProjectCollection teamProjectCollection = VNCTFS.Helper.Get_TeamProjectCollection(configurationServer, teamProjectCollectionNode);

                    CreateWorksheet_TPC_Info(teamProjectCollectionNode, teamProjectCollection, false, orientVertical);
                }
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

        private void btnGet_TP_Info_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                char[] splitChars = { ',' };

                foreach (string tp in cbeTeamProjects.Text.Split(splitChars, StringSplitOptions.None))
                {
                    try
                    {
                        TeamProject teamProject = VNCTFS.Helper.Get_TeamProject(_versionControlServer, tp.Trim());
                        CreateWorksheet_TP_Info(teamProject, cbeSections.Text, orientVertical);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
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
            
        private void btnGet_TPC_Members_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                // Name: tfs.corp.firstam.com\\DefaultCollection    DisplayName: tfs.corp.firstam.com
                string tpcName = _versionControlServer.TeamProjectCollection.Name;
                tpcName = tpcName.Substring(tpcName.IndexOf("\\") + 1);
                CreateWorksheet_TPC_Members(tpcName, orientVertical);
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

        private void btnGet_TPC_ShelfSets_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_ShelveSets(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);

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

        private void btnGet_TPC_Workspaces_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                CreateWorksheet_Workspaces(wucTFSProvider_Picker.Uri, teTeamProjectCollection.Text, orientVertical);

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

        private void btnRunQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                char[] splitChars = { ',' };

                foreach (string tp in cbeTeamProjects.Text.Split(splitChars, StringSplitOptions.None))
                {
                    TeamProject teamProject = VNCTFS.Helper.Get_TeamProject(_versionControlServer, tp.Trim());

                    Project project = _workItemStore.Projects[teamProject.Name];

                    //CreateWorksheet_TP_Query2(_workItemStore, project, tfsQuery_Picker1.Query, tfsQuery_Picker1.Name, orientVertical);
                    //CreateWorksheet_TP_Query(_workItemStore, project, tfsQueryPicker1.Queries, orientVertical);
                }
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


        private void btnSearchForFiles_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                SearchForFiles();
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

        private void btnUnmergedChanges_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();

                MergeCandidate[] mergeCandidates = _versionControlServer.GetMergeCandidates("$/Development", "$/Release", RecursionType.Full);

                foreach (var mergeCandidate in mergeCandidates)
                {
                    if (mergeCandidate.Changeset.Owner == @"DOMAIN\ChuckNorris")
                    {
                        //This is an unmerged changeset commited by Chuck 
                    }
                }
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
                _tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(cbeTeamProjectCollections.SelectedText));

                if (_tfsTeamProjectCollection == null)
                {
                    MessageBox.Show("Cannot GetTeamProjectCollection");
                    return;
                }

                // Each time we change TeamProjectCollection, go get all the services we will use
                // and put in _<member> variables.

                Populate_TPC_Services(_tfsTeamProjectCollection);

                PopulateTeamProjects(_tfsTeamProjectCollection);
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
                _tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(cbeTeamProjectCollections.SelectedText));

                if (_tfsTeamProjectCollection == null)
                {
                    MessageBox.Show("Cannot GetTeamProjectCollection");
                    return;
                }

                // Each time we change TeamProjectCollection, go get all the services we will use
                // and put in _<member> variables.

                Populate_TPC_Services(_tfsTeamProjectCollection);

                PopulateTeamProjects(_tfsTeamProjectCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void teChangeSetID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            double changeSetID = 0;
            string changeSet = (string)Globals.ThisAddIn.Application.ActiveCell.Value.ToString();

            if (double.TryParse(changeSet, out changeSetID))
            {
                teChangeSetID.Text = int.Parse(changeSetID.ToString()).ToString();
            }
            else
            {
                MessageBox.Show("ChangeSetID not a number", "Error");
            }
        }

        private void tfsProvider_Picker1_ControlChanged()
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                PopulateTeamProjectCollections(wucTFSProvider_Picker.Uri);
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
        #endregion

        #region Main Function Routines

        #region CreateWorksheet_*

        private void CreateWorksheet_All_TP_AreaCheck(string areasToCheck, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}", "All_WIS_AreaCheck"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            foreach (Project project in _workItemStore.Projects)
            {
                //XlHlp.AddTitledInfo(insertAt.AddRow(), "Project Name", project.Name);

                insertAt = AddSection_WIS_TP_AreaCheck(insertAt, project, areasToCheck);
            }
        }

        private void CreateWorksheet_All_TP_Developers(bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}", "All_TP_Devs"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "All Developers All TeamProjects", _tfsTeamProjectCollection.Name);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "TFS Team Project");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Developer");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Changeset Count");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Earliest Date");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Latest Date");

            insertAt.IncrementRows();

            foreach (TeamProject teamProject in _versionControlServer.GetAllTeamProjects(refresh: true))
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddRow(), teamProject.Name);

                insertAt = AddSection_VCS_TP_Developers(insertAt, teamProject, true, teamProject.Name);
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tbl_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);
        }

        private void CreateWorksheet_ConfigurationServer_Info(TfsConfigurationServer configurationServer, bool showDetails, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateWorksheet_ConfigurationServerInfo(Start)");

            string sheetName = XlHlp.SafeSheetName("CS_" + configurationServer.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_ConfigurationServer_Info(insertAt, showDetails);

            //startingRow++;

            //// TODO: Decide if want to display something about a Catalog Node

            //// This only returned one row.

            ////ReadOnlyCollection<CatalogNode> childNodes =
            ////    configurationServer.CatalogNode.QueryChildren(null, false, CatalogQueryOptions.None);

            //ReadOnlyCollection<CatalogNode> childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.AnalysisDatabase }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "AnalysisDatabase", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ApplicationDatabase }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ApplicationDatabase", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.DataCollector }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "DataCollector", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.GenericLink }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "GenericLink", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.InfrastructureRoot }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "InfrastructureRoot", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.Machine }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "Machine", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.OrganizationalRoot }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "OrganizationalRoot", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProcessGuidanceSite }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ProcessGuidanceSite", childNodes);

            //childNodes =
            //    configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProjectCollection }, false, CatalogQueryOptions.None);

            //startingRow += AddSection_ChildNodes(insertAt, "ProjectCollection", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProjectCollectionDatabase }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ProjectCollectionDatabase", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProjectPortal }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ProjectPortal", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProjectServerMapping }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ProjectServerMapping", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ProjectServerRegistration }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ProjectServerRegistration", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ReportingConfiguration }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ReportingConfiguration", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ReportingFolder }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ReportingFolder", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ReportingServer }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ReportingServer", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.ResourceFolder }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "ResourceFolder", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.SharePointSiteCreationXlLocation }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "SharePointSiteCreationXlLocation", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.SharePointWebApplication }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "SharePointWebApplication", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.SqlAnalysisInstance }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "SqlAnalysisInstance", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.SqlDatabaseInstance }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "SqlDatabaseInstance", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.SqlReportingInstance }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "SqlReportingInstance", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamFoundationServerInstance }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TeamFoundationServerInstance", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamFoundationWebApplication }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TeamFoundationWebApplication", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TeamProject", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamSystemWebAccess }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TeamSystemWebAccess", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TestController }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TestController", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TestEnvironment }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "TestEnvironment", childNodes);

            //childNodes =
            //     configurationServer.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.WarehouseDatabase }, false, CatalogQueryOptions.None);

            //rngOutput = ws.Cells[startingRow, startingColumn];
            //startingRow += AddSection_ChildNodes(insertAt, "WarehouseDatabase", childNodes);

        }

        private void CreateWorksheet_ShelveSets(string tfsUri, string teamProjectCollection, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}_{1}", teamProjectCollection, "ShelveSets"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_VCS_ShelveSets(insertAt);
        }

        //private void CreateWorksheet_ConfigurationServerNodesInfo(ReadOnlyCollection<CatalogNode> childNodes, bool param1)
        //{
        //    long startTicks = Common.WriteToDebugWindow("CreateWorksheet_ConfigurationServerNodesInfo(Start)");

        //    foreach (CatalogNode childNode in childNodes)
        //    {
        //        CreateWorksheet_ConfigurationServerNodesInfo(childNodes, false);
        //    }

        //    string sheetName = ExcelHlp.SafeSheetName("CSN>" + childNode.N.Name);
        //    Worksheet ws = ExcelHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    // Output starts here.  Each display method returns the output end point.

        //    int startingRow = 2;

        //    startingRow += AddSection_ConfigurationServerInfo(ws, configurationServer, startingRow, showDetails);
        //    startingRow++;

        //    Common.WriteToDebugWindow("CreateWorksheet_ConfigurationServerNodesInfo(End)", startTicks);
        //}

        private void CreateWorksheet_TPC_Info(CatalogNode teamProjectCollectionNode, TfsTeamProjectCollection teamProjectCollection, bool showDetails, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName("TPC_" + GetTeamProjectCollectionName(teamProjectCollection));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_TPC_Info(insertAt, teamProjectCollection, showDetails);

            // Get a catalog of team projects for the collection

            ReadOnlyCollection<CatalogNode> teamProjects = teamProjectCollectionNode.QueryChildren(
                new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None);

            insertAt = AddSection_TP(insertAt, teamProjects);
        }

        private void CreateWorksheet_TPC_Members(string teamProjectCollectionName, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}_{1}", teamProjectCollectionName, "Members"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_TPC_Members(insertAt);
        }

        private void CreateWorksheet_TP_Query(WorkItemStore workItemStore, Project project, Dictionary<string, string> queries, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName("TPQ_" + project.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 10, orientVertical: orientVertical);

            foreach (string queryName in queries.Keys)
            {
                insertAt = AddSection_TP_Query(insertAt, project, queries[queryName], queryName);
            }
        }

        private void CreateWorksheet_TP_Query2(WorkItemStore workItemStore, Project project, string query, string queryName, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName("TPQ_" + project.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_TP_Query(insertAt, project, query, queryName);
        }

        private void CreateWorksheet_TP_Info(TeamProject teamProject, string sectionsToDisplay, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}({1})",
                System.Reflection.MethodInfo.GetCurrentMethod().Name, teamProject.Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}", "TP_", teamProject.ServerItem));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            Project project = _workItemStore.Projects[teamProject.Name];

            XlHlp.AddTitledInfo(ws, 2, 1, "TP Name", teamProject.Name, titleLocation: XlHlp.TitleLocation.Top);

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            //insertAt = AddSection_TP_Info(insertAt, teamProject);

            insertAt = AddSection_VCS(insertAt, teamProject, sectionsToDisplay);

            insertAt = AddSection_WIS(insertAt, project, sectionsToDisplay);
        }

        private void CreateWorksheet_TP_TemplateType(bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName("TPC_Templates");
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1);

            //TfsConfigurationServer tfsConfigServer = VNCTFS.Helper.Get_ConfigurationServer(tfsProviderPicker1.Uri);

            //ReadOnlyCollection<CatalogNode> teamProjectCollectionNodes = VNCTFS.Helper.Get_TeamProjectCollectionNodes(tfsConfigServer);

            //foreach (CatalogNode tpcNode in teamProjectCollectionNodes)
            //{
            //    TfsTeamProjectCollection tpc = VNCTFS.Helper.Get_TeamProjectCollection(tfsConfigServer, tpcNode);

            //    foreach (Project project in _workItemStore.Projects)
            //    {
            //        XlHlp.AddTitledInfo(rngOutput.Offset[XlLocation.Rows, 0], "TP Name", project.Name);

            //        Microsoft.TeamFoundation.WorkItemTracking.Client.CategoryCollection categories = _workItemStore.Projects[project.Name].Categories;

            //        foreach (Category category in categories)
            //        {
            //            string templateType = "FA Bug ???";

            //            switch (category.Name)
            //            {
            //                case "Requirement Category":
            //                    int col = 2;
            //                    XlHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], category.Name);
            //                    XlHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], category.DefaultWorkItemType.Name);

            //                    switch (category.DefaultWorkItemType.Name)
            //                    {
            //                        case "User Story":
            //                            templateType = "Agile";
            //                            break;

            //                        case "Requirement":
            //                            templateType = "CMMI";
            //                            break;

            //                        case "Product Backlog Item":
            //                            templateType = "Scrum";
            //                            break;

            //                        default:
            //                            break;
            //                    }

            //                    XlHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], templateType);
            //                    break;

            //                default:
            //                    int col1 = 5;
            //                    XlHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col1++], templateType);
            //                    break;
            //            }
            //        }

            //        XlLocation.Rows++;
            //    }
            //}
        }

        //private void CreateWorksheet_All_TP_WIS_Info(string sectionsToDisplay)
        //{
        //    XlHlp.DisplayInWatchWindow(string.Format("{0}",
        //        System.Reflection.MethodInfo.GetCurrentMethod().Name));

        //    string sheetName = XlHlp.SafeSheetName(string.Format("{0}", "All_WIS_TPs>"));
        //    Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

        //    int startingRow = 2;
        //    int startingColumn = 1;

        //    XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, 2, 1);

        //    foreach (Project project in _workItemStore.Projects)
        //    {
        //        XlHlp.AddTitledInfo(insertAt.AddRow(), "Project Name", project.Name);

        //        insertAt = AddSection_WIS_TP(insertAt, project, sectionsToDisplay);

        //        insertAt.IncrementRows();
        //    }
        //}

        private void CreateWorksheet_VCS_Branches(string uri, string teamProjectCollectionName, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                 System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}_{1}", teamProjectCollectionName, "Root Branches"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_VCS_RootBranches(insertAt);
        }

        private static void CreateWorkSheet_VCS_ChangeSetInfo(int changesetId, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}  ChangesetId: {1}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name, changesetId));

            Changeset changeSet = _versionControlServer.GetChangeset(changesetId);

            Display_VCS_Changeset_Info(changeSet);

            Display_VCS_ChangeSet_Changes(changeSet);

            Display_VCS_ChangeSet_AssociatedWorkItems(changeSet);

            Display_VCS_Changeset_WorkItems(changeSet);
        }

        private void CreateWorksheet_Workspaces(string tfsUri, string teamProjectCollection, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}_{1}", teamProjectCollection, "WorkSpaces"));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: orientVertical);

            insertAt = AddSection_VCS_Workspaces(insertAt);
        }

        #endregion

        #region AddSection_*

        private XlHlp.XlLocation AddSection_CatalogNode(XlHlp.XlLocation insertAt, CatalogNode catalogNode)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "FullPath:", catalogNode.FullPath);
            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "IsDefault:", catalogNode.IsDefault.ToString());

            insertAt.ColumnsAdded = 2;

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.TableEndColumn + 1);
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_ChildNodes(XlHlp.XlLocation insertAt, string NodeType, ReadOnlyCollection<CatalogNode> childNodes)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            //// List the team project collections

            XlHlp.AddContentToCell(insertAt.InsertRow(1), string.Format("{0}({1})", NodeType, childNodes.Count), 14, XlHlp.MakeBold.Yes);

            //currentRow = startingRow;
            //int innerRowsAdded = 0;
            //int col = 1;

            //foreach (CatalogNode child in childNodes)
            //{
            //    // Need to fix this so expands down the page
            //    innerRowsAdded = AddSection_CatalogNode(ws, rngOutput, child);
            //    insertAt = innerRowsAdded;
            //    currentRow += innerRowsAdded;
            //}

            //XlLocation.Rows++;

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_ConfigurationServer_Info(XlHlp.XlLocation insertAt, object showDetails)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt.MarkStart(XlHlp.MarkType.None);

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "Name:", _configurationServer.Name);
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "HasAuthenticated:", _configurationServer.HasAuthenticated.ToString());
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "IsHostedServer:", _configurationServer.IsHostedServer.ToString());
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "ClientCacheDirectoryForInstance:", _configurationServer.ClientCacheDirectoryForInstance);
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "ClientCacheDirectoryForUser:", _configurationServer.ClientCacheDirectoryForUser);

            insertAt.MarkEnd(XlHlp.MarkType.None);

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.MarkEndColumn + 1);
            }

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #region TeamProject (TP)

        private XlHlp.XlLocation AddSection_TP(XlHlp.XlLocation insertAt, ReadOnlyCollection<CatalogNode> teamProjects)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.AddContentToCell(insertAt.AddRow(), "Team Projects", 14, XlHlp.MakeBold.Yes);

            Worksheet ws = insertAt.workSheet;

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name", 12);

            insertAt.IncrementRows();

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            insertAt = DisplayListOf_TeamProjects(insertAt, teamProjects);

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblTP_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.TableEndColumn + 1);
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_TP_Info(XlHlp.XlLocation insertAt, TeamProject teamProject)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt.MarkStart();

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "TP Name", teamProject.Name);

            insertAt.MarkEnd();

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.MarkEndColumn + 1);
            }

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_TP_Query(XlHlp.XlLocation insertAt, Project project, string query, string queryName)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "Name", queryName);

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "Query", query);

            string parsedQuery = ParseQueryTokens(query, project);

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "ParsedQuery", parsedQuery);

            try
            {
                WorkItemCollection queryResults = _workItemStore.Query(parsedQuery);

                int count;

                if ((count = queryResults.Count) > 0)
                {
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Matches", count.ToString());

                    // Keep in same order as fields, infra.

                    insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Id");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Type");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Title");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CreatedDate");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CreatedBy");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ChangedDate");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ChangedBy");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "AuthorizedDate");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "State");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Tags");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "AreaPath");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "IterationPath");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "RelatedLinkCount");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ExternalLinkCount");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "HyperLinkCount");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Reason");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Rev");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Revision");
                    XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "RevisedDate");

                    insertAt.IncrementRows();

                    foreach (WorkItem workItem in queryResults)
                    {
                        insertAt.ClearOffsets();

                        // TODO(crhodes)
                        // Parse Query and dynamically determine columns to show - this will be way cool!

                        //[System.Id], [System.WorkItemType], [System.Title], [System.State], [System.Tags], 
                        //[Microsoft.VSTS.Scheduling.StoryPoints], [System.AreaPath], [System.IterationPath], 
                        //[System.RelatedLinkCount], [System.ExternalLinkCount], [System.HyperLinkCount] 

                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Id));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Type.Name));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Title));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.CreatedDate.ToString()));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.CreatedBy));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.ChangedDate.ToString()));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.ChangedBy));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.AuthorizedDate.ToString()));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.State));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Tags));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.AreaPath));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.IterationPath));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.RelatedLinkCount));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.ExternalLinkCount));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.HyperLinkCount));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Reason));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Rev));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.Revision));
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", workItem.RevisedDate.ToString()));

                        insertAt.IncrementRows();
                    }

                    insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblQuery_{0}", project.Name));

                    //WorkItem item = queryResults.Cast<WorkItem>().OrderByDescending(iii => iii.CreatedDate).First();
                    //lastCreateDate = ((WorkItem)item).CreatedDate.ToString();
                    //}

                    //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", wit.Name));
                    //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", count));
                    //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", lastCreateDate));
                    //currentRow++;
                    //XlLocation.Rows++;

                    insertAt.Group(insertAt.OrientVertical, hide: true);
                }
            }
            catch (ValidationException cex)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Matches", cex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.MarkEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #region TeamProjectCollection (TPC)

        private XlHlp.XlLocation AddSection_TPC_Info(XlHlp.XlLocation insertAt, TfsTeamProjectCollection tpc, bool showDetails)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "Name:", tpc.Name);

            insertAt = AddSection_WIS_Info(insertAt, tpc);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_TPC_Members(XlHlp.XlLocation insertAt)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            TeamFoundationIdentity everyone = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.EveryoneGroup, MembershipQuery.Direct, ReadIdentityOptions.None);
            TeamFoundationIdentity licensees = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.LicenseesGroup, MembershipQuery.Direct, ReadIdentityOptions.None);
            TeamFoundationIdentity namespaceAdministrators = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.NamespaceAdministratorsGroup, MembershipQuery.Direct, ReadIdentityOptions.None);
            TeamFoundationIdentity serviceUsers = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.ServiceUsersGroup, MembershipQuery.Direct, ReadIdentityOptions.None);

            if (everyone != null)
            {
                insertAt.ClearOffsets();

                XlHlp.AddTitledInfo(insertAt.AddRow(), "Everyone", everyone.Members.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), everyone.DisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), everyone.UniqueName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), everyone.Descriptor.IdentityType);

                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Everyone", "null");
            }

            if (licensees != null)
            {
                insertAt.ClearOffsets();

                XlHlp.AddTitledInfo(insertAt.AddRow(), "Licensees", licensees.Members.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), licensees.DisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), licensees.UniqueName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), licensees.Descriptor.IdentityType);

                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Licensees", "null");
            }

            if (namespaceAdministrators != null)
            {
                insertAt.ClearOffsets();

                XlHlp.AddTitledInfo(insertAt.AddRow(), "NamespaceAdministrators", namespaceAdministrators.Members.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), namespaceAdministrators.DisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), namespaceAdministrators.UniqueName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), namespaceAdministrators.Descriptor.IdentityType);

                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "NamespaceAdministrators", "null");
            }

            if (serviceUsers != null)
            {
                insertAt.ClearOffsets();

                XlHlp.AddTitledInfo(insertAt.AddRow(), "ServiceUsers", serviceUsers.Members.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), serviceUsers.DisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), serviceUsers.UniqueName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), serviceUsers.Descriptor.IdentityType);

                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ServiceUsers", "null");
            }

            TeamFoundationIdentity everyoneExpanded = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.EveryoneGroup, MembershipQuery.Expanded, ReadIdentityOptions.None);

            if (everyoneExpanded != null)
            {
                FetchIdentities(everyoneExpanded.Members);
            }

            TeamFoundationIdentity licenseesExpanded = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.LicenseesGroup, MembershipQuery.Expanded, ReadIdentityOptions.None);

            if (licenseesExpanded != null)
            {
                FetchIdentities(licenseesExpanded.Members);
            }

            TeamFoundationIdentity serviceUsersExpanded = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.ServiceUsersGroup, MembershipQuery.Expanded, ReadIdentityOptions.None);

            if (serviceUsersExpanded != null)
            {
                FetchIdentities(serviceUsersExpanded.Members);
            }

            TeamFoundationIdentity namespaceAdministratorsExpanded = _identityManagementService.ReadIdentity(GroupWellKnownDescriptors.NamespaceAdministratorsGroup, MembershipQuery.Expanded, ReadIdentityOptions.None);

            if (namespaceAdministratorsExpanded != null)
            {
                FetchIdentities(namespaceAdministratorsExpanded.Members);
            }

            XlHlp.AddTitledInfo(insertAt.AddRow(), "All Groups and Identities", "Lots");

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            // Group

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 50, "Identifier");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 50, "Identity");

            // Members

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "IsContainer");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 50, "DisplayName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 80, "UniqueName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "IdentityType");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "UniqueUserId");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "IsActive");

            insertAt.IncrementRows();

            foreach (TeamFoundationIdentity identity in _groups)
            {
                foreach (IdentityDescriptor member in identity.Members)
                {
                    insertAt.ClearOffsets();

                    // Group

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), identity.Descriptor.Identifier);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), identity.DisplayName);

                    // Members

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].IsContainer.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].DisplayName);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].UniqueName);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].Descriptor.IdentityType);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].UniqueUserId.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), _identities[member].IsActive.ToString());

                    insertAt.IncrementRows();
                }
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblMembers_{0}", insertAt.workSheet.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.MarkEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #region VersionControlServer (VCS)

        //private XlHlp.XlLocation AddBranches(ItemIdentifier[] items, XlHlp.XlLocation insertAt, int currentColumn)
        //{
        //    XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);
        //     XlLocation XlLocation = new XlLocation(0,0);


        //    foreach (var item in items)
        //    {
        //        ExcelHlp.DisplayInWatchWindow(string.Format("  Item.ChangeType:{0}",
        //            item.ChangeType.ToString()));

        //        switch (item.ChangeType)
        //        {
        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Add:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Branch:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Delete:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Edit:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Encoding:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Lock:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Merge:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.None:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Property:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Rename:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Rollback:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.SourceRename:

        //                break;

        //            case Microsoft.TeamFoundation.VersionControl.Client.ChangeType.Undelete:

        //                break;
        //        }
        //    }

        //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");
        //    return insertAt;
        //}

        private XlHlp.XlLocation AddSection_VCS(XlHlp.XlLocation insertAt, TeamProject teamProject, string sectionsToDisplay)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Version Control Server (VCS) Information", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Version Control Server (VCS) Information", 14, XlHlp.MakeBold.Yes, orientation: XlOrientation.xlUpward);
                insertAt.DecrementRows();   // AddRow bumped it.
                insertAt.IncrementColumns();
            }

            insertAt = AddSection_VCS_Info(insertAt);

            if (sectionsToDisplay.Contains("Affected Projects"))
            {
                insertAt = AddSection_VCS_TP_AffectedTeamProjects(insertAt, teamProject);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Branches"))
            {
                insertAt = AddSection_VCS_TP_Branches(insertAt, teamProject);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Changesets"))
            {
                insertAt = AddSection_VCS_TP_Changesets(insertAt, teamProject, (bool)cbListChangesetChanges.IsChecked, (bool)cbListChangesetWorkItems.IsChecked);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Developers"))
            {
                insertAt = AddSection_VCS_TP_Developers(insertAt, teamProject, false, null);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("ItemSets"))
            {
                //insertAt = AddSection_VCS_TeamProject_ItemSets(insertAt, teamProject);

                //insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("PendingSets"))
            {
                insertAt = AddSection_VCS_TP_PendingSets(insertAt, teamProject);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("ShelveSets"))
            {
                //insertAt = AddSection_VCS_TeamProject_ShelveSets(insertAt, teamProject);

                //insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Teams"))
            {
                insertAt = AddSection_VCS_TP_Teams(insertAt, teamProject, false);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_Branches(XlHlp.XlLocation insertAt, BranchObject branch)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            //XlHlp.DisplayInWatchWindow(string.Format("{0}{1} - {2}", "".PadLeft(currentColumn * 4), branch.Properties.RootItem.Item, branch.Properties.RootItem.ChangeType.ToString()));

            return insertAt;

            insertAt.ClearOffsets();

            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Item);
            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.DateCreated.ToString());

            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.ChangeType.ToString());
            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Version.DisplayString);
            //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.ToString());

            insertAt.IncrementRows();

            var childBranches = _versionControlServer.QueryBranchObjects(branch.Properties.RootItem, RecursionType.OneLevel);

            //currentColumn++;

            foreach (BranchObject childBranch in childBranches)
            {
                insertAt.ClearOffsets();

                if (childBranch.Properties.RootItem.Item == branch.Properties.RootItem.Item)
                {
                    continue;
                }

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), childBranch.Properties.RootItem.Item);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), childBranch.DateCreated.ToString());

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), childBranch.Properties.RootItem.ChangeType.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), childBranch.Properties.RootItem.Version.DisplayString);

                insertAt.IncrementRows();

                if (childBranch.ChildBranches.Count() > 0)
                {
                    insertAt = AddSection_VCS_Branches(insertAt, childBranch);
                }
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_Info(XlHlp.XlLocation insertAt)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            XlHlp.AddTitledInfo(insertAt.AddRow(2), "ServerGuid:", _versionControlServer.ServerGuid.ToString());
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "TeamProjectCollection:", _versionControlServer.TeamProjectCollection.Name);
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "WebServiceLevel:", _versionControlServer.WebServiceLevel.ToString());
            XlHlp.AddTitledInfo(insertAt.AddRow(2), "LatestChangeSetId:", _versionControlServer.GetLatestChangesetId().ToString());

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable);

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_RootBranches(XlHlp.XlLocation insertAt)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int count = 0;

            var rootBranches = _versionControlServer.QueryRootBranchObjects(RecursionType.None);
            count = rootBranches.Count();
            XlHlp.AddTitledInfo(insertAt.AddRow(), "QueryRootBranchObjects(None)", count.ToString(), 40);
            insertAt = DisplayListOf_Branches(insertAt, rootBranches, false, "None");

            insertAt.IncrementRows();

            rootBranches = _versionControlServer.QueryRootBranchObjects(RecursionType.OneLevel);
            count = rootBranches.Count();
            XlHlp.AddTitledInfo(insertAt.AddRow(), "QueryRootBranchObjects(One)", count.ToString(), 40);
            insertAt = DisplayListOf_Branches(insertAt, rootBranches, false, "OneLevel");

            insertAt.IncrementRows();

            rootBranches = _versionControlServer.QueryRootBranchObjects(RecursionType.Full);
            count = rootBranches.Count();
            XlHlp.AddTitledInfo(insertAt.AddRow(), "QueryRootBranchObjects(Full)", count.ToString(), 40);
            insertAt = DisplayListOf_Branches(insertAt, rootBranches, false, "Full");

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_ShelveSets(XlHlp.XlLocation insertAt)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            // QueryShelvesets(shelvesetName, shelvesetOwner)
            // QueryShelvesets(null, null) returns all shelveets for all owners.

            Shelveset[] shelvesets = _versionControlServer.QueryShelvesets(null, null);

            int count = shelvesets.Count();

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ShelveSets", count.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ShelveSets", count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "OwnerDisplayName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "OwnerName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CreationDate");
            //ExcelHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "DisplayName");
            //ExcelHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "QualifiedName");

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CheckinNote");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Comment");

            insertAt.IncrementRows();

            foreach (Shelveset item in shelvesets)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.OwnerDisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.OwnerName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.CreationDate.ToString());
                //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.DisplayName);
                //ExcelHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.QualifiedName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.CheckinNote.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Comment);

                insertAt.IncrementRows();
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblShelveSets_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_AffectedTeamProjects(XlHlp.XlLocation insertAt, TeamProject teamProject)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int count = 0;



            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AffectedTeamProjects", count.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AffectedTeamProjects", count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            XlHlp.AddContentToCell(insertAt.AddRow(), "Not Implemented Yet");

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblAffectedTP_{0}", teamProject.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_Branches(XlHlp.XlLocation insertAt, TeamProject teamProject)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int count = 0;

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Branches", count.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Branches", count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            var rootBranches = _versionControlServer.QueryRootBranchObjects(RecursionType.OneLevel);

            var rootBranches2 = _versionControlServer.QueryRootBranchObjects(RecursionType.None);

            var rootBranches3 = _versionControlServer.QueryBranchObjects(new ItemIdentifier(teamProject.ServerItem), RecursionType.OneLevel);

            var rootBranches4 = _versionControlServer.QueryBranchObjects(new ItemIdentifier(teamProject.ServerItem), RecursionType.None);


            //Array.ForEach(rootBranches, (bo) => DisplayAllBranches(bo, vcs, currentColumn));

            if (rootBranches.Count() > 0)
            {
                foreach (BranchObject branch in rootBranches)
                {
                    insertAt = AddSection_VCS_Branches(insertAt, branch);
                }
            }
            else
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "<None>");
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblBranches_{0}", teamProject.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_Changesets(XlHlp.XlLocation insertAt, TeamProject teamProject, bool listChanges, bool listWorkItems)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            //Following will get all Changesets in last goBackDays. 
            int goBackDays = int.Parse(teHistoryDays.Text);

            System.Collections.IEnumerable history =
                _versionControlServer.QueryHistory(
                    teamProject.ServerItem,
                    LatestVersionSpec.Instance,
                    0,
                    RecursionType.Full,
                    null,                       // Any User
                    new DateVersionSpec(DateTime.Now - TimeSpan.FromDays(goBackDays)),
                    LatestVersionSpec.Instance,
                    Int32.MaxValue,
                    true,                       // includeChanges
                    false);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Changesets", history.Cast<object>().ToList().Count().ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Changesets", history.Cast<object>().ToList().Count().ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "ChangesetId");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Committer");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Committer DisplayName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner DisplayName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CreationDate");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "CheckinNote");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Comment");
            //ExcelHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "AssociatedWorkItems");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Changes");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "WorkItems");

            insertAt.IncrementRows();

            foreach (Changeset changeset in history)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.ChangesetId.ToString());
                //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.CheckinNote.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.Committer);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.CommitterDisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.OwnerDisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.CreationDate.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.CheckinNote.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.Comment);
                //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.AssociatedWorkItems.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.Changes.Count().ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), changeset.WorkItems.Count().ToString());

                insertAt.IncrementRows();

                if (listChanges)
                {
                    insertAt.IncrementColumns();

                    foreach (Change change in changeset.Changes)
                    {
                        try
                        {
                            XlHlp.AddContentToCell(insertAt.AddRow(1), GetChangeInfo(change));
                            //XlHlp.AddContentToCell(insertAt.AddRow(), GetIterationInfo(workItem));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    insertAt.DecrementColumns();
                }

                if (listWorkItems)
                {
                    insertAt.IncrementColumns();

                    foreach (WorkItem workItem in changeset.WorkItems)
                    {
                        try
                        {
                            XlHlp.AddContentToCell(insertAt.AddRow(1), GetWorkItemInfo(workItem));
                            XlHlp.AddContentToCell(insertAt.AddRow(1), GetIterationInfo(workItem));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    insertAt.DecrementColumns();
                }

            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblChangesets_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_Developers(XlHlp.XlLocation insertAt, TeamProject teamProject, bool displayDataOnly, string teamProjectName)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int goBackDays = int.Parse(teHistoryDays.Text);

            SortedDictionary<string, int> developers = new SortedDictionary<string, int>();
            SortedDictionary<string, DateTime> developersLatestDate = new SortedDictionary<string, DateTime>();
            SortedDictionary<string, DateTime> developersEarliestDate = new SortedDictionary<string, DateTime>();

            //Following will get all Changesets in last goBackDays. 

            GetDevelopersWithChangesets(teamProject, goBackDays, developers, developersLatestDate, developersEarliestDate);

            int count = developers.Count;

            if (!displayDataOnly)
            {

                if (insertAt.OrientVertical)
                {
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Developers (Changesets)", count.ToString());
                }
                else
                {
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Developers (Changesets)", count.ToString(), orientation: XlOrientation.xlUpward);
                    insertAt.IncrementColumns();
                }
            };

            //// Keep in same order as fields, infra.

            insertAt.ClearOffsets();

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "TFS Team Project");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Developer");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Changeset Count");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Earliest Date");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Latest Date");

                insertAt.IncrementRows();
            }

            foreach (string developer in developers.Keys)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), teamProjectName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), developer);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), developers[developer].ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), developersEarliestDate[developer].ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), developersLatestDate[developer].ToString());

                insertAt.IncrementRows();
            }

            if (!displayDataOnly)
            {
                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblDevelopers_{0}", teamProject.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);
            }

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_ItemSets(XlHlp.XlLocation insertAt, TeamProject teamProject)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int count = 0;

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ItemSets", count.ToString());
                //XlHlp.AddContentToCell(insertAt.AddRow(), defaultTeam.Name);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ItemSets", count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            //rngOutput.Value = "List of ItemSets Goes Here";
            //XlLocation.Rows++;

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable);

            insertAt.Group(insertAt.OrientVertical, hide: true);

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.TableEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_PendingSets(XlHlp.XlLocation insertAt, TeamProject teamProject)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            //PendingSet[] pendingChanges = versionControlServer.GetPendingSets(new String[] { teamProject.ServerItem }, RecursionType.Full);
            //PendingSet[] pendingChanges = versionControlServer.GetPendingSets(null, RecursionType.Full);

            // TODO: Pass in parameters
            //PendingSet[] pendingSets = versionControlServer.QueryPendingSets(new[] { "$/" }, RecursionType.Full, "CHRDEV1I", "Christopher Rhodes");

            //PendingChange[] pendingChanges = pendingSets.First().PendingChanges;

            //count = pendingChanges.Count();

            //ExcelHlp.AddTitledInfo(rngOutput.Offset[XlLocation.Rows++, 0], "PendingSets", count.ToString());

            //foreach (PendingChange pendingChange in pendingChanges)
            //{
            //    int col = 1;

            //    ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], pendingChange.ChangeType.ToString());
            //    ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], pendingChange.FileName);
            //    ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], pendingChange.ServerItem);
            //    ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], pendingChange.CreationDate.ToString());
            //    //ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], pendingChange.Type.ToString());

            //    XlLocation.Rows++;
            //}

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_TP_Teams(XlHlp.XlLocation insertAt, TeamProject teamProject, bool displayDataOnly)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            ProjectInfo teamProjectInfo = _commonStructureService.GetProjectFromName(teamProject.Name);
            var tpUri = teamProjectInfo.Uri;

            TfsTeamService teamService = new TfsTeamService();

            teamService.Initialize(teamProject.TeamProjectCollection);

            var defaultTeam = teamService.GetDefaultTeam(tpUri, new List<String>());

            IEnumerable<TeamFoundationTeam> allTeams = teamService.QueryTeams(tpUri);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Teams", allTeams.Count().ToString());
                //XlHlp.AddContentToCell(insertAt.AddRow(), defaultTeam.Name);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Teams", allTeams.Count().ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            // Keep in same order as fields, infra.

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                // Team

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Team");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Description");

                // Members

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "DisplayName");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "UniqueName");

                insertAt.IncrementRows();
            }

            foreach (var team in allTeams.OrderBy(team => team.Name))
            {
                insertAt.ClearOffsets();

                TeamFoundationIdentity[] teamMembers = team.GetMembers(_versionControlServer.TeamProjectCollection, MembershipQuery.Expanded);

                foreach (var member in teamMembers.OrderBy(m => m.UniqueName))
                {
                    insertAt.ClearOffsets();

                    // Team 

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), team.Name);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), team.Description);

                    // Members

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), member.DisplayName);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), member.UniqueName);

                    insertAt.IncrementRows();
                }
            }

            if (!displayDataOnly)
            {
                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblTeams_{0}", teamProject.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);
            }

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.TableEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_VCS_Workspaces(XlHlp.XlLocation insertAt)
        {
            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            // QueryWorkspaces(workspaceName, workspaceOwner, computer)
            // QueryWorkspaces(null, null, null) returns all workspaces for all owners for all computers.

            Workspace[] workSpaces = _versionControlServer.QueryWorkspaces(null, null, null);

            int count = workSpaces.Count();

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString());
                //XlHlp.AddContentToCell(insertAt.AddRow(), defaultTeam.Name);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Workspaces", count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            // Keep in same order as fields, infra.

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Computer");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "OwnerDisplayName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "OwnerName");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "LastAccessDate");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Comment");

            insertAt.IncrementRows();

            foreach (Workspace workspace in workSpaces)
            {
                insertAt.ClearOffsets();

                // Keep in same order with headers, supra.

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.Computer);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.OwnerDisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.OwnerName);
                //ExcelHlp.AddContentToCell(rngOutput.Offset[currentRow, col++], workspace.DisambiguatedDisplayName);
                //ExcelHlp.AddContentToCell(rngOutput.Offset[currentRow, col++], workspace.DisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.LastAccessDate.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), workspace.Comment);
                //ExcelHlp.AddContentToCell(rngOutput.Offset[XlLocation.Rows, col++], workspace.QualifiedName);

                insertAt.IncrementRows();
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblWorkSpaces_{0}", ws.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #region WorkItemStore (WIS)

        private XlHlp.XlLocation AddSection_WIS(XlHlp.XlLocation insertAt, Project project, string sectionsToDisplay)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt = AddSection_WIS_Info(insertAt);
            //// Uncomment next line once Method above returns something
            //insertAt.IncrementRows();

            if (insertAt.OrientVertical)
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Work Item Store (WIS) Information", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Work Item Store (WIS) Information", 14, XlHlp.MakeBold.Yes, orientation: XlOrientation.xlUpward);
                insertAt.DecrementRows();   // AddRow bumped it.
                insertAt.IncrementColumns();
            }

            if (sectionsToDisplay.Contains("Areas"))
            {
                insertAt = AddSection_WIS_TP_Areas(insertAt, project);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Iterations"))
            {
                insertAt = AddSection_WIS_TP_Iterations(insertAt, project);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Stored Queries"))
            {
                insertAt = AddSection_WIS_TP_StoredQueries(insertAt, project);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Work Item Types"))
            {
                insertAt = AddSection_WIS_TP_WorkItemTypes(insertAt, project);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            // Put Work Item Categories last as it has odd output.  Too lazy to fix indent.
            if (sectionsToDisplay.Contains("Work Item Categories"))
            {
                insertAt = AddSection_WIS_TP_WorkItemCategories(insertAt, project);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_Info(XlHlp.XlLocation insertAt)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_Info(XlHlp.XlLocation insertAt, TfsTeamProjectCollection teamProjectCollection)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            // TODO: This needs work to avoid looking across all the projects to get the list of WorkItemTypes

            //foreach (WorkItemType wit in project.WorkItemTypes.Cast<WorkItemType>().OrderBy(nnn => nnn.Name))
            //{
            //    int count = 0;
            //    string lastCreateDate = "???";
            //    try
            //    {
            //        WorkItemCollection queryResults = workItemStore.Query(String.Format("Select [Id], [Created Date] From WorkItems Where [Work Item Type] = '{0}'", wit.Name));
            //        if ((count = queryResults.Count) > 0)
            //        {
            //            WorkItem item = queryResults.Cast<WorkItem>().OrderByDescending(iii => iii.CreatedDate).First();
            //            lastCreateDate = ((WorkItem)item).CreatedDate.ToString();
            //            System.Diagnostics.Debug.WriteLine(((WorkItem)item).CreatedDate);
            //        }
            //        //;
            //        //WorkItem item = queryResults.Cast<WorkItem>().OrderByDescending(iii => iii.CreatedDate).First();
            //        //System.Diagnostics.Debug.WriteLine(((WorkItem)item).CreatedDate);
            //        //foreach (var item in queryResults.Cast<WorkItem>().OrderByDescending(iii => iii.CreatedDate).First())
            //        //{
            //        //    System.Diagnostics.Debug.WriteLine(((WorkItem)item).CreatedDate);
            //        //}
            //        //count = workItemStore.QueryCount(String.Format("Select [Id] From WorkItems Where [Work Item Type] = '{0}'", wit.Name));

            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }

            //    ExcelHlp.AddContentToCell(rngOutput.Offset[currentRow, col2++], string.Format("{0}({1})[{2}]", wit.Name, count, lastCreateDate));
            //}

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_AreaCheck(XlHlp.XlLocation insertAt, Project project, string areasToCheck)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), project.Name);
            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), project.AreaRootNodes.Count.ToString());

            //XlHlp.AddTitledInfo(insertAt.AddRow(), "Areas", project.AreaRootNodes.Count.ToString());

            //insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            char[] splitChars = { ',' };

            if (project.AreaRootNodes.Count == 0)
            {
                //XlHlp.AddContentToCell(insertAt.AddRow(), "None");
            }
            else
            {
                foreach (string area in areasToCheck.Split(splitChars, StringSplitOptions.None))
                {
                    try
                    {
                        var result = project.AreaRootNodes[area];
                        XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), area);
                    }
                    catch (Exception)
                    {
                        // No such area.  Need to learn how to use .Contains()
                        //throw;
                    }
                }

            }

            insertAt.AddRow();

            //insertAt.MarkEnd(XlHlp.MarkType.GroupTable);

            //insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_Areas(XlHlp.XlLocation insertAt, Project project)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int areaNodesCount = project.AreaRootNodes.Count;
            int currentRows = insertAt.RowsAdded;

            // Save the location of the count so we can update later after have traversed all items.

            Range rngCount = insertAt.GetCurrentRange();

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Areas", areaNodesCount.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Areas", areaNodesCount.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            if (project.AreaRootNodes.Count == 0)
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "None");
            }
            else
            {
                insertAt = AddChildNodes(insertAt, project.AreaRootNodes);
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable);

            insertAt.Group(insertAt.OrientVertical);

            // Update 

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(rngCount, "Areas", (insertAt.RowsAdded - currentRows).ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(rngCount, "Areas", (insertAt.RowsAdded - currentRows).ToString(), orientation: XlOrientation.xlUpward);
            }

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.  Use Group end because we have indented
                insertAt.SetLocation(insertAt.RowStart(), insertAt.GroupEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_Iterations(XlHlp.XlLocation insertAt, Project project)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            int iterationNodes = project.IterationRootNodes.Count;
            int currentRows = insertAt.RowsAdded;

            // Save the location of the count so we can update later after have traversed all items.

            Range rngCount = insertAt.GetCurrentRange();

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Iterations", iterationNodes.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Iterations", iterationNodes.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            if (project.IterationRootNodes.Count == 0)
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "None");
            }
            else
            {
                insertAt = AddChildNodes(insertAt, project.IterationRootNodes);
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblIterations_{0}", project.Name));

            insertAt.Group(insertAt.OrientVertical);

            // Update counts

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(rngCount, "Iterations", (insertAt.RowsAdded - currentRows).ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(rngCount, "Iterations", (insertAt.RowsAdded - currentRows).ToString(), orientation: XlOrientation.xlUpward);
            }

            if (!insertAt.OrientVertical)
            {
                // Skip past the info just added.  Use GroupEnd because we have indented to show hierarchy.
                insertAt.SetLocation(insertAt.RowStart(), insertAt.GroupEndColumn + 1);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_StoredQueries(XlHlp.XlLocation insertAt, Project project)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "StoredQueries", "");
                //XlHlp.AddContentToCell(insertAt.AddRow(), defaultTeam.Name);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "StoredQueries", "", orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            insertAt = AddQueryNodes(insertAt, project.QueryHierarchy);

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblStoredQueries_{0}", project.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_WorkItemCategories(XlHlp.XlLocation insertAt, Project project)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Microsoft.TeamFoundation.WorkItemTracking.Client.CategoryCollection categories = _workItemStore.Projects[project.Name].Categories;

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "WorkItem Categories", categories.Count.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "WorkItem Categories", categories.Count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            foreach (Category category in categories.OrderBy(nnn => nnn.Name))
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), category.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), category.DefaultWorkItemType.Name);

                foreach (WorkItemType wit in category.WorkItemTypes.OrderBy(nnn => nnn.Name))
                {
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), wit.Name);
                }

                insertAt.IncrementRows();
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblWICategories_{0}", project.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_WIS_TP_WorkItemTypes(XlHlp.XlLocation insertAt, Project project)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "WorkItem Types", project.WorkItemTypes.Count.ToString());
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "WorkItem Types", project.WorkItemTypes.Count.ToString(), orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            foreach (WorkItemType wit in project.WorkItemTypes.Cast<WorkItemType>().OrderBy(nnn => nnn.Name))
            {
                insertAt.ClearOffsets();
                int count = 0;

                string lastCreateDate = "???";
                try
                {
                    string query = String.Format(
                        "Select [Id], [Created Date] From WorkItems Where [Work Item Type] = '{0}' and [System.TeamProject] = '{1}'",
                        wit.Name, project.Name);

                    WorkItemCollection queryResults = _workItemStore.Query(query);

                    if ((count = queryResults.Count) > 0)
                    {
                        WorkItem item = queryResults.Cast<WorkItem>().OrderByDescending(iii => iii.CreatedDate).First();
                        lastCreateDate = ((WorkItem)item).CreatedDate.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", wit.Name));
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", count));
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), string.Format("{0}", lastCreateDate));

                insertAt.IncrementRows();
            }

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable);

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #endregion

        #region DisplayListOf_*

        private static void Display_VCS_ChangeSet_AssociatedWorkItems(Changeset changeSet)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  AssociatedWorkItems.Count: {0}", changeSet.AssociatedWorkItems.Count()));

            foreach (AssociatedWorkItemInfo item in changeSet.AssociatedWorkItems)
            {
                XlHlp.DisplayInWatchWindow(string.Format("    ID: {0}  Title: {1}  AssignedTo: {2}  WorkItemType: {3}  State: {4}",
                    item.Id,
                    item.Title,
                    item.AssignedTo,
                    item.WorkItemType,
                    item.State));
            }
        }

        private static void Display_VCS_ChangeSet_Changes(Changeset changeSet)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  Changes.Count: {0}", changeSet.Changes.Count()));

            foreach (Change change in changeSet.Changes)
            {
                XlHlp.DisplayInWatchWindow(string.Format("    ChangeType: {0}  CheckinDate: {1}  IsBranch: {2}  ItemType: {3}",
                    change.ChangeType,
                    change.Item.CheckinDate,
                    change.Item.IsBranch,
                    change.Item.ItemType));

                XlHlp.DisplayInWatchWindow(string.Format("      ServerItem: {0}", change.Item.ServerItem));

                if (change.MergeSources != null)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("      MergeSources.Count: {0}", change.MergeSources.Count()));
                }
            }
        }

        private static void Display_VCS_Changeset_Info(Changeset changeSet)
        {

            XlHlp.DisplayInWatchWindow(string.Format(" Committer: {0}", changeSet.Committer));
            XlHlp.DisplayInWatchWindow(string.Format(" CreationDate: {0}", changeSet.CreationDate));
            XlHlp.DisplayInWatchWindow(string.Format(" Owner: {0}", changeSet.Owner));
            XlHlp.DisplayInWatchWindow(string.Format(" CheckinNote: {0}", changeSet.CheckinNote));
            XlHlp.DisplayInWatchWindow(string.Format(" Comment: {0}", changeSet.Comment));
        }

        private static void Display_VCS_Changeset_WorkItems(Changeset changeSet)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  WorkItems.Count: {0}", changeSet.WorkItems.Count()));

            foreach (WorkItem item in changeSet.WorkItems)
            {
                XlHlp.DisplayInWatchWindow(string.Format("    ID: {0}   Title: {1}", item.Id, item.Title));
                XlHlp.DisplayInWatchWindow(string.Format("    AuthorizedDate: {0}", item.AuthorizedDate));
                XlHlp.DisplayInWatchWindow(string.Format("    AreadId: {0}  AreaPath: {1}", item.AreaId, item.AreaPath));

                string iterationInfo = GetIterationInfo(item);

                XlHlp.DisplayInWatchWindow(string.Format("    {0}", iterationInfo));

                XlHlp.DisplayInWatchWindow(string.Format("    AttachedFileCount: {0}", item.AttachedFileCount));

                foreach (Attachment attachment in item.Attachments)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("       ID: {0}  Name: {1}", attachment.Id, attachment.Name));
                    XlHlp.DisplayInWatchWindow(string.Format("         CreationTime: {0}  AttachedTime: {0}", attachment.CreationTime, attachment.AttachedTime));
                    XlHlp.DisplayInWatchWindow(string.Format("         Comment: {0}", attachment.Comment));
                }

                XlHlp.DisplayInWatchWindow(string.Format("    CreatedBy: {0}  CreatedDate: {1}", item.CreatedBy, item.CreatedDate));
                XlHlp.DisplayInWatchWindow(string.Format("    ChangedBy: {0}  ChangedDate: {1}", item.ChangedBy, item.ChangedDate));
                XlHlp.DisplayInWatchWindow(string.Format("    Description: {0}", item.Description));


                XlHlp.DisplayInWatchWindow(string.Format("    Reason: {0}", item.Reason));

                XlHlp.DisplayInWatchWindow(string.Format("    Rev: {0}", item.Rev.ToString()));
                XlHlp.DisplayInWatchWindow(string.Format("    RevisedDate: {0}", item.RevisedDate));
                XlHlp.DisplayInWatchWindow(string.Format("    Revsion: {0}", item.Revision));

                //ExcelHlp.DisplayInWatchWindow(string.Format("    Revisons.Count: {0}", item.Revisions.Count));

                //foreach (Revision revsion in item.Revisions)
                //{
                //    ExcelHlp.DisplayInWatchWindow(string.Format("        Fields.Count: {0}", revsion.Fields.Count));
                //}

                XlHlp.DisplayInWatchWindow(string.Format("    State: {0}", item.State));
                XlHlp.DisplayInWatchWindow(string.Format("    Type: {0}", item.Type.Name));

                XlHlp.DisplayInWatchWindow(string.Format("    ExternalLinkCount: {0}  HyperLinkCount: {1}  RelatedLinkCount: {2}",
                    item.ExternalLinkCount, item.HyperLinkCount, item.RelatedLinkCount));

                XlHlp.DisplayInWatchWindow(string.Format("    Links.Count: {0}", item.Links.Count));

                foreach (Link link in item.Links)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("        ArtifactLinkType.Name: {0}", link.ArtifactLinkType.Name));
                    XlHlp.DisplayInWatchWindow(string.Format("        Comment: {0}", link.Comment));
                }

                XlHlp.DisplayInWatchWindow(string.Format("    WorkItemLinks.Count: {0}", item.WorkItemLinks.Count));

                foreach (WorkItemLink link in item.WorkItemLinks)
                {
                    XlHlp.DisplayInWatchWindow(string.Format("        AddedBy: {0}  AddedDate: {0}", link.AddedBy, link.AddedDate));
                    XlHlp.DisplayInWatchWindow(string.Format("        BaseType: {0}", link.BaseType));
                    XlHlp.DisplayInWatchWindow(string.Format("        ChangedDate: {0}", link.ChangedDate));
                    XlHlp.DisplayInWatchWindow(string.Format("        Comment: {0}", link.Comment));
                    XlHlp.DisplayInWatchWindow(string.Format("        LinkTypeEnd.Id: {0}", link.LinkTypeEnd.Id));
                    XlHlp.DisplayInWatchWindow(string.Format("        LinkTypeEnd.ImmutableName: {0}", link.LinkTypeEnd.ImmutableName));
                    XlHlp.DisplayInWatchWindow(string.Format("        LinkTypeEnd.IsForwardLink: {0}", link.LinkTypeEnd.IsForwardLink));
                    XlHlp.DisplayInWatchWindow(string.Format("        LinkTypeEnd.LinkType: {0}", link.LinkTypeEnd.LinkType));
                    XlHlp.DisplayInWatchWindow(string.Format("        SourceId: {0}", link.SourceId));
                    XlHlp.DisplayInWatchWindow(string.Format("        TargetId: {0}", link.TargetId));

                    WorkItem targetWorkItem = _workItemStore.GetWorkItem(link.TargetId);
                    XlHlp.DisplayInWatchWindow(string.Format("            ID: {0}  Title: {1}", targetWorkItem.Id, targetWorkItem.Title));
                    XlHlp.DisplayInWatchWindow(string.Format("            CreatedBy: {0}  CreatedDate: {1}", targetWorkItem.CreatedBy, targetWorkItem.CreatedDate));
                    XlHlp.DisplayInWatchWindow(string.Format("            Description: {0}", targetWorkItem.Description));
                    XlHlp.DisplayInWatchWindow(string.Format("            Reason: {0}", targetWorkItem.Reason));
                    XlHlp.DisplayInWatchWindow(string.Format("            Rev: {0}", targetWorkItem.Rev));
                    XlHlp.DisplayInWatchWindow(string.Format("            RevisedDate: {0}", targetWorkItem.RevisedDate));
                    XlHlp.DisplayInWatchWindow(string.Format("            State: {0}", targetWorkItem.State));
                    XlHlp.DisplayInWatchWindow(string.Format("            Type: {0}", targetWorkItem.Type));
                    XlHlp.DisplayInWatchWindow(string.Format("            AreaPath: {0}", targetWorkItem.AreaPath));
                    XlHlp.DisplayInWatchWindow(string.Format("            IterationPath: {0}", targetWorkItem.IterationPath));
                }
            }
        }

        private XlHlp.XlLocation DisplayListOf_Branches(XlHlp.XlLocation insertAt, BranchObject[] branches, bool displayDataOnly, string tableSuffix)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "RootItem.Item");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 30, "ParentBranch.Item");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "DateCreated");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner DisplayName");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "ChangeType");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Version");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Related Branches");

                insertAt.IncrementRows();
            }

            foreach (BranchObject branch in branches)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Item);

                string parentBranch = branch.Properties.ParentBranch != null ? branch.Properties.ParentBranch.Item : "<none>";

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parentBranch);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.DateCreated.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.OwnerDisplayName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.ChangeType.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.Properties.RootItem.Version.DisplayString);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), branch.RelatedBranches.Count().ToString());

                insertAt.IncrementRows();
            }

            if (!displayDataOnly)
            {
                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblRootBranches_{0}", tableSuffix));

                insertAt.Group(insertAt.OrientVertical, hide: true);
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_TeamProjects(XlHlp.XlLocation insertAt, ReadOnlyCollection<CatalogNode> projectNodes)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            // The columns in this method need to be kept in sync with CreateTeamProjectsInfo()

            foreach (CatalogNode projectNode in projectNodes.OrderBy(tp => tp.Resource.DisplayName))
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), projectNode.Resource.DisplayName);

                insertAt.IncrementRows();
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        #endregion

        #endregion

        #region Utility Routines

        private XlHlp.XlLocation AddChildNodes(XlHlp.XlLocation insertAt, NodeCollection childNodes)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt.UpdateOffsets();

            foreach (Node item in childNodes)
            {
                //insertAt.ClearOffsets();

                var nodeInfo = _commonStructureService.GetNode(item.Uri.ToString());

                if (item.IsAreaNode)
                {
                    XlHlp.AddContentToCell(insertAt.AddRow(), item.Name);

                    if (item.HasChildNodes)
                    {
                        insertAt.IncrementColumns();
                        insertAt = AddChildNodes(insertAt, item.ChildNodes);
                        insertAt.DecrementColumns();
                    }
                }

                if (item.IsIterationNode)
                {
                    string startdate = nodeInfo.StartDate.HasValue ? ((DateTime)nodeInfo.StartDate).ToShortDateString() : "<null>";
                    string finishdate = nodeInfo.FinishDate.HasValue ? ((DateTime)nodeInfo.FinishDate).ToShortDateString() : "<null>";

                    string days = "??";

                    if (nodeInfo.StartDate.HasValue)
                    {
                        days = ((DateTime)nodeInfo.FinishDate).Subtract((DateTime)nodeInfo.StartDate).TotalDays.ToString();
                    }

                    string iterationinfo = string.Format("{3,2} days ({1} to {2})  {0} (id: {4})",
                        item.Name,
                        //nodeinfo.structuretype,
                        startdate,
                        finishdate,
                        days,
                        item.Id);

                    XlHlp.AddContentToCell(insertAt.AddRow(), iterationinfo);

                    if (item.HasChildNodes)
                    {
                        insertAt.IncrementColumns();
                        insertAt = AddChildNodes(insertAt, item.ChildNodes);
                        insertAt.DecrementColumns();
                    }
                }
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private static XlHlp.XlLocation AddQueryNodes(XlHlp.XlLocation insertAt, QueryFolder queryFolder)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            insertAt.ClearOffsets();

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Name");
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Node Type");

            foreach (var item in queryFolder)
            {
                insertAt.ClearOffsets();

                if (item is QueryDefinition)
                {
                    insertAt.ColumnOffset = 0;

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Name);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "QueryDefinition");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), ((QueryDefinition)item).QueryType.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), ((QueryDefinition)item).QueryText);

                }

                if (item is QueryFolder)
                {
                    insertAt.ColumnOffset = 0;

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Name);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "QueryFolder");
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Id.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.IsPersonal.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.Path);

                    insertAt = AddQueryNodes(insertAt, (QueryFolder)item);
                }
            }

            return insertAt;
        }

        static void FetchIdentities(IdentityDescriptor[] descriptors)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            TeamFoundationIdentity[] identities;

            // If total membership exceeds batch size limit for Read, break it up
            int batchSizeLimit = 100000;

            if (descriptors.Length > batchSizeLimit)
            {
                int batchNum = 0;
                int remainder = descriptors.Length;
                IdentityDescriptor[] batchDescriptors = new IdentityDescriptor[batchSizeLimit];

                while (remainder > 0)
                {
                    int startAt = batchNum * batchSizeLimit;
                    int length = batchSizeLimit;
                    if (length > remainder)
                    {
                        length = remainder;
                        batchDescriptors = new IdentityDescriptor[length];
                    }

                    Array.Copy(descriptors, startAt, batchDescriptors, 0, length);
                    identities = _identityManagementService.ReadIdentities(batchDescriptors, MembershipQuery.Direct, ReadIdentityOptions.None);
                    SortIdentities(identities);
                    remainder -= length;
                }
            }
            else
            {
                identities = _identityManagementService.ReadIdentities(descriptors, MembershipQuery.Direct, ReadIdentityOptions.None);
                SortIdentities(identities);
            }
        }

        private string GetChangeInfo(Change change)
        {
            StringBuilder changeInfo = new StringBuilder();

            changeInfo.AppendFormat("ChangeType: {0}  CheckinDate: {1}  IsBranch: {2}  ItemType: {3}",
                    change.ChangeType,
                    change.Item.CheckinDate,
                    change.Item.IsBranch,
                    change.Item.ItemType);

            changeInfo.AppendFormat("  ServerItem: {0}", change.Item.ServerItem);

            if (change.MergeSources != null)
            {
                changeInfo.AppendFormat("  MergeSources.Count: {0}", change.MergeSources.Count());
            }

            return changeInfo.ToString();
        }

        private static void GetDevelopersWithChangesets(TeamProject teamProject, int goBackDays, SortedDictionary<string, int> developers, SortedDictionary<string, DateTime> developersLatestDate, SortedDictionary<string, DateTime> developersEarliestDate)
        {
            System.Collections.IEnumerable history =
                            _versionControlServer.QueryHistory(
                                teamProject.ServerItem,
                                LatestVersionSpec.Instance,
                                0,
                                RecursionType.Full,
                                null,
                                new DateVersionSpec(DateTime.Now - TimeSpan.FromDays(goBackDays)),
                                LatestVersionSpec.Instance,
                                Int32.MaxValue,
                                false,
                                false);

            // Go find everyone that has issued changes.

            foreach (Changeset changeset in history)
            {
                if (developers.ContainsKey(changeset.Owner))
                {
                    developers[changeset.Owner] += 1;

                    if (developersEarliestDate[changeset.Owner] > changeset.CreationDate)
                    {
                        developersEarliestDate[changeset.Owner] = changeset.CreationDate;
                    }

                    if (developersLatestDate[changeset.Owner] < changeset.CreationDate)
                    {
                        developersLatestDate[changeset.Owner] = changeset.CreationDate;
                    }
                }
                else
                {
                    developers.Add(changeset.Owner, 1);
                    developersEarliestDate.Add(changeset.Owner, changeset.CreationDate);
                    developersLatestDate.Add(changeset.Owner, changeset.CreationDate);
                }
            }
        }

        private bool GetDisplayOrientation()
        {
            return (bool)cbOrientOutputVertically.IsChecked;
        }

        private static string GetIterationInfo(WorkItem workItem)
        {
            // TODO(crhodes):
            //	On some planet this must make sense :)

            var projectNameIndex = workItem.IterationPath.IndexOf("\\", 2);

            var iterationPath = "";

            if (projectNameIndex > 0)
            {
                iterationPath = workItem.IterationPath.Insert(projectNameIndex, "\\Iteration");
            }
            else
            {
                // Directly under Team Project
                iterationPath = string.Format("{0}\\Iteration", workItem.IterationPath);
            }

            // ProjectName\\Iteration\\<Iteration Path>>

            Uri itemUri = workItem.Uri;

            NodeInfo iteration = null;

            //try
            //{
            //    //NodeInfo iteration2 = _commonStructureService.GetNode(item.Uri.ToString());
            //    NodeInfo iteration3 = _commonStructureService.GetNode(item.Uri.AbsoluteUri);
            //}
            //catch (Exception ex)
            //{
            //    string result = ex.ToString();
            //}

            try
            {
                //iteration = _commonStructureService.GetNodeFromPath("VNC\\Iteration\\Release 1\\Sprint 1");
                iteration = _commonStructureService.GetNodeFromPath(iterationPath);
                //iteration = _commonStructureService.GetNodeFromPath("\\WorkItemTracking\\WorkItem\\214");
                //NodeInfo iteration = _commonStructureService.GetNodeFromPath(item.Uri.AbsolutePath);
                //NodeInfo iteration = _commonStructureService.GetNodeFromPath(string.Format("\\{0}", item.IterationPath));
            }
            catch (Exception ex)
            {
                string result = ex.ToString();
            }

            string startDate = iteration.StartDate.HasValue ? ((DateTime)iteration.StartDate).ToShortDateString() : "<null>";
            string finishDate = iteration.FinishDate.HasValue ? ((DateTime)iteration.FinishDate).ToShortDateString() : "<null>";

            string iterationInfo = string.Format("{1} to {2}  IterationId:{0}  IterationPath:{3}",
                workItem.IterationId, startDate, finishDate, workItem.IterationPath);

            return iterationInfo;
        }

        private static string GetTeamProjectCollectionName(TfsTeamProjectCollection teamProjectCollection)
        {
            string colName = teamProjectCollection.Name;
            // TFS in cloud send names back with back slashes
            colName = colName.Replace('\\', '/');
            // strip all all but TeamProjectCollection name part
            colName = colName.Substring(colName.LastIndexOf('/') + 1);

            return colName;
        }

        private string GetWorkItemInfo(WorkItem workItem)
        {
            StringBuilder workItemInfo = new StringBuilder();

            workItemInfo.AppendFormat("ID: {0}  Title: {1}  AreaPath: {2}  IterationPath: {3}",
                workItem.Id,
                workItem.Title,
                workItem.AreaPath, workItem.IterationPath);

            return workItemInfo.ToString();
        }

        private string ParseQueryTokens(string tokenizedQuery, Project project)
        {
            string query = tokenizedQuery.Replace("@project", String.Format("{0}", project.Name));

            return query;
        }

        private void Populate_TPC_Services(TfsTeamProjectCollection tpc)
        {
            _identityManagementService = null;
            _workItemStore = null;
            _versionControlServer = null;

            _commonStructureService = tpc.GetService<ICommonStructureService>();
            _identityManagementService = tpc.GetService<IIdentityManagementService>();
            _workItemStore = new WorkItemStore(tpc);
            _versionControlServer = VNCTFS.Helper.Get_VersionControlServer(tpc);
        }

        private void PopulateTeamProjectCollections(string tfsServerUri)
        {
            // TODO (crhodes): Update List of Team Projects

            _configurationServer = VNCTFS.Helper.Get_ConfigurationServer(tfsServerUri);

            // Get the Team Project Collections

            ReadOnlyCollection<CatalogNode> projectCollectionNodes = VNCTFS.Helper.Get_TeamProjectCollectionNodes(_configurationServer);

            // Populate

            DevExpress.Xpf.Editors.ListItemCollection itemCol = cbeTeamProjectCollections.Items;
            //ComboBoxItemCollection itemCol = cbeTeamProjectCollections.Properties.Items;

     
            itemCol.BeginUpdate();

            itemCol.Clear();

            foreach (CatalogNode teamProjectCollectionNode in projectCollectionNodes)
            {
                TfsTeamProjectCollection teamProjectCollection = VNCTFS.Helper.Get_TeamProjectCollection(_configurationServer, teamProjectCollectionNode);

                // TODO (crhodes): Maybe a class that contains a friendly name and a URI so populating Team Projects is easier.

                itemCol.Add(teamProjectCollection.Uri);
                //itemCol.Add(GetTeamProjectCollectionName(teamProjectCollection));
            }

            cbeTeamProjectCollections.SelectedIndex = -1;
            itemCol.EndUpdate();
        }

        private void PopulateTeamProjects(TfsTeamProjectCollection tpc)
        {
            var projectList = (from Project prj in _workItemStore.Projects select prj.Name).ToList();

            DevExpress.Xpf.Editors.ListItemCollection itemCol = cbeTeamProjects.Items;
            //CheckedListBoxItemCollection itemCol = cbeTeamProjects.Properties.Items;

            itemCol.BeginUpdate();
            itemCol.Clear();

            foreach (var item in projectList)
            {
                itemCol.Add(item);
            }

            itemCol.EndUpdate();
        }

        private XlHlp.XlLocation SearchForFiles()
        {
            string sheetName = XlHlp.SafeSheetName("Files");
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            int startingRow = 2;
            int startingColumn = 1;

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, startingRow, startingColumn);

            // List all of the .sln files.
            string searchPattern = teFilePattern.Text;

            ItemSet items = _versionControlServer.GetItems(searchPattern, RecursionType.Full);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "SearchPattern", searchPattern);
            XlHlp.AddTitledInfo(insertAt.AddRow(), "Count", items.Items.Count().ToString());

            foreach (Item item in items.Items)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.ItemType.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), item.ServerItem.ToString());

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private static void SortIdentities(TeamFoundationIdentity[] identities)
        {
            foreach (TeamFoundationIdentity identity in identities)
            {
                _identities[identity.Descriptor] = identity;

                if (identity.IsContainer)
                {
                    _groups.Add(identity);
                }
            }
        }

        private bool ValidUISelections()
        {
            if (cbeTeamProjectCollections.SelectedText.Length > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Must Select Team Project Collection first", "UI Selection Incomplete");
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void LoadControlContents()
        {
            try
            {
                wucTFSProvider_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
                //tfsQuery_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

    }
}
