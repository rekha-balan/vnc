using System;
using System.Data;
using System.IO;

namespace SQLInformation.Data
{
    
    
    public partial class ApplicationDataSet 
    {
        const string TYPE_NAME = "ApplicationDataSet";

        public void LoadApplicationDataSetFromDB()
        {
//#if TRACE
//            long startTime = Common.WriteToDebugWindow(string.Format("Enter {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
////#endif
            // Pretend we are in the EAC
            using(StreamReader reader = new StreamReader(@"Test Data\TestWindowConsoleParameter.xml"))
            {
                ConfigData.RawXML = reader.ReadToEnd();
            }

            //Load your data here and assign the result to the CollectionViewSource.
            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            //myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

            Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTA;
            Data.ApplicationDataSetTableAdapters.InstancesTableAdapter instancesTA;
            Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter databasesTA;
            Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter dbTablesTA;
            Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter dbViewsTA;
            Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter dbStoredProceduresTA;


            Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;

            serversTA = new Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            instancesTA = new Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
            databasesTA = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            dbTablesTA = new Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
            dbViewsTA = new Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter();
            dbStoredProceduresTA = new Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter();

            tableAdapterManager = new Data.ApplicationDataSetTableAdapters.TableAdapterManager();

            // Must have to do more work before can do this.  Connection is null
            //tableAdapterManager.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;\

            // DO this instead for now.
            serversTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            instancesTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            databasesTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            dbTablesTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            dbViewsTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            dbStoredProceduresTA.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            try
            {
                serversTA.Fill(Common.ApplicationDataSet.Servers);
                instancesTA.Fill(Common.ApplicationDataSet.Instances);
                databasesTA.Fill(Common.ApplicationDataSet.Databases);
                dbTablesTA.Fill(Common.ApplicationDataSet.DBTables);
                dbViewsTA.Fill(Common.ApplicationDataSet.DBViews);
                dbStoredProceduresTA.Fill(Common.ApplicationDataSet.DBStoredProcedures);

                //serversListView.ItemsSource = Common.ApplicationDataSet.Servers;
                //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
            }

            //Common.WriteToDebugWindow(string.Format(" Exit {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name), startTime);
        }

    }
}
