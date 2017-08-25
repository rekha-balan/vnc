using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace VNCWPFUserControls
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static int BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;


        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if TRACE
            long starTicks = VNC.AppLog.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
#endif
            try
            {
                AppDomain.CurrentDomain.UnhandledException +=
                              new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                string firstUserControl = "VNCWPFUserControls.User_Interface.User_Controls.wucDXLayoutControl";
                User_Interface.Windows.DXUserControlHost _window = new User_Interface.Windows.DXUserControlHost("wucDXLayoutControl", firstUserControl);
                //User_Interface.Windows.DXRibbonWindowMain _window = new User_Interface.Windows.DXRibbonWindowMain();

                //String windowArgs = string.Empty;
                // Check for arguments; if there are some build the path to the package out of the args.
                //if (args.Args.Length > 0 && args.Args[0] != null)
                //{
                //    for (int i = 0; i < args.Args.Length; ++i)
                //    {
                //        windowArgs = args.Args[i];
                //        switch (i)
                //        {
                //            case 0: // Patient Id
                //                //patientId = windowArgs;
                //                break;
                //        }
                //    }
                //}

                _window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, starTicks);
#endif
        }

        private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
#if TRACE
            long starTicks = VNC.AppLog.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
#endif

#if TRACE
            VNC.AppLog.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, starTicks);
#endif
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
#if TRACE
            long starTicks = VNC.AppLog.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
#endif

#if TRACE
            VNC.AppLog.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, starTicks);
#endif
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
#if TRACE
            long starTicks = VNC.AppLog.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
#endif

#if TRACE
            VNC.AppLog.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, starTicks);
#endif
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
#if TRACE
            long starTicks = VNC.AppLog.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
#endif
            MessageBox.Show("Application_DispatcherUnhandledException: " + e.Exception.Message, LOG_APPNAME, MessageBoxButton.OK, MessageBoxImage.Warning);
            MessageBox.Show("Application_DispatcherUnhandledException Inner: " + e.Exception.InnerException.Message, LOG_APPNAME, MessageBoxButton.OK, MessageBoxImage.Warning);

            //e.Handled = true;   // Use if can handle exception that is thrown
            e.Handled = false;  // Default

#if TRACE
            VNC.AppLog.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, starTicks);
#endif
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "Uncaught Thread Exception",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
