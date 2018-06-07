using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

using VNC;

namespace EyeOnLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        const string TYPE_NAME = "App";

        public App()
        {
            // This catches any errors from the initial load of the application.
            // Useful for catching missing resource files, etc.

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ApplicationStartingUp(object sender, StartupEventArgs args)
        {
#if TRACE
            VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif

            Common.CurrentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            try
            {
                if (SQLInformation.Data.Config.ADBypass)
                {
                    Common.IsAdministrator = true;
                    Common.IsBetaUser = true;
                }
                else
                {
                    if (! SQLInformation.Data.Config.AD_Users_AllowAll)
                    {
                        bool isAuthorizedUser = ADHelper.ADHelper.CheckGroupMembership(
                            //"maward", 
                            Common.CurrentUser.Identity.Name,
                            SQLInformation.Data.Config.ADGroup_Users,
                            SQLInformation.Data.Config.AD_Domain);

                        if (!isAuthorizedUser)
                        {
                            MessageBox.Show(string.Format("You must be a member of {0}\\{1} to run this application.",
                                SQLInformation.Data.Config.AD_Domain, SQLInformation.Data.Config.ADGroup_Users));
                            return;
                        }
                    }

                    Common.IsAdministrator = ADHelper.ADHelper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        SQLInformation.Data.Config.ADGroup_Administrators,
                        SQLInformation.Data.Config.AD_Domain);


                    Common.IsBetaUser = ADHelper.ADHelper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        SQLInformation.Data.Config.ADGroup_BetaUsers,
                        SQLInformation.Data.Config.AD_Domain);

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

                //User_Interface.Windows.SplashScreen _window1 = new User_Interface.Windows.SplashScreen();
                User_Interface.Windows.DXRibbonWindowMain _window1 = new User_Interface.Windows.DXRibbonWindowMain();

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

    }
}
