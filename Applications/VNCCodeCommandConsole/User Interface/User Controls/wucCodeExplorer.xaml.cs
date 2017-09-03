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


namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class wucCodeExplorer : wucDXBase
    {
        public wucCodeExplorer()
        {
            try
            {
                InitializeComponent();
                UpdateChildUserControls();
            }
            catch (Exception ex)
            {
                
            }

            //int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //this.Width = (primaryScreenWidth * 9) / 10;
            //this.Height = (primaryScreenHeight * 9) / 10;
        }


        private void ShowAs_Checked(object sender, RoutedEventArgs e)
        {
            //if (groupContainer == null)
            //    return;

            //LayoutGroupView containerView, childView;

            //if (sender == checkShowAsGroupBoxes)
            //{
            //    containerView = LayoutGroupView.GroupBox;
            //    childView = LayoutGroupView.GroupBox;
            //}
            //else
            //{
            //    if (sender == checkShowAsTabs)
            //    {
            //        containerView = LayoutGroupView.Tabs;
            //        childView = LayoutGroupView.Group;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            //groupContainer.View = containerView;

            //foreach (FrameworkElement child in groupContainer.GetLogicalChildren(false))
            //{
            //    if (child is LayoutGroup)
            //    {
            //        ((LayoutGroup)child).View = childView;
            //    }
            //}
        }
        void UpdateChildUserControls()
        {
            // Tell child controls where to find controls they need
            // Output goes to controls on CodeExplorer
            // Input comes from controls on CodeExplorerContext

            wucCommands.CodeExplorer = this;
            wucCommands.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsDesign.CodeExplorer = this;
            wucCommandsDesign.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsFind.CodeExplorer = this;
            wucCommandsFind.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsParse.CodeExplorer = this;
            wucCommandsParse.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsPerformance.CodeExplorer = this;
            wucCommandsPerformance.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsQuality.CodeExplorer = this;
            wucCommandsQuality.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsRewrite.CodeExplorer = this;
            wucCommandsRewrite.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsWorkspace.CodeExplorer = this;
            wucCommandsWorkspace.CodeExplorerContext = wucCodeExplorerContext;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //// Things work if this line is present.  Testing to see if it works without 6/13/2012
            //// Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Instances;

            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            // Things work if this line is present.  Testing to see if it works without 6/13/2012
            // Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;

            //serversGridControl.GroupBy("Environment");
            //serversGridControl.Set

            // Update the views.  First ensure a row is selected.

            //tableView1.FocusedRowHandle = 1;

            //tableView1.BestFitColumns();
            //tableView2.BestFitColumns();
            //tableView3.BestFitColumns();
            ////tableView4.BestFitColumns();
            ////tableView5.BestFitColumns();
            ////tableView6.BestFitColumns();
            //tableView7.BestFitColumns();
            //tableView8.BestFitColumns();

            //serversGridControl.GroupBy("SecurityZone");
        }










        //private void btnAllNodes_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;

        //    sb = Commands.Explore.DisplayAllStructuredTriviaCS(wucCodeExplorerContext.teSourceFile.Text);

        //    teSyntaxTree.Text = sb.ToString();
        //}

        //private void btnAllMethods_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;
        //    Boolean includeTrivia = wucCommands.ceMethodsIncludeTrivia.IsChecked.Value;
        //    Boolean statementsOnly = wucCommands.ceMethodsStatementsOnly.IsChecked.Value;

        //    sb = Commands.Explore.DisplayMethodsVB(wucCodeExplorerContext.teSourceFile.Text, includeTrivia, statementsOnly);

        //    teMethods.Text = sb.ToString();
        //}

        //private void btnAllClasses_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;
        //    Boolean includeTrivia = wucCommands.ceClassesIncludeTrivia.IsChecked.Value;
        //    Boolean statementsOnly = wucCommands.ceClassesStatementsOnly.IsChecked.Value;

        //    sb = Commands.Explore.DisplayClassesVB(wucCodeExplorerContext.teSourceFile.Text, includeTrivia, statementsOnly);

        //    lg_Classes.Focus();
   
        //    teClasses.Text = sb.ToString();
        //}

        //private void btnAllModules_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;
        //    Boolean includeTrivia = wucCommands.ceModulesIncludeTrivia.IsChecked.Value;
        //    Boolean statementsOnly = wucCommands.ceModulesStatementsOnly.IsChecked.Value;

        //    sb = Commands.Explore.DisplayModulesVB(wucCodeExplorerContext.teSourceFile.Text, includeTrivia, statementsOnly);

        //    groupContainer.Focus();
        //    lg_Modules.Focus();
        //    teModules.Text = sb.ToString();
        //}

        //private void btnAllStructures_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;
        //    Boolean includeTrivia = wucCommands.ceStructuresIncludeTrivia.IsChecked.Value;
        //    Boolean statementsOnly = wucCommands.ceStructuresStatementsOnly.IsChecked.Value;

        //    sb = Commands.Explore.DisplayStructuresVB(wucCodeExplorerContext.teSourceFile.Text, includeTrivia, statementsOnly);


        //    //lg_Body.Focus();
        //    //lg_Body_dlm.Focus();
        //    //lg_Body_dlm_lg.Focus();
        //    //lg_Body_dlm_lg_lpRoot.Focus();

        //    groupContainer.Focus();
        //    lg_Structures.Focus();
        //    teStructures.Text = sb.ToString();
        //}

        //private void btnParseSourceVB_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb = null;

        //    // TODO(crhodes)
        //    // Need to also get our hands on SyntaxTree that got produced.
        //    // Either change return type
        //    // or add out parameter
        //    // or add another method that returns string

        //    switch (wucCommandsParse.lbeSyntaxWalkerDepth.EditValue.ToString())
        //    {
        //        case "Node":
        //            sb = Commands.Explore.ParseVBDepthNode(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "StructuredTrivia":
        //            sb = Commands.Explore.ParseVBStructuredTrivia(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "Token":
        //            sb = Commands.Explore.ParseVBDepthToken(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "Trivia":
        //            sb = Commands.Explore.ParseVBDepthTrivia(wucCommandsParse.teSourceCode1.Text);
        //            break;
        //    }

        //       teSyntaxTree.Text = sb.ToString();

        //    lg_SyntaxTree.Focus();
        //}

        //private void btnParseSourceCS_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb = null;

        //    // TODO(crhodes)
        //    // Need to also get our hands on SyntaxTree that got produced.
        //    // Either change return type
        //    // or add out parameter
        //    // or add another method that returns string

        //    switch (wucCommandsParse.lbeSyntaxWalkerDepth.EditValue.ToString())
        //    {
        //        case "Node":
        //            sb = Commands.Explore.ParseCSDepthNode(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "StructuredTrivia":
        //            sb = Commands.Explore.ParseCSStructuredTrivia(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "Token":
        //            sb = Commands.Explore.ParseCSDepthToken(wucCommandsParse.teSourceCode1.Text);
        //            break;

        //        case "Trivia":
        //            sb = Commands.Explore.ParseCSDepthTrivia(wucCommandsParse.teSourceCode1.Text);
        //            break;
        //    }

        //    teSyntaxTree.Text = sb.ToString();

        //    lg_SyntaxTree.Focus();
        //}

        //private void btnInfo_Document_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;

        //    sb = VNC.CodeAnalysis.Workspace.Document.Display(wucCodeExplorerContext.teSourceFile.Text);

        //    teWorkspace.Text = sb.ToString();

        //    lg_Workspace.Focus();
        //}

        //private void btnInfo_Project_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;

        //    sb = VNC.CodeAnalysis.Workspace.Project.Display(wucCodeExplorerContext.teProjectFile.Text);

        //    teWorkspace.Text = sb.ToString();

        //    lg_Workspace.Focus();
        //}

        //private void btnInfo_Solution_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb;

        //    sb = VNC.CodeAnalysis.Workspace.Solution.Display(wucCodeExplorerContext.teSolutionFile.Text);

        //    teWorkspace.Text = sb.ToString();

        //    lg_Workspace.Focus();
        //}

        //private void btnCodeToCommentRatioVB_Click(object sender, RoutedEventArgs e)
        //{
        //    //Boolean includeTrivia = ceStructuresIncludeTrivia.IsChecked.Value;
        //    //Boolean statementsOnly = ceStructuresStatementsOnly.IsChecked.Value;

        //    StringBuilder sb = new StringBuilder();

        //    var sourceCode = "";

        //    using (var sr = new StreamReader(wucCodeExplorerContext.teSourceFile.Text))
        //    {
        //        sourceCode = sr.ReadToEnd();
        //    }

        //    //sb = Commands.Explore.CodeToCommentRatioVB(wucCodeExplorerContext.teSourceFile.Text);
        //    sb = VNC.CodeAnalysis.QualityMetrics.VB.CodeToCommentRatio.Check(sourceCode);

        //    teSourceCode.Text = sb.ToString();
        //}

        //private void btnInvocation_Click(object sender, RoutedEventArgs e)
        //{
        //    //Boolean includeTrivia = ceStructuresIncludeTrivia.IsChecked.Value;
        //    //Boolean statementsOnly = ceStructuresStatementsOnly.IsChecked.Value;

        //    StringBuilder sb = new StringBuilder();

        //    var sourceCode = "";

        //    using (var sr = new StreamReader(wucCodeExplorerContext.teSourceFile.Text))
        //    {
        //        sourceCode = sr.ReadToEnd();
        //    }

        //    //sb = Commands.Explore.CodeToCommentRatioVB(wucCodeExplorerContext.teSourceFile.Text);
        //    sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, true, true);

        //    teSourceCode.Text = sb.ToString();
        //}
    }
}
