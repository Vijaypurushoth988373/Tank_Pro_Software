' =========================================================================================
' ModuleProjectInputs.vb — FULL MODULE (Excel version)
' Handles:
'   - Single-form save/load (Form7 - Horizontal)
'   - Multi-form save/load (Form1 + Form2-6 - Vertical)
'   - Module-level data save/load (SkirtSupportData, LugSupportData,
'     StiffenerData, BottomPlateData) via reflection — works even if
'     Form2-6 are never opened
' =========================================================================================

Imports System.IO
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Reflection

Public Module ProjectInputsManager

    Public IsLoadingInputs As Boolean = False

    ' =====================================================================================
    ' 1️⃣ SAVE — SINGLE FORM (used for Form7 - Horizontal)
    ' =====================================================================================
    Public Sub SaveProjectInputs(ByVal frm As Form, ByVal projectFolder As String)
        Dim excelApp As Excel.Application = Nothing
        Dim wb As Excel.Workbook = Nothing

        Try
            If String.IsNullOrWhiteSpace(projectFolder) Then
                Debug.Print("⚠ SaveProjectInputs: projectFolder is empty.")
                Exit Sub
            End If

            If Not Directory.Exists(projectFolder) Then
                Try
                    Directory.CreateDirectory(projectFolder)
                Catch ex As Exception
                    Debug.Print($"❌ Cannot create folder: {ex.Message}")
                    Exit Sub
                End Try
            End If

            Dim filePath As String = InputsFilePath(frm, projectFolder)

            excelApp = New Excel.Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            wb = excelApp.Workbooks.Add()

            Dim inputSheet As Excel.Worksheet = CType(wb.Sheets(1), Excel.Worksheet)
            inputSheet.Name = "Inputs"

            inputSheet.Cells(1, 1) = "Type"
            inputSheet.Cells(1, 2) = "Name"
            inputSheet.Cells(1, 3) = "Value"
            With inputSheet.Range("A1:C1")
                .Font.Bold = True
                .Interior.Color = RGB(220, 230, 241)
            End With

            Dim rowIndex As Integer = 2
            Dim dgvList As New List(Of DataGridView)

            SaveControlsRecursive(frm.Controls, inputSheet, rowIndex, dgvList)
            inputSheet.Columns("A:C").AutoFit()

            For Each dgv As DataGridView In dgvList
                SaveDGVSheet(wb, dgv)
            Next

            If File.Exists(filePath) Then File.Delete(filePath)
            wb.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook)

            Debug.Print($"✅ Saved Excel inputs to: {filePath}")

        Catch ex As Exception
            MessageBox.Show("❌ Error saving project inputs: " & ex.Message,
                            "Save Project Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try
                If wb IsNot Nothing Then wb.Close(False)
                If excelApp IsNot Nothing Then excelApp.Quit()
            Catch
            End Try
            If wb IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            If excelApp IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        End Try
    End Sub

    ' =====================================================================================
    ' 2️⃣ LOAD — SINGLE FORM (auto-finds file by proj code)
    ' =====================================================================================
    Public Sub LoadProjectInputs(ByVal frm As Form, ByVal projectFolder As String)
        Try
            If String.IsNullOrWhiteSpace(projectFolder) Then Exit Sub

            Dim filePath As String = InputsFilePath(frm, projectFolder)

            If Not File.Exists(filePath) Then
                Debug.Print($"⚠ No saved inputs found: {filePath}")
                Exit Sub
            End If

            LoadProjectInputsFromFile(frm, filePath)

        Catch ex As Exception
            MessageBox.Show("❌ Error loading project inputs: " & ex.Message,
                            "Load Project Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =====================================================================================
    ' 3️⃣ LOAD FROM FILE PATH — SINGLE FORM (core loader)
    ' =====================================================================================
    Public Sub LoadProjectInputsFromFile(ByVal frm As Form, ByVal filePath As String)
        Dim loadErrors As New List(Of String)
        Dim excelApp As Excel.Application = Nothing
        Dim wb As Excel.Workbook = Nothing

        Try
            If Not File.Exists(filePath) Then
                MessageBox.Show("File not found: " & filePath, "Load Project Inputs",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            excelApp = New Excel.Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            wb = excelApp.Workbooks.Open(filePath, ReadOnly:=True)

            Dim lookup As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

            Dim inputSheet As Excel.Worksheet = Nothing
            Try
                inputSheet = CType(wb.Sheets("Inputs"), Excel.Worksheet)
            Catch
            End Try

            If inputSheet IsNot Nothing Then
                Dim usedRange As Excel.Range = inputSheet.UsedRange
                Dim lastRow As Integer = usedRange.Rows.Count

                For r As Integer = 2 To lastRow
                    Try
                        Dim ctrlType As String = CStr(inputSheet.Cells(r, 1).Value2)
                        Dim ctrlName As String = CStr(inputSheet.Cells(r, 2).Value2)
                        Dim valObj As Object = inputSheet.Cells(r, 3).Value2
                        Dim value As String = If(valObj IsNot Nothing, valObj.ToString(), "")

                        If String.IsNullOrWhiteSpace(ctrlType) OrElse String.IsNullOrWhiteSpace(ctrlName) Then Continue For

                        Dim key As String = ctrlType.Trim() & "|" & ctrlName.Trim()
                        If Not lookup.ContainsKey(key) Then lookup(key) = New List(Of String)
                        lookup(key).Add(value)
                    Catch
                    End Try
                Next
            End If

            Dim dgvData As New Dictionary(Of String, List(Of List(Of String)))(StringComparer.OrdinalIgnoreCase)

            For Each sh As Excel.Worksheet In wb.Sheets
                If sh.Name.StartsWith("DGV_") Then
                    Dim dgvName As String = sh.Name.Substring(4)
                    Dim rows As New List(Of List(Of String))

                    Dim usedRange As Excel.Range = sh.UsedRange
                    Dim lastRow As Integer = usedRange.Rows.Count
                    Dim lastCol As Integer = usedRange.Columns.Count

                    For r As Integer = 2 To lastRow
                        Dim rowVals As New List(Of String)
                        For c As Integer = 1 To lastCol
                            Try
                                Dim v As Object = sh.Cells(r, c).Value2
                                rowVals.Add(If(v IsNot Nothing, v.ToString(), ""))
                            Catch
                                rowVals.Add("")
                            End Try
                        Next
                        rows.Add(rowVals)
                    Next

                    dgvData(dgvName) = rows
                End If
            Next

            IsLoadingInputs = True
            LoadControlsRecursive(frm.Controls, lookup, dgvData, loadErrors)
            Debug.Print($"✅ Loaded inputs from: {filePath}")

        Catch ex As Exception
            loadErrors.Add("General load error: " & ex.Message)
            Debug.Print($"❌ Load exception: {ex.Message}")
        Finally
            IsLoadingInputs = False
            Try
                If wb IsNot Nothing Then wb.Close(False)
                If excelApp IsNot Nothing Then excelApp.Quit()
            Catch
            End Try
            If wb IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            If excelApp IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        End Try

        If loadErrors.Count > 0 Then
            MessageBox.Show("⚠ Some controls could not be restored:" & vbCrLf &
                            String.Join(vbCrLf, loadErrors.Take(20)),
                            "Load Project Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' =====================================================================================
    ' 4️⃣ BROWSE AND LOAD — OpenFileDialog picks .xlsx
    ' =====================================================================================
    Public Function BrowseAndLoadProjectInputs(ByVal frm As Form, Optional ByVal currentFolder As String = "") As String
        Using dlg As New OpenFileDialog()
            dlg.Title = "Select saved project inputs file"
            dlg.Filter = "Tank Inputs|TankInputs_*.xlsx|Excel Files|*.xlsx"
            dlg.RestoreDirectory = True
            dlg.CheckFileExists = True
            dlg.Multiselect = False

            If Not String.IsNullOrWhiteSpace(currentFolder) AndAlso Directory.Exists(currentFolder) Then
                dlg.InitialDirectory = currentFolder
            Else
                dlg.InitialDirectory = "D:\TEST"
            End If

            If dlg.ShowDialog() = DialogResult.OK Then
                Dim fileToLoad As String = dlg.FileName
                If Not File.Exists(fileToLoad) Then
                    MessageBox.Show("❌ File not found: " & fileToLoad)
                    Return ""
                End If
                LoadProjectInputsFromFile(frm, fileToLoad)
                Return Path.GetDirectoryName(fileToLoad)
            End If
        End Using
        Return ""
    End Function

    ' =====================================================================================
    ' 5️⃣ SAVE — MULTI FORM (Form1 + Form2-6, Vertical vessel)
    ' =====================================================================================
    Public Sub SaveAllVerticalProjectInputs(ByVal projectFolder As String,
                                            ByVal form1 As Form,
                                            ByVal form2 As Form,
                                            ByVal form3 As Form,
                                            ByVal form4 As Form,
                                            ByVal form5 As Form,
                                            ByVal form6 As Form,
                                            Optional ByVal newProjForm As Form = Nothing)

        Dim excelApp As Excel.Application = Nothing
        Dim wb As Excel.Workbook = Nothing

        Try
            If String.IsNullOrWhiteSpace(projectFolder) Then
                Debug.Print("⚠ SaveAllVerticalProjectInputs: projectFolder is empty.")
                Exit Sub
            End If

            If Not Directory.Exists(projectFolder) Then
                Directory.CreateDirectory(projectFolder)
            End If

            Dim filePath As String = InputsFilePath(form1, projectFolder)

            excelApp = New Excel.Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            wb = excelApp.Workbooks.Add()

            While wb.Sheets.Count > 1
                CType(wb.Sheets(wb.Sheets.Count), Excel.Worksheet).Delete()
            End While

            Dim firstSheetUsed As Boolean = False
            Dim allForms As Form() = {newProjForm, form1, form2, form3, form4, form5, form6}

            For Each frm As Form In allForms
                If frm Is Nothing Then Continue For

                Dim tag As String = GetFormTag(frm)

                Dim inputSheet As Excel.Worksheet
                If Not firstSheetUsed Then
                    inputSheet = CType(wb.Sheets(1), Excel.Worksheet)
                    firstSheetUsed = True
                Else
                    inputSheet = CType(wb.Sheets.Add(After:=wb.Sheets(wb.Sheets.Count)), Excel.Worksheet)
                End If
                inputSheet.Name = TruncateSheetName(tag & "_Inputs")

                inputSheet.Cells(1, 1) = "Type"
                inputSheet.Cells(1, 2) = "Name"
                inputSheet.Cells(1, 3) = "Value"
                With inputSheet.Range("A1:C1")
                    .Font.Bold = True
                    .Interior.Color = RGB(220, 230, 241)
                End With

                Dim rowIndex As Integer = 2
                Dim dgvList As New List(Of DataGridView)

                SaveControlsRecursive(frm.Controls, inputSheet, rowIndex, dgvList)
                inputSheet.Columns("A:C").AutoFit()

                For Each dgv As DataGridView In dgvList
                    SaveDGVSheet(wb, dgv, tag)
                Next

                Debug.Print($"✅ Saved form {frm.Name} as tag {tag} — {dgvList.Count} DGVs")
            Next

            '==================================================
            ' Save module-level support data (Skirt/Lug/Stiffener/LegSupport)
            ' Works even if Form2-6 were never opened
            '==================================================
            SaveDataModulesToWorkbook(wb)

            If File.Exists(filePath) Then File.Delete(filePath)
            wb.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook)

            Debug.Print($"✅ Saved ALL vertical-form inputs to: {filePath}")

        Catch ex As Exception
            MessageBox.Show("❌ Error saving project inputs: " & ex.Message,
                            "Save Project Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try
                If wb IsNot Nothing Then wb.Close(False)
                If excelApp IsNot Nothing Then excelApp.Quit()
            Catch
            End Try
            If wb IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            If excelApp IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        End Try
    End Sub

    ' =====================================================================================
    ' 6️⃣ LOAD — MULTI FORM (Form1 + Form2-6, Vertical vessel)
    ' =====================================================================================
    Public Sub LoadAllVerticalProjectInputs(ByVal filePath As String,
                                            ByVal form1 As Form,
                                            ByVal form2 As Form,
                                            ByVal form3 As Form,
                                            ByVal form4 As Form,
                                            ByVal form5 As Form,
                                            ByVal form6 As Form,
                                            Optional ByVal newProjForm As Form = Nothing)

        Dim excelApp As Excel.Application = Nothing
        Dim wb As Excel.Workbook = Nothing
        Dim loadErrors As New List(Of String)

        Try
            If Not File.Exists(filePath) Then
                MessageBox.Show("File not found: " & filePath, "Load Project Inputs",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            excelApp = New Excel.Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            wb = excelApp.Workbooks.Open(filePath, ReadOnly:=True)

            Dim allForms As Form() = {newProjForm, form1, form2, form3, form4, form5, form6}

            For Each frm As Form In allForms
                If frm Is Nothing Then Continue For

                Dim tag As String = GetFormTag(frm)

                Dim lookup As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

                Dim inputSheet As Excel.Worksheet = Nothing
                Try
                    inputSheet = CType(wb.Sheets(TruncateSheetName(tag & "_Inputs")), Excel.Worksheet)
                Catch
                End Try

                If inputSheet IsNot Nothing Then
                    Dim usedRange As Excel.Range = inputSheet.UsedRange
                    Dim lastRow As Integer = usedRange.Rows.Count

                    For r As Integer = 2 To lastRow
                        Try
                            Dim ctrlType As String = CStr(inputSheet.Cells(r, 1).Value2)
                            Dim ctrlName As String = CStr(inputSheet.Cells(r, 2).Value2)
                            Dim valObj As Object = inputSheet.Cells(r, 3).Value2
                            Dim value As String = If(valObj IsNot Nothing, valObj.ToString(), "")

                            If String.IsNullOrWhiteSpace(ctrlType) OrElse String.IsNullOrWhiteSpace(ctrlName) Then Continue For

                            Dim key As String = ctrlType.Trim() & "|" & ctrlName.Trim()
                            If Not lookup.ContainsKey(key) Then lookup(key) = New List(Of String)
                            lookup(key).Add(value)
                        Catch
                        End Try
                    Next
                Else
                    Debug.Print($"⚠ No sheet found: {tag}_Inputs")
                End If

                Dim dgvData As New Dictionary(Of String, List(Of List(Of String)))(StringComparer.OrdinalIgnoreCase)
                Dim dgvPrefix As String = tag & "_DGV_"

                For Each sh As Excel.Worksheet In wb.Sheets
                    If sh.Name.StartsWith(dgvPrefix) Then
                        Dim dgvName As String = sh.Name.Substring(dgvPrefix.Length)
                        Dim rows As New List(Of List(Of String))

                        Dim usedRange As Excel.Range = sh.UsedRange
                        Dim lastRow As Integer = usedRange.Rows.Count
                        Dim lastCol As Integer = usedRange.Columns.Count

                        For r As Integer = 2 To lastRow
                            Dim rowVals As New List(Of String)
                            For c As Integer = 1 To lastCol
                                Try
                                    Dim v As Object = sh.Cells(r, c).Value2
                                    rowVals.Add(If(v IsNot Nothing, v.ToString(), ""))
                                Catch
                                    rowVals.Add("")
                                End Try
                            Next
                            rows.Add(rowVals)
                        Next

                        dgvData(dgvName) = rows
                    End If
                Next

                IsLoadingInputs = True
                LoadControlsRecursive(frm.Controls, lookup, dgvData, loadErrors)
                IsLoadingInputs = False

                Debug.Print($"✅ Loaded form {frm.Name} (tag {tag})")
            Next

            '==================================================
            ' Load module-level support data
            '==================================================
            LoadDataModulesFromWorkbook(wb, loadErrors)

            Debug.Print($"✅ Loaded ALL vertical-form inputs from: {filePath}")

        Catch ex As Exception
            loadErrors.Add("General load error: " & ex.Message)
            Debug.Print($"❌ Load exception: {ex.Message}")
        Finally
            IsLoadingInputs = False
            Try
                If wb IsNot Nothing Then wb.Close(False)
                If excelApp IsNot Nothing Then excelApp.Quit()
            Catch
            End Try
            If wb IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            If excelApp IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        End Try

        If loadErrors.Count > 0 Then
            MessageBox.Show("⚠ Some controls could not be restored:" & vbCrLf &
                            String.Join(vbCrLf, loadErrors.Take(20)),
                            "Load Project Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' =====================================================================================
    ' 7️⃣ SAVE — DATA MODULES (SkirtSupportData, LugSupportData, StiffenerData, BottomPlateData)
    ' Uses reflection — works even if Form2-6 are never opened
    ' =====================================================================================
    Public Sub SaveDataModulesToWorkbook(wb As Excel.Workbook)

        '==================================================
        ' 🔧 LIST YOUR MODULES HERE — add/remove as needed
        '==================================================
        Dim modulesToSave As (String, Type)() = {
            ("MOD_Skirt", GetType(SkirtSupportData)),
            ("MOD_Lug", GetType(LugSupportData)),
            ("MOD_Stiff", GetType(StiffenerData)),
            ("MOD_LegSup", GetType(BottomPlateData))
        }

        For Each item In modulesToSave
            Dim sheetTag As String = item.Item1
            Dim modType As Type = item.Item2

            Try
                Dim sheet As Excel.Worksheet = CType(wb.Sheets.Add(After:=wb.Sheets(wb.Sheets.Count)), Excel.Worksheet)
                sheet.Name = TruncateSheetName(sheetTag)

                sheet.Cells(1, 1) = "VariableName"
                sheet.Cells(1, 2) = "Value"
                With sheet.Range("A1:B1")
                    .Font.Bold = True
                    .Interior.Color = RGB(220, 230, 241)
                End With

                Dim fields As FieldInfo() = modType.GetFields(BindingFlags.Public Or BindingFlags.Static)

                Dim rowIdx As Integer = 2
                For Each fld As FieldInfo In fields
                    Try
                        Dim val As Object = fld.GetValue(Nothing)
                        sheet.Cells(rowIdx, 1) = fld.Name
                        sheet.Cells(rowIdx, 2) = If(val IsNot Nothing, val.ToString(), "")
                        rowIdx += 1
                    Catch ex As Exception
                        Debug.Print($"⚠ Field save failed {modType.Name}.{fld.Name}: {ex.Message}")
                    End Try
                Next

                sheet.Columns("A:B").AutoFit()
                Debug.Print($"✅ Saved module {modType.Name} → sheet {sheetTag} ({fields.Length} fields)")

            Catch ex As Exception
                Debug.Print($"❌ SaveDataModulesToWorkbook failed for {modType.Name}: {ex.Message}")
            End Try
        Next

    End Sub

    ' =====================================================================================
    ' 8️⃣ LOAD — DATA MODULES
    ' =====================================================================================
    Public Sub LoadDataModulesFromWorkbook(wb As Excel.Workbook, loadErrors As List(Of String))

        Dim modulesToLoad As (String, Type)() = {
            ("MOD_Skirt", GetType(SkirtSupportData)),
            ("MOD_Lug", GetType(LugSupportData)),
            ("MOD_Stiff", GetType(StiffenerData)),
            ("MOD_LegSup", GetType(BottomPlateData))
        }

        For Each item In modulesToLoad
            Dim sheetTag As String = item.Item1
            Dim modType As Type = item.Item2

            Try
                Dim sheet As Excel.Worksheet = Nothing
                Try
                    sheet = CType(wb.Sheets(TruncateSheetName(sheetTag)), Excel.Worksheet)
                Catch
                    Debug.Print($"⚠ No sheet found for module {modType.Name} ({sheetTag}) — skipping.")
                    Continue For
                End Try

                Dim usedRange As Excel.Range = sheet.UsedRange
                Dim lastRow As Integer = usedRange.Rows.Count

                Dim sheetValues As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                For r As Integer = 2 To lastRow
                    Try
                        Dim varName As String = CStr(sheet.Cells(r, 1).Value2)
                        Dim valObj As Object = sheet.Cells(r, 2).Value2
                        Dim valStr As String = If(valObj IsNot Nothing, valObj.ToString(), "")
                        If Not String.IsNullOrWhiteSpace(varName) Then
                            sheetValues(varName.Trim()) = valStr
                        End If
                    Catch
                    End Try
                Next

                Dim fields As FieldInfo() = modType.GetFields(BindingFlags.Public Or BindingFlags.Static)
                Dim setCount As Integer = 0

                For Each fld As FieldInfo In fields
                    If sheetValues.ContainsKey(fld.Name) Then
                        Try
                            Dim rawVal As String = sheetValues(fld.Name)

                            If fld.FieldType = GetType(Double) Then
                                Dim d As Double
                                If Double.TryParse(rawVal, d) Then
                                    fld.SetValue(Nothing, d)
                                    setCount += 1
                                End If

                            ElseIf fld.FieldType = GetType(Integer) Then
                                Dim i As Integer
                                If Integer.TryParse(rawVal, i) Then
                                    fld.SetValue(Nothing, i)
                                    setCount += 1
                                End If

                            ElseIf fld.FieldType = GetType(String) Then
                                fld.SetValue(Nothing, rawVal)
                                setCount += 1

                            Else
                                Dim converted As Object = Convert.ChangeType(rawVal, fld.FieldType)
                                fld.SetValue(Nothing, converted)
                                setCount += 1
                            End If

                        Catch ex As Exception
                            loadErrors.Add($"{modType.Name}.{fld.Name}: {ex.Message}")
                        End Try
                    End If
                Next

                Debug.Print($"✅ Loaded module {modType.Name} from sheet {sheetTag} — {setCount}/{fields.Length} fields set")

            Catch ex As Exception
                loadErrors.Add($"Module {modType.Name}: {ex.Message}")
            End Try
        Next

    End Sub

    ' =====================================================================================
    ' PRIVATE HELPERS
    ' =====================================================================================

    Private Function GetFormTag(frm As Form) As String
        Select Case True
            Case frm.Name.ToUpper().Contains("NEWPROJFORM") : Return "F0"
            Case frm.Name.ToUpper().Contains("FORM1") : Return "F1"
            Case frm.Name.ToUpper().Contains("FORM2") : Return "F2"
            Case frm.Name.ToUpper().Contains("FORM3") : Return "F3"
            Case frm.Name.ToUpper().Contains("FORM4") : Return "F4"
            Case frm.Name.ToUpper().Contains("FORM5") : Return "F5"
            Case frm.Name.ToUpper().Contains("FORM6") : Return "F6"
            Case Else : Return frm.Name
        End Select
    End Function

    Private Function InputsFilePath(frm As Form, projectFolder As String) As String
        Try
            Dim projCode As String = ""
            Dim formType As String = ""

            If frm.Name.ToUpper().Contains("FORM7") Then
                formType = "_HOR"
            ElseIf frm.Name.ToUpper().Contains("FORM1") Then
                formType = "_VERT"
            ElseIf frm.Name.ToUpper().Contains("NEWPROJFORM") Then
                formType = "_PROJ"
            End If

            Dim txtCtrl As Control = FindControlByName(frm, "txt_Proj_Code")
            If txtCtrl IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtCtrl.Text) Then
                projCode = SanitizeFileName(txtCtrl.Text.Trim())
            End If

            If String.IsNullOrWhiteSpace(projCode) Then
                projCode = frm.Name
            End If

            Return Path.Combine(projectFolder, $"TankInputs_{projCode}{formType}.xlsx")

        Catch ex As Exception
            Debug.Print($"InputsFilePath failed: {ex.Message}")
            Return Path.Combine(projectFolder, $"TankInputs_{frm.Name}.xlsx")
        End Try
    End Function

    Private Sub SaveControlsRecursive(controls As Control.ControlCollection, sheet As Excel.Worksheet,
                                      ByRef rowIndex As Integer, dgvList As List(Of DataGridView))
        For Each ctrl As Control In controls
            Try
                Select Case True
                    Case TypeOf ctrl Is TextBox
                        sheet.Cells(rowIndex, 1) = "TextBox"
                        sheet.Cells(rowIndex, 2) = ctrl.Name
                        sheet.Cells(rowIndex, 3) = CType(ctrl, TextBox).Text
                        rowIndex += 1

                    Case TypeOf ctrl Is ComboBox
                        sheet.Cells(rowIndex, 1) = "ComboBox"
                        sheet.Cells(rowIndex, 2) = ctrl.Name
                        sheet.Cells(rowIndex, 3) = If(CType(ctrl, ComboBox).Text, "")
                        rowIndex += 1

                    Case TypeOf ctrl Is RadioButton
                        sheet.Cells(rowIndex, 1) = "RadioButton"
                        sheet.Cells(rowIndex, 2) = ctrl.Name
                        sheet.Cells(rowIndex, 3) = CType(ctrl, RadioButton).Checked.ToString()
                        rowIndex += 1

                    Case TypeOf ctrl Is CheckBox
                        sheet.Cells(rowIndex, 1) = "CheckBox"
                        sheet.Cells(rowIndex, 2) = ctrl.Name
                        sheet.Cells(rowIndex, 3) = CType(ctrl, CheckBox).Checked.ToString()
                        rowIndex += 1

                    Case TypeOf ctrl Is NumericUpDown
                        sheet.Cells(rowIndex, 1) = "NumericUpDown"
                        sheet.Cells(rowIndex, 2) = ctrl.Name
                        sheet.Cells(rowIndex, 3) = CType(ctrl, NumericUpDown).Value.ToString()
                        rowIndex += 1

                    Case TypeOf ctrl Is DataGridView
                        dgvList.Add(CType(ctrl, DataGridView))

                End Select

                If ctrl.Controls.Count > 0 Then
                    SaveControlsRecursive(ctrl.Controls, sheet, rowIndex, dgvList)
                End If

            Catch ex As Exception
                Debug.Print($"SaveControlsRecursive: skipped '{ctrl.Name}': {ex.Message}")
            End Try
        Next
    End Sub

    Private Sub SaveDGVSheet(wb As Excel.Workbook, dgv As DataGridView)
        SaveDGVSheet(wb, dgv, "")
    End Sub

    Private Sub SaveDGVSheet(wb As Excel.Workbook, dgv As DataGridView, formTag As String)
        Try
            Dim prefix As String = If(String.IsNullOrEmpty(formTag), "", formTag & "_")
            Dim sheetName As String = TruncateSheetName(prefix & "DGV_" & dgv.Name)

            Dim sheet As Excel.Worksheet = CType(wb.Sheets.Add(After:=wb.Sheets(wb.Sheets.Count)), Excel.Worksheet)
            sheet.Name = sheetName

            For c As Integer = 0 To dgv.Columns.Count - 1
                sheet.Cells(1, c + 1) = dgv.Columns(c).HeaderText
            Next
            With sheet.Range(sheet.Cells(1, 1), sheet.Cells(1, Math.Max(1, dgv.Columns.Count)))
                .Font.Bold = True
                .Interior.Color = RGB(220, 230, 241)
            End With

            Dim rIdx As Integer = 2
            For Each row As DataGridViewRow In dgv.Rows
                If row.IsNewRow Then Continue For
                For c As Integer = 0 To dgv.Columns.Count - 1
                    Try
                        Dim val As Object = row.Cells(c).Value
                        sheet.Cells(rIdx, c + 1) = If(val IsNot Nothing, val.ToString(), "")
                    Catch
                    End Try
                Next
                rIdx += 1
            Next

            sheet.Columns.AutoFit()

        Catch ex As Exception
            Debug.Print($"⚠ SaveDGVSheet failed for {formTag}_{dgv.Name}: {ex.Message}")
        End Try
    End Sub

    Private Sub LoadControlsRecursive(controls As Control.ControlCollection,
                                      lookup As Dictionary(Of String, List(Of String)),
                                      dgvData As Dictionary(Of String, List(Of List(Of String))),
                                      errors As List(Of String))
        For Each ctrl As Control In controls
            Try
                If String.IsNullOrWhiteSpace(ctrl.Name) Then Continue For

                If TypeOf ctrl Is DataGridView Then
                    If dgvData.ContainsKey(ctrl.Name) Then
                        LoadDGV(CType(ctrl, DataGridView), dgvData(ctrl.Name), errors)
                        Debug.Print($"   ✅ DGV loaded: {ctrl.Name} ({dgvData(ctrl.Name).Count} rows)")
                    Else
                        Debug.Print($"   ⚠ No saved sheet for DGV: {ctrl.Name}")
                    End If

                Else
                    Dim ctrlType As String = ctrl.GetType().Name
                    Dim key As String = ctrlType & "|" & ctrl.Name

                    If lookup.ContainsKey(key) Then
                        Dim values As List(Of String) = lookup(key)

                        Select Case True
                            Case TypeOf ctrl Is TextBox
                                CType(ctrl, TextBox).Text = values(0)

                            Case TypeOf ctrl Is ComboBox
                                Dim cb = CType(ctrl, ComboBox)
                                Dim val As String = values(0)
                                If val <> "" AndAlso Not cb.Items.Contains(val) Then
                                    cb.Items.Add(val)
                                End If
                                cb.Text = val

                            Case TypeOf ctrl Is RadioButton
                                Dim chk As Boolean
                                If Boolean.TryParse(values(0), chk) Then
                                    CType(ctrl, RadioButton).Checked = chk
                                End If

                            Case TypeOf ctrl Is CheckBox
                                Dim chk As Boolean
                                If Boolean.TryParse(values(0), chk) Then
                                    CType(ctrl, CheckBox).Checked = chk
                                End If

                            Case TypeOf ctrl Is NumericUpDown
                                Dim nud = CType(ctrl, NumericUpDown)
                                Dim val As Decimal
                                If Decimal.TryParse(values(0), val) Then
                                    nud.Value = Math.Max(nud.Minimum, Math.Min(nud.Maximum, val))
                                End If
                        End Select
                    End If
                End If

            Catch ex As Exception
                errors.Add($"'{ctrl.Name}': {ex.Message}")
            End Try

            If ctrl.Controls.Count > 0 Then
                LoadControlsRecursive(ctrl.Controls, lookup, dgvData, errors)
            End If
        Next
    End Sub

    Private Sub LoadDGV(dgv As DataGridView, rows As List(Of List(Of String)), errors As List(Of String))
        Try
            AddHandler dgv.DataError, AddressOf SuppressDataError
            dgv.Rows.Clear()

            For Each rowVals As List(Of String) In rows
                Try
                    Dim rowIndex As Integer = dgv.Rows.Add()
                    Dim row As DataGridViewRow = dgv.Rows(rowIndex)

                    For colIndex As Integer = 0 To Math.Min(rowVals.Count, dgv.Columns.Count) - 1
                        Try
                            Dim value As String = rowVals(colIndex)

                            If TypeOf dgv.Columns(colIndex) Is DataGridViewComboBoxColumn Then
                                Dim cbc = CType(dgv.Columns(colIndex), DataGridViewComboBoxColumn)
                                If value <> "" AndAlso Not cbc.Items.Contains(value) Then
                                    cbc.Items.Add(value)
                                End If
                            End If

                            row.Cells(colIndex).Value = If(value = "", Nothing, value)

                        Catch ex As Exception
                            errors.Add($"{dgv.Name} cell: {ex.Message}")
                        End Try
                    Next

                Catch ex As Exception
                    errors.Add($"{dgv.Name} row: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            errors.Add($"DGV '{dgv.Name}': {ex.Message}")
        Finally
            RemoveHandler dgv.DataError, AddressOf SuppressDataError
        End Try
    End Sub

    Private Sub SuppressDataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub

    Private Function FindControlByName(parent As Control, name As String) As Control
        For Each ctrl As Control In parent.Controls
            If ctrl.Name.Equals(name, StringComparison.OrdinalIgnoreCase) Then
                Return ctrl
            End If
            If ctrl.Controls.Count > 0 Then
                Dim found As Control = FindControlByName(ctrl, name)
                If found IsNot Nothing Then Return found
            End If
        Next
        Return Nothing
    End Function

    Private Function SanitizeFileName(name As String) As String
        Dim result As String = name
        For Each c As Char In Path.GetInvalidFileNameChars()
            result = result.Replace(c.ToString(), "_")
        Next
        Return result
    End Function

    Private Function TruncateSheetName(name As String) As String
        If name.Length > 31 Then Return name.Substring(0, 31)
        Return name
    End Function

End Module