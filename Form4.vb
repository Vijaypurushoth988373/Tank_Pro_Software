Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form4

    Private isLoading As Boolean = True
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        isLoading = True

        ApplySkirtTypeUI()
        LoadSkirtValuesFromModule()

        isLoading = False

        txt_SkirtS_ID.ReadOnly = True
        txt_SkirtS_THK.ReadOnly = True
        txt_SkirtS_Ht.ReadOnly = True

        txt_SkirtS_BP_OD.ReadOnly = True
        txt_SkirtS_BP_ID.ReadOnly = True
        txt_SkirtS_BP_THK.ReadOnly = True
        txt_SkirtS_BP_ANG.ReadOnly = True
        txt_SkirtS_BP_HOLE_DIA.ReadOnly = True
        txt_SkirtS_BP_HOLE_DIST.ReadOnly = True
        txt_SkirtS_BP_HOLE_NO.ReadOnly = True

        txt_SkirtS_GP_THK.ReadOnly = True
        txt_SkirtS_GP_DIST.ReadOnly = True
        txt_SkirtS_GP_Length.ReadOnly = True
        txt_SkirtS_GP_No_of_Plates.ReadOnly = True

        txt_SkirtS_AL_DIST.ReadOnly = True
        txt_SkirtS_AL_THK.ReadOnly = True
        txt_SkirtS_AL_HOLE_DIA.ReadOnly = True
        txt_SkirtS_AL_NO_LUG.ReadOnly = True

        txt_SkirtS_AL_1_OD.ReadOnly = True
        txt_SkirtS_AL_1_ID.ReadOnly = True
        txt_SkirtS_AL_1_THK.ReadOnly = True
        txt_SkirtS_AL_1_ANG.ReadOnly = True
        txt_SkirtS_AL_1_HOLE_DIA.ReadOnly = True
        txt_SkirtS_AL_1_HOLE_DIST.ReadOnly = True
        txt_SkirtS_AL_1_HOLE_NO.ReadOnly = True


        txt_SkirtS_ID.BackColor = Color.Silver
        txt_SkirtS_THK.BackColor = Color.Silver
        txt_SkirtS_Ht.BackColor = Color.Silver

        txt_SkirtS_BP_OD.BackColor = Color.Silver
        txt_SkirtS_BP_ID.BackColor = Color.Silver
        txt_SkirtS_BP_THK.BackColor = Color.Silver
        txt_SkirtS_BP_ANG.BackColor = Color.Silver
        txt_SkirtS_BP_HOLE_DIA.BackColor = Color.Silver
        txt_SkirtS_BP_HOLE_DIST.BackColor = Color.Silver
        txt_SkirtS_BP_HOLE_NO.BackColor = Color.Silver

        txt_SkirtS_GP_THK.BackColor = Color.Silver
        txt_SkirtS_GP_DIST.BackColor = Color.Silver
        txt_SkirtS_GP_Length.BackColor = Color.Silver
        txt_SkirtS_GP_No_of_Plates.BackColor = Color.Silver

        txt_SkirtS_AL_DIST.BackColor = Color.Silver
        txt_SkirtS_AL_THK.BackColor = Color.Silver
        txt_SkirtS_AL_HOLE_DIA.BackColor = Color.Silver
        txt_SkirtS_AL_NO_LUG.BackColor = Color.Silver

        txt_SkirtS_AL_1_OD.BackColor = Color.Silver
        txt_SkirtS_AL_1_ID.BackColor = Color.Silver
        txt_SkirtS_AL_1_THK.BackColor = Color.Silver
        txt_SkirtS_AL_1_ANG.BackColor = Color.Silver
        txt_SkirtS_AL_1_HOLE_DIA.BackColor = Color.Silver
        txt_SkirtS_AL_1_HOLE_DIST.BackColor = Color.Silver
        txt_SkirtS_AL_1_HOLE_NO.BackColor = Color.Silver

    End Sub

    '==================================================
    ' LOAD ALL SKIRT VALUES FROM MODULE
    '==================================================
    Private Sub LoadSkirtValuesFromModule()

        '========================
        ' SKIRT
        '========================
        txt_SkirtS_ID.Text = SkirtSupportData.Skirt_ID.ToString()
        txt_SkirtS_THK.Text = SkirtSupportData.Skirt_THK.ToString()
        txt_SkirtS_Ht.Text = SkirtSupportData.Skirt_HT.ToString()


        '========================
        ' BASE PLATE
        '========================
        txt_SkirtS_BP_OD.Text = SkirtSupportData.BP_OD.ToString()
        txt_SkirtS_BP_ID.Text = SkirtSupportData.BP_ID.ToString()
        txt_SkirtS_BP_THK.Text = SkirtSupportData.BP_THK.ToString()
        txt_SkirtS_BP_ANG.Text = SkirtSupportData.BP_ANG.ToString()
        txt_SkirtS_BP_HOLE_DIA.Text = SkirtSupportData.BP_HOLE_DIA.ToString()
        txt_SkirtS_BP_HOLE_DIST.Text = SkirtSupportData.BP_HOLE_DIST.ToString()
        txt_SkirtS_BP_HOLE_NO.Text = SkirtSupportData.BP_HOLE_NO.ToString()


        '========================
        ' GUSSET PLATE
        '========================
        txt_SkirtS_GP_THK.Text = SkirtSupportData.GP_THK.ToString()
        txt_SkirtS_GP_DIST.Text = SkirtSupportData.GP_DIST.ToString()
        txt_SkirtS_GP_Length.Text = SkirtSupportData.GP_LENGTH.ToString()
        txt_SkirtS_GP_No_of_Plates.Text = SkirtSupportData.GP_COUNT.ToString()


        '========================
        ' ANCHOR LUG TYPE 1
        '========================
        txt_SkirtS_AL_DIST.Text = SkirtSupportData.AL_DIST.ToString()
        txt_SkirtS_AL_THK.Text = SkirtSupportData.AL_THK.ToString()
        txt_SkirtS_AL_HOLE_DIA.Text = SkirtSupportData.AL_HOLE_DIA.ToString()
        txt_SkirtS_AL_NO_LUG.Text = SkirtSupportData.AL_NO_LUG.ToString()


        '========================
        ' ANCHOR LUG TYPE 2
        '========================
        txt_SkirtS_AL_1_OD.Text = SkirtSupportData.AL1_OD.ToString()
        txt_SkirtS_AL_1_ID.Text = SkirtSupportData.AL1_ID.ToString()
        txt_SkirtS_AL_1_THK.Text = SkirtSupportData.AL1_THK.ToString()
        txt_SkirtS_AL_1_ANG.Text = SkirtSupportData.AL1_ANG.ToString()
        txt_SkirtS_AL_1_HOLE_DIA.Text = SkirtSupportData.AL1_HOLE_DIA.ToString()
        txt_SkirtS_AL_1_HOLE_DIST.Text = SkirtSupportData.AL1_HOLE_DIST.ToString()
        txt_SkirtS_AL_1_HOLE_NO.Text = SkirtSupportData.AL1_HOLE_NO.ToString()

    End Sub

    '==================================================
    ' SAVE VALUES TO MODULE
    '==================================================
    Public Sub SaveSkirtValuesToModule()

        '========================
        ' SKIRT
        '========================
        SkirtSupportData.Skirt_ID = Val(txt_SkirtS_ID.Text)
        SkirtSupportData.Skirt_THK = Val(txt_SkirtS_THK.Text)
        SkirtSupportData.Skirt_HT = Val(txt_SkirtS_Ht.Text)


        '========================
        ' BASE PLATE
        '========================
        SkirtSupportData.BP_OD = Val(txt_SkirtS_BP_OD.Text)
        SkirtSupportData.BP_ID = Val(txt_SkirtS_BP_ID.Text)
        SkirtSupportData.BP_THK = Val(txt_SkirtS_BP_THK.Text)
        SkirtSupportData.BP_ANG = Val(txt_SkirtS_BP_ANG.Text)
        SkirtSupportData.BP_HOLE_DIA = Val(txt_SkirtS_BP_HOLE_DIA.Text)
        SkirtSupportData.BP_HOLE_DIST = Val(txt_SkirtS_BP_HOLE_DIST.Text)
        SkirtSupportData.BP_HOLE_NO = Val(txt_SkirtS_BP_HOLE_NO.Text)


        '========================
        ' GUSSET PLATE
        '========================
        SkirtSupportData.GP_THK = Val(txt_SkirtS_GP_THK.Text)
        SkirtSupportData.GP_DIST = Val(txt_SkirtS_GP_DIST.Text)
        SkirtSupportData.GP_LENGTH = Val(txt_SkirtS_GP_Length.Text)
        SkirtSupportData.GP_COUNT = Val(txt_SkirtS_GP_No_of_Plates.Text)


        '========================
        ' ANCHOR LUG TYPE 1
        '========================
        SkirtSupportData.AL_DIST = Val(txt_SkirtS_AL_DIST.Text)
        SkirtSupportData.AL_THK = Val(txt_SkirtS_AL_THK.Text)
        SkirtSupportData.AL_HOLE_DIA = Val(txt_SkirtS_AL_HOLE_DIA.Text)
        SkirtSupportData.AL_NO_LUG = Val(txt_SkirtS_AL_NO_LUG.Text)


        '========================
        ' ANCHOR LUG TYPE 2
        '========================
        SkirtSupportData.AL1_OD = Val(txt_SkirtS_AL_1_OD.Text)
        SkirtSupportData.AL1_ID = Val(txt_SkirtS_AL_1_ID.Text)
        SkirtSupportData.AL1_THK = Val(txt_SkirtS_AL_1_THK.Text)
        SkirtSupportData.AL1_ANG = Val(txt_SkirtS_AL_1_ANG.Text)
        SkirtSupportData.AL1_HOLE_DIA = Val(txt_SkirtS_AL_1_HOLE_DIA.Text)
        SkirtSupportData.AL1_HOLE_DIST = Val(txt_SkirtS_AL_1_HOLE_DIST.Text)
        SkirtSupportData.AL1_HOLE_NO = Val(txt_SkirtS_AL_1_HOLE_NO.Text)

    End Sub

    Public Sub ApplySkirtTypeUI()

        'Hide optional tabs first
        TP_Gusset_Plate.Parent = Nothing
        TP_Anchor_Lug.Parent = Nothing

        'Hide anchor UI
        Panel1.Visible = False
        Panel2.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False


        '========================
        ' TYPE - 1
        '========================
        If Form1.Rbn_SS_Type_1.Checked Then

            'Skirt + Base Plate only


            '========================
            ' TYPE - 2
            '========================
        ElseIf Form1.Rbn_SS_Type_2.Checked Then

            TP_Gusset_Plate.Parent = TabControl1


            '========================
            ' TYPE - 3
            '========================
        ElseIf Form1.Rbn_SS_Type_3.Checked Then

            TP_Gusset_Plate.Parent = TabControl1
            TP_Anchor_Lug.Parent = TabControl1

            'Anchor Lug Type-3 UI
            Panel1.Visible = True
            PictureBox3.Visible = True


            '========================
            ' TYPE - 4
            '========================
        ElseIf Form1.Rbn_SS_Type_4.Checked Then

            TP_Gusset_Plate.Parent = TabControl1
            TP_Anchor_Lug.Parent = TabControl1

            'Anchor Lug Type-4 UI
            Panel2.Visible = True
            PictureBox4.Visible = True

        End If

    End Sub

    Private Sub CB_BP_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_BP_EDIT_PARAMS.CheckedChanged
        If CB_BP_EDIT_PARAMS.Checked Then

            txt_SkirtS_BP_OD.ReadOnly = False
            txt_SkirtS_BP_ID.ReadOnly = False
            txt_SkirtS_BP_THK.ReadOnly = False
            txt_SkirtS_BP_ANG.ReadOnly = False
            txt_SkirtS_BP_HOLE_DIA.ReadOnly = False
            txt_SkirtS_BP_HOLE_DIST.ReadOnly = False
            txt_SkirtS_BP_HOLE_NO.ReadOnly = False

            txt_SkirtS_BP_OD.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_ID.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_ANG.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_HOLE_DIA.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_HOLE_DIST.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_BP_HOLE_NO.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_SkirtS_BP_OD.ReadOnly = True
            txt_SkirtS_BP_ID.ReadOnly = True
            txt_SkirtS_BP_THK.ReadOnly = True
            txt_SkirtS_BP_ANG.ReadOnly = True
            txt_SkirtS_BP_HOLE_DIA.ReadOnly = True
            txt_SkirtS_BP_HOLE_DIST.ReadOnly = True
            txt_SkirtS_BP_HOLE_NO.ReadOnly = True

            txt_SkirtS_BP_OD.BackColor = Color.Silver
            txt_SkirtS_BP_ID.BackColor = Color.Silver
            txt_SkirtS_BP_THK.BackColor = Color.Silver
            txt_SkirtS_BP_ANG.BackColor = Color.Silver
            txt_SkirtS_BP_HOLE_DIA.BackColor = Color.Silver
            txt_SkirtS_BP_HOLE_DIST.BackColor = Color.Silver
            txt_SkirtS_BP_HOLE_NO.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_GP_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_GP_EDIT_PARAMS.CheckedChanged
        If CB_GP_EDIT_PARAMS.Checked Then

            txt_SkirtS_GP_THK.ReadOnly = False
            txt_SkirtS_GP_DIST.ReadOnly = False
            txt_SkirtS_GP_Length.ReadOnly = False
            txt_SkirtS_GP_No_of_Plates.ReadOnly = False

            txt_SkirtS_GP_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_GP_DIST.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_GP_Length.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_GP_No_of_Plates.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_SkirtS_GP_THK.ReadOnly = True
            txt_SkirtS_GP_DIST.ReadOnly = True
            txt_SkirtS_GP_Length.ReadOnly = True
            txt_SkirtS_GP_No_of_Plates.ReadOnly = True

            txt_SkirtS_GP_THK.BackColor = Color.Silver
            txt_SkirtS_GP_DIST.BackColor = Color.Silver
            txt_SkirtS_GP_Length.BackColor = Color.Silver
            txt_SkirtS_GP_No_of_Plates.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_AL_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_AL_EDIT_PARAMS.CheckedChanged
        If CB_AL_EDIT_PARAMS.Checked Then

            txt_SkirtS_AL_DIST.ReadOnly = False
            txt_SkirtS_AL_THK.ReadOnly = False
            txt_SkirtS_AL_HOLE_DIA.ReadOnly = False
            txt_SkirtS_AL_NO_LUG.ReadOnly = False

            txt_SkirtS_AL_1_OD.ReadOnly = False
            txt_SkirtS_AL_1_ID.ReadOnly = False
            txt_SkirtS_AL_1_THK.ReadOnly = False
            txt_SkirtS_AL_1_ANG.ReadOnly = False
            txt_SkirtS_AL_1_HOLE_DIA.ReadOnly = False
            txt_SkirtS_AL_1_HOLE_DIST.ReadOnly = False
            txt_SkirtS_AL_1_HOLE_NO.ReadOnly = False

            txt_SkirtS_AL_DIST.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_HOLE_DIA.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_NO_LUG.BackColor = Color.FromArgb(192, 192, 255)

            txt_SkirtS_AL_1_OD.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_ID.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_ANG.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_HOLE_DIA.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_HOLE_DIST.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_AL_1_HOLE_NO.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_SkirtS_AL_DIST.ReadOnly = True
            txt_SkirtS_AL_THK.ReadOnly = True
            txt_SkirtS_AL_HOLE_DIA.ReadOnly = True
            txt_SkirtS_AL_NO_LUG.ReadOnly = True

            txt_SkirtS_AL_1_OD.ReadOnly = True
            txt_SkirtS_AL_1_ID.ReadOnly = True
            txt_SkirtS_AL_1_THK.ReadOnly = True
            txt_SkirtS_AL_1_ANG.ReadOnly = True
            txt_SkirtS_AL_1_HOLE_DIA.ReadOnly = True
            txt_SkirtS_AL_1_HOLE_DIST.ReadOnly = True
            txt_SkirtS_AL_1_HOLE_NO.ReadOnly = True

            txt_SkirtS_AL_DIST.BackColor = Color.Silver
            txt_SkirtS_AL_THK.BackColor = Color.Silver
            txt_SkirtS_AL_HOLE_DIA.BackColor = Color.Silver
            txt_SkirtS_AL_NO_LUG.BackColor = Color.Silver

            txt_SkirtS_AL_1_OD.BackColor = Color.Silver
            txt_SkirtS_AL_1_ID.BackColor = Color.Silver
            txt_SkirtS_AL_1_THK.BackColor = Color.Silver
            txt_SkirtS_AL_1_ANG.BackColor = Color.Silver
            txt_SkirtS_AL_1_HOLE_DIA.BackColor = Color.Silver
            txt_SkirtS_AL_1_HOLE_DIST.BackColor = Color.Silver
            txt_SkirtS_AL_1_HOLE_NO.BackColor = Color.Silver

        End If
    End Sub

    Private Sub CB_SKIRT_EDIT_PARAMS_CheckedChanged(sender As Object, e As EventArgs) Handles CB_SKIRT_EDIT_PARAMS.CheckedChanged
        If CB_SKIRT_EDIT_PARAMS.Checked Then
            txt_SkirtS_ID.ReadOnly = False
            txt_SkirtS_THK.ReadOnly = False
            txt_SkirtS_Ht.ReadOnly = False

            txt_SkirtS_ID.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SkirtS_Ht.BackColor = Color.FromArgb(192, 192, 255)

        Else

            txt_SkirtS_ID.ReadOnly = True
            txt_SkirtS_THK.ReadOnly = True
            txt_SkirtS_Ht.ReadOnly = True

            txt_SkirtS_ID.BackColor = Color.Silver
            txt_SkirtS_THK.BackColor = Color.Silver
            txt_SkirtS_Ht.BackColor = Color.Silver

        End If
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) _
    Handles txt_SkirtS_ID.TextChanged,
            txt_SkirtS_THK.TextChanged,
            txt_SkirtS_Ht.TextChanged,
            txt_SkirtS_BP_OD.TextChanged,
            txt_SkirtS_BP_ID.TextChanged,
            txt_SkirtS_BP_THK.TextChanged,
            txt_SkirtS_BP_ANG.TextChanged,
            txt_SkirtS_BP_HOLE_DIA.TextChanged,
            txt_SkirtS_BP_HOLE_DIST.TextChanged,
            txt_SkirtS_BP_HOLE_NO.TextChanged,
            txt_SkirtS_GP_THK.TextChanged,
            txt_SkirtS_GP_DIST.TextChanged,
            txt_SkirtS_GP_Length.TextChanged,
            txt_SkirtS_GP_No_of_Plates.TextChanged,
            txt_SkirtS_AL_DIST.TextChanged,
            txt_SkirtS_AL_THK.TextChanged,
            txt_SkirtS_AL_HOLE_DIA.TextChanged,
            txt_SkirtS_AL_NO_LUG.TextChanged,
            txt_SkirtS_AL_1_OD.TextChanged,
            txt_SkirtS_AL_1_ID.TextChanged,
            txt_SkirtS_AL_1_THK.TextChanged,
            txt_SkirtS_AL_1_ANG.TextChanged,
            txt_SkirtS_AL_1_HOLE_DIA.TextChanged,
            txt_SkirtS_AL_1_HOLE_DIST.TextChanged,
            txt_SkirtS_AL_1_HOLE_NO.TextChanged

        If isLoading Then Exit Sub

        SaveSkirtValuesToModule()

    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveSkirtValuesToModule()
    End Sub


End Class