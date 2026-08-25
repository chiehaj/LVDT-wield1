Public Class RWini

    Public Declare Function GetinifileString Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
    ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, ByVal lpdefault As String, _
    ByVal lpretrunedstring As String, ByVal nSize As Int32, _
    ByVal lpFilename As String) As Int32

    Public Declare Function WriteinifileString Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
    ByVal lpApplictionName As String, _
    ByVal lpKeyName As String, ByVal lpString As String, _
    ByVal lpFilename As String) As Int32

    Function Read(ByVal rsection As String, ByVal rkey As String, ByVal rPath As String)

        Dim tb_temp As New TextBox

        Dim lng_Rtn As Long
        Dim str_Rtn As String
        Dim temp As String
        rPath = Application.StartupPath() & "\" & rPath & ".ini"
        Read = ""
        str_Rtn = New String("", 255)
        Dim fi As New System.IO.FileInfo(rPath)
        If Not fi.Exists Then
            Throw New Exception("設定檔遺失(" & rPath & ")")
        End If
        GetinifileString(rsection, rkey, vbNullString, str_Rtn, 100, rPath)
        Return str_Rtn
    End Function

    Function Write(ByVal rsection As String, ByVal rkey As String, ByVal rvalue As String, ByVal rPath As String)
        Dim lng_Rtn As Long
        Dim str_Rtn As String
        rPath = Application.StartupPath() & "\" & rPath & ".ini"
        Write = ""
        str_Rtn = New String(Chr(20), 255)
        Dim fi As New System.IO.FileInfo(rPath)
        If Not fi.Exists Then
            Throw New Exception("設定檔遺失(" & rPath & ")")
        End If
        WriteinifileString(rsection, rkey, rvalue, rPath)
    End Function
End Class