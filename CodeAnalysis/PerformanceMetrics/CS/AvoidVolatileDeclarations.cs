using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class AvoidVolatileDeclarations
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<FieldDeclarationSyntax>()//#2
            //            .Where(vds => vds.Modifiers
            //            .Any(m => m.ValueText == "volatile"))//#3
            //            .Select(vds => new //#4
            //{
            //                ClassName = vds.Ancestors()
            //            .OfType<ClassDeclarationSyntax>()
            //            .First()?.Identifier.ValueText,
            //                VolatileDeclaration = vds.ToFullString()
            //            })
            //            .Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
