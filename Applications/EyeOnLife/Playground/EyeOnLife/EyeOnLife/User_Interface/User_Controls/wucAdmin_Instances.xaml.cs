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
using System.Windows.Navigation;
using System.Windows.Shapes;

using PacificLife.Life;

using SMO=Microsoft.SqlServer.Management.Smo;
using SMOW=Microsoft.SqlServer.Management.Smo.Wmi;

using SMOH=SMOHelper;

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{

    /// <summary>
    /// Interaction logic for wucAdmin_Instances.xaml
    /// </summary>
    public partial class wucAdmin_Instances : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";
        const string TYPE_NAME = "wucAdmin_Instances";

        #region Initialization

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["instancesViewSource"];
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Instances;
            }
        }

        public wucAdmin_Instances()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        #endregion

        #region Event Handlers

        private void btnLoadDatabases_Click(object sender, RoutedEventArgs e)
        {
            LoadDatabases();
        }

        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            Test_Data.InstanceData.LoadNewInstancesInfoFromFile();

            foreach(var instance in Test_Data.InstanceData.DBInstances)
            {
                SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();
                newInstance.ID = Guid.NewGuid();
                newInstance.Name_Instance = instance.FullName;
                Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
            }
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserAddRows = true;
            //}
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserAddRows = false;
            //}
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserDeleteRows = true;
            //}
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //    if (dataGrid != null)
            //    {
            //        dataGrid.CanUserDeleteRows = false;
            //    }
        }

        private void instancesDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var itm = e.NewItem;
            var dataRowView = (System.Data.DataRowView)itm;
            var dataRow = dataRowView.Row;
            var instance = (SQLInformation.Data.ApplicationDataSet.InstancesRow)dataRow;
            
            //SQLInformation.Data.ApplicationDataSet.InstancesRow newRow = (SQLInformation.Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
            instance.ID = Guid.NewGuid();
        }

        private void instancesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            _editMode = false;
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.IsReadOnly = true;
            //}
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.IsReadOnly = false;
            //}
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter ta = new SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();

            ta.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            ta.Update(Common.ApplicationDataSet.Instances);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            //HIS.Library.Common.HISSchema.CancelEdit();
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _editMode = true;
        }

        #region DataGrid Handlers

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Delete &&
            //  _editMode == false &&
            //  dataGrid.CanUserDeleteRows == true)
            //{
            //    if (MessageBox.Show("Do you want to delete this Attribute?", "Attributes", MessageBoxButton.YesNo) ==
            //      MessageBoxResult.Yes)
            //    {
            //        Attributes.Remove((HIS.Library.AttributeEC)dataGrid.SelectedItem);
            //    }
            //    else
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var snd = sender;

            DataGridRow gridRow = e.Row;
            _editMode = false;
        }
        #endregion

        #endregion

        #region Main Function Routines

        private void LoadDatabases()
        {
            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance = 
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //SMO.Server server = new SMO.Server(instance.InstanceName);
            //server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            //server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            //SMOH.Server serverH = new SMOH.Server(server);

            //SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter taDatabases = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            //SQLInformation.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter taTables = new SQLInformation.Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
            //SQLInformation.Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter taStoredProcedures = new SQLInformation.Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter();

            //taDatabases.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
            //taTables.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
            //taStoredProcedures.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            ////long startTime = Common.WriteToDebugWindow(string.Format(" Adding Databases"));

            //foreach (SMOH.Database dataBase in serverH.Databases.Values)
            //{
            //    SQLInformation.Data.ApplicationDataSet.DatabasesRow newDatabase = Common.ApplicationDataSet.Databases.NewDatabasesRow();
            //    newDatabase.ID = Guid.NewGuid();
            //    newDatabase.Name_Database = dataBase.Name;
            //    newDatabase.DataBaseGuid = dataBase.DatabaseGuid;

            //    //newDatabase.DataBaseGuid = Guid.Parse(dataBase.DatabaseGuid);
            //    newDatabase.CreateDate = DateTime.Parse(dataBase.CreateDate);
            //    newDatabase.ID_DB = dataBase.ID;
            //    newDatabase.Instance_ID = instance.ID;

            //    Common.ApplicationDataSet.Databases.AddDatabasesRow(newDatabase);
            //    taDatabases.Update(Common.ApplicationDataSet.Databases);

            //    //Common.ApplicationDataSet.Databases.AddDatabasesRow(newDatabase);
            //    //taDatabases.Update(Common.ApplicationDataSet.Databases);

            //    // TODO(crhodes): This is in wucAdmin_Databases for now.  Pull out and put in population service.

            //    //startTime = Common.WriteToDebugWindow(string.Format(" Added DB:{0}", dataBase.Name), startTime);

            //    //foreach(SMOH.Table table in dataBase.Tables.Values)
            //    //{
            //    //    SQLInformation.Data.ApplicationDataSet.DBTablesRow newTable = Common.ApplicationDataSet.DBTables.NewDBTablesRow();
            //    //    newTable.ID = Guid.NewGuid();   // See if this is available from table.
            //    //    newTable.TableName = table.Name;
            //    //    newTable.Table_ID = table.ID;
            //    //    newTable.Database_ID = newDatabase.ID;  // From above
            //    //    newTable.Owner = table.Owner;
            //    //    newTable.CreateDate = DateTime.Parse(table.CreateDate);
            //    //    newTable.DataSpaceUsed = int.Parse(table.DataSpaceUsed);
            //    //    try
            //    //    {
            //    //        newTable.DateLastModified = DateTime.Parse(table.DateLastModified);
            //    //    }
            //    //    catch(Exception ex)
            //    //    {
                        
            //    //    }
            //    //    newTable.RowCount = int.Parse(table.RowCount);

            //    //    Common.ApplicationDataSet.DBTables.AddDBTablesRow(newTable);
            //    //    taTables.Update(Common.ApplicationDataSet.DBTables);
            //    //    //Common.WriteToDebugWindow(string.Format("   Added TBL:{0}", table.Name));
            //    //}

            //    //startTime = Common.WriteToDebugWindow(string.Format("Added Tables"), startTime);

            //    //foreach(SMOH.StoredProcedure sp in dataBase.StoredProcedures.Values)
            //    //{
            //    //    SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow newStoredProcedure = Common.ApplicationDataSet.DBStoredProcedures.NewDBStoredProceduresRow();
            //    //    newStoredProcedure.ID = Guid.NewGuid();
            //    //    newStoredProcedure.StoredProcedureName = sp.Name;
            //    //    newStoredProcedure.StoredProcedure_ID = sp.ID;
            //    //    newStoredProcedure.Database_ID = newDatabase.ID;    // From above
            //    //    newStoredProcedure.Owner = sp.Owner;
            //    //    newStoredProcedure.MethodName = sp.MethodName;
            //    //    newStoredProcedure.TextHeader = sp.TextHeader;
            //    //    newStoredProcedure.TextBody = sp.TextBody;
            //    //    newStoredProcedure.IsSystemObject = bool.Parse(sp.IsSystemObject);
            //    //    newStoredProcedure.CreateDate = DateTime.Parse(sp.CreateDate);
            //    //    newStoredProcedure.DateLastModified = DateTime.Parse(sp.DateLastModified);

            //    //    Common.ApplicationDataSet.DBStoredProcedures.AddDBStoredProceduresRow(newStoredProcedure);
            //    //    taStoredProcedures.Update(Common.ApplicationDataSet.DBStoredProcedures);
            //    //    //Common.WriteToDebugWindow(string.Format("   Added SP:{0}", sp.Name));
            //    //}

            //    //startTime = Common.WriteToDebugWindow(string.Format("Added Stored Procedures"), startTime);
            //}
        }


        private void SyncServerInfoAndInstanceInfo(SQLInformation.Data.ApplicationDataSet.InstancesRow instance)
        {
            var servers = from s in Common.ApplicationDataSet.Servers
                         where s.NetName == instance.NetName
                         select s;

            SQLInformation.Data.ApplicationDataSet.ServersRow server = servers.First();

            instance.Server_ID = server.ID;

            server.OSVersion = instance.OSVersion;
            server.PhysicalMemory = instance.PhysicalMemory;
            server.Platform = instance.Platform;
            server.Processors = instance.Processors;

            server.EndEdit();
        }

        private void UpdateInfo()
        {
            SQLInformation.Data.ApplicationDataSet.InstancesRow instance = 
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //SMO.Server server = new SMO.Server(instance.InstanceName);
            //server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            //server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            try
            {
                SMO.Server server = SQLInformation.SMO.Helper.GetServer(instance.Name_Instance);

                SMOH.Server serverH = new SMOH.Server(server);

                instance.ServiceName = serverH.ServiceName;
                instance.Collation = serverH.Collation;
                instance.Edition = serverH.Edition;
                instance.IsClustered = bool.Parse(serverH.IsClustered);
                instance.NetName = serverH.NetName;
                instance.OSVersion = serverH.OSVersion;
                instance.PhysicalMemory = int.Parse(serverH.PhysicalMemory);
                instance.Platform = serverH.Platform;
                instance.Processors = int.Parse(serverH.Processors);
                instance.ServiceAccount = serverH.ServiceAccount;
                instance.ServiceInstanceId = serverH.ServiceInstanceId;
                instance.EngineEdition = serverH.EngineEdition;
                instance.Product = serverH.Product;
                instance.ProductLevel = serverH.ProductLevel;
                instance.Version = serverH.VersionString;
            }
            catch (Exception ex)
            {
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);                
            }
        }

        #endregion

        private void btnSyncInstanceToServer_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            SyncServerInfoAndInstanceInfo(instance);
        }

        /// <summary>
        /// Store current values in ServerInfo history table.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="database"></param>
        private void TakeSnapShot(string instanceName, SQLInformation.Data.ApplicationDataSet.InstancesRow instanceRow)
        {
            SMO.Server server = SMOH.SMOD.GetServer(instanceName);

            SQLInformation.Data.ApplicationDataSetTableAdapters.InstanceInfoTableAdapter tableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.InstanceInfoTableAdapter();

            tableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            //int dbID = int.Parse(serverRow.ID_DB);

            //SMO.Database db = server.Databases.ItemById(dbID);

            SQLInformation.Data.ApplicationDataSet.InstanceInfoRow newSnapShot = Common.ApplicationDataSet.InstanceInfo.NewInstanceInfoRow();

            newSnapShot.Instance_ID = instanceRow.ID;
            newSnapShot.SnapShotDate = DateTime.Now;

            Common.ApplicationDataSet.InstanceInfo.AddInstanceInfoRow(newSnapShot);
            tableAdapter.Update(Common.ApplicationDataSet.InstanceInfo);
        }

        private void btnTakeSnapshot_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes): Hook to TakeSnapShot.  See wucAdmin_Databases
        }
    }
}
