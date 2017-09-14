using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsWorkspace : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        public wucCodeExplorer CodeExplorer = null;
        public wucCodeExplorerContext CodeExplorerContext = null;

        #region Constructors

        public wucCommandsWorkspace()
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

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            // Cheat and force outcome if not using dat
            Common.DataFullyLoaded = true;
            User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //dataGrid.ItemsSource = VNCWPFUserControls.Common.ApplicationDataSet.ApplicationUsage;
                }

                //ViewMode.ApplyAuthorization(this);
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
            //CustomFormat.FormatStorageColumns(e);
        }

        #endregion

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        #region Main Function Routines



        #endregion


        private void btnInfo_Document_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb;

            sb = VNC.CodeAnalysis.Workspace.Document.Display(CodeExplorerContext.teSourceFile.Text);

            CodeExplorer.teWorkspace.Text = sb.ToString();
        }

        private void btnInfo_Project_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb;

            sb = VNC.CodeAnalysis.Workspace.Project.Display(CodeExplorerContext.teProjectFile.Text);

            CodeExplorer.teWorkspace.Text = sb.ToString();


        }

        private void btnInfo_Solution_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb;

            sb = VNC.CodeAnalysis.Workspace.Solution.Display(CodeExplorerContext.teSolutionFile.Text);

            CodeExplorer.teWorkspace.Text = sb.ToString();

        }
    }
}
