using System;
using System.Collections.Generic;
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

using SQLInformation.User_Interface.Windows;

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSplashScreen.xaml
    /// </summary>
    public partial class wucSplashScreen : UserControl
    {
        const string TYPE_NAME = "wucSplashScreen";

        #region Initialization

        public wucSplashScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers


        private void OnAdministration_Click(object sender, RoutedEventArgs e)
        {
            SQLMonitorDBAdminMain win1 = new SQLMonitorDBAdminMain();
            win1.Show();
        }

        private void OnExploreApplications_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnBigPicture_Click(object sender, RoutedEventArgs e)
        {
            BigPicture win1 = new BigPicture();
            win1.Show();
        }

        private void OnCyclonEye_Click(object sender, RoutedEventArgs e)
        {
            AboutDialogWindow win1 = new AboutDialogWindow();
            win1.Show();
        }

        private void OnAdminDatabases_Click(object sender, RoutedEventArgs e)
        {
            AdminDatabases win1 = new AdminDatabases();
            win1.Show();
        }

        private void OnExploreEnvironments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnExploreDatabases_Click(object sender, RoutedEventArgs e)
        {
            ExploreDatabases win1 = new ExploreDatabases();
            win1.Show();
        }

        private void OnExploreInstances_Click(object sender, RoutedEventArgs e)
        {
            ExploreInstances win1 = new ExploreInstances();
            win1.Show();
        }

        private void OnExploreServers_Click(object sender, RoutedEventArgs e)
        {
            ExploreServers win1 = new ExploreServers();
            win1.Show();
        }

        private void OnHelp_Click(object sender, RoutedEventArgs e)
        {
            //Common.OutputWindow.Show();
        }

        private void OnAdminInstances_Click(object sender, RoutedEventArgs e)
        {
            AdminInstances win1 = new AdminInstances();
            win1.Show();
        }

        private void OnMainRibbonWindow_Click(object sender, RoutedEventArgs e)
        {
            MainRibbonWindow win1 = new MainRibbonWindow();
            win1.Show();
        }

        private void OnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow win1 = new MainWindow();
            //win1.Show();
        }

        private void OnAdminServers_Click(object sender, RoutedEventArgs e)
        {
            AdminServers win1 = new AdminServers();
            win1.Show();
        }

        private void OnAdminStoredProcedures_Click(object sender, RoutedEventArgs e)
        {
            AdminStoredProcedures win1 = new AdminStoredProcedures();
            win1.Show();
        }

        private void OnExploreSystems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnAdminTables_Click(object sender, RoutedEventArgs e)
        {
            AdminTables win1 = new AdminTables();
            win1.Show();
        }

        private void OnUnknown_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This space is available :)");
        }

        private void OnAdminViews_Click(object sender, RoutedEventArgs e)
        {
            AdminViews win1 = new AdminViews();
            win1.Show();
        }

        #endregion

    }
}
