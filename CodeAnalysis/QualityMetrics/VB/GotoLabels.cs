
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using System.Data;

using System.Linq;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class GoToLabels
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            var results = tree.GetRoot()
            .DescendantNodes()
            .Where(t => t.Kind() == SyntaxKind.ClassBlock)
            .Cast<ClassBlockSyntax>()
            .Select(cbs =>
            new
            {
                ClassName = cbs.ClassStatement.Identifier,
                Methods = cbs.Members
                .Where(m => m.Kind() == SyntaxKind.FunctionBlock || m.Kind() == SyntaxKind.SubBlock)
                .Cast<MethodBlockSyntax>()
                .Select(mbs =>
                new
                {
                    MethodName = mbs.SubOrFunctionStatement.Identifier.ValueText,
                    HasGoto = VisualBasicSyntaxTree.ParseText(mbs.ToString())
                        .GetRoot()
                        .DescendantTokens()
                        .Any(st => st.Kind() == SyntaxKind.GoToKeyword)
                })
                .Where(mbs => mbs.HasGoto)
                .Select(mbs => mbs.MethodName)
            });

            foreach (var item in results)
            {
                sb.AppendLine(string.Format("Class >{0}<", item.ClassName));

                foreach (var method in item.Methods)
                {
                    sb.AppendLine(string.Format("  Method >{0}<", method.ToString()));
                }
            }

            return sb;
        }
    }
}
