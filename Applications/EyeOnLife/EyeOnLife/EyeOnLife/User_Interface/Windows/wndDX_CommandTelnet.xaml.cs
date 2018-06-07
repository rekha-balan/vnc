using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
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
using System.Diagnostics;
using DevExpress.Xpf.Core.Commands;
//using DevExpress.Xpf.DemoBase;
using DevExpress.Utils;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_Instance.xaml
    /// </summary>
    public partial class wndDX_CommandTelnet : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        string serverName;

        public wndDX_CommandTelnet()
        {
            InitializeComponent();
        }

        public wndDX_CommandTelnet(string instance, string server, int portNumber)
        {
            InitializeComponent();
            tb_Instance.Text = instance;
            serverName = server;
            tx_Port.Text = portNumber.ToString();
            tb_Server.Text = serverName;
        }

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void OnTelnet(object sender, RoutedEventArgs e)
        {
            tb_TelnetResult.Text = "";

            string server = tb_Server.Text;
            int port = int.Parse(tx_Port.Text);

            try
            {
                Helpers.TelnetInterface.TelnetConnection tc = new Helpers.TelnetInterface.TelnetConnection(server, port);
                tb_TelnetResult.Text = string.Format("Successfully connected to {0} on port {1}", server, port);
            }
            catch (Exception ex)
            {
                tb_TelnetResult.Text = ex.Message;
            }
        }

        private void OnResolveName(object sender, RoutedEventArgs e)
        {
            string server = tb_Server.Text;
            tb_IPAddress.Text = Helpers.Server.GetIPV4Address(server);
        }

        private void OnPingServer(object sender, RoutedEventArgs e)
        {
            string hostName = tb_Server.Text;
            Helpers.Server.PingServer(hostName);
        }

    }
}
