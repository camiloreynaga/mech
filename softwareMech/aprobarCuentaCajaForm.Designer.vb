<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class aprobarCuentaCajaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(aprobarCuentaCajaForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtSalAct = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtTotEgr = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtObra = New System.Windows.Forms.TextBox
        Me.txtSol = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbSol = New System.Windows.Forms.ListBox
        Me.txtImpre = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtTotReq = New System.Windows.Forms.TextBox
        Me.txtSalAnt = New System.Windows.Forms.TextBox
        Me.txtTotIns = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.date1 = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.btnApro = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cbTip1 = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cbVis = New System.Windows.Forms.CheckBox
        Me.Navigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.TSModificar = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripLabel7 = New System.Windows.Forms.ToolStripLabel
        Me.txtTotal1 = New System.Windows.Forms.ToolStripTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtFac = New System.Windows.Forms.TextBox
        Me.txtBol = New System.Windows.Forms.TextBox
        Me.txtHon = New System.Windows.Forms.TextBox
        Me.txtOtro = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(1060, 23)
        Me.lblTitulo.Text = "Comprobación de Rendición de Cuentas de Caja Chica"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 628)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtSalAct)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtTotEgr)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtObra)
        Me.Panel1.Controls.Add(Me.txtSol)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lbSol)
        Me.Panel1.Controls.Add(Me.txtImpre)
        Me.Panel1.Controls.Add(Me.txtTotReq)
        Me.Panel1.Controls.Add(Me.txtSalAnt)
        Me.Panel1.Controls.Add(Me.txtTotIns)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.date1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(13, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1046, 77)
        Me.Panel1.TabIndex = 3
        '
        'txtSalAct
        '
        Me.txtSalAct.Location = New System.Drawing.Point(907, 50)
        Me.txtSalAct.Name = "txtSalAct"
        Me.txtSalAct.ReadOnly = True
        Me.txtSalAct.Size = New System.Drawing.Size(74, 20)
        Me.txtSalAct.TabIndex = 359
        Me.txtSalAct.TabStop = False
        Me.txtSalAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(902, 37)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 13)
        Me.Label10.TabIndex = 360
        Me.Label10.Text = "Saldo Actual"
        '
        'txtTotEgr
        '
        Me.txtTotEgr.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTotEgr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotEgr.ForeColor = System.Drawing.Color.White
        Me.txtTotEgr.Location = New System.Drawing.Point(807, 49)
        Me.txtTotEgr.Name = "txtTotEgr"
        Me.txtTotEgr.ReadOnly = True
        Me.txtTotEgr.Size = New System.Drawing.Size(88, 22)
        Me.txtTotEgr.TabIndex = 357
        Me.txtTotEgr.TabStop = False
        Me.txtTotEgr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(804, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 15)
        Me.Label9.TabIndex = 358
        Me.Label9.Text = "Total Egreso"
        '
        'txtObra
        '
        Me.txtObra.Location = New System.Drawing.Point(474, 14)
        Me.txtObra.Name = "txtObra"
        Me.txtObra.ReadOnly = True
        Me.txtObra.Size = New System.Drawing.Size(559, 20)
        Me.txtObra.TabIndex = 356
        '
        'txtSol
        '
        Me.txtSol.Location = New System.Drawing.Point(221, 14)
        Me.txtSol.Name = "txtSol"
        Me.txtSol.ReadOnly = True
        Me.txtSol.Size = New System.Drawing.Size(244, 20)
        Me.txtSol.TabIndex = 351
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(218, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 350
        Me.Label3.Text = "Solicitante:"
        '
        'lbSol
        '
        Me.lbSol.FormattingEnabled = True
        Me.lbSol.Location = New System.Drawing.Point(39, 2)
        Me.lbSol.Name = "lbSol"
        Me.lbSol.Size = New System.Drawing.Size(57, 69)
        Me.lbSol.TabIndex = 1
        '
        'txtImpre
        '
        Me.txtImpre.Location = New System.Drawing.Point(306, 50)
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
        Me.txtTotReq.Location = New System.Drawing.Point(474, 49)
        Me.txtTotReq.Name = "txtTotReq"
        Me.txtTotReq.ReadOnly = True
        Me.txtTotReq.Size = New System.Drawing.Size(88, 22)
        Me.txtTotReq.TabIndex = 8
        Me.txtTotReq.TabStop = False
        Me.txtTotReq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSalAnt
        '
        Me.txtSalAnt.Location = New System.Drawing.Point(390, 50)
        Me.txtSalAnt.Name = "txtSalAnt"
        Me.txtSalAnt.ReadOnly = True
        Me.txtSalAnt.Size = New System.Drawing.Size(66, 20)
        Me.txtSalAnt.TabIndex = 7
        Me.txtSalAnt.TabStop = False
        Me.txtSalAnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotIns
        '
        Me.txtTotIns.Location = New System.Drawing.Point(223, 50)
        Me.txtTotIns.Name = "txtTotIns"
        Me.txtTotIns.ReadOnly = True
        Me.txtTotIns.Size = New System.Drawing.Size(66, 20)
        Me.txtTotIns.TabIndex = 5
        Me.txtTotIns.TabStop = False
        Me.txtTotIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(471, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 15)
        Me.Label8.TabIndex = 295
        Me.Label8.Text = "Total Requer."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(385, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 293
        Me.Label7.Text = "Saldo Anter."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(303, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 291
        Me.Label6.Text = "Imprevistos"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(218, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 289
        Me.Label5.Text = "Total Insum."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(484, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 13)
        Me.Label4.TabIndex = 288
        Me.Label4.Text = "Gastos para Obra:"
        '
        'date1
        '
        Me.date1.Enabled = False
        Me.date1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.date1.Location = New System.Drawing.Point(104, 14)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(107, 20)
        Me.date1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(102, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 280
        Me.Label2.Text = "Fecha:"
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(749, 13)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 278
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-2, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 276
        Me.Label1.Text = "NºSol:"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgTabla1)
        Me.Panel3.Controls.Add(Me.btnApro)
        Me.Panel3.Controls.Add(Me.GroupBox2)
        Me.Panel3.Controls.Add(Me.GroupBox1)
        Me.Panel3.Controls.Add(Me.cbTip1)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.cbVis)
        Me.Panel3.Controls.Add(Me.Navigator1)
        Me.Panel3.Location = New System.Drawing.Point(13, 101)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1046, 522)
        Me.Panel3.TabIndex = 4
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(1, 57)
        Me.dgTabla1.Name = "dgTabla1"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTabla1.Size = New System.Drawing.Size(1044, 440)
        Me.dgTabla1.TabIndex = 15
        '
        'btnApro
        '
        Me.btnApro.BackColor = System.Drawing.SystemColors.Control
        Me.btnApro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApro.Image = CType(resources.GetObject("btnApro.Image"), System.Drawing.Image)
        Me.btnApro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApro.Location = New System.Drawing.Point(592, 28)
        Me.btnApro.Name = "btnApro"
        Me.btnApro.Size = New System.Drawing.Size(162, 23)
        Me.btnApro.TabIndex = 326
        Me.btnApro.Text = "Aprobar Todos y Cerrar"
        Me.btnApro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnApro.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(791, 37)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(166, 26)
        Me.GroupBox2.TabIndex = 325
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Monto Real"
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(401, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(154, 26)
        Me.GroupBox1.TabIndex = 320
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Montos Proyectados"
        '
        'cbTip1
        '
        Me.cbTip1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTip1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbTip1.FormattingEnabled = True
        Me.cbTip1.Location = New System.Drawing.Point(7, 15)
        Me.cbTip1.Name = "cbTip1"
        Me.cbTip1.Size = New System.Drawing.Size(159, 21)
        Me.cbTip1.TabIndex = 320
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 13)
        Me.Label11.TabIndex = 321
        Me.Label11.Text = "Seleccione Tipo Material:"
        '
        'cbVis
        '
        Me.cbVis.AutoSize = True
        Me.cbVis.Location = New System.Drawing.Point(8, 40)
        Me.cbVis.Name = "cbVis"
        Me.cbVis.Size = New System.Drawing.Size(305, 17)
        Me.cbVis.TabIndex = 302
        Me.cbVis.TabStop = False
        Me.cbVis.Text = "Visualizar Insumos de todos los Tipos de Material"
        Me.cbVis.UseVisualStyleBackColor = True
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.BackColor = System.Drawing.SystemColors.Control
        Me.Navigator1.CountItem = Me.ToolStripLabel1
        Me.Navigator1.CountItemFormat = "de {0} "
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator3, Me.TSModificar, Me.ToolStripLabel2, Me.txtTotal, Me.ToolStripTextBox2, Me.ToolStripLabel7, Me.txtTotal1})
        Me.Navigator1.Location = New System.Drawing.Point(0, 497)
        Me.Navigator1.MoveFirstItem = Me.ToolStripButton1
        Me.Navigator1.MoveLastItem = Me.ToolStripButton4
        Me.Navigator1.MoveNextItem = Me.ToolStripButton3
        Me.Navigator1.MovePreviousItem = Me.ToolStripButton2
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.ToolStripTextBox1
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(1046, 25)
        Me.Navigator1.TabIndex = 301
        Me.Navigator1.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(40, 22)
        Me.ToolStripLabel1.Text = "de {0} "
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
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(35, 23)
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
        Me.TSModificar.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TSModificar.Enabled = False
        Me.TSModificar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSModificar.ForeColor = System.Drawing.Color.Navy
        Me.TSModificar.Name = "TSModificar"
        Me.TSModificar.Size = New System.Drawing.Size(220, 25)
        Me.TSModificar.ToolTipText = "Guardar modificaciones de grilla..."
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(74, 22)
        Me.ToolStripLabel2.Text = "Total Parcial"
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(76, 25)
        Me.txtTotal.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ToolStripTextBox2.Enabled = False
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(240, 25)
        '
        'ToolStripLabel7
        '
        Me.ToolStripLabel7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel7.Name = "ToolStripLabel7"
        Me.ToolStripLabel7.Size = New System.Drawing.Size(85, 22)
        Me.ToolStripLabel7.Text = "Total Egreso"
        '
        'txtTotal1
        '
        Me.txtTotal1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal1.Name = "txtTotal1"
        Me.txtTotal1.Size = New System.Drawing.Size(80, 25)
        Me.txtTotal1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(440, 632)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 13)
        Me.Label12.TabIndex = 361
        Me.Label12.Text = "Total Fact.:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(591, 632)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 362
        Me.Label13.Text = "Total Bol.:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(733, 632)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 363
        Me.Label14.Text = "Total Honor.:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(894, 632)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 13)
        Me.Label15.TabIndex = 364
        Me.Label15.Text = "Total Otro:"
        '
        'txtFac
        '
        Me.txtFac.Location = New System.Drawing.Point(511, 629)
        Me.txtFac.Name = "txtFac"
        Me.txtFac.ReadOnly = True
        Me.txtFac.Size = New System.Drawing.Size(74, 20)
        Me.txtFac.TabIndex = 365
        Me.txtFac.TabStop = False
        Me.txtFac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBol
        '
        Me.txtBol.Location = New System.Drawing.Point(653, 629)
        Me.txtBol.Name = "txtBol"
        Me.txtBol.ReadOnly = True
        Me.txtBol.Size = New System.Drawing.Size(74, 20)
        Me.txtBol.TabIndex = 366
        Me.txtBol.TabStop = False
        Me.txtBol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHon
        '
        Me.txtHon.Location = New System.Drawing.Point(814, 629)
        Me.txtHon.Name = "txtHon"
        Me.txtHon.ReadOnly = True
        Me.txtHon.Size = New System.Drawing.Size(74, 20)
        Me.txtHon.TabIndex = 367
        Me.txtHon.TabStop = False
        Me.txtHon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOtro
        '
        Me.txtOtro.Location = New System.Drawing.Point(958, 629)
        Me.txtOtro.Name = "txtOtro"
        Me.txtOtro.ReadOnly = True
        Me.txtOtro.Size = New System.Drawing.Size(74, 20)
        Me.txtOtro.TabIndex = 368
        Me.txtOtro.TabStop = False
        Me.txtOtro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'aprobarCuentaCajaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(1060, 673)
        Me.Controls.Add(Me.txtOtro)
        Me.Controls.Add(Me.txtHon)
        Me.Controls.Add(Me.txtBol)
        Me.Controls.Add(Me.txtFac)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "aprobarCuentaCajaForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.txtFac, 0)
        Me.Controls.SetChildIndex(Me.txtBol, 0)
        Me.Controls.SetChildIndex(Me.txtHon, 0)
        Me.Controls.SetChildIndex(Me.txtOtro, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtSalAct As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTotEgr As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtObra As System.Windows.Forms.TextBox
    Friend WithEvents txtSol As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbSol As System.Windows.Forms.ListBox
    Friend WithEvents txtImpre As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtTotReq As System.Windows.Forms.TextBox
    Friend WithEvents txtSalAnt As System.Windows.Forms.TextBox
    Friend WithEvents txtTotIns As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents date1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Navigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtTotal As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel7 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtTotal1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents cbVis As System.Windows.Forms.CheckBox
    Friend WithEvents cbTip1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFac As System.Windows.Forms.TextBox
    Friend WithEvents txtBol As System.Windows.Forms.TextBox
    Friend WithEvents txtHon As System.Windows.Forms.TextBox
    Friend WithEvents txtOtro As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripTextBox2 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents TSModificar As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnApro As ComponentesSolucion2008.BottomSSP

End Class
