using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class AvoidUsingEmptyStringsToFindZeroLengthStrings
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                                        //Finding all instances of “string” in source code.
            //            var strings = tree
            //            .GetRoot()
            //            .DescendantNodes()
            //            .OfType<VariableDeclarationSyntax>()//#2
            //            .Where(vds => vds.Type.ToFullString().Trim() ==
            //            "string")//#3
            //            .SelectMany(vds => vds.Variables.Select(v =>
            //            v.Identifier.ValueText));//#4
            //            var results = tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<IfStatementSyntax>()
            //            .Where(iss => strings
            //            .Any(s => iss.Condition.ToFullString()
            //            .Contains(s + " == \"\""))) //#5
            //            .Select(iss => new //#6
            //{
            //                MethodName = iss.Ancestors()
            //            .OfType<MethodDeclarationSyntax>()
            //            .First()
            //            .Identifier.ValueText,
            //                Condition = iss.ToFullString()
            //            });
            //            if (results.Any())
            //                results.Dump();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
