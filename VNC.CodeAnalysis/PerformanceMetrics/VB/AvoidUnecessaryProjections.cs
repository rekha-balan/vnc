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

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    class AvoidUnecessaryProjections
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);//#1
//            var decls = tree
//            .GetRoot()
//            .DescendantNodes()
//            .OfType<LocalDeclarationStatementSyntax>();//#2
//            var projectors = new string[] {“.ToList();”,”.ToArray();”};
//if (decls.Count() > 0) //#3
//{
//var defaulters = decls.Select(ldss => new //#4
//{
//    ClassName = ldss.Ancestors()
//.OfType<ClassDeclarationSyntax>()
//.FirstOrDefault()
//?.Identifier
//.ValueText,
//    Method = ldss.Ancestors()
//.OfType<MethodDeclarationSyntax>()
//.FirstOrDefault()?.Identifier
//.ValueText,
//    Statement = ldss.ToFullString().Trim()
//})
////#5
//.Where(ldss => projectors
//.Any(projector => ldss.Statement.Trim()
//.EndsWith(projector)));
//if(defaulters.Count() > 0)
//defaulters.Dump("Un-necessary projections");
//}
            return sb;
        }
    }
}
