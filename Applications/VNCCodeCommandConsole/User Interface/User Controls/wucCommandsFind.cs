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
using Microsoft.CodeAnalysis.VisualBasic;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsFind : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath);
        delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);


        delegate StringBuilder SearchTreeCommand(StringBuilder sb, SyntaxTree tree);
        delegate StringBuilder RewriteTreeCommand(StringBuilder sb, SyntaxTree tree, string targetPattern, string replacementPattern);


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

        //private void LoadControlContents()
        //{
        //    //try
        //    //{
        //    //    wucFind_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.ToString());
        //    //}
        //}

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
        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayMultipleVariableDeclaratorWalkerVB);
        }

        private void btnHttpContextWalker_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((System.Windows.Controls.Button)sender).Tag.ToString();

            DisplayHttpContextWalkerVB(CodeExplorerContext, tag);
        }

        #endregion

        #region Main Function Routines


        private void DisplayHttpContextWalkerVB(wucCodeExplorerContext codeExplorerContext, string context)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, Int32> matches = new Dictionary<string, Int32>();

            var sourceCode = "";

            // If a source file has been specified use it

            if (codeExplorerContext.teSourceFile.Text.Length > 0)
            {
                string fileNameAndPath = codeExplorerContext.teSourceFile.Text;

                if (!File.Exists(fileNameAndPath))
                {
                    sb.AppendLine(string.Format("File ({0}) does not exist.  Check path and name.", fileNameAndPath));
                    goto PrematureExitBummer;
                }

                if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                {
                    sb.AppendLine("Would Process these files ....");
                    sb.AppendLine(string.Format("  {0}", fileNameAndPath));
                }
                else
                {
                    sb.AppendLine(string.Format("Processing ({0}) ...", fileNameAndPath));

                    using (var sr = new StreamReader(fileNameAndPath))
                    {
                        sourceCode = sr.ReadToEnd();
                    }

                    var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                    string pattern = string.Empty;

                    VNC.CodeAnalysis.SyntaxWalkers.VB.HttpContextCurrentInvocationExpression walker = null;
                    walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.HttpContextCurrentInvocationExpression(context);
                    walker.Messages = sb;
                    walker.Matches = matches;

                    walker.Visit(tree.GetRoot());
                }
            }
            else
            {
                // Otherwise, go see if one or more files has been selected

                if (codeExplorerContext.cbeSourceFile.SelectedItems.Count > 0)
                {
                    if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                    {
                        sb.AppendLine("Would Process these files ....");
                    }

                    foreach (XElement file in codeExplorerContext.cbeSourceFile.SelectedItems)
                    {
                        string filePath = codeExplorerContext.teSourcePath.Text + wucCodeExplorerContext.GetFilePath(file);

                        if ( ! File.Exists(filePath))
                        {
                            sb.AppendLine(string.Format("ERROR File ({0}) does not exist.  Check config file", filePath));
                            continue;
                        }

                        if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                        {
                            sb.AppendLine(string.Format("  {0}", filePath));
                        }
                        else
                        {
                            sb.AppendLine(string.Format("Processing ({0}) ...", filePath));

                            using (var sr = new StreamReader(filePath))
                            {
                                sourceCode = sr.ReadToEnd();
                            }

                            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                            string pattern = string.Empty;

                            VNC.CodeAnalysis.SyntaxWalkers.VB.HttpContextCurrentInvocationExpression walker = null;
                            walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.HttpContextCurrentInvocationExpression(context);
                            walker.Messages = sb;
                            walker.Matches = matches;

                            walker.Visit(tree.GetRoot());
                        }
                    }
                }
            }

            sb.AppendLine("Summary");

            foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
            {
                sb.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));
            }

 PrematureExitBummer:
            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        private StringBuilder DisplayMultipleVariableDeclaratorWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MultipleVariableDeclarator();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceVariablesUseRegEx.IsChecked, teVariableRegEx.Text,
                tree, walker);
        }

        #endregion

        #region Utility Methods

        private void ProcessOperation(SearchTreeCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;

            var filesToProcess = CodeExplorerContext.GetFilesToProcess();

            if (filesToProcess.Count > 0)
            {
                if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                {
                    sb.AppendLine("Would Search these files ....");
                }

                foreach (string filePath in filesToProcess)
                {
                    if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                    {
                        sb.AppendLine(string.Format("  {0}", filePath));
                    }
                    else
                    {
                        sb.AppendLine("Searching " + filePath);

                        var sourceCode = "";

                        using (var sr = new StreamReader(filePath))
                        {
                            sourceCode = sr.ReadToEnd();
                        }

                        SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                        sb = command(sb, tree);
                    }
                }
            }
            else
            {
                sb.AppendLine("No files selected to process");
            }

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        StringBuilder InvokeVNCSyntaxWalker(
            StringBuilder sb, 
            Boolean useRegEx, string regEx,
            SyntaxTree syntaxTree,
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker)
        {
            walker.Messages = sb;

            if (useRegEx)
            {
                walker.IdentifierNames = regEx;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.Visit(syntaxTree.GetRoot());

            return sb;
        }

        StringBuilder InvokeVNCTypedSyntaxWalker(
            StringBuilder sb,
            Boolean useRegEx, string regEx,
            SyntaxTree syntaxTree,
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker)
        {
            walker.Messages = sb;

            if (useRegEx)
            {
                walker.IdentifierNames = regEx;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.AllTypes = (bool)ceAllTypes.IsChecked;

            walker.IsByte = (bool)ceIsByte.IsChecked;
            walker.IsBoolean = (bool)ceIsBoolean.IsChecked;
            walker.IsDate = (bool)ceIsDate.IsChecked;
            walker.IsDataTable = (bool)ceIsDataTable.IsChecked;
            walker.IsDateTime = (bool)ceIsDateTime.IsChecked;
            walker.IsInt16 = (bool)ceIsInt16.IsChecked;
            walker.IsInt32 = (bool)ceIsInt32.IsChecked;
            walker.IsInteger = (bool)ceIsInteger.IsChecked;
            walker.IsLong = (bool)ceIsLong.IsChecked;
            walker.IsSingle = (bool)ceIsSingle.IsChecked;
            walker.IsString = (bool)ceIsString.IsChecked;

            walker.IsOtherType = (bool)ceOtherTypes.IsChecked;

            walker.Visit(syntaxTree.GetRoot());

            return sb;
        }

        #endregion
    }
}
