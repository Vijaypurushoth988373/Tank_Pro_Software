Public Class Form5

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        isLoading = True

        LoadLugValuesFromModule()
        ApplyLugTypeUI()

        isLoading = False

        txt_LugS_BP_length.ReadOnly = True
        txt_LugS_BP_width.ReadOnly = True
        txt_LugS_BP_thk.ReadOnly = True
        txt_LugS_BP_arc_radius.ReadOnly = True
        txt_LugS_BP_slot_dist.ReadOnly = True
        txt_LugS_BP_slot_ht.ReadOnly = True
        txt_LugS_BP_slot_leng.ReadOnly = True

        txt_LugS_BP1_OD.ReadOnly = True
        txt_LugS_BP1_Arc_Radius.ReadOnly = True
        txt_LugS_BP1_THK.ReadOnly = True
        txt_LugS_BP1_slot_dist.ReadOnly = True
        txt_LugS_BP1_slot_ht.ReadOnly = True
        txt_LugS_BP1_slot_leng.ReadOnly = True

        txt_LugS_RF_Length.ReadOnly = True
        txt_LugS_RF_Height.ReadOnly = True
        txt_LugS_RF_THK.ReadOnly = True
        txt_LugS_RF_Fillet.ReadOnly = True

        txt_LugS_GP_ARC_RADIUS.ReadOnly = True
        txt_LugS_GP_LENGTH.ReadOnly = True
        txt_LugS_GP_HEIGHT.ReadOnly = True
        txt_LugS_GP_THK.ReadOnly = True
        txt_LugS_GP_DIST.ReadOnly = True
        txt_LugS_GP_CUT_LENGTH.ReadOnly = True
        txt_LugS_GP_CUT_HEIGHT.ReadOnly = True

        txt_LugS_TP_ARC_RADIUS.ReadOnly = True
        txt_LugS_TP_LENGTH.ReadOnly = True
        txt_LugS_TP_HEIGHT.ReadOnly = True
        txt_LugS_TP_THK.ReadOnly = True

        txt_LugS_TP1_OD.ReadOnly = True
        txt_LugS_TP1_ID.ReadOnly = True
        txt_LugS_TP1_THK.ReadOnly = True


        txt_LugS_BP_length.BackColor = Color.Silver
        txt_LugS_BP_width.BackColor = Color.Silver
        txt_LugS_BP_thk.BackColor = Color.Silver
        txt_LugS_BP_arc_radius.BackColor = Color.Silver
        txt_LugS_BP_slot_dist.BackColor = Color.Silver
        txt_LugS_BP_slot_ht.BackColor = Color.Silver
        txt_LugS_BP_slot_leng.BackColor = Color.Silver

        txt_LugS_BP1_OD.BackColor = Color.Silver
        txt_LugS_BP1_Arc_Radius.BackColor = Color.Silver
        txt_LugS_BP1_THK.BackColor = Color.Silver
        txt_LugS_BP1_slot_dist.BackColor = Color.Silver
        txt_LugS_BP1_slot_ht.BackColor = Color.Silver
        txt_LugS_BP1_slot_leng.BackColor = Color.Silver

        txt_LugS_RF_Length.BackColor = Color.Silver
        txt_LugS_RF_Height.BackColor = Color.Silver
        txt_LugS_RF_THK.BackColor = Color.Silver
        txt_LugS_RF_Fillet.BackColor = Color.Silver

        txt_LugS_GP_ARC_RADIUS.BackColor = Color.Silver
        txt_LugS_GP_LENGTH.BackColor = Color.Silver
        txt_LugS_GP_HEIGHT.BackColor = Color.Silver
        txt_LugS_GP_THK.BackColor = Color.Silver
        txt_LugS_GP_DIST.BackColor = Color.Silver
        txt_LugS_GP_CUT_LENGTH.BackColor = Color.Silver
        txt_LugS_GP_CUT_HEIGHT.BackColor = Color.Silver

        txt_LugS_TP_ARC_RADIUS.BackColor = Color.Silver
        txt_LugS_TP_LENGTH.BackColor = Color.Silver
        txt_LugS_TP_HEIGHT.BackColor = Color.Silver
        txt_LugS_TP_THK.BackColor = Color.Silver

        txt_LugS_TP1_OD.BackColor = Color.Silver
        txt_LugS_TP1_ID.BackColor = Color.Silver
        txt_LugS_TP1_THK.BackColor = Color.Silver

    End Sub

    Private isLoading As Boolean = True

    Private Sub LoadLugValuesFromModule()

        '========================
        ' BASE PLATE TYPE 1 & 2
        '========================
        txt_LugS_BP_length.Text = LugSupportData.BP_Length.ToString()
        txt_LugS_BP_width.Text = LugSupportData.BP_Width.ToString()
        txt_LugS_BP_thk.Text = LugSupportData.BP_THK.ToString()
        txt_LugS_BP_arc_radius.Text = LugSupportData.BP_ARC_RADIUS.ToString()
        txt_LugS_BP_slot_dist.Text = LugSupportData.BP_SLOT_DIST.ToString()
        txt_LugS_BP_slot_ht.Text = LugSupportData.BP_SLOT_HT.ToString()
        txt_LugS_BP_slot_leng.Text = LugSupportData.BP_SLOT_LENGTH.ToString()

        '========================
        ' RF PAD
        '========================
        txt_LugS_RF_Length.Text = LugSupportData.RF_Length.ToString()
        txt_LugS_RF_Height.Text = LugSupportData.RF_Height.ToString()
        txt_LugS_RF_THK.Text = LugSupportData.RF_THK.ToString()
        txt_LugS_RF_Fillet.Text = LugSupportData.RF_Fillet.ToString()
        txt_LugS_RF_MAT.Text = LugSupportData.RF_MAT

        '========================
        ' GUSSET PLATE 1
        '========================
        txt_LugS_GP_ARC_RADIUS.Text = LugSupportData.GP_ARC_RADIUS.ToString()
        txt_LugS_GP_LENGTH.Text = LugSupportData.GP_LENGTH.ToString()
        txt_LugS_GP_HEIGHT.Text = LugSupportData.GP_HEIGHT.ToString()
        txt_LugS_GP_THK.Text = LugSupportData.GP_THK.ToString()
        txt_LugS_GP_DIST.Text = LugSupportData.GP_DIST.ToString()
        txt_LugS_GP_CUT_LENGTH.Text = LugSupportData.GP_CUT_LENGTH.ToString()
        txt_LugS_GP_CUT_HEIGHT.Text = LugSupportData.GP_CUT_HEIGHT.ToString()
        txt_LugS_GP_MATERIAL.Text = LugSupportData.GP_MATERIAL

        '========================
        ' GUSSET PLATE 2
        '========================
        txt_LugS_GP_ARC_RADIUS.Text = LugSupportData.GP2_ARC_RADIUS.ToString()
        txt_LugS_GP_LENGTH.Text = LugSupportData.GP2_LENGTH.ToString()
        txt_LugS_GP_HEIGHT.Text = LugSupportData.GP2_HEIGHT.ToString()
        txt_LugS_GP_THK.Text = LugSupportData.GP2_THK.ToString()
        txt_LugS_GP_DIST.Text = LugSupportData.GP2_DIST.ToString()
        txt_LugS_GP_CUT_LENGTH.Text = LugSupportData.GP2_CUT_LENGTH.ToString()
        txt_LugS_GP_CUT_HEIGHT.Text = LugSupportData.GP2_CUT_HEIGHT.ToString()
        txt_LugS_GP_MATERIAL.Text = LugSupportData.GP2_MATERIAL

        '========================
        ' TOP PLATE TYPE 1 & 2
        '========================
        txt_LugS_TP_ARC_RADIUS.Text = LugSupportData.TP_ARC_RADIUS.ToString()
        txt_LugS_TP_LENGTH.Text = LugSupportData.TP_LENGTH.ToString()
        txt_LugS_TP_HEIGHT.Text = LugSupportData.TP_HEIGHT.ToString()
        txt_LugS_TP_THK.Text = LugSupportData.TP_THK.ToString()
        txt_LugS_TP_MATERIAL.Text = LugSupportData.TP_MATERIAL


        '========================
        ' TOP PLATE TYPE 3
        '========================
        txt_LugS_TP1_OD.Text = LugSupportData.TP1_OD.ToString()
        txt_LugS_TP1_ID.Text = LugSupportData.TP1_ID.ToString()
        txt_LugS_TP1_THK.Text = LugSupportData.TP1_THK.ToString()
        txt_LugS_TP1_MAT.Text = LugSupportData.TP1_MAT

    End Sub

    Public Sub SaveLugValuesToModule()

        '========================
        ' BASE PLATE TYPE 1 & 2
        '========================
        If txt_LugS_BP_length.Text <> "" Then LugSupportData.BP_Length = Val(txt_LugS_BP_length.Text)
        If txt_LugS_BP_width.Text <> "" Then LugSupportData.BP_Width = Val(txt_LugS_BP_width.Text)
        If txt_LugS_BP_thk.Text <> "" Then LugSupportData.BP_THK = Val(txt_LugS_BP_thk.Text)
        If txt_LugS_BP_arc_radius.Text <> "" Then LugSupportData.BP_ARC_RADIUS = Val(txt_LugS_BP_arc_radius.Text)
        If txt_LugS_BP_slot_dist.Text <> "" Then LugSupportData.BP_SLOT_DIST = Val(txt_LugS_BP_slot_dist.Text)
        If txt_LugS_BP_slot_ht.Text <> "" Then LugSupportData.BP_SLOT_HT = Val(txt_LugS_BP_slot_ht.Text)
        If txt_LugS_BP_slot_leng.Text <> "" Then LugSupportData.BP_SLOT_LENGTH = Val(txt_LugS_BP_slot_leng.Text)

        '========================
        ' BASE PLATE TYPE 3
        '========================
        If txt_LugS_BP1_OD.Text <> "" Then LugSupportData.BP1_OD = Val(txt_LugS_BP1_OD.Text)
        If txt_LugS_BP1_Arc_Radius.Text <> "" Then LugSupportData.BP1_ARC_RADIUS = Val(txt_LugS_BP1_Arc_Radius.Text)
        If txt_LugS_BP1_THK.Text <> "" Then LugSupportData.BP1_THK = Val(txt_LugS_BP1_THK.Text)
        If txt_LugS_BP1_slot_dist.Text <> "" Then LugSupportData.BP1_SLOT_DIST = Val(txt_LugS_BP1_slot_dist.Text)
        If txt_LugS_BP1_slot_ht.Text <> "" Then LugSupportData.BP1_SLOT_HT = Val(txt_LugS_BP1_slot_ht.Text)
        If txt_LugS_BP1_slot_leng.Text <> "" Then LugSupportData.BP1_SLOT_LENGTH = Val(txt_LugS_BP1_slot_leng.Text)

        '========================
        ' RF PAD
        '========================
        If txt_LugS_RF_Length.Text <> "" Then LugSupportData.RF_Length = Val(txt_LugS_RF_Length.Text)
        If txt_LugS_RF_Height.Text <> "" Then LugSupportData.RF_Height = Val(txt_LugS_RF_Height.Text)
        If txt_LugS_RF_THK.Text <> "" Then LugSupportData.RF_THK = Val(txt_LugS_RF_THK.Text)
        If txt_LugS_RF_Fillet.Text <> "" Then LugSupportData.RF_Fillet = Val(txt_LugS_RF_Fillet.Text)

        LugSupportData.RF_MAT = txt_LugS_RF_MAT.Text

        '========================
        ' GUSSET
        '========================
        If txt_LugS_GP_ARC_RADIUS.Text <> "" Then LugSupportData.GP_ARC_RADIUS = Val(txt_LugS_GP_ARC_RADIUS.Text)
        If txt_LugS_GP_LENGTH.Text <> "" Then LugSupportData.GP_LENGTH = Val(txt_LugS_GP_LENGTH.Text)
        If txt_LugS_GP_HEIGHT.Text <> "" Then LugSupportData.GP_HEIGHT = Val(txt_LugS_GP_HEIGHT.Text)
        If txt_LugS_GP_THK.Text <> "" Then LugSupportData.GP_THK = Val(txt_LugS_GP_THK.Text)
        If txt_LugS_GP_DIST.Text <> "" Then LugSupportData.GP_DIST = Val(txt_LugS_GP_DIST.Text)
        If txt_LugS_GP_CUT_LENGTH.Text <> "" Then LugSupportData.GP_CUT_LENGTH = Val(txt_LugS_GP_CUT_LENGTH.Text)
        If txt_LugS_GP_CUT_HEIGHT.Text <> "" Then LugSupportData.GP_CUT_HEIGHT = Val(txt_LugS_GP_CUT_HEIGHT.Text)

        LugSupportData.GP_MATERIAL = txt_LugS_GP_MATERIAL.Text

        '========================
        ' TOP PLATE
        '========================
        If txt_LugS_TP_ARC_RADIUS.Text <> "" Then LugSupportData.TP_ARC_RADIUS = Val(txt_LugS_TP_ARC_RADIUS.Text)
        If txt_LugS_TP_LENGTH.Text <> "" Then LugSupportData.TP_LENGTH = Val(txt_LugS_TP_LENGTH.Text)
        If txt_LugS_TP_HEIGHT.Text <> "" Then LugSupportData.TP_HEIGHT = Val(txt_LugS_TP_HEIGHT.Text)
        If txt_LugS_TP_THK.Text <> "" Then LugSupportData.TP_THK = Val(txt_LugS_TP_THK.Text)

        LugSupportData.TP_MATERIAL = txt_LugS_TP_MATERIAL.Text

        '========================
        ' TOP PLATE TYPE 3
        '========================
        If txt_LugS_TP1_OD.Text <> "" Then LugSupportData.TP1_OD = Val(txt_LugS_TP1_OD.Text)
        If txt_LugS_TP1_ID.Text <> "" Then LugSupportData.TP1_ID = Val(txt_LugS_TP1_ID.Text)
        If txt_LugS_TP1_THK.Text <> "" Then LugSupportData.TP1_THK = Val(txt_LugS_TP1_THK.Text)

        LugSupportData.TP1_MAT = txt_LugS_TP1_MAT.Text

    End Sub

    Public Sub ApplyLugTypeUI()

        'Remove Top Plate tab first
        TP_Top_Plate.Parent = Nothing

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False


        PictureBox1.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False
        PictureBox5.Visible = False

        '========================
        ' TYPE 1
        '========================
        If Form1.Rbn_Lug_type1.Checked Then

            'Base Plate + RF Pad + Gusset
            'No Top Plate
            PictureBox1.Visible = True
            PictureBox3.Visible = True

            Panel1.Visible = True
            Panel2.Visible = False

        End If


        '========================
        ' TYPE 2
        '========================
        If Form1.Rbn_Lug_type2.Checked Then

            'Show Top Plate
            TP_Top_Plate.Parent = TabControl1
            PictureBox1.Visible = True
            PictureBox3.Visible = True

            Panel1.Visible = True
            Panel2.Visible = False

            Panel3.Visible = True
            Panel4.Visible = False

        End If


        '========================
        ' TYPE 3
        '========================
        If Form1.Rbn_Lug_type3.Checked Then

            'Show Top Plate (different parameters)
            TP_Top_Plate.Parent = TabControl1

            PictureBox4.Visible = True
            PictureBox5.Visible = True

            Panel1.Visible = False
            Panel2.Visible = True

            Panel3.Visible = False
            Panel4.Visible = True

        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CB_BP_PARAMS_EDIT.CheckedChanged

        If CB_BP_PARAMS_EDIT.Checked Then
            txt_LugS_BP_length.ReadOnly = False
            txt_LugS_BP_width.ReadOnly = False
            txt_LugS_BP_thk.ReadOnly = False
            txt_LugS_BP_arc_radius.ReadOnly = False
            txt_LugS_BP_slot_dist.ReadOnly = False
            txt_LugS_BP_slot_ht.ReadOnly = False
            txt_LugS_BP_slot_leng.ReadOnly = False

            txt_LugS_BP1_OD.ReadOnly = False
            txt_LugS_BP1_Arc_Radius.ReadOnly = False
            txt_LugS_BP1_THK.ReadOnly = False
            txt_LugS_BP1_slot_dist.ReadOnly = False
            txt_LugS_BP1_slot_ht.ReadOnly = False
            txt_LugS_BP1_slot_leng.ReadOnly = False

            txt_LugS_BP_length.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_width.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_thk.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_arc_radius.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_slot_dist.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_slot_ht.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP_slot_leng.BackColor = Color.FromArgb(192, 192, 255)

            txt_LugS_BP1_OD.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP1_Arc_Radius.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP1_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP1_slot_dist.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP1_slot_ht.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_BP1_slot_leng.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_LugS_BP_length.ReadOnly = True
            txt_LugS_BP_width.ReadOnly = True
            txt_LugS_BP_thk.ReadOnly = True
            txt_LugS_BP_arc_radius.ReadOnly = True
            txt_LugS_BP_slot_dist.ReadOnly = True
            txt_LugS_BP_slot_ht.ReadOnly = True
            txt_LugS_BP_slot_leng.ReadOnly = True

            txt_LugS_BP1_OD.ReadOnly = True
            txt_LugS_BP1_Arc_Radius.ReadOnly = True
            txt_LugS_BP1_THK.ReadOnly = True
            txt_LugS_BP1_slot_dist.ReadOnly = True
            txt_LugS_BP1_slot_ht.ReadOnly = True
            txt_LugS_BP1_slot_leng.ReadOnly = True

            txt_LugS_BP_length.BackColor = Color.Silver
            txt_LugS_BP_width.BackColor = Color.Silver
            txt_LugS_BP_thk.BackColor = Color.Silver
            txt_LugS_BP_arc_radius.BackColor = Color.Silver
            txt_LugS_BP_slot_dist.BackColor = Color.Silver
            txt_LugS_BP_slot_ht.BackColor = Color.Silver
            txt_LugS_BP_slot_leng.BackColor = Color.Silver

            txt_LugS_BP1_OD.BackColor = Color.Silver
            txt_LugS_BP1_Arc_Radius.BackColor = Color.Silver
            txt_LugS_BP1_THK.BackColor = Color.Silver
            txt_LugS_BP1_slot_dist.BackColor = Color.Silver
            txt_LugS_BP1_slot_ht.BackColor = Color.Silver
            txt_LugS_BP1_slot_leng.BackColor = Color.Silver

        End If

    End Sub

    Private Sub CB_RF_PARAMS_EDIT_CheckedChanged(sender As Object, e As EventArgs) Handles CB_RF_PARAMS_EDIT.CheckedChanged

        If CB_RF_PARAMS_EDIT.Checked Then

            txt_LugS_RF_Length.ReadOnly = False
            txt_LugS_RF_Height.ReadOnly = False
            txt_LugS_RF_THK.ReadOnly = False
            txt_LugS_RF_Fillet.ReadOnly = False


            txt_LugS_RF_Length.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_RF_Height.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_RF_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_RF_Fillet.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_LugS_RF_Length.ReadOnly = True
            txt_LugS_RF_Height.ReadOnly = True
            txt_LugS_RF_THK.ReadOnly = True
            txt_LugS_RF_Fillet.ReadOnly = True

            txt_LugS_RF_Length.BackColor = Color.Silver
            txt_LugS_RF_Height.BackColor = Color.Silver
            txt_LugS_RF_THK.BackColor = Color.Silver
            txt_LugS_RF_Fillet.BackColor = Color.Silver

        End If

    End Sub

    Private Sub CB_GP_PARAMS_EDIT_CheckedChanged(sender As Object, e As EventArgs) Handles CB_GP_PARAMS_EDIT.CheckedChanged

        If CB_GP_PARAMS_EDIT.Checked Then

            txt_LugS_GP_ARC_RADIUS.ReadOnly = False
            txt_LugS_GP_LENGTH.ReadOnly = False
            txt_LugS_GP_HEIGHT.ReadOnly = False
            txt_LugS_GP_THK.ReadOnly = False
            txt_LugS_GP_DIST.ReadOnly = False
            txt_LugS_GP_CUT_LENGTH.ReadOnly = False
            txt_LugS_GP_CUT_HEIGHT.ReadOnly = False

            txt_LugS_GP_ARC_RADIUS.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_LENGTH.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_HEIGHT.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_DIST.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_CUT_LENGTH.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_GP_CUT_HEIGHT.BackColor = Color.FromArgb(192, 192, 255)
        Else

            txt_LugS_GP_ARC_RADIUS.ReadOnly = True
            txt_LugS_GP_LENGTH.ReadOnly = True
            txt_LugS_GP_HEIGHT.ReadOnly = True
            txt_LugS_GP_THK.ReadOnly = True
            txt_LugS_GP_DIST.ReadOnly = True
            txt_LugS_GP_CUT_LENGTH.ReadOnly = True
            txt_LugS_GP_CUT_HEIGHT.ReadOnly = True


            txt_LugS_GP_ARC_RADIUS.BackColor = Color.Silver
            txt_LugS_GP_LENGTH.BackColor = Color.Silver
            txt_LugS_GP_HEIGHT.BackColor = Color.Silver
            txt_LugS_GP_THK.BackColor = Color.Silver
            txt_LugS_GP_DIST.BackColor = Color.Silver
            txt_LugS_GP_CUT_LENGTH.BackColor = Color.Silver
            txt_LugS_GP_CUT_HEIGHT.BackColor = Color.Silver

        End If

    End Sub

    Private Sub CB_TP_PARAMS_EDIT_CheckedChanged(sender As Object, e As EventArgs) Handles CB_TP_PARAMS_EDIT.CheckedChanged

        If CB_TP_PARAMS_EDIT.Checked Then

            txt_LugS_TP_ARC_RADIUS.ReadOnly = False
            txt_LugS_TP_LENGTH.ReadOnly = False
            txt_LugS_TP_HEIGHT.ReadOnly = False
            txt_LugS_TP_THK.ReadOnly = False

            txt_LugS_TP1_OD.ReadOnly = False
            txt_LugS_TP1_ID.ReadOnly = False
            txt_LugS_TP1_THK.ReadOnly = False

            txt_LugS_TP_ARC_RADIUS.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_TP_LENGTH.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_TP_HEIGHT.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_TP_THK.BackColor = Color.FromArgb(192, 192, 255)

            txt_LugS_TP1_OD.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_TP1_ID.BackColor = Color.FromArgb(192, 192, 255)
            txt_LugS_TP1_THK.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_LugS_TP_ARC_RADIUS.ReadOnly = True
            txt_LugS_TP_LENGTH.ReadOnly = True
            txt_LugS_TP_HEIGHT.ReadOnly = True
            txt_LugS_TP_THK.ReadOnly = True

            txt_LugS_TP1_OD.ReadOnly = True
            txt_LugS_TP1_ID.ReadOnly = True
            txt_LugS_TP1_THK.ReadOnly = True

            txt_LugS_TP_ARC_RADIUS.BackColor = Color.Silver
            txt_LugS_TP_LENGTH.BackColor = Color.Silver
            txt_LugS_TP_HEIGHT.BackColor = Color.Silver
            txt_LugS_TP_THK.BackColor = Color.Silver

            txt_LugS_TP1_OD.BackColor = Color.Silver
            txt_LugS_TP1_ID.BackColor = Color.Silver
            txt_LugS_TP1_THK.BackColor = Color.Silver

        End If

    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_LugS_BP_length.TextChanged,
        txt_LugS_BP_width.TextChanged,
        txt_LugS_BP_thk.TextChanged,
        txt_LugS_BP_arc_radius.TextChanged,
        txt_LugS_BP_slot_dist.TextChanged,
        txt_LugS_BP_slot_ht.TextChanged,
        txt_LugS_BP_slot_leng.TextChanged,
        txt_LugS_BP1_OD.TextChanged,
        txt_LugS_BP1_Arc_Radius.TextChanged,
        txt_LugS_BP1_THK.TextChanged,
        txt_LugS_BP1_slot_dist.TextChanged,
        txt_LugS_BP1_slot_ht.TextChanged,
        txt_LugS_BP1_slot_leng.TextChanged,
        txt_LugS_RF_Length.TextChanged,
        txt_LugS_RF_Height.TextChanged,
        txt_LugS_RF_THK.TextChanged,
        txt_LugS_RF_Fillet.TextChanged,
        txt_LugS_TP_ARC_RADIUS.TextChanged,
        txt_LugS_TP_LENGTH.TextChanged,
        txt_LugS_TP_HEIGHT.TextChanged,
        txt_LugS_TP_THK.TextChanged,
        txt_LugS_TP1_OD.TextChanged,
        txt_LugS_TP1_ID.TextChanged,
        txt_LugS_TP1_THK.TextChanged

        If isLoading Then Exit Sub

        SaveLugValuesToModule()

    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveLugValuesToModule()
    End Sub

End Class