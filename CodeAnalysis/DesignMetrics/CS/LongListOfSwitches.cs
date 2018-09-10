using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    public class LongListOfSwitches
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<MethodDeclarationSyntax>() //#2
            //            .Select(mds => //#3
            //            new
            //            {
            //                Name = mds.Identifier.ValueText,
            //                Switches = mds.Body
            //            .DescendantNodes()
            //            .OfType<SwitchStatementSyntax>()
            //            .Select(st =>
            //            new
            //            {
            //                SwitchStatement = st.ToFullString(),
            //                52
            ////How many switch sections are there
            ////in the switch statement.
            //                Sections = st.Sections.Count
            //            })
            //.OrderByDescending(st => st.Sections)//#4
            //            })
            //.Dump("Switch statements per functions");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
