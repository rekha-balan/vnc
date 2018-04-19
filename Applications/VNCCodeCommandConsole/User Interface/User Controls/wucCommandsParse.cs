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
    public partial class wucCommandsParse : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        //public wucCodeExplorer CodeExplorer = null;
        //public wucCodeExplorerContext CodeExplorerContext = null;

        #region Constructors

        public wucCommandsParse()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace15("Start", LOG_APPNAME);
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
            VNC.AppLog.Trace15("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Initialization

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace15("Start", LOG_APPNAME);
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
            VNC.AppLog.Trace15("End", LOG_APPNAME, startTicks);
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

        private void btnParseSourceVB_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = null;
            CodeExplorer.teSyntaxTree.Text = "";

            // TODO(crhodes)
            // Need to also get our hands on SyntaxTree that got produced.
            // Either change return type
            // or add out parameter
            // or add another method that returns string

            switch (lbeSyntaxWalkerDepth.EditValue.ToString())
            {
                case "Node":
                    sb = Commands.Explore.ParseVBDepthNode(teSourceCode1.Text, CodeExplorer.configurationOptions);
                    break;

                case "StructuredTrivia":
                    sb = Commands.Explore.ParseVBStructuredTrivia(teSourceCode1.Text, CodeExplorer.configurationOptions);
                    break;

                case "Token":
                    sb = Commands.Explore.ParseVBDepthToken(teSourceCode1.Text, CodeExplorer.configurationOptions);
                    break;

                case "Trivia":
                    sb = Commands.Explore.ParseVBDepthTrivia(teSourceCode1.Text, CodeExplorer.configurationOptions);
                    break;
            }

            CodeExplorer.teSyntaxTree.Text = sb.ToString();

        }

        private void btnParseSourceCS_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = null;
            CodeExplorer.teSyntaxTree.Text = "";

            // TODO(crhodes)
            // Need to also get our hands on SyntaxTree that got produced.
            // Either change return type
            // or add out parameter
            // or add another method that returns string

            switch (lbeSyntaxWalkerDepth.EditValue.ToString())
            {
                case "Node":
                    sb = Commands.Explore.ParseCSDepthNode(teSourceCode1.Text);
                    break;

                case "StructuredTrivia":
                    sb = Commands.Explore.ParseCSStructuredTrivia(teSourceCode1.Text);
                    break;

                case "Token":
                    sb = Commands.Explore.ParseCSDepthToken(teSourceCode1.Text);
                    break;

                case "Trivia":
                    sb = Commands.Explore.ParseCSDepthTrivia(teSourceCode1.Text);
                    break;
            }

            CodeExplorer.teSyntaxTree.Text = sb.ToString();

        }
    }
}
