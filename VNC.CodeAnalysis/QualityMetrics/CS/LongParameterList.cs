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
    public class LongParameterList
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()
            //            .Select(cds =>
            //           new
            //            {
            //                ClassName = cds.Identifier.ValueText,//#1
            //    Methods = cds.Members.OfType<MethodDeclarationSyntax>()
            //           .Select(mds => new {
            //                MethodName = mds.Identifier.ValueText//#2
            //          ,
            //                Parameters = mds.ParameterList.Parameters.Count//#3
            //})
            //            }).Dump();

            return sb;
        }
    }
}
