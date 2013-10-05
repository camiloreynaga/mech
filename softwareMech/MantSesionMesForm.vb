Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantSesionMesForm
    Dim BindingSource1 As New BindingSource

    Private Sub MantSesionMesForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantSesionMesForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetHotelModule.vb
        Dim sele As String = "select idSesM,mes,ano,estado,idMes,estado1 from VSesionMes where codigo=@cod order by idSesM"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select idMes,mes from TMes order by idMes"
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetHotelModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSesionMes")
            daTabla2.Fill(dsAlmacen, "TMes")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSesionMes"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            'Enlazando el ListBox a la tabla 
            lbMes.DataSource = dsAlmacen
            lbMes.DisplayMember = "TMes.mes"
            lbMes.ValueMember = "idMes"

            configurarColorControl()
            txtCod.DataBindings.Add("Text", BindingSource1, "idSesM")

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Mes"
            .Columns(1).Width = 100
            .Columns(2).HeaderText = "Año"
            .Columns(2).Width = 70
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 80
            .Columns(3).HeaderText = "Estado"
            .Columns(4).Visible = False
            .Columns(5).Visible = False
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
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub txtCod_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCod.TextChanged
        If BindingSource1.Count = 0 Then
            'desEnlazarText()
        Else
            enlazarText()
        End If
    End Sub

    Private Sub enlazarText()
        lbMes.SelectedValue = BindingSource1.Item(BindingSource1.Position)(4)
        lbAno.SelectedIndex = lbAno.FindStringExact(BindingSource1.Item(BindingSource1.Position)(2))
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        If vfNuevo1 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub activarText()
        lbMes.Enabled = True
        lbAno.Enabled = True
    End Sub

    Private Sub desActivarText()
        lbMes.Enabled = False
        lbAno.Enabled = False
    End Sub

    Public Function recuperarCount(ByVal idMes As Short, ByVal ano As Integer) As Short
        'Obtener un único valor de una base de datos
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TSesionMes where idMes=" & idMes & " and ano=" & ano & " and codigo='" & vSCodigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TSesionMes set estado=3 where estado=2 and codigo='" & vSCodigo & "'"
        cmUpdateTable13.Connection = Cn
    End Sub

    Dim cmUpdateTable14 As SqlCommand
    Private Sub comandoUpdate14()
        cmUpdateTable14 = New SqlCommand
        cmUpdateTable14.CommandType = CommandType.Text
        cmUpdateTable14.CommandText = "update TSesionMes set estado=2 where estado=1 and codigo='" & vSCodigo & "'"
        cmUpdateTable14.Connection = Cn
    End Sub

    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar Mes"
            desactivarControles1()
            txtCod.DataBindings.Clear()
            activarText()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else   ' guardar
            If recuperarCount(lbMes.SelectedValue, lbAno.Text.Trim()) > 0 Then
                MessageBox.Show("Ya exíste periodo => " & lbMes.Text.Trim() & " - " & lbAno.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Information)
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

                'TSesionMes  cambiando pendiente=2 a cerrad0=3
                comandoUpdate13()
                cmUpdateTable13.Transaction = myTrans
                cmUpdateTable13.ExecuteNonQuery()

                'TSesionMes  cambiando abierto=1 a pendiente=2
                comandoUpdate14()
                cmUpdateTable14.Transaction = myTrans
                cmUpdateTable14.ExecuteNonQuery()

                'TSesionMes
                comandoInsert()
                cmInserTable.Transaction = myTrans
                If cmInserTable.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True

                'Actualizando el dataSet 
                dsAlmacen.Tables("VSesionMes").Clear()
                daTabla1.Fill(dsAlmacen, "VSesionMes")

                Me.btnCancelar.PerformClick()

                BindingSource1.Position = BindingSource1.Count - 1

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
                Me.Cursor = Cursors.Default

                End
            Catch f As Exception
                wait.Close()
                Me.Cursor = Cursors.Default
                If finalMytrans Then
                    MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End Try
        End If
    End Sub

    Dim vfModificar1 As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(5) = 2 Or BindingSource1.Item(BindingSource1.Position)(5) = 3 Then  '2=Pendiente 3=cerrado
            MessageBox.Show("Proceso denegado, Sesion mes no esta <ABIERTO>", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If vfModificar1 = "modificar" Then
            If dgTabla1.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            enlazarText()
            vfModificar1 = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            txtCod.DataBindings.Clear()
            activarText()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If recuperarCount(lbMes.SelectedValue, lbAno.Text.Trim()) > 0 Then
                MessageBox.Show("Ya exíste periodo => " & lbMes.Text.Trim() & " - " & lbAno.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de modificar sesión mes?" & Chr(13) & "Si modifica, todas sus dependencias" & Chr(13) & "heredaran este nombre...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                lbMes.Focus()
                Exit Sub
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TSesionMes
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet 
                dsAlmacen.Tables("VSesionMes").Clear()
                daTabla1.Fill(dsAlmacen, "VSesionMes")

                Me.btnCancelar.PerformClick()

                BindingSource1.Position = BindingSource1.Count - 1

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
            Catch f As Exception
                wait.Close()
                If finalMytrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        Me.btnNuevo.Text = "Nuevo"
        vfModificar1 = "modificar"
        Me.btnModificar.Text = "Modificar"
        activarControles1()
        txtCod.DataBindings.Add("Text", BindingSource1, "idSesM")
        desActivarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
        enlazarText()
    End Sub

    Private Function recuperarNroReg1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(idSesM) from TMovimientoMech where idSesM=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarNroReg2(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(idSesM) from TPagoDesembolso where idSesM=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(5) = 2 Or BindingSource1.Item(BindingSource1.Position)(5) = 3 Then  '2=Pendiente 3=cerrado
            MessageBox.Show("Proceso denegado, Sesion mes no esta <ABIERTO>", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If recuperarNroReg1(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  Acceso denegado, Sesion Mes tiene Movimientos Procesados...")
            Exit Sub
        End If

        If recuperarNroReg2(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  Acceso denegado, Sesion Mes tiene Desembolsos Procesados...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TSesionMes
            comandoDelete()
            cmDeleteTable.Transaction = myTrans
            If cmDeleteTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar registro por qué esta actualmente en uso...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VSesionMes").Clear()
            daTabla1.Fill(dsAlmacen, "VSesionMes")

            enlazarText()
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

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert TSesionMes(idMes,ano,estado,codigo) values(@idM,@ano,@est,@cod)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@idM", SqlDbType.Int, 0).Value = lbMes.SelectedValue
        cmInserTable.Parameters.Add("@ano", SqlDbType.Int, 0).Value = lbAno.Text.Trim()
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1     '1=Abierto
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TSesionMes set idMes=@idM,ano=@ano where idSesM=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@idM", SqlDbType.Int, 0).Value = lbMes.SelectedValue
        cmUpdateTable.Parameters.Add("@ano", SqlDbType.Int, 0).Value = lbAno.Text.Trim()
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TSesionMes where idSesM=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class
