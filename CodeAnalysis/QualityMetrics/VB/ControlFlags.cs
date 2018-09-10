
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using System.Linq;
using System.Text;

namespace VNC.CodeAnalysis.QualityMetrics.VB
{
    public class ControlFlags
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            var tree = VisualBasicSyntaxTree.ParseText(sourceCode);

            // Find all boolean variables in the class
            // Find all if statements that solely rely on those
            // Find the methods in which these if statements are

            // var bools = tree.GetRoot().DescendantNodes()
            //     .Where(t => t.Kind() == SyntaxKind.VariableDeclaration
            //              && t.ToFullString().Trim().StartsWith("bool"))
            //            .Select(t =>
            //              new
            //              {
            //                VariableName = t.ToFullString().Trim().Split(' ')[1],
            //                Class = t.Ancestors()
            //                  .Where
            //                      (x => x.Kind() == SyntaxKind.ClassDeclaration)
            //                       .Cast<ClassDeclarationSyntax>()
            //                      .First().Identifier.ValueText
            //              })
            //              .Select(t => t.VariableName);//#1

            //            tree.GetRoot().DescendantNodes()
            //            .Where(t => t.Kind == SyntaxKind.IfStatement)
            //            .Cast<IfStatementSyntax>()
            //            .Select(ifs =>
            //           new
            //            {
            //                If = ifs.ToFullString(), //#2
            //    Condition = ifs.Condition.ToFullString(),//#3
            //    Line = ifs.SyntaxTree
            //           .GetLineSpan(ifs.FullSpan, false)
            //           .StartLinePosition.Line + 1//#4
            //})
            //            .Where(ifs => ifs.Condition.Split(new string[]{"&&", "||", "==","(",")","
            //"},StringSplitOptions.RemoveEmptyEntries)
            ////If a control flag is used,
            //.Any (c => bools.Contains(c) ||
            ////it can also be used in a negative way. skipping "!"
            //bools.Contains(c.Substring(1))))//#5
            //           .Dump("if nodes with control flags");

            // Find all the variable declarations



            var bools = tree.GetRoot().DescendantNodes()
                .Where(syn => syn.IsKind(SyntaxKind.VariableDeclarator)
                && ((VariableDeclaratorSyntax)syn).AsClause.Type().ToString() == "Boolean")
                .Select(t =>
                new
                {
                    VariableName = ((VariableDeclaratorSyntax)t).Names[0].ToString(),
                    Class = t.Ancestors()
                    .Where(x => x.IsKind(SyntaxKind.ClassBlock))
                    .Cast<ClassBlockSyntax>().First()
                }).Select(t => t.VariableName);

            var ifconds = tree.GetRoot().DescendantNodes()
                .Where(syn => syn.IsKind(SyntaxKind.IfStatement))
                .Cast<IfStatementSyntax>()
                .Select(ifs =>
                new
                {
                    If = ifs.ToFullString(),
                    Condition = ifs.Condition.ToFullString(),
                    Line = ifs.SyntaxTree.GetLineSpan(ifs.FullSpan).StartLinePosition.Line + 1
                });


            //.Where(ifs => ifs.Condition.Split(new string[] { "&&", "||", "==", "(", ")", "" }, StringSplitOptions.RemoveEmptyEntries)
            //.Any(c => bools.Contains(c) || bools.Contains(c.Substring(1)))));

            foreach (var item in bools)
            {
                var s = item.ToString();
            }
            //foreach (var item in bools)
            //{
            //    var c = item.Class;
            //    var v = item.VariableName;
            //}

            foreach (var item in ifconds)
            {
                var i = item.If;
                var c = item.Condition;
                var l = item.Line;
            }

            //var bools = tree.GetRoot().DescendantNodes()
            //    .Where(syn => syn.IsKind(SyntaxKind.VariableDeclarator) && ((VariableDeclaratorSyntax)syn).AsClause.Type().IsKind(SyntaxKind.BooleanKeyword))
            //    .Cast<VariableDeclaratorSyntax>();

            //var bools = tree.GetRoot().DescendantNodes()
            //    .Where(syn => syn.IsKind(SyntaxKind.VariableDeclarator)
            //        && ((PredefinedTypeSyntax) ((VariableDeclaratorSyntax)syn).AsClause.Type()).Keyword.Text == "Boolean")
            //    .Cast<VariableDeclaratorSyntax>();

            //.Cast<VariableDeclaratorSyntax>();

            //var bools = tree.GetRoot().DescendantNodes()
            //    .Where(syn => syn.IsKind(SyntaxKind.VariableDeclarator)
            //        && ((PredefinedTypeSyntax)(((VariableDeclaratorSyntax)syn).AsClause.Type())).Keyword.Text == "Boolean");

            //var bools = tree.GetRoot().DescendantNodes()
            //    .Where(syn => syn.IsKind(SyntaxKind.VariableDeclarator))
            //    .Cast<VariableDeclaratorSyntax>()
            //    .Where(ac => ((PredefinedTypeSyntax)ac.AsClause.Type()).Keyword.Text == "Boolean");

            //    .Cast<VariableDeclaratorSyntax>();

            //foreach (var item in bools)
            //{
            //    var t = item.AsClause.Type();
            //    var t1 = t as IdentifierNameSyntax;
            //    var t2 = t as SimpleNameSyntax;
            //    var t3 = t as PredefinedTypeSyntax;

            //    var x = t3.Keyword.Text;

            //    var k = item.AsClause.Kind();

            //    sb.AppendLine(item.ToString());
            //}

            //var bools2 = bools.Where(syn => ((PredefinedTypeSyntax)syn.AsClause.Type()).Keyword.Text == "Boolean");


            //foreach (VariableDeclaratorSyntax item in bools)
            //{
            //    var t0 = item.AsClause;
            //    var t = item.AsClause.Type();
            //    var t1 = t as IdentifierNameSyntax;
            //    var t2 = t as SimpleNameSyntax;
            //    var t3 = t as PredefinedTypeSyntax;

            //    var i0 = ((SimpleAsClauseSyntax)t0).Type;
            //    var i1 = i0.ToString();

            //    //var x = t3.Keyword.Text;

            //    //var k = item.AsClause.Kind();

            //    sb.AppendLine(item.ToString());
            //}

            //foreach (var item in bools2)
            //{
            //    var t = item.AsClause;
            //}

            //sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");

            return sb;
        }
    }
}
