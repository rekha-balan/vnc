Option Strict Off
Module Module1

    Sub Main()
        Dim tree As SyntaxTree = VisualBasicSyntaxTree.ParseText(
"Imports System
Imports System.Collections
Imports System.Linq
Imports System.Text
Imports Microsoft.CodeAnalysis
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic

Namespace HelloWorld
    Module Program
        Sub Main(args As String())
            Console.WriteLine(""Hello World!"")
        End Sub
    End Module
End Namespace")

        Dim root As Syntax.CompilationUnitSyntax = tree.GetRoot()

        Dim visitor As New ImportsCollector()
        visitor.Visit(root)

        For Each statement In visitor.Imports
            Console.WriteLine(statement)
        Next
    End Sub

End Module
