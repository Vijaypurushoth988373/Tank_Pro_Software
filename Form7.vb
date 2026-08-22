Option Strict Off
Option Explicit On
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Inventor
Imports Project_CD._24._12.Form1
Imports VB = Microsoft.VisualBasic


Public Class Form7

    Private InventorApp As Inventor.Application

#Region "HORIZONTAL TANK"

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click
        StartInventorAssemblyWorkflow_Horizontal()
    End Sub

    Sub StartInventorAssemblyWorkflow_Horizontal()

        Dim invApp As Inventor.Application = Nothing
        Dim folderPath As String = ""

        '==================================================
        ' A) Detect Inventor status (SAFE – WILL NOT STOP)
        '==================================================
        Try
            If IsInventorRunning() Then

                Try
                    invApp = CType(Marshal.GetActiveObject("Inventor.Application"), Inventor.Application)
                Catch ex As Exception
                    invApp = Nothing
                End Try

                If invApp IsNot Nothing Then
                    Try
                        If HasActiveProject(invApp) Then
                            folderPath = IO.Path.GetDirectoryName(invApp.DesignProjectManager.ActiveDesignProject.FullFileName)
                        Else
                            folderPath = SelectInventorFolder()
                            If folderPath <> "" Then
                                ActivateInventorProject(invApp, folderPath)
                            End If
                        End If
                    Catch ex As Exception
                        folderPath = SelectInventorFolder()
                        If folderPath <> "" Then
                            ActivateInventorProject(invApp, folderPath)
                        End If
                    End Try
                End If

            Else

                Try
                    invApp = CType(CreateObject("Inventor.Application"), Inventor.Application)
                    invApp.Visible = True
                Catch ex As Exception
                    invApp = Nothing
                End Try

                If invApp IsNot Nothing Then
                    Try
                        Dim ipjPath As String = ""
                        If SelectedClient = "ARAMCO" Then
                            ipjPath = "D:\HORIZONTAL_TANK\ARAMCO_HOR_VESSEL\ARAMCO_HOR_VESSEL.ipj"
                        ElseIf SelectedClient = "ADNOC" Then
                            ipjPath = "D:\HORIZONTAL_TANK\ADNOC_HOR_VESSEL\ADNOC_HOR_VESSEL.ipj"
                        ElseIf SelectedClient = "QATAR" Then
                            ipjPath = "D:\HORIZONTAL_TANK\ADNOC_HOR_VESSEL\ADNOC_HOR_VESSEL.ipj"
                        End If
                        folderPath = IO.Path.GetDirectoryName(ipjPath)
                        ActivateInventorProject(invApp, folderPath)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End If

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        '==================================================
        ' ✅ SAVE INPUTS — RIGHT AFTER folderPath IS FINALISED
        ' This is the single correct place — covers ALL paths above
        '==================================================
        If Not String.IsNullOrWhiteSpace(folderPath) AndAlso IO.Directory.Exists(folderPath) Then
            CurrentProjectFolder = folderPath
            SaveProjectInputs(Me, folderPath)
            Debug.Print($"✅ Inputs saved to: {folderPath}")
        Else
            Debug.Print($"⚠ SaveProjectInputs skipped — folderPath = '{folderPath}'")
        End If

        '==================================================
        ' B) Create main assembly (SAFE – WILL NOT STOP)
        '==================================================
        Dim asmDoc As AssemblyDocument = Nothing

        Try
            asmDoc = CreateNewAssembly(invApp, folderPath)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            asmDoc = Nothing
        End Try

        '==================================================
        ' CLIENT-SPECIFIC COMPONENT PATHS
        '==================================================

        Dim clientFolder As String = folderPath
        Dim topCoverOcc As ComponentOccurrence = Nothing

        If SelectedClient = "ADNOC" Then
            clientFolder = IO.Path.Combine(folderPath, "ADNOC")
        ElseIf SelectedClient = "QATAR" Then
            clientFolder = IO.Path.Combine(folderPath, "QATAR")
        ElseIf SelectedClient = "ARAMCO" Then
            clientFolder = IO.Path.Combine(folderPath, "ARAMCO")
        End If

        Dim basepartpath As String = ""
        Dim leftheadPath As String = ""
        Dim shellPath As String = ""
        Dim shellPath1 As String = ""
        Dim shellPath2 As String = ""
        Dim rightheadPath As String = ""

        '------------------------------------------
        ' BASE PART
        '------------------------------------------
        Try
            If SelectedClient = "ARAMCO" Then
                basepartpath = IO.Path.Combine(folderPath, "Base_Part.ipt")
            ElseIf SelectedClient = "ADNOC" Then
                basepartpath = IO.Path.Combine(folderPath, "Base_Part_New.ipt")
            ElseIf SelectedClient = "QATAR" Then
                basepartpath = IO.Path.Combine(folderPath, "Base_Part_New.ipt")
            End If


            If Not IO.File.Exists(basepartpath) Then
                basepartpath = SelectInventorFile("Select Tank Shell Plate File")
            End If
        Catch ex As Exception
            basepartpath = SelectInventorFile("Select Base Part File")
        End Try

        '------------------------------------------
        'LEFT HEAD
        '------------------------------------------
        Try
            If RB_LEFT_HEAD_EH.Checked Then

                If SelectedClient = "ARAMCO" Then
                    leftheadPath = IO.Path.Combine(folderPath, "ARAMCO\LEFT_HEAD.ipt")
                ElseIf SelectedClient = "ADNOC" Then
                    leftheadPath = IO.Path.Combine(folderPath, "ADNOC\LEFT_HEAD.ipt")
                End If

                If Not IO.File.Exists(leftheadPath) Then
                    leftheadPath = SelectInventorFile("Select Ellipsoidal Head File")
                End If

            ElseIf RB_LEFT_HEAD_TH.Checked Then

                If SelectedClient = "ARAMCO" Then
                    leftheadPath = IO.Path.Combine(folderPath, "ARAMCO\LEFT_HEAD.ipt")
                ElseIf SelectedClient = "ADNOC" Then
                    leftheadPath = IO.Path.Combine(folderPath, "ADNOC\LEFT_HEAD.ipt")
                End If

                If Not IO.File.Exists(leftheadPath) Then
                    leftheadPath = SelectInventorFile("Select Ellipsoidal Head File")
                End If

            End If

        Catch ex As Exception
            leftheadPath = SelectInventorFile("Select Head File")
        End Try

        '------------------------------------------
        ' SHELL
        '------------------------------------------
        Try
            If SelectedClient = "ARAMCO" Then
                shellPath = IO.Path.Combine(folderPath, "ARAMCO\SHELL.ipt")
            ElseIf SelectedClient = "ADNOC" Then
                shellPath = IO.Path.Combine(folderPath, "ADNOC\SHELL_1.ipt")
                shellPath1 = IO.Path.Combine(folderPath, "ADNOC\SHELL_2.ipt")
                shellPath2 = IO.Path.Combine(folderPath, "ADNOC\SHELL_3.ipt")
            End If

            If Not IO.File.Exists(shellPath) Then
                shellPath = SelectInventorFile("Select Tank Shell Plate File")
            End If
        Catch ex As Exception
            shellPath = SelectInventorFile("Select Tank Shell Plate File")
        End Try


        '------------------------------------------
        'RIGHT HEAD
        '------------------------------------------
        Try
            If RB_RIGHT_HEAD_EH.Checked Then

                If SelectedClient = "ARAMCO" Then
                    rightheadPath = IO.Path.Combine(folderPath, "ARAMCO\RIGHT_HEAD.ipt")
                ElseIf SelectedClient = "ADNOC" Then
                    rightheadPath = IO.Path.Combine(folderPath, "ADNOC\RIGHT_HEAD.ipt")
                End If

                If Not IO.File.Exists(rightheadPath) Then
                    rightheadPath = SelectInventorFile("Select Ellipsoidal Head File")
                End If

            ElseIf RB_RIGHT_HEAD_TH.Checked Then

                If SelectedClient = "ARAMCO" Then
                    rightheadPath = IO.Path.Combine(folderPath, "ARAMCO\RIGHT_HEAD.ipt")
                ElseIf SelectedClient = "ADNOC" Then
                    rightheadPath = IO.Path.Combine(folderPath, "ADNOC\RIGHT_HEAD.ipt")
                End If

                If Not IO.File.Exists(rightheadPath) Then
                    rightheadPath = SelectInventorFile("Select Ellipsoidal Head File")
                End If

            End If

        Catch ex As Exception
            rightheadPath = SelectInventorFile("Select Head File")
        End Try

        '==================================================
        ' UPDATE PARAMETERS
        '==================================================

        If SelectedClient = "ARAMCO" Then
            basepartpath = IO.Path.Combine(folderPath, "Base_Part.ipt")
        ElseIf SelectedClient = "ADNOC" Then
            basepartpath = IO.Path.Combine(folderPath, "Base_Part_New.ipt")
        End If

        UpdateBasePlateParameters(invApp, basepartpath)

        '==================================================
        ' Place Ellipsoidal Head (SAFE – WILL NOT STOP)
        '==================================================

        Dim leftheadOcc As ComponentOccurrence = Nothing

        '------------------------------------------
        ' Place Ellipsoidal Head
        '------------------------------------------
        Try
            If asmDoc IsNot Nothing AndAlso leftheadPath <> "" AndAlso IO.File.Exists(leftheadPath) Then
                leftheadOcc = PlaceAndConstrainLeftHead_HOR(invApp, asmDoc, leftheadPath)
            End If
        Catch ex As Exception
            leftheadOcc = Nothing
        End Try

        '==================================================
        ' Place Shell (SAFE – WILL NOT STOP)
        '==================================================
        Dim shellOcc As ComponentOccurrence = Nothing
        Dim shellOcc1 As ComponentOccurrence = Nothing
        Dim shellOcc2 As ComponentOccurrence = Nothing

        Try
            If asmDoc IsNot Nothing Then

                If SelectedClient = "ARAMCO" Then
                    '------------------------------------------
                    ' ARAMCO → SINGLE SHELL
                    '------------------------------------------
                    If shellPath <> "" AndAlso IO.File.Exists(shellPath) Then
                        shellOcc = PlaceAndConstrainShell_HOR(invApp, asmDoc, shellPath)
                    End If

                ElseIf SelectedClient = "ADNOC" Then
                    '------------------------------------------
                    ' ADNOC → THREE SHELLS
                    '------------------------------------------
                    If shellPath <> "" AndAlso IO.File.Exists(shellPath) Then
                        shellOcc = PlaceAndConstrainShell_HOR(invApp, asmDoc, shellPath)
                    End If

                    If shellPath1 <> "" AndAlso IO.File.Exists(shellPath1) Then
                        shellOcc1 = PlaceAndConstrainShell_HOR(invApp, asmDoc, shellPath1)
                    End If

                    If shellPath2 <> "" AndAlso IO.File.Exists(shellPath2) Then
                        shellOcc2 = PlaceAndConstrainShell_HOR(invApp, asmDoc, shellPath2)
                    End If

                End If

            End If
        Catch ex As Exception

            shellOcc = Nothing
            shellOcc1 = Nothing
            shellOcc2 = Nothing

        End Try

        '==================================================
        ' Place Right Head (SAFE – WILL NOT STOP)
        '==================================================

        Dim rightheadOcc As ComponentOccurrence = Nothing

        Try
            If asmDoc IsNot Nothing AndAlso rightheadPath <> "" AndAlso IO.File.Exists(rightheadPath) Then
                rightheadOcc = PlaceAndConstrainRightHead_HOR(invApp, asmDoc, rightheadPath)
            End If
        Catch ex As Exception
            rightheadOcc = Nothing
        End Try

        Try
            CleanExistingNozzleData(folderPath)
        Catch ex As Exception

        End Try

#Region "LEFT SIDE SADDLE"

        '=========================
        ' PLACE & CONSTRAIN LEFT SIDE SADDLE
        '=========================
        Dim saddleLeftOcc As ComponentOccurrence = Nothing
        Dim saddleLeftPath As String

        If SelectedClient = "ARAMCO" Then
            saddleLeftPath = IO.Path.Combine(folderPath, "ARAMCO\LEFT SADDLE\LEFT_SADDLE.iam")
        ElseIf SelectedClient = "ADNOC" Then
            saddleLeftPath = IO.Path.Combine(folderPath, "ADNOC\LEFT SADDLE\LEFT_SADDLE.iam")
        End If

        If IO.File.Exists(saddleLeftPath) Then
            saddleLeftOcc = asmDoc.ComponentDefinition.Occurrences.Add(saddleLeftPath, invApp.TransientGeometry.CreateMatrix())
            PlaceAndConstrainSaddleLeft(asmDoc, shellOcc, saddleLeftOcc)
        Else
            Debug.Print($"LEFT SADDLE.iam not found: {saddleLeftPath}")
        End If

#End Region

#Region "RIGHT SIDE SADDLE"

        '=========================
        ' PLACE & CONSTRAIN RIGHT SIDE SADDLE
        '=========================
        Dim saddleRightOcc As ComponentOccurrence = Nothing
        Dim saddleRightPath As String

        If SelectedClient = "ARAMCO" Then
            saddleRightPath = IO.Path.Combine(folderPath, "ARAMCO\RIGHT SADDLE\RIGHT_SADDLE.iam")
        ElseIf SelectedClient = "ADNOC" Then
            saddleRightPath = IO.Path.Combine(folderPath, "ADNOC\RIGHT SADDLE\RIGHT_SADDLE.iam")
        End If

        If IO.File.Exists(saddleRightPath) Then
            saddleRightOcc = asmDoc.ComponentDefinition.Occurrences.Add(saddleRightPath, invApp.TransientGeometry.CreateMatrix())
            PlaceAndConstrainSaddleLeft(asmDoc, shellOcc, saddleRightOcc)
        Else
            Debug.Print($"RIGHT SADDLE.iam not found: {saddleRightPath}")
        End If

#End Region

#Region "LIFTING LUG"

        'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_1_.ipt"))                   'LL1
        'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_2_.ipt"))                   'LL2
        'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_3_.ipt"))                   'LL3
        'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_4_.ipt"))                   'LL4

        '=========================
        ' PLACE & CONSTRAIN LIFTING LUG ASSEMBLIES (1 TO 4)
        '=========================
        Dim liftingLugPaths As String()

        If SelectedClient = "ARAMCO" Then
            liftingLugPaths = {IO.Path.Combine(folderPath, "ARAMCO\LIFT_LUG_ASSEMBLY_1\LIFTING_LUG_ASMBY_1.iam"),
        IO.Path.Combine(folderPath, "ARAMCO\LIFT_LUG_ASSEMBLY_2\LIFTING_LUG_ASMBY_2.iam"),
        IO.Path.Combine(folderPath, "ARAMCO\LIFT_LUG_ASSEMBLY_3\LIFTING_LUG_ASMBY_3.iam"),
        IO.Path.Combine(folderPath, "ARAMCO\LIFT_LUG_ASSEMBLY_4\LIFTING_LUG_ASMBY_4.iam")}
        ElseIf SelectedClient = "ADNOC" Then
            liftingLugPaths = {IO.Path.Combine(folderPath, "ADNOC\LIFTING _LUG_1_\LL_ASSEMBLY_1_.iam"),
        IO.Path.Combine(folderPath, "ADNOC\LIFTING _LUG_2_\LL_ASSEMBLY_2_.iam"),
        IO.Path.Combine(folderPath, "ADNOC\LIFTING _LUG_3_\LL_ASSEMBLY_3_.iam"),
        IO.Path.Combine(folderPath, "ADNOC\LIFTING _LUG_4_\LL_ASSEMBLY_4_.iam")}
        ElseIf SelectedClient = "QATAR" Then
            liftingLugPaths = {IO.Path.Combine(folderPath, "QATAR\LIFT_LUG_ASSEMBLY_1\LIFTING_LUG_ASMBY_1.iam"),
        IO.Path.Combine(folderPath, "QATAR\LIFT_LUG_ASSEMBLY_2\LIFTING_LUG_ASMBY_2.iam"),
        IO.Path.Combine(folderPath, "QATAR\LIFT_LUG_ASSEMBLY_3\LIFTING_LUG_ASMBY_3.iam"),
        IO.Path.Combine(folderPath, "QATAR\LIFT_LUG_ASSEMBLY_4\LIFTING_LUG_ASMBY_4.iam")}
        Else
            liftingLugPaths = {}
        End If

        For i As Integer = 0 To liftingLugPaths.Length - 1
            Dim lugPath As String = liftingLugPaths(i)
            Dim lugNumber As Integer = i + 1

            If IO.File.Exists(lugPath) Then
                Dim lugOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(lugPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainLiftingLug(asmDoc, shellOcc, lugOcc, lugNumber)
            Else
                Debug.Print($"LIFTING_LUG_ASMBY_{lugNumber}.iam not found: {lugPath}")
            End If

        Next i

#End Region

#Region "PLATFORM SUPPORT CLEAT - PFC"
        '=========================
        ' PLACE & CONSTRAIN PFC ASSEMBLIES (1 TO 10)
        '=========================
        For i As Integer = 1 To 10

            Dim pfcPath As String = ""

            If SelectedClient = "ARAMCO" Then
                pfcPath = IO.Path.Combine(folderPath, $"ARAMCO\PFC_{i}\PFC_ASMBY_{i}.iam")
            ElseIf SelectedClient = "ADNOC" Then
                pfcPath = IO.Path.Combine(folderPath, $"ADNOC\PFC_{i}_\PFC_ASMBY_{i}_.iam")
            ElseIf SelectedClient = "QATAR" Then
                pfcPath = IO.Path.Combine(folderPath, $"QATAR\PFC_{i}\PFC_ASMBY_{i}.iam")
            End If

            If pfcPath = "" Then Continue For

            If IO.File.Exists(pfcPath) Then
                Dim pfcOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(pfcPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, pfcOcc, $"PFC_ASMBY_{i}")
            Else
                Debug.Print($"PFC_ASMBY_{i}.iam not found: {pfcPath}")
            End If

        Next i
#End Region

#Region "PLATFORM SUPPORT CLEAT - LSC"

        '=========================
        ' PLACE & CONSTRAIN LSC ASSEMBLIES (1 TO 2)
        '=========================
        For i As Integer = 1 To 2
            Dim lscPath As String = IO.Path.Combine(folderPath, $"ARAMCO\LSC_{i}\LSC_ASMBY_{i}.iam")

            If IO.File.Exists(lscPath) Then
                Dim lscOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(lscPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, lscOcc, $"LSC_ASMBY_{i}")
            Else
                Debug.Print($"LSC_ASMBY_{i}.iam not found: {lscPath}")
            End If

        Next i

#End Region

#Region "PIPE SUPPORT CLEAT - LGS/PSC"

        If SelectedClient = "ARAMCO" Then
            '=========================
            ' ARAMCO → 1 LGS
            '=========================
            Dim lgsPath As String = IO.Path.Combine(folderPath, "ARAMCO\LGS_1\LGS_ASMBY_1.iam")

            If IO.File.Exists(lgsPath) Then
                Dim lgsOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(lgsPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, lgsOcc, "LGS_ASMBY_1")
            Else
                Debug.Print($"LGS_ASMBY_1.iam not found: {lgsPath}")
            End If

        ElseIf SelectedClient = "ADNOC" Then
            '=========================
            ' ADNOC → 5 PSC
            '=========================
            For i As Integer = 1 To 5
                Dim pscPath As String = IO.Path.Combine(folderPath, $"ADNOC\PSC_{i}\Assembly_PSC_{i}.iam")

                If IO.File.Exists(pscPath) Then
                    Dim pscOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(pscPath, invApp.TransientGeometry.CreateMatrix())
                    PlaceAndConstrainShellComponent(asmDoc, shellOcc, pscOcc, $"Assembly_PSC_{i}")
                Else
                    Debug.Print($"Assembly_PSC_{i}.iam not found: {pscPath}")
                End If
            Next i

        End If

#End Region

#Region "PIPE SUPPORT - CTC"

        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then

            Dim CTCPath As String = IO.Path.Combine(folderPath, "ADNOC\CTC_1\Assembly_CTC_1.iam")

            If IO.File.Exists(CTCPath) Then
                Dim ctcOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(CTCPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, ctcOcc, "Assembly_CTC_1")
            Else
                Debug.Print($"Assembly_CTC_1.iam not found: {CTCPath}")
            End If

        End If

#End Region

#Region "NAME PLATE - ASSEMBLY"

        '=========================
        ' PLACE & CONSTRAIN NAME PLATE ASSEMBLY
        '=========================
        Dim namePlatePath As String = ""

        If SelectedClient = "ARAMCO" Then
            namePlatePath = IO.Path.Combine(folderPath, "ARAMCO\NAME_PLATE\NAME_PLATE_ASSEMBLY.iam")
        ElseIf SelectedClient = "ADNOC" Then
            namePlatePath = IO.Path.Combine(folderPath, "ADNOC\NAME_PLATE\NAME_PLATE_ASSEMBLY.iam")
        ElseIf SelectedClient = "QATAR" Then
            namePlatePath = IO.Path.Combine(folderPath, "QATAR\NAME_PLATE\NAME_PLATE_ASSEMBLY.iam")
        End If

        If namePlatePath <> "" Then
            If IO.File.Exists(namePlatePath) Then
                Dim namePlateOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(namePlatePath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, namePlateOcc, "NAME_PLATE_ASSEMBLY")
            Else
                Debug.Print($"NAME_PLATE_ASSEMBLY.iam not found: {namePlatePath}")
            End If
        End If

#End Region

#Region "SHELL NOZZLES – CUT + PLACE + CONSTRAIN (FROM DGV) - HORIZONTAL NOZZLE"

        '==================================================
        ' Load Master Pipe Table (NPS + SCH)
        '==================================================
        Dim pipeTable As List(Of PipeDim) = LoadPipeTable()

        '==================================================
        ' Loop through Shell Nozzle DataGridView
        '==================================================
        For Each row As DataGridViewRow In DGV_Nozzle_Tab_Shell_HOR.Rows

            If row.IsNewRow Then Continue For

            '==================================================
            ' 1️⃣ READ VALUES FROM SHELL DGV
            '==================================================
            Dim nozzleName As String = If(row.Cells("NOZZLE_MARK_TEXT_HOR").Value IsNot Nothing, row.Cells("NOZZLE_MARK_TEXT_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim nps As String = If(row.Cells("SIZE_TEXT_HOR").Value IsNot Nothing, row.Cells("SIZE_TEXT_HOR").Value.ToString().Trim(), "")
            Dim schedule As String = If(row.Cells("NOZZLE_SCH_SHELL_HOR").Value IsNot Nothing, row.Cells("NOZZLE_SCH_SHELL_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim remarks As String = If(row.Cells("REMARKS_SHELL_HOR").Value IsNot Nothing, row.Cells("REMARKS_SHELL_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim flangeClass As String = If(row.Cells("NOZZLE_FLANGE_CLASS_SHELL_HOR").Value IsNot Nothing, row.Cells("NOZZLE_FLANGE_CLASS_SHELL_HOR").Value.ToString().Trim(), "")
            Dim nozzleDistanceMm As Double = SafeDouble(row.Cells("DISTANCE_HOR").Value)
            Dim angleDeg As Double = SafeDouble(row.Cells("ANGLE_HOR").Value)
            Dim outsideProjMm As Double = SafeDouble(row.Cells("OUTSIDE_PROJECTION_SHELL_HOR").Value)
            Dim pipeThkMm As Double = SafeDouble(row.Cells("NOZZLE_THK_SHELL_HOR").Value)
            Dim rfPadODmm As Double = SafeDouble(row.Cells("RF_PAD_OD_1_HOR").Value)
            Dim rfPadThkMm As Double = SafeDouble(row.Cells("RF_PAD_THK_1_HOR").Value)
            Dim offsetDistMm As Double = SafeDouble(row.Cells("OFFSET_DIST_HOR").Value)

            Dim Straight_face As Double

            '==================================================
            ' SKIP INCOMPLETE ROWS
            '==================================================
            If nozzleName = "" OrElse nps = "" Then Continue For

            '==================================================
            ' 2️⃣ RF PAD FLAG
            '==================================================
            Dim includePad As Boolean = (rfPadODmm > 0 AndAlso rfPadThkMm > 0)

            '==================================================
            ' 3️⃣ MANWAY DETECTION
            '==================================================
            Dim isManway As Boolean = remarks.Contains("MANWAY") OrElse
                                  (nozzleName.StartsWith("M") AndAlso nozzleName.Length <= 3)

            '==================================================
            ' 4️⃣ DIP PIPE DETECTION
            '==================================================
            Dim isDipPipe As Boolean = remarks = "DIP PIPE" OrElse
                                   remarks = "DIP PIPE (CHANNEL)" OrElse
                                   remarks = "DIP PIPE (ANGLE)"

            '==================================================
            ' 5️⃣ PIPE LENGTH
            '==================================================
            Dim pipeLengthMm As Double = CalculatePipeLength_Shell_HOR(nps, outsideProjMm, flangeClass)
            row.Tag = pipeLengthMm

            '==================================================
            ' 6️⃣ DERIVE PIPE / NOZZLE DIMENSIONS
            '==================================================
            Dim nozzleODmm As Double = 0.0
            Dim pipeIDmm As Double = 0.0

            If isManway Then
                nozzleODmm = GetPipeOD_mm(nps)

            ElseIf isDipPipe Then
                Dim pipe As PipeDim = GetPipeDim_BySizeAndSchedule(pipeTable, nps, schedule)
                If pipe Is Nothing Then Continue For
                nozzleODmm = pipe.OD
                pipeIDmm = pipe.ID

            Else
                Dim pipe As PipeDim = GetPipeDim_BySizeAndSchedule(pipeTable, nps, schedule)
                If pipe Is Nothing Then Continue For
                nozzleODmm = pipe.OD
                pipeIDmm = pipe.ID
            End If

            Dim rfPadIDmm As Double = nozzleODmm

            '==================================================
            ' 7️⃣ CREATE SHELL NOZZLE CUT
            '==================================================
            Try
                If SelectedClient = "ARAMCO" Then
                    '--------------------------------------------------
                    ' ARAMCO → SINGLE SHELL
                    '--------------------------------------------------
                    If shellOcc IsNot Nothing Then
                        CreateShellNozzle_CircleCut_HOR(invApp:=invApp, shellOcc:=shellOcc, nozzleIndex:=nozzleName,
                nozzleDiaMm:=nozzleODmm, distMm:=nozzleDistanceMm, offsetMm:=offsetDistMm, angleDeg)
                    End If

                ElseIf SelectedClient = "ADNOC" Then

                    Straight_face = CDbl(txt_EH_SF.Text)

                    '--------------------------------------------------
                    ' ADNOC → 3 SHELLS
                    ' SHELL_1 : 0     to 1900  mm
                    ' SHELL_2 : 1900  to 3900  mm  (1900 + 2000)
                    ' SHELL_3 : 3900  to 5900  mm  (3900 + 2000)
                    '--------------------------------------------------
                    Dim shell1Length As Double = 1900
                    Dim shell2Length As Double = 2000
                    Dim shell3Length As Double = 2000

                    Dim shell1End As Double = shell1Length                          ' 1900
                    Dim shell2End As Double = shell1Length + shell2Length           ' 3900
                    Dim shell3End As Double = shell1Length + shell2Length + shell3Length ' 5900

                    If nozzleDistanceMm <= shell1End Then
                        '------------------------------------------
                        ' Nozzle falls on SHELL_1
                        '------------------------------------------
                        If shellOcc IsNot Nothing Then
                            CreateShellNozzle_CircleCut_HOR(invApp:=invApp, shellOcc:=shellOcc,
                    nozzleIndex:=nozzleName, nozzleDiaMm:=nozzleODmm,
                    distMm:=-(nozzleDistanceMm - Straight_face), offsetMm:=offsetDistMm, angleDeg)
                        End If

                    ElseIf nozzleDistanceMm <= shell2End Then
                        '------------------------------------------
                        ' Nozzle falls on SHELL_2
                        ' Offset distance relative to SHELL_2 start
                        '------------------------------------------
                        If shellOcc1 IsNot Nothing Then
                            Dim distRelToShell2 As Double = nozzleDistanceMm
                            CreateShellNozzle_CircleCut_HOR(invApp:=invApp, shellOcc:=shellOcc1,
                    nozzleIndex:=nozzleName, nozzleDiaMm:=nozzleODmm,
                    distMm:=-(distRelToShell2 - Straight_face), offsetMm:=offsetDistMm, angleDeg)
                        End If

                    ElseIf nozzleDistanceMm <= shell3End Then
                        '------------------------------------------
                        ' Nozzle falls on SHELL_3
                        ' Offset distance relative to SHELL_3 start
                        '------------------------------------------
                        If shellOcc2 IsNot Nothing Then
                            Dim distRelToShell3 As Double = nozzleDistanceMm
                            CreateShellNozzle_CircleCut_HOR(invApp:=invApp, shellOcc:=shellOcc2,
                    nozzleIndex:=nozzleName, nozzleDiaMm:=nozzleODmm,
                    distMm:=-(distRelToShell3 - Straight_face), offsetMm:=offsetDistMm, angleDeg)
                        End If

                    Else
                        Debug.WriteLine($"❌ Nozzle {nozzleName} distance {nozzleDistanceMm}mm exceeds total shell length {shell3End}mm")
                    End If

                End If

            Catch ex As Exception
                Debug.WriteLine($"❌ Shell cut failed for {nozzleName}: {ex.Message}")
            End Try

            '==================================================
            ' 8️⃣ CREATE NOZZLE SUB-ASSEMBLY
            '==================================================
            Dim nozAsm As AssemblyDocument = CreateNozzleSubAssembly(invApp, folderPath, "Nozzle_" & nozzleName)

            Dim parts As Dictionary(Of String, String) = GetNozzleComponentPaths_HOR(
                invApp, projectFolder:=folderPath, nozzleName:=nozzleName, nps:=nps,
                remarks:=remarks, isManway:=isManway, angleDeg:=angleDeg,
                offsetDistMm:=offsetDistMm, includePad:=includePad, flangeClass:=flangeClass)

            '==================================================
            ' 9️⃣ BUILD NOZZLE SUB-ASSEMBLY
            '==================================================
            Dim dipType As String = If(parts.ContainsKey("DIP_PIPE_TYPE"), parts("DIP_PIPE_TYPE"), "")

            If isDipPipe Then

                Select Case dipType

                    Case "DIP PIPE"
                        '--------------------------------------------------
                        ' 🟡 DIP PIPE → SINGLE PIPE + FLANGE + RF PAD
                        '--------------------------------------------------
                        If Not parts.ContainsKey("PIPE1") OrElse Not parts.ContainsKey("FLANGE") Then
                            Throw New Exception($"❌ Missing PIPE1 or FLANGE for DIP PIPE nozzle {nozzleName}")
                        End If

                        Dim padPath_DP As String = If(parts.ContainsKey("PAD"), parts("PAD"), "")

                        BuildNozzleAssembly_Shell_HOR(invApp, nozAsm, parts("PIPE1"), parts("FLANGE"),
            padPath_DP, SelectedClient, nozzleName, nozzleODmm, pipeThkMm,
            pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm,
            nps, isManway, nozzleDistanceMm - Straight_face, offsetDistMm, angleDeg,
            pipeIDmm:=pipeIDmm)

                    Case "DIP PIPE (CHANNEL)"
                        '--------------------------------------------------
                        ' 🟠 DIP PIPE (CHANNEL)
                        '--------------------------------------------------
                        If SelectedClient = "ARAMCO" Then
                            '--------------------------------------------------
                            ' ARAMCO → TWO PIPES + CONNECTING PLATES + GASKET
                            '--------------------------------------------------
                            If Not parts.ContainsKey("PIPE1") OrElse Not parts.ContainsKey("PIPE2") OrElse Not parts.ContainsKey("FLANGE") Then
                                Throw New Exception($"❌ Missing PIPE1/PIPE2/FLANGE for DIP PIPE (CHANNEL) nozzle {nozzleName}")
                            End If
                            Dim padPath_DPC As String = If(parts.ContainsKey("PAD"), parts("PAD"), "")
                            BuildNozzleAssembly_DipPipe_Channel_HOR(invApp, nozAsm, parts("PIPE1"), parts("PIPE2"),
    parts("CONNECTING_PLATE_1"), parts("GASKET"), parts("CONNECTING_PLATE_2"), parts("FLANGE"), padPath_DPC,
    SelectedClient, nozzleName, nozzleODmm, pipeThkMm, pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm, nps, nozzleDistanceMm, offsetDistMm, angleDeg)
                            ' nozzleLocation not passed — defaults to "" (Shell logic) ✅

                        ElseIf SelectedClient = "ADNOC" Then
                            '--------------------------------------------------
                            ' ADNOC → SINGLE PIPE + FLANGE + RF PAD
                            '--------------------------------------------------
                            If Not parts.ContainsKey("PIPE1") OrElse Not parts.ContainsKey("FLANGE") Then
                                Throw New Exception($"❌ Missing PIPE1 or FLANGE for ADNOC DIP PIPE (CHANNEL) nozzle {nozzleName}")
                            End If
                            Dim padPath_ADNOC As String = If(parts.ContainsKey("PAD"), parts("PAD"), "")
                            BuildNozzleAssembly_Shell_HOR(invApp, nozAsm,
            parts("PIPE1"), parts("FLANGE"), padPath_ADNOC,
            SelectedClient, nozzleName, nozzleODmm, pipeThkMm,
            pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm,
            nps, isManway, nozzleDistanceMm - Straight_face, offsetDistMm, angleDeg,
            pipeIDmm:=pipeIDmm)

                        End If

                    Case "DIP PIPE (ANGLE)"
                        '--------------------------------------------------
                        ' 🔴 DIP PIPE (ANGLE) → SINGLE PIPE
                        '--------------------------------------------------
                        If Not parts.ContainsKey("PIPE1") OrElse Not parts.ContainsKey("FLANGE") Then
                            Throw New Exception($"❌ Missing PIPE1 or FLANGE for DIP PIPE (ANGLE) nozzle {nozzleName}")
                        End If
                        Dim padPath_DPA As String = If(parts.ContainsKey("PAD"), parts("PAD"), "")
                        BuildNozzleAssembly_DipPipe_Angle_HOR(invApp, nozAsm,
        parts("PIPE1"), parts("FLANGE"), padPath_DPA,
        SelectedClient, nozzleName, nozzleODmm, pipeThkMm,
        pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm,
        nps, nozzleDistanceMm - Straight_face, offsetDistMm, angleDeg,
        pipeIDmm:=pipeIDmm)

                End Select

            Else
                '--------------------------------------------------
                ' 🟢 STANDARD / MANWAY → SINGLE PIPE
                '--------------------------------------------------
                If Not parts.ContainsKey("PIPE") OrElse Not parts.ContainsKey("FLANGE") Then
                    Throw New Exception($"❌ Missing PIPE or FLANGE for nozzle {nozzleName}")
                End If
                Dim pipePath As String = parts("PIPE")
                Dim flangePath As String = parts("FLANGE")
                Dim padPath As String = If(parts.ContainsKey("PAD"), parts("PAD"), "")
                Dim pipePtfePath As String = If(parts.ContainsKey("PIPE_PTFE"), parts("PIPE_PTFE"), "")
                Dim flangePtfePath As String = If(parts.ContainsKey("FLANGE_PTFE"), parts("FLANGE_PTFE"), "")
                Dim padPtfePath As String = If(parts.ContainsKey("PAD_PTFE"), parts("PAD_PTFE"), "")
                BuildNozzleAssembly_Shell_HOR(invApp, nozAsm,
        pipePath, flangePath, padPath,
        SelectedClient, nozzleName, nozzleODmm, pipeThkMm,
        pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm,
        nps, isManway, nozzleDistanceMm - Straight_face, offsetDistMm, angleDeg,
        pipePtfePath, flangePtfePath, padPtfePath, pipeIDmm)

            End If

            nozAsm.Update()

            '==================================================
            ' 🔟 PLACE & CONSTRAIN NOZZLE TO SHELL
            '==================================================
            Dim nozOcc As ComponentOccurrence = PlaceNozzleSubAssembly(invApp, asmDoc, nozAsm.FullFileName)

            ConstrainNozzleToShell_HOR(asmDoc, shellOcc, nozOcc, nozzleDistanceMm + Straight_face, offsetDistMm,
            angleDeg, nozzleName, SelectedClient, isManway)

        Next

#End Region

#Region "HEAD NOZZLES – CUT + PLACE + CONSTRAIN (FROM DGV) - HORIZONTAL"

        '==================================================
        ' Load Master Pipe Table (NPS + SCH)
        '==================================================
        Dim pipeTable_Head As List(Of PipeDim) = LoadPipeTable()

        If pipeTable_Head Is Nothing OrElse pipeTable_Head.Count = 0 Then
            Debug.Print("⚠ PipeTable is empty — reloading.")
            pipeTable_Head = LoadPipeTable()
        End If

        '==================================================
        ' Loop through Head Nozzle DataGridView
        '==================================================
        For Each row As DataGridViewRow In DGV_Nozzle_Tab_Head_HOR.Rows

            If row.IsNewRow Then Continue For

            '==================================================
            ' 1️⃣ READ VALUES FROM HEAD DGV
            '==================================================
            Dim nozzleName As String = If(row.Cells("NOZZLE_MARK_TEXT_HEAD_HOR").Value IsNot Nothing, row.Cells("NOZZLE_MARK_TEXT_HEAD_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim nps As String = If(row.Cells("SIZE_TEXT_HEAD_HOR").Value IsNot Nothing, row.Cells("SIZE_TEXT_HEAD_HOR").Value.ToString().Trim(), "")
            Dim schedule As String = If(row.Cells("NOZZLE_SCH_HEAD_HOR").Value IsNot Nothing, row.Cells("NOZZLE_SCH_HEAD_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim remarks As String = If(row.Cells("REMARKS_HEAD_HOR").Value IsNot Nothing, row.Cells("REMARKS_HEAD_HOR").Value.ToString().Trim().ToUpper(), "")
            Dim flangeClass As String = If(row.Cells("NOZZLE_FLANGE_CLASS_HEAD_HOR").Value IsNot Nothing, row.Cells("NOZZLE_FLANGE_CLASS_HEAD_HOR").Value.ToString().Trim(), "")
            Dim outsideProjMm As Double = SafeDouble(row.Cells("OUTSIDE_PROJECTION_HEAD_HOR").Value)
            Dim pipeThkMm As Double = SafeDouble(row.Cells("NOZZLE_THK_HEAD_HOR").Value)
            Dim rfPadODmm As Double = SafeDouble(row.Cells("RF_PAD_OD_HEAD_HOR").Value)
            Dim rfPadThkMm As Double = SafeDouble(row.Cells("RF_PAD_THK_HEAD_HOR").Value)
            Dim angleDeg As Double = SafeDouble(row.Cells("ANGLE_HEAD_HOR").Value)
            Dim radiusMm As Double = SafeDouble(row.Cells("RADIUS_HEAD_HOR").Value)
            Dim xDist As Double = SafeDouble(row.Cells("X_HEAD_HOR").Value)
            Dim yDist As Double = SafeDouble(row.Cells("Y_HEAD_HOR").Value)
            Dim nozzleLocation As String = If(row.Cells("NOZZLE_LOCATION_HOR").Value IsNot Nothing,
                                              row.Cells("NOZZLE_LOCATION_HOR").Value.ToString().Trim().ToUpper(), "")

            '==================================================
            ' SKIP INCOMPLETE ROWS
            '==================================================
            If nozzleName = "" OrElse nps = "" Then Continue For

            If nozzleLocation = "" Then
                Debug.Print($"⚠ Nozzle {nozzleName} has no LOCATION. Skipping.")
                Continue For
            End If

            '==================================================
            ' 2️⃣ SELECT HEAD OCCURRENCE
            '==================================================
            Dim headOcc As ComponentOccurrence = Nothing
            If nozzleLocation = "LEFT" Then
                headOcc = leftheadOcc
            ElseIf nozzleLocation = "RIGHT" Then
                headOcc = rightheadOcc
            End If

            If headOcc Is Nothing Then
                Debug.Print($"⚠ Head occurrence for '{nozzleLocation}' is Nothing. Skipping {nozzleName}.")
                Continue For
            End If

            '==================================================
            ' 3️⃣ RF PAD FLAG
            '==================================================
            Dim includePad As Boolean = (rfPadODmm > 0 AndAlso rfPadThkMm > 0)

            '==================================================
            ' 4️⃣ MANWAY DETECTION
            '==================================================
            Dim isManway As Boolean = remarks.Contains("MANWAY") OrElse
                                      (nozzleName.StartsWith("M") AndAlso nozzleName.Length <= 3)

            '==================================================
            ' 5️⃣ NORMALIZE NPS — FIND EXACT MATCH IN PIPE TABLE
            '==================================================
            Dim nozzleODmm As Double = 0.0
            Dim pipeIDmm As Double = 0.0

            '==================================================
            ' EXTRACT NUMERIC PART FROM DGV NPS
            ' e.g. "2''" → "2"
            '==================================================
            Dim npsNumeric As String = ""
            For Each c As Char In nps
                If Char.IsDigit(c) OrElse c = "."c OrElse c = "/"c OrElse c = " "c Then
                    npsNumeric &= c
                End If
            Next
            npsNumeric = npsNumeric.Trim()

            '==================================================
            ' FIND MATCHING ENTRY IN PIPE TABLE BY NUMERIC PART
            ' Then use exact ND string from pipe table
            '==================================================
            Dim npsNorm As String = nps  ' default fallback
            Dim matchingND As String = ""

            For Each p As PipeDim In pipeTable_Head
                Dim ptNumeric As String = ""
                For Each c As Char In p.ND
                    If Char.IsDigit(c) OrElse c = "."c OrElse c = "/"c OrElse c = " "c Then
                        ptNumeric &= c
                    End If
                Next
                ptNumeric = ptNumeric.Trim()

                If ptNumeric = npsNumeric Then
                    matchingND = p.ND
                    npsNorm = p.ND  ' ✅ Use exact string from pipe table
                    Exit For
                End If
            Next

            Debug.Print($"NPS: DGV='{nps}' Numeric='{npsNumeric}' Matched='{npsNorm}'")

            If isManway Then
                nozzleODmm = GetPipeOD_mm(nps)
            Else
                Dim schNorm As String = schedule.Trim().ToUpper()
                Dim pipe As PipeDim = GetPipeDim_BySizeAndSchedule(pipeTable_Head, npsNorm, schNorm)

                If pipe Is Nothing Then
                    Debug.Print($"⚠ Pipe not found NPS='{npsNorm}' SCH='{schNorm}' — Skipping {nozzleName}")
                    Continue For
                End If

                nozzleODmm = pipe.OD
                pipeIDmm = pipe.ID
                Debug.Print($"✅ Pipe found: OD={nozzleODmm} ID={pipeIDmm}")
            End If

            '==================================================
            ' 6️⃣ PIPE LENGTH
            '==================================================
            Dim pipeLengthMm As Double = CalculatePipeLength_Head_HOR(npsNorm, outsideProjMm, flangeClass)

            '==================================================
            ' 7️⃣ CREATE HEAD NOZZLE CUT
            '==================================================
            Try
                If headOcc IsNot Nothing Then
                    CreateHeadNozzle_CircleCut_HOR(invApp:=invApp, headOcc:=headOcc, nozzleIndex:=nozzleName, nozzleDiaMm:=nozzleODmm,
                        xDist:=xDist, yDist:=yDist, nozzleLocation:=nozzleLocation)
                End If
            Catch ex As Exception
                Debug.WriteLine($"❌ Head cut failed for {nozzleName}: {ex.Message}")
            End Try

            '==================================================
            ' 8️⃣ CREATE NOZZLE SUB-ASSEMBLY
            '==================================================
            Dim nozAsm_Head As AssemblyDocument = CreateNozzleSubAssembly(invApp, folderPath, "Nozzle_" & nozzleName)

            '✅ Use HEAD-specific function — no DIP PIPE, quadrant-based pipe/pad
            Dim parts_Head As Dictionary(Of String, String) = GetNozzleComponentPaths_HEAD_HOR(invApp, projectFolder:=folderPath, nozzleName:=nozzleName, nps:=npsNorm,
            remarks:=remarks, isManway:=isManway, xDist:=xDist, yDist:=yDist, nozzleLocation:=nozzleLocation, includePad:=includePad, flangeClass:=flangeClass)

            '==================================================
            ' 9️⃣ BUILD NOZZLE SUB-ASSEMBLY
            '==================================================
            If Not parts_Head.ContainsKey("PIPE") OrElse Not parts_Head.ContainsKey("FLANGE") Then
                Debug.Print($"❌ Missing PIPE or FLANGE for head nozzle {nozzleName} — Skipping.")
                Continue For
            End If

            Dim pipePath_Head As String = parts_Head("PIPE")
            Dim flangePath_Head As String = parts_Head("FLANGE")
            Dim padPath_Head As String = If(parts_Head.ContainsKey("PAD"), parts_Head("PAD"), "")

            BuildNozzleAssembly_Head_HOR(invApp, nozAsm_Head, pipePath_Head, flangePath_Head, padPath_Head, SelectedClient, nozzleName, nozzleODmm, pipeThkMm, pipeLengthMm, outsideProjMm, rfPadODmm, rfPadThkMm,
            npsNorm, isManway, radiusMm, angleDeg, xDist, yDist, nozzleLocation, pipeIDmm)

            nozAsm_Head.Update()

            '==================================================
            ' 🔟 PLACE & CONSTRAIN NOZZLE TO HEAD
            '==================================================
            Dim nozOcc_Head As ComponentOccurrence = PlaceNozzleSubAssembly(invApp, asmDoc, nozAsm_Head.FullFileName)
            ConstrainNozzleToHead_HOR(asmDoc, headOcc, nozOcc_Head, radiusMm, angleDeg, xDist, yDist, nozzleName, nozzleLocation, SelectedClient, isManway)

        Next

#End Region

#Region "MANWAY GASKET & COVER"

        '==================================================
        ' 🟢 PLACE & CONSTRAIN MANWAY GASKET AND COVER (DYNAMIC)
        '==================================================

        Dim manwayTags As String() = {"M1", "M2", "M3", "M4"}

        For Each tag As String In manwayTags

            Dim nozzleOcc As ComponentOccurrence = Nothing
            Dim gasketOcc As ComponentOccurrence = Nothing
            Dim coverOcc As ComponentOccurrence = Nothing

            '----------------------------
            ' Find NOZZLE_Mx in assembly
            '----------------------------
            For Each occ As ComponentOccurrence In asmDoc.ComponentDefinition.Occurrences
                If occ.Name.ToUpper().Contains("NOZZLE_" & tag.ToUpper()) Then
                    nozzleOcc = occ
                    Exit For
                End If
            Next

            If nozzleOcc Is Nothing Then
                Debug.Print($"NOZZLE_{tag} not found. Skipping.")
                Continue For
            End If

            '----------------------------
            ' 1️⃣ CLIENT-BASED GASKET PATH
            '----------------------------
            Dim gasketPath As String = ""
            If SelectedClient = "ARAMCO" Then
                gasketPath = IO.Path.Combine(folderPath, "ARAMCO\MANWAY_GASKET.ipt")
            ElseIf SelectedClient = "ADNOC" Then
                gasketPath = IO.Path.Combine(folderPath, "ADNOC\MANWAY_GASKET.ipt")
            ElseIf SelectedClient = "QATAR" Then
                gasketPath = IO.Path.Combine(folderPath, "QATAR\MANWAY_GASKET.ipt")
            End If

            If IO.File.Exists(gasketPath) Then
                gasketOcc = asmDoc.ComponentDefinition.Occurrences.Add(gasketPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainManwayGasket(asmDoc, nozzleOcc, gasketOcc, tag & "_FLANGE")
            Else
                Debug.Print($"Gasket file not found: {gasketPath}. Skipping {tag}.")
                Continue For
            End If

            '----------------------------
            ' 2️⃣ CLIENT-BASED COVER / BLIND FLANGE PATH
            '----------------------------
            Dim coverPath As String = ""
            If SelectedClient = "ARAMCO" Then
                coverPath = IO.Path.Combine(folderPath, "ARAMCO\MANWAY_BLIND_FLANGE.ipt")
            ElseIf SelectedClient = "ADNOC" Then
                coverPath = IO.Path.Combine(folderPath, "ADNOC\MANWAY_BLIND_FLANGE.ipt")
            ElseIf SelectedClient = "QATAR" Then
                coverPath = IO.Path.Combine(folderPath, "QATAR\MANWAY_BLIND_FLANGE.ipt")
            End If

            If IO.File.Exists(coverPath) AndAlso gasketOcc IsNot Nothing Then
                coverOcc = asmDoc.ComponentDefinition.Occurrences.Add(coverPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainManwayCover(asmDoc, gasketOcc, coverOcc)
            Else
                Debug.Print($"Cover file not found: {coverPath}. Skipping {tag}.")
                Continue For
            End If

            '----------------------------
            ' 3️⃣ CLIENT-BASED DAVIT ARM PATH
            '----------------------------
            Dim davitAsmPath As String = ""
            If SelectedClient = "ARAMCO" Then
                davitAsmPath = IO.Path.Combine(folderPath, "ARAMCO\MANWAY_DAVIT_ARM_ASSEMBLY.iam")
            ElseIf SelectedClient = "ADNOC" Then
                If tag = "M1" Then
                    davitAsmPath = IO.Path.Combine(folderPath, "ADNOC\MANWAY_DAVIT_ASSM\MANWAY_DAVIT_ARM_ASSEMBLY.iam")
                ElseIf tag = "M2" Then
                    davitAsmPath = IO.Path.Combine(folderPath, "ADNOC\MANWAY_DAVIT_TOP\MANWAY_DAVIT_ARM_TOP.iam")
                End If
            ElseIf SelectedClient = "QATAR" Then
                davitAsmPath = IO.Path.Combine(folderPath, "QATAR\MANWAY_DAVIT_ARM_ASSEMBLY.iam")
            End If

            If davitAsmPath <> "" AndAlso IO.File.Exists(davitAsmPath) AndAlso nozzleOcc IsNot Nothing Then
                Dim davitOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(davitAsmPath, invApp.TransientGeometry.CreateMatrix())
                If SelectedClient = "ARAMCO" Then
                    PlaceAndConstrainManwayDavit(asmDoc, nozzleOcc, davitOcc, coverOcc, tag & "_FLANGE")
                ElseIf SelectedClient = "ADNOC" Then
                    If tag = "M1" Then
                        PlaceAndConstrainManwayDavit_ADNOC(asmDoc, nozzleOcc, davitOcc, coverOcc, gasketOcc)
                    ElseIf tag = "M2" Then
                        PlaceAndConstrainManwayDavit_ADNOC_M2(asmDoc, davitOcc, coverOcc)
                    End If
                ElseIf SelectedClient = "QATAR" Then
                    PlaceAndConstrainManwayDavit(asmDoc, nozzleOcc, davitOcc, coverOcc, tag & "_FLANGE")
                End If
            Else
                Debug.Print($"Davit arm file not found or NOZZLE_{tag} missing. Skipping {tag}.")
            End If

            '----------------------------
            ' 4️⃣ JACK SCREW LUG MW FLANGE
            ' ✅ ARAMCO ONLY
            '----------------------------
            If SelectedClient = "ARAMCO" Then

                Dim jsOcc As ComponentOccurrence = Nothing
                Dim JS_MW_FLANGE_Path As String = IO.Path.Combine(folderPath, "ARAMCO\JACK_SCREW_LUG_MW_FLANGE.ipt")

                If IO.File.Exists(JS_MW_FLANGE_Path) AndAlso nozzleOcc IsNot Nothing Then
                    jsOcc = asmDoc.ComponentDefinition.Occurrences.Add(JS_MW_FLANGE_Path, invApp.TransientGeometry.CreateMatrix())
                    PlaceAndConstrainJS_MW_FLANGE(asmDoc, nozzleOcc, jsOcc, invApp, tag & "_FLANGE")
                Else
                    Debug.Print($"JS_MW_FLANGE file not found or NOZZLE_{tag} missing. Skipping {tag}.")
                End If

                If jsOcc IsNot Nothing Then
                    CreateCircularPattern_JS_MW_FLANGE(asmDoc, jsOcc, invApp)
                Else
                    Debug.Print($"jsOcc is Nothing. Cannot create circular pattern for {tag}.")
                End If

                '----------------------------
                ' 5️⃣ JACK SCREW LUG MW BLIND FLANGE
                ' ✅ ARAMCO ONLY
                '----------------------------
                Dim jsBFOcc As ComponentOccurrence = Nothing
                Dim blindFlangeOcc As ComponentOccurrence = Nothing
                Dim JS_MW_BLIND_FLANGE_Path As String = IO.Path.Combine(folderPath, "ARAMCO\JACK_SCREW_LUG_MW_BLIND_FLANGE.ipt")

                For Each occ As ComponentOccurrence In asmDoc.ComponentDefinition.Occurrences
                    If occ.Name.ToUpper().Contains("MANWAY_BLIND_FLANGE") Then
                        blindFlangeOcc = occ
                        Exit For
                    End If
                Next

                If IO.File.Exists(JS_MW_BLIND_FLANGE_Path) AndAlso blindFlangeOcc IsNot Nothing Then
                    jsBFOcc = asmDoc.ComponentDefinition.Occurrences.Add(JS_MW_BLIND_FLANGE_Path, invApp.TransientGeometry.CreateMatrix())
                    PlaceAndConstrainJS_MW_BLIND_FLANGE(asmDoc, blindFlangeOcc, jsBFOcc, invApp)
                Else
                    Debug.Print($"JS_MW_BLIND_FLANGE file not found or MANWAY_BLIND_FLANGE missing. Skipping {tag}.")
                End If

                If jsBFOcc IsNot Nothing Then
                    CreateCircularPattern_JS_MW_FLANGE(asmDoc, jsBFOcc, invApp)
                Else
                    Debug.Print($"jsBFOcc is Nothing. Cannot create circular pattern for {tag}.")
                End If

            End If  ' ARAMCO only block ends

        Next tag

#End Region

#Region "LADDER RUNG ASSEMBLY"
        '=========================
        ' PLACE & CONSTRAIN LADDER RUNG ASSEMBLY
        ' ADNOC ONLY
        '=========================
        If SelectedClient = "ADNOC" Then
            Dim ladderRungPath As String = IO.Path.Combine(folderPath, "ADNOC\INSIDE_LADDER\LADDER_RUNG_ASSEMBLY.iam")

            If IO.File.Exists(ladderRungPath) Then
                Dim ladderRungOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(ladderRungPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, ladderRungOcc, "LADDER_RUNG_ASSEMBLY")
            Else
                Debug.Print($"LADDER_RUNG_ASSEMBLY.iam not found: {ladderRungPath}")
            End If
        End If
#End Region

#Region "PIPE SUPPORT - ANGLE"
        '==================================================
        ' PLACE & CONSTRAIN PIPE SUPPORT ANGLE ASSEMBLY
        ' Triggered if ANY nozzle has DIP PIPE (ANGLE)
        '==================================================
        Dim hasDipPipeAngle As Boolean = DGV_Nozzle_Tab_Shell_HOR.Rows.Cast(Of DataGridViewRow)().Where(Function(r) Not r.IsNewRow).Any(Function(r) r.Cells("REMARKS_SHELL_HOR").Value IsNot Nothing AndAlso
                     r.Cells("REMARKS_SHELL_HOR").Value.ToString().Trim().ToUpper() = "DIP PIPE (ANGLE)")

        If hasDipPipeAngle Then

            Dim pipeSupAnglePath As String = ""

            If SelectedClient = "ARAMCO" Then
                pipeSupAnglePath = IO.Path.Combine(folderPath, "ARAMCO\PIPE_SUPPORT_ANGLE\PIPE_SUPPORT_ANGLE_ASSEMBLY.iam")
            ElseIf SelectedClient = "ADNOC" Then
                pipeSupAnglePath = IO.Path.Combine(folderPath, "ADNOC\PIPE_SUPPORT_ANGLE\PIPE_SUPPORT_ANGLE_FOR_NOZZLE.iam")
            ElseIf SelectedClient = "QATAR" Then
                pipeSupAnglePath = IO.Path.Combine(folderPath, "QATAR\PIPE_SUPPORT_ANGLE\PIPE_SUPPORT_ANGLE_ASSEMBLY.iam")
            End If

            If pipeSupAnglePath <> "" AndAlso IO.File.Exists(pipeSupAnglePath) Then
                Dim pipeSupAngleOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(pipeSupAnglePath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, pipeSupAngleOcc, "PIPE_SUPPORT_ANGLE")
            Else
                Debug.Print($"Pipe Support Angle file not found: {pipeSupAnglePath}")
            End If

        End If
#End Region

#Region "PIPE SUPPORT - CHANNEL"
        '==================================================
        ' PLACE & CONSTRAIN PIPE SUPPORT CHANNEL ASSEMBLY
        ' Triggered if ANY nozzle has DIP PIPE (CHANNEL)
        '==================================================
        Dim hasDipPipeChannel As Boolean = DGV_Nozzle_Tab_Shell_HOR.Rows.Cast(Of DataGridViewRow)().Where(Function(r) Not r.IsNewRow).Any(Function(r) r.Cells("REMARKS_SHELL_HOR").Value IsNot Nothing AndAlso
                     r.Cells("REMARKS_SHELL_HOR").Value.ToString().Trim().ToUpper() = "DIP PIPE (CHANNEL)")

        If hasDipPipeChannel Then

            Dim pipeSupChannelPath As String = ""

            If SelectedClient = "ARAMCO" Then
                pipeSupChannelPath = IO.Path.Combine(folderPath, "ARAMCO\PIPE_SUPPORT_CHANNEL\PIPE_SUPPORT_CHANNEL_FOR_NOZZLE.iam")
            ElseIf SelectedClient = "ADNOC" Then
                pipeSupChannelPath = IO.Path.Combine(folderPath, "ADNOC\PIPE_SUPPORT_CHANNEL\PIPE_SUPPORT_CHANNEL_FOR_NOZZLE.iam")
            ElseIf SelectedClient = "QATAR" Then
                pipeSupChannelPath = IO.Path.Combine(folderPath, "QATAR\PIPE_SUPPORT_CHANNEL\PIPE_SUPPORT_CHANNEL_FOR_NOZZLE.iam")
            End If

            If pipeSupChannelPath <> "" AndAlso IO.File.Exists(pipeSupChannelPath) Then
                Dim pipeSupChannelOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(pipeSupChannelPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, pipeSupChannelOcc, "PIPE_SUPPORT_CHANNEL_FOR_NOZZLE")
            Else
                Debug.Print("PIPE_SUPPORT_CHANNEL_FOR_NOZZLE.iam not found: " & pipeSupChannelPath)
            End If

        End If
#End Region

#Region "GRAB RUNG"
        '=========================
        ' PLACE & CONSTRAIN GRAB RUNG
        ' ADNOC ONLY
        '=========================
        If SelectedClient = "ADNOC" Then
            Dim grabRungPath As String = IO.Path.Combine(folderPath, "ADNOC\GRAB_RUNG.ipt")

            If IO.File.Exists(grabRungPath) Then
                Dim grabRungOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(grabRungPath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, grabRungOcc, "GRAB_RUNG")
            Else
                Debug.Print($"GRAB_RUNG.ipt not found: {grabRungPath}")
            End If
        End If
#End Region

#Region "STEP RUNG"
        ''=========================
        '' PLACE & CONSTRAIN STEP RUNGS 1, 2 & 3
        '' ADNOC ONLY
        ''=========================
        'If SelectedClient = "ADNOC" Then
        '    For i As Integer = 1 To 3
        '        Dim stepRungPath As String = IO.Path.Combine(folderPath, $"ADNOC\STEP_RUNG_{i}.ipt")

        '        If IO.File.Exists(stepRungPath) Then
        '            Dim stepRungOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(stepRungPath, invApp.TransientGeometry.CreateMatrix())
        '            PlaceAndConstrainShellComponent(asmDoc, shellOcc, stepRungOcc, $"STEP_RUNG_{i}")
        '        Else
        '            Debug.Print($"STEP_RUNG_{i}.ipt not found: {stepRungPath}")
        '        End If
        '    Next i
        'End If
#End Region

#Region "STIFFENER RING"

        '                'Dim stiffRingOcc As ComponentOccurrence = PlaceAndConstrainStiffenerRing(invApp, asmDoc, folderPath)

#End Region

#Region "PTFE LINING COMPONENTS"
        '=========================
        ' PLACE & CONSTRAIN PTFE LINING COMPONENTS
        ' ARAMCO ONLY, AND ONLY IF THE "PTFE LINING" CHECKBOX IS CHECKED
        '=========================
        If SelectedClient = "ARAMCO" AndAlso CheckBox1.Checked Then

            '--- 1️⃣ SHELL PTFE ---
            Dim shellPtfePath As String = IO.Path.Combine(folderPath, "ARAMCO\PTFE_LINING\SHELL_PTFE.ipt")
            If IO.File.Exists(shellPtfePath) Then
                Dim shellPtfeOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(shellPtfePath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, shellOcc, shellPtfeOcc, "SHELL_PTFE")
            Else
                Debug.Print($"SHELL_PTFE.ipt not found: {shellPtfePath}")
            End If

            '--- 2️⃣ LEFT HEAD PTFE ---
            Dim leftHeadPtfePath As String = IO.Path.Combine(folderPath, "ARAMCO\PTFE_LINING\LEFT_HEAD_PTFE.ipt")
            If IO.File.Exists(leftHeadPtfePath) Then
                Dim leftHeadPtfeOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(leftHeadPtfePath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, leftheadOcc, leftHeadPtfeOcc, "LEFT_HEAD_PTFE")
            Else
                Debug.Print($"LEFT_HEAD_PTFE.ipt not found: {leftHeadPtfePath}")
            End If

            '--- 3️⃣ RIGHT HEAD PTFE ---
            Dim rightHeadPtfePath As String = IO.Path.Combine(folderPath, "ARAMCO\PTFE_LINING\RIGHT_HEAD_PTFE.ipt")
            If IO.File.Exists(rightHeadPtfePath) Then
                Dim rightHeadPtfeOcc As ComponentOccurrence = asmDoc.ComponentDefinition.Occurrences.Add(rightHeadPtfePath, invApp.TransientGeometry.CreateMatrix())
                PlaceAndConstrainShellComponent(asmDoc, rightheadOcc, rightHeadPtfeOcc, "RIGHT_HEAD_PTFE")
            Else
                Debug.Print($"RIGHT_HEAD_PTFE.ipt not found: {rightHeadPtfePath}")
            End If

        End If
#End Region


        Try
            asmDoc.Update()
            asmDoc.Activate()
            invApp.ActiveView.Fit(True)

            SaveProjectInputs(Me, folderPath)

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

#Region "OTHER FUNCTION"

        '        '========================================
        '        ' LET INVENTOR FINISH PROCESSING
        '        '========================================
        '        System.Windows.Forms.Application.DoEvents()
        '        System.Threading.Thread.Sleep(3000)


        '        '========================================
        '        ' CLOSE TEST ASSEMBLY
        '        '========================================
        '        Try
        '            If asmDoc IsNot Nothing Then
        '                'asmDoc.Save()
        '                asmDoc.Close(True)
        '                asmDoc = Nothing
        '            End If
        '        Catch ex As Exception
        '            Debug.WriteLine(ex.Message)
        '        End Try


        '        '========================================
        '        ' WAIT BEFORE QUITTING INVENTOR
        '        '========================================
        '        System.Windows.Forms.Application.DoEvents()
        '        'System.Threading.Thread.Sleep(2000)


        '        '========================================
        '        ' CLOSE INVENTOR SAFELY
        '        '========================================
        '        Try
        '            If invApp IsNot Nothing Then
        '                invApp.Quit()
        '            End If
        '        Catch ex As Exception
        '            Debug.WriteLine(ex.Message)
        '        End Try


        '        '========================================
        '        ' RELEASE COM OBJECTS
        '        '========================================
        '        Try
        '            If asmDoc IsNot Nothing Then
        '                System.Runtime.InteropServices.Marshal.ReleaseComObject(asmDoc)
        '            End If

        '            If invApp IsNot Nothing Then
        '                System.Runtime.InteropServices.Marshal.ReleaseComObject(invApp)
        '            End If
        '        Catch
        '        End Try

        '        asmDoc = Nothing
        '        invApp = Nothing

        '        GC.Collect()
        '        GC.WaitForPendingFinalizers()

        '        '========================================
        '        ' SHOW LOADING UI (3–5 sec)
        '        '========================================
        '        Dim loading As New LOAD()
        '        loading.Show()
        '        loading.Refresh()

        '        System.Windows.Forms.Application.DoEvents()

        '        Try
        '            '==================================================
        '            ' 1) GET OR START INVENTOR
        '            '==================================================
        '            Try
        '                invApp = CType(Marshal.GetActiveObject("Inventor.Application"), Inventor.Application)
        '            Catch
        '                invApp = CType(Activator.CreateInstance(Type.GetTypeFromProgID("Inventor.Application")), Inventor.Application)
        '                invApp.Visible = True
        '            End Try

        '            If invApp Is Nothing Then
        '                MessageBox.Show("❌ Unable to start Inventor.", "Edit 3D")
        '                loading.Close()
        '                Exit Sub
        '            End If

        '            invApp.SilentOperation = True
        '            loading.Close()

        '            '==================================================
        '            ' CONDITION 2: NO DOCUMENT OPEN
        '            '==================================================

        '            '----------------------------------------------
        '            ' 2) ACTIVATE PROJECT
        '            '----------------------------------------------
        '            Dim ipjPath As String = SelectInventorProjectFile()
        '            If String.IsNullOrWhiteSpace(ipjPath) Then Exit Sub

        '            Dim activeProject As Inventor.DesignProject = AddAndActivateProject(invApp, ipjPath)

        '            If activeProject Is Nothing Then Exit Sub


        '            '----------------------------------------------
        '            ' 3) LEG SUPPORT TYPE
        '            '----------------------------------------------
        '            Dim legType As String

        '            If Rbn_Fixed.Checked Then
        '                legType = "ANGLE"
        '            ElseIf Rbn_Sliding.Checked Then
        '                legType = "BEAM"
        '            Else
        '                MessageBox.Show("❌ Select Leg Support Type", "Edit 3D")
        '                Exit Sub
        '            End If


        '            '----------------------------------------------
        '            ' 4) OPEN CORRESPONDING MAIN ASSEMBLY
        '            '----------------------------------------------
        '            asmDoc = OpenMainAssemblyFromProject(invApp, activeProject, legType)
        '            If asmDoc Is Nothing Then Exit Sub

        '            '----------------------------------------------
        '            ' 5) RUN VISIBILITY iLOGIC
        '            '----------------------------------------------
        '            Dim visibilityRulePath As String = GetVisibilityILogicRulePath()

        '            If visibilityRulePath <> "" Then
        '                RunExternalILogicRule(invApp, asmDoc, visibilityRulePath)
        '            End If

        '            '----------------------------------------------
        '            ' 6) UPDATE & SAVE
        '            '----------------------------------------------
        '            'Update_All_Parameters_Only(invApp, asmDoc)

        '            asmDoc.Update()
        '            asmDoc.Save()

        '            MessageBox.Show("3D Model Created Successfully", "Create 3D")

        '        Catch ex As Exception
        '            MessageBox.Show("❌ Edit failed: " & ex.Message, "Create 3D")

        '        Finally
        '            If invApp IsNot Nothing Then
        '                invApp.SilentOperation = False
        '            End If
        '        End Try

#End Region

    End Sub

    Private Sub Update_HOR_BP_Parameters(invApp As Inventor.Application, basepartpath As String)

    End Sub

    Private Sub Update_HOR_EH_Parameters(invApp As Inventor.Application, headPath As String)

    End Sub

    Private Sub Update_HOR_SHELL_Parameters(invApp As Inventor.Application, shellPath As String)

    End Sub


    '==================================================
    ' PLACE & CONSTRAIN LEFT HEAD
    '==================================================
    Public Function PlaceAndConstrainLeftHead_HOR(invApp As Inventor.Application, asmDoc As AssemblyDocument, leftHeadPath As String) As ComponentOccurrence
        '----------------------------------------------
        ' VALIDATE FILE
        '----------------------------------------------
        If Not IO.File.Exists(leftHeadPath) Then
            Throw New Exception("Left Head file not found: " & leftHeadPath)
        End If

        '----------------------------------------------
        ' PLACE LEFT HEAD
        '----------------------------------------------
        Dim asmDef As AssemblyComponentDefinition = asmDoc.ComponentDefinition
        Dim cons As AssemblyConstraints = asmDef.Constraints
        Dim tg As TransientGeometry = invApp.TransientGeometry
        Dim leftHeadOcc As ComponentOccurrence = asmDef.Occurrences.Add(leftHeadPath, tg.CreateMatrix())
        leftHeadOcc.Grounded = False

        '----------------------------------------------
        ' ASSEMBLY ORIGIN PLANE REFERENCES
        '----------------------------------------------
        Dim asmYZ As WorkPlane = asmDef.WorkPlanes.Item("YZ Plane")
        Dim asmXY As WorkPlane = asmDef.WorkPlanes.Item("XY Plane")
        Dim asmXZ As WorkPlane = asmDef.WorkPlanes.Item("XZ Plane")

        '----------------------------------------------
        ' LEFT HEAD REFERENCES
        '----------------------------------------------
        Dim lhDef As PartComponentDefinition = leftHeadOcc.Definition
        Dim lhYZ As WorkPlane = lhDef.WorkPlanes.Item("YZ Plane")
        Dim lhXY As WorkPlane = lhDef.WorkPlanes.Item("XY Plane")
        Dim lhXZ As WorkPlane = lhDef.WorkPlanes.Item("XZ Plane")

        '----------------------------------------------
        ' CREATE PROXIES – LEFT HEAD
        '----------------------------------------------
        Dim lhYZProxy As Object
        Dim lhXYProxy As Object
        Dim lhXZProxy As Object
        leftHeadOcc.CreateGeometryProxy(lhYZ, lhYZProxy)
        leftHeadOcc.CreateGeometryProxy(lhXY, lhXYProxy)
        leftHeadOcc.CreateGeometryProxy(lhXZ, lhXZProxy)

        '----------------------------------------------
        ' APPLY CONSTRAINTS
        '----------------------------------------------
        ' 1️⃣ Assembly YZ ↔ Left Head YZ → Flush
        cons.AddFlushConstraint(asmYZ, lhYZProxy, 0)
        ' 2️⃣ Assembly XY ↔ Left Head XY → Flush
        cons.AddFlushConstraint(asmXY, lhXYProxy, 0)
        ' 3️⃣ Assembly XZ ↔ Left Head XZ → Flush
        cons.AddFlushConstraint(asmXZ, lhXZProxy, 0)

        '----------------------------------------------
        ' UPDATE
        '----------------------------------------------
        asmDoc.Update()
        Return leftHeadOcc

    End Function

    '==================================================
    ' PLACE & CONSTRAIN SHELL
    '==================================================
    Public Function PlaceAndConstrainShell_HOR(invApp As Inventor.Application, asmDoc As AssemblyDocument, shellPath As String) As ComponentOccurrence
        '----------------------------------------------
        ' VALIDATE FILE
        '----------------------------------------------
        If Not IO.File.Exists(shellPath) Then
            Throw New Exception("Shell file not found: " & shellPath)
        End If

        '----------------------------------------------
        ' PLACE SHELL
        '----------------------------------------------
        Dim asmDef As AssemblyComponentDefinition = asmDoc.ComponentDefinition
        Dim cons As AssemblyConstraints = asmDef.Constraints
        Dim tg As TransientGeometry = invApp.TransientGeometry
        Dim shellOcc As ComponentOccurrence = asmDef.Occurrences.Add(shellPath, tg.CreateMatrix())
        shellOcc.Grounded = False

        '----------------------------------------------
        ' ASSEMBLY ORIGIN PLANE REFERENCES
        '----------------------------------------------
        Dim asmYZ As WorkPlane = asmDef.WorkPlanes.Item("YZ Plane")
        Dim asmXZ As WorkPlane = asmDef.WorkPlanes.Item("XZ Plane")
        Dim asmXY As WorkPlane = asmDef.WorkPlanes.Item("XY Plane")

        '----------------------------------------------
        ' SHELL REFERENCES
        '----------------------------------------------
        Dim shellDef As PartComponentDefinition = shellOcc.Definition
        Dim shellYZ As WorkPlane = shellDef.WorkPlanes.Item("YZ Plane")
        Dim shellXZ As WorkPlane = shellDef.WorkPlanes.Item("XZ Plane")
        Dim shellXY As WorkPlane = shellDef.WorkPlanes.Item("XY Plane")

        '----------------------------------------------
        ' CREATE PROXIES – SHELL
        '----------------------------------------------
        Dim shellYZProxy As Object
        Dim shellXZProxy As Object
        Dim shellXYProxy As Object
        shellOcc.CreateGeometryProxy(shellYZ, shellYZProxy)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZProxy)
        shellOcc.CreateGeometryProxy(shellXY, shellXYProxy)

        '----------------------------------------------
        ' APPLY CONSTRAINTS
        '----------------------------------------------
        ' 1️⃣ Assembly YZ ↔ Shell YZ → Flush
        cons.AddFlushConstraint(asmYZ, shellYZProxy, 0)
        ' 2️⃣ Assembly XZ ↔ Shell XZ → Flush
        cons.AddFlushConstraint(asmXZ, shellXZProxy, 0)
        ' 3️⃣ Assembly XY ↔ Shell XY → Flush
        cons.AddFlushConstraint(asmXY, shellXYProxy, 0)

        '----------------------------------------------
        ' UPDATE
        '----------------------------------------------
        asmDoc.Update()
        Return shellOcc

    End Function

    '==================================================
    ' PLACE & CONSTRAIN RIGHT HEAD
    '==================================================
    Public Function PlaceAndConstrainRightHead_HOR(invApp As Inventor.Application, asmDoc As AssemblyDocument, rightHeadPath As String) As ComponentOccurrence
        '----------------------------------------------
        ' VALIDATE FILE
        '----------------------------------------------
        If Not IO.File.Exists(rightHeadPath) Then
            Throw New Exception("Right Head file not found: " & rightHeadPath)
        End If

        '----------------------------------------------
        ' PLACE RIGHT HEAD
        '----------------------------------------------
        Dim asmDef As AssemblyComponentDefinition = asmDoc.ComponentDefinition
        Dim cons As AssemblyConstraints = asmDef.Constraints
        Dim tg As TransientGeometry = invApp.TransientGeometry
        Dim rightHeadOcc As ComponentOccurrence = asmDef.Occurrences.Add(rightHeadPath, tg.CreateMatrix())
        rightHeadOcc.Grounded = False

        '----------------------------------------------
        ' ASSEMBLY ORIGIN PLANE REFERENCES
        '----------------------------------------------
        Dim asmYZ As WorkPlane = asmDef.WorkPlanes.Item("YZ Plane")
        Dim asmXY As WorkPlane = asmDef.WorkPlanes.Item("XY Plane")
        Dim asmXZ As WorkPlane = asmDef.WorkPlanes.Item("XZ Plane")

        '----------------------------------------------
        ' RIGHT HEAD REFERENCES
        '----------------------------------------------
        Dim rhDef As PartComponentDefinition = rightHeadOcc.Definition
        Dim rhYZ As WorkPlane = rhDef.WorkPlanes.Item("YZ Plane")
        Dim rhXY As WorkPlane = rhDef.WorkPlanes.Item("XY Plane")
        Dim rhXZ As WorkPlane = rhDef.WorkPlanes.Item("XZ Plane")

        '----------------------------------------------
        ' CREATE PROXIES – RIGHT HEAD
        '----------------------------------------------
        Dim rhYZProxy As Object
        Dim rhXYProxy As Object
        Dim rhXZProxy As Object
        rightHeadOcc.CreateGeometryProxy(rhYZ, rhYZProxy)
        rightHeadOcc.CreateGeometryProxy(rhXY, rhXYProxy)
        rightHeadOcc.CreateGeometryProxy(rhXZ, rhXZProxy)

        '----------------------------------------------
        ' APPLY CONSTRAINTS
        '----------------------------------------------
        ' 1️⃣ Assembly YZ ↔ Right Head YZ → Flush
        cons.AddFlushConstraint(asmYZ, rhYZProxy, 0)
        ' 2️⃣ Assembly XY ↔ Right Head XY → Flush
        cons.AddFlushConstraint(asmXY, rhXYProxy, 0)
        ' 3️⃣ Assembly XZ ↔ Right Head XZ → Flush
        cons.AddFlushConstraint(asmXZ, rhXZProxy, 0)

        '----------------------------------------------
        ' UPDATE
        '----------------------------------------------
        asmDoc.Update()
        Return rightHeadOcc

    End Function

    Public Function CalculatePipeLength_Shell_HOR(nps As String, outsideProjMm As Double, flangeClass As String) As Double

        Dim pipeLengthMm As Double = outsideProjMm
        Dim flangeHeightMm As Double

        '==================================================
        ' ARAMCO LOGIC
        '==================================================
        flangeHeightMm = GetFlangeHeightMm(nps, flangeClass)
        pipeLengthMm = outsideProjMm - flangeHeightMm

        '==================================================
        ' SAFETY CHECK
        '==================================================
        If pipeLengthMm < 0 Then
            pipeLengthMm = 0
        End If

        Return pipeLengthMm

    End Function

    Public Sub CreateShellNozzle_CircleCut_HOR(invApp As Inventor.Application, shellOcc As ComponentOccurrence, nozzleIndex As String, nozzleDiaMm As Double, distMm As Double, offsetMm As Double, angleDeg As Double)

        '==================================================
        ' UNITS mm → cm
        '==================================================
        Dim distCm As Double = distMm / 10.0
        Dim nozzleDiaCm As Double = nozzleDiaMm / 10.0
        Dim offsetCm As Double = offsetMm / 10.0

        '==================================================
        ' GET SHELL PART
        '==================================================
        Dim partDoc As PartDocument = CType(shellOcc.Definition.Document, PartDocument)
        Dim compDef As PartComponentDefinition = partDoc.ComponentDefinition
        Dim tg As TransientGeometry = invApp.TransientGeometry

        '==================================================
        ' ASSIGN NAMED PARAMETERS
        '==================================================
        Dim params As Parameters = compDef.Parameters
        Dim paramDiaName As String = $"NZ_{nozzleIndex}_DIA"
        Dim paramDistName As String = $"NZ_{nozzleIndex}_DIST"
        Dim paramOffsetName As String = $"NZ_{nozzleIndex}_OFFSET_DIST"

        '----------------------------------------------
        ' CREATE / UPDATE DIA PARAMETER
        '----------------------------------------------
        Try
            params.Item(paramDiaName).Expression = $"{nozzleDiaCm} cm"
        Catch
            params.UserParameters.AddByExpression(paramDiaName, $"{nozzleDiaCm} cm", UnitsTypeEnum.kCentimeterLengthUnits)
        End Try

        '----------------------------------------------
        ' CREATE / UPDATE DIST PARAMETER
        '----------------------------------------------
        Try
            params.Item(paramDistName).Expression = $"{distCm} cm"
        Catch
            params.UserParameters.AddByExpression(paramDistName, $"{distCm} cm", UnitsTypeEnum.kCentimeterLengthUnits)
        End Try

        '----------------------------------------------
        ' CREATE / UPDATE OFFSET PARAMETER
        '----------------------------------------------
        Try
            params.Item(paramOffsetName).Expression = $"{offsetCm} cm"
        Catch
            params.UserParameters.AddByExpression(paramOffsetName, $"{offsetCm} cm", UnitsTypeEnum.kCentimeterLengthUnits)
        End Try

        '==================================================
        ' NORMALIZE ANGLE
        '==================================================
        Dim ang As Integer = CInt(angleDeg Mod 360)

        '==================================================
        ' DETERMINE TARGET PLANE
        '==================================================
        Dim targetPlaneName As String = ""

        Select Case ang
            Case 0, 180
                targetPlaneName = "XZ Plane"
            Case 90, 270
                targetPlaneName = "XY Plane"
            Case Else
                Throw New Exception($"❌ Unsupported nozzle angle: {ang}")
        End Select

        '==================================================
        ' GET TARGET WORK PLANE
        '==================================================
        Dim sketchPlane As WorkPlane = Nothing

        For Each wp As WorkPlane In compDef.WorkPlanes
            If wp.Name.Equals(targetPlaneName, StringComparison.OrdinalIgnoreCase) Then
                sketchPlane = wp
                Exit For
            End If
        Next

        If sketchPlane Is Nothing Then
            Throw New Exception($"❌ {targetPlaneName} not found.")
        End If

        '==================================================
        ' REMOVE OLD FEATURE + SKETCH
        '==================================================
        Dim skName As String = $"NZ_{nozzleIndex}_Sketch"
        Dim cutName As String = $"NZ_{nozzleIndex}_Cut"

        Try
            compDef.Features.ExtrudeFeatures.Item(cutName).Delete()
        Catch
        End Try

        Try
            compDef.Sketches.Item(skName).Delete()
        Catch
        End Try

        '==================================================
        ' CREATE SKETCH
        '==================================================
        Dim sk As PlanarSketch = compDef.Sketches.Add(sketchPlane)
        sk.Name = skName

        '==================================================
        ' PROJECT CENTER POINT
        '==================================================
        Dim centerPoint As WorkPoint = compDef.WorkPoints.Item("Center Point")
        Dim projectedCenter As SketchPoint = sk.AddByProjectingEntity(centerPoint)

        Dim cx As Double = projectedCenter.Geometry.X
        Dim cy As Double = projectedCenter.Geometry.Y

        '==================================================
        ' DETERMINE CIRCLE CENTER
        '==================================================
        Dim circleCenterPt As Point2d

        Select Case ang
            Case 0
                '----------------------------------------------
                ' XZ Plane → Top → Right side
                '----------------------------------------------
                circleCenterPt = tg.CreatePoint2d(cx + distCm, cy + offsetCm)

            Case 180
                '----------------------------------------------
                ' XZ Plane → Bottom → Right side
                '----------------------------------------------
                circleCenterPt = tg.CreatePoint2d(cx + distCm, cy + offsetCm)

            Case 90
                '----------------------------------------------
                ' XY Plane → Flipped to LEFT side → -X
                '----------------------------------------------
                circleCenterPt = tg.CreatePoint2d(cx - distCm, cy + offsetCm)

            Case 270
                '----------------------------------------------
                ' XY Plane → Flipped to LEFT side → -X
                '----------------------------------------------
                circleCenterPt = tg.CreatePoint2d(cx - distCm, cy + offsetCm)

        End Select

        '==================================================
        ' CREATE CIRCLE
        '==================================================
        Dim circ As SketchCircle = sk.SketchCircles.AddByCenterRadius(circleCenterPt, nozzleDiaCm / 2.0)

        '==================================================
        ' 1️⃣ DIAMETER DIMENSION → paramDiaName
        '==================================================
        sk.DimensionConstraints.AddDiameter(circ, tg.CreatePoint2d(circleCenterPt.X + nozzleDiaCm / 2.0, circleCenterPt.Y)).Parameter.Expression = paramDiaName

        '==================================================
        ' 2️⃣ DIST DIMENSION → paramDistName
        '==================================================
        If ang = 0 OrElse ang = 180 Then
            sk.DimensionConstraints.AddTwoPointDistance(projectedCenter, circ.CenterSketchPoint, DimensionOrientationEnum.kHorizontalDim, tg.CreatePoint2d(cx + distCm / 2.0, cy - nozzleDiaCm)).Parameter.Expression = paramDistName
        Else
            '----------------------------------------------
            ' 90°/270° → left side → negative X text position
            '----------------------------------------------
            sk.DimensionConstraints.AddTwoPointDistance(projectedCenter, circ.CenterSketchPoint, DimensionOrientationEnum.kHorizontalDim, tg.CreatePoint2d(cx - distCm / 2.0, cy - nozzleDiaCm)).Parameter.Expression = paramDistName
        End If

        '==================================================
        ' 3️⃣ OFFSET DIMENSION → paramOffsetName
        '==================================================
        If ang = 0 OrElse ang = 180 Then
            sk.DimensionConstraints.AddTwoPointDistance(projectedCenter, circ.CenterSketchPoint, DimensionOrientationEnum.kVerticalDim, tg.CreatePoint2d(cx + distCm + nozzleDiaCm, cy + offsetCm / 2.0)).Parameter.Expression = paramOffsetName
        Else
            '----------------------------------------------
            ' 90°/270° → left side → negative X text position
            '----------------------------------------------
            sk.DimensionConstraints.AddTwoPointDistance(projectedCenter, circ.CenterSketchPoint, DimensionOrientationEnum.kVerticalDim, tg.CreatePoint2d(cx - distCm - nozzleDiaCm, cy - offsetCm / 2.0)).Parameter.Expression = paramOffsetName
        End If

        '==================================================
        ' CREATE PROFILE
        '==================================================
        Dim prof As Profile = sk.Profiles.AddForSolid()

        '==================================================
        ' CREATE CUT
        '==================================================
        Dim cutDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(prof, PartFeatureOperationEnum.kCutOperation)

        Select Case ang
            Case 0
                cutDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
            Case 180
                cutDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection)
            Case 90
                cutDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
            Case 270
                cutDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection)
        End Select

        Dim cutFeat As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(cutDef)
        cutFeat.Name = cutName

        '==================================================
        ' UPDATE PART
        '==================================================
        partDoc.Update()

    End Sub

    '==================================================
    ' CALCULATE PIPE LENGTH FOR HEAD NOZZLE
    '==================================================
    Public Function CalculatePipeLength_Head_HOR(nps As String, outsideProjMm As Double, flangeClass As String) As Double
        Return CalculatePipeLength_Shell_HOR(nps, outsideProjMm, flangeClass)
    End Function

    Public Sub CreateHeadNozzle_CircleCut_HOR(invApp As Inventor.Application, headOcc As ComponentOccurrence, nozzleIndex As String, nozzleDiaMm As Double, xDist As Double, yDist As Double, nozzleLocation As String)
        Dim partDoc As PartDocument = CType(headOcc.Definition.Document, PartDocument)
        Dim compDef As PartComponentDefinition = partDoc.ComponentDefinition
        Dim params As Parameters = compDef.Parameters
        Dim tg As TransientGeometry = invApp.TransientGeometry

        '==================================================
        ' CALCULATE DIMENSIONS (mm → cm)
        '==================================================
        Dim diaCm As Double = nozzleDiaMm / 10.0
        Dim radCm As Double = diaCm / 2.0
        Dim absX As Double = Math.Abs(xDist) / 10.0
        Dim absY As Double = Math.Abs(yDist) / 10.0

        '==================================================
        ' RAW MAPPING — no inversion, diagnostic
        '==================================================
        '==================================================
        ' ✅ AXES SWAPPED on YZ sketch plane
        '    sketch horizontal = DGV Y
        '    sketch vertical    = DGV X
        '==================================================
        Dim finalX As Double = (Math.Abs(yDist) * If(yDist >= 0, 1.0, -1.0)) / 10.0
        Dim finalY As Double = (Math.Abs(xDist) * If(xDist >= 0, 1.0, -1.0)) / 10.0

        '==================================================
        ' DETERMINE QUADRANT
        '==================================================
        Dim quadrant As Integer = 0
        If xDist >= 0 AndAlso yDist >= 0 Then
            quadrant = 1
        ElseIf xDist < 0 AndAlso yDist >= 0 Then
            quadrant = 2
        ElseIf xDist < 0 AndAlso yDist < 0 Then
            quadrant = 3
        ElseIf xDist >= 0 AndAlso yDist < 0 Then
            quadrant = 4
        End If

        '==================================================
        ' LOCATION FLAG
        '==================================================
        Dim isLeft As Boolean = nozzleLocation.ToUpper() = "LEFT"
        Dim locationVal As Integer = If(isLeft, 0, 1)
        Dim cutDirection As PartFeatureExtentDirectionEnum = If(isLeft, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)

        Debug.Print($"Nozzle {nozzleIndex} | finalX={finalX}cm | finalY={finalY}cm | Q={quadrant} | Loc={nozzleLocation}")

        '==================================================
        ' HELPER — ENSURE PARAMETER
        '==================================================
        Dim EnsureParam = Sub(paramName As String, value As Double, units As UnitsTypeEnum)
                              Try
                                  Dim p As UserParameter = Nothing
                                  Try
                                      p = CType(params.Item(paramName), UserParameter)
                                      p.Value = value
                                  Catch
                                      p = params.UserParameters.AddByValue(paramName, value, units)
                                  End Try
                              Catch ex As Exception
                                  Debug.Print($"   ❌ EnsureParam failed for {paramName}: {ex.Message}")
                              End Try
                          End Sub

        '==================================================
        ' SET / CREATE PARAMETERS
        '==================================================
        EnsureParam($"NZ_{nozzleIndex}_DIA", diaCm, UnitsTypeEnum.kCentimeterLengthUnits)
        EnsureParam($"NZ_{nozzleIndex}_X_OFFSET", absX, UnitsTypeEnum.kCentimeterLengthUnits)
        EnsureParam($"NZ_{nozzleIndex}_Y_OFFSET", absY, UnitsTypeEnum.kCentimeterLengthUnits)
        EnsureParam($"NZ_{nozzleIndex}_QUADRANT", CDbl(quadrant), UnitsTypeEnum.kUnitlessUnits)
        EnsureParam($"NZ_{nozzleIndex}_LOCATION", CDbl(locationVal), UnitsTypeEnum.kUnitlessUnits)

        '==================================================
        ' REMOVE EXISTING FEATURE FIRST, THEN SKETCH
        '==================================================
        Dim sketchName As String = $"Nozzle_{nozzleIndex}_Cut_Sketch"
        Dim featureName As String = $"NozzleCut_{nozzleIndex}"

        Try
            For Each feat As PartFeature In compDef.Features
                If feat.Name = featureName Then
                    feat.Delete()
                    Exit For
                End If
            Next
        Catch ex As Exception
            Debug.Print($"   ⚠ Delete feature: {ex.Message}")
        End Try

        Try
            For Each sk As PlanarSketch In compDef.Sketches
                If sk.Name = sketchName Then
                    sk.Delete()
                    Exit For
                End If
            Next
        Catch ex As Exception
            Debug.Print($"   ⚠ Delete sketch: {ex.Message}")
        End Try

        '==================================================
        ' CREATE SKETCH + CUT
        '==================================================
        Try
            Dim sketches As PlanarSketches = compDef.Sketches
            Dim workPlanes As WorkPlanes = compDef.WorkPlanes
            Dim workPoints As WorkPoints = compDef.WorkPoints

            Dim yzPlane As WorkPlane = workPlanes.Item("YZ Plane")
            Dim centerWorkPt As WorkPoint = workPoints.Item("Center Point")

            Dim sketch As PlanarSketch = sketches.Add(yzPlane)
            sketch.Name = sketchName

            Dim projectedCenterPt As SketchPoint = sketch.AddByProjectingEntity(centerWorkPt)

            '==================================================
            ' DRAW CIRCLE AT (finalX, finalY)
            '==================================================
            Dim centerPoint As Point2d = tg.CreatePoint2d(finalX, finalY)
            Dim circle As SketchCircle = sketch.SketchCircles.AddByCenterRadius(centerPoint, radCm)

            '==================================================
            ' ✅ LOCK POSITION — no X/Y constraints at all
            '==================================================
            Try
                sketch.GeometricConstraints.AddFixed(circle.CenterSketchPoint)
                Debug.Print($"   ✅ Circle fixed at ({finalX}, {finalY})")
            Catch ex As Exception
                Debug.Print($"   ⚠ Fixed failed: {ex.Message}")
            End Try

            '==================================================
            ' DIAMETER ONLY
            '==================================================
            Try
                Dim diaConstraint As Object = sketch.DimensionConstraints.AddDiameter(
            circle,
            tg.CreatePoint2d(finalX + radCm + 0.5, finalY))
                diaConstraint.Parameter.Expression = $"NZ_{nozzleIndex}_DIA"
            Catch ex As Exception
                Debug.Print($"   ⚠ Diameter failed: {ex.Message}")
            End Try

            '==================================================
            ' PROFILE + EXTRUDE CUT
            '==================================================
            Dim profile As Profile = sketch.Profiles.AddForSolid()
            Dim extrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kCutOperation)
            extrudeDef.SetThroughAllExtent(cutDirection)
            Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)
            extrude.Name = featureName

            Debug.Print($"✅ Cut created: {nozzleIndex} | Q={quadrant}")

        Catch ex As Exception
            Debug.Print($"❌ Cut creation failed for {nozzleIndex}: {ex.Message}")
        End Try

        '==================================================
        ' UPDATE & SAVE
        '==================================================
        Try
            partDoc.Update()
            partDoc.Save()
        Catch ex As Exception
            Debug.Print($"⚠ Update/Save failed: {ex.Message}")
        End Try
    End Sub

    '==================================================
    ' UPDATED BuildNozzleAssembly_Head_HOR
    '==================================================
    Public Sub BuildNozzleAssembly_Head_HOR(invApp As Inventor.Application, nozAsm As AssemblyDocument, pipePath As String, flangePath As String, rfPadPath As String, SelectedClient As String, nozzleName As String,
    pipeODmm As Double, pipeThkMm As Double, pipeLengthMm As Double, outsideProjMm As Double, rfPadODmm As Double, rfPadThkMm As Double, nps As String, isManway As Boolean, radiusMm As Double, angleDeg As Double, xDist As Double, yDist As Double, nozzleLocation As String,
    Optional pipeIDmm As Double = 0)

        Dim asmDef = nozAsm.ComponentDefinition
        Dim tg = invApp.TransientGeometry

        '================ PIPE =================
        Dim pipeOcc = asmDef.Occurrences.Add(pipePath, tg.CreateMatrix())
        pipeOcc.Name = nozzleName & "_PIPE"
        pipeOcc.Grounded = True

        '✅ Use head-specific parameter setter
        SetPipeDimensions_Head_HOR(pipeOcc, pipeODmm, pipeThkMm, pipeLengthMm, xDist, yDist)

        '================ FLANGE =================
        Dim flangeOcc = asmDef.Occurrences.Add(flangePath, tg.CreateMatrix())
        flangeOcc.Name = nozzleName & "_FLANGE"
        ConstrainFlangeToPipe_Shell_HOR(nozAsm, pipeOcc, flangeOcc, nozzleName, angleDeg, nozzleLocation)
        SetFlangeOutsideProjection(flangeOcc, outsideProjMm)
        If pipeIDmm > 0 Then SetFlangeBore(flangeOcc, pipeIDmm)

        '================ RF PAD =================
        If rfPadPath <> "" Then
            Dim padOcc = asmDef.Occurrences.Add(rfPadPath, tg.CreateMatrix())
            padOcc.Name = nozzleName & "_RF_PAD"

            '✅ Use head-specific RF Pad setter
            SetRFPadParameters_Head_HOR(padOcc, pipeODmm, pipeThkMm, pipeLengthMm,
            xDist, yDist, rfPadODmm, rfPadThkMm)

            ConstrainPadToPipe_Shell_HOR(nozAsm, pipeOcc, padOcc, isManway)
        End If

        nozAsm.Update()
    End Sub

    '==================================================
    ' CONSTRAIN NOZZLE TO HEAD
    '==================================================
    Public Sub ConstrainNozzleToHead_HOR(asmDoc As AssemblyDocument, headOcc As ComponentOccurrence, nozOcc As ComponentOccurrence, radiusMm As Double, angleDeg As Double, xDist As Double, yDist As Double,
    nozzleName As String, nozzleLocation As String, SelectedClient As String, isManway As Boolean)

        '==================================================
        ' Same constraint pattern as ConstrainNozzleToShell_HOR
        ' but references HEAD occurrence instead of SHELL
        '==================================================
        ConstrainNozzleToShell_HOR(asmDoc, headOcc, nozOcc, radiusMm, xDist, angleDeg, nozzleName, SelectedClient, isManway)

    End Sub

    Public Function GetNozzleComponentPaths_HEAD_HOR(invApp As Inventor.Application, projectFolder As String, nozzleName As String, nps As String, remarks As String,
isManway As Boolean, xDist As Double, yDist As Double, nozzleLocation As String, Optional includePad As Boolean = True, Optional flangeClass As String = "150#") As Dictionary(Of String, String)

        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        remarks = If(remarks, "").ToUpper().Trim()

        '==================================================
        ' ENSURE NOZZLES FOLDER EXISTS
        '==================================================
        Dim nozzleFolder As String = ""

        If SelectedClient = "ARAMCO" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "ARAMCO\Nozzles")
        ElseIf SelectedClient = "ADNOC" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "ADNOC\Nozzles")
        ElseIf SelectedClient = "QATAR" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "QATAR\Nozzles")
        End If

        If Not IO.Directory.Exists(nozzleFolder) Then
            IO.Directory.CreateDirectory(nozzleFolder)
        End If

        '==================================================
        ' DETERMINE QUADRANT FROM X & Y
        '==================================================
        Dim quadrant As Integer = 0
        If xDist >= 0 AndAlso yDist >= 0 Then
            quadrant = 1
        ElseIf xDist < 0 AndAlso yDist >= 0 Then
            quadrant = 2
        ElseIf xDist < 0 AndAlso yDist < 0 Then
            quadrant = 3
        ElseIf xDist >= 0 AndAlso yDist < 0 Then
            quadrant = 4
        End If

        Dim quadrantName As String = ""
        Select Case quadrant
            Case 1 : quadrantName = "1ST_QUADRANT"
            Case 2 : quadrantName = "2ND_QUADRANT"
            Case 3 : quadrantName = "3RD_QUADRANT"
            Case 4 : quadrantName = "4TH_QUADRANT"
        End Select

        '==================================================
        ' DETERMINE LH / RH PREFIX BASED ON LOCATION
        '==================================================
        Dim headPrefix As String = If(nozzleLocation.ToUpper() = "LEFT", "LH", "RH")

        Debug.Print($"Head Nozzle {nozzleName} | X={xDist} Y={yDist} | Q={quadrant} | {quadrantName} | Loc={nozzleLocation} | Prefix={headPrefix}")

        '==================================================
        ' GET COMMON FLANGE MASTER
        '==================================================
        Dim flangeMaster As String = ResolveFlangeMaster_ByNPS(projectFolder, nps, flangeClass)

        If Not IO.File.Exists(flangeMaster) Then
            Throw New Exception($"❌ No flange found for NPS {nps} Class {flangeClass}")
        End If

        '==================================================
        ' CLIENT FOLDER
        '==================================================
        Dim clientFolder As String = ""
        If SelectedClient = "ARAMCO" Then
            clientFolder = IO.Path.Combine(projectFolder, "ARAMCO")
        ElseIf SelectedClient = "ADNOC" Then
            clientFolder = IO.Path.Combine(projectFolder, "ADNOC")
        ElseIf SelectedClient = "QATAR" Then
            clientFolder = IO.Path.Combine(projectFolder, "QATAR")
        End If

        '==================================================
        ' PIPE MASTER — BASED ON LOCATION + QUADRANT
        ' LEFT:  PIPE_LH_1ST_QUADRANT.ipt ... 4TH
        ' RIGHT: PIPE_RH_1ST_QUADRANT.ipt ... 4TH
        '==================================================
        Dim pipeMasterFileName As String = $"PIPE_{headPrefix}_{quadrantName}.ipt"
        Dim pipeMaster As String = IO.Path.Combine(projectFolder, pipeMasterFileName)

        If Not IO.File.Exists(pipeMaster) Then
            Throw New Exception($"❌ Head pipe master missing: {pipeMaster}")
        End If

        Dim nozzlePipe As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE.ipt")
        If Not IO.File.Exists(nozzlePipe) Then
            IO.File.Copy(pipeMaster, nozzlePipe, True)
        End If
        result("PIPE") = nozzlePipe
        Debug.Print($"   ✅ Pipe: {pipeMasterFileName}")

        '==================================================
        ' FLANGE
        '==================================================
        Dim nozzleFlange As String = IO.Path.Combine(nozzleFolder, nozzleName & "_FLANGE.ipt")
        If Not IO.File.Exists(nozzleFlange) Then
            IO.File.Copy(flangeMaster, nozzleFlange, True)
        End If
        EnsureFlangeEndPlane(invApp, nozzleFlange, nozzleName)
        result("FLANGE") = nozzleFlange
        Debug.Print($"   ✅ Flange: {IO.Path.GetFileName(nozzleFlange)}")

        '==================================================
        ' OPTIONAL RF PAD — BASED ON LOCATION + QUADRANT
        ' LEFT:  RF_PAD_LH_1ST_QUADRANT.ipt ... 4TH
        ' RIGHT: RF_PAD_RH_1ST_QUADRANT.ipt ... 4TH
        '==================================================
        If includePad Then
            Dim rfPadMasterFileName As String = $"RF_PAD_{headPrefix}_{quadrantName}.ipt"
            Dim rfPadMaster As String = IO.Path.Combine(projectFolder, rfPadMasterFileName)

            If Not IO.File.Exists(rfPadMaster) Then
                Throw New Exception($"❌ Head RF Pad master missing: {rfPadMaster}")
            End If

            Dim padPath As String = IO.Path.Combine(nozzleFolder, nozzleName & "_RF_PAD.ipt")
            If Not IO.File.Exists(padPath) Then
                IO.File.Copy(rfPadMaster, padPath, True)
            End If
            result("PAD") = padPath
            Debug.Print($"   ✅ RF Pad: {rfPadMasterFileName}")
        End If

        Return result

    End Function

    Public Sub BuildNozzleAssembly_Shell_HOR(invApp As Inventor.Application, nozAsm As AssemblyDocument, pipePath As String, flangePath As String, rfPadPath As String, SelectedClient As String, nozzleName As String, pipeODmm As Double,
    pipeThkMm As Double, pipeLengthMm As Double, outsideProjMm As Double, rfPadODmm As Double, rfPadThkMm As Double, nps As String, isManway As Boolean, nozzleDistanceMm As Double, offsetDistMm As Double, angleDeg As Double,
    Optional pipePtfePath As String = "", Optional flangePtfePath As String = "", Optional padPtfePath As String = "", Optional pipeIDmm As Double = 0)

        Dim asmDef = nozAsm.ComponentDefinition
        Dim tg = invApp.TransientGeometry

        '================ PIPE =================
        Dim pipeOcc = asmDef.Occurrences.Add(pipePath, tg.CreateMatrix())
        pipeOcc.Name = nozzleName & "_PIPE"
        pipeOcc.Grounded = True

        SetPipeDimensions_HOR(pipeOcc, pipeODmm, pipeThkMm, pipeLengthMm, nozzleDistanceMm, offsetDistMm)

        '================ PTFE LINING — PIPE =================
        If pipePtfePath <> "" Then
            Dim pipePtfeOcc = asmDef.Occurrences.Add(pipePtfePath, tg.CreateMatrix())
            pipePtfeOcc.Name = nozzleName & "_PIPE_PTFE"
            SetPipeDimensions_HOR(pipePtfeOcc, pipeODmm, pipeThkMm, pipeLengthMm, nozzleDistanceMm, offsetDistMm)
            PlaceAndConstrainShellComponent(nozAsm, pipeOcc, pipePtfeOcc, nozzleName & "_PIPE_PTFE")
        End If

        '================ FLANGE =================
        Dim flangeOcc = asmDef.Occurrences.Add(flangePath, tg.CreateMatrix())
        flangeOcc.Name = nozzleName & "_FLANGE"

        ' Pipe → Flange
        ConstrainFlangeToPipe_Shell_HOR(nozAsm, pipeOcc, flangeOcc, nozzleName, angleDeg)

        SetFlangeOutsideProjection(flangeOcc, outsideProjMm)
        If pipeIDmm > 0 Then SetFlangeBore(flangeOcc, pipeIDmm)

        '================ PTFE LINING — FLANGE =================
        If flangePtfePath <> "" Then
            Dim flangePtfeOcc = asmDef.Occurrences.Add(flangePtfePath, tg.CreateMatrix())
            flangePtfeOcc.Name = nozzleName & "_FLANGE_PTFE"
            PlaceAndConstrainShellComponent(nozAsm, flangeOcc, flangePtfeOcc, nozzleName & "_FLANGE_PTFE")
        End If

        '================ RF PAD =================
        If rfPadPath <> "" Then

            Dim padOcc = asmDef.Occurrences.Add(rfPadPath, tg.CreateMatrix())
            padOcc.Name = nozzleName & "_RF_PAD"

            SetRFPadParameters_HOR(padOcc, rfPadODmm, rfPadThkMm, pipeODmm, nozzleDistanceMm, offsetDistMm)

            ' Pipe → RF PAD
            ConstrainPadToPipe_Shell_HOR(nozAsm, pipeOcc, padOcc, isManway)

            '================ PTFE LINING — RF PAD =================
            If padPtfePath <> "" Then
                Dim padPtfeOcc = asmDef.Occurrences.Add(padPtfePath, tg.CreateMatrix())
                padPtfeOcc.Name = nozzleName & "_RF_PAD_PTFE"
                SetRFPadParameters_HOR(padPtfeOcc, rfPadODmm, rfPadThkMm, pipeODmm, nozzleDistanceMm, offsetDistMm)
                PlaceAndConstrainShellComponent(nozAsm, padOcc, padPtfeOcc, nozzleName & "_RF_PAD_PTFE")
            End If

        End If

        nozAsm.Update()

    End Sub

    Public Sub BuildNozzleAssembly_DipPipe_Angle_HOR(invApp As Inventor.Application, nozAsm As AssemblyDocument, pipePath As String, flangePath As String, rfPadPath As String, SelectedClient As String,
    nozzleName As String, pipeODmm As Double, pipeThkMm As Double, pipeLengthMm As Double, outsideProjMm As Double, rfPadODmm As Double, rfPadThkMm As Double, nps As String, nozzleDistanceMm As Double, offsetDistMm As Double, angleDeg As Double,
    Optional pipeIDmm As Double = 0)

        Dim asmDef = nozAsm.ComponentDefinition
        Dim tg = invApp.TransientGeometry

        '================ PIPE (GROUNDED - NO PARAM UPDATE) =================
        Dim pipeOcc = asmDef.Occurrences.Add(pipePath, tg.CreateMatrix())
        pipeOcc.Name = nozzleName & "_PIPE"
        pipeOcc.Grounded = True

        Dim folderPath As String = "D:\HORIZONTAL_TANK\CD.24.002.C02.001_2400\"

        '================ PIPE SUPPORT BASE =================
        Dim pipeSupportBasePath As String = IO.Path.Combine(folderPath, "ARAMCO\PIPE_SUPPORT_CHANNEL\PIPE_ANG_0_POST_PIPE_SUPPORT_BASE_NEW.ipt")

        If IO.File.Exists(pipeSupportBasePath) Then
            Dim pipeSupportBaseOcc = asmDef.Occurrences.Add(pipeSupportBasePath, tg.CreateMatrix())
            pipeSupportBaseOcc.Name = nozzleName & "_PIPE_SUPPORT_BASE"
            SetPipeDimensions_HOR(pipeSupportBaseOcc, pipeODmm, pipeThkMm, pipeLengthMm, nozzleDistanceMm, offsetDistMm)
        Else
            Debug.Print($"❌ PIPE_SUPPORT_BASE not found: {pipeSupportBasePath}")
        End If

        '================ FLANGE =================
        Dim flangeOcc = asmDef.Occurrences.Add(flangePath, tg.CreateMatrix())
        flangeOcc.Name = nozzleName & "_FLANGE"
        ConstrainFlangeToPipe_Shell_HOR(nozAsm, pipeOcc, flangeOcc, nozzleName, angleDeg)
        SetFlangeOutsideProjection(flangeOcc, outsideProjMm)
        If pipeIDmm > 0 Then SetFlangeBore(flangeOcc, pipeIDmm)

        '================ RF PAD =================
        If rfPadPath <> "" Then
            Dim padOcc = asmDef.Occurrences.Add(rfPadPath, tg.CreateMatrix())
            padOcc.Name = nozzleName & "_RF_PAD"
            SetRFPadParameters_HOR(padOcc, rfPadODmm, rfPadThkMm, pipeODmm, nozzleDistanceMm, offsetDistMm)
            ConstrainPadToPipe_Shell_HOR(nozAsm, pipeOcc, padOcc, False)
        End If

        nozAsm.Update()

    End Sub

    Public Sub ConstrainFlangeToPipe_Shell_HOR(nozAsm As AssemblyDocument, pipeOcc As ComponentOccurrence, flangeOcc As ComponentOccurrence, nozzleName As String, angleDeg As Double,
    Optional nozzleLocation As String = "")

        Dim cons As AssemblyConstraints = nozAsm.ComponentDefinition.Constraints
        Dim invApp As Inventor.Application = nozAsm.Parent

        '==================================================
        ' SAFETY
        '==================================================
        pipeOcc.Grounded = True
        flangeOcc.Grounded = False

        '==================================================
        ' 1️⃣ PIPE (TL) ↔ FLANGE (XZ)
        ' FLUSH
        '==================================================
        Dim pipeTL As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("TL")
        Dim flangeXZ As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim pipeTLp As Object
        Dim flangeXZp As Object
        pipeOcc.CreateGeometryProxy(pipeTL, pipeTLp)
        flangeOcc.CreateGeometryProxy(flangeXZ, flangeXZp)
        cons.AddFlushConstraint(pipeTLp, flangeXZp, 0)

        '==================================================
        ' 2️⃣ PIPE (END_PLANE) ↔ FLANGE (N1_End_Plane)
        ' 180° / 270° → FLUSH · 0° / 90° → MATE
        '==================================================
        Dim pipeEndPlane As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("END_PLANE")
        Dim flangeEndPlane As WorkPlane = GetOrCreateFlangeEndPlane(invApp, flangeOcc, nozzleName)
        flangeEndPlane.Visible = False
        Dim pipeEndPlanep As Object
        Dim flangeEndPlanep As Object
        pipeOcc.CreateGeometryProxy(pipeEndPlane, pipeEndPlanep)
        flangeOcc.CreateGeometryProxy(flangeEndPlane, flangeEndPlanep)

        If angleDeg = 180 OrElse angleDeg = 270 Then
            cons.AddFlushConstraint(pipeEndPlanep, flangeEndPlanep, 0)
        Else
            cons.AddMateConstraint(pipeEndPlanep, flangeEndPlanep, 0)
        End If

        '==================================================
        ' 3️⃣ PIPE (MID_PLANE) ↔ FLANGE (XY)
        ' ✅ RIGHT head → all MATE
        ' ✅ LEFT head / Shell → original Flush/Mate pattern
        '==================================================
        Dim pipeMidPlane As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("MID_PLANE")
        Dim flangeXY As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim pipeMidPlanep As Object
        Dim flangeXYp As Object
        pipeOcc.CreateGeometryProxy(pipeMidPlane, pipeMidPlanep)
        flangeOcc.CreateGeometryProxy(flangeXY, flangeXYp)

        Dim isRightHead As Boolean = nozzleLocation.ToUpper() = "RIGHT"

        If isRightHead Then
            '==================================================
            ' RIGHT HEAD — ALL MATE
            '==================================================
            cons.AddMateConstraint(pipeMidPlanep, flangeXYp, 0)
        Else
            '==================================================
            ' LEFT HEAD / SHELL — ORIGINAL
            '==================================================
            If angleDeg = 0 Then
                cons.AddFlushConstraint(pipeMidPlanep, flangeXYp, 0)
            ElseIf angleDeg = 90 Then
                cons.AddMateConstraint(pipeMidPlanep, flangeXYp, 0)
            ElseIf angleDeg = 180 Then
                cons.AddMateConstraint(pipeMidPlanep, flangeXYp, 0)
            ElseIf angleDeg = 270 Then
                cons.AddFlushConstraint(pipeMidPlanep, flangeXYp, 0)
            End If
        End If

        nozAsm.Update()
    End Sub

    Public Sub ConstrainPadToPipe_Shell_HOR(nozAsm As AssemblyDocument, pipeOcc As ComponentOccurrence, padOcc As ComponentOccurrence, isManway As Boolean)

        Dim cons As AssemblyConstraints = nozAsm.ComponentDefinition.Constraints

        '==================================================
        ' SAFETY
        '==================================================
        pipeOcc.Grounded = True
        padOcc.Grounded = False

        '==================================================
        ' 1️⃣ PIPE (XY) ↔ PAD (XY)
        ' FLUSH
        '==================================================
        Dim pipeXY As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim padXY As WorkPlane = padOcc.Definition.WorkPlanes.Item("XY Plane")

        Dim pipeXYp As Object
        Dim padXYp As Object

        pipeOcc.CreateGeometryProxy(pipeXY, pipeXYp)
        padOcc.CreateGeometryProxy(padXY, padXYp)
        cons.AddFlushConstraint(pipeXYp, padXYp, 0)

        '==================================================
        ' 2️⃣ PIPE (YZ) ↔ PAD (YZ)
        ' FLUSH
        '==================================================
        Dim pipeYZ As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim padYZ As WorkPlane = padOcc.Definition.WorkPlanes.Item("YZ Plane")

        Dim pipeYZp As Object
        Dim padYZp As Object

        pipeOcc.CreateGeometryProxy(pipeYZ, pipeYZp)
        padOcc.CreateGeometryProxy(padYZ, padYZp)
        cons.AddFlushConstraint(pipeYZp, padYZp, 0)

        '==================================================
        ' 3️⃣ PIPE (XZ) ↔ PAD (XZ)
        ' FLUSH
        '==================================================
        Dim pipeXZ As WorkPlane = pipeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim padXZ As WorkPlane = padOcc.Definition.WorkPlanes.Item("XZ Plane")

        Dim pipeXZp As Object
        Dim padXZp As Object

        pipeOcc.CreateGeometryProxy(pipeXZ, pipeXZp)
        padOcc.CreateGeometryProxy(padXZ, padXZp)
        cons.AddFlushConstraint(pipeXZp, padXZp, 0)
        nozAsm.Update()

    End Sub

    Public Sub ConstrainNozzleToShell_HOR(asmDoc As AssemblyDocument, shellOcc As ComponentOccurrence, nozzleOcc As ComponentOccurrence, xMm As Double, yMm As Double,
    angleDeg As Double, nozzleName As String, SelectedClient As String, isManway As Boolean)

        Dim cons As AssemblyConstraints = asmDoc.ComponentDefinition.Constraints

        '==================================================
        ' SAFETY
        '==================================================
        shellOcc.Grounded = False
        nozzleOcc.Grounded = False

        '==================================================
        ' SHELL PLANES
        '==================================================
        Dim shellXY As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim shellYZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim shellXZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XZ Plane")

        Dim shellXYp As Object
        Dim shellYZp As Object
        Dim shellXZp As Object

        shellOcc.CreateGeometryProxy(shellXY, shellXYp)
        shellOcc.CreateGeometryProxy(shellYZ, shellYZp)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZp)

        '==================================================
        ' NOZZLE ASSEMBLY PLANES
        '==================================================
        Dim nozXY As WorkPlane = nozzleOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim nozYZ As WorkPlane = nozzleOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim nozXZ As WorkPlane = nozzleOcc.Definition.WorkPlanes.Item("XZ Plane")

        Dim nozXYp As Object
        Dim nozYZp As Object
        Dim nozXZp As Object

        nozzleOcc.CreateGeometryProxy(nozXY, nozXYp)
        nozzleOcc.CreateGeometryProxy(nozYZ, nozYZp)
        nozzleOcc.CreateGeometryProxy(nozXZ, nozXZp)

        '==================================================
        ' 1️⃣ SHELL (XY)
        ' ↔ NOZZLE ASSEMBLY (XY)
        ' FLUSH
        '==================================================
        cons.AddFlushConstraint(shellXYp, nozXYp, 0)

        '==================================================
        ' 2️⃣ SHELL (YZ)
        ' ↔ NOZZLE ASSEMBLY (YZ)
        ' FLUSH
        '==================================================
        cons.AddFlushConstraint(shellYZp, nozYZp, 0)

        '==================================================
        ' 3️⃣ SHELL (XZ)
        ' ↔ NOZZLE ASSEMBLY (XZ)
        ' FLUSH
        '==================================================
        cons.AddFlushConstraint(shellXZp, nozXZp, 0)

        asmDoc.Update()

    End Sub

    Public Sub SetPipeDimensions_HOR(pipeOcc As ComponentOccurrence, pipeODmm As Double, pipeThkMm As Double, pipeLengthMm As Double, distMm As Double, offsetMm As Double)

        '==================================================
        ' VALIDATION
        '==================================================
        If pipeOcc Is Nothing Then
            Throw New Exception("❌ Pipe occurrence is Nothing")
        End If

        '==================================================
        ' GET PART DEFINITION
        '==================================================
        Dim partDef As PartComponentDefinition = CType(pipeOcc.Definition, PartComponentDefinition)
        Dim params As Parameters = partDef.Parameters

        '==================================================
        ' FETCH PARAMETERS
        '==================================================
        Dim pOD As Parameter = params.Item("PIPE_OD")
        Dim pTHK As Parameter = params.Item("PIPE_THK")
        Dim pLEN As Parameter = params.Item("PIPE_LENGTH")
        Dim pOFF As Parameter = params.Item("PIPE_OFFSET")
        Dim pDIST As Parameter = params.Item("DISTANCE")

        Try
            pOD.Value = pipeODmm / 10.0
            Debug.Print("PIPE_OD OK")
        Catch ex As Exception
            Debug.Print("PIPE_OD failed : " & ex.Message)
        End Try

        Try
            pTHK.Value = pipeThkMm / 10.0
            Debug.Print("PIPE_THK OK")
        Catch ex As Exception
            Debug.Print("PIPE_THK failed : " & ex.Message)
        End Try

        Try
            pLEN.Value = pipeLengthMm / 10.0
            Debug.Print("PIPE_LENGTH OK")
        Catch ex As Exception
            Debug.Print("PIPE_LENGTH failed : " & ex.Message)
        End Try

        Try
            pDIST.Value = distMm / 10.0
            Debug.Print("DISTANCE OK")
        Catch ex As Exception
            Debug.Print("DISTANCE failed : " & ex.Message)
        End Try

        Try
            pOFF.Value = offsetMm / 10.0
            Debug.Print("PIPE_OFFSET OK")
        Catch ex As Exception
            Debug.Print("PIPE_OFFSET failed : " & ex.Message)
        End Try

        Try
            partDef.Document.Update()
            Debug.Print("UPDATE OK")
        Catch ex As Exception
            Debug.Print("UPDATE failed : " & ex.Message)
        End Try

        '==================================================
        ' UPDATE DOCUMENT
        '==================================================
        partDef.Document.Update()

    End Sub

    Public Sub SetRFPadParameters_HOR(padOcc As ComponentOccurrence, rfPadODmm As Double, rfPadThkMm As Double, pipeODmm As Double, nozzleDistanceMm As Double, offsetDistMm As Double)

        '==================================================
        ' VALIDATION
        '==================================================
        If padOcc Is Nothing Then
            Throw New Exception("❌ RF Pad occurrence is Nothing")
        End If

        '==================================================
        ' GET PART DEFINITION
        '==================================================
        Dim partDef As PartComponentDefinition = CType(padOcc.Definition, PartComponentDefinition)
        Dim params As Parameters = partDef.Parameters

        '==================================================
        ' FETCH PARAMETERS
        '==================================================
        Dim pPadOD As Parameter = params.Item("PAD_OD")
        Dim pPadTHK As Parameter = params.Item("PAD_THK")
        Dim pPipeOD As Parameter = params.Item("PIPE_OD")
        Dim pDIST As Parameter = params.Item("DISTANCE")
        Dim pOFF As Parameter = params.Item("PIPE_OFFSET")

        Try
            pPadOD.Value = rfPadODmm / 10.0
            Debug.Print("PAD_OD OK")
        Catch ex As Exception
            Debug.Print("PAD_OD failed : " & ex.Message)
        End Try

        Try
            pPadTHK.Value = rfPadThkMm / 10.0
            Debug.Print("PAD_THK OK")
        Catch ex As Exception
            Debug.Print("PAD_THK failed : " & ex.Message)
        End Try

        Try
            pPipeOD.Value = pipeODmm / 10.0
            Debug.Print("PIPE_OD OK")
        Catch ex As Exception
            Debug.Print("PIPE_OD failed : " & ex.Message)
        End Try

        Try
            pDIST.Value = nozzleDistanceMm / 10.0
            Debug.Print("DISTANCE OK")
        Catch ex As Exception
            Debug.Print("DISTANCE failed : " & ex.Message)
        End Try

        Try
            pOFF.Value = offsetDistMm / 10.0
            Debug.Print("PIPE_OFFSET OK")
        Catch ex As Exception
            Debug.Print("PIPE_OFFSET failed : " & ex.Message)
        End Try

        '==================================================
        ' UPDATE DOCUMENT
        '==================================================
        Try
            partDef.Document.Update()
            Debug.Print("UPDATE OK")
        Catch ex As Exception
            Debug.Print("UPDATE failed : " & ex.Message)
        End Try

    End Sub

    '==================================================
    ' SET PIPE DIMENSIONS FOR HEAD NOZZLE
    '==================================================
    Public Sub SetPipeDimensions_Head_HOR(pipeOcc As ComponentOccurrence,
    pipeODmm As Double, pipeThkMm As Double, pipeLengthMm As Double,
    xDistMm As Double, yDistMm As Double)

        If pipeOcc Is Nothing Then
            Throw New Exception("❌ Pipe occurrence is Nothing")
        End If

        Dim partDef As PartComponentDefinition = CType(pipeOcc.Definition, PartComponentDefinition)
        Dim params As Parameters = partDef.Parameters

        Try
            params.Item("PIPE_OD").Value = pipeODmm / 10.0
            Debug.Print("PIPE_OD OK")
        Catch ex As Exception
            Debug.Print($"PIPE_OD failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_THK").Value = pipeThkMm / 10.0
            Debug.Print("PIPE_THK OK")
        Catch ex As Exception
            Debug.Print($"PIPE_THK failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_X_OFFSET").Value = Math.Abs(xDistMm) / 10.0
            Debug.Print("PIPE_X_OFFSET OK")
        Catch ex As Exception
            Debug.Print($"PIPE_X_OFFSET failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_Y_OFFSET").Value = Math.Abs(yDistMm) / 10.0
            Debug.Print("PIPE_Y_OFFSET OK")
        Catch ex As Exception
            Debug.Print($"PIPE_Y_OFFSET failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_LENGTH").Value = pipeLengthMm / 10.0
            Debug.Print("PIPE_LENGTH OK")
        Catch ex As Exception
            Debug.Print($"PIPE_LENGTH failed: {ex.Message}")
        End Try

        Try
            partDef.Document.Update()
            Debug.Print("✅ Pipe updated")
        Catch ex As Exception
            Debug.Print($"UPDATE failed: {ex.Message}")
        End Try

    End Sub

    '==================================================
    ' SET RF PAD DIMENSIONS FOR HEAD NOZZLE
    '==================================================
    Public Sub SetRFPadParameters_Head_HOR(padOcc As ComponentOccurrence,
    pipeODmm As Double, pipeThkMm As Double, pipeLengthMm As Double,
    xDistMm As Double, yDistMm As Double,
    rfPadODmm As Double, rfPadThkMm As Double)

        If padOcc Is Nothing Then
            Throw New Exception("❌ RF Pad occurrence is Nothing")
        End If

        Dim partDef As PartComponentDefinition = CType(padOcc.Definition, PartComponentDefinition)
        Dim params As Parameters = partDef.Parameters

        Try
            params.Item("PIPE_OD").Value = pipeODmm / 10.0
            Debug.Print("PAD PIPE_OD OK")
        Catch ex As Exception
            Debug.Print($"PAD PIPE_OD failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_THK").Value = pipeThkMm / 10.0
            Debug.Print("PAD PIPE_THK OK")
        Catch ex As Exception
            Debug.Print($"PAD PIPE_THK failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_X_OFFSET").Value = Math.Abs(xDistMm) / 10.0
            Debug.Print("PAD PIPE_X_OFFSET OK")
        Catch ex As Exception
            Debug.Print($"PAD PIPE_X_OFFSET failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_Y_OFFSET").Value = Math.Abs(yDistMm) / 10.0
            Debug.Print("PAD PIPE_Y_OFFSET OK")
        Catch ex As Exception
            Debug.Print($"PAD PIPE_Y_OFFSET failed: {ex.Message}")
        End Try

        Try
            params.Item("PIPE_LENGTH").Value = pipeLengthMm / 10.0
            Debug.Print("PAD PIPE_LENGTH OK")
        Catch ex As Exception
            Debug.Print($"PAD PIPE_LENGTH failed: {ex.Message}")
        End Try

        Try
            params.Item("PAD_OD").Value = rfPadODmm / 10.0
            Debug.Print("PAD_OD OK")
        Catch ex As Exception
            Debug.Print($"PAD_OD failed: {ex.Message}")
        End Try

        Try
            params.Item("PAD_THK").Value = rfPadThkMm / 10.0
            Debug.Print("PAD_THK OK")
        Catch ex As Exception
            Debug.Print($"PAD_THK failed: {ex.Message}")
        End Try

        Try
            partDef.Document.Update()
            Debug.Print("✅ RF Pad updated")
        Catch ex As Exception
            Debug.Print($"RF Pad UPDATE failed: {ex.Message}")
        End Try

    End Sub

    Public Sub PlaceAndConstrainManwayGasket(asmDoc As AssemblyDocument, nozzleM1Occ As ComponentOccurrence, gasketOcc As ComponentOccurrence, flangeName As String)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        ' --- Find FLANGE sub-occurrence inside NOZZLE_M1 dynamically ---
        Dim flangeOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In nozzleM1Occ.SubOccurrences
            If InStr(UCase(subOcc.Name), UCase(flangeName)) > 0 Then
                flangeOcc = subOcc
                Exit For
            End If
        Next

        If flangeOcc Is Nothing Then
            MsgBox($"'{flangeName}' component not found inside '{nozzleM1Occ.Name}'.")
            Exit Sub
        End If

        ' --- Flange Proxies ---
        Dim flangeYZ = flangeOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim flangeXZ = flangeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim flangeXY = flangeOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim flangeYZp, flangeXZp, flangeXYp As Object
        flangeOcc.CreateGeometryProxy(flangeYZ, flangeYZp)
        flangeOcc.CreateGeometryProxy(flangeXZ, flangeXZp)
        flangeOcc.CreateGeometryProxy(flangeXY, flangeXYp)

        ' --- Gasket Proxies ---
        Dim gasketXY = gasketOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim gasketYZ = gasketOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim gasketXZ = gasketOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim gasketXYp, gasketYZp, gasketXZp As Object
        gasketOcc.CreateGeometryProxy(gasketXY, gasketXYp)
        gasketOcc.CreateGeometryProxy(gasketYZ, gasketYZp)
        gasketOcc.CreateGeometryProxy(gasketXZ, gasketXZp)

        ' 1st: Flange (YZ)  →  Gasket (XZ)  - MATE
        cons.AddMateConstraint(flangeYZp, gasketXZp, 0)

        ' 2nd: Flange (XZ)  →  Gasket (YZ)  - MATE
        cons.AddMateConstraint(flangeXZp, gasketYZp, 0)

        ' 3rd: Flange (XY)  →  Gasket (XY)  - MATE
        cons.AddMateConstraint(flangeXYp, gasketXYp, 0)

    End Sub

    Public Sub PlaceAndConstrainManwayCover(asmDoc As AssemblyDocument, gasketOcc As ComponentOccurrence, coverOcc As ComponentOccurrence)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '-------------------------------------------
        ' 1️⃣ GET KEY PLANES
        '-------------------------------------------
        ' MANWAY_GASKET planes
        Dim gasketYZ As WorkPlane = gasketOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim gasketXY As WorkPlane = gasketOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim gasketWP1 As WorkPlane = gasketOcc.Definition.WorkPlanes.Item("Work Plane1")

        ' MANWAY_BLIND_FLANGE planes
        Dim coverXY As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim coverXZ As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim coverYZ As WorkPlane = coverOcc.Definition.WorkPlanes.Item("YZ Plane")

        '-------------------------------------------
        ' 2️⃣ CREATE PROXIES
        '-------------------------------------------
        ' MANWAY_GASKET proxies
        Dim gYZp, gXYp, gWP1p As Object
        gasketOcc.CreateGeometryProxy(gasketYZ, gYZp)
        gasketOcc.CreateGeometryProxy(gasketXY, gXYp)
        gasketOcc.CreateGeometryProxy(gasketWP1, gWP1p)

        ' MANWAY_BLIND_FLANGE proxies
        Dim cXYp, cXZp, cYZp As Object
        coverOcc.CreateGeometryProxy(coverXY, cXYp)
        coverOcc.CreateGeometryProxy(coverXZ, cXZp)
        coverOcc.CreateGeometryProxy(coverYZ, cYZp)

        '-------------------------------------------
        ' 3️⃣ APPLY CONSTRAINTS
        '-------------------------------------------
        ' 1️⃣ MANWAY_GASKET (YZ)         → MANWAY_BLIND_FLANGE (XY)  - MATE
        cons.AddMateConstraint(gYZp, cXYp, 0)

        ' 2️⃣ MANWAY_GASKET (XY)         → MANWAY_BLIND_FLANGE (XZ)  - MATE
        cons.AddMateConstraint(gXYp, cXZp, 0)

        ' 3️⃣ MANWAY_GASKET (Work Plane1) → MANWAY_BLIND_FLANGE (YZ)  - FLUSH
        cons.AddFlushConstraint(gWP1p, cYZp, 0)

    End Sub

    Public Sub PlaceAndConstrainManwayDavit(asmDoc As AssemblyDocument, nozzleM1Occ As ComponentOccurrence, davitOcc As ComponentOccurrence, blindFlangeOcc As ComponentOccurrence, flangeName As String)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' MANWAY_DAVIT_ARM_ASSEMBLY PLANES
        '=========================
        Dim davXZ As WorkPlane = davitOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim davYZ As WorkPlane = davitOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim davXY As WorkPlane = davitOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim davXZp, davYZp, davXYp As Object
        davitOcc.CreateGeometryProxy(davXZ, davXZp)
        davitOcc.CreateGeometryProxy(davYZ, davYZp)
        davitOcc.CreateGeometryProxy(davXY, davXYp)

        '=========================
        ' MANWAY_BLIND_FLANGE PLANES
        '=========================
        Dim bfXZ As WorkPlane = blindFlangeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bfXY As WorkPlane = blindFlangeOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim bfXZp, bfXYp As Object
        blindFlangeOcc.CreateGeometryProxy(bfXZ, bfXZp)
        blindFlangeOcc.CreateGeometryProxy(bfXY, bfXYp)

        '=========================
        ' M1_FLANGE SUB-OCCURRENCE (dynamic name)
        '=========================
        Dim flangeOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In nozzleM1Occ.SubOccurrences
            If InStr(UCase(subOcc.Name), UCase(flangeName)) > 0 Then
                flangeOcc = subOcc
                Exit For
            End If
        Next

        If flangeOcc Is Nothing Then
            MsgBox($"'{flangeName}' not found inside '{nozzleM1Occ.Name}'.")
            Exit Sub
        End If

        Dim flangeWP4 As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("Work Plane4")
        Dim flangeWP4p As Object
        flangeOcc.CreateGeometryProxy(flangeWP4, flangeWP4p)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ MANWAY_BLIND_FLANGE (XZ) → MANWAY_DAVIT_ARM_ASSEMBLY (XZ) - FLUSH
        cons.AddFlushConstraint(bfXZp, davXZp, 0)

        ' 2️⃣ MANWAY_BLIND_FLANGE (XY) → MANWAY_DAVIT_ARM_ASSEMBLY (YZ) - MATE
        cons.AddMateConstraint(bfXYp, davYZp, 0)

        ' 3️⃣ M1_FLANGE (Work Plane4)  → MANWAY_DAVIT_ARM_ASSEMBLY (XY) - FLUSH
        cons.AddFlushConstraint(flangeWP4p, davXYp, 0)

    End Sub

    Public Sub PlaceAndConstrainManwayDavit_ADNOC(asmDoc As AssemblyDocument, nozzleOcc As ComponentOccurrence, davitOcc As ComponentOccurrence, coverOcc As ComponentOccurrence, gasketOcc As ComponentOccurrence)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' FIND 36_BRACKET_FOR_MD SUB-OCCURRENCE INSIDE DAVIT
        '=========================
        Dim bracketOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In davitOcc.SubOccurrences
            If InStr(UCase(subOcc.Name), "36_BRACKET_FOR_MD") > 0 Then
                bracketOcc = subOcc
                Exit For
            End If
        Next

        If bracketOcc Is Nothing Then
            MsgBox("'36_BRACKET_FOR_MD' not found inside MANWAY_DAVIT_ARM_ASSEMBLY.")
            Exit Sub
        End If

        '=========================
        ' MANWAY_BLIND_FLANGE PLANES & PROXIES
        '=========================
        Dim bfXZ As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bfXY As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim bfXZp As Object = Nothing
        Dim bfXYp As Object = Nothing
        coverOcc.CreateGeometryProxy(bfXZ, bfXZp)
        coverOcc.CreateGeometryProxy(bfXY, bfXYp)

        '=========================
        ' MANWAY_GASKET PLANES & PROXIES
        '=========================
        Dim gasketXZ As WorkPlane = gasketOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim gasketXZp As Object = Nothing
        gasketOcc.CreateGeometryProxy(gasketXZ, gasketXZp)

        '=========================
        ' 36_BRACKET_FOR_MD PLANES & PROXIES
        '=========================
        Dim bracketXZ As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bracketYZ As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim bracketXY As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim bracketXZp As Object = Nothing
        Dim bracketYZp As Object = Nothing
        Dim bracketXYp As Object = Nothing
        bracketOcc.CreateGeometryProxy(bracketXZ, bracketXZp)
        bracketOcc.CreateGeometryProxy(bracketYZ, bracketYZp)
        bracketOcc.CreateGeometryProxy(bracketXY, bracketXYp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ MANWAY_BLIND_FLANGE (XZ) → 36_BRACKET_FOR_MD (XZ) - MATE
        cons.AddMateConstraint(bfXZp, bracketXZp, 0)

        ' 2️⃣ MANWAY_BLIND_FLANGE (XY) → 36_BRACKET_FOR_MD (YZ) - FLUSH
        cons.AddFlushConstraint(bfXYp, bracketYZp, 0)

        ' 3️⃣ MANWAY_GASKET (XZ) → 36_BRACKET_FOR_MD (XY) - FLUSH (-246 mm → cm)
        cons.AddFlushConstraint(gasketXZp, bracketXYp, -265 / 10.0)

    End Sub

    Public Sub PlaceAndConstrainManwayDavit_ADNOC_M2(asmDoc As AssemblyDocument, davitOcc As ComponentOccurrence, coverOcc As ComponentOccurrence)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' FIND BRACKET_1_FOR_MANWAY SUB-OCCURRENCE INSIDE DAVIT
        '=========================
        Dim bracketOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In davitOcc.SubOccurrences
            If InStr(UCase(subOcc.Name), "BRACKET_1_FOR_MANWAY") > 0 Then
                bracketOcc = subOcc
                Exit For
            End If
        Next

        If bracketOcc Is Nothing Then
            MsgBox("'BRACKET_1_FOR_MANWAY' not found inside MANWAY_DAVIT_ARM_TOP.")
            Exit Sub
        End If

        '=========================
        ' FIND HINGE_PLATE_1 SUB-OCCURRENCE INSIDE DAVIT
        '=========================
        Dim hingePlateOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In davitOcc.SubOccurrences
            If InStr(UCase(subOcc.Name), "HINGE_PLATE_1") > 0 Then
                hingePlateOcc = subOcc
                Exit For
            End If
        Next

        If hingePlateOcc Is Nothing Then
            MsgBox("'HINGE_PLATE_1' not found inside MANWAY_DAVIT_ARM_TOP.")
            Exit Sub
        End If

        '=========================
        ' MANWAY_BLIND_FLANGE PLANES
        '=========================
        Dim bfXZ As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bfXY As WorkPlane = coverOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim bfWP2 As WorkPlane = coverOcc.Definition.WorkPlanes.Item("Work Plane2")

        '=========================
        ' MANWAY_BLIND_FLANGE PROXIES
        '=========================
        Dim bfXZp As Object = Nothing
        Dim bfXYp As Object = Nothing
        Dim bfWP2p As Object = Nothing
        coverOcc.CreateGeometryProxy(bfXZ, bfXZp)
        coverOcc.CreateGeometryProxy(bfXY, bfXYp)
        coverOcc.CreateGeometryProxy(bfWP2, bfWP2p)

        '=========================
        ' BRACKET_1_FOR_MANWAY PLANES
        '=========================
        Dim bracketXY As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim bracketXZ As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bracketYZ As WorkPlane = bracketOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' BRACKET_1_FOR_MANWAY PROXIES
        '=========================
        Dim bracketXYp As Object = Nothing
        Dim bracketXZp As Object = Nothing
        Dim bracketYZp As Object = Nothing
        bracketOcc.CreateGeometryProxy(bracketXY, bracketXYp)
        bracketOcc.CreateGeometryProxy(bracketXZ, bracketXZp)
        bracketOcc.CreateGeometryProxy(bracketYZ, bracketYZp)

        '=========================
        ' HINGE_PLATE_1 PLANES
        '=========================
        Dim hingeWP1 As WorkPlane = hingePlateOcc.Definition.WorkPlanes.Item("Work Plane1")

        '=========================
        ' HINGE_PLATE_1 PROXIES
        '=========================
        Dim hingeWP1p As Object = Nothing
        hingePlateOcc.CreateGeometryProxy(hingeWP1, hingeWP1p)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ MANWAY_BLIND_FLANGE (XZ)         → BRACKET_1_FOR_MANWAY (XY)       - FLUSH
        cons.AddFlushConstraint(bfXZp, bracketXYp, 0)

        ' 2️⃣ MANWAY_BLIND_FLANGE (XY)         → BRACKET_1_FOR_MANWAY (YZ)       - FLUSH
        cons.AddFlushConstraint(bfXYp, bracketYZp, 0)

        ' 3️⃣ MANWAY_BLIND_FLANGE (Work Plane2) → HINGE_PLATE_1 (Work Plane1)    - MATE
        cons.AddMateConstraint(bfWP2p, hingeWP1p, 0)

    End Sub

    Public Sub PlaceAndConstrainJS_MW_FLANGE(asmDoc As AssemblyDocument, nozzleOcc As ComponentOccurrence, jsOcc As ComponentOccurrence, invApp As Inventor.Application, flangeName As String)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' FIND FLANGE SUB-OCCURRENCE (dynamic)
        '=========================
        Dim flangeOcc As ComponentOccurrence = Nothing
        For Each subOcc As ComponentOccurrence In nozzleOcc.SubOccurrences
            If InStr(UCase(subOcc.Name), UCase(flangeName)) > 0 Then
                flangeOcc = subOcc
                Exit For
            End If
        Next

        If flangeOcc Is Nothing Then
            MsgBox($"'{flangeName}' not found inside '{nozzleOcc.Name}'.")
            Exit Sub
        End If

        '=========================
        ' FLANGE PLANES & PROXIES
        '=========================
        Dim flangeWP4 As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("Work Plane4")
        Dim flangeXZ As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim flangeXY As WorkPlane = flangeOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim flangeWP4p, flangeXZp, flangeXYp As Object
        flangeOcc.CreateGeometryProxy(flangeWP4, flangeWP4p)
        flangeOcc.CreateGeometryProxy(flangeXZ, flangeXZp)
        flangeOcc.CreateGeometryProxy(flangeXY, flangeXYp)

        '=========================
        ' JACK_SCREW_LUG_MW_FLANGE PLANES & PROXIES
        '=========================
        Dim jsXY As WorkPlane = jsOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim jsYZ As WorkPlane = jsOcc.Definition.WorkPlanes.Item("YZ Plane")
        Dim jsXZ As WorkPlane = jsOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim jsXYp, jsYZp, jsXZp As Object
        jsOcc.CreateGeometryProxy(jsXY, jsXYp)
        jsOcc.CreateGeometryProxy(jsYZ, jsYZp)
        jsOcc.CreateGeometryProxy(jsXZ, jsXZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ FLANGE (Work Plane4) → JS_MW_FLANGE (XY) - MATE
        cons.AddMateConstraint(flangeWP4p, jsXYp, 0)

        ' 2️⃣ FLANGE (XZ)          → JS_MW_FLANGE (YZ) - MATE
        cons.AddMateConstraint(flangeXZp, jsYZp, 0)

        ' 3️⃣ FLANGE (XY)          → JS_MW_FLANGE (XZ) - MATE
        cons.AddMateConstraint(flangeXYp, jsXZp, 0)

    End Sub

    Public Sub CreateCircularPattern_JS_MW_FLANGE(asmDoc As AssemblyDocument, jsOcc As ComponentOccurrence, invApp As Inventor.Application)

        Dim asmCompDef As AssemblyComponentDefinition = asmDoc.ComponentDefinition

        '=========================
        ' GET Z-AXIS FROM jsOcc PART ITSELF
        '=========================
        Dim zAxis As WorkAxis = jsOcc.Definition.WorkAxes.Item("Z Axis")
        Dim zAxisProxy As Object = Nothing
        jsOcc.CreateGeometryProxy(zAxis, zAxisProxy)

        '=========================
        ' BUILD OCCURRENCE COLLECTION
        '=========================
        Dim occCollection As ObjectCollection = invApp.TransientObjects.CreateObjectCollection()
        occCollection.Add(jsOcc)

        '=========================
        ' CREATE CIRCULAR PATTERN
        '=========================
        Dim pattern As OccurrencePattern = asmCompDef.OccurrencePatterns.AddCircularPattern(
    occCollection,                                      ' Occurrences to pattern
    zAxisProxy,                                         ' Z-Axis from part itself (proxy)
    True,                                               ' Natural axis direction
    (2 * Math.PI) / 3,                                  ' 120° in radians (360/3)
    3                                                   ' Quantity = 3
)

    End Sub

    Public Sub PlaceAndConstrainJS_MW_BLIND_FLANGE(asmDoc As AssemblyDocument, blindFlangeOcc As ComponentOccurrence, jsOcc As ComponentOccurrence, invApp As Inventor.Application)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' MANWAY_BLIND_FLANGE PLANES
        ' ✅ Direct from blindFlangeOcc (top-level occurrence)
        '=========================
        Dim bfWP1 As WorkPlane = blindFlangeOcc.Definition.WorkPlanes.Item("Work Plane1")
        Dim bfXZ As WorkPlane = blindFlangeOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim bfXY As WorkPlane = blindFlangeOcc.Definition.WorkPlanes.Item("XY Plane")

        '=========================
        ' MANWAY_BLIND_FLANGE PROXIES
        '=========================
        Dim bfWP1p As Object = Nothing
        Dim bfXZp As Object = Nothing
        Dim bfXYp As Object = Nothing
        blindFlangeOcc.CreateGeometryProxy(bfWP1, bfWP1p)
        blindFlangeOcc.CreateGeometryProxy(bfXZ, bfXZp)
        blindFlangeOcc.CreateGeometryProxy(bfXY, bfXYp)

        '=========================
        ' JACK_SCREW_LUG_MW_BLIND_FLANGE PLANES
        '=========================
        Dim jsXY As WorkPlane = jsOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim jsXZ As WorkPlane = jsOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim jsYZ As WorkPlane = jsOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' JACK_SCREW_LUG_MW_BLIND_FLANGE PROXIES
        '=========================
        Dim jsXYp As Object = Nothing
        Dim jsXZp As Object = Nothing
        Dim jsYZp As Object = Nothing
        jsOcc.CreateGeometryProxy(jsXY, jsXYp)
        jsOcc.CreateGeometryProxy(jsXZ, jsXZp)
        jsOcc.CreateGeometryProxy(jsYZ, jsYZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ MANWAY_BLIND_FLANGE (Work Plane1) → JS_MW_BLIND_FLANGE (XY) - MATE
        cons.AddMateConstraint(bfWP1p, jsXYp, 0)

        ' 2️⃣ MANWAY_BLIND_FLANGE (XZ)          → JS_MW_BLIND_FLANGE (XZ) - MATE
        cons.AddMateConstraint(bfXZp, jsXZp, 0)

        ' 3️⃣ MANWAY_BLIND_FLANGE (XY)          → JS_MW_BLIND_FLANGE (YZ) - FLUSH
        cons.AddFlushConstraint(bfXYp, jsYZp, 0)

    End Sub

    Public Sub PlaceAndConstrainSaddleLeft(asmDoc As AssemblyDocument, shellOcc As ComponentOccurrence, saddleOcc As ComponentOccurrence)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' SHELL PLANES
        '=========================
        Dim shellXY As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim shellXZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim shellYZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' SHELL PROXIES
        '=========================
        Dim shellXYp As Object = Nothing
        Dim shellXZp As Object = Nothing
        Dim shellYZp As Object = Nothing
        shellOcc.CreateGeometryProxy(shellXY, shellXYp)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZp)
        shellOcc.CreateGeometryProxy(shellYZ, shellYZp)

        '=========================
        ' SADDLE PLANES
        '=========================
        Dim saddleXY As WorkPlane = saddleOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim saddleXZ As WorkPlane = saddleOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim saddleYZ As WorkPlane = saddleOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' SADDLE PROXIES
        '=========================
        Dim saddleXYp As Object = Nothing
        Dim saddleXZp As Object = Nothing
        Dim saddleYZp As Object = Nothing
        saddleOcc.CreateGeometryProxy(saddleXY, saddleXYp)
        saddleOcc.CreateGeometryProxy(saddleXZ, saddleXZp)
        saddleOcc.CreateGeometryProxy(saddleYZ, saddleYZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ SHELL (XY) → SADDLE (XY) - FLUSH
        cons.AddFlushConstraint(shellXYp, saddleXYp, 0)

        ' 2️⃣ SHELL (XZ) → SADDLE (XZ) - FLUSH
        cons.AddFlushConstraint(shellXZp, saddleXZp, 0)

        ' 3️⃣ SHELL (YZ) → SADDLE (YZ) - FLUSH
        cons.AddFlushConstraint(shellYZp, saddleYZp, 0)

    End Sub

    Public Sub PlaceAndConstrainLiftingLug(asmDoc As AssemblyDocument, shellOcc As ComponentOccurrence, lugOcc As ComponentOccurrence, lugNumber As Integer)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' SHELL PLANES
        '=========================
        Dim shellXY As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim shellXZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim shellYZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' SHELL PROXIES
        '=========================
        Dim shellXYp As Object = Nothing
        Dim shellXZp As Object = Nothing
        Dim shellYZp As Object = Nothing
        shellOcc.CreateGeometryProxy(shellXY, shellXYp)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZp)
        shellOcc.CreateGeometryProxy(shellYZ, shellYZp)

        '=========================
        ' LIFTING LUG PLANES
        '=========================
        Dim lugXY As WorkPlane = lugOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim lugXZ As WorkPlane = lugOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim lugYZ As WorkPlane = lugOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' LIFTING LUG PROXIES
        '=========================
        Dim lugXYp As Object = Nothing
        Dim lugXZp As Object = Nothing
        Dim lugYZp As Object = Nothing
        lugOcc.CreateGeometryProxy(lugXY, lugXYp)
        lugOcc.CreateGeometryProxy(lugXZ, lugXZp)
        lugOcc.CreateGeometryProxy(lugYZ, lugYZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        ' 🔧 Adjust per lug number if constraints differ
        '=========================
        Select Case lugNumber

            Case 1
                ' 1️⃣ SHELL (XY) → LUG (XY) - FLUSH
                cons.AddFlushConstraint(shellXYp, lugXYp, 0)
                ' 2️⃣ SHELL (XZ) → LUG (XZ) - FLUSH
                cons.AddFlushConstraint(shellXZp, lugXZp, 0)
                ' 3️⃣ SHELL (YZ) → LUG (YZ) - FLUSH
                cons.AddFlushConstraint(shellYZp, lugYZp, 0)

            Case 2
                cons.AddFlushConstraint(shellXYp, lugXYp, 0)
                cons.AddFlushConstraint(shellXZp, lugXZp, 0)
                cons.AddFlushConstraint(shellYZp, lugYZp, 0)

            Case 3
                cons.AddFlushConstraint(shellXYp, lugXYp, 0)
                cons.AddFlushConstraint(shellXZp, lugXZp, 0)
                cons.AddFlushConstraint(shellYZp, lugYZp, 0)

            Case 4
                cons.AddFlushConstraint(shellXYp, lugXYp, 0)
                cons.AddFlushConstraint(shellXZp, lugXZp, 0)
                cons.AddFlushConstraint(shellYZp, lugYZp, 0)

        End Select

        Debug.Print($"LIFTING_LUG_ASMBY_{lugNumber} placed and constrained.")

    End Sub

    Public Sub PlaceAndConstrainPFC(asmDoc As AssemblyDocument, shellOcc As ComponentOccurrence, pfcOcc As ComponentOccurrence, pfcNumber As Integer)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' SHELL PLANES
        '=========================
        Dim shellXY As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim shellXZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim shellYZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' SHELL PROXIES
        '=========================
        Dim shellXYp As Object = Nothing
        Dim shellXZp As Object = Nothing
        Dim shellYZp As Object = Nothing
        shellOcc.CreateGeometryProxy(shellXY, shellXYp)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZp)
        shellOcc.CreateGeometryProxy(shellYZ, shellYZp)

        '=========================
        ' PFC PLANES
        '=========================
        Dim pfcXY As WorkPlane = pfcOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim pfcXZ As WorkPlane = pfcOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim pfcYZ As WorkPlane = pfcOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' PFC PROXIES
        '=========================
        Dim pfcXYp As Object = Nothing
        Dim pfcXZp As Object = Nothing
        Dim pfcYZp As Object = Nothing
        pfcOcc.CreateGeometryProxy(pfcXY, pfcXYp)
        pfcOcc.CreateGeometryProxy(pfcXZ, pfcXZp)
        pfcOcc.CreateGeometryProxy(pfcYZ, pfcYZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ SHELL (XY) → PFC (XY) - FLUSH
        cons.AddFlushConstraint(shellXYp, pfcXYp, 0)

        ' 2️⃣ SHELL (XZ) → PFC (XZ) - FLUSH
        cons.AddFlushConstraint(shellXZp, pfcXZp, 0)

        ' 3️⃣ SHELL (YZ) → PFC (YZ) - FLUSH
        cons.AddFlushConstraint(shellYZp, pfcYZp, 0)

        Debug.Print($"PFC_ASMBY_{pfcNumber} placed and constrained.")

    End Sub

    Public Sub PlaceAndConstrainShellComponent(asmDoc As AssemblyDocument, shellOcc As ComponentOccurrence, compOcc As ComponentOccurrence, compName As String)
        Dim cons = asmDoc.ComponentDefinition.Constraints

        '=========================
        ' SHELL PLANES
        '=========================
        Dim shellXY As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim shellXZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim shellYZ As WorkPlane = shellOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' SHELL PROXIES
        '=========================
        Dim shellXYp As Object = Nothing
        Dim shellXZp As Object = Nothing
        Dim shellYZp As Object = Nothing
        shellOcc.CreateGeometryProxy(shellXY, shellXYp)
        shellOcc.CreateGeometryProxy(shellXZ, shellXZp)
        shellOcc.CreateGeometryProxy(shellYZ, shellYZp)

        '=========================
        ' COMPONENT PLANES
        '=========================
        Dim compXY As WorkPlane = compOcc.Definition.WorkPlanes.Item("XY Plane")
        Dim compXZ As WorkPlane = compOcc.Definition.WorkPlanes.Item("XZ Plane")
        Dim compYZ As WorkPlane = compOcc.Definition.WorkPlanes.Item("YZ Plane")

        '=========================
        ' COMPONENT PROXIES
        '=========================
        Dim compXYp As Object = Nothing
        Dim compXZp As Object = Nothing
        Dim compYZp As Object = Nothing
        compOcc.CreateGeometryProxy(compXY, compXYp)
        compOcc.CreateGeometryProxy(compXZ, compXZp)
        compOcc.CreateGeometryProxy(compYZ, compYZp)

        '=========================
        ' 🧲 APPLY CONSTRAINTS
        '=========================
        ' 1️⃣ SHELL (XY) → COMPONENT (XY) - FLUSH
        cons.AddFlushConstraint(shellXYp, compXYp, 0)

        ' 2️⃣ SHELL (XZ) → COMPONENT (XZ) - FLUSH
        cons.AddFlushConstraint(shellXZp, compXZp, 0)

        ' 3️⃣ SHELL (YZ) → COMPONENT (YZ) - FLUSH
        cons.AddFlushConstraint(shellYZp, compYZp, 0)

        Debug.Print($"{compName} placed and constrained.")

    End Sub

    'Public Function PlaceAndConstrainStiffenerRing(invApp As Inventor.Application, asmDoc As AssemblyDocument, folderPath As String) As ComponentOccurrence

    '    '==================================================
    '    ' STIFFENER RING PATH
    '    '==================================================
    '    Dim stiffRingPath As String = IO.Path.Combine(folderPath, "ARAMCO\STIFFENER_RING.ipt")

    '    If Not IO.File.Exists(stiffRingPath) Then
    '        Throw New Exception("❌ STIFFENER_RING.ipt not found : " & stiffRingPath)
    '    End If

    '    '==================================================
    '    ' ASSEMBLY DEFINITION
    '    '==================================================
    '    Dim asmDef As AssemblyComponentDefinition = asmDoc.ComponentDefinition
    '    Dim cons As AssemblyConstraints = asmDef.Constraints
    '    Dim tg As TransientGeometry = invApp.TransientGeometry

    '    '==================================================
    '    ' PLACE COMPONENT
    '    '==================================================
    '    Dim stiffRingOcc As ComponentOccurrence = asmDef.Occurrences.Add(stiffRingPath, tg.CreateMatrix())
    '    stiffRingOcc.Grounded = False
    '    stiffRingOcc.Name = "STIFFENER_RING"

    '    '==================================================
    '    ' MAIN ASSEMBLY ORIGIN PLANES
    '    '==================================================
    '    Dim asmXY As WorkPlane = asmDef.WorkPlanes.Item("XY Plane")
    '    Dim asmYZ As WorkPlane = asmDef.WorkPlanes.Item("YZ Plane")
    '    Dim asmXZ As WorkPlane = asmDef.WorkPlanes.Item("XZ Plane")

    '    '==================================================
    '    ' STIFFENER RING PLANES
    '    '==================================================
    '    Dim ringDef As PartComponentDefinition = CType(stiffRingOcc.Definition, PartComponentDefinition)

    '    Dim ringXY As WorkPlane = ringDef.WorkPlanes.Item("XY Plane")
    '    Dim ringYZ As WorkPlane = ringDef.WorkPlanes.Item("YZ Plane")
    '    Dim ringXZ As WorkPlane = ringDef.WorkPlanes.Item("XZ Plane")

    '    '==================================================
    '    ' CREATE PROXIES
    '    '==================================================
    '    Dim asmXYp, asmYZp, asmXZp As Object
    '    Dim ringXYp, ringYZp, ringXZp As Object

    '    stiffRingOcc.CreateGeometryProxy(ringXY, ringXYp)
    '    stiffRingOcc.CreateGeometryProxy(ringYZ, ringYZp)
    '    stiffRingOcc.CreateGeometryProxy(ringXZ, ringXZp)

    '    asmDef.CreateGeometryProxy(asmXY, asmXYp)
    '    asmDef.CreateGeometryProxy(asmYZ, asmYZp)
    '    asmDef.CreateGeometryProxy(asmXZ, asmXZp)

    '    '==================================================
    '    ' CONSTRAINTS
    '    '==================================================

    '    ' XY ↔ XY
    '    cons.AddFlushConstraint(asmXYp, ringXYp, 0)

    '    ' YZ ↔ YZ
    '    cons.AddFlushConstraint(asmYZp, ringYZp, 0)

    '    ' XZ ↔ XZ
    '    cons.AddFlushConstraint(asmXZp, ringXZp, 0)

    '    asmDoc.Update()

    '    Return stiffRingOcc

    'End Function

    Private Sub ConstrainByOriginPlanes(asmDoc As AssemblyDocument, occ1 As ComponentOccurrence, occ2 As ComponentOccurrence)

        Dim cons As AssemblyConstraints = asmDoc.ComponentDefinition.Constraints

        '---------------------------------------
        ' Get planes
        '---------------------------------------
        Dim occ1XY As WorkPlane = occ1.Definition.WorkPlanes.Item("XY Plane")
        Dim occ1YZ As WorkPlane = occ1.Definition.WorkPlanes.Item("YZ Plane")
        Dim occ1XZ As WorkPlane = occ1.Definition.WorkPlanes.Item("XZ Plane")

        Dim occ2XY As WorkPlane = occ2.Definition.WorkPlanes.Item("XY Plane")
        Dim occ2YZ As WorkPlane = occ2.Definition.WorkPlanes.Item("YZ Plane")
        Dim occ2XZ As WorkPlane = occ2.Definition.WorkPlanes.Item("XZ Plane")

        '---------------------------------------
        ' Create proxies
        '---------------------------------------
        Dim occ1XYp, occ1YZp, occ1XZp As Object
        Dim occ2XYp, occ2YZp, occ2XZp As Object

        occ1.CreateGeometryProxy(occ1XY, occ1XYp)
        occ1.CreateGeometryProxy(occ1YZ, occ1YZp)
        occ1.CreateGeometryProxy(occ1XZ, occ1XZp)

        occ2.CreateGeometryProxy(occ2XY, occ2XYp)
        occ2.CreateGeometryProxy(occ2YZ, occ2YZp)
        occ2.CreateGeometryProxy(occ2XZ, occ2XZp)

        '---------------------------------------
        ' Apply Flush Constraints
        '---------------------------------------
        cons.AddFlushConstraint(occ1XYp, occ2XYp, 0)
        cons.AddFlushConstraint(occ1YZp, occ2YZp, 0)
        cons.AddFlushConstraint(occ1XZp, occ2XZp, 0)

    End Sub

    Public Sub BuildDipPipeChannelAssembly(invApp As Inventor.Application, nozAsm As AssemblyDocument, folderPath As String)

        Dim asmDef As AssemblyComponentDefinition = nozAsm.ComponentDefinition

        Dim tg As TransientGeometry = invApp.TransientGeometry

        '==================================================
        ' PLACE COMPONENTS
        '==================================================

        Dim pipe1Occ As ComponentOccurrence = asmDef.Occurrences.Add(IO.Path.Combine(folderPath, "PIPE_1_FOR_NOZZLE_PIPE_SUPPORT.ipt"), tg.CreateMatrix())
        Dim plate1Occ As ComponentOccurrence = asmDef.Occurrences.Add(IO.Path.Combine(folderPath, "DIP_PIPE_CONNECTING_PLATE_1.ipt"), tg.CreateMatrix())
        Dim gasketOcc As ComponentOccurrence = asmDef.Occurrences.Add(IO.Path.Combine(folderPath, "GASKET_FOR_DIP_PIPE_CONNECTING_PLATE.ipt"), tg.CreateMatrix())
        Dim plate2Occ As ComponentOccurrence = asmDef.Occurrences.Add(IO.Path.Combine(folderPath, "DIP_PIPE_CONNECTING_PLATE_2.ipt"), tg.CreateMatrix())
        Dim pipe2Occ As ComponentOccurrence = asmDef.Occurrences.Add(IO.Path.Combine(folderPath, "PIPE_2_FOR_NOZZLE_PIPE_SUPPORT.ipt"), tg.CreateMatrix())

        '==================================================
        ' CONSTRAINTS
        '==================================================
        ConstrainByOriginPlanes(nozAsm, pipe1Occ, plate1Occ)
        ConstrainByOriginPlanes(nozAsm, plate1Occ, gasketOcc)
        ConstrainByOriginPlanes(nozAsm, gasketOcc, plate2Occ)
        ConstrainByOriginPlanes(nozAsm, plate2Occ, pipe2Occ)

        nozAsm.Update()

    End Sub

    Public Sub BuildNozzleAssembly_DipPipe_Channel_HOR(invApp As Inventor.Application, nozAsm As AssemblyDocument, pipe1Path As String, pipe2Path As String, plate1Path As String, gasketPath As String,
plate2Path As String, flangePath As String, padPath As String, selectedClient As String, nozzleName As String, nozzleODmm As Double, pipeThkMm As Double, pipeLengthMm As Double, outsideProjMm As Double,
rfPadODmm As Double, rfPadThkMm As Double, nps As String, nozzleDistanceMm As Double, offsetDistMm As Double, angleDeg As Double,
    Optional nozzleLocation As String = "")

        Dim asmDef As AssemblyComponentDefinition = nozAsm.ComponentDefinition
        Dim tg As TransientGeometry = invApp.TransientGeometry

        '==================================================
        ' PLACE COMPONENTS
        '==================================================
        Dim pipe1Occ As ComponentOccurrence = asmDef.Occurrences.Add(pipe1Path, tg.CreateMatrix())
        Dim plate1Occ As ComponentOccurrence = asmDef.Occurrences.Add(plate1Path, tg.CreateMatrix())
        Dim gasketOcc As ComponentOccurrence = asmDef.Occurrences.Add(gasketPath, tg.CreateMatrix())
        Dim plate2Occ As ComponentOccurrence = asmDef.Occurrences.Add(plate2Path, tg.CreateMatrix())
        Dim pipe2Occ As ComponentOccurrence = asmDef.Occurrences.Add(pipe2Path, tg.CreateMatrix())
        Dim flangeOcc As ComponentOccurrence = asmDef.Occurrences.Add(flangePath, tg.CreateMatrix())

        Dim padOcc As ComponentOccurrence = Nothing

        If padPath <> "" AndAlso IO.File.Exists(padPath) Then
            padOcc = asmDef.Occurrences.Add(padPath, tg.CreateMatrix())
        End If

        '==================================================
        ' CONSTRAINTS
        '==================================================
        ConstrainByOriginPlanes(nozAsm, pipe1Occ, plate1Occ)
        ConstrainByOriginPlanes(nozAsm, plate1Occ, gasketOcc)
        ConstrainByOriginPlanes(nozAsm, gasketOcc, plate2Occ)
        ConstrainByOriginPlanes(nozAsm, plate2Occ, pipe2Occ)

        '--------------------------------------------------
        ' PIPE1 ↔ FLANGE
        ' Use standard nozzle flange constraints
        '--------------------------------------------------
        ConstrainFlangeToPipe_Shell_HOR(nozAsm, pipe1Occ, flangeOcc, nozzleName, angleDeg, nozzleLocation)


        '--------------------------------------------------
        ' OPTIONAL RF PAD
        '--------------------------------------------------
        If padOcc IsNot Nothing Then

            SetRFPadParameters_HOR(padOcc, rfPadODmm, rfPadThkMm, nozzleODmm, nozzleDistanceMm, offsetDistMm)
            ConstrainByOriginPlanes(nozAsm, pipe1Occ, padOcc)

        End If
        nozAsm.Update()

    End Sub

#End Region

#Region "IMP FUNCTIONS"

    Private Function IsInventorRunning() As Boolean
        Try
            Dim inv = Marshal.GetActiveObject("Inventor.Application")
            Return inv IsNot Nothing
        Catch
            Return False
        End Try
    End Function

    Private Function HasActiveProject(inv As Inventor.Application) As Boolean
        Try
            Dim proj = inv.DesignProjectManager.ActiveDesignProject
            Return proj IsNot Nothing AndAlso IO.File.Exists(proj.FullFileName)
        Catch
            Return False
        End Try
    End Function

    Private Function SelectInventorFolder() As String

        ' Create and display the FolderBrowserDialog
        Using fbd As New FolderBrowserDialog()

            ' Text shown at the top of the folder selection dialog
            fbd.Description = "Select folder containing Inventor project and components"

            ' Show the dialog and check if the user clicked OK
            If fbd.ShowDialog() = DialogResult.OK Then

                ' Return the selected folder path
                Return fbd.SelectedPath

            End If
        End Using

        ' If the user cancels the dialog, return an empty string
        Return String.Empty

    End Function

    Private Sub ActivateInventorProject(invApp As Inventor.Application, projectFolder As String)

        '--------------------------------------
        ' Validate Folder Path
        '--------------------------------------
        If String.IsNullOrWhiteSpace(projectFolder) OrElse Not Directory.Exists(projectFolder) Then
            Throw New Exception("Invalid project folder path.")
        End If

        '--------------------------------------
        ' Find Inventor Project File (*.ipj)
        '--------------------------------------
        Dim ipjFile As String = Directory.GetFiles(projectFolder, "*.ipj").FirstOrDefault()

        If String.IsNullOrEmpty(ipjFile) Then
            Throw New Exception("No Inventor project (.ipj) found in selected folder.")
        End If

        '--------------------------------------
        ' Get Project Manager
        '--------------------------------------
        Dim projMgr As DesignProjectManager = invApp.DesignProjectManager

        Dim projName As String = System.IO.Path.GetFileNameWithoutExtension(ipjFile)

        Dim proj As DesignProject = Nothing

        '--------------------------------------
        ' Check if Project Already Loaded
        '--------------------------------------
        For Each p As DesignProject In projMgr.DesignProjects
            If p.Name.Equals(projName, StringComparison.OrdinalIgnoreCase) Then
                proj = p
                Exit For
            End If
        Next

        '--------------------------------------
        ' Add Project if Not Loaded
        '--------------------------------------
        If proj Is Nothing Then
            proj = projMgr.DesignProjects.AddExisting(ipjFile)
        End If

        '--------------------------------------
        ' Activate Project
        '--------------------------------------
        proj.Activate()

    End Sub

    Private Function CreateNewAssembly(invApp As Inventor.Application, projectFolder As String) As AssemblyDocument

        ' Get the default Assembly template file path (Metric system)
        Dim asmTemplate As String = invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure)

        ' Create a new Assembly document using the selected template
        ' True = make this document the active document
        Dim asmDoc As AssemblyDocument = invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, asmTemplate, True)

        ' Set a user-friendly display name for the assembly
        ' (This does not save the file to disk)
        asmDoc.DisplayName = "Main_Assembly"

        ' Return the created Assembly document
        Return asmDoc

    End Function

    Private Function SelectInventorFile(title As String) As String
        Try
            Using dlg As New OpenFileDialog()
                dlg.Title = title
                dlg.Filter = "Inventor Part (*.ipt)|*.ipt"
                dlg.Multiselect = False

                If dlg.ShowDialog() = DialogResult.OK Then
                    Return dlg.FileName
                End If
            End Using
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        Return ""
    End Function

    Public Function LoadPipeTable() As List(Of PipeDim)

        Dim t As New List(Of PipeDim)

        '========================
        ' 1/8
        '========================
        t.Add(New PipeDim With {.ND = "1/8''", .SCH = "10S", .OD = 10.287, .ID = 7.7978, .THK = 1.2446})
        t.Add(New PipeDim With {.ND = "1/8''", .SCH = "40S", .OD = 10.287, .ID = 6.8326, .THK = 1.7272})
        t.Add(New PipeDim With {.ND = "1/8", .SCH = "80S", .OD = 10.287, .ID = 5.461, .THK = 2.413})
        t.Add(New PipeDim With {.ND = "1/8''", .SCH = "10", .OD = 10.287, .ID = 7.7978, .THK = 1.2446})
        t.Add(New PipeDim With {.ND = "1/8''", .SCH = "40", .OD = 10.287, .ID = 6.8326, .THK = 1.7272})
        t.Add(New PipeDim With {.ND = "1/8", .SCH = "80", .OD = 10.287, .ID = 5.461, .THK = 2.413})

        '========================
        ' 1/4
        '========================
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "10S", .OD = 13.716, .ID = 10.414, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "40S", .OD = 13.716, .ID = 9.2456, .THK = 2.2352})
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "80S", .OD = 13.716, .ID = 7.6708, .THK = 3.0226})
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "10", .OD = 13.716, .ID = 10.414, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "40", .OD = 13.716, .ID = 9.2456, .THK = 2.2352})
        t.Add(New PipeDim With {.ND = "1/4''", .SCH = "80", .OD = 13.716, .ID = 7.6708, .THK = 3.0226})

        '========================
        ' 3/8
        '========================
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "10S", .OD = 17.145, .ID = 13.843, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "40S", .OD = 17.145, .ID = 12.5222, .THK = 2.3114})
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "80S", .OD = 17.145, .ID = 10.7442, .THK = 3.2004})
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "10", .OD = 17.145, .ID = 13.843, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "40", .OD = 17.145, .ID = 12.5222, .THK = 2.3114})
        t.Add(New PipeDim With {.ND = "3/8''", .SCH = "80", .OD = 17.145, .ID = 10.7442, .THK = 3.2004})

        '========================
        ' 1/2
        '========================
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "5S", .OD = 21.336, .ID = 18.034, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "10S", .OD = 21.336, .ID = 17.1196, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "40S", .OD = 21.336, .ID = 15.7988, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "80S", .OD = 21.336, .ID = 13.8684, .THK = 3.7338})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "5", .OD = 21.336, .ID = 18.034, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "10", .OD = 21.336, .ID = 17.1196, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "40", .OD = 21.336, .ID = 15.7988, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "1/2''", .SCH = "80", .OD = 21.336, .ID = 13.8684, .THK = 3.7338})

        '========================
        ' 3/4
        '========================
        t.Add(New PipeDim With {.ND = "3/4''", .SCH = "5S", .OD = 26.67, .ID = 23.368, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "3/4''", .SCH = "10S", .OD = 26.67, .ID = 22.4536, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "3/4''", .SCH = "40S", .OD = 26.67, .ID = 20.9296, .THK = 2.8702})
        t.Add(New PipeDim With {.ND = "3/4''", .SCH = "80S", .OD = 26.67, .ID = 18.8468, .THK = 3.9116})

        '========================
        ' 1
        '========================
        t.Add(New PipeDim With {.ND = "1''", .SCH = "5S", .OD = 33.401, .ID = 30.099, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1''", .SCH = "10S", .OD = 33.401, .ID = 27.8638, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "1''", .SCH = "40S", .OD = 33.401, .ID = 26.6446, .THK = 3.3782})
        t.Add(New PipeDim With {.ND = "1''", .SCH = "80S", .OD = 33.401, .ID = 24.3078, .THK = 4.5466})

        '========================
        ' 1 1/4
        '========================
        t.Add(New PipeDim With {.ND = "1 1/4''", .SCH = "5S", .OD = 42.164, .ID = 38.862, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1 1/4''", .SCH = "10S", .OD = 42.164, .ID = 36.6268, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "1 1/4''", .SCH = "40S", .OD = 42.164, .ID = 35.052, .THK = 3.556})
        t.Add(New PipeDim With {.ND = "1 1/4''", .SCH = "80S", .OD = 42.164, .ID = 32.4612, .THK = 4.8514})

        '========================
        ' 1 1/2
        '========================
        t.Add(New PipeDim With {.ND = "1 1/2''", .SCH = "5S", .OD = 48.26, .ID = 44.958, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "1 1/2''", .SCH = "10S", .OD = 48.26, .ID = 42.7228, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "1 1/2''", .SCH = "40S", .OD = 48.26, .ID = 40.894, .THK = 3.683})
        t.Add(New PipeDim With {.ND = "1 1/2''", .SCH = "80S", .OD = 48.26, .ID = 38.1, .THK = 5.08})

        '========================
        ' 2
        '========================
        t.Add(New PipeDim With {.ND = "2''", .SCH = "5S", .OD = 60.325, .ID = 57.023, .THK = 1.651})
        t.Add(New PipeDim With {.ND = "2''", .SCH = "10S", .OD = 60.325, .ID = 54.7878, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "2''", .SCH = "40S", .OD = 60.325, .ID = 52.5018, .THK = 3.9116})
        t.Add(New PipeDim With {.ND = "2''", .SCH = "80S", .OD = 60.325, .ID = 49.2506, .THK = 5.5372})
        t.Add(New PipeDim With {.ND = "2''", .SCH = "160", .OD = 60.325, .ID = 42.849, .THK = 8.738})

        '========================
        ' 2 1/2
        '========================
        t.Add(New PipeDim With {.ND = "2 1/2''", .SCH = "5S", .OD = 73.025, .ID = 68.8086, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "2 1/2''", .SCH = "10S", .OD = 73.025, .ID = 66.929, .THK = 3.048})
        t.Add(New PipeDim With {.ND = "2 1/2''", .SCH = "40S", .OD = 73.025, .ID = 62.7126, .THK = 5.1562})
        t.Add(New PipeDim With {.ND = "2 1/2''", .SCH = "80S", .OD = 73.025, .ID = 59.0042, .THK = 7.0104})

        '========================
        ' 3
        '========================
        t.Add(New PipeDim With {.ND = "3''", .SCH = "5S", .OD = 88.9, .ID = 84.6836, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "3''", .SCH = "10S", .OD = 88.9, .ID = 82.804, .THK = 3.048})
        t.Add(New PipeDim With {.ND = "3''", .SCH = "40S", .OD = 88.9, .ID = 77.9272, .THK = 5.4864})
        t.Add(New PipeDim With {.ND = "3''", .SCH = "80S", .OD = 88.9, .ID = 73.66, .THK = 7.62})
        t.Add(New PipeDim With {.ND = "3''", .SCH = "80", .OD = 88.9, .ID = 73.66, .THK = 7.62})

        '========================
        ' 4
        '========================
        t.Add(New PipeDim With {.ND = "4''", .SCH = "5S", .OD = 114.3, .ID = 110.0836, .THK = 2.1082})
        t.Add(New PipeDim With {.ND = "4''", .SCH = "10S", .OD = 114.3, .ID = 108.204, .THK = 3.048})
        t.Add(New PipeDim With {.ND = "4''", .SCH = "40S", .OD = 114.3, .ID = 102.2604, .THK = 6.0198})
        t.Add(New PipeDim With {.ND = "4''", .SCH = "80S", .OD = 114.3, .ID = 97.1804, .THK = 8.5598})
        t.Add(New PipeDim With {.ND = "4''", .SCH = "120", .OD = 114.3, .ID = 92.05, .THK = 11.125})

        '========================
        ' 6
        '========================
        t.Add(New PipeDim With {.ND = "6''", .SCH = "5S", .OD = 168.275, .ID = 162.7378, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "6''", .SCH = "10S", .OD = 168.275, .ID = 161.4678, .THK = 3.4036})
        t.Add(New PipeDim With {.ND = "6''", .SCH = "40S", .OD = 168.275, .ID = 154.051, .THK = 7.112})
        t.Add(New PipeDim With {.ND = "6''", .SCH = "80S", .OD = 168.275, .ID = 146.3294, .THK = 10.9728})

        '========================
        ' 8
        '========================
        t.Add(New PipeDim With {.ND = "8''", .SCH = "5S", .OD = 219.075, .ID = 213.5378, .THK = 2.7686})
        t.Add(New PipeDim With {.ND = "8''", .SCH = "10S", .OD = 219.075, .ID = 211.5566, .THK = 3.7592})
        t.Add(New PipeDim With {.ND = "8''", .SCH = "40S", .OD = 219.075, .ID = 202.7174, .THK = 8.1788})
        t.Add(New PipeDim With {.ND = "8''", .SCH = "80S", .OD = 219.075, .ID = 193.675, .THK = 12.7})

        '========================
        ' 10
        '========================
        t.Add(New PipeDim With {.ND = "10''", .SCH = "5S", .OD = 273.05, .ID = 266.2428, .THK = 3.4036})
        t.Add(New PipeDim With {.ND = "10''", .SCH = "10S", .OD = 273.05, .ID = 264.668, .THK = 4.191})
        t.Add(New PipeDim With {.ND = "10''", .SCH = "40S", .OD = 273.05, .ID = 254.508, .THK = 9.271})
        t.Add(New PipeDim With {.ND = "10''", .SCH = "80S", .OD = 273.05, .ID = 247.65, .THK = 12.7})

        '========================
        ' 12
        '========================
        t.Add(New PipeDim With {.ND = "12''", .SCH = "5S", .OD = 323.85, .ID = 315.9252, .THK = 3.9624})
        t.Add(New PipeDim With {.ND = "12''", .SCH = "10S", .OD = 323.85, .ID = 314.706, .THK = 4.572})
        t.Add(New PipeDim With {.ND = "12''", .SCH = "40S", .OD = 323.85, .ID = 304.8, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "12''", .SCH = "80S", .OD = 323.85, .ID = 298.45, .THK = 12.7})

        '========================
        ' 14
        '========================
        t.Add(New PipeDim With {.ND = "14''", .SCH = "5S", .OD = 355.6, .ID = 347.6752, .THK = 3.9624})
        t.Add(New PipeDim With {.ND = "14''", .SCH = "10S", .OD = 355.6, .ID = 346.0496, .THK = 4.7752})
        t.Add(New PipeDim With {.ND = "14''", .SCH = "40S", .OD = 355.6, .ID = 336.55, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "14''", .SCH = "80S", .OD = 355.6, .ID = 330.2, .THK = 12.7})

        '========================
        ' 16
        '========================
        t.Add(New PipeDim With {.ND = "16''", .SCH = "5S", .OD = 406.4, .ID = 398.018, .THK = 4.191})
        t.Add(New PipeDim With {.ND = "16''", .SCH = "10S", .OD = 406.4, .ID = 396.8496, .THK = 4.7752})
        t.Add(New PipeDim With {.ND = "16''", .SCH = "40S", .OD = 406.4, .ID = 387.35, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "16''", .SCH = "80S", .OD = 406.4, .ID = 381, .THK = 12.7})

        '========================
        ' 18
        '========================
        t.Add(New PipeDim With {.ND = "18''", .SCH = "5S", .OD = 457.2, .ID = 448.818, .THK = 4.191})
        t.Add(New PipeDim With {.ND = "18''", .SCH = "10S", .OD = 457.2, .ID = 447.6496, .THK = 4.7752})
        t.Add(New PipeDim With {.ND = "18''", .SCH = "40S", .OD = 457.2, .ID = 438.15, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "18''", .SCH = "80S", .OD = 457.2, .ID = 431.8, .THK = 12.7})

        '========================
        ' 20
        '========================
        t.Add(New PipeDim With {.ND = "20''", .SCH = "5S", .OD = 508, .ID = 498.4496, .THK = 4.7752})
        t.Add(New PipeDim With {.ND = "20''", .SCH = "10S", .OD = 508, .ID = 496.9256, .THK = 5.5372})
        t.Add(New PipeDim With {.ND = "20''", .SCH = "40S", .OD = 508, .ID = 488.95, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "20''", .SCH = "80S", .OD = 508, .ID = 482.6, .THK = 12.7})

        '========================
        ' 22
        '========================
        t.Add(New PipeDim With {.ND = "22''", .SCH = "5S", .OD = 558.8, .ID = 549.2496, .THK = 4.7752})
        t.Add(New PipeDim With {.ND = "22''", .SCH = "10S", .OD = 558.8, .ID = 547.7256, .THK = 5.5372})

        '========================
        ' 24
        '========================
        t.Add(New PipeDim With {.ND = "24''", .SCH = "5S", .OD = 609.6, .ID = 598.5256, .THK = 5.5372})
        t.Add(New PipeDim With {.ND = "24''", .SCH = "10S", .OD = 609.6, .ID = 596.9, .THK = 6.35})
        t.Add(New PipeDim With {.ND = "24''", .SCH = "40S", .OD = 609.6, .ID = 590.55, .THK = 9.525})
        t.Add(New PipeDim With {.ND = "24''", .SCH = "80S", .OD = 609.6, .ID = 584.2, .THK = 12.7})
        t.Add(New PipeDim With {.ND = "24''", .SCH = "-", .OD = 609.6, .ID = 598.5256, .THK = 8})

        '========================
        ' 30
        '========================
        t.Add(New PipeDim With {.ND = "30''", .SCH = "5S", .OD = 762, .ID = 749.3, .THK = 6.35})
        t.Add(New PipeDim With {.ND = "30''", .SCH = "10S", .OD = 762, .ID = 746.1504, .THK = 7.9248})

        Return t

    End Function

    Public Function GetNozzleComponentPaths_HOR(invApp As Inventor.Application, projectFolder As String, nozzleName As String, nps As String, remarks As String, isManway As Boolean, angleDeg As Double,
offsetDistMm As Double, Optional includePad As Boolean = True, Optional flangeClass As String = "150#") As Dictionary(Of String, String)

        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        remarks = If(remarks, "").ToUpper().Trim()

        '==================================================
        ' ENSURE NOZZLES FOLDER EXISTS
        '==================================================
        Dim nozzleFolder As String

        If SelectedClient = "ARAMCO" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "ARAMCO\Nozzles")
        ElseIf SelectedClient = "ADNOC" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "ADNOC\Nozzles")
        ElseIf SelectedClient = "QATAR" Then
            nozzleFolder = IO.Path.Combine(projectFolder, "QATAR\Nozzles")
        End If

        If Not IO.Directory.Exists(nozzleFolder) Then
            IO.Directory.CreateDirectory(nozzleFolder)
        End If

        '==================================================
        ' DIP PIPE DETECTION
        '==================================================
        Dim isDipPipe As Boolean = remarks = "DIP PIPE" OrElse
                               remarks = "DIP PIPE (CHANNEL)" OrElse
                               remarks = "DIP PIPE (ANGLE)"

        '==================================================
        ' GET COMMON FLANGE MASTER
        '==================================================
        Dim flangeMaster As String = ResolveFlangeMaster_ByNPS(projectFolder, nps, flangeClass)

        If Not IO.File.Exists(flangeMaster) Then
            Throw New Exception($"❌ No flange found for NPS {nps} Class {flangeClass}")
        End If

        '==================================================
        ' NORMALIZE ANGLE & DIRECTION (SHARED)
        '==================================================
        Dim ang As Integer = CInt(angleDeg Mod 360)

        Select Case ang
            Case 0, 90, 180, 270
            Case Else
                Throw New Exception($"❌ Unsupported nozzle angle : {ang}")
        End Select

        Dim dir As String = ""
        If offsetDistMm > 0 Then
            dir = "POST"
        ElseIf offsetDistMm < 0 Then
            dir = "NEG"
        Else
            dir = "ORIGIN"
        End If

        '==================================================
        ' PIPE MASTER RESOLUTION
        '==================================================
        If isDipPipe Then

            Select Case remarks

                Case "DIP PIPE"
                    '--------------------------------------------------
                    ' 🟡 DIP PIPE → SINGLE PIPE
                    ' Folder : ARAMCO\
                    ' File   : PIPE_1_FOR_NOZZLE_PIPE_SUPPORT.ipt
                    '--------------------------------------------------
                    Dim pipe1Master_DP As String = IO.Path.Combine(projectFolder, "ARAMCO\PIPE_1_FOR_NOZZLE_PIPE_SUPPORT.ipt")

                    If Not IO.File.Exists(pipe1Master_DP) Then
                        Throw New Exception($"❌ DIP PIPE master missing : {pipe1Master_DP}")
                    End If

                    Dim nozzlePipe1_DP As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE_1.ipt")
                    If Not IO.File.Exists(nozzlePipe1_DP) Then
                        IO.File.Copy(pipe1Master_DP, nozzlePipe1_DP, True)
                    End If
                    result("PIPE1") = nozzlePipe1_DP
                    result("DIP_PIPE_TYPE") = remarks

                Case "DIP PIPE (CHANNEL)"
                    '--------------------------------------------------
                    ' 🟠 DIP PIPE (CHANNEL) → TWO PIPES
                    ' Folder : ARAMCO\PIPE_SUPPORT_CHANNEL\
                    ' File   : PIPE_ANG_{ang}_{dir}_FOR_PS.ipt
                    '--------------------------------------------------
                    Dim pipeFileName_DPC As String = $"PIPE_ANG_{ang}_{dir}_FOR_PS.ipt"
                    Dim pipe1Master_DPC As String = IO.Path.Combine(projectFolder, $"ARAMCO\PIPE_SUPPORT_CHANNEL\{pipeFileName_DPC}")
                    Dim pipe2Master_DPC As String = IO.Path.Combine(projectFolder, $"ARAMCO\PIPE_SUPPORT_CHANNEL\{pipeFileName_DPC}")

                    If Not IO.File.Exists(pipe1Master_DPC) Then
                        Throw New Exception($"❌ DIP PIPE (CHANNEL) master missing : {pipe1Master_DPC}")
                    End If

                    ' Copy PIPE1
                    Dim nozzlePipe1_DPC As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE_1.ipt")
                    If Not IO.File.Exists(nozzlePipe1_DPC) Then
                        IO.File.Copy(pipe1Master_DPC, nozzlePipe1_DPC, True)
                    End If
                    result("PIPE1") = nozzlePipe1_DPC

                    ' Copy PIPE2
                    Dim nozzlePipe2_DPC As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE_2.ipt")
                    If Not IO.File.Exists(nozzlePipe2_DPC) Then
                        IO.File.Copy(pipe2Master_DPC, nozzlePipe2_DPC, True)
                    End If
                    result("PIPE2") = nozzlePipe2_DPC
                    result("DIP_PIPE_TYPE") = remarks

                Case "DIP PIPE (ANGLE)"
                    '--------------------------------------------------
                    ' 🔴 DIP PIPE (ANGLE) → SINGLE PIPE
                    ' Folder : ARAMCO\PIPE_SUPPORT_CHANNEL\
                    ' File   : PIPE_ANG_{ang}_{dir}_FOR_PS.ipt
                    '--------------------------------------------------
                    Dim pipeFileName_DPA As String = $"PIPE_ANG_{ang}_{dir}_FOR_PS.ipt"
                    Dim pipe1Master_DPA As String = IO.Path.Combine(projectFolder, $"ARAMCO\PIPE_SUPPORT_CHANNEL\{pipeFileName_DPA}")

                    If Not IO.File.Exists(pipe1Master_DPA) Then
                        Throw New Exception($"❌ DIP PIPE (ANGLE) master missing : {pipe1Master_DPA}")
                    End If

                    ' ✅ ONLY ONE PIPE
                    Dim nozzlePipe1_DPA As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE_1.ipt")
                    If Not IO.File.Exists(nozzlePipe1_DPA) Then
                        IO.File.Copy(pipe1Master_DPA, nozzlePipe1_DPA, True)
                    End If
                    result("PIPE1") = nozzlePipe1_DPA
                    result("DIP_PIPE_TYPE") = remarks

            End Select

        Else

            '--------------------------------------------------
            ' 🟢 STANDARD / MANWAY → SINGLE PIPE
            '--------------------------------------------------
            Dim pipeFileName As String = $"PIPE_ANG_{ang}_{dir}.ipt"
            Dim pipeMaster As String = IO.Path.Combine(projectFolder, pipeFileName)

            If Not IO.File.Exists(pipeMaster) Then
                Throw New Exception($"❌ Pipe master missing : {pipeMaster}")
            End If

            Dim nozzlePipe As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE.ipt")
            If Not IO.File.Exists(nozzlePipe) Then
                IO.File.Copy(pipeMaster, nozzlePipe, True)
            End If
            result("PIPE") = nozzlePipe

            '--------------------------------------------------
            ' 🟦 PTFE LINING — PIPE (ARAMCO only, "PTFE LINING" checkbox)
            ' Standard pipe above is kept as-is; this is placed alongside it.
            '--------------------------------------------------
            If SelectedClient = "ARAMCO" AndAlso CheckBox1.Checked Then

                Dim ptfeFolder As String = IO.Path.Combine(projectFolder, "ARAMCO\PTFE_LINING")
                Dim pipePtfeMaster As String = IO.Path.Combine(ptfeFolder, $"PIPE_ANG_{ang}_{dir}_PTFE.ipt")

                If Not IO.File.Exists(pipePtfeMaster) Then
                    Throw New Exception($"❌ PTFE pipe master missing : {pipePtfeMaster}")
                End If

                Dim nozzlePipePtfe As String = IO.Path.Combine(nozzleFolder, nozzleName & "_PIPE_PTFE.ipt")
                If Not IO.File.Exists(nozzlePipePtfe) Then
                    IO.File.Copy(pipePtfeMaster, nozzlePipePtfe, True)
                End If
                result("PIPE_PTFE") = nozzlePipePtfe

            End If

        End If

        '==================================================
        ' FLANGE
        '==================================================
        Dim nozzleFlange As String = IO.Path.Combine(nozzleFolder, nozzleName & "_FLANGE.ipt")
        If Not IO.File.Exists(nozzleFlange) Then
            IO.File.Copy(flangeMaster, nozzleFlange, True)
        End If
        EnsureFlangeEndPlane(invApp, nozzleFlange, nozzleName)
        result("FLANGE") = nozzleFlange

        '==================================================
        ' PTFE LINING — FLANGE (ARAMCO only, "PTFE LINING" checkbox)
        ' Standard flange above is kept as-is; this is placed alongside it.
        '==================================================
        If SelectedClient = "ARAMCO" AndAlso CheckBox1.Checked Then

            Dim ptfeFolder As String = IO.Path.Combine(projectFolder, "ARAMCO\PTFE_LINING")
            Dim npsClean As String = nps.Replace("'", "").Replace("""", "").Trim()
            Dim flangePtfeFileName As String
            Select Case flangeClass.Trim().ToUpper()
                Case "150", "150#"
                    flangePtfeFileName = $"FLANGE_{npsClean}_INCH_PTFE.ipt"
                Case "300", "300#"
                    flangePtfeFileName = $"FLANGE_{npsClean}_INCH_CL300_PTFE.ipt"
                Case Else
                    Throw New Exception($"❌ Unsupported flange class for PTFE lining: {flangeClass}")
            End Select
            Dim flangePtfeMaster As String = IO.Path.Combine(ptfeFolder, flangePtfeFileName)

            If Not IO.File.Exists(flangePtfeMaster) Then
                Throw New Exception($"❌ PTFE flange master missing : {flangePtfeMaster}")
            End If

            Dim nozzleFlangePtfe As String = IO.Path.Combine(nozzleFolder, nozzleName & "_FLANGE_PTFE.ipt")
            If Not IO.File.Exists(nozzleFlangePtfe) Then
                IO.File.Copy(flangePtfeMaster, nozzleFlangePtfe, True)
            End If
            result("FLANGE_PTFE") = nozzleFlangePtfe

        End If

        '==================================================
        ' OPTIONAL RF PAD
        '==================================================
        If includePad Then

            Dim usePtfeLining As Boolean = (SelectedClient = "ARAMCO" AndAlso CheckBox1.Checked)
            Dim ptfeFolder As String = IO.Path.Combine(projectFolder, "ARAMCO\PTFE_LINING")

            ' With PTFE lining, the base pad is the "_FOR_LINING" variant (designed to sit
            ' against a PTFE-lined shell) instead of the standard RF pad.
            Dim rfPadFileName As String = If(usePtfeLining, $"RF_PAD_ANG_{ang}_{dir}_FOR_LINING.ipt", $"RF_PAD_ANG_{ang}_{dir}.ipt")
            Dim rfPadMaster As String = IO.Path.Combine(If(usePtfeLining, ptfeFolder, projectFolder), rfPadFileName)

            If Not IO.File.Exists(rfPadMaster) Then
                Throw New Exception($"❌ RF Pad master missing : {rfPadMaster}")
            End If

            Dim padPath As String = IO.Path.Combine(nozzleFolder, nozzleName & "_RF_PAD.ipt")
            If Not IO.File.Exists(padPath) Then
                IO.File.Copy(rfPadMaster, padPath, True)
            End If
            result("PAD") = padPath

            If usePtfeLining Then

                Dim rfPadPtfeMaster As String = IO.Path.Combine(ptfeFolder, $"RF_PAD_ANG_{ang}_{dir}_PTFE.ipt")

                If Not IO.File.Exists(rfPadPtfeMaster) Then
                    Throw New Exception($"❌ PTFE RF Pad master missing : {rfPadPtfeMaster}")
                End If

                Dim padPtfePath As String = IO.Path.Combine(nozzleFolder, nozzleName & "_RF_PAD_PTFE.ipt")
                If Not IO.File.Exists(padPtfePath) Then
                    IO.File.Copy(rfPadPtfeMaster, padPtfePath, True)
                End If
                result("PAD_PTFE") = padPtfePath

            End If

        End If

        Return result

    End Function

    Private Sub CleanExistingNozzleData(projectFolder As String)
        If String.IsNullOrWhiteSpace(projectFolder) Then Exit Sub
        If Not IO.Directory.Exists(projectFolder) Then Exit Sub

        '==================================================
        ' 1️⃣ DELETE ALL FILES STARTING WITH "Nozzle_"
        '==================================================
        For Each filePath As String In IO.Directory.GetFiles(projectFolder)
            Dim fileName As String = IO.Path.GetFileName(filePath).ToUpper()
            If fileName.StartsWith("NOZZLE_") Then
                Try
                    IO.File.Delete(filePath)
                Catch ex As Exception
                    Throw New Exception("❌ Unable to delete file: " & filePath & vbCrLf & ex.Message)
                End Try
            End If
        Next

        '==================================================
        ' 2️⃣ DELETE CLIENT-SPECIFIC NOZZLES FOLDER
        '==================================================
        Dim clientNozzleFolders As String() = {
        IO.Path.Combine(projectFolder, "ARAMCO\Nozzles"),
        IO.Path.Combine(projectFolder, "ADNOC\Nozzles"),
        IO.Path.Combine(projectFolder, "QATAR\Nozzles")
    }

        For Each nozzleFolder As String In clientNozzleFolders
            If IO.Directory.Exists(nozzleFolder) Then
                Try
                    IO.Directory.Delete(nozzleFolder, True)
                    Debug.Print($"✅ Deleted: {nozzleFolder}")
                Catch ex As Exception
                    Throw New Exception("❌ Unable to delete Nozzles folder: " & nozzleFolder & vbCrLf & ex.Message)
                End Try
            End If
        Next

        '==================================================
        ' 3️⃣ DELETE "OldVersions" FOLDER
        '==================================================
        Dim oldVersionsFolder As String = IO.Path.Combine(projectFolder, "OldVersions")
        If IO.Directory.Exists(oldVersionsFolder) Then
            Try
                IO.Directory.Delete(oldVersionsFolder, True)
                Debug.Print($"✅ Deleted: {oldVersionsFolder}")
            Catch ex As Exception
                Throw New Exception("❌ Unable to delete OldVersions folder: " & oldVersionsFolder & vbCrLf & ex.Message)
            End Try
        End If

    End Sub

    Private Function SafeDouble(value As Object) As Double

        If value Is Nothing OrElse value Is DBNull.Value Then
            Return 0
        End If

        Dim txt As String = value.ToString.Trim()

        If txt = "" OrElse txt = "-" OrElse txt = "0" Then
            Return 0
        End If

        Dim result As Double

        If Double.TryParse(txt, result) Then
            Return result
        End If

        Return 0

    End Function

    Private Function GetPipeOD_mm(nps As String) As Double
        Select Case nps.Trim()
            Case "1/8''"
                Return 10.287
            Case "1/4''"
                Return 13.716
            Case "3/8''"
                Return 17.145
            Case "1/2''"
                Return 21.336
            Case "3/4''"
                Return 26.67
            Case "1''"
                Return 33.401
            Case "1 1/4''"
                Return 42.164
            Case "1 1/2''"
                Return 48.26
            Case "2''"
                Return 60.325
            Case "2 1/2''"
                Return 73.025
            Case "3''"
                Return 88.9
            Case "4''"
                Return 114.3
            Case "6''"
                Return 168.275
            Case "8''"
                Return 219.075
            Case "10''"
                Return 273.05
            Case "12''"
                Return 323.85
            Case "14''"
                Return 355.6
            Case "16''"
                Return 406.4
            Case "18''"
                Return 457.2
            Case "20''"
                Return 508.0
            Case "22''"
                Return 558.8
            Case "24''"
                Return 609.6
            Case Else
                Throw New Exception("Unsupported NPS: " & nps)
        End Select
    End Function

    Public Function GetPipeDim_BySizeAndSchedule(pipeTable As List(Of PipeDim), nps As String, schedule As String) As PipeDim
        Try
            '==================================================
            ' CLEAN INPUTS
            '==================================================
            Dim npsClean As String = nps.Trim().ToUpper()
            Dim schClean As String = schedule.Trim().ToUpper()

            '==================================================
            ' 1️⃣ EXACT MATCH FIRST
            '==================================================
            Dim result As PipeDim = pipeTable.FirstOrDefault(
            Function(p) p.ND.Trim().ToUpper() = npsClean AndAlso
                        p.SCH.Trim().ToUpper() = schClean)

            If result IsNot Nothing Then
                Debug.Print($"✅ Exact match: ND='{result.ND}' SCH='{result.SCH}'")
                Return result
            End If

            '==================================================
            ' 2️⃣ NUMERIC PART MATCH FALLBACK
            ' Extract digits/dots/slashes and compare
            '==================================================
            Dim npsNum As String = New String(npsClean.Where(
            Function(c) Char.IsDigit(c) OrElse c = "."c OrElse c = "/"c).ToArray()).Trim()

            result = pipeTable.FirstOrDefault(
            Function(p)
                Dim ptNum As String = New String(p.ND.Where(
                    Function(c) Char.IsDigit(c) OrElse c = "."c OrElse c = "/"c).ToArray()).Trim()
                Return ptNum = npsNum AndAlso p.SCH.Trim().ToUpper() = schClean
            End Function)

            If result IsNot Nothing Then
                Debug.Print($"✅ Numeric match: ND='{result.ND}' SCH='{result.SCH}'")
                Return result
            End If

            '==================================================
            ' 3️⃣ NOT FOUND — Return Nothing (no exception)
            '==================================================
            Debug.Print($"⚠ GetPipeDim_BySizeAndSchedule: NPS='{nps}' SCH='{schedule}' not found.")
            Return Nothing

        Catch ex As Exception
            Debug.Print($"⚠ GetPipeDim_BySizeAndSchedule error: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Function CreateNozzleSubAssembly(invApp As Inventor.Application, projectFolder As String, nozzleName As String) As AssemblyDocument

        Dim asmTemplate As String = invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure)
        Dim nozAsm As AssemblyDocument = invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, asmTemplate, False)
        nozAsm.DisplayName = nozzleName
        Dim nozPath As String = System.IO.Path.Combine(projectFolder, nozzleName & ".iam")
        nozAsm.SaveAs(nozPath, False)
        Return nozAsm

    End Function

    Function PlaceNozzleSubAssembly(invApp As Inventor.Application, asmDoc As AssemblyDocument, nozzleAsmPath As String) As ComponentOccurrence

        Dim asmDef = asmDoc.ComponentDefinition
        Dim tg = invApp.TransientGeometry

        Dim nozOcc = asmDef.Occurrences.Add(nozzleAsmPath, tg.CreateMatrix())

        Return nozOcc

    End Function

    Private Function GetFlangeHeightMm(nps As String, flangeClass As String) As Double

        Select Case flangeClass.Trim().ToUpper()
        '==============================
        ' CLASS 150 (WN)
        '==============================
            Case "150"

                Select Case nps.Trim()
                    Case "1''" : Return 55.373
                    Case "2''" : Return 63.5
                    Case "3''" : Return 69.85
                    Case "4''" : Return 76.2
                    Case "5''" : Return 88.9
                    Case "6''" : Return 88.9
                    Case "8''" : Return 101.6
                    Case "10''" : Return 101.6
                    Case "12''" : Return 114.3
                    Case "14''" : Return 127.0
                    Case "16''" : Return 127.0
                    Case "18''" : Return 139.7
                    Case "24''" : Return 152.4
                End Select

            Case "150#"

                Select Case nps.Trim()
                    Case "1''" : Return 55.373
                    Case "2''" : Return 63.5
                    Case "3''" : Return 69.85
                    Case "4''" : Return 76.2
                    Case "5''" : Return 88.9
                    Case "6''" : Return 88.9
                    Case "8''" : Return 101.6
                    Case "10''" : Return 101.6
                    Case "12''" : Return 114.3
                    Case "14''" : Return 127.0
                    Case "16''" : Return 127.0
                    Case "18''" : Return 139.7
                    Case "24''" : Return 152.4
                End Select

        '==============================
        ' CLASS 300 (WN)
        '==============================
            Case "300"

                Select Case nps.Trim()
                    Case "1''" : Return 52.324
                    Case "2''" : Return 69.85
                    Case "3''" : Return 79.248
                    Case "4''" : Return 85.852
                    Case "5''" : Return 98.552
                    Case "6''" : Return 98.552
                    Case "8''" : Return 111.252
                    Case "10''" : Return 117.348
                    Case "12''" : Return 130.048
                    Case "14''" : Return 142.748
                    Case "16''" : Return 146.05
                    Case "18''" : Return 158.75
                End Select

            Case "300#"

                Select Case nps.Trim()
                    Case "1''" : Return 52.324
                    Case "2''" : Return 69.85
                    Case "3''" : Return 79.248
                    Case "4''" : Return 85.852
                    Case "5''" : Return 98.552
                    Case "6''" : Return 98.552
                    Case "8''" : Return 111.252
                    Case "10''" : Return 117.348
                    Case "12''" : Return 130.048
                    Case "14''" : Return 142.748
                    Case "16''" : Return 146.05
                    Case "18''" : Return 158.75
                End Select

        End Select

        Return 0.0 ' Unknown size or class

    End Function

    Private Function ResolveFlangeMaster_ByNPS(projectFolder As String, npsRaw As String, flangeClass As String) As String

        '==================================================
        ' 1️⃣ CLEAN NPS TEXT
        ' Example:
        ' "2''" → "2"
        '==================================================
        Dim nps As String = npsRaw.Replace("'", "").Replace("""", "").Trim()
        If nps = "" Then
            Throw New Exception("❌ Invalid NPS value for flange")
        End If


        '==================================================
        ' 2️⃣ BUILD FILE NAME BASED ON CLASS
        '==================================================
        Dim flangeFileName As String
        Select Case flangeClass.Trim().ToUpper()
            Case "150", "150#"
                flangeFileName = $"FLANGE_{nps}_INCH.ipt"
            Case "300", "300#"
                flangeFileName = $"FLANGE_{nps}_INCH_CL300.ipt"
            Case Else
                Throw New Exception($"❌ Unsupported flange class: {flangeClass}")
        End Select

        '==================================================
        ' 3️⃣ VALIDATE FILE EXISTS
        '==================================================
        Dim flangeFullPath As String = IO.Path.Combine(projectFolder, flangeFileName)
        If Not IO.File.Exists(flangeFullPath) Then
            Throw New Exception($"❌ Flange file not found: {flangeFileName}")
        End If
        Return flangeFullPath

    End Function

    Public Sub EnsureFlangeEndPlane(invApp As Inventor.Application, flangePath As String, nozzleName As String)

        '==================================================
        ' OPEN FLANGE PART DOCUMENT
        '==================================================
        Dim partDoc As PartDocument = CType(invApp.Documents.Open(flangePath, False), PartDocument)
        Dim partDef As PartComponentDefinition = partDoc.ComponentDefinition

        '==================================================
        ' DYNAMIC PLANE NAME
        ' Example: N6A_End_Plane
        '==================================================
        Dim planeName As String = nozzleName & "_End_Plane"

        '==================================================
        ' REUSE IF ALREADY EXISTS (CLASS 150 CASE)
        '==================================================
        For Each wp As WorkPlane In partDef.WorkPlanes

            If wp.Name.Equals(planeName, StringComparison.OrdinalIgnoreCase) Then

                wp.Visible = False
                partDoc.Close(True)
                Exit Sub

            End If

        Next


        '==================================================
        ' CREATE TEMP OCCURRENCE CONTEXT
        ' (Required for GetFlangeThicknessCm)
        '==================================================
        Dim tempAsm As AssemblyDocument =
        CType(invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject,
                                   ,
                                   False),
              AssemblyDocument)

        Dim asmDef = tempAsm.ComponentDefinition
        Dim tg = invApp.TransientGeometry

        Dim flangeOcc As ComponentOccurrence =
        asmDef.Occurrences.Add(flangePath,
                               tg.CreateMatrix())


        '==================================================
        ' GET FLANGE THICKNESS USING YOUR FUNCTION
        '==================================================
        Dim flangeThkCm As Double = GetFlangeThicknessCm(flangeOcc)

        tempAsm.Close(True)


        If flangeThkCm <= 0 Then
            Throw New Exception("❌ Invalid flange thickness detected.")
        End If


        '==================================================
        ' BASE PLANE = YZ PLANE
        '==================================================
        Dim yzPlane As WorkPlane =
        partDef.WorkPlanes.Item("YZ Plane")


        '==================================================
        ' CREATE OFFSET END PLANE
        '==================================================
        Dim endPlane As WorkPlane =
        partDef.WorkPlanes.AddByPlaneAndOffset(yzPlane,
                                               flangeThkCm)

        endPlane.Name = planeName
        endPlane.Visible = False


        '==================================================
        ' UPDATE + SAVE PART
        '==================================================
        partDoc.Update()
        partDoc.Save()
        partDoc.Close(True)

    End Sub

    '==================================================
    ' NORMALIZE NPS — Remove extra apostrophes
    ' DGV sends  : "2'''"  (3 apostrophes)
    ' PipeTable  : "2''"   (2 apostrophes)
    '==================================================
    Private Function NormalizeNPS(nps As String) As String
        If String.IsNullOrWhiteSpace(nps) Then Return ""
        Dim result As String = nps.Trim()

        ' Replace 3 apostrophes with 2
        result = result.Replace("'''", "''")

        ' Replace inch symbol variants
        result = result.Replace("″", "''")
        result = result.Replace("""", "''")

        Return result
    End Function

    Public Sub SetFlangeOutsideProjection(flangeOcc As ComponentOccurrence, outsideProjectionMm As Double)

        Dim flangeDef As PartComponentDefinition = CType(flangeOcc.Definition, PartComponentDefinition)
        Dim p As Parameter

        Try
            p = flangeDef.Parameters.Item("OUTSIDE_PROJECTION")
        Catch
            'Throw New Exception("❌ Parameter 'OUTSIDE_PROJECTION' not found in flange part")
        End Try

        ' mm → cm
        p.Expression = (outsideProjectionMm).ToString()
        flangeDef.Document.Update()

    End Sub

    ''' Sets the flange's "B" (Bore) parameter to the mating pipe's inside diameter, so the
    ''' flange bore matches whatever NPS/schedule the pipe was resolved to. Silently skipped
    ''' if the flange part has no "B" parameter, same tolerant pattern as
    ''' SetFlangeOutsideProjection.
    Public Sub SetFlangeBore(flangeOcc As ComponentOccurrence, boreMm As Double)

        Dim flangeDef As PartComponentDefinition = CType(flangeOcc.Definition, PartComponentDefinition)
        Dim p As Parameter = Nothing

        Try
            p = flangeDef.Parameters.Item("B")
        Catch
        End Try

        If p Is Nothing Then Exit Sub

        ' mm → cm
        p.Expression = (boreMm).ToString()
        flangeDef.Document.Update()

    End Sub

    Function GetOrCreateFlangeEndPlane(invApp As Inventor.Application, flangeOcc As ComponentOccurrence, nozzleName As String) As WorkPlane

        Dim partDoc As PartDocument = CType(flangeOcc.Definition.Document, PartDocument)

        Dim partDef As PartComponentDefinition = partDoc.ComponentDefinition

        Dim planeName As String = nozzleName & "_End_Plane"

        '--------------------------------------
        ' Reuse if already exists
        '--------------------------------------
        For Each wp As WorkPlane In partDef.WorkPlanes
            If wp.Name = planeName Then
                wp.Visible = False
                Return wp
            End If
        Next

        '--------------------------------------
        ' Measure flange thickness (cm)
        '--------------------------------------
        Dim flangeThkCm As Double = GetFlangeThicknessCm(flangeOcc)

        '--------------------------------------
        ' Base plane: YZ Plane
        ' (flange axis assumed along Y)
        '--------------------------------------
        Dim yzPlane As WorkPlane = partDef.WorkPlanes.Item("YZ Plane")

        '--------------------------------------
        ' Create End Plane using thickness
        '--------------------------------------
        Dim endPlane As WorkPlane = partDef.WorkPlanes.AddByPlaneAndOffset(yzPlane, flangeThkCm)

        endPlane.Name = planeName
        endPlane.Visible = False

        partDoc.Update()

        Return endPlane

    End Function

    Private Function GetParameterSafe(pDef As PartComponentDefinition, paramName As String) As Parameter

        ' 1️⃣ Try User Parameters
        For Each p As Parameter In pDef.Parameters.UserParameters
            If p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase) Then
                Return p
            End If
        Next

        ' 2️⃣ Try Model Parameters
        For Each p As Parameter In pDef.Parameters.ModelParameters
            If p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase) Then
                Return p
            End If
        Next

        Debug.Print($"⚠ Parameter not found (skipped): {paramName}")
        Return Nothing
    End Function

    ''' Sets a parameter's value if it exists; silently skips (logging via
    ''' GetParameterSafe) if it doesn't, instead of throwing.
    Private Sub SetParamSafe(pDef As PartComponentDefinition, paramName As String, value As Double)
        Dim p As Parameter = GetParameterSafe(pDef, paramName)
        If p IsNot Nothing Then p.Value = value
    End Sub

    Private Sub AddNewRowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewRowToolStripMenuItem.Click
        ' Adds a new empty row to the DataGridView named DGV_Shell_Head_Tab
        ' The row will be added at the bottom of the grid
        DGV_Nozzle_Tab_Shell_HOR.Rows.Add()
    End Sub

    Private Sub DeleteNewRowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteNewRowToolStripMenuItem.Click

        ' Exit the procedure if no row is currently selected
        If DGV_Nozzle_Tab_Shell_HOR.CurrentRow Is Nothing Then Exit Sub

        ' Ask the user for confirmation before deleting the selected row
        If MessageBox.Show("Delete selected row?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        End If

        ' Remove the currently selected row from the DataGridView
        DGV_Nozzle_Tab_Shell_HOR.Rows.Remove(DGV_Nozzle_Tab_Shell_HOR.CurrentRow)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ' Adds a new empty row to the DataGridView named DGV_Shell_Head_Tab
        ' The row will be added at the bottom of the grid
        DGV_Nozzle_Tab_Head_HOR.Rows.Add()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        ' Exit the procedure if no row is currently selected
        If DGV_Nozzle_Tab_Head_HOR.CurrentRow Is Nothing Then Exit Sub

        ' Ask the user for confirmation before deleting the selected row
        If MessageBox.Show("Delete selected row?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        End If

        ' Remove the currently selected row from the DataGridView
        DGV_Nozzle_Tab_Head_HOR.Rows.Remove(DGV_Nozzle_Tab_Head_HOR.CurrentRow)
    End Sub

    Function GetFlangeThicknessCm(flangeOcc As ComponentOccurrence) As Double

        Dim partDoc As PartDocument = CType(flangeOcc.Definition.Document, PartDocument)

        Dim body As SurfaceBody = partDoc.ComponentDefinition.SurfaceBodies.Item(1)

        ' Assuming flange axis is along Y
        Dim minY As Double = body.RangeBox.MinPoint.X
        Dim maxY As Double = body.RangeBox.MaxPoint.X

        Return Math.Abs(maxY - minY)

    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        WeldingBlockManager.ImportAutoCADBlockDefinitionsFromFile(Me)
    End Sub

    Public Class WeldingBlockManager

        Public Shared Sub ImportAutoCADBlockDefinitionsFromFile(ByRef Pv As Form)

            Dim inv As Inventor.Application = Marshal.GetActiveObject("Inventor.Application")
            Dim oDrawDoc As DrawingDocument = inv.ActiveDocument

            ' Delete existing blocks
            For Each item As AutoCADBlock In oDrawDoc.ActiveSheet.AutoCADBlocks
                item.Delete()
            Next

            ' Load DWG
            Dim oAutoCAD_Blocks_Sheet_Folder As String = My.Computer.FileSystem.GetDirectoryInfo("..\..\Resources\").FullName
            Dim oAutoCAD_Blocks_Sheet As String = oAutoCAD_Blocks_Sheet_Folder & "Welding Notes.dwg"
            oDrawDoc.AutoCADBlockDefinitions.AddFromFile(oAutoCAD_Blocks_Sheet)

            ' Start position
            Dim currentX As Double = 2
            Dim yPos As Double = 9.15

            ' Insert blocks
            If CType(Pv.Controls.Find("ChckBx_WD_LONG_CIRC", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("1_LONG_CIRC_SEAM_WELD", currentX, yPos)
            End If

            If CType(Pv.Controls.Find("ChckBx_WD_PIPE_WELD", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("2_PIPE_TO_FLANGE_WELD", currentX, yPos)
            End If

            If CType(Pv.Controls.Find("ChckBx_WD_RFPAD_WELD", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("3_SHELL_HEAD_TOP_PLATE_WELD", currentX, yPos)
            End If

            If CType(Pv.Controls.Find("ChckBx_WD_INS_PROJ_WELD", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("4_TOP_COVER_PLATE_WELD", currentX, yPos)
            End If

            If CType(Pv.Controls.Find("ChckBx_WD_LL_WELD", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("5_LIFTING_LUG_WELD_DETAIL_", currentX, yPos)
            End If

            If CType(Pv.Controls.Find("ChckBx_STIFFENER_WELD", True)(0), System.Windows.Forms.CheckBox).Checked Then
                currentX = InsertAutoCADBlockOnSheet("6_NOZZLE_STIFFENER_WELD", currentX, yPos)
            End If

        End Sub


        Public Shared Function InsertAutoCADBlockOnSheet(blockName As String, currentX As Double, yPos As Double) As Double

            Dim inv As Inventor.Application = Marshal.GetActiveObject("Inventor.Application")
            Dim oTG As TransientGeometry = inv.TransientGeometry

            Dim oDrawDoc As DrawingDocument = inv.ActiveDocument
            Dim oSheet As Sheet = oDrawDoc.ActiveSheet

            '    ' Width dictionary (mm → cm)
            Dim blockWidths As New Dictionary(Of String, Double) From {
            {"1_LONG_CIRC_SEAM_WELD", 8.4487},
            {"2_PIPE_TO_FLANGE_WELD", 6.237},
            {"3_SHELL_HEAD_TOP_PLATE_WELD", 7.3434},
            {"4_TOP_COVER_PLATE_WELD", 7.3748},
            {"5_LIFTING_LUG_WELD_DETAIL_", 5.4202},
            {"6_NOZZLE_STIFFENER_WELD", 5.6088}
        }


            For Each oBlockDef As AutoCADBlockDefinition In oDrawDoc.AutoCADBlockDefinitions
                If oBlockDef.Name.ToLower().Contains(blockName.ToLower()) Then
                    oSheet.AutoCADBlocks.Add(oBlockDef, oTG.CreatePoint2d(currentX, yPos), 0, 35)
                    ' Move X for next block
                    currentX += blockWidths(blockName)
                    Exit For
                End If
            Next
            Return currentX

        End Function

    End Class

    Private Sub RB_OT_LL_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_LL.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = True
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = False

        Panel_LL_ADNOC.Left = 0
        Panel_LL_ADNOC.Top = 30

        Panel_LL_ADNOC.Size = New System.Drawing.Size(1600, 400)

    End Sub

    Private Sub RB_OT_NP_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_NP.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = True

        Panel_NP_ADNOC.Left = 0
        Panel_NP_ADNOC.Top = 30

        Panel_NP_ADNOC.Size = New System.Drawing.Size(1600, 400)
    End Sub

    Private Sub RB_OT_GLR_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_SR.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = True
        Panel_NP_ADNOC.Visible = False

        Panel_SR_ADNOC.Left = 0
        Panel_SR_ADNOC.Top = 30

        Panel_SR_ADNOC.Size = New System.Drawing.Size(1600, 500)
    End Sub

    Private Sub RB_OT_GR_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_GR.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = True
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = False

        Panel_GR_ADNOC.Left = 0
        Panel_GR_ADNOC.Top = 30

        Panel_GR_ADNOC.Size = New System.Drawing.Size(1600, 500)
    End Sub

    Private Sub RB_OT_EL_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_EL.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = True
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = False

        Panel_EB_ADNOC.Left = 0
        Panel_EB_ADNOC.Top = 30

        Panel_EB_ADNOC.Size = New System.Drawing.Size(1600, 500)
    End Sub

    Private Sub RB_OT_VB_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_VB.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = True
        TC_Dip_Pipe.Visible = False

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = False

        TC_Vortex_Breaker.Left = 0
        TC_Vortex_Breaker.Top = 30

        TC_Vortex_Breaker.Size = New System.Drawing.Size(1600, 500)
    End Sub

    Private Sub RB_OT_PS_CheckedChanged(sender As Object, e As EventArgs) Handles RB_OT_PS.CheckedChanged
        Panel_LL.Visible = False
        Panel_NP.Visible = False
        Panel_GLR.Visible = False
        Panel_EL.Visible = False
        TC_Vortex_Breaker.Visible = False
        TC_Dip_Pipe.Visible = True

        Panel_LL_ADNOC.Visible = False
        Panel_EB_ADNOC.Visible = False
        Panel_GR_ADNOC.Visible = False
        Panel_SR_ADNOC.Visible = False
        Panel_NP_ADNOC.Visible = False

        TC_Dip_Pipe.Left = 0
        TC_Dip_Pipe.Top = 30

        TC_Dip_Pipe.Size = New System.Drawing.Size(1600, 500)
    End Sub

    Private Sub TabControl4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl4.SelectedIndexChanged
        BuildFinalNozzleScheduleGrid()
    End Sub

    Public Sub BuildFinalNozzleScheduleGrid()

        '========================================================
        ' STEP 1 — Clear Existing Grid
        '========================================================
        DGV_NOZZLE_SCHEDULE_FINAL.Rows.Clear()
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Clear()

        '========================================================
        ' STEP 2 — Add Required Columns
        '========================================================
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("NozzleMark", "NOZZLE MARK")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Service", "SERVICE")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Qty", "QTY")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Size", "SIZE NPS")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Sch", "NOZZLE SCH")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Thk", "NOZZLE THK")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Class", "FLANGE CLASS")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("FlangeType", "FLANGE TYPE")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("RFPADOD", "RF PAD OD")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("RFPADTHK", "RF PAD THK")

        ' NEW Weld Columns
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("WeldA", "WELD SIZE A")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("WeldB", "WELD SIZE B")
        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("WeldC", "WELD SIZE C")

        DGV_NOZZLE_SCHEDULE_FINAL.Columns.Add("Remarks", "REMARKS")

        '========================================================
        ' STEP 3 — Collect Rows from Head + Shell
        '========================================================
        Dim nozzleRows As New List(Of String())

        CollectNozzleRowsForFinal(DGV_Nozzle_Tab_Shell_HOR, nozzleRows)

        '========================================================
        ' STEP 4 — Sort Correctly (N1 → N2 → N6A → M1)
        '========================================================
        nozzleRows = nozzleRows _
        .OrderBy(Function(r) GetNozzleSortKey(r(0))) _
        .ToList()

        '========================================================
        ' STEP 5 — Add Into Final Grid (Weld Columns Blank)
        '========================================================
        For Each noz As String() In nozzleRows

            DGV_NOZZLE_SCHEDULE_FINAL.Rows.Add(
            noz(0), noz(1), noz(2), noz(3), noz(4),
            noz(5), noz(6), noz(7), noz(8), noz(9),
            "", "", "", noz(10))  ' Weld A/B/C empty

        Next

        '========================================================
        ' Allow Only Weld Columns Editable
        '========================================================
        DGV_NOZZLE_SCHEDULE_FINAL.ReadOnly = False

        ' Lock all columns first
        For Each col As DataGridViewColumn In DGV_NOZZLE_SCHEDULE_FINAL.Columns
            col.ReadOnly = True
        Next

        ' Unlock Weld Columns Only
        DGV_NOZZLE_SCHEDULE_FINAL.Columns("WeldA").ReadOnly = False
        DGV_NOZZLE_SCHEDULE_FINAL.Columns("WeldB").ReadOnly = False
        DGV_NOZZLE_SCHEDULE_FINAL.Columns("WeldC").ReadOnly = False

    End Sub

    '==================================================
    ' NORMALIZE NPS BY MATCHING PIPE TABLE FORMAT
    ' Extracts numeric part and rebuilds using exact
    ' same quote characters as pipe table
    '==================================================
    Private Function NormalizeNPS_ByASCII(nps As String, pipeTable As List(Of PipeDim)) As String
        Try
            If String.IsNullOrWhiteSpace(nps) Then Return nps
            If pipeTable Is Nothing OrElse pipeTable.Count = 0 Then Return nps

            '==================================================
            ' GET QUOTE CHARACTER USED IN PIPE TABLE
            '==================================================
            Dim sampleND As String = pipeTable(0).ND
            Dim quoteChar As Char = Chr(39) ' default apostrophe

            ' Find the quote character in sample ND
            For Each c As Char In sampleND
                Dim code As Integer = AscW(c)
                If code = 39 OrElse code = 8216 OrElse code = 8217 OrElse
               code = 34 OrElse code = 8220 OrElse code = 8221 OrElse
               code = 8243 Then
                    quoteChar = c
                    Exit For
                End If
            Next

            Debug.Print($"PipeTable quote char: '{quoteChar}' ASCII={AscW(quoteChar)}")

            '==================================================
            ' EXTRACT NUMERIC PART FROM NPS
            ' e.g. "2'''" → "2"
            '==================================================
            Dim numericPart As String = ""
            For Each c As Char In nps
                Dim code As Integer = AscW(c)
                ' Keep digits, dots, slashes, spaces
                If Char.IsDigit(c) OrElse c = "."c OrElse c = "/"c OrElse c = " "c Then
                    numericPart &= c
                End If
            Next
            numericPart = numericPart.Trim()

            '==================================================
            ' COUNT QUOTES IN PIPE TABLE SAMPLE
            ' e.g. "2''" has 2 quotes
            '==================================================
            Dim quoteCount As Integer = 0
            For Each c As Char In sampleND
                If c = quoteChar Then quoteCount += 1
            Next

            '==================================================
            ' REBUILD NPS WITH CORRECT QUOTE CHAR & COUNT
            '==================================================
            Dim result As String = numericPart & New String(quoteChar, quoteCount)
            Debug.Print($"NPS normalized: '{nps}' → '{result}'")
            Return result

        Catch ex As Exception
            Debug.Print($"NormalizeNPS_ByASCII failed: {ex.Message}")
            Return nps
        End Try
    End Function

    Private Function GetNozzleSortKey(tag As String) As Tuple(Of Integer, Integer, String)

        tag = tag.ToUpper().Trim()

        Dim prefixRank As Integer = 99
        If tag.StartsWith("N") Then prefixRank = 1
        If tag.StartsWith("M") Then prefixRank = 2

        Dim numPart As String = ""
        Dim suffixPart As String = ""

        For i As Integer = 1 To tag.Length - 1
            Dim ch As Char = tag(i)

            If Char.IsDigit(ch) Then
                numPart &= ch
            Else
                suffixPart = tag.Substring(i)
                Exit For
            End If
        Next

        Dim nozzleNum As Integer = 0
        Integer.TryParse(numPart, nozzleNum)

        Return Tuple.Create(prefixRank, nozzleNum, suffixPart)

    End Function

    Private Sub CollectNozzleRowsForFinal(sourceGrid As DataGridView, nozzleRows As List(Of String()))

        For Each row As DataGridViewRow In sourceGrid.Rows

            If row.IsNewRow Then Continue For

            Dim tag As String = Convert.ToString(row.Cells(0).Value)
            If String.IsNullOrWhiteSpace(tag) Then Continue For

            nozzleRows.Add(New String() {
            Convert.ToString(row.Cells(0).Value),   ' MARK
            Convert.ToString(row.Cells(1).Value),   ' SERVICE
            Convert.ToString(row.Cells(2).Value),   ' QTY
            Convert.ToString(row.Cells(3).Value),   ' SIZE
            Convert.ToString(row.Cells(4).Value),   ' SCH
            Convert.ToString(row.Cells(5).Value),   ' THK
            NormalizeFlangeClass(Convert.ToString(row.Cells(6).Value)),  ' CLASS
            Convert.ToString(row.Cells(7).Value),   ' FLANGE TYPE
            Convert.ToString(row.Cells(9).Value),   ' RF PAD OD
            Convert.ToString(row.Cells(10).Value),  ' RF PAD THK
            Convert.ToString(row.Cells(11).Value)   ' REMARKS
        })

        Next

    End Sub

    Private Shared Function NormalizeFlangeClass(val As String) As String

        If String.IsNullOrWhiteSpace(val) Then Return ""

        val = val.Trim()

        ' Remove unwanted characters
        val = val.Replace("#", "")
        val = val.Replace("LB", "")
        val = val.Replace("LBS", "")
        val = val.Replace(" ", "")

        ' Always return as "150#"
        Return val & "#"

    End Function

    Private Function SelectInventorProjectFile() As String

        '--------------------------------------------
        ' 1️ Read EH Inside Diameter
        '--------------------------------------------
        Dim dia As Double

        If Not Double.TryParse(txt_EH_Inside_Dia.Text, dia) Then
            Return String.Empty
        End If

        '--------------------------------------------
        ' 2️ HARD-CODED ROOT (AS REQUIRED)
        '--------------------------------------------
        Dim rootPath As String = "D:\HORIZONTAL_TANK"

        If Not IO.Directory.Exists(rootPath) Then
            Return String.Empty
        End If

        '--------------------------------------------
        ' 3️ Choose project folder by CLIENT + diameter
        '--------------------------------------------
        Dim projectFolder As String = ""

        If SelectedClient = "ARAMCO" Then

            Select Case dia
                Case 900 To 1500
                    projectFolder = "CD.24.12_3D_Model - 1200"
                Case 1501 To 2200
                    projectFolder = "CD.24.12_3D_Model - 1900"
                Case 2201 To 3000
                    projectFolder = "CD.24.002.C02.001_2400"
                Case Else
                    Return String.Empty
            End Select

        ElseIf SelectedClient = "ADNOC" Then

            Select Case dia
                Case 900 To 1500
                    projectFolder = "MP.24.005_3D_Model - 1400"
                Case 1501 To 1800
                    projectFolder = "MP.24.005_3D_Model - 2000"
                Case 1801 To 3500
                    projectFolder = "ADNOC_HOR_VESSEL_2700"
                Case Else
                    Return String.Empty
            End Select

            'ElseIf SelectedClient = "QATAR" Then

            '    Select Case dia
            '        Case 900 To 1500
            '            projectFolder = "CQ.24.001_3D_Model - 1000"
            '        Case 1501 To 2200
            '            projectFolder = "CQ.24.001_3D_Model - 2000"
            '        Case 2201 To 3000
            '            projectFolder = "CQ.24.001_3D_Model - 2600"
            '        Case Else
            '            Return String.Empty
            '    End Select

        Else
            Return String.Empty
        End If

        '--------------------------------------------
        ' 4️ Build full folder path
        '--------------------------------------------
        Dim fullProjectPath As String = IO.Path.Combine(rootPath, projectFolder)
        If Not IO.Directory.Exists(fullProjectPath) Then
            Return String.Empty
        End If

        '--------------------------------------------
        ' 5️ Auto-pick the .ipj file
        '--------------------------------------------
        Dim ipjPath As String = IO.Directory.GetFiles(fullProjectPath, "*.ipj").FirstOrDefault()
        If IO.File.Exists(ipjPath) Then
            Return ipjPath
        End If

        Return String.Empty

    End Function

    Private Function AddAndActivateProject(invApp As Inventor.Application, ipjPath As String) As Inventor.DesignProject

        If invApp Is Nothing Then Return Nothing
        If String.IsNullOrWhiteSpace(ipjPath) Then Return Nothing
        If Not IO.File.Exists(ipjPath) Then Return Nothing

        Try
            Dim dpm As Inventor.DesignProjectManager = invApp.DesignProjectManager

            '==================================================
            ' 1) Check if project already added
            '==================================================
            For Each proj As Inventor.DesignProject In dpm.DesignProjects
                If String.Equals(proj.FullFileName, ipjPath, StringComparison.OrdinalIgnoreCase) Then
                    proj.Activate()
                    Return proj
                End If
            Next

            '==================================================
            ' 2) Add EXISTING project (.ipj)
            '==================================================
            Dim newProj As Inventor.DesignProject = dpm.DesignProjects.AddExisting(ipjPath)

            '==================================================
            ' 3) Activate
            '==================================================
            newProj.Activate()

            Return newProj

        Catch ex As Exception
            MessageBox.Show("❌ Failed to add / activate Inventor project:" & vbCrLf & ex.Message, "Inventor Project Error")
            Return Nothing
        End Try

    End Function

    Private Function GetVisibilityILogicRulePath() As String

        Dim rulePath As String = "D:\HORIZONTAL_TANK\Functions\Visibility.iLogicVb"
        If IO.File.Exists(rulePath) Then
            Return rulePath
        End If
        Return String.Empty

    End Function

    Private Function OpenMainAssemblyFromProject(invApp As Inventor.Application, project As Inventor.DesignProject, legType As String) As Inventor.AssemblyDocument

        '--------------------------------------------------
        ' 1️⃣ Validate inputs
        '--------------------------------------------------
        If invApp Is Nothing OrElse project Is Nothing Then
            Return Nothing
        End If

        '--------------------------------------------------
        ' 2️⃣ Get project folder path
        '--------------------------------------------------
        Dim folder As String = IO.Path.GetDirectoryName(project.FullFileName)

        '--------------------------------------------------
        ' 3️⃣ Select assembly name based on CLIENT + LEG TYPE
        '--------------------------------------------------
        Dim fileName As String = ""

        If SelectedClient = "ARAMCO" Then

            fileName = "ARAMCO\Main_Assembly.iam"

        ElseIf SelectedClient = "ADNOC" Then

            fileName = "Main_Assembly.iam"

        ElseIf SelectedClient = "QATAR" Then

        Else
            Return Nothing
        End If

        '--------------------------------------------------
        ' 4️⃣ Build full path
        '--------------------------------------------------
        Dim asmPath As String = IO.Path.Combine(folder, fileName)

        '--------------------------------------------------
        ' 5️⃣ Open assembly
        '--------------------------------------------------
        If Not IO.File.Exists(asmPath) Then
            Return Nothing
        End If

        Return CType(invApp.Documents.Open(asmPath, True), Inventor.AssemblyDocument)

    End Function

    Public Sub RunExternalILogicRule(invApp As Inventor.Application, doc As Inventor.Document, ruleFilePath As String)

        If invApp Is Nothing Then Exit Sub
        If doc Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(ruleFilePath) Then Exit Sub
        If Not IO.File.Exists(ruleFilePath) Then Exit Sub

        Try
            Dim iLogicAddIn = GetILogicAddIn(invApp)
            If iLogicAddIn Is Nothing Then Exit Sub

            Dim iLogicAuto = iLogicAddIn.Automation

            ' 🔥 RUN EXTERNAL RULE
            iLogicAuto.RunExternalRule(doc, ruleFilePath)

        Catch
            ' Never stop automation
        End Try

    End Sub

    Private Function GetILogicAddIn(invApp As Inventor.Application) As Inventor.ApplicationAddIn

        For Each addIn As Inventor.ApplicationAddIn In invApp.ApplicationAddIns
            If addIn.ClassIdString.Equals("{3BDD8D79-2179-4B11-8A5A-257B1C0263AC}", StringComparison.OrdinalIgnoreCase) Then
                Return addIn
            End If
        Next

        Return Nothing

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim invApp As Inventor.Application = CType(Marshal.GetActiveObject("Inventor.Application"), Inventor.Application)

        '=========================================
        ' 1️ Get Diameter
        '=========================================
        Dim dia As Integer = CInt(txt_EH_Inside_Dia.Text)

        '=========================================
        ' 2️ Decide Project Folder Based on Size
        '=========================================
        Dim projectFolder As String = ""

        If SelectedClient = "ARAMCO" Then

            If dia <= 1500 Then
                projectFolder = ""
            ElseIf dia <= 2200 Then
                projectFolder = ""
            Else
                projectFolder = "D:\HORIZONTAL_TANK\CD.24.002.C02.001_2400\ARAMCO"
            End If

        ElseIf SelectedClient = "ADNOC" Then

            If dia <= 1500 Then
                projectFolder = ""
            ElseIf dia <= 2200 Then
                projectFolder = ""
            Else
                projectFolder = "D:\HORIZONTAL_TANK\ADNOC_HOR_VESSEL_2700\ADNOC"
            End If

        ElseIf SelectedClient = "QATAR" Then

            If dia <= 1500 Then
                projectFolder = ""
            ElseIf dia <= 2200 Then
                projectFolder = ""
            Else
                projectFolder = ""
            End If

        End If

        '=========================================
        ' 3️ Decide Drawing File Based on Leg Type
        '=========================================
        Dim isBeam As Boolean = LegSupportState.CurrentSupportType.ToString().ToUpper() = "BEAM"
        Dim fileName As String
        fileName = "Main_Assembly_2D_Drawing.dwg"

        '=========================================
        ' 4️ Combine Full Path
        '=========================================
        Dim drawingPath As String = IO.Path.Combine(projectFolder, fileName)

        '=========================================
        ' 5️ Open Drawing
        '=========================================
        ' Open drawing
        Dim drawDoc As DrawingDocument = CType(invApp.Documents.Open(drawingPath, True), DrawingDocument)

        '=========================================
        ' 6️ UPDATE MODEL (KEY STEP)
        '=========================================
        drawDoc.Update()

        '👉 CALL CONSOLIDATION FUNCTION HERE
        'FixVisibleFlangeQty()

        '=========================================
        ' 7️ Fit All Sheets
        '=========================================
        For Each sh As Sheet In drawDoc.Sheets
            sh.Activate()
            invApp.ActiveView.Fit()
            invApp.ActiveView.Update()
        Next

        '=========================================
        ' 8️ Activate Sheet 1
        '=========================================
        drawDoc.Sheets.Item(2).Activate()

    End Sub

#End Region

#Region "LOAD FUNCTION"

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If IsLoadingInputs Then Exit Sub

        RB_LEFT_HEAD_EH.Checked = True
        RB_RIGHT_HEAD_EH.Checked = True

        PipeTable = LoadPipeTable()
        LoadDefaultShellNozzlesToDGV()
        'LoadDefaultHeadNozzlesToDGV()

        PB_Left_Saddle.Visible = True
        PB_Right_Saddle.Visible = False

        RB_OT_LL.Checked = True

        TabControl1.TabPages.Remove(TabPage18)
        TabControl4.TabPages.Remove(TabPage15)
        TabControl4.TabPages.Remove(TabPage16)

        SetupNotesDGV(DGV_Notes)
        LoadDefaultNotes(DGV_Notes)

        If SelectedClient = "ARAMCO" Then
            Panel_Shell_Aramco.Visible = True
            Panel_Shell_Adnoc.Visible = False
        ElseIf SelectedClient = "ADNOC" Then
            Panel_Shell_Aramco.Visible = False
            Panel_Shell_Adnoc.Visible = True
        End If

#Region "Left Head Data"
        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then
            txt_EH_Inside_Dia.Text = "2700"
            txt_EH_SF.Text = "50"
            txt_EH_Nom_Thk.Text = "8"
            txt_EH_Min_Thk.Text = "6"
            txt_Slope_Deg.Text = "0.286"
        End If
#End Region

#Region "Shell Data"
        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then
            cmb_Shell_Dia_Adnoc.Text = "ID"
            txt_Shell_Inside_Dia_Adnoc.Text = "2700"
            txt_Shell_Finished_Thk_Adnoc.Text = "8"
            txt_Shell_Nom_Thk_Adnoc.Text = "8"
            txt_Shell_Length1_Adnoc.Text = "1900"
            txt_Shell_Length2_Adnoc.Text = "2000"
            txt_Shell_Length3_Adnoc.Text = "2000"
            txt_Shell_Int_CA_Adnoc.Text = "0"
            txt_Shell_Ext_CA_Adnoc.Text = "0"
            cmb_Shell_Mat_Name_Adnoc.Text = "A-516 Gr.70 N"
            txt_Shell_Long_Eff_Adnoc.Text = "NIL"
            txt_Shell_Cir_Eff_Adnoc.Text = "NIL"
        End If
#End Region

#Region "Right Head Data"
        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then
            txt_EH_Inside_Dia1.Text = "2700"
            txt_EH_SF1.Text = "8"
            txt_EH_Nom_Thk1.Text = "8"
            txt_EH_Min_Thk1.Text = "6"
            txt_EH_Int_CA1.Text = "0"
            txt_EH_Ext_CA1.Text = "0"
            cmb_EH_Mat_Name1.Text = "A-516 Gr.70 N"
            txt_EH_Long_Eff1.Text = "NIL"
            txt_EH_Cir_Eff1.Text = "NIL"
        End If
#End Region

#Region "Left Saddle Data"
        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then
            txt_LS_Dist.Text = "750"
            txt_LS_ANGLE_1.Text = "120"
            txt_LS_ANGLE_2.Text = "132"
            txt_LS_WEARPLATE_THK.Text = "8"
            txt_LS_WEARPLATE_WIDTH.Text = "420"
            txt_LS_WEARPLATE_FILLET.Text = "50"
            txt_LS_BaseP_LENGTH.Text = "1760"
            txt_LS_BaseP_THK.Text = "25"
            txt_LS_BaseP_WIDTH.Text = "250"
            txt_LS_MIDDLE_RIB_WIDTH.Text = "150"
            txt_LS_PTFE_LENGTH.Text = "1730"
            txt_LS_PTFE_THK.Text = "3"
            txt_LS_PTFE_WIDTH.Text = "230"
            txt_LS_RIB_THK.Text = "16"
            txt_LS_LS_RIB_LENGTH_1.Text = "245"
            txt_LS_LS_RIB_LENGTH_2.Text = "450"
            txt_LS_LS_MID_TO_BASE.Text = "1635.5"
            txt_LS_MIDDLE_RIB_THK.Text = "16"
            txt_LS_BEARING_PLATE1_LENGTH.Text = "1780"
            txt_LS_BEARING_PLATE1_THK.Text = "3"
            txt_LS_BEARING_PLATE1_WIDTH.Text = "270"
            txt_LS_GAP.Text = "10"
            txt_LS_BEARING_PLATE2_LENGTH.Text = "1760"
            txt_LS_BEARING_PLATE2_THK.Text = "3"
            txt_LS_BEARING_PLATE2_WIDTH.Text = "280"
        End If
#End Region

#Region "Right Saddle Data"

        If SelectedClient = "ARAMCO" Then

        ElseIf SelectedClient = "ADNOC" Then
            txt_RS_Dist.Text = "750"
            txt_RS_ANGLE_1.Text = "120"
            txt_RS_ANGLE_2.Text = "132"
            txt_RS_WEARPLATE_THK.Text = "8"
            txt_RS_WEARPLATE_WIDTH.Text = "420"
            txt_RS_WEARPLATE_FILLET.Text = "50"
            txt_RS_BaseP_LENGTH.Text = "1760"
            txt_RS_BaseP_THK.Text = "25"
            txt_RS_BaseP_WIDTH.Text = "250"
            txt_RS_MIDDLE_RIB_WIDTH.Text = "150"
            txt_RS_RIB_THK.Text = "16"
            txt_RS_RIB_LENGTH_1.Text = "234"
            txt_RS_RIB_LENGTH_2.Text = "440"
            txt_RS_MID_TO_BASE.Text = "1625"
            txt_RS_MIDDLE_RIB_THK.Text = "16"
            txt_RS_GAP.Text = "10"
        End if

#End Region

    End Sub

    Private Sub Rbn_Angle_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Fixed.CheckedChanged
        PB_Left_Saddle.Visible = False
        PB_Right_Saddle.Visible = False

        Panel_Fixed_Saddle.Visible = False
        Panel_Sliding_Saddle.Visible = False

        Panel_Adnoc_LS.Visible = True
        Panel_Adnoc_RS.Visible = False

        PB_Adnoc_LS.Visible = True
        PB_Adnoc_RS.Visible = False

    End Sub

    Private Sub Rbn_Beam_CheckedChanged(sender As Object, e As EventArgs) Handles Rbn_Sliding.CheckedChanged
        PB_Left_Saddle.Visible = False
        PB_Right_Saddle.Visible = False

        Panel_Fixed_Saddle.Visible = False
        Panel_Sliding_Saddle.Visible = False

        Panel_Adnoc_LS.Visible = False
        Panel_Adnoc_RS.Visible = True

        PB_Adnoc_LS.Visible = False
        PB_Adnoc_RS.Visible = True

    End Sub

    Private isLoadingNozzles As Boolean = False
    Private PipeTable As List(Of PipeDim)

    Public Sub LoadDefaultHeadNozzlesToDGV()

        isLoadingNozzles = True
        DGV_Nozzle_Tab_Head_HOR.Rows.Clear()

        Dim defaultHeadNozzles As Object(,)

        '=========================================================
        ' SELECT DATASET BASED ON CLIENT
        '=========================================================
        If SelectedClient = "ARAMCO" Then

            defaultHeadNozzles = New Object(,) {
            {"N1", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, 250, 250}
        }

            '=====================================================
            ' LOAD INTO GRID
            '=====================================================
            For i As Integer = 0 To defaultHeadNozzles.GetLength(0) - 1

                Dim rowIndex As Integer = DGV_Nozzle_Tab_Head_HOR.Rows.Add()
                Dim row As DataGridViewRow = DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex)

                Dim nozzleMark As String = defaultHeadNozzles(i, 0).ToString()
                Dim nps As String = defaultHeadNozzles(i, 3).ToString()
                Dim sch As String = defaultHeadNozzles(i, 4).ToString().Trim()

                '=================================================
                ' BASIC VALUES
                '=================================================
                row.Cells("NOZZLE_MARK_TEXT_HEAD_HOR").Value = nozzleMark
                row.Cells("NOZZLE_SERVICE_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 1)
                row.Cells("NOZZLE_QTY_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 2)
                row.Cells("SIZE_TEXT_HEAD_HOR").Value = nps

                '=================================================
                ' PIPE NOZZLE CASE
                '=================================================
                If sch <> "-" Then
                    FillSchforSize_Head(rowIndex, nps)
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = sch
                    FillThicknessHead(rowIndex, nps, sch)
                Else
                    ' MANWAY CASE
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = "-"
                    With row.Cells("NOZZLE_THK_HEAD_HOR")
                        .ReadOnly = False
                        .Style.BackColor = System.Drawing.Color.White
                    End With
                End If

                '=================================================
                ' OTHER VALUES
                '=================================================
                row.Cells("NOZZLE_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 5)
                row.Cells("NOZZLE_FLANGE_CLASS_HEAD_HOR").Value = defaultHeadNozzles(i, 6)
                row.Cells("NOZZLE_FLANGE_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 7)
                row.Cells("OUTSIDE_PROJECTION_HEAD_HOR").Value = defaultHeadNozzles(i, 8)
                row.Cells("RF_PAD_OD_HEAD_HOR").Value = defaultHeadNozzles(i, 9)
                row.Cells("RF_PAD_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 10)
                row.Cells("REMARKS_HEAD_HOR").Value = defaultHeadNozzles(i, 11)

                '=================================================
                ' NOZZLE LOCATION (LEFT / RIGHT)
                '=================================================
                row.Cells("NOZZLE_LOCATION_HOR").Value = defaultHeadNozzles(i, 12)

                '=================================================
                ' PLACEMENT TYPE (RADIAL / XY DIRECTION)
                '=================================================
                row.Cells("PLACEMENT_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 13)

                '=================================================
                ' RADIUS / ANGLE / X / Y
                '=================================================
                row.Cells("RADIUS_HEAD_HOR").Value = defaultHeadNozzles(i, 14)
                row.Cells("ANGLE_HEAD_HOR").Value = defaultHeadNozzles(i, 15)
                row.Cells("X_HEAD_HOR").Value = defaultHeadNozzles(i, 16)
                row.Cells("Y_HEAD_HOR").Value = defaultHeadNozzles(i, 17)

            Next

            isLoadingNozzles = False

        ElseIf SelectedClient = "ADNOC" Then

            defaultHeadNozzles = New Object(,) {
            {"N1", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, 250, 250},
            {"N2", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, -250, 250},
            {"N3", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, -250, -250},
            {"N4", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, 250, -250},
            {"N5", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "RIGHT", "XY DIRECTION", "-", 0, 250, 250},
            {"N6", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "RIGHT", "XY DIRECTION", "-", 0, -250, 250},
            {"N7", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "RIGHT", "XY DIRECTION", "-", 0, -250, -250},
            {"N8", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "RIGHT", "XY DIRECTION", "-", 0, 250, -250}
        }   ' 🔧 Replace with actual ADNOC head nozzle defaults

            For i As Integer = 0 To defaultHeadNozzles.GetLength(0) - 1

                Dim rowIndex As Integer = DGV_Nozzle_Tab_Head_HOR.Rows.Add()
                Dim row As DataGridViewRow = DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex)

                Dim nozzleMark As String = defaultHeadNozzles(i, 0).ToString()
                Dim nps As String = defaultHeadNozzles(i, 3).ToString()
                Dim sch As String = defaultHeadNozzles(i, 4).ToString().Trim()

                row.Cells("NOZZLE_MARK_TEXT_HEAD_HOR").Value = nozzleMark
                row.Cells("NOZZLE_SERVICE_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 1)
                row.Cells("NOZZLE_QTY_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 2)
                row.Cells("SIZE_TEXT_HEAD_HOR").Value = nps

                If sch <> "-" Then
                    FillSchforSize_Head(rowIndex, nps)
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = sch
                    FillThicknessHead(rowIndex, nps, sch)
                Else
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = "-"
                    With row.Cells("NOZZLE_THK_HEAD_HOR")
                        .ReadOnly = False
                        .Style.BackColor = System.Drawing.Color.White
                    End With
                End If

                row.Cells("NOZZLE_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 5)
                row.Cells("NOZZLE_FLANGE_CLASS_HEAD_HOR").Value = defaultHeadNozzles(i, 6)
                row.Cells("NOZZLE_FLANGE_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 7)
                row.Cells("OUTSIDE_PROJECTION_HEAD_HOR").Value = defaultHeadNozzles(i, 8)
                row.Cells("RF_PAD_OD_HEAD_HOR").Value = defaultHeadNozzles(i, 9)
                row.Cells("RF_PAD_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 10)
                row.Cells("REMARKS_HEAD_HOR").Value = defaultHeadNozzles(i, 11)
                row.Cells("NOZZLE_LOCATION_HOR").Value = defaultHeadNozzles(i, 12)
                row.Cells("PLACEMENT_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 13)
                row.Cells("RADIUS_HEAD_HOR").Value = defaultHeadNozzles(i, 14)
                row.Cells("ANGLE_HEAD_HOR").Value = defaultHeadNozzles(i, 15)
                row.Cells("X_HEAD_HOR").Value = defaultHeadNozzles(i, 16)
                row.Cells("Y_HEAD_HOR").Value = defaultHeadNozzles(i, 17)

            Next

            isLoadingNozzles = False

        ElseIf SelectedClient = "QATAR" Then

            defaultHeadNozzles = New Object(,) {
            {"N1", "TEST", 1, "2''", "40S", 3.91, "150#", "WNRF", 1000, 160, 8, "NIL", "LEFT", "XY DIRECTION", "-", 0, 250, 250}
        }   ' 🔧 Replace with actual QATAR head nozzle defaults

            For i As Integer = 0 To defaultHeadNozzles.GetLength(0) - 1

                Dim rowIndex As Integer = DGV_Nozzle_Tab_Head_HOR.Rows.Add()
                Dim row As DataGridViewRow = DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex)

                Dim nozzleMark As String = defaultHeadNozzles(i, 0).ToString()
                Dim nps As String = defaultHeadNozzles(i, 3).ToString()
                Dim sch As String = defaultHeadNozzles(i, 4).ToString().Trim()

                row.Cells("NOZZLE_MARK_TEXT_HEAD_HOR").Value = nozzleMark
                row.Cells("NOZZLE_SERVICE_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 1)
                row.Cells("NOZZLE_QTY_TEXT_HEAD_HOR").Value = defaultHeadNozzles(i, 2)
                row.Cells("SIZE_TEXT_HEAD_HOR").Value = nps

                If sch <> "-" Then
                    FillSchforSize_Head(rowIndex, nps)
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = sch
                    FillThicknessHead(rowIndex, nps, sch)
                Else
                    row.Cells("NOZZLE_SCH_HEAD_HOR").Value = "-"
                    With row.Cells("NOZZLE_THK_HEAD_HOR")
                        .ReadOnly = False
                        .Style.BackColor = System.Drawing.Color.White
                    End With
                End If

                row.Cells("NOZZLE_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 5)
                row.Cells("NOZZLE_FLANGE_CLASS_HEAD_HOR").Value = defaultHeadNozzles(i, 6)
                row.Cells("NOZZLE_FLANGE_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 7)
                row.Cells("OUTSIDE_PROJECTION_HEAD_HOR").Value = defaultHeadNozzles(i, 8)
                row.Cells("RF_PAD_OD_HEAD_HOR").Value = defaultHeadNozzles(i, 9)
                row.Cells("RF_PAD_THK_HEAD_HOR").Value = defaultHeadNozzles(i, 10)
                row.Cells("REMARKS_HEAD_HOR").Value = defaultHeadNozzles(i, 11)
                row.Cells("NOZZLE_LOCATION_HOR").Value = defaultHeadNozzles(i, 12)
                row.Cells("PLACEMENT_TYPE_HEAD_HOR").Value = defaultHeadNozzles(i, 13)
                row.Cells("RADIUS_HEAD_HOR").Value = defaultHeadNozzles(i, 14)
                row.Cells("ANGLE_HEAD_HOR").Value = defaultHeadNozzles(i, 15)
                row.Cells("X_HEAD_HOR").Value = defaultHeadNozzles(i, 16)
                row.Cells("Y_HEAD_HOR").Value = defaultHeadNozzles(i, 17)

            Next

            isLoadingNozzles = False

        End If

    End Sub

    '==================================================
    ' FILL SCHEDULE FOR HEAD NOZZLE
    '==================================================
    Private Sub FillSchforSize_Head(rowIndex As Integer, nps As String)
        If PipeTable Is Nothing OrElse PipeTable.Count = 0 Then Exit Sub
        If String.IsNullOrWhiteSpace(nps) Then Exit Sub

        Try
            Dim schList = PipeTable.Where(Function(p) p.ND = nps) _
                               .Select(Function(p) p.SCH) _
                               .Distinct().ToList()

            If schList.Count = 0 Then Exit Sub

            Dim schCell As DataGridViewComboBoxCell = CType(
            DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex).Cells("NOZZLE_SCH_HEAD_HOR"),
            DataGridViewComboBoxCell)

            schCell.DataSource = schList
            DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex).Cells("NOZZLE_SCH_HEAD_HOR").Value = Nothing

        Catch ex As Exception
            Debug.Print($"FillSchforSize_Head failed: {ex.Message}")
        End Try
    End Sub

    '==================================================
    ' FILL THICKNESS FOR HEAD NOZZLE
    '==================================================
    Private Sub FillThicknessHead(rowIndex As Integer, nps As String, sch As String)
        If String.IsNullOrWhiteSpace(sch) Then Exit Sub
        sch = sch.Trim().ToUpper()
        If rowIndex < 0 OrElse rowIndex >= DGV_Nozzle_Tab_Head_HOR.Rows.Count Then Exit Sub
        If String.IsNullOrWhiteSpace(nps) Then Exit Sub

        Try
            Dim pipeTable As List(Of PipeDim) = LoadPipeTable()
            Dim pipe As PipeDim = GetPipeDim_BySizeAndSchedule(pipeTable, nps, sch)
            If pipe Is Nothing Then Exit Sub

            Dim row As DataGridViewRow = DGV_Nozzle_Tab_Head_HOR.Rows(rowIndex)
            row.Cells("NOZZLE_THK_HEAD_HOR").Value = pipe.THK.ToString("0.00")

            With row.Cells("NOZZLE_THK_HEAD_HOR")
                .ReadOnly = True
                .Style.BackColor = System.Drawing.Color.LightGray
            End With

        Catch ex As Exception
            MessageBox.Show("❌ Error in FillThicknessHead: " & ex.Message)
        End Try
    End Sub

    Public Sub LoadDefaultShellNozzlesToDGV()

        isLoadingNozzles = True
        DGV_Nozzle_Tab_Shell_HOR.Rows.Clear()

        Dim defaultShellNozzles As Object(,)

        '=========================================================
        ' SELECT DATASET BASED ON CLIENT
        '=========================================================
        If SelectedClient = "ARAMCO" Then

            defaultShellNozzles = New Object(,) {
        {"N1", "CHEMICAL INLET", 1, "2''", "160", 8.74, "300#", "WNRF", 1550, 180, 12, "NIL", 3355, -350, 0},
        {"N2", "CHEMICAL OUTLET", 1, "2''", "160", 8.74, "300#", "WNRF", 1573, 180, 12, "NIL", 3765, 200, 180},
        {"N3", "DRUM PZV", 1, "6''", "80S", 10.97, "150#", "WNRF", 1550, 290, 12, "NIL", 3355, 400, 0},
        {"N4", "DRUM DRAIN", 1, "2''", "160", 8.74, "300#", "WNRF", 1611, 180, 12, "NIL", 3765, 0, 180},
        {"N5A", "LT (GWR)", 1, "4''", "120", 11.13, "300#", "WNRF", 1550, 235, 12, "NIL", 2955, 400, 0},
        {"N5B", "LT (GWR)", 1, "4''", "120", 11.13, "300#", "WNRF", 1550, 235, 12, "NIL", 2455, 400, 0},
        {"N6A", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1320, 180, 12, "NIL", 2895, 966, 270},
        {"N6B", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1320, 180, 12, "NIL", 2895, -974, 270},
        {"N7", "PZV RETURN", 1, "2''", "160", 8.74, "300#", "WNRF", 1550, 180, 12, "NIL", 1455, 400, 0},
        {"N8", "CALIBRATION POT RETURN", 1, "2''", "160", 8.74, "300#", "WNRF", 1550, 180, 12, "NIL", 3755, 400, 0},
        {"N9", "UTILITY CONNECTION", 1, "2''", "160", 8.74, "300#", "WNRF", 1320, 180, 12, "NIL", 3755, -979, 270},
        {"N10", "N2 INLET", 1, "2''", "160", 8.74, "300#", "WNRF", 1550, 180, 12, "NIL", 2955, -400, 0},
        {"N11", "VENT", 1, "6''", "80S", 10.97, "150#", "WNRF", 1550, 290, 12, "NIL", 3705, 0, 0},
        {"M1", "MANWAY", 1, "24''", "-", 12, "150#", "WNRF", 1750, 900, 12, "NIL", 1050, 0, 270}
        }

            For i As Integer = 0 To defaultShellNozzles.GetLength(0) - 1

                Dim rowIndex As Integer = DGV_Nozzle_Tab_Shell_HOR.Rows.Add()
                Dim row As DataGridViewRow = DGV_Nozzle_Tab_Shell_HOR.Rows(rowIndex)

                Dim nozzleMark As String = defaultShellNozzles(i, 0).ToString()
                Dim nps As String = defaultShellNozzles(i, 3).ToString()
                Dim sch As String = defaultShellNozzles(i, 4).ToString().Trim()

                '=================================================
                ' BASIC VALUES
                '=================================================
                row.Cells("NOZZLE_MARK_TEXT_HOR").Value = nozzleMark
                row.Cells("NOZZLE_SERVICE_TEXT_HOR").Value = defaultShellNozzles(i, 1)
                row.Cells("NOZZLE_QTY_TEXT_HOR").Value = defaultShellNozzles(i, 2)
                row.Cells("SIZE_TEXT_HOR").Value = nps

                '=================================================
                ' PIPE NOZZLE CASE
                '=================================================
                If sch <> "-" Then
                    FillSchforSize_Shell(rowIndex, nps)
                    row.Cells("NOZZLE_SCH_SHELL_HOR").Value = sch
                    FillThicknessShell(rowIndex, nps, sch)
                Else
                    ' MANWAY CASE
                    row.Cells("NOZZLE_SCH_SHELL_HOR").Value = "-"
                    With row.Cells("NOZZLE_THK_SHELL_HOR")
                        .ReadOnly = False
                        .Style.BackColor = System.Drawing.Color.White
                    End With
                End If

                '=================================================
                ' OTHER VALUES
                '=================================================
                row.Cells("NOZZLE_THK_SHELL_HOR").Value = defaultShellNozzles(i, 5)
                row.Cells("NOZZLE_FLANGE_CLASS_SHELL_HOR").Value = defaultShellNozzles(i, 6)
                row.Cells("NOZZLE_FLANGE_TYPE_SHELL_HOR").Value = defaultShellNozzles(i, 7)
                row.Cells("OUTSIDE_PROJECTION_SHELL_HOR").Value = defaultShellNozzles(i, 8)
                row.Cells("RF_PAD_OD_1_HOR").Value = defaultShellNozzles(i, 9)
                row.Cells("RF_PAD_THK_1_HOR").Value = defaultShellNozzles(i, 10)
                row.Cells("REMARKS_SHELL_HOR").Value = defaultShellNozzles(i, 11)

                ' ✅ DISTANCE  → index 12
                row.Cells("DISTANCE_HOR").Value = defaultShellNozzles(i, 12)

                ' ✅ OFFSET_DIST → index 13
                row.Cells("OFFSET_DIST_HOR").Value = defaultShellNozzles(i, 13)

                ' ✅ ANGLE → index 14
                row.Cells("ANGLE_HOR").Value = defaultShellNozzles(i, 14)

            Next

            isLoadingNozzles = False

        ElseIf SelectedClient = "ADNOC" Then

            defaultShellNozzles = New Object(,) {
        {"N1", "CHEMICAL INLET", 1, "3''", "80S", 7.62, "150#", "WNRF", 1732, 210, 8, "NIL", 2230, 0, 0},
        {"N2", "CHEMICAL OUTLET", 1, "2''", "160", 8.74, "150#", "WNRF", 1604, "-", "-", "NIL", 5760, -300, 180},
        {"N3", "PSV RETURN", 1, "2''", "160", 8.74, "150#", "WNRF", 1730, "-", "-", "NIL", 3730, -500, 0},
        {"N4", "UTILITY CONNECTIONS", 1, "2''", "160", 8.74, "150#", "WNRF", 1732, "-", "-", "NIL", 2130, 500, 0},
        {"N5A", "VENT WITH FLAME ARRESTOR", 1, "2''", "160", 8.74, "150#", "WNRF", 1732, "-", "-", "NIL", 2530, 0, 0},
        {"N5B", "VENT WITH FLAME ARRESTOR", 1, "2''", "160", 8.74, "150#", "WNRF", 1732, "-", "-", "NIL", 2930, 0, 0},
        {"N6", "TANK DRAIN", 1, "2''", "160", 8.74, "150#", "WNRF", 1604, "-", "-", "NIL", 5760, 0, 180},
        {"N7", "CALIBRATION POT RETURN", 1, "2''", "160", 8.74, "150#", "WNRF", 1732, "-", "-", "NIL", 3330, -500, 0},
        {"N8", "OVERFLOW", 1, "2''", "160", 8.74, "150#", "WNRF", 1000, "-", "-", "NIL", 1750, 1201, 270},
        {"N9", "LEVEL TRANSMITTER  (LT-6201- 02,04)", 1, "3''", "80S", 7.62, "300#", "WNRF", 1125, 210, 8, "NIL", 4250, -1231, 90},
        {"N10", "LEVEL TRANSMITTER  (LT-6201- 01,03)", 1, "3''", "80S", 7.62, "300#", "WNRF", 1125, 210, 8, "NIL", 2450, -1222, 90},
        {"J1A", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1125, "-", "-", "NIL", 2450, 1138, 90},
        {"J1B", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1608, "-", "-", "NIL", 2450, 112, 90},
        {"J2A", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1608, "-", "-", "NIL", 3556, 22, 90},
        {"J2B", "LEVEL GAUGE", 1, "2''", "160", 8.74, "300#", "WNRF", 1225, "-", "-", "NIL", 3556, -1228, 90},
        {"M1", "MANWAY", 1, "24''", "-", 8, "150#", "WNRF", 1758, 900, 8, "NIL", 1250, 0, 90},
        {"M2", "MANWAY", 1, "24''", "-", 8, "150#", "WNRF", 1732, 900, 8, "NIL", 5350, 0, 0}}

            For i As Integer = 0 To defaultShellNozzles.GetLength(0) - 1

                Dim rowIndex As Integer = DGV_Nozzle_Tab_Shell_HOR.Rows.Add()
                Dim row As DataGridViewRow = DGV_Nozzle_Tab_Shell_HOR.Rows(rowIndex)

                Dim nozzleMark As String = defaultShellNozzles(i, 0).ToString()
                Dim nps As String = defaultShellNozzles(i, 3).ToString()
                Dim sch As String = defaultShellNozzles(i, 4).ToString().Trim()

                '=================================================
                ' BASIC VALUES
                '=================================================
                row.Cells("NOZZLE_MARK_TEXT_HOR").Value = nozzleMark
                row.Cells("NOZZLE_SERVICE_TEXT_HOR").Value = defaultShellNozzles(i, 1)
                row.Cells("NOZZLE_QTY_TEXT_HOR").Value = defaultShellNozzles(i, 2)
                row.Cells("SIZE_TEXT_HOR").Value = nps

                '=================================================
                ' PIPE NOZZLE CASE
                '=================================================
                If sch <> "-" Then
                    FillSchforSize_Shell(rowIndex, nps)
                    row.Cells("NOZZLE_SCH_SHELL_HOR").Value = sch
                    FillThicknessShell(rowIndex, nps, sch)
                Else
                    ' MANWAY CASE
                    row.Cells("NOZZLE_SCH_SHELL_HOR").Value = "-"
                    With row.Cells("NOZZLE_THK_SHELL_HOR")
                        .ReadOnly = False
                        .Style.BackColor = System.Drawing.Color.White
                    End With
                End If

                '=================================================
                ' OTHER VALUES
                '=================================================
                row.Cells("NOZZLE_THK_SHELL_HOR").Value = defaultShellNozzles(i, 5)
                row.Cells("NOZZLE_FLANGE_CLASS_SHELL_HOR").Value = defaultShellNozzles(i, 6)
                row.Cells("NOZZLE_FLANGE_TYPE_SHELL_HOR").Value = defaultShellNozzles(i, 7)
                row.Cells("OUTSIDE_PROJECTION_SHELL_HOR").Value = defaultShellNozzles(i, 8)
                row.Cells("RF_PAD_OD_1_HOR").Value = defaultShellNozzles(i, 9)
                row.Cells("RF_PAD_THK_1_HOR").Value = defaultShellNozzles(i, 10)
                row.Cells("REMARKS_SHELL_HOR").Value = defaultShellNozzles(i, 11)

                ' ✅ DISTANCE  → index 12
                row.Cells("DISTANCE_HOR").Value = defaultShellNozzles(i, 12)

                ' ✅ OFFSET_DIST → index 13
                row.Cells("OFFSET_DIST_HOR").Value = defaultShellNozzles(i, 13)

                ' ✅ ANGLE → index 14
                row.Cells("ANGLE_HOR").Value = defaultShellNozzles(i, 14)

            Next

            isLoadingNozzles = False

        End If

    End Sub

    Private Sub FillSchforSize_Shell(rowIndex As Integer, nps As String)

        ' Get all schedules for selected size
        Dim schList = PipeTable.Where(Function(p) p.ND = nps).Select(Function(p) p.SCH).Distinct().ToList()

        ' Assign schedules to combo cell
        Dim schCell As DataGridViewComboBoxCell = CType(DGV_Nozzle_Tab_Shell_HOR.Rows(rowIndex).Cells("NOZZLE_SCH_SHELL_HOR"), DataGridViewComboBoxCell)

        schCell.DataSource = schList

        ' Clear previous schedule selection
        DGV_Nozzle_Tab_Shell_HOR.Rows(rowIndex).Cells("NOZZLE_SCH_SHELL_HOR").Value = Nothing

        ' Clear thickness also
        'DGV_Nozzle_Tab_Shell.Rows(rowIndex).Cells("NOZZLE_THK_SHELL").Value = ""

    End Sub

    Private Sub FillThicknessShell(rowIndex As Integer, nps As String, sch As String)

        '==================================================
        ' ✅ Skip Manway or Blank Schedule
        '==================================================
        If String.IsNullOrWhiteSpace(sch) Then Exit Sub

        sch = sch.Trim().ToUpper()

        'If sch = "-" Then Exit Sub     '✅ Manway → Do not calculate

        '==================================================
        ' Safety Checks
        '==================================================
        If rowIndex < 0 OrElse rowIndex >= DGV_Nozzle_Tab_Shell_HOR.Rows.Count Then Exit Sub
        If String.IsNullOrWhiteSpace(nps) Then Exit Sub

        Try
            '==================================================
            ' Load Pipe Table
            '==================================================
            Dim pipeTable As List(Of PipeDim) = LoadPipeTable()

            '==================================================
            ' Find Matching Pipe
            '==================================================
            Dim pipe As PipeDim = GetPipeDim_BySizeAndSchedule(pipeTable, nps, sch)

            If pipe Is Nothing Then
                Exit Sub
            End If

            Dim thkValue As Double = pipe.THK

            '==================================================
            ' Set Thickness in DGV
            '==================================================
            Dim row As DataGridViewRow = DGV_Nozzle_Tab_Shell_HOR.Rows(rowIndex)

            row.Cells("NOZZLE_THK_SHELL_HOR").Value = thkValue.ToString("0.00")

            '==================================================
            ' Lock Thickness Cell
            '==================================================
            With row.Cells("NOZZLE_THK_SHELL_HOR")
                .ReadOnly = True
                .Style.BackColor = System.Drawing.Color.LightGray
            End With

        Catch ex As Exception
            MessageBox.Show("❌ Error in FillThicknessShell: " & ex.Message)
        End Try

    End Sub

    Private Sub DGV_Nozzle_Tab_Shell_HOR_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Nozzle_Tab_Shell_HOR.CellValueChanged

        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGV_Nozzle_Tab_Shell_HOR.Rows(e.RowIndex)
        Dim colName As String = DGV_Nozzle_Tab_Shell_HOR.Columns(e.ColumnIndex).Name

        '==================================================
        ' ✅ 1) SIZE CHANGED → FILTER SCHEDULES
        '==================================================
        If colName = "SIZE_TEXT_HOR" Then

            Dim sizeVal = row.Cells("SIZE_TEXT_HOR").Value
            If sizeVal Is Nothing Then Exit Sub

            Dim nps As String = sizeVal.ToString().Trim()

            ' Fill schedules for that size
            FillSchforSize_Shell(e.RowIndex, nps)

            ' Clear schedule + thickness
            RemoveHandler DGV_Nozzle_Tab_Shell_HOR.CellValueChanged, AddressOf DGV_Nozzle_Tab_Shell_HOR_CellValueChanged

            If Not isLoadingNozzles Then
                row.Cells("NOZZLE_SCH_SHELL_HOR").Value = Nothing
                'row.Cells("NOZZLE_THK_SHELL").Value = ""
            End If

            AddHandler DGV_Nozzle_Tab_Shell_HOR.CellValueChanged, AddressOf DGV_Nozzle_Tab_Shell_HOR_CellValueChanged

            Exit Sub
        End If

        '==================================================
        ' ✅ 2) SCHEDULE CHANGED → AUTO THICKNESS
        '==================================================
        If colName = "NOZZLE_SCH_SHELL_HOR" Then

            Dim sizeVal = row.Cells("SIZE_TEXT_HOR").Value
            Dim schVal = row.Cells("NOZZLE_SCH_SHELL_HOR").Value

            If sizeVal Is Nothing OrElse schVal Is Nothing Then Exit Sub

            Dim nps As String = sizeVal.ToString().Trim()
            Dim sch As String = schVal.ToString().Trim()

            ' Fill thickness
            FillThicknessShell(e.RowIndex, nps, sch)

            Exit Sub
        End If

    End Sub

    Public Sub LoadNozzleMarksToMaterialGrid()

        '----------------------------------------
        ' Clear existing rows
        '----------------------------------------
        DGV_Nozzle_Tab_Mat_HOR.Rows.Clear()

        '----------------------------------------
        ' Default Materials
        '----------------------------------------
        Dim PIPE_MAT As String = "A 312 TP316L"
        Dim FLANGE_MAT As String = "SA-182 Gr.F316L"
        Dim RF_PAD_MAT As String = "A 240 Gr.316L"

        '----------------------------------------
        ' HashSet to avoid duplicates
        '----------------------------------------
        Dim added As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        '----------------------------------------
        ' Function to process any grid
        '----------------------------------------
        Dim AddFromGrid As Action(Of DataGridView) = Sub(dgv As DataGridView)

                                                         For Each row As DataGridViewRow In dgv.Rows

                                                             If row.IsNewRow Then Continue For

                                                             Dim nozMark As String = ""

                                                             ' Try column by name
                                                             If dgv.Columns.Contains("Nozzle Mark") Then
                                                                 nozMark = Convert.ToString(row.Cells("Nozzle Mark").Value)
                                                             Else
                                                                 nozMark = Convert.ToString(row.Cells(0).Value)
                                                             End If

                                                             If String.IsNullOrWhiteSpace(nozMark) Then Continue For

                                                             ' Avoid duplicates
                                                             If Not added.Contains(nozMark) Then

                                                                 '----------------------------------------
                                                                 ' Add with default materials
                                                                 '----------------------------------------
                                                                 DGV_Nozzle_Tab_Mat_HOR.Rows.Add(
                    nozMark,
                    PIPE_MAT,
                    FLANGE_MAT,
                    RF_PAD_MAT
                )

                                                                 added.Add(nozMark)

                                                             End If

                                                         Next

                                                     End Sub

        '----------------------------------------
        ' Load from both grids
        '----------------------------------------
        AddFromGrid(DGV_Nozzle_Tab_Head_HOR)
        AddFromGrid(DGV_Nozzle_Tab_Shell_HOR)

    End Sub

    Public Sub SetupNotesDGV(dgv As DataGridView)

        With dgv
            .Columns.Clear()
            .Rows.Clear()

            .Columns.Add("COL_SEQ", "SEQ")
            .Columns.Add("COL_NOTE", "NOTE")

            .Columns("COL_SEQ").Width = 50
            .Columns("COL_NOTE").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            .Columns("COL_NOTE").DefaultCellStyle.WrapMode = DataGridViewTriState.True

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .ReadOnly = False

            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        LoadNozzleMarksToMaterialGrid()
    End Sub

    Private Sub TabControl5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl5.SelectedIndexChanged
        '==================================================
        ' 1️ Decide which diameter textbox to use
        '==================================================
        Dim insideDia As Double

        If RB_LEFT_HEAD_EH.Checked Then
            If Not Double.TryParse(txt_EH_Inside_Dia.Text.Trim(), insideDia) Then Exit Sub

        ElseIf RB_LEFT_HEAD_TH.Checked Then
            If Not Double.TryParse(txt_EH_Inside_Dia.Text.Trim(), insideDia) Then Exit Sub
        Else
            Exit Sub
        End If

        '==================================================
        ' 2️ Update only Diameter fields across tabs
        '==================================================
        txt_Shell_Inside_Dia_Adnoc.Text = insideDia.ToString("0")
        txt_EH_Inside_Dia1.Text = insideDia.ToString("0")

    End Sub

    '=========================================
    ' ADD A NEW BUTTON: "Load Project Inputs"
    ' Name it: Btn_Load_Inputs
    '=========================================
    Private Sub Btn_Load_Inputs_Click(sender As Object, e As EventArgs) Handles Btn_Load_Inputs.Click
        '✅ Pass CurrentProjectFolder from caller
        Dim chosenFolder As String = BrowseAndLoadProjectInputs(Me, CurrentProjectFolder)
        If chosenFolder <> "" Then
            CurrentProjectFolder = chosenFolder
            MessageBox.Show("✅ Project inputs loaded successfully.", "Load Project")
        End If
    End Sub


    Public Sub LoadDefaultNotes(dgv As DataGridView)

        dgv.Rows.Clear()

        dgv.Rows.Add(1, "GENERAL NOTES:")
        dgv.Rows.Add(2, "A. APPLICABLE CODES & STANDARDS:")
        dgv.Rows.Add(3, "1. ASME SECTION II PART A,C&D - 2023 EDITION.")
        dgv.Rows.Add(4, "2. ASME SECTION V - 2023 EDITION.")
        dgv.Rows.Add(5, "3. ASME SECTION VIII DIVISION 1 - 2023 EDITION.")
        dgv.Rows.Add(6, "4. ASME SECTION IX - 2023 EDITION.")
        dgv.Rows.Add(7, "5. ASME B36.10M - 2018 EDITION. (FOR CARBON STEEL PIPES).")
        dgv.Rows.Add(8, "6. ASME B36.19M - 2018 EDITION. (FOR STAINLESS STEEL PIPES).")
        dgv.Rows.Add(9, "7. ASME B16.5 - 2020 EDITION. (FOR FLANGES).")
        dgv.Rows.Add(10, "8. ASME B16.20 - 2023 EDITION. (FOR METALLIC GASKETS).")
        dgv.Rows.Add(11, "9. ASME B1.1-2019 EDITION (FOR UNIFIED INCH SCREW THREADS).")
        dgv.Rows.Add(12, "10. ASME B18.2.2-2022 EDITION (FOR NUTS FOR GENERAL APPLICATIONS).")
        dgv.Rows.Add(13, "11. AMIMS-D-3204 (MANUFACTURE OF PRESSURE VESSELS).")
        dgv.Rows.Add(14, "12. AMIES-W-010 (WELDING REQUIREMENT FOR PRESSURE VESSEL).")
        dgv.Rows.Add(15, "13. AMIES-D-001 DESIGN CRITERIA FOR PRESSURE VESSELS.")
        dgv.Rows.Add(16, "14. AMIES-A-112 METEOROLOGICAL AND SEISMIC DESIGN DATA.")

        dgv.Rows.Add(17, "B. GENERAL")
        dgv.Rows.Add(18, "1. ALL NOZZLE DIAMETERS ARE IN NPS UNLESS OTHERWISE SPECIFIED.")
        dgv.Rows.Add(19, "2. ALL BOLT HOLES OF NOZZLES AND MANWAY FLANGES ARE TO STRADDLE NATURAL CENTER LINES.")
        dgv.Rows.Add(20, "3. ALL REMOVABLE INTERNALS (IF ANY) SHALL PASS THROUGH THE VESSEL MANWAY.")
        dgv.Rows.Add(21, "4. UNLESS NOTED OTHERWISE ALL DIMENSIONS ARE IN MILLIMETER AND ALL ELEVATION ARE IN METERS.")

        dgv.Rows.Add(22, "C. MATERIAL")
        dgv.Rows.Add(23, "1. GASKET SEATING SURFACE FINISH SHALL BE AS PER ASME B16.5, 125-250 µ INCHES (3.2-6.3 µm) AVERAGE ROUGHNESS. GASKET CONTACT SURFACE SHALL BE PROPERLY PROTECTED FROM BLASTING AND SHALL NOT BE COATED OR PAINTED.")

        dgv.Rows.Add(24, "D. FABRICATION")
        dgv.Rows.Add(25, "1. ALL RE-ROLLING OR FORMING OF THE SHELL SECTIONS IS TO BE COMPLETED PRIOR TO RADIOGRAPHY.")
        dgv.Rows.Add(26, "2. WELDS ATTACHING MANWAYS, NOZZLES AND THEIR REINFORCEMENT PADS AND OTHER STRUCTURAL ATTACHMENTS TO PRESSURE COMPONENTS SHALL NOT BE CLOSER THAN 50MM (TOE TO TOE) FROM ANY WELDS UNDER STRESS DUE TO PRESSURE.")
        dgv.Rows.Add(27, "3. TAPPED TELL-TALE HOLES 1/8"" NPT SHALL BE PROVIDED AT LOW LOCATION ACCESSIBLE FOR INSPECTION WITH CENTER OF THE HOLE 25 MM FROM EDGE OF THE REINFORCING PADS FOR ATTACHMENTS INCLUDING NOZZLES AND MANWAYS, EXCEPT LEG ATTACHMENT PAD.")
        dgv.Rows.Add(28, "4. THE FOLLOWING TOLERANCES ARE APPLICABLE DURING CONSTRUCTION:")
        dgv.Rows.Add(29, "A. UW-33 FOR ALIGNMENT OF SHELL AND HEAD EDGES AT BUTT WELD JOINT.")
        dgv.Rows.Add(30, "B. UW-35 & 36 FOR FINISHED LONGITUDINAL / CIRCUMFERENTIAL WELD JOINTS AND FILLET WELDS.")
        dgv.Rows.Add(31, "C. UG-80 FOR OUT-OF-ROUNDNESS FOR SHELL AND UG-81 FOR FORMED HEADS.")
        dgv.Rows.Add(32, "5. GROUNDING LUGS SHALL BE WELDED DIAGONALLY OPPOSITE OF THE VESSEL SADDLE & IT SHOULD BE UN-PAINTED.")
        dgv.Rows.Add(33, "6. EXTERNAL STIFFENERS FOR VESSELS SUBJECTED TO VACUUM IN HORIZONTAL AND VERTICAL VESSELS SHALL BE NOTCHED WHERE THEY CROSS LONGITUDINAL WELDS OR CONNECTION PIECES.")
        dgv.Rows.Add(34, "7. THE INSIDE EDGE OF ALL NOZZLES SHALL BE ROUNDED TO A RADIUS OF AT LEAST 3 MM.")
        dgv.Rows.Add(35, "8. PLATE EDGE LAMINATIONS REVEALED BY MAGNETIC PARTICLE EXAMINATION SHALL BE COMPLETELY REMOVED AND REPAIRED.")
        dgv.Rows.Add(36, "9. 2"" NOZZLES SHALL BE PROVIDED WITH 2 NOS. OF STIFFENERS AT 90° APART AS PER DRAWING.")
        dgv.Rows.Add(37, "10. INTERNAL AND EXTERNAL WELDED ATTACHMENT PAD SHALL HAVE THEIR CORNERS ROUNDED TO A MINIMUM RADIUS OF 50 MM AND SHALL BE FULLY SEAL WELDED.")

        dgv.Rows.Add(38, "E. WELDING")
        dgv.Rows.Add(39, "1. ALL WELDING SHALL BE IN ACCORDANCE WITH THE REQUIREMENTS OF AMIES-W-010 AND ASME SECTION IX-2023 EDITION.")
        dgv.Rows.Add(40, "2. ALL WELDING SURFACES SHALL BE THOROUGHLY CLEANED OF SCALE, RUST, OIL OR OTHER FOREIGN BODIES BEFORE WELDING.")
        dgv.Rows.Add(41, "3. ALL SHELL, HEAD AND NOZZLE JOINTS SHALL BE DOUBLE WELDED BUTT JOINTS WITH FULL PENETRATION.")
        dgv.Rows.Add(42, "4. ALL WELDS SHALL BE FREE FROM UNDERCUTS & IRREGULARITIES.")

        dgv.Rows.Add(43, "F. NON-DESTRUCTIVE EXAMINATION")
        dgv.Rows.Add(44, "1. ALL PRESSURE AND NON-PRESSURE WELDS SHALL BE VISUALLY INSPECTED WHERE ACCESSIBLE.")
        dgv.Rows.Add(45, "2. SKIRT AND KNUCKLE OF COLD FORMED HEADS SHALL BE CHECKED FOR SURFACE CRACKS BY MAGNETIC PARTICLE METHOD.")
        dgv.Rows.Add(46, "3. EXAMINATION OF ALL WELDS SHALL INCLUDE A BAND OF BASE METAL AT LEAST 25MM WIDE ON EACH SIDE OF THE WELD.")
        dgv.Rows.Add(47, "4. DELETED.")
        dgv.Rows.Add(48, "5. WHERE AN ATTACHMENT WELD OF A REINFORCING PAD OR A STRUCTURAL COMPONENT INTERSECTS OR ENCROACHES ON A BUTT-WELD, SPECIAL RT/MT EXAMINATION SHALL BE PERFORMED.")
        dgv.Rows.Add(49, "6. ALL REQUIRED NDE FOR FINAL ACCEPTANCE OF THE VESSEL SHALL BE PERFORMED AFTER COMPLETION OF ALL WELDING AND REPAIRS.")
        dgv.Rows.Add(50, "7. 100% CONVENTIONAL UT METHOD SHALL BE PERFORMED ON THE JOINT FROM AN ACCESSIBLE SIDE.")
        dgv.Rows.Add(51, "8. DELETED.")
        dgv.Rows.Add(52, "9. DELETED.")
        dgv.Rows.Add(53, "10. EACH ELEMENT OF THE VESSEL SHALL BE INSPECTED FOR THICKNESS BY ULTRASONIC MEASUREMENT POINTS AS PER APPENDIX III OF AMIMS-D-3204.")

        dgv.Rows.Add(54, "G. INSPECTION & TESTING")
        dgv.Rows.Add(55, "1. ALL WELDED ATTACHMENTS PROVIDED WITH TELL-TALE HOLES SHALL BE PNEUMATICALLY TESTED AT MINIMUM 100 KPA PRESSURE.")
        dgv.Rows.Add(56, "2. TELLTALE HOLES IN EXTERNAL ATTACHMENT PADS SHALL BE PLUGGED WITH GREASE, WOODEN PLUGS OR OTHER NON-PRESSURE RETAINING MATERIAL.")
        dgv.Rows.Add(57, "3. PRIOR TO FINAL INSPECTION AND HYDROTESTING, THE INSIDE AND OUTSIDE OF THE VESSEL SHALL BE THOROUGHLY CLEANED.")
        dgv.Rows.Add(58, "4. VESSEL SHALL BE PRESSURE TESTED USING WATER AS THE TESTING MEDIA IN ACCORDANCE WITH CODE AND AMIMS-A-007.")
        dgv.Rows.Add(59, "5. THE USE OF SHELLACS, GLUES, LEAD, ETC., ON GASKETS DURING TESTING IS PROHIBITED.")
        dgv.Rows.Add(60, "6. HYDROTEST SHALL BE PERFORMED WITH GASKETS AND BOLTING IDENTICAL TO THOSE REQUIRED IN SERVICE.")
        dgv.Rows.Add(61, "7. AFTER COMPLETION OF PRESSURE TESTING, THE VESSEL SHALL BE COMPLETELY DRAINED AND THOROUGHLY DRIED INCLUDING SURFACES OF INTERNALS.")
        dgv.Rows.Add(62, "8. CHORD LENGTH OF EXTERNAL PRESSURE TEMPLATE AND ACCEPTABLE TOLERANCE VALUES HAS BEEN CALCULATED AS PER UG-80(B).")
        dgv.Rows.Add(63, "9. ALL MATERIALS, EXCEPT CARBON STEELS, SHALL BE ALLOY-VERIFIED BY THE VESSEL MANUFACTURER IN ACCORDANCE WITH AMIES-A-206.")
        dgv.Rows.Add(64, "10. FOR VESSELS MADE COMPLETELY OF STAINLESS STEELS, PICKLING AND PASSIVATION SHALL BE APPLIED ACCORDING TO ASTM A380.")
        dgv.Rows.Add(65, "11. EXT. COATING OF THE DRUM SHALL BE AS PER SURFACE PREPARATION AND PAINTING PROCEDURE DOCUMENT NO.: SA-AMI-310-1PNA-000322 (CD.24.002.J05.001).")

        dgv.Rows.Add(66, "H. PACKING & SHIPPING")
        dgv.Rows.Add(67, "1. THE INTERIOR SURFACES OF VESSELS, INCLUDING INTERNALS, SHALL BE PROTECTED FROM CORROSION BY USE OF A NON-TOXIC VAPOUR PHASE CORROSION INHIBITOR.")
        dgv.Rows.Add(68, "2. NITROGEN BLANKETING AT A PRESSURE OF 35 KPA SHALL BE PROVIDED.")
        dgv.Rows.Add(69, "DANGER - VESSEL FILLED WITH NITROGEN UNDER PRESSURE - ASPHYXIATION HAZARD")
        dgv.Rows.Add(70, "3. THE FOLLOWING SPARES SHALL BE SUPPLIED LOOSE.")
        dgv.Rows.Add(71, "A. MINIMUM TWO SETS OF SPARE GASKETS FOR EACH MANWAY AND BLINDED NOZZLE IN THE VESSEL.")
        dgv.Rows.Add(72, "B. ALL BOLTING WITH MINIMUM 10% SPARE BOLTING (3 MINIMUM FOR EACH SIZE) PER VESSEL.")

    End Sub

#End Region

#Region "UPDATE PARAMETERS"

    Private Sub Update_All_Parameters_Only(invApp As Inventor.Application, asmDoc As AssemblyDocument)

        Dim folderPath As String = IO.Path.GetDirectoryName(invApp.DesignProjectManager.ActiveDesignProject.FullFileName)
        Dim shellIDmm As Double = CDbl(txt_Shell_Inside_Dia.Text)
        Dim shellThkMm As Double = CDbl(txt_Shell_Finished_Thk.Text)

        If SelectedClient = "ARAMCO" Then

            UpdateBasePlateParameters(invApp, IO.Path.Combine(folderPath, "Base_Part.ipt"))
            UpdateShellParameters(invApp, IO.Path.Combine(folderPath, "SHELL.ipt"))

        ElseIf SelectedClient = "ADNOC" Then

            UpdateBasePlateParameters(invApp, IO.Path.Combine(folderPath, "Base_Part_New.ipt"))                 'FOR SHELL, HEAD & SADDLES

            UpdateShellParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\SHELL_1.ipt"))                           'FOR NOZZLES
            UpdateShellParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\SHELL_2.ipt"))                           'FOR NOZZLES
            UpdateShellParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\SHELL_3.ipt"))                           'FOR NOZZLES

            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_1_.ipt"))                   'LL1
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_2_.ipt"))                   'LL2
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_3_.ipt"))                   'LL3
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "LL_BASE_4_.ipt"))                   'LL4

            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_1_.ipt"))             'PFC_1
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_2_.ipt"))             'PFC_2
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_3_.ipt"))             'PFC_3
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_4_.ipt"))             'PFC_4
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_5_.ipt"))             'PFC_5
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_6_.ipt"))             'PFC_6
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_7_.ipt"))             'PFC_7
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_8_.ipt"))             'PFC_8
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_9_.ipt"))             'PFC_9
            'UpdateLiftingLugParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PFC_10_.ipt"))            'PFC_10

            'UpdatePipeSupportParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PSC_1.ipt"))             'PSC_1
            'UpdatePipeSupportParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PSC_2.ipt"))             'PSC_2
            'UpdatePipeSupportParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PSC_3.ipt"))             'PSC_3
            'UpdatePipeSupportParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PSC_4.ipt"))             'PSC_4
            'UpdatePipeSupportParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_PSC_5.ipt"))             'PSC_5

            'UpdateCableTraySupportCleatParameters(invApp, IO.Path.Combine(folderPath, "BASE_PART_CTC_1.ipt"))   'CTC_1

            UpdateNamePlateAdnocParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\NAME_PLATE_BASE_PART.ipt"))     'NAME PLATE

            UpdateStepRungParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\NAME_PLATE_BASE_PART.ipt"))           'STEP RUNG

            UpdateGrabRungParameters(invApp, IO.Path.Combine(folderPath, "ADNOC\NAME_PLATE_BASE_PART.ipt"))           'GRAB RUNG

        ElseIf SelectedClient = "QATAR" Then


        End If

    End Sub

    Public Sub UpdateBasePlateParameters(invApp As Inventor.Application, basePlatePath As String)

        '==================================================
        ' VALIDATION
        '==================================================
        If invApp Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(basePlatePath) Then Exit Sub
        If Not IO.File.Exists(basePlatePath) Then Exit Sub

        Try

            '==================================================
            ' OPEN PART
            '==================================================
            Dim partDoc As PartDocument = CType(invApp.Documents.Open(basePlatePath, False), PartDocument)
            Dim params As Parameters = partDoc.ComponentDefinition.Parameters

            If SelectedClient = "ARAMCO" Then

                '==================================================
                ' VESSEL PARAMETERS
                '==================================================

                params.Item("ID").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("THK").Expression = txt_EH_Nom_Thk.Text & " mm"
                params.Item("LENGTH").Expression = txt_Shell_Length.Text & " mm"
                params.Item("SF").Expression = txt_EH_SF.Text & " mm"

                'SLOPE IS ANGLE
                params.Item("SLOPE").Expression = txt_Slope_Deg.Text & " deg"

                '==================================================
                ' SADDLE PARAMETERS
                '==================================================
                params.Item("SADDLE_DIST_FROM_TL").Expression = txt_LS_Dist_from_TL.Text & " mm"
                params.Item("SADDLE_THK").Expression = txt_LS_Pad_Thk.Text & " mm"
                params.Item("SADDLE_ANGLE").Expression = txt_LS_Mid_Rib_Angle.Text & " deg"
                params.Item("SADDLE_ANGLE_1").Expression = txt_LS_Pad_Angle.Text & " deg"
                params.Item("Cent_Dist_FROM_BASE").Expression = txt_LS_Centre_Dist_From_Base.Text & " mm"

                '==================================================
                ' LEFT SUPPORT
                '==================================================
                params.Item("LS_BP_THK").Expression = txt_LS_BP_Thk.Text & " mm"
                params.Item("LS_BP_LENGTH").Expression = txt_LS_BP_Length.Text & " mm"
                params.Item("LS_BP_WIDTH").Expression = txt_LS_BP_Width.Text & " mm"
                'params.Item("LS_WP_THK").Expression = txt_LS_WP_Thk.Text & " mm"
                params.Item("LS_MID_RIB_LENGTH").Expression = txt_LS_Mid_Rib_Length.Text & " mm"
                params.Item("LS_MID_RIB_WIDTH").Expression = txt_LS_Mid_Rib_Width.Text & " mm"
                params.Item("LS_MID_RIB_THK").Expression = txt_LS_Mid_Rib_Thk.Text & " mm"
                params.Item("LS_RIB_1_LENGTH").Expression = txt_LS_Rib1_Length.Text & " mm"
                params.Item("LS_RIB_2_LENGTH").Expression = txt_LS_Rib2_Length.Text & " mm"
                params.Item("LS_RIB_1_THK").Expression = txt_LS_Rib1_Thk.Text & " mm"
                params.Item("LS_RIB_2_THK").Expression = txt_LS_Rib2_Thk.Text & " mm"
                params.Item("LS_RIB_1_PLATE_WIDTH").Expression = txt_LS_Rib1_Width.Text & " mm"
                params.Item("LS_RIB_2_PLATE_WIDTH").Expression = txt_LS_Rib2_Width.Text & " mm"
                'params.Item("LS_PAD_PLATE_WIDTH").Expression = txt_LS_Pad_Plate_Width.Text & " mm"
                'params.Item("LS_PAD_PLATE_FILLET").Expression = txt_LS_Pad_Fillet.Text & " mm"

                '==================================================
                ' RIGHT SUPPORT
                '==================================================
                params.Item("RS_BP_THK").Expression = txt_RS_BP_Thk.Text & " mm"
                params.Item("RS_BP_LENGTH").Expression = txt_RS_BP_Length.Text & " mm"
                params.Item("RS_BP_WIDTH").Expression = txt_RS_BP_Width.Text & " mm"
                'params.Item("RS_HT_FROM_BASE").Expression = txt_RS_HT_From_Base.Text & " mm"
                params.Item("RS_MID_RIB_LENGTH").Expression = txt_RS_Mid_Rib_Length.Text & " mm"
                'params.Item("RS_MID_RIB_SHORT_LENGTH").Expression = txt_RS_Mid_Rib_Short_Length.Text & " mm"
                params.Item("RS_RIB_1_LENGTH").Expression = txt_RS_Rib1_Length.Text & " mm"
                params.Item("RS_RIB_2_LENGTH").Expression = txt_RS_Rib2_Length.Text & " mm"
                'params.Item("RS_PAD_PLATE_FILLET").Expression = txt_RS_Pad_Plate_Fillet.Text & " mm"

                ''==================================================
                '' STIFFENER RING
                ''==================================================
                'params.Item("STIFFENER_RING_DIST").Expression = txt_Stiffener_Ring_Dist.Text & " mm"
                'params.Item("STIFFENER_RING_THK").Expression = txt_Stiffener_Ring_Thk.Text & " mm"
                'params.Item("STIFFENER_RING_OD").Expression = txt_Stiffener_Ring_OD.Text & " mm"

            ElseIf SelectedClient = "ADNOC" Then

                '==================================================
                ' VESSEL PARAMETERS
                '==================================================
                Try

                    ' HEAD
                    params.Item("ID").Expression = txt_EH_Inside_Dia.Text & " mm"
                    params.Item("SF").Expression = txt_EH_SF.Text & " mm"
                    params.Item("SLOPE").Expression = txt_Slope_Deg.Text & " deg"
                    params.Item("HEAD_THK").Expression = txt_EH_Nom_Thk.Text & " mm"

                    'SHELL
                    params.Item("SHELL_THK").Expression = txt_Shell_Finished_Thk_Adnoc.Text & " mm"
                    params.Item("SHELL_LENGTH_1").Expression = txt_Shell_Length1_Adnoc.Text & " mm"
                    params.Item("SHELL_LENGTH_2").Expression = txt_Shell_Length2_Adnoc.Text & " mm"
                    params.Item("SHELL_LENGTH_3").Expression = txt_Shell_Length3_Adnoc.Text & " mm"

                    'LEFT SADDLE
                    params.Item("LS_DIST").Expression = txt_LS_Dist.Text & " mm"
                    params.Item("ANGLE_1").Expression = txt_LS_ANGLE_1.Text & " deg"
                    params.Item("ANGLE_2").Expression = txt_LS_ANGLE_2.Text & " deg"
                    params.Item("WEARPLATE_THK").Expression = txt_LS_WEARPLATE_THK.Text & " mm"
                    params.Item("WEARPLATE_WIDTH").Expression = txt_LS_WEARPLATE_WIDTH.Text & " mm"
                    params.Item("WEARPLATE_FILLET").Expression = txt_LS_WEARPLATE_FILLET.Text & " mm"
                    params.Item("BP_LENGTH").Expression = txt_LS_BaseP_LENGTH.Text & " mm"
                    params.Item("BP_THK").Expression = txt_LS_BaseP_THK.Text & " mm"
                    params.Item("BP_WIDTH").Expression = txt_LS_BaseP_WIDTH.Text & " mm"
                    params.Item("MID_RIB_WIDTH").Expression = txt_LS_MIDDLE_RIB_WIDTH.Text & " mm"
                    params.Item("PTFE_LENGTH").Expression = txt_LS_PTFE_LENGTH.Text & " mm"
                    params.Item("PTFE_WIDTH").Expression = txt_LS_PTFE_WIDTH.Text & " mm"
                    params.Item("PTFE_THK").Expression = txt_LS_PTFE_THK.Text & " mm"
                    params.Item("RIB_THK").Expression = txt_LS_RIB_THK.Text & " mm"
                    params.Item("LS_RIB_LENGTH_1").Expression = txt_LS_LS_RIB_LENGTH_1.Text & " mm"
                    params.Item("LS_RIB_LENGTH_2").Expression = txt_LS_LS_RIB_LENGTH_2.Text & " mm"
                    params.Item("LS_MID_TO_BASE").Expression = txt_LS_LS_MID_TO_BASE.Text & " mm"
                    params.Item("MID_RIB_THK").Expression = txt_LS_MIDDLE_RIB_THK.Text & " mm"
                    params.Item("BEARING_PLATE_LENGTH").Expression = txt_LS_BEARING_PLATE1_LENGTH.Text & " mm"
                    params.Item("BEARING_PLATE_THK").Expression = txt_LS_BEARING_PLATE1_THK.Text & " mm"
                    params.Item("BEARING_PLATE_WIDTH").Expression = txt_LS_BEARING_PLATE1_WIDTH.Text & " mm"
                    params.Item("GAP").Expression = txt_LS_GAP.Text & " mm"
                    params.Item("BEARING_PLATE2_LENGTH").Expression = txt_LS_BEARING_PLATE2_LENGTH.Text & " mm"
                    params.Item("BEARING_PLATE2_THK").Expression = txt_LS_BEARING_PLATE2_THK.Text & " mm"
                    params.Item("BEARING_PLATE2_WIDTH").Expression = txt_LS_BEARING_PLATE2_WIDTH.Text & " mm"

                    'RIGHT SADDLE
                    params.Item("RS_DIST").Expression = txt_RS_Dist.Text & " mm"
                    params.Item("RS_RIB_LENGTH_1").Expression = txt_RS_RIB_LENGTH_1.Text & " mm"
                    params.Item("RS_RIB_LENGTH_2").Expression = txt_RS_RIB_LENGTH_2.Text & " mm"
                    params.Item("RS_MID_TO_BASE").Expression = txt_RS_MID_TO_BASE.Text & " mm"

                Catch ex As Exception
                    Debug.Print("Msg   : " & ex.Message)
                End Try

            End If

            '==================================================
            ' UPDATE MODEL
            '==================================================
            partDoc.Update()

            partDoc.Save()

            partDoc.Close(True)

        Catch ex As Exception

            Debug.Print("Msg   : " & ex.Message)

        End Try

    End Sub

    Public Sub UpdateShellParameters(invApp As Inventor.Application, shellPath As String)

        '==================================================
        ' VALIDATION
        '==================================================
        If invApp Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(shellPath) Then Exit Sub
        If Not IO.File.Exists(shellPath) Then Exit Sub

        Try

            '==================================================
            ' OPEN SHELL PART
            '==================================================
            Dim partDoc As PartDocument = CType(invApp.Documents.Open(shellPath, False), PartDocument)
            Dim params As Parameters = partDoc.ComponentDefinition.Parameters

            '==================================================
            ' LOOP ALL SHELL NOZZLES
            '==================================================
            For Each row As DataGridViewRow In DGV_Nozzle_Tab_Shell_HOR.Rows

                If row.IsNewRow Then Continue For
                Dim nozzleMark As String = If(row.Cells("NOZZLE_MARK_SHELL_HOR").Value, "").ToString.Trim()
                If nozzleMark = "" Then Continue For

                '----------------------------------------------
                ' Remove spaces if any
                ' N5A -> N5A
                '----------------------------------------------
                nozzleMark = nozzleMark.Replace(" ", "")

                '----------------------------------------------
                ' Read values
                '----------------------------------------------
                Dim nozzleODmm As Double = GetPipeOD_mm(row.Cells("SIZE_NPS_SHELL_HOR").Value.ToString())
                Dim distMm As Double = Val(row.Cells("DISTANCE_SHELL_HOR").Value)
                Dim offsetMm As Double = Val(row.Cells("OFFSET_DIST_SHELL_HOR").Value)

                '----------------------------------------------
                ' Parameter Names
                '----------------------------------------------
                Dim diaParam As String = "NZ_" & nozzleMark & "_DIA"
                Dim distParam As String = "NZ_" & nozzleMark & "_DIST"
                Dim offsetParam As String = "NZ_" & nozzleMark & "_OFFSET_DIST"

                '----------------------------------------------
                ' Update DIA
                '----------------------------------------------
                Try
                    params.Item(diaParam).Expression = nozzleODmm & " mm"
                Catch ex As Exception
                    Debug.Print("Missing Parameter : " & diaParam)
                End Try

                '----------------------------------------------
                ' Update DIST
                '----------------------------------------------
                Try
                    params.Item(distParam).Expression = distMm & " mm"
                Catch ex As Exception
                    Debug.Print("Missing Parameter : " & distParam)
                End Try

                '----------------------------------------------
                ' Update OFFSET DIST
                '----------------------------------------------
                Try
                    params.Item(offsetParam).Expression = offsetMm & " mm"
                Catch ex As Exception
                    Debug.Print("Missing Parameter : " & offsetParam)
                End Try

            Next

            '==================================================
            ' UPDATE
            '==================================================
            partDoc.Update()
            partDoc.Save()
            partDoc.Close(True)

        Catch ex As Exception

            Debug.Print("====================================")
            Debug.Print("UpdateShellPartParameters ERROR")
            Debug.Print(ex.Message)
            Debug.Print("====================================")

        End Try

    End Sub

    Public Sub UpdateLiftingLugParameters(invApp As Inventor.Application, LiftingLugPath As String)

        '==================================================
        ' VALIDATION
        '==================================================
        If invApp Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(LiftingLugPath) Then Exit Sub
        If Not IO.File.Exists(LiftingLugPath) Then Exit Sub

        Try
            '==================================================
            ' OPEN PART
            '==================================================
            Dim partDoc As PartDocument = CType(invApp.Documents.Open(LiftingLugPath, False), PartDocument)
            Dim params As Parameters = partDoc.ComponentDefinition.Parameters

            If SelectedClient = "ARAMCO" Then

                params.Item("LL_DIST").Expression = txt_EH_Inside_Dia.Text & " mm"
                'params.Item("LL_ANGLE").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("LL_PAD_THK").Expression = txt_LL_Pad_Thk.Text & " mm"
                params.Item("LL_PLATE_THK").Expression = txt_LL_PLate_Thk.Text & " mm"
                params.Item("LL_PLATE_LENGTH").Expression = txt_LL_PLate_Length.Text & " mm"
                params.Item("LL_PAD_OFFSET_DIST").Expression = txt_LL_Pad_Offset.Text & " mm"
                params.Item("LL_PAD_ARC_LENGTH").Expression = txt_LL_Arc_Length.Text & " mm"
                params.Item("LL_PAD_LENGTH").Expression = txt_LL_Pad_Length.Text & " mm"
                params.Item("LL_PAD_FILLET").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("LL_SUPPORT_PLATE_DIST").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("LL_SUPPORT_PLATE_THK").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("LL_PLATE_HEIGHT").Expression = txt_EH_Inside_Dia.Text & " mm"
            ElseIf SelectedClient = "ADNOC" Then
                params.Item("LL_DIST").Expression = txt_LL_AD_Assm_Dist1.Text & " mm"
                params.Item("PAD_THK").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PAD_ARC_LENGTH").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PAD_OFFSET").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("LL_OFFSET").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_HT").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PAD_WIDTH").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PAD_FILLET").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("SP_ANGLE").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("SP_DIST").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("SP_HT").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_LENGTH").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("SP_THK").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_DIST_1").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_DIST_2").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_HOLE_DIA").Expression = txt_EH_Inside_Dia.Text & " mm"
                params.Item("PLATE_FILLET").Expression = txt_EH_Inside_Dia.Text & " mm"
            End If





            '==================================================
            ' UPDATE MODEL
            '==================================================
            partDoc.Update()
            partDoc.Save()
            partDoc.Close(True)
        Catch ex As Exception
            Debug.Print("Msg   : " & ex.Message)
        End Try

    End Sub

    Public Sub UpdatePlatformSupportParameters(invApp As Inventor.Application, PlatformSupportPath As String)

    End Sub

    Public Sub UpdatePipeSupportParameters(invApp As Inventor.Application, PipeSupportPath As String)

    End Sub

    Public Sub UpdateCableTraySupportCleatParameters(invApp As Inventor.Application, CableTraySupportCleatPath As String)

    End Sub

    Public Sub UpdateNamePlateAdnocParameters(invApp As Inventor.Application, CableTraySupportCleatPath As String)

    End Sub

    Public Sub UpdateStepRungParameters(invapp As Inventor.Application, StepRungPath As String)

    End Sub

    Public Sub UpdateGrabRungParameters(invapp As Inventor.Application, StepRungPath As String)

    End Sub

#End Region

#Region "EDIT FUNCTIONS"

    Private Const MASTER_ROOT As String = "D:\HORIZONTAL_TANK"
    Private CurrentProjectFolder As String = String.Empty

    Private Sub Edit_3D_Click(sender As Object, e As EventArgs) Handles Edit_3D.Click, Button2.Click

        Dim invApp As Inventor.Application = Nothing
        Dim asmDoc As AssemblyDocument = Nothing

        Try
            '==================================================
            ' 1) GET OR START INVENTOR
            '==================================================
            Try
                invApp = CType(Marshal.GetActiveObject("Inventor.Application"), Inventor.Application)
            Catch
                invApp = CType(Activator.CreateInstance(Type.GetTypeFromProgID("Inventor.Application")), Inventor.Application)
                invApp.Visible = True
            End Try

            If invApp Is Nothing Then
                MessageBox.Show("❌ Unable to start Inventor.", "Edit 3D")
                Exit Sub
            End If

            invApp.SilentOperation = True

            '==================================================
            ' CONDITION 1: DOCUMENT ALREADY OPEN
            '==================================================
            If invApp.Documents.Count > 0 Then

                If TypeOf invApp.ActiveDocument Is AssemblyDocument Then

                    asmDoc = CType(invApp.ActiveDocument, AssemblyDocument)

                    ' --- NEW: refuse to touch the master, redirect to copy workflow ---
                    If IsUnderMasterRoot(asmDoc.FullFileName) Then
                        MessageBox.Show("⚠️ The currently open assembly is the MASTER file:" & vbCrLf & asmDoc.FullFileName & vbCrLf & vbCrLf & "Please close it and run Edit 3D again so a working copy " &
                        "can be created and edited instead.", "Edit 3D - Master File Protected")
                        Exit Sub
                    End If

                    ' Open document is already a working copy -> edit it directly
                    CurrentProjectFolder = IO.Path.GetDirectoryName(asmDoc.FullFileName)

                    Update_All_Parameters_Only(invApp, asmDoc)

                    asmDoc.Update()
                    asmDoc.Save()

                    MessageBox.Show("✅ 3D Model Updated Successfully", "Create 3D")
                    Exit Sub

                Else
                    MessageBox.Show("❌ Active document is not an Assembly.", "Create 3D")
                    Exit Sub
                End If

            End If

            '==================================================
            ' CONDITION 2: NO DOCUMENT OPEN
            '==================================================

            '----------------------------------------------
            ' 2) LOCATE MASTER PROJECT (.ipj)
            '----------------------------------------------
            Dim masterIpjPath As String = SelectInventorProjectFile()
            If String.IsNullOrWhiteSpace(masterIpjPath) Then Exit Sub

            '----------------------------------------------
            ' 2a) VALIDATE PROJECT CODE (used for folder/file naming)
            '----------------------------------------------
            If String.IsNullOrWhiteSpace(txt_Proj_Code.Text) Then
                MessageBox.Show("❌ Please enter a Project Code (txt_Proj_Code) before proceeding.", "Edit 3D")
                Exit Sub
            End If

            '----------------------------------------------
            ' 3) ASK USER WHERE TO COPY THE PROJECT
            '----------------------------------------------
            Dim destRootFolder As String = ChooseDestinationFolder()
            If String.IsNullOrWhiteSpace(destRootFolder) Then Exit Sub

            '----------------------------------------------
            ' 4) COPY THE WHOLE PROJECT FOLDER TO DESTINATION
            '----------------------------------------------
            Dim copiedIpjPath As String = CopyProjectToDestination(masterIpjPath, destRootFolder)
            If String.IsNullOrWhiteSpace(copiedIpjPath) Then
                MessageBox.Show("❌ Failed to copy project folder.", "Edit 3D")
                Exit Sub
            End If

            ' Remember the copied project folder so other buttons (e.g. 2D Drawing)
            ' can work from this same copy instead of the master.
            CurrentProjectFolder = IO.Path.GetDirectoryName(copiedIpjPath)

            ' ✅ ADD THIS RIGHT AFTER:
            SaveProjectInputs(Me, CurrentProjectFolder)

            '----------------------------------------------
            ' 5) ACTIVATE THE COPIED PROJECT (NOT THE MASTER)
            '----------------------------------------------
            Dim activeProject As Inventor.DesignProject = AddAndActivateProject(invApp, copiedIpjPath)
            If activeProject Is Nothing Then Exit Sub


            '----------------------------------------------
            ' 6) LEG SUPPORT TYPE
            '----------------------------------------------
            Dim legType As String

            'If Rbn_Angle.Checked Then
            '    legType = "ANGLE"
            'ElseIf Rbn_Beam.Checked Then
            '    legType = "BEAM"
            'Else
            '    MessageBox.Show("❌ Select Leg Support Type", "Edit 3D")
            '    Exit Sub
            'End If


            '----------------------------------------------
            ' 7) OPEN MAIN ASSEMBLY FROM THE COPIED PROJECT
            '----------------------------------------------
            asmDoc = OpenMainAssemblyFromProject(invApp, activeProject, legType)
            If asmDoc Is Nothing Then Exit Sub


            '----------------------------------------------
            ' 8) RUN VISIBILITY iLOGIC
            '----------------------------------------------
            Dim visibilityRulePath As String = GetVisibilityILogicRulePath()

            If visibilityRulePath <> "" Then
                RunExternalILogicRule(invApp, asmDoc, visibilityRulePath)
            End If


            '----------------------------------------------
            ' 9) UPDATE & SAVE (the copy only)
            '----------------------------------------------
            Update_All_Parameters_Only(invApp, asmDoc)

            asmDoc.Update()
            asmDoc.Save()

            ' ✅ ADD THIS RIGHT AFTER:
            SaveProjectInputs(Me, CurrentProjectFolder)
            MessageBox.Show("✅ 3D Model Created Successfully in:" & vbCrLf & copiedIpjPath, "Create 3D")

        Catch ex As Exception
            MessageBox.Show("❌ Edit failed: " & ex.Message, "Create 3D")
        Finally
            If invApp IsNot Nothing Then
                invApp.SilentOperation = False
            End If
        End Try

    End Sub

    Private Function IsUnderMasterRoot(filePath As String) As Boolean

        If String.IsNullOrWhiteSpace(filePath) Then Return False

        Try
            Dim fullMaster As String = IO.Path.GetFullPath(MASTER_ROOT).TrimEnd(IO.Path.DirectorySeparatorChar)
            Dim fullFile As String = IO.Path.GetFullPath(filePath)

            Return fullFile.StartsWith(fullMaster & IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try

    End Function

    Private Function ChooseDestinationFolder() As String

        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select a folder where the working copy of the project will be created"
            fbd.ShowNewFolderButton = True

            If fbd.ShowDialog() = DialogResult.OK Then

                ' Prevent choosing a destination that is inside the master root
                If IsUnderMasterRoot(fbd.SelectedPath) Then
                    MessageBox.Show("❌ Destination cannot be inside the master project folder (" & MASTER_ROOT & ").", "Edit 3D")
                    Return String.Empty
                End If

                Return fbd.SelectedPath
            End If
        End Using

        Return String.Empty

    End Function

    Private Function CopyProjectToDestination(masterIpjPath As String, destRootFolder As String) As String

        Try
            Dim sourceFolder As String = IO.Path.GetDirectoryName(masterIpjPath)

            '--------------------------------------------
            ' Build destination folder/file names from txt_Proj_Code
            '--------------------------------------------
            Dim projCode As String = SanitizeForFileName(txt_Proj_Code.Text.Trim())

            If String.IsNullOrWhiteSpace(projCode) Then
                MessageBox.Show("❌ Project Code (txt_Proj_Code) is empty or invalid.", "Edit 3D")
                Return String.Empty
            End If

            Dim destFolder As String = IO.Path.Combine(destRootFolder, projCode)

            If IO.Directory.Exists(destFolder) Then
                MessageBox.Show("❌ Destination folder already exists:" & vbCrLf & destFolder & vbCrLf & vbCrLf & "Please choose a different Project Code or destination.", "Edit 3D")
                Return String.Empty
            End If

            ' --- Recursive copy of the whole project folder ---
            CopyDirectoryRecursive(sourceFolder, destFolder)

            ' --- Rename the copied .ipj to "<ProjCode>.ipj" ---
            Dim copiedIpjOldPath As String = IO.Path.Combine(destFolder, IO.Path.GetFileName(masterIpjPath))
            Dim copiedIpjNewName As String = projCode & ".ipj"
            Dim copiedIpjNewPath As String = IO.Path.Combine(destFolder, copiedIpjNewName)

            If IO.File.Exists(copiedIpjOldPath) Then
                IO.File.Move(copiedIpjOldPath, copiedIpjNewPath)
            Else
                ' Fallback: find any .ipj in the copied folder
                copiedIpjNewPath = IO.Directory.GetFiles(destFolder, "*.ipj").FirstOrDefault()
                If String.IsNullOrWhiteSpace(copiedIpjNewPath) Then Return String.Empty
            End If

            ' --- Rewrite absolute workspace/subfolder references to point at the copy ---
            RewriteProjectFilePaths(copiedIpjNewPath, sourceFolder, destFolder)

            Return copiedIpjNewPath

        Catch ex As Exception
            MessageBox.Show("❌ Error copying project folder:" & vbCrLf & ex.Message, "Edit 3D")
            Return String.Empty
        End Try

    End Function

    Private Function SanitizeForFileName(name As String) As String

        If String.IsNullOrWhiteSpace(name) Then Return String.Empty

        Dim invalidChars As Char() = IO.Path.GetInvalidFileNameChars()
        Dim result As New System.Text.StringBuilder()

        For Each c As Char In name
            If Array.IndexOf(invalidChars, c) < 0 Then
                result.Append(c)
            Else
                result.Append("_")
            End If
        Next

        Return result.ToString().Trim()

    End Function

    Private Sub CopyDirectoryRecursive(sourceDir As String, destDir As String)

        IO.Directory.CreateDirectory(destDir)

        ' Copy files
        For Each filePath As String In IO.Directory.GetFiles(sourceDir)

            Dim fileName As String = IO.Path.GetFileName(filePath)

            ' Skip Inventor lock files / temp files
            If fileName.StartsWith("~$") OrElse
           fileName.EndsWith(".lck", StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If

            Dim destFilePath As String = IO.Path.Combine(destDir, fileName)
            IO.File.Copy(filePath, destFilePath, overwrite:=True)

            ' Make sure the copy isn't left read-only (can happen with some PDM/vault setups)
            Dim attrs As IO.FileAttributes = IO.File.GetAttributes(destFilePath)
            If (attrs And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                IO.File.SetAttributes(destFilePath, attrs And Not IO.FileAttributes.ReadOnly)
            End If

        Next

        ' Recurse into subdirectories
        For Each subDir As String In IO.Directory.GetDirectories(sourceDir)
            Dim subDirName As String = IO.Path.GetFileName(subDir)
            Dim destSubDir As String = IO.Path.Combine(destDir, subDirName)
            CopyDirectoryRecursive(subDir, destSubDir)
        Next

    End Sub

    Private Sub RewriteProjectFilePaths(ipjPath As String, oldFolder As String, newFolder As String)

        Try
            Dim lines() As String = IO.File.ReadAllLines(ipjPath)

            For i As Integer = 0 To lines.Length - 1

                Dim line As String = lines(i)

                ' Only touch lines that define a path (Workspace, Frequently Used Subfolders,
                ' Libraries, etc.) and that contain the OLD absolute folder.
                If line.IndexOf(oldFolder, StringComparison.OrdinalIgnoreCase) >= 0 Then

                    If line.TrimStart().StartsWith("Workspace=", StringComparison.OrdinalIgnoreCase) OrElse
                   line.TrimStart().StartsWith("Frequently Used Subfolders=", StringComparison.OrdinalIgnoreCase) Then

                        lines(i) = line.Replace(oldFolder, newFolder)

                    End If

                End If

            Next

            IO.File.WriteAllLines(ipjPath, lines)

        Catch ex As Exception
            ' Non-fatal: log/notify but continue, since most projects use relative paths
            MessageBox.Show("⚠️ Could not update paths inside copied project file:" & vbCrLf & ex.Message, "Edit 3D - Warning")
        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        StartInventorAssemblyWorkflow_Horizontal()
    End Sub

#End Region

End Class