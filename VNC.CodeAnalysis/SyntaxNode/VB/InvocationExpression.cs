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
        public static StringBuilder Display(StreamReader stream, string identifier, Boolean includeTrivia)
        {
            return Display(stream.ReadToEnd(), identifier, includeTrivia);
        }

        public static StringBuilder Display(string sourceCode, string identifier, Boolean includeTrivia)
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

        public static StringBuilder Display(string sourceCode, string identifier, Boolean includeTrivia, AdditionalNodes additionalNodes)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            //IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> syntaxNodes;
            IEnumerable<InvocationExpressionSyntax> syntaxNodes;

            syntaxNodes = tree.GetRoot().DescendantNodes().OfType<InvocationExpressionSyntax>();

            foreach (InvocationExpressionSyntax node in syntaxNodes.Where(e => e.Expression.ToString() == identifier))
            {
                
                sb.AppendLine(string.Format("FullString: >{0}<", node.Expression.ToFullString()));
                sb.AppendLine(string.Format("ToString: >{0}<\nExpression: >{1}<\nArgumentList: >{2}<",
                    node.ToString(),
                    node.Expression.ToString(),
                    node.ArgumentList.ToString()));

                IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> extraSyntaxNodes;
                IEnumerable<Microsoft.CodeAnalysis.SyntaxToken> extraSyntaxTokens;
                IEnumerable<Microsoft.CodeAnalysis.SyntaxTrivia> extraSyntaxTrivias;
                
                IEnumerable<Microsoft.CodeAnalysis.SyntaxNodeOrToken> extraSyntaxNodeOrTokens;
                ChildSyntaxList extraChildSyntaxList;

                switch (additionalNodes)
                {
                    case AdditionalNodes.Ancestors:
                        sb.AppendLine("Ancestors");
                        // There is an ascend out of trivia parameter
                        extraSyntaxNodes = node.Ancestors();
                        DisplayExtraInfo(extraSyntaxNodes, sb);
                        break;

                    case AdditionalNodes.AncestorsAndSelf:
                        sb.AppendLine("AncestorsAndSelf");
                        extraSyntaxNodes = node.AncestorsAndSelf();
                        DisplayExtraInfo(extraSyntaxNodes, sb);
                        break;

                    case AdditionalNodes.ChildNodes:
                        sb.AppendLine("ChildNodes");
                        extraSyntaxNodes = node.ChildNodes();
                        DisplayExtraInfo(extraSyntaxNodes, sb);
                        break;

                    case AdditionalNodes.ChildNodesAndTokens:
                        sb.AppendLine("ChildNodesAndTokens");
                        extraChildSyntaxList = node.ChildNodesAndTokens();
                        break;

                    case AdditionalNodes.ChildTokens:
                        sb.AppendLine("ChildTokens");
                        extraSyntaxTokens = node.ChildTokens();
                        DisplayExtraInfo(extraSyntaxTokens, sb);
                        break;

                    case AdditionalNodes.DescendantNodes:
                        sb.AppendLine("DescendantNodes");
                        extraSyntaxNodes = node.DescendantNodes();
                        DisplayExtraInfo(extraSyntaxNodes, sb);
                        break;

                    case AdditionalNodes.DescendantNodesAndSelf:
                        sb.AppendLine("DescendantNodesAndSelf");
                        extraSyntaxNodes = node.DescendantNodesAndSelf();
                        DisplayExtraInfo(extraSyntaxNodes, sb);
                        break;

                    case AdditionalNodes.DescendantNodesAndTokens:
                        sb.AppendLine("DescendantNodesAndTokens");
                        extraSyntaxNodeOrTokens = node.DescendantNodesAndTokensAndSelf();
                        DisplayExtraInfo(extraSyntaxNodeOrTokens, sb);
                        break;

                    case AdditionalNodes.DescendantNodesAndTokensAndSelf:
                        sb.AppendLine("DescendantNodesAndTokensAndSelf");
                        extraSyntaxNodeOrTokens = node.DescendantNodesAndTokensAndSelf();
                        DisplayExtraInfo(extraSyntaxNodeOrTokens, sb);
                        break;

                    case AdditionalNodes.DescendantTokens:
                        sb.AppendLine("DescendantTokens");
                        extraSyntaxTokens = node.DescendantTokens();
                        DisplayExtraInfo(extraSyntaxTokens, sb);
                        break;

                    case AdditionalNodes.DescendantTrivia:
                        sb.AppendLine("DescendantTrivia");
                        extraSyntaxTrivias = node.DescendantTrivia();
                        break;
                }
            }

            return sb;
        }
        static void DisplayExtraInfo(IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> extraSyntaxNodes, StringBuilder sb)
        {
            foreach (Microsoft.CodeAnalysis.SyntaxNode node in extraSyntaxNodes)
            {
                sb.AppendLine(string.Format("{0}  Kind : {1}", node.ToString(), node.Kind().ToString()));
            }
        }

        static void DisplayExtraInfo(IEnumerable<Microsoft.CodeAnalysis.SyntaxToken> extraSyntaxNodes, StringBuilder sb)
        {
            foreach (Microsoft.CodeAnalysis.SyntaxToken node in extraSyntaxNodes)
            {
                sb.AppendLine(string.Format("{0}  Kind : {1}", node.ToString(), node.Kind().ToString()));
            }
        }

        static void DisplayExtraInfo(IEnumerable<Microsoft.CodeAnalysis.SyntaxNodeOrToken> extraSyntaxNodes, StringBuilder sb)
        {
            foreach (Microsoft.CodeAnalysis.SyntaxToken node in extraSyntaxNodes)
            {
                sb.AppendLine(string.Format("{0}  Kind : {1}", node.ToString(), node.Kind().ToString()));
            }
        }
    }
}
