using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace VNC_AddLogging
{
    [Title("VNC Add Logging")]
    public partial class VNC_AddLogging : ToolWindowPlugIn
    {
        #region Enums, Fields, Properties, Structures
        
        // This gives access to the control so other code can interact with the controls
        // on the form.

        public static User_Interface.User_Controls_WPF.wucLoggingControlPanel controlPanel;
        #endregion

        //VNC_AddLogging_WPFControls.UserControl1 _wpfControl;


        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
            //_wpfControl = new VNC_AddLogging_WPFControls.UserControl1();
            controlPanel = new User_Interface.User_Controls_WPF.wucLoggingControlPanel();
            elementHost1.Child = controlPanel;
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion
    }
}