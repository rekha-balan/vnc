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

namespace SQLInformation.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for BigPicture.xaml
    /// </summary>
    public partial class BigPicture : Window
    {
        const string TYPE_NAME = "BigPicture";

        public BigPicture()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            //SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));

            //TODO (crhodes): Should be able to do this in XAML.

            CollectionViewSource serversViewSource = ((CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.Source = Common.ApplicationDataSet.Servers;
            serversViewSource.View.MoveCurrentToFirst();

            CollectionViewSource serversInstancesDatabasesDBTablesViewSource = ((CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBTablesViewSource")));
            serversInstancesDatabasesDBTablesViewSource.View.MoveCurrentToFirst();

            CollectionViewSource serversInstancesDatabasesDBStoredProceduresViewSource = ((CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBStoredProceduresViewSource")));
            serversInstancesDatabasesDBStoredProceduresViewSource.View.MoveCurrentToFirst();
        }
    }
}
