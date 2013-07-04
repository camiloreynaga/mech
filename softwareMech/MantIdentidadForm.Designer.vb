<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantIdentidadForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantIdentidadForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtDetraccion = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtCue = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtRep = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCel = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtFax = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtRuc = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label9 = New System.Windows.Forms.Label
        Me.lbTabla2 = New System.Windows.Forms.ListBox
        Me.lbTabla1 = New System.Windows.Forms.ListBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtEma = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFono = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDir = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDNI = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNom = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cbBuscar = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtBuscar = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.Navigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.btnNuevo = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        Me.lblTitulo.Text = "Mantenimiento de Datos de Entidad"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtDetraccion)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtCue)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtRep)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtCel)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtFax)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtRuc)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.lbTabla2)
        Me.Panel1.Controls.Add(Me.lbTabla1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtEma)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtFono)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtDir)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtDNI)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtNom)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(889, 158)
        Me.Panel1.TabIndex = 1
        '
        'txtDetraccion
        '
        Me.txtDetraccion.Location = New System.Drawing.Point(563, 131)
        Me.txtDetraccion.Name = "txtDetraccion"
        Me.txtDetraccion.ReadOnly = True
        Me.txtDetraccion.Size = New System.Drawing.Size(319, 20)
        Me.txtDetraccion.TabIndex = 10
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(399, 134)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(159, 13)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "Cuenta para Detracciones:"
        '
        'txtCue
        '
        Me.txtCue.Location = New System.Drawing.Point(118, 131)
        Me.txtCue.Name = "txtCue"
        Me.txtCue.ReadOnly = True
        Me.txtCue.Size = New System.Drawing.Size(271, 20)
        Me.txtCue.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 134)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 13)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Cuenta Bancaria:"
        '
        'txtRep
        '
        Me.txtRep.Location = New System.Drawing.Point(118, 104)
        Me.txtRep.Name = "txtRep"
        Me.txtRep.ReadOnly = True
        Me.txtRep.Size = New System.Drawing.Size(271, 20)
        Me.txtRep.TabIndex = 6
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(46, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 13)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Contacto :"
        '
        'txtCel
        '
        Me.txtCel.Location = New System.Drawing.Point(534, 78)
        Me.txtCel.Name = "txtCel"
        Me.txtCel.ReadOnly = True
        Me.txtCel.Size = New System.Drawing.Size(256, 20)
        Me.txtCel.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(409, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(105, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Cel / RPM /RPC:"
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(199, 78)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = True
        Me.txtFax.Size = New System.Drawing.Size(190, 20)
        Me.txtFax.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(164, 82)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Fax :"
        '
        'txtRuc
        '
        Me.txtRuc.Location = New System.Drawing.Point(630, 19)
        Me.txtRuc.MaxLength = 11
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.ReadOnly = True
        Me.txtRuc.Size = New System.Drawing.Size(160, 20)
        Me.txtRuc.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(627, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "RUC :"
        '
        'lbTabla2
        '
        Me.lbTabla2.Enabled = False
        Me.lbTabla2.FormattingEnabled = True
        Me.lbTabla2.Items.AddRange(New Object() {"Activo", "Inactivo"})
        Me.lbTabla2.Location = New System.Drawing.Point(810, 19)
        Me.lbTabla2.Name = "lbTabla2"
        Me.lbTabla2.Size = New System.Drawing.Size(72, 43)
        Me.lbTabla2.TabIndex = 4
        '
        'lbTabla1
        '
        Me.lbTabla1.FormattingEnabled = True
        Me.lbTabla1.Location = New System.Drawing.Point(7, 17)
        Me.lbTabla1.Name = "lbTabla1"
        Me.lbTabla1.Size = New System.Drawing.Size(183, 56)
        Me.lbTabla1.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(807, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Estado"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Tipo Entidad :"
        '
        'txtEma
        '
        Me.txtEma.Location = New System.Drawing.Point(630, 105)
        Me.txtEma.Name = "txtEma"
        Me.txtEma.ReadOnly = True
        Me.txtEma.Size = New System.Drawing.Size(252, 20)
        Me.txtEma.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(587, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Email :"
        '
        'txtFono
        '
        Me.txtFono.Location = New System.Drawing.Point(630, 52)
        Me.txtFono.Name = "txtFono"
        Me.txtFono.ReadOnly = True
        Me.txtFono.Size = New System.Drawing.Size(160, 20)
        Me.txtFono.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(627, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Telefono :"
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(196, 52)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.ReadOnly = True
        Me.txtDir.Size = New System.Drawing.Size(413, 20)
        Me.txtDir.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(196, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Dirección"
        '
        'txtDNI
        '
        Me.txtDNI.Location = New System.Drawing.Point(444, 104)
        Me.txtDNI.MaxLength = 8
        Me.txtDNI.Name = "txtDNI"
        Me.txtDNI.ReadOnly = True
        Me.txtDNI.Size = New System.Drawing.Size(116, 20)
        Me.txtDNI.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(401, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "DNI :"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(196, 19)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.ReadOnly = True
        Me.txtNom.Size = New System.Drawing.Size(413, 20)
        Me.txtNom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(196, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Razon Social :"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.cbBuscar)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.txtBuscar)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Navigator1)
        Me.Panel2.Controls.Add(Me.dgTabla1)
        Me.Panel2.Location = New System.Drawing.Point(14, 187)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(889, 427)
        Me.Panel2.TabIndex = 2
        '
        'cbBuscar
        '
        Me.cbBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBuscar.FormattingEnabled = True
        Me.cbBuscar.Items.AddRange(New Object() {"R.U.C.", "RAZON SOCIAL"})
        Me.cbBuscar.Location = New System.Drawing.Point(225, 3)
        Me.cbBuscar.Name = "cbBuscar"
        Me.cbBuscar.Size = New System.Drawing.Size(115, 21)
        Me.cbBuscar.TabIndex = 1
        Me.cbBuscar.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(615, 7)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(169, 13)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Ingrese criterio de busquedá"
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(346, 4)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(263, 20)
        Me.txtBuscar.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(135, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Seleccione Identidad :"
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.BackColor = System.Drawing.SystemColors.Control
        Me.Navigator1.CountItem = Me.BindingNavigatorCountItem
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.Navigator1.Location = New System.Drawing.Point(0, 398)
        Me.Navigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.Navigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.Navigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.Navigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(885, 25)
        Me.Navigator1.TabIndex = 1
        Me.Navigator1.Text = "BindingNavigator1"
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
        Me.BindingNavigatorPositionItem.ReadOnly = True
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(1, 27)
        Me.dgTabla1.MultiSelect = False
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTabla1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgTabla1.Size = New System.Drawing.Size(884, 369)
        Me.dgTabla1.TabIndex = 4
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(384, 622)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevo.TabIndex = 3
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevo.UseVisualStyleBackColor = False
        '
        'btnModificar
        '
        Me.btnModificar.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.Image = CType(resources.GetObject("btnModificar.Image"), System.Drawing.Image)
        Me.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar.Location = New System.Drawing.Point(500, 623)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(84, 23)
        Me.btnModificar.TabIndex = 4
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(613, 623)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(722, 623)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminar.TabIndex = 6
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(829, 623)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
        Me.btnCerrar.TabIndex = 7
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'MantIdentidadForm
        '
        Me.AcceptButton = Me.btnNuevo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnModificar)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MantIdentidadForm"
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.btnModificar, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnEliminar, 0)
        Me.Controls.SetChildIndex(Me.btnCerrar, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNom As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtDNI As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDir As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFono As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEma As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbTabla2 As System.Windows.Forms.ListBox
    Friend WithEvents lbTabla1 As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Navigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnNuevo As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents txtRuc As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtFax As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCel As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtRep As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents cbBuscar As System.Windows.Forms.ComboBox
    Friend WithEvents txtCue As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDetraccion As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label

End Class
