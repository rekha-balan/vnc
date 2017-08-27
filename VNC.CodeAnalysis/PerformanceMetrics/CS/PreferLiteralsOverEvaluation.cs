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
    class PreferLiteralsOverEvaluation
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code); //#1
//                                                         //Finding all the literals in the code.
//            var literals = tree.GetRoot()
//            .DescendantNodes()
//            .OfType<LiteralExpressionSyntax>()//#2
//            .Select(les => les.ToFullString())
//            .Distinct();
//            tree.GetRoot()
//            .DescendantNodes()
//            .OfType<InvocationExpressionSyntax>()
//            .Select(ies => new //#3
//{
//                MethodName = ies.Ancestors()
//            .OfType<MethodDeclarationSyntax>()
//            ?.First()?.Identifier.ValueText,
//                Expression = ies.Expression.ToFullString(),
//                CallTokens = ies.Expression.ChildNodes()
//            .Select(e => e.ToFullString())
//            })
//            //#4
//            .Where(ies => ies.CallTokens.Any(ct => literals.Contains(ct)))
//            //#5
//            .Select(ies =>
//            new
//            {
//                MethodName = ies.MethodName,
//                Expression = ies.Expression
//            })
//            .Dump("Methods using ToUpper or ToLower on string literals");

            return sb;
        }
    }
}
