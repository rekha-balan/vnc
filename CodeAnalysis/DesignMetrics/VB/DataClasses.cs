using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class DataClasses
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);
            //            var classes = tree
            //            .GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()//#1
            //            .Select(cds => new //#2
            //{
            //    //Name of the class
            //    Name = cds.Identifier.ValueText,
            //    //Number of members of the class
            //    MemberCount = cds.Members.Count,
            //    //Number of public properties
            //    PublicPropertyCount = cds.Members
            //            .OfType<PropertyDeclarationSyntax>()
            //            .Count(pds => pds.Modifiers
            //            .Any(m => m.ValueText == "public"))
            //            })
            //            .Where(cds => cds.MemberCount == cds.PublicPropertyCount)//#3
            //            .Dump("Data Classes");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
