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

        private void LoadControlContents()
        {
            try
            {
                wucFind_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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

        private void btnInvocationWalker_Click(object sender, RoutedEventArgs e)
        {
            CodeExplorer.teSourceCode.Text = DisplayInvocationWalkerVB(CodeExplorerContext.teSourceFile.Text).ToString();
        }

        private void btnInvocation_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(CodeExplorerContext.teSourceFile.Text))
            {
                sourceCode = sr.ReadToEnd();
            }

            //string identifier = teIdentifier.Text;
            string identifier = User_Controls.wucFind_Picker.ValueProperty.ToString();

            Boolean includeTrivia = ceIncludeTrivia.IsChecked.Value;
            var additionalLocations = (VNC.CodeAnalysis.SyntaxNode.AdditionalNodes)lbeNodes.SelectedIndex;

            sb = VNC.CodeAnalysis.SyntaxNode.VB.InvocationExpression.Display(sourceCode, identifier, includeTrivia, additionalLocations);

            CodeExplorer.teSourceCode.Text = sb.ToString();
        }

        private void wucFindPicker_ControlChanged()
        {

        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            //CustomFormat.FormatStorageColumns(e);
        }

        #endregion

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        #region Main Function Routines



        #endregion

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
                    walker.StringBuilder = sb;
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
                        string fileNameAndPath = codeExplorerContext.teSourcePath.Text + wucCodeExplorerContext.GetFilePath(file);

                        if ( ! File.Exists(fileNameAndPath))
                        {
                            sb.AppendLine(string.Format("ERROR File ({0}) does not exist.  Check config file", fileNameAndPath));
                            continue;
                        }

                        if ((Boolean)ceListImpactedFilesOnly.IsChecked)
                        {
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
                            walker.StringBuilder = sb;
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

        internal StringBuilder DisplayInvocationWalkerVB(string fileNameAndPath)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            //string pattern = teIdentifier.Text;
            string pattern = User_Controls.wucFind_Picker.ValueProperty.ToString();

            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.InvocationExpression(pattern);
            walker.StringBuilder = sb;
            walker.Visit(tree.GetRoot());

            return sb;
        }
    }
}
