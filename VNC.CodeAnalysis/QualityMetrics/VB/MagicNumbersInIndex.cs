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
    public class MagicNumbersInIndex
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<BracketedArgumentListSyntax>()
            //.Select(bals =>
            //new
            //{
            //    Method = bals.Ancestors()
            //.OfType<MethodDeclarationSyntax>()
            //.First()
            //.Identifier.ValueText,
            //    Indices = bals.Arguments
            //.Select(a => a.GetText()
            //.Container
            //.CurrentText
            //.ToString())
            //})
            ////Find defaulter methods that use magic indices
            //.Where(bals =>
            //bals.Indices
            //.Any(i => Regex.Match(i, "[0-9]+").Success))
            //.Dump("Methods using magic indices");


            return sb;
        }
    }
}
