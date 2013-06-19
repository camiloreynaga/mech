Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantPersonalForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' Tipo de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource3 As New BindingSource


    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Dim vfCampoDni As String
    Dim vfModificar As String = "modificar"
    Dim cmUpdateTable15 As SqlCommand
    Dim cmInserTable1 As SqlCommand
    Dim cmUpdateTable As SqlCommand
    Dim cmDeleteTable As SqlCommand
    Dim cmDeleteTable1 As SqlCommand
    Dim cmInserTable13 As SqlCommand
    Dim cmUpdateTable1 As SqlCommand
    Dim cmUpdateTable13 As SqlCommand

#Region "Metodos"

    Private Sub botones(ByVal estado As Integer)

        Select Case estado
            Case 1
                'caso 1 inicial
                btnNuevo.Enabled = True
                btnModificar.Enabled = False
                btnCancelar.Enabled = False
                btnEliminar.Enabled = False
                btnCerrar.Enabled = True
                'si fila seleccionada
            Case 2
                btnNuevo.Enabled = True
                btnModificar.Enabled = True
                btnCancelar.Enabled = True
                btnEliminar.Enabled = True
                btnCerrar.Enabled = False
            Case 3 ' si click en nuevo
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnCancelar.Enabled = True
                btnEliminar.Enabled = False
                btnCerrar.Enabled = False
            Case 4 ' si click en Modificar
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnCancelar.Enabled = True
                btnEliminar.Enabled = False
                btnCerrar.Enabled = False
            Case 5 ' si click en 
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnCancelar.Enabled = True
                btnEliminar.Enabled = False
                btnCerrar.Enabled = False

        End Select

       

      


    End Sub


    ''' <summary>
    ''' modificar los encabezados de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nombre"
            '.Columns(1).Width = 100
            .Columns(2).HeaderText = "Apellido"
            '.Columns(2).Width = 100
            .Columns(3).HeaderText = "Usuario"
            .Columns(4).HeaderText = "Cargo"
            '.Columns(6).HeaderText = "Cargo"
            .Columns(8).HeaderText = "Estado"
            .Columns(10).HeaderText = "DNI"
            .Columns(11).HeaderText = "Dirección"
            .Columns(12).HeaderText = "Telefono"
            '.Columns(7).Width = 70
            .Columns(13).HeaderText = "email"
            .Columns(14).HeaderText = "Password"

            .Columns(5).HeaderText = "Tipo_Usu"
            .Columns(7).HeaderText = "cod cargo"
            .Columns(9).HeaderText = "cod estado"

            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(9).Visible = False
            .Columns("codLugar").Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    ''' <summary>
    ''' Configurar los colores del control
    ''' </summary>
    ''' <remarks></remarks>
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
        Label7.ForeColor = ForeColorLabel
        Label8.ForeColor = ForeColorLabel
        Label9.ForeColor = ForeColorLabel
        Label10.ForeColor = ForeColorLabel
        Label11.ForeColor = ForeColorLabel
        Label12.ForeColor = ForeColorLabel
        Label13.ForeColor = ForeColorLabel
        Label14.ForeColor = ForeColorLabel
        Label15.ForeColor = ForeColorLabel
        Label16.ForeColor = ForeColorLabel


        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        btnAceptar.ForeColor = ForeColorButtom
        btnQuitar.ForeColor = ForeColorButtom
    End Sub

    ''' <summary>
    ''' muestra nombre Personal,cargo,estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarText()
        If BindingSource1.Count = 0 Then
            'desEnlazarText()
        Else
            txtNom.Text = BindingSource1.Item(BindingSource1.Position)(1) 'Nombre de personal
            txtApe.Text = BindingSource1.Item(BindingSource1.Position)(2) 'apellidos
            txtDni.Text = BindingSource1.Item(BindingSource1.Position)(10) 'dni
            txtDirec.Text = BindingSource1.Item(BindingSource1.Position)(11) 'direccion
            txtFono.Text = BindingSource1.Item(BindingSource1.Position)(12) ' telefono
            txtEmail.Text = BindingSource1.Item(BindingSource1.Position)(13) ' email
            cbCargo.SelectedValue = BindingSource1.Item(BindingSource1.Position)(7) 'cargo

            cbTipoUser.SelectedValue = BindingSource1.Item(BindingSource1.Position)(5) 'tipo usuario

            txtUsu.Text = BindingSource1.Item(BindingSource1.Position)(3)

            cbUbi.SelectedValue = BindingSource1.Item(BindingSource1.Position)(16)
            If BindingSource1.Item(BindingSource1.Position)(9) = 1 Then 'Activo
                lbTabla2.SelectedIndex = 0
            Else 'Inactivo
                lbTabla2.SelectedIndex = 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' desactiva los botones de acuerdo a la operacion que se realiza
    ''' </summary>
    ''' <remarks></remarks>
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

        'cbTipoUser.Enabled = False
        cbCargo.Enabled = True
        cbUbi.Enabled = True
        Panel1.Enabled = True


    End Sub
    ''' <summary>
    ''' Activa los botones inciales
    ''' </summary>
    ''' <remarks></remarks>
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

        'lista activo/inactivo
        lbTabla2.Enabled = False

        txtNom.ReadOnly = True
        txtApe.ReadOnly = True
        txtDirec.ReadOnly = True
        txtDni.ReadOnly = True
        txtEmail.ReadOnly = True
        txtFono.ReadOnly = True

        'cbCargo.Enabled = False
        ' cbUbi.Enabled = False
        'cbTipoUser.Enabled = True
    End Sub
    'limpia los textbox LITO
    Private Sub limpiarText1()
        txtNom.ReadOnly = False
        txtApe.ReadOnly = False
        txtDirec.ReadOnly = False
        txtDni.ReadOnly = False
        txtFono.ReadOnly = False
        txtEmail.ReadOnly = False
        If vfNuevo1 = "guardar" Then
            txtNom.Clear()
            txtApe.Clear()
            txtDirec.Clear()
            txtDni.Clear()
            txtFono.Clear()
            txtEmail.Clear()
        End If
    End Sub


    ''' <summary>
    ''' Valida los campos de datos personales 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCampos() As Boolean
        'Creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtNom.Text.Trim, 3) Then
            txtNom.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtApe.Text.Trim, 3) Then
            txtApe.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtDirec.Text.Trim, 3) Then
            txtDirec.errorProv()
            Return True
        End If
        If txtDni.Text.Length > 0 Then
            If ValidaDocIdent(txtDni.Text.Trim) Then
                'txtDni.errorProv()
                MessageBox.Show("Por favor ingrese un DNI valido", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtDni.Focus()
                txtDni.SelectAll()
                Return True
            End If
        Else
            'txtDni.errorProv()
            'Return True

        End If
        'Todo OK RAS
        Return False
    End Function


    ''' <summary>
    ''' verifica si el usuario tiene asignado (lugar Trabajo) ubicación
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarPersLugar(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TPersLugar where codPers=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCotizacion(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TCotizacion where codPers=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarDetalleSolicitud(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleSol where codPers=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarOrdenCompra(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TOrdenCompra where codPers=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSolicitud(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TSolicitud where codPers=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    ''' <summary>
    ''' devuelve el codigo de tipo de cargo (o= corporativo 1 = obra)
    ''' </summary>
    ''' <param name="codUsuario">codigo de tipo de usuario</param>
    ''' <returns>tipo de cargo</returns>
    ''' <remarks></remarks>
    Private Function recuperarTipoCargo(ByVal codUsuario As Integer) As Integer

        Return BindingSource3.Item(codUsuario)(2).ToString()

    End Function


    ''' <summary>
    ''' ingresa datos en la tabla Personal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        'Query para la inserción de datos en sql..
        cmInserTable1.CommandText = "insert into TPersonal(usuario,pass,codTipU,nombre,apellido,codCar,dni,dir,fono,email,estado) values('','',@codTipoUsuario,@nom,@ape,@cargo,@dni,@dir,@fono,@email,1)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@codTipoUsuario", SqlDbType.Int, 0).Value = 0 ' usuario por defecto0 0
        cmInserTable1.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = txtNom.Text.Trim()
        cmInserTable1.Parameters.Add("@ape", SqlDbType.VarChar, 30).Value = txtApe.Text.Trim()
        cmInserTable1.Parameters.Add("@cargo", SqlDbType.Int, 0).Value = cbCargo.SelectedValue
        cmInserTable1.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDni.Text.Trim()
        cmInserTable1.Parameters.Add("@dir", SqlDbType.VarChar, 60).Value = txtDirec.Text.Trim()
        cmInserTable1.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmInserTable1.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = txtEmail.Text.Trim()
    End Sub

    ''' <summary>
    ''' actualiza datos en la tabla personal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TPersonal set estado=@est,nombre=@nom,apellido=@ape,codCar=@codCargo,dni=@dni,dir=@dir,fono=@fono,email=@email where codPers=@codPers"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = dgTabla1.Rows(BindingSource1.Position).Cells(0).Value
        If lbTabla2.SelectedIndex = 0 Then 'Activo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else  'Inactivo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
        'Revisar si no hace el ingreso correcto
        cmUpdateTable.Parameters.Add("@codCargo", SqlDbType.Int, 0).Value = cbCargo.SelectedValue
        cmUpdateTable.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = txtNom.Text.Trim()
        cmUpdateTable.Parameters.Add("@ape", SqlDbType.VarChar, 30).Value = txtApe.Text.Trim()
        cmUpdateTable.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDni.Text.Trim()
        cmUpdateTable.Parameters.Add("@dir", SqlDbType.VarChar, 60).Value = txtDirec.Text.Trim()

        cmUpdateTable.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmUpdateTable.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = txtEmail.Text.Trim()

    End Sub

    ''' <summary>
    ''' elimina datos en la tabla personal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TPersonal where codPers=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = dgTabla1.Rows(BindingSource1.Position).Cells(0).Value
    End Sub

    ''' <summary>
    ''' valida campos para acceso de usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCampos1() As Boolean
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtUsu.Text.Trim(), 3) Then
            txtUsu.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 3 caracteres...")
            Return True
        End If

        If cbTipoUser.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione Cargo de usuario", nomNegocio, Nothing, MessageBoxIcon.Error)
            cbTipoUser.Focus()
            Return True
        End If

        If validaCampoVacioMinCaracNoNumer(txtCon.Text.Trim(), 5) Then
            txtCon.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 5 caracteres y alfanumericos...")
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtCon1.Text.Trim(), 5) Then
            txtCon1.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 5 caracteres y alfanumericos...")
            Return True
        End If
        If ValidaContraseñaRepetidaIgual(txtCon.Text.Trim(), txtCon1.Text.Trim()) Then
            MessageBox.Show("Error contraseñas diferentes, digite contraseñas iguales...", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtCon.Focus()
            Return True
        End If

        

        If cbUbi.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione Ubicación", nomNegocio, Nothing, MessageBoxIcon.Error)
            cbUbi.Focus()
            Return True
        End If


        'Todo OK
        Return False
    End Function

    ''' <summary>
    ''' eliminar datos en la tabla TPersLugar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TPersLugar where codPers=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub


    ''' <summary>
    ''' inserta datos en la tabla TPersLugar
    ''' </summary>
    ''' <param name="codPers"></param>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Private Sub comandoInsert13(ByVal codPers As Short, ByVal codigo As String)
        cmInserTable13 = New SqlCommand
        cmInserTable13.CommandType = CommandType.Text
        cmInserTable13.CommandText = "insert TPersLugar(codPers,codigo) values(@codPers,@cod)"
        cmInserTable13.Connection = Cn
        cmInserTable13.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = codPers
        cmInserTable13.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = codigo
    End Sub

    ''' <summary>
    ''' Actualiza la tabla TPersonal, con datos de Usuario, password y tipo de usuario 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TPersonal set usuario=@usu,pass=@pas1,codTipU=@codTipU where codPers=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@usu", SqlDbType.VarChar, 20).Value = txtUsu.Text.Trim()
        cmUpdateTable1.Parameters.Add("@pas1", SqlDbType.VarChar, 20).Value = txtCon.Text.Trim()
        cmUpdateTable1.Parameters.Add("@codTipU", SqlDbType.Int, 0).Value = cbTipoUser.SelectedValue
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    ''' <summary>
    ''' Actualiza la tabla TPersonal, con datos de Usuario, password y tipo de usuario
    ''' </summary>
    ''' <param name="usu">usuario</param>
    ''' <param name="con">contraseña</param>
    ''' <remarks></remarks>
    Private Sub comandoUpdate1(ByVal usu As String, ByVal con As String)
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TPersonal set usuario=@usu,pass=@pas1,codTipU=@codTipU where codPers=@cod"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@usu", SqlDbType.VarChar, 20).Value = usu
        cmUpdateTable13.Parameters.Add("@pas1", SqlDbType.VarChar, 20).Value = con
        cmUpdateTable13.Parameters.Add("@codTipU", SqlDbType.Int, 0).Value = 0
        cmUpdateTable13.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    ''' <summary>
    ''' refresca los datos de la grilla Personal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefrescarGrilla()
        'dsAlmacen.Clear()
        'dsAlmacen.Dispose()
        'Actualizando el dataSet 
        vfCampo1 = txtNom.Text.Trim()
        dsAlmacen.Tables("VPersonal").Clear()
        daTPers.Fill(dsAlmacen, "VPersonal")

        BindingSource1.Position = BindingSource1.Find("nombre", vfCampo1)
    End Sub

#End Region

#Region "Eventos Form"

    Private Sub MantPersonalForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        ' Dim sele As String = "select codPers,usuario,pass,nombre,apellido,dni,dir,fono,email,cargo,codCar,tipo,codTipU,estado1,estado from VPersonal where codTipU>0 order by nombre"  'codUsu=1 Ruddy RAS
        Dim sele As String = "select codPers,nombre,apellido,usuario,tipo,codTipU,cargo,codCar,estado1,estado,dni,dir,fono,email,password,ubicacion,codLugar from VPersonal order by nombre"
        crearDataAdapterTable(daTPers, sele)

        sele = "select codTipU,tipo,tipoCargo from TTipoUsu " ' order by tipo"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select distinct codigo,nombre from VLugarTrabajo"
        crearDataAdapterTable(daVSuc, sele)

        sele = "Select codCar,cargo from TCargo order by cargo"
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llena el dataSet con los dataAdapter
            daTPers.Fill(dsAlmacen, "VPersonal")
            daTabla1.Fill(dsAlmacen, "TTipoUsu")
            daVSuc.Fill(dsAlmacen, "VLugarTrabajo")
            daTabla2.Fill(dsAlmacen, "TCargo")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VPersonal"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()


            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TTipoUsu"
            cbTipoUser.DataSource = BindingSource3 'dsAlmacen
            cbTipoUser.DisplayMember = "tipo" '"TTipoUsu.tipo"
            cbTipoUser.ValueMember = "codTipU"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VLugarTrabajo"
            cbUbi.DataSource = BindingSource2
            cbUbi.DisplayMember = "nombre"
            cbUbi.ValueMember = "codigo"

            cbCargo.DataSource = dsAlmacen
            cbCargo.DisplayMember = "TCargo.cargo"
            cbCargo.ValueMember = "TCargo.codCar"

            configurarColorControl()

            ' poniendo invisible a los combos de ubicacion, mientras está seleccionado Administrador en tipo de usuario
            cbUbi.Visible = False
            Label5.Visible = False

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub MantPersonalForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
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

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

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
            'revisa la no duplicidad de DNI
            If txtDni.Text.Length > 0 Then
                If BindingSource1.Find("dni", txtDni.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya existe el número de DNI: " & txtDni.Text.Trim() & Chr(13) & "Cambie de DNI... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtDni.Focus()
                    txtDni.SelectAll()
                    Exit Sub
                End If
            End If
            

            'revisar validación de nombre
            If BindingSource1.Find("nombre", txtNom.Text.Trim()) >= 0 And BindingSource1.Find("apellido", txtApe.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe personal: " & txtNom.Text.Trim() & " " & txtApe.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtNom.Focus()
                txtNom.SelectAll()
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                vfCampo1 = txtNom.Text.Trim()

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
                dsAlmacen.Tables("VPersonal").Clear()
                daTPers.Fill(dsAlmacen, "VPersonal")

                btnCancelar.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nombre", vfCampo1)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")
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

         

            'revisa la no duplicidad de DNI
            If txtDni.Text.Length > 0 Then
                vfCampoDni = dgTabla1.Rows(BindingSource1.Position).Cells(10).Value
                If vfCampoDni.ToUpper() <> txtDni.Text.ToUpper().Trim() Then
                    If BindingSource1.Find("dni", txtDni.Text.Trim()) >= 0 Then
                        MessageBox.Show("Ya existe ese número de DNI: " & txtDni.Text.Trim() & Chr(13) & "Cambie de DNI... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                        txtDni.Focus()
                        txtDni.SelectAll()
                        Exit Sub
                    End If
                End If
            End If


            vfCampo1 = dgTabla1.Rows(BindingSource1.Position).Cells(1).Value
            If vfCampo1.ToUpper() <> txtNom.Text.ToUpper.Trim() Then
                If BindingSource1.Find("nombre", txtNom.Text.Trim()) >= 0 And BindingSource1.Find("apellido", txtApe.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya existe personal: " & txtNom.Text.Trim() & " " & txtApe.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtNom.Focus()
                    txtNom.SelectAll()
                    Exit Sub
                End If
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

                'If dgTabla1.Rows(BindingSource1.Position).Cells(14).Value <> "" Then       'verificando si a usuario ya se le asigno acceso....
                '    If cbTipoUser.SelectedValue <> dgTabla1.Rows(BindingSource1.Position).Cells(5).Value Then
                '        If (cbTipoUser.SelectedValue = 1) And (dgTabla1.Rows(BindingSource1.Position).Cells(5).Value <> 1) Then    '1=administrador
                '            MsgBox("cambiando de usuario a administrador")
                '            'Tabla TUsuUbi
                '            comandoDelete1()
                '            cmDeleteTable1.Transaction = myTrans
                '            cmDeleteTable1.ExecuteNonQuery()
                '        Else
                '            If (cbTipoUser.SelectedValue = 2) And (dgTabla1.Rows(BindingSource1.Position).Cells(5).Value <> 2) Then    '2=Sudadministrador
                '                MsgBox("cambiando de usuario a Sudadministrador")
                '                'Tabla TUsuUbi
                '                comandoDelete1()
                '                cmDeleteTable1.Transaction = myTrans
                '                cmDeleteTable1.ExecuteNonQuery()
                '            Else
                '                If (cbTipoUser.SelectedValue <> 1) And (dgTabla1.Rows(BindingSource1.Position).Cells(5).Value = 1) Then
                '                    MsgBox("cambiando de administrador a usuario")
                '                    'TUsuario
                '                    comandoUpdate15()
                '                    cmUpdateTable15.Transaction = myTrans
                '                    cmUpdateTable15.ExecuteNonQuery()
                '                End If
                '                If (cbTipoUser.SelectedValue <> 1) And (dgTabla1.Rows(BindingSource1.Position).Cells(5).Value = 2) Then
                '                    MsgBox("cambiando de Sudadministrador a usuario")
                '                    'TUsuario
                '                    comandoUpdate15()
                '                    cmUpdateTable15.Transaction = myTrans
                '                    cmUpdateTable15.ExecuteNonQuery()
                '                End If
                '            End If
                '        End If
                '    End If
                'End If

                'TUsuario
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampo1 = txtNom.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("VPersonal").Clear()
                daTPers.Fill(dsAlmacen, "VPersonal")

                btnCancelar.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nombre", vfCampo1)

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

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        ' si personal tiene asigando una ubicacion
        If cbTipoUser.SelectedValue = 1 OrElse cbTipoUser.SelectedValue = 2 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE ACCESO A UBICACIONES...")
            Exit Sub
        Else
            If recuperarPersLugar(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
                StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE ACCESO A UBICACIONES...")
                Exit Sub
            End If
        End If

        If recuperarCotizacion(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE COTIZACIONES REGISTRADAS...")
            Exit Sub
        End If

        If recuperarDetalleSolicitud(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE REQUERIMIENTOS REGISTRADOS...")
            Exit Sub
        End If

        If recuperarOrdenCompra(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE ORDENES DE COMPRA REGISTRADAS...")
            Exit Sub
        End If

        If recuperarSolicitud(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE SOLICITUDES REGISTRADAS...")
            Exit Sub
        End If


        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            'Tabla TUsuario
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
            dsAlmacen.Tables("VPersonal").Clear()
            daTPers.Fill(dsAlmacen, "VPersonal")

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

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If BindingSource1.Position = -1 Then
            MessageBox.Show("No existe registro a procesar...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(14) <> "" Then   'usuario ya asignado a ubicación
            MessageBox.Show("USUARIO YA FUE ASIGNADO A UBICACIÓN...", "SSP SAC", Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(9) = 0 Then   'Inactivo
            MessageBox.Show("Proceso denegado, usuario esta en el estado [Inactivo]...", "SSP SAC", Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ValidarCampos1() Then
            Exit Sub
        End If

        'Funcion recuperarUsu... creado en el modulo varFuncPublicasModule
        Dim codigo As Object = recuperarUsu1(txtUsu.Text.Trim(), txtCon.Text.Trim())
        If codigo >= 1 Then       'Ya existe usuario con la contrasseña
            MessageBox.Show("YA EXISTE USUARIO CON LA CONTRASEÑA ASIGNADA." & Chr(13) & "CAMBIE NOMBRE DE USUARIO O CONTRASEÑA...", "SSP SAC", Nothing, MessageBoxIcon.Information)
            txtUsu.Focus()
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Está seguro de asignar acceso a " & BindingSource1.Item(BindingSource1.Position)(1) & " " & BindingSource1.Item(BindingSource1.Position)(2) & " a " & cbUbi.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            'actualizando TUsuario
            comandoUpdate1()
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            'verificando el tipo de usuario 


            If recuperarTipoCargo(cbTipoUser.SelectedIndex) = 1 Then '0=Coporativo 1=Obra              
                'TPersLugar
                ' ingresando persona por lugar de trabajo 
                comandoInsert13(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue)
                cmInserTable13.Transaction = myTrans
                If cmInserTable13.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            MessageBox.Show("Fue asignado con exito acceso a usuario...", nomNegocio, Nothing, MessageBoxIcon.Information)
            wait.Close()
        Catch f As Exception
            wait.Close()
            'deshace la transaccion
            myTrans.Rollback()
            MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
        End Try
        'Me.Close()
        RefrescarGrilla()
    End Sub


    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        If BindingSource1.Position = -1 Then
            MessageBox.Show("No existe usuario a quitar...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(14) = "" Then
            MessageBox.Show("USUARIO ACTUALMENTE NO TIENE ACCESO...", "SOLUCIONES SOFTWARE PERU", Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Está seguro de quitar acceso a " & BindingSource1.Item(BindingSource1.Position)(1) & " " & BindingSource1.Item(BindingSource1.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            'Tabla TUsuUbi
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            cmDeleteTable1.ExecuteNonQuery()

            'actualizando TUsuario
            comandoUpdate1("", "")
            cmUpdateTable13.Transaction = myTrans
            If cmUpdateTable13.ExecuteNonQuery() < 1 Then
                wait.Close()
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            MessageBox.Show("Fue quitado con exito el acceso de usuario...", nomNegocio, Nothing, MessageBoxIcon.Information)
            wait.Close()
        Catch f As Exception
            wait.Close()
            'deshace la transaccion
            myTrans.Rollback()
            MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
        End Try
        'Me.Close()
        RefrescarGrilla()
    End Sub

    Private Sub txtDni_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDni.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cbTipoUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoUser.SelectedIndexChanged
        If cbTipoUser.SelectedIndex > -1 Then
            If cbUbi.Items.Count > 0 Then


                Dim value As Object = recuperarTipoCargo(cbTipoUser.SelectedIndex)
                If value.ToString() <> "System.Data.DataViewManagerListItemTypeDescriptor" Then
                    If value.ToString() <> "System.Data.DataRowView" Then
                        If value = 0 Then 'OrElse value = 2 Then
                            Label5.Visible = False
                            cbUbi.Visible = False
                            cbUbi.SelectedIndex = 0
                        Else
                            Label5.Visible = True
                            cbUbi.Visible = True
                            cbUbi.SelectedIndex = 0
                        End If
                    End If


                End If
            End If
        Else
            Label5.Visible = False
            cbUbi.Visible = False
        End If
    End Sub

    Private Sub lbTabla2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTabla2.SelectedIndexChanged
        If cbTipoUser.SelectedValue = 1 OrElse cbTipoUser.SelectedValue = 2 Then
            If lbTabla2.SelectedIndex = 1 Then
                StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE ACCESO A TODAS LAS UBICACIONES, DEBE QUITAR EL ACCESO PARA CAMBIAR ESTADO...")
                lbTabla2.SelectedIndex = 0
                Exit Sub
            End If

        Else
            If lbTabla2.SelectedIndex = 1 Then
                If recuperarPersLugar(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0 Then
                    StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PERSONAL TIENE ACCESO A UBICACIONES DEBE QUITAR EL ACCESO PARA CAMBIAR ESTADO...")
                    lbTabla2.SelectedIndex = 0
                    Exit Sub
                End If
            End If
            
        End If

    End Sub

    Private Sub dgTabla1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellClick
        enlazarText()
        txtCon.Text = ""
        txtCon1.Text = ""

    End Sub

    Private Sub dgTabla1_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellEnter
        enlazarText()
        txtCon.Text = ""
        txtCon1.Text = ""

    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged

        'BindingSource1.Find("nombre", txtBuscar.Text.Trim())
        BindingSource1.Filter = "nombre like '" & txtBuscar.Text.Trim() & "%' or apellido like '" & txtBuscar.Text.Trim() & "%'"
    End Sub
#End Region

    Private Sub cbCargo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCargo.SelectedIndexChanged
       
    End Sub
End Class
