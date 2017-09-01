using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
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
