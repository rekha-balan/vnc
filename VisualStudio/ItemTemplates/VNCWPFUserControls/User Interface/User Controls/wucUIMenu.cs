using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.Xpf.Core;
using VNC;

namespace VNCWPFUserControls.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSQLMonitorAdminMain.xaml
    /// </summary>
    public partial class wucUIMenu : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        //public event EventHandler TitleChanged2;

        public wucUIMenu()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        private UserControl _currentControl;
        //private wucDX_Base _currentControl;

        #region Event Handlers


        private void DisplayUserControlFromTag(object sender, RoutedEventArgs e)
        {
            //string controlName = ((Button)sender).Tag.ToString();
            string typeName = ((Button)sender).Tag.ToString();

            // TODO(crhodes)
            // Put this string in config or Common.

            //string typeName = string.Format("VNCWPFUserControls.User_Interface.User_Controls.{0}",
            //                controlName);

            try
            {
                Type ucType = Type.GetType(typeName);
                var uc = Activator.CreateInstance(ucType);

                //if (controlName == "wucDX_Databases")
                //{
                //    ((Databases)uc).DisplayOptions = new EyeOnLife.User_Interface.Content_Controls.cc2();
                //}

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

        private void UnhookTitleEvent(wucDXBase control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
                //this.TitleChanged2 -= SetTitle2;
            }
        }


        private void HookTitleEvent(wucDXBase control)
        {
            SetTitle(control, EventArgs.Empty);

            //SetTitle2(this, EventArgs.Empty);

            if (control != null)
            {
                control.TitleChanged += SetTitle;
                //this.TitleChanged2 += SetTitle2;
            }
        }

        private void SetTitle(object sender, EventArgs e)
        {
            // TODO(crhodes): This needs to raise the event to
            // the window that hosts us.  How to do that??

            wucDXBase uc = sender as wucDXBase;

            if (uc != null && !string.IsNullOrEmpty(uc.Title))
            {
                System.Diagnostics.Debug.WriteLine(this.Title);
                this.Title = string.Format("EOL:{0}", uc.Title);
                Title = uc.Title;
                System.Diagnostics.Debug.WriteLine(this.Title);
            }
            else
            {
                this.Title = "main Form";
            }

            //SetTitle2(sender, e);
        }

        //private void SetTitle2(object sender, EventArgs e)
        //{
        //    if (TitleChanged2 !=null)
        //    {
        //        TitleChanged2(this, EventArgs.Empty);
        //    }
        //}

        private void ShowUserControl(UserControl control)
        {
            //UnhookTitleEvent(_currentControl);
            dpUserControlContainer.Children.Clear();

            if (control != null)
            {
                dpUserControlContainer.Children.Add(control);
                _currentControl = control;
            }

            //HookTitleEvent(_currentControl);
        }

        #endregion

    }
}
