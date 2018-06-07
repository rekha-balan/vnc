using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
using VNC;


namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Servers : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Constructors

        public Servers()
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
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            EyeOnLife.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    ((CollectionViewSource)this.Resources["serversViewSource"]).Source = EyeOnLife.Common.ApplicationDataSet.Servers;
                    ((CollectionViewSource)this.Resources["adDomains"]).Source = Common.ApplicationDataSet.LKUP_ADDomains;
                    ((CollectionViewSource)this.Resources["environments"]).Source = Common.ApplicationDataSet.LKUP_Environments;
                    ((CollectionViewSource)this.Resources["securityZones"]).Source = Common.ApplicationDataSet.LKUP_SecurityZones;

                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.Servers;
                }

                base.dataGrid = dataGrid;

                ViewMode.ApplyAuthorization(this);

                string[] ckDisplayColumns = { "ckDisplayEnvironmentColumns", "ckDisplayNotesColumns" };

                ViewMode.UpdateDisplayColumnsCheckBoxes(cc_DisplayOptions, ckDisplayColumns);

                LogUsage(this.GetType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Event Handlers

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnGetHelpOn(object sender, RoutedEventArgs e)
        {

        }

        private void OnPingServer_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSet.ServersRow server =
                (SQLInformation.Data.ApplicationDataSet.ServersRow)
                ((System.Data.DataRowView)dataGrid.View.FocusedRowData.Row).Row;

            Helpers.Server.PingServer(server.NetName);
        }

        private void OnItemClick_Cmd2(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void OnNotesChanged(object sender, KeyEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
            ahg_Save.Expanded = true;
        }

        private void OnAddNewRow(object sender, RoutedEventArgs e)
        {
            wndDX_NewServer win1 = new wndDX_NewServer();
            win1.ShowDialog();
        }
        
        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Save.Visibility = System.Windows.Visibility.Visible;
            ahg_Save.Expanded = true;
            lp_Save.Visibility = System.Windows.Visibility.Visible;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Servers_Update();
            //Common.ApplicationDataSet.ServersTA.Update(Common.ApplicationDataSet.Servers);

            ahg_Save.Visibility = System.Windows.Visibility.Hidden;
            ahg_Save.Expanded = false;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Servers.RejectChanges();

            ahg_Save.Visibility = System.Windows.Visibility.Hidden;
            ahg_Save.Expanded = false;
        }

        #endregion

        #region Main Function Routines


        #endregion

    }
}
