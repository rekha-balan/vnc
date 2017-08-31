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

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    public class AvoidExcessiveLocalVariables
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                            //The recommended value is 64;
            //                                            //But for deomonstration purpose it is changed to 4
            //const int MAX_LOCALS_ALLOWED = 4; //#2
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<MethodDeclarationSyntax>() //#3
            //.Where(mds =>
            //mds.Body.Statements
            //.OfType<LocalDeclarationStatementSyntax>()
            //.Count() >= MAX_LOCALS_ALLOWED) //#4
            //.Select(mds => mds.Identifier.ValueText)//#5
            //.Dump("Methods with many local variable declarations");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
