<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReporteCardexForm
    Inherits ComponentesSolucion2008.plantillaForm1

    'Form invalida a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReporteCardexForm))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbObras = New System.Windows.Forms.ComboBox
        Me.cbAlmacen = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dgCardex = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgInsumos = New System.Windows.Forms.DataGridView
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtInsumo = New System.Windows.Forms.TextBox
        Me.btnVis = New ComponentesSolucion2008.BottomSSP(Me.components)
        CType(Me.dgCardex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgInsumos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(1185, 23)
        Me.lblTitulo.Text = "Reporte de Cardex de Insumos"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Ubicación:"
        '
        'cbObras
        '
        Me.cbObras.FormattingEnabled = True
        Me.cbObras.Location = New System.Drawing.Point(94, 26)
        Me.cbObras.Name = "cbObras"
        Me.cbObras.Size = New System.Drawing.Size(366, 21)
        Me.cbObras.TabIndex = 4
        '
        'cbAlmacen
        '
        Me.cbAlmacen.FormattingEnabled = True
        Me.cbAlmacen.Location = New System.Drawing.Point(523, 26)
        Me.cbAlmacen.Name = "cbAlmacen"
        Me.cbAlmacen.Size = New System.Drawing.Size(164, 21)
        Me.cbAlmacen.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(466, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Almacen"
        '
        'dgCardex
        '
        Me.dgCardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCardex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgCardex.Location = New System.Drawing.Point(3, 16)
        Me.dgCardex.Name = "dgCardex"
        Me.dgCardex.Size = New System.Drawing.Size(1149, 328)
        Me.dgCardex.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgCardex)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 283)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1155, 347)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgInsumos)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 53)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1152, 224)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'dgInsumos
        '
        Me.dgInsumos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgInsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInsumos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgInsumos.Location = New System.Drawing.Point(3, 16)
        Me.dgInsumos.Name = "dgInsumos"
        Me.dgInsumos.Size = New System.Drawing.Size(1146, 205)
        Me.dgInsumos.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(693, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Insumo"
        '
        'txtInsumo
        '
        Me.txtInsumo.Location = New System.Drawing.Point(744, 26)
        Me.txtInsumo.Name = "txtInsumo"
        Me.txtInsumo.Size = New System.Drawing.Size(296, 20)
        Me.txtInsumo.TabIndex = 8
        '
        'btnVis
        '
        Me.btnVis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVis.Image = CType(resources.GetObject("btnVis.Image"), System.Drawing.Image)
        Me.btnVis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVis.Location = New System.Drawing.Point(1046, 26)
        Me.btnVis.Name = "btnVis"
        Me.btnVis.Size = New System.Drawing.Size(126, 24)
        Me.btnVis.TabIndex = 334
        Me.btnVis.TabStop = False
        Me.btnVis.Text = "Visualizar Kardex"
        Me.btnVis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnVis.UseVisualStyleBackColor = True
        '
        'ReporteCardexForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1185, 675)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbObras)
        Me.Controls.Add(Me.txtInsumo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbAlmacen)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnVis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ReporteCardexForm"
        Me.Controls.SetChildIndex(Me.btnVis, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.cbAlmacen, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtInsumo, 0)
        Me.Controls.SetChildIndex(Me.cbObras, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        CType(Me.dgCardex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgInsumos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbObras As System.Windows.Forms.ComboBox
    Friend WithEvents cbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgCardex As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgInsumos As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtInsumo As System.Windows.Forms.TextBox
    Friend WithEvents btnVis As ComponentesSolucion2008.BottomSSP

End Class
