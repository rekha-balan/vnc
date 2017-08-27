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

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    class ValueTypesShouldOverrideEqualsAndGetHashCode
    {
        static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);//#1
//            tree.GetRoot()
//            .DescendantNodes()
//            .OfType<StructDeclarationSyntax>()//#2
//            .Select(sds => new //#3
//{
//                StructName = sds.Identifier.ValueText,
//    //Flag if “Equals” is overridden
//    OverridenEquals =
//            sds.Members
//            .OfType<MethodDeclarationSyntax>()
//            .FirstOrDefault(m => m.Identifier
//            .ValueText == "Equals"
//            && m.Modifiers.Any(mo =>
//            mo.ValueText == "override")) != null,
//    //Flag if “GetHashCode” is overridden
//    OverridenGetHashCode =
//            sds.Members
//            .OfType<MethodDeclarationSyntax>()
//            .FirstOrDefault(m => m.Identifier
//            .ValueText == "GetHashCode"
//            && m.Modifiers.Any(mo =>
//            mo.ValueText == "override")) != null
//            })
//            .Where(sds => !sds.OverridenEquals
//            || !sds.OverridenGetHashCode)//#4
//            .Dump("Defaulter Structs");

            return sb;
        }
    }
}
