<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeguimientoFacturasForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeguimientoFacturasForm))
        Me.dgDesembolso = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnImp = New System.Windows.Forms.ToolStripButton
        Me.rdoObra = New System.Windows.Forms.RadioButton
        Me.rdoProv = New System.Windows.Forms.RadioButton
        Me.rdoSerie = New System.Windows.Forms.RadioButton
        Me.btnVer = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkObra = New System.Windows.Forms.CheckBox
        Me.dtpInicio = New System.Windows.Forms.DateTimePicker
        Me.cbObras = New System.Windows.Forms.ComboBox
        Me.dtpFin = New System.Windows.Forms.DateTimePicker
        Me.lblDel = New System.Windows.Forms.Label
        Me.lblAl = New System.Windows.Forms.Label
        Me.lblSerie = New System.Windows.Forms.Label
        Me.cbSerie = New System.Windows.Forms.ComboBox
        Me.lblProv = New System.Windows.Forms.Label
        Me.cbProv = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTotalSoles = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTotalDolares = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDetraccionesSoles = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDetraccionesDolares = New System.Windows.Forms.TextBox
        Me.lblObra = New System.Windows.Forms.Label
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(895, 23)
        Me.lblTitulo.Text = "Reporte de facturas"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 573)
        '
        'dgDesembolso
        '
        Me.dgDesembolso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgDesembolso.Location = New System.Drawing.Point(3, 11)
        Me.dgDesembolso.Name = "dgDesembolso"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDesembolso.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgDesembolso.Size = New System.Drawing.Size(864, 446)
        Me.dgDesembolso.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BindingNavigator1)
        Me.GroupBox1.Controls.Add(Me.dgDesembolso)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(870, 488)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.btnImp})
        Me.BindingNavigator1.Location = New System.Drawing.Point(3, 460)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(864, 25)
        Me.BindingNavigator1.TabIndex = 4
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(37, 22)
        Me.BindingNavigatorCountItem.Text = "de {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Número total de elementos"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Mover primero"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Mover anterior"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Posición"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Posición actual"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Mover siguiente"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Mover último"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnImp
        '
        Me.btnImp.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnImp.Image = CType(resources.GetObject("btnImp.Image"), System.Drawing.Image)
        Me.btnImp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImp.Name = "btnImp"
        Me.btnImp.Size = New System.Drawing.Size(71, 22)
        Me.btnImp.Text = "Imprimir"
        Me.btnImp.ToolTipText = "Imprimir orden de desembolso"
        '
        'rdoObra
        '
        Me.rdoObra.AutoSize = True
        Me.rdoObra.Location = New System.Drawing.Point(6, 18)
        Me.rdoObra.Name = "rdoObra"
        Me.rdoObra.Size = New System.Drawing.Size(52, 17)
        Me.rdoObra.TabIndex = 0
        Me.rdoObra.TabStop = True
        Me.rdoObra.Text = "Obra"
        Me.rdoObra.UseVisualStyleBackColor = True
        '
        'rdoProv
        '
        Me.rdoProv.AutoSize = True
        Me.rdoProv.Location = New System.Drawing.Point(64, 18)
        Me.rdoProv.Name = "rdoProv"
        Me.rdoProv.Size = New System.Drawing.Size(83, 17)
        Me.rdoProv.TabIndex = 1
        Me.rdoProv.TabStop = True
        Me.rdoProv.Text = "Proveedor"
        Me.rdoProv.UseVisualStyleBackColor = True
        '
        'rdoSerie
        '
        Me.rdoSerie.AutoSize = True
        Me.rdoSerie.Location = New System.Drawing.Point(153, 18)
        Me.rdoSerie.Name = "rdoSerie"
        Me.rdoSerie.Size = New System.Drawing.Size(54, 17)
        Me.rdoSerie.TabIndex = 2
        Me.rdoSerie.TabStop = True
        Me.rdoSerie.Text = "Serie"
        Me.rdoSerie.UseVisualStyleBackColor = True
        '
        'btnVer
        '
        Me.btnVer.Location = New System.Drawing.Point(808, 29)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(75, 23)
        Me.btnVer.TabIndex = 6
        Me.btnVer.Text = "Ver"
        Me.btnVer.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdoObra)
        Me.GroupBox2.Controls.Add(Me.rdoSerie)
        Me.GroupBox2.Controls.Add(Me.rdoProv)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 23)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(222, 50)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filtro"
        '
        'chkObra
        '
        Me.chkObra.AutoSize = True
        Me.chkObra.Location = New System.Drawing.Point(303, 59)
        Me.chkObra.Name = "chkObra"
        Me.chkObra.Size = New System.Drawing.Size(61, 17)
        Me.chkObra.TabIndex = 4
        Me.chkObra.Text = "Todas"
        Me.chkObra.UseVisualStyleBackColor = True
        '
        'dtpInicio
        '
        Me.dtpInicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInicio.Location = New System.Drawing.Point(291, 29)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(98, 20)
        Me.dtpInicio.TabIndex = 0
        '
        'cbObras
        '
        Me.cbObras.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbObras.FormattingEnabled = True
        Me.cbObras.Location = New System.Drawing.Point(370, 55)
        Me.cbObras.Name = "cbObras"
        Me.cbObras.Size = New System.Drawing.Size(473, 21)
        Me.cbObras.TabIndex = 5
        '
        'dtpFin
        '
        Me.dtpFin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFin.Location = New System.Drawing.Point(419, 29)
        Me.dtpFin.Name = "dtpFin"
        Me.dtpFin.Size = New System.Drawing.Size(98, 20)
        Me.dtpFin.TabIndex = 1
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(259, 31)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(30, 13)
        Me.lblDel.TabIndex = 11
        Me.lblDel.Text = "Del:"
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(395, 31)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(22, 13)
        Me.lblAl.TabIndex = 11
        Me.lblAl.Text = "Al:"
        '
        'lblSerie
        '
        Me.lblSerie.AutoSize = True
        Me.lblSerie.Location = New System.Drawing.Point(259, 29)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(40, 13)
        Me.lblSerie.TabIndex = 12
        Me.lblSerie.Text = "Serie:"
        '
        'cbSerie
        '
        Me.cbSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSerie.FormattingEnabled = True
        Me.cbSerie.Location = New System.Drawing.Point(305, 28)
        Me.cbSerie.Name = "cbSerie"
        Me.cbSerie.Size = New System.Drawing.Size(121, 21)
        Me.cbSerie.TabIndex = 2
        '
        'lblProv
        '
        Me.lblProv.AutoSize = True
        Me.lblProv.Location = New System.Drawing.Point(259, 34)
        Me.lblProv.Name = "lblProv"
        Me.lblProv.Size = New System.Drawing.Size(69, 13)
        Me.lblProv.TabIndex = 14
        Me.lblProv.Text = "Proveedor:"
        '
        'cbProv
        '
        Me.cbProv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProv.FormattingEnabled = True
        Me.cbProv.Location = New System.Drawing.Point(334, 32)
        Me.cbProv.Name = "cbProv"
        Me.cbProv.Size = New System.Drawing.Size(468, 21)
        Me.cbProv.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(68, 576)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Total S/."
        '
        'txtTotalSoles
        '
        Me.txtTotalSoles.Location = New System.Drawing.Point(132, 573)
        Me.txtTotalSoles.Name = "txtTotalSoles"
        Me.txtTotalSoles.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalSoles.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(254, 578)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Total $"
        '
        'txtTotalDolares
        '
        Me.txtTotalDolares.Location = New System.Drawing.Point(307, 573)
        Me.txtTotalDolares.Name = "txtTotalDolares"
        Me.txtTotalDolares.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalDolares.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(413, 578)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Detracción S/."
        '
        'txtDetraccionesSoles
        '
        Me.txtDetraccionesSoles.Location = New System.Drawing.Point(510, 573)
        Me.txtDetraccionesSoles.Name = "txtDetraccionesSoles"
        Me.txtDetraccionesSoles.Size = New System.Drawing.Size(100, 20)
        Me.txtDetraccionesSoles.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(619, 573)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Detracción $"
        '
        'txtDetraccionesDolares
        '
        Me.txtDetraccionesDolares.Location = New System.Drawing.Point(705, 573)
        Me.txtDetraccionesDolares.Name = "txtDetraccionesDolares"
        Me.txtDetraccionesDolares.Size = New System.Drawing.Size(100, 20)
        Me.txtDetraccionesDolares.TabIndex = 16
        '
        'lblObra
        '
        Me.lblObra.AutoSize = True
        Me.lblObra.Location = New System.Drawing.Point(259, 59)
        Me.lblObra.Name = "lblObra"
        Me.lblObra.Size = New System.Drawing.Size(38, 13)
        Me.lblObra.TabIndex = 17
        Me.lblObra.Text = "Obra:"
        '
        'SeguimientoFacturasForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(895, 618)
        Me.Controls.Add(Me.lblObra)
        Me.Controls.Add(Me.txtDetraccionesDolares)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtDetraccionesSoles)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTotalDolares)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTotalSoles)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbProv)
        Me.Controls.Add(Me.lblProv)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.chkObra)
        Me.Controls.Add(Me.btnVer)
        Me.Controls.Add(Me.cbObras)
        Me.Controls.Add(Me.lblAl)
        Me.Controls.Add(Me.dtpFin)
        Me.Controls.Add(Me.lblSerie)
        Me.Controls.Add(Me.dtpInicio)
        Me.Controls.Add(Me.lblDel)
        Me.Controls.Add(Me.cbSerie)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SeguimientoFacturasForm"
        Me.Controls.SetChildIndex(Me.cbSerie, 0)
        Me.Controls.SetChildIndex(Me.lblDel, 0)
        Me.Controls.SetChildIndex(Me.dtpInicio, 0)
        Me.Controls.SetChildIndex(Me.lblSerie, 0)
        Me.Controls.SetChildIndex(Me.dtpFin, 0)
        Me.Controls.SetChildIndex(Me.lblAl, 0)
        Me.Controls.SetChildIndex(Me.cbObras, 0)
        Me.Controls.SetChildIndex(Me.btnVer, 0)
        Me.Controls.SetChildIndex(Me.chkObra, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.lblProv, 0)
        Me.Controls.SetChildIndex(Me.cbProv, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtTotalSoles, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtTotalDolares, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtDetraccionesSoles, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtDetraccionesDolares, 0)
        Me.Controls.SetChildIndex(Me.lblObra, 0)
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgDesembolso As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rdoObra As System.Windows.Forms.RadioButton
    Friend WithEvents rdoProv As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSerie As System.Windows.Forms.RadioButton
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkObra As System.Windows.Forms.CheckBox
    Friend WithEvents dtpInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbObras As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDel As System.Windows.Forms.Label
    Friend WithEvents lblAl As System.Windows.Forms.Label
    Friend WithEvents lblSerie As System.Windows.Forms.Label
    Friend WithEvents cbSerie As System.Windows.Forms.ComboBox
    Friend WithEvents lblProv As System.Windows.Forms.Label
    Friend WithEvents cbProv As System.Windows.Forms.ComboBox
    Friend WithEvents btnImp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotalSoles As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTotalDolares As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDetraccionesSoles As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDetraccionesDolares As System.Windows.Forms.TextBox
    Friend WithEvents lblObra As System.Windows.Forms.Label

End Class
