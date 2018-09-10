using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class LadderIfStatements
    {
        public static StringBuilder Check(string fileNameAsourceCodendPath)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.MethodDeclaration)
            //            .Cast<MethodDeclarationSyntax>()
            //            .Select(t =>
            //           new
            //            {
            //                Name = t.Identifier.ValueText,
            //                IfStatements = t.Body.Statements
            //           .Where(s => s.Kind() == SyntaxKind.IfStatement)
            //           .Cast<IfStatementSyntax>()
            //            })
            //.Select(t =>
            //new
            //{
            //   Name = t.Name,
            //   Ladders = t.IfStatements
            //.Select(i => i.Condition.ToFullString())
            //.ToLookup(i => i.Substring(0, i.IndexOf('=')))
            //.Where(i => i.Count() >= 2)
            //})
            //.Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
