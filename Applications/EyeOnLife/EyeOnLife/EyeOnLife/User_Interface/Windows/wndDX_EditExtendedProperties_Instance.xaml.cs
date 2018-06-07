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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

using VNC;
using SQLInformation;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_Instance.xaml
    /// </summary>
    public partial class wndDX_EditExtendedProperties_Instance : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        SQLInformation.ExpandMask.DatabaseExpandSetting databaseExpandSetting = null;
        SQLInformation.ExpandMask.InstanceExpandSetting instanceExpandSetting = null;
        SQLInformation.ExpandMask.JobServerExpandSetting jobServerExpandSetting = null;

        public wndDX_EditExtendedProperties_Instance()
        {
            InitializeComponent();
            Initialize_ExpandMasks();
        }

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            applicationDataSet = EyeOnLife.Common.ApplicationDataSet;

            // TODO(crhodes): Figureout why can't do this in Xaml.  Seems basic.

            cbe_NetName.ItemsSource = applicationDataSet.Servers;
            cbe_ADDomain.ItemsSource = applicationDataSet.LKUP_ADDomains;
            cbe_Environment.ItemsSource = applicationDataSet.LKUP_Environments;

            textEdit_ID.Text = Guid.NewGuid().ToString();
        }

        private void Initialize_ExpandMasks()
        {
            instanceExpandSetting = new SQLInformation.ExpandMask.InstanceExpandSetting(0);
            databaseExpandSetting = new SQLInformation.ExpandMask.DatabaseExpandSetting(0);
            jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(0);

            cbe_IsMonitored.ItemsSource = SQLInformation.ExpandMask.InstanceExpandSetting.OptionValues;
            cbe_DefaultDatabaseExpandMask.ItemsSource = SQLInformation.ExpandMask.DatabaseExpandSetting.OptionValues;
            cbe_DefaultJobServerExpandMask.ItemsSource = SQLInformation.ExpandMask.JobServerExpandSetting.OptionValues;
        }


        private void OnCreateSMOLogin(object sender, RoutedEventArgs e)
        {
            //SQLInformation.SMO.Helper.CreateSQLMonitorLogin(txtEdit_Name_Instance.Text);
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            int instanceExpandMask = 0;
            int databaseExpandMask = 0;
            int jobServerExpandMask = 0;

            try
            {
                foreach (var item in cbe_IsMonitored.SelectedItems)
                {
                    instanceExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                foreach (var item in cbe_DefaultDatabaseExpandMask.SelectedItems)
                {
                    databaseExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                foreach (var item in cbe_DefaultJobServerExpandMask.SelectedItems)
                {
                    jobServerExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(instanceExpandMask);

                SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();

                newInstance.ID = Guid.Parse(textEdit_ID.Text);
                newInstance.Name_Instance = txtEdit_Name_Instance.Text;
                newInstance.NetName = cbe_NetName.Text;
                newInstance.ADDomain = cbe_ADDomain.Text;
                newInstance.Environment = cbe_Environment.Text;

                newInstance.IsMonitored = instanceExpandSetting.IsMonitored;
                newInstance.ExpandContent = instanceExpandSetting.ExpandContent;
                newInstance.ExpandJobServer = instanceExpandSetting.ExpandJobServer;
                newInstance.ExpandStorage = instanceExpandSetting.ExpandStorage;
                newInstance.ExpandLogins = instanceExpandSetting.ExpandLogins;
                newInstance.ExpandServerRoles = instanceExpandSetting.ExpandServerRoles;
                newInstance.ExpandTriggers = instanceExpandSetting.ExpandTriggers;

                newInstance.DefaultDatabaseExpandMask = databaseExpandMask;
                newInstance.DefaultJobServerExpandMask = jobServerExpandMask;

                Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
                Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
        }

        private void cbe_NetName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            txtEdit_Name_Instance.Text = cbe_NetName.Text;
        }
    }
}
