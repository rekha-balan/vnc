<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnTestAll = New System.Windows.Forms.Button()
        Me.cbIsHighResolution = New System.Windows.Forms.CheckBox()
        Me.txtElapsedTime = New System.Windows.Forms.TextBox()
        Me.txtStartTicks = New System.Windows.Forms.TextBox()
        Me.txtEndTicks = New System.Windows.Forms.TextBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtElapsedTime_LogInfo = New System.Windows.Forms.TextBox()
        Me.txtElapsedTime_LogInfoStack = New System.Windows.Forms.TextBox()
        Me.txtElapsedTime_LogWrite = New System.Windows.Forms.TextBox()
        Me.txtElapsedTime_GetTimeStamp = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtElapsedTime_LogWriteLight = New System.Windows.Forms.TextBox()
        Me.btnTimeLogging = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtElapsedTime_LogTraceE = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtElapsedTime_LogTrace = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtElapsedTime_Trace = New System.Windows.Forms.TextBox()
        Me.btnTestTrace = New System.Windows.Forms.Button()
        Me.btnTestException = New System.Windows.Forms.Button()
        Me.btnTestFailure = New System.Windows.Forms.Button()
        Me.btnTestDebug = New System.Windows.Forms.Button()
        Me.btnTestInfo = New System.Windows.Forms.Button()
        Me.btnTestWarning = New System.Windows.Forms.Button()
        Me.btnTestError = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnLogEventID = New System.Windows.Forms.Button()
        Me.txtEventID = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnFoo = New System.Windows.Forms.Button()
        Me.btnBar = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox5.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.SuspendLayout
        '
        'btnTestAll
        '
        Me.btnTestAll.Location = New System.Drawing.Point(18, 46)
        Me.btnTestAll.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestAll.Name = "btnTestAll"
        Me.btnTestAll.Size = New System.Drawing.Size(328, 44)
        Me.btnTestAll.TabIndex = 0
        Me.btnTestAll.Text = "Test All Logging Methods"
        Me.btnTestAll.UseVisualStyleBackColor = true
        '
        'cbIsHighResolution
        '
        Me.cbIsHighResolution.AutoSize = true
        Me.cbIsHighResolution.Location = New System.Drawing.Point(286, 35)
        Me.cbIsHighResolution.Margin = New System.Windows.Forms.Padding(6)
        Me.cbIsHighResolution.Name = "cbIsHighResolution"
        Me.cbIsHighResolution.Size = New System.Drawing.Size(218, 29)
        Me.cbIsHighResolution.TabIndex = 1
        Me.cbIsHighResolution.Text = "Is High Resolution"
        Me.cbIsHighResolution.UseVisualStyleBackColor = true
        '
        'txtElapsedTime
        '
        Me.txtElapsedTime.Location = New System.Drawing.Point(18, 135)
        Me.txtElapsedTime.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime.Name = "txtElapsedTime"
        Me.txtElapsedTime.Size = New System.Drawing.Size(196, 31)
        Me.txtElapsedTime.TabIndex = 2
        '
        'txtStartTicks
        '
        Me.txtStartTicks.Location = New System.Drawing.Point(18, 37)
        Me.txtStartTicks.Margin = New System.Windows.Forms.Padding(6)
        Me.txtStartTicks.Name = "txtStartTicks"
        Me.txtStartTicks.Size = New System.Drawing.Size(196, 31)
        Me.txtStartTicks.TabIndex = 3
        '
        'txtEndTicks
        '
        Me.txtEndTicks.Location = New System.Drawing.Point(18, 87)
        Me.txtEndTicks.Margin = New System.Windows.Forms.Padding(6)
        Me.txtEndTicks.Name = "txtEndTicks"
        Me.txtEndTicks.Size = New System.Drawing.Size(196, 31)
        Me.txtEndTicks.TabIndex = 4
        '
        'txtFrequency
        '
        Me.txtFrequency.Location = New System.Drawing.Point(18, 79)
        Me.txtFrequency.Margin = New System.Windows.Forms.Padding(6)
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(248, 31)
        Me.txtFrequency.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(280, 88)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 25)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Frequency"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(236, 38)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 25)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Start Ticks"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(236, 92)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 25)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "End Ticks"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(236, 142)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 25)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Elapsed Time"
        '
        'txtElapsedTime_LogInfo
        '
        Me.txtElapsedTime_LogInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogInfo.Location = New System.Drawing.Point(22, 127)
        Me.txtElapsedTime_LogInfo.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogInfo.Name = "txtElapsedTime_LogInfo"
        Me.txtElapsedTime_LogInfo.Size = New System.Drawing.Size(344, 37)
        Me.txtElapsedTime_LogInfo.TabIndex = 15
        '
        'txtElapsedTime_LogInfoStack
        '
        Me.txtElapsedTime_LogInfoStack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogInfoStack.Location = New System.Drawing.Point(22, 210)
        Me.txtElapsedTime_LogInfoStack.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogInfoStack.Name = "txtElapsedTime_LogInfoStack"
        Me.txtElapsedTime_LogInfoStack.Size = New System.Drawing.Size(344, 37)
        Me.txtElapsedTime_LogInfoStack.TabIndex = 17
        '
        'txtElapsedTime_LogWrite
        '
        Me.txtElapsedTime_LogWrite.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogWrite.Location = New System.Drawing.Point(22, 294)
        Me.txtElapsedTime_LogWrite.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogWrite.Name = "txtElapsedTime_LogWrite"
        Me.txtElapsedTime_LogWrite.Size = New System.Drawing.Size(344, 37)
        Me.txtElapsedTime_LogWrite.TabIndex = 19
        '
        'txtElapsedTime_GetTimeStamp
        '
        Me.txtElapsedTime_GetTimeStamp.Location = New System.Drawing.Point(18, 129)
        Me.txtElapsedTime_GetTimeStamp.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_GetTimeStamp.Name = "txtElapsedTime_GetTimeStamp"
        Me.txtElapsedTime_GetTimeStamp.Size = New System.Drawing.Size(246, 31)
        Me.txtElapsedTime_GetTimeStamp.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(280, 135)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(282, 25)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "GetTimeStamp elapsed time"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(16, 96)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 25)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "PLLog.Info"
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(16, 263)
        Me.Label8.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 25)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "PLLog.Write"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(14, 344)
        Me.Label9.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(177, 25)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "PLLog.WriteLight"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(16, 179)
        Me.Label10.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(181, 25)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "PLLog.Info+Stack"
        '
        'txtElapsedTime_LogWriteLight
        '
        Me.txtElapsedTime_LogWriteLight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogWriteLight.Location = New System.Drawing.Point(20, 375)
        Me.txtElapsedTime_LogWriteLight.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogWriteLight.Name = "txtElapsedTime_LogWriteLight"
        Me.txtElapsedTime_LogWriteLight.Size = New System.Drawing.Size(346, 37)
        Me.txtElapsedTime_LogWriteLight.TabIndex = 26
        '
        'btnTimeLogging
        '
        Me.btnTimeLogging.Location = New System.Drawing.Point(20, 46)
        Me.btnTimeLogging.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTimeLogging.Name = "btnTimeLogging"
        Me.btnTimeLogging.Size = New System.Drawing.Size(250, 44)
        Me.btnTimeLogging.TabIndex = 28
        Me.btnTimeLogging.Text = "Time Logging"
        Me.btnTimeLogging.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtElapsedTime_GetTimeStamp)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFrequency)
        Me.GroupBox1.Controls.Add(Me.cbIsHighResolution)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 27)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Size = New System.Drawing.Size(584, 187)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "System.Diagnostics.Stopwatch"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnStop)
        Me.GroupBox2.Controls.Add(Me.btnStart)
        Me.GroupBox2.Controls.Add(Me.txtStartTicks)
        Me.GroupBox2.Controls.Add(Me.txtEndTicks)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtElapsedTime)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(614, 23)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Size = New System.Drawing.Size(518, 190)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Demonstrate Timing"
        '
        'btnStop
        '
        Me.btnStop.Enabled = false
        Me.btnStop.Location = New System.Drawing.Point(394, 92)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(6)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(110, 44)
        Me.btnStop.TabIndex = 10
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = true
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(394, 33)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(110, 44)
        Me.btnStart.TabIndex = 8
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = true
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogTraceE)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogTrace)
        Me.GroupBox3.Controls.Add(Me.btnTimeLogging)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogWriteLight)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogWrite)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogInfoStack)
        Me.GroupBox3.Controls.Add(Me.txtElapsedTime_LogInfo)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 225)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox3.Size = New System.Drawing.Size(584, 646)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Time Logging Code"
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Location = New System.Drawing.Point(16, 556)
        Me.Label11.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(149, 25)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "PLLog.TraceE"
        '
        'txtElapsedTime_LogTraceE
        '
        Me.txtElapsedTime_LogTraceE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogTraceE.Location = New System.Drawing.Point(22, 587)
        Me.txtElapsedTime_LogTraceE.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogTraceE.Name = "txtElapsedTime_LogTraceE"
        Me.txtElapsedTime_LogTraceE.Size = New System.Drawing.Size(344, 37)
        Me.txtElapsedTime_LogTraceE.TabIndex = 31
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(16, 467)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 25)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "PLLog.Trace"
        '
        'txtElapsedTime_LogTrace
        '
        Me.txtElapsedTime_LogTrace.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtElapsedTime_LogTrace.Location = New System.Drawing.Point(22, 498)
        Me.txtElapsedTime_LogTrace.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_LogTrace.Name = "txtElapsedTime_LogTrace"
        Me.txtElapsedTime_LogTrace.Size = New System.Drawing.Size(344, 37)
        Me.txtElapsedTime_LogTrace.TabIndex = 29
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Controls.Add(Me.txtElapsedTime_Trace)
        Me.GroupBox4.Controls.Add(Me.btnTestTrace)
        Me.GroupBox4.Controls.Add(Me.btnTestException)
        Me.GroupBox4.Controls.Add(Me.btnTestFailure)
        Me.GroupBox4.Controls.Add(Me.btnTestDebug)
        Me.GroupBox4.Controls.Add(Me.btnTestInfo)
        Me.GroupBox4.Controls.Add(Me.btnTestWarning)
        Me.GroupBox4.Controls.Add(Me.btnTestError)
        Me.GroupBox4.Controls.Add(Me.btnTestAll)
        Me.GroupBox4.Location = New System.Drawing.Point(614, 225)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox4.Size = New System.Drawing.Size(516, 492)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Test Logging"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(18, 402)
        Me.Button1.Margin = New System.Windows.Forms.Padding(6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(480, 44)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Test Batch Notification Custom Trace Listener"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'txtElapsedTime_Trace
        '
        Me.txtElapsedTime_Trace.Location = New System.Drawing.Point(266, 290)
        Me.txtElapsedTime_Trace.Margin = New System.Windows.Forms.Padding(6)
        Me.txtElapsedTime_Trace.Name = "txtElapsedTime_Trace"
        Me.txtElapsedTime_Trace.Size = New System.Drawing.Size(234, 31)
        Me.txtElapsedTime_Trace.TabIndex = 8
        '
        'btnTestTrace
        '
        Me.btnTestTrace.Location = New System.Drawing.Point(268, 235)
        Me.btnTestTrace.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestTrace.Name = "btnTestTrace"
        Me.btnTestTrace.Size = New System.Drawing.Size(238, 44)
        Me.btnTestTrace.TabIndex = 7
        Me.btnTestTrace.Text = "Test Trace Methods"
        Me.btnTestTrace.UseVisualStyleBackColor = true
        '
        'btnTestException
        '
        Me.btnTestException.Location = New System.Drawing.Point(18, 344)
        Me.btnTestException.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestException.Name = "btnTestException"
        Me.btnTestException.Size = New System.Drawing.Size(328, 44)
        Me.btnTestException.TabIndex = 6
        Me.btnTestException.Text = "Test Exception Methods"
        Me.btnTestException.UseVisualStyleBackColor = true
        '
        'btnTestFailure
        '
        Me.btnTestFailure.Location = New System.Drawing.Point(18, 121)
        Me.btnTestFailure.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestFailure.Name = "btnTestFailure"
        Me.btnTestFailure.Size = New System.Drawing.Size(238, 44)
        Me.btnTestFailure.TabIndex = 5
        Me.btnTestFailure.Text = "Test Failure Methods"
        Me.btnTestFailure.UseVisualStyleBackColor = true
        '
        'btnTestDebug
        '
        Me.btnTestDebug.Location = New System.Drawing.Point(18, 235)
        Me.btnTestDebug.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestDebug.Name = "btnTestDebug"
        Me.btnTestDebug.Size = New System.Drawing.Size(238, 44)
        Me.btnTestDebug.TabIndex = 4
        Me.btnTestDebug.Text = "Test Debug Methods"
        Me.btnTestDebug.UseVisualStyleBackColor = true
        '
        'btnTestInfo
        '
        Me.btnTestInfo.Location = New System.Drawing.Point(266, 177)
        Me.btnTestInfo.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestInfo.Name = "btnTestInfo"
        Me.btnTestInfo.Size = New System.Drawing.Size(238, 44)
        Me.btnTestInfo.TabIndex = 3
        Me.btnTestInfo.Text = "Test Info Methods"
        Me.btnTestInfo.UseVisualStyleBackColor = true
        '
        'btnTestWarning
        '
        Me.btnTestWarning.Location = New System.Drawing.Point(18, 177)
        Me.btnTestWarning.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestWarning.Name = "btnTestWarning"
        Me.btnTestWarning.Size = New System.Drawing.Size(238, 44)
        Me.btnTestWarning.TabIndex = 2
        Me.btnTestWarning.Text = "Test Warning Methods"
        Me.btnTestWarning.UseVisualStyleBackColor = true
        '
        'btnTestError
        '
        Me.btnTestError.Location = New System.Drawing.Point(266, 121)
        Me.btnTestError.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTestError.Name = "btnTestError"
        Me.btnTestError.Size = New System.Drawing.Size(238, 44)
        Me.btnTestError.TabIndex = 1
        Me.btnTestError.Text = "Test Error Methods"
        Me.btnTestError.UseVisualStyleBackColor = true
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnLogEventID)
        Me.GroupBox5.Controls.Add(Me.txtEventID)
        Me.GroupBox5.Location = New System.Drawing.Point(614, 749)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(499, 151)
        Me.GroupBox5.TabIndex = 33
        Me.GroupBox5.TabStop = false
        Me.GroupBox5.Text = "EventID"
        '
        'btnLogEventID
        '
        Me.btnLogEventID.Location = New System.Drawing.Point(284, 44)
        Me.btnLogEventID.Margin = New System.Windows.Forms.Padding(6)
        Me.btnLogEventID.Name = "btnLogEventID"
        Me.btnLogEventID.Size = New System.Drawing.Size(110, 44)
        Me.btnLogEventID.TabIndex = 9
        Me.btnLogEventID.Text = "Log"
        Me.btnLogEventID.UseVisualStyleBackColor = true
        '
        'txtEventID
        '
        Me.txtEventID.Location = New System.Drawing.Point(9, 44)
        Me.txtEventID.Margin = New System.Windows.Forms.Padding(6)
        Me.txtEventID.Name = "txtEventID"
        Me.txtEventID.Size = New System.Drawing.Size(248, 31)
        Me.txtEventID.TabIndex = 6
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnBar)
        Me.GroupBox6.Controls.Add(Me.btnFoo)
        Me.GroupBox6.Location = New System.Drawing.Point(26, 880)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(562, 151)
        Me.GroupBox6.TabIndex = 34
        Me.GroupBox6.TabStop = false
        Me.GroupBox6.Text = "Call DLL"
        '
        'btnFoo
        '
        Me.btnFoo.Location = New System.Drawing.Point(9, 38)
        Me.btnFoo.Margin = New System.Windows.Forms.Padding(6)
        Me.btnFoo.Name = "btnFoo"
        Me.btnFoo.Size = New System.Drawing.Size(252, 44)
        Me.btnFoo.TabIndex = 9
        Me.btnFoo.Text = "TestLoggingDLL-foo()"
        Me.btnFoo.UseVisualStyleBackColor = true
        '
        'btnBar
        '
        Me.btnBar.Location = New System.Drawing.Point(9, 98)
        Me.btnBar.Margin = New System.Windows.Forms.Padding(6)
        Me.btnBar.Name = "btnBar"
        Me.btnBar.Size = New System.Drawing.Size(252, 44)
        Me.btnBar.TabIndex = 10
        Me.btnBar.Text = "TestLoggingDLL-bar()"
        Me.btnBar.UseVisualStyleBackColor = true
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12!, 25!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1136, 1063)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.GroupBox5.ResumeLayout(false)
        Me.GroupBox5.PerformLayout
        Me.GroupBox6.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents btnTestAll As System.Windows.Forms.Button
    Friend WithEvents cbIsHighResolution As System.Windows.Forms.CheckBox
    Friend WithEvents txtElapsedTime As System.Windows.Forms.TextBox
    Friend WithEvents txtStartTicks As System.Windows.Forms.TextBox
    Friend WithEvents txtEndTicks As System.Windows.Forms.TextBox
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtElapsedTime_LogInfo As System.Windows.Forms.TextBox
    Friend WithEvents txtElapsedTime_LogInfoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtElapsedTime_LogWrite As System.Windows.Forms.TextBox
    Friend WithEvents txtElapsedTime_GetTimeStamp As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtElapsedTime_LogWriteLight As System.Windows.Forms.TextBox
    Friend WithEvents btnTimeLogging As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestFailure As System.Windows.Forms.Button
    Friend WithEvents btnTestDebug As System.Windows.Forms.Button
    Friend WithEvents btnTestInfo As System.Windows.Forms.Button
    Friend WithEvents btnTestWarning As System.Windows.Forms.Button
    Friend WithEvents btnTestError As System.Windows.Forms.Button
    Friend WithEvents btnTestTrace As System.Windows.Forms.Button
    Friend WithEvents btnTestException As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents txtElapsedTime_Trace As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtElapsedTime_LogTrace As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtElapsedTime_LogTraceE As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnLogEventID As Button
    Friend WithEvents txtEventID As TextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents btnBar As Button
    Friend WithEvents btnFoo As Button
End Class
