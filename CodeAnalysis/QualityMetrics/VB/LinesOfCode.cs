using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class LinesOfCode
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.ClassDeclaration)
            //            .Cast<ClassDeclarationSyntax>()
            //            .Select(cds =>
            //           new {
            //                ClassName = cds.Identifier.ValueText,//#1
            //    Methods = cds.Members
            //           .OfType<MethodDeclarationSyntax>()
            //           .Select(mds =>
            //          new {
            //                MethodName = mds.Identifier.ValueText,//#2
            //    LOC = mds.Body.SyntaxTree
            //          .GetLineSpan(mds.FullSpan).EndLinePosition.Line //#3
            //          - mds.Body.SyntaxTree.GetLineSpan(mds.FullSpan)
            //          .StartLinePosition.Line - 3 //#4
            //})
            //            }
            //            )
            //            .Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
