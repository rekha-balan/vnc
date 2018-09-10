using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class LargeClass
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            var classAndMembers = tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => t.Kind() == SyntaxKind.ClassDeclaration)
            //            .Cast<ClassDeclarationSyntax>()//#1
            //            .Select(cds =>
            //           new {
            //                ClassName = cds.Identifier.ValueText,//#2
            //    Size = cds.Members.Count//#3
            //});
            //            var averageLength =
            //            classAndMembers
            //            .Select(classDetails => classDetails.Size)
            //            .Average();//#4
            //            classAndMembers
            //            .Where(am => am.Size > averageLength)//#5
            //            .Dump("Large Class");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
