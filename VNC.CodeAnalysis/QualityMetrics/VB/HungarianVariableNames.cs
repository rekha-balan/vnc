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

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class HungarianVariableNames
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //        Func<string, string, bool> IsHungarian = (varName, typeName) =>
            //        {
            //            bool result = false;
            //            string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //            if (typeName == "bool"
            //            && varName.StartsWith("b")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "char"
            //            && varName.StartsWith("c")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "string"
            //            && varName.StartsWith("str")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "int"
            //            && varName.StartsWith("i")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "float"
            //            && varName.StartsWith("f")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "short"
            //            && varName.StartsWith("s")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            if (typeName == "long"
            //            && varName.StartsWith("l")
            //            && upperCase.Contains(varName[1]))
            //                result = true;
            //            return result;
            //        };
            //        var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(t => t.Kind() == SyntaxKind.FieldDeclaration)
            //        .Cast<FieldDeclarationSyntax>()
            //        .Select(fds =>
            //       new
            //        {
            ////#2
            //TypeName = fds.Declaration.Type.ToFullString().Trim(),
            ////#3
            //VarName = fds.Declaration.Variables
            //       .Select(v => v.Identifier.Value).First()
            //        })
            //        //#4
            //        .Where(fds => IsHungarian(fds.VarName.ToString(),
            //       fds.TypeName.ToString()))
            //        .Dump("Hungaranian Notations");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
