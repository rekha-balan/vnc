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
namespace VNC.CodeAnalysis.PerformanceMetrics.VB
{
    class AvoidBoxing
    {
        static StringBuilder Check(string sourceCode)
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

                    return sb;
            }
    }
}
