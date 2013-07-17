<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeguimientoOrdenDesembolsoForm
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.gbDesembolso = New System.Windows.Forms.GroupBox
        Me.dgDesembolso = New System.Windows.Forms.DataGridView
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.chkReciboEgreso = New System.Windows.Forms.CheckBox
        Me.chkOtros = New System.Windows.Forms.CheckBox
        Me.chkGuiaRemision = New System.Windows.Forms.CheckBox
        Me.chkVoucherDetraccion = New System.Windows.Forms.CheckBox
        Me.chkBoleta = New System.Windows.Forms.CheckBox
        Me.chkVoucher = New System.Windows.Forms.CheckBox
        Me.chkFactura = New System.Windows.Forms.CheckBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtOtros = New System.Windows.Forms.TextBox
        Me.txtMotivoDesem = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.dgPagos = New System.Windows.Forms.DataGridView
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtFormaPago = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtDescripcionPago = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtClasifiPago = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtEstadoPago = New System.Windows.Forms.TextBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.dgContabilidad = New System.Windows.Forms.DataGridView
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtPeriodoTrib = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtEstadoConta = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDetraccion = New System.Windows.Forms.TextBox
        Me.txtEstadoDesem = New System.Windows.Forms.TextBox
        Me.txtMonto = New System.Windows.Forms.TextBox
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSolicitante = New System.Windows.Forms.TextBox
        Me.txtFechaDesem = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtEstadoTesoreria = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtEstadoGerencia = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtEstadoContab = New System.Windows.Forms.TextBox
        Me.txtNombreGrte = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtOrdCompra = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtRuc = New System.Windows.Forms.TextBox
        Me.txtTelefonoProv = New System.Windows.Forms.TextBox
        Me.txtCuentaBco = New System.Windows.Forms.TextBox
        Me.txtCuentaDetraccion = New System.Windows.Forms.TextBox
        Me.txtEmailProv = New System.Windows.Forms.TextBox
        Me.txtObra = New System.Windows.Forms.TextBox
        Me.txtProveedor = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.gbDesembolso.SuspendLayout()
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.dgPagos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.dgContabilidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.gbDesembolso.Controls.Add(Me.dgDesembolso)
        Me.gbDesembolso.Location = New System.Drawing.Point(20, 47)
        Me.gbDesembolso.Name = "gbDesembolso"
        Me.gbDesembolso.Size = New System.Drawing.Size(883, 205)
        Me.gbDesembolso.TabIndex = 3
        Me.gbDesembolso.TabStop = False
        '
        'dgDesembolso
        '
        Me.dgDesembolso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgDesembolso.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgDesembolso.Location = New System.Drawing.Point(3, 16)
        Me.dgDesembolso.Name = "dgDesembolso"
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDesembolso.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgDesembolso.Size = New System.Drawing.Size(877, 186)
        Me.dgDesembolso.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 93)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(883, 304)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(875, 278)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Solicitud"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox5.Controls.Add(Me.chkReciboEgreso)
        Me.GroupBox5.Controls.Add(Me.chkOtros)
        Me.GroupBox5.Controls.Add(Me.chkGuiaRemision)
        Me.GroupBox5.Controls.Add(Me.chkVoucherDetraccion)
        Me.GroupBox5.Controls.Add(Me.chkBoleta)
        Me.GroupBox5.Controls.Add(Me.chkVoucher)
        Me.GroupBox5.Controls.Add(Me.chkFactura)
        Me.GroupBox5.Controls.Add(Me.Label31)
        Me.GroupBox5.Controls.Add(Me.txtOtros)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 184)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(854, 91)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        '
        'chkReciboEgreso
        '
        Me.chkReciboEgreso.AutoSize = True
        Me.chkReciboEgreso.Location = New System.Drawing.Point(499, 35)
        Me.chkReciboEgreso.Name = "chkReciboEgreso"
        Me.chkReciboEgreso.Size = New System.Drawing.Size(133, 17)
        Me.chkReciboEgreso.TabIndex = 28
        Me.chkReciboEgreso.Text = "Recibo de Egresos"
        Me.chkReciboEgreso.UseVisualStyleBackColor = True
        '
        'chkOtros
        '
        Me.chkOtros.AutoSize = True
        Me.chkOtros.Location = New System.Drawing.Point(332, 61)
        Me.chkOtros.Name = "chkOtros"
        Me.chkOtros.Size = New System.Drawing.Size(56, 17)
        Me.chkOtros.TabIndex = 30
        Me.chkOtros.Text = "Otros"
        Me.chkOtros.UseVisualStyleBackColor = True
        '
        'chkGuiaRemision
        '
        Me.chkGuiaRemision.AutoSize = True
        Me.chkGuiaRemision.Location = New System.Drawing.Point(332, 35)
        Me.chkGuiaRemision.Name = "chkGuiaRemision"
        Me.chkGuiaRemision.Size = New System.Drawing.Size(125, 17)
        Me.chkGuiaRemision.TabIndex = 29
        Me.chkGuiaRemision.Text = "Guia de Remisión"
        Me.chkGuiaRemision.UseVisualStyleBackColor = True
        '
        'chkVoucherDetraccion
        '
        Me.chkVoucherDetraccion.AutoSize = True
        Me.chkVoucherDetraccion.Location = New System.Drawing.Point(148, 61)
        Me.chkVoucherDetraccion.Name = "chkVoucherDetraccion"
        Me.chkVoucherDetraccion.Size = New System.Drawing.Size(157, 17)
        Me.chkVoucherDetraccion.TabIndex = 32
        Me.chkVoucherDetraccion.Text = "Voucher de Detracción"
        Me.chkVoucherDetraccion.UseVisualStyleBackColor = True
        '
        'chkBoleta
        '
        Me.chkBoleta.AutoSize = True
        Me.chkBoleta.Location = New System.Drawing.Point(148, 38)
        Me.chkBoleta.Name = "chkBoleta"
        Me.chkBoleta.Size = New System.Drawing.Size(62, 17)
        Me.chkBoleta.TabIndex = 31
        Me.chkBoleta.Text = "Boleta"
        Me.chkBoleta.UseVisualStyleBackColor = True
        '
        'chkVoucher
        '
        Me.chkVoucher.AutoSize = True
        Me.chkVoucher.Location = New System.Drawing.Point(11, 61)
        Me.chkVoucher.Name = "chkVoucher"
        Me.chkVoucher.Size = New System.Drawing.Size(73, 17)
        Me.chkVoucher.TabIndex = 26
        Me.chkVoucher.Text = "Voucher"
        Me.chkVoucher.UseVisualStyleBackColor = True
        '
        'chkFactura
        '
        Me.chkFactura.AutoSize = True
        Me.chkFactura.Location = New System.Drawing.Point(11, 38)
        Me.chkFactura.Name = "chkFactura"
        Me.chkFactura.Size = New System.Drawing.Size(69, 17)
        Me.chkFactura.TabIndex = 27
        Me.chkFactura.Text = "Factura"
        Me.chkFactura.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(4, 11)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(166, 13)
        Me.Label31.TabIndex = 25
        Me.Label31.Text = "Responsabilidad de Entrega"
        '
        'txtOtros
        '
        Me.txtOtros.Location = New System.Drawing.Point(408, 58)
        Me.txtOtros.Name = "txtOtros"
        Me.txtOtros.Size = New System.Drawing.Size(216, 20)
        Me.txtOtros.TabIndex = 24
        '
        'txtMotivoDesem
        '
        Me.txtMotivoDesem.Location = New System.Drawing.Point(127, 15)
        Me.txtMotivoDesem.Name = "txtMotivoDesem"
        Me.txtMotivoDesem.Size = New System.Drawing.Size(462, 20)
        Me.txtMotivoDesem.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Motivo Desembolso"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox7)
        Me.TabPage2.Controls.Add(Me.GroupBox6)
        Me.TabPage2.Controls.Add(Me.Label23)
        Me.TabPage2.Controls.Add(Me.txtClasifiPago)
        Me.TabPage2.Controls.Add(Me.txtDescripcionPago)
        Me.TabPage2.Controls.Add(Me.Label30)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(875, 278)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Pagos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.dgPagos)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 63)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(854, 157)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        '
        'dgPagos
        '
        Me.dgPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgPagos.Location = New System.Drawing.Point(9, 19)
        Me.dgPagos.Name = "dgPagos"
        Me.dgPagos.Size = New System.Drawing.Size(839, 132)
        Me.dgPagos.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.txtNombreGrte)
        Me.GroupBox6.Controls.Add(Me.txtFormaPago)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.txtEstadoPago)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(854, 54)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(157, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Forma de Pago Negociada"
        '
        'txtFormaPago
        '
        Me.txtFormaPago.Location = New System.Drawing.Point(9, 29)
        Me.txtFormaPago.Name = "txtFormaPago"
        Me.txtFormaPago.Size = New System.Drawing.Size(321, 20)
        Me.txtFormaPago.TabIndex = 12
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 229)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(107, 13)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "Descripción Pago"
        '
        'txtDescripcionPago
        '
        Me.txtDescripcionPago.Location = New System.Drawing.Point(126, 226)
        Me.txtDescripcionPago.Name = "txtDescripcionPago"
        Me.txtDescripcionPago.Size = New System.Drawing.Size(734, 20)
        Me.txtDescripcionPago.TabIndex = 10
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(41, 255)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(79, 13)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Clasificación"
        '
        'txtClasifiPago
        '
        Me.txtClasifiPago.Location = New System.Drawing.Point(126, 252)
        Me.txtClasifiPago.Name = "txtClasifiPago"
        Me.txtClasifiPago.Size = New System.Drawing.Size(113, 20)
        Me.txtClasifiPago.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(689, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Estado:"
        '
        'txtEstadoPago
        '
        Me.txtEstadoPago.Location = New System.Drawing.Point(745, 13)
        Me.txtEstadoPago.Name = "txtEstadoPago"
        Me.txtEstadoPago.Size = New System.Drawing.Size(100, 20)
        Me.txtEstadoPago.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox9)
        Me.TabPage3.Controls.Add(Me.GroupBox8)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(875, 278)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Contabilidad"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.dgContabilidad)
        Me.GroupBox9.Location = New System.Drawing.Point(6, 62)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(793, 160)
        Me.GroupBox9.TabIndex = 4
        Me.GroupBox9.TabStop = False
        '
        'dgContabilidad
        '
        Me.dgContabilidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgContabilidad.Location = New System.Drawing.Point(9, 19)
        Me.dgContabilidad.Name = "dgContabilidad"
        Me.dgContabilidad.Size = New System.Drawing.Size(778, 135)
        Me.dgContabilidad.TabIndex = 0
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label16)
        Me.GroupBox8.Controls.Add(Me.txtPeriodoTrib)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.txtEstadoConta)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(790, 50)
        Me.GroupBox8.TabIndex = 0
        Me.GroupBox8.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(9, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(108, 13)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "Periodo Tributario"
        '
        'txtPeriodoTrib
        '
        Me.txtPeriodoTrib.Location = New System.Drawing.Point(123, 14)
        Me.txtPeriodoTrib.Name = "txtPeriodoTrib"
        Me.txtPeriodoTrib.Size = New System.Drawing.Size(163, 20)
        Me.txtPeriodoTrib.TabIndex = 17
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(533, 17)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(50, 13)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "Estado:"
        '
        'txtEstadoConta
        '
        Me.txtEstadoConta.Location = New System.Drawing.Point(591, 14)
        Me.txtEstadoConta.Name = "txtEstadoConta"
        Me.txtEstadoConta.Size = New System.Drawing.Size(100, 20)
        Me.txtEstadoConta.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Solicitante"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(648, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Total Detracción"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(675, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Total Monto"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Fecha"
        '
        'txtDetraccion
        '
        Me.txtDetraccion.Location = New System.Drawing.Point(756, 67)
        Me.txtDetraccion.Name = "txtDetraccion"
        Me.txtDetraccion.Size = New System.Drawing.Size(121, 20)
        Me.txtDetraccion.TabIndex = 3
        Me.txtDetraccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEstadoDesem
        '
        Me.txtEstadoDesem.Location = New System.Drawing.Point(756, 15)
        Me.txtEstadoDesem.Name = "txtEstadoDesem"
        Me.txtEstadoDesem.Size = New System.Drawing.Size(121, 20)
        Me.txtEstadoDesem.TabIndex = 3
        '
        'txtMonto
        '
        Me.txtMonto.Location = New System.Drawing.Point(756, 41)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(121, 20)
        Me.txtMonto.TabIndex = 3
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(184, 19)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.Size = New System.Drawing.Size(60, 20)
        Me.txtNro.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(151, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nro"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(632, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Estado Desembolso"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.txtEstadoGerencia)
        Me.GroupBox2.Controls.Add(Me.Label33)
        Me.GroupBox2.Controls.Add(Me.txtEstadoContab)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.txtEstadoTesoreria)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtEstadoDesem)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TabControl1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtSolicitante)
        Me.GroupBox2.Controls.Add(Me.txtDetraccion)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtMonto)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtNro)
        Me.GroupBox2.Controls.Add(Me.txtFechaDesem)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 255)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(883, 398)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'txtSolicitante
        '
        Me.txtSolicitante.Location = New System.Drawing.Point(6, 62)
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.Size = New System.Drawing.Size(238, 20)
        Me.txtSolicitante.TabIndex = 3
        '
        'txtFechaDesem
        '
        Me.txtFechaDesem.Location = New System.Drawing.Point(54, 19)
        Me.txtFechaDesem.Name = "txtFechaDesem"
        Me.txtFechaDesem.Size = New System.Drawing.Size(91, 20)
        Me.txtFechaDesem.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(433, 39)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(60, 13)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Tesoreria"
        '
        'txtEstadoTesoreria
        '
        Me.txtEstadoTesoreria.Location = New System.Drawing.Point(499, 36)
        Me.txtEstadoTesoreria.Name = "txtEstadoTesoreria"
        Me.txtEstadoTesoreria.Size = New System.Drawing.Size(121, 20)
        Me.txtEstadoTesoreria.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(433, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(58, 13)
        Me.Label33.TabIndex = 0
        Me.Label33.Text = "Gerencia"
        '
        'txtEstadoGerencia
        '
        Me.txtEstadoGerencia.Location = New System.Drawing.Point(499, 14)
        Me.txtEstadoGerencia.Name = "txtEstadoGerencia"
        Me.txtEstadoGerencia.Size = New System.Drawing.Size(121, 20)
        Me.txtEstadoGerencia.TabIndex = 3
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(416, 63)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(77, 13)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "Contabilidad"
        '
        'txtEstadoContab
        '
        Me.txtEstadoContab.Location = New System.Drawing.Point(499, 62)
        Me.txtEstadoContab.Name = "txtEstadoContab"
        Me.txtEstadoContab.Size = New System.Drawing.Size(121, 20)
        Me.txtEstadoContab.TabIndex = 3
        '
        'txtNombreGrte
        '
        Me.txtNombreGrte.Location = New System.Drawing.Point(348, 29)
        Me.txtNombreGrte.Name = "txtNombreGrte"
        Me.txtNombreGrte.Size = New System.Drawing.Size(321, 20)
        Me.txtNombreGrte.TabIndex = 12
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(345, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 13)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Gerente Aprobador"
        '
        'txtOrdCompra
        '
        Me.txtOrdCompra.Location = New System.Drawing.Point(678, 13)
        Me.txtOrdCompra.Name = "txtOrdCompra"
        Me.txtOrdCompra.Size = New System.Drawing.Size(95, 20)
        Me.txtOrdCompra.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(595, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(77, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Ord. Compra"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(74, 26)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(307, 21)
        Me.ComboBox1.TabIndex = 6
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(698, 26)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(201, 21)
        Me.ComboBox2.TabIndex = 6
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(21, 26)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(38, 13)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "Obra:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(616, 29)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 13)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "Proveedor:"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtProveedor)
        Me.GroupBox4.Controls.Add(Me.txtObra)
        Me.GroupBox4.Controls.Add(Me.txtEmailProv)
        Me.GroupBox4.Controls.Add(Me.txtCuentaDetraccion)
        Me.GroupBox4.Controls.Add(Me.txtCuentaBco)
        Me.GroupBox4.Controls.Add(Me.txtTelefonoProv)
        Me.GroupBox4.Controls.Add(Me.txtRuc)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 57)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(854, 125)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'txtRuc
        '
        Me.txtRuc.Location = New System.Drawing.Point(634, 41)
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.Size = New System.Drawing.Size(214, 20)
        Me.txtRuc.TabIndex = 3
        '
        'txtTelefonoProv
        '
        Me.txtTelefonoProv.Location = New System.Drawing.Point(117, 67)
        Me.txtTelefonoProv.Name = "txtTelefonoProv"
        Me.txtTelefonoProv.Size = New System.Drawing.Size(157, 20)
        Me.txtTelefonoProv.TabIndex = 3
        '
        'txtCuentaBco
        '
        Me.txtCuentaBco.Location = New System.Drawing.Point(117, 95)
        Me.txtCuentaBco.Name = "txtCuentaBco"
        Me.txtCuentaBco.Size = New System.Drawing.Size(200, 20)
        Me.txtCuentaBco.TabIndex = 3
        '
        'txtCuentaDetraccion
        '
        Me.txtCuentaDetraccion.Location = New System.Drawing.Point(469, 93)
        Me.txtCuentaDetraccion.Name = "txtCuentaDetraccion"
        Me.txtCuentaDetraccion.Size = New System.Drawing.Size(163, 20)
        Me.txtCuentaDetraccion.TabIndex = 3
        '
        'txtEmailProv
        '
        Me.txtEmailProv.Location = New System.Drawing.Point(323, 67)
        Me.txtEmailProv.Name = "txtEmailProv"
        Me.txtEmailProv.Size = New System.Drawing.Size(266, 20)
        Me.txtEmailProv.TabIndex = 3
        '
        'txtObra
        '
        Me.txtObra.Location = New System.Drawing.Point(117, 15)
        Me.txtObra.Name = "txtObra"
        Me.txtObra.Size = New System.Drawing.Size(731, 20)
        Me.txtObra.TabIndex = 3
        '
        'txtProveedor
        '
        Me.txtProveedor.Location = New System.Drawing.Point(117, 41)
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.Size = New System.Drawing.Size(472, 20)
        Me.txtProveedor.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Obra / Ubicación"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(46, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Proveedor"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(595, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "RUC"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(54, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Telefono"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(60, 96)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Cuenta "
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(354, 96)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Cta. Detracciones"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(280, 73)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "Email"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtMotivoDesem)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.txtOrdCompra)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(854, 45)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(260, 12)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(139, 70)
        Me.ListView1.TabIndex = 7
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'SeguimientoOrdenDesembolsoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.gbDesembolso)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label21)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SeguimientoOrdenDesembolsoForm"
        Me.Controls.SetChildIndex(Me.Label21, 0)
        Me.Controls.SetChildIndex(Me.ComboBox2, 0)
        Me.Controls.SetChildIndex(Me.Label20, 0)
        Me.Controls.SetChildIndex(Me.ComboBox1, 0)
        Me.Controls.SetChildIndex(Me.gbDesembolso, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.gbDesembolso.ResumeLayout(False)
        CType(Me.dgDesembolso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.dgPagos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.dgContabilidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbDesembolso As System.Windows.Forms.GroupBox
    Friend WithEvents dgDesembolso As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDetraccion As System.Windows.Forms.TextBox
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMotivoDesem As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtEstadoPago As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents dgPagos As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtEstadoConta As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents dgContabilidad As System.Windows.Forms.DataGridView
    Friend WithEvents txtEstadoDesem As System.Windows.Forms.TextBox
    Friend WithEvents txtSolicitante As System.Windows.Forms.TextBox
    Friend WithEvents txtFechaDesem As System.Windows.Forms.TextBox
    Friend WithEvents chkReciboEgreso As System.Windows.Forms.CheckBox
    Friend WithEvents chkOtros As System.Windows.Forms.CheckBox
    Friend WithEvents chkGuiaRemision As System.Windows.Forms.CheckBox
    Friend WithEvents chkVoucherDetraccion As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoleta As System.Windows.Forms.CheckBox
    Friend WithEvents chkVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents chkFactura As System.Windows.Forms.CheckBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtOtros As System.Windows.Forms.TextBox
    Friend WithEvents txtDescripcionPago As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtPeriodoTrib As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFormaPago As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtClasifiPago As System.Windows.Forms.TextBox
    Friend WithEvents txtEstadoGerencia As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtEstadoTesoreria As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtEstadoContab As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNombreGrte As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtOrdCompra As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtProveedor As System.Windows.Forms.TextBox
    Friend WithEvents txtObra As System.Windows.Forms.TextBox
    Friend WithEvents txtEmailProv As System.Windows.Forms.TextBox
    Friend WithEvents txtCuentaDetraccion As System.Windows.Forms.TextBox
    Friend WithEvents txtCuentaBco As System.Windows.Forms.TextBox
    Friend WithEvents txtTelefonoProv As System.Windows.Forms.TextBox
    Friend WithEvents txtRuc As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView

End Class
