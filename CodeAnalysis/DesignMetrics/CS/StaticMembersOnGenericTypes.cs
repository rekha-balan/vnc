using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    public class StaticMembersOnGenericTypes
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()
            //            .Where(cds => cds.Arity > 0)//#1
            //            .Select(cds => //#2
            //            new
            //            {
            //    //Name of the generic class
            //    GenericClassName = cds.Identifier.ValueText,
            //    //Static methods in the generic class
            //    StaticMethods = cds.Members
            //            .OfType<MethodDeclarationSyntax>()
            //            .Where(mds => mds.Modifiers
            //.Any(m => m.ValueText == "static"))
            //.Select(mds => mds.Identifier.ValueText)
            //            })
            //.Where(cds => cds.StaticMethods.Count() > 0)//#3
            //.Dump("Static methods on generic types");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
