Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class crearMaterialForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim dataTable2 As New DataTable()
    Dim dataTable4 As New DataTable()

    Dim bindingSource1 As New BindingSource()
    Dim bindingSource2 As New BindingSource()
    Dim bindingSource4 As New BindingSource()

    Private Sub crearMaterialForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetRestaurantModule.vb
        Dim sele As String = "select codMat,material,uniBase,preBase,tipoM,est,hist,estado,codTipM,codUni,codAreaM from VMaterial" 'where codTipM=@codT" 'or codAreaM=@codA"
        crearDataAdapterTable(dTable1, sele)
        'dTable1.SelectCommand.Parameters.Add("@codT", SqlDbType.Int, 0).Value = 0

        sele = "select codTipM,tipoM from TTipoMat"
        crearDataAdapterTable(dTable2, sele)
        sele = "select codUni,unidad from TUnidad where codUni>1 order by unidad"  '1=""
        crearDataAdapterTable(dTable4, sele)

        Try
            'llenar la tabla virtual con los dataAdapter
            dTable1.Fill(dataTable1)
            dTable2.Fill(dataTable2)
            dTable4.Fill(dataTable4)

            bindingSource1.DataSource = dataTable1
            Navigator1.BindingSource = bindingSource1
            dgTabla1.DataSource = bindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource1.Sort = "material"
            ModificarColumnasDGV()

            bindingSource2.DataSource = dataTable2
            cbTipo.DataSource = bindingSource2
            cbTipo.DisplayMember = "tipoM"
            cbTipo.ValueMember = "codTipM"
            bindingSource2.Sort = "tipoM"

            bindingSource4.DataSource = dataTable4
            cbUni1.DataSource = BindingSource4
            cbUni1.DisplayMember = "unidad"
            cbUni1.ValueMember = "codUni"

            configurarColorControl()
            cbBuscar.SelectedIndex = 0

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

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub crearMaterialForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Width = 35
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 350
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Unid."
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "PrecS/."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderText = "Tipo Insumo"
            .Columns(4).Width = 120
            .Columns(5).Width = 60
            .Columns(5).HeaderText = "Estado"
            .Columns(6).Width = 1000
            .Columns(6).HeaderText = ""
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
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
        Label7.ForeColor = ForeColorLabel
        btnNuevo2.ForeColor = ForeColorButtom
        btnModificar2.ForeColor = ForeColorButtom
        btnCancelar2.ForeColor = ForeColorButtom
        btnEliminar2.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        Dim campo As String
        If cbBuscar.SelectedIndex = 0 Then
            campo = "material"
        End If
        If cbBuscar.SelectedIndex = 1 Then
            campo = "codMat"
        End If

        If cbBuscar.SelectedIndex = 0 Then
            'Tipo String
            bindingSource1.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%'"
        Else
            If Not IsNumeric(txtBuscar.Text.Trim()) Then
                StatusBarClass1.messageBarraEstado(" INGRESE DATO NUMERICO...")
                txtBuscar.SelectAll()
                Exit Sub
            End If
            bindingSource1.Filter = campo & "=" & txtBuscar.Text.Trim()
        End If
        If bindingSource1.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass1.messageBarraEstado("")
            colorearFila()
        Else
            'txtBuscar.Focus()
            'txtBuscar.SelectAll()
            StatusBarClass1.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To bindingSource1.Count - 1
            If bindingSource1.Item(j)(7) = 0 Then 'Inactivo
                dgTabla1.Rows(j).DefaultCellStyle.BackColor = Color.Yellow
                dgTabla1.Rows(j).DefaultCellStyle.ForeColor = Color.DarkBlue
            End If
        Next
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        enlazarText()
    End Sub

    Private Sub enlazarText()
        If bindingSource1.Count = 0 Then
            'desEnlazarText()
        Else
            txtProd.Text = bindingSource1.Item(bindingSource1.Position)(1)
            cbUni1.SelectedValue = bindingSource1.Item(bindingSource1.Position)(9)
            txtPre1.Text = bindingSource1.Item(bindingSource1.Position)(3)
            cbTipo.SelectedValue = bindingSource1.Item(bindingSource1.Position)(8)
            If bindingSource1.Item(bindingSource1.Position)(7) = 1 Then '1=Activo
                lbEstado.SelectedIndex = 0
            Else '0=Inactivo
                lbEstado.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        If vfNuevo2 = "guardar" Then
            btnModificar2.Enabled = False
            btnModificar2.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo2.Enabled = False
            btnNuevo2.FlatStyle = FlatStyle.Flat
        End If
        btnCancelar2.Enabled = True
        btnCancelar2.FlatStyle = FlatStyle.Standard
        btnEliminar2.Enabled = False
        btnEliminar2.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        btnNuevo2.Enabled = True
        btnNuevo2.FlatStyle = FlatStyle.Standard
        btnModificar2.Enabled = True
        btnModificar2.FlatStyle = FlatStyle.Standard
        btnCancelar2.Enabled = False
        btnCancelar2.FlatStyle = FlatStyle.Flat
        btnEliminar2.Enabled = True
        btnEliminar2.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub activarText()
        txtProd.ReadOnly = False
        cbUni1.Enabled = True
        txtPre1.ReadOnly = False
        If vfNuevo2 = "guardar" Then

        Else    'Se presiono <Modificar>
            lbEstado.Enabled = True
        End If
    End Sub

    Private Sub desActivarText()
        txtProd.ReadOnly = True
        cbUni1.Enabled = False
        txtPre1.ReadOnly = True
        lbEstado.Enabled = False
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If validaCampoVacioMinCaracNoNumer(txtProd.Text.Trim, 3) Then
            txtProd.errorProv()
            Return True
        End If
        If ValidaNroMayorOigualCero(txtPre1.Text) Then
            txtPre1.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarExiste1(ByVal prod As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from VBusquedaMat where material='" & prod & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarProd1(ByVal prod As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select tipoM from VBusquedaMat where material='" & prod & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfNuevo2 As String = "nuevo"
    Dim vfCampo2 As String
    Private Sub btnNuevo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo2.Click
        If vfNuevo2 = "nuevo" Then
            vfNuevo2 = "guardar"
            Me.btnNuevo2.Text = "Guardar"
            desactivarControles1()
            activarText()
            txtProd.Clear()
            txtPre1.Text = "0"
            cbTipo.Focus()
            StatusBarClass1.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo2
        Else   ' guardar
            'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
            If ValidarCampos() Then
                Exit Sub
            End If

            If recuperarExiste1(txtProd.Text.Trim()) > 0 Then
                MessageBox.Show("Proceso Denegado, Material: " & txtProd.Text.Trim() & Chr(13) & "Fue asignado en el tipo de material: " & recuperarProd1(txtProd.Text.Trim), nomNegocio, Nothing, MessageBoxIcon.Error)
                txtProd.Focus()
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            wait.Show()
            Me.Cursor = Cursors.WaitCursor
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass1.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()

                'TMaterial
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
                Dim codMat As Integer = cmInserTable.Parameters("@Identity").Value
                vfCampo2 = codMat.ToString()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass1.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True

                bindingSource1.RemoveFilter()
                'Actualizando el dataSet 
                dataTable1.Clear()
                dTable1.Fill(dataTable1)

                Me.btnCancelar2.PerformClick()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                bindingSource1.Position = bindingSource1.Find("codMat", vfCampo2)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass1.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
                Me.Cursor = Cursors.Default
                colorearFila()
                txtBuscar.Focus()
                txtBuscar.SelectAll()
            Catch f As Exception
                wait.Close()
                Me.Cursor = Cursors.Default
                If finalMytrans Then
                    MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show("tipoM de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End Try
        End If
    End Sub

    Dim vfModificar2 As String = "modificar"
    Private Sub btnModificar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar2.Click
        If vfModificar2 = "modificar" Then
            If dgTabla1.Rows.Count = 0 Then
                StatusBarClass1.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            enlazarText()
            vfModificar2 = "actualizar"
            btnModificar2.Text = "Actualizar"
            desactivarControles1()
            activarText()
            txtProd.Focus()
            StatusBarClass1.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar2
        Else    'Actualizar
            'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
            If ValidarCampos() Then
                Exit Sub
            End If

            If txtProd.Text.Trim().ToUpper() <> bindingSource1.Item(bindingSource1.Position)(1).ToString.ToUpper() Then
                If recuperarExiste1(txtProd.Text.Trim()) > 0 Then
                    MessageBox.Show("Proceso Denegado, Material: " & txtProd.Text.Trim() & Chr(13) & "Fue asignado al tipo de material: " & recuperarProd1(txtProd.Text.Trim), nomNegocio, Nothing, MessageBoxIcon.Error)
                    txtProd.Focus()
                    Exit Sub
                End If
            End If

            If lbEstado.SelectedIndex = 1 Then 'Inactivo
                'If recuperarSumStock(BindingSource2.Item(BindingSource2.Position)(0)) > 0 Then
                'MessageBox.Show("PROCESO DENEGADO, ESTE PRODUCTO NO SE DEBE PONER AL ESTADO DE [INACTIVO] POR TENER STOCK>0", nomNegocio, Nothing, MessageBoxIcon.Error)
                'lbEstado.SelectedIndex = 0 'Activo
                'Exit Sub
                'End If
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass1.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TMaterial
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampo2 = bindingSource1.Item(bindingSource1.Position)(0)

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass1.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                bindingSource1.RemoveFilter()
                'Actualizando el dataSet 
                dataTable1.Clear()
                dTable1.Fill(dataTable1)

                Me.btnCancelar2.PerformClick()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                bindingSource1.Position = bindingSource1.Find("codMat", vfCampo2)

                StatusBarClass1.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
                colorearFila()
                txtBuscar.Focus()
                txtBuscar.SelectAll()
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

    Private Sub btnCancelar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar2.Click
        vfNuevo2 = "nuevo"
        Me.btnNuevo2.Text = "Nuevo"
        vfModificar2 = "modificar"
        Me.btnModificar2.Text = "Modificar"
        activarControles1()
        desActivarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
        enlazarText()
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertTMaterial"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@prod", SqlDbType.VarChar, 100).Value = txtProd.Text.Trim()
        cmInserTable.Parameters.Add("@codU1", SqlDbType.Int, 0).Value = cbUni1.SelectedValue
        cmInserTable.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = txtPre1.Text
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1 'Activo
        cmInserTable.Parameters.Add("@codA", SqlDbType.Int, 0).Value = 2 'improvisado
        cmInserTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbTipo.SelectedValue
        cmInserTable.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = "Creo " & Now.Date & " " & vPass & "-" & vSUsuario
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TMaterial set material=@pro,codUni=@uni1,preBase=@pre1,estado=@est,codTipM=@codT,hist=@hist where codMat=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@pro", SqlDbType.VarChar, 100).Value = txtProd.Text.Trim()
        cmUpdateTable.Parameters.Add("@uni1", SqlDbType.Int, 0).Value = cbUni1.SelectedValue
        cmUpdateTable.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = txtPre1.Text
        If lbEstado.SelectedIndex = 0 Then 'Activo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
        Else  'Inactivo
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbTipo.SelectedValue
        cmUpdateTable.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = bindingSource1.Item(bindingSource1.Position)(6) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = bindingSource1.Item(bindingSource1.Position)(0)
    End Sub

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleSol where codMat=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount2(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleCot where codMat=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount3(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleCompra where codMat=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount4(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleGuia where codMat=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar2.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass1.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount1(bindingSource1.Item(bindingSource1.Position)(0)) > 0 Then
            StatusBarClass1.messageBarraEstado("  PROCESO DENEGADO, INSUMO TIENE INSTANCIAS EN SOLICITUD...")
            Exit Sub
        End If

        If recuperarCount2(bindingSource1.Item(bindingSource1.Position)(0)) > 0 Then
            StatusBarClass1.messageBarraEstado("  PROCESO DENEGADO, INSUMO TIENE INSTANCIAS EN COTIZACION...")
            Exit Sub
        End If

        If recuperarCount3(bindingSource1.Item(bindingSource1.Position)(0)) > 0 Then
            StatusBarClass1.messageBarraEstado("  PROCESO DENEGADO, INSUMO TIENE INSTANCIAS EN DOC. COMPRAS...")
            Exit Sub
        End If

        If recuperarCount4(bindingSource1.Item(bindingSource1.Position)(0)) > 0 Then
            StatusBarClass1.messageBarraEstado("  PROCESO DENEGADO, INSUMO TIENE INSTANCIAS EN GUIAS...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtProd.Focus()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass1.messageBarraEstado("  ELIMINANDO REGISTROS...")

            'Tabla TProducto
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("ERROR..No se puede eliminar producto...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass1.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dataTable1.Clear()
            dTable1.Fill(dataTable1)

            enlazarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass1.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()
            colorearFila()
        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("tipoM de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TMaterial where codMat=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = bindingSource1.Item(bindingSource1.Position)(0)
    End Sub

    Private Sub txtProd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProd.GotFocus, txtProd.MouseClick
        txtProd.SelectAll()
    End Sub

    Private Sub txtPre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre1.GotFocus, txtPre1.MouseClick
        txtPre1.SelectAll()
    End Sub
End Class
