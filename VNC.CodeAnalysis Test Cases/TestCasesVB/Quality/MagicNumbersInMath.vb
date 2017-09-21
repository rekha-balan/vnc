Public Class MagicNumbersInMath
    Public Function fun(g As Integer) As Single
        Dim size As Integer = 10000
        g += 23456
        g += size
        Return CSng((g / 10))
    End Function

    Public Function updateRate(rate As Decimal) As Decimal
        Return rate / 0.2345D
    End Function

    Public Function updateRateM(rateM As Decimal) As Decimal
        Dim basis As Decimal = New Decimal(2345, 0, 0, False, 4)
        Return rateM / basis
    End Function
End Class
