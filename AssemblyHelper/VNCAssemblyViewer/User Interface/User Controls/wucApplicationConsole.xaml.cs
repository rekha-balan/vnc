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
//using SQLInformation;

namespace VNCAssemblyViewer.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSQLMonitorAdminMain.xaml
    /// </summary>
    public partial class wucApplicationConsole : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = VNCAssemblyViewer.ErrorNumbers.VNCAssemblyViewer;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        public wucApplicationConsole()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            InitializeComponent();
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        private wucDX_Base _currentControl;

        #region Event Handlers


        private void DisplayUserControlFromTag(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes): This needs work.  May want to take the event stuff out of
            // AdminButtonEvent.  For now, just check if tag is null.  Means we just clicked
            // a button to be locally handled.

            if (((Button)sender).Tag == null)
            {
                return;
            }

            string typeName = string.Format("{0}.User_Interface.User_Controls.{1}",
                            Common.LOG_APPNAME,
                            ((Button)sender).Tag.ToString());

            Type ucType = Type.GetType(typeName);

            try
            {
                var uc = Activator.CreateInstance(ucType);
                ShowUserControl((wucDX_Base)uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Cannot load type:{0}.  Check Tag Name.\n {1}",
                    typeName,
                    ex.ToString()));
            }
        }


        private void UnhookTitleEvent(wucDX_Base control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
            }
        }

        private void ShowUserControl(wucDX_Base control)
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

        private void HookTitleEvent(wucDX_Base control)
        {
            SetTitle(control, EventArgs.Empty);

            if (control != null)
            {
                control.TitleChanged += SetTitle;
            }

        }

        private void SetTitle(object sender, EventArgs e)
        {
            //ucBase uc = sender as ucBase;

            //if (uc != null && ! string.IsNullOrEmpty(uc.Title))
            //{
            //    this.Title = string.Format("{0}", uc.Title);
            //}
            //else
            //{
            //    this.Title = "main Form";
            //}
        }

        #endregion


    }
}
