using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace VNC.CodeAnalysis
{
    public class Types
    {
        // TODO(crhodes)
        // These are the old painful ways with lots of arguments

        //public delegate StringBuilder SearchFileCommandOld(StringBuilder sb, string filePath, Dictionary<string, Int32> matches, Dictionary<string, Int32> crcMatchesToString, Dictionary<string, Int32> crcMatchesToFullString);
        //public delegate StringBuilder SearchTreeCommandOld(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches, Dictionary<string, Int32> crcMatchesToString, Dictionary<string, Int32> crcMatchesToFullString);
        //public delegate StringBuilder RewriteFileCommandOld(StringBuilder sb, SyntaxTree tree, string filePath, string targetPattern, string replacementPattern, Dictionary<string, Int32> replacements, out Boolean performedReplacement);
        //public delegate StringBuilder RewriteTreeCommandOld(StringBuilder sb, SyntaxTree tree, string filePath, string targetPattern, string replacementPattern, Dictionary<string, Int32> replacements, out Boolean performedReplacement);

        public delegate StringBuilder SearchFileCommand(SearchFileCommandConfiguration commandConfiguration);
        public delegate StringBuilder SearchTreeCommand(SearchTreeCommandConfiguration commandConfiguration);

        public delegate StringBuilder RewriteFileCommand(RewriteFileCommandConfiguration commandConfiguration, out Boolean performedReplacement);
        public delegate StringBuilder RewriteTreeCommand(RewriteTreeCommandConfiguration commandConfiguration, out Boolean performedReplacement);
    }
}