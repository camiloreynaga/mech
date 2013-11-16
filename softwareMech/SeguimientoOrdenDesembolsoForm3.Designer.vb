<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeguimientoOrdenDesembolsoForm3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeguimientoOrdenDesembolsoForm3))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.gbDesembolso = New System.Windows.Forms.GroupBox
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
        Me.btnImprimir = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.dgDesembolso = New System.Windows.Forms.DataGridView
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtEstadoDesem = New System.Windows.Forms.TextBox
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtEstadoGerencia = New System.Windows.Forms.TextBox
        Me.txtEstadoContab = New System.Windows.Forms.TextBox
        Me.txtEstadoTesoreria = New System.Windows.Forms.TextBox
        Me.txtFechaContabilidad = New System.Windows.Forms.TextBox
        Me.txtFechaTesoreria = New System.Windows.Forms.TextBox
        Me.txtFechaGerencia = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFechaDesem = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDetraccion = New System.Windows.Forms.TextBox
        Me.txtMonto = New System.Windows.Forms.TextBox
        Me.txtNombreGerente = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtNombreConta = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtNombreTesoreria = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSolicitante = New System.Windows.Forms.TextBox
        Me.txtObsGerencia = New System.Windows.Forms.TextBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.cbObra = New System.Windows.Forms.ComboBox
        Me.cbProveedor = New System.Windows.Forms.ComboBox
        Me.lblObra = New System.Windows.Forms.Label
        Me.lblProveedor = New System.Windows.Forms.Label
        Me.chkObras = New System.Windows.Forms.CheckBox
        Me.btnVer = New System.Windows.Forms.Button
        Me.lblDesde = New System.Windows.Forms.Label
        Me.dtpInicio = New System.Windows.Forms.DateTimePicker
        Me.lblHasta = New System.Windows.Forms.Label
        Me.dtpFin = New System.Windows.Forms.DateTimePicker
        Me.btnImprimirGrilla = New System.Windows.Forms.Button
        Me.rdoObra = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdoSerie = New System.Windows.Forms.RadioButton
        Me.rdoProveedor = New System.Windows.Forms.RadioButton
        Me.cbSerie = New System.Windows.Forms.ComboBox
        Me.lblSerie = New System.Windows.Forms.Label
        Me.gbDesembolso.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        Me.lblTitulo.Text = "Seguimiento de ordenes de Desembolsos"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'gbDesembolso
        '
        Me.gbDesembolso.Controls.Add(Me.BindingNavigator1)
        Me.gbDesembolso.Controls.Add(Me.dgDesembolso)
        Me.gbDesembolso.Location = New System.Drawing.Point(16, 71)
        Me.gbDesembolso.Name = "gbDesembolso"
        Me.gbDesembolso.Size = New System.Drawing.Size(893, 463)
        Me.gbDesembolso.TabIndex = 3
        Me.gbDesembolso.TabStop = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.btnImprimir, Me.ToolStripSeparator1})
        Me.BindingNavigator1.Location = New System.Drawing.Point(3, 435)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(887, 25)
        Me.BindingNavigator1.TabIndex = 1
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
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(138, 22)
        Me.btnImprimir.Text = "Imprimir Desembolso"
        Me.btnImprimir.ToolTipText = "Imprimir orden de desembolso"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'dgDesembolso
        '
        Me.dgDesembolso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgDesembolso.Location = New System.Drawing.Point(0, 11)
        Me.dgDesembolso.Name = "dgDesembolso"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDesembolso.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgDesembolso.Size = New System.Drawing.Size(887, 421)
        Me.dgDesembolso.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(18, 15)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Solicitante"
        '
        'txtEstadoDesem
        '
        Me.txtEstadoDesem.Location = New System.Drawing.Point(561, 12)
        Me.txtEstadoDesem.Name = "txtEstadoDesem"
        Me.txtEstadoDesem.Size = New System.Drawing.Size(85, 20)
        Me.txtEstadoDesem.TabIndex = 3
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(838, 12)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.Size = New System.Drawing.Size(45, 20)
        Me.txtNro.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(795, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nro"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(437, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Estado Desembolso"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtEstadoGerencia)
        Me.GroupBox2.Controls.Add(Me.txtEstadoContab)
        Me.GroupBox2.Controls.Add(Me.txtEstadoTesoreria)
        Me.GroupBox2.Controls.Add(Me.txtFechaContabilidad)
        Me.GroupBox2.Controls.Add(Me.txtFechaTesoreria)
        Me.GroupBox2.Controls.Add(Me.txtFechaGerencia)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFechaDesem)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtDetraccion)
        Me.GroupBox2.Controls.Add(Me.txtMonto)
        Me.GroupBox2.Controls.Add(Me.txtNombreGerente)
        Me.GroupBox2.Controls.Add(Me.Label33)
        Me.GroupBox2.Controls.Add(Me.txtNombreConta)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.txtNombreTesoreria)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtEstadoDesem)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.txtSolicitante)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtNro)
        Me.GroupBox2.Controls.Add(Me.txtObsGerencia)
        Me.GroupBox2.Controls.Add(Me.btnCerrar)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 528)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(895, 120)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'txtEstadoGerencia
        '
        Me.txtEstadoGerencia.Location = New System.Drawing.Point(561, 37)
        Me.txtEstadoGerencia.Name = "txtEstadoGerencia"
        Me.txtEstadoGerencia.Size = New System.Drawing.Size(85, 20)
        Me.txtEstadoGerencia.TabIndex = 49
        '
        'txtEstadoContab
        '
        Me.txtEstadoContab.Location = New System.Drawing.Point(561, 89)
        Me.txtEstadoContab.Name = "txtEstadoContab"
        Me.txtEstadoContab.Size = New System.Drawing.Size(85, 20)
        Me.txtEstadoContab.TabIndex = 47
        '
        'txtEstadoTesoreria
        '
        Me.txtEstadoTesoreria.Location = New System.Drawing.Point(561, 63)
        Me.txtEstadoTesoreria.Name = "txtEstadoTesoreria"
        Me.txtEstadoTesoreria.Size = New System.Drawing.Size(85, 20)
        Me.txtEstadoTesoreria.TabIndex = 48
        '
        'txtFechaContabilidad
        '
        Me.txtFechaContabilidad.Location = New System.Drawing.Point(355, 88)
        Me.txtFechaContabilidad.Name = "txtFechaContabilidad"
        Me.txtFechaContabilidad.Size = New System.Drawing.Size(76, 20)
        Me.txtFechaContabilidad.TabIndex = 44
        '
        'txtFechaTesoreria
        '
        Me.txtFechaTesoreria.Location = New System.Drawing.Point(355, 62)
        Me.txtFechaTesoreria.Name = "txtFechaTesoreria"
        Me.txtFechaTesoreria.Size = New System.Drawing.Size(76, 20)
        Me.txtFechaTesoreria.TabIndex = 46
        '
        'txtFechaGerencia
        '
        Me.txtFechaGerencia.Location = New System.Drawing.Point(355, 38)
        Me.txtFechaGerencia.Name = "txtFechaGerencia"
        Me.txtFechaGerencia.Size = New System.Drawing.Size(76, 20)
        Me.txtFechaGerencia.TabIndex = 45
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(307, 93)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(42, 13)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "Fecha"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(307, 68)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(42, 13)
        Me.Label22.TabIndex = 40
        Me.Label22.Text = "Fecha"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(307, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(42, 13)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Fecha"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(307, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Fecha"
        '
        'txtFechaDesem
        '
        Me.txtFechaDesem.Location = New System.Drawing.Point(355, 12)
        Me.txtFechaDesem.Name = "txtFechaDesem"
        Me.txtFechaDesem.Size = New System.Drawing.Size(76, 20)
        Me.txtFechaDesem.TabIndex = 39
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(675, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Total Detracción"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(702, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Total Monto"
        '
        'txtDetraccion
        '
        Me.txtDetraccion.Location = New System.Drawing.Point(783, 91)
        Me.txtDetraccion.Name = "txtDetraccion"
        Me.txtDetraccion.Size = New System.Drawing.Size(100, 20)
        Me.txtDetraccion.TabIndex = 32
        Me.txtDetraccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMonto
        '
        Me.txtMonto.Location = New System.Drawing.Point(783, 65)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(100, 20)
        Me.txtMonto.TabIndex = 33
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNombreGerente
        '
        Me.txtNombreGerente.Location = New System.Drawing.Point(91, 38)
        Me.txtNombreGerente.Name = "txtNombreGerente"
        Me.txtNombreGerente.Size = New System.Drawing.Size(210, 20)
        Me.txtNombreGerente.TabIndex = 27
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(27, 41)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(58, 13)
        Me.Label33.TabIndex = 22
        Me.Label33.Text = "Gerencia"
        '
        'txtNombreConta
        '
        Me.txtNombreConta.Location = New System.Drawing.Point(91, 90)
        Me.txtNombreConta.Name = "txtNombreConta"
        Me.txtNombreConta.Size = New System.Drawing.Size(210, 20)
        Me.txtNombreConta.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(655, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(106, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "Obs. de Gerencia"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 91)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(77, 13)
        Me.Label34.TabIndex = 21
        Me.Label34.Text = "Contabilidad"
        '
        'txtNombreTesoreria
        '
        Me.txtNombreTesoreria.Location = New System.Drawing.Point(91, 64)
        Me.txtNombreTesoreria.Name = "txtNombreTesoreria"
        Me.txtNombreTesoreria.Size = New System.Drawing.Size(210, 20)
        Me.txtNombreTesoreria.TabIndex = 24
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(25, 64)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(60, 13)
        Me.Label32.TabIndex = 20
        Me.Label32.Text = "Tesoreria"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(437, 93)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(120, 13)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Estado Contabilidad"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(452, 68)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(103, 13)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "Estado Tesoreria"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(454, 44)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(101, 13)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "Estado Gerencia"
        '
        'txtSolicitante
        '
        Me.txtSolicitante.Location = New System.Drawing.Point(91, 12)
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.Size = New System.Drawing.Size(210, 20)
        Me.txtSolicitante.TabIndex = 3
        '
        'txtObsGerencia
        '
        Me.txtObsGerencia.Location = New System.Drawing.Point(652, 37)
        Me.txtObsGerencia.Multiline = True
        Me.txtObsGerencia.Name = "txtObsGerencia"
        Me.txtObsGerencia.Size = New System.Drawing.Size(231, 24)
        Me.txtObsGerencia.TabIndex = 27
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(658, 37)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(80, 25)
        Me.btnCerrar.TabIndex = 7
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'cbObra
        '
        Me.cbObra.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbObra.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbObra.DropDownWidth = 550
        Me.cbObra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbObra.FormattingEnabled = True
        Me.cbObra.Location = New System.Drawing.Point(350, 52)
        Me.cbObra.Name = "cbObra"
        Me.cbObra.Size = New System.Drawing.Size(423, 21)
        Me.cbObra.TabIndex = 6
        '
        'cbProveedor
        '
        Me.cbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbProveedor.DropDownWidth = 400
        Me.cbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProveedor.FormattingEnabled = True
        Me.cbProveedor.Location = New System.Drawing.Point(305, 32)
        Me.cbProveedor.Name = "cbProveedor"
        Me.cbProveedor.Size = New System.Drawing.Size(468, 21)
        Me.cbProveedor.TabIndex = 6
        '
        'lblObra
        '
        Me.lblObra.AutoSize = True
        Me.lblObra.Location = New System.Drawing.Point(243, 53)
        Me.lblObra.Name = "lblObra"
        Me.lblObra.Size = New System.Drawing.Size(38, 13)
        Me.lblObra.TabIndex = 6
        Me.lblObra.Text = "Obra:"
        '
        'lblProveedor
        '
        Me.lblProveedor.AutoSize = True
        Me.lblProveedor.Location = New System.Drawing.Point(236, 35)
        Me.lblProveedor.Name = "lblProveedor"
        Me.lblProveedor.Size = New System.Drawing.Size(69, 13)
        Me.lblProveedor.TabIndex = 6
        Me.lblProveedor.Text = "Proveedor:"
        '
        'chkObras
        '
        Me.chkObras.AutoSize = True
        Me.chkObras.Checked = True
        Me.chkObras.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkObras.Location = New System.Drawing.Point(287, 52)
        Me.chkObras.Name = "chkObras"
        Me.chkObras.Size = New System.Drawing.Size(61, 17)
        Me.chkObras.TabIndex = 7
        Me.chkObras.Text = "Todas"
        Me.chkObras.UseVisualStyleBackColor = True
        '
        'btnVer
        '
        Me.btnVer.Location = New System.Drawing.Point(779, 32)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(55, 23)
        Me.btnVer.TabIndex = 0
        Me.btnVer.Text = "VER"
        Me.btnVer.UseVisualStyleBackColor = True
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Location = New System.Drawing.Point(233, 32)
        Me.lblDesde.Name = "lblDesde"
        Me.lblDesde.Size = New System.Drawing.Size(47, 13)
        Me.lblDesde.TabIndex = 11
        Me.lblDesde.Text = "Desde:"
        '
        'dtpInicio
        '
        Me.dtpInicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInicio.Location = New System.Drawing.Point(284, 28)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(102, 20)
        Me.dtpInicio.TabIndex = 10
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(392, 32)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(44, 13)
        Me.lblHasta.TabIndex = 12
        Me.lblHasta.Text = "Hasta:"
        '
        'dtpFin
        '
        Me.dtpFin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFin.Location = New System.Drawing.Point(443, 29)
        Me.dtpFin.Name = "dtpFin"
        Me.dtpFin.Size = New System.Drawing.Size(102, 20)
        Me.dtpFin.TabIndex = 9
        '
        'btnImprimirGrilla
        '
        Me.btnImprimirGrilla.Image = CType(resources.GetObject("btnImprimirGrilla.Image"), System.Drawing.Image)
        Me.btnImprimirGrilla.Location = New System.Drawing.Point(836, 32)
        Me.btnImprimirGrilla.Name = "btnImprimirGrilla"
        Me.btnImprimirGrilla.Size = New System.Drawing.Size(75, 23)
        Me.btnImprimirGrilla.TabIndex = 8
        Me.btnImprimirGrilla.Text = "Imprimir"
        Me.btnImprimirGrilla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnImprimirGrilla.UseVisualStyleBackColor = True
        '
        'rdoObra
        '
        Me.rdoObra.AutoSize = True
        Me.rdoObra.Checked = True
        Me.rdoObra.Location = New System.Drawing.Point(6, 15)
        Me.rdoObra.Name = "rdoObra"
        Me.rdoObra.Size = New System.Drawing.Size(52, 17)
        Me.rdoObra.TabIndex = 0
        Me.rdoObra.TabStop = True
        Me.rdoObra.Text = "Obra"
        Me.rdoObra.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoSerie)
        Me.GroupBox1.Controls.Add(Me.rdoProveedor)
        Me.GroupBox1.Controls.Add(Me.rdoObra)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(18, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(212, 39)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtro"
        '
        'rdoSerie
        '
        Me.rdoSerie.AutoSize = True
        Me.rdoSerie.Location = New System.Drawing.Point(153, 15)
        Me.rdoSerie.Name = "rdoSerie"
        Me.rdoSerie.Size = New System.Drawing.Size(54, 17)
        Me.rdoSerie.TabIndex = 2
        Me.rdoSerie.Text = "Serie"
        Me.rdoSerie.UseVisualStyleBackColor = True
        '
        'rdoProveedor
        '
        Me.rdoProveedor.AutoSize = True
        Me.rdoProveedor.Location = New System.Drawing.Point(64, 15)
        Me.rdoProveedor.Name = "rdoProveedor"
        Me.rdoProveedor.Size = New System.Drawing.Size(83, 17)
        Me.rdoProveedor.TabIndex = 1
        Me.rdoProveedor.Text = "Proveedor"
        Me.rdoProveedor.UseVisualStyleBackColor = True
        '
        'cbSerie
        '
        Me.cbSerie.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSerie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSerie.DropDownWidth = 106
        Me.cbSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSerie.FormattingEnabled = True
        Me.cbSerie.Location = New System.Drawing.Point(289, 32)
        Me.cbSerie.Name = "cbSerie"
        Me.cbSerie.Size = New System.Drawing.Size(76, 21)
        Me.cbSerie.TabIndex = 6
        '
        'lblSerie
        '
        Me.lblSerie.AutoSize = True
        Me.lblSerie.Location = New System.Drawing.Point(243, 34)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(40, 13)
        Me.lblSerie.TabIndex = 6
        Me.lblSerie.Text = "Serie:"
        '
        'SeguimientoOrdenDesembolsoForm3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.gbDesembolso)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnImprimirGrilla)
        Me.Controls.Add(Me.btnVer)
        Me.Controls.Add(Me.lblObra)
        Me.Controls.Add(Me.cbObra)
        Me.Controls.Add(Me.lblHasta)
        Me.Controls.Add(Me.dtpFin)
        Me.Controls.Add(Me.chkObras)
        Me.Controls.Add(Me.lblSerie)
        Me.Controls.Add(Me.cbSerie)
        Me.Controls.Add(Me.cbProveedor)
        Me.Controls.Add(Me.lblProveedor)
        Me.Controls.Add(Me.dtpInicio)
        Me.Controls.Add(Me.lblDesde)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SeguimientoOrdenDesembolsoForm3"
        Me.Text = "jkl"
        Me.Controls.SetChildIndex(Me.lblDesde, 0)
        Me.Controls.SetChildIndex(Me.dtpInicio, 0)
        Me.Controls.SetChildIndex(Me.lblProveedor, 0)
        Me.Controls.SetChildIndex(Me.cbProveedor, 0)
        Me.Controls.SetChildIndex(Me.cbSerie, 0)
        Me.Controls.SetChildIndex(Me.lblSerie, 0)
        Me.Controls.SetChildIndex(Me.chkObras, 0)
        Me.Controls.SetChildIndex(Me.dtpFin, 0)
        Me.Controls.SetChildIndex(Me.lblHasta, 0)
        Me.Controls.SetChildIndex(Me.cbObra, 0)
        Me.Controls.SetChildIndex(Me.lblObra, 0)
        Me.Controls.SetChildIndex(Me.btnVer, 0)
        Me.Controls.SetChildIndex(Me.btnImprimirGrilla, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.gbDesembolso, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.gbDesembolso.ResumeLayout(False)
        Me.gbDesembolso.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbDesembolso As System.Windows.Forms.GroupBox
    Friend WithEvents dgDesembolso As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtEstadoDesem As System.Windows.Forms.TextBox
    Friend WithEvents txtSolicitante As System.Windows.Forms.TextBox
    Friend WithEvents cbObra As System.Windows.Forms.ComboBox
    Friend WithEvents cbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents lblObra As System.Windows.Forms.Label
    Friend WithEvents lblProveedor As System.Windows.Forms.Label
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
    Friend WithEvents btnImprimir As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkObras As System.Windows.Forms.CheckBox
    Friend WithEvents txtNombreGerente As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtNombreConta As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtNombreTesoreria As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDetraccion As System.Windows.Forms.TextBox
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents txtEstadoGerencia As System.Windows.Forms.TextBox
    Friend WithEvents txtEstadoContab As System.Windows.Forms.TextBox
    Friend WithEvents txtEstadoTesoreria As System.Windows.Forms.TextBox
    Friend WithEvents txtFechaContabilidad As System.Windows.Forms.TextBox
    Friend WithEvents txtFechaTesoreria As System.Windows.Forms.TextBox
    Friend WithEvents txtFechaGerencia As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFechaDesem As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtObsGerencia As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents lblDesde As System.Windows.Forms.Label
    Friend WithEvents dtpInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHasta As System.Windows.Forms.Label
    Friend WithEvents dtpFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImprimirGrilla As System.Windows.Forms.Button
    Friend WithEvents rdoObra As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoSerie As System.Windows.Forms.RadioButton
    Friend WithEvents rdoProveedor As System.Windows.Forms.RadioButton
    Friend WithEvents cbSerie As System.Windows.Forms.ComboBox
    Friend WithEvents lblSerie As System.Windows.Forms.Label

End Class
