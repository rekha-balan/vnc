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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace VNCAssemblyViewer.User_Interface.Windows
{

    public partial class dxwUserControlHost : DXWindow
    {
        public dxwUserControlHost()
        {
            InitializeComponent();

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        }

        // TODO: Maybe take size and position parameters
        public dxwUserControlHost(string title, string userControlFullyQualifiedName)
        {
            try
            {
                InitializeComponent();

                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                this.Title = title;
                DisplayUserControlFromName(userControlFullyQualifiedName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DisplayUserControlFromName(string userControlName)
        {
            //string typeName = string.Format("VNCAssemblyViewer.User_Interface.User_Controls.{0}",
            //                ((Button)sender).Tag.ToString());

            Type ucType = Type.GetType(userControlName);
            //Type ucType = Type.GetType(typeName);

            try
            {
                var uc = Activator.CreateInstance(ucType);
                ShowUserControl((User_Interface.User_Controls.wucDX_Base)uc);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect Tag Name.  Cannot load type:{0}", userControlName);
            }
        }

        public void ShowUserControl(User_Interface.User_Controls.wucDX_Base control)
        {
            //UnhookTitleEvent(_currentControl);
            g_UserControlContainer.Children.Clear();

            if (control != null)
            {
                g_UserControlContainer.Children.Add(control);

                if (control.MinWidth > 0)
                {
                    this.Width = control.MinWidth + Common.WINDOW_HOSTING_USER_CONTROL_WIDTH_PAD;
                    this.MinWidth = this.Width;
                }

                if (control.MinHeight > 0)
                {
                    this.Height = control.MinHeight + Common.WINDOW_HOSTING_USER_CONTROL_HEIGHT_PAD;
                    this.MinHeight = this.Height;
                }
                //_currentControl = control;
            }

            //HookTitleEvent(_currentControl);
        }
    }
}
