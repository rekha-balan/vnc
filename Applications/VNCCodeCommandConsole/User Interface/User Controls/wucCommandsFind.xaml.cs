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
        private void btnHttpContextWalker_Click(object sender, RoutedEventArgs e)
        {
            //string controlTag = (string)e.Source
            var foo = e;
            var tag = ((System.Windows.Controls.Button)sender).Tag.ToString();

            DisplayHttpContextWalkerVB(CodeExplorerContext, tag);
            //DisplayHttpContextWalkerVB(CodeExplorerContext.teSourceFile.Text, tag);
        }

        delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath, string pattern);
        delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);

        void ProcessOperation(SearchFileCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;
            string invocationExpression = teIdentifier.Text;

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
                        sb = command(sb, filePath, invocationExpression);
                    }
                }
            }
            else
            {
                sb.AppendLine("No files selected to process");
            }

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        private void btnInvocationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayInvocationExpressionWalkerVB);
        }

        private void btnInvocation_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            var sourceCode = "";

            using (var sr = new StreamReader(CodeExplorerContext.teSourceFile.Text))
            {
                sourceCode = sr.ReadToEnd();
            }

            string identifier = teIdentifier.Text;

            Boolean includeTrivia = ceIncludeTrivia.IsChecked.Value;
            var additionalLocations = (VNC.CodeAnalysis.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

            sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        //private void wucFindPicker_ControlChanged()
        //{

        //}

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

        void DisplayHttpContextWalkerVB(wucCodeExplorerContext codeExplorerContext, string context)
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

        #endregion

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, string filePath, string invocationExpression)
        {
            //StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.InvocationExpression(invocationExpression);

            walker.Messages = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            //StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.VariableDeclarator();

            walker.Messages = sb;

            if ((bool)ceVariablesUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teVariableRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.AllTypes = (bool)ceAllTypes.IsChecked;
            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            walker.IsBoolean = (bool)ceIsBoolean.IsChecked;
            walker.IsDate = (bool)ceIsDate.IsChecked;
            walker.IsDateTime = (bool)ceIsDateTime.IsChecked;
            walker.IsInt16 = (bool)ceIsInt16.IsChecked;
            walker.IsInt32 = (bool)ceIsInt32.IsChecked;
            walker.IsInteger = (bool)ceIsInteger.IsChecked;
            walker.IsLong = (bool)ceIsLong.IsChecked;
            walker.IsSingle = (bool)ceIsSingle.IsChecked;
            walker.IsString = (bool)ceIsString.IsChecked;

            walker.IsOtherType = (bool)ceOtherTypes.IsChecked;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private StringBuilder DisplayStructureBlockWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            //StringBuilder sb = new StringBuilder();
            Boolean showFields = (bool)ceShowFields.IsChecked;

            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.StructureBlock();

            walker.Messages = sb;

            if ((bool)ceStructuresUseRegEx.IsChecked)
            {
                walker.StructureNames = teStructureRegEx.Text;
            }
            else
            {
                walker.StructureNames = ".*";
            }

            if ((bool)ceFieldsUseRegEx.IsChecked)
            {
                walker.FieldNames = teFieldsRegEx.Text;
            }
            else
            {
                walker.FieldNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.ShowFields = (bool)ceShowFields.IsChecked;

            walker.AllFieldTypes = (bool)ceAllTypes.IsChecked;
            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            walker.IsBoolean = (bool)ceIsBoolean.IsChecked;
            walker.IsDate = (bool)ceIsDate.IsChecked;
            walker.IsDateTime = (bool)ceIsDateTime.IsChecked;
            walker.IsInt16 = (bool)ceIsInt16.IsChecked;
            walker.IsInt32 = (bool)ceIsInt32.IsChecked;
            walker.IsInteger = (bool)ceIsInteger.IsChecked;
            walker.IsLong = (bool)ceIsLong.IsChecked;
            walker.IsSingle = (bool)ceIsSingle.IsChecked;
            walker.IsString = (bool)ceIsString.IsChecked;

            walker.IsOtherType = (bool)ceOtherTypes.IsChecked;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayVariableDeclaratorWalkerVB);
        }

        private void btnFindStructures_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayStructureBlockWalkerVB);
        }

        private void ceFieldsUseRegEx_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ceFieldsUseRegEx_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {

        }
    }
}
