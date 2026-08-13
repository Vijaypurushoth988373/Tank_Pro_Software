Imports System.IO

Public Class NewProjForm

    Private Const MaxRecentEntries As Integer = 8

    Private ReadOnly RecentListPath As String =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TankPro", "recent_projects.txt")

    Private Sub NewProjForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshRecentMenu()
    End Sub

    ' =====================================================================================
    ' File > New Project — clears this form's project fields and proceeds to Main_UI
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

        GoToMainUI()
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
    ' File > Open — browse for a saved TankInputs_*.xlsx and load it
    ' =====================================================================================
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        OpenProjectViaBrowse()
    End Sub

    ' =====================================================================================
    ' File > Upload Data — same as Open (load an existing project's saved inputs)
    ' =====================================================================================
    Private Sub UploadDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadDataToolStripMenuItem.Click
        OpenProjectViaBrowse()
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

    ' The master Inventor project this app initializes every new project from —
    ' same literal path Form1 itself falls back to (see its ipjPath default).
    Private Const MasterProjectIpjPath As String = "D:\Projects\Inventor\CD.24.12_3D_Model - Test\CD.24.012.007_Test.ipj"

    ' =====================================================================================
    ' Project directory browse button — picks a destination, copies the master Inventor
    ' project into it (as Form1's own copy logic does for later designs), and proceeds
    ' to Main_UI once the copy succeeds.
    ' =====================================================================================
    Private Sub btn_proj_dir_Click(sender As Object, e As EventArgs) Handles btn_proj_dir.Click
        If String.IsNullOrWhiteSpace(txt_Proj_Code.Text) Then
            MessageBox.Show("Enter a Project Code before selecting the project directory.", "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not File.Exists(MasterProjectIpjPath) Then
            MessageBox.Show("Master project file not found:" & vbCrLf & MasterProjectIpjPath, "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Select project directory"
            If Directory.Exists(txt_Proj_Location.Text) Then
                dlg.SelectedPath = txt_Proj_Location.Text
            End If

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim copiedIpjPath As String = Form1.CopyProjectToDestination(MasterProjectIpjPath, dlg.SelectedPath, txt_Proj_Code.Text.Trim())
            If String.IsNullOrWhiteSpace(copiedIpjPath) Then Exit Sub ' CopyProjectToDestination already showed the error

            Dim copiedProjectFolder As String = Path.GetDirectoryName(copiedIpjPath)
            txt_Proj_Location.Text = copiedProjectFolder

            ' Keep Form1's own Project Code in sync — it drives its own copy/design flow later.
            Form1.txt_Proj_Code.Text = txt_Proj_Code.Text

            MessageBox.Show("✅ Project created at:" & vbCrLf & copiedProjectFolder, "Project Directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            GoToMainUI()
        End Using
    End Sub

    ' =====================================================================================
    ' PRIVATE HELPERS
    ' =====================================================================================

    Private Sub SaveCurrentProject()
        Dim folder As String = txt_Proj_Location.Text

        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then
            Using dlg As New FolderBrowserDialog()
                dlg.Description = "Select project directory to save into"
                If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
                folder = dlg.SelectedPath
                txt_Proj_Location.Text = folder
            End Using
        End If

        Dim savedPath As String = SaveAllVerticalProjectInputs(folder, Form1, Form2, Form3, Form4, Form5, Form6, Me)

        If Not String.IsNullOrWhiteSpace(savedPath) Then
            AddToRecentList(savedPath)
            MessageBox.Show("✅ Project saved: " & Path.GetFileName(savedPath), "Save Project",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub OpenProjectViaBrowse()
        Dim chosenFolder As String = BrowseAndLoadAllVerticalProjectInputs(Form1, Form2, Form3, Form4, Form5, Form6, Me, txt_Proj_Location.Text)

        If chosenFolder <> "" Then
            txt_Proj_Location.Text = chosenFolder
            GoToMainUI()
        End If
    End Sub

    Private Sub LoadProjectDirect(filePath As String)
        If Not File.Exists(filePath) Then
            MessageBox.Show("File not found: " & filePath, "Open Recent",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LoadAllVerticalProjectInputs(filePath, Form1, Form2, Form3, Form4, Form5, Form6, Me)
        txt_Proj_Location.Text = Path.GetDirectoryName(filePath)
        AddToRecentList(filePath)
        GoToMainUI()
    End Sub

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

    Private Sub AddToRecentList(filePath As String)
        Try
            Dim dir As String = Path.GetDirectoryName(RecentListPath)
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)

            Dim entries As New List(Of String)
            If File.Exists(RecentListPath) Then
                entries.AddRange(File.ReadAllLines(RecentListPath))
            End If

            entries.RemoveAll(Function(p) p.Equals(filePath, StringComparison.OrdinalIgnoreCase))
            entries.Insert(0, filePath)

            If entries.Count > MaxRecentEntries Then
                entries = entries.GetRange(0, MaxRecentEntries)
            End If

            File.WriteAllLines(RecentListPath, entries)
            RefreshRecentMenu()

        Catch ex As Exception
            Debug.Print($"⚠ AddToRecentList failed: {ex.Message}")
        End Try
    End Sub

    ''' The "Open Recent" menu item's control name is SaveAsToolStripMenuItem
    ''' (a pre-existing naming mismatch in the Designer file — its .Text is "Open Recent").
    Private Sub RefreshRecentMenu()
        Try
            SaveAsToolStripMenuItem.DropDownItems.Clear()

            If Not File.Exists(RecentListPath) Then
                Dim emptyItem As New ToolStripMenuItem("(No recent projects)")
                emptyItem.Enabled = False
                SaveAsToolStripMenuItem.DropDownItems.Add(emptyItem)
                Exit Sub
            End If

            Dim entries As String() = File.ReadAllLines(RecentListPath)
            If entries.Length = 0 Then
                Dim emptyItem As New ToolStripMenuItem("(No recent projects)")
                emptyItem.Enabled = False
                SaveAsToolStripMenuItem.DropDownItems.Add(emptyItem)
                Exit Sub
            End If

            For Each entry As String In entries
                Dim capturedPath As String = entry
                Dim item As New ToolStripMenuItem(Path.GetFileName(capturedPath))
                item.ToolTipText = capturedPath
                AddHandler item.Click, Sub(s, e) LoadProjectDirect(capturedPath)
                SaveAsToolStripMenuItem.DropDownItems.Add(item)
            Next

        Catch ex As Exception
            Debug.Print($"⚠ RefreshRecentMenu failed: {ex.Message}")
        End Try
    End Sub

End Class
