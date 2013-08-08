Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoGRform

#Region "Variables"

    ''' <summary>
    ''' Guia Remision
    ''' </summary>
    ''' <remarks>Guia de Remision</remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' Detalle Guia Remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Obra
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource
    ''' <summary>
    ''' Almacen
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource3 As New BindingSource

    ''' <summary>
    ''' Nro Serie GR
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource4 As New BindingSource

    Dim oGrilla As New cConfigFormControls

    Dim countEntradas As Integer = 0
    Dim consultaBd As Boolean = False
    Dim VPosicionGrilla As Integer = 0
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
        sele = "select  codGuiaE, talon, nroGuia, fecIni, codSerS,razon,  codIde, codestado, Estado,Origen,Destino,codUbiOri,codUbiDes,partida, llegada, codET, empTrans,marcaNro,codVeh,nombre,  codT, motivo,   codMotG, nroFact, obs, Personal,  codPers,ruc from VSeguimientoGR"
        crearDataAdapterTable(daTabla1, sele)

        'sele = "PA_SeguimientoComprobantes"
        sele = "PA_LugarTrabajo" '"Select codigo,nombre from tLugarTrabajo"
        crearDataAdapterTableProcedure(daTabla3, sele)

        sele = "select codSerS,serie from VSeguimientoGRSerie "
        crearDataAdapterTable(daTabla5, sele)

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

            daTabla3.Fill(dsAlmacen, "TLugarTrabajo")
            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TLugarTrabajo"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            daTabla5.Fill(dsAlmacen, "VSeguimientoGRSerie")
            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VSeguimientoGRSerie"
            cbSerie.DataSource = BindingSource4
            cbSerie.DisplayMember = "serie"
            cbSerie.ValueMember = "codSerS"

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
    ''' Consulta Datos del Detalle de GR desde la BD
    ''' </summary>
    ''' <param name="cod"> codigo de obra  para filtrar </param>
    ''' <remarks></remarks>
    Private Sub DatosAlmacenGR(ByVal cod As String)

        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        wait.Show()

        'Validando la cantidad de datos de GR 
        ' BindingSource2.Item(BindingSource0.Position)(0)

        Try

            Dim sele As String = "select codUbi,ubicacion,codigo from TUbicacion where codigo ='" & cod & "'"
            crearDataAdapterTable(daTabla4, sele)

            If cbAlmacen.Items.Count > 0 Then
                dsAlmacen.Tables("TUbicacion").Clear()
                'daTPers.Fill(dsAlmacen, "VSeguimientoGRDetalle")
            End If

            daTabla4.Fill(dsAlmacen, "TUbicacion")
            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TUbicacion"
            cbAlmacen.DataSource = BindingSource3
            cbAlmacen.DisplayMember = "ubicacion"
            cbAlmacen.ValueMember = "codUbi"

        Catch f As Exception

            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    ''' <summary>
    ''' Consulta Datos del Detalle de GR desde la BD
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosDetalleGR()
        VerificaConexion()

        'Validando la cantidad de datos de GR 
        If BindingSource0.Count > 0 Then


            Dim wait As New waitForm
            wait.Show()
            Me.Cursor = Cursors.WaitCursor
            wait.Show()

            Dim cod As Integer = BindingSource0.Item(BindingSource0.Position)(0)

            Try
                If dgGR.RowCount > 0 Then 'BindingSource0.Count > 0 Then
                    Dim sele As String = "select codDGE, codigo,cant,descrip,unidad,peso,codGuiaE,codMat,linea1,entregado,personal,recibido,obsR FROM VSeguimientoGRDetalle where codGuiaE=" & cod
                    crearDataAdapterTable(daTabla2, sele)
                Else
                    Exit Sub
                End If

                If dgDetalleGR.RowCount > 0 Then
                    dsAlmacen.Tables("VSeguimientoGRDetalle").Clear()
                    'daTPers.Fill(dsAlmacen, "VSeguimientoGRDetalle")
                End If

                daTabla2.Fill(dsAlmacen, "VSeguimientoGRDetalle")
                BindingSource1.DataSource = dsAlmacen
                BindingSource1.DataMember = "VSeguimientoGRDetalle"
                dgDetalleGR.DataSource = BindingSource1 ' 
                BindingNavigator2.BindingSource = BindingSource1

            Catch f As Exception

                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            Finally
                wait.Close()
                Me.Cursor = Cursors.Default
            End Try
        Else

            If dgDetalleGR.RowCount > 0 Then
                dsAlmacen.Tables("VSeguimientoGRDetalle").Clear()
                'daTPers.Fill(dsAlmacen, "VSeguimientoGRDetalle")
            End If


        End If
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
                .Columns("talon").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'nro Guia
                .Columns("nroGuia").HeaderText = "N°"
                .Columns("nroGuia").Width = 50
                .Columns("nroGuia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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
                .Columns("ruc").Visible = False

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
                .Columns("codigo").Visible = False

                'nro Guia
                .Columns("cant").HeaderText = "Cantidad"
                .Columns("cant").Width = 60
                .Columns("cant").DefaultCellStyle.Format = "N2"
                .Columns("cant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'fecha de Inicio 
                .Columns("descrip").HeaderText = "Decripción"
                .Columns("descrip").Width = 300
                'Codigo de Serie
                .Columns("unidad").HeaderText = "Und"
                .Columns("unidad").Width = 60
                'razon
                .Columns("peso").HeaderText = "Peso"
                .Columns("peso").Width = 60
                .Columns("peso").DefaultCellStyle.Format = "N2"
                .Columns("peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Banco 
                .Columns("codGuiaE").Visible = False

                .Columns("codMat").Visible = False

                .Columns("linea1").HeaderText = "Nota"
                .Columns("linea1").Width = 150
                .Columns("entregado").HeaderText = "Entregado"
                .Columns("entregado").Width = 70
                .Columns("personal").HeaderText = "Personal"
                .Columns("personal").Width = 150
                .Columns("recibido").HeaderText = "Recibido"
                .Columns("recibido").Width = 70
                .Columns("obsR").HeaderText = "Obs."
                .Columns("obsR").Width = 120
                'entregado,personal,recibido,obsR

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

            Dim aCriterios As String() = {"PENDIENTE", "CERRADO", "ANULADO"}
            Dim aBackColors As Color() = {Color.White, Color.Green, Color.Red}
            Dim aForeColors As Color() = {Color.Black, Color.White, Color.White}

            'If txtEstado.Text.Trim() = "PENDIENTE" Then
            'End If

            txtPartida.Text = BindingSource0.Item(BindingSource0.Position)(13)
            txtLlegada.Text = BindingSource0.Item(BindingSource0.Position)(14)
            txtMotivo.Text = BindingSource0.Item(BindingSource0.Position)(21)
            txtEstado.Text = BindingSource0.Item(BindingSource0.Position)(8)

            'Dandole Color al Textbox 
            colorTextBox(txtEstado, aCriterios, aBackColors, aForeColors)

        End If
    End Sub

    Private Sub enlazarTextDetalleGR()

        If dgGR.RowCount = 0 Then
            Exit Sub
        Else
            ' If BindingSource0.Count > 0 Then


            txtDenominacion.Text = BindingSource0.Item(BindingSource0.Position)(5)
            txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(27)

            txtChofer.Text = BindingSource0.Item(BindingSource0.Position)(19)
            txtVehiculo.Text = BindingSource0.Item(BindingSource0.Position)(17)
            txtObs.Text = BindingSource0.Item(BindingSource0.Position)(24)

        End If


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
    ''' Pinta la grila de GR y Detalle GR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ColorearGrilla()

        oGrilla.colorearFilasDGV(dgGR, "Estado", "CERRADO", Color.Green, Color.White)
        ' oGrilla.colorearFilasDGV(dgDesembolso, "estado_desembolso", "PENDIENTE", Color.Yellow, Color.Red)

        oGrilla.colorearFilasDGV(dgGR, "Estado", "ANULADO", Color.Red, Color.White)

        oGrilla.colorearFilasDGV(dgDetalleGR, "entregado", "ENTREGADO", Color.Green, Color.White)
        oGrilla.colorearFilasDGV(dgDetalleGR, "recibido", "RECIBIDO", Color.Green, Color.White)

        oGrilla.colorearFilasDGV(dgDetalleGR, "recibido", "INCOMPLETO", Color.Yellow, Color.Red)

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
        'If BindingSource4.Position >= 0 And BindingSource5.Position >= 0 Then


        BindingSource0.Filter = ""
        Dim pFiltro As String = BindingSource0.Filter
        Dim pCriterio As String

        If chkDestino.Checked = False Then

            'Dim vAlmacen As Integer = cbAlmacen.SelectedValue
            'If String.IsNullOrEmpty(cbAlmacen.SelectedValue) Then
            '    cbAlmacen.SelectedIndex = 0
            'End If
            pCriterio = "codUbiDes=" & cbAlmacen.SelectedValue
            pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
        End If

        If chkSerie.Checked = False Then
            pCriterio = "codSerS=" & cbSerie.SelectedValue
            pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
        End If

        'If cbSerieGR.Text = "TODOS" Or cbSerieGR.Text = "" Then
        '    ' AddCriterioFiltro(pCriterio, pFiltro)

        'Else
        '    pCriterio = "estado_desembolso='" & cbSerieGR.Text.Trim() & "'"
        '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
        'End If

        'If chkSolicitante.Checked = False Then
        '    pCriterio = "solicitante = '" & cbSolicitante.Text.Trim() & "'"
        '    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
        'End If

        BindingSource0.Filter = pFiltro

        'BindingSource0.Sort = "codGuiaE Desc"
        'End If
        'Colorea la Grilla
        ColorearGrilla()
        'LLenandoDetalleGR()

        DatosDetalleGR()

        '
    End Sub


    Private Sub LLenandoDetalleGR()
        'cambia la posición de la variable Posicion para iniciar de nuevo el proceso de llenado de datos de 
        'detalle
        'If chkDestino.Checked = False Then


        'End If


        ' Valida si cambio la fila seleccionada
        If VPosicionGrilla <> BindingSource0.Position Then
            VPosicionGrilla = BindingSource0.Position
            consultaBd = False
        End If


        'Verifica si ya se consulto a la BD
        If consultaBd = False Then
            DatosDetalleGR()
            consultaBd = True
        End If
        enlazarTextGR()
        enlazarTextDetalleGR()
    End Sub

#End Region

#Region "Eventos"

    Private Sub SeguimientoGRform_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

        Me.Close()

    End Sub

    Private Sub SeguimientoGRform_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DatosIniciales()
        'DatosDetalleGR()
        ModificandoColumnasDGV_GR()
        If BindingSource1.Count > 0 Then
            ModificandoColumnasDGV_DetalleGR()
        End If

        configurarColorControl()
    End Sub

    Private Sub dgGR_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgGR.CellClick, dgGR.CellEnter
        LLenandoDetalleGR()
    End Sub

#End Region

    Private Sub SeguimientoGRform_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ColorearGrilla()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub chkDestino_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDestino.CheckedChanged

        If chkDestino.Checked Then
            cbObra.Visible = False
            cbAlmacen.Visible = False
            lblAlmacen.Visible = False
        Else
            cbObra.Visible = True
            cbAlmacen.Visible = True
            lblAlmacen.Visible = True

            'cambia el estado de bandera de consulta de detalle de guia
            '
            'consultaBd = False

        End If

        filtrando()

    End Sub

    Private Sub cbObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObra.SelectedIndexChanged

        Try
            If BindingSource2.Position >= 0 Then
                If cbObra.SelectedValue.ToString() = "System.Data.DataRowView" Then
                    DatosAlmacenGR(BindingSource2.Item(BindingSource2.Position)(0))
                Else
                    DatosAlmacenGR(cbObra.SelectedValue)
                End If
            End If
        Catch ex As Exception
        End Try


        filtrando()

    End Sub


    Private Sub cbAlmacen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAlmacen.SelectedIndexChanged
        If BindingSource3.Count > 0 Then
            filtrando()
        End If
    End Sub

    Private Sub dgGR_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgGR.DataBindingComplete

        ' LLenandoDetalleGR()
        ' MsgBox("cambio data")
    End Sub

    Private Sub chkSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSerie.CheckedChanged
        If chkSerie.Checked Then
            cbSerie.Visible = False
        Else
            cbSerie.Visible = True

            'cambia el estado de bandera de consulta de detalle de guia
            '
            'consultaBd = False

        End If

        filtrando()
    End Sub

    Private Sub cbSerie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSerie.SelectedIndexChanged
        'Try
        '    If BindingSource4.Position >= 0 Then
        '        'If cbSerie.SelectedValue.ToString() = "System.Data.DataRowView" Then
        '        '    ' DatosAlmacenGR(BindingSource4.Item(BindingSource4.Position)(0))
        '        'Else
        '        '    DatosAlmacenGR(cbObra.SelectedValue)
        '        'End If
        '    End If
        'Catch ex As Exception
        'End Try


        filtrando()
    End Sub

   
End Class
