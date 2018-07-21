using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

using VNC_WPF_Application1.User_Interface.Windows;
using VNC;
//using SQLInformation;

namespace VNC_WPF_Application1.User_Interface.User_Controls
{
    public partial class wucApplicationUsage : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = VNC_WPF_Application1.ErrorNumbers.VNC_WPF_Application1;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Constructors

        public wucApplicationUsage()
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
            VNC_WPF_Application1.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    dataGrid.ItemsSource = VNC_WPF_Application1.Common.ApplicationDS.ApplicationUsage;
                }

                ViewMode.ApplyAuthorization(this);
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

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            //CustomFormat.FormatStorageColumns(e);
        }

        #endregion

        #region Main Function Routines



        #endregion

        #region Private Methods


        #endregion

        #region Utility Routines


        #endregion

    }
}
