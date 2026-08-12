<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_UI))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Rbn_Rect_Vessel = New System.Windows.Forms.RadioButton()
        Me.Rbn_Vert_Vessel = New System.Windows.Forms.RadioButton()
        Me.Rbn_Hor_Vessel = New System.Windows.Forms.RadioButton()
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Rbn_Rect_Vessel)
        Me.Panel1.Controls.Add(Me.Rbn_Vert_Vessel)
        Me.Panel1.Controls.Add(Me.Rbn_Hor_Vessel)
        Me.Panel1.Location = New System.Drawing.Point(2, 66)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(276, 221)
        Me.Panel1.TabIndex = 0
        '
        'Rbn_Rect_Vessel
        '
        Me.Rbn_Rect_Vessel.AutoSize = True
        Me.Rbn_Rect_Vessel.Enabled = False
        Me.Rbn_Rect_Vessel.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Rbn_Rect_Vessel.Location = New System.Drawing.Point(43, 151)
        Me.Rbn_Rect_Vessel.Name = "Rbn_Rect_Vessel"
        Me.Rbn_Rect_Vessel.Size = New System.Drawing.Size(174, 23)
        Me.Rbn_Rect_Vessel.TabIndex = 1
        Me.Rbn_Rect_Vessel.Text = "RECTANGULAR VESSEL"
        Me.Rbn_Rect_Vessel.UseVisualStyleBackColor = True
        '
        'Rbn_Vert_Vessel
        '
        Me.Rbn_Vert_Vessel.AutoSize = True
        Me.Rbn_Vert_Vessel.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Rbn_Vert_Vessel.Location = New System.Drawing.Point(43, 92)
        Me.Rbn_Vert_Vessel.Name = "Rbn_Vert_Vessel"
        Me.Rbn_Vert_Vessel.Size = New System.Drawing.Size(140, 23)
        Me.Rbn_Vert_Vessel.TabIndex = 0
        Me.Rbn_Vert_Vessel.Text = "VERTICAL VESSEL"
        Me.Rbn_Vert_Vessel.UseVisualStyleBackColor = True
        '
        'Rbn_Hor_Vessel
        '
        Me.Rbn_Hor_Vessel.AutoSize = True
        Me.Rbn_Hor_Vessel.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rbn_Hor_Vessel.Location = New System.Drawing.Point(43, 34)
        Me.Rbn_Hor_Vessel.Name = "Rbn_Hor_Vessel"
        Me.Rbn_Hor_Vessel.Size = New System.Drawing.Size(163, 23)
        Me.Rbn_Hor_Vessel.TabIndex = 0
        Me.Rbn_Hor_Vessel.Text = "HORIZONTAL VESSEL"
        Me.Rbn_Hor_Vessel.UseVisualStyleBackColor = True
        '
        'PictureBox17
        '
        Me.PictureBox17.Image = CType(resources.GetObject("PictureBox17.Image"), System.Drawing.Image)
        Me.PictureBox17.Location = New System.Drawing.Point(2, -2)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(276, 75)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox17.TabIndex = 5
        Me.PictureBox17.TabStop = False
        '
        'Main_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 285)
        Me.Controls.Add(Me.PictureBox17)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main_UI"
        Me.Text = "MAIN UI"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Rbn_Vert_Vessel As RadioButton
    Friend WithEvents Rbn_Hor_Vessel As RadioButton
    Friend WithEvents Rbn_Rect_Vessel As RadioButton
    Friend WithEvents PictureBox17 As PictureBox
End Class
