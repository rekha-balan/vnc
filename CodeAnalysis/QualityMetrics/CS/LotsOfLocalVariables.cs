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
    public class LotsOfLocalVariables
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //        SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            //        List<MethodDeclarationSyntax> methods =
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(d => d.Kind() == SyntaxKind.MethodDeclaration)
            //        .Cast<MethodDeclarationSyntax>()
            //        .ToList();//#1
            //        methods
            //        .Select(z =>
            //        new {
            //            MethodName = z.Identifier.ValueText,//#2
            //NBLocal = z.Body.Statements
            //        //#3
            //        .Count(x => x.Kind() == SyntaxKind.LocalDeclarationStatement)
            //        })
            //        .OrderByDescending(x => x.NBLocal)
            //        .ToList()
            //        .ForEach(x =>
            //        Console.WriteLine(x.MethodName + " " + x.NBLocal));

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
