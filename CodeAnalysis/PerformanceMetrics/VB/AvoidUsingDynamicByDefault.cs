using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    public class AvoidUsingDynamicByDefault
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(code);//#1
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<VariableDeclarationSyntax>()//#2
            //                                    //#3
            //.Where(vds => vds.Type.ToFullString().Trim() == "dynamic")
            //.Select(vds => vds.ToFullString())
            //.Dump("All usages of dynamic. Some may not be required");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
