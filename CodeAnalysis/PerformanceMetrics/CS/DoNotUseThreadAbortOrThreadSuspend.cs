using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class DoNotUseThreadAbortOrThreadSuspend
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                                        //Finding names of all “Thread” objects
            //            var allThreadNames =
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<LocalDeclarationStatementSyntax>()//#2
            //            .Where(ldss => ldss.Declaration.Type
            //            .ToFullString().Trim() == "Thread" ||
            //            ldss.Declaration.Type.ToFullString().Trim()
            //            == "System.Threading.Thread")//#3
            //            .SelectMany(ldss => ldss.Declaration.Variables
            //            .Select(v => v.Identifier.ValueText));//#4
            //                                                  //Finding all the method invocations
            //            tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<InvocationExpressionSyntax>()//#5
            //            .Where(ies => allThreadNames
            //            .Any(tn => ies.Expression.ToFullString()
            //            .Trim().StartsWith(tn + ".Abort")//#6
            //            || ies.Expression.ToFullString()
            //            .Trim().StartsWith(tn + ".Suspend")))
            //            .Select(d => new //#7
            //{
            //                Method = d.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First().Identifier.ValueText,
            //                Line = d.Expression.ToFullString().Trim()
            //            })
            //            .Dump("Defaulters");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
