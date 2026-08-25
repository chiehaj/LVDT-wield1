Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.ChartArea
Imports System.Net.Sockets
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Graphics
Imports System.Windows.Forms
Imports System.Threading

Imports System.Data.SqlClient.SqlConnection
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Windows.Forms.Control

Public Class NO2

    Inherits Form2
    Private Sub NO2_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        MAIN.btn_NO2.Enabled = True
    End Sub
End Class