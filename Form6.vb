Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadStiffenerValuesFromModule()

        'Only auto-calc when NOT editing
        If Not CheckBox1.Checked Then
            txt_SP_ID.Text = Form1.txt_Shell_Inside_Dia.Text
            UpdateStiffenerFromDiameter()
        End If

        txt_SP_ID.ReadOnly = True
        txt_SP_THK.ReadOnly = True
        txt_SP_Length.ReadOnly = True
        txt_SP_Height.ReadOnly = True
        txt_SP_ND1.ReadOnly = True
        txt_SP_ND2.ReadOnly = True
        txt_SP_ND3.ReadOnly = True
        txt_SP_ND4.ReadOnly = True
        txt_SP_Min_Dist.ReadOnly = True
        txt_SP_Noz_Rad.ReadOnly = True

        txt_SP_ID.BackColor = Color.Silver
        txt_SP_THK.BackColor = Color.Silver
        txt_SP_Length.BackColor = Color.Silver
        txt_SP_Height.BackColor = Color.Silver
        txt_SP_ND1.BackColor = Color.Silver
        txt_SP_ND2.BackColor = Color.Silver
        txt_SP_ND3.BackColor = Color.Silver
        txt_SP_ND4.BackColor = Color.Silver
        txt_SP_Min_Dist.BackColor = Color.Silver
        txt_SP_Noz_Rad.BackColor = Color.Silver

    End Sub

    Private Sub LoadStiffenerValuesFromModule()

        txt_SP_ID.Text = StiffenerData.SP_ID.ToString()
        txt_SP_THK.Text = StiffenerData.SP_THK.ToString()
        txt_SP_Length.Text = StiffenerData.SP_LENGTH.ToString()
        txt_SP_Height.Text = StiffenerData.SP_HEIGHT.ToString()

        txt_SP_ND1.Text = StiffenerData.SP_ND1.ToString()
        txt_SP_ND2.Text = StiffenerData.SP_ND2.ToString()
        txt_SP_ND3.Text = StiffenerData.SP_ND3.ToString()
        txt_SP_ND4.Text = StiffenerData.SP_ND4.ToString()

        txt_SP_Min_Dist.Text = StiffenerData.SP_MIN_DIST.ToString()
        txt_SP_Noz_Rad.Text = StiffenerData.SP_NOZ_RAD.ToString()

    End Sub

    Public Sub SaveStiffenerValuesToModule()

        StiffenerData.SP_ID = Val(txt_SP_ID.Text)
        StiffenerData.SP_THK = Val(txt_SP_THK.Text)
        StiffenerData.SP_LENGTH = Val(txt_SP_Length.Text)
        StiffenerData.SP_HEIGHT = Val(txt_SP_Height.Text)

        StiffenerData.SP_ND1 = Val(txt_SP_ND1.Text)
        StiffenerData.SP_ND2 = Val(txt_SP_ND2.Text)
        StiffenerData.SP_ND3 = Val(txt_SP_ND3.Text)
        StiffenerData.SP_ND4 = Val(txt_SP_ND4.Text)

        StiffenerData.SP_MIN_DIST = Val(txt_SP_Min_Dist.Text)
        StiffenerData.SP_NOZ_RAD = Val(txt_SP_Noz_Rad.Text)

    End Sub

    Private Sub txt_SP_ID_TextChanged(sender As Object, e As EventArgs) Handles txt_SP_ID.TextChanged

        If isUserEditing OrElse CheckBox1.Checked Then Exit Sub
        UpdateStiffenerFromDiameter()

    End Sub

    Private Sub UpdateStiffenerFromDiameter()

        Dim ID As Integer
        If Not Integer.TryParse(txt_SP_ID.Text, ID) Then Exit Sub

        Dim stiffLength As Double = 0
        Dim stiffHeight As Double = 0
        Dim nozRad As Double = 0
        Dim ND1 As Double = 0
        Dim ND2 As Double = 0
        Dim ND3 As Double = 0
        Dim ND4 As Double = 0
        Dim Min_Dist As Double = 0

        Select Case ID

            Case 1000 To 1099
                stiffLength = 300 : stiffHeight = 300
                nozRad = 325
                ND1 = 84 : ND2 = 100 : ND3 = 40 : ND4 = 38
                Min_Dist = 25

            Case 1100 To 1199
                stiffLength = 300 : stiffHeight = 300
                nozRad = 350
                ND1 = 84 : ND2 = 100 : ND3 = 65 : ND4 = 36
                Min_Dist = 25

            Case 1200 To 1299
                stiffLength = 300 : stiffHeight = 300
                nozRad = 400
                ND1 = 104 : ND2 = 115 : ND3 = 58 : ND4 = 50
                Min_Dist = 25

            Case 1300 To 1399
                stiffLength = 300 : stiffHeight = 300
                nozRad = 400
                ND1 = 104 : ND2 = 150 : ND3 = 58 : ND4 = 100
                Min_Dist = 25

            Case 1400 To 1499
                stiffLength = 300 : stiffHeight = 300
                nozRad = 400
                ND1 = 104 : ND2 = 175 : ND3 = 58 : ND4 = 125
                Min_Dist = 25

            Case 1500 To 1599
                stiffLength = 300 : stiffHeight = 300
                nozRad = 400
                ND1 = 104 : ND2 = 175 : ND3 = 108 : ND4 = 125
                Min_Dist = 25

            Case 1600 To 1699
                stiffLength = 300 : stiffHeight = 300
                nozRad = 450
                ND1 = 104 : ND2 = 175 : ND3 = 118 : ND4 = 100
                Min_Dist = 25

            Case 1700 To 1799
                stiffLength = 300 : stiffHeight = 300
                nozRad = 450
                ND1 = 104 : ND2 = 175 : ND3 = 118 : ND4 = 110
                Min_Dist = 25

            Case 1800 To 1899
                stiffLength = 300 : stiffHeight = 300
                nozRad = 450
                ND1 = 104 : ND2 = 175 : ND3 = 50 : ND4 = 120
                Min_Dist = 25

            Case 1900 To 1999
                stiffLength = 600 : stiffHeight = 600
                nozRad = 600
                ND1 = 104 : ND2 = 175 : ND3 = 123 : ND4 = 155
                Min_Dist = 25

            Case 2000 To 2099
                stiffLength = 400 : stiffHeight = 400
                nozRad = 500
                ND1 = 104 : ND2 = 175 : ND3 = 123 : ND4 = 165
                Min_Dist = 25

            Case 2100 To 2199
                stiffLength = 450 : stiffHeight = 450
                nozRad = 550
                ND1 = 104 : ND2 = 175 : ND3 = 123 : ND4 = 165
                Min_Dist = 25

            Case 2200 To 2299
                stiffLength = 450 : stiffHeight = 450
                nozRad = 550
                ND1 = 104 : ND2 = 175 : ND3 = 123 : ND4 = 175
                Min_Dist = 25

            Case 2300 To 2399
                stiffLength = 500 : stiffHeight = 500
                nozRad = 600
                ND1 = 104 : ND2 = 175 : ND3 = 143 : ND4 = 185
                Min_Dist = 25

            Case 2400 To 2499
                stiffLength = 525 : stiffHeight = 525
                nozRad = 600
                ND1 = 104 : ND2 = 175 : ND3 = 143 : ND4 = 205
                Min_Dist = 25

            Case 2500 To 2599
                stiffLength = 525 : stiffHeight = 525
                nozRad = 625
                ND1 = 104 : ND2 = 175 : ND3 = 143 : ND4 = 200
                Min_Dist = 25

            Case 2600 To 2699
                stiffLength = 550 : stiffHeight = 550
                nozRad = 650
                ND1 = 104 : ND2 = 200 : ND3 = 143 : ND4 = 215
                Min_Dist = 25

            Case 2700 To 2799
                stiffLength = 550 : stiffHeight = 550
                nozRad = 675
                ND1 = 104 : ND2 = 200 : ND3 = 143 : ND4 = 205
                Min_Dist = 25

            Case 2800 To 2899
                stiffLength = 550 : stiffHeight = 550
                nozRad = 675
                ND1 = 104 : ND2 = 200 : ND3 = 143 : ND4 = 225
                Min_Dist = 25

            Case 2900 To 2999
                stiffLength = 550 : stiffHeight = 550
                nozRad = 675
                ND1 = 104 : ND2 = 200 : ND3 = 143 : ND4 = 245
                Min_Dist = 25

            Case 3000
                stiffLength = 550 : stiffHeight = 550
                nozRad = 700
                ND1 = 104 : ND2 = 200 : ND3 = 143 : ND4 = 250
                Min_Dist = 25

        End Select

        txt_SP_Length.Text = stiffLength.ToString()
        txt_SP_Height.Text = stiffHeight.ToString()
        txt_SP_Noz_Rad.Text = nozRad.ToString()
        txt_SP_Min_Dist.Text = Min_Dist.ToString()

        txt_SP_ND1.Text = ND1.ToString()
        txt_SP_ND2.Text = ND2.ToString()
        txt_SP_ND3.Text = ND3.ToString()
        txt_SP_ND4.Text = ND4.ToString()

        '-----------------------------
        ' Update Module Values
        '-----------------------------
        StiffenerData.SP_ID = ID
        StiffenerData.SP_LENGTH = stiffLength
        StiffenerData.SP_HEIGHT = stiffHeight

        StiffenerData.SP_NOZ_RAD = nozRad
        StiffenerData.SP_MIN_DIST = Min_Dist

        StiffenerData.SP_ND1 = ND1
        StiffenerData.SP_ND2 = ND2
        StiffenerData.SP_ND3 = ND3
        StiffenerData.SP_ND4 = ND4

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txt_SP_ID.ReadOnly = False
            txt_SP_THK.ReadOnly = False
            txt_SP_Length.ReadOnly = False
            txt_SP_Height.ReadOnly = False
            txt_SP_ND1.ReadOnly = False
            txt_SP_ND2.ReadOnly = False
            txt_SP_ND3.ReadOnly = False
            txt_SP_ND4.ReadOnly = False
            txt_SP_Min_Dist.ReadOnly = False
            txt_SP_Noz_Rad.ReadOnly = False

            txt_SP_ID.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_THK.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_Length.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_Height.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_ND1.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_ND2.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_ND3.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_ND4.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_Min_Dist.BackColor = Color.FromArgb(192, 192, 255)
            txt_SP_Noz_Rad.BackColor = Color.FromArgb(192, 192, 255)

        Else
            txt_SP_ID.ReadOnly = True
            txt_SP_THK.ReadOnly = True
            txt_SP_Length.ReadOnly = True
            txt_SP_Height.ReadOnly = True
            txt_SP_ND1.ReadOnly = True
            txt_SP_ND2.ReadOnly = True
            txt_SP_ND3.ReadOnly = True
            txt_SP_ND4.ReadOnly = True
            txt_SP_Min_Dist.ReadOnly = True
            txt_SP_Noz_Rad.ReadOnly = True

            txt_SP_ID.BackColor = Color.Silver
            txt_SP_THK.BackColor = Color.Silver
            txt_SP_Length.BackColor = Color.Silver
            txt_SP_Height.BackColor = Color.Silver
            txt_SP_ND1.BackColor = Color.Silver
            txt_SP_ND2.BackColor = Color.Silver
            txt_SP_ND3.BackColor = Color.Silver
            txt_SP_ND4.BackColor = Color.Silver
            txt_SP_Min_Dist.BackColor = Color.Silver
            txt_SP_Noz_Rad.BackColor = Color.Silver
        End If
    End Sub

    'Form6
    Private isUserEditing As Boolean = False

    'Form6
    Private Sub TextBox_Enter(sender As Object, e As EventArgs) _
    Handles txt_SP_Length.Enter, txt_SP_Height.Enter,
            txt_SP_ND1.Enter, txt_SP_ND2.Enter,
            txt_SP_ND3.Enter, txt_SP_ND4.Enter

        isUserEditing = True

    End Sub

    'Form6
    Private Sub TextBox_Leave(sender As Object, e As EventArgs) Handles txt_SP_Length.Leave, txt_SP_Height.Leave,
            txt_SP_ND1.Leave, txt_SP_ND2.Leave,
            txt_SP_ND3.Leave, txt_SP_ND4.Leave

        isUserEditing = False
        SaveStiffenerValuesToModule()

    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_SP_ID.TextChanged, txt_SP_THK.TextChanged,
        txt_SP_Length.TextChanged, txt_SP_Height.TextChanged,
        txt_SP_ND1.TextChanged, txt_SP_ND2.TextChanged,
        txt_SP_ND3.TextChanged, txt_SP_ND4.TextChanged,
        txt_SP_Min_Dist.TextChanged, txt_SP_Noz_Rad.TextChanged

        If CheckBox1.Checked Then
            SaveStiffenerValuesToModule()
        End If

    End Sub


    Private Sub Form6_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If CheckBox1.Checked Then
            SaveStiffenerValuesToModule()
        End If

    End Sub


End Class