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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCommandsRewrite : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Constructors

        public wucCommandsRewrite()
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

        private void btnReplace_ConvertToInt16_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            CodeExplorer.teWorkspace.Clear();

            var sourceCode = "";
            string identifier = teIdentifier.Text;
            string projectFullPath = CodeExplorerContext.teProjectFile.Text;
            if (projectFullPath != "")
            {
                var workSpace = MSBuildWorkspace.Create();
                var project = workSpace.OpenProjectAsync(projectFullPath).Result;

                foreach (var document in project.Documents)
                {
                    string filePath = document.FilePath;

                    if (filePath.Contains("designer"))
                    {
                        continue;
                    }

                    if (filePath.Contains("My Project"))
                    {
                        continue;
                    }

                    if (document.Name == "Assembly.vb")
                    {
                        continue;
                    }

                    if (document.Name.EndsWith(".vb"))
                    {
                        sb.AppendLine("ReWriting " + filePath);
                        RewriteFile(filePath);
                    }
                }
            }
            else
            {
                string filePath = CodeExplorerContext.teSourceFile.Text;
                sb.AppendLine("ReWriting" + filePath);
                RewriteFile(filePath);
            }

            CodeExplorer.teWorkspace.Text = sb.ToString();
        }

        private void RewriteFile(string filePath)
        {
            string sourceCode;

            using (var sr = new StreamReader(filePath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var syntaxNodes = tree.GetRoot().DescendantNodes();

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.VB.ReplaceConvertToInt16();

            SyntaxNode newNode = rewriter.Visit(tree.GetRoot());

            if (newNode != tree.GetRoot())
            {
                string newFilePath = filePath + "2";

                File.WriteAllText(newFilePath, newNode.ToFullString());
            }
        }
    }
}
