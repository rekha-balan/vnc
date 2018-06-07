using System;
using System.Data;
using System.IO;

namespace EyeOnLife.Data {
    
    
    public partial class ApplicationDataSet 
    {
        partial class LKUP_ADDomainsDataTable
        {
        }

        const string TYPE_NAME = "ApplicationDataSet";

//        public void LoadApplicationDataSetFromDB()
//        {
//#if TRACE
//            long startTime = Common.WriteToDebugWindow(string.Format("Enter {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
//#endif
//            // Pretend we are in the EAC
//            using(StreamReader reader = new StreamReader(@"Test Data\TestWindowConsoleParameter.xml"))
//            {
//                ConfigData.RawXML = reader.ReadToEnd();
//            }

//            //Load your data here and assign the result to the CollectionViewSource.
//            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
//            //myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

//            Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTA;
//            Data.ApplicationDataSetTableAdapters.InstancesTableAdapter instancesTA;
//            Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter databasesTA;
//            Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter dbTablesTA;
//            Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter dbViewsTA;
//            Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter dbStoredProceduresTA;
//            Data.ApplicationDataSetTableAdapters.LKUP_ADDomainsTableAdapter adDomainsTA;
//            Data.ApplicationDataSetTableAdapters.LKUP_EnvironmentsTableAdapter environmentsTA;


//            Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;

//            serversTA = new Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
//            instancesTA = new Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
//            databasesTA = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
//            dbTablesTA = new Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
//            dbViewsTA = new Data.ApplicationDataSetTableAdapters.DBViewsTableAdapter();
//            dbStoredProceduresTA = new Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter();
//            adDomainsTA = new ApplicationDataSetTableAdapters.LKUP_ADDomainsTableAdapter();
//            environmentsTA = new ApplicationDataSetTableAdapters.LKUP_EnvironmentsTableAdapter();

//            tableAdapterManager = new Data.ApplicationDataSetTableAdapters.TableAdapterManager();

//            // Must have to do more work before can do this.  Connection is null
//            //tableAdapterManager.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;\

//            // DO this instead for now.
//            serversTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            instancesTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            databasesTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            dbTablesTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            dbViewsTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            dbStoredProceduresTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            adDomainsTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
//            environmentsTA.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

//            try
//            {
//                serversTA.Fill(Common.ApplicationDataSet.Servers);
//                instancesTA.Fill(Common.ApplicationDataSet.Instances);
//                databasesTA.Fill(Common.ApplicationDataSet.Databases);
//                dbTablesTA.Fill(Common.ApplicationDataSet.DBTables);
//                dbViewsTA.Fill(Common.ApplicationDataSet.DBViews);
//                dbStoredProceduresTA.Fill(Common.ApplicationDataSet.DBStoredProcedures);
//                adDomainsTA.Fill(Common.ApplicationDataSet.LKUP_ADDomains);
//                environmentsTA.Fill(Common.ApplicationDataSet.LKUP_Environments);

//                //serversListView.ItemsSource = Common.ApplicationDataSet.Servers;
//                //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
//            }
//            catch(Exception ex)
//            {
//                System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
//            }

//            Common.WriteToDebugWindow(string.Format(" Exit {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name), startTime);
//        }

    }
}
