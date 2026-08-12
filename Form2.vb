Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2

    Private Sub Leg_Support_Assembly_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Listen to support type changes
        AddHandler LegSupportState.SupportTypeChanged, AddressOf OnSupportTypeChanged

        ' Sync UI immediately
        ApplySupportTypeUI(LegSupportState.CurrentSupportType)

        isLoading = True

        LoadAngleValuesFromModule()
        LoadBeamValuesFromModule()

        isLoading = False

        ''Read Only - ANGLE
        txt_BaseP_length.ReadOnly = True
        txt_BaseP_height.ReadOnly = True
        txt_BaseP_hole_dia.ReadOnly = True
        txt_BaseP_hole_length.ReadOnly = True
        txt_BaseP_hole_ht.ReadOnly = True
        txt_BaseP_thk.ReadOnly = True

        txt_CSP_length.ReadOnly = True
        txt_CSP_height.ReadOnly = True
        txt_CSP_thickness.ReadOnly = True
        txt_CSP_distance.ReadOnly = True
        txt_CSP_arc_radius.ReadOnly = True
        txt_CSP_fillet.ReadOnly = True

        txt_BottomP_length.ReadOnly = True
        txt_BottomP_height.ReadOnly = True
        txt_BottomP_thk.ReadOnly = True
        txt_BottomP_arc_radius.ReadOnly = True
        txt_BottomP_fillet.ReadOnly = True

        txt_RFPad_length.ReadOnly = True
        txt_RFPad_height.ReadOnly = True
        txt_RFPad_Thk.ReadOnly = True
        txt_RFPad_Fillet.ReadOnly = True

        ''Read Only - BEAM
        txt_BaseP_B_length.ReadOnly = True
        txt_BaseP_B_height.ReadOnly = True
        txt_BaseP_B_hole_dia.ReadOnly = True
        txt_BaseP_B_hole_length.ReadOnly = True
        txt_BaseP_B_hole_height.ReadOnly = True
        txt_BaseP_B_thk.ReadOnly = True
        txt_BaseP_B_dist.ReadOnly = True

        txt_CSP_B_length.ReadOnly = True
        txt_CSP_B_height.ReadOnly = True
        txt_CSP_B_thickness.ReadOnly = True
        txt_CSP_B_arc_radius.ReadOnly = True
        txt_CSP_B_fillet.ReadOnly = True

        txt_BottomP_B_length.ReadOnly = True
        txt_BottomP_B_height.ReadOnly = True
        txt_BottomP_B_thk.ReadOnly = True
        txt_BottomP_B_arc_radius.ReadOnly = True

        txt_RFPad_B_length.ReadOnly = True
        txt_RFPad_B_height.ReadOnly = True
        txt_RFPad_B_Thk.ReadOnly = True
        txt_RFPad_B_Fillet.ReadOnly = True

        ''TextBox Color  - ANGLE
        txt_BaseP_length.BackColor = Color.Silver
        txt_BaseP_height.BackColor = Color.Silver
        txt_BaseP_hole_dia.BackColor = Color.Silver
        txt_BaseP_hole_length.BackColor = Color.Silver
        txt_BaseP_hole_ht.BackColor = Color.Silver
        txt_BaseP_thk.BackColor = Color.Silver

        txt_CSP_length.BackColor = Color.Silver
        txt_CSP_height.BackColor = Color.Silver
        txt_CSP_thickness.BackColor = Color.Silver
        txt_CSP_distance.BackColor = Color.Silver
        txt_CSP_arc_radius.BackColor = Color.Silver
        txt_CSP_fillet.BackColor = Color.Silver

        txt_BottomP_length.BackColor = Color.Silver
        txt_BottomP_height.BackColor = Color.Silver
        txt_BottomP_thk.BackColor = Color.Silver
        txt_BottomP_arc_radius.BackColor = Color.Silver
        txt_BottomP_fillet.BackColor = Color.Silver

        txt_RFPad_length.BackColor = Color.Silver
        txt_RFPad_height.BackColor = Color.Silver
        txt_RFPad_Thk.BackColor = Color.Silver
        txt_RFPad_Fillet.BackColor = Color.Silver

        ''TextBox Color  - BEAM
        txt_BaseP_B_length.BackColor = Color.Silver
        txt_BaseP_B_height.BackColor = Color.Silver
        txt_BaseP_B_hole_dia.BackColor = Color.Silver
        txt_BaseP_B_hole_length.BackColor = Color.Silver
        txt_BaseP_B_hole_height.BackColor = Color.Silver
        txt_BaseP_B_thk.BackColor = Color.Silver
        txt_BaseP_B_dist.BackColor = Color.Silver

        txt_CSP_B_length.BackColor = Color.Silver
        txt_CSP_B_height.BackColor = Color.Silver
        txt_CSP_B_thickness.BackColor = Color.Silver
        txt_CSP_B_arc_radius.BackColor = Color.Silver
        txt_CSP_B_fillet.BackColor = Color.Silver

        txt_BottomP_B_length.BackColor = Color.Silver
        txt_BottomP_B_height.BackColor = Color.Silver
        txt_BottomP_B_thk.BackColor = Color.Silver
        txt_BottomP_B_arc_radius.BackColor = Color.Silver

        txt_RFPad_B_length.BackColor = Color.Silver
        txt_RFPad_B_height.BackColor = Color.Silver
        txt_RFPad_B_Thk.BackColor = Color.Silver
        txt_RFPad_B_Fillet.BackColor = Color.Silver

    End Sub

    Private Sub OnSupportTypeChanged(newType As LegSupportState.SupportType)

        ApplySupportTypeUI(newType)

    End Sub

    Private Sub ApplySupportTypeUI(supportType As LegSupportState.SupportType)

        Select Case supportType

            Case LegSupportState.SupportType.Angle

                ' ANGLE PANELS
                Panel_Angle.Visible = True
                Panel_Beam.Visible = False

                Panel_BP_Angle.Visible = True
                Panel_BP_Beam.Visible = False

                Panel_CSP_Angle.Visible = True
                Panel_CSP_Beam.Visible = False

                Panel_BSP_Angle.Visible = True
                Panel_BSP_Beam.Visible = False

                Panel_RFPad_Angle.Visible = True
                Panel_RFPad_Beam.Visible = False

                PB_LS_ANGLE.Visible = True
                PB_LS_BEAM.Visible = False

                Panel_Angle.Left = 8
                Panel_Angle.Top = 36

                Panel_BP_Angle.Left = 6
                Panel_BP_Angle.Top = 40

                Panel_CSP_Angle.Left = 4
                Panel_CSP_Angle.Top = 43

                Panel_BSP_Angle.Left = 6
                Panel_BSP_Angle.Top = 47

                Panel_RFPad_Angle.Left = 8
                Panel_RFPad_Angle.Top = 38

                PB_LS_ANGLE.Left = 384
                PB_LS_ANGLE.Top = 2

                PB_LS_ANGLE.Visible = True
                PB_LS_BEAM.Visible = False

                PB_BP_ANGLE.Visible = True
                PB_BP_BEAM.Visible = False

                PB_CSP_ANGLE.Visible = True
                PB_CSP_BEAM.Visible = False

                PB_BOP_ANGLE.Visible = True
                PB_BOP_BEAM.Visible = False

                PB_RFPAD_.Visible = True

                LoadAngleValuesFromModule()

            Case LegSupportState.SupportType.Beam

                ' BEAM PANELS
                Panel_Angle.Visible = False
                Panel_Beam.Visible = True

                Panel_BP_Angle.Visible = False
                Panel_BP_Beam.Visible = True

                Panel_CSP_Angle.Visible = False
                Panel_CSP_Beam.Visible = True

                Panel_BSP_Angle.Visible = False
                Panel_BSP_Beam.Visible = True

                Panel_RFPad_Angle.Visible = False
                Panel_RFPad_Beam.Visible = True

                PB_LS_ANGLE.Visible = False
                PB_LS_BEAM.Visible = True

                Panel_Beam.Left = 8
                Panel_Beam.Top = 36

                Panel_BP_Beam.Left = 6
                Panel_BP_Beam.Top = 40

                Panel_CSP_Beam.Left = 4
                Panel_CSP_Beam.Top = 43

                Panel_BSP_Beam.Left = 6
                Panel_BSP_Beam.Top = 47

                Panel_RFPad_Beam.Left = 8
                Panel_RFPad_Beam.Top = 38

                PB_LS_BEAM.Left = 384
                PB_LS_BEAM.Top = 2

                PB_LS_ANGLE.Visible = False
                PB_LS_BEAM.Visible = True

                PB_BP_ANGLE.Visible = False
                PB_BP_BEAM.Visible = True

                PB_CSP_ANGLE.Visible = False
                PB_CSP_BEAM.Visible = True

                PB_BOP_ANGLE.Visible = False
                PB_BOP_BEAM.Visible = True

                PB_RFPAD_.Visible = True

                LoadBeamValuesFromModule()

        End Select

    End Sub

    Private Sub LoadAngleValuesFromModule()

        ' Base Plate
        txt_BaseP_length.Text = BottomPlateData.BasePlate_LengthMm.ToString()
        txt_BaseP_height.Text = BottomPlateData.BasePlate_LengthMm.ToString()
        txt_BaseP_hole_dia.Text = BottomPlateData.BasePlate_HoleDiaMm.ToString()
        txt_BaseP_hole_length.Text = BottomPlateData.BasePlate_HoleDistMm.ToString()
        txt_BaseP_hole_ht.Text = BottomPlateData.BasePlate_HoleDistMm.ToString()
        txt_BaseP_thk.Text = BottomPlateData.BasePlate_ThicknessMm.ToString()

        ' Bottom Plate
        txt_BottomP_length.Text = BottomPlateData.LengthMm.ToString()
        txt_BottomP_height.Text = BottomPlateData.HeightMm.ToString()
        txt_BottomP_fillet.Text = BottomPlateData.FilletMm.ToString()
        txt_BottomP_thk.Text = BottomPlateData.ThicknessMm.ToString()
        txt_BottomP_arc_radius.Text = BottomPlateData.ArcRadiusMm.ToString()

        ' Leg Support – Angle
        txt_LegS_length.Text = BottomPlateData.LegAngle_LengthMm.ToString()
        txt_LegS_width.Text = BottomPlateData.LegAngle_WidthMm.ToString()
        txt_LegS_thickness.Text = BottomPlateData.LegAngle_ThicknessMm.ToString()
        txt_LegS_Height.Text = BottomPlateData.LegAngle_HeightMm.ToString()

        ' RF Pad
        txt_RFPad_length.Text = BottomPlateData.RFPad_LengthMm.ToString()
        txt_RFPad_height.Text = BottomPlateData.RFPad_HeightMm.ToString()
        txt_RFPad_Thk.Text = BottomPlateData.RFPad_ThicknessMm.ToString()
        txt_RFPad_Fillet.Text = BottomPlateData.RFPad_FilletMm.ToString()

        ' Top Plate
        txt_CSP_B_length.Text = BottomPlateData.BeamTopClosure_LengthMm.ToString()
        txt_CSP_B_height.Text = BottomPlateData.BeamTopClosure_WidthMm.ToString()
        txt_CSP_B_thickness.Text = BottomPlateData.BeamTopClosure_ThicknessMm.ToString()
        txt_CSP_B_arc_radius.Text = BottomPlateData.BeamTopClosure_ArcRadiusMm.ToString()
        txt_CSP_B_fillet.Text = BottomPlateData.BeamTopClosure_FilletMm.ToString()

    End Sub

    Private Sub LoadBeamValuesFromModule()

        ' Beam Leg
        txt_LegS_Sec_Ht.Text = BottomPlateData.LegBeam_SecHeightMm.ToString()
        txt_LegS_Sec_Wh.Text = BottomPlateData.LegBeam_SecWidthMm.ToString()
        txt_LegS_Web_Thk.Text = BottomPlateData.LegBeam_WebThicknessMm.ToString()
        txt_LegS_Flange_Thk.Text = BottomPlateData.LegBeam_FlangeThicknessMm.ToString()
        txt_LegS_Beam_Ht.Text = BottomPlateData.LegBeam_HeightMm.ToString()

        ' Beam Base Plate
        txt_BaseP_B_length.Text = BottomPlateData.BeamBaseP_LengthMm.ToString()
        txt_BaseP_B_height.Text = BottomPlateData.BeamBaseP_WidthMm.ToString()
        txt_BaseP_B_hole_dia.Text = BottomPlateData.BeamBaseP_HoleDiaMm.ToString()
        txt_BaseP_B_hole_length.Text = BottomPlateData.BeamBaseP_HoleDistMm.ToString()
        txt_BaseP_B_hole_height.Text = BottomPlateData.BeamBaseP_HoleHeightMm.ToString()
        txt_BaseP_B_thk.Text = BottomPlateData.BeamBaseP_ThicknessMm.ToString()
        txt_BaseP_B_dist.Text = BottomPlateData.BeamBaseP_DistanceMm.ToString()

        ' Beam Bottom Closure
        txt_BottomP_B_length.Text = BottomPlateData.BeamBottomP_LengthMm.ToString()
        txt_BottomP_B_height.Text = BottomPlateData.BeamBottomP_HeightMm.ToString()
        txt_BottomP_B_thk.Text = BottomPlateData.BeamBottomP_ThicknessMm.ToString()
        txt_BottomP_B_arc_radius.Text = BottomPlateData.BeamBottomP_ArcRadiusMm.ToString()

        ' Beam RF Pad
        txt_RFPad_B_length.Text = BottomPlateData.BeamRFPad_LengthMm.ToString()
        txt_RFPad_B_height.Text = BottomPlateData.BeamRFPad_HeightMm.ToString()
        txt_RFPad_B_Thk.Text = BottomPlateData.BeamRFPad_ThicknessMm.ToString()
        txt_RFPad_B_Fillet.Text = BottomPlateData.BeamRFPad_FilletMm.ToString()

        ' Beam Top Closure
        txt_CSP_B_length.Text = BottomPlateData.BeamTopClosure_LengthMm.ToString()
        txt_CSP_B_height.Text = BottomPlateData.BeamTopClosure_WidthMm.ToString()
        txt_CSP_B_thickness.Text = BottomPlateData.BeamTopClosure_ThicknessMm.ToString()
        txt_CSP_B_arc_radius.Text = BottomPlateData.BeamTopClosure_ArcRadiusMm.ToString()
        txt_CSP_B_fillet.Text = BottomPlateData.BeamTopClosure_FilletMm.ToString()

    End Sub

    Private Sub Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        RemoveHandler LegSupportState.SupportTypeChanged, AddressOf OnSupportTypeChanged

    End Sub

    Private Sub cmb_LegS_Beam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_LegS_Beam.SelectedIndexChanged

        If String.IsNullOrWhiteSpace(cmb_LegS_Beam.Text) Then Exit Sub

        Dim parts() As String = cmb_LegS_Beam.Text.Split(" "c)

        If parts.Length >= 2 Then
            txt_LegS_Sec_Ht.Text = parts(1).Trim()
            txt_LegS_Sec_Wh.Text = parts(1).Trim()
        End If

    End Sub

    Private Sub cmb_LegS_Angle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_LegS_Angle.SelectedIndexChanged

        If String.IsNullOrEmpty(cmb_LegS_Angle.Text) Then Exit Sub

        Dim parts() As String = cmb_LegS_Angle.Text.Split("x"c)

        If parts.Length = 2 Then
            txt_LegS_length.Text = parts(0).Trim()
            txt_LegS_width.Text = parts(1).Trim()
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CB_BP_EDIT_PARAMS.CheckedChanged
        If CB_BP_EDIT_PARAMS.Checked Then

            ''Read Only - ANGLE
            txt_BaseP_length.ReadOnly = False
            txt_BaseP_height.ReadOnly = False
            txt_BaseP_hole_dia.ReadOnly = False
            txt_BaseP_hole_length.ReadOnly = False
            txt_BaseP_hole_ht.ReadOnly = False
            txt_BaseP_thk.ReadOnly = False

            ''Read Only - BEAM
            txt_BaseP_B_length.ReadOnly = False
            txt_BaseP_B_height.ReadOnly = False
            txt_BaseP_B_hole_dia.ReadOnly = False
            txt_BaseP_B_hole_length.ReadOnly = False
            txt_BaseP_B_hole_height.ReadOnly = False
            txt_BaseP_B_thk.ReadOnly = False
            txt_BaseP_B_dist.ReadOnly = False

            ''TextBox Color  - ANGLE
            txt_BaseP_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_hole_dia.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_hole_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_hole_ht.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_thk.BackColor = Color.FromArgb(192, 192, 255)

            ''TextBox Color  - BEAM
            txt_BaseP_B_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_hole_dia.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_hole_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_hole_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_BaseP_B_dist.BackColor = Color.FromArgb(192, 192, 255)

        Else
            ''Read Only - ANGLE
            txt_BaseP_length.ReadOnly = True
            txt_BaseP_height.ReadOnly = True
            txt_BaseP_hole_dia.ReadOnly = True
            txt_BaseP_hole_length.ReadOnly = True
            txt_BaseP_hole_ht.ReadOnly = True
            txt_BaseP_thk.ReadOnly = True

            ''Read Only - BEAM
            txt_BaseP_B_length.ReadOnly = True
            txt_BaseP_B_height.ReadOnly = True
            txt_BaseP_B_hole_dia.ReadOnly = True
            txt_BaseP_B_hole_length.ReadOnly = True
            txt_BaseP_B_hole_height.ReadOnly = True
            txt_BaseP_B_thk.ReadOnly = True
            txt_BaseP_B_dist.ReadOnly = True

            ''TextBox Color  - ANGLE
            txt_BaseP_length.BackColor = Color.Silver
            txt_BaseP_height.BackColor = Color.Silver
            txt_BaseP_hole_dia.BackColor = Color.Silver
            txt_BaseP_hole_length.BackColor = Color.Silver
            txt_BaseP_hole_ht.BackColor = Color.Silver
            txt_BaseP_thk.BackColor = Color.Silver

            ''TextBox Color  - BEAM
            txt_BaseP_B_length.BackColor = Color.Silver
            txt_BaseP_B_height.BackColor = Color.Silver
            txt_BaseP_B_hole_dia.BackColor = Color.Silver
            txt_BaseP_B_hole_length.BackColor = Color.Silver
            txt_BaseP_B_hole_height.BackColor = Color.Silver
            txt_BaseP_B_thk.BackColor = Color.Silver
            txt_BaseP_B_dist.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_CSP_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_CSP_EDIT_PARAMS.CheckedChanged
        If CB_CSP_EDIT_PARAMS.Checked Then

            txt_CSP_length.ReadOnly = False
            txt_CSP_height.ReadOnly = False
            txt_CSP_thickness.ReadOnly = False
            txt_CSP_distance.ReadOnly = False
            txt_CSP_arc_radius.ReadOnly = False
            txt_CSP_fillet.ReadOnly = False

            txt_CSP_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_thickness.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_distance.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_arc_radius.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_fillet.BackColor = Color.FromArgb(192, 192, 255)

            txt_CSP_B_length.ReadOnly = False
            txt_CSP_B_height.ReadOnly = False
            txt_CSP_B_thickness.ReadOnly = False
            txt_CSP_B_arc_radius.ReadOnly = False
            txt_CSP_B_fillet.ReadOnly = False

            txt_CSP_B_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_B_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_B_thickness.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_B_arc_radius.BackColor = Color.FromArgb(192, 192, 255)
            txt_CSP_B_fillet.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_CSP_length.ReadOnly = True
            txt_CSP_height.ReadOnly = True
            txt_CSP_thickness.ReadOnly = True
            txt_CSP_distance.ReadOnly = True
            txt_CSP_arc_radius.ReadOnly = True
            txt_CSP_fillet.ReadOnly = True

            txt_CSP_length.BackColor = Color.Silver
            txt_CSP_height.BackColor = Color.Silver
            txt_CSP_thickness.BackColor = Color.Silver
            txt_CSP_distance.BackColor = Color.Silver
            txt_CSP_arc_radius.BackColor = Color.Silver
            txt_CSP_fillet.BackColor = Color.Silver

            txt_CSP_B_length.ReadOnly = True
            txt_CSP_B_height.ReadOnly = True
            txt_CSP_B_thickness.ReadOnly = True
            txt_CSP_B_arc_radius.ReadOnly = True
            txt_CSP_B_fillet.ReadOnly = True

            txt_CSP_B_length.BackColor = Color.Silver
            txt_CSP_B_height.BackColor = Color.Silver
            txt_CSP_B_thickness.BackColor = Color.Silver
            txt_CSP_B_arc_radius.BackColor = Color.Silver
            txt_CSP_B_fillet.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_BoP_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_BoP_EDIT_PARAMS.CheckedChanged
        If CB_BoP_EDIT_PARAMS.Checked Then

            txt_BottomP_length.ReadOnly = False
            txt_BottomP_height.ReadOnly = False
            txt_BottomP_thk.ReadOnly = False
            txt_BottomP_arc_radius.ReadOnly = False
            txt_BottomP_fillet.ReadOnly = False

            txt_BottomP_B_length.ReadOnly = False
            txt_BottomP_B_height.ReadOnly = False
            txt_BottomP_B_thk.ReadOnly = False
            txt_BottomP_B_arc_radius.ReadOnly = False

            txt_BottomP_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_arc_radius.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_fillet.BackColor = Color.FromArgb(192, 192, 255)

            txt_BottomP_B_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_B_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_B_thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_BottomP_B_arc_radius.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_BottomP_length.ReadOnly = True
            txt_BottomP_height.ReadOnly = True
            txt_BottomP_thk.ReadOnly = True
            txt_BottomP_arc_radius.ReadOnly = True
            txt_BottomP_fillet.ReadOnly = True

            txt_BottomP_B_length.ReadOnly = True
            txt_BottomP_B_height.ReadOnly = True
            txt_BottomP_B_thk.ReadOnly = True
            txt_BottomP_B_arc_radius.ReadOnly = True

            txt_BottomP_length.BackColor = Color.Silver
            txt_BottomP_height.BackColor = Color.Silver
            txt_BottomP_thk.BackColor = Color.Silver
            txt_BottomP_arc_radius.BackColor = Color.Silver
            txt_BottomP_fillet.BackColor = Color.Silver

            txt_BottomP_B_length.BackColor = Color.Silver
            txt_BottomP_B_height.BackColor = Color.Silver
            txt_BottomP_B_thk.BackColor = Color.Silver
            txt_BottomP_B_arc_radius.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_RFPAD_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_RFPAD_EDIT_PARAMS.CheckedChanged
        If CB_RFPAD_EDIT_PARAMS.Checked Then

            txt_RFPad_length.ReadOnly = False
            txt_RFPad_height.ReadOnly = False
            txt_RFPad_Thk.ReadOnly = False
            txt_RFPad_Fillet.ReadOnly = False

            txt_RFPad_B_length.ReadOnly = False
            txt_RFPad_B_height.ReadOnly = False
            txt_RFPad_B_Thk.ReadOnly = False
            txt_RFPad_B_Fillet.ReadOnly = False

            txt_RFPad_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_Thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_Fillet.BackColor = Color.FromArgb(192, 192, 255)

            txt_RFPad_B_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_B_height.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_B_Thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_RFPad_B_Fillet.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_RFPad_length.ReadOnly = True
            txt_RFPad_height.ReadOnly = True
            txt_RFPad_Thk.ReadOnly = True
            txt_RFPad_Fillet.ReadOnly = True

            txt_RFPad_B_length.ReadOnly = True
            txt_RFPad_B_height.ReadOnly = True
            txt_RFPad_B_Thk.ReadOnly = True
            txt_RFPad_B_Fillet.ReadOnly = True

            txt_RFPad_length.BackColor = Color.Silver
            txt_RFPad_height.BackColor = Color.Silver
            txt_RFPad_Thk.BackColor = Color.Silver
            txt_RFPad_Fillet.BackColor = Color.Silver

            txt_RFPad_B_length.BackColor = Color.Silver
            txt_RFPad_B_height.BackColor = Color.Silver
            txt_RFPad_B_Thk.BackColor = Color.Silver
            txt_RFPad_B_Fillet.BackColor = Color.Silver

        End If
    End Sub

    Private Sub SaveForm2ValuesToModule()

        '==============================
        ' ANGLE
        '==============================
        BottomPlateData.BasePlate_LengthMm = Val(txt_BaseP_length.Text)
        BottomPlateData.BasePlate_HeightMm = Val(txt_BaseP_height.Text)
        BottomPlateData.BasePlate_ThicknessMm = Val(txt_BaseP_thk.Text)
        BottomPlateData.BasePlate_HoleDiaMm = Val(txt_BaseP_hole_dia.Text)
        BottomPlateData.BasePlate_HoleDistMm = Val(txt_BaseP_hole_length.Text)

        BottomPlateData.LengthMm = Val(txt_BottomP_length.Text)
        BottomPlateData.HeightMm = Val(txt_BottomP_height.Text)
        BottomPlateData.ThicknessMm = Val(txt_BottomP_thk.Text)
        BottomPlateData.FilletMm = Val(txt_BottomP_fillet.Text)
        BottomPlateData.ArcRadiusMm = Val(txt_BottomP_arc_radius.Text)

        BottomPlateData.RFPad_LengthMm = Val(txt_RFPad_length.Text)
        BottomPlateData.RFPad_HeightMm = Val(txt_RFPad_height.Text)
        BottomPlateData.RFPad_ThicknessMm = Val(txt_RFPad_Thk.Text)
        BottomPlateData.RFPad_FilletMm = Val(txt_RFPad_Fillet.Text)

        BottomPlateData.LegAngle_LengthMm = Val(txt_LegS_length.Text)
        BottomPlateData.LegAngle_WidthMm = Val(txt_LegS_width.Text)
        BottomPlateData.LegAngle_ThicknessMm = Val(txt_LegS_thickness.Text)
        BottomPlateData.LegAngle_HeightMm = Val(txt_LegS_Height.Text)

        '==============================
        ' BEAM
        '==============================
        BottomPlateData.LegBeam_SecHeightMm = Val(txt_LegS_Sec_Ht.Text)
        BottomPlateData.LegBeam_SecWidthMm = Val(txt_LegS_Sec_Wh.Text)
        BottomPlateData.LegBeam_WebThicknessMm = Val(txt_LegS_Web_Thk.Text)
        BottomPlateData.LegBeam_FlangeThicknessMm = Val(txt_LegS_Flange_Thk.Text)
        BottomPlateData.LegBeam_HeightMm = Val(txt_LegS_Beam_Ht.Text)

        BottomPlateData.BeamBaseP_LengthMm = Val(txt_BaseP_B_length.Text)
        BottomPlateData.BeamBaseP_WidthMm = Val(txt_BaseP_B_height.Text)
        BottomPlateData.BeamBaseP_ThicknessMm = Val(txt_BaseP_B_thk.Text)
        BottomPlateData.BeamBaseP_HoleDiaMm = Val(txt_BaseP_B_hole_dia.Text)
        BottomPlateData.BeamBaseP_HoleDistMm = Val(txt_BaseP_B_hole_length.Text)
        BottomPlateData.BeamBaseP_HoleHeightMm = Val(txt_BaseP_B_hole_height.Text)
        BottomPlateData.BeamBaseP_DistanceMm = Val(txt_BaseP_B_dist.Text)

        BottomPlateData.BeamBottomP_LengthMm = Val(txt_BottomP_B_length.Text)
        BottomPlateData.BeamBottomP_HeightMm = Val(txt_BottomP_B_height.Text)
        BottomPlateData.BeamBottomP_ThicknessMm = Val(txt_BottomP_B_thk.Text)
        BottomPlateData.BeamBottomP_ArcRadiusMm = Val(txt_BottomP_B_arc_radius.Text)

        BottomPlateData.BeamRFPad_LengthMm = Val(txt_RFPad_B_length.Text)
        BottomPlateData.BeamRFPad_HeightMm = Val(txt_RFPad_B_height.Text)
        BottomPlateData.BeamRFPad_ThicknessMm = Val(txt_RFPad_B_Thk.Text)
        BottomPlateData.BeamRFPad_FilletMm = Val(txt_RFPad_B_Fillet.Text)

    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) _
Handles txt_BaseP_length.TextChanged, txt_BaseP_height.TextChanged,
        txt_BottomP_length.TextChanged, txt_RFPad_length.TextChanged,
        txt_LegS_length.TextChanged, txt_LegS_Sec_Ht.TextChanged

        If isLoading Then Exit Sub   '🔥 IMPORTANT

        SaveForm2ValuesToModule()

    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveForm2ValuesToModule()
    End Sub

    Private isLoading As Boolean = True

End Class