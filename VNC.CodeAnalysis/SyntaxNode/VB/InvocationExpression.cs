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

using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxNode.VB
{
    public class InvocationExpression
    {
        public static StringBuilder Display(StreamReader stream, Boolean includeTrivia, string identifier)
        {
            return Display(stream.ReadToEnd(), includeTrivia, identifier);
        }

        public static StringBuilder Display(string sourceCode, Boolean includeTrivia, string identifier)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            //IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> syntaxNodes;
            IEnumerable<InvocationExpressionSyntax> syntaxNodes;


            syntaxNodes = tree.GetRoot().DescendantNodes().OfType<InvocationExpressionSyntax>();

            //foreach (Microsoft.CodeAnalysis.SyntaxNode node in syntaxNodes)
            foreach (InvocationExpressionSyntax node in syntaxNodes.Where(e => e.Expression.ToString() == identifier))
            {
                sb.AppendLine(string.Format("FullString >{0}<", node.Expression.ToFullString()));
                sb.AppendLine(string.Format("ToString>{0}< Expression >{1}< ArgumentList >{2}<",
                    node.ToString(),
                    node.Expression.ToString(),
                    node.ArgumentList.ToString()));
            }

            return sb;
        }
    }
}
