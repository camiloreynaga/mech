Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantCajaChicaForm

#Region "Variables"
    ''' <summary>
    ''' Caja Chica
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Lugar de trabajo/Obra
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' Personal
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource3 As New BindingSource

    ''' <summary>
    ''' Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Dim vfCampoDni As String
    Dim vfModificar As String = "modificar"

    Dim cmInserTable1 As SqlCommand
    Dim cmUpdateTable1 As SqlCommand
    Dim cmDeleteTable1 As SqlCommand


#End Region

#Region "Métodos"


    '
    ''' <summary>
    ''' Crea un DataAdapter 
    ''' </summary>
    ''' <param name="DATable">Adapater</param>
    ''' <param name="sele">Procedimiento Almacenado</param>
    ''' <remarks></remarks>
    Public Sub crearDataAdapterTableProcedure(ByRef DATable As SqlDataAdapter, ByVal sele As String)
        DATable = New SqlDataAdapter
        Dim cmSele As New SqlCommand
        cmSele.CommandType = CommandType.StoredProcedure
        cmSele.CommandText = sele
        cmSele.Connection = Cn
        'Agregando el comado select al dataAdapter
        DATable.SelectCommand = cmSele
    End Sub

    'Datos de inicio

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
        Dim sele As String = "Select codCC,fechaCre,simbolo,codMon,saldo,codigo,obra,codPers,responsable,codEstado,estado from VCajaChica"
        'sele = "select "
        crearDataAdapterTable(daTabla1, sele)

        
        sele = "PA_LugarTrabajo" '"Select codigo,nombre from tLugarTrabajo"
        'crearDataAdapterTable(daTabla4, sele)
        crearDataAdapterTableProcedure(daTabla2, sele)

        sele = "select codMon,moneda,simbolo from TMoneda"
        '"Select codIde,razon from TIdentidad where idTipId=2"
        'crearDataAdapterTable(daTabla5, sele)
        crearDataAdapterTable(daTabla3, sele)
        'daTabla1.SelectCommand.Parameters.Add("@idDesembolso", SqlDbType.Int).Value = 0

        sele = "select codPers, (nombre +' '+ apellido) as solicitante from Tpersonal where codPers > 1"
        crearDataAdapterTable(daTabla4, sele)

        Try
            crearDSAlmacen()

            daTabla1.Fill(dsAlmacen, "TCajaChica")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TCajaChica"
            dgCaja.DataSource = BindingSource1
            BindingNavigator1.BindingSource = BindingSource1

            daTabla2.Fill(dsAlmacen, "TLugarTrabajo")
            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TLugarTrabajo"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            daTabla3.Fill(dsAlmacen, "TMoneda")
            cbMoneda.DataSource = dsAlmacen.Tables("TMoneda")
            cbMoneda.DisplayMember = "moneda"
            cbMoneda.ValueMember = "codMon"

            daTabla4.Fill(dsAlmacen, "TSolicitante")
            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TSolicitante"
            BindingSource3.Sort = "solicitante ASC"
            cbResponsable.DataSource = BindingSource3
            cbResponsable.DisplayMember = "solicitante"
            cbResponsable.ValueMember = "codPers"

        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    'Botones

    'Validaciones

    'Secuencia de Botones

    'Colores de Frm
    ''' <summary>
    ''' modificar los encabezados de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        oGrilla.ConfigGrilla(dgCaja)
        dgCaja.ReadOnly = True
        dgCaja.AllowUserToAddRows = False
        dgCaja.AllowUserToDeleteRows = False

        With dgCaja
            .Columns("codCC").HeaderText = "Cod"
            .Columns("codCC").Visible = False
            .Columns("fechaCre").HeaderText = "Creación"
            .Columns("fechaCre").Width = 75
            '.Columns(1).Width = 100
            .Columns("simbolo").HeaderText = "Moneda"
            .Columns("simbolo").Width = 55
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '.Columns(2).Width = 100
            .Columns("codMon").Visible = False
            .Columns("saldo").HeaderText = "Saldo"
            .Columns("saldo").Width = 70
            .Columns("saldo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("saldo").DefaultCellStyle.Format = "N2"
            '.Columns(6).HeaderText = "Cargo"
            .Columns("codigo").Visible = False
            .Columns("obra").HeaderText = "Obra"
            .Columns("obra").Width = 270
            .Columns("codPers").Visible = False
            .Columns("responsable").HeaderText = "Responsable"
            .Columns("responsable").Width = 200
            '.Columns(7).Width = 70
            .Columns("codEstado").Visible = False
            .Columns("estado").HeaderText = "Estado"
            .Columns("estado").Width = 60
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



        'Me.lblTitulo.BackColor = TituloBackColorP
        'Me.lblTitulo.ForeColor = HeaderForeColorP
        'Me.lblDerecha.BackColor = TituloBackColorP
        'Me.lblDerecha.ForeColor = HeaderForeColorP
        'Me.Text = nomNegocio

        oGrilla.configurarColorControl("Label", GroupBox1, ForeColorLabel)
        ' oGrilla.configurarColorControl("TextBox", GroupBox1, ForeColorLabel)
        oGrilla.configurarColorControl("RadioButton", GroupBox1, ForeColorLabel)


        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        
    End Sub

    ''' <summary>
    ''' desactiva los botones de acuerdo a la operacion que se realiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControles1()
        GroupBox2.Enabled = False
        ' Panel3.Enabled = False
        For i As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(i) Is TextBox Then
                CType(GroupBox1.Controls(i), TextBox).ReadOnly = False

            End If
            If TypeOf GroupBox1.Controls(i) Is ComboBox Then
                GroupBox1.Controls(i).Enabled = True
            End If

        Next



        If vfNuevo1 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
            'lbTabla2.Enabled = True
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat

        'cbTipoUser.Enabled = False
        ' cbCargo.Enabled = True
        'cbUbi.Enabled = True
        '  Panel1.Enabled = True


    End Sub
    ''' <summary>
    ''' Activa los botones inciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activarControles1()
        GroupBox2.Enabled = True
        '    Panel3.Enabled = True
        For i As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(i) Is TextBox Then
                CType(GroupBox1.Controls(i), TextBox).ReadOnly = True
            End If
            If TypeOf GroupBox1.Controls(i) Is ComboBox Then
                GroupBox1.Controls(i).Enabled = False
            End If

        Next



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

        'txtSaldo.ReadOnly = True
       
    End Sub

    ''' <summary>
    ''' Limpiar datos de la tabla personal.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarText()

        txtSaldo.Text = "0.0"
        RbActivo.Checked = True
        RbInactivo.Checked = False

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertar()

        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "Insert into TCajaChica values (@fechaCre,@codMon,@saldo,@codigo,@codPers,1) "
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@fechaCre", SqlDbType.Date).Value = Date.Today
        cmInserTable1.Parameters.Add("@codMon", SqlDbType.Int).Value = cbMoneda.SelectedValue
        Dim param1 As SqlParameter = New SqlParameter("@saldo", SqlDbType.Decimal)
        param1.Scale = 2
        param1.Precision = 10
        param1.Value = txtSaldo.Text.Trim()
        cmInserTable1.Parameters.Add(param1)
        cmInserTable1.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        cmInserTable1.Parameters.Add("@codPers", SqlDbType.Int).Value = cbResponsable.SelectedValue

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "Update TCajaChica set fechaCre=@fechaCre,codMon=@codMon,saldo=@saldo,codigo=@codigo,codPers=@codPers, estCaja=@estCaja WHERE codCC=@codCC"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fechaCre", SqlDbType.Date).Value = Date.Today
        cmUpdateTable1.Parameters.Add("@codMon", SqlDbType.Int).Value = cbMoneda.SelectedValue
        Dim param1 As SqlParameter = New SqlParameter("@saldo", SqlDbType.Decimal)
        param1.Scale = 2
        param1.Precision = 10
        param1.Value = txtSaldo.Text.Trim()
        cmUpdateTable1.Parameters.Add(param1)
        cmUpdateTable1.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        cmUpdateTable1.Parameters.Add("@codPers", SqlDbType.Int).Value = cbResponsable.SelectedValue

        'Estado
        If RbActivo.Checked Then
            cmUpdateTable1.Parameters.Add("@estCaja", SqlDbType.Int).Value = 1
        Else
            cmUpdateTable1.Parameters.Add("@estCaja", SqlDbType.Int).Value = 0
        End If

        cmUpdateTable1.Parameters.Add("@codCC", SqlDbType.Int).Value = BindingSource1.Item(BindingSource1.Position)(0)

    End Sub

    ''' <summary>
    '''  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDelete()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "Delete TCajaChica WHERE codCC=@codCC"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@codCC", SqlDbType.Int).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    ''' <summary>
    ''' enlaza los controles del form con la grilla 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarText()
        If dgCaja.RowCount = 0 Then
            txtCodCaja.Clear()
            txtSaldo.Text = "0.0"
            Exit Sub
        Else
            If BindingSource1.Position >= 0 Then
                txtCodCaja.Text = BindingSource1.Item(BindingSource1.Position)(0)
                cbMoneda.SelectedValue = BindingSource1.Item(BindingSource1.Position)(3)
                txtSaldo.Text = BindingSource1.Item(BindingSource1.Position)(4)
                cbObra.SelectedValue = BindingSource1.Item(BindingSource1.Position)(5)
                cbResponsable.SelectedValue = BindingSource1.Item(BindingSource1.Position)(7)
                If BindingSource1.Item(BindingSource1.Position)(9) = 1 Then
                    RbActivo.Checked = True
                    RbInactivo.Checked = False
                Else
                    RbInactivo.Checked = True
                    RbActivo.Checked = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' valida caja saldo de Chica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCaja() As Boolean
        'Cantida vacia

        If validaCampoVacio(txtSaldo.Text.Trim) Then
            MessageBox.Show("Por favor ingrese saldo valido", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtSaldo.Focus()
            Return True
        End If

        If IsNumeric(txtSaldo.Text.Trim()) = False Then
            MessageBox.Show("Por favor ingrese saldo valido", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtSaldo.Focus()
            Return True
        End If


    End Function




#End Region



#Region "Eventos Form"




#End Region




    Private Sub MantCajaChicaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        configurarColorControl()
        DatosIniciales()

        ModificarColumnasDGV()

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub




    Private Sub MantCajaChicaForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        dgCaja.Dispose()

    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            limpiarText()
            cbMoneda.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else

            'Validar Controles

            If ValidarCaja() Then
                Exit Sub

            End If

            'Revisa la no duplicidad de Caja en una Obra
            'una caja por obra
            If BindingSource1.Find("codigo", cbObra.SelectedValue) >= 0 Then
                MessageBox.Show("Ya existe una caja chica para la Obra/Ubicación: " & cbObra.Text & Chr(13) & "Cambie de Obra/Lugar... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                cbObra.Focus()
                Exit Sub
            End If

            If BindingSource1.Find("codPers", cbResponsable.SelectedValue) >= 0 Then
                MessageBox.Show("La persona : " & cbResponsable.Text & Chr(13) & "ya es responsable de otra caja chica, cambie de persona... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                cbResponsable.Focus()
                Exit Sub
            End If


            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                vfCampo1 = cbObra.SelectedValue  ' txtCodCaja.Text.Trim()

                comandoInsertar()
                cmInserTable1.Transaction = myTrans

                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMytrans = True

                'Actualizando el Dataset
                dsAlmacen.Tables("TCajaChica").Clear()
                daTabla1.Fill(dsAlmacen, "TCajaChica")

                btnCancelar.PerformClick()

                'Ubicando el ítem registrado en la grilla
                BindingSource1.Position = BindingSource1.Find("codigo", vfCampo1)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")

            Catch f As Exception
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
            Finally
                wait.Close()
            End Try
        End If
    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar = "modificar" Then
            If dgCaja.RowCount = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If

            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            'limpiarText1()
            enlazarText()
            cbMoneda.Focus()
            RbActivo.Enabled = True
            RbInactivo.Enabled = True
            StatusBarClass.messageBarraEstado("")

            Me.AcceptButton = Me.btnModificar

        Else
            'If ValidarCampos() Then
            '    Exit Sub
            'End If
            If ValidarCaja() Then
                Exit Sub

            End If

            vfCampo1 = dgCaja.Rows(BindingSource1.Position).Cells(5).Value
            If vfCampo1 <> cbObra.SelectedValue Then
                If BindingSource1.Find("codigo", cbObra.SelectedValue) >= 0 Then
                    MessageBox.Show("Ya existe una caja chica para la Obra/Ubicación: " & cbObra.Text & Chr(13) & "Cambie de Obra/Lugar... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    cbObra.Focus()
                    Exit Sub
                End If
            End If

            vfCampo1 = dgCaja.Rows(BindingSource1.Position).Cells(7).Value
            If vfCampo1 <> cbResponsable.SelectedValue Then
                If BindingSource1.Find("codPers", cbResponsable.SelectedValue) >= 0 Then
                    MessageBox.Show("La persona : " & cbResponsable.Text & Chr(13) & "ya es responsable de otra caja chica, cambie de persona... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    cbResponsable.Focus()
                    Exit Sub
                End If
            End If


            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

                comandoUpdate()
                cmUpdateTable1.Transaction = myTrans
                If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfCampo1 = txtCodCaja.Text.Trim()

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON ÉXITO...")

                'Actualizando el dataSet dsHPI
                dsAlmacen.Tables("TCajaChica").Clear()
                daTabla1.Fill(dsAlmacen, "TCajaChica")

                btnCancelar.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("codCC", vfCampo1)

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

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        activarControles1()
        enlazarText()

        RbActivo.Enabled = False
        RbInactivo.Enabled = False

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        If dgCaja.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If



        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            cbMoneda.Focus()
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
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar caja chica porque está actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
            finalMytrans = True

            'Actualizando el dataSet dsAlmacen
            dsAlmacen.Tables("TCajaChica").Clear()
            daTabla1.Fill(dsAlmacen, "TCajaChica")

            enlazarText()
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

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgCaja.Dispose()
        Me.Close()

    End Sub


    Private Sub dgCaja_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCaja.CellClick, dgCaja.CellEnter
        enlazarText()
    End Sub


    Private Sub MantCajaChicaForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.Close()
    End Sub
End Class
