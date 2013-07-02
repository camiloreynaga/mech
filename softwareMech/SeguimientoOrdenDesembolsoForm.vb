Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoOrdenDesembolsoForm

#Region "Variables"
    Dim BindingSource0 As New BindingSource
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
        Dim sele As String = "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,moneda,simbolo,nombre,apellido,ruc,fono,email from VOrdenDesembolsoSeguimiento"

        crearDataAdapterTable(daVDetOrden, sele)


        Try
            crearDSAlmacen()
            daVDetOrden.Fill(dsAlmacen, "VDesembolsoSeguimiento")

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VDesembolsoSeguimiento"
            dgDesembolso.DataSource = BindingSource0



        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Customisa la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV()

        Dim oGrilla As New cConfigFormControls
        oGrilla.ConfigGrilla(dgDesembolso)
        'dgDesembolso.
        With dgDesembolso
            'columnas no visibles
            .Columns("idOP").Visible = False
            .Columns("serie").HeaderText = ""
            .Columns("serie").DisplayIndex = 0

            'NroDesembolso
            .Columns("nroDes").HeaderText = "Nro"
            .Columns("nroDes").DisplayIndex = 1
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
            'Monto de Detracción
            .Columns("montoDet").HeaderText = "Detracción"
            'Monto Diferencia 
            .Columns("montoDif").Visible = False
            'Obra 
            .Columns("obra").HeaderText = "Obra/Lugar"
            'Proveedor
            .Columns("proveedor").HeaderText = "Proveedor"
            'Forma de pago negociada
            .Columns("banco").HeaderText = "Forma de Pago"
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

        End With





    End Sub

    ''' <summary>
    ''' Enlaza los datos de la grilla con los controles del form
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
            txtSolicitante.Text = BindingSource0.Item(BindingSource0.Position)(28) + " " + BindingSource0.Item(BindingSource0.Position)(29)
            txtMonto.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(7).ToString
            txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(27).ToString + " " + BindingSource0.Item(BindingSource0.Position)(8).ToString
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

        End If
    End Sub


#End Region




    Private Sub SeguimientoOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DatosIniciales()
        ModificandoColumnasDGV()
        enlazarText()


    End Sub


    Private Sub txtObra_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObra.TextChanged

    End Sub

    Private Sub dgDesembolso_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDesembolso.CellClick
        enlazarText()
    End Sub
End Class
