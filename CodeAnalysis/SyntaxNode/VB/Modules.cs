
using Microsoft.CodeAnalysis;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;
using System.Collections.Generic;

using System.IO;

using System.Linq;
using System.Text;

namespace VNC.CodeAnalysis.SyntaxNode.VB
{
    public class Modules
    {
        public static StringBuilder Display(StreamReader stream, Boolean includeTrivia, Boolean statementsOnly)
        {
            return Display(stream.ReadToEnd(), includeTrivia, statementsOnly);
        }

        public static StringBuilder Display(string sourceCode, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> syntaxNodes;

            if (statementsOnly)
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<ModuleStatementSyntax>();
            }
            else
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<ModuleBlockSyntax>();
            }

            foreach (Microsoft.CodeAnalysis.SyntaxNode node in syntaxNodes)
            {
                sb.AppendLine(includeTrivia ? node.ToFullString() : node.ToString());
            }

            return sb;
        }
    }
}
