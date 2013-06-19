<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegistraDocCompra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegistraDocCompra))
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.cbMoneda = New System.Windows.Forms.ComboBox
        Me.TextBoxSSP1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtNroOrden = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.btnAperturar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.dtpFechaOrdCompra = New System.Windows.Forms.DateTimePicker
        Me.lblSolicitante = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbObra = New System.Windows.Forms.ComboBox
        Me.cbProveedor = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtObservacion = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPlazoEntrega = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.cbBuscar = New System.Windows.Forms.ComboBox
        Me.txtPre = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtCan = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label24 = New System.Windows.Forms.Label
        Me.btnAgrega = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCrear = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnProcesa = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Navigator2 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.TSModificar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.txtTotal = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.dgTabla2 = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rb2 = New System.Windows.Forms.RadioButton
        Me.rb1 = New System.Windows.Forms.RadioButton
        Me.txtIGV = New System.Windows.Forms.TextBox
        Me.CheckBoxIGV = New System.Windows.Forms.CheckBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.txtSub = New System.Windows.Forms.TextBox
        Me.lblTotal = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator2.SuspendLayout()
        CType(Me.dgTabla2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 607)
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.ComboBox1)
        Me.Panel2.Controls.Add(Me.cbMoneda)
        Me.Panel2.Controls.Add(Me.TextBoxSSP1)
        Me.Panel2.Controls.Add(Me.txtNroOrden)
        Me.Panel2.Controls.Add(Me.btnAperturar)
        Me.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.Panel2.Controls.Add(Me.dtpFechaOrdCompra)
        Me.Panel2.Controls.Add(Me.lblSolicitante)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.cbObra)
        Me.Panel2.Controls.Add(Me.cbProveedor)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtObservacion)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtPlazoEntrega)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Location = New System.Drawing.Point(20, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(818, 133)
        Me.Panel2.TabIndex = 336
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(109, 87)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 337
        '
        'cbMoneda
        '
        Me.cbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMoneda.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbMoneda.FormattingEnabled = True
        Me.cbMoneda.Location = New System.Drawing.Point(546, 58)
        Me.cbMoneda.Name = "cbMoneda"
        Me.cbMoneda.Size = New System.Drawing.Size(55, 21)
        Me.cbMoneda.TabIndex = 188
        '
        'TextBoxSSP1
        '
        Me.TextBoxSSP1.Location = New System.Drawing.Point(736, 36)
        Me.TextBoxSSP1.Name = "TextBoxSSP1"
        Me.TextBoxSSP1.ReadOnly = True
        Me.TextBoxSSP1.Size = New System.Drawing.Size(75, 20)
        Me.TextBoxSSP1.TabIndex = 26
        '
        'txtNroOrden
        '
        Me.txtNroOrden.Location = New System.Drawing.Point(546, 35)
        Me.txtNroOrden.Name = "txtNroOrden"
        Me.txtNroOrden.ReadOnly = True
        Me.txtNroOrden.Size = New System.Drawing.Size(75, 20)
        Me.txtNroOrden.TabIndex = 26
        '
        'btnAperturar
        '
        Me.btnAperturar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAperturar.Image = CType(resources.GetObject("btnAperturar.Image"), System.Drawing.Image)
        Me.btnAperturar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAperturar.Location = New System.Drawing.Point(614, 88)
        Me.btnAperturar.Name = "btnAperturar"
        Me.btnAperturar.Size = New System.Drawing.Size(93, 23)
        Me.btnAperturar.TabIndex = 9
        Me.btnAperturar.Text = "Aperturar"
        Me.btnAperturar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAperturar.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(349, 61)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'dtpFechaOrdCompra
        '
        Me.dtpFechaOrdCompra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaOrdCompra.Location = New System.Drawing.Point(109, 61)
        Me.dtpFechaOrdCompra.Name = "dtpFechaOrdCompra"
        Me.dtpFechaOrdCompra.Size = New System.Drawing.Size(104, 20)
        Me.dtpFechaOrdCompra.TabIndex = 0
        '
        'lblSolicitante
        '
        Me.lblSolicitante.AutoSize = True
        Me.lblSolicitante.Location = New System.Drawing.Point(85, 9)
        Me.lblSolicitante.Name = "lblSolicitante"
        Me.lblSolicitante.Size = New System.Drawing.Size(114, 13)
        Me.lblSolicitante.TabIndex = 23
        Me.lblSolicitante.Text = "Usuario Solicitante"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Solicitante :"
        '
        'cbObra
        '
        Me.cbObra.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbObra.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbObra.DropDownWidth = 193
        Me.cbObra.FormattingEnabled = True
        Me.cbObra.Location = New System.Drawing.Point(614, 3)
        Me.cbObra.Name = "cbObra"
        Me.cbObra.Size = New System.Drawing.Size(151, 21)
        Me.cbObra.TabIndex = 2
        '
        'cbProveedor
        '
        Me.cbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbProveedor.FormattingEnabled = True
        Me.cbProveedor.Items.AddRange(New Object() {"Seleccione"})
        Me.cbProveedor.Location = New System.Drawing.Point(88, 34)
        Me.cbProveedor.Name = "cbProveedor"
        Me.cbProveedor.Size = New System.Drawing.Size(365, 21)
        Me.cbProveedor.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Proveedor :"
        '
        'txtObservacion
        '
        Me.txtObservacion.Location = New System.Drawing.Point(349, 90)
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.ReadOnly = True
        Me.txtObservacion.Size = New System.Drawing.Size(210, 20)
        Me.txtObservacion.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(257, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Observación :"
        '
        'txtPlazoEntrega
        '
        Me.txtPlazoEntrega.Location = New System.Drawing.Point(740, 64)
        Me.txtPlazoEntrega.Name = "txtPlazoEntrega"
        Me.txtPlazoEntrega.ReadOnly = True
        Me.txtPlazoEntrega.Size = New System.Drawing.Size(45, 20)
        Me.txtPlazoEntrega.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Forma Pago:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(484, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Moneda:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(627, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(107, 13)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "Tipo de  Cambio :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(496, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Serie :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(219, 63)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(124, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Fecha Cancelación :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(627, 38)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(103, 13)
        Me.Label20.TabIndex = 14
        Me.Label20.Text = "Nro Documento :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 63)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 13)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Fecha Emision :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(500, 7)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(108, 13)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "Tipo Documento :"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.txtBuscar)
        Me.Panel4.Controls.Add(Me.cbBuscar)
        Me.Panel4.Controls.Add(Me.txtPre)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label23)
        Me.Panel4.Controls.Add(Me.txtCan)
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Controls.Add(Me.btnAgrega)
        Me.Panel4.Controls.Add(Me.btnCrear)
        Me.Panel4.Controls.Add(Me.btnProcesa)
        Me.Panel4.Controls.Add(Me.dgTabla1)
        Me.Panel4.Location = New System.Drawing.Point(14, 445)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(901, 180)
        Me.Panel4.TabIndex = 337
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(300, 2)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(169, 20)
        Me.txtBuscar.TabIndex = 2
        '
        'cbBuscar
        '
        Me.cbBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbBuscar.FormattingEnabled = True
        Me.cbBuscar.Items.AddRange(New Object() {"INSUMO", "CODIGO"})
        Me.cbBuscar.Location = New System.Drawing.Point(321, 1)
        Me.cbBuscar.Name = "cbBuscar"
        Me.cbBuscar.Size = New System.Drawing.Size(81, 21)
        Me.cbBuscar.TabIndex = 2
        Me.cbBuscar.TabStop = False
        '
        'txtPre
        '
        Me.txtPre.Location = New System.Drawing.Point(542, 2)
        Me.txtPre.Name = "txtPre"
        Me.txtPre.Size = New System.Drawing.Size(65, 20)
        Me.txtPre.TabIndex = 3
        Me.txtPre.Text = "0"
        Me.txtPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(475, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 313
        Me.Label7.Text = "Prec_Uni.:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(214, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 13)
        Me.Label23.TabIndex = 311
        Me.Label23.Text = "Digite insumo:"
        '
        'txtCan
        '
        Me.txtCan.Location = New System.Drawing.Point(159, 2)
        Me.txtCan.Name = "txtCan"
        Me.txtCan.Size = New System.Drawing.Size(52, 20)
        Me.txtCan.TabIndex = 1
        Me.txtCan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(118, 6)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(41, 13)
        Me.Label24.TabIndex = 308
        Me.Label24.Text = "Cant.:"
        '
        'btnAgrega
        '
        Me.btnAgrega.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAgrega.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgrega.Image = CType(resources.GetObject("btnAgrega.Image"), System.Drawing.Image)
        Me.btnAgrega.Location = New System.Drawing.Point(611, 1)
        Me.btnAgrega.Name = "btnAgrega"
        Me.btnAgrega.Size = New System.Drawing.Size(25, 23)
        Me.btnAgrega.TabIndex = 4
        Me.btnAgrega.TabStop = False
        Me.btnAgrega.UseVisualStyleBackColor = True
        '
        'btnCrear
        '
        Me.btnCrear.BackColor = System.Drawing.SystemColors.Control
        Me.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCrear.Image = CType(resources.GetObject("btnCrear.Image"), System.Drawing.Image)
        Me.btnCrear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCrear.Location = New System.Drawing.Point(830, 1)
        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Size = New System.Drawing.Size(60, 23)
        Me.btnCrear.TabIndex = 6
        Me.btnCrear.TabStop = False
        Me.btnCrear.Text = "Crear"
        Me.btnCrear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCrear.UseVisualStyleBackColor = False
        '
        'btnProcesa
        '
        Me.btnProcesa.BackColor = System.Drawing.SystemColors.Control
        Me.btnProcesa.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnProcesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcesa.Image = CType(resources.GetObject("btnProcesa.Image"), System.Drawing.Image)
        Me.btnProcesa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProcesa.Location = New System.Drawing.Point(778, 1)
        Me.btnProcesa.Name = "btnProcesa"
        Me.btnProcesa.Size = New System.Drawing.Size(49, 23)
        Me.btnProcesa.TabIndex = 5
        Me.btnProcesa.TabStop = False
        Me.btnProcesa.Text = "F5"
        Me.btnProcesa.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProcesa.UseVisualStyleBackColor = False
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(0, 25)
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgTabla1.Size = New System.Drawing.Size(900, 153)
        Me.dgTabla1.TabIndex = 7
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Navigator2)
        Me.Panel3.Controls.Add(Me.dgTabla2)
        Me.Panel3.Location = New System.Drawing.Point(14, 172)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(901, 198)
        Me.Panel3.TabIndex = 338
        '
        'Navigator2
        '
        Me.Navigator2.AddNewItem = Nothing
        Me.Navigator2.BackColor = System.Drawing.SystemColors.Control
        Me.Navigator2.CountItem = Me.ToolStripLabel1
        Me.Navigator2.CountItemFormat = "de {0} insumos"
        Me.Navigator2.DeleteItem = Nothing
        Me.Navigator2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator3, Me.TSModificar, Me.ToolStripSeparator4, Me.ToolStripButton5, Me.ToolStripSeparator6, Me.ToolStripProgressBar1, Me.ToolStripSeparator5, Me.txtTotal, Me.ToolStripLabel2, Me.ToolStripSeparator7, Me.ToolStripSeparator8, Me.ToolStripButton6})
        Me.Navigator2.Location = New System.Drawing.Point(0, 173)
        Me.Navigator2.MoveFirstItem = Me.ToolStripButton1
        Me.Navigator2.MoveLastItem = Me.ToolStripButton4
        Me.Navigator2.MoveNextItem = Me.ToolStripButton3
        Me.Navigator2.MovePreviousItem = Me.ToolStripButton2
        Me.Navigator2.Name = "Navigator2"
        Me.Navigator2.PositionItem = Me.ToolStripTextBox1
        Me.Navigator2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator2.Size = New System.Drawing.Size(901, 25)
        Me.Navigator2.TabIndex = 301
        Me.Navigator2.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(85, 22)
        Me.ToolStripLabel1.Text = "de {0} insumos"
        Me.ToolStripLabel1.ToolTipText = "Número total de elementos"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "Mover primero"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "Mover anterior"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Posición"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.ReadOnly = True
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(50, 23)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolStripTextBox1.ToolTipText = "Posición actual"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "Mover siguiente"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton4.Text = "Mover último"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'TSModificar
        '
        Me.TSModificar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSModificar.ForeColor = System.Drawing.Color.Navy
        Me.TSModificar.Image = CType(resources.GetObject("TSModificar.Image"), System.Drawing.Image)
        Me.TSModificar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSModificar.Name = "TSModificar"
        Me.TSModificar.Size = New System.Drawing.Size(158, 22)
        Me.TSModificar.Text = "Guardar Modificaciones"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton5.ForeColor = System.Drawing.Color.Navy
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(136, 22)
        Me.ToolStripButton5.Text = "Imprimir Cotización"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(50, 22)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'txtTotal
        '
        Me.txtTotal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.txtTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(80, 25)
        Me.txtTotal.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(47, 22)
        Me.ToolStripLabel2.Text = "TOTAL:"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton6.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(114, 22)
        Me.ToolStripButton6.Text = "Eliminar insumo"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'dgTabla2
        '
        Me.dgTabla2.AllowUserToAddRows = False
        Me.dgTabla2.AllowUserToDeleteRows = False
        Me.dgTabla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla2.Location = New System.Drawing.Point(1, 0)
        Me.dgTabla2.Name = "dgTabla2"
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla2.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgTabla2.Size = New System.Drawing.Size(900, 173)
        Me.dgTabla2.TabIndex = 15
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb2)
        Me.GroupBox1.Controls.Add(Me.rb1)
        Me.GroupBox1.Location = New System.Drawing.Point(483, 381)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(175, 40)
        Me.GroupBox1.TabIndex = 345
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Calculo de I.G.V."
        '
        'rb2
        '
        Me.rb2.AutoSize = True
        Me.rb2.Location = New System.Drawing.Point(97, 17)
        Me.rb2.Name = "rb2"
        Me.rb2.Size = New System.Drawing.Size(61, 17)
        Me.rb2.TabIndex = 1
        Me.rb2.Text = "Tipo 2"
        Me.rb2.UseVisualStyleBackColor = True
        '
        'rb1
        '
        Me.rb1.AutoSize = True
        Me.rb1.Checked = True
        Me.rb1.Location = New System.Drawing.Point(15, 17)
        Me.rb1.Name = "rb1"
        Me.rb1.Size = New System.Drawing.Size(61, 17)
        Me.rb1.TabIndex = 0
        Me.rb1.TabStop = True
        Me.rb1.Text = "Tipo 1"
        Me.rb1.UseVisualStyleBackColor = True
        '
        'txtIGV
        '
        Me.txtIGV.Location = New System.Drawing.Point(813, 399)
        Me.txtIGV.Name = "txtIGV"
        Me.txtIGV.ReadOnly = True
        Me.txtIGV.Size = New System.Drawing.Size(92, 20)
        Me.txtIGV.TabIndex = 342
        Me.txtIGV.TabStop = False
        Me.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CheckBoxIGV
        '
        Me.CheckBoxIGV.AutoSize = True
        Me.CheckBoxIGV.Location = New System.Drawing.Point(730, 402)
        Me.CheckBoxIGV.Name = "CheckBoxIGV"
        Me.CheckBoxIGV.Size = New System.Drawing.Size(81, 17)
        Me.CheckBoxIGV.TabIndex = 344
        Me.CheckBoxIGV.TabStop = False
        Me.CheckBoxIGV.Text = "Sin I.G.V."
        Me.CheckBoxIGV.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(813, 419)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(92, 20)
        Me.TextBox1.TabIndex = 343
        Me.TextBox1.TabStop = False
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSub
        '
        Me.txtSub.Location = New System.Drawing.Point(813, 379)
        Me.txtSub.Name = "txtSub"
        Me.txtSub.ReadOnly = True
        Me.txtSub.Size = New System.Drawing.Size(92, 20)
        Me.txtSub.TabIndex = 341
        Me.txtSub.TabStop = False
        Me.txtSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(733, 424)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(69, 13)
        Me.lblTotal.TabIndex = 340
        Me.lblTotal.Text = "TOTAL S/."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(731, 386)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 13)
        Me.Label13.TabIndex = 339
        Me.Label13.Text = "SUB TOTAL"
        '
        'RegistraDocCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(915, 652)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtIGV)
        Me.Controls.Add(Me.CheckBoxIGV)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtSub)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "RegistraDocCompra"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.lblTotal, 0)
        Me.Controls.SetChildIndex(Me.txtSub, 0)
        Me.Controls.SetChildIndex(Me.TextBox1, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxIGV, 0)
        Me.Controls.SetChildIndex(Me.txtIGV, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator2.ResumeLayout(False)
        Me.Navigator2.PerformLayout()
        CType(Me.dgTabla2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents txtNroOrden As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents btnAperturar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents dtpFechaOrdCompra As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSolicitante As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbObra As System.Windows.Forms.ComboBox
    Friend WithEvents cbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtObservacion As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPlazoEntrega As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSSP1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents cbBuscar As System.Windows.Forms.ComboBox
    Friend WithEvents txtPre As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCan As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnAgrega As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCrear As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnProcesa As ComponentesSolucion2008.BottomSSP
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Navigator2 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSModificar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtTotal As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgTabla2 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtIGV As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIGV As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSub As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label

End Class
