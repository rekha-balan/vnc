using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Visio = Microsoft.Office.Interop.Visio;
using Office = Microsoft.Office.Core;

using VNC;
using System.Security.Principal;
using System.Windows.Forms;

namespace SupportTools_Visio
{
    public partial class ThisAddIn
    {
        // HACK(crhodes)
        // Not sure we ever got CustomTaskPanes to work in Visio.  Went back to using the Ribbon

        // Need to do a bit more work to use CustomTask Panes in Visio.  (Handled by Designer normally)

        internal Microsoft.Office.Tools.CustomTaskPaneCollection CustomTaskPanes;

        private static System.Windows.Application _XamlApp;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                AppLog.Trace("Enter", Common.PROJECT_NAME);

                Globals.Ribbons.Ribbon.chkDisplayEvents.Checked = Common.DisplayEvents;
                Globals.Ribbons.Ribbon.chkEnableAppEvents.Checked = Common.HasAppEvents;

                if (Common.HasAppEvents)
                {
                    Common.AppEvents = new Events.VisioAppEvents();
                    Common.AppEvents.VisioApplication = Globals.ThisAddIn.Application;
                }

                Common.VisioHelper.VisioApplication = Globals.ThisAddIn.Application;

                // Need to do a bit more work to use CustomTask Panes in Visio.  (Handled by Designer normally)

                CustomTaskPanes = Globals.Factory.CreateCustomTaskPaneCollection(null, null, "CustomTaskPanes", "CustomTaskPanes", this);

                InitializeWPFApplication();

                AppLog.Trace("Exit", Common.PROJECT_NAME);
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, Common.PROJECT_NAME);
                throw (ex);
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            try
            {
                AppLog.Trace("Enter", Common.PROJECT_NAME);

                // Need to do a bit more work to use CustomTask Panes in Visio.  (Handled by Designer normally)
                CustomTaskPanes.Dispose();

                if (Common.HasAppEvents)
                {
                    Common.AppEvents = null;
                }

                UnLoadXamlApplicationResources();

                AppLog.Trace("Exit", Common.PROJECT_NAME);
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, Common.PROJECT_NAME);
                throw (ex);
            }
        }

        void InitializeWPFApplication()
        {
            Common.CurrentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            LoadXamlApplicationResources();

            try
            {
                if (Data.Config.ADBypass)
                {
                    Common.IsAdministrator = true;
                    Common.IsBetaUser = true;
                    Common.IsDeveloper = true;
                }
                //else
                //{
                //    if (!Data.Config.AD_Users_AllowAll)
                //    {
                //        bool isAuthorizedUser = ADHelper.ADHelper.CheckGroupMembership(
                //            //"maward", 
                //            Common.CurrentUser.Identity.Name,
                //            SQLInformation.Data.Config.ADGroup_Users,
                //            SQLInformation.Data.Config.AD_Domain);

                //        if (!isAuthorizedUser)
                //        {
                //            MessageBox.Show(string.Format("You must be a member of {0}\\{1} to run this application.",
                //                SQLInformation.Data.Config.AD_Domain, SQLInformation.Data.Config.ADGroup_Users));
                //            return;
                //        }
                //    }

                //    Common.IsAdministrator = ADHelper.ADHelper.CheckDirectGroupMembership(
                //        Common.CurrentUser.Identity.Name,
                //        SQLInformation.Data.Config.ADGroup_Administrators,
                //        SQLInformation.Data.Config.AD_Domain);


                //    Common.IsBetaUser = ADHelper.ADHelper.CheckDirectGroupMembership(
                //        Common.CurrentUser.Identity.Name,
                //        SQLInformation.Data.Config.ADGroup_BetaUsers,
                //        SQLInformation.Data.Config.AD_Domain);

                //    Common.IsDeveloper = Common.CurrentUser.Identity.Name.Contains("crhodes") ? true : false;

                //    // Next lines are for testing UI only.  Comment out for normal operation.
                //    //Common.IsAdministrator = false;   
                //    //Common.IsBetaUser = false; 
                //    //Common.IsDeveloper = false;
                //}

                // Cannot do here as the Common.ApplicationDataSet has not been loaded.  Need to move here or do later.
                // For now this is in DXRibbonWindowMain();

                //var eventMessage = "Started";
                //SQLInformation.Helper.IndicateApplicationUsage(LOG_APPNAME, DateTime.Now, currentUser.Identity.Name, eventMessage);

                // Launch the main window.

                // Done from Ribbon

                //User_Interface.Windows.SplashScreen _window1 = new User_Interface.Windows.SplashScreen();
                //User_Interface.Windows.DXRibbonWindowMain _window1 = new User_Interface.Windows.DXRibbonWindowMain();

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

                //_window1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// LoadXamlApplicationResources
        ///
        /// Creates Xaml Resources collection in System.Windows.Application
        /// for use in Hosted applications withoug App.Xaml
        /// </summary>

        private static void LoadXamlApplicationResources()
        {
            Common.DeveloperMode = true;
            Common.WriteToDebugWindow("LoadXamlApplicationResources()");
            Common.DeveloperMode = false;
            try
            {
                // Create a WPF Application
                _XamlApp = new System.Windows.Application();

                // Load the resources

                // This works

                //var resources = System.Windows.Application.LoadComponent(
                //    new Uri("SupportTools_Excel;component/Resources/Xaml/Brushes.xaml", UriKind.Relative)) as System.Windows.ResourceDictionary;

                // Now lets try with

                var resources = System.Windows.Application.LoadComponent(
                    new Uri("SupportTools_Excel;component/Resources/Xaml/Application.xaml", UriKind.Relative)) as System.Windows.ResourceDictionary;

                //var resources = System.Windows.Application.LoadComponent(
                //    new Uri("pack:/SupportTools_Excel;:,,/Resources/Xaml/Application.xaml")) as System.Windows.ResourceDictionary;

                // Merge it on application level

                _XamlApp.Resources.MergedDictionaries.Add(resources);
            }
            catch (Exception ex)
            {
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow(ex.ToString());
                Common.DeveloperMode = false;
            }
        }

        private void UnLoadXamlApplicationResources()
        {
            try
            {
                if (null != _XamlApp)
                {
                    _XamlApp.Shutdown();
                    _XamlApp = null;
                }
            }
            catch (Exception ex)
            {
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow(ex.ToString());
                Common.DeveloperMode = false;
            }
        }
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
