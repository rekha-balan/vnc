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
        public delegate StringBuilder SearchFileCommand(StringBuilder sb, string filePath);
        public delegate StringBuilder RewriteFileCommand(StringBuilder sb, string filePath, string targetPattern, string replacementPattern);


        public delegate StringBuilder SearchTreeCommand(StringBuilder sb, Dictionary<string, Int32> matches, SyntaxTree tree);
        public delegate StringBuilder RewriteTreeCommand(StringBuilder sb, SyntaxTree tree, string targetPattern, string replacementPattern);
    }
}