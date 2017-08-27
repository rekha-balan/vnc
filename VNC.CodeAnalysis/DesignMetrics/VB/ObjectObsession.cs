using System;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using CS = Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace VNC.CodeAnalysis.DesignMetrics.VB
{
    public class ObjectObsession
    {
        public static StringBuilder Check(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

//            var tree = CSharpSyntaxTree.ParseText(code);
//            var result = tree.GetRoot()
//            .DescendantNodes()
//            .OfType<MethodDeclarationSyntax>()//#1
//            .Where(thisMethod => //#2
//                                 //This method is not an event declaration
//            !thisMethod.IsEventDeclaration()
//            && thisMethod.TakesOrReturnsObject())
//            .Select(thisMethod =>
//            thisMethod.Identifier.ValueText); //#3

//            if (result.Count() > 0)
//            {
//                result.Dump(@"Methods that aren't event handlers but takes or
//returns objects");
//            }

            return sb;
        }
    }

    public static class MethoDeclEx
    {
        //public static bool IsEventDeclaration
        //(this MethodDeclarationSyntax mds)
        //{
        //    return mds.ParameterList
        //    .Parameters
        //    .Any(p =>
        //    p.Type.ToFullString().EndsWith("EventArgs"));
        //}
        //public static bool TakesOrReturnsObject
        //(this MethodDeclarationSyntax mds)
        //{
        //    return mds.ParameterList
        //    .Parameters
        //    .Any(p =>
        //    //If any parameter is of type object
        //    p.Type.ToFullString().ToLower()
        //    .Trim() == "object")
        //    //if return type is of type object
        //    || mds.ReturnType.ToFullString()
        //    .ToLower().Trim() == "object";
        //}
    }
}
