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

namespace SQLInformation.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Pretend we are in the EAC
            using(StreamReader reader = new StreamReader(@"Test Data\TestWindowConsoleParameter.xml"))
            {
                ConfigData.RawXML = reader.ReadToEnd();
            }

            //Load your data here and assign the result to the CollectionViewSource.
            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            //myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

            Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter;
            Data.ApplicationDataSetTableAdapters.InstancesTableAdapter instancesTableAdapter;
            Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter databasesTableAdapter;

            Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;

            serversTableAdapter = new Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            instancesTableAdapter = new Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
            databasesTableAdapter = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();

            tableAdapterManager = new Data.ApplicationDataSetTableAdapters.TableAdapterManager();

            // Must have to do more work before can do this.  Connection is null
            //tableAdapterManager.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;\

            // DO this instead for now.
            serversTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            instancesTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            databasesTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            try
            {
                serversTableAdapter.Fill(Common.ApplicationDataSet.Servers);
                instancesTableAdapter.Fill(Common.ApplicationDataSet.Instances);
                //databasesTableAdapter.Fill(Common.ApplicationDataSet.Databases);
                //serversListView.ItemsSource = Common.ApplicationDataSet.Servers;
                //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void OnIntialized(object sender, EventArgs e)
        {

        }
    }
}
