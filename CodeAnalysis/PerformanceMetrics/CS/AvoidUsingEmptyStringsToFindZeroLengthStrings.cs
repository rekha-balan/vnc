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

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class AvoidUsingEmptyStringsToFindZeroLengthStrings
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                                        //Finding all instances of “string” in source code.
            //            var strings = tree
            //            .GetRoot()
            //            .DescendantNodes()
            //            .OfType<VariableDeclarationSyntax>()//#2
            //            .Where(vds => vds.Type.ToFullString().Trim() ==
            //            "string")//#3
            //            .SelectMany(vds => vds.Variables.Select(v =>
            //            v.Identifier.ValueText));//#4
            //            var results = tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<IfStatementSyntax>()
            //            .Where(iss => strings
            //            .Any(s => iss.Condition.ToFullString()
            //            .Contains(s + " == \"\""))) //#5
            //            .Select(iss => new //#6
            //{
            //                MethodName = iss.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First()
            //            .Identifier.ValueText,
            //                Condition = iss.ToFullString()
            //            });
            //            if (results.Any())
            //                results.Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}