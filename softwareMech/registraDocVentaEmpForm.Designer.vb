<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class registraDocVentaEmpForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(registraDocVentaEmpForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbDoc = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.cbSerie = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtHist = New System.Windows.Forms.TextBox
        Me.txtDato = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.GB3 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.date1 = New System.Windows.Forms.DateTimePicker
        Me.cbMes = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnImprimir = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnAnula = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCierra = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.txtDir = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnNuevo = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.cbCli = New System.Windows.Forms.ComboBox
        Me.txtRuc = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtBuscar = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtTotal = New System.Windows.Forms.TextBox
        Me.lblTotal = New System.Windows.Forms.Label
        Me.txtIGV = New System.Windows.Forms.TextBox
        Me.CheckBoxIGV = New System.Windows.Forms.CheckBox
        Me.txtSub = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rb2 = New System.Windows.Forms.RadioButton
        Me.rb1 = New System.Windows.Forms.RadioButton
        Me.txtLetraTotal = New System.Windows.Forms.TextBox
        Me.txtDes2 = New System.Windows.Forms.TextBox
        Me.txtTot2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtPre2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtDet2 = New System.Windows.Forms.TextBox
        Me.txtCan2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtDes1 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtTot1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtPre1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtDet1 = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCan1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.cbMoneda = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cbObra = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GB3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        Me.lblTitulo.Text = "Registro de Facturas Mech"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 513)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lbDoc)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtNro)
        Me.Panel1.Controls.Add(Me.cbSerie)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(116, 194)
        Me.Panel1.TabIndex = 0
        '
        'lbDoc
        '
        Me.lbDoc.FormattingEnabled = True
        Me.lbDoc.Location = New System.Drawing.Point(1, 56)
        Me.lbDoc.Name = "lbDoc"
        Me.lbDoc.Size = New System.Drawing.Size(111, 134)
        Me.lbDoc.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 307
        Me.Label3.Text = "NºDocVenta:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(62, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Nº"
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(62, 17)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.ReadOnly = True
        Me.txtNro.Size = New System.Drawing.Size(41, 20)
        Me.txtNro.TabIndex = 2
        Me.txtNro.TabStop = False
        Me.txtNro.Text = "1000"
        '
        'cbSerie
        '
        Me.cbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSerie.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbSerie.FormattingEnabled = True
        Me.cbSerie.Location = New System.Drawing.Point(6, 16)
        Me.cbSerie.Name = "cbSerie"
        Me.cbSerie.Size = New System.Drawing.Size(50, 21)
        Me.cbSerie.TabIndex = 1
        Me.cbSerie.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Serie:"
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(39, 64)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 276
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtHist)
        Me.Panel2.Controls.Add(Me.txtDato)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.GB3)
        Me.Panel2.Controls.Add(Me.btnImprimir)
        Me.Panel2.Controls.Add(Me.btnAnula)
        Me.Panel2.Controls.Add(Me.btnCierra)
        Me.Panel2.Controls.Add(Me.txtDir)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnNuevo)
        Me.Panel2.Controls.Add(Me.btnEliminar)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnModificar)
        Me.Panel2.Controls.Add(Me.cbCli)
        Me.Panel2.Controls.Add(Me.txtRuc)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtBuscar)
        Me.Panel2.Location = New System.Drawing.Point(131, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(774, 194)
        Me.Panel2.TabIndex = 1
        '
        'txtHist
        '
        Me.txtHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHist.Location = New System.Drawing.Point(177, 139)
        Me.txtHist.Name = "txtHist"
        Me.txtHist.ReadOnly = True
        Me.txtHist.Size = New System.Drawing.Size(591, 20)
        Me.txtHist.TabIndex = 8
        Me.txtHist.TabStop = False
        '
        'txtDato
        '
        Me.txtDato.Location = New System.Drawing.Point(177, 116)
        Me.txtDato.Name = "txtDato"
        Me.txtDato.ReadOnly = True
        Me.txtDato.Size = New System.Drawing.Size(326, 20)
        Me.txtDato.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(174, 104)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(38, 13)
        Me.Label17.TabIndex = 354
        Me.Label17.Text = "Dato:"
        '
        'GB3
        '
        Me.GB3.Controls.Add(Me.Label8)
        Me.GB3.Controls.Add(Me.date1)
        Me.GB3.Controls.Add(Me.cbMes)
        Me.GB3.Controls.Add(Me.Label10)
        Me.GB3.Location = New System.Drawing.Point(1, 2)
        Me.GB3.Name = "GB3"
        Me.GB3.Size = New System.Drawing.Size(168, 92)
        Me.GB3.TabIndex = 0
        Me.GB3.TabStop = False
        Me.GB3.Text = "Periodo:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 333
        Me.Label8.Text = "Fecha:"
        '
        'date1
        '
        Me.date1.Enabled = False
        Me.date1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.date1.Location = New System.Drawing.Point(7, 66)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(107, 20)
        Me.date1.TabIndex = 2
        '
        'cbMes
        '
        Me.cbMes.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMes.Enabled = False
        Me.cbMes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMes.ForeColor = System.Drawing.Color.White
        Me.cbMes.FormattingEnabled = True
        Me.cbMes.Location = New System.Drawing.Point(7, 27)
        Me.cbMes.Name = "cbMes"
        Me.cbMes.Size = New System.Drawing.Size(153, 24)
        Me.cbMes.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Mes:"
        '
        'btnImprimir
        '
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.Location = New System.Drawing.Point(4, 166)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(73, 23)
        Me.btnImprimir.TabIndex = 15
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnAnula
        '
        Me.btnAnula.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAnula.Image = CType(resources.GetObject("btnAnula.Image"), System.Drawing.Image)
        Me.btnAnula.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnula.Location = New System.Drawing.Point(4, 114)
        Me.btnAnula.Name = "btnAnula"
        Me.btnAnula.Size = New System.Drawing.Size(71, 23)
        Me.btnAnula.TabIndex = 13
        Me.btnAnula.TabStop = False
        Me.btnAnula.Text = "Anular."
        Me.btnAnula.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnula.UseVisualStyleBackColor = True
        '
        'btnCierra
        '
        Me.btnCierra.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCierra.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCierra.Image = CType(resources.GetObject("btnCierra.Image"), System.Drawing.Image)
        Me.btnCierra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCierra.Location = New System.Drawing.Point(4, 140)
        Me.btnCierra.Name = "btnCierra"
        Me.btnCierra.Size = New System.Drawing.Size(73, 24)
        Me.btnCierra.TabIndex = 14
        Me.btnCierra.TabStop = False
        Me.btnCierra.Text = "Cerrar."
        Me.btnCierra.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCierra.UseVisualStyleBackColor = True
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(275, 56)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.ReadOnly = True
        Me.txtDir.Size = New System.Drawing.Size(494, 20)
        Me.txtDir.TabIndex = 6
        Me.txtDir.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(272, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 348
        Me.Label7.Text = "Direc.:"
        '
        'btnNuevo
        '
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(110, 166)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(76, 23)
        Me.btnNuevo.TabIndex = 9
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(360, 165)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminar.TabIndex = 12
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(190, 166)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.Image = CType(resources.GetObject("btnModificar.Image"), System.Drawing.Image)
        Me.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar.Location = New System.Drawing.Point(280, 166)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(76, 23)
        Me.btnModificar.TabIndex = 11
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'cbCli
        '
        Me.cbCli.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCli.DropDownWidth = 500
        Me.cbCli.Enabled = False
        Me.cbCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCli.FormattingEnabled = True
        Me.cbCli.Location = New System.Drawing.Point(284, 16)
        Me.cbCli.MaxDropDownItems = 50
        Me.cbCli.Name = "cbCli"
        Me.cbCli.Size = New System.Drawing.Size(367, 21)
        Me.cbCli.TabIndex = 4
        '
        'txtRuc
        '
        Me.txtRuc.Location = New System.Drawing.Point(178, 56)
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.ReadOnly = True
        Me.txtRuc.Size = New System.Drawing.Size(90, 20)
        Me.txtRuc.TabIndex = 5
        Me.txtRuc.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(176, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "RUC.:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(285, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Cliente:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(175, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Digite =>"
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(177, 17)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.ReadOnly = True
        Me.txtBuscar.Size = New System.Drawing.Size(108, 20)
        Me.txtBuscar.TabIndex = 3
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.CheckBox1)
        Me.Panel3.Controls.Add(Me.txtTotal)
        Me.Panel3.Controls.Add(Me.lblTotal)
        Me.Panel3.Controls.Add(Me.txtIGV)
        Me.Panel3.Controls.Add(Me.CheckBoxIGV)
        Me.Panel3.Controls.Add(Me.txtSub)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.GroupBox1)
        Me.Panel3.Controls.Add(Me.txtLetraTotal)
        Me.Panel3.Controls.Add(Me.txtDes2)
        Me.Panel3.Controls.Add(Me.txtTot2)
        Me.Panel3.Controls.Add(Me.txtPre2)
        Me.Panel3.Controls.Add(Me.txtDet2)
        Me.Panel3.Controls.Add(Me.txtCan2)
        Me.Panel3.Controls.Add(Me.txtDes1)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.txtTot1)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.txtPre1)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.txtDet1)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.txtCan1)
        Me.Panel3.Controls.Add(Me.cbMoneda)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.cbObra)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Location = New System.Drawing.Point(14, 218)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(891, 315)
        Me.Panel3.TabIndex = 2
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(763, 287)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(93, 20)
        Me.txtTotal.TabIndex = 338
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(687, 290)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(69, 13)
        Me.lblTotal.TabIndex = 337
        Me.lblTotal.Text = "TOTAL S/."
        '
        'txtIGV
        '
        Me.txtIGV.Location = New System.Drawing.Point(763, 266)
        Me.txtIGV.Name = "txtIGV"
        Me.txtIGV.ReadOnly = True
        Me.txtIGV.Size = New System.Drawing.Size(93, 20)
        Me.txtIGV.TabIndex = 335
        Me.txtIGV.TabStop = False
        Me.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxIGV
        '
        Me.CheckBoxIGV.AutoSize = True
        Me.CheckBoxIGV.Location = New System.Drawing.Point(685, 268)
        Me.CheckBoxIGV.Name = "CheckBoxIGV"
        Me.CheckBoxIGV.Size = New System.Drawing.Size(81, 17)
        Me.CheckBoxIGV.TabIndex = 336
        Me.CheckBoxIGV.TabStop = False
        Me.CheckBoxIGV.Text = "Sin I.G.V."
        Me.CheckBoxIGV.UseVisualStyleBackColor = True
        '
        'txtSub
        '
        Me.txtSub.Location = New System.Drawing.Point(763, 244)
        Me.txtSub.Name = "txtSub"
        Me.txtSub.ReadOnly = True
        Me.txtSub.Size = New System.Drawing.Size(93, 20)
        Me.txtSub.TabIndex = 334
        Me.txtSub.TabStop = False
        Me.txtSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(687, 247)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 13)
        Me.Label16.TabIndex = 333
        Me.Label16.Text = "SUB TOTAL"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb2)
        Me.GroupBox1.Controls.Add(Me.rb1)
        Me.GroupBox1.Location = New System.Drawing.Point(517, 254)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(160, 40)
        Me.GroupBox1.TabIndex = 332
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Calculo de I.G.V."
        '
        'rb2
        '
        Me.rb2.AutoSize = True
        Me.rb2.Location = New System.Drawing.Point(88, 17)
        Me.rb2.Name = "rb2"
        Me.rb2.Size = New System.Drawing.Size(61, 17)
        Me.rb2.TabIndex = 1
        Me.rb2.Text = "Tipo 2"
        Me.rb2.UseVisualStyleBackColor = True
        '
        'rb1
        '
        Me.rb1.AutoSize = True
        Me.rb1.Location = New System.Drawing.Point(15, 17)
        Me.rb1.Name = "rb1"
        Me.rb1.Size = New System.Drawing.Size(61, 17)
        Me.rb1.TabIndex = 0
        Me.rb1.Text = "Tipo 1"
        Me.rb1.UseVisualStyleBackColor = True
        '
        'txtLetraTotal
        '
        Me.txtLetraTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLetraTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLetraTotal.Location = New System.Drawing.Point(5, 268)
        Me.txtLetraTotal.Name = "txtLetraTotal"
        Me.txtLetraTotal.Size = New System.Drawing.Size(507, 20)
        Me.txtLetraTotal.TabIndex = 331
        Me.txtLetraTotal.TabStop = False
        Me.txtLetraTotal.Text = "SON:"
        '
        'txtDes2
        '
        Me.txtDes2.AcceptsReturn = True
        Me.txtDes2.AcceptsTab = True
        Me.txtDes2.Location = New System.Drawing.Point(98, 175)
        Me.txtDes2.Multiline = True
        Me.txtDes2.Name = "txtDes2"
        Me.txtDes2.ReadOnly = True
        Me.txtDes2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDes2.Size = New System.Drawing.Size(522, 61)
        Me.txtDes2.TabIndex = 12
        '
        'txtTot2
        '
        Me.txtTot2.Location = New System.Drawing.Point(768, 149)
        Me.txtTot2.Name = "txtTot2"
        Me.txtTot2.ReadOnly = True
        Me.txtTot2.Size = New System.Drawing.Size(83, 20)
        Me.txtTot2.TabIndex = 11
        Me.txtTot2.TabStop = False
        Me.txtTot2.Text = "0"
        Me.txtTot2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPre2
        '
        Me.txtPre2.Location = New System.Drawing.Point(664, 149)
        Me.txtPre2.Name = "txtPre2"
        Me.txtPre2.ReadOnly = True
        Me.txtPre2.Size = New System.Drawing.Size(83, 20)
        Me.txtPre2.TabIndex = 10
        Me.txtPre2.Text = "0"
        Me.txtPre2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDet2
        '
        Me.txtDet2.Location = New System.Drawing.Point(98, 149)
        Me.txtDet2.Name = "txtDet2"
        Me.txtDet2.ReadOnly = True
        Me.txtDet2.Size = New System.Drawing.Size(522, 20)
        Me.txtDet2.TabIndex = 9
        '
        'txtCan2
        '
        Me.txtCan2.Location = New System.Drawing.Point(31, 149)
        Me.txtCan2.Name = "txtCan2"
        Me.txtCan2.ReadOnly = True
        Me.txtCan2.Size = New System.Drawing.Size(52, 20)
        Me.txtCan2.TabIndex = 8
        Me.txtCan2.Text = "0"
        Me.txtCan2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDes1
        '
        Me.txtDes1.AcceptsReturn = True
        Me.txtDes1.AcceptsTab = True
        Me.txtDes1.Location = New System.Drawing.Point(98, 73)
        Me.txtDes1.Multiline = True
        Me.txtDes1.Name = "txtDes1"
        Me.txtDes1.ReadOnly = True
        Me.txtDes1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDes1.Size = New System.Drawing.Size(522, 57)
        Me.txtDes1.TabIndex = 7
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(765, 33)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 324
        Me.Label15.Text = "TOTAL"
        '
        'txtTot1
        '
        Me.txtTot1.Location = New System.Drawing.Point(768, 47)
        Me.txtTot1.Name = "txtTot1"
        Me.txtTot1.ReadOnly = True
        Me.txtTot1.Size = New System.Drawing.Size(83, 20)
        Me.txtTot1.TabIndex = 6
        Me.txtTot1.TabStop = False
        Me.txtTot1.Text = "0"
        Me.txtTot1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(661, 33)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 13)
        Me.Label14.TabIndex = 322
        Me.Label14.Text = "Prec_Unit.:"
        '
        'txtPre1
        '
        Me.txtPre1.Location = New System.Drawing.Point(664, 47)
        Me.txtPre1.Name = "txtPre1"
        Me.txtPre1.ReadOnly = True
        Me.txtPre1.Size = New System.Drawing.Size(83, 20)
        Me.txtPre1.TabIndex = 5
        Me.txtPre1.Text = "0"
        Me.txtPre1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(95, 33)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 13)
        Me.Label13.TabIndex = 320
        Me.Label13.Text = "Digite Detalle:"
        '
        'txtDet1
        '
        Me.txtDet1.Location = New System.Drawing.Point(98, 47)
        Me.txtDet1.Name = "txtDet1"
        Me.txtDet1.ReadOnly = True
        Me.txtDet1.Size = New System.Drawing.Size(522, 20)
        Me.txtDet1.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(30, 34)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 13)
        Me.Label12.TabIndex = 318
        Me.Label12.Text = "Cant.:"
        '
        'txtCan1
        '
        Me.txtCan1.Location = New System.Drawing.Point(31, 47)
        Me.txtCan1.Name = "txtCan1"
        Me.txtCan1.ReadOnly = True
        Me.txtCan1.Size = New System.Drawing.Size(52, 20)
        Me.txtCan1.TabIndex = 3
        Me.txtCan1.Text = "1"
        Me.txtCan1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbMoneda
        '
        Me.cbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMoneda.Enabled = False
        Me.cbMoneda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbMoneda.FormattingEnabled = True
        Me.cbMoneda.IntegralHeight = False
        Me.cbMoneda.Location = New System.Drawing.Point(718, 4)
        Me.cbMoneda.Name = "cbMoneda"
        Me.cbMoneda.Size = New System.Drawing.Size(132, 21)
        Me.cbMoneda.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(661, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 316
        Me.Label11.Text = "Moneda:"
        '
        'cbObra
        '
        Me.cbObra.DropDownHeight = 500
        Me.cbObra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbObra.DropDownWidth = 600
        Me.cbObra.Enabled = False
        Me.cbObra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbObra.FormattingEnabled = True
        Me.cbObra.IntegralHeight = False
        Me.cbObra.Location = New System.Drawing.Point(98, 4)
        Me.cbObra.Name = "cbObra"
        Me.cbObra.Size = New System.Drawing.Size(384, 21)
        Me.cbObra.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 296
        Me.Label9.Text = "Sede / Obra:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(491, 7)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(120, 17)
        Me.CheckBox1.TabIndex = 339
        Me.CheckBox1.Text = "No Imprimir Obra"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'registraDocVentaEmpForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(915, 558)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "registraDocVentaEmpForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GB3.ResumeLayout(False)
        Me.GB3.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbDoc As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents cbSerie As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnNuevo As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents cbCli As System.Windows.Forms.ComboBox
    Friend WithEvents txtRuc As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtDir As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cbObra As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnImprimir As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnAnula As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCierra As ComponentesSolucion2008.BottomSSP
    Friend WithEvents GB3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents date1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbMes As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCan1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDet1 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPre1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTot1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtDes1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDes2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTot2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtPre2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtDet2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCan2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtLetraTotal As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtIGV As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIGV As System.Windows.Forms.CheckBox
    Friend WithEvents txtSub As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtHist As System.Windows.Forms.TextBox
    Friend WithEvents txtDato As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox

End Class
