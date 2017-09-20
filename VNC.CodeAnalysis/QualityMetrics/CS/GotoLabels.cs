using System;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class GoToLabels
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = CSharpSyntaxTree.ParseText(sourceCode);

            var results = tree.GetRoot()
                .DescendantNodes()
                .Where(t => t.Kind() == SyntaxKind.ClassDeclaration)
                .Cast<ClassDeclarationSyntax>()
                .Select(cds =>
                new
                {
                    ClassName = cds.Identifier.ValueText,
                    Methods = cds.Members
                        .Where(m => m.Kind() == SyntaxKind.MethodDeclaration)
                        .Cast<MethodDeclarationSyntax>()
                        .Select(mds =>
                        new
                        {
                            MethodName = mds.Identifier.ValueText,
                            HasGoto = CSharpSyntaxTree.ParseText(mds.ToString())
                                .GetRoot()
                                .DescendantTokens() 
                                .Any (st => st.Kind() == SyntaxKind.GotoKeyword)
                        })
                        .Where(mds => mds.HasGoto)
                        .Select(mds => mds.MethodName)
                });

            foreach (var item in results)
            {
                sb.AppendLine(string.Format("Class >{0}<", item.ClassName));

                foreach (var method in item.Methods)
                {
                    sb.AppendLine(string.Format("  Method >{0}<,", method.ToString()));
                }
            }

            return sb;
        }
    }
}
