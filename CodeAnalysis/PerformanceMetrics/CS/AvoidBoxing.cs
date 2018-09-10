using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.PerformanceMetrics.CS
{
    public class AvoidBoxing
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();



            //var tree = CSharpSyntaxTree.ParseText(code);//#1
            //                                            //There are couple of boxing calls in the provided code sample
            //                                            //These should have been avoided
            //                                            //x - Int
            //                                            //o -> object
            //var objects =
            //tree.GetRoot()
            //.DescendantNodes().OfType<VariableDeclarationSyntax>()//#2
            //.SelectMany(aes => aes.Variables.Select(v =>
            //new //#3
            //{
            //    Type = aes.GetFirstToken().ValueText,
            //    Name = v.Identifier.ValueText,
            //    Value = aes.GetLastToken().ValueText
            //})
            //);
            //var defaulters = objects //#4
            //.Where(aes => aes.Type == "object"
            //&& objects.FirstOrDefault(d => d.Name == aes.Value
            //&& d.Type != "object") != null)
            //.Dump("Boxing calls");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
            }
    }
}
