using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VNCXaml = VNC.Xaml;

namespace SupportTools_Excel.User_Interface.Forms
{
    public partial class frmSalesforceRallyInfo : Form
    {
        public frmSalesforceRallyInfo()
        {
            //// Create a WPF Application
            //var app = new System.Windows.Application();

            //// Load the resources

            //// This works

            ////var resources = System.Windows.Application.LoadComponent(
            ////    new Uri("SupportTools_Excel;component/Resources/Xaml/Brushes.xaml", UriKind.Relative)) as System.Windows.ResourceDictionary;

            //// Now lets try with 

            //var resources = System.Windows.Application.LoadComponent(
            //    new Uri("SupportTools_Excel;component/Resources/Xaml/Application.xaml", UriKind.Relative)) as System.Windows.ResourceDictionary;

            ////var resources = System.Windows.Application.LoadComponent(
            ////    new Uri("pack:/SupportTools_Excel;:,,/Resources/Xaml/Application.xaml")) as System.Windows.ResourceDictionary;

            //// Merge it on application level

            //app.Resources.MergedDictionaries.Add(resources);

            InitializeComponent();
        }
    }
}
