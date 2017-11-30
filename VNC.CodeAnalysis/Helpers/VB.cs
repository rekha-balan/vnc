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
        public static List<string> GetMethodNames(string sourceCode)
        {
            List<string> methodNames = new List<string>();

            SyntaxTree tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatementNames walker = null;
            walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.MethodStatementNames();
            walker.MethodNames = methodNames;
            walker.Visit(tree.GetRoot());

            return methodNames;
        }

        public static string GetContainingMethod(VisualBasicSyntaxNode node)
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

        public static string GetContainingContext(VisualBasicSyntaxNode node, DisplayInfo displayInfo)
        {
            string ancestorContext = "";

            if (displayInfo.InTryBlock)
            {
                var inTryBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.TryBlock))
                    .Cast<TryBlockSyntax>().ToList();

                if (inTryBlock.Count > 0)
                {
                    ancestorContext += "T ";
                }                            
            }

            if (displayInfo.InWhileBlock)
            {
                var inDoWhileBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.DoWhileLoopBlock))
                    .Cast<DoLoopBlockSyntax>().ToList();

                if (inDoWhileBlock.Count > 0)
                {
                    ancestorContext += "W ";
                }
            }

            if (displayInfo.InForBlock)
            {
                var inForBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ForBlock))
                    .Cast<ForBlockSyntax>().ToList();

                if (inForBlock.Count > 0)
                {
                    ancestorContext += "F ";
                }
            }

            if (displayInfo.InIfBlock)
            {
                var inMultiLineIfBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.MultiLineIfBlock))
                    .Cast<MultiLineIfBlockSyntax>().ToList();

                if (inMultiLineIfBlock.Count > 0)
                {
                    ancestorContext += "I ";
                }
            }

            if (ancestorContext.Length > 0)
            {
                ancestorContext = string.Format("{0,8}", ancestorContext);
            }

            string classModuleContext = "";

            if (displayInfo.ClassOrModuleName)
            {
                var inClassBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                    .Cast<ClassBlockSyntax>().ToList();

                var inModuleBlock = node.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ModuleBlock))
                    .Cast<ModuleBlockSyntax>().ToList();

                string typeName = "unknown";
                string className = "unknown";
                string moduleName = "unknown";

                if (inClassBlock.Count > 0)
                {
                    typeName = "Class";

                    className = node.Ancestors()
                        .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                        .Cast<ClassBlockSyntax>().First().ClassStatement.Identifier.ToString();
                }

                if (inModuleBlock.Count > 0)
                {
                    typeName = "Module";

                    moduleName = node.Ancestors()
                        .Where(x => x.IsKind(SyntaxKind.ModuleBlock))
                        .Cast<ModuleBlockSyntax>().First().ModuleStatement.Identifier.ToString();
                }

                typeName = String.Format("{0, 8}{1,6}:({2,-25})",
                    ancestorContext,
                    typeName,
                    typeName == "Class" ? className : moduleName);
            }

            string methodContext = "";

            if (displayInfo.MethodName)
            {
                methodContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
            }

            return ancestorContext + classModuleContext + methodContext;
        }

        public static StringBuilder InvokeVNCSyntaxWalker(
            StringBuilder sb,
            Boolean useRegEx, string regEx,
            Dictionary<string, Int32> matches, 
            SyntaxTree syntaxTree,
            SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker, 
            DisplayInfo displayInfo)
        {
            walker.Messages = sb;

            //walker.Display.ClassOrModuleName = displayInfo.ClassOrModuleName;
            //walker.Display.MethodName = displayInfo.MethodName;
            walker.IdentifierNames = useRegEx ? regEx : ".*";
            walker.Display = displayInfo;

            walker.InitializeRegEx();

            walker.Matches = matches;

            walker.Visit(syntaxTree.GetRoot());

            return sb;
        }

        //public static StringBuilder InvokeVNCTypedSyntaxWalker(
        //    StringBuilder sb,
        //    Boolean useRegEx, string regEx,
        //    Dictionary<string, Int32> matches, 
        //    SyntaxTree syntaxTree,
        //    SyntaxWalkers.VB.VNCVBTypedSyntaxWalkerBase walker, DisplayInfo displayInfo)
        //{
        //    //walker.AllTypes = displayInfo.AllTypes;

        //    //walker.IsByte = displayInfo.Byte;
        //    //walker.IsBoolean = displayInfo.Boolean;
        //    //walker.IsDate = displayInfo.Date;
        //    //walker.IsDataTable = displayInfo.DataTable;
        //    //walker.IsDateTime = displayInfo.DateTime;
        //    //walker.IsInt16 = displayInfo.Int16;
        //    //walker.IsInt32 = displayInfo.Int32;
        //    //walker.IsInteger = displayInfo.Integer;
        //    //walker.IsLong = displayInfo.Long;
        //    //walker.IsSingle = displayInfo.Single;
        //    //walker.IsString = displayInfo.String;

        //    //walker.IsOtherType = displayInfo.OtherTypes;

        //    return InvokeVNCSyntaxWalker(sb, useRegEx, regEx, matches, syntaxTree, walker, displayInfo);
        //}

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
