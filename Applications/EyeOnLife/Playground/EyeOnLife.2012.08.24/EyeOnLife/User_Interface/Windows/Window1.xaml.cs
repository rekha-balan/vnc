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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : DXWindow
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            // Load data into the table Servers. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter applicationDataSetServersTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            applicationDataSetServersTableAdapter.Fill(applicationDataSet.Servers);
            System.Windows.Data.CollectionViewSource serversViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.View.MoveCurrentToFirst();
            // Load data into the table Instances. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter applicationDataSetInstancesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
            applicationDataSetInstancesTableAdapter.Fill(applicationDataSet.Instances);
            System.Windows.Data.CollectionViewSource serversInstancesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesViewSource")));
            serversInstancesViewSource.View.MoveCurrentToFirst();
            // Load data into the table Databases. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter applicationDataSetDatabasesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            applicationDataSetDatabasesTableAdapter.Fill(applicationDataSet.Databases);
            System.Windows.Data.CollectionViewSource serversInstancesDatabasesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesViewSource")));
            serversInstancesDatabasesViewSource.View.MoveCurrentToFirst();
            // Load data into the table DBTables. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter applicationDataSetDBTablesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
            applicationDataSetDBTablesTableAdapter.Fill(applicationDataSet.DBTables);
            System.Windows.Data.CollectionViewSource serversInstancesDatabasesDBTablesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBTablesViewSource")));
            serversInstancesDatabasesDBTablesViewSource.View.MoveCurrentToFirst();
        }
    }
}
