Public Class Security
    Public Function DecodeConnectionString(ByVal strCS As String) As String

        'decode the connection string
        Dim intI As Int16 = 0, intJ As Int16 = 0
        Dim strTemp As String = ""
        strCS = Trim(strCS)
        If InStr(strCS.ToLower, ".mdb") > 0 Then GoTo ExitThisFunction

        'first reverse the string
        For intI = Len(strCS) To 1 Step -1
            strTemp &= Mid(strCS, intI, 1)
        Next
        'strTemp = strCS
        intJ = 1 : strCS = ""
        For intI = 1 To Len(strTemp)
            'If intI Mod 2 = 0 Then intJ = 1
            strCS = strCS & Chr(Asc(Mid(strTemp, intI, 1)) - intJ)

            intJ += 1
            If intJ Mod 4 = 0 Then intJ = 1
            'If intJ = 1 Then
            '    intJ = 2
            'Else
            '    intJ = 1
            'End If
            'strTemp &= Mid(strCS, intI, 1)
            'If intI Mod 10 = 0 Then intJ = 1
            'strTemp &= Chr((Asc(Mid(strCS, intI, 1)) + intJ))
            'intJ = intJ + 1
        Next

        'For intI = Len(strCS) To 1 Step -1
        '    strTemp &= Mid(strCS, intI, 1)
        'Next

        'intJ = 1 : strCS = ""
        'For intI = 1 To Len(strTemp)
        '    If intI Mod 10 = 0 Then intJ = 1
        '    strCS &= Chr((Asc(Mid(strTemp, intI, 1)) - intJ))
        '    intJ = intJ + 1
        'Next
ExitThisFunction:

        Return strCS.Trim
    End Function
    Public Function EncryptConnectionString(ByVal strCS As String) As String

        'encode the connection string
        Dim strTemp As String = "", intI As Int16 = 0, intJ As Int16 = 0

        strCS = Trim(strCS)
        If InStr(strCS.ToLower, ".mdb") > 0 Then GoTo ExitThisFunction
        strTemp = ""
        intJ = 1
        For intI = 1 To Len(strCS) 'To 1 Step -1
            strTemp = strTemp & Chr(Asc(Mid(strCS, intI, 1)) + intJ)
            ' strTemp = strTemp & Asc(Mid(strCS, intI, 1))

            'If intJ = 1 Then
            '    intJ = 2
            'Else
            '    intJ = 1
            'End If
            intJ += 1
            If intJ Mod 4 = 0 Then intJ = 1
        Next

        'now reverse it
        strCS = ""
        For intI = Len(strTemp) To 1 Step -1
            strCS &= Mid(strTemp, intI, 1)
        Next
ExitThisFunction:

        Return strCS.Trim
    End Function
End Class
