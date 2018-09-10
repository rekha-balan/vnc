using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class MultipleReturnStatements
    {
        public static StringBuilder Check(string sourceCode)
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
            //                Name = t.Identifier.ValueText, //#1
            //    Returns = t.Body.DescendantTokens()
            //           .Count(st => st.Kind() == SyntaxKind.ReturnKeyword)//#2
            //})
            //            //Method should ideally have one return statement
            //            //That way it is easier to refactor them later.
            //            .Where(t => t.Returns > 1).Dump("Multiple return
            //            statements");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
