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
    public class PreferJaggedArraysOverMultidimentionalArrays
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);//#1
//            tree.GetRoot()
//            .DescendantNodes()
//            .OfType<ArrayRankSpecifierSyntax>()//#2
//            .Select(ats => new //#3
//{
//                BelongsTo = ats.Ancestors()
//            .OfType<ClassDeclarationSyntax>()
//            .First()?.Identifier.ValueText,
//                ArrayType = ats.ToFullString()
//            })
//            .Where(ats => ats.ArrayType.Contains(","))//#4
//            .Dump("Classes with multi-dimentional array");

            return sb;
        }
    }
}
