using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC.CodeAnalysis
{
    public class Types
    {
        public delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath, Dictionary<string, Int32> matches);
        public delegate StringBuilder SearchTreeCommand(StringBuilder sb, SyntaxTree tree, Dictionary<string, Int32> matches);

        public delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern, Dictionary<string, Int32> replacements, out Boolean performedReplacement);
        public delegate StringBuilder RewriteTreeCommand(StringBuilder sb, SyntaxTree tree, string targetPattern, string replacementPattern, Dictionary<string, Int32> replacements, out Boolean performedReplacement);
    }
}