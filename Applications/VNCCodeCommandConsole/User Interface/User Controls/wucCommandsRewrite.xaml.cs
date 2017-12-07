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
        private void btnWrapInDebug2_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(WrapSQLCallsInDebug2VB);
        }

        private void btnRewrite_InvocationExpression_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(RewriteInvocationExpressionVB);
        }
        private void btnCommentOut_InvocationExpression_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(CommentFileVB);
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

        void ProcessOperation(VNC.CodeAnalysis.Types.RewriteFileCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;
            string newInvocationExpression = teNewInvocationExpression.Text;
            string targetInvocationExpression = teTargetInvocationExpression.Text;

            var filesToProcess = CodeExplorerContext.GetFilesToProcess();

            Dictionary<string, Int32> replacements = new Dictionary<string, int>();

            if (filesToProcess.Count > 0)
            {
                if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                {
                    sb.AppendLine("Would Process these files ....");
                }

                foreach (string filePath in filesToProcess)
                {
                    try
                    {
                        if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                        {
                            sb.AppendLine(string.Format("  {0}", filePath));
                        }
                        else
                        {
                            Boolean performedReplacement = false;

                            sb = command(sb, filePath, targetInvocationExpression, newInvocationExpression, replacements, out performedReplacement);

                            if (performedReplacement)
                            {
                                sb.AppendLine("Rewrote " + filePath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
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

        private StringBuilder CommentFileVB(
            StringBuilder sb, 
            string filePath, 
            string targetInvocationExpression, string newInvocationExpression, 
            Dictionary<string, int> replacements, out Boolean performedReplacement)
        {
            string sourceCode;
            performedReplacement = false;

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.CommentOutSingleLineInvocationExpression(targetInvocationExpression, teComment.Text);

            rewriter.Messages = sb;

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            if (newNode != tree.GetRoot())
            {
                string newFilePath = filePath + (ceAddFileSuffix.IsChecked.Value ? teFileSuffix.Text : "");

                File.WriteAllText(newFilePath, newNode.ToFullString());
                performedReplacement = true;
            }

            return sb;
        }

        private StringBuilder RewriteInvocationExpressionVB(
            StringBuilder sb, 
            string filePath, 
            string targetInvocationExpression, string newInvocationExpression,
            Dictionary<string, int> replacements, out Boolean performedReplacement)
        {
            string sourceCode;
            performedReplacement = false;

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.InvocationExpression(targetInvocationExpression, newInvocationExpression);

            rewriter.Messages = sb;

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            string fileSuffix = ceAddFileSuffix.IsChecked.Value ? teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(filePath, tree, newNode, fileSuffix);

            return sb;
        }

        private StringBuilder WrapSQLCallsInDebug2VB(
            StringBuilder sb, 
            string filePath, 
            string targetPattern, string notUsed,
            Dictionary<string, int> replacements, out bool performedReplacement)
        {
            string sourceCode;
            performedReplacement = false;

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);


            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.WrapSQLCallsInDebug2(targetPattern);

            rewriter.Messages = sb;
            rewriter.Replacements = replacements;
            rewriter.Display = CodeExplorer.outputOptions.GetDisplayInfo();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            string fileSuffix = ceAddFileSuffix.IsChecked.Value ? teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(filePath, tree, newNode, fileSuffix);

            return sb;
        }
    }
}
