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
Public Class Form2

    Dim conn As New SqlConnection
    Dim cmmd As New SqlCommand
    Dim strSQL As String
    Dim dset As New DataSet
    Dim dr As SqlDataReader
    Dim dc As SqlDataReader
    Dim c As String
    Dim DA As New SqlDataAdapter
    Dim Chart1 As New Chart()
    Dim timer1Count As Integer
    Dim lowvalue As Integer
    Dim hivalue As Integer
    Dim RecWielding As Boolean
    Dim AutoDrawCycle As Integer
    Dim ChartMaxTime As Double
    Dim ChartMinValue As Double
    Dim ChartMaxValue As Double

    Public cancelConnect As New Boolean
    Public mytcpclient As New TcpClient        '宣告 mytcpclient 
    Public myNetWorkStream As NetworkStream     '宣告 mynetworkstream
    Dim tcpConnectState As Boolean

    Dim RWini As New RWini()
    'Dim Chart1 As New Chart()
    'Dim chartArea1 As New ChartArea()

    Dim charttime() As Double
    Dim chartvalue() As Double

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
    Dim CkautoFile() As String
    Dim Filecount As Integer
    Dim CkFilecount As Integer
    Dim todayDate As String
    Dim todayDateY As String
    Dim todayDateM As String
    Dim todayDateD As String


    Private Sub Form2_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        tm_getDirFileCount.Enabled = False
        'MsgBox("close")
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Size = New Size(720, 435)
        box_Grap.Size = New Size(585, 300)
        Dim d1, d2 As String
        If Now.Date.Month < 10 Then
            d1 = "0" & Now.Date.Month
        Else
            d1 = Now.Date.Month
        End If
        If Now.Date.Day < 10 Then
            d2 = "0" & Now.Date.Day
        Else
            d2 = Now.Date.Day
        End If
        todayDateY = Now.Date.Year
        todayDateM = d1
        todayDateD = d2
        todayDate = Now.Date.Year & d1 & d2
        checkDirFile()
        meh_source = Me.Height
        mew_source = Me.Width
        tcH_source = Me.TabControl1.Height
        tcW_source = Me.TabControl1.Width
        AutoDrawCycle = 0
        tbx_AutoDrawCycle.Text = 1
        tbx_wieldLineShift.Text = 0
        TextBox2.Text = 0
        ComboBox1.Items.Clear()
        For i As Integer = 1 To 5
            ComboBox1.Items.Add(i)
        Next
        
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle

        'connection(RWini.Read("PLC", "IP", "para"), RWini.Read("PLC", "PORT", "para"))
        'Timer3.Enabled = True
        'Me.Chart1.ChartAreas.Clear() 
        'Me.Chart1.ChartAreas.Add(chartArea1)
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

        For i As Integer = 0 To tempvalue.Length - 1
            tempvalue(i) = chartvalue(i + outtime - 1)
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
        lb_highsp.Text = Format(high_sp, "#0.0##")
        lb_lowsp.Text = Format(low_sp, "#0.0##")

        For x As Double = 0 To time.Length - 1
            If (value(x) > 0) Then

            End If
            series1.Points.AddXY(time(x), Format(value(x), "##.###"))
        Next
        series1.ChartType = SeriesChartType.Line
        series1.BorderColor = Color.Black
        series1.BorderWidth = 1
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = time(time.Length - 1) + 0.4
        'tbx_viewValue1.Text = value.Min
        tbx_viewValue1.Text = tempvalue.Min - 0.03
        tbx_viewValue2.Text = tempvalue.Max + 0.03

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
        Me.Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "{0:0.0}"
        Me.Chart1.ChartAreas(0).AxisX.Interval = 0.2
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
        ChartMaxTime = charttime.Max
        ChartMinValue = chartvalue.Min
        ChartMaxValue = chartvalue.Max

        chartH_source = Me.Chart1.Height
        chartW_source = Me.Chart1.Width
        grapH_source = box_Grap.Height
        grapW_source = box_Grap.Width

        lb_Serialname.Text = seriesname

    End Sub
    Public Sub ClearChart()
        Me.Chart1.Series.Clear()
        Me.Chart1.ChartAreas.Clear()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Chart1.ChartAreas(0).AxisX.Minimum = tbx_viewTime1.Text
        Me.Chart1.ChartAreas(0).AxisX.Maximum = tbx_viewTime2.Text
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                str_path = OpenFileDialog1.FileName()
                str_spath = OpenFileDialog1.SafeFileName

                FileOpen(Filenum, str_path, OpenMode.Input)
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

                lb_lowc.Text = lb_lowc.Text & "," & vbCr & strTemp(count - 1)
                Addfile(strtime, strValue, str_spath)
                ListBox1.Items.Add(str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
            Else
                MsgBox("nothing ...........")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub Addfile(ByVal time() As Double, ByVal value() As Double, ByVal seriesname As String)
        charttime = time
        chartvalue = value
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

        ChartMaxTime = charttime.Max
        ChartMinValue = chartvalue.Min
        ChartMaxValue = chartvalue.Max

        ' Add series to the chart  
        Me.Chart1.Series.Add(series1)
        'auto adj chart
        If ChartMaxTime > Me.Chart1.ChartAreas(0).AxisX.Maximum Then
            Me.Chart1.ChartAreas(0).AxisX.Maximum = ChartMaxTime + 0.4
        End If
        If ChartMinValue < Me.Chart1.ChartAreas(0).AxisY.Minimum Then
            Me.Chart1.ChartAreas(0).AxisY.Minimum = ChartMinValue - 0.03
        End If
        If ChartMaxValue > Me.Chart1.ChartAreas(0).AxisY.Maximum Then
            Me.Chart1.ChartAreas(0).AxisY.Maximum = ChartMaxValue + 0.03
        End If
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

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            tbx_viewValue2.Text = tbx_viewValue1.Text + 0.2
        End If
    End Sub

    Private Sub tbx_viewValue2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (tbx_viewValue1.Text > tbx_viewValue2.Text) Then
            tbx_viewValue1.Text = tbx_viewValue2.Text - 0.2
        End If
    End Sub

    Private Sub btn_Allview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Chart1.ChartAreas(0).AxisX.Minimum = 0
        Me.Chart1.ChartAreas(0).AxisX.Maximum = charttime.Max + 1
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = charttime.Max
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = chartvalue.Min - 0.1
        Me.Chart1.ChartAreas(0).AxisY.Maximum = chartvalue.Max + 0.1
        tbx_viewValue1.Text = chartvalue.Min
        tbx_viewValue2.Text = chartvalue.Max
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If
    End Sub

    Private Sub btn_viewdetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_viewdetail.Click
        ' Dim tempvalue() As Double
        Dim outtime As Double
        'outtime = tbx_movetime.Text * 10
        outtime = RWini.Read("para", "出牙輪時間", "para") * 10
        Dim tempvalue(charttime.Length - outtime) As Double

        For i As Integer = 0 To tempvalue.Length - 1
            tempvalue(i) = chartvalue(i + outtime - 1)
        Next

        Me.Chart1.ChartAreas(0).AxisX.Minimum = RWini.Read("para", "出牙輪時間", "para")
        Me.Chart1.ChartAreas(0).AxisX.Maximum = charttime.Max
        tbx_viewTime1.Text = RWini.Read("para", "出牙輪時間", "para")
        tbx_viewTime2.Text = charttime.Max
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = tempvalue.Min
        Me.Chart1.ChartAreas(0).AxisY.Maximum = tempvalue.Max
        tbx_viewValue1.Text = tempvalue.Min
        tbx_viewValue2.Text = tempvalue.Max
        If tbx_ValuePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisY.Interval = tbx_ValuePitch.Text
        End If


    End Sub

    Private Sub btn_paramform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_paramform.Click
        form_param.Show()
    End Sub

    Public Sub readtxt()
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
                lb_lowc.Text = strTemp(count - 1)
                chkChart()
                DrawingChart(strtime, strValue, str_spath)
                tbx_wieldLineShift.Text = Me.Chart1.ChartAreas(0).AxisY.Minimum
                ListBox2.Items.Clear()
            Else
                MsgBox("nothing ......")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Sub btn_ReadDatatxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReadDatatxt.Click
        readtxt()

    End Sub

    Public Sub checkFile()
        Dim sss As String
        Dim strpath As String
        Dim yymmdd As String
        Dim sourcepath As String
        Dim targetpath As String
        Dim direxists As Boolean
        Dim dirpath As String
        sss = Date.Now.ToString("yyyy-MM-dd-HHmmss")
        yymmdd = Date.Now.ToString("yyyy-MM-dd")
        dirpath = Application.StartupPath & "\..\..\..\b\" & Me.Text & "\" & yymmdd
        targetpath = dirpath & "\" & sss & ".txt"
        direxists = System.IO.Directory.Exists(dirpath)
        If direxists Then
        Else
            System.IO.Directory.CreateDirectory(dirpath)
        End If
        FileClose()
        strpath = Application.StartupPath & "\test.txt"
        FileCopy(strpath, targetpath)
        'FileCopy("D:\test1.txt", "D:\b\中斷值" & sss & ".txt")
        FileClose()
        Dim Filenum As Integer

        Filenum = FreeFile()
        FileOpen(Filenum, strpath, OpenMode.Output)

        Print(Filenum, "")
        FileClose()
        'FileOpen(Filenum, "D:\test1.txt", OpenMode.Output)

        'Print(Filenum, "")
        'FileClose()

    End Sub

    Public Sub readtesttxt()
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer
        Dim str_path As String
        Dim strpath As String

        strpath = Application.StartupPath & "\test.txt"

        'Filenum = FreeFile()
        'FileOpen(Filenum, strpath, OpenMode.Append)


        Try
            count = 0
            Filenum = FreeFile()
            'If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            str_path = strpath
            'End If
            FileOpen(Filenum, str_path, OpenMode.Input)
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
            lb_lowc.Text = strTemp(count - 1)
            '新增焊接資料累加顯示
            If AutoDrawCycle = 0 Then
                chkChart()
                DrawingChart(strtime, strValue, "now")
            ElseIf AutoDrawCycle < tbx_AutoDrawCycle.Text And AutoDrawCycle > 0 Then
                Addfile(strtime, strValue, AutoDrawCycle)
            End If
            lb_AutoDrawCycle.Text = AutoDrawCycle + 1
            'DrawingChart(strtime, strValue, "now")
            AutoDrawCycle = AutoDrawCycle + 1
            If AutoDrawCycle >= tbx_AutoDrawCycle.Text Then
                AutoDrawCycle = 0
            End If

            FileClose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Sub WriteData(ByVal str_comm As String)
        Try
            Dim myBytes() As Byte = Encoding.Default.GetBytes(str_comm)
            Dim MYBYTES16(myBytes.Length) As Byte
            Dim myChar() As Char = str_comm.ToCharArray
            Dim test As String
            Dim i As Integer
            test = "RD DM3000.L"

            i = 0

            If (mytcpclient.Connected = False) Then
                mytcpclient.Connect(RWini.Read("PLC", "IP", "para"), Integer.Parse(RWini.Read("PLC", "PORT", "para")))
                myNetWorkStream = mytcpclient.GetStream
            End If
            If (mytcpclient.Connected = True) Then
                If myNetWorkStream.CanWrite Then
                    myNetWorkStream.Write(myBytes, 0, myBytes.Length)
                End If
                For Each b As Byte In myBytes
                    'myNetWorkStream.WriteByte(Convert.ToByte(Hex(b)))
                    'tbx_TcpResult.Text = tbx_TcpResult.Text & vbCrLf & Convert.ToByte(Hex(b))
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub
    Public Sub ReadData()
        'MsgBox(mytcpclient.Available)
        Dim bufferSize As Integer = mytcpclient.Available
        Dim myBufferBytes(mytcpclient.Available - 1) As Byte
        Dim strContent As String
        If mytcpclient.Connected = True Then
            If mytcpclient.Available > 0 Then
                myNetWorkStream.Read(myBufferBytes, 0, bufferSize)
                strContent = Encoding.Default.GetString(myBufferBytes, 0, bufferSize)
                'tbx_TcpResult.Text = tbx_TcpResult.Text & strContent
                tbx_TcpResult.Text = strContent
            End If
        Else
            MsgBox("DISCONNECT !!!")
        End If
    End Sub

    Public Sub ReadTotxt()
        Dim Filenum As Integer
        Dim strTemp As String
        Dim b As Byte
        Dim overl As Integer
        Dim strpath As String

        strpath = Application.StartupPath & "\test.txt"

        Filenum = FreeFile()
        FileOpen(Filenum, strpath, OpenMode.Append)

        Dim bufferSize As Integer = mytcpclient.Available
        Dim myBufferBytes(mytcpclient.Available - 1) As Byte
        Dim strContent As String
        If mytcpclient.Connected = True Then
            If mytcpclient.Available > 0 Then

                strTemp = timer1Count / 10
                myNetWorkStream.Read(myBufferBytes, 0, bufferSize)
                overl = bufferSize - 13
                If (overl = 0) Then
                    strContent = Encoding.Default.GetString(myBufferBytes, 0, bufferSize)
                ElseIf (bufferSize > 0) Then
                    strContent = Encoding.Default.GetString(myBufferBytes, overl, bufferSize - overl)
                Else

                End If

                strTemp = strTemp & "," & Integer.Parse(strContent) / 1000
                PrintLine(Filenum, strTemp)
            End If
        Else
            'MsgBox("DISCONNECT !!!")
        End If
        FileClose()

    End Sub
    Public Sub ReadWieldValue()
        WriteData("RD DM3010.L" & vbCr)
        'ReadTotxt()
        Threading.Thread.Sleep(10)
        ReadTotxt1("中斷讀值", "lo")
    End Sub
    Public Sub ReadAllLVDT(ByVal rdlvdtcom As String)
        Dim Filenum As Integer
        Dim strTemp As String
        Dim b As Byte
        Dim overl As Integer
        Dim strpath As String
        Dim wieldTime As String
        Try
            wieldTime = tbx_TcpResult.Text
            WriteData(rdlvdtcom & vbCr)
            Threading.Thread.Sleep(10)
            ReadData()

            strpath = Application.StartupPath & "\test.txt"

            Filenum = FreeFile()
            FileOpen(Filenum, strpath, OpenMode.Append)

            Dim templvdtValue As String = tbx_TcpResult.Text

            Dim LVDTvalue() As String
            LVDTvalue = Split(templvdtValue, " ")
            For ii As Integer = 0 To LVDTvalue.Length - 1
                strTemp = ii / 10 & "," & Integer.Parse(LVDTvalue(ii)) / 1000
                PrintLine(Filenum, strTemp)
            Next
            Threading.Thread.Sleep(10)
            FileClose()
        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Sub
    Public Sub ReadTotxt1(ByVal Dataname As String, ByVal lowhi As String)
        Dim Filenum As Integer
        Dim strTemp As String
        Dim b As Byte
        Dim overl As Integer
        Dim strpath As String

        strpath = Application.StartupPath & "\test.txt"

        Filenum = FreeFile()
        FileOpen(Filenum, strpath, OpenMode.Append)

        Dim bufferSize As Integer = mytcpclient.Available
        Dim myBufferBytes(mytcpclient.Available - 1) As Byte
        Dim strContent As String
        If mytcpclient.Connected = True Then
            If mytcpclient.Available > 0 Then

                'strTemp = timer1Count / 10
                myNetWorkStream.Read(myBufferBytes, 0, bufferSize)
                overl = bufferSize - 13
                If (overl = 0) Then
                    strContent = Encoding.Default.GetString(myBufferBytes, 0, bufferSize)
                ElseIf (bufferSize > 0) Then
                    strContent = Encoding.Default.GetString(myBufferBytes, overl, bufferSize - overl)
                Else

                End If
                Dim inttemp = Integer.Parse(strContent) / 1000
                strTemp = Integer.Parse(strContent) / 1000
                lowvalue = inttemp
                PrintLine(Filenum, strTemp)
            End If
        Else
            'MsgBox("DISCONNECT !!!")
        End If
        FileClose()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'WriteData("RD DM3000.L" & vbCr)
        'ReadTotxt()
        'Threading.Thread.Sleep(10)
        'ReadTotxt()
        'Threading.Thread.Sleep(100)
        timer1Count = timer1Count + 1
        lbl_timer2.Text = timer1Count / 10
        WriteData("RD R13007" & vbCr)
        Threading.Thread.Sleep(10)
        ReadData()
        'Threading.Thread.Sleep(100)
        If tbx_TcpResult.Text <> "" And IsNumeric(tbx_TcpResult.Text) Then
            If tbx_TcpResult.Text = 1 Then
                Timer1.Enabled = False
                Timer2.Enabled = False
                WriteData("RD DM3030.L" & vbCr)
                Threading.Thread.Sleep(10)
                ReadData()
                Dim tempwieldtime = tbx_TcpResult.Text
                If tempwieldtime < 1 Then
                    tempwieldtime = 1
                End If
                Dim temptxt = "RDS DM3300.L " & tempwieldtime * 1
                'WriteData(temptxt & vbCr)
                'Threading.Thread.Sleep(10)
                ReadAllLVDT(temptxt)
                ReadWieldValue()
                readtesttxt()
                checkFile()
                Timer3.Enabled = True
            End If
        End If
        lb_RECstate.Text = "焊接資料記錄中！！"
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        RecWielding = False
        MsgBox("異常發生造成取樣錯誤(設定為60秒需取樣完成)")
        lb_RECstate.Text = "異常發生已暫停記錄"
        'lbl_timer2.Text = Timer2.ToString
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If mytcpclient.Connected = True Then
            WriteData("RD MR2712" & vbCr)
            Threading.Thread.Sleep(10)
            ReadData()
        End If
        'Threading.Thread.Sleep(100)
        If tbx_TcpResult.Text <> "" And IsNumeric(tbx_TcpResult.Text) Then
            If tbx_TcpResult.Text = 1 Then
                'checkFile()
                timer1Count = 0
                Timer1.Interval = 100
                Timer1.Enabled = True
                Timer2.Interval = 60000
                Timer2.Enabled = True
                Timer3.Enabled = False
            End If
        End If
    End Sub

    Public Sub connection(ByVal str_ipAddress As String, ByVal int_port As Integer)
        Try
            'str_ipAddress = Format(str_ipAddress.ToString, "000.000.000.000")
            connectting.Show()
            Dim iptemp() As String
            Dim str_ip As String
            iptemp = str_ipAddress.Split(".")
            For i As Integer = 0 To 3
                If i = 3 Then
                    str_ip = str_ip & iptemp(i)
                Else
                    str_ip = str_ip & iptemp(i) & "."
                End If

            Next
            lb_iptext.Text = str_ip
            'Me.Text = str_ip
            str_ip = lb_iptext.Text
            'str_ip = "192.168.0.10"
            'If cancelConnect = True Then
            mytcpclient.Connect(str_ip, int_port)
            'End If

            If (mytcpclient.Connected) Then
                tcpConnectState = True
                myNetWorkStream = mytcpclient.GetStream()
                'lbl_connectState.Text = "Connect ok"
                plcconnect_light.FillColor = Color.Green
            Else
                'lbl_connectState.Text = "Not Connect"
                plcconnect_light.FillColor = Color.Red
                lb_iptext.Text = str_ip & "  connect fail"
            End If
            ' If cancelConnect = True And mytcpclient.Connected Then
            connectting.Close()
            'End If
        Catch ex As ArgumentOutOfRangeException
            'lbl_connectState.Text = "Not Connect"
            plcconnect_light.FillColor = Color.Red
            connectting.Close()
            MessageBox.Show("ArgumentOutOfRangeException 例外 :" + vbCrLf + ex.Message, _
                "Port error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As SocketException
            'lbl_connectState.Text = "Not Connect"
            plcconnect_light.FillColor = Color.Red
            connectting.Close()
            MessageBox.Show("SocketException 例外 :" + vbCrLf + ex.Message, _
            "Connect error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Function ckResult()
        Dim tempmax, tempmin, tempck As Double
        Dim peakmax, peakmin As Double
        Dim limitmax, limitmin As Double
        Dim outtime As Double

        'outtime = RWini.Read("para", "出牙輪時間", "para") * 10
        If outtime = 0 Then
            outtime = 1
        End If

        Dim ttp As Integer = charttime.Length - outtime
        Dim tempvalue(ttp) As Double

        For i As Integer = 0 To tempvalue.Length - 1
            tempvalue(i) = chartvalue(i + outtime - 1)
        Next

        For i = 3 To tempvalue.Length - 4
            If i = 3 Then
                tempmax = tempvalue(i)
                tempmin = tempvalue(i)
            Else
                If tempvalue(i) > tempmax Then
                    tempmax = tempvalue(i)
                ElseIf tempvalue(i) < tempmin Then
                    tempmin = tempvalue(i)
                End If
            End If

        Next

        peakmax = tempmax
        peakmin = tempmin

        'peakmax = Me.Chart1.Series(0)..AxisY.Maximum
        'peakmin = Me.Chart1.ChartAreas(0).AxisY.Minimum
        For j As Integer = 0 To tempvalue.Length

        Next

        lb_peakMAX.Text = peakmax
        lb_peakMIN.Text = peakmin

        limitmax = RWini.Read("para", "maxlimit", "para")
        limitmin = RWini.Read("para", "minlimit", "para")

        If peakmax < limitmax And peakmax > limitmin And peakmin > limitmin And peakmin < limitmax Then
            lb_ckResult.Text = "PASS"
            lb_ckResult.ForeColor = Color.Green
        Else
            lb_ckResult.Text = "NG"
            lb_ckResult.ForeColor = Color.Red
        End If


    End Function

    Private Sub btn_PLCform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PLCform.Click
        Form1.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If mytcpclient.Connected Then
            Timer3.Interval = 10
            Timer3.Enabled = True
            lb_RECstate.Text = "記錄啟用，等待焊接..."
            RecWielding = True
            Button4.Enabled = False
        Else
            MsgBox("PLC未連線，請先connect")
        End If
    End Sub

    Private Sub btn_StopREC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_StopREC.Click
        RecWielding = False
        Timer3.Enabled = False
        lb_RECstate.Text = "已停止記錄！！"
        Button4.Enabled = True
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

    Private Sub btn_ConnectGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ConnectGo.Click
        'connection(RWini.Read("PLC", "IP", "para"), RWini.Read("PLC", "PORT", "para"))
        'connection("192.168.14.10", "8501")
        mytcpclient = New TcpClient        '宣告 mytcpclient 
        'myNetWorkStream = New NetworkStream
        connection(RWini.Read("PLC", Me.Text, "para"), RWini.Read("PLC", "PORT", "para"))
    End Sub

    Private Sub btn_CancelConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CancelConnect.Click
        If mytcpclient.Connected Then
            mytcpclient.Close()
            tcpConnectState = False
            plcconnect_light.FillColor = Color.Red
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
        'System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath & "\..\..\..\")
        System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath & "D:\LVDT-Data")

    End Sub

    Private Sub btn_ReadWieldTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReadWieldTime.Click
        If RecWielding = True Then
            MsgBox("資料即時取樣中，請先停止即時取樣後再試一次")
        ElseIf RecWielding = False Then
            If mytcpclient.Connected Then
                WriteData("RD DM3018" & vbCr)
                Threading.Thread.Sleep(10)
                ReadData()
                Dim totalDatanum = CInt(tbx_TcpResult.Text)
                If totalDatanum < 1 Then
                    MsgBox("NoDATA")
                Else
                    ReadWieldTime(totalDatanum)
                End If

            End If
        End If
    End Sub
    Public Sub ReadWieldTime(ByVal totaldatanum As Integer)
        Dim Filenum As Integer
        Dim strTemp As String
        Dim sourcepath As String
        btn_ReadWieldTime.Text = "時間擷取中..."
        btn_ReadWieldTime.Enabled = False
        Try
            Dim readcyclecount = totaldatanum \ 100

            sourcepath = Application.StartupPath & "\temptime.txt"
            Filenum = FreeFile()
            FileOpen(Filenum, sourcepath, OpenMode.Output)

            Print(Filenum, "")
            FileClose()

            FileOpen(Filenum, sourcepath, OpenMode.Append)
            For aa As Integer = 0 To readcyclecount
                Threading.Thread.Sleep(10)
                Dim setdmnum = 15006 + aa * 600
                Dim setrdnum = 1
                If aa < readcyclecount Then
                    setrdnum = 600
                ElseIf aa = readcyclecount Then
                    setrdnum = (totaldatanum - (aa * 100)) * 6
                End If
                WriteData("RDS DM" & setdmnum & " " & setrdnum & vbCr)
                Threading.Thread.Sleep(100)
                ReadData()
                Threading.Thread.Sleep(100)
                Dim temprdvalue As String = tbx_TcpResult.Text
                Dim wieldtime() As String
                wieldtime = Split(temprdvalue, " ")
                Threading.Thread.Sleep(100)
                For ii As Integer = 0 To wieldtime.Length - 6 Step 6
                    Dim wieldalltime = wieldtime(ii).ToString / 10
                    Dim lvdttime = wieldtime(ii + 1).ToString / 10
                    Dim recmm = wieldtime(ii + 2).ToString / 1
                    Dim recdd = wieldtime(ii + 3).ToString / 1
                    Dim rechh = wieldtime(ii + 4).ToString / 1
                    Dim recmin = wieldtime(ii + 5).ToString \ 60
                    Dim recss = wieldtime(ii + 5).ToString - (recmin * 60)

                    strTemp = recmm & "-" & recdd & "-" & rechh & ":" & recmin & ":" & recss & "," & wieldalltime & "," & lvdttime
                    PrintLine(Filenum, strTemp)
                Next
                Threading.Thread.Sleep(10)
            Next
            FileClose()
            wieldtimeFilecopy()
            btn_ReadWieldTime.Text = "焊接時間取"
            btn_ReadWieldTime.Enabled = True
        Catch ex As Exception
            btn_ReadWieldTime.Text = "焊接時間取"
            btn_ReadWieldTime.Enabled = True
            MsgBox(ex)
        End Try

    End Sub
    Public Sub wieldtimeFilecopy()
        Dim sss As String
        Dim yymmdd As String
        Dim sourcepath As String
        Dim targetpath As String
        Dim direxists As Boolean
        Dim dirpath As String
        yymmdd = Date.Now.ToString("yyyy-MM-dd")
        sss = Date.Now.ToString("yyyy-MM-dd-HHmmss")
        dirpath = Application.StartupPath & "\..\..\..\WieldTimeREC\" & Me.Text & "\" & yymmdd
        MsgBox(dirpath)
        direxists = System.IO.Directory.Exists(dirpath)
        If direxists Then
        Else
            System.IO.Directory.CreateDirectory(dirpath)
        End If
        FileClose()
        sourcepath = Application.StartupPath & "\temptime.txt"
        targetpath = dirpath & "\" & sss & "-wieldtime.txt"

        FileCopy(sourcepath, targetpath)
        FileClose()
        Dim Filenum As Integer

        Filenum = FreeFile()
        FileOpen(Filenum, sourcepath, OpenMode.Output)

        Print(Filenum, "")
        FileClose()

        MsgBox("資料取樣完成！！" & vbCr & "資料存於：" & targetpath)

    End Sub

    Private Sub tbx_AutoDrawCycle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_AutoDrawCycle.TextChanged
        If tbx_AutoDrawCycle.Text <= 0 Then
            tbx_AutoDrawCycle.Text = 1
        End If
    End Sub

    Private Sub btn_autoread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_autoread.Click
        lb_DirTarget.Text = RWini.Read("PATH", Me.Text, "para")
        lb_DirckTarget.Text = RWini.Read("CKPATH", Me.Text, "para")
        Dim dirtemp() As String
        Dim ckdirtemp() As String
        dirtemp = Directory.GetFiles(lb_DirTarget.Text)
        ckdirtemp = Directory.GetFiles(lb_DirckTarget.Text)
        Filecount = dirtemp.Length
        CkFilecount = ckdirtemp.Length
        If auto_read = False Then
            tm_getDirFileCount.Enabled = True
            auto_read = True
            btn_autoread.Text = "記錄中"
        Else
            tm_getDirFileCount.Enabled = False
            auto_read = False
            btn_autoread.Text = "停止中"
        End If
    End Sub

    Private Sub tm_getDirFileCount_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tm_getDirFileCount.Tick
        lb_DirTarget.Text = RWini.Read("PATH", Me.Text, "para")
        lb_DirckTarget.Text = RWini.Read("CKPATH", Me.Text, "para")
        Dim dirtemp() As String
        Dim ckdirtemp() As String
        dirtemp = Directory.GetFiles(lb_DirTarget.Text)
        Label41.Text = "檔案數量: " & dirtemp.Length
        '畫LVDT檔案曲線
        If dirtemp.Length <> Filecount And dirtemp.Length > 0 Then
            'lb_DirFilecount.Text = dirtemp.Length
            Filecount = dirtemp.Length
            ReDim autoFile(dirtemp.Length)
            autoFile = Directory.GetFiles(lb_DirTarget.Text)
            Dim tempname() As String
            tempname = autoFile(Filecount - 1).Split("\")
            'readcsv(tempname(tempname.Length - 1), autoFile(Filecount - 1), 1)
            '新增焊接資料累加顯示
            If AutoDrawCycle = 0 Then
                'chkChart()
                readcsv(tempname(tempname.Length - 1), autoFile(Filecount - 1), AutoDrawCycle + 1)
            ElseIf AutoDrawCycle < tbx_AutoDrawCycle.Text And AutoDrawCycle > 0 Then
                readcsv(tempname(tempname.Length - 1), autoFile(Filecount - 1), AutoDrawCycle + 1)
            End If
            lb_AutoDrawCycle.Text = AutoDrawCycle + 1
            'DrawingChart(strtime, strValue, "now")
            AutoDrawCycle = AutoDrawCycle + 1
            If AutoDrawCycle >= tbx_AutoDrawCycle.Text Then
                AutoDrawCycle = 0
            End If
        End If
        '畫比對LVDT曲線
        ckdirtemp = Directory.GetFiles(lb_DirckTarget.Text)
        If ckdirtemp.Length <> CkFilecount And ckdirtemp.Length > 0 Then
            'lb_DirFilecount.Text = dirtemp.Length
            CkFilecount = ckdirtemp.Length
            ReDim CkautoFile(ckdirtemp.Length)
            CkautoFile = Directory.GetFiles(lb_DirckTarget.Text)
            Dim tempname() As String
            tempname = CkautoFile(CkFilecount - 1).Split("\")
            'readcsv(tempname(tempname.Length - 1), autoFile(Filecount - 1), 1)
            '新增焊接資料累加顯示
            readcsv(tempname(tempname.Length - 1), CkautoFile(Filecount - 1), 2)
        End If
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
            If ex.Message.Contains("正在使用檔案") Then
                Filecount = Filecount - 1
            Else
                MsgBox(ex.Message)
            End If

        End Try
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

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Chart1.ChartAreas(0).AxisX.Minimum = tbx_viewTime1.Text
        Me.Chart1.ChartAreas(0).AxisX.Maximum = tbx_viewTime2.Text
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
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
        Me.Chart1.ChartAreas(0).AxisX.Maximum = charttime.Max + 1
        tbx_viewTime1.Text = 0
        tbx_viewTime2.Text = charttime.Max
        If tbx_ViewTimePitch.Text = 0 Then
            Me.Chart1.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Else
            Me.Chart1.ChartAreas(0).AxisX.Interval = tbx_ViewTimePitch.Text
        End If
        Me.Chart1.ChartAreas(0).AxisY.Minimum = chartvalue.Min - 0.1
        Me.Chart1.ChartAreas(0).AxisY.Maximum = chartvalue.Max + 0.1
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

    Private Sub btn_serdel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_serdel.Click
        Dim aa As String = ListBox1.SelectedItem
        If aa <> "" Then

            Chart1.Series.RemoveAt(Chart1.Series.IndexOf(ListBox1.SelectedItem))
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub btn_AddWield_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddWield.Click
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

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListBox2.Items.Count > 0 Then
            For x As Integer = ListBox2.Items.Count - 1 To 0 Step -1
                Me.Chart1.Series.RemoveAt(Me.Chart1.Series.IndexOf(ListBox2.Items(x)))
                ListBox2.Items.RemoveAt(x)
            Next
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
                Else
                    Chart1.Series(i).BorderWidth = 0
                End If
            Next
        End If
        lb_Serialname.Text = ListBox1.SelectedItem
    End Sub

    Private Sub ListBox2_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        For i As Integer = 0 To ListBox2.Items.Count - 1
            If i = ListBox2.SelectedIndex Then
                Dim j = Me.Chart1.Series.IndexOf(ListBox2.Items(i))
                Chart1.Series(j).BorderWidth = 3
            Else
                Chart1.Series(i).BorderWidth = 1
            End If

        Next
    End Sub
    Private Sub checkDirFile()
        Dim test() As String
        Dim iii = 0
        Dim ii = 0
        Dim newdir, rootdir, newname As String
        Dim setdate, setdatey, setdatem, setdated As String
        Dim clickTime As String
        Dim mano As String
        Dim allfilename() As String
        Dim allfilepath() As String
        Dim allfileDD() As String

        Dim cktest() As String
        Dim ckiii = 0
        Dim ckii = 0
        Dim cknewdir, ckrootdir, cknewname As String
        Dim ckallfilename() As String
        Dim ckallfilepath() As String
        Dim ckallfileDD() As String
        Try
            clickTime = TimeOfDay.Hour & "-" & TimeOfDay.Minute & "-" & TimeOfDay.Second
            mano = Me.Text
            rootdir = "D:\LVDT-Data\" & mano & "\wield-LVDT\"
            '找出資料夾中有多少 *.csv 檔，且將數量放入iii
            For Each ff As String In My.Computer.FileSystem.GetFiles(rootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_*.csv")
                iii = iii + 1
            Next
            '重新定義allfilename、allfilepath、allfileDD的陣列長度
            ReDim allfilename(iii - 1)
            ReDim allfilepath(iii - 1)
            ReDim test(iii - 1)
            iii = 0
            ii = 0
            '整理LVDT檔案資料夾，保留今日資料，舊資料歸檔
            For Each ff As String In My.Computer.FileSystem.GetFiles(rootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_*.csv")
                Dim temp(), temp2() As String
                Dim temp2a As String
                Dim nname As Boolean
                temp = ff.Split("\")
                'temp(temp.length-1) 取檔案名稱
                temp2 = temp(temp.Length - 1).Split("_")
                'temp2 取 NO1-LVDT、月日時分秒.csv
                temp2a = temp2(temp2.Length - 1).Substring(0, 4)
                'temp2a 取月日
                If ii = 0 Then
                    'ii =0 為第一筆資料
                    test(ii) = temp2a
                    'test 月日 檔案 陣列
                    ii = ii + 1
                Else

                    nname = False
                    For ai As Integer = 0 To ii - 1
                        If test(ai) = temp2a Then
                            nname = False
                        Else
                            nname = True
                        End If
                    Next
                End If
                If nname Then
                    test(ii) = temp2a
                    ii = ii + 1
                End If
                allfilepath(iii) = ff
                allfilename(iii) = temp(temp.Length - 1)
                'My.Computer.FileSystem.MoveFile(ff, newdir & "\s\" & temp(temp.Length - 1))
                iii = iii + 1
            Next
            '整理結束
            ckrootdir = "D:\LVDT-Data\" & mano & "\wield-LVDT-ck\"
            '找出資料夾中有多少 *.csv 檔，且將數量放入iii
            For Each ff As String In My.Computer.FileSystem.GetFiles(ckrootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_*.csv")
                ckiii = ckiii + 1
            Next
            '重新定義allfilename、allfilepath、allfileDD的陣列長度
            ReDim ckallfilename(ckiii - 1)
            ReDim ckallfilepath(ckiii - 1)
            ReDim cktest(ckiii - 1)
            ckiii = 0
            ckii = 0
            '整理 比對檔案目錄 XXX-CK
            For Each ff As String In My.Computer.FileSystem.GetFiles(ckrootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_*.csv")
                Dim temp(), temp2() As String
                Dim temp2a As String
                Dim nname As Boolean
                temp = ff.Split("\")
                'temp(temp.length-1) 取檔案名稱
                temp2 = temp(temp.Length - 1).Split("_")
                'temp2 取 NO1-LVDT、月日時分秒.csv
                temp2a = temp2(temp2.Length - 1).Substring(0, 4)
                'temp2a 取月日
                If ckii = 0 Then
                    'ii =0 為第一筆資料
                    cktest(ckii) = temp2a
                    'test 月日 檔案 陣列
                    ckii = ckii + 1
                Else

                    nname = False
                    For ai As Integer = 0 To ckii - 1
                        If cktest(ai) = temp2a Then
                            nname = False
                        Else
                            nname = True
                        End If
                    Next
                End If
                If nname Then
                    cktest(ckii) = temp2a
                    ckii = ckii + 1
                End If
                ckallfilepath(ckiii) = ff
                ckallfilename(ckiii) = temp(temp.Length - 1)
                'My.Computer.FileSystem.MoveFile(ff, newdir & "\s\" & temp(temp.Length - 1))
                ckiii = ckiii + 1
            Next
            '整理完成

            ReDim allfileDD(ii - 1)
            For ai As Integer = 0 To ii - 1
                allfileDD(ai) = test(ai)
            Next
            ReDim ckallfileDD(ckii - 1)
            For ai As Integer = 0 To ckii - 1
                ckallfileDD(ai) = cktest(ai)
            Next
            'MsgBox("ok")
            iii = 0
            ckiii = 0
            For ai As Integer = 0 To allfileDD.Length - 1
                If allfileDD(ai) = todayDateM & todayDateD Then
                    'MsgBox(allfileDD(ai))
                Else
                    newdir = "D:\LVDT-Data\" & mano & "\wield-LVDT\" & todayDateY & allfileDD(ai)
                    newname = todayDate & "-" & clickTime
                    My.Computer.FileSystem.CreateDirectory(newdir & "\s")
                    '讀取資料夾內有多少檔案分類(使用月日做區分)
                    For Each ff As String In My.Computer.FileSystem.GetFiles(rootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_" & allfileDD(ai) & "*.csv")
                        Dim temp() As String
                        temp = ff.Split("\")
                        My.Computer.FileSystem.MoveFile(ff, newdir & "\s\" & temp(temp.Length - 1))
                    Next
                    '將分類檔案取出中斷點、焊接時間等資訊並建立新CSV檔
                    For Each ff As String In My.Computer.FileSystem.GetFiles(newdir & "\s\", FileIO.SearchOption.SearchTopLevelOnly, "*_" & allfileDD(ai) & "*.csv")
                        Dim temp() As String
                        temp = ff.Split("\")
                        readcsv(ff, temp(temp.Length - 1), newdir, newname, mano)
                    Next
                End If
            Next

            For ai As Integer = 0 To ckallfileDD.Length - 1
                If ckallfileDD(ai) = todayDateM & todayDateD Then
                    'MsgBox(allfileDD(ai))
                Else
                    cknewdir = "D:\LVDT-Data\" & mano & "\wield-LVDT\" & todayDateY & ckallfileDD(ai)
                    newname = todayDate & "-" & clickTime
                    My.Computer.FileSystem.CreateDirectory(cknewdir & "\s-ck")
                    '讀取資料夾內有多少檔案分類(使用月日做區分)
                    For Each ff As String In My.Computer.FileSystem.GetFiles(ckrootdir, FileIO.SearchOption.SearchTopLevelOnly, "*_" & ckallfileDD(ai) & "*.csv")
                        Dim temp() As String
                        temp = ff.Split("\")
                        My.Computer.FileSystem.MoveFile(ff, cknewdir & "\s-ck\" & temp(temp.Length - 1))
                    Next
                End If
            Next
            'MsgBox("ok")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            Dim upcut As Integer = 0
            For i = 0 To strValue.Length - 1
                If strValue(i) > lvdtmax Then
                    lvdtmax = strValue(i)
                ElseIf strValue(i) < lvdtmin Then
                    lvdtmin = strValue(i)
                End If
                If stoplvdt >= 0 Then
                    If strValue(i) > stoplvdt And upcut = 0 And stoptime = 0 Then
                        upcut = 1
                    End If
                    If strValue(i) < stoplvdt And stoptime = 0 And upcut = 1 Then
                        If i > 0 Then
                            stoptime = strtime(i - 1)
                        Else
                            stoptime = strtime(i)
                        End If
                    End If
                ElseIf stoplvdt < 0 Then
                    If strValue(i) < stoplvdt And stoptime = 0 Then
                        If i > 0 Then
                            stoptime = strtime(i - 1)
                        Else
                            stoptime = strtime(i)
                        End If
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

    Private Sub btn_relineborder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_relineborder.Click
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If i = ListBox1.SelectedIndex Then
                Chart1.Series(i).BorderWidth = 2
            Else
                Chart1.Series(i).BorderWidth = 0
            End If
        Next
    End Sub
End Class