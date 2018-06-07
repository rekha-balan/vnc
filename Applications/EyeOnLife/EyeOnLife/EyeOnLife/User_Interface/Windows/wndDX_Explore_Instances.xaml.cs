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

using WpfHelper;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class wndDX_Explore_Instances : DXWindow, IDisposable
    {

        Object serverCollection = null;

        #region Constructors

        public wndDX_Explore_Instances()
        {
            try
            {
                InitializeComponent();

                int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

                this.Width = (primaryScreenWidth * 9) / 10;
                this.Height = (primaryScreenHeight * 9) / 10;

                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        #endregion
        
        #region Initialization

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                //// Things work if this line is present.  Testing to see if it works without 6/13/2012
                //// Yup, still works.  Don't need this line as it is done in the XAML.
                //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;

                ((CollectionViewSource)this.Resources["serversViewSource"]).Source = Common.ApplicationDataSet.Servers;

                ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;

                // This line changes the Source of the serversInstancesViewSource.

                ((CollectionViewSource)this.Resources["instancesViewSource"]).Source = Common.ApplicationDataSet.Instances;

                // Update the views.  First ensure a row is selected.

                tableView.FocusedRowHandle = 1;

                //serversGridControl.GroupBy("SecurityZone");

                //var ckDisplayEnvironmentColumns = DisplayOptions.VisualTreeHelperExtensions.FindAncestor<CheckBox>(cc_DisplayOptions_Instances);

                //var ckDisplayEnvironmentColumns = cc_DisplayOptions_Instances.FindName("ckDisplayEnvironmentColumns");

                var ckDisplayEnvironmentColumns = VisualTreeHelperExtensions.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayEnvironmentColumns");
                ((CheckBox)ckDisplayEnvironmentColumns).IsChecked = true;

                //var ckDisplayOperatingSystemColumns = DisplayOptions.VisualTreeHelperExtensions.FindAncestor<CheckBox>(cc_DisplayOptions_Instances);
                var ckDisplayOperatingSystemColumns = VisualTreeHelperExtensions.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayOperatingSystemColumns");
                ((CheckBox)ckDisplayOperatingSystemColumns).IsChecked = false;

                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions2);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions3);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions4);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions5);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions6);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions7);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions8);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions9);
                ViewMode.DisplayOptionsVisibility(cc_DisplayOptions10);

                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails1);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails2);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails3);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails4);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails5);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails6);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails7);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails8);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails9);
                ViewMode.SnapShotDetailsVisibility(lc_SnapShotDetails10);

                ViewMode.AutoHideGroupVisibility(ahg_Left);
                ViewMode.AutoHideGroupVisibility(ahg_Top);
                ViewMode.AutoHideGroupVisibility(ahg_Right);
                ViewMode.AutoHideGroupVisibility(ahg_Bottom);

                //if (Common.UserMode == Common.UserModes.Basic)
                //{
                //    var adminOptions = VisualTreeHelperExtensions.FindChild<WrapPanel>(cc_DisplayOptions_Instances, "AdminOptions");
                //    ((WrapPanel)adminOptions).Visibility = System.Windows.Visibility.Hidden;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        ~wndDX_Explore_Instances()
        {
            Dispose(false);
        }

        #region Event Handlers

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

//<CollectionViewSource 
//      x:Key="serversInstancesViewSource" 
//      Source="{Binding Path=FK_Instances_Servers, Source={StaticResource serversViewSource}}" />
//<CollectionViewSource
//      x:Key="serversInstancesViewSource" 
//      Source="{Binding Path=Instances, Source={StaticResource applicationDataSet}}" />

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

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances.RejectChanges();
        }

        private void DXWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSize();
        }

        private void UpdateGridSize()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                            "DXWindow",
                            this.Width, this.Height,
                            this.ActualWidth, this.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lc_Root",
                lc_Root.Width, lc_Root.Height,
                lc_Root.ActualWidth, lc_Root.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lg_Header",
                lg_Header.Width, this.lg_Header.Height,
                lg_Header.ActualWidth, lg_Header.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lg_Body",
                lg_Body.Width, lg_Body.Height,
                lg_Body.ActualWidth, lg_Body.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lg_Body_dockLayoutManager",
                lg_Body_dockLayoutManager.Width, lg_Body_dockLayoutManager.Height,
                lg_Body_dockLayoutManager.ActualWidth, lg_Body_dockLayoutManager.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lg_Body_dockLayoutManager_layoutGroup",
                lg_Body_dockLayoutManager_layoutGroup.Width, lg_Body_dockLayoutManager_layoutGroup.Height,
                lg_Body_dockLayoutManager_layoutGroup.ActualWidth, lg_Body_dockLayoutManager_layoutGroup.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lc_ServersGrid",
                lc_ServersGrid.Width, lc_ServersGrid.Height,
                lc_ServersGrid.ActualWidth, lc_ServersGrid.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "lc_ServerDetail",
                lc_ServerDetail.Width, lc_ServerDetail.Height,
                lc_ServerDetail.ActualWidth, lc_ServerDetail.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "ahg_Root",
                ahg_Bottom.Width, ahg_Bottom.Height,
                ahg_Bottom.ActualWidth, ahg_Bottom.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "paneToolbox",
                paneToolbox.Width, paneToolbox.Height,
                paneToolbox.ActualWidth, paneToolbox.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("{0,35} - Width:{1} Height:{2}  ActualWidth:{3} ActualHeight:{4}",
                "layoutGroupFooter",
                layoutGroupFooter.Width, layoutGroupFooter.Height,
                layoutGroupFooter.ActualWidth, layoutGroupFooter.ActualHeight));

            System.Diagnostics.Debug.WriteLine(string.Format("\nDXWindow-lc_Root:{0}\nlc_Root-lg_Body:{1}\nlg_Body-(lc_ServersGrid+lc_ServerDetail+ahg_Root:{2}",
                this.ActualHeight - lc_Root.ActualHeight,
                lc_Root.ActualHeight - lg_Body.ActualHeight,
                lg_Body.ActualHeight - (lc_ServersGrid.ActualHeight + lc_ServerDetail.ActualHeight + ahg_Bottom.ActualHeight)
                ));

            // TODO(crhodes): Figure out this hack
            double heightHack = double.Parse(textBox_HeightHack.Text);

            gc_Servers.Height = 
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_ServerDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Instances.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_InstanceDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Databases.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_DatabaseDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_Tables.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_TableDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBViews.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_ViewDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_StoredProcedures.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_StoredProcedureDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DDLTriggers.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_DDLTriggerDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBUsers.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_DBUserDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_DBRoles.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_DBRoleDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

            gc_UserDefinedFunctions.Height =
                lg_Body_dockLayoutManager.ActualHeight
                - (lc_UserDefinedFunctionDetail.ActualHeight + paneToolbox.ActualHeight + heightHack);

        }

        private void OnSendFeedback_Click(object sender, RoutedEventArgs e)
        {
            ExternalProgram.SendFeedback();
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomDisplay.FormatStorageColumns(e);
        }

        private void paneToolbox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //UpdateGridSize();
        }

        private void paneToolbox_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateGridSize();
        }

    }
}
