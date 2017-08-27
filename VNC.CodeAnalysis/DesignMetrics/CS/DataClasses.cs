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
    class DataClasses
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);
//            var classes = tree
//            .GetRoot()
//            .DescendantNodes()
//            .OfType<ClassDeclarationSyntax>()//#1
//            .Select(cds => new //#2
//{
//    //Name of the class
//    Name = cds.Identifier.ValueText,
//    //Number of members of the class
//    MemberCount = cds.Members.Count,
//    //Number of public properties
//    PublicPropertyCount = cds.Members
//            .OfType<PropertyDeclarationSyntax>()
//            .Count(pds => pds.Modifiers
//            .Any(m => m.ValueText == "public"))
//            })
//            .Where(cds => cds.MemberCount == cds.PublicPropertyCount)//#3
//            .Dump("Data Classes");

            return sb;
        }
    }
}
