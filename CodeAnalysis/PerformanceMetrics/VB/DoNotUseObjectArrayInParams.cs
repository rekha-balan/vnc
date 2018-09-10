using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    public class DoNotUseObjectArrayInParams
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<MethodDeclarationSyntax>()//#2
            //            .Where(mds => mds.ParameterList.Parameters
            //            .Any(p => p.Modifiers
            //            .Any(m => m.Text == "params" && //#3
            //            p.Type.ToFullString().Replace(" ", string.Empty)
            //            .Contains("object[]"))))
            //            .Select(mds => new //#4
            //            {
            //                //Name of the class
            //                ClassName = mds.Ancestors()
            //.OfType<ClassDeclarationSyntax>()
            //.First()
            //.Identifier
            //.ValueText,
            //                //The name of defaulter method
            //                MethodName = mds.Identifier.ValueText
            //            })
            //.Dump("Methods with param objects");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
