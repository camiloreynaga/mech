Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008



Public Class mantLugarForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim cConfigGrilla As New cConfigFormControls

    Private Sub mantLugarForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub mantLugarForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codigo,fecAper,nombre,lugar,tiempoMeta,tiempoContr,presupMeta,presupContr,razon,dir,codIde,idTipId,estado,codEstado,color from VLugarUbicacion"
        crearDataAdapterTable(daTabla1, sele)
        sele = "select codUbi,ubicacion,'est'=case when estado=0 then 'Abierto' else 'Cerrado' end,color,codigo,estado from TUbicacion"
        crearDataAdapterTable(daTUbi, sele)
        sele = "select codIde,razon,idTipId from TIdentidad where idTipId=1 and estado=1 order by razon"  'idTipId=1 Cliente
        crearDataAdapterTable(daTCliente, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VLugarUbicacion")
            daTUbi.Fill(dsAlmacen, "TUbicacion")
            daTCliente.Fill(dsAlmacen, "TIdentidad")

            AgregarRelacion()

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VLugarUbicacion"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa

            BindingSource2.DataSource = BindingSource1
            BindingSource2.DataMember = "Relacion1"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            cbCli.DataSource = dsAlmacen
            cbCli.DisplayMember = "TIdentidad.razon"
            cbCli.ValueMember = "codIde"




            configurarColorControl()
            cbColor.SelectedIndex = 0
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub mantUbicacionForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'MsgBox("pintando formulario")
        colorear()

    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource2.Count - 1
            dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.FromName(BindingSource2.Item(j)(3))
            dgTabla2.Refresh()
        Next
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("VLugarUbicacion").Columns("codigo"), dsAlmacen.Tables("TUbicacion").Columns("codigo"))
        dsAlmacen.Relations.Add(relation1)
    End Sub

    Private Sub ModificarColumnasDGV()

        cConfigGrilla.ConfigGrilla(dgTabla1)
        cConfigGrilla.ConfigGrilla(dgTabla2)

        With dgTabla1
            .Columns(0).HeaderText = "Cód"
            .Columns(0).Width = 45
            .Columns(1).HeaderText = "Fec_Aper"
            .Columns(1).Width = 70
            .Columns(2).HeaderText = "Descripción Obra"
            .Columns(2).Width = 320
            .Columns(3).HeaderText = "Ubicacion"
            .Columns(3).Width = 240
            .Columns(4).Width = 80
            .Columns(4).HeaderText = "Tiem_Meta"
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "Tiem_Contrato"
            .Columns(6).Width = 60
            .Columns(6).HeaderText = "Pres_Meta"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).Width = 60
            .Columns(7).HeaderText = "Pres_Cont"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).HeaderText = "Cliente"
            .Columns(8).Width = 240
            .Columns(9).HeaderText = "Dir_Cliente"
            .Columns(9).Width = 280
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).HeaderText = "Código"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Ubicación"
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).Width = 215
            .Columns(2).Width = 70
            .Columns(2).HeaderText = "Estado"
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 70
            .Columns(3).HeaderText = "Color"
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
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



        btnNuevo1.ForeColor = ForeColorButtom
        btnModificar1.ForeColor = ForeColorButtom
        btnCancelar1.ForeColor = ForeColorButtom
        btnEliminar1.ForeColor = ForeColorButtom
        btnNuevo2.ForeColor = ForeColorButtom
        btnModificar2.ForeColor = ForeColorButtom
        btnCancelar2.ForeColor = ForeColorButtom
        btnEliminar2.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub cbColor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cbColor.BackColor = Color.FromName(cbColor.Text.Trim())
        txtUbi.Focus()
    End Sub

    Private Sub enlazarText1()
        If BindingSource1.Count = 0 Then
            'desEnlazarText()
        Else
            txtNro.Text = BindingSource1.Item(BindingSource1.Position)(0)
            date1.Value = BindingSource1.Item(BindingSource1.Position)(1)
            txtNom.Text = BindingSource1.Item(BindingSource1.Position)(2)
            txtDir.Text = BindingSource1.Item(BindingSource1.Position)(3)
            txtT1.Text = BindingSource1.Item(BindingSource1.Position)(4)
            txtT2.Text = BindingSource1.Item(BindingSource1.Position)(5)
            txtP1.Text = BindingSource1.Item(BindingSource1.Position)(6)
            txtP2.Text = BindingSource1.Item(BindingSource1.Position)(7)
            cbCli.SelectedValue = BindingSource1.Item(BindingSource1.Position)(10)

            If BindingSource1.Item(BindingSource1.Position)(13) = 1 Then
                lbEstadoObra.SelectedIndex = 0 'Activo
            Else
                If BindingSource1.Item(BindingSource1.Position)(13) = 2 Then
                    lbEstadoObra.SelectedIndex = 1  'Paralizado
                Else
                    lbEstadoObra.SelectedIndex = 2  'Culminado
                End If

            End If
            cbColorLugar.SelectedIndex = cbColorLugar.FindStringExact(BindingSource1.Item(BindingSource1.Position)(14))
        End If


    End Sub

    Private Sub enlazarText()
        If BindingSource2.Count = 0 Then
            'desEnlazarText()
        Else
            txtUbi.Text = BindingSource2.Item(BindingSource2.Position)(1)
            If BindingSource2.Item(BindingSource2.Position)(5) = 0 Then
                lbEst2.SelectedIndex = 0   'Abierto
            Else
                If BindingSource2.Item(BindingSource2.Position)(5) = 1 Then 'CERRAdO
                    lbEst2.SelectedIndex = 1

                End If
            End If
            cbColor.SelectedIndex = cbColor.FindStringExact(BindingSource2.Item(BindingSource2.Position)(3))
        End If
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        Panel2.Enabled = False
        Panel3.Enabled = False
        If vfNuevo1 = "guardar" Then
            btnModificar2.Enabled = False
            btnModificar2.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo2.Enabled = False
            btnNuevo2.FlatStyle = FlatStyle.Flat
            lbEst2.Enabled = True
        End If
        btnCancelar2.Enabled = True
        btnCancelar2.FlatStyle = FlatStyle.Standard
        btnEliminar2.Enabled = False
        btnEliminar2.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
        cbColor.Enabled = True
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        Panel2.Enabled = True
        Panel3.Enabled = True
        btnCancelar2.Enabled = False
        btnCancelar2.FlatStyle = FlatStyle.Flat
        btnNuevo2.Enabled = True
        btnNuevo2.FlatStyle = FlatStyle.Standard
        btnModificar2.Enabled = True
        btnModificar2.FlatStyle = FlatStyle.Standard
        btnEliminar2.Enabled = True
        btnEliminar2.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
        cbColor.Enabled = False
        lbEst2.Enabled = False
        txtUbi.ReadOnly = True
    End Sub

    Private Function ValidarCampos() As Boolean
        'Creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtUbi.Text.Trim, 3) Then
            txtUbi.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo2.Click
        If BindingSource1.Item(BindingSource1.Position)(0) = "00-00" Then
            StatusBarClass.messageBarraEstado(" PROCESO DENEGADO, SOLO DEBE EXISTIR UN ALMACEN PRINCIPAL...")
            Exit Sub
        End If

        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo2.Text = "Guardar"
            desactivarControles1()
            txtUbi.ReadOnly = False
            txtUbi.Clear()
            txtUbi.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo2
        Else   ' guardar
            If ValidarCampos() Then
                Exit Sub
            End If

            If BindingSource2.Find("ubicacion", txtUbi.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste ubicación: " & txtUbi.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtUbi.Focus()
                txtUbi.SelectAll()
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                Dim vfCampo1 As String = txtUbi.Text.Trim()

                'TUbicacion
                'insertando la ubicación
                comandoInsert1(txtUbi.Text.Trim(), BindingSource1.Item(BindingSource1.Position)(0), cbColor.Text.Trim())

                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'TUbicacion
                'comandoUpdate13()
                'cmUpdateTable13.Transaction = myTrans
                'If cmUpdateTable13.ExecuteNonQuery() < 1 Then
                '    'deshace la transaccion
                '    wait.Close()
                '    myTrans.Rollback()
                '    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                '    Me.Close()
                '    Exit Sub
                'End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True

                'Actualizando el dataSet 
                dsAlmacen.Tables("TUbicacion").Clear()
                daTUbi.Fill(dsAlmacen, "TUbicacion")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource2.Position = BindingSource2.Find("ubicacion", vfCampo1)
                btnCancelar2.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
                colorear()
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

    Private Function recuperarCountEst(ByVal cod As String, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TUbicacion where estado=1 and codigo='" & cod & "'"
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount13(ByVal cod As String, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codigo) from TPersLugar where codigo='" & cod & "'"
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSumStock(ByVal cod As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select ISNULL(SUM(cant),-1) from TDetalleArt where codUbi=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfModificar As String = "modificar"
    Private Sub btnModificar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar2.Click
        If vfModificar = "modificar" Then
            If dgTabla2.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar2.Text = "Actualizar"
            desactivarControles1()
            enlazarText()
            txtUbi.ReadOnly = False
            txtUbi.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar2
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If
            Dim vfCampo1 As String = BindingSource2.Item(BindingSource2.Position)(1)
            If vfCampo1.ToUpper() <> txtUbi.Text.ToUpper.Trim() Then
                If BindingSource2.Find("ubicacion", txtUbi.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya exíste ubicación: " & txtUbi.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtUbi.Focus()
                    txtUbi.SelectAll()
                    Exit Sub
                End If
            End If

            'If BindingSource2.Item(BindingSource2.Position)(0) = 1 Then
            '    If lbEst2.SelectedIndex = 1 Or lbEst2.SelectedIndex = 2 Then
            '        StatusBarClass.messageBarraEstado(" PROCESO DENEGADO, ESTA UBICACION NO SE DEBE PONER AL ESTADO DE [PARALIZADO o CULMINADO]")
            '        lbEst2.SelectedIndex = 0 'Activo
            '        Exit Sub
            '    End If
            'End If

            If lbEst2.SelectedIndex = 1 Then 'Inactivo
                'If recuperarSumStock(BindingSource2.Item(BindingSource2.Position)(0)) > 0 Then
                'MessageBox.Show("PROCESO DENEGADO, ESTA UBICACION NO SE DEBE PONER AL ESTADO DE [INACTIVO] POR TENER STOCK>0", nomNegocio, Nothing, MessageBoxIcon.Error)
                'lbEst2.SelectedIndex = 0 'Activo
                'Exit Sub
                'End If
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                'TUbicacion
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

                'TUbicacion
                'comandoUpdate13()
                'cmUpdateTable13.Transaction = myTrans
                'If cmUpdateTable13.ExecuteNonQuery() < 1 Then
                '    'deshace la transaccion
                '    wait.Close()
                '    myTrans.Rollback()
                '    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                '    Me.Close()
                '    Exit Sub
                'End If

                If recuperarCountEst(BindingSource1.Item(BindingSource1.Position)(0), myTrans) = 0 Then 'Toda ubicacion desativada
                    If recuperarCount13(BindingSource1.Item(BindingSource1.Position)(0), myTrans) > 0 Then
                        wait.Close()
                        myTrans.Rollback()
                        btnCancelar2.PerformClick()
                        MessageBox.Show("ACTUALIZACION DENEGADA... SUCURSAL TIENE PERSONAL ASIGNADO A SUCURSAL...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If

                vfCampo1 = txtUbi.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("TUbicacion").Clear()
                daTUbi.Fill(dsAlmacen, "TUbicacion")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource2.Position = BindingSource2.Find("ubicacion", vfCampo1)
                btnCancelar2.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
                colorear()
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
        vfNuevo1 = "nuevo"
        btnNuevo2.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar2.Text = "Modificar"
        activarControles1()
        enlazarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Function recuperarCount(ByVal cod As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetES where codUbi=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar2.Click
        If dgTabla2.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If
        If BindingSource2.Item(BindingSource2.Position)(0) = 1 Then
            StatusBarClass.messageBarraEstado(" PROCESO DENEGADO, ESTA UBICACION NO SE PUEDE ELIMINAR POR SER LA PRINCIPAL...")
            Exit Sub
        End If

        'If recuperarCount(BindingSource2.Item(BindingSource2.Position)(0)) > 0 Then
        'StatusBarClass.messageBarraEstado("  PROCESO DENEGADO... UBICACION TIENE ENTRADAS SALIDAS...")
        'Exit Sub
        'End If

        If BindingSource2.Count = 1 Then 'Unica ubicacion
            If recuperarCount1(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
                StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... SUCURSAL TIENE PERSONAL ASIGNADO A SUCURSAL...")
                Exit Sub
            End If
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtUbi.Focus()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TUbicacion
            comandoDelete()
            cmDeleteTable.Transaction = myTrans
            If cmDeleteTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar Representante por qué esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            If recuperarCountEst(BindingSource1.Item(BindingSource1.Position)(0), myTrans) = 0 Then 'Toda ubicacion desativada
                If recuperarCount13(BindingSource1.Item(BindingSource1.Position)(0), myTrans) > 0 Then
                    wait.Close()
                    myTrans.Rollback()
                    btnCancelar2.PerformClick()
                    MessageBox.Show("ELIMINACION DENEGADA... SUCURSAL TIENE PERSONAL ASIGNADO A SUCURSAL...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet dsAlmacen
            dsAlmacen.Tables("TUbicacion").Clear()
            daTUbi.Fill(dsAlmacen, "TUbicacion")

            enlazarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()
            colorear()
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
    ''' <summary>
    ''' inserta un registro de ubicación (almacen) en TUbicacion
    ''' </summary>
    ''' <param name="ubicacion"></param>
    ''' <param name="codigo"></param>
    ''' <param name="color"></param>
    ''' <remarks></remarks>
    Private Sub comandoInsert1(ByVal ubicacion As String, ByVal codigo As Object, ByVal color As String)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert TUbicacion(ubicacion,estado,codigo,color) values(@ubi,0,@cod,@col)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@ubi", SqlDbType.VarChar, 50).Value = ubicacion 'txtUbi.Text.Trim()
        cmInserTable1.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = codigo 'BindingSource1.Item(BindingSource1.Position)(0)

        cmInserTable1.Parameters.Add("@col", SqlDbType.VarChar, 15).Value = color  'cbColor.Text.Trim()
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TUbicacion set ubicacion=@ubi,estado=@est,color=@col where codUbi=@codU"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@ubi", SqlDbType.VarChar, 50).Value = txtUbi.Text.Trim()
        If lbEst2.SelectedIndex = 0 Then 'Abierto
            cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        Else  'Inactivo
            If lbEst2.SelectedIndex = 1 Then 'Cerrado
                cmUpdateTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1
            
            End If
        End If
        cmUpdateTable.Parameters.Add("@col", SqlDbType.VarChar, 15).Value = cbColor.Text.Trim()
        cmUpdateTable.Parameters.Add("@codU", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TUbicacion set color=@col where codigo=@cod"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@col", SqlDbType.VarChar, 15).Value = cbColor.Text.Trim()
        cmUpdateTable13.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TUbicacion where codUbi=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Private Sub desactivarControles2()
        Panel1.Enabled = False
        Panel3.Enabled = False
        Panel4.Enabled = False
        If vfNuevo2 = "guardar" Then
            btnModificar1.Enabled = False
            btnModificar1.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo1.Enabled = False
            btnNuevo1.FlatStyle = FlatStyle.Flat
            'lbEst1.Enabled = True
        End If
        btnCancelar1.Enabled = True
        btnCancelar1.FlatStyle = FlatStyle.Standard
        btnEliminar1.Enabled = False
        btnEliminar1.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat


        cbCli.Enabled = True

        cbColorLugar.Enabled = True
        date1.Enabled = True
    End Sub

    Private Sub activarControles2()
        Panel1.Enabled = True
        Panel3.Enabled = True
        Panel4.Enabled = True
        btnCancelar1.Enabled = False
        btnCancelar1.FlatStyle = FlatStyle.Flat
        btnNuevo1.Enabled = True
        btnNuevo1.FlatStyle = FlatStyle.Standard
        btnModificar1.Enabled = True
        btnModificar1.FlatStyle = FlatStyle.Standard
        btnEliminar1.Enabled = True
        btnEliminar1.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
        'lbEst1.Enabled = False
        txtNom.ReadOnly = True
        txtDir.ReadOnly = True
        txtT1.ReadOnly = True
        txtT2.ReadOnly = True
        txtP1.ReadOnly = True
        txtP2.ReadOnly = True

        cbCli.Enabled = False
        cbColorLugar.Enabled = False
        date1.Enabled = False
        lbEstadoObra.Enabled = False
    End Sub

    Private Function ValidarCampos1() As Boolean
        'Creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtNom.Text().Trim, 3) Then
            txtNom.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtDir.Text.Trim(), 3) Then
            txtDir.errorProv()
            Return True
        End If
        If ValidaNroMayorOigualCero(txtP1.Text.Trim) Then
            txtP1.errorProv()
            Return True
        End If
        If ValidaNroMayorOigualCero(txtP2.Text.Trim) Then
            txtP2.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Dim vfNuevo2 As String = "nuevo"
    Private Sub btnNuevo1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo1.Click
        If vfNuevo2 = "nuevo" Then
            vfNuevo2 = "guardar"
            Me.btnNuevo1.Text = "Guardar"
            desactivarControles2()
            txtNom.ReadOnly = False
            txtDir.ReadOnly = False
            txtT1.ReadOnly = False
            txtT2.ReadOnly = False
            txtP1.ReadOnly = False
            txtP2.ReadOnly = False
            txtNro.Clear()
            txtNom.Clear()
            txtDir.Clear()
            txtT1.Clear()
            txtT2.Clear()
            txtP1.Text = "0"
            txtP2.Text = "0"
            cbCli.Focus()
            cbColorLugar.SelectedIndex = 0
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo1
        Else   ' guardar
            If ValidarCampos1() Then
                Exit Sub
            End If

            If BindingSource1.Find("codigo", txtNro.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste codLug: " & txtNro.Text.Trim() & Chr(13) & "Cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtNro.Focus()
                txtNro.SelectAll()
                Exit Sub
            End If

            If BindingSource1.Find("nombre", txtNom.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste lugar de trabajo: " & txtNom.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
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
                Dim vfCampo1 As String = txtNom.Text.Trim()

                'Dim ruc As String = recuperarDato("ruc", myTrans)

                'TLugarTrabajo

                Dim _codLugar As Object = recuperarCod(myTrans)
                comandoInsert2(_codLugar)
                cmInserTable2.Transaction = myTrans
                If cmInserTable2.ExecuteNonQuery() < 1 Then
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

                ''
                comandoInsert1("ALMACEN DE OBRA", "00-0" & _codLugar.ToString(), "White")
                Dim myTrans2 As SqlTransaction = Cn.BeginTransaction()
                cmInserTable1.Transaction = myTrans2
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans2.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se creo el almacen de obra...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    'Me.Close()
                    'Exit Sub
                End If
                myTrans2.Commit()

                'Actualizando el dataSet 
                dsAlmacen.Tables("TUbicacion").Clear()
                dsAlmacen.Tables("VLugarUbicacion").Clear()
                daTabla1.Fill(dsAlmacen, "VLugarUbicacion")
                daTUbi.Fill(dsAlmacen, "TUbicacion")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nombre", vfCampo1)
                btnCancelar1.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
                colorear()
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

    Private Sub btnCancelar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar1.Click
        vfNuevo2 = "nuevo"
        btnNuevo1.Text = "Nuevo"
        vfModificar2 = "modificar"
        btnModificar1.Text = "Modificar"
        activarControles2()
        enlazarText1()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Dim vfModificar2 As String = "modificar"
    Private Sub btnModificar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar1.Click
        If vfModificar2 = "modificar" Then
            If dgTabla1.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar2 = "actualizar"
            btnModificar1.Text = "Actualizar"
            desactivarControles2()
            enlazarText1()
            txtNom.ReadOnly = False
            txtDir.ReadOnly = False
            txtT1.ReadOnly = False
            txtT2.ReadOnly = False
            txtP1.ReadOnly = False
            txtP2.ReadOnly = False
            lbEstadoObra.Enabled = True
            txtNom.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar1
        Else    'Actualizar
            If ValidarCampos1() Then
                Exit Sub
            End If

            Dim vfCampo1 As String = BindingSource1.Item(BindingSource1.Position)(2)
            If vfCampo1.ToUpper() <> txtNom.Text.ToUpper.Trim() Then
                If BindingSource1.Find("nombre", txtNom.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya exíste lugar de trabajo: " & txtNom.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
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
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                'TLugarTrabajo
                comandoUpdate1()
                cmUpdateTable1.Transaction = myTrans
                If cmUpdateTable1.ExecuteNonQuery() < 1 Then
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

                'actualizando los estados de los almacenes
                If lbEstadoObra.SelectedIndex > 0 Then
                    ActualizarEstadoAlmacen(BindingSource1.Item(BindingSource1.Position)(0), 1)
                End If

                If lbEstadoObra.SelectedIndex = 0 Then
                    ActualizarEstadoAlmacen(BindingSource1.Item(BindingSource1.Position)(0), 0)
                End If



                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("TUbicacion").Clear()
                dsAlmacen.Tables("VLugarUbicacion").Clear()
                daTabla1.Fill(dsAlmacen, "VLugarUbicacion")
                daTUbi.Fill(dsAlmacen, "TUbicacion")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("nombre", vfCampo1)
                btnCancelar1.PerformClick()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
                colorear()
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

    Private Function recuperarCount1(ByVal cod As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codigo) from TPersLugar where codigo='" & cod & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount2(ByVal cod As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codigo) from TUbicacion where codigo='" & cod & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar1.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If
        If BindingSource1.Item(BindingSource1.Position)(0).ToString = "00-00" Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... NO SE PUEDE ELIMINAR ESTÉ LUGAR DE TRABAJO...")
            Exit Sub
        End If

        If recuperarCount1(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... LUGAR TIENE PERSONAL ASIGNADO A LUGAR...")
            Exit Sub
        End If

        'If recuperarCount2(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
        '    StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... LUGAR TIENE UBICACIONES PROCESADAS...")
        '    Exit Sub
        'End If

        If DevolverNroSolicitudesObra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE SOLICITUDES PROCESADAS...")
            Exit Sub
        End If

        If DevolverNroCotizacionesObra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE COTIZACIONES PROCESADAS...")
            Exit Sub
        End If

        If DevolverNroOrdenesCompraObra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... LUGAR TIENE ORDENES DE COMPRA PROCESADAS...")
            Exit Sub
        End If

        If DevolverNroOrdenesDesembolsoObra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... LUGAR TIENE ORDENES DE DESEMBOLSO PROCESADAS...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtNom.Focus()
            Exit Sub
        End If
        'ELIMINA DATOS DE ALMACEN
        EliminarAlmacenes(BindingSource1.Item(BindingSource1.Position)(0))

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TLugarTrabajo
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
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
            dsAlmacen.Tables("TUbicacion").Clear()
            dsAlmacen.Tables("VLugarUbicacion").Clear()
            daTabla1.Fill(dsAlmacen, "VLugarUbicacion")
            daTUbi.Fill(dsAlmacen, "TUbicacion")

            enlazarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()
            colorear()
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

    Public Function recuperarDato(ByVal campo As String, ByVal myTrans As SqlTransaction) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select " & campo & " from TLugarTrabajo where codigo='00-00'"
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Public Function recuperarCod(ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TLugarTrabajo"
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal nro1 As Short)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "insert into TLugarTrabajo(codigo,fecAper,nombre,lugar,tiempoMeta,tiempoContr,presupMeta,presupContr,codIde,estado,color) values(@nro,@fec,@nom,@dir,@t1,@t2,@p1,@p2,@codI,@estado,@color)"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@nro", SqlDbType.VarChar, 10).Value = "00-0" & nro1.ToString
        cmInserTable2.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable2.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = txtNom.Text.Trim()
        cmInserTable2.Parameters.Add("@dir", SqlDbType.VarChar, 100).Value = txtDir.Text.Trim()
        cmInserTable2.Parameters.Add("@t1", SqlDbType.VarChar, 20).Value = txtT1.Text.Trim()
        cmInserTable2.Parameters.Add("@t2", SqlDbType.VarChar, 20).Value = txtT2.Text.Trim()
        cmInserTable2.Parameters.Add("@p1", SqlDbType.Decimal, 0).Value = txtP1.Text.Trim()
        cmInserTable2.Parameters.Add("@p2", SqlDbType.Decimal, 0).Value = txtP2.Text.Trim()
        cmInserTable2.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbCli.SelectedValue
        cmInserTable2.Parameters.Add("@estado", SqlDbType.Int).Value = 1
        cmInserTable2.Parameters.Add("@color", SqlDbType.VarChar, 15).Value = cbColorLugar.Text
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TLugarTrabajo set fecAper=@fec,nombre=@nom,lugar=@dir,tiempoMeta=@t1,tiempoContr=@t2,presupMeta=@p1,presupContr=@p2,codIde=@codI,estado=@estado,color=@color where codigo=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable1.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = txtNom.Text.Trim()
        cmUpdateTable1.Parameters.Add("@dir", SqlDbType.VarChar, 100).Value = txtDir.Text.Trim()
        cmUpdateTable1.Parameters.Add("@t1", SqlDbType.VarChar, 20).Value = txtT1.Text.Trim()
        cmUpdateTable1.Parameters.Add("@t2", SqlDbType.VarChar, 20).Value = txtT2.Text.Trim()
        cmUpdateTable1.Parameters.Add("@p1", SqlDbType.Decimal, 0).Value = txtP1.Text.Trim()
        cmUpdateTable1.Parameters.Add("@p2", SqlDbType.Decimal, 0).Value = txtP2.Text.Trim()
        cmUpdateTable1.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbCli.SelectedValue
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = BindingSource1.Item(BindingSource1.Position)(0)
        If lbEstadoObra.SelectedIndex = 0 Then 'Ejecución
            cmUpdateTable1.Parameters.Add("@estado", SqlDbType.Int, 0).Value = 1
        Else
            If lbEstadoObra.SelectedIndex = 1 Then '1=Paralizado
                cmUpdateTable1.Parameters.Add("@estado", SqlDbType.Int, 0).Value = 2
            Else
                cmUpdateTable1.Parameters.Add("@estado", SqlDbType.Int, 0).Value = 3
            End If
        End If
        cmUpdateTable1.Parameters.Add("@color", SqlDbType.VarChar, 15).Value = cbColorLugar.Text
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TLugarTrabajo where codigo=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        colorear()
    End Sub

    
    Private Sub lbEstadoObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbEstadoObra.SelectedIndexChanged

        If BindingSource1.Item(BindingSource1.Position)(0).ToString = "00-00" Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... NO SE PUEDE MODIFICAR ESTADO...")
            lbEstadoObra.SelectedIndex = 0
            Exit Sub
        End If

        If DevolverNroSolicitudes(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE SOLICITUDES PENDIENTES.")
            lbEstadoObra.SelectedIndex = 0
            Exit Sub
        End If

        If DevolverNroCotizaciones(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE COTIZACIONES PENDIENTES.")
            lbEstadoObra.SelectedIndex = 0
            Exit Sub
        End If

        If DevolverNroOrdenesCompra(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE ORDENES DE COMPRA PENDIENTES.")
            lbEstadoObra.SelectedIndex = 0
            Exit Sub
        End If

        If DevolverNroOrdenesDesembolso(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... OBRA TIENE ORDENES DE DESEMBOLSO PENDIENTES.")
            lbEstadoObra.SelectedIndex = 0
            Exit Sub
        End If

    End Sub

#Region "Metodos para validar lugar trabajo"

    ''' <summary>
    ''' Devuelve la cantidad de solicitudes
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <remarks></remarks>
    Private Function DevolverNroSolicitudes(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select count(estDet) from Tsolicitud where codigo =@codigo and estDet=0 "
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function
    ''' <summary>
    ''' devuelve la cantidad de solicitudes por obra
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroSolicitudesObra(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select count(estDet) from Tsolicitud where codigo =@codigo"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' Devuelve la cantidad de cotizaciones abiertas
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroCotizaciones(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(TGC.codGruC ) from TCotizacion TCO inner join TGrupoCot TGC on TCO.codGruC =TGC.codGruC where TCO.codigo =@codigo and TGC.estGru =0"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function
    ''' <summary>
    ''' Devuelve la cantidad de cotizaciones por OBra
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroCotizacionesObra(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(TGC.codGruC ) from TCotizacion TCO inner join TGrupoCot TGC on TCO.codGruC =TGC.codGruC where TCO.codigo =@codigo"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' devuelve la cantidad de ordenes de compra abiertas
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroOrdenesCompra(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(estado) from TOrdenCompra where codigo =@codigo and (estado=0 or estado =1)"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' devuelve la cantidad de Ordenes de compra por Obra
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroOrdenesCompraObra(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(estado) from TOrdenCompra where codigo =@codigo"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' devuelve la cantidad de ordenes de desembolso abiertas
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroOrdenesDesembolso(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(estado) from TOrdenDesembolso where codigo=@codigo and (estado=0 or estado=1)"
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' devuelve la cantidad de ordenes de Desembolso por Obra
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DevolverNroOrdenesDesembolsoObra(ByVal idLugar As String) As Integer
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "select COUNT(estado) from TOrdenDesembolso where codigo=@codigo "
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        Return consulta.ExecuteScalar()
    End Function

    ''' <summary>
    ''' cambia los estados de almacen
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarEstadoAlmacen(ByVal idLugar As String, ByVal estado As Integer)
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "update TUbicacion set estado =" & estado & " where codigo =@codigo "
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        consulta.ExecuteScalar()
    End Sub

    ''' <summary>
    ''' Elimina los Almacenes de una obra 
    ''' </summary>
    ''' <param name="idLugar"></param>
    ''' <remarks></remarks>
    Private Sub EliminarAlmacenes(ByVal idLugar As String)
        Dim consulta As SqlCommand = New SqlCommand
        consulta.CommandType = CommandType.Text
        consulta.CommandText = "delete TUbicacion where codigo =@codigo "
        consulta.Connection = Cn
        consulta.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = idLugar
        consulta.ExecuteScalar()
    End Sub

#End Region



 

    Private Sub dgTabla1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellClick
        enlazarText1()
    End Sub

    Private Sub dgTabla1_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellEnter
        enlazarText()
    End Sub
End Class

