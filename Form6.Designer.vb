<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form6
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form6))
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.PB_LS_BEAM = New System.Windows.Forms.PictureBox()
        Me.Panel_Beam = New System.Windows.Forms.Panel()
        Me.txt_SP_Noz_Rad = New System.Windows.Forms.TextBox()
        Me.txt_SP_Min_Dist = New System.Windows.Forms.TextBox()
        Me.txt_SP_ND4 = New System.Windows.Forms.TextBox()
        Me.txt_SP_ND3 = New System.Windows.Forms.TextBox()
        Me.txt_SP_ND2 = New System.Windows.Forms.TextBox()
        Me.txt_SP_ND1 = New System.Windows.Forms.TextBox()
        Me.txt_SP_Height = New System.Windows.Forms.TextBox()
        Me.txt_SP_Length = New System.Windows.Forms.TextBox()
        Me.txt_SP_THK = New System.Windows.Forms.TextBox()
        Me.txt_SP_ID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.PB_LS_BEAM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Beam.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox17
        '
        Me.PictureBox17.Image = CType(resources.GetObject("PictureBox17.Image"), System.Drawing.Image)
        Me.PictureBox17.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(190, 85)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox17.TabIndex = 79
        Me.PictureBox17.TabStop = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel7.Controls.Add(Me.Label178)
        Me.Panel7.Location = New System.Drawing.Point(190, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(574, 85)
        Me.Panel7.TabIndex = 80
        '
        'Label178
        '
        Me.Label178.AutoSize = True
        Me.Label178.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label178.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label178.ForeColor = System.Drawing.Color.White
        Me.Label178.Location = New System.Drawing.Point(3, 28)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(573, 29)
        Me.Label178.TabIndex = 0
        Me.Label178.Text = "VESSEL / TANK 3D MODEL AND DRAWING AUTOMATION"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Calibri", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(113, 98)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(174, 23)
        Me.Label19.TabIndex = 101
        Me.Label19.Text = "Stiffener Parameters"
        '
        'PB_LS_BEAM
        '
        Me.PB_LS_BEAM.Image = CType(resources.GetObject("PB_LS_BEAM.Image"), System.Drawing.Image)
        Me.PB_LS_BEAM.Location = New System.Drawing.Point(390, 85)
        Me.PB_LS_BEAM.Name = "PB_LS_BEAM"
        Me.PB_LS_BEAM.Size = New System.Drawing.Size(370, 325)
        Me.PB_LS_BEAM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_LS_BEAM.TabIndex = 122
        Me.PB_LS_BEAM.TabStop = False
        '
        'Panel_Beam
        '
        Me.Panel_Beam.Controls.Add(Me.txt_SP_Noz_Rad)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_Min_Dist)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_ND4)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_ND3)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_ND2)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_ND1)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_Height)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_Length)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_THK)
        Me.Panel_Beam.Controls.Add(Me.txt_SP_ID)
        Me.Panel_Beam.Controls.Add(Me.Label9)
        Me.Panel_Beam.Controls.Add(Me.Label6)
        Me.Panel_Beam.Controls.Add(Me.Label8)
        Me.Panel_Beam.Controls.Add(Me.Label7)
        Me.Panel_Beam.Controls.Add(Me.Label5)
        Me.Panel_Beam.Controls.Add(Me.Label4)
        Me.Panel_Beam.Controls.Add(Me.Label3)
        Me.Panel_Beam.Controls.Add(Me.Label2)
        Me.Panel_Beam.Controls.Add(Me.Label1)
        Me.Panel_Beam.Controls.Add(Me.Label33)
        Me.Panel_Beam.Location = New System.Drawing.Point(12, 124)
        Me.Panel_Beam.Name = "Panel_Beam"
        Me.Panel_Beam.Size = New System.Drawing.Size(359, 310)
        Me.Panel_Beam.TabIndex = 123
        '
        'txt_SP_Noz_Rad
        '
        Me.txt_SP_Noz_Rad.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_Noz_Rad.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_Noz_Rad.Location = New System.Drawing.Point(168, 285)
        Me.txt_SP_Noz_Rad.Name = "txt_SP_Noz_Rad"
        Me.txt_SP_Noz_Rad.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_Noz_Rad.TabIndex = 117
        Me.txt_SP_Noz_Rad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_Min_Dist
        '
        Me.txt_SP_Min_Dist.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_Min_Dist.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_Min_Dist.Location = New System.Drawing.Point(168, 255)
        Me.txt_SP_Min_Dist.Name = "txt_SP_Min_Dist"
        Me.txt_SP_Min_Dist.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_Min_Dist.TabIndex = 117
        Me.txt_SP_Min_Dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_ND4
        '
        Me.txt_SP_ND4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_ND4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_ND4.Location = New System.Drawing.Point(168, 225)
        Me.txt_SP_ND4.Name = "txt_SP_ND4"
        Me.txt_SP_ND4.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_ND4.TabIndex = 117
        Me.txt_SP_ND4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_ND3
        '
        Me.txt_SP_ND3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_ND3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_ND3.Location = New System.Drawing.Point(168, 195)
        Me.txt_SP_ND3.Name = "txt_SP_ND3"
        Me.txt_SP_ND3.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_ND3.TabIndex = 117
        Me.txt_SP_ND3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_ND2
        '
        Me.txt_SP_ND2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_ND2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_ND2.Location = New System.Drawing.Point(168, 165)
        Me.txt_SP_ND2.Name = "txt_SP_ND2"
        Me.txt_SP_ND2.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_ND2.TabIndex = 117
        Me.txt_SP_ND2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_ND1
        '
        Me.txt_SP_ND1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_ND1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_ND1.Location = New System.Drawing.Point(168, 135)
        Me.txt_SP_ND1.Name = "txt_SP_ND1"
        Me.txt_SP_ND1.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_ND1.TabIndex = 117
        Me.txt_SP_ND1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_Height
        '
        Me.txt_SP_Height.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_Height.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_Height.Location = New System.Drawing.Point(168, 105)
        Me.txt_SP_Height.Name = "txt_SP_Height"
        Me.txt_SP_Height.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_Height.TabIndex = 117
        Me.txt_SP_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_Length
        '
        Me.txt_SP_Length.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_Length.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_Length.Location = New System.Drawing.Point(168, 75)
        Me.txt_SP_Length.Name = "txt_SP_Length"
        Me.txt_SP_Length.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_Length.TabIndex = 117
        Me.txt_SP_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_THK
        '
        Me.txt_SP_THK.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_SP_THK.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_THK.Location = New System.Drawing.Point(168, 45)
        Me.txt_SP_THK.Name = "txt_SP_THK"
        Me.txt_SP_THK.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_THK.TabIndex = 117
        Me.txt_SP_THK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SP_ID
        '
        Me.txt_SP_ID.BackColor = System.Drawing.Color.Silver
        Me.txt_SP_ID.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SP_ID.Location = New System.Drawing.Point(168, 15)
        Me.txt_SP_ID.Name = "txt_SP_ID"
        Me.txt_SP_ID.Size = New System.Drawing.Size(167, 22)
        Me.txt_SP_ID.TabIndex = 114
        Me.txt_SP_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(22, 288)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 15)
        Me.Label9.TabIndex = 115
        Me.Label9.Text = "Stiffener Min Dist, mm"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 258)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 15)
        Me.Label6.TabIndex = 115
        Me.Label6.Text = "Stiffener Min Dist, mm"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(46, 228)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 15)
        Me.Label8.TabIndex = 115
        Me.Label8.Text = "Stiffener ND4, mm"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(46, 198)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 15)
        Me.Label7.TabIndex = 115
        Me.Label7.Text = "Stiffener ND3, mm"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(46, 168)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 15)
        Me.Label5.TabIndex = 115
        Me.Label5.Text = "Stiffener ND2, mm"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(46, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 15)
        Me.Label4.TabIndex = 115
        Me.Label4.Text = "Stiffener ND1, mm"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 15)
        Me.Label3.TabIndex = 115
        Me.Label3.Text = "Stiffener Height, mm"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(34, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 15)
        Me.Label2.TabIndex = 115
        Me.Label2.Text = "Stiffener Length, mm"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(48, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 15)
        Me.Label1.TabIndex = 115
        Me.Label1.Text = "Stiffener THK, mm"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(75, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(75, 15)
        Me.Label33.TabIndex = 115
        Me.Label33.Text = "Shell ID, mm"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CheckBox1.Location = New System.Drawing.Point(12, 440)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(114, 19)
        Me.CheckBox1.TabIndex = 136
        Me.CheckBox1.Text = "Edit Parameters"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Form6
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 461)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Panel_Beam)
        Me.Controls.Add(Me.PB_LS_BEAM)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.PictureBox17)
        Me.Name = "Form6"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "VESSEL / TANK 3D MODEL & DRAWING AUTOMATION"
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.PB_LS_BEAM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Beam.ResumeLayout(False)
        Me.Panel_Beam.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox17 As PictureBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label178 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents PB_LS_BEAM As PictureBox
    Friend WithEvents Panel_Beam As Panel
    Friend WithEvents txt_SP_Length As TextBox
    Friend WithEvents txt_SP_THK As TextBox
    Friend WithEvents txt_SP_ID As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_SP_Height As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_SP_ND3 As TextBox
    Friend WithEvents txt_SP_ND2 As TextBox
    Friend WithEvents txt_SP_ND1 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents txt_SP_ND4 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_SP_Min_Dist As TextBox
    Friend WithEvents txt_SP_Noz_Rad As TextBox
    Friend WithEvents Label9 As Label
End Class
