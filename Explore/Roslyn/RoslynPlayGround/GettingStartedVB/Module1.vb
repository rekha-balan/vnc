Option Strict Off

Module Module1

    Sub Main()
        ' Can create SyntaxNodes using Create method
        ' CSharpSyntaxTree.Create()

        ' Or parse source code
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

        ' CompilationUnitSyntax
        ' SyntaxKind
        Dim root As Syntax.CompilationUnitSyntax = tree.GetRoot()

        ' SyntaxList<StatementSyntax>
        ' SyntaxKind
        Dim firstMember = root.Members(0)

        ' NamespaceBlockSyntax
        ' SyntaxKind
        Dim helloWorldDeclaration As Syntax.NamespaceBlockSyntax = firstMember

        ' ModuleBlockSyntax
        ' SyntaxKind
        Dim programDeclaration As Syntax.ModuleBlockSyntax = helloWorldDeclaration.Members(0)

        ' MethodBlockSyntax
        ' SyntaxKind
        Dim mainDeclaration As Syntax.MethodBlockSyntax = programDeclaration.Members(0)

        ' SeparatedSyntaxList<ParameterSyntax>
        ' SyntaxKind
        Dim argsParameter As Syntax.ParameterSyntax = mainDeclaration.BlockStatement.ParameterList.Parameters(0)

        ' IEnumerable<ParameterSyntax>
        ' SyntaxKind
        Dim firstParameters = From methodStatement In root.DescendantNodes().
                                  OfType(Of Syntax.MethodStatementSyntax)()
                              Where methodStatement.Identifier.ValueText = "Main"
                              Select methodStatement.ParameterList.Parameters.First()

        ' ParameterSyntax
        ' SyntaxKind
        Dim argsParameter2 = firstParameters.First()

    End Sub

End Module
