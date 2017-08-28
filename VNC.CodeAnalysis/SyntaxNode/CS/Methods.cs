using System;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;


namespace VNC.CodeAnalysis.SyntaxNode.CS
{
    public class Methods
    {
        public static StringBuilder Display(StreamReader stream, Boolean includeTrivia, Boolean statementsOnly)
        {
            return Display(stream.ReadToEnd(), includeTrivia, statementsOnly);
        }

        public static StringBuilder Display(string sourceCode, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> syntaxNodes;

            //if (statementsOnly)
            //{
            //    syntaxNodes = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();
            //}
            //else
            //{
            //    syntaxNodes = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();
            //}

            //foreach (Microsoft.CodeAnalysis.SyntaxNode node in syntaxNodes)
            //{
            //    sb.AppendLine(node.ToFullString());
            //}

            return sb;
        }
    }
}
