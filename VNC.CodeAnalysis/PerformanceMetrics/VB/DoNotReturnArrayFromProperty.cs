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
    public class DoNotReturnArrayFromProperty
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);//#1
//            tree.GetRoot()
//            .DescendantNodes()
//            .OfType<ClassDeclarationSyntax>()//#2
//            .Select(cds => new //#3
//{
//                ClassName = cds.Identifier.ValueText,
//                Properties = cds.Members
//            .OfType<PropertyDeclarationSyntax>()
//            .Select(pds => new //#4
//{
//                PropertyName = pds.Identifier.ValueText,
//                PropertyType = pds.Type.ToFullString()
//            .Trim()
//            })
//            })
//.Where(cds => cds.Properties //#5
//.Any(p => p.PropertyType.Contains("[")))
//.Dump("Properties returning an array");

            return sb;
        }
    }
}
