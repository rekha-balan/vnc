Public Class Class1

    Public Structure MyStruct1
        Public i As Int16
        Public j As Int32
        Public s1 As String
        Public s2 As String
    End Structure

    Public Structure MyStruct2
        Public i As Int16
        Public j As Int32
        <VBFixedString(10)> Public s1 As String
        <VBFixedString(20)> Public s2 As String
    End Structure

End Class
