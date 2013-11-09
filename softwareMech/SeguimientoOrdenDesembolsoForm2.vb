Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports CrystalDecisions.Shared


Public Class SeguimientoOrdenDesembolsoForm2

#Region "Variables"
    ''' <summary>
    ''' Desembolsos / Solicitante
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
    ''' Aprobaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource3 As New BindingSource

    ''' <summary>
    ''' Obra/Lugar
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource4 As New BindingSource

    ''' <summary>
    ''' Proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource5 As New BindingSource

    ''' <summary>
    ''' Solicitante
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindignSource6 As New BindingSource

    Dim BindingSource7 As New BindingSource

    ''' <summary>
    ''' Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' instancia de Objeto de la clase datamanager, de administración de datos
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    Dim vCodDesem As Integer = -1

    'variable temporales para fecha de inicio y fin

    Dim _fechaIni As String
    Dim _fechaFin As String

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Crea un DataAdapter 
    ''' </summary>
    ''' <param name="DATable">Adapater</param>
    ''' <param name="sele">Procedimiento Almacenado</param>
    ''' <remarks></remarks>
    Public Sub crearDataAdapterTableProcedure(ByRef DATable As SqlDataAdapter, ByVal sele As String)
        DATable = New SqlDataAdapter
        Dim cmSele As New SqlCommand
        cmSele.CommandType = CommandType.StoredProcedure
        cmSele.CommandText = sele
        cmSele.Connection = Cn
        'Agregando el comado select al dataAdapter
        DATable.SelectCommand = cmSele
    End Sub

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
        Dim sele As String '= "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,moneda,simbolo,solicitante,ruc,fono,email,codObra,codIde from VOrdenDesembolsoSeguimiento"
        sele = "PA_SeguimientoDesembolso"
        crearDataAdapterTableProcedure(daVDetOrden, sele)
        'crearDataAdapterTable(daVDetOrden, sele)

        sele = "PA_SeguimientoPagos"
        'sele = "Select codDesembolso,fecPago,montoPago,tipoP,moneda,simbolo,nroCue,banco,pagoDet,montoD,nroP,clasif from VPagoDesembolsoSeguimiento"
        'crearDataAdapterTable(daTabla1, sele)
        crearDataAdapterTableProcedure(daTabla1, sele)

        'sele = "PA_SeguimientoComprobantes"
        sele = "select idOP,fecEnt,nroConfor  from TOrdenDesembolso"
        crearDataAdapterTable(daTabla2, sele)
        'crearDataAdapterTableProcedure(daTabla2, sele)

        'sele = "PA_SeguimientoAprobaciones"
        sele = "select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir from VAprobacionesSeguimiento "

        crearDataAdapterTable(daTabla3, sele)
        'crearDataAdapterTableProcedure(daTabla3, sele)

        sele = "PA_LugarTrabajo" '"Select codigo,nombre from tLugarTrabajo"
        'crearDataAdapterTable(daTabla4, sele)
        crearDataAdapterTableProcedure(daTabla4, sele)

        sele = "PA_Proveedores"
        '"Select codIde,razon from TIdentidad where idTipId=2"
        'crearDataAdapterTable(daTabla5, sele)
        crearDataAdapterTableProcedure(daTabla5, sele)
        'daTabla1.SelectCommand.Parameters.Add("@idDesembolso", SqlDbType.Int).Value = 0

        sele = "select (nombre +' '+ apellido) as solicitante from Tpersonal where codPers > 1 order by solicitante asc"
        crearDataAdapterTable(daTabla6, sele)

        Try
            crearDSAlmacen()
            daVDetOrden.Fill(dsAlmacen, "VDesembolsoSeguimiento")
            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VDesembolsoSeguimiento"
            dgDesembolso.DataSource = BindingSource0
            BindingNavigator1.BindingSource = BindingSource0

            daTabla1.Fill(dsAlmacen, "VDesembolsoPagos")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VDesembolsoPagos"
            dgPagos.DataSource = BindingSource1 ' 

            daTabla2.Fill(dsAlmacen, "VDesembolsoComprobante")
            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDesembolsoComprobante"
            dgContabilidad.DataSource = BindingSource2

            daTabla3.Fill(dsAlmacen, "VAprobaciones")
            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VAprobaciones"

            daTabla4.Fill(dsAlmacen, "TLugarTrabajo")
            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "TLugarTrabajo"
            cbObra.DataSource = BindingSource4
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"


            daTabla5.Fill(dsAlmacen, "TIdentidad")
            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "TIdentidad"
            ' BindingSource5.Filter = "idTipId=2"
            BindingSource5.Sort = "razon asc"
            cbProveedor.DataSource = BindingSource5
            cbProveedor.DisplayMember = "razon"
            cbProveedor.ValueMember = "codIde"

            daTabla6.Fill(dsAlmacen, "TSolicitante")
            BindignSource6.DataSource = dsAlmacen
            BindignSource6.DataMember = "TSolicitante"
            BindignSource6.Sort = "solicitante ASC"
            

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
        'oGrilla.ConfigGrilla(dgContabilidad)
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
        'oGrilla.ConfigGrilla(dgPagos)
        dgPagos.ReadOnly = True
        dgPagos.AllowUserToAddRows = False
        dgPagos.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try
            With dgPagos
                'codigo Desembolo
                .Columns("codDesembolso").Visible = False
                'fecha de pago
                .Columns("fecPago").HeaderText = "Fecha"
                '.Columns("fecPago").DisplayIndex = 0
                .Columns("fecPago").Width = 70
                'Simbolo Moneda
                .Columns("simbolo").HeaderText = ""
                .Columns("simbolo").DisplayIndex = 2
                .Columns("simbolo").Width = 30

                'monto Pagado 
                .Columns("montoPago").HeaderText = "Monto"
                .Columns("montoPago").DisplayIndex = 3
                .Columns("montoPago").Width = 80
                .Columns("montoPago").DefaultCellStyle.Format = "N2"
                .Columns("montoPago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'Medio de PAgo
                .Columns("tipoP").HeaderText = "Medio Pago"
                .Columns("tipoP").DisplayIndex = 5
                .Columns("tipoP").Width = 220
                'Moneda

                ' .Columns("monea").DisplayIndex = 7
                .Columns("moneda").Visible = False
                'Banco 
                .Columns("banco").HeaderText = "Banco"
                .Columns("banco").DisplayIndex = 7
                .Columns("banco").Width = 100
                'Numero de Cuenta usada
                .Columns("nroCue").HeaderText = "N° Cuenta"
                .Columns("nroCue").DisplayIndex = 8
                .Columns("nroCue").Width = 160

                'Descripcion del pago
                .Columns("pagoDet").Visible = False
                '                .Columns("pagoDet").HeaderText = "Descripción"
                '.Columns("pagoDet").DisplayIndex = 6
                '.Columns("pagoDet").Width = 250

                'Monto de detracción
                .Columns("montoD").HeaderText = "Detracción"
                .Columns("montoD").DisplayIndex = 4
                .Columns("montoD").Width = 80
                .Columns("montoD").DefaultCellStyle.Format = "N2"
                .Columns("montoD").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                'numero Operacion /cheque
                .Columns("nroP").HeaderText = "N°_Op./Cheq."
                .Columns("nroP").Width = 85
                .Columns("nroP").DisplayIndex = 6
                'Clasificación de pagos 
                .Columns("clasif").Visible = False
                '  .Columns("clasif").HeaderText = "Clasificación"
                '.Columns("clasif").Width = 100

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

        'oGrilla.ConfigGrilla(dgDesembolso)
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
            .Columns("nroDes").Width = 40
            'Numero
            .Columns("nro").Visible = False
            'Fecha (Solicitud) Desembolso
            .Columns("fecDes").HeaderText = "Fecha"
            .Columns("fecDes").DisplayIndex = 2
            .Columns("fecDes").Width = 70
            'estado
            .Columns("estado_desembolso").HeaderText = "Estado"
            .Columns("estado_desembolso").DisplayIndex = 3
            .Columns("estado_desembolso").Width = 75
            'Datos Historicos
            .Columns("hist").Visible = False
            'Monto de Desembolso
            .Columns("monto").HeaderText = "Monto"
            .Columns("monto").DisplayIndex = 5
            .Columns("monto").Width = 78
            .Columns("monto").DefaultCellStyle.Format = "N2"
            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("montoDet").DefaultCellStyle.Font
            'Monto de Detracción
            .Columns("montoDet").HeaderText = "Detracción"
            .Columns("montoDet").DefaultCellStyle.Format = "N2"
            .Columns("montoDet").Width = 78
            .Columns("montoDet").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Monto Diferencia 
            .Columns("montoDif").Visible = False
            'Obra 
            .Columns("obra").HeaderText = "Obra/Lugar"
            .Columns("obra").Width = 270
            'Proveedor
            .Columns("proveedor").HeaderText = "Proveedor"
            .Columns("proveedor").Width = 200
            'Forma de pago negociada
            .Columns("banco").Visible = False
            '.Columns("banco").HeaderText = "Forma_Pago"
            'Nro de cuenta del proveedor
            .Columns("nroCta").Visible = False
            'Nro de cuentra para Detracción del proveedor
            .Columns("nroDet").Visible = False
            'Motivo de Desembolso diferente a Orden de compra
            .Columns("datoReq").Visible = False
            '.Columns("datoReq").HeaderText = "Motivo"
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
            .Columns("simbolo").Width = 30
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Nombre de Solicitante
            .Columns("solicitante").Visible = False
            .Columns("codObra").Visible = False
            .Columns("codIde").Visible = False
            'Ruc del proveedor
            .Columns("ruc").Visible = False
            'Telefono Proveedor
            .Columns("fono").Visible = False
            'Dirección de email
            .Columns("email").Visible = False
            .Columns("firmaSolicitante").Visible = False
            .Columns("estado_Gere").Visible = False
            .Columns("estado_Teso").Visible = False
            .Columns("firmaGerencia").Visible = False
            .Columns("firmaTesoreria").Visible = False
            .Columns("estado_Conta").Visible = False
            .Columns("firmaContabilidad").Visible = False

        End With
    End Sub

    Private Sub enlazarTextConta()
        If dgContabilidad.Rows.Count = 0 Then
            Exit Sub

        Else


        End If
    End Sub


    ''' <summary>
    ''' Enlaza los datos de la grilla Desembolso con los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarText()
        If dgDesembolso.RowCount = 0 Then
            Exit Sub



        Else
            'Datos de Generales de Orden desembolso
            txtEstadoDesem.Text = BindingSource0.Item(BindingSource0.Position)(5)

            'pinta TextBox txtEstadoDesem
            Dim aCriterios As String() = {"TERMINADO", "ANULADO"}
            Dim aBackColors As Color() = {Color.Green, Color.Red}
            Dim aForeColors As Color() = {Color.White, Color.White}
            colorTextBox(txtEstadoDesem, aCriterios, aBackColors, aForeColors)


            txtNro.Text = BindingSource0.Item(BindingSource0.Position)(2)
            txtFechaDesem.Text = BindingSource0.Item(BindingSource0.Position)(4)
            txtSolicitante.Text = BindingSource0.Item(BindingSource0.Position)(28)
            txtMonto.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(7).ToString
            txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(8).ToString

            'txtMonto.Text = BindingSource0.Item(BindingSource0.Position)(7).ToString
            'txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(8).ToString


            txtFormaPago.Text = BindingSource0.Item(BindingSource0.Position)(12)

            'Datos específicos de desembolso
            txtMotivoDesem.Text = BindingSource0.Item(BindingSource0.Position)(15)
            txtObra.Text = BindingSource0.Item(BindingSource0.Position)(10)
            txtProveedor.Text = BindingSource0.Item(BindingSource0.Position)(11)
            txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(29)

            txtTelefonoProv.Text = BindingSource0.Item(BindingSource0.Position)(30)
            txtEmailProv.Text = BindingSource0.Item(BindingSource0.Position)(31)
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
            txtDescripcionPago.Clear()
            txtClasifiPago.Clear()

            Exit Sub
        Else
            If BindingSource1.Position >= 0 Then
                txtDescripcionPago.Text = BindingSource1.Item(BindingSource1.Position)(8)
                txtClasifiPago.Text = BindingSource1.Item(BindingSource1.Position)(11)
            End If

        End If

        'Dando formato a los números

        ' oGrilla.FormatoContabilidad(txtMontoPago)

        'oGrilla.FormatoContabilidad(txtDetraccionPago)

    End Sub

    ''' <summary>
    ''' Pinta el TextBox Seleccionado con los parametros enviados
    ''' </summary>
    ''' <param name="pTextBox">texbox</param>
    ''' <param name="criterios">criterio a evaluar</param>
    ''' <param name="pBackColor">arreglo de colores la fondo</param>
    ''' <param name="pForeColor">arrelgo de colores para letra</param>
    ''' <remarks></remarks>
    Private Sub colorTextBox(ByVal pTextBox As TextBox, ByVal criterios As String(), ByVal pBackColor As Color(), ByVal pForeColor As Color())

        For i As Integer = 0 To criterios.Length - 1
            If pTextBox.Text = criterios.GetValue(i).ToString Then
                pTextBox.BackColor = pBackColor.GetValue(i)
                pTextBox.ForeColor = pForeColor.GetValue(i)
                Exit For
            Else
                pTextBox.BackColor = Color.White
                pTextBox.ForeColor = Color.Black
            End If
        Next


    End Sub
    ''' <summary>
    ''' Enlaza los textos de clasificación y subclasficación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextClasificacion()
        txtClasificacion.Text = BindingSource7.Item(0)(0)
        txtSubClasif.Text = BindingSource7.Item(0)(1)

    End Sub

    Private Sub enlazarTextAprobaciones()
        'Dim nro As Integer = BindingSource0.Item(BindingSource0.Position)(0)


        Dim aCriterios As String() = {"APROBADO", "OBSERVADO", "RECHAZADO"}
        Dim aBackColors As Color() = {Color.Green, Color.Yellow, Color.Red}
        Dim aForeColors As Color() = {Color.White, Color.Red, Color.White}

        'Aprobación Gerencia probando si 
        If BindingSource3.Count > 1 Then
            If BindingSource3.Item(1)(3) = "GERENCIA" Then
                txtEstadoGerencia.Text = BindingSource3.Item(1)(4)
                txtNombreGerente.Text = BindingSource3.Item(1)(1) & " " & BindingSource3.Item(1)(2)
                txtObsGerencia.Text = BindingSource3.Item(1)(5)
                txtFechaGerencia.Text = BindingSource3.Item(1)(6)
            End If

        Else
            txtEstadoGerencia.Text = "PENDIENTE"
            txtNombreGerente.Text = ""
            txtFechaGerencia.Text = ""
            txtObsGerencia.Text = ""

        End If
        'Aprobación Tesoreria
        If BindingSource3.Count > 2 Then
            If BindingSource3.Item(2)(3) = "TESORERIA" Then
                txtEstadoTesoreria.Text = BindingSource3.Item(2)(4)
                txtNombreTesoreria.Text = BindingSource3.Item(2)(1) & " " & BindingSource3.Item(2)(2)
                txtFechaTesoreria.Text = BindingSource3.Item(2)(6)
            End If

        Else
            txtEstadoTesoreria.Text = "PENDIENTE"
            txtNombreTesoreria.Text = ""
            txtFechaTesoreria.Text = ""
        End If
        'Aprobación de Contabilidad
        If BindingSource3.Count > 3 Then
            If BindingSource3.Item(3)(3) = "CONTABILIDAD" Then
                txtEstadoContab.Text = BindingSource3.Item(3)(4)
                txtNombreConta.Text = BindingSource3.Item(3)(1) & " " & BindingSource3.Item(3)(2)
                txtFechaContabilidad.Text = BindingSource3.Item(3)(6)
            End If

        Else
            txtEstadoContab.Text = "PENDIENTE"
            txtNombreConta.Text = ""
            txtFechaContabilidad.Text = ""
        End If

        'Pintando el Texbox
        colorTextBox(txtEstadoGerencia, aCriterios, aBackColors, aForeColors)
        colorTextBox(txtEstadoTesoreria, aCriterios, aBackColors, aForeColors)
        colorTextBox(txtEstadoContab, aCriterios, aBackColors, aForeColors)
    End Sub


    ''' <summary>
    ''' configura los colores de los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()
        Me.BackColor = BackColorP

        'Color para los labels del contenedor principal
        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then 'LABELS
                Me.Controls(i).ForeColor = ForeColorLabel

            End If

            If TypeOf Me.Controls(i) Is CheckBox Then 'CHECKBOX
                Me.Controls(i).ForeColor = ForeColorLabel

            End If

            'If TypeOf Me.Controls(i) Is TextBox Then 'TEXTBOX
            '    CType(Me.Controls(i), TextBox).ReadOnly = True
            'End If

            If TypeOf Me.Controls(i) Is GroupBox Then 'TEXTBOX
                For c As Integer = 0 To Me.Controls(i).Controls.Count - 1
                    If TypeOf Me.Controls(i).Controls(c) Is TextBox Then 'TEXTBOX
                        CType(Me.Controls(i).Controls(c), TextBox).ReadOnly = True
                    End If

                    If TypeOf Me.Controls(i).Controls(c) Is RadioButton Then
                        Me.Controls(i).Controls(c).ForeColor = ForeColorLabel
                    End If

                Next
            End If
        Next

        'recorriendo tabs de tabcontrol
        For j As Integer = 0 To TabControl1.TabPages.Count - 1

            For index As Integer = 0 To TabControl1.TabPages(j).Controls.Count - 1
                TabControl1.TabPages(j).BackColor = BackColorP

                'TabControl1.TabPages(0).Controls
                If TypeOf TabControl1.TabPages(j).Controls(index) Is GroupBox Then
                    TabControl1.TabPages(j).BackColor = BackColorP
                    oGrilla.configurarColorControl("Label", TabControl1.TabPages(j).Controls(index), ForeColorLabel)
                    oGrilla.configurarColorControl("CheckBox", TabControl1.TabPages(j).Controls(index), ForeColorLabel)

                    For k As Integer = 0 To TabControl1.TabPages(j).Controls(index).Controls.Count - 1
                        If TypeOf TabControl1.TabPages(j).Controls(index).Controls(k) Is TextBox Then
                            CType(TabControl1.TabPages(j).Controls(index).Controls(k), TextBox).ReadOnly = True  ' ForeColorLabel
                        End If

                    Next

                End If
                If TypeOf TabControl1.TabPages(j).Controls(index) Is Label Then
                    TabControl1.TabPages(j).Controls(index).ForeColor = ForeColorLabel
                End If
                If TypeOf TabControl1.TabPages(j).Controls(index) Is TextBox Then
                    CType(TabControl1.TabPages(j).Controls(index), TextBox).ReadOnly = True  ' ForeColorLabel
                End If
                If TypeOf TabControl1.TabPages(j).Controls(index) Is CheckBox Then
                    TabControl1.TabPages(j).Controls(index).ForeColor = ForeColorLabel
                End If

            Next

        Next

        'Para el Group Box del form
        oGrilla.configurarColorControl("Label", GroupBox2, ForeColorLabel)

        '
        'chkSolicitante.BackColor = Color.White


    End Sub


    ''' <summary>
    ''' Pinta la grila de Desembolsos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ColorearGrilla()

        oGrilla.colorearFilasDGV(dgDesembolso, "estado_desembolso", "TERMINADO", Color.Green, Color.White)
        'oGrilla.colorearFilasDGV(dgDesembolso, "estado_desembolso", "PENDIENTE", Color.Yellow, Color.Red)

        oGrilla.colorearFilasDGV(dgDesembolso, "estado_desembolso", "ANULADO", Color.Red, Color.White)

    End Sub

    ''' <summary>
    ''' Añade criterios a los filtros
    ''' </summary>
    ''' <param name="criterio"></param>
    ''' <param name="filtro"></param>
    ''' <remarks></remarks>
    Private Function AddCriterioFiltro(ByVal criterio As String, ByVal filtro As String) As String
        If filtro.Length > 0 Then
            filtro &= " and " & criterio
        Else
            filtro &= " " & criterio
        End If
        Return filtro
    End Function
    ''' <summary>
    ''' Filtra Desembolso 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub filtrando()
        If cbObra.Items.Count > 0 And cbObra.Items.Count > 0 Then 'BindingSource4.Position >= 0 And BindingSource5.Position >= 0 Then

            BindingSource0.Filter = ""
            Dim pFiltro As String = BindingSource0.Filter
            Dim pCriterio As String
            'Filtro para Obra
            If chkObras.Checked = False Then
                pCriterio = "codObra='" & cbObra.SelectedValue & "'"
                pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            End If

            'If chkProveedor.Checked = False Then
            '    pCriterio = "codIde =" & cbProveedor.SelectedValue
            '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            'End If

            'If cbEstadoDesembolso.Text = "TODOS" Or cbEstadoDesembolso.Text = "" Then
            ' AddCriterioFiltro(pCriterio, pFiltro)

            'Else
            '    pCriterio = "estado_desembolso='" & cbEstadoDesembolso.Text.Trim() & "'"
            '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            'End If

            'If chkSolicitante.Checked = False Then
            '    pCriterio = "solicitante = '" & cbSolicitante.Text.Trim() & "'"
            '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            'End If

            'If txtNroDesembolso.Text.Length > 0 Then
            '    pCriterio = "nroDes=" & txtNroDesembolso.Text.Trim()
            '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            'End If

            BindingSource0.Filter = pFiltro

            BindingSource0.Sort = "NroDes Desc"
        End If
        'Colorea la Grilla
        ColorearGrilla()

    End Sub


    ''' <summary>
    ''' cambia a texto los numeros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function cambiarNroTotalLetra() As String
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        Dim retorna As String = ""
        If BindingSource0.Item(BindingSource0.Position)(27) = "S/." Then    '30=Nuevos solesl
            'If BindingSource10.Item(BindingSource10.Position)(7) = 30 Then
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(BindingSource0.Item(BindingSource0.Position)(7)) > 0 Then
            cALetra.Numero = Val(CDbl(BindingSource0.Item(BindingSource0.Position)(7)))
            retorna = "SON: " & cALetra.ALetra.ToUpper()
        End If
        Return retorna
    End Function


    ''' <summary>
    ''' recupera el nro de Orden de compra
    ''' </summary>
    ''' <param name="criterio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarOrdenCompra(ByVal criterio As String) As Object
        Dim consulta As String = "PA_RecuperarOrdenCompra" ' "select nroOrden from TDesOrden where idOp=" & criterio
        Dim comando As New SqlCommand
        comando.Connection = Cn
        comando.Parameters.Add(New SqlParameter("@idDesembolso", SqlDbType.Int)).Value = criterio

        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = consulta
        Return comando.ExecuteScalar
    End Function

    ''' <summary>
    ''' recupera la orden de compra con un formato concatenado para impresion
    ''' </summary>
    ''' <param name="idOP"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarNroOrdenCompra(ByVal idOP As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select mech.FN_ConcaNroOrden1(" & idOP & ")"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

#End Region

#Region "Eventos"

#End Region


    Private Sub SeguimientoOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        Me.AcceptButton = btnVer


        configurarColorControl()

        'Cargando el combo Obras
        oDataManager.CargarCombo("PA_LugarTrabajo", CommandType.StoredProcedure, cbObra, "codigo", "nombre")
        'Ordenando de forma alfabetica

        'Cargando el combo de Proveedores
        oDataManager.CargarCombo("PA_Proveedores", CommandType.StoredProcedure, cbProveedor, "codIde", "razon")

        oDataManager.CargarCombo("select codSerO,serie from TSerieOrden where estado=1 order by serie", CommandType.Text, cbSerie, "serie", "serie")

        'Cargando el combo Solicitante
        'oDataManager.CargarCombo("select (nombre +' '+ apellido) as solicitante from Tpersonal where codPers > 1 order by solicitante asc", CommandType.Text, cbSolicitante.ComboBox, "solicitante", "solicitante")


        'DatosIniciales()

        ' dgDesembolso.FirstDisplayedScrollingRowIndex = 0

        'Modifica las columnas de Grilla Desembolso

        'ModificandoColumnasDGV()
        'ModificandoColumnasDGVPagos()
        'ModificandoColumnaDGVConta()

        'BindingSource1.Filter = "codDesembolso=" & BindingSource0.Item(BindingSource0.Position)(0)
        'BindingSource2.Filter = "idOP=" & BindingSource0.Item(BindingSource0.Position)(0)
        'BindingSource3.Filter = "idOP=" & BindingSource0.Item(BindingSource0.Position)(0)



        'enlazarText()
        'enlazarTextAprobaciones()


        Me.Cursor = Cursors.Default

        'Ejecuta la carga de Datos para el día actual
        'btnVer.PerformClick()

        ' filtrando()
        wait.Close()
    End Sub


    Private Sub dgDesembolso_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDesembolso.CellClick, dgDesembolso.CellEnter

        If dgDesembolso.RowCount > 0 Then

            If vCodDesem <> BindingSource0.Item(BindingSource0.Position)(0) Then
                'Enlazar los datos de Grilla con los de form
                enlazarTextPagos()

                Dim queryPagos As String = "Select codDesembolso,fecPago,montoPago,tipoP,moneda,simbolo,nroCue,banco,pagoDet,montoD,nroP,clasif from VPagoDesembolsoSeguimiento where codDesembolso=" & BindingSource0.Item(BindingSource0.Position)(0)
                '"PA_SeguimientoPagos"
                oDataManager.CargarGrilla(queryPagos, CommandType.Text, dgPagos, BindingSource1)
                ModificandoColumnasDGVPagos()

                Dim queryConta As String = "select idOP,fecEnt,nroConfor  from TOrdenDesembolso where idOP =" & BindingSource0.Item(BindingSource0.Position)(0)
                oDataManager.CargarGrilla(queryConta, CommandType.Text, dgContabilidad, BindingSource2)
                ModificandoColumnaDGVConta()


                Dim _dg As New DataGridView
                Dim queryApro As String = "select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir from VAprobacionesSeguimiento where idOp=" & BindingSource0.Item(BindingSource0.Position)(0)
                oDataManager.CargarGrilla(queryApro, CommandType.Text, _dg, BindingSource3)

                enlazarText()


                'Consultando clasificaciones
                Dim _dg2 As New DataGridView

                Dim queryClasi As String = "select Clasificacion,tipoClasif from vSubClasifiDesembolso where idOp=" & BindingSource0.Item(BindingSource0.Position)(0)
                oDataManager.CargarGrilla(queryClasi, CommandType.Text, _dg2, BindingSource7)


                'filtrando para que muestre los registros de pagos por orden de desembolso seleccionado

                ' BindingSource1.Filter = "codDesembolso=" & BindingSource0.Item(BindingSource0.Position)(0)
                'filtrando para que muestre los registros de contabilidad por orden de desembolso seleccionado
                ' BindingSource2.Filter = "idOP=" & BindingSource0.Item(BindingSource0.Position)(0)
                'BindingSource3.Filter = "idOP=" & BindingSource0.Item(BindingSource0.Position)(0)


                enlazarTextAprobaciones()

                enlazarTextClasificacion()

                'Actualizando la Varible de control
                vCodDesem = BindingSource0.Item(BindingSource0.Position)(0)


            End If

        Else
            ' limpiando los text
            'For i As Integer = 0 To GroupBox2.Controls.Count - 1
            '    If TypeOf GroupBox2.Controls(i) Is TextBox Then
            '        CType(GroupBox2.Controls(i), TextBox).Clear()
            '    End If
            'Next

            'For i As Integer = 0 To TabControl1.TabPages(0).Controls.Count - 1
            '    If TypeOf TabControl1.TabPages(0).Controls(i) Is TextBox Then
            '        CType(TabControl1.TabPages(0).Controls(i), TextBox).Clear()
            '    End If
            'Next

        End If ''



    End Sub

    Private Sub dgDesembolso_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDesembolso.CurrentCellChanged

        Me.Cursor = Cursors.WaitCursor
        Try
            txtOrdCompra.Text = recuperarNroOrdenCompra(CInt(BindingSource0.Item(BindingSource0.Position)(0)))

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub



    Private Sub dgPagos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgPagos.CellClick

        enlazarTextPagos()

    End Sub

    Private Sub dgContabilidad_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContabilidad.CellClick
        enlazarTextConta()

    End Sub



    Private Sub SeguimientoOrdenDesembolsoForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Close()
    End Sub

    Private Sub TabControl1_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting

        If dgDesembolso.RowCount > 0 Then

            'Comprabando si la grilla tiene la primera columna no visible
            If dgPagos.Columns("codDesembolso").Visible Then
                dgPagos.Columns("codDesembolso").Visible = False
            End If
            If dgContabilidad.Columns(0).Visible Then
                dgContabilidad.Columns(0).Visible = False
            End If

        End If
    End Sub


    Private Sub btnCerrar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgContabilidad.Dispose()
        dgDesembolso.Dispose()
        dgPagos.Dispose()
        Me.Close()
    End Sub



    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        MessageBox.Show(BindingSource3.Item(1)(3).ToString())

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SeguimientoOrdenDesembolsoForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

    End Sub

    Private Sub chkObras_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkObras.CheckedChanged

        If chkObras.Checked Then
            cbObra.Visible = False
        Else
            cbObra.Visible = True
        End If


        ' filtrando()

    End Sub

    Private Sub chkProveedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If chkProveedor.Checked Then
        '    cbProveedor.Visible = False
        'Else
        '    cbProveedor.Visible = True
        'End If

        'FiltrarGrillaDesembolso()
        'FiltrandoPorEstado()
        ' filtrando()

    End Sub

    Private Sub cbObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObra.SelectedIndexChanged

        'FiltrarGrillaDesembolso()
        'FiltrandoPorEstado()
        ' filtrando()

    End Sub

    Private Sub cbProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProveedor.SelectedIndexChanged
        'FiltrarGrillaDesembolso()
        'FiltrandoPorEstado()
        ' filtrando()
    End Sub

   

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        vCodDoc = BindingSource0.Item(BindingSource0.Position)(0)
        vParam1 = cambiarNroTotalLetra()
        If String.IsNullOrEmpty(txtOrdCompra.Text) = False Then
            vParam2 = txtOrdCompra.Text.Trim() 'recuperarNroOrdenCompra()
        Else
            vParam2 = ""
        End If

        Dim informe As New ReportViewerOrdenDesembolsoForm
        informe.ShowDialog()
    End Sub


  

    Private Sub btnOrdCompra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdCompra.Click
        If txtOrdCompra.Text.Trim() = "" Then
            MessageBox.Show("Proceso denegado, Orden de Desembolso No tiene relación con Orden de Compra", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        'vCod1 = BindingSource3.Item(BindingSource3.Position)(2) & " - " & BindingSource3.Item(BindingSource3.Position)(3)
        vNroOrden = RecuperarOrdenCompra(BindingSource0.Item(BindingSource0.Position)(0)) ' txtOrdCompra.Text  '(BindingSource3.Item(BindingSource3.Position)(0)) 'idOP 

        Dim jala As New jalarOrdenCompra2Form
        jala.ShowDialog()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub txtObsGerencia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObsGerencia.TextChanged

    End Sub

    Private Sub dgDesembolso_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDesembolso.Sorted
        ColorearGrilla()
    End Sub


    Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        Dim fechaInicio As New SqlParameter("@fechaInicio", SqlDbType.Date)
        fechaInicio.Value = dtpInicio.Value
        Dim fechaFin As New SqlParameter("@fechaFin", SqlDbType.Date)
        fechaFin.Value = dtpFin.Value


        Dim parametros(1) As SqlParameter

        parametros(0) = fechaInicio
        parametros(1) = fechaFin

        'obteniendo la fecha para reportes
        _fechaIni = dtpInicio.Text
        _fechaFin = dtpFin.Text


        Dim consulta As String = "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro, nroConfor, fecEnt, moneda, simbolo, solicitante, ruc, fono, email, codObra, codIde,firmaSolicitante,estado_Gere,estado_Teso,firmaGerencia,firmaTesoreria,estado_Conta,firmaContabilidad from VOrdenDesembolsoSeguimiento_2 " 'where fecDes between @fechaInicio and @fechaFin "



        If rdoObra.Checked = True Then
            consulta += " where fecDes between @fechaInicio and @fechaFin "
            oDataManager.CargarGrilla(consulta, parametros, CommandType.Text, dgDesembolso, BindingSource0)
        End If

        If rdoProveedor.Checked = True Then
            consulta += " WHERE codIde =" & cbProveedor.SelectedValue
            oDataManager.CargarGrilla(consulta, CommandType.Text, dgDesembolso, BindingSource0)
        End If

        If rdoSerie.Checked = True Then
            consulta += " WHERE serie =" & cbSerie.SelectedValue
            oDataManager.CargarGrilla(consulta, CommandType.Text, dgDesembolso, BindingSource0)
        End If

        '"PA_SeguimientoDesembolso"

        BindingNavigator1.BindingSource = BindingSource0

        ModificandoColumnasDGV()

        'Dando Color a la Grilla
        ColorearGrilla()


        'filtrando 
        filtrando()

        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnImprimirGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirGrilla.Click
        If dgDesembolso.RowCount = 0 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        'creando variables para parametros
        Dim parameters As New ParameterFields

        Dim pFechaIni As New ParameterField
        Dim pFechaFin As New ParameterField

        Dim valorFechaIni As New ParameterDiscreteValue
        Dim valorFechaFin As New ParameterDiscreteValue

        'definiendo los nombres de la variables para el reporte

        pFechaIni.Name = "pFechaFin"
        pFechaFin.Name = "pFechaIni"

        '----------
        'Definiendo los nombres de los parametros.
        valorFechaIni.Value = _fechaIni
        valorFechaFin.Value = _fechaFin

        pFechaIni.CurrentValues.Add(valorFechaIni)
        pFechaFin.CurrentValues.Add(valorFechaFin)

        parameters.Add(pFechaIni)
        parameters.Add(pFechaFin)



        Dim datos As DataSetInformesCr = CargarDatos()

        Dim frm As New ReportViewerSeguimientoDesem(datos)

        frm.CrystalReportViewer1.ParameterFieldInfo = parameters

        frm.WindowState = FormWindowState.Maximized
        frm.ShowDialog()

    End Sub
    ''' <summary>
    ''' Carga los datos de la Grilla a un DataTable de DataSetInformesCr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarDatos() As DataSetInformesCr
        Dim ds As New DataSetInformesCr

        For Each row As DataGridViewRow In dgDesembolso.Rows
            Dim rowInf As DataSetInformesCr.DatosSeguimientoDesemRow = ds.DatosSeguimientoDesem.NewDatosSeguimientoDesemRow
            rowInf.fecDes = CDate(row.Cells("fecDes").Value)
            rowInf.nroDes = CStr(row.Cells("nroDes").Value)
            rowInf.estado = CStr(row.Cells("estado_desembolso").Value)
            rowInf.simbolo = CStr(row.Cells("simbolo").Value)
            rowInf.monto = CDec(row.Cells("monto").Value)
            rowInf.detraccion = CDec(row.Cells("montoDet").Value)
            rowInf.proveedor = CStr(row.Cells("proveedor").Value)
            rowInf.obra = CStr(row.Cells("obra").Value)
            rowInf.solicitante = CStr(row.Cells("solicitante").Value)
            rowInf.serie = CStr(row.Cells("serie").Value)
            rowInf.motivo = CStr(row.Cells("datoReq").Value)

            rowInf.estadoGerencia = CStr(row.Cells("estado_Gere").Value)
            rowInf.estadoTesoreria = CStr(row.Cells("estado_Teso").Value)
            rowInf.estadoContabilidad = CStr(row.Cells("estado_Conta").Value)


            'ingresando los valroes de estado
            'Dim _dg As New DataGridView
            'Dim queryApro As String = "select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir from VAprobacionesSeguimiento where idOp=" & row.Cells("idOP").Value 'BindingSource0.Item(BindingSource0.Position)(0)
            'oDataManager.CargarGrilla(queryApro, CommandType.Text, _dg, BindingSource3)

            ''Aprobación Gerencia
            'If BindingSource3.Count > 1 Then
            '    rowInf.estadoGerencia = BindingSource3.Item(1)(4)
            'Else
            '    rowInf.estadoGerencia = ""
            'End If

            'If BindingSource3.Count > 2 Then
            '    'Aprobación Tesoreria
            '    rowInf.estadoTesoreria = BindingSource3.Item(2)(4)
            'Else
            '    rowInf.estadoTesoreria = ""
            'End If

            'If BindingSource3.Count > 3 Then
            '    'Aprobación Contabilidad
            '    rowInf.estadoContabilidad = BindingSource3.Item(3)(4)
            'Else
            '    rowInf.estadoContabilidad = ""
            'End If

            ds.DatosSeguimientoDesem.AddDatosSeguimientoDesemRow(rowInf)

        Next

        Return ds

    End Function

    
    Private Sub rdoObra_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoObra.CheckedChanged, rdoSerie.CheckedChanged, rdoProveedor.CheckedChanged
        'configurando opciones a filtrar
        If rdoObra.Checked = True Then
            'cbObra.Visible = True

            chkObras.Visible = True
            lblDesde.Visible = True
            lblHasta.Visible = True
            lblObra.Visible = True

            dtpInicio.Visible = True
            dtpFin.Visible = True

            'Poniendo los otros controles invisibles
            'controles Proveedor invisible
            lblProveedor.Visible = False
            'chkProveedor.Checked = True
            cbProveedor.Visible = False

            'controles Serie invisible
            lblSerie.Visible = False
            cbSerie.Visible = False


        End If

        If rdoProveedor.Checked = True Then

            lblProveedor.Visible = True
            cbProveedor.Visible = True

            'Controles Obras invisible
            chkObras.Checked = True
            chkObras.Visible = False
            lblDesde.Visible = False
            lblHasta.Visible = False
            lblObra.Visible = False

            dtpInicio.Visible = False
            dtpFin.Visible = False

            'controles Serie invisible
            lblSerie.Visible = False
            cbSerie.Visible = False

        End If

        If rdoSerie.Checked = True Then
            'controles Serie invisible
            lblSerie.Visible = True
            cbSerie.Visible = True

            '
            lblProveedor.Visible = False
            cbProveedor.Visible = False
            'Controles Obras invisible
            chkObras.Checked = True
            chkObras.Visible = False
            lblDesde.Visible = False
            lblHasta.Visible = False
            lblObra.Visible = False

            dtpInicio.Visible = False
            dtpFin.Visible = False
        End If

        'limpiando grilla
        dgDesembolso.DataSource = ""
        dgPagos.DataSource = ""
        dgContabilidad.DataSource = ""

    End Sub
End Class
