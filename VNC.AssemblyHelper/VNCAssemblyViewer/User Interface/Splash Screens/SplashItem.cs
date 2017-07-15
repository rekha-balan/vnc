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
//using VNC;
using VNCAssemblyViewer.User_Interface.User_Controls;

namespace VNCAssemblyViewer.User_Interface.SplashScreens
{
    public class SplashItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ViewMode UserMode { get; set; }

        public SplashItem()
        {

        }

        public SplashItem(string name, string value, ViewMode userMode)
        {
            Name = name;
            Value = value;
            UserMode = userMode;
        }
    }
}
