<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantOrdenDesembCajaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantOrdenDesembCajaForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.btnCancelar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevo = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.txtLetraTotal = New System.Windows.Forms.TextBox
        Me.date1 = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnAnula = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.cbMon = New System.Windows.Forms.ComboBox
        Me.txtTot = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtDet = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.btnImprimir = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnElimina = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMon = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtSer = New System.Windows.Forms.TextBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtDato = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.cbObra = New System.Windows.Forms.ComboBox
        Me.txtBan = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.txtObs = New System.Windows.Forms.TextBox
        Me.txtEst = New System.Windows.Forms.TextBox
        Me.txtGer = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtOtro = New System.Windows.Forms.TextBox
        Me.txtEst1 = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.checkB7 = New System.Windows.Forms.CheckBox
        Me.checkB4 = New System.Windows.Forms.CheckBox
        Me.checkB6 = New System.Windows.Forms.CheckBox
        Me.checkB5 = New System.Windows.Forms.CheckBox
        Me.checkB3 = New System.Windows.Forms.CheckBox
        Me.checkB2 = New System.Windows.Forms.CheckBox
        Me.checkB1 = New System.Windows.Forms.CheckBox
        Me.txtObs1 = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtNom1 = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Panel0 = New System.Windows.Forms.Panel
        Me.Navigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal2 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal3 = New System.Windows.Forms.ToolStripTextBox
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel0.SuspendLayout()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        Me.lblTitulo.Text = "Formulario de Apertura de Orden de Desembolso Caja Chica"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtNro)
        Me.Panel1.Controls.Add(Me.btnCancelar)
        Me.Panel1.Controls.Add(Me.btnNuevo)
        Me.Panel1.Controls.Add(Me.txtLetraTotal)
        Me.Panel1.Controls.Add(Me.date1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnAnula)
        Me.Panel1.Controls.Add(Me.cbMon)
        Me.Panel1.Controls.Add(Me.txtTot)
        Me.Panel1.Controls.Add(Me.txtDet)
        Me.Panel1.Controls.Add(Me.btnImprimir)
        Me.Panel1.Controls.Add(Me.btnElimina)
        Me.Panel1.Controls.Add(Me.btnModificar)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtMon)
        Me.Panel1.Controls.Add(Me.txtSer)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(14, 355)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(892, 83)
        Me.Panel1.TabIndex = 3
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(125, 9)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.ReadOnly = True
        Me.txtNro.Size = New System.Drawing.Size(47, 20)
        Me.txtNro.TabIndex = 311
        Me.txtNro.TabStop = False
        Me.txtNro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(713, 56)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(81, 23)
        Me.btnCancelar.TabIndex = 334
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnNuevo
        '
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(626, 31)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(81, 27)
        Me.btnNuevo.TabIndex = 4
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnNuevo, "Aperturar nueva orden de desembolso sin orden de compra...")
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'txtLetraTotal
        '
        Me.txtLetraTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLetraTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLetraTotal.Location = New System.Drawing.Point(25, 36)
        Me.txtLetraTotal.Name = "txtLetraTotal"
        Me.txtLetraTotal.ReadOnly = True
        Me.txtLetraTotal.Size = New System.Drawing.Size(578, 20)
        Me.txtLetraTotal.TabIndex = 323
        Me.txtLetraTotal.TabStop = False
        Me.txtLetraTotal.Text = "SON:"
        '
        'date1
        '
        Me.date1.Enabled = False
        Me.date1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.date1.Location = New System.Drawing.Point(101, 37)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(107, 20)
        Me.date1.TabIndex = 312
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 332
        Me.Label1.Text = "Serie:"
        '
        'btnAnula
        '
        Me.btnAnula.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAnula.Image = CType(resources.GetObject("btnAnula.Image"), System.Drawing.Image)
        Me.btnAnula.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnula.Location = New System.Drawing.Point(800, 3)
        Me.btnAnula.Name = "btnAnula"
        Me.btnAnula.Size = New System.Drawing.Size(77, 25)
        Me.btnAnula.TabIndex = 331
        Me.btnAnula.TabStop = False
        Me.btnAnula.Text = "Anular"
        Me.btnAnula.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnAnula, "Anular orden de desembolso...")
        Me.btnAnula.UseVisualStyleBackColor = True
        '
        'cbMon
        '
        Me.cbMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMon.Enabled = False
        Me.cbMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbMon.FormattingEnabled = True
        Me.cbMon.IntegralHeight = False
        Me.cbMon.Location = New System.Drawing.Point(214, 9)
        Me.cbMon.Name = "cbMon"
        Me.cbMon.Size = New System.Drawing.Size(50, 21)
        Me.cbMon.TabIndex = 0
        '
        'txtTot
        '
        Me.txtTot.Location = New System.Drawing.Point(475, 36)
        Me.txtTot.Name = "txtTot"
        Me.txtTot.ReadOnly = True
        Me.txtTot.Size = New System.Drawing.Size(78, 20)
        Me.txtTot.TabIndex = 3
        Me.txtTot.TabStop = False
        Me.txtTot.Text = "0.00"
        Me.txtTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDet
        '
        Me.txtDet.Location = New System.Drawing.Point(352, 36)
        Me.txtDet.Name = "txtDet"
        Me.txtDet.ReadOnly = True
        Me.txtDet.Size = New System.Drawing.Size(78, 20)
        Me.txtDet.TabIndex = 2
        Me.txtDet.TabStop = False
        Me.txtDet.Text = "0.00"
        Me.txtDet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnImprimir
        '
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.Location = New System.Drawing.Point(800, 54)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(77, 25)
        Me.btnImprimir.TabIndex = 327
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Imprimir orden de desembolso...")
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnElimina
        '
        Me.btnElimina.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnElimina.Image = CType(resources.GetObject("btnElimina.Image"), System.Drawing.Image)
        Me.btnElimina.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElimina.Location = New System.Drawing.Point(800, 28)
        Me.btnElimina.Name = "btnElimina"
        Me.btnElimina.Size = New System.Drawing.Size(77, 25)
        Me.btnElimina.TabIndex = 326
        Me.btnElimina.TabStop = False
        Me.btnElimina.Text = "Eliminar"
        Me.btnElimina.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnElimina, "Elimina orden de desembolso...")
        Me.btnElimina.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.Image = CType(resources.GetObject("btnModificar.Image"), System.Drawing.Image)
        Me.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar.Location = New System.Drawing.Point(711, 4)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(81, 25)
        Me.btnModificar.TabIndex = 325
        Me.btnModificar.TabStop = False
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnModificar, "Guardar modificaciones de orden de desembolso...")
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(438, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 322
        Me.Label6.Text = "Total:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(300, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 320
        Me.Label5.Text = "Detrac.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(171, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 318
        Me.Label4.Text = "Monto:"
        '
        'txtMon
        '
        Me.txtMon.Location = New System.Drawing.Point(265, 9)
        Me.txtMon.Name = "txtMon"
        Me.txtMon.ReadOnly = True
        Me.txtMon.Size = New System.Drawing.Size(78, 20)
        Me.txtMon.TabIndex = 1
        Me.txtMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSer
        '
        Me.txtSer.Location = New System.Drawing.Point(66, 9)
        Me.txtSer.Name = "txtSer"
        Me.txtSer.ReadOnly = True
        Me.txtSer.Size = New System.Drawing.Size(39, 20)
        Me.txtSer.TabIndex = 314
        Me.txtSer.TabStop = False
        Me.txtSer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(425, 36)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 313
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(57, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 310
        Me.Label2.Text = "Fecha:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(106, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 308
        Me.Label3.Text = "Nº"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtDato)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.cbObra)
        Me.Panel2.Controls.Add(Me.txtBan)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Location = New System.Drawing.Point(14, 441)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(892, 91)
        Me.Panel2.TabIndex = 4
        '
        'txtDato
        '
        Me.txtDato.Location = New System.Drawing.Point(133, 60)
        Me.txtDato.Name = "txtDato"
        Me.txtDato.ReadOnly = True
        Me.txtDato.Size = New System.Drawing.Size(661, 20)
        Me.txtDato.TabIndex = 332
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 63)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(123, 13)
        Me.Label18.TabIndex = 331
        Me.Label18.Text = "Detalle Desembolso:"
        '
        'cbObra
        '
        Me.cbObra.DropDownHeight = 500
        Me.cbObra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbObra.DropDownWidth = 400
        Me.cbObra.Enabled = False
        Me.cbObra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbObra.FormattingEnabled = True
        Me.cbObra.IntegralHeight = False
        Me.cbObra.Location = New System.Drawing.Point(133, 6)
        Me.cbObra.Name = "cbObra"
        Me.cbObra.Size = New System.Drawing.Size(659, 21)
        Me.cbObra.TabIndex = 287
        '
        'txtBan
        '
        Me.txtBan.Location = New System.Drawing.Point(133, 34)
        Me.txtBan.Name = "txtBan"
        Me.txtBan.ReadOnly = True
        Me.txtBan.Size = New System.Drawing.Size(210, 20)
        Me.txtBan.TabIndex = 323
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(95, 13)
        Me.Label14.TabIndex = 319
        Me.Label14.Text = "Forma de pago:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 13)
        Me.Label7.TabIndex = 288
        Me.Label7.Text = "Sede / Obra Caja:"
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.txtObs)
        Me.Panel6.Controls.Add(Me.txtEst)
        Me.Panel6.Controls.Add(Me.txtGer)
        Me.Panel6.Controls.Add(Me.Label19)
        Me.Panel6.Controls.Add(Me.txtOtro)
        Me.Panel6.Controls.Add(Me.txtEst1)
        Me.Panel6.Controls.Add(Me.Label33)
        Me.Panel6.Controls.Add(Me.checkB7)
        Me.Panel6.Controls.Add(Me.checkB4)
        Me.Panel6.Controls.Add(Me.checkB6)
        Me.Panel6.Controls.Add(Me.checkB5)
        Me.Panel6.Controls.Add(Me.checkB3)
        Me.Panel6.Controls.Add(Me.checkB2)
        Me.Panel6.Controls.Add(Me.checkB1)
        Me.Panel6.Controls.Add(Me.txtObs1)
        Me.Panel6.Controls.Add(Me.Label35)
        Me.Panel6.Controls.Add(Me.txtNom1)
        Me.Panel6.Controls.Add(Me.Label32)
        Me.Panel6.Location = New System.Drawing.Point(14, 534)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(892, 119)
        Me.Panel6.TabIndex = 8
        '
        'txtObs
        '
        Me.txtObs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtObs.Location = New System.Drawing.Point(678, 100)
        Me.txtObs.Name = "txtObs"
        Me.txtObs.ReadOnly = True
        Me.txtObs.Size = New System.Drawing.Size(210, 13)
        Me.txtObs.TabIndex = 383
        Me.txtObs.TabStop = False
        '
        'txtEst
        '
        Me.txtEst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEst.Location = New System.Drawing.Point(677, 77)
        Me.txtEst.Name = "txtEst"
        Me.txtEst.Size = New System.Drawing.Size(210, 20)
        Me.txtEst.TabIndex = 382
        Me.txtEst.TabStop = False
        Me.txtEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGer
        '
        Me.txtGer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGer.Location = New System.Drawing.Point(677, 56)
        Me.txtGer.Name = "txtGer"
        Me.txtGer.ReadOnly = True
        Me.txtGer.Size = New System.Drawing.Size(210, 20)
        Me.txtGer.TabIndex = 381
        Me.txtGer.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(612, 59)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 13)
        Me.Label19.TabIndex = 380
        Me.Label19.Text = "Aprueba:"
        '
        'txtOtro
        '
        Me.txtOtro.Location = New System.Drawing.Point(104, 90)
        Me.txtOtro.Name = "txtOtro"
        Me.txtOtro.ReadOnly = True
        Me.txtOtro.Size = New System.Drawing.Size(166, 20)
        Me.txtOtro.TabIndex = 377
        '
        'txtEst1
        '
        Me.txtEst1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEst1.Location = New System.Drawing.Point(677, 25)
        Me.txtEst1.Name = "txtEst1"
        Me.txtEst1.Size = New System.Drawing.Size(210, 20)
        Me.txtEst1.TabIndex = 379
        Me.txtEst1.TabStop = False
        Me.txtEst1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(211, 13)
        Me.Label33.TabIndex = 378
        Me.Label33.Text = "RESPONSABILIDAD DE ENTREGA:"
        '
        'checkB7
        '
        Me.checkB7.AutoSize = True
        Me.checkB7.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB7.Location = New System.Drawing.Point(42, 90)
        Me.checkB7.Name = "checkB7"
        Me.checkB7.Size = New System.Drawing.Size(56, 17)
        Me.checkB7.TabIndex = 376
        Me.checkB7.Text = "Otros"
        Me.checkB7.UseVisualStyleBackColor = True
        '
        'checkB4
        '
        Me.checkB4.AutoSize = True
        Me.checkB4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB4.Location = New System.Drawing.Point(311, 61)
        Me.checkB4.Name = "checkB4"
        Me.checkB4.Size = New System.Drawing.Size(115, 17)
        Me.checkB4.TabIndex = 375
        Me.checkB4.Text = "Recibo Egresos"
        Me.checkB4.UseVisualStyleBackColor = True
        '
        'checkB6
        '
        Me.checkB6.AutoSize = True
        Me.checkB6.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB6.Location = New System.Drawing.Point(130, 61)
        Me.checkB6.Name = "checkB6"
        Me.checkB6.Size = New System.Drawing.Size(139, 17)
        Me.checkB6.TabIndex = 374
        Me.checkB6.Text = "Voucher Detracción"
        Me.checkB6.UseVisualStyleBackColor = True
        '
        'checkB5
        '
        Me.checkB5.AutoSize = True
        Me.checkB5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB5.Location = New System.Drawing.Point(25, 61)
        Me.checkB5.Name = "checkB5"
        Me.checkB5.Size = New System.Drawing.Size(73, 17)
        Me.checkB5.TabIndex = 373
        Me.checkB5.Text = "Voucher"
        Me.checkB5.UseVisualStyleBackColor = True
        '
        'checkB3
        '
        Me.checkB3.AutoSize = True
        Me.checkB3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB3.Location = New System.Drawing.Point(319, 25)
        Me.checkB3.Name = "checkB3"
        Me.checkB3.Size = New System.Drawing.Size(107, 17)
        Me.checkB3.TabIndex = 372
        Me.checkB3.Text = "Guia Remisión"
        Me.checkB3.UseVisualStyleBackColor = True
        '
        'checkB2
        '
        Me.checkB2.AutoSize = True
        Me.checkB2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB2.Location = New System.Drawing.Point(208, 25)
        Me.checkB2.Name = "checkB2"
        Me.checkB2.Size = New System.Drawing.Size(62, 17)
        Me.checkB2.TabIndex = 371
        Me.checkB2.Text = "Boleta"
        Me.checkB2.UseVisualStyleBackColor = True
        '
        'checkB1
        '
        Me.checkB1.AutoSize = True
        Me.checkB1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.checkB1.Location = New System.Drawing.Point(29, 26)
        Me.checkB1.Name = "checkB1"
        Me.checkB1.Size = New System.Drawing.Size(69, 17)
        Me.checkB1.TabIndex = 370
        Me.checkB1.Text = "Factura"
        Me.checkB1.UseVisualStyleBackColor = True
        '
        'txtObs1
        '
        Me.txtObs1.Location = New System.Drawing.Point(139, 90)
        Me.txtObs1.Name = "txtObs1"
        Me.txtObs1.Size = New System.Drawing.Size(110, 20)
        Me.txtObs1.TabIndex = 348
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(125, 93)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(86, 13)
        Me.Label35.TabIndex = 347
        Me.Label35.Text = "Observación :"
        '
        'txtNom1
        '
        Me.txtNom1.Location = New System.Drawing.Point(677, 4)
        Me.txtNom1.Name = "txtNom1"
        Me.txtNom1.ReadOnly = True
        Me.txtNom1.Size = New System.Drawing.Size(210, 20)
        Me.txtNom1.TabIndex = 342
        Me.txtNom1.TabStop = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(608, 7)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(71, 13)
        Me.Label32.TabIndex = 341
        Me.Label32.Text = "Solicitante:"
        '
        'Panel0
        '
        Me.Panel0.Controls.Add(Me.Navigator1)
        Me.Panel0.Controls.Add(Me.dgTabla1)
        Me.Panel0.Location = New System.Drawing.Point(14, 23)
        Me.Panel0.Name = "Panel0"
        Me.Panel0.Size = New System.Drawing.Size(892, 330)
        Me.Panel0.TabIndex = 9
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.BackColor = System.Drawing.SystemColors.Control
        Me.Navigator1.CountItem = Me.ToolStripLabel1
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator4, Me.ToolStripLabel2, Me.txtTotal, Me.ToolStripSeparator3, Me.ToolStripLabel3, Me.txtTotal1, Me.ToolStripSeparator5, Me.ToolStripLabel4, Me.txtTotal2, Me.ToolStripSeparator6, Me.ToolStripLabel5, Me.txtTotal3})
        Me.Navigator1.Location = New System.Drawing.Point(0, 305)
        Me.Navigator1.MoveFirstItem = Me.ToolStripButton1
        Me.Navigator1.MoveLastItem = Me.ToolStripButton4
        Me.Navigator1.MoveNextItem = Me.ToolStripButton3
        Me.Navigator1.MovePreviousItem = Me.ToolStripButton2
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.ToolStripTextBox1
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(892, 25)
        Me.Navigator1.TabIndex = 330
        Me.Navigator1.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(37, 22)
        Me.ToolStripLabel1.Text = "de {0}"
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
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(53, 22)
        Me.ToolStripLabel2.Text = "Total S/."
        '
        'txtTotal
        '
        Me.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(80, 25)
        Me.txtTotal.Text = "0.00"
        Me.txtTotal.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel3.Text = "Total $"
        '
        'txtTotal1
        '
        Me.txtTotal1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal1.Name = "txtTotal1"
        Me.txtTotal1.ReadOnly = True
        Me.txtTotal1.Size = New System.Drawing.Size(80, 25)
        Me.txtTotal1.Text = "0.00"
        Me.txtTotal1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(63, 22)
        Me.ToolStripLabel4.Text = "Detrac.S/."
        '
        'txtTotal2
        '
        Me.txtTotal2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal2.Name = "txtTotal2"
        Me.txtTotal2.ReadOnly = True
        Me.txtTotal2.Size = New System.Drawing.Size(70, 25)
        Me.txtTotal2.Text = "0.00"
        Me.txtTotal2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        Me.ToolStripLabel5.Size = New System.Drawing.Size(55, 22)
        Me.ToolStripLabel5.Text = "Detrac.$"
        '
        'txtTotal3
        '
        Me.txtTotal3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal3.Name = "txtTotal3"
        Me.txtTotal3.ReadOnly = True
        Me.txtTotal3.Size = New System.Drawing.Size(70, 25)
        Me.txtTotal3.Text = "0.00"
        Me.txtTotal3.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(0, 0)
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTabla1.Size = New System.Drawing.Size(892, 305)
        Me.dgTabla1.TabIndex = 329
        '
        'MantOrdenDesembCajaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel0)
        Me.Controls.Add(Me.Panel6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MantOrdenDesembCajaForm"
        Me.Controls.SetChildIndex(Me.Panel6, 0)
        Me.Controls.SetChildIndex(Me.Panel0, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel0.ResumeLayout(False)
        Me.Panel0.PerformLayout()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents date1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSer As System.Windows.Forms.TextBox
    Friend WithEvents txtMon As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTot As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDet As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtLetraTotal As System.Windows.Forms.TextBox
    Friend WithEvents btnImprimir As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnElimina As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbObra As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtBan As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents txtNom1 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtObs1 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cbMon As System.Windows.Forms.ComboBox
    Friend WithEvents btnAnula As ComponentesSolucion2008.BottomSSP
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtOtro As System.Windows.Forms.TextBox
    Friend WithEvents checkB7 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB4 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB6 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB5 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB3 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB2 As System.Windows.Forms.CheckBox
    Friend WithEvents checkB1 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel0 As System.Windows.Forms.Panel
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents Navigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtTotal As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtTotal1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel4 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtTotal2 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel5 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtTotal3 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents txtEst1 As System.Windows.Forms.TextBox
    Friend WithEvents txtObs As System.Windows.Forms.TextBox
    Friend WithEvents txtEst As System.Windows.Forms.TextBox
    Friend WithEvents txtGer As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDato As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnNuevo As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar As ComponentesSolucion2008.BottomSSP

End Class
