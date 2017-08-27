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

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class LadderIfStatements
    {
        public static StringBuilder Check(string fileNameAsourceCodendPath)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.MethodDeclaration)
            //            .Cast<MethodDeclarationSyntax>()
            //            .Select(t =>
            //           new
            //            {
            //                Name = t.Identifier.ValueText,
            //                IfStatements = t.Body.Statements
            //           .Where(s => s.Kind() == SyntaxKind.IfStatement)
            //           .Cast<IfStatementSyntax>()
            //            })
            //.Select(t =>
            //new
            //{
            //   Name = t.Name,
            //   Ladders = t.IfStatements
            //.Select(i => i.Condition.ToFullString())
            //.ToLookup(i => i.Substring(0, i.IndexOf('=')))
            //.Where(i => i.Count() >= 2)
            //})
            //.Dump();

            return sb;
        }
    }
}
