Option Strict Off

Module Module1

    Sub Main()
        Dim tree = VisualBasicSyntaxTree.ParseText(
"Imports System
Imports System.Collections.Generic
Imports System.Text
 
Namespace HelloWorld
    Class Class1
        Shared Sub Main(args As String())
            Console.WriteLine(""Hello, World!"")
        End Sub
    End Class
End Namespace")

        Dim root As CompilationUnitSyntax = tree.GetRoot()

        Dim compilation As Compilation =
                        VisualBasicCompilation.Create("HelloWorld").
                                               AddReferences(MetadataReference.CreateFromFile(GetType(Object).Assembly.Location)).
                                               AddSyntaxTrees(tree)

        ' Binding a name

        Dim model = compilation.GetSemanticModel(tree)

        Dim firstImport As SimpleImportsClauseSyntax = root.Imports(0).ImportsClauses(0)

        Dim nameInfo = model.GetSymbolInfo(firstImport.Name)

        Dim systemSymbol As INamespaceSymbol = nameInfo.Symbol

        For Each ns In systemSymbol.GetNamespaceMembers()
            Console.WriteLine(ns.Name)
        Next

        Dim helloWorldString = root.DescendantNodes().
            OfType(Of LiteralExpressionSyntax)().First()

        Dim literalInfo = model.GetTypeInfo(helloWorldString)

        Dim stringTypeSymbol As INamedTypeSymbol = literalInfo.Type

        Dim methodNames = From method In stringTypeSymbol.GetMembers().
                                                          OfType(Of IMethodSymbol)()
                          Where method.ReturnType.Equals(stringTypeSymbol) AndAlso
                                method.DeclaredAccessibility = Accessibility.Public
                          Select method.Name Distinct

        Console.Clear()

        For Each name In methodNames
            Console.WriteLine(name)
        Next

    End Sub

End Module
