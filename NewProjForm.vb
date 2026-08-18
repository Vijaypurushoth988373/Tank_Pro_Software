Imports System.IO

Public Class NewProjForm

    ' =====================================================================================
    ' File > New Project — resets Revision to "A" and clears Project Code/Directory so the
    ' user enters a code and picks a location (via btn_proj_dir) before continuing.
    ' =====================================================================================
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If Not String.IsNullOrWhiteSpace(txt_Proj_Code.Text) OrElse Not String.IsNullOrWhiteSpace(txt_Proj_Location.Text) Then
            Dim confirm As DialogResult = MessageBox.Show(
                "Starting a new project will clear the Project Code, Revision, and Directory shown here." & vbCrLf &
                "This does not delete any previously saved files." & vbCrLf & vbCrLf & "Continue?",
                "New Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm <> DialogResult.Yes Then Exit Sub
        End If

        txt_Proj_Code.Text = ""
        txt_Proj_Rev.Text = "A"
        txt_Proj_Location.Text = ""
    End Sub

    ' =====================================================================================
    ' File > New Revision — bumps the revision and saves as a new file, leaving the
    ' previous revision's file untouched (InputsFilePath appends "_Rev<X>" to the filename)
    ' =====================================================================================
    Private Sub NewRevisionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewRevisionToolStripMenuItem.Click
        If String.IsNullOrWhiteSpace(txt_Proj_Code.Text) Then
            MessageBox.Show("Enter a Project Code before starting a new revision.", "New Revision",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        txt_Proj_Rev.Text = BumpRevision(txt_Proj_Rev.Text)
        SaveCurrentProject()
    End Sub

    ' =====================================================================================
    ' File > Open/Load — browse for a saved TankInputs_*.xlsx and load it
    ' =====================================================================================
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim chosenFolder As String = BrowseAndLoadAllVerticalProjectInputs(Form1, Form2, Form3, Form4, Form5, Form6, Me, txt_Proj_Location.Text)

        If chosenFolder <> "" Then
            txt_Proj_Location.Text = StripRevisionAndProjCode(chosenFolder)
            GoToMainUI()
        End If
    End Sub

    ' =====================================================================================
    ' File > Save
    ' =====================================================================================
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveCurrentProject()
    End Sub

    ' =====================================================================================
    ' File > Revisions comparing — not implemented yet
    ' =====================================================================================
    Private Sub RevisionsComparingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionsComparingToolStripMenuItem.Click
        MessageBox.Show("Revisions comparing is coming soon.", "Revisions Comparing",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' =====================================================================================
    ' Project directory browse button — picks the BASE project directory and proceeds to
    ' Main_UI. The master Inventor project is NOT copied here: at this point the tank type
    ' (Horizontal/Vertical) and client (ARAMCO/ADNOC/QATAR) aren't known yet — those are
    ' chosen on Main_UI/Client_UI, which is where the correct master gets copied into
    ' <this folder>\<ProjCode>\REV_<revision> (see Client_UI.CopyMasterProjectIfNeeded).
    ' =====================================================================================
    Private Sub btn_proj_dir_Click(sender As Object, e As EventArgs) Handles btn_proj_dir.Click
        If String.IsNullOrWhiteSpace(txt_Proj_Code.Text) Then
            MessageBox.Show("Enter a Project Code before selecting the project directory.", "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Select project directory"
            If Directory.Exists(txt_Proj_Location.Text) Then
                dlg.SelectedPath = txt_Proj_Location.Text
            End If

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            txt_Proj_Location.Text = dlg.SelectedPath

            GoToMainUI()
        End Using
    End Sub

    ' =====================================================================================
    ' PRIVATE HELPERS
    ' =====================================================================================

    Private Sub SaveCurrentProject()
        Dim baseFolder As String = txt_Proj_Location.Text

        If String.IsNullOrWhiteSpace(baseFolder) OrElse Not Directory.Exists(baseFolder) Then
            Using dlg As New FolderBrowserDialog()
                dlg.Description = "Select project directory to save into"
                If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
                baseFolder = dlg.SelectedPath
                txt_Proj_Location.Text = baseFolder
            End Using
        End If

        ' Save alongside the model, in the same <ProjCode>\REV_<rev> folder
        ' Create 3D / Update 3D / btn_proj_dir use.
        Dim revision As String = If(String.IsNullOrWhiteSpace(txt_Proj_Rev.Text), "A", txt_Proj_Rev.Text.Trim())
        Dim projectFolder As String = If(String.IsNullOrWhiteSpace(txt_Proj_Code.Text),
                                         baseFolder,
                                         Path.Combine(baseFolder, txt_Proj_Code.Text.Trim(), "REV_" & revision))

        Dim savedPath As String = SaveAllVerticalProjectInputs(projectFolder, Form1, Form2, Form3, Form4, Form5, Form6, Me)

        If Not String.IsNullOrWhiteSpace(savedPath) Then
            MessageBox.Show("✅ Project saved: " & Path.GetFileName(savedPath), "Save Project",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ''' Given a project folder that may be "<base>\<ProjCode>\REV_<x>" (as produced by
    ''' Save/Create 3D/Update 3D), returns the base folder so it can be stored back into
    ''' txt_Proj_Location without doubling the <ProjCode>\REV_<rev> suffix on the next save.
    ''' Returns the folder unchanged if it doesn't match that pattern (e.g. an older/manually
    ''' organized save).
    Private Function StripRevisionAndProjCode(projectFolder As String) As String
        Dim revSegment As String = Path.GetFileName(projectFolder)
        If revSegment.StartsWith("REV_", StringComparison.OrdinalIgnoreCase) Then
            Dim projCodeFolder As String = Path.GetDirectoryName(projectFolder)
            If Not String.IsNullOrWhiteSpace(projCodeFolder) Then
                Dim baseFolder As String = Path.GetDirectoryName(projCodeFolder)
                If Not String.IsNullOrWhiteSpace(baseFolder) Then Return baseFolder
            End If
        End If

        Return projectFolder
    End Function

    Private Sub GoToMainUI()
        Main_UI.StartPosition = FormStartPosition.Manual
        Main_UI.Location = Me.Location
        Main_UI.WindowState = Me.WindowState
        Main_UI.Show()
        Me.Hide()
    End Sub

    ''' Bumps a revision code: trailing letter A-Y -> next letter, Z -> AA;
    ''' trailing digits increment by 1; anything else gets "-2" appended.
    Private Function BumpRevision(current As String) As String
        Dim rev As String = If(current, "").Trim()
        If rev = "" Then Return "A"

        Dim lastChar As Char = rev(rev.Length - 1)

        If Char.IsDigit(lastChar) Then
            Dim digitsStart As Integer = rev.Length - 1
            While digitsStart > 0 AndAlso Char.IsDigit(rev(digitsStart - 1))
                digitsStart -= 1
            End While
            Dim prefix As String = rev.Substring(0, digitsStart)
            Dim numPart As Integer
            If Integer.TryParse(rev.Substring(digitsStart), numPart) Then
                Return prefix & (numPart + 1).ToString()
            End If
        ElseIf Char.IsLetter(lastChar) Then
            If lastChar = "Z"c Then
                Return rev & "A"
            End If
            Return rev.Substring(0, rev.Length - 1) & Chr(Asc(Char.ToUpper(lastChar)) + 1)
        End If

        Return rev & "-2"
    End Function

End Class
