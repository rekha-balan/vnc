Public Class MethodsNotUsingAllParameters
    Public Function fun(x As Integer, z As Integer) As Integer
        x += 1
        Return x + 1
    End Function

    Public Function funny(x As Double) As Double
        Return x / 2.13
    End Function
End Class
