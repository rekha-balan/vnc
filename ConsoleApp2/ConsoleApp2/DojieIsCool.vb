Public Class DojieIsCool

    Private _prop1 As String
    Property Prop1 As String
        Get
            Return _prop1
        End Get
        Set(ByVal Value As String)
            _prop1 = Value
        End Set
    End Property


    Private _prop2 As String = ""
    Property Prop2 As String
        Get
            Return _prop2
        End Get
        Set(ByVal Value As String)
            _prop2 = Value
        End Set
    End Property

End Class
