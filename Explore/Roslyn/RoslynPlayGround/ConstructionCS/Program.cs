using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
//using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ConstructionCS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new SyntaxNode using the CSharp.SyntaxFactory
            // System.Collections.Generic

            // NB. By uncommenting the using static Microsoft...SyntaxFactory the SyntaxFactory
            // qualifier can be removed.  Leaving in to help convey it is the factory that is used.

            NameSyntax name = SyntaxFactory.IdentifierName("System");
            name = SyntaxFactory.QualifiedName(name, SyntaxFactory.IdentifierName("Collections"));
            name = SyntaxFactory.QualifiedName(name, SyntaxFactory.IdentifierName("Generic"));

            // This is the original code

            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"
using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}");

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var oldUsing = root.Usings[1];  // system.Collections
            var newUsing = oldUsing.WithName(name); // System.Collections.Generic

            root = root.ReplaceNode(oldUsing, newUsing);

            // Need to save changes to make them persist...
        }
    }
}
