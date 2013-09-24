<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class procesarEgresoCajaChicaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(procesarEgresoCajaChicaForm))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFec = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSal = New System.Windows.Forms.TextBox
        Me.lblMon1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNota = New System.Windows.Forms.TextBox
        Me.lblMon2 = New System.Windows.Forms.Label
        Me.btnPro = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnVis = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Navigator2 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.btnImp = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDes = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.dgTabla2 = New System.Windows.Forms.DataGridView
        Me.txtMon = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbCaja = New System.Windows.Forms.ComboBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtObra = New System.Windows.Forms.TextBox
        Me.txtSaldo = New System.Windows.Forms.TextBox
        Me.txtSalio = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtSol = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtTotOtr = New System.Windows.Forms.TextBox
        Me.txtTotHon = New System.Windows.Forms.TextBox
        Me.txtTotBol = New System.Windows.Forms.TextBox
        Me.txtTotFac = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtEst = New System.Windows.Forms.TextBox
        Me.lbSol = New System.Windows.Forms.ListBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtImpre = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtTotReq = New System.Windows.Forms.TextBox
        Me.txtSalAnt = New System.Windows.Forms.TextBox
        Me.txtTotIns = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblMon4 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblMon3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.date1 = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator2.SuspendLayout()
        CType(Me.dgTabla2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        Me.lblTitulo.Text = "Procesar Egreso de Dinero de Caja Chica"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Sesión Fecha Caja:"
        '
        'txtFec
        '
        Me.txtFec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFec.Location = New System.Drawing.Point(138, 155)
        Me.txtFec.Name = "txtFec"
        Me.txtFec.ReadOnly = True
        Me.txtFec.Size = New System.Drawing.Size(76, 20)
        Me.txtFec.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(215, 185)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Saldo Caja Chica"
        '
        'txtSal
        '
        Me.txtSal.BackColor = System.Drawing.Color.Maroon
        Me.txtSal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSal.ForeColor = System.Drawing.Color.White
        Me.txtSal.Location = New System.Drawing.Point(336, 182)
        Me.txtSal.Name = "txtSal"
        Me.txtSal.ReadOnly = True
        Me.txtSal.Size = New System.Drawing.Size(75, 22)
        Me.txtSal.TabIndex = 16
        '
        'lblMon1
        '
        Me.lblMon1.AutoSize = True
        Me.lblMon1.Location = New System.Drawing.Point(314, 185)
        Me.lblMon1.Name = "lblMon1"
        Me.lblMon1.Size = New System.Drawing.Size(14, 13)
        Me.lblMon1.TabIndex = 17
        Me.lblMon1.Text = "$"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(416, 186)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Monto a Egresar"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(297, 159)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Nota:"
        '
        'txtNota
        '
        Me.txtNota.Location = New System.Drawing.Point(333, 156)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(458, 20)
        Me.txtNota.TabIndex = 21
        '
        'lblMon2
        '
        Me.lblMon2.AutoSize = True
        Me.lblMon2.Location = New System.Drawing.Point(511, 186)
        Me.lblMon2.Name = "lblMon2"
        Me.lblMon2.Size = New System.Drawing.Size(14, 13)
        Me.lblMon2.TabIndex = 22
        Me.lblMon2.Text = "$"
        '
        'btnPro
        '
        Me.btnPro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPro.Image = CType(resources.GetObject("btnPro.Image"), System.Drawing.Image)
        Me.btnPro.Location = New System.Drawing.Point(630, 178)
        Me.btnPro.Name = "btnPro"
        Me.btnPro.Size = New System.Drawing.Size(29, 28)
        Me.btnPro.TabIndex = 23
        Me.btnPro.TabStop = False
        Me.btnPro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnPro, "Procesar Ingreso de Dinero a Caja Chica...")
        Me.btnPro.UseVisualStyleBackColor = True
        '
        'btnVis
        '
        Me.btnVis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVis.Image = CType(resources.GetObject("btnVis.Image"), System.Drawing.Image)
        Me.btnVis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVis.Location = New System.Drawing.Point(669, 180)
        Me.btnVis.Name = "btnVis"
        Me.btnVis.Size = New System.Drawing.Size(122, 23)
        Me.btnVis.TabIndex = 334
        Me.btnVis.TabStop = False
        Me.btnVis.Text = "Ver Movimientos"
        Me.btnVis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnVis, "Visualizar movimientos de Ingresos y Egresos de Caja Chica...")
        Me.btnVis.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Navigator2)
        Me.Panel2.Controls.Add(Me.dgTabla2)
        Me.Panel2.Location = New System.Drawing.Point(14, 210)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(901, 443)
        Me.Panel2.TabIndex = 335
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(210, 13)
        Me.Label6.TabIndex = 331
        Me.Label6.Text = "Movimientos de Ingresos y Egresos:"
        '
        'Navigator2
        '
        Me.Navigator2.AddNewItem = Nothing
        Me.Navigator2.CountItem = Me.ToolStripLabel4
        Me.Navigator2.DeleteItem = Nothing
        Me.Navigator2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripSeparator6, Me.ToolStripTextBox2, Me.ToolStripLabel4, Me.ToolStripSeparator7, Me.ToolStripButton7, Me.ToolStripButton8, Me.ToolStripSeparator8, Me.ToolStripSeparator11, Me.btnImp, Me.ToolStripSeparator10, Me.btnDes, Me.ToolStripSeparator9})
        Me.Navigator2.Location = New System.Drawing.Point(0, 416)
        Me.Navigator2.MoveFirstItem = Me.ToolStripButton5
        Me.Navigator2.MoveLastItem = Me.ToolStripButton8
        Me.Navigator2.MoveNextItem = Me.ToolStripButton7
        Me.Navigator2.MovePreviousItem = Me.ToolStripButton6
        Me.Navigator2.Name = "Navigator2"
        Me.Navigator2.PositionItem = Me.ToolStripTextBox2
        Me.Navigator2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator2.Size = New System.Drawing.Size(899, 25)
        Me.Navigator2.TabIndex = 329
        Me.Navigator2.Text = "BindingNavigator1"
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel4.Text = "de {0}"
        Me.ToolStripLabel4.ToolTipText = "Número total de elementos"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton5.Text = "Mover primero"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton6.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton6.Text = "Mover anterior"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.AccessibleName = "Posición"
        Me.ToolStripTextBox2.AutoSize = False
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.ReadOnly = True
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(50, 23)
        Me.ToolStripTextBox2.Text = "0"
        Me.ToolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolStripTextBox2.ToolTipText = "Posición actual"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton7.Text = "Mover siguiente"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton8.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton8.Text = "Mover último"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 25)
        '
        'btnImp
        '
        Me.btnImp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnImp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnImp.Image = CType(resources.GetObject("btnImp.Image"), System.Drawing.Image)
        Me.btnImp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImp.Name = "btnImp"
        Me.btnImp.Size = New System.Drawing.Size(76, 22)
        Me.btnImp.Text = "Imprimir"
        Me.btnImp.ToolTipText = "Imprimir Kardex..."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'btnDes
        '
        Me.btnDes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnDes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDes.ForeColor = System.Drawing.Color.Blue
        Me.btnDes.Image = CType(resources.GetObject("btnDes.Image"), System.Drawing.Image)
        Me.btnDes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDes.Name = "btnDes"
        Me.btnDes.Size = New System.Drawing.Size(88, 22)
        Me.btnDes.Text = "Deshacer..."
        Me.btnDes.ToolTipText = "Deshacer Ultimo Movimiento..."
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'dgTabla2
        '
        Me.dgTabla2.AllowUserToAddRows = False
        Me.dgTabla2.AllowUserToDeleteRows = False
        Me.dgTabla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla2.Location = New System.Drawing.Point(0, 18)
        Me.dgTabla2.Name = "dgTabla2"
        Me.dgTabla2.ReadOnly = True
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla2.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgTabla2.Size = New System.Drawing.Size(899, 398)
        Me.dgTabla2.TabIndex = 330
        '
        'txtMon
        '
        Me.txtMon.Location = New System.Drawing.Point(532, 182)
        Me.txtMon.Name = "txtMon"
        Me.txtMon.Size = New System.Drawing.Size(71, 20)
        Me.txtMon.TabIndex = 336
        Me.txtMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 183)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 337
        Me.Label7.Text = "Egresar de:"
        '
        'cbCaja
        '
        Me.cbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCaja.FormattingEnabled = True
        Me.cbCaja.Location = New System.Drawing.Point(87, 181)
        Me.cbCaja.Name = "cbCaja"
        Me.cbCaja.Size = New System.Drawing.Size(121, 21)
        Me.cbCaja.TabIndex = 338
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtObra)
        Me.Panel1.Controls.Add(Me.txtSaldo)
        Me.Panel1.Controls.Add(Me.txtSalio)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.txtSol)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.txtTotOtr)
        Me.Panel1.Controls.Add(Me.txtTotHon)
        Me.Panel1.Controls.Add(Me.txtTotBol)
        Me.Panel1.Controls.Add(Me.txtTotFac)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtEst)
        Me.Panel1.Controls.Add(Me.lbSol)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtImpre)
        Me.Panel1.Controls.Add(Me.txtTotReq)
        Me.Panel1.Controls.Add(Me.txtSalAnt)
        Me.Panel1.Controls.Add(Me.txtTotIns)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.lblMon4)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lblMon3)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.date1)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 120)
        Me.Panel1.TabIndex = 339
        '
        'txtObra
        '
        Me.txtObra.Location = New System.Drawing.Point(455, 27)
        Me.txtObra.Name = "txtObra"
        Me.txtObra.ReadOnly = True
        Me.txtObra.Size = New System.Drawing.Size(438, 20)
        Me.txtObra.TabIndex = 356
        '
        'txtSaldo
        '
        Me.txtSaldo.Location = New System.Drawing.Point(380, 94)
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.Size = New System.Drawing.Size(66, 20)
        Me.txtSaldo.TabIndex = 355
        Me.txtSaldo.TabStop = False
        Me.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSalio
        '
        Me.txtSalio.Location = New System.Drawing.Point(299, 94)
        Me.txtSalio.Name = "txtSalio"
        Me.txtSalio.ReadOnly = True
        Me.txtSalio.Size = New System.Drawing.Size(66, 20)
        Me.txtSalio.TabIndex = 354
        Me.txtSalio.TabStop = False
        Me.txtSalio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(377, 80)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(79, 13)
        Me.Label24.TabIndex = 353
        Me.Label24.Text = "Saldo a Salir"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(296, 80)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 13)
        Me.Label23.TabIndex = 352
        Me.Label23.Text = "SalioDeCaja"
        '
        'txtSol
        '
        Me.txtSol.Location = New System.Drawing.Point(455, 3)
        Me.txtSol.Name = "txtSol"
        Me.txtSol.ReadOnly = True
        Me.txtSol.Size = New System.Drawing.Size(244, 20)
        Me.txtSol.TabIndex = 351
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(386, 6)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(71, 13)
        Me.Label21.TabIndex = 350
        Me.Label21.Text = "Solicitante:"
        '
        'txtTotOtr
        '
        Me.txtTotOtr.Location = New System.Drawing.Point(774, 96)
        Me.txtTotOtr.Name = "txtTotOtr"
        Me.txtTotOtr.ReadOnly = True
        Me.txtTotOtr.Size = New System.Drawing.Size(66, 20)
        Me.txtTotOtr.TabIndex = 349
        Me.txtTotOtr.TabStop = False
        Me.txtTotOtr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotHon
        '
        Me.txtTotHon.Location = New System.Drawing.Point(774, 75)
        Me.txtTotHon.Name = "txtTotHon"
        Me.txtTotHon.ReadOnly = True
        Me.txtTotHon.Size = New System.Drawing.Size(66, 20)
        Me.txtTotHon.TabIndex = 348
        Me.txtTotHon.TabStop = False
        Me.txtTotHon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotBol
        '
        Me.txtTotBol.Location = New System.Drawing.Point(595, 96)
        Me.txtTotBol.Name = "txtTotBol"
        Me.txtTotBol.ReadOnly = True
        Me.txtTotBol.Size = New System.Drawing.Size(66, 20)
        Me.txtTotBol.TabIndex = 347
        Me.txtTotBol.TabStop = False
        Me.txtTotBol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotFac
        '
        Me.txtTotFac.Location = New System.Drawing.Point(594, 75)
        Me.txtTotFac.Name = "txtTotFac"
        Me.txtTotFac.ReadOnly = True
        Me.txtTotFac.Size = New System.Drawing.Size(66, 20)
        Me.txtTotFac.TabIndex = 346
        Me.txtTotFac.TabStop = False
        Me.txtTotFac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(665, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(112, 13)
        Me.Label20.TabIndex = 345
        Me.Label20.Text = "Total Otros      S/."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(665, 78)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(111, 13)
        Me.Label19.TabIndex = 344
        Me.Label19.Text = "Total Honorar. S/."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(486, 99)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(110, 13)
        Me.Label18.TabIndex = 343
        Me.Label18.Text = "Total Boleta    S/."
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(485, 79)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 13)
        Me.Label17.TabIndex = 342
        Me.Label17.Text = "Total Factura  S/."
        '
        'txtEst
        '
        Me.txtEst.Location = New System.Drawing.Point(146, 3)
        Me.txtEst.Name = "txtEst"
        Me.txtEst.Size = New System.Drawing.Size(86, 20)
        Me.txtEst.TabIndex = 4
        Me.txtEst.TabStop = False
        '
        'lbSol
        '
        Me.lbSol.FormattingEnabled = True
        Me.lbSol.Location = New System.Drawing.Point(39, 2)
        Me.lbSol.Name = "lbSol"
        Me.lbSol.Size = New System.Drawing.Size(57, 108)
        Me.lbSol.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(99, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 341
        Me.Label8.Text = "Estado:"
        '
        'txtImpre
        '
        Me.txtImpre.Location = New System.Drawing.Point(217, 49)
        Me.txtImpre.Name = "txtImpre"
        Me.txtImpre.ReadOnly = True
        Me.txtImpre.Size = New System.Drawing.Size(66, 20)
        Me.txtImpre.TabIndex = 6
        Me.txtImpre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotReq
        '
        Me.txtTotReq.BackColor = System.Drawing.Color.Navy
        Me.txtTotReq.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotReq.ForeColor = System.Drawing.Color.White
        Me.txtTotReq.Location = New System.Drawing.Point(213, 93)
        Me.txtTotReq.Name = "txtTotReq"
        Me.txtTotReq.ReadOnly = True
        Me.txtTotReq.Size = New System.Drawing.Size(76, 22)
        Me.txtTotReq.TabIndex = 8
        Me.txtTotReq.TabStop = False
        Me.txtTotReq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSalAnt
        '
        Me.txtSalAnt.Location = New System.Drawing.Point(217, 71)
        Me.txtSalAnt.Name = "txtSalAnt"
        Me.txtSalAnt.ReadOnly = True
        Me.txtSalAnt.Size = New System.Drawing.Size(66, 20)
        Me.txtSalAnt.TabIndex = 7
        Me.txtSalAnt.TabStop = False
        Me.txtSalAnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotIns
        '
        Me.txtTotIns.Location = New System.Drawing.Point(217, 27)
        Me.txtTotIns.Name = "txtTotIns"
        Me.txtTotIns.ReadOnly = True
        Me.txtTotIns.Size = New System.Drawing.Size(66, 20)
        Me.txtTotIns.TabIndex = 5
        Me.txtTotIns.TabStop = False
        Me.txtTotIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(99, 93)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 15)
        Me.Label9.TabIndex = 295
        Me.Label9.Text = "Total Requer."
        '
        'lblMon4
        '
        Me.lblMon4.AutoSize = True
        Me.lblMon4.Location = New System.Drawing.Point(191, 97)
        Me.lblMon4.Name = "lblMon4"
        Me.lblMon4.Size = New System.Drawing.Size(25, 13)
        Me.lblMon4.TabIndex = 296
        Me.lblMon4.Text = "S/."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(101, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 293
        Me.Label10.Text = "Saldo Anterior"
        '
        'lblMon3
        '
        Me.lblMon3.AutoSize = True
        Me.lblMon3.Location = New System.Drawing.Point(191, 74)
        Me.lblMon3.Name = "lblMon3"
        Me.lblMon3.Size = New System.Drawing.Size(25, 13)
        Me.lblMon3.TabIndex = 294
        Me.lblMon3.Text = "S/."
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(102, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 13)
        Me.Label11.TabIndex = 291
        Me.Label11.Text = "Imprevistos"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(191, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(25, 13)
        Me.Label12.TabIndex = 292
        Me.Label12.Text = "S/."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(101, 30)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 13)
        Me.Label13.TabIndex = 289
        Me.Label13.Text = "Total Insumos"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(191, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(25, 13)
        Me.Label14.TabIndex = 290
        Me.Label14.Text = "S/."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(345, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 13)
        Me.Label15.TabIndex = 288
        Me.Label15.Text = "Gastos para Obra:"
        '
        'date1
        '
        Me.date1.Enabled = False
        Me.date1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.date1.Location = New System.Drawing.Point(274, 3)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(107, 20)
        Me.date1.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(233, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(46, 13)
        Me.Label16.TabIndex = 280
        Me.Label16.Text = "Fecha:"
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(749, 27)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 278
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(-2, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(43, 13)
        Me.Label22.TabIndex = 276
        Me.Label22.Text = "NºSol:"
        '
        'procesarEgresoCajaChicaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.cbCaja)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtMon)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnVis)
        Me.Controls.Add(Me.btnPro)
        Me.Controls.Add(Me.txtNota)
        Me.Controls.Add(Me.lblMon2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSal)
        Me.Controls.Add(Me.lblMon1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtFec)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "procesarEgresoCajaChicaForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtFec, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.lblMon1, 0)
        Me.Controls.SetChildIndex(Me.txtSal, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.lblMon2, 0)
        Me.Controls.SetChildIndex(Me.txtNota, 0)
        Me.Controls.SetChildIndex(Me.btnPro, 0)
        Me.Controls.SetChildIndex(Me.btnVis, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.txtMon, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.cbCaja, 0)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Navigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator2.ResumeLayout(False)
        Me.Navigator2.PerformLayout()
        CType(Me.dgTabla2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFec As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSal As System.Windows.Forms.TextBox
    Friend WithEvents lblMon1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNota As System.Windows.Forms.TextBox
    Friend WithEvents lblMon2 As System.Windows.Forms.Label
    Friend WithEvents btnPro As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnVis As ComponentesSolucion2008.BottomSSP
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Navigator2 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel4 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox2 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnImp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDes As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgTabla2 As System.Windows.Forms.DataGridView
    Friend WithEvents txtMon As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbCaja As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtTotOtr As System.Windows.Forms.TextBox
    Friend WithEvents txtTotHon As System.Windows.Forms.TextBox
    Friend WithEvents txtTotBol As System.Windows.Forms.TextBox
    Friend WithEvents txtTotFac As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtEst As System.Windows.Forms.TextBox
    Friend WithEvents lbSol As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtImpre As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtTotReq As System.Windows.Forms.TextBox
    Friend WithEvents txtSalAnt As System.Windows.Forms.TextBox
    Friend WithEvents txtTotIns As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblMon4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblMon3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents date1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtSol As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtSalio As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents txtObra As System.Windows.Forms.TextBox

End Class
