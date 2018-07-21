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
using System.Windows.Shapes;

using DevExpress.Xpf.Core;
using VNC;
using VNC_WPF_Application1.User_Interface;
//using SQLInformation;

namespace VNC_WPF_Application1.User_Interface.User_Controls
{

    public partial class wucTableViewerFancy : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = VNC_WPF_Application1.ErrorNumbers.VNC_WPF_Application1;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        public wucTableViewerFancy()
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

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            VNC_WPF_Application1.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //dataGrid.ItemsSource = VNC_WPF_Application1.Common.ApplicationDS.DatabaseInfo;
                }

                ViewMode.ApplyAuthorization(this);
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

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

    }
}
