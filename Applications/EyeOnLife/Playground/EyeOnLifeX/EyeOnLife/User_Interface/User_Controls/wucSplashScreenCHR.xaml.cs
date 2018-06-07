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

using EyeOnLife.User_Interface.Windows;
using PacificLife.Life;
using SQLInformation;
using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSplashScreen.xaml
    /// </summary>
    public partial class wucSplashScreenCHR : UserControl
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucSplashScreenCHR()
        {
#if TRACE
            long startTicks = PLLog.Trace5("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
#if TRACE
            PLLog.Trace5("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

        }

        #endregion

        #region Properties


        #endregion

        #region Event Handlers


        private void OnBigPictureDBContents_Click(object sender, RoutedEventArgs e)
        {
            wndDX_BigPictureDBContents win1 = new wndDX_BigPictureDBContents();
            win1.Show();
        }

        private void OnAdministration_Click(object sender, RoutedEventArgs e)
        {
            SQLMonitorDBAdminMain win1 = new SQLMonitorDBAdminMain();
            win1.Show();
        }

        private void OnDailySnapShot(object sender, RoutedEventArgs e)
        {
            SQLInformation.Helper.TakeSnapShot_Daily(null);
        }

        private void OnBigPictureDBStorage_Click(object sender, RoutedEventArgs e)
        {
            wndDX_BigPictureDBStorage win1 = new wndDX_BigPictureDBStorage();
            win1.Show();
        }

        private void OnCyclonEye_Click(object sender, RoutedEventArgs e)
        {
            AboutDialogWindow win1 = new AboutDialogWindow();
            win1.Show();
        }

        private void OnEditValueChanged_Theme(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            var bar = sender;            
            var foo = e.NewValue;

            System.Diagnostics.Debug.WriteLine(foo.GetType());
            System.Diagnostics.Debug.WriteLine(bar.GetType());
            
            //ThemeManager.SetThemeName(this, "DeepBlue");
            //ThemeManager.SetThemeName(this,  ((Theme)e.NewValue).Name);
            
            // This is the magic that sets the theme for the entire application!

            ThemeManager.ApplicationThemeName = ((Theme)e.NewValue).Name;
        }

        private void OnExploreApplications_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnExploreDatabases_Click(object sender, RoutedEventArgs e)
        {
            ExploreDatabases win1 = new ExploreDatabases();
            win1.Show();
        }

        private void OnExploreEnvironments_Click(object sender, RoutedEventArgs e)
        {
            dxExploreEnvironments win1 = new dxExploreEnvironments();
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

        private void OnExploreSystems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Go see crhodes");
            //Common.OutputWindow.Show();
        }

        private void OnHourlySnapShot(object sender, RoutedEventArgs e)
        {
            SQLInformation.Helper.TakeSnapShot_IntraDay(null);
        }

        private void OnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow win1 = new MainWindow();
            //win1.Show();
        }


        private void OnToDo_Click(object sender, RoutedEventArgs e)
        {
            ExternalProgram ep = new ExternalProgram();
            ep.Launch("notepad", @".\TODO.txt", null);
        }

        private void OnUnknown_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This space is available :)");
        }


        private void OnWindow1_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void OnWindow2_Click(object sender, RoutedEventArgs e)
        {
            Window2 win1 = new Window2();
            win1.Show();
        }


        private void OnWindow3_Click(object sender, RoutedEventArgs e)
        {
            dxWindow3 win1 = new dxWindow3();
            win1.Show();
        }

        private void OnWindow4_Click(object sender, RoutedEventArgs e)
        {
            wndDX_NewInstance win1 = new wndDX_NewInstance();
            win1.ShowDialog();
        }
        #endregion

        #region Main Function Routines

        public static int ExecuteCommand(string Command, int Timeout)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            Process = Process.Start(ProcessInfo);
            Process.WaitForExit(Timeout);
            ExitCode = Process.ExitCode;
            Process.Close();

            return ExitCode;
        }

        #endregion

    }
}
