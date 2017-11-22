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
    public partial class wucCommandsFindVBSyntax : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath);
        delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);


        delegate StringBuilder SearchTreeCommand(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree);
        delegate StringBuilder RewriteTreeCommand(StringBuilder sb, SyntaxTree tree, string targetPattern, string replacementPattern);


        #region Constructors

        public wucCommandsFindVBSyntax()
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

        private void btnMemberAccessExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayMemberAccessExpressionWalkerVB);
        }

        private void btnSimpleAsClauseWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplaySimpleAsClauseWalkerVB);
        }

        private void btnObjectCreationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayObjectCreationExpressionWalkerVB);
        }

        private void btnSyntaxTriviaWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplaySyntaxTriviaWalkerVB);
        }

        private void btnSyntaxTokenWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplaySyntaxTokenWalkerVB);
        }

        private void btnSyntaxNodeWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplaySyntaxNodeWalkerVB);
        }

        private void btnBinaryExpresionWalker_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(DisplayBinaryExpressiontWalkerVB);
        }

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

        private StringBuilder DisplayMemberAccessExpressionWalkerVB(StringBuilder sb, Dictionary<string, int> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MemberAccessExpression();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceMemberAccessExpressionUseRegEx.IsChecked, teMemberAccessExpressionRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplaySyntaxNodeWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.SyntaxNode();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxNodeUseRegEx.IsChecked, teSyntaxNodeRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplaySyntaxTokenWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.SyntaxToken();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTokenUseRegEx.IsChecked, teSyntaxTokenRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplaySimpleAsClauseWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.SimpleAsClause();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceSimpleAsClauseUseRegEx.IsChecked, teSimpleAsClauseRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplayObjectCreationExpressionWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ObjectCreationExpression();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceObjectCreationExpressionUseRegEx.IsChecked, teObjectCreationExpressionRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplaySyntaxTriviaWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.SyntaxTrivia();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTriviaUseRegEx.IsChecked, teSyntaxTriviaRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayBinaryExpressiontWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.BinaryExpression();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceBinaryExpressionUseRegEx.IsChecked, teBinaryExpressionRegEx.Text,
                matches, tree, walker);
        }
        StringBuilder DisplayAssignmentStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AssignmentStatement();
            walker.MatchLeft = (bool)ceAssignmentStatementMatchLeft.IsChecked;
            walker.MatchRight = (bool)ceAssignmentStatementMatchRight.IsChecked;

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceAssignmentStatementUseRegEx.IsChecked, teAssignmentStatementRegEx.Text,
                matches, tree, walker);
        }

        StringBuilder DisplayLocalDeclarationStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.LocalDeclarationStatement();

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceLocalDeclarationStatementUseRegEx.IsChecked, teLocalDeclarationStatementRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayStructureBlockWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.StructureBlock();

            walker.ShowFields = (bool)ceStructureShowFields.IsChecked;

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            walker.AllFieldTypes = (bool)ceAllTypes.IsChecked;
            walker.FieldNames = (bool)ceStructureFieldsUseRegEx.IsChecked ? teStructureFieldsRegEx.Text : ".*";
            walker.StructureNames = (bool)ceStructuresUseRegEx.IsChecked ? teStructureRegEx.Text : ".*";

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceStructuresUseRegEx.IsChecked, teStructureRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.VariableDeclarator();

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceVariableDeclaratorUseRegEx.IsChecked, teVariableDeclaratorRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.InvocationExpression();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceInvocationExpressionUseRegEx.IsChecked, teInvocationExpressionRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayParameterListWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ParameterList();

            return InvokeVNCSyntaxWalker(sb, 
                (bool)ceParameterListUseRegEx.IsChecked, teParameterListRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayArgumentListWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ArgumentList();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceArgumentListUseRegEx.IsChecked, teArgumentListRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayPropertyStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
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

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)cePropertyStatementUseRegEx.IsChecked, tePropertyStatementRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayFieldDeclarationWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.FieldDeclaration();

            walker.HasAttributes = (bool)ceHasAttributes.IsChecked;

            return InvokeVNCTypedSyntaxWalker(sb,
                (bool)ceFieldDeclarationUseRegEx.IsChecked, teFieldDeclarationRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayClassStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
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
                matches, tree, walker);
        }

        private StringBuilder DisplayImportsStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.ImportsStatement();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceImportsStatementUseRegEx.IsChecked, teImportsStatementRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayMethodStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowMethodBlock.IsChecked)
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodBlock();
            }
            else
            {
                walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatement();
                //(walker as VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatement).ShowParameters = (Boolean)ceShowMethodParameters.IsChecked;
                ((VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatement)walker).ShowParameters = (Boolean)ceShowMethodParameters.IsChecked;
            }

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceMethodStatementUseRegEx.IsChecked, teMethodStatementRegEx.Text,
                matches, tree, walker);
        }

        private StringBuilder DisplayModuleStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
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
                matches, tree, walker);
        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.NamespaceStatement();

            return InvokeVNCSyntaxWalker(sb,
                (bool)ceNamespaceStatementUseRegEx.IsChecked, teNamespaceStatementRegEx.Text,
                matches, tree, walker);
        }

        #endregion

        #region Utility Methods

        private void ProcessOperation(SearchTreeCommand command)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teSourceCode.Clear();
            CodeExplorer.teSourceCode.InvalidateVisual();
            //CodeExplorer.teSourceCode.Text = "";

            string projectFullPath = CodeExplorerContext.teProjectFile.Text;

            var filesToProcess = CodeExplorerContext.GetFilesToProcess();

            Dictionary<string, Int32> matches = new Dictionary<string, int>();

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
                        StringBuilder sbFileResults = new StringBuilder();

                        var sourceCode = "";

                        using (var sr = new StreamReader(filePath))
                        {
                            sourceCode = sr.ReadToEnd();
                        }

                        SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                        sbFileResults = command(sbFileResults, matches, tree);

                        if ((bool)ceAlwaysDisplayFileName.IsChecked || (sbFileResults.Length > 0))
                        {
                            sb.AppendLine("Searching " + filePath);
                        }

                        sb.Append(sbFileResults.ToString());
                    }
                }
            }
            else
            {
                sb.AppendLine("No files selected to process");
            }

            if (! (Boolean)ceDisplayResults.IsChecked)
            {
                // If only want to see the summary ...
                sb.Clear();
            }

            if ((Boolean)ceDisplaySummary.IsChecked)
            {
                // Add information from the matches dictionary
                sb.AppendLine("Summary");

                foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (matches[item] >= sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));                       
                    }
                }
            }

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        StringBuilder InvokeVNCSyntaxWalker(
            StringBuilder sb,
            Boolean useRegEx, string regEx,
            Dictionary<string, Int32> matches, SyntaxTree syntaxTree,
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker)
        {
            walker.Messages = sb;

            walker.DisplayClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            walker.DisplayMethodName = (bool)ceDisplayMethodName.IsChecked;
            walker.IdentifierNames = useRegEx ? regEx : ".*";

            walker.InitializeRegEx();

            walker.Matches = matches;

            walker.Visit(syntaxTree.GetRoot());

            return sb;
        }

        StringBuilder InvokeVNCTypedSyntaxWalker(
            StringBuilder sb,
            Boolean useRegEx, string regEx,
            Dictionary<string, Int32> matches, SyntaxTree syntaxTree,
            VNC.CodeAnalysis.SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker)
        {
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

            return InvokeVNCSyntaxWalker(sb, useRegEx, regEx, matches, syntaxTree, walker);
        }

        #endregion
    }
}
