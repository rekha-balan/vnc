using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class FragmentedConditions
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();


            var tree = CSharpSyntaxTree.ParseText(sourceCode);

            var results = tree.GetRoot()
            .DescendantNodes()
            .Where(t => t.Kind() == SyntaxKind.MethodDeclaration)
            .Cast<MethodDeclarationSyntax>()//#1
            .Select(t => new
            {
                Name = t.Identifier.ValueText,
                IfStatements = t.Body.Statements
               .Where(m => m.Kind() == SyntaxKind.IfStatement)
               .Cast<IfStatementSyntax>()
               .Select(iss =>
                  //#2
                  new
                  {
                      Statement = iss.Statement.ToFullString(),
                      //#3
                      IfStatement = iss.Condition.ToFullString()
                  })
                //#4
                //.ToLookup(iss => iss.Statement)
            });
            //        .Dump("Fragmented conditions");

            foreach(var item in results)
            {
                sb.AppendLine(item.Name);

                foreach (var ifs in item.IfStatements)
                {
                    sb.AppendLine(ifs.Statement);
                    sb.AppendLine(ifs.IfStatement);
                }
            }

            return sb;
        }
    }
}
