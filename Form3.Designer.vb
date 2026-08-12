<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DGV_Stiffener = New System.Windows.Forms.DataGridView()
        Me.Nozzle_Mark_Stiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Height_Stiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dist_Stiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Angle_Stiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Number_Stiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ANGLE_BW_STIFF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Direction_Stiff = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.DGV_Stiffener, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DGV_Stiffener)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1573, 340)
        Me.Panel1.TabIndex = 0
        '
        'DGV_Stiffener
        '
        Me.DGV_Stiffener.AllowUserToAddRows = False
        Me.DGV_Stiffener.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Stiffener.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nozzle_Mark_Stiff, Me.Height_Stiff, Me.Dist_Stiff, Me.Angle_Stiff, Me.Number_Stiff, Me.ANGLE_BW_STIFF, Me.Direction_Stiff})
        Me.DGV_Stiffener.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DGV_Stiffener.GridColor = System.Drawing.SystemColors.Control
        Me.DGV_Stiffener.Location = New System.Drawing.Point(0, 0)
        Me.DGV_Stiffener.Name = "DGV_Stiffener"
        Me.DGV_Stiffener.Size = New System.Drawing.Size(735, 330)
        Me.DGV_Stiffener.TabIndex = 104
        '
        'Nozzle_Mark_Stiff
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nozzle_Mark_Stiff.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nozzle_Mark_Stiff.HeaderText = "NOZZLE MARK"
        Me.Nozzle_Mark_Stiff.MinimumWidth = 7
        Me.Nozzle_Mark_Stiff.Name = "Nozzle_Mark_Stiff"
        Me.Nozzle_Mark_Stiff.Width = 110
        '
        'Height_Stiff
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Height_Stiff.DefaultCellStyle = DataGridViewCellStyle2
        Me.Height_Stiff.HeaderText = "HEIGHT"
        Me.Height_Stiff.Name = "Height_Stiff"
        Me.Height_Stiff.Width = 70
        '
        'Dist_Stiff
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dist_Stiff.DefaultCellStyle = DataGridViewCellStyle3
        Me.Dist_Stiff.HeaderText = "DISTANCE"
        Me.Dist_Stiff.Name = "Dist_Stiff"
        Me.Dist_Stiff.Width = 80
        '
        'Angle_Stiff
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Angle_Stiff.DefaultCellStyle = DataGridViewCellStyle4
        Me.Angle_Stiff.HeaderText = "ANGLE"
        Me.Angle_Stiff.Name = "Angle_Stiff"
        Me.Angle_Stiff.Width = 70
        '
        'Number_Stiff
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Number_Stiff.DefaultCellStyle = DataGridViewCellStyle5
        Me.Number_Stiff.HeaderText = "NO OF STIFFENER"
        Me.Number_Stiff.Name = "Number_Stiff"
        Me.Number_Stiff.Width = 130
        '
        'ANGLE_BW_STIFF
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ANGLE_BW_STIFF.DefaultCellStyle = DataGridViewCellStyle6
        Me.ANGLE_BW_STIFF.HeaderText = "ANGLE BW STIFFENER"
        Me.ANGLE_BW_STIFF.Name = "ANGLE_BW_STIFF"
        Me.ANGLE_BW_STIFF.Width = 150
        '
        'Direction_Stiff
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Direction_Stiff.DefaultCellStyle = DataGridViewCellStyle7
        Me.Direction_Stiff.HeaderText = "DIRECTION"
        Me.Direction_Stiff.Items.AddRange(New Object() {"NORMAL", "FLIP"})
        Me.Direction_Stiff.Name = "Direction_Stiff"
        Me.Direction_Stiff.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Direction_Stiff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Direction_Stiff.Width = 80
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1315, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(244, 330)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 103
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1040, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(275, 330)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 102
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(732, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(310, 330)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 101
        Me.PictureBox2.TabStop = False
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1560, 332)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form3"
        Me.Text = "STIFFENER DETAILS"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DGV_Stiffener, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents DGV_Stiffener As DataGridView
    Friend WithEvents Nozzle_Mark_Stiff As DataGridViewTextBoxColumn
    Friend WithEvents Height_Stiff As DataGridViewTextBoxColumn
    Friend WithEvents Dist_Stiff As DataGridViewTextBoxColumn
    Friend WithEvents Angle_Stiff As DataGridViewTextBoxColumn
    Friend WithEvents Number_Stiff As DataGridViewTextBoxColumn
    Friend WithEvents ANGLE_BW_STIFF As DataGridViewTextBoxColumn
    Friend WithEvents Direction_Stiff As DataGridViewComboBoxColumn
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
