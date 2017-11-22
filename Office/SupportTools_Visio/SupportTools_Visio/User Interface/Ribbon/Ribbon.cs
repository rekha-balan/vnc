using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using Visio=Microsoft.Office.Interop.Visio;

namespace SupportTools_Visio
{
    public partial class Ribbon
    {

        #region Constructors
        #endregion

        #region Event Handlers

        private void btnAddColorSupport_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.AddColorSupportToSelection();
        }

        private void btnAddIDAndTextSupport_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.Add_IDandTextSupport_ToSelection();
        }

        private void btnAddIDSupport_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.Add_IDSupport_ToSelection();
        }

        private void btnAddTextControl_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.Add_TextControl_ToSelection();
        }

        private void btnDisplayPageNames_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.DisplayPageNames();
        }

        private void btnLoadLayers_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Application.LayerManager();
        }

        private void btnAddDefaultLayers_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.AddDefaultLayers();
        }

        private void btnAddDefaultLayers_Page_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.AddDefaultLayers();
        }

        private void btnAddHeaderAndFooter_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.AddHeaderAndFooter();
        }

        private void btnAddHyperLink_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.AddHyperlinkToPage_FromShapeText();
        }

        private void btnAddInInfo_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayAddInInfo();
        }

        private void btnAddIsPageName_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.Add_User_IsPageName();
        }

        private void btnAddNavigationLinks_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.AddNavigationLinks();
        }

        private void btnAddNavLinks_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.AddNavigationLinks(Globals.ThisAddIn.Application.ActivePage);
        }

        private void btnAddTableOfContents_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.CreateTableOfContents();
        }

        private void btnAllPageOff_Click(object sender, RibbonControlEventArgs e)
        {
            string layerName = cmbLayers.Text;

            if (layerName.Length > 0)
            {
                Actions.Visio_Document.DisplayLayer(layerName, false);
            }
        }

        private void btnAllPageOn_Click(object sender, RibbonControlEventArgs e)
        {
            string layerName = cmbLayers.Text;

            if (layerName.Length > 0)
            {
                Actions.Visio_Document.DisplayLayer(layerName, true);
            }
        }

        private void btnAppUtilities_Click(object sender, RibbonControlEventArgs e)
        {
            if (Common.TaskPaneAppUtil == null)
            {
                Common.TaskPaneAppUtil = VNC.AddinHelper.TaskPaneUtil.AddTaskPane(new User_Interface.Task_Panes.TaskPane_AppUtil(), "App Utilities", Globals.ThisAddIn.CustomTaskPanes);
            }
            else
            {
                Common.TaskPaneAppUtil.Visible = !Common.TaskPaneAppUtil.Visible;
            }
        }

        private void btnDebugWindow_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayDebugWindow();
        }

        private void btnDeletePages_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.DeletePages();
        }

        private void btnDeveloperMode_Click(object sender, RibbonControlEventArgs e)
        {
            ToggleDeveloperMode();
        }

        private void btnGetApplicationInfo_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Application.DisplayInfo();
        }

        private void btnGetDocumentInfo_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.DisplayInfo();
        }

        private void btnGetPageInfo_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.GatherInfo(Globals.ThisAddIn.Application.ActivePage);
        }

        private void btnGetShapeInfo_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.GatherInfo();
        }

        private void btnGetStencilInfo_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Stencil.GatherInfo();
        }

        private void btnHilight_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmEditVisio();
            frm.Show();
        }

        private void btnMakeLinkableMaster_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.MakeLinkableMaster();
        }

        private void btnNavigateDown_Click(object sender, RibbonControlEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("TODO: Navigate Down");
        }

        private void btnNavigateUp_Click(object sender, RibbonControlEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("TODO: Navigate Up");
        }

        private void btnPageOff_Click(object sender, RibbonControlEventArgs e)
        {
            string layerName = cmbLayers.Text;

            if (layerName.Length > 0)
            {
                Actions.Visio_Page.DisplayLayer(Globals.ThisAddIn.Application.ActivePage, layerName, false);
            }
        }

        private void btnPageOn_Click(object sender, RibbonControlEventArgs e)
        {
            string layerName = cmbLayers.Text;

            if (layerName.Length > 0)
            {
                Actions.Visio_Page.DisplayLayer(Globals.ThisAddIn.Application.ActivePage, layerName, true);
            }
        }

        private void btnPrintPage_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.PrintPage();
        }

        private void btnPrintPages_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.PrintPages();
        }

        private void btnRelatedIntfrastructure_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmRelateShape();
            frm.Show();
        }

        private void btnRelatedProcess_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmRelateShape();
            frm.Show();
        }

        private void btnRelatedSystem_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmRelateShape();
            frm.Show();
        }

        private void btnRemoveLayers_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.RemoveLayers();
        }

        private void btnRemoveLayers_Page_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.RemoveLayers();
        }

        private void btnRetrive_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmRetrieveShape();
            frm.Show();
        }

        private void btnSavePage_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.SavePage(Globals.ThisAddIn.Application.ActivePage);
        }

        private void btnSavePages_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.SavePages();
        }

        private void btnShapeEditor_Click(object sender, RibbonControlEventArgs e)
        {
            using (User_Interface.Forms.frmShapeEditor frmF = new User_Interface.Forms.frmShapeEditor())
            {
                try
                {
                    if (DialogResult.Cancel == frmF.ShowDialog()) // modal
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: EntLib
                    MessageBox.Show(ex.ToString());
                    throw (ex);
                }
                finally
                {

                }
            }
        }

        private void btnShapeEditor2_Click(object sender, RibbonControlEventArgs e)
        {
            User_Interface.Forms.frmShapeEditor frmF = new User_Interface.Forms.frmShapeEditor();

            frmF.Show();    // Modeless
        }

        private void btnSortAllPages_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.SortAllPages();
        }

        private void btnSyncPageNames_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.SyncPageNames();
        }

        private void btnSyncPageNamesPage_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.SyncPageNames();
        }

        private void btnUpdatePageNameShapes_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Document.UpdatePageNameShapes();
        }

        private void btnUpdatePageNameShapesPage_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Page.UpdatePageNameShapes(Globals.ThisAddIn.Application.ActivePage);
        }

        private void btnValidate_Click(object sender, RibbonControlEventArgs e)
        {
            var frm = new User_Interface.Forms.frmRetrieveShape();
            frm.Show();
        }

        private void btnWatchWindow_Click(object sender, RibbonControlEventArgs e)
        {
            DisplayWatchWindow();
        }

        private void btnWebPage_Click(object sender, RibbonControlEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("TODO: Navigate to Web Page");
        }

        private void btnZeroMargins_Click(object sender, RibbonControlEventArgs e)
        {
            Actions.Visio_Shape.ZeroMargins();
        }

        private void chkDisplayChattyEvents_Click(object sender, RibbonControlEventArgs e)
        {
            Common.DisplayChattyEvents = chkDisplayChattyEvents.Checked;
        }
        private void chkDisplayEvents_Click(object sender, RibbonControlEventArgs e)
        {
            Common.DisplayEvents = chkDisplayEvents.Checked;
        }

        private void chkEnableAppEvents_Click(object sender, RibbonControlEventArgs e)
        {
            Common.HasAppEvents = chkEnableAppEvents.Checked;

            if (Common.HasAppEvents)
            {
                if (Common.AppEvents == null)
                {
                    Common.AppEvents = new Events.VisioAppEvents();
                    Common.AppEvents.VisioApplication = Globals.ThisAddIn.Application;
                }
            }
            else
            {
                Common.AppEvents = null;
            }
        }

        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        #endregion

        #region Main Function Routines

        private void DisplayAddInInfo()
        {
            VNC.AddinHelper.AddInInfo.DisplayInfo();
        }

        private void DisplayDebugWindow()
        {
            if (VNC.AddinHelper.Common.DebugWindow.Visible)
            {
                VNC.AddinHelper.Common.DebugWindow.Visible = false;
            }
            else
            {
                VNC.AddinHelper.Common.DebugWindow.Visible = true;
            }
        }

        private void DisplayWatchWindow()
        {
            VNC.AddinHelper.Common.WatchWindow.Visible = ! VNC.AddinHelper.Common.WatchWindow.Visible;
        }

        private void ToggleDeveloperMode()
        {
            VNC.AddinHelper.Common.DeveloperMode = ! VNC.AddinHelper.Common.DeveloperMode;
            Globals.Ribbons.Ribbon.grpDebug.Visible = VNC.AddinHelper.Common.DeveloperMode;
        }


        #endregion

        private void btnLaunchWPFCommandCockpit_Click(object sender, RibbonControlEventArgs e)
        {
            User_Interface.Forms.frmWPFUserInterface frmF = new User_Interface.Forms.frmWPFUserInterface();

            frmF.Show();    // Modeless

            //using (User_Interface.Forms.frmWPFUserInterface frmF = new User_Interface.Forms.frmWPFUserInterface())
            //{
            //    try
            //    {
            //        if (DialogResult.Cancel == frmF.ShowDialog()) // modal
            //        {
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // TODO: EntLib
            //        MessageBox.Show(ex.ToString());
            //        throw (ex);
            //    }
            //    finally
            //    {

            //    }
            //}
        }

        private void btnCommandCockpitWPFWindow_Click(object sender, RibbonControlEventArgs e)
        {
            User_Interface.Windows.DXRibbonWindowMain wndW = new User_Interface.Windows.DXRibbonWindowMain();
            //User_Interface.Windows.MainWindow wndW = new User_Interface.Windows.MainWindow();

            wndW.Show();
        }
    }
}
