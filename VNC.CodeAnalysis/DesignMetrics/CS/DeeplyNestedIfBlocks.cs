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

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    public class DeeplyNestedIfBlocks
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => loopTypes.Any(l => t.Kind() == l))//#3
            //            .Select(t => new //#4
            //{
            //                Method = t.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First()
            //            .Identifier.ValueText,
            //                Nesting = 1 + t.Ancestors()
            //            .Count(z => loopTypes
            //            .Any(l => z.Kind() == l))
            //            })
            //            .ToLookup(t => t.Method)
            //            //#5
            //            .ToDictionary(t => t.Key,
            //            t => t.Select(m => m.Nesting).Max())
            //            .Select(t => new { Method = t.Key, Nesting = t.Value })
            //            //Find only if blocks that are deeper than 3 levels.
            //            .Where(t => t.Nesting >= 3)//#6
            //            .Dump("Deeply nested if-statements");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
