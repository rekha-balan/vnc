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
    public partial class wucSplashScreen : UserControl
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucSplashScreen()
        {
#if TRACE
            long startTicks = PLLog.Trace5("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
            ThemeManager.ApplicationThemeName = SQLInformation.Data.Config.DefaultUITheme;
#if TRACE
            PLLog.Trace5("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

        }

        #endregion

        #region Properties


        #endregion

        #region Event Handlers


        private void OnAllInstances_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Something that shows list of instances perhaps");
        }

        private void OnBigPictureDBContents_Click(object sender, RoutedEventArgs e)
        {
            wndDX_BigPictureDBContents win1 = new wndDX_BigPictureDBContents();
            win1.Show();
        }

        private void OnAllDatabases_Click(object sender, RoutedEventArgs e)
        {
            wndDX_Databases win1 = new wndDX_Databases();
            win1.Show();
        }

        private void OnAllLogins_Click(object sender, RoutedEventArgs e)
        {
            wndDX_Logins win1 = new wndDX_Logins();
            win1.Show();
        }

        private void OnAllDBUsers_Click(object sender, RoutedEventArgs e)
        {
            wndDX_DBUsers win1 = new wndDX_DBUsers();
            win1.Show();
        }

        private void OnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDialogWindow win1 = new AboutDialogWindow();
            win1.Show();
        }

        private void OnAdministration_Click(object sender, RoutedEventArgs e)
        {
            SQLMonitorDBAdminMain win1 = new SQLMonitorDBAdminMain();
            win1.Show();
        }


        private void OnBigPictureDBStorage_Click(object sender, RoutedEventArgs e)
        {
            wndDX_BigPictureDBStorage win1 = new wndDX_BigPictureDBStorage();
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

        private void OnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Go see crhodes");
            //Common.OutputWindow.Show();
        }

        private void OnSnapShot_Daily(object sender, RoutedEventArgs e)
        {
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Instance);

            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, databaseExpandSetting);
        }

        private void OnSnapShot_IntraDay(object sender, RoutedEventArgs e)
        {
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Instance);

            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, databaseExpandSetting);
        }

        private void OnSnapShot_Weekly(object sender, RoutedEventArgs e)
        {
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Instance);

            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, databaseExpandSetting);
        }

        private void OnLKUPTables_Click(object sender, RoutedEventArgs e)
        {
            wndDX_LKUPTables win1 = new wndDX_LKUPTables();
            win1.Show();
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
