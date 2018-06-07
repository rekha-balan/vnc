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
    /// Interaction logic for dxBigPictureInstance.xaml
    /// </summary>
    public partial class dxBigPictureInstance : DXWindow
    {
        public dxBigPictureInstance()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            // Things work if this line is present.  Testing to see if it works without 6/13/2012
            // Yup, still works.  Don't need this line as it is done in the XAML.
            myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
            serversGridControl.GroupBy("Environment");
            serversGridControl.GroupBy("SecurityZone");
        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            gridColumn1.Visible = true;
            gridColumn1a.Visible = true;
        }


        private void HideIDColumns(Nullable<bool> isChecked)
        {
            if ((bool)isChecked)
            {
                gridColumn1.Visible = true;
            }
            else
            {
                gridColumn1.Visible = false;
            }
        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn1.Visible = false;
            gridColumn1a.Visible = false;
        }
    }
}
