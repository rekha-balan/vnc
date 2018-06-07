using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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

using SMO = Microsoft.SqlServer.Management.Smo;
using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;

//using SMOH = SMOHelper;

using SQLInformation;

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Databases.xaml
    /// </summary>
    public partial class wucAdmin_Databases : ucBase
    {
        const string TYPE_NAME = "wucAdmin_Databases";

        #region Initialization

        public wucAdmin_Databases()
        {

            InitializeComponent();

        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["databasesViewSource"];
                myCollectionViewSource.Source = SQLInformation.Common.ApplicationDataSet.Databases;

                // These lines are key.  Turns out don't need to load the Databases as these lines drag the databases in because of the relationship (I guess).
                CollectionViewSource instanceSource = (CollectionViewSource)this.Resources["instancesViewSource"];
                instanceSource.Source = Common.ApplicationDataSet.Instances;
                instanceSource.View.MoveCurrentToFirst();

            }
        }


        #endregion

        #region Event Handlers

        #region DataGrid Handlers

        private void databasesDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var dataRow = ((System.Data.DataRowView)e.NewItem).Row;
            var database = (Data.ApplicationDataSet.DatabasesRow)dataRow;

            database.ID = Guid.NewGuid();
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _editMode = true;
        }

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

        private void instancesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            _editMode = false;
        }

        #endregion

        private void btnLoadTables_Click(object sender, RoutedEventArgs e)
        {
            LoadTables();
        }

        private void btnLoadViews_Click(object sender, RoutedEventArgs e)
        {
            LoadViews();
        }

        private void btnSaveExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            Data.ApplicationDataSet.DatabasesRow database =
                (Data.ApplicationDataSet.DatabasesRow)
                ((System.Data.DataRowView)databasesDataGrid.SelectedItem).Row;

            SaveExtendedProperties(database);
        }

        private void btnLoadExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            Data.ApplicationDataSet.DatabasesRow database =
                (Data.ApplicationDataSet.DatabasesRow)
                ((System.Data.DataRowView)databasesDataGrid.SelectedItem).Row;

            LoadExtendedProperties(database);
        }

        private void btnStoredProcedures_Click(object sender, RoutedEventArgs e)
        {
            LoadStoredProcedures();
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        //private void btnLoadDatabases_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadDatabases();
        //}

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
            Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter ta = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();

            ta.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            ta.Update(Common.ApplicationDataSet.Databases);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            //HIS.Library.Common.HISSchema.CancelEdit();
        }

        #endregion

        #region Main Function Routines

        //private void LoadDatabases()
        //{
        //    throw new NotImplementedException();
        //}
       
        private static void LoadExtendedProperties(Data.ApplicationDataSet.DatabasesRow database)
        {
            var instanceName = from item in Common.ApplicationDataSet.Instances
                               where item.ID == database.Instance_Id
                               select item.InstanceName;

            SMO.Server server = new SMO.Server((string)instanceName.First());
            server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            server.ConnectionContext.Login = "SMonitor";
            server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            SMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name].ExtendedProperties;

            foreach (SMO.ExtendedProperty prop in extendedProps)
            {
                Console.WriteLine(string.Format("EP Name:{0}  Value:{1}", prop.Name, prop.Value));
            }

            try { database.EP_Area = (string)extendedProps["EP_Area"].Value; }
            catch (Exception) { database.EP_Area = "[Not Set]"; }

            try { database.EP_DBApprover = (string)extendedProps["EP_DBApprover"].Value; }
            catch (Exception) { database.EP_DBApprover = "[Not Set]"; }

            try { database.EP_DRTier = (string)extendedProps["EP_DRTier"].Value; }
            catch (Exception) { database.EP_DRTier = "[Not Set]"; }

            try { database.EP_PrimaryDBContact = (string)extendedProps["EP_PrimaryDBContact"].Value; }
            catch (Exception) { database.EP_PrimaryDBContact = "[Not Set]"; }

            try { database.EP_Team = (string)extendedProps["EP_Team"].Value; }
            catch (Exception) { database.EP_Team = "[Not Set]"; }
        }

        private void LoadStoredProcedures()
        {
            throw new System.NotImplementedException();
        }

        private void LoadTables()
        {
            Data.ApplicationDataSet.DatabasesRow database = 
                (Data.ApplicationDataSet.DatabasesRow)
                ((System.Data.DataRowView)databasesDataGrid.SelectedItem).Row;

            // Need to go get the Instance Name.   For now do the loads in the Instance Page
            //SMO.Server server = new SMO.Server(instance.InstanceName);
            //server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            //server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            //SMOH.Server serverH = new SMOH.Server(server);

            //Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter ta = new Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();

            //ta.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            //foreach(SMOH.Database dataBase in serverH.Databases.Values)
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
            //    ta.Update(Common.ApplicationDataSet.Databases);
            //}
        }

        private void LoadViews()
        {
            throw new System.NotImplementedException();
        }

        private void SaveExtendedProperties(Data.ApplicationDataSet.DatabasesRow database)
        {
            var instanceName = from item in Common.ApplicationDataSet.Instances
                               where item.ID == database.Instance_Id
                               select item.InstanceName;

            SMO.Server server = new SMO.Server((string)instanceName.First());
            server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            server.ConnectionContext.Login = "SMonitor";
            server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            SMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name].ExtendedProperties;

            SMO.Database smoDatabase = server.Databases[database.Name];

            try
            { 
                extendedProps["EP_Area"].Value = database.EP_Area;
                extendedProps["EP_Area"].Alter();
            }
            catch (Exception)
            {
                SMO.ExtendedProperty ep = new SMO.ExtendedProperty(smoDatabase, "EP_Area", database.EP_Area);
                ep.Create();
            }

            try
            { 
                extendedProps["EP_DBApprover"].Value = database.EP_DBApprover;
                extendedProps["EP_DBApprover"].Alter();
            }
            catch (Exception)
            {
                SMO.ExtendedProperty ep = new SMO.ExtendedProperty(smoDatabase, "EP_DBApprover", database.EP_DBApprover);
                ep.Create();
            }

            try 
            { 
                extendedProps["EP_DRTier"].Value = database.EP_DRTier;
                extendedProps["EP_DRTier"].Alter();
            }
            catch (Exception)
            {
                SMO.ExtendedProperty ep = new SMO.ExtendedProperty(smoDatabase, "EP_DRTier", database.EP_DRTier);
                ep.Create();
            }

            try
            { 
                extendedProps["EP_PrimaryDBContact"].Value = database.EP_PrimaryDBContact;
                extendedProps["EP_PrimaryDBContact"].Alter();
            }
            catch (Exception)
            {
                SMO.ExtendedProperty ep = new SMO.ExtendedProperty(smoDatabase, "EP_PrimaryDBContact", database.EP_PrimaryDBContact);
                ep.Create();
            }

            try
            {
                extendedProps["EP_Team"].Value = database.EP_Team;
                extendedProps["EP_Team"].Alter();
            }
            catch (Exception)
            {
                SMO.ExtendedProperty ep = new SMO.ExtendedProperty(smoDatabase, "EP_Team", database.EP_Team);
                ep.Create();
            }

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
        }

        #endregion

    }
}
