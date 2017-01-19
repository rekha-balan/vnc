Module Module2
    Private Sub bar()
    End Sub

    Private Sub bar1()

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

    

    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
    
    End Sub

    Private Sub bar2()

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        Dim varName As String

    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If

    End Sub

    Private Sub bar3(ByVal paramName As String, 
                     ByVal param2 As String)        
    End Sub
    Private Sub bar4(ByVal paramName As String, 
                     ByVal param2 As String)

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

    

    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
          
    End Sub

    Private Sub bar5(ByVal paramName As String, 
                     ByVal param2 As String)

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        Dim i As Integer
        Dim s As String

    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If

    End Sub

    Private Function foo1() As Integer

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If



    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
        Return 1
    End Function

    Private Function foo2() As Integer
        

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If



    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
        Return 1
    End Function

    Private Function foo3() As Integer

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If



    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
        If true Then Return 1
    End Function

    Private Function foo4() As Integer

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        If true Then Return 1



    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
        Return 2
    End Function

    Private Function foo5() As Integer


    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        Try



    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
            If true Then Return 1

            Return 2
        Catch ex As Exception
            
        End Try
    End Function

    Private Function foo6() As Integer


    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        Dim i As Int16
        i = 7


    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If

    End Function

    Private Function foo7() As Integer


    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        foo7 = 1


    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If

    End Function
	

    #If TRACE
        Dim startTicks As Long = Log.Trace9("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

    Private Sub SearchForPart()     'called from frmoposumm (txtpartno, keypress events)
        Ec.PartSearch.ClearPlanSearchObject(fObjPlanSearch)
        txtPartNo.Text = gAN
        fObjPlanSearch.RecType = "0"        'dont change this, may affect plan rename
        fObjPlanSearch.PartNo = gAN
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        RefreshGrid()
        Windows.Forms.Cursor.Current = Cursors.Default

    #If TRACE
        Log.Trace9("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If

    End Sub

End Module
