using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    public class DoNotReturnArrayFromProperty
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()//#2
            //            .Select(cds => new //#3
            //{
            //                ClassName = cds.Identifier.ValueText,
            //                Properties = cds.Members
            //            .OfType<PropertyDeclarationSyntax>()
            //            .Select(pds => new //#4
            //{
            //                PropertyName = pds.Identifier.ValueText,
            //                PropertyType = pds.Type.ToFullString()
            //            .Trim()
            //            })
            //            })
            //.Where(cds => cds.Properties //#5
            //.Any(p => p.PropertyType.Contains("[")))
            //.Dump("Properties returning an array");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
