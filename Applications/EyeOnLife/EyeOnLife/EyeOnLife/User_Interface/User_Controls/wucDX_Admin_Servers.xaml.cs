using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DevExpress.Xpf.Core;

using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucDX_Admin_Servers.xaml
    /// </summary>
    public partial class wucDX_Admin_Servers : ucBase
    {
        public wucDX_Admin_Servers()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
                //tableView1.BestFitColumns();

                //CollectionViewSource myADDomains = (CollectionViewSource)this.Resources["adDomains"];
                //myADDomains.Source = Common.ApplicationDataSet.LKUP_ADDomains;

                ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;
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
            Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            SaveButton.Visibility = System.Windows.Visibility.Hidden;
            SaveButton.IsEnabled = false;

            CancelButton.Visibility = System.Windows.Visibility.Hidden;
            CancelButton.IsEnabled = false;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Servers.RejectChanges();
            SaveButton.Visibility = System.Windows.Visibility.Hidden;
            SaveButton.IsEnabled = false;

            CancelButton.Visibility = System.Windows.Visibility.Hidden;
            CancelButton.IsEnabled = false;
        }

        private void btnAddNewRow_Click(object sender, RoutedEventArgs e)
        {
            wndDX_NewServer win1 = new wndDX_NewServer();
            win1.ShowDialog();
        }

        private void PostDataToXML()
        {
            string outputFile = @"C:\temp\ServersDataGrid.xml";

            var v1 = dataGrid.ItemsSource;
            var v3 = ((System.Windows.Data.BindingListCollectionView)v1).SourceCollection;
            var v4 = ((System.Data.DataView)v3).Table;
            v4.WriteXml(outputFile);

            //Common.ApplicationDataSet.Instances.WriteXml(outputFile);
        }

        private void btnSaveToXML_Click(object sender, RoutedEventArgs e)
        {
            PostDataToXML();
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete the selected server?", "Confirm Action", MessageBoxButton.YesNo))
            {
                tableView1.DeleteRow(tableView1.FocusedRowHandle);
            }
        }

        private void OnDeleteAllRows(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you really, really sure you want to delete everything?", "Confirm Action", MessageBoxButton.YesNo))
            {
                Common.ApplicationDataSet.Servers.Clear();
                Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            }
        }

        private void OnGetIPAddress(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet.ServersRow server =
                (SQLInformation.Data.ApplicationDataSet.ServersRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            server.IPAddress = Helpers.Server.GetIPV4Address(server.NetName);

            Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
        }

        private void OnGetMultipleIPAddress(object sender, RoutedEventArgs e)
        {
            var rows = tableView1.SelectedRows;

            foreach (DataRowView s in tableView1.SelectedRows)
            {
                SQLInformation.Data.ApplicationDataSet.ServersRow server = 
                    (SQLInformation.Data.ApplicationDataSet.ServersRow)s.Row;

                server.IPAddress = Helpers.Server.GetIPV4Address(server.NetName);

                Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            }
        }

        private void OnGetAllIPAddress(object sender, RoutedEventArgs e)
        {
            foreach (SQLInformation.Data.ApplicationDataSet.ServersRow server in Common.ApplicationDataSet.Servers)
            {
                try
                {
                    server.IPAddress = Helpers.Server.GetIPV4Address(server.NetName);

                    Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Could not get IP Address for {0}.", server.NetName));
                }
            }
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            SaveButton.Visibility = System.Windows.Visibility.Visible;
            SaveButton.IsEnabled = true;

            CancelButton.Visibility = System.Windows.Visibility.Visible;
            CancelButton.IsEnabled = true;
        }
    }
}
