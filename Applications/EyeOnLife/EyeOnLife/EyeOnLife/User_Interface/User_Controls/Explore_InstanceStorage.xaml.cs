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

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;
using VNC;
using SQLInformation;


namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class Explore_InstanceStorage : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        Object serverCollection = null;

        #region Constructors

        public Explore_InstanceStorage()
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
                //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                //// Things work if this line is present.  Testing to see if it works without 6/13/2012
                //// Yup, still works.  Don't need this line as it is done in the XAML.
                //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;

                //((CollectionViewSource)this.Resources["serversViewSource"]).Source = Common.ApplicationDataSet.Servers;

                ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;

                // This line changes the Source of the serversInstancesViewSource.

                //((CollectionViewSource)this.Resources["instancesViewSource"]).Source = Common.ApplicationDataSet.Instances;

                lc_Root.DataContext = Common.ApplicationDataSet.Instances;
                gc_Instances.ItemsSource = Common.ApplicationDataSet.Instances;

                ViewMode.ApplyAuthorization(this);

                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions2);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions3);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions4);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions5);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions6);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions7);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions8);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions9);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions10);

                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails1);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails2);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails3);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails4);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails5);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails6);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails7);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails8);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails9);
                //ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails10);

                //ViewMode.AutoHideGroupVisibility(ahg_Left);
                //ViewMode.AutoHideGroupVisibility(ahg_Top);
                //ViewMode.AutoHideGroupVisibility(ahg_Right);
                //ViewMode.AutoHideGroupVisibility(ahg_Bottom);

                var ckDisplayEnvironmentColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayEnvironmentColumns");
                if (ckDisplayEnvironmentColumns != null)
                {
                    ((CheckBox)ckDisplayEnvironmentColumns).IsChecked = true;
                }

                var ckDisplayOperatingSystemColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayOperatingSystemColumns");
                if (ckDisplayOperatingSystemColumns != null)
                {
                    ((CheckBox)ckDisplayOperatingSystemColumns).IsChecked = false;
                }

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

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetSnapShotErrorColumns(e);
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnFocusedRowChanged_Database(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                series1.DataSource = null;
                series1a.DataSource = null;
                series1b.DataSource = null;

                return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.DatabasesRow)((DataRowView)row).Row;
            var dbID = row2.ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DatabaseInfo
                .Where(di => di.Database_ID == dbID)
                .Select(i => new
                {
                    SnapShotDate = i.SnapShotDate,
                    IndexSpaceUsage = i.IndexSpaceUsage / 1024.0,
                    DataSpaceUsage = i.DataSpaceUsage / 1024.0,
                    SpaceAvailable = i.SpaceAvailable / 1024.0
                });

            try
            {
                series1.DataSource = chartSource;
                series1a.DataSource = chartSource;
                series1b.DataSource = chartSource;
            }
            catch (Exception ex)
            {

            }

        }

        private void OnFocusedRowChanged_DataFile(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                seriesDFS.DataSource = null;
                seriesDFS1.DataSource = null;
                seriesDFS2.DataSource = null;
                //seriesDFS3.DataSource = null;

                seriesDFA.DataSource = null;
                seriesDFA1.DataSource = null;
                seriesDFA2.DataSource = null;
                seriesDFA3.DataSource = null;

                return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.DBDataFilesRow)((DataRowView)row).Row;
            var dbID = row2.Database_ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DBDataFileInfo
                .Where(di => di.Database_ID == dbID)
                .Select(i => new
                {
                    SnapShotDate = i.SnapShotDate,
                    //BytesReadFromDisk = (System.DBNull.Value.Equals(i.BytesReadFromDisk) ? 0 : i.BytesReadFromDisk),
                    //BytesWrittenToDisk = (System.DBNull.Value.Equals(i.BytesWrittenToDisk) ? 0 : i.BytesWrittenToDisk),
                    //NumberOfDiskReads = (System.DBNull.Value.Equals(i.NumberOfDiskReads) ? 0 : i.NumberOfDiskReads),
                    //NumberOfDiskWrites = (System.DBNull.Value.Equals(i.NumberOfDiskWrites) ? 0 : i.NumberOfDiskWrites),
                    //BytesReadFromDisk = ((Nullable<long>)i.BytesReadFromDisk != null ? i.BytesReadFromDisk : 0),
                    //BytesWrittenToDisk = ((Nullable<long>)i.BytesWrittenToDisk != null ? i.BytesWrittenToDisk : 0),
                    //NumberOfDiskReads = ((Nullable<long>)i.NumberOfDiskReads != null ? i.NumberOfDiskReads : 0),
                    //NumberOfDiskWrites = ((Nullable<long>)i.NumberOfDiskWrites != null ? i.NumberOfDiskWrites : 0),
                    AvailableSpace = i.AvailableSpace / 1024.0,
                    Size = i.Size / 1024.0,
                    UsedSpace = i.UsedSpace / 1024.0
                    //VolumeFreeSpace = i.VolumeFreeSpace / 1024.0
                });

            try
            {
                seriesDFS.DataSource = chartSource;
                seriesDFS1.DataSource = chartSource;
                seriesDFS2.DataSource = chartSource;
                //seriesDFS3.DataSource = chartSource;

                seriesDFA.DataSource = chartSource;
                seriesDFA1.DataSource = chartSource;
                seriesDFA2.DataSource = chartSource;
                seriesDFA3.DataSource = chartSource;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnFocusedRowChanged_LogFile(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                seriesLFS.DataSource = null;
                seriesLFS1.DataSource = null;

                //seriesLFS3.DataSource = null;

                seriesLFA.DataSource = null;
                seriesLFA1.DataSource = null;
                seriesLFA2.DataSource = null;
                seriesLFA3.DataSource = null;

                return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.DBLogFilesRow)((DataRowView)row).Row;
            var dbID = row2.Database_ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DBLogFileInfo
                .Where(di => di.Database_ID == dbID)
                .Select(i => new
                {
                    SnapShotDate = i.SnapShotDate,
                    //BytesReadFromDisk = ((long?)i.BytesReadFromDisk != null ? (long?)i.BytesReadFromDisk : (long?)0),
                    //BytesWrittenToDisk = ((long?)i.BytesWrittenToDisk != null ? (long?)i.BytesWrittenToDisk : (long?)0),
                    //NumberOfDiskReads = ((long?)i.NumberOfDiskReads != null ? (long?)i.NumberOfDiskReads : (long?)0),
                    //NumberOfDiskWrites = ((long?)i.NumberOfDiskWrites != null ? (long?)i.NumberOfDiskWrites : (long?)0),
                    Size = i.Size / 1024.0,
                    UsedSpace = i.UsedSpace / 1024.0
                    //VolumeFreeSpace = i.VolumeFreeSpace / 1024.0
                });

            try
            {
                seriesLFS.DataSource = chartSource;
                seriesLFS1.DataSource = chartSource;

                //seriesLFS3.DataSource = chartSource;

                seriesLFA.DataSource = chartSource;
                seriesLFA1.DataSource = chartSource;
                seriesLFA2.DataSource = chartSource;
                seriesLFA3.DataSource = chartSource;
            }
            catch (Exception ex)
            {

            }

        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSize();
        }

        private void paneToolbox_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateGridSize();
        }

        private void ShowAs_Checked(object sender, RoutedEventArgs e)
        {
            if (groupContainer == null)
                return;

            LayoutGroupView containerView, childView;

            if (sender == checkShowAsGroupBoxes)
            {
                containerView = LayoutGroupView.GroupBox;
                childView = LayoutGroupView.GroupBox;
            }
            else
            {
                if (sender == checkShowAsTabs)
                {
                    containerView = LayoutGroupView.Tabs;
                    childView = LayoutGroupView.Group;
                }
                else
                {
                    return;
                }
            }

            groupContainer.View = containerView;

            foreach (FrameworkElement child in groupContainer.GetLogicalChildren(false))
            {
                if (child is LayoutGroup)
                {
                    ((LayoutGroup)child).View = childView;
                }
            }
        }


        private void StartWith(object sender, RoutedEventArgs e)
        {
            //if (groupContainer == null)
            //    return;

            //var serversVS = (CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //var serversInstancesVS = (CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //var instancesVS = (CollectionViewSource)this.Resources["instancesViewSource"];

            //var serversInstancesDatabasesVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesViewSource"];
            //var instancesDatabasesVS = (CollectionViewSource)this.Resources["instancesDatabasesViewSource"];

            //var serversInstancesDatabasesDatabaseInfoVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDatabaseInfoViewSource"];
            //var instancesDatabasesDatabaseInfoVS = (CollectionViewSource)this.Resources["instancesDatabasesDatabaseInfoViewSource"];

            //var serversInstancesDatabasesDBTablesVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBTablesViewSource"];
            //var instancesDatabasesDBTablesVS = (CollectionViewSource)this.Resources["instancesDatabasesDBTablesViewSource"];

            //var serversInstancesDatabasesDBViewsVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBViewsViewSource"];
            //var instancesDatabasesDBViewsVS = (CollectionViewSource)this.Resources["instancesDatabasesDBViewsViewSource"];

            //var serversInstancesDatabasesDBStoredProceduresVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBStoredProceduresViewSource"];
            //var instancesDatabasesDBStoredProceduresVS = (CollectionViewSource)this.Resources["instancesDatabasesDBStoredProceduresViewSource"];

            //var serversInstancesDatabasesDBDdlTriggersVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBDdlTriggersViewSource"];
            //var instancesDatabasesDBDdlTriggersVS = (CollectionViewSource)this.Resources["instancesDatabasesDBDdlTriggersViewSource"];

            //var serversInstancesDatabasesDBUsersVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBUsersViewSource"];
            //var instancesDatabasesDBUsersVS = (CollectionViewSource)this.Resources["instancesDatabasesDBUsersViewSource"];

            //var serversInstancesDatabasesDBRolesVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBRolesViewSource"];
            //var instancesDatabasesDBRolesVS = (CollectionViewSource)this.Resources["instancesDatabasesDBRolesViewSource"];

            //var serversInstancesDatabasesDBUserDefinedFunctionsVS = (CollectionViewSource)this.Resources["serversInstancesDatabasesDBUserDefinedFunctionsViewSource"];
            //var instancesDatabasesDBUserDefinedFunctionsVS = (CollectionViewSource)this.Resources["instancesDatabasesDBUserDefinedFunctionsViewSource"];


            //if (sender == startWithServers)
            //{
            //    Binding serversInstancesBinding = new Binding("FK_Instances_Servers");
            //    serversInstancesBinding.Source = serversInstancesVS;
            //    //instancesGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, serversInstancesBinding);

            //    //instancesGridControl.ItemsSource = serversInstancesVS;
            //    //databasesGridControl.ItemsSource = serversInstancesDatabasesVS;
            //    //tablesGridControl.ItemsSource = serversInstancesDatabasesDBTablesVS;
            //    //viewsGridControl.ItemsSource = serversInstancesDatabasesDBViewsVS;
            //    //storedProceduresGridControl.ItemsSource = serversInstancesDatabasesDBStoredProceduresVS;
            //    //ddlTriggersGridControl.ItemsSource = serversInstancesDatabasesDBDdlTriggersVS;
            //    //dbUsersGridControl.ItemsSource = serversInstancesDatabasesDBUsersVS;
            //    //dbRolesGridControl.ItemsSource = serversInstancesDatabasesDBRolesVS;
            //    //dbUserDefinedFunctionsGridControl.ItemsSource = serversInstancesDatabasesDBUserDefinedFunctionsVS;
            //}
            //else
            //{
            //    if (sender == startWithInstances)
            //    {
            //        ////Binding instancesBinding = new Binding("Instances");
            //        ////instancesBinding.Source = Common.ApplicationDataSet;

            //        ////instancesGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, instancesBinding);

            //        //instancesGridControl.ItemsSource = Common.ApplicationDataSet.Instances;

            //        ////instancesGridControl.ItemsSource = instancesVS;

            //        ////databasesGridControl.ItemsSource = Common.ApplicationDataSet.Databases;
                    
            //        ////databasesGridControl.ItemsSource = Common.ApplicationDataSet.Relations[Common.ApplicationDataSet.Relations.IndexOf("FK_Databases_Instances")];

            //        //Binding databasesBinding = new Binding();
            //        ////Binding databasesBinding = new Binding("FK_Databases_Instances");
                    
            //        //databasesBinding.Source = Common.ApplicationDataSet.Databases;
            //        //databasesBinding.Path = new PropertyPath("FK_Databases_Instances");

            //        //instancesGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, databasesBinding);

            //        //Binding tablesBinding = new Binding("FK_DBTables_Databases");
            //        //tablesBinding.Source = Common.ApplicationDataSet.DBTables;
            //        //tablesGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, tablesBinding);

            //        //Binding viewsBinding = new Binding("FK_DBViews_Databases");
            //        //viewsBinding.Source = Common.ApplicationDataSet.DBViews;
            //        //viewsGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, viewsBinding);

            //        //Binding storedProceduresBinding = new Binding("FK_DBStoredProcedures_Databases");
            //        //storedProceduresBinding.Source = Common.ApplicationDataSet.DBStoredProcedures;
            //        //storedProceduresGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, storedProceduresBinding);

            //        //Binding dbRolesBinding = new Binding("FK_DBRoles_Databases");
            //        //dbRolesBinding.Source = Common.ApplicationDataSet.DBRoles;
            //        //dbRolesGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, dbRolesBinding);

            //        //Binding dbUsersBinding = new Binding("FK_DBUsers_Databases");
            //        //dbUsersBinding.Source = Common.ApplicationDataSet.DBUsers;
            //        //dbUsersGridControl.SetBinding(DevExpress.Xpf.Grid.GridControl.ItemsSourceProperty, dbUsersBinding);



            //        //databasesGridControl.ItemsSource = instancesDatabasesVS;
            //        //tablesGridControl.ItemsSource = instancesDatabasesDBTablesVS;
            //        //viewsGridControl.ItemsSource = instancesDatabasesDBViewsVS;
            //        //storedProceduresGridControl.ItemsSource = instancesDatabasesDBStoredProceduresVS;
            //        //ddlTriggersGridControl.ItemsSource = instancesDatabasesDBDdlTriggersVS;
            //        //dbUsersGridControl.ItemsSource = instancesDatabasesDBUsersVS;
            //        //dbRolesGridControl.ItemsSource = instancesDatabasesDBRolesVS;
            //        //dbUserDefinedFunctionsGridControl.ItemsSource = instancesDatabasesDBUserDefinedFunctionsVS;
       

            //        //// This seems to work well (at least the first time)
            //        ////src.Source = Common.ApplicationDataSet.Instances;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }
        #endregion

        #region Main Function Routines

        private void UpdateGridSize()
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //                "DXWindow",
            //                this.Width, this.Height,
            //                this.ActualWidth, this.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lc_Root",
            //    lc_Root.Width, lc_Root.Height,
            //    lc_Root.ActualWidth, lc_Root.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lg_Header",
            //    lg_Header.Width, this.lg_Header.Height,
            //    lg_Header.ActualWidth, lg_Header.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lg_Body",
            //    lg_Body.Width, lg_Body.Height,
            //    lg_Body.ActualWidth, lg_Body.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lg_Body_dlm",
            //    lg_Body_dlm.Width, lg_Body_dlm.Height,
            //    lg_Body_dlm.ActualWidth, lg_Body_dlm.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lg_Body_dlm_layoutGroup",
            //    lg_Body_dlm_layoutGroup.Width, lg_Body_dlm_layoutGroup.Height,
            //    lg_Body_dlm_layoutGroup.ActualWidth, lg_Body_dlm_layoutGroup.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lc_ServersGrid",
            //    lc_ServersGrid.Width, lc_ServersGrid.Height,
            //    lc_ServersGrid.ActualWidth, lc_ServersGrid.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "lc_ServerDetail",
            //    lc_ServerDetail.Width, lc_ServerDetail.Height,
            //    lc_ServerDetail.ActualWidth, lc_ServerDetail.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "ahg_Root",
            //    ahg_Root.Width, ahg_Root.Height,
            //    ahg_Root.ActualWidth, ahg_Root.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "paneToolbox",
            //    paneToolbox.Width, paneToolbox.Height,
            //    paneToolbox.ActualWidth, paneToolbox.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "layoutGroupFooter",
            //    layoutGroupFooter.Width, layoutGroupFooter.Height,
            //    layoutGroupFooter.ActualWidth, layoutGroupFooter.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("\nDXWindow-lc_Root:{0}\nlc_Root-lg_Body:{1}\nlg_Body-(lc_ServersGrid+lc_ServerDetail+ahg_Root:{2}",
            //    this.ActualHeight - lc_Root.ActualHeight,
            //    lc_Root.ActualHeight - lg_Body.ActualHeight,
            //    lg_Body.ActualHeight - (lc_ServersGrid.ActualHeight + lc_ServerDetail.ActualHeight + ahg_Root.ActualHeight)
            //    ));

            // TODO(crhodes): Figure out this hack
            double heightHack = double.Parse(textBox_HeightHack.Text);

            //gc_Servers.Height =
            //    lg_Body_dlm.ActualHeight
            //    - (lc_ServerDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Instances.Height =
                lg_Body_dlm.ActualHeight
                - (lc_InstanceDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Databases.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DatabaseDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

        }

        #endregion

    }
}
