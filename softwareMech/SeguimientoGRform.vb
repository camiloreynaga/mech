Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoGRform

#Region "Variables"

    ''' <summary>
    ''' Guia Remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' Detalle Guia Remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    Dim BindingSource2 As New BindingSource

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
        Dim sele As String '= "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,moneda,simbolo,solicitante,ruc,fono,email,codObra,codIde from VOrdenDesembolsoSeguimiento"
        sele = "select  codGuiaE, talon, nroGuia, fecIni, codSerS,razon,  codIde, codestado, Estado,Origen,Destino,codUbiOri,codUbiDes,partida, llegada, codET, empTrans,marcaNro,codVeh,nombre,  codT, motivo,   codMotG, nroFact, obs, Personal,  codPers from VSeguimientoGR"
        crearDataAdapterTable(daTabla1, sele)


        'sele = "PA_SeguimientoComprobantes"
        sele = "select codDGE, Obra,codigo,cant,descrip,unidad,peso,codGuiaE,codMat,linea1 FROM VSeguimientoGRDetalle"
        crearDataAdapterTable(daTabla2, sele)
        'crearDataAdapterTableProcedure(daTabla2, sele)


        'sele = "PA_LugarTrabajo" '"Select codigo,nombre from tLugarTrabajo"
        'crearDataAdapterTable(daTabla4, sele)
        'crearDataAdapterTableProcedure(daTabla4, sele)

        'sele = "PA_Proveedores"
        '"Select codIde,razon from TIdentidad where idTipId=2"
        'crearDataAdapterTable(daTabla5, sele)
        'crearDataAdapterTableProcedure(daTabla5, sele)
        'daTabla1.SelectCommand.Parameters.Add("@idDesembolso", SqlDbType.Int).Value = 0

        'sele = "select (nombre +' '+ apellido) as solicitante from Tpersonal where codPers > 1"
        'crearDataAdapterTable(daTabla6, sele)

        Try
            crearDSAlmacen()
            daTabla1.Fill(dsAlmacen, "VSeguimientoGR")
            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VSeguimientoGR"
            dgGR.DataSource = BindingSource0
            BindingNavigator1.BindingSource = BindingSource0

            daTabla2.Fill(dsAlmacen, "VSeguimientoGRDetalle")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSeguimientoGRDetalle"
            dgDetalleGR.DataSource = BindingSource1 ' 


            'daTabla4.Fill(dsAlmacen, "TLugarTrabajo")
            'BindingSource4.DataSource = dsAlmacen
            'BindingSource4.DataMember = "TLugarTrabajo"
            'cbObra.DataSource = BindingSource4
            'cbObra.DisplayMember = "nombre"
            'cbObra.ValueMember = "codigo"


            'daTabla5.Fill(dsAlmacen, "TIdentidad")
            'BindingSource5.DataSource = dsAlmacen
            'BindingSource5.DataMember = "TIdentidad"
            '' BindingSource5.Filter = "idTipId=2"
            'BindingSource5.Sort = "razon asc"
            'cbProveedor.DataSource = BindingSource5
            'cbProveedor.DisplayMember = "razon"
            'cbProveedor.ValueMember = "codIde"

            'daTabla6.Fill(dsAlmacen, "TSolicitante")
            'BindignSource6.DataSource = dsAlmacen
            'BindignSource6.DataMember = "TSolicitante"
            'BindignSource6.Sort = "solicitante ASC"
            'cbSolicitante.ComboBox.DataSource = BindignSource6
            'cbSolicitante.ComboBox.DisplayMember = "solicitante"
            'cbSolicitante.ComboBox.ValueMember = "solicitante"

        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Customiza la grilla  GR
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV_GR()
        oGrilla.ConfigGrilla(dgGR)
        dgGR.ReadOnly = True
        dgGR.AllowUserToAddRows = False
        dgGR.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try
            With dgGR
                'codigo GR
                .Columns("codGuiaE").Visible = False
                'nro talon
                .Columns("talon").HeaderText = "Talon"
                .Columns("talon").Width = 50
                'nro Guia
                .Columns("nroGuia").HeaderText = "N°"
                .Columns("nroGuia").Width = 50
                'fecha de Inicio 
                .Columns("fecIni").HeaderText = "Fecha"
                .Columns("fecIni").Width = 80
                'Codigo de Serie
                .Columns("codSerS").Visible = False
                'razon
                .Columns("razon").HeaderText = "Destinatario"
                .Columns("razon").Width = 170
                'Banco 
                .Columns("codIde").Visible = False

                .Columns("Estado").HeaderText = "Estado"
                .Columns("Estado").Width = 80

                .Columns("codestado").Visible = False

                'Numero de Cuenta usada
                .Columns("Origen").HeaderText = "Origen"
                .Columns("Origen").Width = 160

                .Columns("Destino").HeaderText = "Destino"
                .Columns("Destino").Width = 160

                'Descripcion del pago
                .Columns("codUbiOri").Visible = False
                .Columns("codUbiDes").Visible = False
                '                .Columns("pagoDet").HeaderText = "Descripción"
                '.Columns("pagoDet").DisplayIndex = 6
                '.Columns("pagoDet").Width = 250

                'Monto de detracción

                .Columns("partida").HeaderText = "Partida"
                .Columns("partida").Width = 150
                .Columns("llegada").DefaultCellStyle.Format = "Llegada"
                .Columns("llegada").Width = 150

                'numero Operacion /cheque
                .Columns("codET").Visible = False

                .Columns("empTrans").HeaderText = "Emp. Transp."
                .Columns("empTrans").Width = 150

                .Columns("marcaNro").HeaderText = "Vehiculo"
                .Columns("marcaNro").Width = 100
                .Columns("codVeh").Visible = False

                .Columns("nombre").HeaderText = "Chofer"
                .Columns("nombre").Width = 120
                'Clasificación de pagos 
                .Columns("codT").Visible = False

                .Columns("motivo").HeaderText = "Motivo"
                .Columns("motivo").Width = 120

                .Columns("codMotG").Visible = False

                .Columns("nroFact").HeaderText = "Factura"
                .Columns("nrofact").Width = 50

                .Columns("obs").HeaderText = "Observaciones"
                .Columns("obs").Width = 100
                .Columns("Personal").HeaderText = "Personal"
                .Columns("Personal").Width = 120
                .Columns("codPers").Visible = False

                '  .Columns("clasif").HeaderText = "Clasificación"
                '.Columns("clasif").Width = 100

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    ''' <summary>
    ''' Customiza la grilla  Detalle GR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV_DetalleGR()
        oGrilla.ConfigGrilla(dgDetalleGR)
        dgDetalleGR.ReadOnly = True
        dgDetalleGR.AllowUserToAddRows = False
        dgDetalleGR.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try
            With dgDetalleGR
                'codigo GR
                .Columns("codDGE").Visible = False
                'nro talon
                .Columns("Obra").Visible = False
                .Columns("codigo").Visible = False

                'nro Guia
                .Columns("cant").HeaderText = "Cantidad"
                .Columns("cant").Width = 50
                'fecha de Inicio 
                .Columns("descrip").HeaderText = "Decripción"
                .Columns("descrip").Width = 250
                'Codigo de Serie
                .Columns("unidad").HeaderText = "Und"
                .Columns("unidad").Width = 60
                'razon
                .Columns("peso").HeaderText = "Peso"
                .Columns("peso").Width = 50
                'Banco 
                .Columns("codGuiaE").Visible = False

                .Columns("codMat").Visible = False

                .Columns("linea1").HeaderText = "Obs"
                .Columns("linea1").Width = 100

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    ''' <summary>
    ''' enlaza los datos de la grilla con los de form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextGR()
        If dgGR.RowCount = 0 Then
            Exit Sub

        Else
            txtPartida.Text = BindingSource0.Item(BindingSource0.Position)(13)
            txtLlegada.Text = BindingSource0.Item(BindingSource0.Position)(14)
            txtMotivo.Text = BindingSource0.Item(BindingSource0.Position)(21)
            txtEstado.Text = BindingSource0.Item(BindingSource0.Position)(8)
        End If
    End Sub

    Private Sub enlazarTextDetalleGR()

        txtDenominacion.Text = BindingSource0.Item(BindingSource0.Position)(5)
        ' txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(27)

        txtChofer.Text = BindingSource0.Item(BindingSource0.Position)(19)
        txtVehiculo.Text = BindingSource0.Item(BindingSource0.Position)(17)
        txtObs.Text = BindingSource0.Item(BindingSource0.Position)(24)


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

            If TypeOf Me.Controls(i) Is TextBox Then 'TEXTBOX
                CType(Me.Controls(i), TextBox).ReadOnly = True
            End If

            If TypeOf Me.Controls(i) Is GroupBox Then 'TEXTBOX
                For c As Integer = 0 To Me.Controls(i).Controls.Count - 1

                    oGrilla.configurarColorControl("Label", Me.Controls(i), ForeColorLabel)

                    If TypeOf Me.Controls(i).Controls(c) Is TextBox Then 'TEXTBOX
                        CType(Me.Controls(i).Controls(c), TextBox).ReadOnly = True
                    End If


                Next
            End If
        Next

    End Sub

#End Region

#Region "Eventos"

#End Region

    Private Sub SeguimientoGRform_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

        Me.Close()

    End Sub

    Private Sub SeguimientoGRform_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DatosIniciales()
        ModificandoColumnasDGV_GR()
        ModificandoColumnasDGV_DetalleGR()
        configurarColorControl()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub


    Private Sub dgGR_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgGR.CellClick, dgGR.CellEnter
        enlazarTextGR()

        'filtrar los datos de detalle
    End Sub
End Class
