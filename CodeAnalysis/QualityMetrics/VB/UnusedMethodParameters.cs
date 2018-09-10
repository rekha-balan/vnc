using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
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

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
