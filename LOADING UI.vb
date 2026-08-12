Public Class LOADING_UI

    Private WithEvents splashTimer As New Timer()

    Private Sub LOADING_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True

        splashTimer.Interval = 3000   ' 3 seconds
        splashTimer.Start()

    End Sub

    Private Sub splashTimer_Tick(sender As Object, e As EventArgs) Handles splashTimer.Tick

        splashTimer.Stop()

        ' Open Main Form
        Dim mainForm As New Main_UI()
        mainForm.Show()

        ' Close Splash
        Me.Hide()

        mainForm.StartPosition = FormStartPosition.Manual
        mainForm.Location = Me.Location
        'mainForm.Size = Me.Size
        mainForm.WindowState = Me.WindowState

    End Sub

End Class