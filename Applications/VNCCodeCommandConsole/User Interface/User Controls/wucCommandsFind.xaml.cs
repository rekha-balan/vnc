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
    public partial class wucCommandsFind : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        public wucCodeExplorer CodeExplorer = null;
        public wucCodeExplorerContext CodeExplorerContext = null;

        #region Constructors

        public wucCommandsFind()
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

        private void btnInvocation_Click(object sender, RoutedEventArgs e)
        {
            //Boolean includeTrivia = ceStructuresIncludeTrivia.IsChecked.Value;
            //Boolean statementsOnly = ceStructuresStatementsOnly.IsChecked.Value;

            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(CodeExplorerContext.teSourceFile.Text))
            {
                sourceCode = sr.ReadToEnd();
            }

            string identifier = teIdentifier.Text;

            //sb = Commands.Explore.CodeToCommentRatioVB(wucCodeExplorerContext.teSourceFile.Text);
            sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, true, identifier);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }
    }
}
