<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantVehiculoTransportistaFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantVehiculoTransportistaFrm))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgVehiculo = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMarcaPlaca = New System.Windows.Forms.TextBox
        Me.txtConstancia = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtLicencia = New System.Windows.Forms.TextBox
        Me.txtDni = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BindingNavigator2 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem1 = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem1 = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem1 = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem1 = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem1 = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem1 = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.dgConductor = New System.Windows.Forms.DataGridView
        Me.cbEmpTrans = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnNuevoC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnCancelarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevoV = New ComponentesSolucion2008.BottomSSP(Me.components)
        CType(Me.dgVehiculo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator2.SuspendLayout()
        CType(Me.dgConductor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(946, 23)
        Me.lblTitulo.Text = "Mantenimiento de Vehiculos y Choferes"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 405)
        '
        'dgVehiculo
        '
        Me.dgVehiculo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgVehiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgVehiculo.Location = New System.Drawing.Point(3, 16)
        Me.dgVehiculo.Name = "dgVehiculo"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgVehiculo.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgVehiculo.Size = New System.Drawing.Size(392, 208)
        Me.dgVehiculo.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtMarcaPlaca)
        Me.GroupBox1.Controls.Add(Me.txtConstancia)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(398, 65)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vehiculo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Marca / Placa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(226, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "N° Constancia:"
        '
        'txtMarcaPlaca
        '
        Me.txtMarcaPlaca.Location = New System.Drawing.Point(16, 39)
        Me.txtMarcaPlaca.Name = "txtMarcaPlaca"
        Me.txtMarcaPlaca.Size = New System.Drawing.Size(191, 20)
        Me.txtMarcaPlaca.TabIndex = 0
        '
        'txtConstancia
        '
        Me.txtConstancia.Location = New System.Drawing.Point(229, 39)
        Me.txtConstancia.Name = "txtConstancia"
        Me.txtConstancia.Size = New System.Drawing.Size(163, 20)
        Me.txtConstancia.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BindingNavigator1)
        Me.GroupBox2.Controls.Add(Me.dgVehiculo)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(398, 259)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.BindingNavigator1.Location = New System.Drawing.Point(3, 231)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(392, 25)
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtLicencia)
        Me.GroupBox3.Controls.Add(Me.txtDni)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtNombre)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(427, 53)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(512, 65)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Chofer"
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(358, 39)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Size = New System.Drawing.Size(87, 20)
        Me.txtLicencia.TabIndex = 2
        '
        'txtDni
        '
        Me.txtDni.Location = New System.Drawing.Point(10, 39)
        Me.txtDni.MaxLength = 8
        Me.txtDni.Name = "txtDni"
        Me.txtDni.Size = New System.Drawing.Size(83, 20)
        Me.txtDni.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(107, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Nombre:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(355, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "N° Licencia:"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(107, 39)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(234, 20)
        Me.txtNombre.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "DNI:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.BindingNavigator2)
        Me.GroupBox4.Controls.Add(Me.dgConductor)
        Me.GroupBox4.Location = New System.Drawing.Point(427, 153)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(512, 259)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'BindingNavigator2
        '
        Me.BindingNavigator2.AddNewItem = Nothing
        Me.BindingNavigator2.CountItem = Me.BindingNavigatorCountItem1
        Me.BindingNavigator2.DeleteItem = Nothing
        Me.BindingNavigator2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem1, Me.BindingNavigatorMovePreviousItem1, Me.BindingNavigatorSeparator3, Me.BindingNavigatorPositionItem1, Me.BindingNavigatorCountItem1, Me.BindingNavigatorSeparator4, Me.BindingNavigatorMoveNextItem1, Me.BindingNavigatorMoveLastItem1, Me.BindingNavigatorSeparator5})
        Me.BindingNavigator2.Location = New System.Drawing.Point(3, 231)
        Me.BindingNavigator2.MoveFirstItem = Me.BindingNavigatorMoveFirstItem1
        Me.BindingNavigator2.MoveLastItem = Me.BindingNavigatorMoveLastItem1
        Me.BindingNavigator2.MoveNextItem = Me.BindingNavigatorMoveNextItem1
        Me.BindingNavigator2.MovePreviousItem = Me.BindingNavigatorMovePreviousItem1
        Me.BindingNavigator2.Name = "BindingNavigator2"
        Me.BindingNavigator2.PositionItem = Me.BindingNavigatorPositionItem1
        Me.BindingNavigator2.Size = New System.Drawing.Size(506, 25)
        Me.BindingNavigator2.TabIndex = 4
        Me.BindingNavigator2.Text = "BindingNavigator2"
        '
        'BindingNavigatorCountItem1
        '
        Me.BindingNavigatorCountItem1.Name = "BindingNavigatorCountItem1"
        Me.BindingNavigatorCountItem1.Size = New System.Drawing.Size(37, 22)
        Me.BindingNavigatorCountItem1.Text = "de {0}"
        Me.BindingNavigatorCountItem1.ToolTipText = "Número total de elementos"
        '
        'BindingNavigatorMoveFirstItem1
        '
        Me.BindingNavigatorMoveFirstItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem1.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem1.Name = "BindingNavigatorMoveFirstItem1"
        Me.BindingNavigatorMoveFirstItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem1.Text = "Mover primero"
        '
        'BindingNavigatorMovePreviousItem1
        '
        Me.BindingNavigatorMovePreviousItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem1.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem1.Name = "BindingNavigatorMovePreviousItem1"
        Me.BindingNavigatorMovePreviousItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem1.Text = "Mover anterior"
        '
        'BindingNavigatorSeparator3
        '
        Me.BindingNavigatorSeparator3.Name = "BindingNavigatorSeparator3"
        Me.BindingNavigatorSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem1
        '
        Me.BindingNavigatorPositionItem1.AccessibleName = "Posición"
        Me.BindingNavigatorPositionItem1.AutoSize = False
        Me.BindingNavigatorPositionItem1.Name = "BindingNavigatorPositionItem1"
        Me.BindingNavigatorPositionItem1.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem1.Text = "0"
        Me.BindingNavigatorPositionItem1.ToolTipText = "Posición actual"
        '
        'BindingNavigatorSeparator4
        '
        Me.BindingNavigatorSeparator4.Name = "BindingNavigatorSeparator4"
        Me.BindingNavigatorSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem1
        '
        Me.BindingNavigatorMoveNextItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem1.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem1.Name = "BindingNavigatorMoveNextItem1"
        Me.BindingNavigatorMoveNextItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem1.Text = "Mover siguiente"
        '
        'BindingNavigatorMoveLastItem1
        '
        Me.BindingNavigatorMoveLastItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem1.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem1.Name = "BindingNavigatorMoveLastItem1"
        Me.BindingNavigatorMoveLastItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem1.Text = "Mover último"
        '
        'BindingNavigatorSeparator5
        '
        Me.BindingNavigatorSeparator5.Name = "BindingNavigatorSeparator5"
        Me.BindingNavigatorSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'dgConductor
        '
        Me.dgConductor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgConductor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgConductor.Location = New System.Drawing.Point(3, 16)
        Me.dgConductor.Name = "dgConductor"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgConductor.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgConductor.Size = New System.Drawing.Size(506, 208)
        Me.dgConductor.TabIndex = 3
        '
        'cbEmpTrans
        '
        Me.cbEmpTrans.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmpTrans.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmpTrans.FormattingEnabled = True
        Me.cbEmpTrans.Location = New System.Drawing.Point(179, 26)
        Me.cbEmpTrans.Name = "cbEmpTrans"
        Me.cbEmpTrans.Size = New System.Drawing.Size(452, 21)
        Me.cbEmpTrans.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Empresa de Transportes"
        '
        'btnNuevoC
        '
        Me.btnNuevoC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoC.Image = CType(resources.GetObject("btnNuevoC.Image"), System.Drawing.Image)
        Me.btnNuevoC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoC.Location = New System.Drawing.Point(504, 124)
        Me.btnNuevoC.Name = "btnNuevoC"
        Me.btnNuevoC.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoC.TabIndex = 16
        Me.btnNuevoC.Text = "Nuevo"
        Me.btnNuevoC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoC.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(864, 124)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
        Me.btnCerrar.TabIndex = 20
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnCancelarC
        '
        Me.btnCancelarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelarC.Enabled = False
        Me.btnCancelarC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelarC.Image = CType(resources.GetObject("btnCancelarC.Image"), System.Drawing.Image)
        Me.btnCancelarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelarC.Location = New System.Drawing.Point(684, 124)
        Me.btnCancelarC.Name = "btnCancelarC"
        Me.btnCancelarC.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelarC.TabIndex = 18
        Me.btnCancelarC.Text = "Cancelar"
        Me.btnCancelarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelarC.UseVisualStyleBackColor = True
        '
        'btnEliminarC
        '
        Me.btnEliminarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarC.Image = CType(resources.GetObject("btnEliminarC.Image"), System.Drawing.Image)
        Me.btnEliminarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarC.Location = New System.Drawing.Point(774, 124)
        Me.btnEliminarC.Name = "btnEliminarC"
        Me.btnEliminarC.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarC.TabIndex = 19
        Me.btnEliminarC.Text = "Eliminar"
        Me.btnEliminarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarC.UseVisualStyleBackColor = True
        '
        'btnModificarC
        '
        Me.btnModificarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarC.Image = CType(resources.GetObject("btnModificarC.Image"), System.Drawing.Image)
        Me.btnModificarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarC.Location = New System.Drawing.Point(594, 124)
        Me.btnModificarC.Name = "btnModificarC"
        Me.btnModificarC.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarC.TabIndex = 17
        Me.btnModificarC.Text = "Modificar"
        Me.btnModificarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarC.UseVisualStyleBackColor = True
        '
        'btnModificarV
        '
        Me.btnModificarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarV.Image = CType(resources.GetObject("btnModificarV.Image"), System.Drawing.Image)
        Me.btnModificarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarV.Location = New System.Drawing.Point(154, 124)
        Me.btnModificarV.Name = "btnModificarV"
        Me.btnModificarV.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarV.TabIndex = 17
        Me.btnModificarV.Text = "Modificar"
        Me.btnModificarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarV.UseVisualStyleBackColor = True
        '
        'btnEliminarV
        '
        Me.btnEliminarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarV.Image = CType(resources.GetObject("btnEliminarV.Image"), System.Drawing.Image)
        Me.btnEliminarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarV.Location = New System.Drawing.Point(334, 124)
        Me.btnEliminarV.Name = "btnEliminarV"
        Me.btnEliminarV.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarV.TabIndex = 19
        Me.btnEliminarV.Text = "Eliminar"
        Me.btnEliminarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarV.UseVisualStyleBackColor = True
        '
        'btnCancelarV
        '
        Me.btnCancelarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelarV.Enabled = False
        Me.btnCancelarV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelarV.Image = CType(resources.GetObject("btnCancelarV.Image"), System.Drawing.Image)
        Me.btnCancelarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelarV.Location = New System.Drawing.Point(244, 124)
        Me.btnCancelarV.Name = "btnCancelarV"
        Me.btnCancelarV.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelarV.TabIndex = 18
        Me.btnCancelarV.Text = "Cancelar"
        Me.btnCancelarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelarV.UseVisualStyleBackColor = True
        '
        'btnNuevoV
        '
        Me.btnNuevoV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoV.Image = CType(resources.GetObject("btnNuevoV.Image"), System.Drawing.Image)
        Me.btnNuevoV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoV.Location = New System.Drawing.Point(64, 124)
        Me.btnNuevoV.Name = "btnNuevoV"
        Me.btnNuevoV.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoV.TabIndex = 16
        Me.btnNuevoV.Text = "Nuevo"
        Me.btnNuevoV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoV.UseVisualStyleBackColor = True
        '
        'MantVehiculoTransportistaFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(946, 450)
        Me.Controls.Add(Me.btnNuevoV)
        Me.Controls.Add(Me.btnNuevoC)
        Me.Controls.Add(Me.btnCancelarV)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnEliminarV)
        Me.Controls.Add(Me.btnCancelarC)
        Me.Controls.Add(Me.btnModificarV)
        Me.Controls.Add(Me.btnEliminarC)
        Me.Controls.Add(Me.btnModificarC)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbEmpTrans)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MantVehiculoTransportistaFrm"
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.GroupBox4, 0)
        Me.Controls.SetChildIndex(Me.cbEmpTrans, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.btnModificarC, 0)
        Me.Controls.SetChildIndex(Me.btnEliminarC, 0)
        Me.Controls.SetChildIndex(Me.btnModificarV, 0)
        Me.Controls.SetChildIndex(Me.btnCancelarC, 0)
        Me.Controls.SetChildIndex(Me.btnEliminarV, 0)
        Me.Controls.SetChildIndex(Me.btnCerrar, 0)
        Me.Controls.SetChildIndex(Me.btnCancelarV, 0)
        Me.Controls.SetChildIndex(Me.btnNuevoC, 0)
        Me.Controls.SetChildIndex(Me.btnNuevoV, 0)
        CType(Me.dgVehiculo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator2.ResumeLayout(False)
        Me.BindingNavigator2.PerformLayout()
        CType(Me.dgConductor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgVehiculo As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents dgConductor As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMarcaPlaca As System.Windows.Forms.TextBox
    Friend WithEvents txtConstancia As System.Windows.Forms.TextBox
    Friend WithEvents cbEmpTrans As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDni As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLicencia As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnCancelarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevoV As ComponentesSolucion2008.BottomSSP
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
    Friend WithEvents BindingNavigator2 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator5 As System.Windows.Forms.ToolStripSeparator

End Class
