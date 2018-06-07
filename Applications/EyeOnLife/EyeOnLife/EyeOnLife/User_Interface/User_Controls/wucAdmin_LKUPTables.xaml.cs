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
using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_LKUPTables.xaml
    /// </summary>
    public partial class wucAdmin_LKUPTables : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wucAdmin_LKUPTables()
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

        private void saveADButton_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.LKUP_ADDomainsTableAdapter adDomainTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.LKUP_ADDomainsTableAdapter();

            adDomainTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            adDomainTableAdapter.Update(Common.ApplicationDataSet.LKUP_ADDomains);
        }

        private void saveEnvironmentButton_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.LKUP_EnvironmentsTableAdapter environmentsTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.LKUP_EnvironmentsTableAdapter();

            environmentsTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            environmentsTableAdapter.Update(Common.ApplicationDataSet.LKUP_Environments);
        }

        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            // Read the data from the XML file.

            //Test_Data.InstanceData.LoadNewInstanceListFromFile();
            Test_Data.LKUPData.LoadNewListFromFile();

            foreach(var adDomain in Test_Data.LKUPData.ADDomains)
            {
                SQLInformation.Data.ApplicationDataSet.LKUP_ADDomainsRow newRow = Common.ApplicationDataSet.LKUP_ADDomains.NewLKUP_ADDomainsRow();
                newRow.ID = adDomain.ID;
                newRow.ADDomainName = adDomain.Name;
                Common.ApplicationDataSet.LKUP_ADDomains.AddLKUP_ADDomainsRow(newRow);
            }

            foreach(var environment in Test_Data.LKUPData.Environments)
            {
                SQLInformation.Data.ApplicationDataSet.LKUP_EnvironmentsRow newRow = Common.ApplicationDataSet.LKUP_Environments.NewLKUP_EnvironmentsRow();
                newRow.ID = environment.ID;
                newRow.EnvironmentName = environment.Name;
                Common.ApplicationDataSet.LKUP_Environments.AddLKUP_EnvironmentsRow(newRow);
            }
        }

        private void undoADButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.LKUP_ADDomains.RejectChanges();
        }

        private void undoEnvironmentButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.LKUP_Environments.RejectChanges();
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

            //((SQLInformation.Data.ApplicationDataSet.InstancesRow)e.Row).id = Guid.NewGuid();
            _editMode = false;
        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myADomainsDViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["lKUP_ADDomainsViewSource"];
                System.Windows.Data.CollectionViewSource myEnvironmentsViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["lKUP_EnvironmentsViewSource"];
                myADomainsDViewSource.Source = EyeOnLife.Common.ApplicationDataSet.LKUP_ADDomains;
                myEnvironmentsViewSource.Source = EyeOnLife.Common.ApplicationDataSet.LKUP_Environments;
            }
        }


        private void dataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //var itm = e.NewItem;
            //var dataRowView = (System.Data.DataRowView)itm;
            //var dataRowView = (System.Data.DataRowView)e.NewItem;

            //var dataRow = dataRowView.Row;

            //var server = (SQLInformation.Data.ApplicationDataSet.ServersRow)dataRowView.Row;
            var server = (SQLInformation.Data.ApplicationDataSet.ServersRow)((System.Data.DataRowView)e.NewItem).Row;

            //SQLInformation.Data.ApplicationDataSet.InstancesRow newRow = (SQLInformation.Data.ApplicationDataSet.InstancesRow)((DataGridRow)e.NewItem).Row;
            server.ID = Guid.NewGuid();
        }

        private void lKUP_ADDomainsDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {

        }

        private void lKUP_EnvironmentsDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {

        }
    }
}
