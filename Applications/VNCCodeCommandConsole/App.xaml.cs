using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace VNCCodeCommandConsole
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

                Common.CurrentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                if (Data.Config.ADBypass)
                {
                    Common.IsAdministrator = true;
                    Common.IsBetaUser = true;
                }
                else
                {
                    if (!Data.Config.AD_Users_AllowAll)
                    {

                        bool isAuthorizedUser = VNC.AD.Helper.CheckGroupMembership(
                            //"maward", 
                            Common.CurrentUser.Identity.Name,
                            Data.Config.ADGroup_Users,
                            Data.Config.AD_Domain);

                        if (!isAuthorizedUser)
                        {
                            MessageBox.Show(string.Format("You must be a member of {0}\\{1} to run this application.",
                                Data.Config.AD_Domain, Data.Config.ADGroup_Users));
                            return;
                        }
                    }

                    Common.IsAdministrator = VNC.AD.Helper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        Data.Config.ADGroup_Administrators,
                        Data.Config.AD_Domain);


                    Common.IsBetaUser = VNC.AD.Helper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        Data.Config.ADGroup_BetaUsers,
                        Data.Config.AD_Domain);

                    Common.IsDeveloper = Common.CurrentUser.Identity.Name.Contains("crhodes") ? true : false;

                    // Next lines are for testing UI only.  Comment out for normal operation.
                    //Common.IsAdministrator = false;   
                    //Common.IsBetaUser = false; 
                    //Common.IsDeveloper = false;
                }

                // Cannot do here as the Common.ApplicationDataSet has not been loaded.  Need to move here or do later.
                // For now this is in DXRibbonWindowMain();

                //var eventMessage = "Started";
                //SQLInformation.Helper.IndicateApplicationUsage(LOG_APPNAME, DateTime.Now, currentUser.Identity.Name, eventMessage);

                // Launch the main window.



                // TODO(crhodes)
                // Would be cool to start this with a Window specified in the App.Config file

                //User_Interface.Windows.SplashScreen _window1 = new User_Interface.Windows.SplashScreen();
                User_Interface.Windows.DXRibbonWindowMain _window1 = new User_Interface.Windows.DXRibbonWindowMain();
                //User_Interface.Windows.About _window1 = new User_Interface.Windows.About();

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

                _window1.Show();
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
