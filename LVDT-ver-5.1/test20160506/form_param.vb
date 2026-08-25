Public Class form_param

    Dim rwini As New RWini

    Private Sub form_param_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Readpara()
    End Sub

    Private Sub btn_Readini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Readini.Click
        Readpara()
    End Sub

    Function Readpara()
        tbx_movetime.Text = rwini.Read("para", "出牙輪時間", "para")
        tbx_maxlimit.Text = rwini.Read("para", "maxlimit", "para")
        tbx_minlimit.Text = rwini.Read("para", "minlimit", "para")
        tbx_worktime1.Text = rwini.Read("para", "壓下秒數", "para")
        tbx_worktime2.Text = rwini.Read("para", "全程時間", "para")
        tbx_metallength.Text = rwini.Read("para", "工件長度", "para")
        tbx_toollength.Text = rwini.Read("para", "模具長", "para")
        tbx_pitch.Text = rwini.Read("para", "pitch", "para")
        tbx_front.Text = rwini.Read("para", "前斜", "para")
        tbx_put.Text = rwini.Read("para", "放料", "para")
        tbx_back.Text = rwini.Read("para", "後斜", "para")
    End Function

    Function Writepara()
        rwini.Write("para", "出牙輪時間", tbx_movetime.Text, "para")
        rwini.Write("para", "maxlimit", tbx_maxlimit.Text, "para")
        rwini.Write("para", "minlimit", tbx_minlimit.Text, "para")
        rwini.Write("para", "壓下秒數", tbx_worktime1.Text, "para")
        rwini.Write("para", "全程時間", tbx_worktime2.Text, "para")
        rwini.Write("para", "工件長度", tbx_metallength.Text, "para")
        rwini.Write("para", "模具長", tbx_toollength.Text, "para")
        rwini.Write("para", "pitch", tbx_pitch.Text, "para")
        rwini.Write("para", "前斜", tbx_front.Text, "para")
        rwini.Write("para", "放料", tbx_put.Text, "para")
        rwini.Write("para", "後斜", tbx_back.Text, "para")
    End Function

    Private Sub btn_writeini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_writeini.Click
        Writepara()
    End Sub

    Private Sub btn_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Exit.Click
        Me.Close()
    End Sub
End Class