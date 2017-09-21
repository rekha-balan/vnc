Public Class LadderIfStatements
    Public Function funnyBad() As Integer
        Dim flag As Boolean = Me.c1() = 1
        Dim result As Integer
        If flag Then
            result = 1
        Else
            Dim flag2 As Boolean = Me.c1() = 2
            If flag2 Then
                result = 2
            Else
                Dim flag3 As Boolean = Me.c1() = 3
                If flag3 Then
                    result = 3
                Else
                    Dim flag4 As Boolean = Me.c1() = 4
                    If flag4 Then
                        result = 4
                    Else
                        Dim flag5 As Boolean = Me.c2() = 23
                        If flag5 Then
                            result = 23
                        Else
                            Dim flag6 As Boolean = Me.c3() = 24
                            If flag6 Then
                                result = 24
                            Else
                                result = 9
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return result
    End Function

    Public Sub funnyGood()
        Dim flag As Boolean = Me.c1() = 3
        If flag Then
            Me.f1()
        End If
        Dim flag2 As Boolean = Me.c2() = 34
        If flag2 Then
            Me.f1()
        End If
    End Sub

    Private Function c1() As Integer
        Return 1
    End Function

    Private Function c2() As Integer
        Return 1
    End Function

    Private Function c3() As Integer
        Return 1
    End Function

    Private Sub f1()
    End Sub

End Class
