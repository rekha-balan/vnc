﻿using System;

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
    public class DeeplyNestedLoops
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            var loopTypes = new List<SyntaxKind>()
            //{
            //SyntaxKind.ForStatement,
            //SyntaxKind.ForEachStatement,
            //SyntaxKind.WhileStatement
            //};//#2
            //            tree.GetRoot()
            //                .DescendantNodes()
            //.Where(t => loopTypes.Any(l => t.Kind() == l))//#3
            //.Select(t => new
            //{
            //    //#4
            //    Method = t.Ancestors().OfType<MethodDeclarationSyntax>()
            //.First()
            //.Identifier.ValueText,
            //    Nesting = 1 + t.Ancestors()
            //.Count(z => loopTypes
            //.Any(l => z.Kind() == l))
            //})//#5
            //.ToLookup(t => t.Method)
            //.ToDictionary(t => t.Key,
            //t => t.Select(m => m.Nesting).Max())//#6
            //.Select(t => new { Method = t.Key, Nesting = t.Value })
            //.Where(t => t.Nesting >= 3)//#7
            //.Dump("Deeply Nested Loops");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}