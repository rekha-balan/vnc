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
        public static bool SaveFileChanges(string filePath, SyntaxTree tree, Microsoft.CodeAnalysis.SyntaxNode newNode, string fileSuffix)
        {
            Boolean performedReplacement = false;

            if (newNode != tree.GetRoot())
            {
                string newFilePath = filePath + fileSuffix;

                File.WriteAllText(newFilePath, newNode.ToFullString());
                performedReplacement = true;
            }

            return performedReplacement;
        }
    }
}
