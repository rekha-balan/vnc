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
    public class LotsOfMethodOverloads
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //        var tree = SyntaxTree.ParseText(code);
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(t => t.Kind == SyntaxKind.ClassDeclaration)
            //        .Cast<ClassDeclarationSyntax>()
            //        .Select(cds =>
            //       new
            //        {
            //            ClassName = cds.Identifier.ValueText,//#1
            //Methods = cds.Members.OfType < MethodDeclarationSy
            //       ntax > ()//#2
            //       .Select(mds => mds.Identifier.ValueText)
            //        })
            //        .Select(cds => new {
            //            ClassName = cds.ClassName,
            //            Overloads = cds.Methods
            //       .ToLookup(m => m)
            //       .ToDictionary(m => m.Key, m => m.Count())
            //        })//#3
            //        .Dump("Overloaded Methods");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
