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
//#if TRACE
//            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
//#endif
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
//#if TRACE
//            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
//#endif
        }

        #endregion

        #region Initialization

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
//#if TRACE
//            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
//#endif
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
//#if TRACE
//            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
//#endif
        }

        #endregion

        #region Event Handlers

        private void btnMemberAccessExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayMemberAccessExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSimpleAsClauseWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySimpleAsClauseWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnObjectCreationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayObjectCreationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxTriviaWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTriviaWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxTokenWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTokenWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxNodeWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxNodeWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnBinaryExpresionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayBinaryExpressiontWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnAssignmentStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayAssignmentStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnLocalDeclarationStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayLocalDeclarationStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnParameterListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayParameterListWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnArgumentListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayArgumentListWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnPropertyStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayPropertyStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnFieldDeclarationWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayFieldDeclarationWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnModuleStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayModuleStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnMethodStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayMethodStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnClassStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayClassStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnNamespaceStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayNamespaceStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnFindStructures_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayStructureBlockWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayVariableDeclaratorWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnImportsStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayImportsStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnInvocationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayInvocationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        //private void btnInvocation_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    CodeExplorer.teSourceCode.Clear();

        //    var sourceCode = "";

        //    using (var sr = new StreamReader(CodeExplorerContext.teSourceFile.Text))
        //    {
        //        sourceCode = sr.ReadToEnd();
        //    }

        //    string identifier = teInvocationExpressionRegEx.Text;

        //    Boolean includeTrivia = ceIncludeTrivia.IsChecked.Value;
        //    var additionalLocations = (VNCCA.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

        //    //sb = VNCCA.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

        //    CodeExplorer.teSourceCode.Text = sb.ToString();
        //}

        #endregion

        #region Main Function Routines

        private StringBuilder DisplayMemberAccessExpressionWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, int> matches)
        {
            var walker = new VNCSW.VB.MemberAccessExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceMemberAccessExpressionUseRegEx.IsChecked, teMemberAccessExpressionRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplaySyntaxNodeWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.SyntaxNode();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxNodeUseRegEx.IsChecked, teSyntaxNodeRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplaySyntaxTokenWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.SyntaxToken();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTokenUseRegEx.IsChecked, teSyntaxTokenRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplaySimpleAsClauseWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.SimpleAsClause();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSimpleAsClauseUseRegEx.IsChecked, teSimpleAsClauseRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplayObjectCreationExpressionWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.ObjectCreationExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceObjectCreationExpressionUseRegEx.IsChecked, teObjectCreationExpressionRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplaySyntaxTriviaWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.SyntaxTrivia();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceSyntaxTriviaUseRegEx.IsChecked, teSyntaxTriviaRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayBinaryExpressiontWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.BinaryExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceBinaryExpressionUseRegEx.IsChecked, teBinaryExpressionRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }
        StringBuilder DisplayAssignmentStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.AssignmentStatement();
            walker.MatchLeft = (bool)ceAssignmentStatementMatchLeft.IsChecked;
            walker.MatchRight = (bool)ceAssignmentStatementMatchRight.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceAssignmentStatementUseRegEx.IsChecked, teAssignmentStatementRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        StringBuilder DisplayLocalDeclarationStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.LocalDeclarationStatement();

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceLocalDeclarationStatementUseRegEx.IsChecked, teLocalDeclarationStatementRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayStructureBlockWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.StructureBlock();

            walker.ShowFields = (bool)ceStructureShowFields.IsChecked;

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            walker.AllFieldTypes = (bool)CodeExplorer.configurationOptions.ceAllTypes.IsChecked;
            walker.FieldNames = (bool)ceStructureFieldsUseRegEx.IsChecked ? teStructureFieldsRegEx.Text : ".*";
            walker.StructureNames = (bool)ceStructuresUseRegEx.IsChecked ? teStructureRegEx.Text : ".*";

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceStructuresUseRegEx.IsChecked, teStructureRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.VariableDeclarator();

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceVariableDeclaratorUseRegEx.IsChecked, teVariableDeclaratorRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayInvocationExpressionWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.InvocationExpression();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceInvocationExpressionUseRegEx.IsChecked, teInvocationExpressionRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayParameterListWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.ParameterList();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb, 
                (bool)ceParameterListUseRegEx.IsChecked, teParameterListRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayArgumentListWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.ArgumentList();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceArgumentListUseRegEx.IsChecked, teArgumentListRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayPropertyStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
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

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)cePropertyStatementUseRegEx.IsChecked, tePropertyStatementRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayFieldDeclarationWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNCSW.VB.FieldDeclaration();

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceFieldDeclarationUseRegEx.IsChecked, teFieldDeclarationRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayClassStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
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
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayImportsStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.ImportsStatement();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceImportsStatementUseRegEx.IsChecked, teImportsStatementRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayMethodStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
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
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayModuleStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
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
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches)
        {
            var walker = new VNCSW.VB.NamespaceStatement();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
                (bool)ceNamespaceStatementUseRegEx.IsChecked, teNamespaceStatementRegEx.Text,
                matches, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());
        }

        #endregion

        #region Utility Methods

        #endregion
    }
}
