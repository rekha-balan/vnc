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


namespace EyeOnLife.User_Interface.Windows
{

    public partial class wndDX_AdminHost : DXWindow
    {
        public wndDX_AdminHost()
        {
            InitializeComponent();

            int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            this.Width = (primaryScreenWidth * 9) / 10;
            this.Height = (primaryScreenHeight * 9) / 10;

            //this.Width = SQLInformation.Data.Config.ScreenWidth_Explore;
            //this.Height = SQLInformation.Data.Config.ScreenHeight_Explore;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        }

        public wndDX_AdminHost(string title, string userControlName)
        {
            try
            {
                InitializeComponent();

                int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

                this.Width = (primaryScreenWidth * 9) / 10;
                this.Height = (primaryScreenHeight * 9) / 10;
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                this.Title = title;
                DisplayUserControlFromTag(userControlName);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void DisplayUserControlFromTag(string userControlName)
        {
            //string typeName = string.Format("EyeOnLife.User_Interface.User_Controls.{0}",
            //                ((Button)sender).Tag.ToString());

            Type ucType = Type.GetType(userControlName);
            //Type ucType = Type.GetType(typeName);

            try
            {
                var uc = Activator.CreateInstance(ucType);
                ShowUserControl((User_Interface.ucBase)uc);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect Tag Name.  Cannot load type:{0}", userControlName);
            }
        }

        private void ShowUserControl(ucBase control)
        {
            //UnhookTitleEvent(_currentControl);
            dpUserControlContainer.Children.Clear();

            if (control != null)
            {
                dpUserControlContainer.Children.Add(control);
                //_currentControl = control;
            }

            //HookTitleEvent(_currentControl);
        }
    }
}
