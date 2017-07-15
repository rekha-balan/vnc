using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace VNCAssemblyViewer.User_Interface
{
    public class Helper
    {
        public static void DisplayAdminUserControl(string title, string userControlName)
        {
            try
            {
                var win1 = new VNCAssemblyViewer.User_Interface.Windows.dxwUserControlHost(title, userControlName);
                win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        public static void ValidateDataFullyLoaded()
        {
            while (!VNCAssemblyViewer.Common.DataFullyLoaded)
            {
                MessageBox.Show("Data not fully loaded yet");
                Thread.Sleep(2000);
            }
        }
    }
}
