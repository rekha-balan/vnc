using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class OutParameters
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(code);
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<ClassDeclarationSyntax>()//#1
            //.Select(cds => new
            //{
            //    ClassName = cds.Identifier.ValueText,
            //    Methods = cds.Members
            //.OfType<MethodDeclarationSyntax>()//#2
            //.Where(mds => //#3
            //mds.ParameterList
            //.Parameters.Any(z =>
            //z.Modifiers.Any(m =>
            //m.ValueText == "out")))
            //.Select(mds => mds.Identifier.ValueText)
            //})
            //.Dump("Methods with \"out\" parameters");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");

            return sb;
        }
    }
}
