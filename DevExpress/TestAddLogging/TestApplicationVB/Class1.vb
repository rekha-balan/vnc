Public Class Class1

    Private Sub bar()
    End Sub

    Private Sub bar1()
        
    End Sub

    Private Sub bar2()
        Dim varName As String
    End Sub

    Private Sub bar3(ByVal paramName As String, 
                     ByVal param2 As String)        
    End Sub
    Private Sub bar4(ByVal paramName As String, 
                     ByVal param2 As String)
              
    End Sub

    Private Sub bar5(ByVal paramName As String, 
                     ByVal param2 As String)
        Dim i As Integer
        Dim s As String
    End Sub

    Private Function foo1() As Integer
        Return 1
    End Function

    Private Function foo2() As Integer
        
        Return 1
    End Function

    Private Function foo3() As Integer
        If true Then Return 1
    End Function

    Private Function foo4() As Integer
                If true Then Return 1

        Return 2
    End Function

    Private Function foo5() As Integer

        Try

            If true Then Return 1

            Return 2
        Catch ex As Exception

        End Try
    End Function

    Private Function foo6() As Integer

        Dim i As Int16
        i = 7

    End Function

    Private Function foo7() As Integer

        foo7 = 1

    End Function
End Class
