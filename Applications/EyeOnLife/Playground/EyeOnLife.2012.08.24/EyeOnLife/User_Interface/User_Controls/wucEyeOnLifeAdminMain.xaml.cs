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
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSQLMonitorAdminMain.xaml
    /// </summary>
    public partial class wucEyeOnLifeAdminMain : UserControl
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wucEyeOnLifeAdminMain()
        {
            InitializeComponent();
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
        }

        private ucBase _currentControl;

        #region Event Handlers


        private void btnDatabases_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_Databases uc = new User_Controls.wucDX_Admin_Databases();
            ShowUserControl(uc);
        }

        private void btnDatabaseInfo_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DatabaseInfo uc = new wucDX_Admin_DatabaseInfo();
            ShowUserControl(uc);
        }


        private void btnDBDataFileInfo_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBDataFileInfo uc = new wucDX_Admin_DBDataFileInfo();
            ShowUserControl(uc);
        }

        private void btnDBDataFiles_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBDataFiles uc = new wucDX_Admin_DBDataFiles();
            ShowUserControl(uc);
        }

        private void btnDBDdlTriggers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBDdlTriggers uc = new User_Controls.wucDX_Admin_DBDdlTriggers();
            ShowUserControl(uc);
        }

        private void btnDBFileGroups_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBFileGroups uc = new wucDX_Admin_DBFileGroups();
            ShowUserControl(uc);
        }

        private void btnDBLogFileInfo_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBLogFileInfo uc = new wucDX_Admin_DBLogFileInfo();
            ShowUserControl(uc);
        }

        private void btnDBLogFiles_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBLogFiles uc = new wucDX_Admin_DBLogFiles();
            ShowUserControl(uc);
        }

        private void btnDBRoles_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBRoles uc = new wucDX_Admin_DBRoles();
            ShowUserControl(uc);
        }

        private void btnDBStoredProcedures_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBStoredProcedures uc = new wucDX_Admin_DBStoredProcedures();
            ShowUserControl(uc);
        }

        private void btnDBTables_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBTables uc = new wucDX_Admin_DBTables();
            ShowUserControl(uc);
        }

        private void btnDBUserDefinedFunctions_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBUserDefinedFunctions uc = new wucDX_Admin_DBUserDefinedFunctions();
            ShowUserControl(uc);
        }

        private void btnDBUsers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBUsers uc = new wucDX_Admin_DBUsers();
            ShowUserControl(uc);
        }

        private void btnDBViews_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_DBViews uc = new wucDX_Admin_DBViews();
            ShowUserControl(uc);
        }

        private void btnInstanceInfo_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_InstanceInfo uc = new wucDX_Admin_InstanceInfo();
            ShowUserControl(uc);
        }

        private void btnInstances_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_Instances uc = new wucDX_Admin_Instances();
            ShowUserControl(uc);
        }

        private void btnJobServers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JobServers uc = new wucDX_Admin_JobServers();
            ShowUserControl(uc);
        }

        private void btnJSAlertCategories_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSAlertCategories uc = new wucDX_Admin_JSAlertCategories();
            ShowUserControl(uc);
        }

        private void btnJSAlerts_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSAlerts uc = new wucDX_Admin_JSAlerts();
            ShowUserControl(uc);
        }

        private void btnJSJobCategories_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSJobCategories uc = new wucDX_Admin_JSJobCategories();
            ShowUserControl(uc);
        }

        private void btnJSJobs_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSJobs uc = new wucDX_Admin_JSJobs();
            ShowUserControl(uc);
        }

        private void btnJSJobSchedules_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSJobSchedules uc = new wucDX_Admin_JSJobSchedules();
            ShowUserControl(uc);
        }

        private void btnJSJobSteps_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSJobSteps uc = new wucDX_Admin_JSJobSteps();
            ShowUserControl(uc);
        }

        private void btnJSOperatorCategories_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSOperatorCategories uc = new wucDX_Admin_JSOperatorCategories();
            ShowUserControl(uc);
        }

        private void btnJSOperators_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSOperators uc = new wucDX_Admin_JSOperators();
            ShowUserControl(uc);
        }

        private void btnJSProxyAccounts_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSProxyAccounts uc = new wucDX_Admin_JSProxyAccounts();
            ShowUserControl(uc);
        }

        private void btnJSSharedSchedules_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSSharedSchedules uc = new wucDX_Admin_JSSharedSchedules();
            ShowUserControl(uc);
        }

        private void btnJSTargetServerGroups_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSTargetServerGroups uc = new wucDX_Admin_JSTargetServerGroups();
            ShowUserControl(uc);
        }

        private void btnJSTargetServers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_JSTargetServers uc = new wucDX_Admin_JSTargetServers();
            ShowUserControl(uc);
        }

        private void btnLinkedServers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_LinkedServers uc = new wucDX_Admin_LinkedServers();
            ShowUserControl(uc);
        }

        private void btnLKUPTables_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_LKUPTables uc = new wucDX_Admin_LKUPTables();
            ShowUserControl(uc);
        }

        private void btnLogins_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_Logins uc = new wucDX_Admin_Logins();
            ShowUserControl(uc);
        }

        private void btnServerInfo_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_ServerInfo uc = new wucDX_Admin_ServerInfo();
            ShowUserControl(uc);
        }

        private void btnServerRoles_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_ServerRoles uc = new wucDX_Admin_ServerRoles();
            ShowUserControl(uc);
        }

        private void btnServers_Click(object sender, RoutedEventArgs e)
        {
            wucDX_Admin_Servers uc = new wucDX_Admin_Servers();
            ShowUserControl(uc);
        }

        private void UnhookTitleEvent(ucBase control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
            }
        }

        private void ShowUserControl(ucBase control)
        {
            //UnhookTitleEvent(_currentControl);
            dpUserControlContainer.Children.Clear();

            if (control != null)
            {
            	dpUserControlContainer.Children.Add(control);
                _currentControl = control;
            }

            //HookTitleEvent(_currentControl);
        }

        private void HookTitleEvent(ucBase control)
        {
            SetTitle(control, EventArgs.Empty);

            if (control != null)
            {
                control.TitleChanged += SetTitle;
            }

        }

        private void SetTitle(object sender, EventArgs e)
        {
            //ucBase uc = sender as ucBase;

            //if (uc != null && ! string.IsNullOrEmpty(uc.Title))
            //{
            //    this.Title = string.Format("{0}", uc.Title);
            //}
            //else
            //{
            //    this.Title = "main Form";
            //}
        }



        #endregion

    }
}
