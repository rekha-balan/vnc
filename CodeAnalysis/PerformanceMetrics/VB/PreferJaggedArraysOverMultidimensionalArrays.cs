using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    public class PreferJaggedArraysOverMultidimensionalArrays
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ArrayRankSpecifierSyntax>()//#2
            //            .Select(ats => new //#3
            //{
            //                BelongsTo = ats.Ancestors()
            //            .OfType<ClassDeclarationSyntax>()
            //            .First()?.Identifier.ValueText,
            //                ArrayType = ats.ToFullString()
            //            })
            //            .Where(ats => ats.ArrayType.Contains(","))//#4
            //            .Dump("Classes with multi-dimentional array");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
