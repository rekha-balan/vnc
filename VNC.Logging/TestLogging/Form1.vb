Imports System.Reflection
Imports VNC
Public Class Form1


    Private Sub btnTimeLogging_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeLogging.Click
        Dim frequency As Double
        Dim startTime_LogTraceE, startTime_LogTrace, startTime_LogInfo, startTime_LogInfoStack, startTime_Write, startTime_WriteLight As Long
        Dim endTime_LogTraceE, endTime_LogTrace, endTime_LogInfo, endTime_LogInfoStack, endTime_Write, endTime_WriteLight As Long
        Dim elapsedTime_LogTraceE, elapsedTime_LogTrace, elapsedTime_LogInfo, elapsedTime_LogInfoStack, elapsedTime_Write, elapsedTime_WriteLight As Double

        frequency = System.Diagnostics.Stopwatch.Frequency

        ' Time different events.

        ' Time around Log.Info method

        startTime_LogInfo = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.Info("TimeInfo", "General")
        Next

        endTime_LogInfo = Stopwatch.GetTimestamp
        elapsedTime_LogInfo = ((endTime_LogInfo - startTime_LogInfo) / 100.0) / frequency

        ' Time around Log.Info + Stack method

        startTime_LogInfoStack = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.Info("TimeInfo", "General", AppLog.ShowStack.Yes)
        Next

        endTime_LogInfoStack = Stopwatch.GetTimestamp
        elapsedTime_LogInfoStack = ((endTime_LogInfoStack - startTime_LogInfoStack) / 100.0) / frequency

        ' Time around Write method

        startTime_Write = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.Write("TimeWrite", TraceEventType.Information, "General", 1000, "ClasesName", "MethodName", False, startTime_Write)
        Next

        endTime_Write = Stopwatch.GetTimestamp
        elapsedTime_Write = ((endTime_Write - startTime_Write) / 100) / frequency

        ' Time around WriteLight method

        startTime_WriteLight = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.WriteLight("TimeWriteLight", TraceEventType.Information, "General", 1000, "ClasesName", "MethodName", False, startTime_WriteLight)
        Next

        endTime_WriteLight = Stopwatch.GetTimestamp
        elapsedTime_WriteLight = ((endTime_WriteLight - startTime_WriteLight) / 100) / frequency

        ' Time around Log.Trace method

        startTime_LogTrace = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.Trace("TimeTrace", "General")
        Next

        endTime_LogTrace = Stopwatch.GetTimestamp
        elapsedTime_LogTrace = ((endTime_LogTrace - startTime_LogTrace) / 100) / frequency

        ' Time around Log.TraceE method

        startTime_LogTraceE = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            AppLog.Trace("TimeTrace", "General", startTime_LogTraceE)
        Next

        endTime_LogTraceE = Stopwatch.GetTimestamp
        elapsedTime_LogTraceE = ((endTime_LogTraceE - startTime_LogTraceE) / 100) / frequency

        ' Now update the form to reflect the results

        Me.txtElapsedTime_LogInfo.Text = elapsedTime_LogInfo
        Me.txtElapsedTime_LogInfoStack.Text = elapsedTime_LogInfoStack
        Me.txtElapsedTime_LogWrite.Text = elapsedTime_Write
        Me.txtElapsedTime_LogWriteLight.Text = elapsedTime_WriteLight
        Me.txtElapsedTime_LogTrace.Text = elapsedTime_LogTrace
        Me.txtElapsedTime_LogTraceE.Text = elapsedTime_LogTraceE
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim frequency As Double
        Dim startTime As Long
        Dim endTime As Long
        Dim elapsedTime As Double

        ' Get some general information about the Stopwatch

        Me.cbIsHighResolution.Checked = System.Diagnostics.Stopwatch.IsHighResolution
        frequency = System.Diagnostics.Stopwatch.Frequency
        Me.txtFrequency.Text = frequency.ToString

        ' Call once just to be sure all code is loaded

        Stopwatch.GetTimestamp()

        startTime = Stopwatch.GetTimestamp

        For i As Integer = 1 To 100
            Stopwatch.GetTimestamp()
        Next

        endTime = Stopwatch.GetTimestamp

        elapsedTime = ((endTime - startTime) / 100.0) / frequency
        Me.txtElapsedTime_GetTimeStamp.Text = elapsedTime.ToString
    End Sub

    Private Sub btnTestAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestAll.Click
        Dim tl As New TestLogging

        tl.Log_All()
    End Sub

    Private Sub btnTestFailure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestFailure.Click
        Dim tl As New TestLogging

        tl.Log_Failure()
    End Sub

    Private Sub btnTestError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestError.Click
        Dim tl As New TestLogging

        tl.Log_Error()
    End Sub

    Private Sub btnTestWarning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestWarning.Click
        Dim tl As New TestLogging

        tl.Log_Warning()
    End Sub

    Private Sub btnTestInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestInfo.Click
        Dim tl As New TestLogging

        tl.Log_Info()
    End Sub

    Private Sub btnTestDebug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestDebug.Click
        Dim tl As New TestLogging

        tl.Log_Debug()
    End Sub

    Private Sub btnTestTrace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestTrace.Click
        Dim tl As New TestLogging
        Dim frequency As Double
        Dim endTime As Long
        Dim elapsedTime As Double

        m_StartTime = Stopwatch.GetTimestamp
        frequency = Stopwatch.Frequency

        tl.Log_Trace()

        endTime = Stopwatch.GetTimestamp

        elapsedTime = (endTime - m_StartTime) / frequency
        Me.txtElapsedTime_Trace.Text = elapsedTime.ToString
    End Sub

    Private Sub btnTestException_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestException.Click
        Dim tl As New TestLogging

        tl.TestExceptionHandling()
    End Sub

    Private m_StartTime As Long

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        m_StartTime = Stopwatch.GetTimestamp
        Me.txtEndTicks.Clear()
        Me.txtElapsedTime.Clear()

        Me.txtStartTicks.Text = m_StartTime.ToString

        Me.btnStart.Enabled = False
        Me.btnStop.Enabled = True
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Dim frequency As Double
        Dim endTime As Long
        Dim elapsedTime As Double

        frequency = System.Diagnostics.Stopwatch.Frequency
        endTime = Stopwatch.GetTimestamp
        Me.txtEndTicks.Text = endTime.ToString

        elapsedTime = (endTime - m_StartTime) / frequency
        Me.txtElapsedTime.Text = elapsedTime.ToString

        Me.btnStart.Enabled = True
        Me.btnStop.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tl As New TestLogging

        tl.TestBatchNotificationCustomListner()
    End Sub

    Private Sub btnLogEventID_Click(sender As Object, e As EventArgs) Handles btnLogEventID.Click
        AppLog.Info("EventIDTest", "", Integer.Parse(txtEventID.Text))
    End Sub

    Private Sub btnFoo_Click(sender As Object, e As EventArgs) Handles btnFoo.Click
        AppLog.Info("In btnFoo_Click()", "TESTLOGGING")
        Dim testDLLClass As New TestLoggingDLL.Class1

        testDLLCLass.foo()
    End Sub

    Private Sub btnBar_Click(sender As Object, e As EventArgs) Handles btnBar.Click
        AppLog.Info("In btnBar_Click()", "TESTLOGGING")
        Dim testDLLClass As New TestLoggingDLL.Class1

        testDLLCLass.bar()
    End Sub
End Class
