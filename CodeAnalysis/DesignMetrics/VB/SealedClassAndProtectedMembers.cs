using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class SealedClassAndProtectedMembers
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(code);
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<ClassDeclarationSyntax>()
            //.Where(cds => cds.Modifiers
            //.Any(m => m.ValueText == "sealed")) //#1
            //.Select
            //(
            //cds => //#2
            //new
            //{
            //    ClassName = cds.Identifier.ValueText,
            //    ProtectedMembers =
            //cds.Members
            //.OfType<MethodDeclarationSyntax>()
            //.Where(mds =>
            //mds.Modifiers
            //.Any(m => m.ValueText ==
            //"protected"))
            //.Select(mds => mds.Identifier
            //.ValueText)
            //}
            //)
            //.Where(cds => cds.ProtectedMembers.Count() > 0)//#3
            //.Dump("CA1047 Defaulters");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
