<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewProjForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewProjForm))
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.Label219 = New System.Windows.Forms.Label()
        Me.txt_Proj_Code = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Proj_Rev = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Proj_Location = New System.Windows.Forms.TextBox()
        Me.btn_proj_dir = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewRevisionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevisionsComparingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox17
        '
        Me.PictureBox17.Image = Global.Project_CD._24._12.My.Resources.Resources.WhatsApp_Image_2026_09_12_at_4_06_14_PM
        Me.PictureBox17.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(475, 118)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox17.TabIndex = 7
        Me.PictureBox17.TabStop = False
        '
        'Label219
        '
        Me.Label219.AutoSize = True
        Me.Label219.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label219.Location = New System.Drawing.Point(66, 131)
        Me.Label219.Name = "Label219"
        Me.Label219.Size = New System.Drawing.Size(91, 19)
        Me.Label219.TabIndex = 37
        Me.Label219.Text = "Project Code"
        '
        'txt_Proj_Code
        '
        Me.txt_Proj_Code.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Proj_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Proj_Code.Location = New System.Drawing.Point(195, 127)
        Me.txt_Proj_Code.Name = "txt_Proj_Code"
        Me.txt_Proj_Code.Size = New System.Drawing.Size(177, 26)
        Me.txt_Proj_Code.TabIndex = 38
        Me.txt_Proj_Code.Text = "TEST"
        Me.txt_Proj_Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(67, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 19)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Revision"
        '
        'txt_Proj_Rev
        '
        Me.txt_Proj_Rev.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Proj_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Proj_Rev.Location = New System.Drawing.Point(195, 168)
        Me.txt_Proj_Rev.Name = "txt_Proj_Rev"
        Me.txt_Proj_Rev.Size = New System.Drawing.Size(177, 26)
        Me.txt_Proj_Rev.TabIndex = 38
        Me.txt_Proj_Rev.Text = "A"
        Me.txt_Proj_Rev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(66, 212)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 19)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Project Directory"
        '
        'txt_Proj_Location
        '
        Me.txt_Proj_Location.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_Proj_Location.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Proj_Location.Location = New System.Drawing.Point(195, 209)
        Me.txt_Proj_Location.Name = "txt_Proj_Location"
        Me.txt_Proj_Location.Size = New System.Drawing.Size(177, 26)
        Me.txt_Proj_Location.TabIndex = 38
        Me.txt_Proj_Location.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btn_proj_dir
        '
        Me.btn_proj_dir.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_proj_dir.Location = New System.Drawing.Point(376, 212)
        Me.btn_proj_dir.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_proj_dir.Name = "btn_proj_dir"
        Me.btn_proj_dir.Size = New System.Drawing.Size(35, 21)
        Me.btn_proj_dir.TabIndex = 39
        Me.btn_proj_dir.Text = "----"
        Me.btn_proj_dir.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btn_proj_dir.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(474, 24)
        Me.MenuStrip1.TabIndex = 40
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.NewRevisionToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.RevisionsComparingToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.NewToolStripMenuItem.Text = "New Project"
        '
        'NewRevisionToolStripMenuItem
        '
        Me.NewRevisionToolStripMenuItem.Name = "NewRevisionToolStripMenuItem"
        Me.NewRevisionToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.NewRevisionToolStripMenuItem.Text = "New Revision"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.OpenToolStripMenuItem.Text = "Open / Load"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'RevisionsComparingToolStripMenuItem
        '
        Me.RevisionsComparingToolStripMenuItem.Name = "RevisionsComparingToolStripMenuItem"
        Me.RevisionsComparingToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.RevisionsComparingToolStripMenuItem.Text = "Revision Comparison"
        '
        'NewProjForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 250)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.btn_proj_dir)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label219)
        Me.Controls.Add(Me.txt_Proj_Location)
        Me.Controls.Add(Me.txt_Proj_Rev)
        Me.Controls.Add(Me.txt_Proj_Code)
        Me.Controls.Add(Me.PictureBox17)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "NewProjForm"
        Me.Text = "NewProjForm"
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox17 As PictureBox
    Friend WithEvents Label219 As Label
    Friend WithEvents txt_Proj_Code As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Proj_Rev As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Proj_Location As TextBox
    Friend WithEvents btn_proj_dir As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewRevisionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RevisionsComparingToolStripMenuItem As ToolStripMenuItem
End Class
