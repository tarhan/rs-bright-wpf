﻿Imports System.Xml

Public Module ustRec
    Function getCID(pageUrl As String) As String
        Dim fixed As String
        If pageUrl Like "http://www.ustream.tv/channel/*" Then
            fixed = pageUrl.Replace("/theater", "")
        ElseIf pageUrl Like "http://www.ustream.tv/recorded/*" Then
            fixed = pageUrl.Replace("/theater", "")
            Return fixed.Substring(fixed.LastIndexOf("/"c) + 1)
        End If
        Dim x As XmlDocument = getXmlApiResult(fixed)
        Return (x.DocumentElement.FirstChild.FirstChild.FirstChild.Value)
    End Function
    Function getLiveUrl(cid As String) As String
        Return String.Format("http://iphone-streaming.ustream.tv/ustreamVideo/{0}/streams/live/playlist.m3u8", cid)
    End Function
    Function getRecordedLiveUrl(cid As String) As String
        Return String.Format("http://tcdn.ustream.tv/video/{0}", cid)
    End Function
    Function getXmlApiResult(pageUrl As String) As XmlDocument
        Dim x As New XmlDocument()
        x.Load(New XmlTextReader(pageUrl.Replace("www.ustream.tv/", "api.ustream.tv/xml/") + "/getinfo"))
        Return x
    End Function
End Module