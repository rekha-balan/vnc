Option Strict Off

Imports Microsoft.CodeAnalysis.VisualBasic.SyntaxFactory
Module Module1

    Sub Main()
        Dim name As NameSyntax = IdentifierName("System")
        name = QualifiedName(name, IdentifierName("Collections"))
        name = QualifiedName(name, IdentifierName("Generic"))
    End Sub

End Module
