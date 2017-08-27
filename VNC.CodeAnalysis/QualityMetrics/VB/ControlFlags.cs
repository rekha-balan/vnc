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
    class ControlFlags
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            //Find all boolean variables in the class
            //            //Find all if statements that solely rely on those
            //            //Find the methods in which these if statements are
            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            var bools = tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.VariableDeclaration
            //           && t.ToFullString().Trim().StartsWith("bool"))
            //            .Select(t =>
            //           new
            //            {
            //                VariableName = t.ToFullString().Trim()
            //           .Split(' ')[1],
            //                Class = t.Ancestors()
            //           .Where
            //           (x => x.Kind() == SyntaxKind.ClassDeclaration)
            //           .Cast<ClassDeclarationSyntax>()
            //           .First().Identifier.ValueText
            //            })
            //            .Select(t => t.VariableName);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind == SyntaxKind.IfStatement)
            //            .Cast<IfStatementSyntax>()
            //            .Select(ifs =>
            //           new
            //            {
            //                If = ifs.ToFullString(), //#2
            //    Condition = ifs.Condition.ToFullString(),//#3
            //    Line = ifs.SyntaxTree
            //           .GetLineSpan(ifs.FullSpan, false)
            //           .StartLinePosition.Line + 1//#4
            //})
            //            .Where(ifs => ifs.Condition.Split(new string[]{"&&", "||", "==","(",")","
            //"},StringSplitOptions.RemoveEmptyEntries)
            ////If a control flag is used,
            //.Any (c => bools.Contains(c) ||
            ////it can also be used in a negative way. skipping "!"
            //bools.Contains(c.Substring(1))))//#5
            //           .Dump("if nodes with control flags");

            return sb;
        }
    }
}
