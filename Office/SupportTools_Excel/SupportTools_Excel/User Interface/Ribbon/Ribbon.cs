using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

using ExcelHlp = VNC.AddinHelper.Excel;
using TFSHlp = VNC.TFS.Helper;
using VNCHlp = VNC.AddinHelper;

namespace SupportTools_Excel
{
    public partial class Ribbon
    {

        #region Event Handlers

        private void btnExcelUtilities_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneUtilities = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_Utilities(), "Excel Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneUtilities.Width = Common.TaskPaneUtilities.Control.Width;
            Common.TaskPaneUtilities.Visible = ! Common.TaskPaneUtilities.Visible;
        }

        private void btnSMO_Click(object sender, RibbonControlEventArgs e)
        {
            if (Common.TaskPaneSMO == null)
            {
                Common.TaskPaneSMO = VNCHlp.TaskPaneUtil.AddTaskPane(
                    new User_Interface.Task_Panes.TaskPane_SMO(), 
                    "SMO Utilities", Globals.ThisAddIn.CustomTaskPanes);
                // This works if the minimum size for the control has been set.
                Common.TaskPaneSMO.Width = Common.TaskPaneSMO.Control.Width;
            }
            else
            {
                Common.TaskPaneSMO.Visible = !Common.TaskPaneSMO.Visible;
            }
        }

        //private void btnTPDevelopment_Click(object sender, RibbonControlEventArgs e)
        //{
        //    if (Common.TaskPaneDevelopment == null)
        //    {
        //        Common.TaskPaneDevelopment = VNCHlp.TaskPaneUtil.AddTaskPane(
        //            new User_Interface.Task_Panes.TaskPane_Developer(), 
        //            "Developer Utilities", Globals.ThisAddIn.CustomTaskPanes);
        //        // This works if the minimum size for the control has been set.
        //        Common.TaskPaneDevelopment.Width = Common.TaskPaneDevelopment.Control.Width;
        //    }
        //    else
        //    {
        //        Common.TaskPaneDevelopment.Visible = !Common.TaskPaneDevelopment.Visible;
        //    }
        //}

        private void btnTPDevelopment_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneDevelopment = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_Developer(), "Developer Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneDevelopment.Width = Common.TaskPaneDevelopment.Control.Width;
            Common.TaskPaneDevelopment.Visible = !Common.TaskPaneDevelopment.Visible;
        }

        private void ddTheme_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            // TODO(crhodes):
            // This doesn't work.  Try putting it in Support Tools
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.MetropolisLightName;

            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = ((RibbonDropDown)sender).SelectedItem.Label;
        }

        private void btnActiveDirectory_Click(object sender, RibbonControlEventArgs e)
        {
            if (Common.TaskPaneActiveDirectory == null)
            {
                Common.TaskPaneActiveDirectory = VNCHlp.TaskPaneUtil.AddTaskPane(
                    new User_Interface.Task_Panes.TaskPane_ActiveDirectory(), 
                    "Active Directory Utilities", Globals.ThisAddIn.CustomTaskPanes);
                // This works if the minimum size for the control has been set.
                Common.TaskPaneActiveDirectory.Width = Common.TaskPaneActiveDirectory.Control.Width;
            }
            else
            {
                Common.TaskPaneActiveDirectory.Visible = !Common.TaskPaneActiveDirectory.Visible;
            }
        }

        private void btnAddInInfo_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayAddInInfo();
        }

        private void btnDebugWindow_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayDebugWindow();
        }

        private void btnDeveloperMode_Click(object sender, RibbonControlEventArgs e)
        {
            ToggleDeveloperMode();
        }

        private void btnLoadSalesforceRallyInfo_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmSalesforceRallyInfo();
            frm.Show();
        }

        private void btnLoadWPFHost_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmWPFHost();
            frm.Show();
        }

        private void btnRally_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneRally = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_Rally(), "Rally Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneRally.Width = Common.TaskPaneRally.Control.Width;
            Common.TaskPaneRally.Visible = !Common.TaskPaneRally.Visible;
        }

        private void btnSalesforce_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneSalesforce = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_Salesforce(), "Salesforce Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneSalesforce.Width = Common.TaskPaneSalesforce.Control.Width;
            Common.TaskPaneSalesforce.Visible = !Common.TaskPaneSalesforce.Visible;
        }

        private void btnSharePoint_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneSharePoint = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_SharePoint(), "SharePoint Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneSharePoint.Width = Common.TaskPaneSharePoint.Control.Width;
            Common.TaskPaneSharePoint.Visible = !Common.TaskPaneSharePoint.Visible;
        }

        private void btnTFS_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneTFS = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_TFS(), "TFS Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneTFS.Width = Common.TaskPaneTFS.Control.Width;
            Common.TaskPaneTFS.Visible = !Common.TaskPaneTFS.Visible;
        }

        private void btnAppUtilities_Click(object sender, RibbonControlEventArgs e)
        {
            //if(ThisAddIn.TaskPaneExcelUtil == null)
            //{
            //    ThisAddIn.TaskPaneExcelUtil = VNCHlp.TaskPaneUtil.AddTaskPane(new User_Interface.Task_Panes.TaskPane_ExcelUtil(), "Excel Utilities", Globals.ThisAddIn.CustomTaskPanes);
            //    // This works if the minimum size for the control has been set.
            //    ThisAddIn.TaskPaneExcelUtil.Width = ThisAddIn.TaskPaneExcelUtil.Control.Width;
            //}
            //else
            //{
            //    ThisAddIn.TaskPaneExcelUtil.Visible = !ThisAddIn.TaskPaneExcelUtil.Visible;
            //}

            //string taskPaneName = string.Format("{0}({1})", "Excel Utilities", Globals.ThisAddIn.Application.Hwnd);

            //if (Common.TaskPaneExcelUtil == null)
            //{
            Common.TaskPaneExcelUtil = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_ExcelUtil(), "Excel Utilities", 
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

                //Common.TaskPaneExcelUtil = VNCHlp.TaskPaneUtil.AddTaskPane(new User_Interface.Task_Panes.TaskPane_ExcelUtil(), "Excel Utilities", Globals.ThisAddIn.CustomTaskPanes);
                // This works if the minimum size for the control has been set.
            Common.TaskPaneExcelUtil.Width = Common.TaskPaneExcelUtil.Control.Width;
            Common.TaskPaneExcelUtil.Visible = !Common.TaskPaneExcelUtil.Visible;
            //}
            //else
            //{
            //    Common.TaskPaneExcelUtil.Visible = !Common.TaskPaneExcelUtil.Visible;
            //}
        }

        //private void btnITRs_Click(object sender, RibbonControlEventArgs e)
        //{
        //    if(Common.TaskPaneITRs == null)
        //    {
        //        Common.TaskPaneITRs = AddinHelper.TaskPaneUtil.AddTaskPane(new User_Interface.Task_Panes.TaskPane_ITRs(), "ITRs", Globals.ThisAddIn.CustomTaskPanes);
        //                        // This works if the minimum size for the control has been set.
        //          Common.TaskPaneITRs.Width = Common.TaskPaneITRs.Control.Width;
        //    }
        //    else
        //    {
        //        Common.TaskPaneITRs.Visible = ! Common.TaskPaneITRs.Visible;
        //    }
        //}

        private void btnLogParser_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneLogParser = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_LogParser(), "Log Parser",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneLogParser.Width = Common.TaskPaneLogParser.Control.Width;
            Common.TaskPaneLogParser.Visible = !Common.TaskPaneLogParser.Visible;
        }

        private void btnLTC_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneLTC = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_LTC(), "LTC Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneLTC.Width = Common.TaskPaneLTC.Control.Width;
            Common.TaskPaneLTC.Visible = !Common.TaskPaneLTC.Visible;
        }

        private void btnMTreaty_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneMTreaty = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_MTreaty(), "MTreaty Utilities",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneMTreaty.Width = Common.TaskPaneMTreaty.Control.Width;
            Common.TaskPaneMTreaty.Visible = !Common.TaskPaneMTreaty.Visible;
        }

        private void btnNetworkTraces_Click(object sender, RibbonControlEventArgs e)
        {
            Common.TaskPaneNetworkTrace = VNCHlp.TaskPaneUtil.GetTaskPane(
                () => new User_Interface.Task_Panes.TaskPane_NetworkTrace(), "Network Traces",
                Globals.ThisAddIn.CustomTaskPanes, Globals.ThisAddIn.Application.Hwnd.ToString());

            // This works if the minimum size for the control has been set.
            Common.TaskPaneNetworkTrace.Width = Common.TaskPaneNetworkTrace.Control.Width;
            Common.TaskPaneNetworkTrace.Visible = !Common.TaskPaneNetworkTrace.Visible;
        }

        private void btnSQLSMO_Click(object sender, RibbonControlEventArgs e)
        {
            if (Common.TaskPaneSQLSMO == null)
            {
                Common.TaskPaneSQLSMO = VNCHlp.TaskPaneUtil.AddTaskPane(new User_Interface.Task_Panes.TaskPane_SQLSMO(), "SQL SMO", Globals.ThisAddIn.CustomTaskPanes);
                // This throws an exception
                //Globals.ThisAddIn.Application.CommandBars["SQL SMO"].Width = Common.TaskPaneSQLSMO.Width;
                foreach (Microsoft.Office.Core.CommandBar bar in Globals.ThisAddIn.Application.CommandBars)
                {
                    string foo = bar.Name;

                    if (foo == "SQL SMO")
                    {
                        // Which is curious as the bar is found!
                        //Globals.ThisAddIn.Application.CommandBars["SQL SMO"].Width = Common.TaskPaneSQLSMO.Width;
                    }
                }

                // This works if the minimum size for the control has been set.
                Common.TaskPaneSQLSMO.Width = Common.TaskPaneSQLSMO.Control.Width;
            }
            else
            {
                Common.TaskPaneSQLSMO.Visible = !Common.TaskPaneSQLSMO.Visible;
            }
        }

        private void btnWatchWindow_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayWatchWindow();
        }

        private void chkDisplayEvents_Click(object sender, RibbonControlEventArgs e)
        {
            Common.DisplayEvents = chkDisplayEvents.Checked;
        }

        private void chkEnableAppEvents_Click(object sender, RibbonControlEventArgs e)
        {
            Common.HasAppEvents = chkEnableAppEvents.Checked;

            if(Common.HasAppEvents)
            {
                if(Common.AppEvents == null)
                {
                    Common.AppEvents = new Events.ExcelAppEvents();
                    Common.AppEvents.ExcelApplication = Globals.ThisAddIn.Application;
                }
            }
            else
            {
                Common.AppEvents = null;
                Common.AppEvents.ExcelApplication = null;
            }
        }

        private void chkScreenUpdates_Click(object sender, RibbonControlEventArgs e)
        {
            ExcelHlp.DisplayScreenUpdates = chkScreenUpdates.Checked;
        }
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }
        #endregion

        #region Main Function Routines


        private void DisplayAddInInfo()
        {
            VNCHlp.AddInInfo.DisplayInfo();
        }

        private void DisplayDebugWindow()
        {
            if(VNCHlp.Common.DebugWindow.Visible)
            {
                VNCHlp.Common.DebugWindow.Visible = false;
            }
            else
            {
                VNCHlp.Common.DebugWindow.Visible = true;
            }
        }

        private void DisplayWatchWindow()
        {
            VNCHlp.Common.WatchWindow.Visible = !VNCHlp.Common.WatchWindow.Visible;
        }

        private void ToggleDeveloperMode()
        {
            VNCHlp.Common.DeveloperMode = !VNCHlp.Common.DeveloperMode;
            Globals.Ribbons.Ribbon.grpDebug.Visible = VNCHlp.Common.DeveloperMode;
        }

        #endregion
    }
}
