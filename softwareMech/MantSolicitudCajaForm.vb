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

        oDataManager.CargarListBox("select codSC,nroSol from TSolicitudCaja where codCC=" & vCajaChica, CommandType.Text, lbSolicitud, "nroSol", "codSC")
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
            BindingSource0.Position = BindingSource0.Find("nroSol", campo)

            lbSolicitud.FindString(,

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
End Class
