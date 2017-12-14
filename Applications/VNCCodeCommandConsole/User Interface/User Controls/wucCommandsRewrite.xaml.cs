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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

using VNCCA = VNC.CodeAnalysis;
using VNCSR = VNC.CodeAnalysis.SyntaxRewriters;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsRewrite : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Constructors

        public wucCommandsRewrite()
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
            LoadControlContents();

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
        private void btnRemove_InvocationExpression_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(Remove_InvocationExpressionVB, CodeExplorer.configurationOptions);
        }
        private void btnWrapSQLFillCallsInDALHelpers_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(WrapSQLFillCallsInDALHelperVB, CodeExplorer.configurationOptions);
        }

        private void btnWrapSQLExecuteXCallsInDALHelpers_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(WrapSQLExecuteXCallsInDALHelperVB, CodeExplorer.configurationOptions);
        }

        private void btnRewrite_InvocationExpression_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(RewriteInvocationExpressionVB, CodeExplorer.configurationOptions);
        }
        private void btnCommentOut_InvocationExpression_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(CommentOut_InvocationExpressionVB, CodeExplorer.configurationOptions);
        }

        //private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        //{
        //    //CustomFormat.FormatStorageColumns(e);
        //}

        #endregion

        //private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        //{
        //    //UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        //}

        #region Main Function Routines

        void ProcessOperation(VNCCA.Types.RewriteFileCommand command,
            wucConfigurationOptions configurationOptions)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();
            CodeExplorer.teSourceCode.InvalidateVisual();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;

            string newInvocationExpression = teNewInvocationExpression.Text;
            string targetInvocationExpression = teTargetInvocationExpression.Text;

            var filesToProcess = CodeExplorerContext.GetFilesToProcess();

            Dictionary<string, Int32> replacements = new Dictionary<string, int>();

            if (filesToProcess.Count > 0)
            {
                if ((Boolean)configurationOptions.ceListImpactedFilesOnly.IsChecked)
                {
                    sb.AppendLine("Would Process these files ....");
                }

                foreach (string filePath in filesToProcess)
                {
                    try
                    {
                        if ((Boolean)configurationOptions.ceListImpactedFilesOnly.IsChecked)
                        {
                            sb.AppendLine(string.Format("  {0}", filePath));
                        }
                        else
                        {
                            StringBuilder sbFileResults = new StringBuilder();

                            var sourceCode = "";

                            using (var sr = new System.IO.StreamReader(filePath))
                            {
                                sourceCode = sr.ReadToEnd();
                            }

                            Boolean performedReplacement = false;

                            // 
                            // This is where the action happens
                            //

                            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                            sbFileResults = command(sbFileResults, tree, filePath, targetInvocationExpression, newInvocationExpression, replacements, out performedReplacement);

                            if ((bool)configurationOptions.ceAlwaysDisplayFileName.IsChecked || (sbFileResults.Length > 0))
                            {
                                sb.AppendLine("Searching " + filePath);
                            }

                            if (performedReplacement)
                            {
                                sb.AppendLine("Rewrote " + filePath);
                            }

                            sb.Append(sbFileResults.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                sb.AppendLine("No files selected to process");
            }

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        #endregion

        private StringBuilder CommentOut_InvocationExpressionVB(
            StringBuilder sb,
            SyntaxTree tree,
            string filePath,
            string targetInvocationExpression, string newInvocationExpression, 
            Dictionary<string, int> replacements, out Boolean performedReplacement)
        {
            
            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.CommentOutSingleLineInvocationExpression(targetInvocationExpression, teComment.Text);

            rewriter.Messages = sb;

            rewriter.IdentifierNames = (bool)ceReplacementTargetUseRegEx.IsChecked ? teTargetInvocationExpression.Text : ".*";
            rewriter.InitializeRegEx();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            if (newNode != tree.GetRoot())
            {
                string newFilePath = filePath + (CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "");

                File.WriteAllText(newFilePath, newNode.ToFullString());
                performedReplacement = true;
            }

            return sb;
        }

        private StringBuilder Remove_InvocationExpressionVB(
            StringBuilder sb,
            SyntaxTree tree,
            string filePath,
            string targetInvocationExpression, string newInvocationExpression,
            Dictionary<string, int> replacements, out Boolean performedReplacement)
        {

            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.RemoveInvocationExpression(targetInvocationExpression, teComment.Text);

            rewriter.Messages = sb;

            rewriter.IdentifierNames = (bool)ceReplacementTargetUseRegEx.IsChecked ? teTargetInvocationExpression.Text : ".*";
            rewriter.InitializeRegEx();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            if (newNode != tree.GetRoot())
            {
                string newFilePath = filePath + (CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "");

                File.WriteAllText(newFilePath, newNode.ToFullString());
                performedReplacement = true;
            }

            return sb;
        }

        private StringBuilder RewriteInvocationExpressionVB(
            StringBuilder sb, 
            SyntaxTree tree,
            string filePath, 
            string targetInvocationExpression, string newInvocationExpression,
            Dictionary<string, int> replacements, out Boolean performedReplacement)
        {
            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.InvocationExpression(targetInvocationExpression, newInvocationExpression);

            rewriter.Messages = sb;

            rewriter.InitializeRegEx();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(filePath, tree, newNode, fileSuffix);

            return sb;
        }

        private StringBuilder WrapSQLExecuteXCallsInDALHelperVB(
            StringBuilder sb, 
            SyntaxTree tree,
            string filePath, 
            string targetPattern, string notUsed,
            Dictionary<string, int> replacements, out bool performedReplacement)
        {
            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.WrapSQLExecuteXCallsInDALHelper(targetPattern);

            rewriter.Messages = sb;

            rewriter.InitializeRegEx();

            rewriter.Replacements = replacements;
            rewriter.Display = CodeExplorer.configurationOptions.GetConfigurationInfo();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(filePath, tree, newNode, fileSuffix);

            return sb;
        }

        StringBuilder WrapSQLFillCallsInDALHelperVB(
            StringBuilder sb, 
            SyntaxTree tree, 
            string filePath, 
            string targetPattern, string notUsed, 
            Dictionary<string, int> replacements, 
            out bool performedReplacement)
        {
            {
                performedReplacement = false;

                var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.WrapSQLFillCallsInDALHelper(targetPattern);

                rewriter.Messages = sb;

                rewriter.InitializeRegEx();

                rewriter.Replacements = replacements;
                rewriter.Display = CodeExplorer.configurationOptions.GetConfigurationInfo();

                SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

                string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

                performedReplacement = VNCSR.Helpers.SaveFileChanges(filePath, tree, newNode, fileSuffix);

                return sb;
            }
        }
    }
}
