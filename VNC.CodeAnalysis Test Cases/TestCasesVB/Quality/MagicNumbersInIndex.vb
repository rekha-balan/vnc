Public Class MagicNumbersInIndex
    Public Function fun() As String
        Dim dic As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
        Dim x As Integer = 323
        Return dic(x) + x + dic(323)
    End Function

    Public Function funny() As Single
        Dim d As Integer = 234
        Dim dic As Dictionary(Of Integer, Single) = New Dictionary(Of Integer, Single)()
        Return dic(d)
    End Function
End Class
