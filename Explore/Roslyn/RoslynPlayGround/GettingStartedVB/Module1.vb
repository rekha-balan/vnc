Option Strict Off

Module Module1

    Sub Main()
        Dim tree As SyntaxTree = VisualBasicSyntaxTree.ParseText(
"Imports System
Imports System.Collections
Imports System.Linq
Imports System.Text

Namespace HelloWorld
    Module Program
        Sub Main(args As String())
            Console.WriteLine(""Hello World!"")
        End Sub
    End Module
End Namespace")

        Dim root As Syntax.CompilationUnitSyntax = tree.GetRoot()

        Dim firstMember = root.Members(0)

        Dim helloWorldDeclaration As Syntax.NamespaceBlockSyntax = firstMember

        Dim programDeclaration As Syntax.ModuleBlockSyntax = helloWorldDeclaration.Members(0)

        Dim mainDeclaration As Syntax.MethodBlockSyntax = programDeclaration.Members(0)

        Dim argsParameter As Syntax.ParameterSyntax = mainDeclaration.BlockStatement.ParameterList.Parameters(0)

        Dim firstParameters = From methodStatement In root.DescendantNodes().
                                  OfType(Of Syntax.MethodStatementSyntax)()
                              Where methodStatement.Identifier.ValueText = "Main"
                              Select methodStatement.ParameterList.Parameters.First()

        Dim argsParameter2 = firstParameters.First()



    End Sub

End Module
