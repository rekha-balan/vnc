using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class HttpContextCurrentInvocationExpression : VisualBasicSyntaxWalker
    {
        public StringBuilder StringBuilder;
        public Dictionary<string, Int32> Matches = new Dictionary<string, Int32>();

        private string _pattern;

        public HttpContextCurrentInvocationExpression(string pattern) : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            _pattern = "HttpContext.Current." + pattern;
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            string expression = node.Expression.ToString();

            if (expression == _pattern)
            {
                string nodeValue = node.ToString();

                if (Matches.ContainsKey(nodeValue))
                {
                    Matches[nodeValue] += 1;
                }
                else
                {
                    Matches.Add(nodeValue, 1);
                }

                var a1 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ClassStatement))
                    .Cast<ClassStatementSyntax>().ToList();

                var a2 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionStatement) || x.IsKind(SyntaxKind.SubStatement))
                    .Cast<MethodStatementSyntax>().ToList();

                var a3 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                    .Cast<ClassBlockSyntax>().ToList();

                var a4 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                    .Cast<MethodBlockSyntax>().ToList();

                var className = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                    .Cast<ClassBlockSyntax>().First().ClassStatement.Identifier.ToString();

                var methodName = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                    .Cast<MethodBlockSyntax>().First().SubOrFunctionStatement.Identifier.ToString();

                //string className = node.Ancestors()
                //    .Where(x => x.IsKind(SyntaxKind.ClassStatement))
                //    .Cast<ClassStatementSyntax>().First().ToString();

                //string methodName = node.Ancestors()
                //    .Where(x => x.IsKind(SyntaxKind.FunctionStatement) || x.IsKind(SyntaxKind.SubStatement))
                //    .Cast<MethodStatementSyntax>().First().ToString();

                StringBuilder.AppendLine(String.Format("Class:({0,-25})  Method:({1,-35}) {2}", className, methodName, nodeValue));
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }
    }
}
