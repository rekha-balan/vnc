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
    class GotoLabels
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.ClassDeclaration)
            //            .Cast<ClassDeclarationSyntax>()
            //            .Select(cds =>
            //           new
            //            {
            //                ClassName = cds.Identifier.ValueText,
            //                Methods =
            //           cds.Members
            //           .Where(m => m.Kind() ==
            //          SyntaxKind.MethodDeclaration)
            //           .Cast<MethodDeclarationSyntax>()
            //           .Select(mds =>
            //          new
            //            {
            //                MethodName = mds.Identifier.ValueText,
            //                HasGoto =
            //          CSharpSyntaxTree.ParseText(mds.ToString())
            //          .GetRoot()
            //          .DescendantTokens()
            //          //Checking whether the method uses "goto" labels or not .Any (st => st.Kind()
            //          == SyntaxKind.GotoKeyword)
            //            })
            //            .Where(mds => mds.HasGoto)
            //            .Select(mds => mds.MethodName)})
            //.Dump("Classwise methods which use goto");

            return sb;
        }
    }
}
