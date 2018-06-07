using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DevExpress.Data.Filtering;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using DevExpress.Xpf.Core.Commands;
//using DevExpress.Xpf.DemoBase;
using DevExpress.Utils;

using VNC;
using EyeOnLife.User_Interface.Windows;

using System.Collections;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Instances : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        private GridColumn currentColumn = null;

        #region Constructors
        
        public Instances()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Initialization

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            EyeOnLife.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //((CollectionViewSource)this.Resources["instancesViewSource"]).Source =
                    //    EyeOnLife.Common.ApplicationDataSet.Instances;

                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.Instances;

                    ((CollectionViewSource)this.Resources["adDomains"]).Source = 
                        Common.ApplicationDataSet.LKUP_ADDomains;

                    ((CollectionViewSource)this.Resources["environments"]).Source = 
                        Common.ApplicationDataSet.LKUP_Environments;

                    ((CollectionViewSource)this.Resources["securityZones"]).Source = 
                        Common.ApplicationDataSet.LKUP_SecurityZones;
                }

                ApplyDataFilters();

                ViewMode.ApplyAuthorization(this);

                // This list needs to match the CheckBox names in Display_StylesAndTemplates.xml
                string[] ckDisplayColumns = { "ckDisplayEnvironmentColumns", "ckDisplayNotesColumns", "ckDisplaySizeColumns" };

                ViewMode.UpdateDisplayColumnsCheckBoxes(cc_DisplayOptions, ckDisplayColumns);

                LogUsage(this.GetType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }
        
        #endregion

        #region Event Handlers

        private void ck_EnableUpdates_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ck_EnableUpdates_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetSnapShotErrorColumns(e);
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        private void gc_ExpandContent_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnAddNewInstance(object sender, RoutedEventArgs e)
        {

        }

        private void OnCheckAllRows(object sender, ItemClickEventArgs e)
        {

            if (currentColumn != null)
            {
                System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick2: {0} - {1} {2}", currentColumn.Name, currentColumn.FieldName, e.Item.Name);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick2: currentColumn is null");
                return;
            }

            bool isChecked = (e.Item.Name == "bi_CheckAll" ? true : false) ;

            // TODO(crhodes): 
            //      This might be cleaner with  the loop inside of the switch.
            //      Need to determine how to pass column to a routine with a loop.

            foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
            {
                switch (currentColumn.FieldName)
                {
                    case "ExpandContent":
                        instance.ExpandContent = isChecked;
                        break;

                    case "ExpandJobServer":
                        instance.ExpandJobServer = isChecked;
                        break;

                    case "ExpandLinkedServers":
                        instance.ExpandLinkedServers = isChecked;
                        break;

                    case "ExpandLogins":
                        instance.ExpandLogins = isChecked;
                        break;

                    case "ExpandServerRoles":
                        instance.ExpandServerRoles = isChecked;
                        break;

                    case "ExpandStorage":
                        instance.ExpandStorage = isChecked;
                        break;

                    case "ExpandTriggers":
                        instance.ExpandTriggers = isChecked;
                        break;

                    default:
                        System.Diagnostics.Debug.WriteLine("Ignoring {0}", currentColumn.FieldName);
                        break;

                }
            }

            ahg_Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void OnColumnHeaderRightClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var foo = (BarCheckItem)sender;
            var bar = (BarCheckItem)e.Item;

            if (bar.Parent != null)
            {
                var imgr = bar.Parent;
                //var own = imgr.Owner;
            }
            var prn = bar.Parent;

            // BarManager changed in 14.2.  No longer has a .Manager property
            if (BarManager.GetBarManager(e.Item) != null)
                //if (bar.Manager != null)
                {
                var mgr = BarManager.GetBarManager(e.Item);

                var tv = (TableView)mgr.Parent;

                if (currentColumn != null)
                {
                    System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick: {0} - {1}", currentColumn.Name, currentColumn.FieldName);
                }
                else
                {
                    return;
                }

                var isChecked = (bool)bar.IsChecked;

                foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
                {

                    switch (currentColumn.FieldName)
                    {
                        case "ExpandContent":
                            instance.ExpandContent = isChecked;
                            break;

                        case "ExpandJobServer":
                            instance.ExpandJobServer = isChecked;
                            break;

                        case "ExpandLinkedServers":
                            instance.ExpandLinkedServers = isChecked;
                            break;

                        case "ExpandLogins":
                            instance.ExpandLogins = isChecked;
                            break;

                        case "ExpandServerRoles":
                            instance.ExpandServerRoles = isChecked;
                            break;

                        case "ExpandStorage":
                            instance.ExpandStorage = isChecked;
                            break;

                        case "ExpandTriggers":
                            instance.ExpandTriggers = isChecked;
                            break;

                        default:
                            System.Diagnostics.Debug.WriteLine("Ignoring {0}", currentColumn.FieldName);
                            break;

                    }
                }
            }
        }

        private void OnCreateSMOLogon(object sender, ItemClickEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            try
            {
                string message;

                if (!SQLInformation.SMO.Actions.Instance.CreateSQLMonitorLogin(instance.Name_Instance, out message))
                {
                	MessageBox.Show(string.Format("CreateSQLMonitorLogin Failed: {0}", message));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnFocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                seriesInstance_SpaceAvailable.DataSource = null;
                seriesInstance_Size.DataSource = null;
                seriesInstance_IndexSpace.DataSource = null;
                seriesInstance_DataSpace.DataSource = null;

                return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.InstancesRow)((DataRowView)row).Row;
            var instID = row2.ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.InstanceInfo
                .Where(inst => inst.Instance_ID == instID)
                .Select(i => new
                {
                    SnapShotDate = i.SnapShotDate,
                    IndexSpaceUsage = i.Total_IndexSpaceUsage / 1024.0,
                    DataSpaceUsage = i.Total_DataSpaceUsage / 1024.0,
                    SpaceAvailable = i.Total_SpaceAvailable / 1024.0,
                    Size = i.Total_Size
                });

            try
            {
                System.Diagnostics.Debug.WriteLine(((XYDiagram2D)cht_Instance.Diagram).Panes.Count);
                seriesInstance_SpaceAvailable.DataSource = chartSource;
                seriesInstance_Size.DataSource = chartSource;
                seriesInstance_IndexSpace.DataSource = chartSource;
                seriesInstance_DataSpace.DataSource = chartSource;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnGetHelpOnInstanceExpandTemplate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DetailsPane_Styles");
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnItemClick_Cmd2(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void OnNotesChanged(object sender, KeyEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnCreateSMOLogin(object sender, RoutedEventArgs e)
        {
            var commandWindow = new wndDX_CommandOne(tableView.SelectedRows);
            commandWindow.ShowDialog();
        }

        private void OnDeleteAllRows(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you really, really sure you want to delete everything?", "Confirm Action", MessageBoxButton.YesNo))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Seriously, this is completly starting over.  Does, Rhodes think this is a good idea.  Did Berke ask you to do this??", "Confirm Craziness", MessageBoxButton.YesNo))
                {
                    Common.ApplicationDataSet.Instances.Clear();
                    Common.ApplicationDataSet.Instances_Update();
                    //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                }
            }
        }

        private void OnDeleteRow(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete the selected instances?  This will cause a cascading delete of all related information", "Confirm Action", MessageBoxButton.YesNo))
            {
                try
                {
                    DataRow[] rowsToDelete = new DataRow[tableView.SelectedRows.Count];

                    for (int i = 0; i < tableView.SelectedRows.Count; i++)
                    {
                        rowsToDelete[i] = ((DataRowView)tableView.SelectedRows[i]).Row;
                    }

                    foreach (DataRow row in rowsToDelete)
                    {
                        row.Delete();
                    }

                    Common.ApplicationDataSet.Instances_Update();
                    //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);

                    // Need to reload the DataSet as all the work was done in the database.
                    // by the cascading delete triggers.

                    Common.ApplicationDataSet.LoadApplicationDataSetFromDB(Common.ApplicationDataSet);                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void OnShowGridMenu(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            var foo = (TableView)sender;
            var bar = (TableView)e.Source;

            var mnuiType = e.MenuInfo;

            switch (e.MenuInfo.MenuType)
            {
                case GridMenuType.Column:
                    var mnui = (GridColumnMenuInfo)e.MenuInfo;
                    currentColumn = mnui.Column;

            	    GridColumn column = mnui.Column;
                    System.Diagnostics.Debug.WriteLine("OnShowGridMenu: {0} - {1}", column.Name, column.FieldName);

                    switch (currentColumn.FieldName)
                    {
                        case "ExpandContent":
                        case "ExpandJobServer":
                        case "ExpandLinkedServers":
                        case "ExpandLogins":
                        case "ExpandServerRoles":
                        case "ExpandStorage":
                        case "ExpandTriggers":
                            bi_CheckAll.IsVisible = true;
                            bi_UnCheckAll.IsVisible = true;
                            break;

                        default:
                            bi_CheckAll.IsVisible = false;
                            bi_UnCheckAll.IsVisible = false;
                            break;

                    } 
      
                    break;

                case GridMenuType.FixedTotalSummary:
                case GridMenuType.GroupPanel:
                case GridMenuType.RowCell:
                case GridMenuType.TotalSummary:
                    return;
                    break;
            }

        }

        private void OnTelnetToInstance(object sender, ItemClickEventArgs e)
        {
            //var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            int portNumber = 0;

            if (instance.Port != null)
            {
                int.TryParse(instance.Port.ToString(), out portNumber);

                if (portNumber > 0)
                {
                    var commandWindow = new wndDX_CommandTelnet(instance.Name_Instance, instance.NetName, instance.Port);
                    commandWindow.ShowDialog();                    
                }
                else
                {
                    MessageBox.Show("Port number must be greater than 0");
                }
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnUpdateInfo(object sender, ItemClickEventArgs e)
        {
            var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            // Check to see if have linked to a server yet.  If not, do that.

            try
            {
                Guid serverID;

                Guid.TryParse(instance.Server_ID.ToString(), out serverID);
            }
            catch (Exception)
            {
                SyncServerInfoAndInstanceInfo(instance);
            }

            FetchSMOInfo(instance);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances_Update();
            //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);

            ahg_Save.Visibility = System.Windows.Visibility.Hidden;
            ahg_Save.Expanded = true;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances.RejectChanges();

            ahg_Save.Visibility = System.Windows.Visibility.Hidden;
            ahg_Save.Expanded = false;
        }

        private void btnSyncInstanceToServer_Click(object sender, RoutedEventArgs e)
        {
            foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
            {
                SyncServerInfoAndInstanceInfo(instance);
            }

            Common.ApplicationDataSet.Instances_Update();
            //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        }

        #endregion

        #region Main Function Routines

        /// <summary>
        /// Filter the data that is being displayed.
        /// </summary>
        private void ApplyDataFilters()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("[IsMonitored] = true");
            dataGrid.FilterCriteria = filter;
            //dataGrid.ActiveFilterEnabled = true;

            //dataGrid.FilterString = "[Crawled] = 'True'";
        }

        private void SyncServerInfoAndInstanceInfo(SQLInformation.Data.ApplicationDataSet.InstancesRow instance)
        {
            var servers = from s in Common.ApplicationDataSet.Servers
                          where s.NetName.ToUpper() == instance.NetName.ToUpper()
                          select s;

            try
            {
                SQLInformation.Data.ApplicationDataSet.ServersRow server = servers.First();

                instance.Server_ID = server.ID;

                // TODO: Verify these are set before trying to SYNC.

                try
                {
                    server.ADDomain = instance.ADDomain;
                    server.Environment = instance.Environment;
                    server.SecurityZone = instance.SecurityZone;

                    server.OSVersion = instance.OSVersion;
                    server.PhysicalMemory = instance.PhysicalMemory;
                    server.Platform = instance.Platform;
                    server.Processors = instance.Processors;
                }
                catch (Exception)
                {

                }

                server.EndEdit();
            }
            catch (Exception)
            {
                // No matching server.
                MessageBox.Show("No matching server found.  Adding new server.");

                int instanceExpandMask = 127;

                try
                {
                    SQLInformation.Data.ApplicationDataSet.ServersRow newServer = Common.ApplicationDataSet.Servers.NewServersRow();

                    newServer.ID = Guid.NewGuid();
                    newServer.NetName = instance.NetName.ToUpper();
                    newServer.DefaultInstanceExpandMask = instanceExpandMask;

                    // Wrap in exception in case not populated

                    try
                    {
                        newServer.OSVersion = instance.OSVersion;
                        newServer.PhysicalMemory = instance.PhysicalMemory;
                        newServer.Platform = instance.Platform;
                        newServer.Processors = instance.Processors;
                    }
                    catch (Exception)
                    {

                    }

                    Common.ApplicationDataSet.Servers.AddServersRow(newServer);
                    Common.ApplicationDataSet.Servers_Update();
                    //Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);

                    instance.Server_ID = newServer.ID;
                    Common.ApplicationDataSet.Instances_Update();
                    //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);

                }
                catch (Exception ex)
                {
                    VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }

            }
        }

        private void FetchSMOInfo(SQLInformation.Data.ApplicationDataSet.InstancesRow instanceRow)
        {
            SQLInformation.ExpandMask.InstanceExpandSetting instanceExpandSetting = new SQLInformation.ExpandMask.InstanceExpandSetting(instanceRow.Server_ID);
            SQLInformation.ExpandMask.JobServerExpandSetting jobServerExpandSetting = new SQLInformation.ExpandMask.JobServerExpandSetting(instanceRow.DefaultJobServerExpandMask);
            SQLInformation.ExpandMask.DatabaseExpandSetting databaseExpandSetting = new SQLInformation.ExpandMask.DatabaseExpandSetting(instanceRow.DefaultDatabaseExpandMask);

            SQLInformation.SMO.Instance.GetInfoFromSMO(instanceRow, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
        }

        #endregion

        double calculatedTotalDataSpaceUsage;
        double calculatedTotalIndexSpaceUsage;
        double calculatedTotalSize;
        double calculatedTotalSpaceAvailable;

        private void OnCustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            const double GB = 1024 * 1024 * 1024;
            const double MB = 1024 * 1024;
            const double KB = 1024;

            if (((GridSummaryItem)e.Item).FieldName == "Total_DataSpaceUsage")
            {
                if (e.IsTotalSummary)
                {
                    //double calculatedValue = 0;

                    switch (e.SummaryProcess)
                    {
                        case DevExpress.Data.CustomSummaryProcess.Start:
                            calculatedTotalDataSpaceUsage = 0;
                            break;

                        case DevExpress.Data.CustomSummaryProcess.Calculate:
                            double? foo = 0;
                            try
                            {
                                foo = (double?)e.FieldValue;
                                if (foo.HasValue)
                                {
                                    calculatedTotalDataSpaceUsage += (double)e.FieldValue / MB;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            break;

                        case DevExpress.Data.CustomSummaryProcess.Finalize:
                            e.TotalValue = calculatedTotalDataSpaceUsage;
                            break;
                    }
                }
            }

            if (((GridSummaryItem)e.Item).FieldName == "Total_IndexSpaceUsage")
            {
                if (e.IsTotalSummary)
                {
                    //double calculatedValue = 0;

                    switch (e.SummaryProcess)
                    {
                        case DevExpress.Data.CustomSummaryProcess.Start:
                            calculatedTotalIndexSpaceUsage = 0;
                            break;

                        case DevExpress.Data.CustomSummaryProcess.Calculate:
                            double? foo = 0;
                            try
                            {
                                foo = (double?)e.FieldValue;
                                if (foo.HasValue)
                                {
                                    calculatedTotalIndexSpaceUsage += (double)e.FieldValue / MB;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            break;

                        case DevExpress.Data.CustomSummaryProcess.Finalize:
                            e.TotalValue = calculatedTotalIndexSpaceUsage;
                            break;
                    }
                }
            }

            if (((GridSummaryItem)e.Item).FieldName == "Total_Size")
            {
                if (e.IsTotalSummary)
                {
                    //double calculatedValue = 0;

                    switch (e.SummaryProcess)
                    {
                        case DevExpress.Data.CustomSummaryProcess.Start:
                            calculatedTotalSize = 0;
                            break;

                        case DevExpress.Data.CustomSummaryProcess.Calculate:
                            double? foo = 0;
                            try
                            {
                                foo = (double?)e.FieldValue;
                                if (foo.HasValue)
                                {
                                    calculatedTotalSize += (double)e.FieldValue / KB;
                                }
                            }
                            catch (Exception ex)
                            {
                                
                            }

                            break;

                        case DevExpress.Data.CustomSummaryProcess.Finalize:
                            e.TotalValue = calculatedTotalSize;
                            break;
                    }
                }
            }

            if (((GridSummaryItem)e.Item).FieldName == "Total_SpaceAvailable")
            {
                if (e.IsTotalSummary)
                {
                    //double calculatedValue = 0;

                    switch (e.SummaryProcess)
                    {
                        case DevExpress.Data.CustomSummaryProcess.Start:
                            calculatedTotalSpaceAvailable = 0;
                            break;

                        case DevExpress.Data.CustomSummaryProcess.Calculate:
                            double? foo = 0;
                            try
                            {
                                foo = (double?)e.FieldValue;
                                if (foo.HasValue)
                                {
                                    calculatedTotalSpaceAvailable += (double)e.FieldValue / MB;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            break;

                        case DevExpress.Data.CustomSummaryProcess.Finalize:
                            e.TotalValue = calculatedTotalSpaceAvailable;
                            break;
                    }
                }
            }
        }

    }

    public class RowDetailContainerControl : ContentControl
    {
        public RowDetailContainerControl()
        {
            this.SetDefaultStyleKey(typeof(RowDetailContainerControl));
        }
    }

}
