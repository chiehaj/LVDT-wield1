Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.ChartArea
Imports System.Net.Sockets
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Graphics
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO

Imports System.Data.SqlClient.SqlConnection
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Windows.Forms.Control

Public Class FTPcsvMove
    Dim allfilename() As String
    Dim allfilepath() As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim test() As String
        Dim iii = 0
        Dim ii = 0
        Dim newdir, rootdir, newname As String
        Dim setdate, setdatey, setdatem, setdated As String
        Dim clickTime As String
        Dim mano As String
        Try
            clickTime = TimeOfDay.Hour & "-" & TimeOfDay.Minute & "-" & TimeOfDay.Second
            setdatey = tb_setDatey.Text
            setdatem = tb_setDatem.Text
            setdated = tb_setDated.Text
            mano = cb_mano.SelectedItem
            If setdatey = "" Or setdatem = "" Or setdated = "" Or mano = "" Then
                MsgBox("資料不完整，請確認！！")
            Else
                setdate = setdatey & setdatem & setdated
                rootdir = "D:\LVDT-Data\" & mano & "\wield-LVDT\"
                newdir = "D:\LVDT-Data\" & mano & "\wield-LVDT\" & setdate
                newname = setdate & "-" & clickTime
                My.Computer.FileSystem.CreateDirectory(newdir & "\s")
                For Each ff As String In My.Computer.FileSystem.GetFiles(rootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_" & setdatem & setdated & "*.csv")
                    iii = iii + 1
                Next
                ReDim allfilename(iii)
                ReDim allfilepath(iii)
                ReDim test(iii)
                iii = 0
                For Each ff As String In My.Computer.FileSystem.GetFiles(rootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_" & setdatem & setdated & "*.csv")
                    Dim temp() As String
                    temp = ff.Split("\")
                    test(iii) = ff
                    allfilepath(iii) = ff
                    allfilename(iii) = temp(temp.Length - 1)
                    My.Computer.FileSystem.MoveFile(ff, newdir & "\s\" & temp(temp.Length - 1))
                    iii = iii + 1
                Next
                For Each ff As String In My.Computer.FileSystem.GetFiles(newdir & "\s\", FileIO.SearchOption.SearchTopLevelOnly, "*_" & setdatem & setdated & "*.csv")
                    Dim temp() As String
                    temp = ff.Split("\")
                    readCSV(ff, temp(temp.Length - 1), newdir, newname, mano)
                Next
                MsgBox("ok")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FTPcsvMove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cb_mano.Items.Clear()
        For i = 1 To 15
            cb_mano.Items.Add("NO" & i)
        Next
        cb_mano.SelectedIndex = 0
    End Sub

    Public Sub readCSV(ByVal spath As String, ByVal sname As String, ByVal tpath As String, ByVal tname As String, ByVal mano As String)
        Dim strTemp(1000) As String
        Dim count As Integer            '定義count為資料筆數
        '定義str_path為選取檔案路徑
        '定義str_spath為選取檔案名稱
        Dim strtime() As Double
        Dim strValue() As Double
        Dim readline As Integer
        Dim stoplvdt, stoptime, lvdtmax, lvdtmin As Double
        Try
            readline = 0
            count = 0
            Dim lread As String
            Dim sr As New StreamReader(spath)
            While sr.Peek <> -1
                Dim csvreadtemp() As String
                lread = sr.ReadLine
                If lread <> "" Then
                    csvreadtemp = lread.Split(",")
                    If readline = 0 Then
                        count = csvreadtemp(1)
                        ReDim strtime(count)
                        ReDim strValue(count)
                    ElseIf readline = 1 Then
                        stoplvdt = csvreadtemp(1) / 1000
                    ElseIf readline > 1 And readline - 2 <= count Then
                        strtime(readline - 2) = (readline - 2) / 10
                        strValue(readline - 2) = csvreadtemp(1) / 1000
                    ElseIf readline - 2 > count Then
                        Exit While
                    End If
                    readline += 1
                Else
                    Exit While
                End If
            End While
            sr.Close()
            stoptime = 0
            lvdtmax = 0
            lvdtmin = 0
            For i = 0 To strValue.Length - 1
                If strValue(i) > lvdtmax Then
                    lvdtmax = strValue(i)
                ElseIf strValue(i) < lvdtmin Then
                    lvdtmin = strValue(i)
                End If
                If strValue(i) < stoplvdt And stoptime = 0 Then
                    If i > 0 Then
                        stoptime = strtime(i - 1)
                    Else
                        stoptime = strtime(i)
                    End If
                End If
            Next

            Dim nCSVfile As String
            Dim wtoCSVs As String
            Dim writeCSV As System.IO.StreamWriter

            Dim stemp1(), stemp2() As String
            Dim stemp3() As Char
            stemp1 = sname.Split("_")
            stemp2 = stemp1(1).Split(".")
            stemp3 = stemp2(0).ToCharArray
            wtoCSVs = stemp3(0) & stemp3(1) & stemp3(2) & stemp3(3) & "-" & stemp3(4) & stemp3(5) & stemp3(6) & stemp3(7) & stemp3(8) & stemp3(9)
            wtoCSVs = wtoCSVs & "," & count / 10
            wtoCSVs = wtoCSVs & "," & stoplvdt
            wtoCSVs = wtoCSVs & "," & stoptime
            wtoCSVs = wtoCSVs & "," & lvdtmax
            wtoCSVs = wtoCSVs & "," & lvdtmin

            nCSVfile = tpath & "\" & mano & "-" & tname & ".csv"

            If My.Computer.FileSystem.FileExists(nCSVfile) Then
                writeCSV = My.Computer.FileSystem.OpenTextFileWriter(nCSVfile, True)
                writeCSV.WriteLine(wtoCSVs)
                writeCSV.Close()
            Else
                writeCSV = My.Computer.FileSystem.OpenTextFileWriter(nCSVfile, True)
                writeCSV.WriteLine("日期,焊接時間,中斷LVDT值,中斷時間,LVDT-MAX,LVDT-MIN")
                writeCSV.WriteLine(wtoCSVs)
                writeCSV.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function checksetdate(ByVal setdate As String, ByVal c As Integer)
        Dim result As String = ""
        Dim temp() As Char
        temp = setdate.ToCharArray
        If setdate.Length < c Then
            For i = 0 To c - setdate.Length - 1
                setdate = "0" & setdate
            Next
        ElseIf setdate.Length > c Then
            For i = 0 To c - 1
                result = result & temp(i)
            Next
            setdate = result
        End If
        Return setdate
    End Function

    Private Sub tb_setDatey_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tb_setDatey.KeyPress
        If e.KeyChar = Chr(48) Or e.KeyChar = Chr(49) Or e.KeyChar = Chr(50) Or e.KeyChar = Chr(51) Or e.KeyChar = Chr(52) Or e.KeyChar = Chr(53) Or e.KeyChar = Chr(54) Or e.KeyChar = Chr(55) Or e.KeyChar = Chr(56) Or e.KeyChar = Chr(57) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(13) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub tb_setDatem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tb_setDatem.KeyPress
        If e.KeyChar = Chr(48) Or e.KeyChar = Chr(49) Or e.KeyChar = Chr(50) Or e.KeyChar = Chr(51) Or e.KeyChar = Chr(52) Or e.KeyChar = Chr(53) Or e.KeyChar = Chr(54) Or e.KeyChar = Chr(55) Or e.KeyChar = Chr(56) Or e.KeyChar = Chr(57) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(13) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub tb_setDated_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tb_setDated.KeyPress
        If e.KeyChar = Chr(48) Or e.KeyChar = Chr(49) Or e.KeyChar = Chr(50) Or e.KeyChar = Chr(51) Or e.KeyChar = Chr(52) Or e.KeyChar = Chr(53) Or e.KeyChar = Chr(54) Or e.KeyChar = Chr(55) Or e.KeyChar = Chr(56) Or e.KeyChar = Chr(57) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(13) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub tb_setDatey_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_setDatey.LostFocus
        tb_setDatey.Text = checksetdate(tb_setDatey.Text, 4)
    End Sub

    Private Sub tb_setDatem_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_setDatem.LostFocus
        tb_setDatem.Text = checksetdate(tb_setDatem.Text, 2)
    End Sub

    Private Sub tb_setDated_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_setDated.LostFocus
        tb_setDated.Text = checksetdate(tb_setDated.Text, 2)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            MsgBox(FolderBrowserDialog1.SelectedPath)
        End If
    End Sub
End Class