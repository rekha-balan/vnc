using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using PacificLife.Life;

namespace EyeOnLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        const string TYPE_NAME = "App";



        void ApplicationStartingUp(object sender, StartupEventArgs args)
        {
#if TRACE
            PLLog.Trace5("Start", PLLOG_APPNAME);
#endif
            // Tell other assemblies where to write output messages.
            //SMOHelper.Common.OutputWindow = Common.OutputWindow;

            //User_Interface.Windows.MainWindow _window1 = new User_Interface.Windows.MainWindow();
            //User_Interface.Windows.MainWindowSkeleton _window1 = new User_Interface.Windows.MainWindowSkeleton();
            //User_Interface.Windows.MainRibbonWindow _window1 = new User_Interface.Windows.MainRibbonWindow();
            //User_Interface.Windows.Window1 _window1 = new User_Interface.Windows.Window1();
            //User_Interface.Windows.Window2 _window1 = new User_Interface.Windows.Window2();
            User_Interface.Windows.SplashScreen _window1 = new User_Interface.Windows.SplashScreen();

            String windowArgs = string.Empty;
            // Check for arguments; if there are some build the path to the package out of the args.
            if (args.Args.Length > 0 && args.Args[0] != null)
            {
                for (int i = 0; i < args.Args.Length; ++i)
                {
                    windowArgs = args.Args[i];
                    switch (i)
                    {
                        case 0: // Patient Id
                            //patientId = windowArgs;
                            break;
                    }
                }
            }

            _window1.Show();
        }

        private void Application_SessionEnding_1(object sender, SessionEndingCancelEventArgs e)
        {
            //MessageBox.Show("Application_SessionEnding_1");
            //Common.OutputWindow.Close();
            //Common.OutputWindow = null;
        }

        private void Application_Exit_1(object sender, ExitEventArgs e)
        {
            //MessageBox.Show("Application_Exit_1");
            //Common.OutputWindow.Close();
            //Common.OutputWindow = null;
        }

        private void Application_Deactivated_1(object sender, EventArgs e)
        {
            //Common.OutputWindow.Close();
            //Common.OutputWindow = null;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
