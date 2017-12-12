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

        public static string GetContainingContext(VisualBasicSyntaxNode node, ConfigurationOptions displayInfo)
        {
            string ancestorContext = GetAncestorContext(node, displayInfo);

            string classModuleContext = GetClassModuleContext(node, displayInfo);

            string methodContext = GetMethodContext(node, displayInfo);

            string sourceContext = GetSourceContext(node, displayInfo);

            return ancestorContext + classModuleContext + methodContext + sourceContext;
        }

        private static string GetSourceContext(VisualBasicSyntaxNode node, ConfigurationOptions displayInfo)
        {
            string sourceContext = "";

            if (displayInfo.SourceLocation)
            {
                var location = node.GetLocation();
                var sourceSpan = location.SourceSpan;
                var lineSpan = location.GetLineSpan();

                //// NB.  Lines start at 0.  Add one so when we look in Visual Studio it makes sense.

                //var startLine = location.GetLineSpan().StartLinePosition.Line + 1;
                //var endLine = location.GetLineSpan().EndLinePosition.Line + 1;

                sourceContext = string.Format("SourceSpan: >{0}< LineSpan: >{1}<",
                    sourceSpan.ToString(),
                    lineSpan.ToString());
            }

            return sourceContext;
        }

        private static string GetMethodContext(VisualBasicSyntaxNode node, ConfigurationOptions displayInfo)
        {
            string methodContext = "";

            if (displayInfo.MethodName)
            {
                methodContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
            }

            return methodContext;
        }

        private static string GetClassModuleContext(VisualBasicSyntaxNode node, ConfigurationOptions displayInfo)
        {
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

                classModuleContext = String.Format("{0, 8}{1,6}:({2,-25})",
                    classModuleContext,
                    typeName,
                    typeName == "Class" ? className : moduleName);
            }

            return classModuleContext;
        }

        private static string GetAncestorContext(VisualBasicSyntaxNode node, ConfigurationOptions displayInfo)
        {
            string ancestorContext = "";

            if (displayInfo.ContainingBlock)
            {
                ancestorContext += GetContainingBlock(node).Kind().ToString();
            }

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

            return ancestorContext;
        }

        public static StringBuilder InvokeVNCSyntaxWalkerOld(
            StringBuilder sb,
            Boolean useRegEx, string regEx,
            Dictionary<string, Int32> matches,
            Dictionary<string, Int32> crcMatchesToString,
            Dictionary<string, Int32> crcMatchesToFullString,
            SyntaxTree syntaxTree,
            SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker, 
            ConfigurationOptions displayInfo)
        {
            walker.Messages = sb;

            //walker.Display.ClassOrModuleName = displayInfo.ClassOrModuleName;
            //walker.Display.MethodName = displayInfo.MethodName;
            walker.IdentifierNames = useRegEx ? regEx : ".*";
            walker.Display = displayInfo;

            walker.InitializeRegEx();

            walker.Matches = matches;
            walker.CRCMatchesToString = crcMatchesToString;
            walker.CRCMatchesToFullString = crcMatchesToFullString;

            walker.Visit(syntaxTree.GetRoot());

            return sb;
        }

        public static StringBuilder InvokeVNCSyntaxWalker(
            SyntaxWalkers.VB.VNCVBSyntaxWalkerBase walker,
            SearchTreeCommandConfiguration commandConfiguration)
        {
            walker.Messages = commandConfiguration.Results;

            walker.IdentifierNames = commandConfiguration.UseRegEx ? commandConfiguration.RegEx : ".*";
            walker.Display = commandConfiguration.ConfigurationOptions;

            walker.InitializeRegEx();

            walker.Matches = commandConfiguration.Matches;
            walker.CRCMatchesToString = commandConfiguration.CRCMatchesToString;
            walker.CRCMatchesToFullString = commandConfiguration.CRCMatchesToFullString;

            walker.Visit(commandConfiguration.SyntaxTree.GetRoot());

            return commandConfiguration.Results;
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
            else
            {
                var triviaList = node.GetLeadingTrivia().ToSyntaxTriviaList();

                Boolean leadingTriviaResult = false;

                foreach (SyntaxTrivia syntaxTrivia in node.GetLeadingTrivia().ToSyntaxTriviaList())
                {
                    var triviaKind = syntaxTrivia.Kind();

                    if (triviaKind == SyntaxKind.EndOfLineTrivia 
                        || triviaKind == SyntaxKind.WhitespaceTrivia
                        || triviaKind == SyntaxKind.CommentTrivia)
                    {
                        leadingTriviaResult = true;
                    }
                }

                Boolean trailingTriviaResult = false;

                foreach (SyntaxTrivia syntaxTrivia in node.GetTrailingTrivia().ToSyntaxTriviaList())
                {
                    var triviaKind = syntaxTrivia.Kind();

                    if (triviaKind == SyntaxKind.EndOfLineTrivia
                        || triviaKind == SyntaxKind.WhitespaceTrivia
                        || triviaKind == SyntaxKind.CommentTrivia)
                    {
                        trailingTriviaResult = true;
                    }
                }

                result = leadingTriviaResult & trailingTriviaResult;
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

        static VisualBasicSyntaxNode GetContainingBlock(VisualBasicSyntaxNode node)
        {
            //var block = node.Parent as VisualBasicSyntaxNode;
            //var blockKind = block.Kind();
            var blockKindText = node.Parent.Kind().ToString();

            if (blockKindText.Contains("Block"))
            {
                return (VisualBasicSyntaxNode)node.Parent;
            }

            if (blockKindText == "CompilationUnit")
            {
                return node;
            }

            return GetContainingBlock(node.Parent as VisualBasicSyntaxNode);
        }
    }
}
