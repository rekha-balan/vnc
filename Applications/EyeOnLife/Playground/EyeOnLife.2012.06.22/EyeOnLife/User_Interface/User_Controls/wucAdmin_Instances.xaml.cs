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

using SMO=Microsoft.SqlServer.Management.Smo;
using SMOW=Microsoft.SqlServer.Management.Smo.Wmi;

//using SMOH=SMOHelper;

using SQLInformation;

namespace SQLInformation.User_Interface.User_Controls
{

    /// <summary>
    /// Interaction logic for wucAdmin_Instances.xaml
    /// </summary>
    public partial class wucAdmin_Instances : ucBase
    {
        const string TYPE_NAME = "wucAdmin_Instances";

        #region Initialization

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["instancesViewSource"];
                myCollectionViewSource.Source = SQLInformation.Common.ApplicationDataSet.Instances;
            }
        }

        public wucAdmin_Instances()
        {
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
            Test_Data.InstanceData.LoadNewInstanceListFromFile();

            foreach(var instance in Test_Data.InstanceData.DBInstances)
            {
                Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();
                newInstance.ID = Guid.NewGuid();
                newInstance.InstanceName = instance.FullName;
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
            var instance = (Data.ApplicationDataSet.InstancesRow)dataRow;
            
            //Data.ApplicationDataSet.InstancesRow newRow = (Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
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
            Data.ApplicationDataSetTableAdapters.InstancesTableAdapter ta = new Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();

            ta.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

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
            //Data.ApplicationDataSet.InstancesRow instance = 
            //    (Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //SMO.Server server = new SMO.Server(instance.InstanceName);
            //server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            //server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            //SMOH.Server serverH = new SMOH.Server(server);

            //Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter taDatabases = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();
            //Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter taTables = new Data.ApplicationDataSetTableAdapters.DBTablesTableAdapter();
            //Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter taStoredProcedures = new Data.ApplicationDataSetTableAdapters.DBStoredProceduresTableAdapter();

            //taDatabases.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;
            //taTables.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            //foreach (SMOH.Database dataBase in serverH.Databases.Values)
            //{
            //    Data.ApplicationDataSet.DatabasesRow newDatabase = Common.ApplicationDataSet.Databases.NewDatabasesRow();
            //    newDatabase.ID = Guid.NewGuid();
            //    newDatabase.Name = dataBase.Name;
            //    newDatabase.DataBaseGuid = dataBase.DatabaseGuid;

            //    //newDatabase.DataBaseGuid = Guid.Parse(dataBase.DatabaseGuid);
            //    newDatabase.CreateDate = DateTime.Parse(dataBase.CreateDate);
            //    newDatabase.ID_DB = dataBase.ID;
            //    newDatabase.Instance_Id = instance.ID;

            //    Common.ApplicationDataSet.Databases.AddDatabasesRow(newDatabase);
            //    taDatabases.Update(Common.ApplicationDataSet.Databases);

            //    foreach(SMOH.Table table in dataBase.Tables.Values)
            //    {
            //        Data.ApplicationDataSet.DBTablesRow newTable = Common.ApplicationDataSet.DBTables.NewDBTablesRow();
            //        newTable.ID = Guid.NewGuid();   // See if this is available from table.
            //        newTable.TableName = table.Name;
            //        newTable.Table_ID = table.ID;
            //        newTable.Database_ID = newDatabase.ID;  // From above
            //        newTable.Owner = table.Owner;
            //        newTable.CreateDate = DateTime.Parse(table.CreateDate);
            //        newTable.DataSpaceUsed = table.DataSpaceUsed;
            //        newTable.DateLastModified = DateTime.Parse(table.DateLastModified);
            //        newTable.RowCount = table.RowCount;

            //        Common.ApplicationDataSet.DBTables.AddDBTablesRow(newTable);
            //        taTables.Update(Common.ApplicationDataSet.DBTables);
            //    }

            //    foreach(SMOH.StoredProcedure sp in dataBase.StoredProcedures.Values)
            //    {
            //        Data.ApplicationDataSet.DBStoredProceduresRow newStoredProcedure = Common.ApplicationDataSet.DBStoredProcedures.NewDBStoredProceduresRow();
            //        newStoredProcedure.ID = Guid.NewGuid();
            //        newStoredProcedure.StoredProcedureName = sp.Name;
            //        newStoredProcedure.StoredProcedure_ID = sp.ID;
            //        newStoredProcedure.Database_ID = newDatabase.ID;    // From above
            //        newStoredProcedure.Owner = sp.Owner;
            //        newStoredProcedure.MethodName = sp.MethodName;
            //        newStoredProcedure.TextHeader = sp.TextHeader;
            //        newStoredProcedure.TextBody = sp.TextBody;
            //        newStoredProcedure.IsSystemObject = sp.IsSystemObject;
            //        newStoredProcedure.CreateDate = DateTime.Parse(sp.CreateDate);
            //        newStoredProcedure.DateLastModified = DateTime.Parse(sp.DateLastModified);

            //        Common.ApplicationDataSet.DBStoredProcedures.AddDBStoredProceduresRow(newStoredProcedure);
            //        taStoredProcedures.Update(Common.ApplicationDataSet.DBStoredProcedures);
            //    }
            //}
        }

        private void UpdateInfo()
        {
            //Data.ApplicationDataSet.InstancesRow instance = 
            //    (Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //SMO.Server server = new SMO.Server(instance.InstanceName);
            //server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            //server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            //SMOH.Server serverH = new SMOH.Server(server);

            //instance.ServiceName = serverH.ServiceName;
            //instance.Collation = serverH.Collation;
            //instance.Edition = serverH.Edition;
            //instance.IsClustered = serverH.IsClustered;
            //instance.NetName = serverH.NetName;
            //instance.OSVersion = serverH.OSVersion;
            //instance.PhysicalMemory = serverH.PhysicalMemory;
            //instance.Platform = serverH.Platform;
            //instance.Processors = serverH.Processors;
            //instance.ServiceAccount = serverH.ServiceAccount;
            //instance.ServiceInstanceId = serverH.ServiceInstanceId;
            //instance.EngineEdition = serverH.EngineEdition;
            //instance.Product = serverH.Product;
            //instance.ProductLevel = serverH.ProductLevel;
            //instance.Version = serverH.VersionString;
        }

        #endregion
    }
}
