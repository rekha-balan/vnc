Public Class MultipleReturnStatements
    Public Function fun(x As Integer) As Integer
        x += 1
        Dim flag As Boolean = x = 0
        Dim result As Integer
        If flag Then
            result = x
        Else
            result = x + 2
        End If
        Return result
    End Function

    Public Function funny3(x As Integer) As Double
        Return CDec((x / 12))
    End Function

End Class
