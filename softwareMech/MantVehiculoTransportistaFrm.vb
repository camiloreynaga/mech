Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008



Public Class MantVehiculoTransportistaFrm

#Region "Variables"
    ''' <summary>
    ''' Empresa transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' Conductor / Transportista
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource3 As New BindingSource

    ''' <summary>
    '''  Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' auxiliar para nuevo Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfNuevo1 As String = "nuevo"
    ''' <summary>
    ''' auxiliar para vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfCampo1 As String
    ''' <summary>
    ''' auxiliar para Modificar vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfModificar As String = "modificar"

    ''' <summary>
    ''' auxiliar para nuevo Conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfNuevoC As String = "nuevo"
    ''' <summary>
    ''' auxiliar para conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfCampoC As String
    ''' <summary>
    ''' auxiliar para Modificar conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfModificarC As String = "modificar"

    ''' <summary>
    ''' command Insert Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInserTable1 As SqlCommand
    ''' <summary>
    ''' command Update Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateTable1 As SqlCommand
    ''' <summary>
    ''' command Delete vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteTable1 As SqlCommand

    ''' <summary>
    ''' command Insert Conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInserTable2 As SqlCommand
    ''' <summary>
    ''' command Update Conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateTable2 As SqlCommand
    ''' <summary>
    ''' command Delete Conductor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteTable2 As SqlCommand


#End Region



#Region "Métodos"

    ''' <summary>
    ''' Metodo que carga los datos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosIniciales()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        wait.Show()
        Dim sele As String = "select codET,nombre,ruc from TEmpTransp where codET>1"
        'sele = "select "
        crearDataAdapterTable(daTabla1, sele)


        sele = "select codVeh,marcaNro,nroConst,codET  from TVehiculo "
        'crearDataAdapterTable(daTabla4, sele)
        crearDataAdapterTable(daTabla2, sele)

        sele = "select codT,nombre,DNI,codET,nroLic from TTransportista  "
        crearDataAdapterTable(daTabla3, sele)
        'daTabla1.SelectCommand.Parameters.Add("@idDesembolso", SqlDbType.Int).Value = 0
        Try
            crearDSAlmacen()

            daTabla1.Fill(dsAlmacen, "TEmpTransp")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TEmpTransp"
            BindingSource1.Sort = "nombre ASC"

            cbEmpTrans.DataSource = BindingSource1
            cbEmpTrans.DisplayMember = "nombre"
            cbEmpTrans.ValueMember = "codET"

            daTabla2.Fill(dsAlmacen, "TVehiculo")
            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TVehiculo"
            dgVehiculo.DataSource = BindingSource2

            daTabla3.Fill(dsAlmacen, "TTransportista")
            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TTransportista"
            dgConductor.DataSource = BindingSource3

            BindingSource3.Sort = "nombre ASC"


        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    ''' <summary>
    ''' Configurar los colores del control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()
        Me.BackColor = BackColorP



        'Me.lblTitulo.BackColor = TituloBackColorP
        'Me.lblTitulo.ForeColor = HeaderForeColorP
        'Me.lblDerecha.BackColor = TituloBackColorP
        'Me.lblDerecha.ForeColor = HeaderForeColorP
        'Me.Text = nomNegocio
        Label4.ForeColor = ForeColorLabel

        oGrilla.configurarColorControl("Label", GroupBox1, ForeColorLabel)
        ' oGrilla.configurarColorControl("TextBox", GroupBox1, ForeColorLabel)
        oGrilla.configurarColorControl("ComboBox", GroupBox1, ForeColorLabel)

        oGrilla.configurarColorControl("Label", GroupBox3, ForeColorLabel)
        oGrilla.configurarColorControl("ComboBox", GroupBox3, ForeColorLabel)


        btnNuevoV.ForeColor = ForeColorButtom
        btnModificarV.ForeColor = ForeColorButtom
        btnCancelarV.ForeColor = ForeColorButtom
        btnEliminarV.ForeColor = ForeColorButtom

        btnNuevoC.ForeColor = ForeColorButtom
        btnModificarC.ForeColor = ForeColorButtom
        btnCancelarC.ForeColor = ForeColorButtom
        btnEliminarC.ForeColor = ForeColorButtom

        btnCerrar.ForeColor = ForeColorButtom

    End Sub

    ''' <summary>
    ''' modificar los encabezados de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        oGrilla.ConfigGrilla(dgVehiculo)
        dgVehiculo.ReadOnly = True
        dgVehiculo.AllowUserToAddRows = False
        dgVehiculo.AllowUserToDeleteRows = False

        With dgVehiculo
            .Columns("codVeh").HeaderText = "Cod"
            .Columns("codVeh").Visible = False
            .Columns("marcaNro").HeaderText = "Marca/Placa"
            .Columns("marcaNro").Width = 155
            .Columns("nroConst").HeaderText = "N° Constancia"
            .Columns("nroConst").Width = 120

            .Columns("codET").Visible = False

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    ''' <summary>
    ''' modificar los encabezados de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGVConductor()
        oGrilla.ConfigGrilla(dgConductor)
        dgConductor.ReadOnly = True
        dgConductor.AllowUserToAddRows = False
        dgConductor.AllowUserToDeleteRows = False

        With dgConductor
            .Columns("codT").Visible = False
            .Columns("nombre").HeaderText = "Chofer"
            .Columns("nombre").Width = 175
            .Columns("DNI").HeaderText = "DNI"
            .Columns("DNI").Width = 60
            .Columns("codET").Visible = False
            .Columns("nroLic").HeaderText = "N° Licencia"
            .Columns("nroLic").Width = 100
            .Columns("saldo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertarVehiculo()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "Insert into TVehiculo values (@marcaNro,@nroConst,@codET) "
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@marcaNro", SqlDbType.VarChar, 40).Value = txtMarcaPlaca.Text.Trim
        cmInserTable1.Parameters.Add("@nroConst", SqlDbType.VarChar, 40).Value = txtConstancia.Text.Trim()
        cmInserTable1.Parameters.Add("@codET", SqlDbType.Int).Value = cbEmpTrans.SelectedValue
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdateVehiculo()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "Update TVehiculo set marcaNro=@marcaNro,nroConst=@nroConst,codET=@codET WHERE codVeh=@codVeh"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@marcaNro", SqlDbType.VarChar, 40).Value = txtMarcaPlaca.Text.Trim
        cmUpdateTable1.Parameters.Add("@nroConst", SqlDbType.VarChar, 40).Value = txtConstancia.Text.Trim
        cmUpdateTable1.Parameters.Add("@codET", SqlDbType.Int).Value = cbEmpTrans.SelectedValue

        cmUpdateTable1.Parameters.Add("@codVeh", SqlDbType.Int).Value = BindingSource2.Item(BindingSource2.Position)(0)

    End Sub

    ''' <summary>
    '''  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDeleteVehiculo()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "Delete TVehiculo WHERE codVeh=@codVeh"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@codVeh", SqlDbType.Int).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertarConductor()
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "Insert into TTransportista values (@nombre,@DNI,@nroLic,@codET) "
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@nombre", SqlDbType.VarChar, 60).Value = txtNombre.Text.Trim
        cmInserTable2.Parameters.Add("@DNI", SqlDbType.VarChar, 8).Value = txtDni.Text.Trim()
        cmInserTable2.Parameters.Add("@nroLic", SqlDbType.VarChar, 30).Value = txtLicencia.Text.Trim()
        cmInserTable2.Parameters.Add("@codET", SqlDbType.Int).Value = cbEmpTrans.SelectedValue
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdateConductor()
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "Update TTransportista set nombre=@nombre,DNI=@DNI,nroLic=@nroLic,codET=@codET WHERE codT=@codT"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@nombre", SqlDbType.VarChar, 60).Value = txtMarcaPlaca.Text.Trim
        cmUpdateTable2.Parameters.Add("@DNI", SqlDbType.VarChar, 8).Value = txtConstancia.Text.Trim
        cmUpdateTable2.Parameters.Add("@nroLic", SqlDbType.VarChar, 30).Value = txtLicencia.Text
        cmUpdateTable2.Parameters.Add("@codET", SqlDbType.Int).Value = cbEmpTrans.SelectedValue
        cmUpdateTable2.Parameters.Add("@codVeh", SqlDbType.Int).Value = BindingSource3.Item(BindingSource3.Position)(0)

    End Sub

    ''' <summary>
    '''  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDeleteConductor()
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "Delete TTransportista WHERE codT=@codT"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@codT", SqlDbType.Int).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    ''' <summary>
    ''' enlaza los controles del form con la grilla Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextVehiculo()
        If dgVehiculo.RowCount = 0 Then
            txtCodVehiculo.Clear()
            txtMarcaPlaca.Clear()
            txtConstancia.Clear()
            Exit Sub
        Else
            If BindingSource1.Position >= 0 Then
                txtMarcaPlaca.Text = BindingSource2.Item(BindingSource2.Position)(1)
                txtConstancia.Text = BindingSource2.Item(BindingSource2.Position)(2)
                cbEmpTrans.SelectedValue = BindingSource2.Item(BindingSource2.Position)(3)
            End If
        End If
    End Sub
    ''' <summary>
    ''' enlaza los controles del form con la grilla Conductor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextConductor()
        If dgConductor.RowCount = 0 Then
            txtCodConductor.Clear()
            txtNombre.Clear()
            txtDni.Clear()
            txtLicencia.Clear()
            Exit Sub
        Else
            If BindingSource1.Position >= 0 Then
                txtNombre.Text = BindingSource3.Item(BindingSource3.Position)(1)
                txtDni.Text = BindingSource3.Item(BindingSource3.Position)(2)
                cbEmpTrans.SelectedValue = BindingSource3.Item(BindingSource3.Position)(3)
                txtLicencia.Text = BindingSource3.Item(BindingSource3.Position)(4)
            End If
        End If
    End Sub


    ''' <summary>
    ''' desactiva los botones de acuerdo a la operacion que se realiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControlesVehiculo()
        GroupBox2.Enabled = False

        GroupBox3.Enabled = False
        GroupBox4.Enabled = False

        'Panel3.Enabled = False
        For i As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(i) Is TextBox Then
                CType(GroupBox1.Controls(i), TextBox).ReadOnly = False

            End If
            If TypeOf GroupBox1.Controls(i) Is ComboBox Then
                GroupBox1.Controls(i).Enabled = True
            End If
        Next

        If vfNuevo1 = "guardar" Then
            btnModificarV.Enabled = False
            btnModificarV.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevoV.Enabled = False
            btnNuevoV.FlatStyle = FlatStyle.Flat
            'lbTabla2.Enabled = True
        End If
        btnCancelarV.Enabled = True
        btnCancelarV.FlatStyle = FlatStyle.Standard
        btnEliminarV.Enabled = False
        btnEliminarV.FlatStyle = FlatStyle.Flat

        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat

        'Botones Vehiculo
        btnNuevoC.Enabled = False
        btnModificarC.Enabled = False
        btnCancelarC.Enabled = False
        btnEliminarC.Enabled = False

    End Sub
    ''' <summary>
    ''' Activa los botones inciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activarControlesVehiculo()
        GroupBox2.Enabled = True

        GroupBox3.Enabled = True
        GroupBox4.Enabled = True

        '    Panel3.Enabled = True
        For i As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(i) Is TextBox Then
                CType(GroupBox1.Controls(i), TextBox).ReadOnly = True
            End If
            If TypeOf GroupBox1.Controls(i) Is ComboBox Then
                GroupBox1.Controls(i).Enabled = False
            End If

        Next



        btnCancelarV.Enabled = False
        btnCancelarV.FlatStyle = FlatStyle.Flat
        btnNuevoV.Enabled = True
        btnNuevoV.FlatStyle = FlatStyle.Standard
        btnModificarV.Enabled = True
        btnModificarV.FlatStyle = FlatStyle.Standard
        btnEliminarV.Enabled = True
        btnEliminarV.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard

        'txtSaldo.ReadOnly = True

        'Botones Conductor
        btnNuevoC.Enabled = True
        btnModificarC.Enabled = True
        btnCancelarC.Enabled = True
        btnEliminarC.Enabled = True

    End Sub



    ''' <summary>
    ''' desactiva los botones de Conductor de acuerdo a la operacion que se realiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControlesConductor()
        GroupBox4.Enabled = False
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False

        'Panel3.Enabled = False
        For i As Integer = 0 To GroupBox3.Controls.Count - 1
            If TypeOf GroupBox3.Controls(i) Is TextBox Then
                CType(GroupBox3.Controls(i), TextBox).ReadOnly = False

            End If
            If TypeOf GroupBox3.Controls(i) Is ComboBox Then
                GroupBox3.Controls(i).Enabled = True
            End If
        Next

        If vfNuevoC = "guardar" Then
            btnModificarC.Enabled = False
            btnModificarC.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevoC.Enabled = False
            btnNuevoC.FlatStyle = FlatStyle.Flat
            'lbTabla2.Enabled = True
        End If
        btnCancelarC.Enabled = True
        btnCancelarC.FlatStyle = FlatStyle.Standard
        btnEliminarC.Enabled = False
        btnEliminarC.FlatStyle = FlatStyle.Flat

        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat

        'Botones Vehiculo
        btnNuevoV.Enabled = False
        btnModificarV.Enabled = False
        btnCancelarV.Enabled = False
        btnEliminarV.Enabled = False


    End Sub
    ''' <summary>
    ''' Activa los botones inciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activarControlesConductor()
        GroupBox4.Enabled = True
        GroupBox1.Enabled = True
        GroupBox2.Enabled = True

        '    Panel3.Enabled = True
        For i As Integer = 0 To GroupBox3.Controls.Count - 1
            If TypeOf GroupBox3.Controls(i) Is TextBox Then
                CType(GroupBox3.Controls(i), TextBox).ReadOnly = True
            End If
            If TypeOf GroupBox3.Controls(i) Is ComboBox Then
                GroupBox3.Controls(i).Enabled = False
            End If

        Next

        btnCancelarC.Enabled = False
        btnCancelarC.FlatStyle = FlatStyle.Flat
        btnNuevoC.Enabled = True
        btnNuevoC.FlatStyle = FlatStyle.Standard
        btnModificarC.Enabled = True
        btnModificarC.FlatStyle = FlatStyle.Standard
        btnEliminarC.Enabled = True
        btnEliminarC.FlatStyle = FlatStyle.Standard

        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard

        'Botones Vehiculo
        btnNuevoV.Enabled = True
        btnModificarV.Enabled = True

        btnCancelarV.Enabled = True
        btnEliminarV.Enabled = True



        'txtSaldo.ReadOnly = True

    End Sub


    ''' <summary>
    ''' Limpiar los controles text del forma para Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarTextVehiculo()
        txtCodVehiculo.Clear()
        txtMarcaPlaca.Clear()
        txtConstancia.Clear()
    End Sub
    ''' <summary>
    ''' Limpiar los controles text del forma para Vehiculo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarTextConductor()
        txtCodConductor.Clear()
        txtNombre.Clear()
        txtDni.Clear()
        txtLicencia.Clear()
    End Sub



#End Region





#Region "Eventos"

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgConductor.Dispose()
        dgVehiculo.Dispose()
        Me.Close()
    End Sub

#End Region

    
    Private Sub MantVehiculoTransportistaFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        configurarColorControl()
        DatosIniciales()

        ModificarColumnasDGV()
        ModificarColumnasDGVConductor()

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnNuevoV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoV.Click

        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevoV.Text = "Guardar"
            desactivarControlesVehiculo()
            limpiarTextVehiculo()
            cbEmpTrans.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevoV

        Else

            'Validar controles

            'If BindingSource2. 
            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try

                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                vfCampo1 = txtMarcaPlaca.Text

                If BindingSource2.Find("marcaNro", txtMarcaPlaca.Text.Trim) >= 0 Then
                    MessageBox.Show("Ya existe un vehiculo de marca y placa: " & txtMarcaPlaca.Text.Trim & Chr(13) & "Cambie de marca y placa... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtMarcaPlaca.Focus()
                    Exit Sub
                End If


                comandoInsertarVehiculo()
                cmInserTable1.Transaction = myTrans

                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub

                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                'Actualizando el dataset

                dsAlmacen.Tables("TVehiculo").Clear()
                daTabla2.Fill(dsAlmacen, "TVehiculo")

                btnCancelarV.PerformClick()

                'Ubicando el ítem registrado
                BindingSource2.Position = BindingSource2.Find("marcaNro", vfCampo1)

                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")


            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If


            Finally
                wait.Close()

            End Try

        End If

    End Sub

    Private Sub btnModificarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarV.Click

        If vfModificar = "modificar" Then
            If dgVehiculo.RowCount = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If

            vfModificar = "actualizar"
            btnModificarV.Text = "Actualizar"
            desactivarControlesVehiculo()
            'limpiarText1()
            enlazarTextVehiculo()
            'cbEmpTrans.Focus()
            txtMarcaPlaca.Focus()
            StatusBarClass.messageBarraEstado("")

            Me.AcceptButton = Me.btnModificarV
        Else

            vfCampo1 = dgVehiculo.Rows(BindingSource2.Position).Cells(1).Value
            If vfCampo1 <> txtMarcaPlaca.Text.Trim Then
                If BindingSource2.Find("marcaNro", txtMarcaPlaca.Text.Trim) >= 0 Then
                    MessageBox.Show("Ya existe un vehiculo de marca y placa: " & txtMarcaPlaca.Text.Trim & Chr(13) & "Cambie de marca y placa... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtMarcaPlaca.Focus()
                    Exit Sub
                End If
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

                comandoUpdateVehiculo()
                cmUpdateTable1.Transaction = myTrans
                If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampo1 = txtMarcaPlaca.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON ÉXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("TVehiculo").Clear()
                daTabla2.Fill(dsAlmacen, "TVehiculo")

                btnCancelarV.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource2.Position = BindingSource2.Find("marcaNro", vfCampo1)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")
                'wait.Close()

            Catch f As Exception

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
            Finally
                wait.Close()
            End Try

        End If


    End Sub

    Private Sub btnCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarV.Click

        vfNuevo1 = "nuevo"
        btnNuevoV.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificarV.Text = "Modificar"
        activarControlesVehiculo()
        enlazarTextVehiculo()

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")


    End Sub

    Private Sub btnEliminarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarV.Click

        If dgVehiculo.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtMarcaPlaca.Focus()
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
            comandoDeleteVehiculo()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar vehiculo porque está actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
            finalMytrans = True

            'Actualizando el dataSet dsAlmacen
            dsAlmacen.Tables("TVehiculo").Clear()
            daTabla2.Fill(dsAlmacen, "TVehiculo")

            enlazarTextVehiculo()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
            wait.Close()
        Catch f As Exception

            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If

        Finally
            wait.Close()
        End Try

    End Sub


    Private Sub btnNuevoC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoC.Click
        If vfNuevoC = "nuevo" Then
            vfNuevoC = "guardar"
            Me.btnNuevoC.Text = "Guardar"
            desactivarControlesConductor()
            limpiarTextConductor()
            cbEmpTrans.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevoC

        Else

            'Validar controles

            'If BindingSource2. 
            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            vfCampoC = txtDni.Text.Trim()

            If BindingSource3.Find("DNI", txtDni.Text.Trim) >= 0 Then
                MessageBox.Show("Ya existe un chofer con el DNI: " & txtDni.Text.Trim & Chr(13) & "Cambie de DNI... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtDni.Focus()
                Exit Sub
            End If

            Try

                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                


                comandoInsertarConductor()
                cmInserTable2.Transaction = myTrans

                If cmInserTable2.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub

                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                'Actualizando el dataset

                dsAlmacen.Tables("TTransportista").Clear()
                daTabla3.Fill(dsAlmacen, "TTransportista")

                btnCancelarC.PerformClick()

                'Ubicando el ítem registrado
                BindingSource3.Position = BindingSource3.Find("DNI", vfCampo1)

                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")


            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If


            Finally
                wait.Close()

            End Try

        End If
    End Sub

    Private Sub btnModificarC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarC.Click
        If vfModificarC = "modificar" Then
            If dgConductor.RowCount = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificarC = "actualizar"
            btnModificarC.Text = "Actualizar"
            desactivarControlesConductor()
            'limpiarText1()
            enlazarTextConductor()
            'cbEmpTrans.Focus()
            txtDni.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificarC
        Else

            vfCampoC = dgVehiculo.Rows(BindingSource3.Position).Cells(2).Value
            If vfCampoC <> txtDni.Text.Trim Then
                If BindingSource3.Find("DNI", txtDni.Text.Trim) >= 0 Then
                    MessageBox.Show("Ya existe un chofer con el DNI: " & txtDni.Text.Trim & Chr(13) & "Cambie de DNI... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtDni.Focus()
                    Exit Sub
                End If
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

                comandoUpdateConductor()
                cmUpdateTable2.Transaction = myTrans
                If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampoC = txtDni.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON ÉXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("TTransportista").Clear()
                daTabla3.Fill(dsAlmacen, "TTransportista")

                btnCancelarC.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource3.Position = BindingSource3.Find("DNI", vfCampoC)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")
                'wait.Close()

            Catch f As Exception

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
            Finally
                wait.Close()
            End Try
        End If
    End Sub

    Private Sub btnEliminarC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarC.Click

        If dgConductor.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtDni.Focus()
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
            comandoDeleteConductor()
            cmDeleteTable2.Transaction = myTrans
            If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar Conductor porque está actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
            finalMytrans = True

            'Actualizando el dataSet dsAlmacen
            dsAlmacen.Tables("TTransportista").Clear()
            daTabla3.Fill(dsAlmacen, "TTransportista")

            enlazarTextConductor()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
            wait.Close()
        Catch f As Exception

            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If

        Finally
            wait.Close()
        End Try
    End Sub

   

    Private Sub btnCancelarC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarC.Click

        vfNuevoC = "nuevo"
        btnNuevoC.Text = "Nuevo"
        vfModificarC = "modificar"
        btnModificarC.Text = "Modificar"
        activarControlesConductor()
        enlazarTextConductor()

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")

    End Sub

    Private Sub dgVehiculo_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgVehiculo.CellClick, dgVehiculo.CellEnter
        enlazarTextVehiculo()
    End Sub

    Private Sub dgConductor_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgConductor.CellClick, dgConductor.CellEnter
        enlazarTextConductor()
    End Sub

    Private Sub cbEmpTrans_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEmpTrans.SelectedIndexChanged
        If BindingSource1.Count >= 0 Then

            Try

                BindingSource2.Filter = "codET=" & cbEmpTrans.SelectedValue
                BindingSource3.Filter = "codET=" & cbEmpTrans.SelectedValue

            Catch ex As Exception

            End Try
        End If
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
End Class
