using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    public class AbstractTypesWithConstructors
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);
            //            var abstractTypes =
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()
            //            .Where(cds => cds.Modifiers
            //            .Any(m => m.ValueText == "abstract"))//#1
            //            .Select(cds => new //#2
            //{
            //                ClassName = cds.Identifier.ValueText,
            //                PublicConstructors =
            //            cds.Members
            //            .OfType<ConstructorDeclarationSyntax>()
            //            .Any(c => c.Modifiers
            //            .Any(m => m.ValueText == "public"))
            //            })
            //            .Where(cds => cds.PublicConstructors)//#3
            //            .Dump("AbstractTypesShouldNotHaveConstructors Violators");

            sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
