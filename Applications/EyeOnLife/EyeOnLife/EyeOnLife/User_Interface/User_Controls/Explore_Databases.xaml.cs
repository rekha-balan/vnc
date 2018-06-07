using System;
using System.Collections.Generic;
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

using System.Threading;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class Explore_Databases : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";


        Object serverCollection = null;

        #region Constructors

        public Explore_Databases()
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
                ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;

                lc_Root.DataContext = Common.ApplicationDataSet.Databases;

                gc_Databases.ItemsSource = Common.ApplicationDataSet.Databases;

                string[] ckDisplayColumns = { "ckDisplayEnvironmentColumns", "ckDisplayNotesColumns" };

                ViewMode.UpdateDisplayColumnsCheckBoxes(cc_DisplayOptions3, ckDisplayColumns);

                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions2);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions3);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions4);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions5);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions6);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions7);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions8);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions9);
                //ViewMode.DisplayOptionsVisibility(cc_DisplayOptions10);

                ViewMode.AutoHideGroupVisibility(ahg_Left);
                ViewMode.AutoHideGroupVisibility(ahg_Top);
                ViewMode.AutoHideGroupVisibility(ahg_Right);
                ViewMode.AutoHideGroupVisibility(ahg_Bottom);
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


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
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
            //    "ahg_Root",
            //    ahg_Bottom.Width, ahg_Bottom.Height,
            //    ahg_Bottom.ActualWidth, ahg_Bottom.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "paneToolbox",
            //    paneToolbox.Width, paneToolbox.Height,
            //    paneToolbox.ActualWidth, paneToolbox.ActualHeight));

            //System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
            //    "layoutGroupFooter",
            //    lg_Footer.Width, lg_Footer.Height,
            //    lg_Footer.ActualWidth, lg_Footer.ActualHeight));


            // TODO(crhodes): Figure out this hack
            double heightHack = double.Parse(textBox_HeightHack.Text);

            gc_Databases.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DatabaseDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Tables.Height =
                lg_Body_dlm.ActualHeight
                - (lc_TableDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBViews.Height =
                lg_Body_dlm.ActualHeight
                - (lc_ViewDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_StoredProcedures.Height =
                lg_Body_dlm.ActualHeight
                - (lc_StoredProcedureDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DDLTriggers.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DDLTriggerDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBUsers.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DBUserDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBRoles.Height =
                lg_Body_dlm.ActualHeight
                - (lc_DBRoleDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_UserDefinedFunctions.Height =
                lg_Body_dlm.ActualHeight
                - (lc_UserDefinedFunctionDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void paneToolbox_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateGridSize();
        }

        private void tableView4_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var foo = e;
        }

        private void tableView3_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            return;
            //var x = tableView3.FocusedRow;
            //var xx = (System.Data.DataRowView)x;
            //var xxx = xx.Row;

            //var row = (SQLInformation.Data.ApplicationDataSet.DatabasesRow)xxx;

            //var v = row.ID;

            //var tables = Common.ApplicationDataSet.DBTables.Where(t => t.Database_ID == v);

            //foreach (var t in tables)
            //{
            //    System.Diagnostics.Debug.WriteLine(t.Name_Table);
            //}

            //try
            //{
            //    gc_Tables.ItemsSource = tables.ToArray();
            //}
            //catch (Exception ex)
            //{
                
            //}

            ////gc_Tables.ItemsSource = tables.ToList();
            //var foo = e;

        }

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

    }
}
