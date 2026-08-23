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
            CopyMasterProjectIfNeeded()

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
            CopyMasterProjectIfNeeded()

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
            CopyMasterProjectIfNeeded()

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

    ' =====================================================================================
    ' Copies the correct master Inventor project (Vertical vs. Horizontal, and per-client
    ' for Horizontal) into <NewProjForm.txt_Proj_Location>\<ProjCode>\REV_<revision>, now
    ' that both SelectedType and SelectedClient are known. No-ops silently if NewProjForm
    ' wasn't used to set up a project location/code (e.g. Open/Load flow, or the app was
    ' entered some other way) — Form1/Form7's own Create 3D / Update 3D still fall back to
    ' their own master-copy logic in that case.
    ' =====================================================================================
    Private Sub CopyMasterProjectIfNeeded()
        Dim projLocation As String = NewProjForm.txt_Proj_Location.Text.Trim()
        Dim projCode As String = NewProjForm.txt_Proj_Code.Text.Trim()

        If String.IsNullOrWhiteSpace(projLocation) OrElse Not IO.Directory.Exists(projLocation) OrElse String.IsNullOrWhiteSpace(projCode) Then
            Exit Sub
        End If

        Dim masterIpjPath As String = ResolveMasterIpjPath(SelectedType, SelectedClient)
        If String.IsNullOrWhiteSpace(masterIpjPath) Then
            MessageBox.Show("❌ No master project defined for " & SelectedType & " / " & SelectedClient & ".", "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not IO.File.Exists(masterIpjPath) Then
            MessageBox.Show("Master project file not found:" & vbCrLf & masterIpjPath, "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim revision As String = If(String.IsNullOrWhiteSpace(NewProjForm.txt_Proj_Rev.Text), "A", NewProjForm.txt_Proj_Rev.Text.Trim())

        Dim copiedIpjPath As String = Form1.CopyProjectToDestination(masterIpjPath, projLocation, projCode, revision)
        If String.IsNullOrWhiteSpace(copiedIpjPath) Then Exit Sub ' CopyProjectToDestination already showed the error

        Dim copiedProjectFolder As String = IO.Path.GetDirectoryName(copiedIpjPath)

        ' Keep the target form's own Project Code in sync — it drives its own copy/design flow later.
        If SelectedType = "VERTICAL" Then
            Form1.txt_Proj_Code.Text = projCode
        ElseIf SelectedType = "HORIZONTAL" Then
            Form7.txt_Proj_Code.Text = projCode
        End If

        MessageBox.Show("✅ Project created at:" & vbCrLf & copiedProjectFolder, "Project Directory",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' Resolves the master Inventor project (.ipj) for a given tank type/client, matching
    ''' the same paths Form1's Create 3D and Form7's own default-project logic already use.
    Private Function ResolveMasterIpjPath(tankType As String, client As String) As String
        If tankType = "VERTICAL" Then
            Return "D:\Projects\Inventor\CD.24.12_3D_Model - Test\CD.24.012.007_Test.ipj"
        ElseIf tankType = "HORIZONTAL" Then
            Select Case client
                Case "ARAMCO"
                    Return "D:\ARAMCO_HOR_VESSEL\ARAMCO_HOR_VESSEL.ipj"
                Case "ADNOC", "QATAR"
                    Return "D:\HORIZONTAL_TANK\ADNOC_HOR_VESSEL\ADNOC_HOR_VESSEL.ipj"
            End Select
        End If

        Return String.Empty
    End Function

End Class
