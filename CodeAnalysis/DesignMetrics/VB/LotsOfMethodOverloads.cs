using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class LotsOfMethodOverloads
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //        var tree = SyntaxTree.ParseText(code);
            //        tree.GetRoot()
            //        .DescendantNodes()
            //        .Where(t => t.Kind == SyntaxKind.ClassDeclaration)
            //        .Cast<ClassDeclarationSyntax>()
            //        .Select(cds =>
            //       new
            //        {
            //            ClassName = cds.Identifier.ValueText,//#1
            //Methods = cds.Members.OfType < MethodDeclarationSy
            //       ntax > ()//#2
            //       .Select(mds => mds.Identifier.ValueText)
            //        })
            //        .Select(cds => new {
            //            ClassName = cds.ClassName,
            //            Overloads = cds.Methods
            //       .ToLookup(m => m)
            //       .ToDictionary(m => m.Key, m => m.Count())
            //        })//#3
            //        .Dump("Overloaded Methods");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
