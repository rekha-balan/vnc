Option Strict Off

Public Class ImportsCollector
    Inherits VisualBasicSyntaxWalker

    Public ReadOnly [Imports] As New List(Of Syntax.ImportsStatementSyntax)()

    Public Overrides Sub VisitSimpleImportsClause(node As SimpleImportsClauseSyntax)
        If node.Name.ToString() = "System" OrElse
            node.Name.ToString().StartsWith("System.") Then Return

        [Imports].Add(node.Parent)
    End Sub
End Class
