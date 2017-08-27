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

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    class MagicNumbersInCondition
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            var operators = new List<SyntaxKind>()
            //{
            //SyntaxKind.GreaterThanToken,
            //SyntaxKind.GreaterThanEqualsToken,
            //SyntaxKind.LessThanEqualsToken,
            //SyntaxKind.LessThanToken,
            //SyntaxKind.EqualsEqualsToken,
            //SyntaxKind.LessThanLessThanEqualsToken,
            //SyntaxKind.GreaterThanGreaterThanEqualsToken
            //};

            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.ClassDeclaration)
            //            .Cast<ClassDeclarationSyntax>()
            //            .Select(t =>
            //           new
            //            {
            //                ClassName = t.Identifier.ValueText,//#1
            //    MethodTokens
            //           = t.Members
            //           .Where(m => m.Kind() == SyntaxKind.MethodDeclaration)
            //           .Cast<MethodDeclarationSyntax>()
            //           .Select(
            //           mds =>
            //           new
            //            {
            //                MethodName = mds.Identifier.ValueText,
            //                Tokens = CSharpSyntaxTree.ParseText(mds.ToFullString())
            //           .GetRoot()
            //           .DescendantTokens()
            //           .Select(c => c.Kind())
            //            })
            //           .Select(w =>
            //          new
            //            {
            //                MethodName = w.MethodName, //#2
            //    Toks = w.Tokens.Zip(w.Tokens.Skip(1), (a, b) =>
            //     operators.Any(op => op == a) && b
            //     == SyntaxKind.NumericLiteralToken)//#3
            //})
            //           .Where(w => w.Toks.Any(to => to == true))//#4
            //           .Select(w => w.MethodName)
            //            }).Dump();


            return sb;
        }
    }
}
