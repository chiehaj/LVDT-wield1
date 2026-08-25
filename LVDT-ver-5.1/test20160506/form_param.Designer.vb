<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_param
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
        Me.tbx_minlimit = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.tbx_maxlimit = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.tbx_movetime = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.btn_Exit = New System.Windows.Forms.Button
        Me.btn_writeini = New System.Windows.Forms.Button
        Me.btn_Readini = New System.Windows.Forms.Button
        Me.tbx_worktime2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbx_worktime1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbx_pitch = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tbx_toollength = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbx_metallength = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbx_back = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tbx_put = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.tbx_front = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'tbx_minlimit
        '
        Me.tbx_minlimit.Location = New System.Drawing.Point(87, 79)
        Me.tbx_minlimit.Name = "tbx_minlimit"
        Me.tbx_minlimit.Size = New System.Drawing.Size(67, 22)
        Me.tbx_minlimit.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 82)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 12)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "監控下限："
        '
        'tbx_maxlimit
        '
        Me.tbx_maxlimit.Location = New System.Drawing.Point(87, 41)
        Me.tbx_maxlimit.Name = "tbx_maxlimit"
        Me.tbx_maxlimit.Size = New System.Drawing.Size(67, 22)
        Me.tbx_maxlimit.TabIndex = 37
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 12)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "監控上限："
        '
        'tbx_movetime
        '
        Me.tbx_movetime.Location = New System.Drawing.Point(87, 6)
        Me.tbx_movetime.Name = "tbx_movetime"
        Me.tbx_movetime.Size = New System.Drawing.Size(67, 22)
        Me.tbx_movetime.TabIndex = 35
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 9)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "出牙輪時間："
        '
        'btn_Exit
        '
        Me.btn_Exit.Location = New System.Drawing.Point(270, 224)
        Me.btn_Exit.Name = "btn_Exit"
        Me.btn_Exit.Size = New System.Drawing.Size(75, 35)
        Me.btn_Exit.TabIndex = 40
        Me.btn_Exit.Text = "EXIT"
        Me.btn_Exit.UseVisualStyleBackColor = True
        '
        'btn_writeini
        '
        Me.btn_writeini.Location = New System.Drawing.Point(145, 224)
        Me.btn_writeini.Name = "btn_writeini"
        Me.btn_writeini.Size = New System.Drawing.Size(75, 35)
        Me.btn_writeini.TabIndex = 41
        Me.btn_writeini.Text = "SAVE"
        Me.btn_writeini.UseVisualStyleBackColor = True
        '
        'btn_Readini
        '
        Me.btn_Readini.Location = New System.Drawing.Point(18, 224)
        Me.btn_Readini.Name = "btn_Readini"
        Me.btn_Readini.Size = New System.Drawing.Size(75, 35)
        Me.btn_Readini.TabIndex = 42
        Me.btn_Readini.Text = "READ"
        Me.btn_Readini.UseVisualStyleBackColor = True
        '
        'tbx_worktime2
        '
        Me.tbx_worktime2.Location = New System.Drawing.Point(87, 151)
        Me.tbx_worktime2.Name = "tbx_worktime2"
        Me.tbx_worktime2.Size = New System.Drawing.Size(67, 22)
        Me.tbx_worktime2.TabIndex = 46
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 154)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "全程時間："
        '
        'tbx_worktime1
        '
        Me.tbx_worktime1.Location = New System.Drawing.Point(87, 116)
        Me.tbx_worktime1.Name = "tbx_worktime1"
        Me.tbx_worktime1.Size = New System.Drawing.Size(67, 22)
        Me.tbx_worktime1.TabIndex = 44
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "壓下秒數："
        '
        'tbx_pitch
        '
        Me.tbx_pitch.Location = New System.Drawing.Point(278, 79)
        Me.tbx_pitch.Name = "tbx_pitch"
        Me.tbx_pitch.Size = New System.Drawing.Size(67, 22)
        Me.tbx_pitch.TabIndex = 54
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(220, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 12)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "pitch："
        '
        'tbx_toollength
        '
        Me.tbx_toollength.Location = New System.Drawing.Point(278, 41)
        Me.tbx_toollength.Name = "tbx_toollength"
        Me.tbx_toollength.Size = New System.Drawing.Size(67, 22)
        Me.tbx_toollength.TabIndex = 52
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(207, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "模具長："
        '
        'tbx_metallength
        '
        Me.tbx_metallength.Location = New System.Drawing.Point(278, 6)
        Me.tbx_metallength.Name = "tbx_metallength"
        Me.tbx_metallength.Size = New System.Drawing.Size(67, 22)
        Me.tbx_metallength.TabIndex = 50
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(195, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "工件長度："
        '
        'tbx_back
        '
        Me.tbx_back.Location = New System.Drawing.Point(278, 189)
        Me.tbx_back.Name = "tbx_back"
        Me.tbx_back.Size = New System.Drawing.Size(67, 22)
        Me.tbx_back.TabIndex = 60
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(219, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "後斜："
        '
        'tbx_put
        '
        Me.tbx_put.Location = New System.Drawing.Point(278, 151)
        Me.tbx_put.Name = "tbx_put"
        Me.tbx_put.Size = New System.Drawing.Size(67, 22)
        Me.tbx_put.TabIndex = 58
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(219, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 12)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "放料："
        '
        'tbx_front
        '
        Me.tbx_front.Location = New System.Drawing.Point(278, 116)
        Me.tbx_front.Name = "tbx_front"
        Me.tbx_front.Size = New System.Drawing.Size(67, 22)
        Me.tbx_front.TabIndex = 56
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(219, 119)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "前斜："
        '
        'form_param
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 269)
        Me.Controls.Add(Me.tbx_back)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tbx_put)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbx_front)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tbx_pitch)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbx_toollength)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbx_metallength)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbx_worktime2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbx_worktime1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_Readini)
        Me.Controls.Add(Me.btn_writeini)
        Me.Controls.Add(Me.btn_Exit)
        Me.Controls.Add(Me.tbx_minlimit)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.tbx_maxlimit)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tbx_movetime)
        Me.Controls.Add(Me.Label13)
        Me.Name = "form_param"
        Me.Text = "form_param"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbx_minlimit As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbx_maxlimit As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbx_movetime As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btn_Exit As System.Windows.Forms.Button
    Friend WithEvents btn_writeini As System.Windows.Forms.Button
    Friend WithEvents btn_Readini As System.Windows.Forms.Button
    Friend WithEvents tbx_worktime2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbx_worktime1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_pitch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbx_toollength As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbx_metallength As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbx_back As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbx_put As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbx_front As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
