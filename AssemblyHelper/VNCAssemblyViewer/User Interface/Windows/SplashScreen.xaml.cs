using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

using VNC;
using VNCAssemblyViewer.User_Interface;
using VNCAssemblyViewer.User_Interface.User_Controls;
using VNCAssemblyViewer.User_Interface.SplashScreens;
using System.Text.RegularExpressions;

namespace VNCAssemblyViewer.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = VNCAssemblyViewer.ErrorNumbers.VNCAssemblyViewer;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        private wucDX_Base _currentControl;

        #region Constructors

        public SplashScreen()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            try
            {
                Theme theme = new Theme("CHR1", "DevExpress.Xpf.Themes.CHR1.v12.2");
                theme.AssemblyName = "DevExpress.Xpf.Themes.CHR1.v12.2";
                Theme.RegisterTheme(theme);

                theme = new Theme("Halloween", "DevExpress.Xpf.Themes.Halloween.v12.2");
                theme.AssemblyName = "DevExpress.Xpf.Themes.Halloween.v12.2";
                Theme.RegisterTheme(theme);
                InitializeComponent();

                ThemeManager.ApplicationThemeName = VNCAssemblyViewer.Data.Config.DefaultUITheme;

                int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

                this.Width = (primaryScreenWidth * 9) / 10;
                this.Height = (primaryScreenHeight * 9) / 10;

                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        #endregion

        #region Initialization

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Common.ApplicationDS.LoadApplicationDSFromDB(Common.ApplicationDS);
            }

            ViewModes viewModes = new ViewModes();
            cbe_UserMode.ItemsSource = viewModes.Items;

            Common.UserMode = new ViewMode(VNCAssemblyViewer.Data.Config.DefaultUserMode);

            //cbe_UserMode.ItemsSource = ViewMode.OptionValues;

            SplashScreenItems ssi = new SplashScreenItems();

            // TODO(crhodes): Clean this up.  This controls how the UI looks on first startup.
            // Make it not hard coded.  Common.UserMode is initialized from app.config.

            if (Common.UserMode.Basic)
            {
                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Basic); ;
            }
            else if (Common.UserMode.Advanced)
            {
                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Advanced);
            }
            else if (Common.UserMode.Administrator)
            {
                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Administrator);
            }
            else if (Common.UserMode.Beta)
            {
                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Beta);
            }

#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        #endregion

        #region Event Handlers

        private void HookTitleEvent(wucDX_Base control)
        {
            SetTitle(control, EventArgs.Empty);

            if (control != null)
            {
                control.TitleChanged += SetTitle;
                //control.TitleChanged2 += SetTitle2;
            }
        }

        private void OnAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win1 = new About();
                win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void OnEditValueChanged_FirstSplash(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            var ev = e;
            var foo = e.NewValue;

            string typeName = string.Format("VNCAssemblyViewer.User_Interface.User_Controls.{0}",
                ((SplashItem)e.NewValue).Value);

            try
            {
                Type ucType = Type.GetType(typeName);
                var uc = Activator.CreateInstance(ucType);
                ShowUserControl((UserControl)uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Cannot load type:{0}.  Check Tag Name.\n {1}",
                    typeName,
                    ex));
            }
        }

        private void OnEditValueChanged_Theme(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            // This is the magic that sets the theme for the entire application!

            ThemeManager.ApplicationThemeName = ((Theme)e.NewValue).Name;
            //ThemeManager.SetTheme(this, ((Theme)e.NewValue).Name);
        }

        private void OnEditValueChanged_UserMode(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            var userMode = e.NewValue;

            Common.UserMode.CurrentMode = (int)userMode;
            VNCAssemblyViewer.Data.Config.DefaultUserMode = (int)userMode;

            SplashScreenItems ssi = new SplashScreenItems();

            if (Common.UserMode.Basic)
            {
                lc_Root.Background = Brushes.Transparent;

                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Basic); ;
            }
            else if (Common.UserMode.Advanced)
            {
                //lc_Root.Background = new SolidColorBrush(Color.FromArgb(0x10, 0x00, 0xFF, 0xC7));
                lc_Root.Background = Brushes.Transparent;

                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Advanced);
            }
            else if (Common.UserMode.Administrator)
            {
                lc_Root.Background = new SolidColorBrush(Color.FromArgb(0x7E, 0xFF, 0x01, 0x01));

                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Administrator);
            }
            else if (Common.UserMode.Beta)
            {
                cbe_SplashScreens.ItemsSource = ssi.Items.Where(um => um.UserMode.Beta);
            }
        }

        private void OnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Go see crhodes");
            //Common.OutputWindow.Show();
        }

        private void OnSendFeedback_Click(object sender, RoutedEventArgs e)
        {
            ExternalProgram.SendFeedback();
        }

        private void UnhookTitleEvent(wucDX_Base control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
            }
        }

        #endregion

        #region Main Function Routines

        private void SetTitle(object sender, EventArgs e)
        {
            wucDX_Base uc = sender as wucDX_Base;

            if (uc != null && !string.IsNullOrEmpty(uc.Title))
            {
                this.Title = string.Format("VNCAssemblyViewer: {0}", uc.Title);
            }
            else
            {
                this.Title = "VNCAssemblyViewer";
            }
        }

        private void SetTitle2(object sender, EventArgs e)
        {
            wucDX_Base uc = sender as wucDX_Base;

            if (uc != null && !string.IsNullOrEmpty(uc.Title))
            {
                this.Title = string.Format("VNCAssemblyViewer: {0}", uc.Title);
            }
            else
            {
                this.Title = "VNCAssemblyViewer";
            }
        }

        private void ShowUserControl(UserControl control)
        {
            UnhookTitleEvent(_currentControl);
            splashScreenGrid.Children.Clear();

            if (control != null)
            {
                splashScreenGrid.Children.Add(control);
                _currentControl = (wucDX_Base)control;
            }

            HookTitleEvent(_currentControl);
        }

        #endregion

        private void OnGetHelpOn(object sender, RoutedEventArgs e)
        {
            string helpTopic = ((Button)sender).Tag.ToString();

            Help.GetHelpOnTopic(helpTopic);
        }

    }
}
