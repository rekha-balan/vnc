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
    /// Interaction logic for wndDX_NewServer.xaml
    /// </summary>
    public partial class wndDX_NewServer : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        SQLInformation.ExpandMask.InstanceExpandSetting instanceExpandSetting = null;

        public wndDX_NewServer()
        {
            InitializeComponent();
            Initialize_ExpandMasks();
        }

        private void Initialize_ExpandMasks()
        {
            instanceExpandSetting = new SQLInformation.ExpandMask.InstanceExpandSetting(0);

            cbe_DefaultInstanceExpandMask.ItemsSource = SQLInformation.ExpandMask.InstanceExpandSetting.OptionValues;
        }

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            applicationDataSet = EyeOnLife.Common.ApplicationDataSet;
            cbe_ADDomain.ItemsSource = applicationDataSet.LKUP_ADDomains;
            cbe_Environment.ItemsSource = applicationDataSet.LKUP_Environments;
            textEdit_ID.Text = Guid.NewGuid().ToString();
        }

        private void ClearEntries()
        {
            textEdit_ID.Text = Guid.NewGuid().ToString();
            cbe_ADDomain.SelectedItem = "";
            cbe_Environment.SelectedItem = "";
            cbe_DefaultInstanceExpandMask.SelectedItem = "";
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            int instanceExpandMask = 0;

            try
            {
                foreach (var item in cbe_DefaultInstanceExpandMask.SelectedItems)
                {
                    instanceExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                SQLInformation.Data.ApplicationDataSet.ServersRow newServer = Common.ApplicationDataSet.Servers.NewServersRow();

                newServer.ID = Guid.Parse(textEdit_ID.Text);
                newServer.NetName = textEdit_NetName.Text;
                newServer.ADDomain = cbe_ADDomain.Text;
                newServer.Environment = cbe_Environment.Text;
                newServer.DefaultInstanceExpandMask = instanceExpandMask;

                Common.ApplicationDataSet.Servers.AddServersRow(newServer);
                Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);                
            }

            ClearEntries();
        }
    }
}
