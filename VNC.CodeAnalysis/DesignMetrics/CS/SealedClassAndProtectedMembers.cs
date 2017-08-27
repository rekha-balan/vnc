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
    class SealedClassAndProtectedMembers
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(code);
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<ClassDeclarationSyntax>()
            //.Where(cds => cds.Modifiers
            //.Any(m => m.ValueText == "sealed")) //#1
            //.Select
            //(
            //cds => //#2
            //new
            //{
            //    ClassName = cds.Identifier.ValueText,
            //    ProtectedMembers =
            //cds.Members
            //.OfType<MethodDeclarationSyntax>()
            //.Where(mds =>
            //mds.Modifiers
            //.Any(m => m.ValueText ==
            //"protected"))
            //.Select(mds => mds.Identifier
            //.ValueText)
            //}
            //)
            //.Where(cds => cds.ProtectedMembers.Count() > 0)//#3
            //.Dump("CA1047 Defaulters");
            return sb;
        }
    }
}
