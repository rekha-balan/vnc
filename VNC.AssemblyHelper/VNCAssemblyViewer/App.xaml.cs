using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows;

using VNCAD = VNC.AD;

namespace VNCAssemblyViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static int CLASS_BASE_ERRORNUMBER = VNCAssemblyViewer.ErrorNumbers.VNCAssemblyViewer;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        const string TYPE_NAME = "App";

        public App()
        {
            // This catches any errors from the intial load of the application.
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if TRACE
            VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            // This came from the DevExpress Wizard.
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();

            Common.CurrentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            try
            {
                if (VNCAssemblyViewer.Data.Config.ADBypass)
                {
                    if (!(Common.IsAdministrator = Common.CurrentUser.Identity.Name.Contains("crhodes") ? true : false))
                    {
                        Common.IsAdministrator = Common.CurrentUser.Identity.Name.Contains("Christopher") ? true : false;
                    }

                    if (!(Common.IsBetaUser = Common.CurrentUser.Identity.Name.Contains("crhodes") ? true : false))
                    {
                        Common.IsBetaUser = Common.CurrentUser.Identity.Name.Contains("Christopher") ? true : false;
                    }

                    if (!(Common.IsDeveloper = Common.CurrentUser.Identity.Name.Contains("crhodes") ? true : false))
                    {
                        Common.IsDeveloper = Common.CurrentUser.Identity.Name.Contains("Christopher") ? true : false;
                    }
                }
                else
                {
                    if (!VNCAssemblyViewer.Data.Config.AD_Users_AllowAll)
                    {
                        bool isAuthorizedUser = VNCAD.Helper.CheckGroupMembership(
                            //"maward", 
                            Common.CurrentUser.Identity.Name,
                            VNCAssemblyViewer.Data.Config.ADGroup_Users,
                            VNCAssemblyViewer.Data.Config.AD_Domain);

                        if (!isAuthorizedUser)
                        {
                            MessageBox.Show(string.Format("You must be a member of {0}\\{1} to run this application.",
                                VNCAssemblyViewer.Data.Config.AD_Domain, VNCAssemblyViewer.Data.Config.ADGroup_Users));
                            return;
                        }
                    }

                    Common.IsAdministrator = VNCAD.Helper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        VNCAssemblyViewer.Data.Config.ADGroup_Administrators,
                        VNCAssemblyViewer.Data.Config.AD_Domain);


                    Common.IsBetaUser = VNCAD.Helper.CheckDirectGroupMembership(
                        Common.CurrentUser.Identity.Name,
                        VNCAssemblyViewer.Data.Config.ADGroup_BetaUsers,
                        VNCAssemblyViewer.Data.Config.AD_Domain);

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
                User_Interface.Windows.dxrwMain _window1 = new User_Interface.Windows.dxrwMain();

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

        private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {

        }

        private void Application_Deactivated(object sender, EventArgs e)
        {

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}
