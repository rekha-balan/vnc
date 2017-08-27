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

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class MulitpleReturnStatements
    {
        public static StringBuilder Check(string sourceCode)
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
            //                Name = t.Identifier.ValueText, //#1
            //    Returns = t.Body.DescendantTokens()
            //           .Count(st => st.Kind() == SyntaxKind.ReturnKeyword)//#2
            //})
            //            //Method should ideally have one return statement
            //            //That way it is easier to refactor them later.
            //            .Where(t => t.Returns > 1).Dump("Multiple return
            //            statements");

            return sb;
        }
    }
}
