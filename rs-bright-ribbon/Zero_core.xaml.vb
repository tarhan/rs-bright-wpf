﻿Public Class Zero_core

    Public Property Title As String
    Private Sub Frame_Navigated(sender As Object, e As NavigationEventArgs)
        url.Text = e.Uri.AbsoluteUri
    End Sub
    Public Shared ReadOnly TitleChangedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("TitleChanged", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Zero_core))
    Custom Event TitleChanged As RoutedEventHandler
        AddHandler(value As RoutedEventHandler)
            Me.AddHandler(TitleChangedEvent, value)
        End AddHandler

        RemoveHandler(value As RoutedEventHandler)
            Me.RemoveHandler(TitleChangedEvent, value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event
    Public Shared ReadOnly UriChangedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("UriChanged", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(Zero_core))
    Custom Event UriChanged As RoutedEventHandler
        AddHandler(value As RoutedEventHandler)
            Me.AddHandler(UriChangedEvent, value)
        End AddHandler

        RemoveHandler(value As RoutedEventHandler)
            Me.RemoveHandler(UriChangedEvent, value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event
    Public Property Uri As String
    Private Sub WebBrowser_LoadCompleted(sender As Object, e As NavigationEventArgs)
        Dim myDocument As mshtml.HTMLDocument = DirectCast(sender.Document, mshtml.HTMLDocument)
        Uri = e.Uri.AbsoluteUri
        RaiseEvent UriChanged(Me, New RoutedEventArgs(UriChangedEvent, sender))
        If Title <> myDocument.title Then
            Title = myDocument.title
            RaiseEvent TitleChanged(Me, New RoutedEventArgs(TitleChangedEvent, sender))
        End If
        back.IsEnabled = b.CanGoBack
        forward.IsEnabled = b.CanGoForward
    End Sub

    Private Sub Button_Click()
        On Error GoTo Err
        If url.Tag Then
            b.Refresh()
        Else
            b.Navigate(New Uri(url.Text))
        End If
Err:    'TODO: ナビゲーションをキャンセルする、入力を元に戻す

    End Sub

    Private Sub url_TextChanged(sender As Object, e As TextChangedEventArgs) Handles url.TextChanged
        sender.tag = Uri = sender.text
    End Sub

    Private Sub url_KeyUp(sender As Object, e As KeyEventArgs) Handles url.KeyUp
        If e.Key = Key.Enter Then Button_Click()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        If b.CanGoBack Then b.GoBack()
    End Sub

    Private Sub forward_Click(sender As Object, e As RoutedEventArgs) Handles forward.Click
        If b.CanGoForward Then b.GoForward()
    End Sub

    Private Sub url_GotKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles url.GotKeyboardFocus
        url.SelectAll()
    End Sub
End Class
