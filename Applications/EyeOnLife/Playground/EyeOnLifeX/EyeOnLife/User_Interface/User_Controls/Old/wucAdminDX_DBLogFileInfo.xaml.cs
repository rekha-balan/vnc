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

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Databases.xaml
    /// </summary>
    public partial class wucAdminDX_DBLogFileInfo : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucAdminDX_DBLogFileInfo()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["dBLogFileInfoViewSource"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.DBLogFileInfo;
            }
        }

        #endregion

        #region Event Handlers

        #region DataGrid Handlers

        private void dataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var dataRow = ((System.Data.DataRowView)e.NewItem).Row;
            var database = (SQLInformation.Data.ApplicationDataSet.DatabasesRow)dataRow;

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

        private void btnLoadExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //LoadExtendedProperties(databaseRow);
        }

        private void btnSaveExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //SaveExtendedProperties(database);
        }
        private void btnTakeSnapshot_Click(object sender, RoutedEventArgs e)
        {
            //// This seems ridiculous but what the heck, it works.

            //DataGridRow row = this.dataGrid.ItemContainerGenerator.ContainerFromIndex(this.dataGrid.SelectedIndex) as DataGridRow;
            //ComboBox ele = this.dataGrid.Columns[3].GetCellContent(row) as ComboBox;

            //string instanceName = ele.Text;

            //SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //TakeSnapShot(instanceName, databaseRow);
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = true;
            }
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = false;
            }
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = true;
            }
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = false;
            }
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = true;
            }
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = false;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter ta = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();

            ta.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            ta.Update(Common.ApplicationDataSet.Databases);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Databases.RejectChanges();
        }

        #endregion

        #region Main Function Routines

        // TODO.  Make this look like LoadStoredProcedures

        private static void LoadExtendedProperties(SQLInformation.Data.ApplicationDataSet.DatabasesRow database)
        {
            var instanceName = from item in Common.ApplicationDataSet.Instances
                               where item.ID == database.Instance_ID
                               select item.Name_Instance;

            SMO.Server server = new SMO.Server((string)instanceName.First());
            server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            server.ConnectionContext.Login = "SMonitor";
            server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            SMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name_Database].ExtendedProperties;

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

        private void SaveExtendedProperties(SQLInformation.Data.ApplicationDataSet.DatabasesRow database)
        {
            var instanceName = from item in Common.ApplicationDataSet.Instances
                               where item.ID == database.Instance_ID
                               select item.Name_Instance;

            SMO.Server server = new SMO.Server((string)instanceName.First());
            server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            server.ConnectionContext.Login = "SMonitor";
            server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            SMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name_Database].ExtendedProperties;

            SMO.Database smoDatabase = server.Databases[database.Name_Database];

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

        /// <summary>
        /// Store current values in DatabaseInfo history table.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="database"></param>
        private void TakeSnapShot(string instanceName, SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow)
        {
            //SMO.Server server = SMOH.SMOD.GetServer(instanceName);
            SMO.Server server = SQLInformation.SMO.Helper.GetServer(instanceName);

            SQLInformation.Data.ApplicationDataSetTableAdapters.DatabaseInfoTableAdapter tableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabaseInfoTableAdapter();

            tableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            int dbID = databaseRow.ID_DB;

            SMO.Database db = server.Databases.ItemById(dbID);

            SQLInformation.Data.ApplicationDataSet.DatabaseInfoRow newSnapShot = Common.ApplicationDataSet.DatabaseInfo.NewDatabaseInfoRow();

            newSnapShot.SnapShotDate = DateTime.Now;
            newSnapShot.Database_ID = databaseRow.ID;
            newSnapShot.Instance_ID = databaseRow.Instance_ID;
            newSnapShot.IndexSpaceUsage = db.IndexSpaceUsage;
            newSnapShot.DataSpaceUsage = db.DataSpaceUsage;
            newSnapShot.Size = db.Size;
            newSnapShot.SpaceAvailable = db.SpaceAvailable;

            Common.ApplicationDataSet.DatabaseInfo.AddDatabaseInfoRow(newSnapShot);
            tableAdapter.Update(Common.ApplicationDataSet.DatabaseInfo);
        }

        private void UpdateInfo()
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
