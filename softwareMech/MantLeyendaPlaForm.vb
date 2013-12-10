Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class MantLeyendaPlaForm
    Dim BindingSource1 As New BindingSource

    Private Sub MantLeyendaPlaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantLeyendaPlaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetRestaurantModule.vb
        Dim sele As String = "select codLP,leyenda,abrev,'asi'=case when est=1 then 'SI' else 'NO' end,est from TLeyendaPla order by leyenda"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetRestaurantModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "TLeyendaPla")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TLeyendaPla"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            configurarColorControl()

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Leyenda"
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).Width = 130
            .Columns(2).HeaderText = "Abrev"
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).Width = 50
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 40
            .Columns(3).HeaderText = "Asist."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(4).Visible = False
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
        Label4.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub enlazarText()
        If BindingSource1.Count = 0 Then
            'desEnlazarText()
        Else
            txtLey.Text = BindingSource1.Item(BindingSource1.Position)(1)
            txtAbr.Text = BindingSource1.Item(BindingSource1.Position)(2)
            If BindingSource1.Item(BindingSource1.Position)(4) = 1 Then
                lbEfe.SelectedIndex = 0   'SI
            Else
                lbEfe.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        enlazarText()
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        lbEfe.Enabled = True

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
        lbEfe.Enabled = True
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
        txtLey.ReadOnly = True
        txtAbr.ReadOnly = True
    End Sub

    Private Function ValidarCampos() As Boolean
        If validaCampoVacioMinCaracNoNumer(txtLey.Text.Trim, 3) Then
            txtLey.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtAbr.Text.Trim, 1) Then
            txtAbr.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            txtLey.ReadOnly = False
            txtAbr.ReadOnly = False
            txtLey.Clear()
            txtAbr.Clear()
            txtLey.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else   ' guardar
            If ValidarCampos() Then
                Exit Sub
            End If

            If BindingSource1.Find("leyenda", txtLey.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste leyenda: " & txtLey.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtLey.Focus()
                txtLey.SelectAll()
                Exit Sub
            End If

            If BindingSource1.Find("abrev", txtAbr.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste Abrev: " & txtAbr.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtAbr.Focus()
                txtAbr.SelectAll()
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                Dim vfCampo1 As String = txtLey.Text.Trim()

                'TLeyendaPla
                comandoInsert1()
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True

                'Actualizando el dataSet dsAlmacen
                dsAlmacen.Tables("TLeyendaPla").Clear()
                daTabla1.Fill(dsAlmacen, "TLeyendaPla")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("leyenda", vfCampo1)
                btnCancelar.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
            Catch f As Exception
                wait.Close()
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
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        activarControles1()
        enlazarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Dim vfModificar As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar = "modificar" Then
            If dgTabla1.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            enlazarText()
            txtLey.ReadOnly = False
            txtAbr.ReadOnly = False
            txtLey.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If

            Dim vfCampo1 As String = BindingSource1.Item(BindingSource1.Position)(1)
            If vfCampo1.ToUpper() <> txtLey.Text.ToUpper.Trim() Then
                If BindingSource1.Find("leyenda", txtLey.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya exíste leyenda: " & txtLey.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtLey.Focus()
                    txtLey.SelectAll()
                    Exit Sub
                End If
            End If

            vfCampo1 = BindingSource1.Item(BindingSource1.Position)(2)
            If vfCampo1.ToUpper() <> txtAbr.Text.ToUpper.Trim() Then
                If BindingSource1.Find("abrev", txtAbr.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya exíste Abrev: " & txtAbr.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtAbr.Focus()
                    txtAbr.SelectAll()
                    Exit Sub
                End If
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                'TLeyendaPla
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                vfCampo1 = txtLey.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet dsAlmacen
                dsAlmacen.Tables("TLeyendaPla").Clear()
                daTabla1.Fill(dsAlmacen, "TLeyendaPla")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("leyenda", vfCampo1)
                btnCancelar.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
            Catch f As Exception
                wait.Close()
                If finalMytrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End Try
        End If
    End Sub

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TVenta where codLP=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        'If recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
        '    StatusBarClass.messageBarraEstado(" PROCESO DENEGADO, TIPO PAGO TIENE VENTAS PROCESADAS...")
        '    Exit Sub
        'End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtLey.Focus()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TLeyendaPla
            comandoDelete()
            cmDeleteTable.Transaction = myTrans
            If cmDeleteTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar Representante por qué esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet dsAlmacen
            dsAlmacen.Tables("TLeyendaPla").Clear()
            daTabla1.Fill(dsAlmacen, "TLeyendaPla")

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

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert TLeyendaPla(leyenda,abrev,est) values(@ley,@abr,@est)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@ley", SqlDbType.VarChar, 20).Value = txtLey.Text.Trim()
        cmInserTable1.Parameters.Add("@abr", SqlDbType.VarChar, 3).Value = txtAbr.Text.Trim()
        If lbEfe.SelectedIndex = 0 Then 'SI
            cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else  'NO
            cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TLeyendaPla set leyenda=@ley,abrev=@abr,est=@est where codLP=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@ley", SqlDbType.VarChar, 20).Value = txtLey.Text.Trim()
        cmUpdateTable.Parameters.Add("@abr", SqlDbType.VarChar, 3).Value = txtAbr.Text.Trim()
        If lbEfe.SelectedIndex = 0 Then 'SI
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else  'NO
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TLeyendaPla where codLP=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub txtLey_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLey.GotFocus, txtLey.MouseClick
        txtLey.SelectAll()
    End Sub

    Private Sub txtAbr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbr.GotFocus, txtAbr.MouseClick
        txtAbr.SelectAll()
    End Sub
End Class
