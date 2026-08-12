<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Client_UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Client_UI))
        Me.Rbn_Aramco = New System.Windows.Forms.RadioButton()
        Me.Rbn_Adnoc = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Rbn_Qatar = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Rbn_Aramco
        '
        Me.Rbn_Aramco.AutoSize = True
        Me.Rbn_Aramco.Font = New System.Drawing.Font("Calibri", 20.25!)
        Me.Rbn_Aramco.ForeColor = System.Drawing.Color.SeaGreen
        Me.Rbn_Aramco.Location = New System.Drawing.Point(16, 47)
        Me.Rbn_Aramco.Name = "Rbn_Aramco"
        Me.Rbn_Aramco.Size = New System.Drawing.Size(135, 37)
        Me.Rbn_Aramco.TabIndex = 0
        Me.Rbn_Aramco.Text = "ARAMCO"
        Me.Rbn_Aramco.UseVisualStyleBackColor = True
        '
        'Rbn_Adnoc
        '
        Me.Rbn_Adnoc.AutoSize = True
        Me.Rbn_Adnoc.BackColor = System.Drawing.SystemColors.Control
        Me.Rbn_Adnoc.Font = New System.Drawing.Font("Calibri", 20.25!)
        Me.Rbn_Adnoc.ForeColor = System.Drawing.Color.SeaGreen
        Me.Rbn_Adnoc.Location = New System.Drawing.Point(16, 153)
        Me.Rbn_Adnoc.Name = "Rbn_Adnoc"
        Me.Rbn_Adnoc.Size = New System.Drawing.Size(115, 37)
        Me.Rbn_Adnoc.TabIndex = 0
        Me.Rbn_Adnoc.Text = "ADNOC"
        Me.Rbn_Adnoc.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Rbn_Qatar)
        Me.Panel1.Controls.Add(Me.Rbn_Aramco)
        Me.Panel1.Controls.Add(Me.Rbn_Adnoc)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Location = New System.Drawing.Point(1, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(488, 334)
        Me.Panel1.TabIndex = 1
        '
        'Rbn_Qatar
        '
        Me.Rbn_Qatar.AutoSize = True
        Me.Rbn_Qatar.BackColor = System.Drawing.SystemColors.Control
        Me.Rbn_Qatar.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rbn_Qatar.ForeColor = System.Drawing.Color.SeaGreen
        Me.Rbn_Qatar.Location = New System.Drawing.Point(16, 260)
        Me.Rbn_Qatar.Name = "Rbn_Qatar"
        Me.Rbn_Qatar.Size = New System.Drawing.Size(107, 37)
        Me.Rbn_Qatar.TabIndex = 1
        Me.Rbn_Qatar.Text = "QATAR"
        Me.Rbn_Qatar.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Project_CD._24._12.My.Resources.Resources.Saudi_Aramco_logo
        Me.PictureBox1.Location = New System.Drawing.Point(175, 14)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(308, 97)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Project_CD._24._12.My.Resources.Resources.ADNOC_Logo
        Me.PictureBox2.Location = New System.Drawing.Point(175, 119)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(308, 98)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.Project_CD._24._12.My.Resources.Resources.Qatar_Logo
        Me.PictureBox3.Location = New System.Drawing.Point(175, 223)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(308, 108)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 4
        Me.PictureBox3.TabStop = False
        '
        'PictureBox17
        '
        Me.PictureBox17.Image = CType(resources.GetObject("PictureBox17.Image"), System.Drawing.Image)
        Me.PictureBox17.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(488, 91)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox17.TabIndex = 6
        Me.PictureBox17.TabStop = False
        '
        'Client_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 418)
        Me.Controls.Add(Me.PictureBox17)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Client_UI"
        Me.Text = "CLIENT UI"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Rbn_Aramco As RadioButton
    Friend WithEvents Rbn_Adnoc As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox17 As PictureBox
    Friend WithEvents Rbn_Qatar As RadioButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
End Class
