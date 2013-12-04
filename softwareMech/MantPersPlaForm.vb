Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class MantPersPlaForm
    Dim BindingSource1 As New BindingSource

    Private Sub MantPersPlaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantPersPlaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select 
        Dim sele As String = "select codPer,nom,sexo,dni,fecNac,cargo,est,dir,fono,email,estado,codCar,nombre,apePat,apeMat from VPersPlanilla"
        crearDataAdapterTable(daTPers, sele)
        sele = "select codCar,cargo from TCargoPers order by cargo"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'procedimiento para instanciar el dataSet 
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTPers.Fill(dsAlmacen, "VPersPlanilla")
            daTabla1.Fill(dsAlmacen, "TCargoPers")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VPersPlanilla"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            BindingSource1.Sort = "nom"
            ModificarColumnasDGV()

            cbCargo.DataSource = dsAlmacen
            cbCargo.DisplayMember = "TCargoPers.cargo"
            cbCargo.ValueMember = "codCar"

            configurarColorControl()
            cbBuscar.SelectedIndex = 0
            txtBuscar.Focus()

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nombre y Apellidos"
            .Columns(1).Width = 250
            .Columns(2).Width = 30
            .Columns(2).HeaderText = "S"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 65
            .Columns(3).HeaderText = "DNI"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 70
            .Columns(4).HeaderText = "FecNac"
            .Columns(5).Width = 120
            .Columns(5).HeaderText = "Cargo"
            .Columns(6).Width = 60
            .Columns(6).HeaderText = "Estado"
            .Columns(7).Width = 300
            .Columns(7).HeaderText = "Dirección"
            .Columns(8).Width = 200
            .Columns(8).HeaderText = "Telefono-Celular"
            .Columns(9).Width = 200
            .Columns(9).HeaderText = "Email"
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
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
        Label5.ForeColor = ForeColorLabel
        Label6.ForeColor = ForeColorLabel
        Label7.ForeColor = ForeColorLabel
        Label8.ForeColor = ForeColorLabel
        Label9.ForeColor = ForeColorLabel
        Label10.ForeColor = ForeColorLabel
        Label11.ForeColor = ForeColorLabel
        Label12.ForeColor = ForeColorLabel
        Label13.ForeColor = ForeColorLabel
        btnBuscar.ForeColor = ForeColorButtom
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
            txtNom.Text = BindingSource1.Item(BindingSource1.Position)(12)
            txtPat.Text = BindingSource1.Item(BindingSource1.Position)(13)
            txtMat.Text = BindingSource1.Item(BindingSource1.Position)(14)
            If BindingSource1.Item(BindingSource1.Position)(2) = "M" Then
                cbSexo.SelectedIndex = 0
            Else 'Femenino
                cbSexo.SelectedIndex = 1
            End If
            txtDNI.Text = BindingSource1.Item(BindingSource1.Position)(3)
            date1.Value = BindingSource1.Item(BindingSource1.Position)(4)
            txtDir.Text = BindingSource1.Item(BindingSource1.Position)(7)
            txtFono.Text = BindingSource1.Item(BindingSource1.Position)(8)
            txtEma.Text = BindingSource1.Item(BindingSource1.Position)(9)
            If BindingSource1.Item(BindingSource1.Position)(10) = 1 Then 'Activo
                lbTabla2.SelectedIndex = 0
            Else 'Inactivo
                lbTabla2.SelectedIndex = 1
            End If
            cbCargo.SelectedValue = BindingSource1.Item(BindingSource1.Position)(11)
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim campo As String
        If cbBuscar.SelectedIndex = 0 Then
            campo = "nom"
        End If
        If cbBuscar.SelectedIndex = 1 Then
            campo = "dni"
            If txtBuscar.Text.Trim() = "" Then
                txtBuscar.Focus()
                Exit Sub
            End If
            If ValidaDocIdent(txtBuscar.Text.Trim()) Then
                txtBuscar.errorProv()
                Exit Sub
            End If
        End If

        If (cbBuscar.SelectedIndex = 1) Then 'dni
            BindingSource1.Filter = campo & "='" & txtBuscar.Text.Trim() & "'"
        Else        'nombre cliente
            BindingSource1.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%'"
        End If

        If BindingSource1.Count > 0 Then
            dgTabla1.Focus()
            Me.AcceptButton = Me.btnNuevo
            StatusBarClass.messageBarraEstado("")
        Else
            txtBuscar.Focus()
            txtBuscar.SelectAll()
            StatusBarClass.messageBarraEstado(" NO EXISTE PERSONAL CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
    End Sub

    Private Sub cbBuscar_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBuscar.SelectedIndexChanged
        txtBuscar.Focus()
    End Sub

    Private Sub txtBuscar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtBuscar.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        Me.AcceptButton = Me.btnBuscar
    End Sub

    Private Sub desactivarControles1()
        Panel2.Enabled = False
        Panel3.Enabled = False
        If vfNuevo1 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
            lbTabla2.Enabled = True
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
        Panel1.Enabled = True
    End Sub

    Private Sub activarControles1()
        Panel2.Enabled = True
        Panel3.Enabled = True
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
        lbTabla2.Enabled = False
        txtNom.ReadOnly = True
        txtPat.ReadOnly = True
        txtMat.ReadOnly = True
        txtDNI.ReadOnly = True
        txtDir.ReadOnly = True
        txtFono.ReadOnly = True
        txtEma.ReadOnly = True
    End Sub

    Private Sub limpiarText1()
        txtNom.ReadOnly = False
        txtPat.ReadOnly = False
        txtMat.ReadOnly = False
        txtDNI.ReadOnly = False
        txtDir.ReadOnly = False
        txtFono.ReadOnly = False
        txtEma.ReadOnly = False
        If vfNuevo1 = "guardar" Then
            txtNom.Clear()
            txtPat.Clear()
            txtMat.Clear()
            txtDNI.Clear()
            txtDir.Clear()
            txtFono.Clear()
            txtEma.Clear()
        End If
    End Sub

    Private Function ValidarCampos() As Boolean
        'Creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtNom.Text.Trim, 3) Then
            txtNom.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtPat.Text.Trim, 3) Then
            txtPat.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtMat.Text.Trim, 3) Then
            txtMat.errorProv()
            Return True
        End If
        If ValidaDocIdent(txtDNI.Text.Trim()) Then
            txtDNI.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtDir.Text.Trim, 3) Then
            txtDir.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarExiste1(ByVal dni As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TPersona where dni='" & dni & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCli1(ByVal dni As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select 'Personal => '+nom from VPersPlanilla where dni='" & dni & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            limpiarText1()
            txtNom.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else   ' guardar
            If ValidarCampos() Then
                Exit Sub
            End If

            If BindingSource1.Find("nom", txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste personal: " & txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtNom.Focus()
                txtNom.SelectAll()
                Exit Sub
            End If

            If txtDNI.Text.Trim() <> "" Then
                If recuperarExiste1(txtDNI.Text.Trim()) > 0 Then
                    MessageBox.Show("Proceso Denegado, " & recuperarCli1(txtDNI.Text.Trim) & Chr(13) & "Fue asignado al DNI : " & txtDNI.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                    txtDNI.Focus()
                    'btnCancelar.PerformClick()
                    Exit Sub
                End If
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                vfCampo1 = txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim()

                'TPersonal
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

                'Actualizando el dataSet 
                dsAlmacen.Tables("VPersPlanilla").Clear()
                daTPers.Fill(dsAlmacen, "VPersPlanilla")

                btnCancelar.PerformClick()
                BindingSource1.RemoveFilter() 'quita filtro de registros
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nom", vfCampo1)

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
            limpiarText1()
            enlazarText()
            txtNom.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If

            vfCampo1 = dgTabla1.Rows(BindingSource1.Position).Cells(1).Value
            If vfCampo1.ToUpper() <> (txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim()).ToUpper.Trim() Then
                If BindingSource1.Find("nom", txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya exíste personal: " & txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtNom.Focus()
                    txtNom.SelectAll()
                    Exit Sub
                End If
            End If

            If txtDNI.Text.Trim() <> "" Then
                If txtDNI.Text.Trim().ToUpper() <> BindingSource1.Item(BindingSource1.Position)(3).ToString.ToUpper() Then
                    If recuperarExiste1(txtDNI.Text.Trim()) > 0 Then
                        MessageBox.Show("Proceso Denegado, " & recuperarCli1(txtDNI.Text.Trim) & Chr(13) & "Fue asignado al DNI : " & txtDNI.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                        txtDNI.Focus()
                        Exit Sub
                    End If
                End If
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                'TPersona
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampo1 = txtNom.Text.Trim() & " " & txtPat.Text.Trim() & " " & txtMat.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("VPersPlanilla").Clear()
                daTPers.Fill(dsAlmacen, "VPersPlanilla")

                btnCancelar.PerformClick()
                BindingSource1.RemoveFilter() 'quita filtro de registros
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nom", vfCampo1)

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

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TPersObra where codPer=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE INSTANCIAS EN OBRAS...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtNom.Focus()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TPersona
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
            dsAlmacen.Tables("VPersPlanilla").Clear()
            daTPers.Fill(dsAlmacen, "VPersPlanilla")

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
        cmInserTable1.CommandText = "insert TPersona(nombre,apePat,apeMat,dni,sexo,fecNac,dir,fono,email,estado,codCar) values(@nom,@pat,@mat,@dni,@sex,@fec,@dir,@fono,@ema,1,@codC)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = txtNom.Text.Trim()
        cmInserTable1.Parameters.Add("@pat", SqlDbType.VarChar, 20).Value = txtPat.Text.Trim()
        cmInserTable1.Parameters.Add("@mat", SqlDbType.VarChar, 20).Value = txtMat.Text.Trim()
        cmInserTable1.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDNI.Text.Trim()
        cmInserTable1.Parameters.Add("@sex", SqlDbType.VarChar, 3).Value = cbSexo.Text.Trim()
        cmInserTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable1.Parameters.Add("@dir", SqlDbType.VarChar, 60).Value = txtDir.Text.Trim()
        cmInserTable1.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmInserTable1.Parameters.Add("@ema", SqlDbType.VarChar, 100).Value = txtEma.Text.Trim().ToLower()
        cmInserTable1.Parameters.Add("@codC", SqlDbType.Int, 0).Value = cbCargo.SelectedValue
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TPersona set nombre=@nom,apePat=@pat,apeMat=@mat,dni=@dni,sexo=@sex,fecNac=@fec,dir=@dir,fono=@fono,email=@ema,estado=@est,codCar=@codC where codPer=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = txtNom.Text.Trim()
        cmUpdateTable.Parameters.Add("@pat", SqlDbType.VarChar, 20).Value = txtPat.Text.Trim()
        cmUpdateTable.Parameters.Add("@mat", SqlDbType.VarChar, 20).Value = txtMat.Text.Trim()
        cmUpdateTable.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDNI.Text.Trim()
        cmUpdateTable.Parameters.Add("@sex", SqlDbType.VarChar, 3).Value = cbSexo.Text.Trim()
        cmUpdateTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable.Parameters.Add("@dir", SqlDbType.VarChar, 60).Value = txtDir.Text.Trim()
        cmUpdateTable.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmUpdateTable.Parameters.Add("@ema", SqlDbType.VarChar, 100).Value = txtEma.Text.Trim().ToLower()
        If lbTabla2.SelectedIndex = 0 Then 'Activo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else  'Inactivo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable.Parameters.Add("@codC", SqlDbType.Int, 0).Value = cbCargo.SelectedValue
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = dgTabla1.Rows(BindingSource1.Position).Cells(0).Value
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TPersona where codPer=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = dgTabla1.Rows(BindingSource1.Position).Cells(0).Value
    End Sub
End Class
