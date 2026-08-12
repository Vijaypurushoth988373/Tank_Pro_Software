Public Class Main_UI
    Private Sub Main_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UncheckAllRadioButtons(Me)
        isFormLoaded = True
    End Sub

    Private isFormLoaded As Boolean = False

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

    Private Sub Rbn_Hor_Vessel_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Hor_Vessel.CheckedChanged

        If Not isFormLoaded Then Exit Sub
        If Rbn_Hor_Vessel.Checked = True Then
            SelectedType = "HORIZONTAL"   ' ← Set this here

            Client_UI.StartPosition = FormStartPosition.Manual
            Client_UI.Location = Me.Location
            Client_UI.WindowState = Me.WindowState
            Client_UI.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub Rbn_Vert_Vessel_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Vert_Vessel.CheckedChanged

        If Not isFormLoaded Then Exit Sub
        If Rbn_Vert_Vessel.Checked = True Then
            SelectedType = "VERTICAL"   ' ← Set this here

            Client_UI.StartPosition = FormStartPosition.Manual
            Client_UI.Location = Me.Location
            Client_UI.WindowState = Me.WindowState
            Client_UI.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub Rbn_Rect_Vessel_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Rect_Vessel.CheckedChanged
        If Rbn_Rect_Vessel.Checked = True Then
            MessageBox.Show("Still Under Development, use Vertical Vessel")
        End If
    End Sub

End Class