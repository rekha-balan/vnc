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

//using SMOH=SMOHelper;

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{

    /// <summary>
    /// Interaction logic for wucAdmin_Instances.xaml
    /// </summary>
    public partial class wucAdmin_InstancesDX : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        #region Initialization

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["instancesViewSource"];
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Instances;

                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["instancesViewSource1"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.Instances;
            }
        }

        public wucAdmin_InstancesDX()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        #endregion

        #region Event Handlers

        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            Test_Data.InstanceData.LoadNewInstancesInfoFromFile();

            foreach(var instance in Test_Data.InstanceData.DBInstances)
            {
                // TODO(crhodes): Make smarter to only add new instances.
                var existingInstance = from inst in Common.ApplicationDataSet.Instances
                                       where inst.Name_Instance == instance.FullName
                                       select inst.Name_Instance;

                if (existingInstance.Count() == 0)
                {
                    SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.GetNewInstancesRow(instance.FullName);
                    Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);                   
                }

            }
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            FetchSMOInfo();
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

        private static void UpdateDatabase()
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter ta = new SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();

            ta.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            ta.Update(Common.ApplicationDataSet.Instances);
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatabase();
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

        private void SyncServerInfoAndInstanceInfo(SQLInformation.Data.ApplicationDataSet.InstancesRow instance)
        {
            var servers = from s in Common.ApplicationDataSet.Servers
                         where s.NetName == instance.NetName
                         select s;

            try
            {
                SQLInformation.Data.ApplicationDataSet.ServersRow server = servers.First();

                    instance.Server_ID = server.ID;

                    server.OSVersion = instance.OSVersion;
                    server.PhysicalMemory = instance.PhysicalMemory;
                    server.Platform = instance.Platform;
                    server.Processors = instance.Processors;

                    server.EndEdit();
            }
            catch (Exception ex)
            {
                // No matching server.
                MessageBox.Show("No matching server.  Add new entry then continue");
            }
        }

        private void FetchSMOInfo()
        {
            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance = 
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //try
            //{
            //    SMO.Server server = SQLInformation.SMO.Helper.GetServer(instance.Name_Instance);

            //    instance.ServiceName = server.ServiceName;
            //    instance.Collation = server.Collation;
            //    instance.Edition = server.Edition;
            //    instance.IsClustered = server.IsClustered;
            //    instance.NetName = server.NetName;
            //    instance.OSVersion = server.OSVersion;
            //    instance.PhysicalMemory = server.PhysicalMemory;
            //    instance.Platform = server.Platform;
            //    instance.Processors = server.Processors;

            //    try
            //    {
            //        instance.ServiceAccount = server.ServiceAccount;
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //    try
            //    {
            //        instance.ServiceInstanceId = server.ServiceInstanceId;
            //    }
            //    catch (Exception ex)
            //    {
            //        
            //    }

            //    instance.EngineEdition = server.EngineEdition.ToString();
            //    instance.Product = server.Product;
            //    instance.ProductLevel = server.ProductLevel;
            //    instance.Version = server.VersionString;

            //    instance.SnapShotDate = DateTime.Now;
            //    UpdateDatabase();
            //}
            //catch(Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            //{
            //    instance.NetName = "Cannot Connect";
            //    instance.SnapShotDate = DateTime.Now;
            //    UpdateDatabase();              
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString()); 
            //    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);                
            //}
        }

        #endregion

        private void btnSyncInstanceToServer_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)instancesDataGrid.SelectedItem).Row;

            //SyncServerInfoAndInstanceInfo(instance);
        }

        /// <summary>
        /// Store current values in ServerInfo history table.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="database"></param>
        private void TakeSnapShot(string instanceName, SQLInformation.Data.ApplicationDataSet.InstancesRow instanceRow)
        {
            //SMO.Server server = SMOH.SMOD.GetServer(instanceName);
            SMO.Server server = SQLInformation.SMO.Helper.GetServer(instanceName);

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
            //SQLInformation.SMO.Helper.TakeInstancesSnapShot(Common.ApplicationDataSet.Instances);
            // TODO(crhodes): Hook to TakeSnapShot.  See wucAdmin_Databases
        }

    }
}
