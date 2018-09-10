
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using System.Linq;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class CodeToCommentRatio
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            // Both of these return the same results.

            //var x1 = tree.GetRoot().DescendantNodes().Where(syn => syn.IsKind(SyntaxKind.ClassBlock));
            //var x2 = tree.GetRoot().DescendantNodes().OfType<ClassBlockSyntax>();

            var results = tree.GetRoot().DescendantNodes().OfType<ClassBlockSyntax>()
                .Cast<ClassBlockSyntax>()
                .Select(c =>
                   new
                   {
                       ClassName = c.BlockStatement.Identifier,
                       Methods = c.Members.OfType<MethodBlockSyntax>()
                   }
                )
                .Select(t =>
                    new
                    {
                        ClassName = t.ClassName,
                        MethodDetails = t.Methods
                            .Select(m =>
                               new
                               {
                                   Name = ((MethodStatementSyntax)m.DescendantNodes().First()).Identifier.ValueText,
                                   Lines = m.Statements.Count,
                                   Comments = m.DescendantTrivia().Count(b => b.IsKind(SyntaxKind.CommentTrivia))
                               }
                            )
                    });

            foreach (var item in results)
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
