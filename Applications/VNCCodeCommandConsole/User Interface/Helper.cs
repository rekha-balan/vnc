using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using VNCCA = VNC.CodeAnalysis;

namespace VNCCodeCommandConsole.User_Interface
{
    public class Helper
    {
        public static void DisplayAdminUserControl(string title, string userControlName)
        {
            try
            {
                var win1 = new VNCCodeCommandConsole.User_Interface.Windows.UserControlHost(title, userControlName);
                win1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        public static void ValidateDataFullyLoaded()
        {
            while (!Common.DataFullyLoaded)
            {
                MessageBox.Show("Data not fully loaded yet");
                Thread.Sleep(2000);
            }
        }

        public static void ProcessOperation(VNCCA.Types.SearchTreeCommand searchTreeCommand,
            User_Controls.wucCodeExplorer codeExplorer,
            User_Controls.wucCodeExplorerContext codeExplorerContext,
            User_Controls.wucConfigurationOptions configurationOptions)
        {
            StringBuilder sb = new StringBuilder();
            codeExplorer.teSourceCode.Clear();
            codeExplorer.teSourceCode.InvalidateVisual();
            //CodeExplorer.teSourceCode.Text = "";

            codeExplorer.teSyntaxNode.Clear();
            codeExplorer.teSyntaxToken.Clear();
            codeExplorer.teSyntaxTrivia.Clear();
            codeExplorer.teSyntaxStructuredTrivia.Clear();

            codeExplorer.teSummary.Clear();
            codeExplorer.teSummaryCRCToString.Clear();
            codeExplorer.teSummaryCRCToFullString.Clear();

            string projectFullPath = codeExplorerContext.teProjectFile.Text;

            var filesToProcess = codeExplorerContext.GetFilesToProcess();

            Dictionary<string, Int32> matches = new Dictionary<string, int>();
            Dictionary<string, Int32> crcMatchesToString = new Dictionary<string, int>();
            Dictionary<string, Int32> crcMatchesToFullString = new Dictionary<string, int>();

            if (filesToProcess.Count > 0)
            {
                if ((Boolean)configurationOptions.ceListImpactedFilesOnly.IsChecked)
                {
                    sb.AppendLine("Would Search these files ....");
                }

                foreach (string filePath in filesToProcess)
                {
                    if ((Boolean)configurationOptions.ceListImpactedFilesOnly.IsChecked)
                    {
                        sb.AppendLine(string.Format("  {0}", filePath));
                    }
                    else
                    {
                        StringBuilder sbFileResults = new StringBuilder();

                        var sourceCode = "";

                        using (var sr = new System.IO.StreamReader(filePath))
                        {
                            sourceCode = sr.ReadToEnd();
                        }

                        // 
                        // This is where the action happens
                        //

                        SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

                        VNCCA.SearchTreeCommandConfiguration searchTreeCommandConfiguration = new VNCCA.SearchTreeCommandConfiguration();

                        searchTreeCommandConfiguration.Results = sbFileResults;
                        searchTreeCommandConfiguration.SyntaxTree = tree;
                        searchTreeCommandConfiguration.Matches = matches;
                        searchTreeCommandConfiguration.CRCMatchesToString = crcMatchesToString;
                        searchTreeCommandConfiguration.CRCMatchesToFullString = crcMatchesToFullString;

                        sbFileResults = searchTreeCommand(searchTreeCommandConfiguration);

                        if ((bool)configurationOptions.ceAlwaysDisplayFileName.IsChecked || (sbFileResults.Length > 0))
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

            if (!(Boolean)configurationOptions.ceDisplayResults.IsChecked)
            {
                // If only want to see the summary ...
                sb.Clear();
            }

            codeExplorer.teSourceCode.Text = sb.ToString();

            if ((Boolean)configurationOptions.ceDisplaySummary.IsChecked)
            {
                StringBuilder summary = new StringBuilder();

                // Add information from the matches dictionary
                summary.AppendLine("\n*** Summary ***\n");

                foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (matches[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        summary.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));
                    }
                }

                codeExplorer.teSummary.Text = summary.ToString();

                if ((Boolean)configurationOptions.ceDisplayCRC32.IsChecked)
                {
                    summary.Clear();

                    summary.AppendLine("\n*** CRC ToString Summary *** \n");

                    foreach (var item in crcMatchesToString.OrderBy(v => v.Key).Select(k => k.Key))
                    {
                        if (crcMatchesToString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                        {
                            summary.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToString[item], item));
                        }
                    }

                    codeExplorer.teSummaryCRCToString.Text = summary.ToString();

                    summary.Clear();

                    summary.AppendLine("\n*** CRC ToFullString Summary ***\n");

                    foreach (var item in crcMatchesToFullString.OrderBy(v => v.Key).Select(k => k.Key))
                    {
                        if (crcMatchesToFullString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                        {
                            summary.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToFullString[item], item));
                        }
                    }

                    codeExplorer.teSummaryCRCToString.Text = summary.ToString();
                }

            }


        }
    }
}
