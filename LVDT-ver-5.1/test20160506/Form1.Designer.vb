<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbx_IP = New System.Windows.Forms.TextBox
        Me.tbx_Port = New System.Windows.Forms.TextBox
        Me.tbx_TestSendCommand = New System.Windows.Forms.TextBox
        Me.tbx_TcpResult = New System.Windows.Forms.TextBox
        Me.lbl_connectState = New System.Windows.Forms.Label
        Me.btn_Connect = New System.Windows.Forms.Button
        Me.btn_SendCommand = New System.Windows.Forms.Button
        Me.btn_ClearResult = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btn_DrawingLine = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button
        Me.lbl_timer2 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(69, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PORT"
        '
        'tbx_IP
        '
        Me.tbx_IP.Location = New System.Drawing.Point(12, 24)
        Me.tbx_IP.Name = "tbx_IP"
        Me.tbx_IP.Size = New System.Drawing.Size(133, 22)
        Me.tbx_IP.TabIndex = 2
        '
        'tbx_Port
        '
        Me.tbx_Port.Location = New System.Drawing.Point(151, 24)
        Me.tbx_Port.Name = "tbx_Port"
        Me.tbx_Port.Size = New System.Drawing.Size(100, 22)
        Me.tbx_Port.TabIndex = 3
        '
        'tbx_TestSendCommand
        '
        Me.tbx_TestSendCommand.Location = New System.Drawing.Point(14, 95)
        Me.tbx_TestSendCommand.Name = "tbx_TestSendCommand"
        Me.tbx_TestSendCommand.Size = New System.Drawing.Size(133, 22)
        Me.tbx_TestSendCommand.TabIndex = 4
        '
        'tbx_TcpResult
        '
        Me.tbx_TcpResult.Location = New System.Drawing.Point(14, 135)
        Me.tbx_TcpResult.Multiline = True
        Me.tbx_TcpResult.Name = "tbx_TcpResult"
        Me.tbx_TcpResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.tbx_TcpResult.Size = New System.Drawing.Size(178, 74)
        Me.tbx_TcpResult.TabIndex = 5
        '
        'lbl_connectState
        '
        Me.lbl_connectState.AutoSize = True
        Me.lbl_connectState.Location = New System.Drawing.Point(93, 60)
        Me.lbl_connectState.Name = "lbl_connectState"
        Me.lbl_connectState.Size = New System.Drawing.Size(15, 12)
        Me.lbl_connectState.TabIndex = 6
        Me.lbl_connectState.Text = "IP"
        '
        'btn_Connect
        '
        Me.btn_Connect.Location = New System.Drawing.Point(12, 55)
        Me.btn_Connect.Name = "btn_Connect"
        Me.btn_Connect.Size = New System.Drawing.Size(75, 23)
        Me.btn_Connect.TabIndex = 7
        Me.btn_Connect.Text = "Connect"
        Me.btn_Connect.UseVisualStyleBackColor = True
        '
        'btn_SendCommand
        '
        Me.btn_SendCommand.Location = New System.Drawing.Point(153, 94)
        Me.btn_SendCommand.Name = "btn_SendCommand"
        Me.btn_SendCommand.Size = New System.Drawing.Size(75, 23)
        Me.btn_SendCommand.TabIndex = 8
        Me.btn_SendCommand.Text = "Send"
        Me.btn_SendCommand.UseVisualStyleBackColor = True
        '
        'btn_ClearResult
        '
        Me.btn_ClearResult.Location = New System.Drawing.Point(198, 186)
        Me.btn_ClearResult.Name = "btn_ClearResult"
        Me.btn_ClearResult.Size = New System.Drawing.Size(75, 23)
        Me.btn_ClearResult.TabIndex = 9
        Me.btn_ClearResult.Text = "Clean"
        Me.btn_ClearResult.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'btn_DrawingLine
        '
        Me.btn_DrawingLine.Location = New System.Drawing.Point(95, 313)
        Me.btn_DrawingLine.Name = "btn_DrawingLine"
        Me.btn_DrawingLine.Size = New System.Drawing.Size(19, 23)
        Me.btn_DrawingLine.TabIndex = 11
        Me.btn_DrawingLine.Text = "DrawLine"
        Me.btn_DrawingLine.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(124, 313)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(21, 31)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "開始記錄"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(58, 313)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(21, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "圖表產生"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lbl_timer2
        '
        Me.lbl_timer2.AutoSize = True
        Me.lbl_timer2.Location = New System.Drawing.Point(14, 228)
        Me.lbl_timer2.Name = "lbl_timer2"
        Me.lbl_timer2.Size = New System.Drawing.Size(37, 12)
        Me.lbl_timer2.TabIndex = 14
        Me.lbl_timer2.Text = "Label3"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Timer3
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 249)
        Me.Controls.Add(Me.lbl_timer2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_DrawingLine)
        Me.Controls.Add(Me.btn_ClearResult)
        Me.Controls.Add(Me.btn_SendCommand)
        Me.Controls.Add(Me.btn_Connect)
        Me.Controls.Add(Me.lbl_connectState)
        Me.Controls.Add(Me.tbx_TcpResult)
        Me.Controls.Add(Me.tbx_TestSendCommand)
        Me.Controls.Add(Me.tbx_Port)
        Me.Controls.Add(Me.tbx_IP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbx_IP As System.Windows.Forms.TextBox
    Friend WithEvents tbx_Port As System.Windows.Forms.TextBox
    Friend WithEvents tbx_TestSendCommand As System.Windows.Forms.TextBox
    Friend WithEvents tbx_TcpResult As System.Windows.Forms.TextBox
    Friend WithEvents lbl_connectState As System.Windows.Forms.Label
    Friend WithEvents btn_Connect As System.Windows.Forms.Button
    Friend WithEvents btn_SendCommand As System.Windows.Forms.Button
    Friend WithEvents btn_ClearResult As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btn_DrawingLine As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lbl_timer2 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents Timer3 As System.Windows.Forms.Timer

End Class
