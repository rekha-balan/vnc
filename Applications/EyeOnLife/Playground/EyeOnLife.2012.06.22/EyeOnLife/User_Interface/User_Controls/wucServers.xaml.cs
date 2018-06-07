using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucServers.xaml
    /// </summary>
    public partial class wucServers : UserControl
    {
        const string TYPE_NAME = "wucServers";

        Detail3DManager detail3DManager;
        MasterListManager masterListManager;

        public wucServers()
        {
            InitializeComponent();
        }

        private void Overview3D_FlipCompleted(object sender, EventArgs e)
        {
            double angleTo = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateToProperty);
            double angleFrom = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty);
            Overview3D.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Initialized_1(object sender, EventArgs e)
        {
            detail3DManager = new Detail3DManager(this);
            masterListManager = new MasterListManager(this);  
        }

        private void OnServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnDetailsButton(object sender, RoutedEventArgs e)
        {
            detail3DManager.ShowDetailBack();
            List3DOverlay.Visibility = Visibility.Visible;
        }

        private void OnBackToOverview(object sender, RoutedEventArgs e)
        {
            detail3DManager.ShowDetailFront();
            List3DOverlay.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
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
                    //serversTableAdapter.Fill(Common.ApplicationDataSet.Servers);
                    //instancesTableAdapter.Fill(Common.ApplicationDataSet.Instances);
                    //databasesTableAdapter.Fill(Common.ApplicationDataSet.Databases);
                    //serversListView.ItemsSource = Common.ApplicationDataSet.Servers;
                    //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
                }

                //serversDataGrid.DataContext = Common.ApplicationDataSet.Servers.DefaultView;
                // This is needed to make it work.
                //ServerList.DataContext = Common.ApplicationDataSet.Servers.DefaultView;
            }

            masterListManager.Load();
        }
    }
}
