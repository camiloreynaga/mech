Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class MantSolicitudCajaForm

#Region "Variables"

    ''' <summary>
    ''' Encabezado Solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' Detalle Solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Insumos
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' insertar solicitud caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertTable As SqlCommand

    Dim cmInsertTableDetalle As SqlCommand
    ''' <summary>
    ''' actualizar solicitud caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUdpateTable As SqlCommand

    Dim cmUpdateTableDetalle As SqlCommand


    Dim cmDeleteTable As SqlCommand

    Dim cmDeleteTableDetalle As SqlCommand


    ''' <summary>
    ''' variable para codigo caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Dim vCajaChica As Integer

    ''' <summary>
    ''' Administracion de data con BD
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' objeto de la clase cConfigControl
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' Lista de Indices de filas para actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listaUpdate As New List(Of Integer)

#End Region

#Region "Métodos"

    Private Function CommandData(ByVal consulta As String, ByVal conexion As SqlConnection, ByVal type As CommandType) As SqlCommand
        Dim cmd As New SqlCommand(consulta, conexion)
        cmd.CommandType = type
        'cmd.Parameters.AddRange()
        Return cmd
    End Function
    ''' <summary>
    ''' comando Insertar Solicitud Caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsert(ByVal _idCaja As Integer)
        cmInsertTable = New SqlCommand()
        cmInsertTable.CommandType = CommandType.Text
        cmInsertTable.CommandText = "insert into TSolicitudCaja values (@nroSol,@fechaSol,@codPers,@codCC,0,0,0)"
        cmInsertTable.Connection = Cn
        cmInsertTable.Parameters.Add("@nroSol", SqlDbType.Int).Value = txtNro.Text.Trim()
        cmInsertTable.Parameters.Add("@fechaSol", SqlDbType.Date).Value = dtpFecha.Value.Date
        cmInsertTable.Parameters.Add("@codPers", SqlDbType.Int).Value = vPass 'obteniendo el codigo de usuario
        cmInsertTable.Parameters.Add("@codCC", SqlDbType.Int).Value = _idCaja
    End Sub
    ''' <summary>
    ''' comando Actualizar solicitud caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate()
        cmUdpateTable = New SqlCommand()
        cmUdpateTable.CommandType = CommandType.Text
        cmUdpateTable.CommandText = "Update TSolicitudCaja set fechaSol=@fechaSol,montoSol=@montoSol where codSC=@cod"
        cmUdpateTable.Connection = Cn
        cmUdpateTable.Parameters.Add("@fechaSol", SqlDbType.Date).Value = dtpFecha.Value.Date
        cmUdpateTable.Parameters.Add("@montoSol", SqlDbType.Decimal).Value = txtTotal.Text.Trim()
        cmUdpateTable.Parameters.Add("@cod", SqlDbType.Decimal).Value = BindingSource0.Item(BindingSource0.Position)(0)

        'obteniendo el codigo de usuario
    End Sub

    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand()
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "Delete TSolicitudCaja where codSC=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int).Value = BindingSource0.Item(BindingSource0.Position)(0)
    End Sub

    ''' <summary>
    ''' comando para insert detalle de solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertDetalle()
        cmInsertTableDetalle = New SqlCommand
        cmInsertTableDetalle.CommandType = CommandType.StoredProcedure
        'cmInsertTableDetalle.CommandText = "insert into DetSolCaja(cant1,insumo,prec1,uniMed,obsSol,estDet,codMat,codAreaM,codSc) Values(@cant1,0,@insumo,@prec1,0.00,@uniMed,@obsSol,@estDet,@codMat)"
        cmInsertTableDetalle.CommandText = "PA_InsertDetalleSolCaja"
        cmInsertTableDetalle.Connection = Cn
        cmInsertTableDetalle.Parameters.Add("@cant1", SqlDbType.Decimal, 0).Value = txtCan.Text.Trim
        cmInsertTableDetalle.Parameters.Add("@cant2", SqlDbType.Decimal, 0).Value = 0.0
        cmInsertTableDetalle.Parameters.Add("@insumo", SqlDbType.VarChar, 100).Value = BindingSource2.Item(BindingSource2.Position)(1)
        cmInsertTableDetalle.Parameters.Add("@prec1", SqlDbType.Decimal, 0).Value = txtPrecio.Text.Trim()
        cmInsertTableDetalle.Parameters.Add("@prec2", SqlDbType.Decimal, 0).Value = 0.0
        cmInsertTableDetalle.Parameters.Add("@uniMed", SqlDbType.VarChar, 20).Value = BindingSource2.Item(BindingSource2.Position)(2)
        cmInsertTableDetalle.Parameters.Add("@obsSol", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmInsertTableDetalle.Parameters.Add("@codApro", SqlDbType.Int, 0).Value = 0 'sin verificar
        cmInsertTableDetalle.Parameters.Add("@estDet", SqlDbType.Int, 0).Value = 0
        cmInsertTableDetalle.Parameters.Add("@obsApro", SqlDbType.VarChar, 200).Value = ""
        cmInsertTableDetalle.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
        cmInsertTableDetalle.Parameters.Add("@codAreaM", SqlDbType.Int, 0).Value = cbArea.SelectedValue
        cmInsertTableDetalle.Parameters.Add("@codSC", SqlDbType.Int, 0).Value = BindingSource0.Item(BindingSource0.Position)(0)
        cmInsertTableDetalle.Parameters.Add("@estRen", SqlDbType.Int, 0).Value = 0
        cmInsertTableDetalle.Parameters.Add("@codRen", SqlDbType.Int, 0).Value = 0
        cmInsertTableDetalle.Parameters.Add("@nroDocRen", SqlDbType.VarChar, 30).Value = ""

        cmInsertTableDetalle.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInsertTableDetalle.Parameters("@Identity").Direction = ParameterDirection.Output

    End Sub
    ''' <summary>
    ''' comando update Detalle Solicitud
    ''' </summary>
    ''' <param name="_cantidad"></param>
    ''' <param name="_precio"></param>
    ''' <param name="_obs"></param>
    ''' <param name="_cod"></param>
    ''' <remarks></remarks>
    Private Sub comandoUpdateDetalle(ByVal _cantidad As Decimal, ByVal _precio As Decimal, ByVal _obs As String, ByVal _cod As Integer)
        cmUpdateTableDetalle = New SqlCommand
        cmUpdateTableDetalle.CommandType = CommandType.Text
        cmUpdateTableDetalle.CommandText = "Update DetSolCaja Set cant1=@cant1,prec1=@prec1,obsSol=@obsSol Where codDetSol=@cod"
        cmUpdateTableDetalle.Connection = Cn
        cmUpdateTableDetalle.Parameters.Add("@cant1", SqlDbType.Decimal, 0).Value = _cantidad
        cmUpdateTableDetalle.Parameters.Add("@prec1", SqlDbType.Decimal, 0).Value = _precio
        cmUpdateTableDetalle.Parameters.Add("@obsSol", SqlDbType.VarChar, 200).Value = _obs
        cmUpdateTableDetalle.Parameters.Add("@cod", SqlDbType.Int, 0).Value = _cod

    End Sub

    Private Sub comandoDeleteDetalle(ByVal cod As Integer)

        cmDeleteTableDetalle = New SqlCommand
        cmDeleteTableDetalle.CommandType = CommandType.Text
        cmDeleteTableDetalle.CommandText = "Delete DetSolCaja where codDetSol=@cod"
        cmDeleteTableDetalle.Connection = Cn
        cmDeleteTableDetalle.Parameters.Add("@cod", SqlDbType.Int, 0).Value = cod

    End Sub

    ''' <summary>
    ''' asiga una formato de ceros al número de solicitud
    ''' </summary>
    ''' <param name="max"></param>
    ''' <remarks></remarks>
    Private Sub asignarNro(ByVal max As Integer)
        Select Case CInt(max)
            Case Is < 99
                txtNro.Text = "000" & max
            Case 100 To 999
                txtNro.Text = "00" & max
            Case 1000 To 9999
                txtNro.Text = "0" & max
            Case Is > 9999
                txtNro.Text = max
        End Select
    End Sub

    ''' <summary>
    ''' recupera el ultimo número de solicitud
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <remarks></remarks>
    Private Sub recuperarUltimoNro(ByVal cod As Integer)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroSol),0)+1 from TSolicitudCaja where codCC=" & cod
        cmdMaxCodigo.Connection = Cn
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

    Private Function recuperarSolCajaMovimiento(ByVal cod As Integer)
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.Connection = Cn
        cmd.CommandText = "select COUNT(*) from TMovimientoCaja where codSC = " & cod
        Return cmd.ExecuteScalar()

    End Function

    Private Function recuperarDetalleSolCaja(ByVal cod As Integer)
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.Connection = Cn
        cmd.CommandText = "select COUNT(*) from DetSolCaja where codSC = " & cod
        Return cmd.ExecuteScalar()

    End Function

    Private Sub ConsultaDetalleCaja()

        oDataManager.CargarGrilla("select  codDetSol,insumo,cant1,uniMed,prec1,obsSol,estado,aprobador,obsApro,areaM,tipoM,codMat,codEstado,Parcial1 from VDetaleSolCaja Where codSC=" & lbSolicitud.SelectedValue & " and codAreaM= " & cbArea.SelectedValue, CommandType.Text, dgDetalleSol, BindingSource1)
        Navigator2.BindingSource = BindingSource1
        ModificarColumnasDetalleSol()

        ColorearColumnas()

        calcularTotalSolicitado()

        'Limpiar lista de indice de fila actualizadas
        _listaUpdate.Clear()

    End Sub

    Private Sub calcularTotalSolicitado()

        txtTotal.Text = oGrilla.SumarColumnaGrilla(dgDetalleSol, "Parcial1")

    End Sub

    Private Sub ActualizarMontoSolicitado()
        'Obteniendo el último calculo
        ' calcularTotalSolicitado()

        'Actualizando el Monto Total Solicitado en TSolicitudCaja
        Dim myTrans2 As SqlTransaction = Cn.BeginTransaction()
        comandoUpdate()
        cmUdpateTable.Transaction = myTrans2
        If cmUdpateTable.ExecuteNonQuery() < 1 Then
            ' wait.Close()
            Me.Cursor = Cursors.Default
            myTrans2.Rollback()
            MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End If

        myTrans2.Commit()

    End Sub


    Private Sub ConsultaSolicitudCaja()

        oDataManager.CargarListBox("select codSC,nro +' '+ estado as nro from VsolicitudCaja where codCC=" & vCajaChica, CommandType.Text, lbSolicitud, "codSC", "nro")


    End Sub

    ''' <summary>
    ''' valida los el registro del detalle de solicitud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCampos() As Boolean
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        If ValidarCantMayorCero(txtPrecio.Text) Then
            txtPrecio.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function


    ''' <summary>
    ''' da formato a la grilla Detalle Solicitud caja
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDetalleSol()

        'dgDetalleSol.ReadOnly = True
        dgDetalleSol.AllowUserToAddRows = False
        dgDetalleSol.AllowUserToDeleteRows = False

        With dgDetalleSol
            .Columns("codDetSol").Visible = False
            .Columns("insumo").HeaderText = "Insumo"
            .Columns("insumo").Width = 340
            .Columns("insumo").ReadOnly = True

            .Columns("cant1").Width = 50
            .Columns("cant1").HeaderText = "Cant."
            .Columns("cant1").ReadOnly = False
            .Columns("cant1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cant1").DefaultCellStyle.Format = "N2"

            .Columns("uniMed").Width = 45
            .Columns("uniMed").HeaderText = "Unid."
            .Columns("uniMed").ReadOnly = True

            .Columns("prec1").HeaderText = "Precio"
            .Columns("prec1").Width = 50
            .Columns("prec1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("prec1").DefaultCellStyle.Format = "N2"
            .Columns("prec1").ReadOnly = False

            .Columns("estado").HeaderText = "Estado"
            .Columns("estado").Width = 75

            .Columns("obsSol").ReadOnly = False 'NO editable
            .Columns("obsSol").HeaderText = "Obs."
            .Columns("obsSol").Width = 100

            .Columns("tipoM").HeaderText = "Tipo_Insumo"
            .Columns("tipoM").Width = 100
            .Columns("tipoM").Visible = False

            .Columns("obsSol").Width = 300
            .Columns("ObsSol").HeaderText = "Observacion solicitante"
            .Columns("ObsSol").ReadOnly = False

            .Columns("aprobador").Width = 100
            .Columns("aprobador").HeaderText = "Verificador"
            .Columns("aprobador").ReadOnly = True

            .Columns("obsApro").Width = 400
            .Columns("obsApro").HeaderText = "Observacion verificador"
            .Columns("obsApro").ReadOnly = True

            .Columns("areaM").HeaderText = "Área"
            .Columns("areaM").Width = 100
            .Columns("areaM").ReadOnly = True

            .Columns("codMat").Visible = False

            .Columns("Parcial1").HeaderText = "Parcial"
            .Columns("Parcial1").Width = 50
            .Columns("Parcial1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Parcial1").DefaultCellStyle.Format = "N2"
            .Columns("Parcial1").ReadOnly = True
            .Columns("Parcial1").DisplayIndex = 5

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    ''' <summary>
    ''' Da formato a las columnas de la grilla insumos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasInsumo()

        dgInsumo.ReadOnly = True
        dgInsumo.AllowUserToAddRows = False
        dgInsumo.AllowUserToDeleteRows = False

        With dgInsumo
            .Columns("codMat").Visible = False
            .Columns("material").HeaderText = "Descripción Insumo"
            .Columns("material").Width = 595


            .Columns("uniBase").Width = 65
            .Columns("uniBase").HeaderText = "Unid."

            .Columns("preBase").HeaderText = "Precio"
            .Columns("preBase").Width = 55
            .Columns("preBase").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("preBase").DefaultCellStyle.Format = "N2"

            .Columns("tipoM").HeaderText = "Tipo Insumo"
            .Columns("tipoM").Width = 120

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub ColorearColumnas()
        oGrilla.colorearFilasDGV(dgDetalleSol, "cant1", Color.AliceBlue, Color.Black)
        oGrilla.colorearFilasDGV(dgDetalleSol, "prec1", Color.AliceBlue, Color.Black)
        oGrilla.colorearFilasDGV(dgDetalleSol, "obsSol", Color.AliceBlue, Color.Black)

        oGrilla.colorearFilasDGV(dgDetalleSol, "estado", "PENDIENTE", Color.White, Color.Black)
        oGrilla.colorearFilasDGV(dgDetalleSol, "estado", "APROBADO", Color.Green, Color.White)
        oGrilla.colorearFilasDGV(dgDetalleSol, "estado", "OBSERVADO", Color.Yellow, Color.Red)
        oGrilla.colorearFilasDGV(dgDetalleSol, "estado", "RECHAZADO", Color.Red, Color.White)

    End Sub

    Private Sub enlazartext()
        If BindingSource0.Count > 0 Then
            dtpFecha.Value = BindingSource0.Item(BindingSource0.Position)(1)
            'txtNro.Text = BindingSource0.Item(BindingSource0.Position)(5)
            txtTotal.Text = BindingSource0.Item(BindingSource0.Position)(6)

        End If


    End Sub

#End Region

#Region "Eventos"

#End Region

    Private Sub MantSolicitudCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = Keys.F5 Then
            btnProcesa.PerformClick()
        End If


        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function


    Private Sub MantSolicitudCajaForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'obteniendo el codigo de caja chica
        vCajaChica = oDataManager.consultarTabla("select codCC from TCajaChica where codigo ='" & vSCodigo & "'", CommandType.Text)

        'obteniendo el último número de solicitud
        recuperarUltimoNro(vCajaChica)

        'obteniendo el nombre de la obra / lugar
        txtObra.Text = vSNomSuc
        txtSolicitante.Text = vSUsuario

        'cargando el combobox Areas 
        oDataManager.CargarCombo("select codAreaM,areaM  from  TAreaMat ", CommandType.Text, cbArea, "codAreaM", "areaM")
        'Cargando el ListBox de Nro de Solicitud

        ConsultaSolicitudCaja()
        'oDataManager.CargarListBox("select codSC,nro +' '+ estado as nro from VsolicitudCaja where codCC=" & vCajaChica, CommandType.Text, lbSolicitud, "codSC", "nro")


    End Sub

    Private Sub btnSolicitud_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitud.Click

        If oDataManager.consultarTabla("select count(*) from TCajaChica", CommandType.Text) = 0 Then
            StatusBarClass.messageBarraEstado("  POR FAVOR CREE UNA CAJA PRIMERO...")
            Exit Sub
        End If


        recuperarUltimoNro(vCajaChica)
        Dim campo As String = txtNro.Text

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TSolicitud

            comandoInsert(vCajaChica)
            cmInsertTable.Transaction = myTrans
            If cmInsertTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMytrans = True

            'vfVan1 = False
            'vfVan2 = False

            'Actualizando el lisxbox
            ConsultaSolicitudCaja()

            'dsAlmacen.Tables("VSolAper").Clear()
            'daTabla1.Fill(dsAlmacen, "VSolAper")

            'Buscando por nombre de campo y luego pocisionarlo con el indice

            lbSolicitud.SelectedIndex = lbSolicitud.FindString(campo)

            'lbSolicitud.FindString(,

            'vfVan1 = True
            'vfVan2 = True
            recuperarUltimoNro(vCajaChica)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try

    End Sub

    Private Sub lbSolicitud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSolicitud.SelectedIndexChanged

        'consultando el encabezado de la solicitud de caja chica 
        If Not TypeOf lbSolicitud.SelectedValue Is Dato Then

            Dim vgrilla As New DataGridView
            oDataManager.CargarGrilla("select codSC,fechaSol, nombres,estSol,codPers,nro,montoSol  from VsolicitudCaja where codSC = " & lbSolicitud.SelectedValue, CommandType.Text, vgrilla, BindingSource0)

            enlazartext()
            'oDataManager.CargarGrilla("select  * from detSolCaja Where codSC=" & lbSolicitud.SelectedValue, CommandType.Text, dgDetalleSol, BindingSource1)
            ConsultaDetalleCaja()
        End If

    End Sub

    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        oDataManager.CargarGrilla("select codMat,material,uniBase,preBase,tipoM from VMaterialSele", CommandType.Text, dgInsumo, BindingSource2)
        BindingNavigator1.BindingSource = BindingSource2
        ModificarColumnasInsumo()
    End Sub

    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click

        If dgInsumo.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a agregar...")
            Exit Sub
        End If

        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Solicitud NO APERTURADA...")
            Exit Sub
        End If

        'If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
        '    MessageBox.Show("No se puede agregar requerimiento por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
        '    Exit Sub
        'End If

        'If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
        '    MessageBox.Show("No se puede agregar requerimiento por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
        '    Exit Sub
        'End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If BindingSource1.Find("codMat", BindingSource2.Item(BindingSource2.Position)(0)) >= 0 Then
            MessageBox.Show("Ya existe requerimiento: " & BindingSource2.Item(BindingSource2.Position)(1), nomNegocio, Nothing, MessageBoxIcon.Information)
            txtInsumo.Focus()
            txtInsumo.SelectAll()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()

        Try

            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            comandoInsertDetalle()
            cmInsertTableDetalle.Transaction = myTrans
            If cmInsertTableDetalle.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub

            End If

            Dim codDetSol As Integer = cmInsertTableDetalle.Parameters("@Identity").Value
            myTrans.Commit()





            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMytrans = True

            'actualizar detalle solicitud
            ConsultaDetalleCaja()

            'Actualizando el Monto Total Solicitado en TSolicitudCaja
            ActualizarMontoSolicitado()

            BindingSource1.Position = BindingSource1.Find("codDetSol", codDetSol)

            StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")

            txtObs.Clear()
            txtCan.Focus()
            txtCan.SelectAll()

        Catch f As Exception

            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACIÓN PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cbArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbArea.SelectedIndexChanged

        If lbSolicitud.Items.Count > 0 Then
            If Not TypeOf lbSolicitud.SelectedValue Is Dato Then
                ConsultaDetalleCaja()
            End If
        End If
    End Sub

    Private Sub MantSolicitudCajaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ColorearColumnas()

    End Sub

    Private Sub dgDetalleSol_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgDetalleSol.Sorted
        ColorearColumnas()
    End Sub

    Private Sub txtInsumo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInsumo.TextChanged
        'FILTRANDO INSUMO
        BindingSource2.Filter = "material like '" & txtInsumo.Text.Trim() & "%'"

        If BindingSource2.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnAgrega
            'colorearFila1()
        Else
            'txtBuscar.Focus()
            'txtBuscar.SelectAll()
            StatusBarClass.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If

    End Sub



    Private Sub dgDetalleSol_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDetalleSol.CellEndEdit

        Dim _item As Integer = BindingSource1.Position
        Dim resul As Boolean = _listaUpdate.Exists(Function(p) p = _item)
        Dim _estado As Integer = BindingSource1.Item(_item)(12)

        'Comprobando si ya existe el indice en la lista
        If _estado = 0 Or _estado = 2 Then
            If resul = False Then
                _listaUpdate.Add(_item) 'agregando a lista
            End If
        Else
            dgDetalleSol.CancelEdit()
            StatusBarClass.messageBarraEstado(" Sólo se puede modificar si el estado es igual a PENDIENTE u OBSERVADO...")
        End If

    End Sub



    Private Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
            Exit Sub
        End If


    End Sub

    Private Sub btnActualizarDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizarDetalle.Click
        If BindingSource0.Item(BindingSource0.Position)(3) = 1 Then
            MessageBox.Show("No se puede actualizar requerimientos por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource1.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE REGISTROS A PROCESAR...")
            Exit Sub
        End If

        ' si estado en aprobado o rechazado no se puede modificar
        Dim resp As Short = MessageBox.Show("¿ESTÁ SEGURO DE GUARDAR LAS MODIFICACIONES?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        Dim finalMytrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")

            'recorriendo las lista de indices de filas modificadas
            For j As Short = 0 To _listaUpdate.Count - 1

                'If BindingSource1.Item(j)(13) = 1 Or BindingSource3.Item(j)(13) = 3 Then '1=pendiente  3=Observado
                'actualizando TDetalleSol
                comandoUpdateDetalle(BindingSource1.Item(_listaUpdate(j))(2), BindingSource1.Item(_listaUpdate(j))(4), BindingSource1.Item(_listaUpdate(j))(5).ToString().Trim(), BindingSource1.Item(_listaUpdate(j))(0))
                cmUpdateTableDetalle.Transaction = myTrans
                If cmUpdateTableDetalle.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                    'End If
                End If
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            


            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

            ConsultaSolicitudCaja() ' Actualizando Grilla Y mostrando de nuevo

            'Actualizando el Monto Total Solicitado en TSolicitudCaja
            ActualizarMontoSolicitado()

            StatusBarClass.messageBarraEstado("  Registros actualizados con éxito...")

        Catch f As Exception

            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnEliminarItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEliminarItem.Click

        If BindingSource1.Count = 0 Then
            MessageBox.Show("NO existen registros a eliminar", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource0.Item(BindingSource0.Position)(3) = 1 Then
            MessageBox.Show("No se puede eliminar requerimiento por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource0.Item(BindingSource0.Position)(4) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro requerimiento...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(12) <> 0 Then
            MessageBox.Show("NO se debe eliminar por NO estar en el estado de [PENDIENTE]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Está seguro de eliminar: " & BindingSource1.Item(BindingSource1.Position)(1) & "  Si elimina no podra deshacer...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Cursor = Cursors.AppStarting
        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TDetalleSol
            comandoDeleteDetalle(BindingSource1.Item(BindingSource1.Position)(0))
            cmDeleteTableDetalle.Transaction = myTrans
            If cmDeleteTableDetalle.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

           


            StatusBarClass.messageBarraEstado("  ELIMINACION CON EXITO...")
            finalMytrans = True

            'Actualizando el dataTable
            ConsultaDetalleCaja()

            'Actualizando el Monto Total Solicitado en TSolicitudCaja
            ActualizarMontoSolicitado()

            'accionesIniciales()
            StatusBarClass.messageBarraEstado("  ELIMINACIÓN CON ÉXITO...")
        Catch f As Exception

            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        ' si codSc registrado en tMovimiento no se elimina
        If recuperarSolCajaMovimiento(BindingSource0.Item(BindingSource0.Position)(0)) > 0 Then
            MessageBox.Show("No se puede ELIMINAR por tener movimientos de caja...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarDetalleSolCaja(BindingSource0.Item(BindingSource0.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud tiene registros en el detalle...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de eliminar solicitud de Caja Nº " & BindingSource0.Item(BindingSource0.Position)(5), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TSolicitud
            comandoDelete()
            cmDeleteTable.Transaction = myTrans
            If cmDeleteTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
            finalMytrans = True
            'Actualizando el dataTable

            ' enlazarText()
            ConsultaSolicitudCaja()
            'ConsultaDetalleCaja()

            'visualizarDet()
            recuperarUltimoNro(vCajaChica)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
            wait.Close()

        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

    End Sub

    Private Sub btnCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Dim crea As New crearMaterialForm
        crea.ShowDialog()
    End Sub

    Private Sub dgInsumo_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgInsumo.CellDoubleClick
        btnAgrega.PerformClick()
    End Sub

    Private Sub txtCan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan.KeyPress
        ValidarNumeroDecimal(txtCan, e)
    End Sub

    Private Sub txtCan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.TextChanged, txtPrecio.TextChanged
        Me.AcceptButton = Me.btnAgrega
    End Sub

    
    Private Sub txtPrecio_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecio.KeyPress
        ValidarNumeroDecimal(txtPrecio, e)
    End Sub
End Class
