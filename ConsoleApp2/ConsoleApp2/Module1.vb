Module Module1

    Sub Main()
        Dim ohMy = New DojieIsCool()

        Dim s1 = ohMy.Prop1
        Dim s2 = ohMy.Prop2

        Dim s1Answer As String = If(s1 Is Nothing, "true", "false")
        Dim s2Answer As String = If(s2 Is Nothing, "true", "false")

        Console.WriteLine("Not sure about his code")
        Console.WriteLine(String.Format("s1 is null? {0}", s1Answer))
        Console.WriteLine(String.Format("s2 is null? {0}", s2Answer))
        Console.ReadLine()

    End Sub

End Module
