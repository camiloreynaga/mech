Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class aperturaSolicitudForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub aperturaSolicitudForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub aperturaSolicitudForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nro,fecSol,est,nombres,lugar,obs,estado,codigo,codPers from VSolAper where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,tipoM,nombres,obs1,nombres1,obs2,idSol,codEstS from VDetSol where idSol=@idS" 'areaM,descrip
        crearDataAdapterTable(daVMat, sele)
        daVMat.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolAper")
            daVMat.Fill(dsAlmacen, "VDetSol")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolAper"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetSol"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "areaM,descrip"
            ModificarColumnasDGV()

            configurarColorControl()
            recuperarUltimoNro(vSCodigo)

            vfVan1 = True
            visualizarDet()

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub aperturaSolicitudForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(13) = 2 Then 'Aprobado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.YellowGreen
                dgTabla2.Rows(j).DefaultCellStyle.ForeColor = Color.DarkBlue
            End If
            If BindingSource2.Item(j)(13) = 3 Then 'Observado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.Red
            End If
            If BindingSource2.Item(j)(13) = 4 Then 'Rechazado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.Yellow
            End If
        Next
    End Sub

    Private Sub recuperarUltimoNro(ByVal cod As String)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroS),0)+1 from TSolicitud where codigo='" & cod & "'"
        cmdMaxCodigo.Connection = Cn
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

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

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSol").Clear()
            daVMat.SelectCommand.Parameters("@idS").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daVMat.Fill(dsAlmacen, "VDetSol")
            colorearFila()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºSol"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fech_Sol"
            .Columns(2).Width = 70
            .Columns(3).Width = 60
            .Columns(3).HeaderText = "Estado"
            .Columns(4).Width = 100
            .Columns(4).HeaderText = "Solicitante"
            .Columns(5).Width = 280
            .Columns(5).HeaderText = "Lugar/Obra"
            .Columns(6).Width = 340
            .Columns(6).HeaderText = "Observacion Solicitante"
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Necesid."
            .Columns(1).Width = 70
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 260
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Cant."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "Unidad"
            .Columns(5).Width = 70
            .Columns(5).HeaderText = "Estado"
            .Columns(6).HeaderText = "Area_Insumo"
            .Columns(6).Width = 100
            .Columns(7).HeaderText = "Tipo_Insumo"
            .Columns(7).Width = 100
            .Columns(8).Width = 100
            .Columns(8).HeaderText = "Solicitante"
            .Columns(9).Width = 300
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).Width = 100
            .Columns(10).HeaderText = "Verificador"
            .Columns(11).Width = 400
            .Columns(11).HeaderText = "Observacion verificador"
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        Label1.ForeColor = ForeColorLabel
        Label2.ForeColor = ForeColorLabel
        Label3.ForeColor = ForeColorLabel
        btnAperturar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Function recuperarAbierto(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TSolicitud where estado=0 and codigo='" & codigo & "'" '0=abierto
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TSolicitud set estado=1 where estado=0 and codigo='" & vSCodigo & "'"
        cmUpdateTable13.Connection = Cn
    End Sub

    Private Sub btnAperturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAperturar.Click
        'If recuperarAbierto(vSCodigo) > 0 Then
        'MessageBox.Show("Proceso denegado, Ya existe una solicitud aperturada...", nomNegocio, Nothing, MessageBoxIcon.Error)
        'Exit Sub
        'End If

        If BindingSource1.Position <> -1 Then
            If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) = 0) Then
                StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud NO tiene registros en detalle solicitud...")
                Exit Sub
            End If
        End If

        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de aperturar solicitud de requerimiento?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            recuperarUltimoNro(vSCodigo)
            Dim campo As String = txtNro.Text

            'TSolicitud  cambiando abierto=0 a cerrado=1
            comandoUpdate13()
            cmUpdateTable13.ExecuteNonQuery()

            'llamando al procedimiento k crea el comando insert
            comandoInsert()
            cmInserTable.ExecuteNonQuery()

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            vfVan1 = True
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("nro", campo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            wait.Close()
        Catch
            wait.Close()
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                'myTrans.Rollback()
                MessageBox.Show("NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TSolicitud(nroS,fecSol,codPers,codigo,estado,obs) values(@nro,@fec,@codP,@cod,0,@obs)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo
        cmInserTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
    End Sub

    Private Function recuperarUltimoDoc(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroS) from TSolicitud where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleSol where idSol=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc(BindingSource1.Item(BindingSource1.Position)(8)) <> CInt(BindingSource1.Item(BindingSource1.Position)(1)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima solicitud ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud tiene registros en detalle solicitud...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True
            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            vfVan1 = True
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
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

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TSolicitud where idSol=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        btnAperturar.FlatStyle = FlatStyle.Flat
        btnAperturar.Enabled = False
        btnModificar.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnCancelar.Enabled = True
        btnElimina.FlatStyle = FlatStyle.Flat
        btnElimina.Enabled = False
        Panel2.Enabled = False

        date1.Value = BindingSource1.Item(BindingSource1.Position)(2)
        txtObs.Text = BindingSource1.Item(BindingSource1.Position)(6)

        Me.AcceptButton = btnModificar
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Enabled = False
        btnAperturar.FlatStyle = FlatStyle.Standard
        btnAperturar.Enabled = True
        btnElimina.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = True
        Panel2.Enabled = True

        txtObs.Clear()
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta seguro de modificar Solicitud?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TSolicitud
            comandoUpdate1()
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            Dim idSol As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            vfVan1 = True
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            btnCancelar.PerformClick()
            BindingSource1.Position = BindingSource1.Find("idSol", idSol)

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If
            Exit Sub
        End Try
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TSolicitud set fecSol=@fec,obs=@obs where idSol=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub
End Class
