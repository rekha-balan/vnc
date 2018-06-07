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

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for BigPicture.xaml
    /// </summary>
    public partial class BigPicture2 : Window
    {
        const string TYPE_NAME = "BigPicture";

        public BigPicture2()
        {
#if TRACE
            Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            Common.WriteToDebugWindow(string.Format("Enter {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            //EyeOnLife.Data.ApplicationDataSet applicationDataSet = ((EyeOnLife.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            //// Load data into the table Servers. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.ServersTableAdapter applicationDataSetServersTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            //applicationDataSetServersTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetServersTableAdapter.Fill(applicationDataSet.Servers);

            System.Windows.Data.CollectionViewSource serversViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.Source = Common.ApplicationDataSet.Servers;


            //try
            //{
            //    serversViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
            //// Load data into the table Instances. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter applicationDataSetInstancesTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
            //applicationDataSetInstancesTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetInstancesTableAdapter.Fill(applicationDataSet.Instances);
            //System.Windows.Data.CollectionViewSource serversInstancesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesViewSource")));
            //try
            //{
            //    serversInstancesViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
            //// Load data into the table Databases. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter applicationDataSetDatabasesTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            //applicationDataSetDatabasesTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetDatabasesTableAdapter.Fill(applicationDataSet.Databases);
            //System.Windows.Data.CollectionViewSource serversInstancesDatabasesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesViewSource")));
            //try
            //{
            //    serversInstancesDatabasesViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
            //// Load data into the table DBStoredProcedures. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter applicationDataSetDBStoredProceduresTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter();
            //applicationDataSetDBStoredProceduresTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetDBStoredProceduresTableAdapter.Fill(applicationDataSet.DBStoredProcedures);
            //System.Windows.Data.CollectionViewSource serversInstancesDatabasesDBStoredProceduresViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBStoredProceduresViewSource")));
            //try
            //{
            //    serversInstancesDatabasesDBStoredProceduresViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
            //// Load data into the table DBViews. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter applicationDataSetDBViewsTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter();
            //applicationDataSetDBViewsTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetDBViewsTableAdapter.Fill(applicationDataSet.DBViews);
            //System.Windows.Data.CollectionViewSource serversInstancesDatabasesDBViewsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBViewsViewSource")));
            //try
            //{
            //    serversInstancesDatabasesDBViewsViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
            //// Load data into the table DBTables. You can modify this code as needed.
            //EyeOnLife.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter applicationDataSetDBTablesTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
            //applicationDataSetDBTablesTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //applicationDataSetDBTablesTableAdapter.Fill(applicationDataSet.DBTables);
            //System.Windows.Data.CollectionViewSource serversInstancesDatabasesDBTablesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBTablesViewSource")));
            //try
            //{
            //    serversInstancesDatabasesDBTablesViewSource.View.MoveCurrentToFirst();
            //}
            //catch(Exception ex)
            //{
                
            //}
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

        private void Expander_Expanded_1(object sender, RoutedEventArgs e)
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
