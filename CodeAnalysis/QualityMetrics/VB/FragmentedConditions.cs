using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class FragmentedConditions
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var results = tree.GetRoot()
                .DescendantNodes()
                .Where(t => t.Kind() == SyntaxKind.FunctionBlock || t.Kind() == SyntaxKind.SubBlock)
                .Cast<MethodBlockSyntax>()
                .Select(mb =>
                new
                {
                    Name = mb.SubOrFunctionStatement.Identifier.ValueText,
                    IfStatements = mb.Statements
                    .Where(i => i.Kind() == SyntaxKind.IfStatement)
                    .Cast<Microsoft.CodeAnalysis.VisualBasic.Syntax.IfStatementSyntax>()
                    .Select(iss =>
                    new
                    {
                        Statement = iss.ToFullString(),
                        IfStatement = iss.Condition.ToFullString()
                    })
                }).ToLookup(iss => iss.ToString());

            foreach (var item in results)
            {

            }

            //        var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(t => t.Kind() == SyntaxKind.MethodDeclaration)
            //        .Cast<MethodDeclarationSyntax>()//#1
            //        .Select(t => new {
            //            Name = t.Identifier.ValueText,
            //            IfStatements = t.Body.Statements
            //       .Where(m => m.Kind() == SyntaxKind.IfStatement)
            //       .Cast<IfStatementSyntax>()
            //       .Select(iss =>
            //      //#2
            //      new {
            //            Statement = iss.Statement.ToFullString(),
            ////#3
            //IfStatement = iss.Condition.ToFullString()
            //        })
            //       //#4
            //       .ToLookup(iss => iss.Statement)
            //        })
            //        .Dump("Fragmented conditions");

            sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
