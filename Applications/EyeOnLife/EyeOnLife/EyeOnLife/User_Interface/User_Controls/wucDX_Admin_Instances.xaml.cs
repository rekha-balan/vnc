﻿using System;
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

using DevExpress.Xpf.Core;
using System.Diagnostics;
using DevExpress.Xpf.Core.Commands;
//using DevExpress.Xpf.DemoBase;
using DevExpress.Utils;

//using SMO = Microsoft.SqlServer.Management.Smo;
//using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;


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
                //tableView1.BestFitColumns();
                ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;
 
            }
        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            gridColumn_ID.Visible = true;
            gridColumn_Server_ID.Visible = true;
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
            gridColumn_Server_ID.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = true;
            gridColumn_SnapShotDuration.Visible = true;
            gridColumn_SnapShotError.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = false;
            gridColumn_SnapShotDuration.Visible = false;
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
                    newInstance.NetName = instance.server;
                    newInstance.DefaultDatabaseExpandMask = 2047;
                    newInstance.DefaultJobServerExpandMask = 8191;
                    Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
                }

            }
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                (SQLInformation.Data.ApplicationDataSet.InstancesRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            // Check to see if have linked to a server yet.  If not, do that.

            try
            {
                Guid serverID;

                Guid.TryParse(instance.Server_ID.ToString(), out serverID);
            }
            catch (Exception )
            {
                SyncServerInfoAndInstanceInfo(instance);                
            }

            FetchSMOInfo(instance);

        }

        private void btnSyncInstanceToServer_Click(object sender, RoutedEventArgs e)
        {
            // This for DataGrid

            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            // This for GridControl
            //var row = dataGrid.View.FocusedRowData.Row;

            //SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
            //    (SQLInformation.Data.ApplicationDataSet.InstancesRow)
            //    ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;
            //SyncServerInfoAndInstanceInfo(instance);

            foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
            {
                SyncServerInfoAndInstanceInfo(instance);
            }

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

                // TODO: Verify these are set before trying to SYNC.

                try
                {
                    server.OSVersion = instance.OSVersion;
                    server.PhysicalMemory = instance.PhysicalMemory;
                    server.Platform = instance.Platform;
                    server.Processors = instance.Processors;
                }
                catch (Exception )
                {
                    
                }

                server.EndEdit();
            }
            catch (Exception )
            {
                // No matching server.
                //MessageBox.Show("No matching server found.  Adding new server.");

                int instanceExpandMask = 127;

                try
                {
                    SQLInformation.Data.ApplicationDataSet.ServersRow newServer = Common.ApplicationDataSet.Servers.NewServersRow();

                    newServer.ID = Guid.NewGuid();
                    newServer.NetName = instance.NetName;
                    newServer.DefaultInstanceExpandMask = instanceExpandMask;

                    // Wrap in exception in case not populated

                    try
                    {
                        newServer.OSVersion = instance.OSVersion;
                        newServer.PhysicalMemory = instance.PhysicalMemory;
                        newServer.Platform = instance.Platform;
                        newServer.Processors = instance.Processors;
                    }
                    catch (Exception )
                    {
                        
                    }

                    Common.ApplicationDataSet.Servers.AddServersRow(newServer);
                    Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);

                    instance.Server_ID = newServer.ID;
                    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);

                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }

            }
        }

        private void FetchSMOInfo(SQLInformation.Data.ApplicationDataSet.InstancesRow instanceRow)
        {
            SQLInformation.ExpandMask.InstanceExpandSetting instanceExpandSetting = new SQLInformation.ExpandMask.InstanceExpandSetting(instanceRow.Server_ID);
            SQLInformation.ExpandMask.JobServerExpandSetting jobServerExpandSetting = new SQLInformation.ExpandMask.JobServerExpandSetting(instanceRow.DefaultJobServerExpandMask);
            SQLInformation.ExpandMask.DatabaseExpandSetting databaseExpandSetting = new SQLInformation.ExpandMask.DatabaseExpandSetting(instanceRow.DefaultDatabaseExpandMask);

            SQLInformation.SMO.Instance.GetInfoFromSMO(instanceRow, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
            //SQLInformation.SMO.Helper.Get_Instance_FromSMO(instanceRow, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
        }

        #endregion

        private void PostDataToXML()
        {
            string outputFile = @"C:\temp\InstancesDataGrid.xml";

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

        private void btnAddNewRow_Click(object sender, RoutedEventArgs e)
        {
            wndDX_NewInstance win1 = new wndDX_NewInstance();
            win1.ShowDialog();
        }

        private void RowTemplateComboBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            //if (grid == null)
            //    return;
            ////OnCurrentThemeNameChanged();
            //if (rowTemplateComboBox.SelectedIndex == 0)
            //    view.DataRowTemplate = (DataTemplate)Resources["expandableRowDetailTemplate"];
            //if (rowTemplateComboBox.SelectedIndex == 1)
            //    view.DataRowTemplate = (DataTemplate)Resources["rowDetailTemplate"];
            //if (rowTemplateComboBox.SelectedIndex == 2)
            //{
            //    view.DataRowTemplate = (DataTemplate)Resources.MergedDictionaries[Resources.MergedDictionaries.Count - 1]["mailMergeRowDetailTemplate"];
            //}
            //if (rowTemplateComboBox.SelectedIndex == 3)
            //    view.DataRowTemplate = (DataTemplate)grid.Resources["rowToolTipTemplate"];
            //if (rowTemplateComboBox.SelectedIndex == 4)
            //    view.ClearValue(DevExpress.Xpf.Grid.TableView.DataRowTemplateProperty);
        }

        private void OnCreateSMOLogin(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView s in tableView1.SelectedRows)
            {
                var row = dataGrid.View.FocusedRowData.Row;

                SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                    (SQLInformation.Data.ApplicationDataSet.InstancesRow)s.Row;

                SQLInformation.SMO.Actions.Instance.CreateSQLMonitorLogin(instance.Name_Instance);               
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete the selected Instance?", "Confirm Action", MessageBoxButton.YesNo))
            {
                tableView1.DeleteRow(tableView1.FocusedRowHandle);
            }
        }

        private void OnDeleteAllRows(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you really, really sure you want to delete everything?", "Confirm Action", MessageBoxButton.YesNo))
            {
                Common.ApplicationDataSet.Instances.Clear();
                Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
            }
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            //SaveButton.Visibility = System.Windows.Visibility.Visible;
            //SaveButton.IsEnabled = true;

            //CancelButton.Visibility = System.Windows.Visibility.Visible;
            //CancelButton.IsEnabled = true;
        }

    }

    public class RowDetailContainerControl : ContentControl
    {
        public RowDetailContainerControl()
        {
            this.SetDefaultStyleKey(typeof(RowDetailContainerControl));
        }
    }
}
