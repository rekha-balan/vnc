using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class MagicNumbersInMath
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //        List<CS.SyntaxKind> kinds = new List<CS.SyntaxKind>()
            //        {
            //        CS.SyntaxKind.AddAssignExpression,//+=
            //        CS.SyntaxKind.SubtractAssignExpression,//-=
            //        CS.SyntaxKind.MultiplyAssignExpression,//*=
            //        CS.SyntaxKind.DivideAssignExpression, // /=
            //        CS.SyntaxKind.AddExpression, // +
            //        CS.SyntaxKind.SubtractExpression,// -
            //        CS.SyntaxKind.MultiplyExpression,// *
            //        CS.SyntaxKind.DivideExpression // /
            //        };

            //        CS.CSharpSyntaxTree.ParseText(sourceCode)
            //        .GetRoot()
            //        .DescendantNodes()
            //        .Where(st => st.Kind == CS.SyntaxKind.MethodDeclaration)
            //        .Cast<MethodDeclarationSyntax>()
            //        .Select(st =>
            //       new
            //        {
            //            MethodName = st.Identifier.ValueText,//#1
            //MagicLines =
            //       CS.CSharpSyntaxTree.ParseText(st.ToFullString())
            //       .GetRoot()
            //       .DescendantNodes()
            //       .Where(z => kinds
            //      .Any(k => k == z.Kind()))
            //       .Select(q => q.ToFullString().Trim())
            //       .Where(w => CS.CSharpSyntaxTree
            //       .ParseText("void dummy(){" + w.ToString() + "}")
            //       .GetRoot()
            //       .DescendantTokens()
            //       .Any(s => //#2
            //       s.Kind == SyntaxKind.NumericLiteralToken))
            //        }).Dump("Magic lines. Please avoid these");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
