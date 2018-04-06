using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace VNC.CodeAnalysis.SyntaxRewriters
{
    public class Helpers
    {
        public static bool SaveFileChanges(RewriteFileCommandConfiguration commandConfiguration, Microsoft.CodeAnalysis.SyntaxNode newNode)
        {
            Boolean performedReplacement = false;

            if (newNode != commandConfiguration.SyntaxTree.GetRoot())
            {

                string fileSuffix = commandConfiguration.ConfigurationOptions.AddFileSuffix ? commandConfiguration.ConfigurationOptions.FileSuffix : "";

                string newFilePath = commandConfiguration.FilePath + fileSuffix;

                File.WriteAllText(newFilePath, newNode.ToFullString());

                performedReplacement = true;
            }

            return performedReplacement;
        }
    }
}
