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

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class UnusedMethodParameters
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            List<MethodDeclarationSyntax> methods =
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<MethodDeclarationSyntax>().ToList();
            //            methods.Select(z =>
            //            {
            //                var parameters =
            //                z.ParameterList.Parameters
            //                .Select(p => p.Identifier.ValueText);

            //                return
            //                new
            //                {
            //                    MethodName =
            //                z.Identifier.ValueText,//#1
            //                                       //#2
            //    IsUsingAllParameter =
            //                parameters.All
            //                (x => z.Body.Statements.SelectMany(s => s.DescendantTokens()).Select(s =>
            //              s.ValueText).Distinct().Contains(x))
            //                };
            //            })
            //.Where(x => !x.IsUsingAllParameter)
            //.ToList()
            //.ForEach(x => Console.WriteLine(x.MethodName + " "
            //+ x.IsUsingAllParameter));

            return sb;
        }
    }
}
