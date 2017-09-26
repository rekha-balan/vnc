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
    public partial class wucCommandsFindSyntax : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath);
        delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);


        delegate StringBuilder SearchTreeCommand(StringBuilder sb, SyntaxTree tree);
        delegate StringBuilder RewriteTreeCommand(StringBuilder sb, SyntaxTree tree, string targetPattern, string replacementPattern);


        #region Constructors

        public wucCommandsFindSyntax()
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
        private void btnAssignmentStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayAssignmentStatementWalkerVB);
        }
        private void btnLocalDeclarationStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayLocalDeclarationStatementWalkerVB);
        }

        private void btnParameterListWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayParameterListWalkerVB);
        }

        private void btnArgumentListWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayArgumentListWalkerVB);
        }

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

            string identifier = teInvocationExpressionRegEx.Text;

            Boolean includeTrivia = ceIncludeTrivia.IsChecked.Value;
            var additionalLocations = (VNC.CodeAnalysis.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

            sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        #endregion

        #region Main Function Routines
        StringBuilder DisplayAssignmentStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ParameterList();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceAssignmentStatementUseRegEx.IsChecked, teAssignmentStatementRegEx.Text,
                tree, walker);
        }

        StringBuilder DisplayLocalDeclarationStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.LocalDeclarationStatement();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            //walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceLocalDeclarationStatementUseRegEx.IsChecked, teLocalDeclarationStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayStructureBlockWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.StructureBlock();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            //walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.ShowFields = (bool)ceShowFields.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceStructuresUseRegEx.IsChecked, teStructureRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.VariableDeclarator();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            //walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceVariableDeclaratorUseRegEx.IsChecked, teVariableDeclaratorRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.InvocationExpression();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            //walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceInvocationExpressionUseRegEx.IsChecked, teInvocationExpressionRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayParameterListWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ParameterList();

            return InvokeVNCSyntaxWalker(sb, 
                (bool)ceParameterListUseRegEx.IsChecked, teParameterListRegEx.Text, 
                tree, walker);
        }

        private StringBuilder DisplayArgumentListWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ArgumentList();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceArgumentListUseRegEx.IsChecked, teArgumentListRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayPropertyStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowPropertyBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.PropertyBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.PropertyStatement();
            }

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)cePropertyStatementUseRegEx.IsChecked, tePropertyStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayFieldDeclarationWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.FieldDeclaration();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceFieldDeclarationUseRegEx.IsChecked, teFieldDeclarationRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayClassStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker = null;

            if ((bool)ceShowClassBlock.IsChecked)
            {
                 walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ClassBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ClassStatement();
            }

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceClassStatementUseRegEx.IsChecked, teClassStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayImportsStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ImportsStatement();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceImportsStatementUseRegEx.IsChecked, teImportsStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayMethodStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowMethodBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatement();
            }

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceMethodStatementUseRegEx.IsChecked, teMethodStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayModuleStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowModuleBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ModuleBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ModuleStatement();
            }

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceModuleStatementUseRegEx.IsChecked, teModuleStatementRegEx.Text,
                tree, walker);
        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(StringBuilder sb, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.NamespaceStatement();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceNamespaceStatementUseRegEx.IsChecked, teNamespaceStatementRegEx.Text,
                tree, walker);
        }

         #endregion

        #region Utility Methods

        private void ProcessOperation(SearchTreeCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;
            //string pattern = teInvocationExpressionRegEx.Text;

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

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

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
            //walker.Messages = sb;

            //if (useRegEx)
            //{
            //    walker.IdentifierNames = regEx;
            //}
            //else
            //{
            //    walker.IdentifierNames = ".*";
            //}

            //walker.InitializeRegEx();

            //walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            //walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;

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

            return InvokeVNCSyntaxWalker(sb, useRegEx, regEx, syntaxTree, walker);

            //walker.Visit(syntaxTree.GetRoot());

            //return sb;
        }

        #endregion
    }
}
