Imports System.Net.Sockets
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Graphics
Imports System.Windows.Forms
Imports System.Threading

Imports System.Data.SqlClient.SqlConnection
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.ChartArea
Imports System.Data
Imports System.Data.OleDb
Imports System.Web
'Imports System.Windows.Forms
'Imports System.Windows.Forms.WebControls
Imports System.Windows.Forms.Control


Public Class Form1

    Dim RWini As New RWini

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

    Dim mytcpclient As New TcpClient        '宣告 mytcpclient 
    Dim myNetWorkStream As NetworkStream     '宣告 mynetworkstream
    Dim tcpConnectState As Boolean


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbx_IP.Text = RWini.Read("PLC", "IP", "para")
        tbx_Port.Text = RWini.Read("PLC", "PORT", "para")
        tbx_TestSendCommand.Text = "RD DM500.L"
        'Form2.Show()
    End Sub

    Private Sub connection(ByVal str_ipAddress As String, ByVal int_port As Integer)
        Try
            mytcpclient.Connect(str_ipAddress, int_port)
            If (mytcpclient.Connected) Then
                tcpConnectState = True
                myNetWorkStream = mytcpclient.GetStream()
                lbl_connectState.Text = "Connect ok"
            Else
                lbl_connectState.Text = "Not Connect"
            End If
        Catch ex As ArgumentOutOfRangeException
            lbl_connectState.Text = "Not Connect"
            MessageBox.Show("ArgumentOutOfRangeException 例外 :" + vbCrLf + ex.Message, _
                "Port error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As SocketException
            lbl_connectState.Text = "Not Connect"
            MessageBox.Show("SocketException 例外 :" + vbCrLf + ex.Message, _
            "Connect error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub WriteData(ByVal str_comm As String)

        Dim myBytes() As Byte = Encoding.Default.GetBytes(str_comm)
        Dim MYBYTES16(myBytes.Length) As Byte
        Dim myChar() As Char = str_comm.ToCharArray
        Dim test As String
        Dim i As Integer
        test = "RD DM500.L"

        i = 0

        If (mytcpclient.Connected = False) Then
            mytcpclient.Connect(tbx_IP.Text, Integer.Parse(tbx_Port.Text))
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


    End Sub
    Private Sub ReadData()
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


    Private Sub btn_SendCommand_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_SendCommand.Click
        WriteData(tbx_TestSendCommand.Text + vbCr)
        System.Threading.Thread.Sleep(100)
        ReadData()
    End Sub

    Private Sub FXPLC()
        'command rule
        'title + PLC no + timer + cmopant number + compant count +
    End Sub

    Private Sub btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Connect.Click
        connection(tbx_IP.Text, Integer.Parse(tbx_Port.Text))
        'If (Form2.mytcpclient.Connected = True) Then
        'Timer3.Interval = 10
        'Timer3.Enabled = True
        'End If
    End Sub

    Private Sub DrawPoints(ByVal g As System.Drawing.Graphics)
        Dim arr As Point() = {New Point(0, 0), New Point(1, 1), New Point(10, 1)}
        'Dim pen As System.Drawing.Pen
        Dim blackpen As New Pen(Color.Black, 3)
        'pen.LineJoin = Drawing2D.LineJoin.Round
        'pen.Color = Color.Black
        g.DrawLines(blackpen, arr)
    End Sub

    Private Sub btn_DrawingLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DrawingLine.Click
        Dim g As System.Drawing.Graphics
        ' panel_lineGraph.Refresh()


        'DrawPoints(g)
    End Sub

    Public Sub DrawLinesPoint(ByVal e As PaintEventArgs)

        ' Create pen.
        Dim blackPen As New Pen(Color.Black, 3)

        ' Create array of points that define lines to draw.
        Dim points As Point() = {New Point(10, 10), New Point(10, 100), _
        New Point(200, 50), New Point(250, 300)}

        'Draw lines to screen.
        e.Graphics.DrawLines(blackPen, points)
    End Sub

    Private Sub panel_lineGraph_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim blackPen As New Pen(Color.Black, 3)

        ' Create array of points that define lines to draw.
        Dim points As Point() = {New Point(10, 10), New Point(10, 100), _
        New Point(200, 50), New Point(250, 300)}
        e.Graphics.DrawLines(blackPen, points)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        checkFile()
        WriteData("WR MR101 1" + vbCr)
        Threading.Thread.Sleep(10)
        ReadData()
        'Threading.Thread.Sleep(100)
        WriteData("WR MR100 1" + vbCr)
        Threading.Thread.Sleep(10)
        ReadData()
        'Threading.Thread.Sleep(100)
        timer1Count = 0
        Timer1.Interval = 100
        Timer1.Enabled = True
        Timer2.Interval = 50000
        Timer2.Enabled = True
    End Sub

    Private Sub btn_ClearResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearResult.Click
        'Me.Chart1.Series.Clear()
        'Me.Controls.Remove(Me.Chart1)
        tbx_TcpResult.Text = ""
        Form2.ClearChart()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        readtxt()
        If Form2.ShowInTaskbar = False Then
            MsgBox("Form2 isn't working")
            'Form2.Show()
            'Form2.DrawingChart()
        End If
    End Sub

    Public Sub readtxt()
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
            End If
            FileOpen(Filenum, str_path, OpenMode.Input)
            Do Until EOF(Filenum)
                strTemp(count) = LineInput(Filenum)
                count += 1
            Loop
            FileClose()
            Dim strtime(count - 1) As Double
            Dim strValue(count - 1) As Double
            For i As Integer = 0 To count - 1
                Dim temp() As String
                temp = strTemp(i).Split(",")
                strtime(i) = temp(0)
                strValue(i) = temp(1)
            Next
            If Form2.ShowInTaskbar = False Then
                Form2.Show()
            End If
            Form2.chkChart()
            Form2.DrawingChart(strtime, strValue, str_spath)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Public Sub readtesttxt()
        Dim Filenum As Integer
        Dim strTemp(1000) As String
        Dim count As Integer
        Dim str_path As String
        Try
            count = 0
            Filenum = FreeFile()
            'If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            str_path = "D:\test.txt"
            'End If
            FileOpen(Filenum, str_path, OpenMode.Input)
            Do Until EOF(Filenum)
                strTemp(count) = LineInput(Filenum)
                count += 1
            Loop
            FileClose()
            Dim strtime(count - 1) As Double
            Dim strValue(count - 1) As Double
            For i As Integer = 0 To count - 1
                Dim temp() As String
                temp = strTemp(i).Split(",")
                strtime(i) = temp(0)
                strValue(i) = temp(1)
            Next
            If Form2.ShowInTaskbar = False Then
                Form2.Show()
            End If
            Form2.chkChart()
            Form2.DrawingChart(strtime, strValue, "now")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        WriteData("RD DM500.L" & vbCr)
        'ReadTotxt()
        Threading.Thread.Sleep(10)
        ReadTotxt()
        'Threading.Thread.Sleep(100)
        timer1Count = timer1Count + 1
        lbl_timer2.Text = timer1Count / 10
        WriteData("RD MR100" & vbCr)
        Threading.Thread.Sleep(10)
        ReadData()
        'Threading.Thread.Sleep(100)
        If tbx_TcpResult.Text <> "" And IsNumeric(tbx_TcpResult.Text) Then
            If tbx_TcpResult.Text = 0 Then
                Timer1.Enabled = False
                Timer2.Enabled = False
                Timer3.Enabled = True
                readtesttxt()
            End If
        End If
    End Sub
    Public Sub ReadTotxt()
        Dim Filenum As Integer
        Dim strTemp As String
        Dim b As Byte
        Dim overl As Integer

        Filenum = FreeFile()
        FileOpen(Filenum, "D:\test.txt", OpenMode.Append)

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

                strTemp = strTemp & "," & Integer.Parse(strContent) / 10000
                PrintLine(Filenum, strTemp)
            End If
        Else
            'MsgBox("DISCONNECT !!!")
        End If
        FileClose()

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer1.Enabled = False
        Timer2.Enabled = False
        'lbl_timer2.Text = Timer2.ToString
    End Sub
    Public Sub checkFile()
        Dim sss As String
        sss = Date.Now.ToString("yyyy-MM-dd-HHmmss")
        FileCopy("D:\test.txt", "D:\b\" & sss & ".txt")
        Dim Filenum As Integer

        Filenum = FreeFile()
        FileOpen(Filenum, "D:\test.txt", OpenMode.Output)

        Print(Filenum, "")
        FileClose()

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        WriteData("RD MR100" & vbCr)
        Threading.Thread.Sleep(10)
        ReadData()
        'Threading.Thread.Sleep(100)
        If tbx_TcpResult.Text <> "" And IsNumeric(tbx_TcpResult.Text) Then
            If tbx_TcpResult.Text = 1 Then
                checkFile()
                timer1Count = 0
                Timer1.Interval = 100
                Timer1.Enabled = True
                Timer2.Interval = 50000
                Timer2.Enabled = True
                Timer3.Enabled = False
            End If
        End If
    End Sub
End Class