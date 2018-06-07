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
    /// <summary>
    /// Interaction logic for wndDX_LKUPTables.xaml
    /// </summary>
    public partial class wndDX_LKUPTables : DXWindow
    {
        public wndDX_LKUPTables()
        {
            InitializeComponent();
            this.Width = SQLInformation.Data.Config.ScreenWidth_Explore;
            this.Height = SQLInformation.Data.Config.ScreenHeight_Explore;
        }
    }
}
