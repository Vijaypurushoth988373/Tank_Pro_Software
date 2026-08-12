Public Class Client_UI

    Private Sub Client_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UncheckAllRadioButtons(Me)
    End Sub

    Private Sub UncheckAllRadioButtons(parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False
            End If

            ' Recursively check inside containers
            If ctrl.HasChildren Then
                UncheckAllRadioButtons(ctrl)
            End If
        Next
    End Sub

    Private Sub Rbn_Aramco_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Aramco.CheckedChanged
        If Rbn_Aramco.Checked = True Then
            SelectedClient = "ARAMCO"   ' ← Set this here

            If SelectedClient = "ARAMCO" And SelectedType = "HORIZONTAL" Then
                Form7.StartPosition = FormStartPosition.Manual
                Form7.Location = Me.Location
                Form7.WindowState = Me.WindowState
                Form7.Show()
                Me.Hide()
            ElseIf SelectedClient = "ARAMCO" And SelectedType = "VERTICAL" Then
                Form1.StartPosition = FormStartPosition.Manual
                Form1.Location = Me.Location
                Form1.WindowState = Me.WindowState
                Form1.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Rbn_Adnoc_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Adnoc.CheckedChanged
        If Rbn_Adnoc.Checked = True Then
            SelectedClient = "ADNOC"   ' ← Set this here

            If SelectedClient = "ADNOC" And SelectedType = "HORIZONTAL" Then
                Form7.StartPosition = FormStartPosition.Manual
                Form7.Location = Me.Location
                Form7.WindowState = Me.WindowState
                Form7.Show()
                Me.Hide()
            ElseIf SelectedClient = "ADNOC" And SelectedType = "VERTICAL" Then
                Form1.StartPosition = FormStartPosition.Manual
                Form1.Location = Me.Location
                Form1.WindowState = Me.WindowState
                Form1.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Qatar.CheckedChanged
        If Rbn_Qatar.Checked = True Then
            SelectedClient = "QATAR"   ' ← Set this here

            If SelectedClient = "QATAR" And SelectedType = "HORIZONTAL" Then
                Form7.StartPosition = FormStartPosition.Manual
                Form7.Location = Me.Location
                Form7.WindowState = Me.WindowState
                Form7.Show()
                Me.Hide()
            ElseIf SelectedClient = "QATAR" And SelectedType = "VERTICAL" Then
                Form1.StartPosition = FormStartPosition.Manual
                Form1.Location = Me.Location
                Form1.WindowState = Me.WindowState
                Form1.Show()
                Me.Hide()
            End If
        End If
    End Sub
End Class