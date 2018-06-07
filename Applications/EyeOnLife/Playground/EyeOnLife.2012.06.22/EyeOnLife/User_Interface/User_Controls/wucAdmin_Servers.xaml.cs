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
using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;

//using SMOH = SMOHelper;

using SQLInformation;

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Servers.xaml
    /// </summary>
    public partial class wucAdmin_Servers : ucBase
    {
        const string TYPE_NAME = "wucAdmin_Servers";

        public wucAdmin_Servers()
        {

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
            Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter = new Data.ApplicationDataSetTableAdapters.ServersTableAdapter();

            serversTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            serversTableAdapter.Update(Common.ApplicationDataSet.Servers);

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
                // Things work if this line is present.  Testing to see if it works without 6/13/2012
                // Yup, still works.  Don't need this line as it is done in the XAML.
                myCollectionViewSource.Source = SQLInformation.Common.ApplicationDataSet.Servers;
             }
        }

        private void UpdateInfo()
        {
            Data.ApplicationDataSet.ServersRow server = 
                (Data.ApplicationDataSet.ServersRow)
                ((System.Data.DataRowView)serversDataGrid.SelectedItem).Row;

            Console.WriteLine(server.NetName);
            Console.WriteLine(server.id.ToString());
            System.Diagnostics.Debug.WriteLine(server.NetName);

            //var row1 = serversDataGrid.SelectedItem;

            //var gridRow = serversDataGrid.ItemContainerGenerator.ContainerFromItem(serversDataGrid.SelectedItem);

            //System.Data.DataRowView row2 = (System.Data.DataRowView)serversDataGrid.SelectedItem;

            //var realRow = row2.Row;

            //var realRow2 = (Data.ApplicationDataSet.ServersRow)row2.Row;

            //Data.ApplicationDataSet.ServersRow typedRow = (Data.ApplicationDataSet.ServersRow)row2.Row;

            //ManagedComputer mc = new ManagedComputer(typedRow.name);

            //Common.WriteToDebugWindow(typedRow.name);
            //Common.WriteToDebugWindow(String.Format("Name:{0}  State:{1}", mc.Name, mc.State));

            //foreach (ClientProtocol cp in mc.ClientProtocols)
            //{
            //    Common.WriteToDebugWindow(String.Format("  CP Name:{0}  Order:{1}  IsEnabled:{2}", cp.Name, cp.Order, cp.IsEnabled));
            //}

            //foreach (ServerInstance si in mc.ServerInstances)
            //{
            //    Common.WriteToDebugWindow(String.Format("  SI Name:{0}", si.Name));

            //    foreach (ServerProtocol sp in si.ServerProtocols)
            //    {
            //        Common.WriteToDebugWindow(String.Format("    SP Name:{0}  IsEnabled:{1}", sp.Name, sp.IsEnabled));

            //        foreach (ServerIPAddress sipa in sp.IPAddresses)
            //        {
            //            Common.WriteToDebugWindow(String.Format("        SIPA Name:{0}  IPAddress:{1}  State:{2}", sipa.Name, sipa.IPAddress, sipa.State ));

            //            foreach (IPAddressProperty ipap in sipa.IPAddressProperties)
            //            {
            //                Common.WriteToDebugWindow(String.Format("        IPAP Name:{0}  Value:{1}", ipap.Name, ipap.Value));
            //            }
            //        }
            //    }
            //}
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        private void serversDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //var itm = e.NewItem;
            //var dataRowView = (System.Data.DataRowView)itm;
            //var dataRowView = (System.Data.DataRowView)e.NewItem;

            //var dataRow = dataRowView.Row;

            //var server = (Data.ApplicationDataSet.ServersRow)dataRowView.Row;
            var server = (Data.ApplicationDataSet.ServersRow)((System.Data.DataRowView)e.NewItem).Row;

            //Data.ApplicationDataSet.InstancesRow newRow = (Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
            server.id = Guid.NewGuid();
        }
    }
}
