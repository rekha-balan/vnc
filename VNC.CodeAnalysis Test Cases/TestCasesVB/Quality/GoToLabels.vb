Public Class GoToLabels
    Public Shared Sub message()
    End Sub

    Public Sub gotoFun()
        Dim array As Integer(,) = Nothing
        For i As Integer = 0 To 2 - 1
            For j As Integer = 0 To 9 - 1
                Dim flag As Boolean = array(i, j).Equals(3)
                If flag Then
                    GoTo IL_41
                End If
            Next
        Next
IL_41:
        GoToLabels.chump()
    End Sub

    Private Shared Sub chump()
        Dim i As Integer = 0
        Dim cost As Integer = 0
        Select Case i
            Case 1
            Case 2
                cost += 25
            Case 3
                cost += 50
            Case Else
                Return
        End Select
        cost += 25
    End Sub

End Class
