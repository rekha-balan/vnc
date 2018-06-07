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

using DevExpress.Data.Filtering;

using DevExpress.Xpf.Charts;
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
    public partial class Explore_DatabaseStorage : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Constructors

        public Explore_DatabaseStorage()
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

                //((CollectionViewSource)this.Resources["databasesViewSource"]).Source = Common.ApplicationDataSet.Databases;

                lc_Root.DataContext = Common.ApplicationDataSet.Databases;

                gc_Databases.ItemsSource = Common.ApplicationDataSet.Databases;

                ApplyDataFilters();

                ViewMode.ApplyAuthorization(this);

                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions2);
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

                ViewMode.AutoHideGroupVisibility(ahg_Left);
                ViewMode.AutoHideGroupVisibility(ahg_Top);
                ViewMode.AutoHideGroupVisibility(ahg_Right);
                ViewMode.AutoHideGroupVisibility(ahg_Bottom);

                string[] ckDisplayColumns = { "ckDisplayEnvironmentColumns", "ckDisplayBackupColumns", "ckDisplaySizeColumns" };

                ViewMode.UpdateDisplayColumnsCheckBoxes(cc_DisplayOptions3, ckDisplayColumns);

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
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnDisplayDataFileInfo_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            // Get the list of datafileinfo records for the current datafile

            var row = gc_DBDataFiles.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.DBDataFilesRow datafile =
                (SQLInformation.Data.ApplicationDataSet.DBDataFilesRow)((DataRowView)row).Row;

            Guid dbID = datafile.Database_ID;
            Guid dbDataFileID = datafile.ID;

            var dataFileInfo = Common.ApplicationDataSet.DBDataFileInfo
                .Where(id => dbDataFileID == id.DBDataFile_ID)
                .OrderBy(s => s.SnapShotDate);

            var uc = new EyeOnLife.User_Interface.User_Controls.DB_DataFileInfo(dataFileInfo);

            var window = new EyeOnLife.User_Interface.Windows.UserControlHost();
            window.ShowUserControl(uc);

            window.Show();
        }


        private void OnDisplayDBLogins(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            // Get the list of logins for the Database

            var row = gc_Databases.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
                (SQLInformation.Data.ApplicationDataSet.DatabasesRow)((DataRowView)row).Row;

            Guid instanceID = database.Instance_ID;
            Guid dbID = database.ID;

            var logins = Common.ApplicationDataSet.Logins.Where(l => l.Instance_ID == instanceID).OrderBy(n => n.Name_Login);
            var dbUsers = Common.ApplicationDataSet.DBUsers.Where(u => u.Database_ID == dbID).OrderBy(n => n.Name_User);

            var win = new EyeOnLife.User_Interface.Windows.InstanceDatabaseInfo();

            win.tb_Instance.Text = database.Name_Instance;
            win.tb_Database.Text = database.Name_Database;

            win.lv_Logins.ItemsSource = logins;
            win.lv_Users.ItemsSource = dbUsers;

            win.Show();

            //foreach (var login in logins)
            //{
            //    System.Diagnostics.Debug.WriteLine("{0} - {1}", login.Name_Instance, login.Name_Login);
            //}

            //foreach (var user in dbUsers)
            //{
            //    System.Diagnostics.Debug.WriteLine("{0} - {1}", user.Login, user.Name_User); 
            //}
        }

        private void OnDisplayLogFileInfo_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            // Get the list of datafileinfo records for the current datafile

            var row = gc_DBLogFiles.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.DBLogFilesRow logFile =
                (SQLInformation.Data.ApplicationDataSet.DBLogFilesRow)((DataRowView)row).Row;

            Guid dbID = logFile.Database_ID;
            Guid dbLogFileID = logFile.ID;

            var logFileInfo = Common.ApplicationDataSet.DBLogFileInfo
                .Where(id => dbLogFileID == id.DBLogFile_ID)
                .OrderBy(s => s.SnapShotDate);

            var uc = new EyeOnLife.User_Interface.User_Controls.DB_LogFileInfo(logFileInfo);

            var window = new EyeOnLife.User_Interface.Windows.UserControlHost();
            window.ShowUserControl(uc);

            window.Show();
        }

        private void OnFocusedRowChanged_Database(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                seriesDB_SpaceAvailable.DataSource = null;
                seriesDB_Size.DataSource = null;
                seriesDB_IndexSpace.DataSource = null;
                seriesDB_DataSpace.DataSource = null;

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
                    SpaceAvailable = i.SpaceAvailable / 1024.0,
                    Size = i.Size
                });

            try
            {
                System.Diagnostics.Debug.WriteLine(((XYDiagram2D)cht_Database.Diagram).Panes.Count);
                seriesDB_SpaceAvailable.DataSource = chartSource;
                seriesDB_Size.DataSource = chartSource;
                seriesDB_IndexSpace.DataSource = chartSource;
                seriesDB_DataSpace.DataSource = chartSource;
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

                seriesDFS_AvailableSpace.DataSource = null;
                seriesDFS_Size.DataSource = null;
                seriesDFS_UsedSpace.DataSource = null;

                //seriesDFA_BytesRead.DataSource = null;
                //seriesDFA_BytesWritten.DataSource = null;
                //seriesDFA_NumberReads.DataSource = null;
                //seriesDFA_NumberWrites.DataSource = null;

            	return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.DBDataFilesRow)((DataRowView)row).Row;
            var dbID = row2.Database_ID;
            var dataFileID = row2.ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DBDataFileInfo
                .Where(di => di.DBDataFile_ID == dataFileID)
                .Select(i => new
                {
                    SnapShotDate = i.SnapShotDate,
                    //BytesReadFromDisk = (System.DBNull.Value.Equals(i.BytesReadFromDisk) ?  0 : i.BytesReadFromDisk),
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
                seriesDFS_AvailableSpace.DataSource = chartSource;
                seriesDFS_Size.DataSource = chartSource;
                seriesDFS_UsedSpace.DataSource = chartSource;

                // More AvailableSpace to second axis
                //XYDiagram2D.SetSeriesAxisY(seriesDFS2, ((XYDiagram2D)chartDataFileSpace.Diagram).SecondaryAxesY[0]);
                //seriesDFS3.DataSource = chartSource;

                //seriesDFA_BytesRead.DataSource = chartSource;
                //seriesDFA_BytesWritten.DataSource = chartSource;

                //seriesDFA_NumberReads.DataSource = chartSource;
                //seriesDFA_NumberWrites.DataSource = chartSource;

                //XYDiagram2D.SetSeriesAxisY(seriesDFA2, ((XYDiagram2D)chartDataFileActivity.Diagram).SecondaryAxesY[0]);
                //XYDiagram2D.SetSeriesAxisY(seriesDFA3, ((XYDiagram2D)chartDataFileActivity.Diagram).SecondaryAxesY[0]);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void OnFocusedRowChanged_FileGroup(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void OnFocusedRowChanged_LogFile(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;

            if (row == null)
            {
                // TODO(crhodes): Perhaps clear the series datasources

                //seriesLFS_AvailableSpace.DataSource = null;
                seriesLFS_Size.DataSource = null;
                seriesLFS_UsedSpace.DataSource = null;

                //seriesLFA_BytesRead.DataSource = null;
                //seriesLFA_BytesWritten.DataSource = null;
                //seriesLFA_NumberReads.DataSource = null;
                //seriesLFA_NumberWrites.DataSource = null;

                return;
            }

            var row2 = (SQLInformation.Data.ApplicationDataSet.DBLogFilesRow)((DataRowView)row).Row;
            var dbID = row2.Database_ID;
            var logFileID = row2.ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DBLogFileInfo
                .Where(di => di.DBLogFile_ID == logFileID)
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
                //seriesLFS_AvailableSpace.DataSource = chartSource;
                seriesLFS_Size.DataSource = chartSource;
                seriesLFS_UsedSpace.DataSource = chartSource;

                // More AvailableSpace to second axis
                //XYDiagram2D.SetSeriesAxisY(seriesDFS2, ((XYDiagram2D)chartDataFileSpace.Diagram).SecondaryAxesY[0]);
                //seriesDFS3.DataSource = chartSource;

                //seriesLFA_BytesRead.DataSource = chartSource;
                //seriesLFA_BytesWritten.DataSource = chartSource;

                //seriesLFA_NumberReads.DataSource = chartSource;
                //seriesLFA_NumberWrites.DataSource = chartSource;
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
        #endregion

        #region Main Function Routines
       
        /// <summary>
        /// Filter the data that is being displayed.
        /// </summary>
        private void ApplyDataFilters()
        {

            CriteriaOperator filter = CriteriaOperator.Parse("[NotFound] = true");
            gc_Databases.AddMRUFilter(filter);

            CriteriaOperator filter1 = CriteriaOperator.Parse("[NotFound] = false And [IsMonitored_Instance] = true");
            gc_Databases.FilterCriteria = filter1;
            //dataGrid.ActiveFilterEnabled = true;

            //dataGrid.FilterString = "[Crawled] = 'True'";
        }

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

            //gc_Instances.Height =
            //    lg_Body_dlm.ActualHeight
            //    - (lc_InstanceDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Databases.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DatabaseDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

        }

        #endregion

    }
}
