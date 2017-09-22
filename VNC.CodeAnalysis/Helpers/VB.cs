using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Text.RegularExpressions;

namespace VNC.CodeAnalysis.Helpers
{
    public static class VB
    {
        public static string GetContainingMethod(DeclarationStatementSyntax node)
        {
            string methodName = "none";

            var a5 = node.Ancestors()
                .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                .Cast<MethodBlockSyntax>().ToList();

            if (a5.Count > 0)
            {
                methodName = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.FunctionBlock) || x.IsKind(SyntaxKind.SubBlock))
                    .Cast<MethodBlockSyntax>().First().SubOrFunctionStatement.Identifier.ToString();
            }

            return methodName;
        }

        public static string GetContainingType(DeclarationStatementSyntax node)
        {
            string typeName = "unknown";

            var a3 = node.Ancestors()
                .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                .Cast<ClassBlockSyntax>().ToList();

            var a3a = node.Ancestors()
                .Where(x => x.IsKind(SyntaxKind.ModuleBlock))
                .Cast<ModuleBlockSyntax>().ToList();

            string className = "unknown";
            string moduleName = "unknown";

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

            typeName = String.Format("{0,6}:({1,-25})",
                typeName,
                typeName == "Class" ? className : moduleName);

            return typeName;
        }

        public static Boolean IsOnLineByItself(InvocationExpressionSyntax node)
        {
            Boolean result = false;

            // Seems like Trivia is Trivia  No notion of with our without :)

            string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
            //string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

            string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
            //string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

            if (String.IsNullOrWhiteSpace(existingLeadingTrivia) && String.IsNullOrWhiteSpace(existingTrailingTrivia))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Takes a (multi-line) comment and returns a leading whitespace aware comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="leadingWhiteSpace"></param>
        /// <returns></returns>
        public static string MultiLineComment(string comment, string leadingWhiteSpace)
        {
            StringBuilder result = new StringBuilder();

            var lines = Regex.Split(comment, "\r\n|\r|\n");

            foreach (string line in lines)
            {
                result.AppendFormat(string.Format("'{0}\n{1}", line, leadingWhiteSpace));
            }

            return result.ToString();
        }
    }
}
