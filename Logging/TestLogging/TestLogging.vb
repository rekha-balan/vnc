Option Strict On

Imports System.Reflection
Imports VNC
Public Class TestLogging
    Public Const iEventNumber As Integer = 16004

    Public Sub Log_All()
        Log_Failure()
        Log_Error()
        Log_Warning()
        Log_Info()
        Log_Debug()
        Log_Trace()

        TestExceptionHandling()
    End Sub

    Public Sub Log_Failure()
        AppLog.Failure("FailMessage with Category", "MyFailCategory")
        AppLog.Failure("FailMessage with Category with Stack", "MyFailCategory", True)
        AppLog.Failure("FailMessage", "OnTrac")
        AppLog.Failure("FailMessage", "OnTracK2")
        AppLog.Failure("FailMessage", "OnTracUI")
        AppLog.Failure("FailMessage", "OnTracWP")
        AppLog.Failure("FailMessage", "OnTracWS")
        AppLog.Failure("FailMessage", "OnTracWS", iEventNumber)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")

        AppLog.Failure("FailMessage", "OnTrac", dictionary)
        AppLog.Failure("FailMessage", "OnTrac", iEventNumber, dictionary)


    End Sub

    Public Sub Log_Error()
        AppLog.Error("ErrorMessage with Category", "MyErrorCategory")
        AppLog.Error("ErrorMessage with Category with Stack", "MyErrorCategory", True)
        AppLog.Error("ErrorMessage", "OnTrac")
        AppLog.Error("ErrorMessage", "OnTracK2")
        AppLog.Error("ErrorMessage", "OnTracUI")
        AppLog.Error("ErrorMessage", "OnTracWP")
        AppLog.Error("ErrorMessage", "OnTracWS")
        AppLog.Error("ErrorMessage", "OnTracWS", iEventNumber)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")
        AppLog.Error("ErrorMessage", "OnTrac", dictionary)
        AppLog.Error("ErrorMessage", "OnTrac", iEventNumber, dictionary)

    End Sub

    Public Sub Log_Warning()
        AppLog.Warning("WarnMessage with Category", "MyWarnCategory")
        AppLog.Warning("WarnMessage with Category with Stack", "MyWarnCategory", True)
        AppLog.Warning("WarnMessage", "OnTrac")
        AppLog.Warning("WarnMessage", "OnTracK2")
        AppLog.Warning("WarnMessage", "OnTracUI")
        AppLog.Warning("WarnMessage", "OnTracWP")
        AppLog.Warning("WarnMessage", "OnTracWS")
        AppLog.Warning("WarnMessage", "OnTracWS", iEventNumber)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")
        AppLog.Warning("WarnMessage", "OnTrac", dictionary)
        AppLog.Warning("WarnMessage", "OnTrac", iEventNumber, dictionary)

    End Sub

    Public Sub Log_Info()
        Dim startTicks As Long


        startTicks = AppLog.Info("Enter", "MyInfoCategory")

        AppLog.Info("InfoMessage with Category", "MyInfoCategory")
        AppLog.Info("InfoMessage", "OnTrac")
        AppLog.Info("InfoMessage", "OnTracK2")
        AppLog.Info("InfoMessage", "OnTracUI")
        AppLog.Info("InfoMessage", "OnTracWP")
        AppLog.Info("InfoMessage", "OnTracWS")
        AppLog.Info("InfoMessage", "OnTracWS", iEventNumber)

        AppLog.Info("Exit", "MyInfoCategory", startTicks)
        AppLog.Info("Exit", "MyInfoCategory", iEventNumber, startTicks)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")
        AppLog.Info("InfoMessage", "OnTrac", dictionary)
        AppLog.Info("InfoMessage", "OnTrac", iEventNumber, dictionary)
        AppLog.Info("InfoMessage", "OnTrac", startTicks, dictionary)
        AppLog.Info("InfoMessage", "OnTrac", iEventNumber, startTicks, dictionary)

    End Sub

    Public Sub Log_Debug()
        Dim startTicks As Long

        startTicks = AppLog.Debug("Enter", "MyDebugCategory")

        AppLog.Debug("DebugMessage with Category", "MyDebugCategory")
        AppLog.Debug("DebugMessage", "OnTrac")
        AppLog.Debug("DebugMessage", "OnTracK2")
        AppLog.Debug("DebugMessage", "OnTracUI")
        AppLog.Debug("DebugMessage", "OnTracWP")
        AppLog.Debug("DebugMessage", "OnTracWS")
        AppLog.Debug("DebugMessage", "OnTracWS", iEventNumber)

        AppLog.Debug("Exit", "MyDebugCategory", startTicks)
        AppLog.Debug("Exit", "MyDebugCategory", iEventNumber, startTicks)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")
        AppLog.Debug("DebugMessage", "OnTrac", dictionary)
        AppLog.Debug("DebugMessage", "OnTrac", iEventNumber, dictionary)
        AppLog.Debug("DebugMessage", "OnTrac", startTicks, dictionary)
        AppLog.Debug("DebugMessage", "OnTrac", iEventNumber, startTicks, dictionary)

    End Sub

    Public Sub Log_Trace()
        Dim startTicks As Long

        startTicks = AppLog.Trace("Enter", "MyTraceCategory")

        AppLog.Trace("TraceMessage with Category", "MyTraceCategory")
        AppLog.Trace("TraceMessage", "OnTrac")
        AppLog.Trace("TraceMessage", "OnTracK2")
        AppLog.Trace("TraceMessage", "OnTracUI")
        AppLog.Trace("TraceMessage", "OnTracWP")
        AppLog.Trace("TraceMessage", "OnTracWS")
        AppLog.Trace("TraceMessage", "OnTracWS", iEventNumber)

        AppLog.Trace1("Trace 1 Message", "MyTraceCategory")
        AppLog.Trace2("Trace 2 Message", "MyTraceCategory")
        AppLog.Trace3("Trace 3 Message", "MyTraceCategory")
        AppLog.Trace4("Trace 4 Message", "MyTraceCategory")
        AppLog.Trace5("Trace 5 Message", "MyTraceCategory")

        AppLog.Trace("Exit", "MyTraceCategory", startTicks)
        AppLog.Trace("Exit", "MyTraceCategory", iEventNumber, startTicks)

        'Populate Dictionary for extended properties
        Dim dictionary As New Dictionary(Of String, String)
        dictionary.Add("Application", "EAI_PS58Process")
        dictionary.Add("PolicyNumber", "VP12345678")
        dictionary.Add("WorksheetID", "35378")
        AppLog.Trace("TraceMessage", "OnTrac", dictionary)
        AppLog.Trace("TraceMessage", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace("TraceMessage", "OnTrac", startTicks, dictionary)
        AppLog.Trace("TraceMessage", "OnTrac", iEventNumber, startTicks, dictionary)

        AppLog.Trace1("Trace 1 Message", "OnTrac", dictionary)
        AppLog.Trace1("Trace 1 Message", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace1("Trace 1 Message", "OnTrac", startTicks, dictionary)
        AppLog.Trace1("Trace 1 Message", "OnTrac", iEventNumber, startTicks, dictionary)

        AppLog.Trace2("Trace 2 Message", "OnTrac", dictionary)
        AppLog.Trace2("Trace 2 Message", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace2("Trace 2 Message", "OnTrac", startTicks, dictionary)
        AppLog.Trace2("Trace 2 Message", "OnTrac", iEventNumber, startTicks, dictionary)

        AppLog.Trace3("Trace 3 Message", "OnTrac", dictionary)
        AppLog.Trace3("Trace 3 Message", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace3("Trace 3 Message", "OnTrac", startTicks, dictionary)
        AppLog.Trace3("Trace 3 Message", "OnTrac", iEventNumber, startTicks, dictionary)

        AppLog.Trace4("Trace 4 Message", "OnTrac", dictionary)
        AppLog.Trace4("Trace 4 Message", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace4("Trace 4 Message", "OnTrac", startTicks, dictionary)
        AppLog.Trace4("Trace 4 Message", "OnTrac", iEventNumber, startTicks, dictionary)

        AppLog.Trace5("Trace 5 Message", "OnTrac", dictionary)
        AppLog.Trace5("Trace 5 Message", "OnTrac", iEventNumber, dictionary)
        AppLog.Trace5("Trace 5 Message", "OnTrac", startTicks, dictionary)
        AppLog.Trace5("Trace 5 Message", "OnTrac", iEventNumber, startTicks, dictionary)

    End Sub

    Public Sub TestExceptionHandling()
        Try
            Throw (New System.Exception)
        Catch ex As Exception
            AppLog.Failure(ex, "ExceptionCategory")
            AppLog.Error(ex, "ExceptionCategory")
            AppLog.Warning(ex, "ExceptionCategory")
        End Try

    End Sub
    Public Sub TestBatchNotificationCustomListner()
        Dim i As Integer
        Try
            'Throw Exception by trying to convert string to int.
            i = Convert.ToInt32("abc")

        Catch ex As Exception

            Dim dictionary As New Dictionary(Of String, String)
            dictionary.Add("NotificationMode", "Batch")
            dictionary.Add("ApplicationName", "TestAppLogging")
            dictionary.Add("TransactionField1", "PolicyNumber:VP12345678")
            dictionary.Add("TransactionField2", "EffDt:08012011")
            dictionary.Add("DetailedDesc", ex.ToString())
            dictionary.Add("CurrentIterationCount", "1")
            dictionary.Add("MaxRetryCount", "5")
            AppLog.Error(ex.Message, "BatchNotification", dictionary)
            'C# Code sample
            'Dictionary<String, String> dictionary = new Dictionary<String, String>();
            'dictionary.Add("NotificationMode", "Batch");
            'dictionary.Add("ApplicationName", "TestAppLogging");
            'dictionary.Add("TransactionField1", "PolicyNumber:VP12345678");
            'dictionary.Add("TransactionField2", "EffDt:08012011");
            'dictionary.Add("DetailedDesc", ex.ToString());
            'dictionary.Add("CurrentIterationCount", "1");
            'dictionary.Add("MaxRetryCount", "5");
            'PacificLife.Life.AppLog.Error(ex.Message, "BatchNotification", dictionary);
        End Try
    End Sub
End Class
