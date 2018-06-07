using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SQLInformation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        const string TYPE_NAME = "App";

        static string patientId = string.Empty;
        public static string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        void ApplicationStartingUp(object sender, StartupEventArgs args)
        {
//#if TRACE
//            Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
//#endif
//            // Tell other assemblies where to write output messages.
//            SMOHelper.Common.OutputWindow = Common.OutputWindow;

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
                            patientId = windowArgs;
                            break;
                    }
                }
            }

            _window1.Show();
        }
    }
}
