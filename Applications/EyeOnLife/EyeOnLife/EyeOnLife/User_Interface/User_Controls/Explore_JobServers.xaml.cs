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


namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class Explore_JobServers : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        Object serverCollection = null;

        #region Constructors

        public Explore_JobServers()
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

                ((CollectionViewSource)this.Resources["instancesViewSource"]).Source = Common.ApplicationDataSet.Instances;

                //var ckDisplayEnvironmentColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayEnvironmentColumns");
                //if (ckDisplayEnvironmentColumns != null)
                //{
                //    ((CheckBox)ckDisplayEnvironmentColumns).IsChecked = true;
                //}

                //var ckDisplayOperatingSystemColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions2, "ckDisplayOperatingSystemColumns");
                //if (ckDisplayOperatingSystemColumns != null)
                //{
                //    ((CheckBox)ckDisplayOperatingSystemColumns).IsChecked = false;
                //}

                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions2);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions3);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions4);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions5);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions6);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions7);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions8);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions9);
                //UserMode.DisplayOptionsVisibility(cc_DisplayOptions10);

                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails1);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails2);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails3);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails4);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails5);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails6);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails7);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails8);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails9);
                //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails10);

                ViewMode.AutoHideGroupVisibility(ahg_Left);
                ViewMode.AutoHideGroupVisibility(ahg_Top);
                ViewMode.AutoHideGroupVisibility(ahg_Right);
                ViewMode.AutoHideGroupVisibility(ahg_Bottom);

                //if (Common.UserMode == Common.UserModes.Basic)
                //{
                //    var adminOptions = VNC.Xaml.PhysicalTree.FindChild<WrapPanel>(cc_DisplayOptions_Instances, "AdminOptions");
                //    ((WrapPanel)adminOptions).Visibility = System.Windows.Visibility.Hidden;
                //}

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

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSize();
        }

        private void UpdateGridSize()
        {
            // TODO(crhodes): Figure out this hack.  Not sure why this number is bigger on JobServers
            double heightHack = double.Parse(textBox_HeightHack.Text);

            lc_Instances.Height = lg_Body_dlm.ActualHeight - (lc_InstanceDetail.ActualHeight + heightHack);
            lc_JobServers.Height = lg_Body_dlm.ActualHeight - (lc_JobServerDetail.ActualHeight + heightHack);
            lc_JSAlertCategories.Height = lg_Body_dlm.ActualHeight - (lc_JSAlertCategoryDetail.ActualHeight + heightHack);
            lc_JSAlerts.Height = lg_Body_dlm.ActualHeight - (lc_JSAlertDetail.ActualHeight + heightHack);
            lc_JSJobCategories.Height = lg_Body_dlm.ActualHeight - (lc_JSJobCategoryDetail.ActualHeight + heightHack);
            lc_JSOperatorCategories.Height = lg_Body_dlm.ActualHeight - (lc_JSOperatorCategoryDetail.ActualHeight + heightHack);
            lc_JSOperators.Height = lg_Body_dlm.ActualHeight - (lc_JSOperatorDetail.ActualHeight + heightHack);
            lc_JSProxyAccounts.Height = lg_Body_dlm.ActualHeight - (lc_JSProxyAccountDetail.ActualHeight + heightHack);
            lc_JSSharedSchedules.Height = lg_Body_dlm.ActualHeight - (lc_JSSharedScheduleDetail.ActualHeight + heightHack);
            lc_JSTargetServerGroups.Height = lg_Body_dlm.ActualHeight - (lc_JSTargetServerGroupDetail.ActualHeight + heightHack);
            lc_JSTargetServers.Height = lg_Body_dlm.ActualHeight - (lc_JSTargetServerDetail.ActualHeight + heightHack);
            lc_JSJobs.Height = lg_Body_dlm.ActualHeight - (lc_JSJobDetail.ActualHeight + heightHack);
            lc_JSJobSchedules.Height = lg_Body_dlm.ActualHeight - (lc_JSJobScheduleDetail.ActualHeight + heightHack);
            lc_JSJobSteps.Height = lg_Body_dlm.ActualHeight - (lc_JSJobStepDetail.ActualHeight + heightHack);

        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void paneToolbox_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateGridSize();
        }

    }
}
