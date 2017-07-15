using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    public static class SimpleGenerator
    {
        public static SyntaxTree CreateEmptyClass(string className)
        {
            var code = @"
 public class Class1
 {
 }
";
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var identifierToken = syntaxTree.GetRoot().DescendantTokens()
                .First(t => t.IsKind(SyntaxKind.IdentifierToken)
                            && t.Parent.Kind() == SyntaxKind.ClassDeclaration);
            var newIdentifier = SyntaxFactory.Identifier(className);

            return SyntaxFactory.SyntaxTree(syntaxTree.GetRoot().ReplaceToken(identifierToken, newIdentifier));
        }

        public static ClassDeclarationSyntax AddProperty(this ClassDeclarationSyntax currentClass, string name, INamedTypeSymbol type)
        {
            if (currentClass.DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Any(p => p.Identifier.Text == name))
            {
                // Class already has the specified property (at least by name)
                return currentClass;
            }

            // Consider how many properties the class has.  Maybe too many to be practical.

            if (currentClass.DescendantNodes().OfType<PropertyDeclarationSyntax>().Count() > 128)
            {
                throw new Exception("Class already has 128 properties.  Do you really want more !");
            }

            var typeSyntax = SyntaxFactory.ParseTypeName(type.Name);

            var newProperty = SyntaxFactory.PropertyDeclaration(typeSyntax, name)
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(new[]
                {
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                })));

            return currentClass.AddMembers(newProperty);
        }
    }
}
