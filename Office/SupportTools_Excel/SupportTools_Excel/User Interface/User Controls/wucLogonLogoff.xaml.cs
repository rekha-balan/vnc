using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Rally.RestApi;
using Rally.RestApi.Json;
using Rally.RestApi.Response;

using XlHlp = VNC.AddinHelper.Excel;

using SF = SupportTools_Excel.SalesforceSR;

using VNCRally = VNC.Rally;
using VNCXaml = VNC.Xaml;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucLogonLogoff.xaml
    /// </summary>
    public partial class wucLogonLogoff : UserControl
    {

        // Rally Stuff

        string _Rally_username = "crhodes@harborsys.com";
        string _Rally_password = "H0jnacki!";
        string _Rally_serverUrl = "https://rally1.rallydev.com";
        private RallyRestApi _Rally_RestApi;

        // Salesforce Stuff

        SF.SoapClient _Salesforce_loginClient = null;
        string _Salesforce_SessionId = null;
        string _Salesforce_ServerUrl = null;
        EndpointAddress _Salesforce_apiAddr = null;

        public RallyRestApi Rally_RestApi
        {
            get
            {
                return _Rally_RestApi;
            }
            set
            {
                _Rally_RestApi = value;
            }
        }

        public string Salesforce_SessionId
        {
            get
            {
                return _Salesforce_SessionId;
            }
            set
            {
                _Salesforce_SessionId = value;
            }
        }
        public EndpointAddress Salesforce_apiAddr
        {
            get
            {
                return _Salesforce_apiAddr;
            }
            set
            {
                _Salesforce_apiAddr = value;
            }
        }

        public wucLogonLogoff()
        {
            InitializeComponent();
        }

        private void btnLogoff_Rally_Click(object sender, RoutedEventArgs e)
        {
            if (null != _Rally_RestApi)
            {
                try
                {
                    VNCRally.Helper.LogoutRestApi();
                    Rally_RestApi = null;

                    VNCXaml.Helper.EnableAndShow(this.btnLogon_Rally);

                    VNCXaml.Helper.DisableAndHide(this.btnLogoff_Rally);
                    //VNCXaml.Helper.DisableAndHide(this.btnRetrieveInfo_Rally);

                    //DisableAndHide(lg_RallyTriageInfo);
                    // Figure out how to handle this.  Maybe binding to a property this
                    // class exposes to toggle stuff.
                    //VNCXaml.Helper.DisableAndHide(lg_Rally);
                }
                catch (Exception ex)
                {
                    XlHlp.DisplayInWatchWindow(ex.ToString());
                    XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
                }
            }
        }

        private void btnLogoff_Salesforce_Click(object sender, RoutedEventArgs e)
        {
            if (null != _Salesforce_loginClient)
            {
                try
                {
                    _Salesforce_loginClient.Close();

                    _Salesforce_loginClient = null;
                    _Salesforce_ServerUrl = null;
                    Salesforce_SessionId = null;

                    Salesforce_apiAddr = null;

                    VNCXaml.Helper.EnableAndShow(this.btnLogon_Salesforce);

                    VNCXaml.Helper.DisableAndHide(this.btnLogoff_Salesforce);
                    //VNCXaml.Helper.DisableAndHide(this.btnRetrieveInfo_Salesforce);

                    //DisableAndHide(lg_SalesforceTriageInfo);
                    // Figure out how to handle this.  Maybe binding to a property this
                    // class exposes to toggle stuff.
                    //VNCXaml.Helper.DisableAndHide(lg_Salesforce);
                }
                catch (Exception ex)
                {
                    XlHlp.DisplayInWatchWindow(ex.ToString());
                    XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
                }
            }
        }

        private void btnLogon_Rally_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rally_RestApi = VNCRally.Helper.LogonRestApi(_Rally_serverUrl, _Rally_password, _Rally_username);

                VNCXaml.Helper.DisableAndHide(this.btnLogon_Rally);

                VNCXaml.Helper.EnableAndShow(this.btnLogoff_Rally);
                //VNCXaml.Helper.EnableAndShow(this.btnRetrieveInfo_Rally);

                //EnableAndShow(lg_RallyTriageInfo);
                // Figure out how to handle this.  Maybe binding to a property this
                // class exposes to toggle stuff.
                //VNCXaml.Helper.EnableAndShow(lg_Rally);
            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void btnLogon_Salesforce_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Salesforce_loginClient = new SF.SoapClient("Soap");

                //string sfPassword = "/*n4IJHNH1ZZ8t*/";
                string sfPassword = "IzW7JXUK3PF6";
                //string sfToken = "iiQMXOoX0xiUQ5JHqDM62jsNr";
                string sfToken = "DPuYjDgOEuV7WsJLoVTgaNtTN";

                string sfUserName = "crhodes@harborsys.com";
                string loginPassword = sfPassword + sfToken;

                SF.LoginResult result = _Salesforce_loginClient.login(null, sfUserName, loginPassword);

                Salesforce_SessionId = result.sessionId;
                _Salesforce_ServerUrl = result.serverUrl;

                XlHlp.DisplayInWatchWindow(string.Format("Session ID is >{0}<   Server URL is >{1}<", Salesforce_SessionId, _Salesforce_ServerUrl));

                Salesforce_apiAddr = new EndpointAddress(_Salesforce_ServerUrl);

                // If we got this far, enable the processing buttons

                VNCXaml.Helper.DisableAndHide(this.btnLogon_Salesforce);

                VNCXaml.Helper.EnableAndShow(this.btnLogoff_Salesforce);
                //VNCXaml.Helper.EnableAndShow(this.btnRetrieveInfo_Salesforce);

                //EnableAndShow(lg_SalesforceTriageInfo);
                // TODO(crhodes):
                // Figure out how to handle this.  Maybe binding to a property this
                // class exposes to toggle stuff.
                //VNCXaml.Helper.EnableAndShow(lg_Salesforce);


            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

    }
}
