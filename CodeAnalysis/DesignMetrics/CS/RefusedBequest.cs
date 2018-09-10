using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.DesignMetrics.CS
{
    public class RefusedBequest
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            //            var tree = CSharpSyntaxTree.ParseText(code);
            //            var interfaces = tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<InterfaceDeclarationSyntax>()
            //            .Select(ids =>
            //           new
            //            {
            //                InterfaceName = ids.Identifier.ValueText,
            //                MethodNames = ids.Members
            //           .OfType<MethodDeclarationSyntax>()
            //           .Select(mds => mds.Identifier.ValueText)
            //            })
            //            .ToLookup(ids => ids.InterfaceName,
            //            ids => ids.MethodNames)
            //            .ToDictionary(ids => ids.Key,
            //            ids => ids.SelectMany(i => i)
            //            .ToList());//#1
            //            string[] notImpl = { "throw", "new", "NotImplementedException" };
            //            var result = tree.GetRoot()
            //            .DescendantNodes()
            //            .OfType<ClassDeclarationSyntax>()
            //            .Select(cds =>
            //           new {
            //                ClassName = cds.Identifier.ValueText,//#2
            //                                                     //get rid of the initial colon
            //               DerivingFrom = cds.BaseList.GetText()//#3
            //.ToString()
            //.Substring(1)
            //.Split(new char[] { ',' },
            //StringSplitOptions.RemoveEmptyEntries),
            //               Methods = cds.Members
            //.OfType<MethodDeclarationSyntax>()
            //.Where(mds => mds.Body.Statements.Count == 1)//#4
            //.Select(mds =>
            //new {
            //   MethodName = mds.Identifier.ValueText,
            //   BelongsTo = interfaces.FirstOrDefault //#5
            //(i => i.Value.Contains(mds.Identifier.ValueText)),
            //   NotImple = mds.Body.Statements[0].
            //ToFullString()
            //.Trim()
            //.Split(new char[] { ' ', '(' },
            //StringSplitOptions.RemoveEmptyEntries)
            //.Take(3)
            //.SequenceEqual(new string[] { "throw", "new", "NotImplementedException" }
            //)//#6
            //})
            //.Where(mds => mds.BelongsTo.Key != null
            //&& mds.NotImple)
            //.Select(mds =>
            //new {
            //   MethodName = mds.MethodName,
            //   BelongsTo = mds.BelongsTo.Key,
            //   NotImple = mds.NotImple
            //})
            //           })
            //.Where(cds => cds.Methods.Count() > 0);
            //            result
            //            .SelectMany(r => r.Methods
            //           .Select(m => new
            //          KeyValuePair<string, string>
            //          (
            //          r.ClassName,
            //          m.BelongsTo
            //          )))
            //            .ToLookup(r => r.Value, r => r.Key)
            //            .ToDictionary(r => r.Key, r => r
            //           .Distinct()
            //           .ToList())
            //            //#7
            //            .Select(r => new {
            //                InterfaceName = r.Key,
            //                ClassNames = r.Value
            //            })
            //            .Dump("Probable Refused Bequest Relationships");

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");
            return sb;
        }
    }
}
