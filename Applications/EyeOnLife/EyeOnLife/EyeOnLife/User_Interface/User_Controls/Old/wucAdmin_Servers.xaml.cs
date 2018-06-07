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

using SMO = Microsoft.SqlServer.Management.Smo;
//using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;

//using SMOH = SMOHelper;

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Servers.xaml
    /// </summary>
    public partial class wucAdmin_Servers : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wucAdmin_Servers()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        #region Event Handlers

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

        #endregion

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //Common.ApplicationDataSet.Servers.AcceptChanges();
            //Common.ApplicationDataSet.AcceptChanges();
            //SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();

            //serversTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
            ////serversTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            //serversTableAdapter.Update(Common.ApplicationDataSet.Servers);

            Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);

        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            //HIS.Library.Common.HISSchema.CancelEdit();
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

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _editMode = true;
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var snd = sender;

            DataGridRow gridRow = e.Row;
            
           //((Data.ApplicationDataSet.InstancesRow)e.Row).id = Guid.NewGuid();
            _editMode = false;
        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
             // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;

                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource1"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
                //tableView1.BestFitColumns();
             }
        }

        private void serversDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //var itm = e.NewItem;
            //var dataRowView = (System.Data.DataRowView)itm;
            //var dataRowView = (System.Data.DataRowView)e.NewItem;

            //var dataRow = dataRowView.Row;

            //var server = (Data.ApplicationDataSet.ServersRow)dataRowView.Row;
            var server = (SQLInformation.Data.ApplicationDataSet.ServersRow)((System.Data.DataRowView)e.NewItem).Row;

            //Data.ApplicationDataSet.InstancesRow newRow = (Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
            server.ID = Guid.NewGuid();
        }

        ///// <summary>
        ///// Store current values in ServerInfo history table.
        ///// </summary>
        ///// <param name="instance"></param>
        ///// <param name="database"></param>
        //private void TakeSnapShot(string instanceName, SQLInformation.Data.ApplicationDataSet.ServersRow serverRow)
        //{
        //    //SMO.Server server = SMOH.SMOD.GetServer(instanceName);

        //    SQLInformation.Data.ApplicationDataSetTableAdapters.ServerInfoTableAdapter tableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServerInfoTableAdapter();

        //    tableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

        //    //int dbID = int.Parse(serverRow.ID_DB);

        //    //SMO.Database db = server.Databases.ItemById(dbID);

        //    SQLInformation.Data.ApplicationDataSet.ServerInfoRow newSnapShot = Common.ApplicationDataSet.ServerInfo.NewServerInfoRow();

        //    newSnapShot.Server_ID = serverRow.ID;
        //    newSnapShot.SnapShotDate = DateTime.Now;

        //    Common.ApplicationDataSet.ServerInfo.AddServerInfoRow(newSnapShot);
        //    tableAdapter.Update(Common.ApplicationDataSet.ServerInfo);
        //}

        private void btnTakeSnapshot_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes): Hook to TakeSnapShot.  See wucAdmin_Databases
        }
    }
}
