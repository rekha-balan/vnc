using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

using VNC;
using XlHlp = VNC.AddinHelper.Excel;

namespace SupportTools_Excel
{
    public partial class ThisAddIn
    {
        private static System.Windows.Application _XamlApp;
        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneExcelUtil;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                AppLog.Trace("Enter", Common.PROJECT_NAME);
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow("ThisAddIn_Startup()");
                Common.DeveloperMode = false;

                Globals.Ribbons.Ribbon.chkDisplayEvents.Checked = Common.DisplayEvents;
                Globals.Ribbons.Ribbon.chkEnableAppEvents.Checked = Common.HasAppEvents;

                if (Common.HasAppEvents)
                {
                    Common.AppEvents = new Events.ExcelAppEvents();
                    Common.AppEvents.ExcelApplication = Globals.ThisAddIn.Application;
                }

                XlHlp.ExcelApplication = Globals.ThisAddIn.Application;

                // For the WPF stuff we do

                LoadXamlApplicationResources();

                AppLog.Trace("Exit", Common.PROJECT_NAME);
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, Common.PROJECT_NAME);
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow(ex.ToString());
                Common.DeveloperMode = false;
                throw (ex);
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            try
            {
                AppLog.Trace("Enter", Common.PROJECT_NAME);
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow("ThisAddIn_Shutdown()");
                Common.DeveloperMode = false;

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
                Common.DeveloperMode = true;
                Common.WriteToDebugWindow(ex.ToString());
                Common.DeveloperMode = false;
                throw (ex);
            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += ThisAddIn_Startup;
            this.Shutdown += ThisAddIn_Shutdown;
        }

        #endregion

        /// <summary>
        /// LoadXamlApplicationResources
        ///
        /// Creates Xaml Resources collection in System.Windows.Application
        /// for use in Hosted applications without App.Xaml
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
    }
}
