Public Class Form1
    Dim status = True

    Sub disable() '4G
        send("int set int name=""Ethernet 5"" admin=disabled")
        status = False
        NotifyIcon1.BalloonTipTitle = "NetSwitcher"
        NotifyIcon1.BalloonTipText = "Vous venez de passer sur la 4G"
        NotifyIcon1.ShowBalloonTip(5000)
        GToolStripMenuItem.Enabled = False
        ADSLToolStripMenuItem.Enabled = True
    End Sub

    Sub enable() 'ADSL
        send("int set int name=""Ethernet 5"" admin=enabled")
        status = True
        NotifyIcon1.BalloonTipTitle = "NetSwitcher"
        NotifyIcon1.BalloonTipText = "Vous venez de passer sur l'ADSL"
        NotifyIcon1.ShowBalloonTip(5000)
        ADSLToolStripMenuItem.Enabled = False
        GToolStripMenuItem.Enabled = True
    End Sub

    Sub send(ByVal command)
        Dim psi As New ProcessStartInfo() ' Initialize ProcessStartInfo (psi)
        psi.Verb = "runas" ' runas = Run As Administrator
        psi.FileName = "netsh" ' File or exe to run (this cannot take arguments, use ProcessStartInfo.Arguments instead
        psi.Arguments = command ' Arguments for the process that you're going to run
        Try
            Process.Start(psi) ' Run the process (User is required to press Yes to run the program with Administrator access)
        Catch
            MsgBox("User cancelled the operation", 16, "") ' User pressed No
        End Try
    End Sub

    Private Sub ADSLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADSLToolStripMenuItem.Click
        enable()
    End Sub

    Private Sub GToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GToolStripMenuItem.Click
        disable()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If status Then
            disable()
        Else
            enable()
        End If
    End Sub
End Class
