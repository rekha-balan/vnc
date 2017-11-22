Public Class TestClass
    Public Function maybe_do_something(something As Integer, somethingelse As Integer, etc As Integer) As Integer
        Dim flag As Boolean = something <> -1
        Dim result As Integer
        If flag Then
            result = 0
        Else
            Dim flag2 As Boolean = somethingelse <> -1
            If flag2 Then
                result = 0
            Else
                Dim flag3 As Boolean = etc <> -1
                If flag3 Then
                    result = 0
                Else
                    result = 1
                End If
            End If
        End If
        Return result
    End Function

    Public Function otherFun(bailIfIEqualZero As Integer, shouldNeverBeEmpty As String) As Integer
        Dim flag As Boolean = bailIfIEqualZero = 0
        Dim result As Integer
        If flag Then
            result = 0
        Else
            Dim flag2 As Boolean = String.IsNullOrEmpty(shouldNeverBeEmpty)
            If flag2 Then
                result = 0
            Else
                result = 1
            End If
        End If
        Return result
    End Function

End Class
