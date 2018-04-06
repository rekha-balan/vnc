Public Class MagicNumbersInConditions
    Private x As Integer = 8

    Public Function IsGood(password As String) As Boolean
        Dim flag As Boolean = password.Length < 5
        Return Not flag AndAlso password.Length >= 7
    End Function

    Public Function fun() As Integer
        Dim g As Integer() = Nothing
        Return g(Me.x)
    End Function

    Public Function zun() As Boolean
        Dim z As Single = 0F
        Dim flag As Boolean = CDec(z) > 3.4
        Return Not flag
    End Function
End Class
