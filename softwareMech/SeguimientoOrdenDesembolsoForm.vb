Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoOrdenDesembolsoForm

#Region "Variables"
    ''' <summary>
    ''' Solicitante
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' Pagos
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls
#End Region

#Region "Métodos"


    ''' <summary>
    ''' Metodo que carga los datos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosIniciales()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        wait.Show()
        Dim sele As String = "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,moneda,simbolo,nombre,apellido,ruc,fono,email from VOrdenDesembolsoSeguimiento Order By idOp Asc"
        crearDataAdapterTable(daVDetOrden, sele)

        sele = "Select codDesembolso,fecPago,montoPago,tipoP,moneda,simbolo,nroCue,banco,pagoDet,montoD,nroP,clasif from VPagoDesembolsoSeguimiento"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select idOP,fecEnt,nroConfor  from TOrdenDesembolso"
        crearDataAdapterTable(daTabla2, sele)
        'daTabla1.SelectCommand.Parameters.Add("@idDesembolso", SqlDbType.Int).Value = 0

        Try
            crearDSAlmacen()
            daVDetOrden.Fill(dsAlmacen, "VDesembolsoSeguimiento")
            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VDesembolsoSeguimiento"
            dgDesembolso.DataSource = BindingSource0

            daTabla1.Fill(dsAlmacen, "VDesembolsoPagos")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VDesembolsoPagos"
            dgPagos.DataSource = BindingSource1

            daTabla2.Fill(dsAlmacen, "VDesembolsoComprobante")
            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDesembolsoComprobante"
            dgContabilidad.DataSource = BindingSource2



        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Cutomiza la grila de Contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnaDGVConta()
        oGrilla.ConfigGrilla(dgContabilidad)
        dgContabilidad.ReadOnly = True
        dgContabilidad.AllowUserToAddRows = False
        dgContabilidad.AllowUserToDeleteRows = False
        dgContabilidad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        With dgContabilidad
            .Columns(0).Visible = False
            .Columns("fecEnt").HeaderText = "Fecha Registro"
            .Columns("nroConfor").HeaderText = "Nro Documento"

        End With


    End Sub

    ''' <summary>
    ''' Customiza la grilla Pagos
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGVPagos()
        oGrilla.ConfigGrilla(dgPagos)
        dgPagos.ReadOnly = True
        dgPagos.AllowUserToAddRows = False
        dgPagos.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try
            With dgPagos
                'codigo Desembolo
                .Columns("codDesembolso").Visible = False
                '.Columns("codDesembolso").DisplayIndex = 8
                'fecha de pago
                .Columns("fecPago").HeaderText = "Fecha Pago"
                '.Columns("fecPago").DisplayIndex = 0
                .Columns("fecPago").Width = 80
                'Simbolo Moneda
                .Columns("simbolo").HeaderText = ""
                .Columns("simbolo").DisplayIndex = 2
                .Columns("simbolo").Width = 30

                'monto Pagado 
                .Columns("montoPago").HeaderText = "Monto"
                .Columns("montoPago").DisplayIndex = 3
                .Columns("montoPago").Width = 100
                .Columns("montoPago").DefaultCellStyle.Format = "N2"
                .Columns("montoPago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'Medio de PAgo
                .Columns("tipoP").HeaderText = "Medio Pago"
                .Columns("tipoP").DisplayIndex = 4
                .Columns("tipoP").Width = 200
                'Moneda

                ' .Columns("monea").DisplayIndex = 7
                .Columns("moneda").Visible = False
                'Banco 
                .Columns("banco").HeaderText = "Banco"
                .Columns("banco").DisplayIndex = 5
                .Columns("banco").Width = 60
                'Numero de Cuenta usada
                .Columns("nroCue").HeaderText = "N° Cuenta"
                .Columns("nroCue").DisplayIndex = 6
                .Columns("nroCue").Width = 150

                'Descripcion del pago
                .Columns("pagoDet").HeaderText = "Descripción"
                '.Columns("pagoDet").DisplayIndex = 6
                .Columns("pagoDet").Width = 250

                'Monto de detracción
                .Columns("montoD").HeaderText = "Detracción"
                .Columns("montoD").Width = 200
                .Columns("montoD").DefaultCellStyle.Format = "N2"
                .Columns("montoD").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                'numero Operacion /cheque
                .Columns("nroP").HeaderText = "Nro"
                .Columns("nroP").Width = 40
                'Clasificación de pagos 
                .Columns("clasif").HeaderText = "Clasificación"
                .Columns("clasif").Width = 100

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    ''' <summary>
    ''' Customisa la grilla Desembolsos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV()

        oGrilla.ConfigGrilla(dgDesembolso)
        dgDesembolso.ReadOnly = True
        dgDesembolso.AllowUserToAddRows = False
        dgDesembolso.AllowUserToDeleteRows = False
        With dgDesembolso

            .Columns("idOP").Visible = False
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").DisplayIndex = 0
            .Columns("serie").Width = 40
            'NroDesembolso
            .Columns("nroDes").HeaderText = "Nro"
            .Columns("nroDes").DisplayIndex = 1
            .Columns("nroDes").Width = 50
            'Numero
            .Columns("nro").Visible = False
            'Fecha (Solicitud) Desembolso
            .Columns("fecDes").HeaderText = "Fecha"
            .Columns("fecDes").DisplayIndex = 2
            'estado
            .Columns("estado_desembolso").HeaderText = "Estado"
            .Columns("estado_desembolso").DisplayIndex = 3
            'Datos Historicos
            .Columns("hist").Visible = False
            'Monto de Desembolso
            .Columns("monto").HeaderText = "Monto"
            .Columns("monto").DisplayIndex = 5
            .Columns("monto").DefaultCellStyle.Format = "N2"
            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("montoDet").DefaultCellStyle.Font

            'Monto de Detracción
            .Columns("montoDet").HeaderText = "Detracción"
            .Columns("montoDet").DefaultCellStyle.Format = "N2"
            .Columns("montoDet").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Monto Diferencia 
            .Columns("montoDif").Visible = False
            'Obra 
            .Columns("obra").HeaderText = "Obra/Lugar"
            .Columns("obra").Width = 200
            'Proveedor
            .Columns("proveedor").HeaderText = "Proveedor"
            'Forma de pago negociada
            .Columns("banco").HeaderText = "Forma_Pago"
            'Nro de cuenta del proveedor
            .Columns("nroCta").Visible = False
            'Nro de cuentra para Detracción del proveedor
            .Columns("nroDet").Visible = False
            'Motivo de Desembolso diferente a Orden de compra
            .Columns("datoReq").HeaderText = "Motivo"
            'check de Factura
            .Columns("factCheck").Visible = False
            'check de Boleta
            .Columns("bolCheck").Visible = False
            'check de Guia de remision
            .Columns("guiaCheck").Visible = False
            'check de voucheer
            .Columns("vouCheck").Visible = False
            'check de voucher de Detraccion
            .Columns("vouDCheck").Visible = False
            ' check de recibo
            .Columns("reciCheck").Visible = False
            ' check de otro tipo de documento
            .Columns("otroCheck").Visible = False
            ' descripcion de otro tipo de documento
            .Columns("descOtro").Visible = False
            ' número de documento de conformidad entregado a contabilidad
            .Columns("nroConfor").Visible = False
            'fecha de entrega de documentos a contabilidad
            .Columns("fecEnt").Visible = False
            'Moneda
            .Columns("moneda").Visible = False
            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").DisplayIndex = 4
            .Columns("simbolo").Width = 40
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Nombre de Solicitante
            .Columns("nombre").Visible = False
            .Columns("apellido").Visible = False
            'Ruc del proveedor
            .Columns("ruc").Visible = False
            'Telefono Proveedor
            .Columns("fono").Visible = False
            'Dirección de email
            .Columns("email").Visible = False


        End With





    End Sub

    Private Sub enlazarTextConta()
        If dgContabilidad.Rows.Count = 0 Then
            Exit Sub

        Else

            txtFechaRegConta.Text = BindingSource2.Item(BindingSource2.Position)(1)
            txtNroComprobConta.Text = BindingSource2.Item(BindingSource2.Position)(2)

        End If
    End Sub


    ''' <summary>
    ''' Enlaza los datos de la grilla Desembolso con los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarText()
        If dgDesembolso.Rows.Count = 0 Then
            Exit Sub

        Else
            'Datos de Generales de Orden desembolso
            txtEstadoDesem.Text = BindingSource0.Item(BindingSource0.Position)(5)
            txtNro.Text = BindingSource0.Item(BindingSource0.Position)(2)
            txtFechaDesem.Text = BindingSource0.Item(BindingSource0.Position)(4)
            txtSolicitante.Text = BindingSource0.Item(BindingSource0.Position)(28) + " " + BindingSource0.Item(BindingSource0.Position)(29).ToString()
            txtMonto.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(7).ToString
            txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(8).ToString

            'txtMonto.Text = BindingSource0.Item(BindingSource0.Position)(7).ToString
            'txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(8).ToString


            txtFormaPago.Text = BindingSource0.Item(BindingSource0.Position)(12)

            'Datos específicos de desembolso
            txtMotivoDesem.Text = BindingSource0.Item(BindingSource0.Position)(15)
            txtObra.Text = BindingSource0.Item(BindingSource0.Position)(10)
            txtProveedor.Text = BindingSource0.Item(BindingSource0.Position)(11)
            txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(30)

            txtTelefonoProv.Text = BindingSource0.Item(BindingSource0.Position)(31)
            txtEmailProv.Text = BindingSource0.Item(BindingSource0.Position)(32)
            txtCuentaBco.Text = BindingSource0.Item(BindingSource0.Position)(13)
            txtCuentaDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(14)

            'Factura
            If BindingSource0.Item(BindingSource0.Position)(16) = 1 Then
                chkFactura.Checked = True
            Else
                chkFactura.Checked = False
            End If

            'Boleta
            If BindingSource0.Item(BindingSource0.Position)(17) = 1 Then
                chkBoleta.Checked = True
            Else
                chkBoleta.Checked = False
            End If
            'Guia de Remision
            If BindingSource0.Item(BindingSource0.Position)(18) = 1 Then
                chkGuiaRemision.Checked = True
            Else
                chkGuiaRemision.Checked = False
            End If

            'Voucher
            If BindingSource0.Item(BindingSource0.Position)(19) = 1 Then
                chkVoucher.Checked = True
            Else
                chkVoucher.Checked = False
            End If
            'Voucher Detracción
            If BindingSource0.Item(BindingSource0.Position)(20) = 1 Then
                chkVoucherDetraccion.Checked = True
            Else
                chkVoucherDetraccion.Checked = False
            End If
            'Recibo de Egresos
            If BindingSource0.Item(BindingSource0.Position)(21) = 1 Then
                chkReciboEgreso.Checked = True
            Else
                chkReciboEgreso.Checked = False
            End If
            'Otro 
            If BindingSource0.Item(BindingSource0.Position)(22) = 1 Then
                chkOtros.Checked = True
            Else
                chkOtros.Checked = False
            End If

            '   oGrilla.FormatoContabilidad(txtMonto)
            '  oGrilla.FormatoContabilidad(txtDetraccion)

        End If
    End Sub

    ''' <summary>
    ''' Enlaza los dotos de la grilla Pagos con los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextPagos()
        If dgPagos.Rows.Count = 0 Then
            Exit Sub
        Else
            txtFechaPago.Text = BindingSource1.Item(BindingSource1.Position)(1)
            txtMonedaPago.Text = BindingSource1.Item(BindingSource1.Position)(4)
            txtMontoPago.Text = BindingSource1.Item(BindingSource1.Position)(2)
            txtBancoPago.Text = BindingSource1.Item(BindingSource1.Position)(7)
            txtCuentaPago.Text = BindingSource1.Item(BindingSource1.Position)(6)
            txtMedioPago.Text = BindingSource1.Item(BindingSource1.Position)(3)
            txtDescripcionPago.Text = BindingSource1.Item(BindingSource1.Position)(8)
            txtDetraccionPago.Text = BindingSource1.Item(BindingSource1.Position)(9)
            txtNroPago.Text = BindingSource1.Item(BindingSource1.Position)(10)
            txtClasifiPago.Text = BindingSource1.Item(BindingSource1.Position)(11)

        End If

        'Dando formato a los números
       
        ' oGrilla.FormatoContabilidad(txtMontoPago)

        'oGrilla.FormatoContabilidad(txtDetraccionPago)

    End Sub


    ''' <summary>
    ''' configura los colores de los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        'recorriendo tabs de tabcontrol
        For j As Integer = 0 To TabControl1.TabPages.Count - 1

            For index As Integer = 0 To TabControl1.TabPages(j).Controls.Count - 1
                'TabControl1.TabPages(0).Controls
                If TypeOf TabControl1.TabPages(j).Controls(index) Is GroupBox Then
                    TabControl1.TabPages(j).BackColor = BackColorP
                    oGrilla.configurarColorControl("Label", TabControl1.TabPages(j).Controls(index), ForeColorLabel)
                    oGrilla.configurarColorControl("CheckBox", TabControl1.TabPages(j).Controls(index), ForeColorLabel)
                End If
            Next

        Next


        oGrilla.configurarColorControl("Label", GroupBox2, ForeColorLabel)
        



    End Sub
#End Region

    Private Sub SeguimientoOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        configurarColorControl()

        DatosIniciales()
        'Modifica las columnas de Grilla Desembolso
        ModificandoColumnasDGV()
        ModificandoColumnasDGVPagos()
        ModificandoColumnaDGVConta()

        enlazarText()

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub txtObra_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObra.TextChanged

    End Sub


    Private Sub dgDesembolso_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDesembolso.CellClick
        enlazarText()
        'filtrando para que muestre los registros de pagos por orden de desembolso seleccionado
        BindingSource1.Filter = "codDesembolso=" & BindingSource0.Item(BindingSource0.Position)(0)
        'filtrando para que muestre los registros de contabilidad por orden de desembolso seleccionado
        BindingSource2.Filter = "idOP=" & BindingSource0.Item(BindingSource0.Position)(0)

    End Sub

    Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMonedaPago.TextChanged, txtFechaPago.TextChanged

    End Sub

    Private Sub dgPagos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgPagos.CellClick

        enlazarTextPagos()

    End Sub

    Private Sub dgContabilidad_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContabilidad.CellClick
        enlazarTextConta()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgContabilidad.Dispose()
        dgDesembolso.Dispose()
        dgPagos.Dispose()
        Me.Close()

    End Sub

    Private Sub SeguimientoOrdenDesembolsoForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Close()
    End Sub

    
End Class
