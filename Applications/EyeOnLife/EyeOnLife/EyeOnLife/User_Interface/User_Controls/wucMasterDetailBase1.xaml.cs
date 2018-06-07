using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Commands;
using DevExpress.Xpf.Grid;

using EyeOnLife.User_Interface.Windows;
using VNC;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class wucMasterDetailBase1 : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Constructors

        public wucMasterDetailBase1()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Initialization

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //((CollectionViewSource)this.Resources["serversViewSource"]).Source = EyeOnLife.Common.ApplicationDataSet.Servers;
                    //((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                    //((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                    //((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;

                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.DatabaseInfo;


                }

                base.dataGrid = dataGrid;

                ViewMode.ApplyAuthorization(this);

                var ckDisplayEnvironmentColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions, "ckDisplayEnvironmentColumns");
                if (ckDisplayEnvironmentColumns != null)
                {
                    // Not clear why this is null on the first load of the program??
                    ((CheckBox)ckDisplayEnvironmentColumns).IsChecked = true;
                }

                var ckDisplayNotesColumns = VNC.Xaml.PhysicalTree.FindChild<CheckBox>(cc_DisplayOptions, "ckDisplayNotesColumns");
                if (ckDisplayNotesColumns != null)
                {
                    // Not clear why this is null on the first load of the program??
                    ((CheckBox)ckDisplayNotesColumns).IsChecked = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Event Handlers

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //UnboundColumns.GetSnapShotErrorColumns(e);
        }

        private void OnAddNewRow(object sender, RoutedEventArgs e)
        {
            wndDX_NewServer win1 = new wndDX_NewServer();
            win1.ShowDialog();
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
        }


        private void OnCheckAllRows(object sender, ItemClickEventArgs e)
        {

            if (currentColumn != null)
            {
                System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick2: {0} - {1} {2}", currentColumn.Name, currentColumn.FieldName, e.Item.Name);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick2: currentColumn is null");
                return;
            }

            bool isChecked = (e.Item.Name == "bi_CheckAll" ? true : false);

            // TODO(crhodes): 
            //      This might be cleaner with  the loop inside of the switch.
            //      Need to determine how to pass column to a routine with a loop.

            foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
            {
                switch (currentColumn.FieldName)
                {
                    case "ExpandContent":
                        instance.ExpandContent = isChecked;
                        break;

                    case "ExpandJobServer":
                        instance.ExpandJobServer = isChecked;
                        break;

                    case "ExpandLinkedServers":
                        instance.ExpandLinkedServers = isChecked;
                        break;

                    case "ExpandLogins":
                        instance.ExpandLogins = isChecked;
                        break;

                    case "ExpandServerRoles":
                        instance.ExpandServerRoles = isChecked;
                        break;

                    case "ExpandStorage":
                        instance.ExpandStorage = isChecked;
                        break;

                    case "ExpandTriggers":
                        instance.ExpandTriggers = isChecked;
                        break;

                    default:
                        System.Diagnostics.Debug.WriteLine("Ignoring {0}", currentColumn.FieldName);
                        break;

                }
            }

            ahg_Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void OnColumnHeaderRightClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var foo = (BarCheckItem)sender;
            var bar = (BarCheckItem)e.Item;

            if (bar.Parent != null)
            {
                var imgr = bar.Parent;
                //var own = imgr.Owner;
            }
            var prn = bar.Parent;

            // BarManager changed in 14.2.  No longer has a .Manager property
            if (BarManager.GetBarManager(e.Item) != null)
            //if (bar.Manager != null)
            {
                var mgr = BarManager.GetBarManager(e.Item);

                var tv = (TableView)mgr.Parent;

                if (currentColumn != null)
                {
                    System.Diagnostics.Debug.WriteLine("OnColumnHeaderRightClick: {0} - {1}", currentColumn.Name, currentColumn.FieldName);
                }
                else
                {
                    return;
                }

                var isChecked = (bool)bar.IsChecked;

                foreach (SQLInformation.Data.ApplicationDataSet.InstancesRow instance in Common.ApplicationDataSet.Instances)
                {

                    switch (currentColumn.FieldName)
                    {
                        case "ExpandContent":
                            instance.ExpandContent = isChecked;
                            break;

                        case "ExpandJobServer":
                            instance.ExpandJobServer = isChecked;
                            break;

                        case "ExpandLinkedServers":
                            instance.ExpandLinkedServers = isChecked;
                            break;

                        case "ExpandLogins":
                            instance.ExpandLogins = isChecked;
                            break;

                        case "ExpandServerRoles":
                            instance.ExpandServerRoles = isChecked;
                            break;

                        case "ExpandStorage":
                            instance.ExpandStorage = isChecked;
                            break;

                        case "ExpandTriggers":
                            instance.ExpandTriggers = isChecked;
                            break;

                        default:
                            System.Diagnostics.Debug.WriteLine("Ignoring {0}", currentColumn.FieldName);
                            break;

                    }
                }
            }
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnGetHelpOnInstanceExpandTemplate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DetailsPane_Styles");
        }

        //private void OnFocusedColumnChanged(object sender, DevExpress.Xpf.Grid.FocusedColumnChangedEventArgs e)
        //{
        //    var foo = (TableView)sender;
        //    var bar = (TableView)e.Source;
        //    var column = (GridColumn)e.NewColumn;
        //    System.Diagnostics.Debug.WriteLine("OnFocusedColumnChanged: {0} - {1}", column.Name, column.FieldName);

        //}

        //private void OnColumnHeaderClick(object sender, DevExpress.Xpf.Grid.ColumnHeaderClickEventArgs e)
        //{
        //    var foo = (TableView)sender;
        //    var bar = (TableView)e.Source;
        //    var column = e.Column;
        //    System.Diagnostics.Debug.WriteLine("OnColumnHeaderClick: {0} - {1}", column.Name, column.FieldName);
        //}

        private void OnShowGridMenu(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            var foo = (TableView)sender;
            var bar = (TableView)e.Source;
            var mnui = (GridColumnMenuInfo)e.MenuInfo;
            var mnut = e.MenuType;

            if (mnut == GridMenuType.Column)
            {
                currentColumn = mnui.Column;

                GridColumn column = mnui.Column;
                System.Diagnostics.Debug.WriteLine("OnShowGridMenu: {0} - {1}", column.Name, column.FieldName);

                switch (currentColumn.FieldName)
                {
                    case "ExpandContent":
                    case "ExpandJobServer":
                    case "ExpandLinkedServers":
                    case "ExpandLogins":
                    case "ExpandServerRoles":
                    case "ExpandStorage":
                    case "ExpandTriggers":
                        bi_CheckAll.IsVisible = true;
                        bi_UnCheckAll.IsVisible = true;
                        break;

                    default:
                        bi_CheckAll.IsVisible = false;
                        bi_UnCheckAll.IsVisible = false;
                        break;

                }
            }
        }

        private void OnDeleteAllRows(object sender, RoutedEventArgs e)
        {
            //if (MessageBoxResult.Yes == MessageBox.Show("Are you really, really sure you want to delete everything?", "Confirm Action", MessageBoxButton.YesNo))
            //{
            //    Common.ApplicationDataSet.Servers.Clear();
            //    Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            //}
        }

        private void OnDeleteRow(object sender, RoutedEventArgs e)
        {
            //if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete the selected servers?", "Confirm Action", MessageBoxButton.YesNo))
            //{
            //    //tableView1.DeleteRow(tableView1.FocusedRowHandle);

            //    try
            //    {
            //        DataRow[] rowsToDelete = new DataRow[tableView.SelectedRows.Count];

            //        for (int i = 0; i < tableView.SelectedRows.Count; i++)
            //        {
            //            rowsToDelete[i] = ((DataRowView)tableView.SelectedRows[i]).Row;
            //        }

            //        foreach (DataRow row in rowsToDelete)
            //        {
            //            row.Delete();
            //        }

            //        Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
        }

        private void OnFocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var row = e.NewRow;
            var row2 = (SQLInformation.Data.ApplicationDataSet.DatabaseInfoRow)((DataRowView)row).Row;
            var dbID = row2.Database_ID;

            var chartSource = EyeOnLife.Common.ApplicationDataSet.DatabaseInfo
                .Where(di => di.Database_ID == dbID)
                .Select(i => new { 
                    SnapShotDate = i.SnapShotDate, 
                    IndexSpaceUsage = i.IndexSpaceUsage / 1024.0, 
                    DataSpaceUsage = i.DataSpaceUsage / 1024.0,
                    SpaceAvailable = i.SpaceAvailable / 1024.0,
                    Size = i.Size
                });
            
            series1.DataSource = chartSource;
            series1a.DataSource = chartSource;
            series1b.DataSource = chartSource;
            try
            {
                XYDiagram2D.SetSeriesAxisY(series1b, ((XYDiagram2D)chart1.Diagram).SecondaryAxesY[0]);
            }
            catch (Exception ex)
            {
                
            }
            
            series2.DataSource = chartSource;
            series2a.DataSource = chartSource;
            series2b.DataSource = chartSource;
            series2c.DataSource = chartSource;

            series3.DataSource = chartSource;
            series3a.DataSource = chartSource;
            series3b.DataSource = chartSource;
            series3c.DataSource = chartSource;
        }

        #endregion

        #region Main Function Routines

        private void PostDataToXML()
        {
            string outputFile = @"C:\temp\ServersDataGrid.xml";

            var v1 = dataGrid.ItemsSource;
            var v3 = ((System.Windows.Data.BindingListCollectionView)v1).SourceCollection;
            var v4 = ((System.Data.DataView)v3).Table;
            v4.WriteXml(outputFile);

            //Common.ApplicationDataSet.Instances.WriteXml(outputFile);
        }

        #endregion

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
