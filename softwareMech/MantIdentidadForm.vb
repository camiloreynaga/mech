Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantIdentidadForm
    ''' <summary>
    ''' Identidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource
    ''' <summary>
    ''' Tipo Identidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    Private Sub MantIdentidadForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantIdentidadForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,tipoId,razon,ruc,dir,fono,fax,celRpm,email,estado1,repres,dni,estado,idTipId,cuentaBan,cuentaDet from VTipoIdentidad where codIde>1 order by razon" '1 MECH defecto
        crearDataAdapterTable(daTPers, sele)

        sele = "select idTipId,tipoId from TTipoIdent Order by tipoId"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTPers.Fill(dsAlmacen, "VTipoIdentidad")
            daTabla1.Fill(dsAlmacen, "TTipoIdent")

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TTipoIdent"
            lbTabla1.DataSource = BindingSource2
            lbTabla1.DisplayMember = "tipoId"
            lbTabla1.ValueMember = "idTipId"

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VTipoIdentidad"
            BindingSource1.Filter = "idTipId=" & lbTabla1.SelectedValue
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            configurarColorControl()
            'txtCodPers.DataBindings.Add("Text", BindingSource1, "codUsu")
            vfVan1 = True
            cbBuscar.SelectedIndex = 1


            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    Dim vfVan1 As Boolean = False
    Dim vfObs As Short = 1
    Private Sub lbTabla1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTabla1.SelectedIndexChanged
        relacion()
        txtBuscar.Focus()
    End Sub

    Private Sub relacion()
        If vfVan1 And vfObs = 1 Then
            BindingSource1.Filter = "idTipId=" & lbTabla1.SelectedValue
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Identidad"
            .Columns(1).Width = 90
            .Columns(2).HeaderText = "Razon Social"
            .Columns(2).Width = 260
            .Columns(3).Width = 85
            .Columns(3).HeaderText = "RUC"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 320
            .Columns(4).HeaderText = "Dirección"
            .Columns(5).Width = 120
            .Columns(5).HeaderText = "Telefono"
            .Columns(6).Width = 120
            .Columns(6).HeaderText = "Fax"
            .Columns(7).Width = 120
            .Columns(7).HeaderText = "Cel_Rpm"
            .Columns(8).Width = 180
            .Columns(8).HeaderText = "Email"
            .Columns(9).Width = 60
            .Columns(9).HeaderText = "Estado"
            .Columns(10).Width = 200
            .Columns(10).HeaderText = "Contacto"
            .Columns(11).Width = 70
            .Columns(11).HeaderText = "DNI"
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Width = 200
            .Columns(14).HeaderText = "Cuenta Bancaria"
            .Columns("cuentaDet").HeaderText = "Cuenta Detracción"
            .Columns("cuentaDet").Width = 200
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
        Label14.ForeColor = ForeColorLabel
        Label15.ForeColor = ForeColorLabel
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
            txtNom.Text = BindingSource1.Item(BindingSource1.Position)(2)
            txtRuc.Text = BindingSource1.Item(BindingSource1.Position)(3)
            txtDir.Text = BindingSource1.Item(BindingSource1.Position)(4)
            txtFono.Text = BindingSource1.Item(BindingSource1.Position)(5)
            txtFax.Text = BindingSource1.Item(BindingSource1.Position)(6)
            txtCel.Text = BindingSource1.Item(BindingSource1.Position)(7)
            txtEma.Text = BindingSource1.Item(BindingSource1.Position)(8)
            txtRep.Text = BindingSource1.Item(BindingSource1.Position)(10)
            txtDNI.Text = BindingSource1.Item(BindingSource1.Position)(11)
            txtCue.Text = BindingSource1.Item(BindingSource1.Position)(14)
            txtDetraccion.Text = BindingSource1.Item(BindingSource1.Position)(15)
            If BindingSource1.Item(BindingSource1.Position)(12) = 1 Then
                lbTabla2.SelectedIndex = 0 'Activo
            Else
                lbTabla2.SelectedIndex = 1  'Inactivo
            End If
        End If
    End Sub

    Private Sub desactivarControles1()
        Panel2.Enabled = False
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
        lbTabla1.Enabled = True
        Panel1.Enabled = True
    End Sub

    Private Sub activarControles1()
        Panel2.Enabled = True
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
        'lbTabla1.Enabled = False
        lbTabla2.Enabled = False
        txtNom.ReadOnly = True
        txtRuc.ReadOnly = True
        txtDir.ReadOnly = True
        txtFono.ReadOnly = True
        txtFax.ReadOnly = True
        txtCel.ReadOnly = True
        txtEma.ReadOnly = True
        txtRep.ReadOnly = True
        txtDNI.ReadOnly = True
        txtCue.ReadOnly = True
        txtDetraccion.ReadOnly = True
    End Sub

    Private Sub limpiarText2()
        txtNom.Clear()
        txtRuc.Clear()
        txtDir.Clear()
        txtFono.Clear()
        txtFax.Clear()
        txtCel.Clear()
        txtEma.Clear()
        txtRep.Clear()
        txtDNI.Clear()
        txtCue.Clear()
        txtDetraccion.Clear()
    End Sub

    Private Sub limpiarText1()
        txtNom.ReadOnly = False
        txtRuc.ReadOnly = False
        txtDir.ReadOnly = False
        txtFono.ReadOnly = False
        txtFax.ReadOnly = False
        txtCel.ReadOnly = False
        txtEma.ReadOnly = False
        txtRep.ReadOnly = False
        txtDNI.ReadOnly = False
        txtCue.ReadOnly = False
        txtDetraccion.ReadOnly = False
        If vfNuevo1 = "guardar" Then
            txtNom.Clear()
            txtRuc.Clear()
            txtDir.Clear()
            txtFono.Clear()
            txtFax.Clear()
            txtCel.Clear()
            txtEma.Clear()
            txtRep.Clear()
            txtDNI.Clear()
            txtCue.Clear()
            txtDetraccion.Clear()
        End If
    End Sub

    ''' <summary>
    ''' valida datos para indentidad 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCampos() As Boolean
        'Creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtNom.Text.Trim, 3) Then
            txtNom.errorProv()
            Return True
        End If
        'valida campo vaio o menor a 11 digitos en RUC
        If ValidaRUC1(txtRuc.Text.Trim()) Then
            txtRuc.errorProv()
            Return True
        End If
        'Valida campo vacio en dirección
        If validaCampoVacioMinCaracNoNumer(txtDir.Text.Trim, 3) Then
            txtDir.errorProv()
            Return True
        End If
        If ValidaDocIdent(txtDNI.Text.Trim()) Then
            txtDNI.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function
    'Valida la duplicidad de razon social -CR
    Private Function recuperarExisteRazonSoc(ByVal razon As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "SELECT COUNT(razon) from TIdentidad where razon ='" & razon & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function


    ' valida el ruc consultando a la BD
    Private Function recuperarExiste(ByVal ruc As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codIde) from TIdentidad where ruc='" & ruc & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarProd(ByVal ruc As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select tipoId+' => '+razon from VTipoIdentidad where ruc='" & ruc & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarExiste1(ByVal dni As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codIde) from TIdentidad where dni='" & dni & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarProd1(ByVal dni As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select tipoId+' => '+repres from VTipoIdentidad where dni='" & dni & "'"
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

            If recuperarExisteRazonSoc(txtNom.Text.Trim()) > 0 Then
                MessageBox.Show("Ya existe " & lbTabla1.Text.Trim() & " => " & txtNom.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtNom.Focus()
                txtNom.SelectAll()
                Exit Sub
            End If

            If txtRuc.Text.Trim() <> "" Then
                If recuperarExiste(txtRuc.Text.Trim()) > 0 Then
                    MessageBox.Show("Proceso Denegado, " & recuperarProd(txtRuc.Text.Trim) & Chr(13) & "Fue asignado al RUC : " & txtRuc.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                    txtRuc.Focus()
                    'btnCancelar.PerformClick()
                    Exit Sub
                End If
            End If

            If txtDNI.Text.Trim() <> "" Then
                If recuperarExiste1(txtDNI.Text.Trim()) > 0 Then
                    MessageBox.Show("Proceso Denegado, " & recuperarProd1(txtDNI.Text.Trim) & Chr(13) & "Fue asignado al DNI : " & txtDNI.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
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
                vfCampo1 = txtNom.Text.Trim()

                'TIdentidad
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
                dsAlmacen.Tables("VTipoIdentidad").Clear()
                daTPers.Fill(dsAlmacen, "VTipoIdentidad")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("razon", vfCampo1)

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
    'LITO
    ''' <summary>
    ''' devuelva el numero de lugares de trabajo relacionados con una identidad (cliente)
    ''' </summary>
    ''' <param name="codIde"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarLugarTrabajo(ByVal codIde As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(codIde) from TLugarTrabajo where codIde=" & codIde
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    ''' <summary>
    ''' devuelve el nro de cotizaciones relacionadas con una identidad (proveedor)
    ''' </summary>
    ''' <param name="codIde"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarCotizacion(ByVal codIde As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(codIde) from TCotizacion where codIde=" & codIde
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    ''' <summary>
    ''' devuelve el nro de ordenes de compra relacionados con la identidad (proveedor)
    ''' </summary>
    ''' <param name="codIde"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarOrdenCompra(ByVal codIde As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(codIde) from TOrdenCompra where codIde=" & codIde
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    ''' <summary>
    ''' devuelve el nro de ordenes de Desembolso relacionados con la identidad (proveedor)
    ''' </summary>
    ''' <param name="codIde"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarOrdenDesembolso(ByVal codIde As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(codIde) from TOrdenDesembolso where codIde=" & codIde
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function


    'Private Function recuperarOrdenCompra(ByVal codIde As Integer) As Integer
    '    Dim cmdCampo As SqlCommand = New SqlCommand
    '    cmdCampo.CommandType = CommandType.Text
    '    cmdCampo.CommandText = "select COUNT(*) from TOrdenCompra where codIde=" & codIde
    '    cmdCampo.Connection = Cn
    '    Return cmdCampo.ExecuteScalar
    'End Function

    Dim vfModificar As String = "modificar"
    Dim vfIdTip As Integer
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
            vfObs = 0   'para romper la relacion
            vfIdTip = BindingSource1.Item(BindingSource1.Position)(13)
            lbTabla1.SelectedValue = BindingSource1.Item(BindingSource1.Position)(13)
        Else    'Actualizar

            If ValidarCampos() Then
                Exit Sub
            End If

            vfCampo1 = dgTabla1.Rows(BindingSource1.Position).Cells(2).Value
            If vfCampo1.ToUpper() <> txtNom.Text.ToUpper.Trim() Then
                If recuperarExisteRazonSoc(txtNom.Text.Trim()) > 0 Then
                    MessageBox.Show("Ya exíste " & lbTabla1.Text.Trim() & " => " & txtNom.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtNom.Focus()
                    txtNom.SelectAll()
                    Exit Sub
                End If
            End If

            If txtRuc.Text.Trim() <> "" Then
                If txtRuc.Text.Trim().ToUpper() <> BindingSource1.Item(BindingSource1.Position)(3).ToString.ToUpper() Then
                    If recuperarExiste(txtRuc.Text.Trim()) > 0 Then
                        MessageBox.Show("Proceso Denegado, " & recuperarProd(txtRuc.Text.Trim) & Chr(13) & "Fue asignado al RUC : " & txtRuc.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                        txtRuc.Focus()
                        'btnCancelar.PerformClick()
                        Exit Sub
                    End If
                End If
            End If

            If txtDNI.Text.Trim() <> "" Then
                If txtDNI.Text.Trim().ToUpper() <> BindingSource1.Item(BindingSource1.Position)(11).ToString.ToUpper() Then
                    If recuperarExiste1(txtDNI.Text.Trim()) > 0 Then
                        MessageBox.Show("Proceso Denegado, " & recuperarProd1(txtDNI.Text.Trim) & Chr(13) & "Fue asignado al DNI : " & txtDNI.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                        txtDNI.Focus()
                        'btnCancelar.PerformClick()
                        Exit Sub
                    End If
                End If
            End If

            If lbTabla1.SelectedValue <> vfIdTip Then
                If recuperarLugarTrabajo(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
                    MessageBox.Show("Proceso Denegado, Identidad no se puede cambiar a otro Tipo de Identidad" & Chr(13) & "por tener lugar de trabajo Asignado...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    lbTabla1.Focus()
                    btnCancelar.PerformClick()
                    Exit Sub
                End If
            End If

            'If lbTabla1.SelectedValue <> vfIdTip Then
            '    If recuperarOrdenCompra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            '        MessageBox.Show("Proceso Denegado, Identidad no se puede cambiar a otro Tipo de Identidad" & Chr(13) & "por tener Orden de Compra procesados...", nomNegocio, Nothing, MessageBoxIcon.Error)
            '        lbTabla1.Focus()
            '        btnCancelar.PerformClick()
            '        Exit Sub
            '    End If
            'End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                'TIdentidad
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
                dsAlmacen.Tables("VTipoIdentidad").Clear()
                daTPers.Fill(dsAlmacen, "VTipoIdentidad")

                btnCancelar.PerformClick()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("razon", vfCampo1)

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
        limpiarText2()
        vfObs = 1
        relacion()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TOrdenCompra where codIde=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TOrdenCompra where codIdeCli=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount2(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDocCompra where codIde=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount3(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TContrato where codIde=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If
        If dgTabla1.Rows(BindingSource1.Position).Cells(0).Value = 1 Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO... ENTIDAD CLAVE...")
            Exit Sub
        End If
        'para modificar con SOLICITUDES  

        If (recuperarLugarTrabajo(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0) Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... ENTIDAD TIENE LUGAR DE TRABAJO ASIGNADO...")
            Exit Sub
        End If

        If (recuperarCotizacion(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0) Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... ENTIDAD TIENE COTIZACIONES RELACIONADAS...")
            Exit Sub
        End If
        If (recuperarOrdenCompra(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0) Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... ENTIDAD TIENE ORDENES DE COMPRA RELACIONADAS...")
            Exit Sub
        End If
        If (recuperarOrdenDesembolso(dgTabla1.Rows(BindingSource1.Position).Cells(0).Value) > 0) Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... ENTIDAD TIENE ORDENES DE DESEMBOLSO RELACIONADAS...")
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
            dsAlmacen.Tables("VTipoIdentidad").Clear()
            daTPers.Fill(dsAlmacen, "VTipoIdentidad")

            enlazarText()
            limpiarText2()
            vfObs = 1
            relacion()
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
                MessageBox.Show("tipoId de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert TIdentidad(razon,ruc,dir,fono,fax,celRpm,email,repres,dni,estado,idTipId,cuentaBan,cuentaDet) values(@raz,@ruc,@dir,@fono,@fax,@cel,@ema,@rep,@dni,@est,@idT,@cue,@detraccion)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@raz", SqlDbType.VarChar, 60).Value = txtNom.Text.Trim()
        cmInserTable1.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmInserTable1.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDir.Text.Trim()
        cmInserTable1.Parameters.Add("@fono", SqlDbType.VarChar, 30).Value = txtFono.Text.Trim()
        cmInserTable1.Parameters.Add("@fax", SqlDbType.VarChar, 30).Value = txtFax.Text.Trim()
        cmInserTable1.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmInserTable1.Parameters.Add("@ema", SqlDbType.VarChar, 30).Value = txtEma.Text.Trim().ToLower()
        cmInserTable1.Parameters.Add("@rep", SqlDbType.VarChar, 60).Value = txtRep.Text.Trim()
        cmInserTable1.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDNI.Text.Trim()
        cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1 'Activo
        cmInserTable1.Parameters.Add("@idT", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
        cmInserTable1.Parameters.Add("@cue", SqlDbType.VarChar, 60).Value = txtCue.Text.Trim()
        cmInserTable1.Parameters.Add("@detraccion", SqlDbType.VarChar, 60).Value = txtDetraccion.Text.Trim()

    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TIdentidad set razon=@raz,ruc=@ruc,dir=@dir,fono=@fono,fax=@fax,celRpm=@cel,email=@ema,repres=@rep,dni=@dni,estado=@est,idTipId=@idT,cuentaBan=@cue,cuentaDet=@detraccion where codIde=@codIde"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@raz", SqlDbType.VarChar, 60).Value = txtNom.Text.Trim()
        cmUpdateTable.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmUpdateTable.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDir.Text.Trim()
        cmUpdateTable.Parameters.Add("@fono", SqlDbType.VarChar, 30).Value = txtFono.Text.Trim()
        cmUpdateTable.Parameters.Add("@fax", SqlDbType.VarChar, 30).Value = txtFax.Text.Trim()
        cmUpdateTable.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmUpdateTable.Parameters.Add("@ema", SqlDbType.VarChar, 30).Value = txtEma.Text.Trim().ToLower()
        cmUpdateTable.Parameters.Add("@rep", SqlDbType.VarChar, 60).Value = txtRep.Text.Trim()
        cmUpdateTable.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = txtDNI.Text.Trim()
        If lbTabla2.SelectedIndex = 0 Then 'Activo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else '1=Inactivo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable.Parameters.Add("@idT", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
        cmUpdateTable.Parameters.Add("@cue", SqlDbType.VarChar, 60).Value = txtCue.Text.Trim()
        cmUpdateTable.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
        cmUpdateTable.Parameters.Add("@detraccion", SqlDbType.VarChar, 60).Value = txtDetraccion.Text.Trim()
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TIdentidad where codIde=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub cbBuscar_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBuscar.SelectedIndexChanged
        txtBuscar.Focus()
    End Sub

    Private Sub txtBuscar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus, txtBuscar.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged


        Dim campo As String
        If cbBuscar.SelectedIndex = 0 Then
            campo = "ruc"
        End If
        If cbBuscar.SelectedIndex = 1 Then
            campo = "razon"
        End If

        If cbBuscar.SelectedIndex = 0 Then
            'Tipo String
            BindingSource1.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%' and idTipId=" & lbTabla1.SelectedValue
        Else 'razon
            BindingSource1.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%' and idTipId=" & lbTabla1.SelectedValue
        End If
        
    End Sub



    Private Sub txtRuc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRuc.KeyPress, txtDNI.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub


    Private Sub lbTabla2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTabla2.SelectedIndexChanged


        If RecuperarIdentidadLugarTrabajo() > 0 Then

            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CLIENTE TIENE OBRAS EN EJECUCIÓN O PARALIZADAS.")
            lbTabla2.SelectedIndex = 0
        End If

        If RecuperarIdentidadGrupoCot() > 0 Then

            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PROVEEDOR TIENE COTIZACIONES ABIERTAS, POR FAVOR CIERRE LAS COTIZACIONES.")
            lbTabla2.SelectedIndex = 0

        End If


        If RecuperarIdentidadOrdenCompra() > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PROVEEDOR TIENE ORDENES DE COMPRA PENDIENTES DE CIERRE.")
            lbTabla2.SelectedIndex = 0

        End If

        If RecuperarIdentidadOrdenDesembolso() > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... PROVEEDOR TIENE ORDENES DE DESEMBOLSO PENDIENTES DE CIERRE.")
            lbTabla2.SelectedIndex = 0
        End If

    End Sub

#Region "validacion para inactividad"
    ''' <summary>
    ''' recupera la cantidad de grupo de cotizaciones abiertas para un identidad proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarIdentidadGrupoCot() As Object

        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(estGru) from TGrupoCot TG inner join TCotizacion TC on TG.codGruC =TC.codGruC Where codIde = @codIde and estGru=0"

        Dim valor As Integer = BindingSource1.Item(BindingSource1.Position)(0) ' dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Parameters.Add(New SqlParameter("@codIde", SqlDbType.Int)).Value = valor  'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Connection = Cn
        Dim resul As Object = cmdCampo.ExecuteScalar()
        Return resul

    End Function
    ''' <summary>
    ''' recupera la cantidad de obras en ejecución o paralizadas para un cliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarIdentidadLugarTrabajo() As Integer

        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(tlu.codIde) from TLugarTrabajo TLU inner join TUbicacion TUB on tlu.codigo  = tub.codigo where tlu.codIde=@codIde and (TLU.estado = 0 or TLU.estado = 1)"
        Dim valor As Integer = BindingSource1.Item(BindingSource1.Position)(0) ' dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Parameters.Add(New SqlParameter("@codIde", SqlDbType.Int)).Value = valor  'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Connection = Cn
        Dim resul As Object = cmdCampo.ExecuteScalar()
        Return resul
        'Dim consulta As String = 

    End Function
    ''' <summary>
    ''' recupera la cantida de compras abiertas por una identidad proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarIdentidadOrdenCompra() As Integer

        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(codIde) from TOrdenCompra where codIde =@codIde and (estado =0 or estado=1)"
        Dim valor As Integer = BindingSource1.Item(BindingSource1.Position)(0) 'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Parameters.Add(New SqlParameter("@codIde", SqlDbType.Int)).Value = valor  'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Connection = Cn
        Dim resul As Object = cmdCampo.ExecuteScalar()
        Return resul
        'Dim consulta As String = 

    End Function
    ''' <summary>
    ''' recupera la cantidad de ordenes de desembolso abiertas para una identidad proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarIdentidadOrdenDesembolso() As Integer

        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codIde) from TOrdenDesembolso where codIde =@codIde and (estado=0 or estado=1)"
        Dim valor As Integer = BindingSource1.Item(BindingSource1.Position)(0) 'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Parameters.Add(New SqlParameter("@codIde", SqlDbType.Int)).Value = valor  'dgTabla1.SelectedRows(0).Cells("codIde").Value
        cmdCampo.Connection = Cn
        Dim resul As Object = cmdCampo.ExecuteScalar()
        Return resul
        'Dim consulta As String = 
    End Function
#End Region


    Private Sub dgTabla1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellClick
        enlazarText()

    End Sub

    Private Sub dgTabla1_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellEnter
        enlazarText()

    End Sub

    Private Sub dgTabla1_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgTabla1.DataBindingComplete

        'DirectCast(sender, DataGridView).ClearSelection()


    End Sub

End Class
