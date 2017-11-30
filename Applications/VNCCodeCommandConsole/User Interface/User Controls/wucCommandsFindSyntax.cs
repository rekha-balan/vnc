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

using VNCCA = VNC.CodeAnalysis;
using VNCSW = VNC.CodeAnalysis.SyntaxWalkers;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsFindVBSyntax : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

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
            Helper.ProcessOperation(DisplayMemberAccessExpressionWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnSimpleAsClauseWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySimpleAsClauseWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnObjectCreationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayObjectCreationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnSyntaxTriviaWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTriviaWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnSyntaxTokenWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTokenWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnSyntaxNodeWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxNodeWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnBinaryExpresionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayBinaryExpressiontWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnAssignmentStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayAssignmentStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnLocalDeclarationStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayLocalDeclarationStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnParameterListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayParameterListWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnArgumentListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayArgumentListWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnPropertyStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayPropertyStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnFieldDeclarationWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayFieldDeclarationWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnModuleStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayModuleStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnMethodStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayMethodStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnClassStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayClassStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnNamespaceStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayNamespaceStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnFindStructures_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayStructureBlockWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayVariableDeclaratorWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnImportsStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayImportsStatementWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
        }

        private void btnInvocationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayInvocationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, outputOptions);
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
            var additionalLocations = (VNCCA.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

            sb = VNCCA.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        #endregion

        #region Main Function Routines

        private StringBuilder DisplayMemberAccessExpressionWalkerVB(StringBuilder sb, Dictionary<string, int> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.MemberAccessExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceMemberAccessExpressionUseRegEx.IsChecked, teMemberAccessExpressionRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplaySyntaxNodeWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.SyntaxNode();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxNodeUseRegEx.IsChecked, teSyntaxNodeRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplaySyntaxTokenWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.SyntaxToken();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTokenUseRegEx.IsChecked, teSyntaxTokenRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplaySimpleAsClauseWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.SimpleAsClause();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSimpleAsClauseUseRegEx.IsChecked, teSimpleAsClauseRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplayObjectCreationExpressionWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.ObjectCreationExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceObjectCreationExpressionUseRegEx.IsChecked, teObjectCreationExpressionRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplaySyntaxTriviaWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.SyntaxTrivia();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTriviaUseRegEx.IsChecked, teSyntaxTriviaRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayBinaryExpressiontWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.BinaryExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceBinaryExpressionUseRegEx.IsChecked, teBinaryExpressionRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }
        StringBuilder DisplayAssignmentStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.AssignmentStatement();
            walker.MatchLeft = (bool)ceAssignmentStatementMatchLeft.IsChecked;
            walker.MatchRight = (bool)ceAssignmentStatementMatchRight.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceAssignmentStatementUseRegEx.IsChecked, teAssignmentStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        StringBuilder DisplayLocalDeclarationStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.LocalDeclarationStatement();

            walker.HasAttributes = (bool)outputOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceLocalDeclarationStatementUseRegEx.IsChecked, teLocalDeclarationStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayStructureBlockWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.StructureBlock();

            walker.ShowFields = (bool)ceStructureShowFields.IsChecked;

            walker.HasAttributes = (bool)outputOptions.ceHasAttributes.IsChecked;

            walker.AllFieldTypes = (bool)outputOptions.ceAllTypes.IsChecked;
            walker.FieldNames = (bool)ceStructureFieldsUseRegEx.IsChecked ? teStructureFieldsRegEx.Text : ".*";
            walker.StructureNames = (bool)ceStructuresUseRegEx.IsChecked ? teStructureRegEx.Text : ".*";

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceStructuresUseRegEx.IsChecked, teStructureRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.VariableDeclarator();

            walker.HasAttributes = (bool)outputOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceVariableDeclaratorUseRegEx.IsChecked, teVariableDeclaratorRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.InvocationExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceInvocationExpressionUseRegEx.IsChecked, teInvocationExpressionRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayParameterListWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.ParameterList();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb, 
                (bool)ceParameterListUseRegEx.IsChecked, teParameterListRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayArgumentListWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.ArgumentList();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceArgumentListUseRegEx.IsChecked, teArgumentListRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayPropertyStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowPropertyBlock.IsChecked)
            {
                walker = new VNCSW.VB.PropertyBlock();
            }
            else
            {
                walker = new VNCSW.VB.PropertyStatement();
            }

            walker.HasAttributes = (bool)outputOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)cePropertyStatementUseRegEx.IsChecked, tePropertyStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayFieldDeclarationWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNCSW.VB.FieldDeclaration();

            walker.HasAttributes = (bool)outputOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceFieldDeclarationUseRegEx.IsChecked, teFieldDeclarationRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayClassStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNCSW.VB.VNCVBSyntaxWalkerBase walker = null;

            if ((bool)ceShowClassBlock.IsChecked)
            {
                 walker = new VNCSW.VB.ClassBlock();
            }
            else
            {
                walker = new VNCSW.VB.ClassStatement();
            }

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceClassStatementUseRegEx.IsChecked, teClassStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayImportsStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.ImportsStatement();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceImportsStatementUseRegEx.IsChecked, teImportsStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayMethodStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowMethodBlock.IsChecked)
            {
                walker = new VNCSW.VB.MethodBlock();
            }
            else
            {
                walker = new VNCSW.VB.MethodStatement();
                //(walker as VNCSW.VB.MethodStatement).ShowParameters = (Boolean)ceShowMethodParameters.IsChecked;
                ((VNCSW.VB.MethodStatement)walker).ShowParameters = (Boolean)ceShowMethodParameters.IsChecked;
            }

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceMethodStatementUseRegEx.IsChecked, teMethodStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayModuleStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            if ((bool)ceShowModuleBlock.IsChecked)
            {
                walker = new VNCSW.VB.ModuleBlock();
            }
            else
            {
                walker = new VNCSW.VB.ModuleStatement();
            }

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceModuleStatementUseRegEx.IsChecked, teModuleStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree)
        {
            var walker = new VNCSW.VB.NamespaceStatement();

           // VNC.CodeAnalysis.DisplayInfo displayInfo = new VNCCA.DisplayInfo();

           //displayInfo.ClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
           // displayInfo.MethodName = (bool)ceDisplayMethodName.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceNamespaceStatementUseRegEx.IsChecked, teNamespaceStatementRegEx.Text,
                matches, tree, walker, outputOptions.GetDisplayInfo());
        }



        #endregion

        #region Utility Methods

        //private void ProcessOperation(VNCCA.Types.SearchTreeCommand command)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    CodeExplorer.teSourceCode.Clear();
        //    CodeExplorer.teSourceCode.InvalidateVisual();
        //    //CodeExplorer.teSourceCode.Text = "";

        //    string projectFullPath = CodeExplorerContext.teProjectFile.Text;

        //    var filesToProcess = CodeExplorerContext.GetFilesToProcess();

        //    Dictionary<string, Int32> matches = new Dictionary<string, int>();

        //    if (filesToProcess.Count > 0)
        //    {
        //        if ((Boolean)outputOptions.ceListImpactedFilesOnly.IsChecked)
        //        {
        //            sb.AppendLine("Would Search these files ....");
        //        }

        //        foreach (string filePath in filesToProcess)
        //        {
        //            if ((Boolean)outputOptions.ceListImpactedFilesOnly.IsChecked)
        //            {
        //                sb.AppendLine(string.Format("  {0}", filePath));
        //            }
        //            else
        //            {
        //                StringBuilder sbFileResults = new StringBuilder();

        //                var sourceCode = "";

        //                using (var sr = new StreamReader(filePath))
        //                {
        //                    sourceCode = sr.ReadToEnd();
        //                }

        //                SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

        //                sbFileResults = command(sbFileResults, matches, tree);

        //                if ((bool)outputOptions.ceAlwaysDisplayFileName.IsChecked || (sbFileResults.Length > 0))
        //                {
        //                    sb.AppendLine("Searching " + filePath);
        //                }

        //                sb.Append(sbFileResults.ToString());
        //            }
        //        }
        //    }
        //    else
        //    {
        //        sb.AppendLine("No files selected to process");
        //    }

        //    if (! (Boolean)outputOptions.ceDisplayResults.IsChecked)
        //    {
        //        // If only want to see the summary ...
        //        sb.Clear();
        //    }

        //    if ((Boolean)outputOptions.ceDisplaySummary.IsChecked)
        //    {
        //        // Add information from the matches dictionary
        //        sb.AppendLine("Summary");

        //        foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
        //        {
        //            if (matches[item] >= outputOptions.sbDisplaySummaryMinimum.Value)
        //            {
        //                sb.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));                       
        //            }
        //        }
        //    }

        //    CodeExplorer.teSourceCode.Text = sb.ToString();
        //}

        //StringBuilder InvokeVNCSyntaxWalker(
        //    StringBuilder sb,
        //    Boolean useRegEx, string regEx,
        //    Dictionary<string, Int32> matches, SyntaxTree syntaxTree,
        //    VNCSW.VB.VNCVBSyntaxWalkerBase walker)
        //{
        //    walker.Messages = sb;

        //    VNC.CodeAnalysis.DisplayInfo displayInfo = new VNCCA.DisplayInfo();

        //    displayInfo.ClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
        //    displayInfo.MethodName = (bool)ceDisplayMethodName.IsChecked;

        //    walker.IdentifierNames = useRegEx ? regEx : ".*";

        //    walker.InitializeRegEx();

        //    walker.Matches = matches;

        //    walker.Visit(syntaxTree.GetRoot());

        //    return sb;
        //}

        //StringBuilder InvokeVNCTypedSyntaxWalker(
        //    StringBuilder sb,
        //    Boolean useRegEx, string regEx,
        //    Dictionary<string, Int32> matches, SyntaxTree syntaxTree,
        //    VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker)
        //{
        //    VNC.CodeAnalysis.DisplayInfo displayInfo = new VNCCA.DisplayInfo();

        //    displayInfo.AllTypes = (bool)ceAllTypes.IsChecked;

        //    displayInfo.Byte = (bool)ceIsByte.IsChecked;
        //    displayInfo.Boolean = (bool)ceIsBoolean.IsChecked;
        //    displayInfo.Date = (bool)ceIsDate.IsChecked;
        //    displayInfo.DataTable = (bool)ceIsDataTable.IsChecked;
        //    displayInfo.DateTime = (bool)ceIsDateTime.IsChecked;
        //    displayInfo.Int16 = (bool)ceIsInt16.IsChecked;
        //    displayInfo.Int32 = (bool)ceIsInt32.IsChecked;
        //    displayInfo.Integer = (bool)ceIsInteger.IsChecked;
        //    displayInfo.Long = (bool)ceIsLong.IsChecked;
        //    displayInfo.Single = (bool)ceIsSingle.IsChecked;
        //    displayInfo.String = (bool)ceIsString.IsChecked;

        //    displayInfo.OtherTypes = (bool)ceOtherTypes.IsChecked;

        //    return VNC.CodeAnalysis.Helpers.VB.InvokeVNCSyntaxWalker(sb, useRegEx, regEx, matches, syntaxTree, walker);
        //}

        #endregion
    }
}
