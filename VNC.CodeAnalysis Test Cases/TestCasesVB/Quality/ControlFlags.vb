Public Class ControlFlags
    Private cflag As Boolean = False

    Public Sub update()
        Dim flag As Boolean = Not Me.cflag
        If flag Then
            Dim flag2 As Boolean = Me.thatThing()
            If flag2 Then
                Me.cflag = True
            End If
        Else
            Me.thatOtherThing()
        End If
    End Sub

    Private Sub thatOtherThing()
        Dim flag As Boolean = False
        Dim flag2 As Boolean = flag
        If flag2 Then
        End If
    End Sub

    Private Function thatThing() As Boolean
        Return True
    End Function

End Class
