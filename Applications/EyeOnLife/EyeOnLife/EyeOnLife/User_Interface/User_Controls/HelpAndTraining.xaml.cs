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
using SQLInformation;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSQLMonitorAdminMain.xaml
    /// </summary>
    public partial class HelpAndTraining : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        //public event EventHandler TitleChanged2;

        public HelpAndTraining()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        private wucDX_Base _currentControl;

        #region Event Handlers


        private void DisplayUserControlFromTag(object sender, RoutedEventArgs e)
        {
            string controlName = ((Button)sender).Tag.ToString();
            string typeName = string.Format("EyeOnLife.User_Interface.User_Controls.{0}",
                            controlName);

            try
            {
                Type ucType = Type.GetType(typeName);
                var uc = Activator.CreateInstance(ucType);

                if (controlName == "wucDX_Databases")
                {
                    ((Databases)uc).DisplayOptions = new EyeOnLife.User_Interface.Content_Controls.cc2();
                }

                ShowUserControl((wucDX_Base)uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Cannot load type:{0}.  Check Tag Name.\n {1}",
                    typeName,
                    ex));
            }
        }

        private void UnhookTitleEvent(wucDX_Base control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
                //this.TitleChanged2 -= SetTitle2;
            }
        }


        private void HookTitleEvent(wucDX_Base control)
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

            wucDX_Base uc = sender as wucDX_Base;

            if (uc != null && ! string.IsNullOrEmpty(uc.Title))
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

        private void ShowUserControl(wucDX_Base control)
        {
            UnhookTitleEvent(_currentControl);
            dpUserControlContainer.Children.Clear();

            if (control != null)
            {
                dpUserControlContainer.Children.Add(control);
                _currentControl = control;
            }

            HookTitleEvent(_currentControl);
        }

        #endregion

        private void OnPlay1(object sender, RoutedEventArgs e)
        {
            try
            {
                media1.Position = TimeSpan.Zero;
                media1.Play();
            }
            catch (Exception ex)
            {

            }
        }

        private void OnPause1(object sender, RoutedEventArgs e)
        {
            media1.Pause();
        }

        private void OnStop1(object sender, RoutedEventArgs e)
        {
            media1.Stop();
        }

        private void OnPlay2(object sender, RoutedEventArgs e)
        {
            try
            {
                media2.Position = TimeSpan.Zero;
                media2.Play();
            }
            catch (Exception ex)
            {

            }
        }

        private void OnPause2(object sender, RoutedEventArgs e)
        {
            media2.Pause();
        }

        private void OnStop2(object sender, RoutedEventArgs e)
        {
            media2.Stop();
        }

        private void OnPlay3(object sender, RoutedEventArgs e)
        {
            try
            {
                media3.Position = TimeSpan.Zero;
                media3.Play();
            }
            catch (Exception ex)
            {

            }
        }

        private void OnPause3(object sender, RoutedEventArgs e)
        {
            media3.Pause();
        }

        private void OnStop3(object sender, RoutedEventArgs e)
        {
            media3.Stop();
        }


        private void OnPlay4(object sender, RoutedEventArgs e)
        {
            try
            {
                media4.Position = TimeSpan.Zero;
                media4.Play();
            }
            catch (Exception ex)
            {

            }
        }

        private void OnPause4(object sender, RoutedEventArgs e)
        {
            media4.Pause();
        }

        private void OnStop4(object sender, RoutedEventArgs e)
        {
            media4.Stop();
        }


    }
}
