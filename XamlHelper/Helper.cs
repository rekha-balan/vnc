using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace VNC.Xaml
{
    public static class Helper
    {
        public static void DisableAndHide(System.Windows.Controls.Control control)
        {
            control.IsEnabled = false;
            control.Visibility = Visibility.Hidden;
        }
        public static void EnableAndShow(System.Windows.Controls.Control control)
        {
            control.IsEnabled = true;
            control.Visibility = Visibility.Visible;
        }
    }
}