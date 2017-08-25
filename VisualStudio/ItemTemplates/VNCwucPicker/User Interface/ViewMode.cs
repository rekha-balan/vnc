﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpf.Docking;

using VNCwucPicker.User_Interface.User_Controls;
using VNC;

namespace VNCwucPicker.User_Interface
{
    public class ViewMode
    {
        //private static int CLASS_BASE_ERRORNUMBER = VNCwucPicker.ErrorNumbers.VNCwucPicker;
        //private const string LOG_APPNAME = "VNCwucPicker";

        public enum Mode : int
        {
            Basic = 1,
            Advanced = 2,
            Administrator = 4,
            Beta = 8
        }

        #region Constructors

        public ViewMode(int currentMode)
        {
            _CurrentMode = currentMode;
            InitOptionValues();
        }

        #endregion

        #region Properties

        private bool _Administrator = false;
        private bool _Advanced = false;
        private bool _Basic = false;
        private bool _Beta = false;
        private int _CurrentMode;

        private static Dictionary<string, int> _OptionValues;

        public bool Administrator
        {
            get { return (_CurrentMode & (int)Mode.Administrator) > 0; }
            set
            {
                _Administrator = value;
            }
        }

        public bool Advanced
        {
            get { return (_CurrentMode & (int)Mode.Advanced) > 0; }
            set
            {
                _Advanced = value;
            }
        }

        public bool Basic
        {
            get { return (_CurrentMode & (int)Mode.Basic) > 0; }
            set
            {
                _Basic = value;
            }
        }

        public bool Beta
        {
            get { return (_CurrentMode & (int)Mode.Beta) > 0; }
            set
            {
                _Beta = value;
            }
        }
        public int CurrentMode
        {
            get { return _CurrentMode; }
            set
            {
                _CurrentMode = value;
            }
        }

        public static Dictionary<string, int> OptionValues
        {
            get { return _OptionValues; }
        }
        #endregion

        #region Main Function Routines

        public static void ApplyAuthorization(DXWindow control)
        {

            //UserMode.DisplayOptionsVisibility(cc_DisplayOptions);

            //UserMode.DetailPaneVisibility(lc_ItemDetail);

            //UserMode.LayoutPanelVisibility(lp_ToolBox, false);
            //UserMode.LayoutPanelVisibility(lp_AdminToolBox, true);

            //UserMode.SnapShotDetailsVisibility(lc_SnapShotDetails);

            //UserMode.AutoHideGroupVisibility(ahg_Left);
            //UserMode.AutoHideGroupVisibility(ahg_Top);
            //UserMode.AutoHideGroupVisibility(ahg_Right);
            //UserMode.AutoHideGroupVisibility(ahg_Bottom);
        }

        public static void ApplyAuthorization(UserControl control)
        {

            var found = control.FindName("cc_DisplayOptions");
            DisplayOptionsVisibility((ContentControl)found);

            found = control.FindName("lc_ItemDetail");
            DetailPaneVisibility((LayoutControl)found);

            found = control.FindName("lp_ToolBox");
            LayoutPanelVisibility((LayoutPanel)found, false);

            found = control.FindName("lp_AdminToolBox");
            LayoutPanelVisibility((LayoutPanel)found, true);

            found = control.FindName("wp_AdminTools");
            WrapPanelVisibility((WrapPanel)found, true);

            found = control.FindName("lc_SnapShotDetails");
            SnapShotDetailsVisibility((LayoutControl)found);

            found = control.FindName("lc_ExpandDetails");
            SnapShotDetailsVisibility((LayoutControl)found);

            found = control.FindName("ahg_Left");
            AutoHideGroupVisibility((AutoHideGroup)found);

            found = control.FindName("ahg_Top");
            AutoHideGroupVisibility((AutoHideGroup)found);

            found = control.FindName("ahg_Right");
            AutoHideGroupVisibility((AutoHideGroup)found);

            found = control.FindName("ahg_Bottom");
            AutoHideGroupVisibility((AutoHideGroup)found);

        }

        public static void AutoHideGroupVisibility(AutoHideGroup control)
        {
            if (control == null)
            {
                VNC.AppLog.Warning("control is null", Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                control.Visibility = Visibility.Visible;
            }
            else
            {
                control.Visibility = Visibility.Hidden;
            }
        }

        public static void DetailPaneVisibility(LayoutControl control)
        {
            if (control == null)
            {
                VNC.AppLog.Warning("control is null", Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                control.Visibility = Visibility.Visible;
            }
            else
            {
                control.Visibility = Visibility.Hidden;
            }
        }

        public static void DisplayOptionsVisibility(ContentControl control)
        {
            var adminOptions = VNC.Xaml.PhysicalTree.FindChild<WrapPanel>(control, "AdminOptions");

            if (adminOptions == null)
            {
                VNC.AppLog.Warning(string.Format("Can't locate element {0}", "AdminOptions"), Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator)
            {
                ((WrapPanel)adminOptions).Visibility = Visibility.Visible;
            }
            else
            {
                ((WrapPanel)adminOptions).Visibility = Visibility.Hidden;
            }

            var developerOptions = VNC.Xaml.PhysicalTree.FindChild<WrapPanel>(control, "DeveloperOptions");

            if (developerOptions == null)
            {
                VNC.AppLog.Warning(string.Format("Can't locate element {0}", "DeveloperOptions"), Common.LOG_APPNAME);
                return;
            }

            if (Common.DeveloperMode)
            {
                ((WrapPanel)developerOptions).Visibility = Visibility.Visible;
            }
            else
            {
                ((WrapPanel)developerOptions).Visibility = Visibility.Hidden;
            }
        }

        private void InitOptionValues()
        {
            // TODO(crhodes): Do this with reflection on the Expand Enum.

            List<ViewModeItem> viewModeList = new List<ViewModeItem>();

            viewModeList.Add(new ViewModeItem("Basic", "Basic", Mode.Basic));
            viewModeList.Add(new ViewModeItem("Expanded", "Advanced", Mode.Advanced));
        }


        public static void LayoutPanelVisibility(LayoutPanel control, bool RequiresAdmin)
        {
            if (control == null)
            {
                VNC.AppLog.Warning("control is null", Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                if (RequiresAdmin)
                {
                    if (Common.UserMode.Administrator)
                    {
                        control.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        control.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    control.Visibility = Visibility.Visible;
                }
            }
            else
            {
                control.Visibility = Visibility.Hidden;
            }
        }

        // TODO(crhodes): Figure out why FindChild does not find deeply embedded content control.
        public static void ReflectUserMode(DXWindow window)
        {
            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                var cc_DisplayOptions = VNC.Xaml.PhysicalTree.FindChild<ContentControl>(window, "cc_DisplayOptions");

                if (cc_DisplayOptions == null)
                {
                    VNC.AppLog.Warning(string.Format("Can't locate element {0}", "cc_DisplayOptions"), Common.LOG_APPNAME);
                    return;
                }

                var adminOptions = VNC.Xaml.PhysicalTree.FindChild<WrapPanel>(cc_DisplayOptions, "AdminOptions");

                if (adminOptions == null)
                {
                    VNC.AppLog.Warning(string.Format("Can't locate element {0}", "adminOptions"), Common.LOG_APPNAME);
                    return;
                }

                ((WrapPanel)adminOptions).Visibility = Visibility.Visible;
            }
        }

        public static void ReflectUserMode(wucDX_Base window)
        {
            var cc_DisplayOptions = VNC.Xaml.PhysicalTree.FindChild<ContentControl>(window, "cc_DisplayOptions");

            if (cc_DisplayOptions == null)
            {
                VNC.AppLog.Warning(string.Format("Can't locate element {0}", "cc_DisplayOptions"), Common.LOG_APPNAME);
                return;
            }

            var adminOptions = VNC.Xaml.PhysicalTree.FindChild<WrapPanel>(cc_DisplayOptions, "AdminOptions");

            if (adminOptions == null)
            {
                VNC.AppLog.Warning(string.Format("Can't locate element {0}", "adminOptions"), Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                ((WrapPanel)adminOptions).Visibility = Visibility.Visible;
            }
            else
            {
                ((WrapPanel)adminOptions).Visibility = Visibility.Hidden;
            }
        }

        public static void SnapShotDetailsVisibility(LayoutControl control)
        {
            if (control == null)
            {
                VNC.AppLog.Warning("control is null", Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator)
            {
                control.Visibility = Visibility.Visible;
            }
            else
            {
                control.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Indicate the ckDisplayColumns[] checkboxes are checked.
        /// The corresponding grid columns should start out in the checked state.
        /// </summary>
        /// <param name="contentControl"></param>
        /// <param name="ckDisplayColumns"></param>
        public static void UpdateDisplayColumnsCheckBoxes(ContentControl contentControl, string[] ckDisplayColumns)
        {
            if (ckDisplayColumns != null)
            {
                foreach (string ckDisplayColumn in ckDisplayColumns)
                {
                    var displayColumn = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(contentControl, ckDisplayColumn);
                    if (displayColumn != null)
                    {
                        // Not clear why this is null on the first load of the program??
                        ((CheckBox)displayColumn).IsChecked = true;
                    }
                }
            }
        }

        public static void WrapPanelVisibility(WrapPanel control, bool RequiresAdmin)
        {
            if (control == null)
            {
                VNC.AppLog.Warning("control is null", Common.LOG_APPNAME);
                return;
            }

            if (Common.UserMode.Administrator || Common.UserMode.Advanced)
            {
                if (RequiresAdmin)
                {
                    if (Common.UserMode.Administrator)
                    {
                        control.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        control.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    control.Visibility = Visibility.Visible;
                }
            }
            else
            {
                control.Visibility = Visibility.Hidden;
            }
        }

        #endregion
    }
}
