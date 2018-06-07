using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//using SMO = Microsoft.SqlServer.Management.Smo;
//using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;


using DevExpress.Xpf.Core;
using PacificLife.Life;

using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucDX_Admin_Instances.xaml
    /// </summary>
    public partial class wucDX_Admin_Instances : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wucDX_Admin_Instances()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["instancesViewSource"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.Instances;
                tableView1.BestFitColumns();
            }
        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            gridColumn_ID.Visible = true;
        }


        private void HideIDColumns(Nullable<bool> isChecked)
        {
            if ((bool)isChecked)
            {
                gridColumn_ID.Visible = true;
            }
            else
            {
                gridColumn_ID.Visible = false;
            }
        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn_ID.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = true;
            gridColumn_SnapShotError.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = false;
            gridColumn_SnapShotError.Visible = false;
        }

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
            Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances.RejectChanges();
        }

        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            Test_Data.InstanceData.LoadNewInstancesInfoFromFile();

            foreach (var instance in Test_Data.InstanceData.DBInstances)
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

        private void btnSyncInstanceToServer_Click(object sender, RoutedEventArgs e)
        {
            // This for DataGrid

            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            // This for GridControl
            var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;


            SyncServerInfoAndInstanceInfo(instance);
        }


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
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //try
            //{
            //    SMO.Server server = SQLInformation.SMO.Helper.Get_Server(instance.Name_Instance);

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

            //    }

            //    instance.EngineEdition = server.EngineEdition.ToString();
            //    instance.Product = server.Product;
            //    instance.ProductLevel = server.ProductLevel;
            //    instance.Version = server.VersionString;

            //    instance.SnapShotDate = DateTime.Now;
            //    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
            //}
            //catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            //{
            //    instance.NetName = "Cannot Connect";
            //    instance.SnapShotDate = DateTime.Now;
            //    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            //}
        }

        #endregion
        private void PostDataToXML()
        {
            var v1 = dataGrid.ItemsSource;
            var v2 = (SQLInformation.Data.ApplicationDataSet.InstancesDataTable)v1;

            ((SQLInformation.Data.ApplicationDataSet.InstancesDataTable)dataGrid.ItemsSource).WriteXml(@"C:\temp\InstancesDataGrid.xml");
        }

        private void btnSaveToXML_Click(object sender, RoutedEventArgs e)
        {
            PostDataToXML();
        }

        private void btnAddNewRow_Click(object sender, RoutedEventArgs e)
        {
            wndDX_NewInstance win1 = new wndDX_NewInstance();
            win1.ShowDialog();
        }

    }
}
