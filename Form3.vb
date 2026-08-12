Public Class Form3

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeStiffenerGrid()

    End Sub

    Public Sub InitializeStiffenerGrid()

        '=====================================================
        ' ✅ Basic Grid Setup
        '=====================================================
        With DGV_Stiffener

            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = True
            .MultiSelect = False

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RowHeadersVisible = False

        End With

        '=====================================================
        ' ✅ Ensure Nozzle_Mark Column Exists
        '=====================================================
        If Not DGV_Stiffener.Columns.Contains("Nozzle_Mark_Stiff") Then
            Throw New Exception("❌ Column 'Nozzle_Mark_Stiff' not found in DGV_Stiffener. Please create it manually.")
        End If

        '=====================================================
        ' ✅ Optional: Clear Old Data
        '=====================================================
        DGV_Stiffener.Rows.Clear()

    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        SaveStiffenerDataToBackend()

    End Sub

    Public Sub SaveStiffenerDataToBackend()

        ' Clear old values
        StiffenerDict.Clear()

        ' Loop all rows
        For Each row As DataGridViewRow In DGV_Stiffener.Rows

            If row.IsNewRow Then Continue For
            If row.Cells("Nozzle_Mark_Stiff").Value Is Nothing Then Continue For

            Dim nozzleMark As String =
                row.Cells("Nozzle_Mark_Stiff").Value.ToString().Trim().ToUpper()

            Dim info As New Form1.StiffenerInfo()

            Double.TryParse(row.Cells("Height_Stiff").Value?.ToString(), info.HeightMm)
            Double.TryParse(row.Cells("Dist_Stiff").Value?.ToString(), info.DistanceMm)
            Double.TryParse(row.Cells("Angle_Stiff").Value?.ToString(), info.AngleDeg)
            Integer.TryParse(row.Cells("Number_Stiff").Value?.ToString(), info.Count)
            Double.TryParse(row.Cells("ANGLE_BW_STIFF").Value?.ToString(), info.anglebwstiffener)

            Dim dirObj = row.Cells("Direction_Stiff").Value
            info.Direction = If(dirObj IsNot Nothing, dirObj.ToString().Trim().ToUpper(), "NORMAL")

            ' Store in backend
            StiffenerDict(nozzleMark) = info

        Next

    End Sub

    Private Sub DGV_Stiffener_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Stiffener.CellValueChanged

        SaveStiffenerDataToBackend()

    End Sub

End Class