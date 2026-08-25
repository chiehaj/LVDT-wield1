<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTPcsvMove
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.cb_mano = New System.Windows.Forms.ComboBox
        Me.tb_setDatey = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.tb_setDatem = New System.Windows.Forms.TextBox
        Me.tb_setDated = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(35, 234)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 58)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cb_mano
        '
        Me.cb_mano.FormattingEnabled = True
        Me.cb_mano.Location = New System.Drawing.Point(35, 49)
        Me.cb_mano.Name = "cb_mano"
        Me.cb_mano.Size = New System.Drawing.Size(104, 23)
        Me.cb_mano.TabIndex = 1
        '
        'tb_setDatey
        '
        Me.tb_setDatey.Location = New System.Drawing.Point(35, 151)
        Me.tb_setDatey.Name = "tb_setDatey"
        Me.tb_setDatey.Size = New System.Drawing.Size(71, 25)
        Me.tb_setDatey.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "機台編號"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "日期設定"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "年"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(124, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "月"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(210, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "日"
        '
        'tb_setDatem
        '
        Me.tb_setDatem.Location = New System.Drawing.Point(123, 151)
        Me.tb_setDatem.Name = "tb_setDatem"
        Me.tb_setDatem.Size = New System.Drawing.Size(71, 25)
        Me.tb_setDatem.TabIndex = 8
        '
        'tb_setDated
        '
        Me.tb_setDated.Location = New System.Drawing.Point(213, 151)
        Me.tb_setDated.Name = "tb_setDated"
        Me.tb_setDated.Size = New System.Drawing.Size(71, 25)
        Me.tb_setDated.TabIndex = 9
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(209, 241)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(43, 50)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FTPcsvMove
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(310, 337)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.tb_setDated)
        Me.Controls.Add(Me.tb_setDatem)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tb_setDatey)
        Me.Controls.Add(Me.cb_mano)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FTPcsvMove"
        Me.Text = "FTPcsvMove"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cb_mano As System.Windows.Forms.ComboBox
    Friend WithEvents tb_setDatey As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tb_setDatem As System.Windows.Forms.TextBox
    Friend WithEvents tb_setDated As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
End Class
