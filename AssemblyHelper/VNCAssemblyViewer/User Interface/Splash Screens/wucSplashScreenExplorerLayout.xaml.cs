using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VNCAssemblyViewer.User_Interface.Windows;
//using VNC;
//using VNCAssemblyViewer;
using DevExpress.Xpf.Core;

namespace VNCAssemblyViewer.User_Interface.User_Controls.SplashScreens
{
    /// <summary>
    /// Interaction logic for wucSplashScreen.xaml
    /// </summary>
    public partial class wucSplashScreenExplorerLayout : UserControl
    {

        private static int CLASS_BASE_ERRORNUMBER = VNCAssemblyViewer.ErrorNumbers.VNCAssemblyViewer;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Initialization

        public wucSplashScreenExplorerLayout()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
            ThemeManager.ApplicationThemeName = VNCAssemblyViewer.Data.Config.DefaultUITheme;
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

        }

        #endregion

        #region Properties


        #endregion

        #region Event Handlers


        private void OnExplore_Instances_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var win1 = new wndDX_Explore_Instances();
                //win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void OnAllDatabases_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl(
                "Instance Logins",
                "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_Databases");
        }

        private void OnAllLogins_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl(
                "Instance Logins",
                "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_Databases");
        }

        private void OnAllDBUsers_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl(
                "DBUsers",
                "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_DBUsers");
        }

        private void OnAdministration_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl(
                "SQLMonitorDB SMO Tables",
                "VNCAssemblyViewer.User_Interface.User_Controls.wucEyeOnLifeAdminMain");
        }

        private void OnExplore_DatabaseContents(object sender, RoutedEventArgs e)
        {
            try
            {
                //var win1 = new wndDX_ExploreDatabaseContents();
                //win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void OnExplore_DatabaseStorage(object sender, RoutedEventArgs e)
        {
            try
            {
                //var win1 = new wndDX_ExploreDatabaseStorage();
                //win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void OnExplore_JobServers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var win1 = new wndDX_ExploreJobServers();
                //win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }


        private void OnExplore_LookupTables(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl("LKUP Tables", "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_LKUPTables");
        }

        private void OnInstanceRoles_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl("Instance ServerRoles", "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_ServerRoles");
        }


        private void OnUnknown_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This space is available :)");
        }

        #endregion

        private void OnAllServers_Click(object sender, RoutedEventArgs e)
        {
            User_Interface.Helper.DisplayAdminUserControl("Servers", "VNCAssemblyViewer.User_Interface.User_Controls.wucDX_Admin_Servers");
        }


        #region Main Function Routines


        #endregion

    }
}
