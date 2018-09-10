using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.CS
{
    public class MagicNumbersInIndex
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //var tree = CSharpSyntaxTree.ParseText(sourceCode);
            //tree.GetRoot()
            //.DescendantNodes()
            //.OfType<BracketedArgumentListSyntax>()
            //.Select(bals =>
            //new
            //{
            //    Method = bals.Ancestors()
            //.OfType<MethodDeclarationSyntax>()
            //.First()
            //.Identifier.ValueText,
            //    Indices = bals.Arguments
            //.Select(a => a.GetText()
            //.Container
            //.CurrentText
            //.ToString())
            //})
            ////Find defaulter methods that use magic indices
            //.Where(bals =>
            //bals.Indices
            //.Any(i => Regex.Match(i, "[0-9]+").Success))
            //.Dump("Methods using magic indices");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
