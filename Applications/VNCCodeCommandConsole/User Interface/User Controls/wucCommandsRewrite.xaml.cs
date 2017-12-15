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

        #endregion

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

                            VNCCA.RewriteFileCommandConfiguration rewriteFileCommandConfiguration = new VNCCA.RewriteFileCommandConfiguration();
                            rewriteFileCommandConfiguration.Results = sbFileResults;
                            rewriteFileCommandConfiguration.SyntaxTree = tree;
                            rewriteFileCommandConfiguration.FilePath = filePath;
                            rewriteFileCommandConfiguration.Replacements = replacements;

                            rewriteFileCommandConfiguration.UseRegEx = (bool)ceReplacementTargetUseRegEx.IsChecked;
                            rewriteFileCommandConfiguration.TargetPattern = teTargetInvocationExpression.Text;
                            rewriteFileCommandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

                            sbFileResults = command(rewriteFileCommandConfiguration, out performedReplacement);
                            //sbFileResults = command(sbFileResults, tree, filePath, targetInvocationExpression, newInvocationExpression, replacements, out performedReplacement);

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


        private StringBuilder RewriteInvocationExpressionVB(VNCCA.RewriteFileCommandConfiguration commandConfiguration, out bool performedReplacement)
        {
            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.InvocationExpression(
                commandConfiguration.TargetPattern, commandConfiguration.ReplacementPattern);

            rewriter.Messages = commandConfiguration.Results;

            rewriter._configurationOptions = commandConfiguration.ConfigurationOptions;

            SyntaxNode newNode = rewriter.Visit(commandConfiguration.SyntaxTree.GetRoot());

            string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(commandConfiguration, newNode);

            return commandConfiguration.Results;
        }

        private StringBuilder WrapSQLExecuteXCallsInDALHelperVB(VNCCA.RewriteFileCommandConfiguration commandConfiguration, out bool performedReplacement)
        {
            performedReplacement = false;

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.WrapSQLExecuteXCallsInDALHelper(
                commandConfiguration.TargetPattern);

            rewriter._configurationOptions = commandConfiguration.ConfigurationOptions;

            rewriter.Messages = commandConfiguration.Results;

            SyntaxNode newNode = rewriter.Visit(commandConfiguration.SyntaxTree.GetRoot());

            string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

            performedReplacement = VNCSR.Helpers.SaveFileChanges(commandConfiguration, newNode);

            return commandConfiguration.Results;
        }

        StringBuilder WrapSQLFillCallsInDALHelperVB(VNCCA.RewriteFileCommandConfiguration commandConfiguration, out bool performedReplacement)
        {
            {
                performedReplacement = false;

                var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.WrapSQLFillCallsInDALHelper(commandConfiguration.TargetPattern);

                rewriter._configurationOptions = commandConfiguration.ConfigurationOptions;

                rewriter.Messages = commandConfiguration.Results;

                SyntaxNode newNode = rewriter.Visit(commandConfiguration.SyntaxTree.GetRoot());

                string fileSuffix = CodeExplorer.configurationOptions.ceAddFileSuffix.IsChecked.Value ? CodeExplorer.configurationOptions.teFileSuffix.Text : "";

                performedReplacement = VNCSR.Helpers.SaveFileChanges(commandConfiguration, newNode);

                return commandConfiguration.Results;
            }
        }
    }
}
