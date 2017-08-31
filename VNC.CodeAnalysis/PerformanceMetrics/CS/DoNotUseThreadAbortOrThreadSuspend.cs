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
using CS = Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class DoNotUseThreadAbortOrThreadSuspend
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                                        //Finding names of all “Thread” objects
            //            var allThreadNames =
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<LocalDeclarationStatementSyntax>()//#2
            //            .Where(ldss => ldss.Declaration.Type
            //            .ToFullString().Trim() == "Thread" ||
            //            ldss.Declaration.Type.ToFullString().Trim()
            //            == "System.Threading.Thread")//#3
            //            .SelectMany(ldss => ldss.Declaration.Variables
            //            .Select(v => v.Identifier.ValueText));//#4
            //                                                  //Finding all the method invocations
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<InvocationExpressionSyntax>()//#5
            //            .Where(ies => allThreadNames
            //            .Any(tn => ies.Expression.ToFullString()
            //            .Trim().StartsWith(tn + ".Abort")//#6
            //            || ies.Expression.ToFullString()
            //            .Trim().StartsWith(tn + ".Suspend")))
            //            .Select(d => new //#7
            //{
            //                Method = d.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First().Identifier.ValueText,
            //                Line = d.Expression.ToFullString().Trim()
            //            })
            //            .Dump("Defaulters");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
