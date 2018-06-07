using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Add_Server : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";
    
        #region Constructors
        
        public Add_Server()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {

                }
                LogUsage(this.GetType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Event Handlers

        private void OnCreateNewServer(object sender, RoutedEventArgs e)
        {
            try
            {
                SQLInformation.Data.ApplicationDataSet.ServersRow newServer = Common.ApplicationDataSet.Servers.NewServersRow();
                newServer.ID = Guid.NewGuid();
                newServer.NetName = te_ServerName.Text.ToUpper();

                Window parentWindow = Window.GetWindow(this);


                if (CanPingServer(newServer.NetName))
                {
                    Common.ApplicationDataSet.Servers.AddServersRow(newServer);
                    Common.ApplicationDataSet.Servers_Update();
                    //Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
                    parentWindow.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Cannot Ping Server.  Check Name");
                    parentWindow.DialogResult = false;
                }            
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
        }

        private bool CanPingServer(string netName)
        {
            bool result = false;

            var ping = new Ping();

            var pingOptions = new PingOptions();

            pingOptions.DontFragment = true;
            pingOptions.Ttl = 128;

            try
            {
                if (IPStatus.Success == ping.Send(netName).Status)
                {
            	    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                //MessageBox.Show(string.Format("Exception Thrown: {0} {1}", ex.ToString(), ex.InnerException.ToString()), "Ping Results");
            }

            return result;
        }

        private bool IsValidNetName(string netName)
        {
            bool result = false;


            CanPingServer(netName);

            return result;
        }

        #endregion

        #region Main Function Routines



        #endregion


    }
}
