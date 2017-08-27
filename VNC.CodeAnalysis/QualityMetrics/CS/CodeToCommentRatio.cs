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
    class CodeToCommentRatio
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            // Both of these return the same results.

            var x1 = tree.GetRoot().DescendantNodes().Where(syn => syn.IsKind(VB.SyntaxKind.ClassBlock));
            var x2 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>();

            //sb.AppendLine("Where(...)");
            //foreach (SyntaxNode node in x1)
            //{
            //    sb.AppendLine(node.ToFullString());
            //}

            //sb.AppendLine("OfType(...)");
            //foreach (SyntaxNode node in x2)
            //{
            //    sb.AppendLine(node.ToFullString());
            //}

            //var x3 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
            //    .Cast<VB.Syntax.ClassBlockSyntax>()
            //    .Select(c =>
            //       new
            //       {
            //           ClassName = c.BlockStatement.Identifier,
            //           Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
            //       });

            //foreach (var node in x3)
            //{
            //    sb.AppendLine(node.ClassName.Text);
            //    //sb.AppendLine(node.ClassName.Value.ToString());

            //    foreach (var method in node.Methods)
            //    {

            //        sb.AppendLine(method.ToString());
            //    }
            //}

            //var x4 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
            //    .Cast<VB.Syntax.ClassBlockSyntax>()
            //    .Select(c =>
            //       new
            //       {
            //           ClassName = c.BlockStatement.Identifier,
            //           Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
            //       });

            //foreach (var node in x4)
            //{
            //    //sb.AppendLine(node.ClassName.Text);
            //    sb.AppendLine(node.ClassName.Value.ToString());

            //    foreach (var method in node.Methods)
            //    {
            //        sb.AppendLine("Method");
            //        VB.Syntax.MethodStatementSyntax statement = method.DescendantNodes().First() as VB.Syntax.MethodStatementSyntax;
            //        sb.AppendLine(statement.Identifier.ToString());
            //        sb.AppendLine("Parameters");
            //        sb.AppendLine(statement.ParameterList.ToString());
            //    }
            //}

            var x5 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
                .Cast<VB.Syntax.ClassBlockSyntax>()
                .Select(c =>
                   new
                   {
                       ClassName = c.BlockStatement.Identifier,
                       Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
                   })
                .Select(t =>
                    new
                    {
                        ClassName = t.ClassName,
                        MethodDetails = t.Methods
                            .Select(m =>
                               new
                               {
                                   Name = ((VB.Syntax.MethodStatementSyntax)m.DescendantNodes().First()).Identifier.ValueText,
                                   Lines = m.Statements.Count,
                                   Comments = m.DescendantTrivia().Count(b => b.IsKind(VB.SyntaxKind.CommentTrivia))
                               }
                            )
                    });

            foreach (var item in x5)
            {
                sb.AppendLine(item.ClassName.Text);

                foreach (var detail in item.MethodDetails)
                {
                    sb.AppendLine(string.Format("   {0,-40}   Statements:{1,5}    Comments:{2,5}",
                        detail.Name,
                        detail.Lines.ToString(),
                        detail.Comments.ToString()));
                }
            }

            return sb;
        }
    }
}
