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

using EyeOnLife;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for dxWindow3.xaml
    /// </summary>
    public partial class dxWindow3 : DXWindow
    {
        public dxWindow3()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //treeListControl1.DataSource = Common.ApplicationDataSet.Servers;

            //dataGrid1

            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            applicationDataSet = Common.ApplicationDataSet;

            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            // Things work if this line is present.  Testing to see if it works without 6/13/2012
            // Yup, still works.  Don't need this line as it is done in the XAML.
            myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
        }
    }
}
