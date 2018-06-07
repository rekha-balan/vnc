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

    public partial class wndDX_ServerRoles : DXWindow
    {
        public wndDX_ServerRoles()
        {
            InitializeComponent();
            this.Width = SQLInformation.Data.Config.ScreenWidth_Explore;
            this.Height = SQLInformation.Data.Config.ScreenHeight_Explore;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        }
    }
}
