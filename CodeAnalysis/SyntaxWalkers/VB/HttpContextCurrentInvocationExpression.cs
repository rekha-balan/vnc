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
        public StringBuilder Messages;
        public Boolean DisplayClassOrModuleName;
        public Boolean DisplayMethodName;

        ConfigurationOptions Display = new ConfigurationOptions();

        public Dictionary<string, Int32> Matches; // = new Dictionary<string, Int32>();

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

                var a3a = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ModuleBlock))
                    .Cast<ModuleBlockSyntax>().ToList();

                var a4 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                    .Cast<MethodBlockSyntax>().ToList();

                var a5 = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                    .Cast<MethodBlockSyntax>().ToList();

                string className = "unknown";
                string moduleName = "unknown";
                string typeName = "unknown";
                string methodName = "none";

                if (a3.Count > 0)
                {
                    typeName = "Class";

                    className = node.Ancestors()
                        .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                        .Cast<ClassBlockSyntax>().First().ClassStatement.Identifier.ToString();
                }

                if (a3a.Count > 0)
                {
                    typeName = "Module";

                    moduleName = node.Ancestors()
                        .Where(x => x.IsKind(SyntaxKind.ModuleBlock))
                        .Cast<ModuleBlockSyntax>().First().ModuleStatement.Identifier.ToString();
                }

                if (a5.Count > 0)
                {
                    methodName = node.Ancestors()
                        .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                        .Cast<MethodBlockSyntax>().First().SubOrFunctionStatement.Identifier.ToString();
                }

                //string className = node.Ancestors()
                //    .Where(x => x.IsKind(SyntaxKind.ClassStatement))
                //    .Cast<ClassStatementSyntax>().First().ToString();

                //string methodName = node.Ancestors()
                //    .Where(x => x.IsKind(SyntaxKind.FunctionStatement) || x.IsKind(SyntaxKind.SubStatement))
                //    .Cast<MethodStatementSyntax>().First().ToString();

                //StringBuilder.AppendLine(String.Format("{0,6}:({1,-25})  Method:({2,-35}) {3}", 
                //    typeName,
                //    typeName == "Class" ? className : moduleName,
                //    Helpers.VB.GetContainingMethod(node), nodeValue));

                string messageContext = "";

                if (DisplayClassOrModuleName)
                {
                    // HACK(crhodes)
                    // Figure out how to get Helpers to work

                    messageContext = Helpers.VB.GetContainingContext(node, Display);
                }

                if (DisplayMethodName)
                {
                    // HACK(crhodes)
                    // Figure out how to get Helpers to work

                    messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethodName(node));
                }

                Messages.AppendLine(String.Format("{0} {1}",
                    messageContext, 
                    nodeValue));
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }
    }
}
