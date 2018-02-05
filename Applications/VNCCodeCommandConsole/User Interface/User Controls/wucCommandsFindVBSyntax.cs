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
using VNC.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

using VNCCA = VNC.CodeAnalysis;
using VNCSW = VNC.CodeAnalysis.SyntaxWalkers;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsFindVBSyntax : wucDXBase
    {
        #region Enums, Fields, Properties
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #endregion

        #region Constructors

        public wucCommandsFindVBSyntax()
        {
            //#if TRACE
            //            long startTicks = VNC.AppLog.Trace15("Start", LOG_APPNAME);
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
            //            VNC.AppLog.Trace15("End", LOG_APPNAME, startTicks);
            //#endif
        }

        #endregion

        #region Internal Methods

        #endregion

        #region Initialization and Load

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
            //#if TRACE
            //            long startTicks = VNC.AppLog.Trace15("Start", LOG_APPNAME);
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
            //            VNC.AppLog.Trace15("End", LOG_APPNAME, startTicks);
            //#endif
        }

        #endregion

        #region Event Handlers
        private void btnExpressionStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayExpressionStatementVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnAsNewClauseWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayAsNewClauseVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnFindStructures_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayStructureBlockWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnFieldDeclarationWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayFieldDeclarationWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnClassStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayClassStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnBinaryExpresionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayBinaryExpressiontWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnAssignmentStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayAssignmentStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnArgumentListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayArgumentListWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnImportsStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayImportsStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnInvocationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayInvocationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnLocalDeclarationStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayLocalDeclarationStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnMemberAccessExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayMemberAccessExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnMethodStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayMethodStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnModuleStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayModuleStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnNamespaceStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayNamespaceStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnObjectCreationExpressionWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayObjectCreationExpressionWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnParameterListWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayParameterListWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnPropertyStatementWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayPropertyStatementWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSimpleAsClauseWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySimpleAsClauseWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxNodeWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxNodeWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxTokenWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTokenWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnSyntaxTriviaWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplaySyntaxTriviaWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        private void btnVariableDeclaratorWalker_Click(object sender, RoutedEventArgs e)
        {
            Helper.ProcessOperation(DisplayVariableDeclaratorWalkerVB, CodeExplorer, CodeExplorerContext, CodeExplorer.configurationOptions);
        }

        #endregion

        #region Private Methods


        

        #endregion


        #region Main Function Routines
        StringBuilder DisplayExpressionStatementVB(SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.ExpressionStatement();

            commandConfiguration.UseRegEx = (bool)ceExpressionStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teExpressionStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayMemberAccessExpressionWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.MemberAccessExpression();

            // TODO(crhodes)
            // Leaving this comment in to show the progression from too many arguments to commandConfiguration class.
            // Adding another argument was a pain.  Had to change too many places.  Adding another thing is now easy.

            //return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
            //    (bool)ceMemberAccessExpressionUseRegEx.IsChecked, teMemberAccessExpressionRegEx.Text,
            //    matches, crcMatchesToString, crcMatchesToFullString, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());

            commandConfiguration.UseRegEx = (bool)ceMemberAccessExpressionUseRegEx.IsChecked;
            commandConfiguration.RegEx = teMemberAccessExpressionRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplaySyntaxNodeWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.SyntaxNode();

            commandConfiguration.UseRegEx = (bool)ceSyntaxNodeUseRegEx.IsChecked;
            commandConfiguration.RegEx = teSyntaxNodeRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplaySyntaxTokenWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.SyntaxToken();

            commandConfiguration.UseRegEx = (bool)ceSyntaxTokenUseRegEx.IsChecked;
            commandConfiguration.RegEx = teSyntaxTokenRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplayAsNewClauseVB(SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.AsNewClause();

            commandConfiguration.UseRegEx = (bool)ceAsNewClauseUseRegEx.IsChecked;
            commandConfiguration.RegEx = teAsNewClauseRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplaySimpleAsClauseWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.SimpleAsClause();

            commandConfiguration.UseRegEx = (bool)ceSimpleAsClauseUseRegEx.IsChecked;
            commandConfiguration.RegEx = teSimpleAsClauseRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplayObjectCreationExpressionWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.ObjectCreationExpression();

            commandConfiguration.UseRegEx = (bool)ceObjectCreationExpressionUseRegEx.IsChecked;
            commandConfiguration.RegEx = teObjectCreationExpressionRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplaySyntaxTriviaWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.SyntaxTrivia();

            commandConfiguration.UseRegEx = (bool)ceSyntaxTriviaUseRegEx.IsChecked;
            commandConfiguration.RegEx = teSyntaxTriviaRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayBinaryExpressiontWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.BinaryExpression();

            commandConfiguration.UseRegEx = (bool)ceBinaryExpressionUseRegEx.IsChecked;
            commandConfiguration.RegEx = teBinaryExpressionRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }
        StringBuilder DisplayAssignmentStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.AssignmentStatement();
            walker.MatchLeft = (bool)ceAssignmentStatementMatchLeft.IsChecked;
            walker.MatchRight = (bool)ceAssignmentStatementMatchRight.IsChecked;

            commandConfiguration.UseRegEx = (bool)ceAssignmentStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teAssignmentStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        StringBuilder DisplayLocalDeclarationStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.LocalDeclarationStatement();

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            commandConfiguration.UseRegEx = (bool)ceLocalDeclarationStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teLocalDeclarationStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayStructureBlockWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.StructureBlock();

            walker.ShowFields = (bool)ceStructureShowFields.IsChecked;

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            walker.AllFieldTypes = (bool)CodeExplorer.configurationOptions.ceAllTypes.IsChecked;
            walker.FieldNames = (bool)ceStructureFieldsUseRegEx.IsChecked ? teStructureFieldsRegEx.Text : ".*";
            walker.StructureNames = (bool)ceStructuresUseRegEx.IsChecked ? teStructureRegEx.Text : ".*";

            commandConfiguration.UseRegEx = (bool)ceStructuresUseRegEx.IsChecked;
            commandConfiguration.RegEx = teStructureRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayVariableDeclaratorWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.VariableDeclarator();

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            commandConfiguration.UseRegEx = (bool)ceVariableDeclaratorUseRegEx.IsChecked;
            commandConfiguration.RegEx = teVariableDeclaratorRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayInvocationExpressionWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.InvocationExpression();

            commandConfiguration.UseRegEx = (bool)ceInvocationExpressionUseRegEx.IsChecked;
            commandConfiguration.RegEx = teInvocationExpressionRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayParameterListWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.ParameterList();

            commandConfiguration.UseRegEx = (bool)ceParameterListUseRegEx.IsChecked;
            commandConfiguration.RegEx = teParameterListRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayArgumentListWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.ArgumentList();

            commandConfiguration.UseRegEx = (bool)ceArgumentListUseRegEx.IsChecked;
            commandConfiguration.RegEx = teArgumentListRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayPropertyStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
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

            commandConfiguration.UseRegEx = (bool)cePropertyStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = tePropertyStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayFieldDeclarationWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            VNCCA.SyntaxNode.FieldDeclarationLocation fieldDeclarationLocation = VNCCA.SyntaxNode.FieldDeclarationLocation.Class;

            // TODO(crhodes)
            // Go look at EyeOnLife and see how to do this in a cleaner way.

            switch (lbeFieldDeclarationLocation.EditValue.ToString())
            {
                case "Class":
                    fieldDeclarationLocation = VNCCA.SyntaxNode.FieldDeclarationLocation.Class;
                    break;

                case "Module":
                    fieldDeclarationLocation = VNCCA.SyntaxNode.FieldDeclarationLocation.Module;
                    break;

                case "Structure":
                    fieldDeclarationLocation = VNCCA.SyntaxNode.FieldDeclarationLocation.Structure;
                    break;
            }
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            walker = new VNCSW.VB.FieldDeclaration(fieldDeclarationLocation);

            walker.HasAttributes = (bool)CodeExplorer.configurationOptions.ceHasAttributes.IsChecked;

            commandConfiguration.UseRegEx = (bool)ceFieldDeclarationUseRegEx.IsChecked;
            commandConfiguration.RegEx = teFieldDeclarationRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayClassStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
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

            commandConfiguration.UseRegEx = (bool)ceClassStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teClassStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayImportsStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.ImportsStatement();

            //return VNCCA.Helpers.VB.InvokeVNCSyntaxWalkerOld(commandConfiguration.Results,
            //    (bool)ceImportsStatementUseRegEx.IsChecked, teImportsStatementRegEx.Text,
            //    commandConfiguration.Matches, 
            //    commandConfiguration.CRCMatchesToString, 
            //    commandConfiguration.CRCMatchesToFullString,
            //    commandConfiguration.SyntaxTree, 
            //    walker, 
            //    CodeExplorer.configurationOptions.GetConfigurationInfo());

            commandConfiguration.UseRegEx = (bool)ceImportsStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teImportsStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);
        }

        private StringBuilder DisplayMethodStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            VNCSW.VB.VNCVBTypedSyntaxWalkerBase walker = null;

            commandConfiguration.UseRegEx = (bool)ceMethodStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teMethodStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            if ((bool)ceShowMethodBlock.IsChecked)
            {
                walker = new VNCSW.VB.MethodBlock();
                commandConfiguration.ConfigurationOptions.ShowBlockCRC = true;
            }
            else
            {
                walker = new VNCSW.VB.MethodStatement();
            }

            StringBuilder results = VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker, commandConfiguration);

            // We may have done a deep dive on a method.  Go grab the results.
            // TODO(crhodes)
            // This might only be if in MethodBlock mode.  See above.

            CodeExplorer.teSyntaxNode.Text += walker.WalkerNode.ToString();
            CodeExplorer.teSyntaxToken.Text += walker.WalkerToken.ToString();
            CodeExplorer.teSyntaxTrivia.Text += walker.WalkerTrivia.ToString();
            CodeExplorer.teSyntaxStructuredTrivia.Text += walker.WalkerStructuredTrivia.ToString();

            return results;
        }

        private StringBuilder DisplayModuleStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
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


            commandConfiguration.UseRegEx = (bool)ceModuleStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teModuleStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker,
                commandConfiguration);

        }

        private StringBuilder DisplayNamespaceStatementWalkerVB(VNCCA.SearchTreeCommandConfiguration commandConfiguration)
        {
            var walker = new VNCSW.VB.NamespaceStatement();

            //return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(sb,
            //    (bool)ceNamespaceStatementUseRegEx.IsChecked, teNamespaceStatementRegEx.Text,
            //    matches, crcMatchesToString, crcMatchesToFullString, tree, walker, CodeExplorer.configurationOptions.GetConfigurationInfo());

            commandConfiguration.UseRegEx = (bool)ceNamespaceStatementUseRegEx.IsChecked;
            commandConfiguration.RegEx = teNamespaceStatementRegEx.Text;
            commandConfiguration.ConfigurationOptions = CodeExplorer.configurationOptions.GetConfigurationInfo();

            return VNCCA.Helpers.VB.InvokeVNCSyntaxWalker(walker,
                commandConfiguration);
        }

        #endregion

        #region Utility Methods

        #endregion
    }
}
