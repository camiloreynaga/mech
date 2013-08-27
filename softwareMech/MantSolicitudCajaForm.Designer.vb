<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantSolicitudCajaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantSolicitudCajaForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnImprimir = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.lbSolicitud = New System.Windows.Forms.ListBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtObra = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSolicitante = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnElimina = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnSolicitud = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.txtTotal = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.dgDetalleSol = New System.Windows.Forms.DataGridView
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
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.txtInsumo = New System.Windows.Forms.TextBox
        Me.dgInsumo = New System.Windows.Forms.DataGridView
        Me.cbBuscar = New System.Windows.Forms.ComboBox
        Me.txtPrecio = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtCan = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label24 = New System.Windows.Forms.Label
        Me.btnAgrega = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCrear = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnProcesa = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtObs = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cbArea = New System.Windows.Forms.ComboBox
        Me.Panel1.SuspendLayout()
        CType(Me.dgDetalleSol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgInsumo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(925, 23)
        Me.lblTitulo.Text = "Solicitud de caja chica"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 515)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cbArea)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.btnImprimir)
        Me.Panel1.Controls.Add(Me.lbSolicitud)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtObra)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtSolicitante)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnElimina)
        Me.Panel1.Controls.Add(Me.btnModificar)
        Me.Panel1.Controls.Add(Me.btnSolicitud)
        Me.Panel1.Controls.Add(Me.dtpFecha)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtNro)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Location = New System.Drawing.Point(20, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 113)
        Me.Panel1.TabIndex = 5
        '
        'btnImprimir
        '
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.Location = New System.Drawing.Point(211, 85)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(25, 23)
        Me.btnImprimir.TabIndex = 308
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'lbSolicitud
        '
        Me.lbSolicitud.FormattingEnabled = True
        Me.lbSolicitud.Location = New System.Drawing.Point(16, 13)
        Me.lbSolicitud.Name = "lbSolicitud"
        Me.lbSolicitud.Size = New System.Drawing.Size(113, 95)
        Me.lbSolicitud.TabIndex = 306
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 307
        Me.Label5.Text = "NºSolicitud:"
        '
        'txtObra
        '
        Me.txtObra.Location = New System.Drawing.Point(224, 34)
        Me.txtObra.Name = "txtObra"
        Me.txtObra.ReadOnly = True
        Me.txtObra.Size = New System.Drawing.Size(370, 20)
        Me.txtObra.TabIndex = 299
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(139, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 298
        Me.Label7.Text = "Obra / Lugar:"
        '
        'txtSolicitante
        '
        Me.txtSolicitante.Location = New System.Drawing.Point(224, 59)
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.ReadOnly = True
        Me.txtSolicitante.Size = New System.Drawing.Size(370, 20)
        Me.txtSolicitante.TabIndex = 299
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(151, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 298
        Me.Label4.Text = "Solicitante:"
        '
        'btnElimina
        '
        Me.btnElimina.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnElimina.Image = CType(resources.GetObject("btnElimina.Image"), System.Drawing.Image)
        Me.btnElimina.Location = New System.Drawing.Point(180, 85)
        Me.btnElimina.Name = "btnElimina"
        Me.btnElimina.Size = New System.Drawing.Size(25, 23)
        Me.btnElimina.TabIndex = 297
        Me.btnElimina.TabStop = False
        Me.btnElimina.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.Image = CType(resources.GetObject("btnModificar.Image"), System.Drawing.Image)
        Me.btnModificar.Location = New System.Drawing.Point(149, 85)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(25, 23)
        Me.btnModificar.TabIndex = 295
        Me.btnModificar.TabStop = False
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'btnSolicitud
        '
        Me.btnSolicitud.BackColor = System.Drawing.SystemColors.Control
        Me.btnSolicitud.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSolicitud.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnSolicitud.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Olive
        Me.btnSolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSolicitud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSolicitud.Image = CType(resources.GetObject("btnSolicitud.Image"), System.Drawing.Image)
        Me.btnSolicitud.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSolicitud.Location = New System.Drawing.Point(434, 1)
        Me.btnSolicitud.Name = "btnSolicitud"
        Me.btnSolicitud.Size = New System.Drawing.Size(133, 26)
        Me.btnSolicitud.TabIndex = 13
        Me.btnSolicitud.Text = "Nueva Solicitud"
        Me.btnSolicitud.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSolicitud.UseVisualStyleBackColor = False
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(224, 5)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(107, 20)
        Me.dtpFecha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(151, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Fecha Sol.:"
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(381, 4)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.ReadOnly = True
        Me.txtNro.Size = New System.Drawing.Size(47, 20)
        Me.txtNro.TabIndex = 1
        Me.txtNro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(336, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "NºSol.:"
        '
        'btnCerrar
        '
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(492, 5)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
        Me.btnCerrar.TabIndex = 309
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(768, 295)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(87, 20)
        Me.txtTotal.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(722, 298)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Total:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(171, 13)
        Me.Label6.TabIndex = 316
        Me.Label6.Text = "Requerimiento semanal obra:"
        '
        'dgDetalleSol
        '
        Me.dgDetalleSol.AllowUserToAddRows = False
        Me.dgDetalleSol.AllowUserToDeleteRows = False
        Me.dgDetalleSol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgDetalleSol.Location = New System.Drawing.Point(20, 158)
        Me.dgDetalleSol.Name = "dgDetalleSol"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDetalleSol.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgDetalleSol.Size = New System.Drawing.Size(901, 129)
        Me.dgDetalleSol.TabIndex = 315
        '
        'Navigator2
        '
        Me.Navigator2.AddNewItem = Nothing
        Me.Navigator2.BackColor = System.Drawing.SystemColors.Control
        Me.Navigator2.CountItem = Me.ToolStripLabel1
        Me.Navigator2.CountItemFormat = "de {0} requerimientos"
        Me.Navigator2.DeleteItem = Nothing
        Me.Navigator2.Dock = System.Windows.Forms.DockStyle.None
        Me.Navigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator3, Me.ToolStripButton5, Me.ToolStripSeparator4, Me.ToolStripSeparator5, Me.ToolStripButton6})
        Me.Navigator2.Location = New System.Drawing.Point(20, 290)
        Me.Navigator2.MoveFirstItem = Me.ToolStripButton1
        Me.Navigator2.MoveLastItem = Me.ToolStripButton4
        Me.Navigator2.MoveNextItem = Me.ToolStripButton3
        Me.Navigator2.MovePreviousItem = Me.ToolStripButton2
        Me.Navigator2.Name = "Navigator2"
        Me.Navigator2.PositionItem = Me.ToolStripTextBox1
        Me.Navigator2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator2.Size = New System.Drawing.Size(622, 25)
        Me.Navigator2.TabIndex = 317
        Me.Navigator2.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripLabel1.Text = "de {0} requerimientos"
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
        'ToolStripButton5
        '
        Me.ToolStripButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton5.ForeColor = System.Drawing.Color.Navy
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(158, 22)
        Me.ToolStripButton5.Text = "Guardar Modificaciones"
        Me.ToolStripButton5.ToolTipText = "Guardar Modificaciones establecidad en grilla..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton6.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(158, 22)
        Me.ToolStripButton6.Text = "Eliminar Requerimiento"
        Me.ToolStripButton6.ToolTipText = "Eliminar Requerimiento de obra..."
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.txtObs)
        Me.Panel4.Controls.Add(Me.txtInsumo)
        Me.Panel4.Controls.Add(Me.dgInsumo)
        Me.Panel4.Controls.Add(Me.cbBuscar)
        Me.Panel4.Controls.Add(Me.txtPrecio)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label22)
        Me.Panel4.Controls.Add(Me.Label23)
        Me.Panel4.Controls.Add(Me.txtCan)
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Controls.Add(Me.btnAgrega)
        Me.Panel4.Controls.Add(Me.btnCrear)
        Me.Panel4.Controls.Add(Me.btnProcesa)
        Me.Panel4.Location = New System.Drawing.Point(17, 355)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(904, 183)
        Me.Panel4.TabIndex = 318
        '
        'txtInsumo
        '
        Me.txtInsumo.Location = New System.Drawing.Point(193, 2)
        Me.txtInsumo.Name = "txtInsumo"
        Me.txtInsumo.Size = New System.Drawing.Size(169, 20)
        Me.txtInsumo.TabIndex = 2
        '
        'dgInsumo
        '
        Me.dgInsumo.AllowUserToAddRows = False
        Me.dgInsumo.AllowUserToDeleteRows = False
        Me.dgInsumo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInsumo.Location = New System.Drawing.Point(0, 25)
        Me.dgInsumo.Name = "dgInsumo"
        Me.dgInsumo.ReadOnly = True
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgInsumo.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgInsumo.Size = New System.Drawing.Size(900, 153)
        Me.dgInsumo.TabIndex = 7
        '
        'cbBuscar
        '
        Me.cbBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbBuscar.FormattingEnabled = True
        Me.cbBuscar.Items.AddRange(New Object() {"INSUMO", "CODIGO"})
        Me.cbBuscar.Location = New System.Drawing.Point(329, 51)
        Me.cbBuscar.Name = "cbBuscar"
        Me.cbBuscar.Size = New System.Drawing.Size(81, 21)
        Me.cbBuscar.TabIndex = 2
        Me.cbBuscar.TabStop = False
        '
        'txtPrecio
        '
        Me.txtPrecio.Location = New System.Drawing.Point(435, 2)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(65, 20)
        Me.txtPrecio.TabIndex = 3
        Me.txtPrecio.Text = "0.00"
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(368, 5)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 13)
        Me.Label22.TabIndex = 313
        Me.Label22.Text = "Prec_Uni.:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(107, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 13)
        Me.Label23.TabIndex = 311
        Me.Label23.Text = "Digite insumo:"
        '
        'txtCan
        '
        Me.txtCan.Location = New System.Drawing.Point(49, 2)
        Me.txtCan.Name = "txtCan"
        Me.txtCan.Size = New System.Drawing.Size(52, 20)
        Me.txtCan.TabIndex = 1
        Me.txtCan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(8, 7)
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
        Me.btnAgrega.Location = New System.Drawing.Point(747, 2)
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
        Me.btnCrear.DialogResult = System.Windows.Forms.DialogResult.Cancel
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
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(508, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 311
        Me.Label8.Text = "Obs:"
        '
        'txtObs
        '
        Me.txtObs.Location = New System.Drawing.Point(547, 3)
        Me.txtObs.Name = "txtObs"
        Me.txtObs.Size = New System.Drawing.Size(169, 20)
        Me.txtObs.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(269, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 310
        Me.Label9.Text = "Área:"
        '
        'cbArea
        '
        Me.cbArea.FormattingEnabled = True
        Me.cbArea.Location = New System.Drawing.Point(312, 87)
        Me.cbArea.Name = "cbArea"
        Me.cbArea.Size = New System.Drawing.Size(167, 21)
        Me.cbArea.TabIndex = 311
        '
        'MantSolicitudCajaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(925, 560)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Navigator2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dgDetalleSol)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.Label3)
        Me.Name = "MantSolicitudCajaForm"
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtTotal, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.dgDetalleSol, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Navigator2, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgDetalleSol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator2.ResumeLayout(False)
        Me.Navigator2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgInsumo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnImprimir As ComponentesSolucion2008.BottomSSP
    Friend WithEvents lbSolicitud As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSolicitante As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnElimina As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSolicitud As ComponentesSolucion2008.BottomSSP
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgDetalleSol As System.Windows.Forms.DataGridView
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
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtInsumo As System.Windows.Forms.TextBox
    Friend WithEvents dgInsumo As System.Windows.Forms.DataGridView
    Friend WithEvents cbBuscar As System.Windows.Forms.ComboBox
    Friend WithEvents txtPrecio As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCan As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnAgrega As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCrear As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnProcesa As ComponentesSolucion2008.BottomSSP
    Friend WithEvents txtObra As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtObs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbArea As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
