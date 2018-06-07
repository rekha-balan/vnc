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


namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_Instance.xaml
    /// </summary>
    public partial class wndDX_NewInstance : DXWindow
    {
        public wndDX_NewInstance()
        {
            InitializeComponent();
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            iDTextBox.Text = Guid.NewGuid().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();

            newInstance.ID = Guid.Parse(iDTextBox.Text);
            newInstance.Name_Instance = name_InstanceTextBox.Text;
            newInstance.NetName = netNameTextBox.Text;
            newInstance.IsMonitored = (bool)isMonitoredCheckBox.IsChecked;
            newInstance.ExpandContent = (bool)expandContentCheckBox.IsChecked;
            newInstance.ExpandStorage = (bool)expandStorageCheckBox.IsChecked;
            newInstance.ExpandLogins = (bool)expandLoginsCheckBox.IsChecked;
            newInstance.ExpandServerRoles = (bool)expandServerRolesCheckBox.IsChecked;
            newInstance.ExpandTriggers = (bool)expandTriggersCheckBox.IsChecked;
            newInstance.DefaultDatabaseExpandMask = int.Parse(defaultDatabaseExpandMaskTextBox.Text);

            Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
            Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        }
    }
}
