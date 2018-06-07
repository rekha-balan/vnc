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
    /// Interaction logic for wndDX_NewServer.xaml
    /// </summary>
    public partial class wndDX_NewServer : DXWindow
    {
        public wndDX_NewServer()
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
            SQLInformation.Data.ApplicationDataSet.ServersRow newServer = Common.ApplicationDataSet.Servers.NewServersRow();

            newServer.ID = Guid.Parse(iDTextBox.Text);
            newServer.NetName = netNameTextBox.Text;
            newServer.Environment = environmentTextBox.Text;
            newServer.OSVersion = oSVersionTextBox.Text;
            newServer.PhysicalMemory = int.Parse(physicalMemoryTextBox.Text);
            newServer.Platform = platformTextBox.Text;
            newServer.Processors = int.Parse(processorsTextBox.Text);

            Common.ApplicationDataSet.Servers.AddServersRow(newServer);
            Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
        }
    }
}
