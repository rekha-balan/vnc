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

        delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath, string pattern);
        delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);

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
        private void btnPropertyStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayPropertyStatementWalkerVB);
        }
        private void btnFieldDeclarationWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayFieldDeclarationWalkerVB);
        }
        private void btnModuleStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayModuleStatementWalkerVB);
        }
        private void btnMethodStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayMethodStatementWalkerVB);
        }
        private void btnClassStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayClassStatementWalkerVB);
        }
        private void btnNamespaceStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayNamespaceStatementWalkerVB);
        }
        private void btnFindStructures_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayStructureBlockWalkerVB);
        }
        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayVariableDeclaratorWalkerVB);
        }
        private void btnImportsStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayImportsStatementWalkerVB);
        }

        private void btnHttpContextWalker_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((System.Windows.Controls.Button)sender).Tag.ToString();

            DisplayHttpContextWalkerVB(CodeExplorerContext, tag);
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

            string identifier = teIdentifierRegEx.Text;

            Boolean includeTrivia = ceIncludeTrivia.IsChecked.Value;
            var additionalLocations = (VNC.CodeAnalysis.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

            sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        #endregion

        #region Main Function Routines
        StringBuilder DisplayPropertyStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowPropertyBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.PropertyBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.PropertyStatement();
            }

            walker.Messages = sb;

            if ((bool)cePropertyStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = tePropertyStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;

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

        StringBuilder DisplayFieldDeclarationWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.FieldDeclaration();

            walker.Messages = sb;

            if ((bool)ceFieldDeclarationUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teFieldDeclarationRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;

            //walker.AllFieldTypes = (bool)ceAllTypes.IsChecked;
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

        StringBuilder DisplayClassStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowClassBlock.IsChecked)
            {
                 walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ClassBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ClassStatement();
            }

            walker.Messages = sb;

            if ((bool)ceClassStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teClassStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private StringBuilder DisplayImportsStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ImportsStatement();

            walker.Messages = sb;

            if ((bool)ceImportsStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teImportsStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.Visit(tree.GetRoot());

            return sb;
        }

        StringBuilder DisplayMethodStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowMethodBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatement();
            }

            walker.Messages = sb;

            if ((bool)ceMethodStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teMethodStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        StringBuilder DisplayModuleStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowModuleBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ModuleBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ModuleStatement();
            }

            walker.Messages = sb;

            if ((bool)ceModuleStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teModuleStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.NamespaceStatement();

            walker.Messages = sb;

            if ((bool)ceNamespaceStatementUseRegEx.IsChecked)
            {
                walker.IdentifierNames = teNamespaceStatementRegEx.Text;
            }
            else
            {
                walker.IdentifierNames = ".*";
            }

            walker.InitializeRegEx();

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        private void ProcessOperation(SearchFileCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;
            string pattern = teIdentifierRegEx.Text;

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
                        sb = command(sb, filePath, pattern);
                    }
                }
            }
            else
            {
                sb.AppendLine("No files selected to process");
            }

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

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

        #endregion

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, string filePath, string pattern)
        {
            //StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.InvocationExpression();

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
    }
}
