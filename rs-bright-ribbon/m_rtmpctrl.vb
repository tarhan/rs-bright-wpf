﻿Module m_rtmpctrl
    Friend _l As New ObjectModel.ObservableCollection(Of kkffmpegrtmpinfo)
    Friend Function NewProcess(cmdoption As String, vtitle As String)
        Dim p As New Process
        With p.StartInfo
            .FileName = getFFMpath()
            .Arguments = cmdoption
            .CreateNoWindow = True
            .RedirectStandardError = True
            .RedirectStandardInput = True
            .UseShellExecute = False
        End With
        p.EnableRaisingEvents = True
        Dim listItem As New kkffmpegrtmpinfo(p, vtitle)
        _l.Add(listItem)
        Return listItem
    End Function
End Module

Public Class kkffmpegrtmpinfo
    Dim _instance As Process
    Public ReadOnly Property Time As TimeSpan
        Get
            Return Date.Now - _instance.StartTime
        End Get
    End Property
    Dim _t As String

    Public ReadOnly Property Title As String
        Get
            Return _t
        End Get
    End Property
    Sub New(ByVal p As Process, ByVal vtitle As String)
        _instance = p
        _t = vtitle
        AddHandler _instance.ErrorDataReceived, AddressOf redirectError
        AddHandler _instance.Exited, Sub() If _instance.ExitCode = 0 Then _instance.Close()
        '_instance.Start()
    End Sub
    Private Sub redirectError(sender As Object, e As DataReceivedEventArgs)

    End Sub
    Public Sub StopRec()
        _instance.StandardInput.Write("q"c)
    End Sub
End Class