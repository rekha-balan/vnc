using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class LongParameterList
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()
            //            .Select(cds =>
            //           new
            //            {
            //                ClassName = cds.Identifier.ValueText,//#1
            //    Methods = cds.Members.OfType<MethodDeclarationSyntax>()
            //           .Select(mds => new {
            //                MethodName = mds.Identifier.ValueText//#2
            //          ,
            //                Parameters = mds.ParameterList.Parameters.Count//#3
            //})
            //            }).Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
