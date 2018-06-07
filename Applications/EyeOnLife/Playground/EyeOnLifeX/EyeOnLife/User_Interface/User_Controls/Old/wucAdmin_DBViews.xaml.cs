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

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Views.xaml
    /// </summary>
    public partial class wucAdmin_DBViews : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wucAdmin_DBViews()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["viewsViewSource"];
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.DBViews;
            }
        }

        #region Event Handlers


        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            //Test_Data.InstanceData.LoadNewInstanceListFromFile();

            //foreach(var instance in Test_Data.InstanceData.DBInstances)
            //{
            //    Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();
            //    newInstance.ID = Guid.NewGuid();
            //    newInstance.InstanceName = instance.FullName;
            //    Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
            //}
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
            //try
            //{
            //    HIS.Library.Common.HISSchema.Save();
            //    MessageBox.Show("Your changes were saved");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Your changes were not saved");
            //    MessageBox.Show(ex.Message);
            //}
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
            _editMode = false;
        }


        private void dataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //var itm = e.NewItem;
            //var dataRowView = (System.Data.DataRowView)itm;
            //var dataRow = dataRowView.Row;
            //var instance = (Data.ApplicationDataSet.InstancesRow)dataRow;

            ////Data.ApplicationDataSet.InstancesRow newRow = (Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
            //instance.ID = Guid.NewGuid();
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

    }
}
