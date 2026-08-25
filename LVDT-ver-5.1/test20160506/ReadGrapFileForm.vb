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

Public Class ReadGrapFileForm

    Dim Chart1 As New Chart()
    Dim timer1Count As Integer
    Dim lowvalue As Integer
    Dim hivalue As Integer
    Dim RecWielding As Boolean

    Dim RWini As New RWini()
    'Dim Chart1 As New Chart()
    'Dim chartArea1 As New ChartArea()

    Dim charttime() As Double
    Dim chartvalue() As Double

    Dim lvdt_timescale As Double
    Dim chartH_source As Double
    Dim chartW_source As Double
    Dim grapH_source As Double
    Dim grapW_source As Double
    Dim grapH_dis As Double
    Dim grapW_dis As Double
    Dim meh_source As Double
    Dim mew_source As Double
    Dim tcH_source As Double
    Dim tcW_source As Double
    Dim auto_read As Boolean
    Dim autoFile() As String
    Dim Filecount As Integer

    Dim cutvalue(9999) As String
    Dim cutvaluecount As Integer

    Private Sub ReadGrapFileForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        tm_getDirFileCount.Enabled = False
    End Sub

    Private Sub ReadGrapFileForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lb_listselectitem.Text = "list selected"
        grapH_dis = box_Grap.Top
        grapW_dis = Me.Width - box_Grap.Width - box_Grap.Left
        meh_source = Me.Height
        mew_source = Me.Width
        tcH_source = Me.TabControl1.Height
        tcW_source = Me.TabControl1.Width
        tm_getDirFileCount.Enabled = False
        auto_read = False
        btn_autoread.Text = "自動"
        tbx_timescale.Text = RWini.Read("para", "timescale", "para")
        lvdt_timescale = tbx_timescale.Text
        tbx_wieldLineShift.Text = 0
        TextBox2.Text = 0
        ComboBox1.Items.Clear()
        For i As Integer = 1 To 5
            ComboBox1.Items.Add(i)
        Next
    End Sub

    Public Sub chkChart()
        Dim a As Integer = Me.Chart1.ChartAreas.Count
        Dim b As Integer = Me.Chart1.Series.Count
        If Me.Chart1 Is Nothing Then
        Else
            If a > 0 Then
                Me.Chart1.ChartAreas.Clear()
            End If
            If b > 0 Then
                Me.Chart1.Series.Clear()
                ListBox1.Items.Clear()
            End If
        End If
    End Sub

    Public Sub DrawingChart(ByVal time() As Double, ByVal value() As Double, ByVal seriesname As String)
        charttime = time
        chartvalue = value
        'If tbx_movetime.Text = "" Then
        'tbx_movetime.Text = 0.0
        'End If
        Dim dou_pushsp As Double            'dou_pushsp：壓速=((工件長／全程時間)*壓下秒數)／pitch (成形)
        Dim dou_front As Double             'dou_front：前斜
        Dim dou_put As Double               'dou_put：放料
        Dim dou_back As Double              'dou_back：後斜
        Dim dou_stepthree As Double         'dou_stepthree：第三階段(+剩下-不足) = (前斜+放料+後斜+壓速) - 模具長/pitch
        Dim dou_metallength As Double       'dou_metallength：工件長度
        Dim dou_toollength As Double        'dou_toollength：模具長
        Dim dou_pushtime As Double          'dou_pushtime：壓下秒數
        Dim dou_worktime As Double          'dou_worktime：全程時間     
        Dim dou_pitch As Double             'dou_pitch：pitch

        'dou_metallength = RWini.Read("para", "工件長度", "para")
        'dou_worktime = RWini.Read("para", "全程時間", "para")
        'dou_pushtime = RWini.Read("para", "壓下秒數", "para")
        'dou_toollength = RWini.Read("para", "模具長", "para")
        'dou_pitch = RWini.Read("para", "pitch", "para")
        'dou_front = RWini.Read("para", "前斜", "para")
        'dou_put = RWini.Read("para", "放料", "para")
        'dou_back = RWini.Read("para", "後斜", "para")
        'dou_pushsp = ((dou_metallength / dou_worktime) * dou_pushtime) / dou_pitch
        'dou_stepthree = _
        '(((dou_front / dou_pitch) + (dou_back / dou_pitch) + (dou_put / dou_pitch) + dou_pushsp)) - (dou_toollength / dou_pitch)
        'dou_stepthree = Format(dou_stepthree, "##0.0##")
        'dou_pushsp = Format(dou_pushsp, "##0.0##")

        Dim outtime As Double
        'outtime = RWini.Read("para", "出牙輪時間", "para") * 10
        If outtime = 0 Then
            outtime = 1
        End If
        Dim ttp As Integer = charttime.Length - outtime
        Dim tempvalue(ttp) As Double
        Dim value_min As Double
        Dim value_max As Double
        value_max = -99
        value_min = 99
        For i As Integer = 0 To tempvalue.Length - 1
            tempvalue(i) = chartvalue(i + outtime - 1)
            If tempvalue(i) > value_max Then
                value_max = tempvalue(i)
            End If
            If tempvalue(i) < value_min Then
                value_min = tempvalue(i)
            End If
        Next
        Dim Chart1 As New Chart()

        ' Create Chart Area  
        Dim chartArea1 As New ChartArea()

        ' Add Chart Area to the Chart 
        Me.Chart1.ChartAreas.Clear()
        Me.Chart1.ChartAreas.Add(chartArea1)
        Dim movetime As Double
        'movetime = Format(tbx_movetime.Text / 1, "#.#")
        'movetime = Format(Form1.tbx_length.Text / Form1.tbx_movesp.Text, "#.#")
        'lb_movetime.Text = Format(movetime, "#.#")
        ' Create a data series  
        Dim series1 As New Series()
        Dim series2 As New Series()
        Dim series3 As New Series()
        ' Add data points to the first series 
        Dim org_value As Double
        Dim new_value As Double
        Dim dis_value As Double
        Dim high_sp As Double = 0
        Dim low_sp As Double = 0
        Dim temp_sp As Double
        'For a As Double = 0 To 20
        'For b As Double = 1 To time.Length - 1
        'If (value(a) > 0 And value(a + 1) > 0) Then
        'org_value = value(a)
        'new_value = value(a + 1)
        'dis_value = org_value - new_value
        'temp_sp = dis_value * 10
        'If (temp_sp > high_sp) Then
        'high_sp = temp_sp
        'ElseIf (temp_sp < high_sp And temp_sp < low_sp And low_sp <> 0) Then
        'low_sp = temp_sp
        'ElseIf (temp_sp < high_sp And low_sp = 0) Then
        'low_sp = temp_sp
        'End If
        'End If

        'Next
        ' Next
        'lb_highsp.Text = Format(high_sp, "#0.0##")
        'lb_lowsp.Text = Format(low_sp, "#0.0##")

        For x As Double = 0 To time.Length - 1
            If (value(x) > 0) Then

            End If
            series1.Points.AddXY(time(x), Format(value(x), "##.###"))
        Next
        series1.ChartType = SeriesChartType.Line
        series1.BorderColor = Color.Black
        series1.BorderWidth = 1
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = time(time.Length - 1) + (4 / lvdt_timescale)
        'tbx_viewValue1.Text = value.Min
        tbx_viewValue1.Text = value_min - 0.03
        tbx_viewValue2.Text = value_max + 0.03

        ' Add data points to the second series  
        For x As Double = 0 To 10 Step 0.1
            series2.Points.AddXY(x, 0.01)
        Next
        series2.ChartType = SeriesChartType.Line
        series2.BorderColor = Color.Red
        series2.BorderWidth = 1
        For x As Double = 0 To 10 Step 0.1
            series3.Points.AddXY(x, -0.01)
        Next
        series3.ChartType = SeriesChartType.Line
        series3.BorderColor = Color.Red
        series3.BorderWidth = 1

        ' Add series to the chart  
        series1.Name = seriesname
        Me.Chart1.Series.Add(series1)
        'Me.Chart1.Series.Add(series2)
        'Me.Chart1.Series.Add(series3)
        If lvdt_timescale = 10 Then
            Me.Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "{0:0.0}"
            Me.Chart1.ChartAreas(0).AxisX.Interval = 0.2
        ElseIf lvdt_timescale = 100 Then
            Me.Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "{0:0.00}"
            Me.Chart1.ChartAreas(0).AxisX.Interval = 0.02
        End If

        Me.Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "{0.000}"
        Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = 0.01
        Me.Chart1.Series(0).BorderWidth = 2

        tbx_ValuePitch.Text = Me.Chart1.ChartAreas(0).AxisY.Interval()
        tbx_ViewTimePitch.Text = Me.Chart1.ChartAreas(0).AxisX.Interval()
        ComboBox1.SelectedItem = Me.Chart1.Series(0).BorderWidth

        ' Me.Chart1.ChartAreas(0).AxisY.
        ' Set chart control location  
        ' Chart1.Location = New System.Drawing.Point(16, 48) 

        ' Set Chart control size  
        Me.Chart1.Height = Me.box_Grap.Height
        Me.Chart1.Width = Me.box_Grap.Width

        ' Add chart control to the form  
        box_Grap.Controls.AddRange(New System.Windows.Forms.Control() {Me.Chart1})

        ListBox1.Items.Add(Me.Chart1.Series(0).Name)
        Me.Chart1.ChartAreas(0).AxisX.Minimum = tbx_viewTime1.Text
        Me.Chart1.ChartAreas(0).AxisX.Maximum = tbx_viewTime2.Text
        Me.Chart1.ChartAreas(0).AxisY.Minimum = tbx_viewValue1.Text
        Me.Chart1.ChartAreas(0).AxisY.Maximum = tbx_viewValue2.Text

        chartH_source = Me.Chart1.Height
        chartW_source = Me.Chart1.Width
        grapH_source = box_Grap.Height
        grapW_source = box_Grap.Width

    End Sub
    Public Sub ClearChart()
        Me.Chart1.Series.Clear()
        Me.Chart1.ChartAreas.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (tbx_viewTime2.Text < tbx_viewTime1.Text) Then
            tbx_viewTime2.Text = tbx_viewTime1.Text + 10
        End If
        Me.Chart1.ChartAreas(0).AxisX.Minimum = tbx_viewTime1.Text
        Me.Chart1.ChartAreas(0).AxisX.Maximum = tbx_viewTime2.Text
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If

    End Sub

    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (tbx_viewValue2.Text < tbx_viewValue1.Text) Then
            tbx_viewValue2.Text = tbx_viewValue1.Text + 0.5
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = tbx_viewValue1.Text
        Me.Chart1.ChartAreas(0).AxisY.Maximum = tbx_viewValue2.Text
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If

    End Sub

    Private Sub btn_AddOtherFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer
        Dim str_path As String
        Dim str_spath As String
        Try
            count = 0
            Filenum = FreeFile()
            OpenFileDialog1.InitialDirectory = "D:\"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_path = OpenFileDialog1.FileName()
                str_spath = OpenFileDialog1.SafeFileName
                If str_path.EndsWith("txt") Or str_path.EndsWith("TXT") Then
                    readtxt(str_spath, str_path, 2)
                ElseIf str_path.EndsWith("csv") Or str_path.EndsWith("CSV") Then
                    readcsv(str_spath, str_path, 2)
                End If
            Else
                MsgBox("nothing ...........")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub Addfile(ByVal time() As Double, ByVal value() As Double, ByVal seriesname As String)

        ' Create a data series  
        Dim series1 As New Series()
        Dim new_color As Color
        'If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        'new_color = Form1.ColorDialog1.Color
        'End If


        ' Add data points to the first series 
        For x As Double = 0 To time.Length - 1
            series1.Points.AddXY(time(x), value(x))
        Next
        series1.ChartType = SeriesChartType.Line
        'series1.BorderColor = new_color
        series1.BorderWidth = ComboBox1.Text
        series1.Name = seriesname

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
    End Sub
    Function AddlimitLine(ByVal point As String)
        Dim series1 As New Series()
        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum

        For x As Double = x_min To x_max Step Me.Chart1.ChartAreas(0).AxisX.Interval
            series1.Points.AddXY(x, point)
        Next
        series1.ChartType = SeriesChartType.Line
        series1.BorderWidth = 2
        series1.Name = "基準" & point

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox1.Items.Add(Me.Chart1.Series(Me.Chart1.Series.Count - 1).Name)
    End Function
    Private Sub btn_AddLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim series1 As New Series()
        Dim new_color As Color
        If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            new_color = Form1.ColorDialog1.Color
        End If

        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum

        ' Add data points to the first series 
        For x As Double = x_min To x_max Step Me.Chart1.ChartAreas(0).AxisX.Interval
            series1.Points.AddXY(x, tbx_AddLine.Text)
        Next
        series1.ChartType = SeriesChartType.Line
        series1.BorderColor = new_color
        series1.BorderWidth = 1
        series1.Name = "基準" & tbx_AddLine.Text

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox1.Items.Add(Me.Chart1.Series(Me.Chart1.Series.Count - 1).Name)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        For i As Integer = 0 To Me.Chart1.Series.Count - 1
            Me.Chart1.Series(i).BorderWidth = ComboBox1.SelectedItem
        Next

    End Sub

    Private Sub btn_Linedelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Linedelete.Click
        Me.Chart1.Series.RemoveAt(ListBox1.SelectedIndex)
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub tbx_viewValue1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (tbx_viewValue2.Text < tbx_viewValue1.Text) Then
            'tbx_viewValue2.Text = tbx_viewValue1.Text + 0.2
        End If
    End Sub

    Private Sub tbx_viewValue2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (tbx_viewValue1.Text > tbx_viewValue2.Text) Then
            'tbx_viewValue1.Text = tbx_viewValue2.Text - 0.2
        End If
    End Sub

    Private Sub btn_Allview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Chart1.ChartAreas(0).AxisX.Minimum = 0
        Me.Chart1.ChartAreas(0).AxisX.Maximum = charttime.Max
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = charttime.Max
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = chartvalue.Min
        Me.Chart1.ChartAreas(0).AxisY.Maximum = chartvalue.Max
        tbx_viewValue1.Text = chartvalue.Min
        tbx_viewValue2.Text = chartvalue.Max
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If
    End Sub

    Public Sub readtxt(ByVal str_spath As String, ByVal str_path As String, ByVal drch As Integer)
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer            '定義count為資料筆數
        '定義str_path為選取檔案路徑
        '定義str_spath為選取檔案名稱
        Try
            count = 0
            Filenum = FreeFile()
            FileOpen(Filenum, str_path, OpenMode.Input)         '開啟檔案為input(讀取)模式
            Do Until EOF(Filenum)
                strTemp(count) = LineInput(Filenum)
                count += 1
            Loop
            FileClose()
            Dim strtime(count - 2) As Double
            Dim strValue(count - 2) As Double
            For i As Integer = 0 To count - 2
                Dim temp() As String
                temp = strTemp(i).Split(",")
                strtime(i) = temp(0)
                strValue(i) = temp(1)
            Next
            If drch = 1 Then
                cutvalue(count) = strTemp(count - 1)
                lb_lowc.Text = strTemp(count - 1)
                chkChart()
                DrawingChart(strtime, strValue, str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
                ListBox2.Items.Clear()
            Else
                lb_lowc.Text = lb_lowc.Text & "," & vbCr & strTemp(count - 1)
                Addfile(strtime, strValue, str_spath)
                ListBox1.Items.Add(str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Sub btn_ReadDatatxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReadDatatxt.Click
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer            '定義count為資料筆數
        Dim str_path As String          '定義str_path為選取檔案路徑
        Dim str_spath As String         '定義str_spath為選取檔案名稱
        Try
            count = 0
            Filenum = FreeFile()
            OpenFileDialog1.InitialDirectory = Application.StartupPath & "\b"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_spath = OpenFileDialog1.SafeFileName
                str_path = OpenFileDialog1.FileName()
                readtxt(str_spath, str_path, 1)
            Else
                MsgBox("nothing ......")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lb_selectcolor.ForeColor = Chart1.Series(ListBox1.SelectedIndex).BorderColor
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If i = ListBox1.SelectedIndex Then
                Chart1.Series(i).BorderWidth = 3
            Else
                Chart1.Series(i).BorderWidth = 1
            End If

        Next
    End Sub

    Private Sub btn_serdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim aa As String = ListBox1.SelectedItem
        If aa <> "" Then

            Chart1.Series.RemoveAt(Chart1.Series.IndexOf(ListBox1.SelectedItem))
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub btn_AddXline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim series1 As New Series()
        Dim new_color As Color
        If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            new_color = Form1.ColorDialog1.Color
        End If

        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum
        Dim y_min As Double = Me.Chart1.ChartAreas(0).AxisY.Minimum

        ' Add data points to the first series 
        'For x As Double = x_min To tbx_AddXline.Text Step Me.Chart1.ChartAreas(0).AxisX.Interval
        For x As Double = x_min To tbx_AddXline.Text Step 0.01
            series1.Points.AddXY(x, y_min + 0.01)
        Next
        series1.ChartType = SeriesChartType.Line
        'series1.BorderColor = new_color
        series1.BorderWidth = 1
        series1.Name = TextBox1.Text

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox1.Items.Add(Me.Chart1.Series(Me.Chart1.Series.Count - 1).Name)
        Me.Chart1.Series(TextBox1.Text).ToolTip = "aa"
        Dim lbpostion As Integer
        If Me.Chart1.Series(TextBox1.Text).Points.Count > 1 Then
            lbpostion = Me.Chart1.Series(TextBox1.Text).Points.Count / 2
        Else
            lbpostion = 0
        End If
        Me.Chart1.Series(TextBox1.Text).Points(lbpostion).Label = "HH"
        '==============================================================================================

    End Sub

    Private Sub btn_AddWield_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer
        Dim str_path As String
        Dim str_spath As String
        Dim lbarry() As Label = {Me.lb_SU, Me.lb_US, Me.lb_C1, Me.lb_CL1, Me.lb_C2 _
                                 , Me.lb_CL2, Me.lb_C3, Me.lb_ds, Me.lb_Ho, Me.lb_T2, Me.lb_T3, _
                                 Me.lb_T4, Me.lb_T5, Me.lb_T6}
        Try
            If ListBox2.Items.Count > 0 Then
                For x As Integer = ListBox2.Items.Count - 1 To 0 Step -1
                    Me.Chart1.Series.RemoveAt(Me.Chart1.Series.IndexOf(ListBox2.Items(x)))
                    ListBox2.Items.RemoveAt(x)
                Next
            End If

            count = 0
            Filenum = FreeFile()
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_path = OpenFileDialog1.FileName()
                str_spath = OpenFileDialog1.SafeFileName

                FileOpen(Filenum, str_path, OpenMode.Input)
                Do Until EOF(Filenum)
                    strTemp(count) = LineInput(Filenum)
                    count += 1
                Loop
                FileClose()
                Dim str_name(count - 1) As String
                Dim strValue(count - 1) As Double
                For i As Integer = 0 To count - 1
                    Dim temp() As String
                    temp = strTemp(i).Split(",")
                    str_name(i) = temp(0)
                    If temp(0) = "SU" Or temp(0) = "US" Or temp(0) = "C1" Or temp(0) = "CL1" _
                    Or temp(0) = "C2" Or temp(0) = "CL2" Or temp(0) = "C3" Or temp(0) = "ds" Or temp(0) = "Ho" Then
                        strValue(i) = temp(1) * 0.016
                    Else
                        strValue(i) = temp(1)
                    End If
                    If strValue(i) > 0 Then
                        AddWieldfile(str_name(i), strValue(i), "CK4")
                    End If
                    lbarry(i).Text = temp(1)
                Next
            Else
                MsgBox("nothing ..............")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddWieldfile(ByVal str_name As String, ByVal value As Double, ByVal seriesname As String)
        Dim series1 As New Series()
        Dim new_color As Color
        Dim seriesLastpoint As Double
        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum
        Dim y_min As Double = Me.Chart1.ChartAreas(0).AxisY.Minimum
        'If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        'new_color = Form1.ColorDialog1.Color
        'End If
        seriesLastpoint = TextBox2.Text
        If ListBox2.Items.Count > 0 Then
            Dim lastname As String = ListBox2.Items.Item(ListBox2.Items.Count - 1)
            Dim poincunt As Integer = Me.Chart1.Series(lastname).Points.Count
            seriesLastpoint = Math.Round(Me.Chart1.Series(lastname).Points(poincunt - 1).XValue, 4)
        End If
        ' Add data points to the first series 
        'For x As Double = x_min To tbx_AddXline.Text Step Me.Chart1.ChartAreas(0).AxisX.Interval

        For x As Double = seriesLastpoint To seriesLastpoint + value Step 0.001
            series1.Points.AddXY(x, tbx_wieldLineShift.Text)
        Next
        series1.ChartType = SeriesChartType.Line
        'series1.BorderColor = new_color
        series1.BorderWidth = 2
        series1.Name = str_name

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox2.Items.Add(str_name)
        Me.Chart1.Series(str_name).ToolTip = str_name
        Dim lbpostion As Integer
        If Me.Chart1.Series(str_name).Points.Count > 1 Then
            lbpostion = Me.Chart1.Series(str_name).Points.Count / 2
        Else
            lbpostion = 0
        End If
        Me.Chart1.Series(str_name).Points(lbpostion).Label = str_name
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To ListBox2.Items.Count - 1
            If i = ListBox2.SelectedIndex Then
                Dim j = Me.Chart1.Series.IndexOf(ListBox2.Items(i))
                Chart1.Series(j).BorderWidth = 3
            Else
                Chart1.Series(i).BorderWidth = 1
            End If

        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListBox2.Items.Count > 0 Then
            For x As Integer = ListBox2.Items.Count - 1 To 0 Step -1
                Me.Chart1.Series.RemoveAt(Me.Chart1.Series.IndexOf(ListBox2.Items(x)))
                ListBox2.Items.RemoveAt(x)
            Next
        End If
    End Sub

    Private Sub btn_opendatadir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_opendatadir.Click
        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath & "\b")
    End Sub

    Private Sub btn_readCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_readCSV.Click
        For ct = 0 To cutvalue.Length - 1
            cutvalue(ct) = ""
        Next
        cutvaluecount = 0
        Dim Filenum As Integer
        Dim str_path(1000) As String          '定義str_path為選取檔案路徑
        Dim str_spath(1000) As String         '定義str_spath為選取檔案名稱
        Dim def_path As String
        Try
            Filenum = FreeFile()
            def_path = RWini.Read("PATH", "DD", "para")
            OpenFileDialog1.Multiselect = True
            OpenFileDialog1.InitialDirectory = def_path
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_spath = OpenFileDialog1.SafeFileNames
                str_path = OpenFileDialog1.FileNames
                Dim tempath() As String
                Dim tempathR As String
                tempath = str_path(0).Split("\")
                For aa = 0 To tempath.Length - 2
                    tempathR = tempathR & tempath(aa) & "\"
                Next
                RWini.Write("PATH", "DD", tempathR, "para")
                For f = 0 To str_path.Length - 1
                    If str_path(f).EndsWith("txt") Or str_path(f).EndsWith("TXT") Then
                        readtxt(str_spath(f), str_path(f), f + 1)
                    ElseIf str_path(f).EndsWith("csv") Or str_path(f).EndsWith("CSV") Then
                        readcsv(str_spath(f), str_path(f), f + 1)
                    End If
                    'readcsv(str_spath(f), str_path(f), f + 1)
                Next
            Else
                MsgBox("nothing ......")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub readcsv(ByVal str_spath As String, ByVal str_path As String, ByVal drch As Integer)
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer            '定義count為資料筆數
        '定義str_path為選取檔案路徑
        '定義str_spath為選取檔案名稱
        Dim strtime() As Double
        Dim strValue() As Double
        Dim readline As Integer
        Dim stoplvdt As Double
        Try
            readline = 0
            count = 0
            lvdt_timescale = tbx_timescale.Text
            Dim lread As String
            Dim sr As New StreamReader(str_path)
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
                        strtime(readline - 2) = (readline - 2) / lvdt_timescale
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
            cutvalue(cutvaluecount) = stoplvdt
            cutvaluecount = cutvaluecount + 1
            If drch = 1 Then
                lb_lowc.Text = stoplvdt & vbCr
                chkChart()
                DrawingChart(strtime, strValue, str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
                ListBox2.Items.Clear()
            ElseIf drch > 1 Then
                lb_lowc.Text = lb_lowc.Text & vbCr & stoplvdt
                Addfile(strtime, strValue, str_spath)
                ListBox1.Items.Add(str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tbx_viewTime2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub tbx_timescale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lvdt_timescale = tbx_timescale.Text
    End Sub

    Private Sub tm_getDirFileCount_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tm_getDirFileCount.Tick
        lb_DirTarget.Text = RWini.Read("PATH", "DD", "para")
        Dim dirtemp() As String
        dirtemp = Directory.GetFiles(lb_DirTarget.Text)
        If dirtemp.Length <> Filecount Then
            lb_DirFilecount.Text = dirtemp.Length
            Filecount = dirtemp.Length
            ReDim autoFile(dirtemp.Length)
            autoFile = Directory.GetFiles(lb_DirTarget.Text)
            Dim tempname() As String
            tempname = autoFile(Filecount - 1).Split("\")
            readcsv(tempname(tempname.Length - 1), autoFile(Filecount - 1), 1)
        End If
    End Sub

    Private Sub btn_viewbig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_viewbig.Click
        Dim tempw As Double
        Dim temph As Double
        Dim gp_setw, gp_seth As Double
        Dim ct_setw, ct_seth As Double
        gp_seth = box_Grap.Height * 1.1
        gp_setw = box_Grap.Width * 1.1
        ct_seth = Me.Chart1.Height * 1.1
        ct_setw = Me.Chart1.Width * 1.1

        temph = gp_seth + box_Grap.Top + 20
        tempw = gp_setw + box_Grap.Left

        Dim aa = Screen.PrimaryScreen.WorkingArea.Height
        Dim bb = Screen.PrimaryScreen.WorkingArea.Width

        If temph > aa Then
            temph = aa
            Dim newb As Double
            newb = (temph - box_Grap.Top - 20) / box_Grap.Height
            Me.Height = aa
            box_Grap.Height = box_Grap.Height * newb
            Me.Chart1.Height = Me.Chart1.Height * newb
        Else

            If temph > Me.Height Then
                Me.Height = temph
            End If

            box_Grap.Height = gp_seth
            Me.Chart1.Height = ct_seth
        End If
        If tempw > bb Then
            tempw = bb
            Dim newb As Double
            newb = (tempw - box_Grap.Left) / box_Grap.Width
            Me.Width = bb
            box_Grap.Width = box_Grap.Width * newb
            Me.Chart1.Width = Me.Chart1.Width * newb
        Else
            If tempw > Me.Width Then
                Me.Width = tempw
            End If
            box_Grap.Width = gp_setw
            Me.Chart1.Width = ct_setw
        End If


    End Sub

    Private Sub btn_viewsmall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_viewsmall.Click
        Dim tempw As Double
        Dim temph As Double
        Dim gp_setw, gp_seth As Double
        Dim ct_setw, ct_seth As Double

        gp_seth = box_Grap.Size.Height / 1.1
        gp_setw = box_Grap.Width / 1.1

        ct_seth = Me.Chart1.Height / 1.1
        ct_setw = Me.Chart1.Width / 1.1
        temph = Me.Height / 1.1
        tempw = Me.Width / 1.1
        If gp_seth < grapH_source Then
            gp_seth = grapH_source
        End If
        If gp_setw < grapW_source Then
            gp_setw = grapW_source
        End If
        If ct_seth < chartH_source Then
            ct_seth = chartH_source
        End If
        If ct_setw < chartW_source Then
            ct_setw = chartW_source
        End If
        If temph < meh_source Then
            temph = meh_source
        End If
        If tempw < mew_source Then
            tempw = mew_source
        End If
        box_Grap.Height = gp_seth
        box_Grap.Width = gp_setw
        Me.Chart1.Height = ct_seth
        Me.Chart1.Width = ct_setw
        Me.Height = temph
        Me.Width = tempw
    End Sub

    Private Sub btn_viewmax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_viewmax.Click
        Dim tempw As Double
        Dim temph As Double

        Dim aa = Screen.PrimaryScreen.WorkingArea.Height
        Dim bb = Screen.PrimaryScreen.WorkingArea.Width


        Me.Location = New Point(0, 0)
        temph = aa
        Dim newb As Double
        newb = (temph - Me.TabControl1.Top - 20) / Me.TabControl1.Height
        Me.Height = aa
        Me.TabControl1.Height = Me.TabControl1.Height * newb
        box_Grap.Height = Me.TabControl1.Height - 20
        Me.Chart1.Height = Me.TabControl1.Height - 20

        tempw = bb
        newb = (tempw - Me.TabControl1.Left) / box_Grap.Width
        Me.Width = bb
        box_Grap.Width = box_Grap.Width * newb
        Me.Chart1.Width = Me.Chart1.Width * newb
        Me.TabControl1.Width = Me.TabControl1.Width * newb

    End Sub

    Private Sub btn_viewmini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_viewmini.Click
        box_Grap.Height = grapH_source
        box_Grap.Width = grapW_source
        Me.Chart1.Height = chartH_source
        Me.Chart1.Width = chartW_source
        Me.Height = meh_source
        Me.Width = mew_source
        Me.TabControl1.Height = tcH_source
        Me.TabControl1.Width = tcW_source

    End Sub

    Private Sub btn_autoread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_autoread.Click
        If auto_read = False Then
            tm_getDirFileCount.Enabled = True
            auto_read = True
            btn_autoread.Text = "自動中"
        Else
            tm_getDirFileCount.Enabled = False
            auto_read = False
            btn_autoread.Text = "自動"
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (tbx_viewTime2.Text < tbx_viewTime1.Text) Then
            tbx_viewTime2.Text = tbx_viewTime1.Text + 10
        End If
        Me.Chart1.ChartAreas(0).AxisX.Minimum = tbx_viewTime1.Text
        Me.Chart1.ChartAreas(0).AxisX.Maximum = tbx_viewTime2.Text
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim vv1, vv2 As Double
        vv1 = Val(tbx_viewValue1.Text)
        vv2 = Val(tbx_viewValue2.Text)
        If (vv2 < vv1) Then
            tbx_viewValue2.Text = tbx_viewValue1.Text + 0.5
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = tbx_viewValue1.Text
        Me.Chart1.ChartAreas(0).AxisY.Maximum = tbx_viewValue2.Text
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If
    End Sub

    Private Sub btn_AddLine_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddLine.Click
        Dim series1 As New Series()
        Dim new_color As Color
        If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            new_color = Form1.ColorDialog1.Color
        End If

        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum

        ' Add data points to the first series 
        For x As Double = x_min To x_max Step Me.Chart1.ChartAreas(0).AxisX.Interval
            series1.Points.AddXY(x, tbx_AddLine.Text)
        Next
        series1.ChartType = SeriesChartType.Line
        series1.BorderColor = new_color
        series1.BorderWidth = 1
        series1.Name = "基準" & tbx_AddLine.Text

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox1.Items.Add(Me.Chart1.Series(Me.Chart1.Series.Count - 1).Name)
    End Sub

    Private Sub btn_AddXline_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddXline.Click
        Dim series1 As New Series()
        Dim new_color As Color
        If Form1.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            new_color = Form1.ColorDialog1.Color
        End If

        Dim x_min As Double = Me.Chart1.ChartAreas(0).AxisX.Minimum
        Dim x_max As Double = Me.Chart1.ChartAreas(0).AxisX.Maximum
        Dim y_min As Double = Me.Chart1.ChartAreas(0).AxisY.Minimum

        ' Add data points to the first series 
        'For x As Double = x_min To tbx_AddXline.Text Step Me.Chart1.ChartAreas(0).AxisX.Interval
        For x As Double = x_min To tbx_AddXline.Text Step 0.01
            series1.Points.AddXY(x, y_min + 0.01)
        Next
        series1.ChartType = SeriesChartType.Line
        'series1.BorderColor = new_color
        series1.BorderWidth = 1
        series1.Name = TextBox1.Text

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        ListBox1.Items.Add(Me.Chart1.Series(Me.Chart1.Series.Count - 1).Name)
        Me.Chart1.Series(TextBox1.Text).ToolTip = "aa"
        Dim lbpostion As Integer
        If Me.Chart1.Series(TextBox1.Text).Points.Count > 1 Then
            lbpostion = Me.Chart1.Series(TextBox1.Text).Points.Count / 2
        Else
            lbpostion = 0
        End If
        Me.Chart1.Series(TextBox1.Text).Points(lbpostion).Label = "HH"
        '==============================================================================================
    End Sub

    Private Sub btn_Allview_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Allview.Click
        Me.Chart1.ChartAreas(0).AxisX.Minimum = 0
        Me.Chart1.ChartAreas(0).AxisX.Maximum = charttime.Max
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = charttime.Max
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = chartvalue.Min
        Me.Chart1.ChartAreas(0).AxisY.Maximum = chartvalue.Max
        tbx_viewValue1.Text = chartvalue.Min
        tbx_viewValue2.Text = chartvalue.Max
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If
    End Sub

    Private Sub btn_AddOtherFile_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddOtherFile.Click
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer
        Dim str_path As String
        Dim str_spath As String
        Try
            count = 0
            Filenum = FreeFile()
            OpenFileDialog1.InitialDirectory = "D:\"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_path = OpenFileDialog1.FileName()
                str_spath = OpenFileDialog1.SafeFileName
                If str_path.EndsWith("txt") Or str_path.EndsWith("TXT") Then
                    readtxt(str_spath, str_path, 2)
                ElseIf str_path.EndsWith("csv") Or str_path.EndsWith("CSV") Then
                    readcsv(str_spath, str_path, 2)
                End If
            Else
                MsgBox("nothing ...........")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_serdel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_serdel.Click
        Dim aa As String = ListBox1.SelectedItem
        If aa <> "" Then

            Chart1.Series.RemoveAt(Chart1.Series.IndexOf(ListBox1.SelectedItem))
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItems.Count > 1 Then
            For i As Integer = 0 To ListBox1.Items.Count - 1
                Chart1.Series(i).BorderWidth = 0
            Next
            For Each bb As Integer In ListBox1.SelectedIndices
                Chart1.Series(bb).BorderWidth = 1
            Next
        Else
            For i As Integer = 0 To ListBox1.Items.Count - 1
                If i = ListBox1.SelectedIndex Then
                    Chart1.Series(i).BorderWidth = 1
                    lb_listselectitem.Text = ListBox1.Items(i)
                    lb_listselectitem.Text = lb_listselectitem.Text & " - " & cutvalue(i)
                Else
                    Chart1.Series(i).BorderWidth = 0
                End If
            Next
        End If
        'lb_listselectitem.Text = ListBox1.Items(i)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For i As Integer = 0 To ListBox1.Items.Count - 1
            Chart1.Series(i).BorderWidth = 1
        Next
        lb_listselectitem.Text = "list selected"
    End Sub

    Private Sub tbx_viewValue2_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_viewValue2.TextChanged

    End Sub
End Class