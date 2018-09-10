using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class DeeplyNestedIfBlocks
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .Where(t => loopTypes.Any(l => t.Kind() == l))//#3
            //            .Select(t => new //#4
            //{
            //                Method = t.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First()
            //            .Identifier.ValueText,
            //                Nesting = 1 + t.Ancestors()
            //            .Count(z => loopTypes
            //            .Any(l => z.Kind() == l))
            //            })
            //            .ToLookup(t => t.Method)
            //            //#5
            //            .ToDictionary(t => t.Key,
            //            t => t.Select(m => m.Nesting).Max())
            //            .Select(t => new { Method = t.Key, Nesting = t.Value })
            //            //Find only if blocks that are deeper than 3 levels.
            //            .Where(t => t.Nesting >= 3)//#6
            //            .Dump("Deeply nested if-statements");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");

            return sb;
        }
    }
}
