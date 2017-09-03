using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxCS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Can create SyntaxNodes using Create method
            //CSharpSyntaxTree.Create()

            // Or parse source code

            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"using System;
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

            // CompilationUnitSyntax
            // SyntaxKind
            var root = (CompilationUnitSyntax)tree.GetRoot();

            // SyntaxList<MemberDeclarationSyntax>
            // SyntaxKind
            var firstMember = root.Members[0];

            // NamespaceDeclarationSyntax
            // SyntaxKind = 
            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;


            // ClassDeclarationSyntax
            // SyntaxKind = 
            var programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

            // MethodDeclarationSyntax
            // SyntaxKind = 
            var mainDeclaration = (MethodDeclarationSyntax)programDeclaration.Members[0];

            // SeparatedSyntaxList<ParameterSyntax>
            // SyntaxKind = 
            var argsParameter = mainDeclaration.ParameterList.Parameters[0];

            // IEnumerable<ParameterSyntax>
            // SyntaxKind
            var firstParameters = from methodDeclaration
                                  in root.DescendantNodes().OfType<MethodDeclarationSyntax>()
                                  where methodDeclaration.Identifier.ValueText == "Main"
                                  select methodDeclaration.ParameterList.Parameters.First();

            // ParameterSyntax
            // SyntaxKind = 
            var argsParameter2 = firstParameters.Single();
        }
    }
}
