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

    ''' <summary>
    ''' variable para codigo caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Dim vCajaChica As Integer

    Dim oDataManager As New cDataManager



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
    Private Sub comandoInsert()
        cmInsertTable = New SqlCommand()
        cmInsertTable.CommandType = CommandType.Text
        cmInsertTable.CommandText = "insert into TSolicitudCaja values (@nroSol,@fechaSol,@codPers,@codCC,0,0,0)"
        cmInsertTable.Connection = Cn
        cmInsertTable.Parameters.Add("@nroSol", SqlDbType.Int).Value = txtNro.Text.Trim()
        cmInsertTable.Parameters.Add("@fechaSol", SqlDbType.Date).Value = dtpFecha.Value.Date
        cmInsertTable.Parameters.Add("@codPers", SqlDbType.Int).Value = vPass 'obteniendo el codigo de usuario
        cmInsertTable.Parameters.Add("@codCC", SqlDbType.Int).Value = 1
    End Sub
    ''' <summary>
    ''' comando Actualizar solicitud caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate()
        cmUdpateTable = New SqlCommand()
        cmUdpateTable.CommandType = CommandType.Text
        cmUdpateTable.CommandText = "Update TSolicitudCaja set nroSol=@nroSol,fechaSol=@fechaSol,codPers=@codPers,montoSol=@montoSol where codSC=@cod"
        cmUdpateTable.Connection = Cn
        cmUdpateTable.Parameters.Add("@nroSol", SqlDbType.Int).Value = txtNro.Text.Trim()
        cmUdpateTable.Parameters.Add("@fechaSol", SqlDbType.Date).Value = dtpFecha.Value.Date
        cmUdpateTable.Parameters.Add("@codPers", SqlDbType.Int).Value = vPass 'obteniendo el codigo de usuario
        cmUdpateTable.Parameters.Add("@codCC", SqlDbType.Int).Value = 1
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
        cmInsertTableDetalle.Parameters.Add("@uniMed", SqlDbType.VarChar, 20).Value = BindingSource2.Item(BindingSource2.Position)(3)
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

    Private Sub ConsultaDetalleCaja()

        oDataManager.CargarGrilla("select  codDetSol,insumo,cant1,uniMed,prec1,obsSol,estDet,codApro,obsApro,codMat,codAreaM,codSC,estRen,codRen,nroDocRen from detSolCaja Where codSC=" & lbSolicitud.SelectedValue & " and codAreaM= " & cbArea.SelectedValue, CommandType.Text, dgDetalleSol, BindingSource1)

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

        'Todo OK RAS
        Return False
    End Function

    Private Sub ModificarColumnasDetalleSol()
        With dgDetalleSol
            .Columns("codDetSol").Visible = False
            .Columns("insumo").HeaderText = "Insumo"
            .Columns("insumo").Width = 340
            .Columns("insumo").ReadOnly = True 'NO editable
            .Columns("cant1").Width = 50
            .Columns("cant1").HeaderText = "Cant."
            .Columns("cant1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cant1").DefaultCellStyle.Format = "N2"

            .Columns(4).Width = 45
            .Columns(4).HeaderText = "Unid."
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).HeaderText = "Estado"
            .Columns(5).Width = 75
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Area_Insumo"
            .Columns(6).Width = 100
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).HeaderText = "Tipo_Insumo"
            .Columns(7).Width = 100
            .Columns(7).Visible = False
            .Columns(7).ReadOnly = True 'NO editable
            .Columns(8).Width = 100
            .Columns(8).HeaderText = "Solicitante"
            .Columns(8).ReadOnly = True 'NO editable
            .Columns(9).Width = 300
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).Width = 100
            .Columns(10).HeaderText = "Verificador"
            .Columns(11).Width = 400
            .Columns(11).HeaderText = "Observacion verificador"
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

#End Region

#Region "Eventos"

#End Region

    Private Sub MantSolicitudCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

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
        oDataManager.CargarListBox("select codSC,nro +' '+ estado as nro from VsolicitudCaja where codCC=" & vCajaChica, CommandType.Text, lbSolicitud, "codSC", "nro")

        Cn.Open()

    End Sub

    Private Sub btnSolicitud_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitud.Click

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
            comandoInsert()
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

            'Actualizando el dataTable

            'dsAlmacen.Tables("VSolAper").Clear()
            'daTabla1.Fill(dsAlmacen, "VSolAper")

            'Buscando por nombre de campo y luego pocisionarlo con el indice

            'lbSolicitud.Items. = BindingSource0.Find("nroSol", campo)

            'lbSolicitud.FindString(,

            'vfVan1 = True
            'vfVan2 = True
            recuperarUltimoNro(vSCodigo)

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
            oDataManager.CargarGrilla("select codSC,fechaSol, nombres  from VsolicitudCaja where codSC = " & lbSolicitud.SelectedValue, CommandType.Text, vgrilla, BindingSource0)

            'oDataManager.CargarGrilla("select  * from detSolCaja Where codSC=" & lbSolicitud.SelectedValue, CommandType.Text, dgDetalleSol, BindingSource1)
            ConsultaDetalleCaja()
        End If

    End Sub

    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        oDataManager.CargarGrilla("select codMat,material,uniBase,preBase,tipoM,hist from VMaterialSele", CommandType.Text, dgInsumo, BindingSource2)

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
            MessageBox.Show("Ya existe requerimiento: " & BindingSource2.Item(BindingSource2.Position)(2), nomNegocio, Nothing, MessageBoxIcon.Information)
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
End Class
