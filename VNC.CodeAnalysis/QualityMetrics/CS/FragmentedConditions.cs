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

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    class FragmentedConditions
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();


            //        var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(t => t.Kind() == SyntaxKind.MethodDeclaration)
            //        .Cast<MethodDeclarationSyntax>()//#1
            //        .Select(t => new {
            //            Name = t.Identifier.ValueText,
            //            IfStatements = t.Body.Statements
            //       .Where(m => m.Kind() == SyntaxKind.IfStatement)
            //       .Cast<IfStatementSyntax>()
            //       .Select(iss =>
            //      //#2
            //      new {
            //            Statement = iss.Statement.ToFullString(),
            ////#3
            //IfStatement = iss.Condition.ToFullString()
            //        })
            //       //#4
            //       .ToLookup(iss => iss.Statement)
            //        })
            //        .Dump("Fragmented conditions");

            return sb;
        }
    }
}
