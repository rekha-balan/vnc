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

        public static void ProcessOperationOld(VNCCA.Types.SearchTreeCommandOld command, 
            User_Controls.wucCodeExplorer codeExplorer, 
            User_Controls.wucCodeExplorerContext codeExplorerContext,
            User_Controls.wucConfigurationOptions configurationOptions)
        {
            StringBuilder sb = new StringBuilder();
            codeExplorer.teSourceCode.Clear();
            codeExplorer.teSourceCode.InvalidateVisual();
            //CodeExplorer.teSourceCode.Text = "";

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

                        sbFileResults = command(sbFileResults, tree, matches, crcMatchesToString, crcMatchesToFullString);

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

            if ((Boolean)configurationOptions.ceDisplaySummary.IsChecked)
            {
                // Add information from the matches dictionary
                sb.AppendLine("Summary");

                foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (matches[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));
                    }
                }

                sb.AppendLine("CRC ToString Summary");

                foreach (var item in crcMatchesToString.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (crcMatchesToString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToString[item], item));
                    }
                }

                sb.AppendLine("CRC ToFullString Summary");

                foreach (var item in crcMatchesToFullString.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (crcMatchesToFullString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToFullString[item], item));
                    }
                }
            }

            codeExplorer.teSourceCode.Text = sb.ToString();
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

            if ((Boolean)configurationOptions.ceDisplaySummary.IsChecked)
            {
                // Add information from the matches dictionary
                sb.AppendLine("Summary");

                foreach (var item in matches.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (matches[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", matches[item], item));
                    }
                }

                sb.AppendLine("CRC ToString Summary");

                foreach (var item in crcMatchesToString.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (crcMatchesToString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToString[item], item));
                    }
                }

                sb.AppendLine("CRC ToFullString Summary");

                foreach (var item in crcMatchesToFullString.OrderBy(v => v.Key).Select(k => k.Key))
                {
                    if (crcMatchesToFullString[item] >= configurationOptions.sbDisplaySummaryMinimum.Value)
                    {
                        sb.AppendLine(string.Format("Count: {0,3} {1} ", crcMatchesToFullString[item], item));
                    }
                }
            }

            codeExplorer.teSourceCode.Text = sb.ToString();
        }
    }
}
