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
    public class LocalClasses
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            // var tree = CSharpSyntaxTree.ParseText(code);
            // tree
            // .GetRoot()
            // .DescendantNodes()
            // .OfType<ClassDeclarationSyntax>()//#1
            // .Select(cds =>
            //new
            // {
            //     ClassName = cds.Identifier.ValueText,
            //     LocalClasses = cds.Members
            //.OfType<ClassDeclarationSyntax>()
            //.Select(m => m.Identifier.ValueText)
            // }
            // )
            // .Where(cds => cds.LocalClasses.Count() >= 1)//#3
            // .Dump("Local Classes");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");

            return sb;
        }
    }
}
