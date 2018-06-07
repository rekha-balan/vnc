using System;
using System.Collections.Generic;
using System.Linq;
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

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for BigPicture.xaml
    /// </summary>
    public partial class BigPictureDBContents : DXWindow
    {
        const string TYPE_NAME = "BigPicture";

        public BigPictureDBContents()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("Enter {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            CollectionViewSource serversViewSource = ((CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.Source = Common.ApplicationDataSet.Servers;

        }

        private void HideIDColumns(Nullable<bool> isChecked)
        {
            if ((bool)isChecked)
            {
                iDColumn_Servers.Visibility = Visibility.Visible;

                iDColumn_Instances.Visibility = Visibility.Visible;
                server_IDColumn.Visibility = Visibility.Visible;

                iDColumn_Databases.Visibility = Visibility.Visible;
                instance_IDColumn.Visibility = Visibility.Visible;

                iDColumn_Tables.Visibility = Visibility.Visible;
                database_IDColumn2.Visibility = Visibility.Visible;

                iDColumn_Views.Visibility = Visibility.Visible;
                database_IDColumn1.Visibility = Visibility.Visible;

                iDColumn_StoredProcedures.Visibility = Visibility.Visible;
                database_IDColumn.Visibility = Visibility.Visible;
            }
            else
            {
                iDColumn_Servers.Visibility = Visibility.Hidden;

                iDColumn_Instances.Visibility = Visibility.Hidden;
                server_IDColumn.Visibility = Visibility.Hidden;

                iDColumn_Databases.Visibility = Visibility.Hidden;
                instance_IDColumn.Visibility = Visibility.Hidden;

                iDColumn_Tables.Visibility = Visibility.Hidden;
                database_IDColumn2.Visibility = Visibility.Hidden;

                iDColumn_Views.Visibility = Visibility.Hidden;
                database_IDColumn1.Visibility = Visibility.Hidden;

                iDColumn_StoredProcedures.Visibility = Visibility.Hidden;
                database_IDColumn.Visibility = Visibility.Hidden;
            }
        }

        private void OnShowIDs_Click(object sender, RoutedEventArgs e)
        {
            HideIDColumns(((CheckBox)sender).IsChecked);
        }

        private void OnShowIDs_Uncheked(object sender, RoutedEventArgs e)
        {

        }

        private void OnShowIDs_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void OnScreenSize_Click(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
            	case "SmallScreen":
                    this.Width = 800;
                    this.Height = 600;
            		break;

                case "MediumScreen":
                    this.Width = 1000;
                    this.Height = 600;
                    break;

                case "LargeScreen":
                    this.Width = 1200;
                    this.Height = 800;
                    break;

                case "XLargeScreen":
                    this.Width = 1400;
                    this.Height = 1000;
                    break;
            }
            
        }
    }
}
