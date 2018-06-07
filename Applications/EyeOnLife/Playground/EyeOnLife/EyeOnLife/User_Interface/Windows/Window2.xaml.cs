using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : DXWindow
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            // Load data into the table Servers. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter applicationDataSetServersTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            applicationDataSetServersTableAdapter.Fill(applicationDataSet.Servers);
            System.Windows.Data.CollectionViewSource serversViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.View.MoveCurrentToFirst();
            // Load data into the table Databases. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter applicationDataSetDatabasesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            applicationDataSetDatabasesTableAdapter.Fill(applicationDataSet.Databases);
            System.Windows.Data.CollectionViewSource databasesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("databasesViewSource")));
            databasesViewSource.View.MoveCurrentToFirst();
            // Load data into the table DatabaseInfo. You can modify this code as needed.
            SQLInformation.Data.ApplicationDataSetTableAdapters.DatabaseInfoTableAdapter applicationDataSetDatabaseInfoTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabaseInfoTableAdapter();
            applicationDataSetDatabaseInfoTableAdapter.Fill(applicationDataSet.DatabaseInfo);
            System.Windows.Data.CollectionViewSource databasesDatabaseInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("databasesDatabaseInfoViewSource")));
            databasesDatabaseInfoViewSource.View.MoveCurrentToFirst();
        }

        private void OnIntialized(object sender, EventArgs e)
        {

        }
    }
}
