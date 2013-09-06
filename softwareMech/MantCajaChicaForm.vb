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
    ''' Serie de desembolso
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource4 As New BindingSource

    ''' <summary>
    ''' Detalle CajaChica
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource5 As New BindingSource

    ''' <summary>
    ''' serie
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource6 As New BindingSource


    ''' <summary>
    ''' Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' instancia de objeto manejador de datos
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Dim vfCampoDni As String
    Dim vfModificar As String = "modificar"
    ' comandos para creacion de Caja chica
    Dim cmInserTable1 As SqlCommand
    Dim cmUpdateTable1 As SqlCommand
    Dim cmDeleteTable1 As SqlCommand

    ' comandos para cajas (A-B)
    Dim cmInserTableDetalle As SqlCommand
    Dim cmUpdateTableDetalle As SqlCommand
    Dim cmDeleteTableDetalle As SqlCommand

    Dim vfNuevoD As String = "nuevo"
    Dim vfModificarD As String = "modificar"


    ''' <summary>
    ''' variabel aux para contar movimientos
    ''' </summary>
    ''' <remarks></remarks>
    Dim vMovimientos As Integer

#End Region

#Region "Métodos"

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


    Private Sub AgregarRelacion()
        'relacionando CajaChica con Cajas
        Dim relation As New DataRelation("RelacionCaja", dsAlmacen.Tables("TCajaChica").Columns("codCC"), dsAlmacen.Tables("TCajas").Columns("codCC"))
        dsAlmacen.Relations.Add(relation)
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
        Dim sele As String = "Select codCC,fechaCre,codigo,obra,codPers,responsable,codSerO,serie  from VCajaChica"

        'consulta para TCajas
        'sele = "Select codCC,fechaCre,simbolo,codMon,saldo,codigo,obra,codPers,responsable,codEstado,estado,codSerO,serie from VCajaChica"

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


        sele = "select codSerO, serie from TSerieOrden where estado=1"
        crearDataAdapterTable(daTabla6, sele)

        sele = "select codCaj,caja,codMon,moneda,simbolo,saldo,codEstado,estado,codCC from VCajas " 'Where codCC=" '& BindingSource1.Item(BindingSource1.Position)(0)
        crearDataAdapterTable(daTabla5, sele)


        Try
            crearDSAlmacen()

            

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

            daTabla6.Fill(dsAlmacen, "TSerie")
            BindingSource6.DataSource = dsAlmacen
            BindingSource6.DataMember = "TSerie"
            BindingSource6.Sort = "serie ASC"
            cbSerie.DataSource = BindingSource6
            cbSerie.DisplayMember = "serie"
            cbSerie.ValueMember = "codSerO"

            'carga de grilla Cajas Chicas
            daTabla1.Fill(dsAlmacen, "TCajaChica")
            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TCajaChica"
            dgCaja.DataSource = BindingSource1
            BindingNavigator1.BindingSource = BindingSource1

            daTabla5.Fill(dsAlmacen, "TCajas")

            'relacionando las tablas 
            AgregarRelacion()

            BindingSource5.DataSource = BindingSource1
            BindingSource5.DataMember = "RelacionCaja"

            dgDetalleCaja.DataSource = BindingSource5
            BindingNavigator2.BindingSource = BindingSource5





        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub mostrarDataTCajas()
        'If BindingSource1.Position > -1 Then

        '    Dim sele As String = "select codCaj,caja,codMon,moneda,simbolo,saldo,codEstado,estado,codCC from VCajas Where codCC=" & BindingSource1.Item(BindingSource1.Position)(0)
        '    crearDataAdapterTable(daTabla5, sele)

        '    daTabla5.Fill(dsAlmacen, "TCajas")
        '    BindingSource5.DataSource = dsAlmacen
        '    BindingSource5.DataMember = "TCajas"

        '    dgDetalleCaja.DataSource = BindingSource5
        '    BindingNavigator2.BindingSource = BindingSource5

        '    ModificarColumnasDetalleCaja()

        'End If


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
        'dgCaja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        With dgCaja
            .Columns("codCC").HeaderText = "Cod"
            .Columns("codCC").Visible = False

            .Columns("fechaCre").HeaderText = "Creación"
            .Columns("fechaCre").Width = 70
            '.Columns(1).Width = 100

            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").Width = 40
            .Columns("serie").DisplayIndex = 2

            .Columns("codigo").Visible = False

            .Columns("obra").HeaderText = "Obra"
            .Columns("obra").Width = 430
            .Columns("codPers").Visible = False
            .Columns("responsable").HeaderText = "Responsable"
            .Columns("responsable").Width = 300
            '.Columns(7).Width = 70
            
            .Columns("codSerO").Visible = False

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
    Private Sub ModificarColumnasDetalleCaja()
        'oGrilla.ConfigGrilla(dgCaja)
        dgDetalleCaja.ReadOnly = True
        dgDetalleCaja.AllowUserToAddRows = False
        dgDetalleCaja.AllowUserToDeleteRows = False
        dgDetalleCaja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        With dgDetalleCaja
            .Columns("codCC").Visible = False
            .Columns("codCaj").Visible = False

            .Columns("caja").HeaderText = "Caja"
            .Columns("caja").Width = 100

            .Columns("moneda").HeaderText = "Moneda"
            .Columns("moneda").Width = 100

            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").Width = 55
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '.Columns(2).Width = 100
            .Columns("codMon").Visible = False

            .Columns("saldo").HeaderText = "Saldo"
            .Columns("saldo").Width = 70
            .Columns("saldo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("saldo").DefaultCellStyle.Format = "N2"
            '.Columns(6).HeaderText = "Cargo"


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

        For index As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(index) Is RadioButton Then
                CType(Me.Controls(index), RadioButton).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(index) Is Label Then
                CType(Me.Controls(index), Label).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(index) Is Button Then
                CType(Me.Controls(index), Button).ForeColor = ForeColorButtom
            End If

        Next


        'btnNuevo.ForeColor = ForeColorButtom
        'btnModificar.ForeColor = ForeColorButtom
        'btnCancelar.ForeColor = ForeColorButtom
        'btnEliminar.ForeColor = ForeColorButtom
        'btnCerrar.ForeColor = ForeColorButtom
        
    End Sub

    ''' <summary>
    ''' desactiva los botones de acuerdo a la operacion que se realiza
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControles1()
        GroupBox2.Enabled = False
        ' Panel3.Enabled = False
        For i As Integer = 0 To GroupBox1.Controls.Count - 1
            'If TypeOf GroupBox1.Controls(i) Is TextBox Then
            '    CType(GroupBox1.Controls(i), TextBox).ReadOnly = False

            'End If
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


        btnNuevoD.Enabled = False
        btnNuevoD.FlatStyle = FlatStyle.Flat
        btnModificarD.Enabled = False
        btnModificarD.FlatStyle = FlatStyle.Flat
        btnCancelarD.Enabled = False
        btnCancelarD.FlatStyle = FlatStyle.Flat
        btnEliminarD.Enabled = False
        btnEliminarD.FlatStyle = FlatStyle.Flat

        'cbTipoUser.Enabled = False
        ' cbCargo.Enabled = True
        'cbUbi.Enabled = True
        '  Panel1.Enabled = True


    End Sub


    ''' <summary>
    ''' desactiva los botones de acuerdo a la operacion que se realiza para detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControlesDetalle()
        GroupBox3.Enabled = False
        ' Panel3.Enabled = False
        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is ComboBox Then
                Me.Controls(i).Enabled = True
            End If
        Next

        txtCaja.ReadOnly = False

        If vfNuevoD = "guardar" Then
            btnModificarD.Enabled = False
            btnModificarD.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevoD.Enabled = False
            btnNuevoD.FlatStyle = FlatStyle.Flat
            'lbTabla2.Enabled = True
        End If
        btnCancelarD.Enabled = True
        btnCancelarD.FlatStyle = FlatStyle.Standard
        btnEliminarD.Enabled = False
        btnEliminarD.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat

        btnNuevo.Enabled = False
        btnNuevo.FlatStyle = FlatStyle.Flat
        btnModificar.Enabled = False
        btnModificar.FlatStyle = FlatStyle.Flat
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat


       
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

        btnNuevoD.Enabled = True
        btnNuevoD.FlatStyle = FlatStyle.Standard
        btnModificarD.Enabled = True
        btnModificarD.FlatStyle = FlatStyle.Standard
        btnEliminarD.Enabled = True
        btnEliminarD.FlatStyle = FlatStyle.Standard


      
        'txtSaldo.ReadOnly = True

    End Sub

    ''' <summary>
    ''' Activa los botones inciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activarControlesDetalle()
        GroupBox3.Enabled = True
        '    Panel3.Enabled = True
        For i As Integer = 0 To Me.Controls.Count - 1
           
            If TypeOf Me.Controls(i) Is ComboBox Then
                Me.Controls(i).Enabled = False
            End If

        Next
        txtCaja.ReadOnly = True


        btnCancelarD.Enabled = False
        btnCancelarD.FlatStyle = FlatStyle.Flat
        btnNuevoD.Enabled = True
        btnNuevoD.FlatStyle = FlatStyle.Standard
        btnModificarD.Enabled = True
        btnModificarD.FlatStyle = FlatStyle.Standard
        btnEliminarD.Enabled = True
        btnEliminarD.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard

        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard

        'txtSaldo.ReadOnly = True

    End Sub

    ''' <summary>
    ''' Limpiar datos de la tabla personal.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarText()
        txtCaja.Text = ""
        txtSaldo.Text = "0.0"
        RbActivo.Checked = True
        RbInactivo.Checked = False

    End Sub
    ''' <summary>
    ''' registrar los datos de una descripcion de caja inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CajaPorDefecto()
        txtCaja.Text = "Caja Chica"
        cbMoneda.SelectedValue = 30
        btnNuevoD.PerformClick()

    End Sub

    ''' <summary>
    ''' comando para insertar un registro de caja chica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertar()

        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertTCajaChica "
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@fechaCre", SqlDbType.Date).Value = Date.Today


        cmInserTable1.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        cmInserTable1.Parameters.Add("@codPers", SqlDbType.Int).Value = cbResponsable.SelectedValue
        cmInserTable1.Parameters.Add("@codSerie", SqlDbType.Int).Value = cbSerie.SelectedValue
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub ComandoInsertarDetalle(ByVal caja As String, ByVal moneda As Integer, ByVal codCC As Integer)
        cmInserTableDetalle = New SqlCommand
        cmInserTableDetalle.CommandType = CommandType.Text
        cmInserTableDetalle.CommandText = "Insert into TCajas values (@caja,@codMon,0.0,1,@codCC)"
        cmInserTableDetalle.Connection = Cn
        cmInserTableDetalle.Parameters.Add("@caja", SqlDbType.VarChar, 20).Value = caja 'txtCaja.Text.Trim()
        cmInserTableDetalle.Parameters.Add("@codMon", SqlDbType.Int).Value = moneda 'cbMoneda.SelectedValue
        cmInserTableDetalle.Parameters.Add("@codCC", SqlDbType.Int).Value = codCC 'txtCodCaja.Text.Trim() 'BindingSource1.Item(BindingSource1.Position)(0) 'codigo de caja chica
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdate()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        If vMovimientos > 0 Then
            cmUpdateTable1.CommandText = "Update TCajaChica set codPers=@codPers WHERE codCC=@codCC"
        Else
            cmUpdateTable1.CommandText = "Update TCajaChica set fechaCre=@fechaCre,codigo=@codigo,codPers=@codPers,codSerO=@codSerie WHERE codCC=@codCC"
        End If

        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fechaCre", SqlDbType.Date).Value = Date.Today
        cmUpdateTable1.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        cmUpdateTable1.Parameters.Add("@codPers", SqlDbType.Int).Value = cbResponsable.SelectedValue

        cmUpdateTable1.Parameters.Add("@codCC", SqlDbType.Int).Value = BindingSource1.Item(BindingSource1.Position)(0)

        cmUpdateTable1.Parameters.Add("@codSerie", SqlDbType.Int).Value = cbSerie.SelectedValue

    End Sub

    Private Sub comandoUpdateDetalle()

        cmUpdateTableDetalle = New SqlCommand
        cmUpdateTableDetalle.CommandType = CommandType.Text
        If vMovimientos > 0 Then
            cmUpdateTableDetalle.CommandText = "Update TCajas set caja=@caja,estCaja=@estCaja WHERE codCaj=@codCaj"
        Else
            cmUpdateTableDetalle.CommandText = "Update TCajas set caja=@caja,codMon=@codMon,estCaja=@estCaja WHERE codCaj=@codCaj"
        End If

        cmUpdateTableDetalle.Connection = Cn
        cmUpdateTableDetalle.Parameters.Add("@caja", SqlDbType.VarChar, 20).Value = txtCaja.Text.Trim
        cmUpdateTableDetalle.Parameters.Add("@codMon", SqlDbType.Int).Value = cbMoneda.SelectedValue
        'Dim param1 As SqlParameter = New SqlParameter("@saldo", SqlDbType.Decimal)
        'param1.Scale = 2
        'param1.Precision = 10
        'param1.Value = txtSaldo.Text.Trim()
        'cmUpdateTableDetalle.Parameters.Add(param1)

        'Estado
        If RbActivo.Checked Then
            cmUpdateTableDetalle.Parameters.Add("@estCaja", SqlDbType.Int).Value = 1
        Else
            cmUpdateTableDetalle.Parameters.Add("@estCaja", SqlDbType.Int).Value = 0
        End If

        cmUpdateTableDetalle.Parameters.Add("@codCaj", SqlDbType.Int).Value = BindingSource5.Item(BindingSource5.Position)(0)



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


    Private Sub comandoDeleteDetalle()
        cmDeleteTableDetalle = New SqlCommand
        cmDeleteTableDetalle.CommandType = CommandType.Text
        cmDeleteTableDetalle.CommandText = "Delete TCajas WHERE codCaj=@codCaj"
        cmDeleteTableDetalle.Connection = Cn
        cmDeleteTableDetalle.Parameters.Add("@codCaj", SqlDbType.Int).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    ''' <summary>
    ''' enlaza los controles del form con la grilla 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarText()
        If dgCaja.RowCount = 0 Then
            txtCodCaja.Clear()
            'txtSaldo.Text = "0.0"
            'Exit Sub
        Else
            If BindingSource1.Position >= 0 Then
                txtCodCaja.Text = BindingSource1.Item(BindingSource1.Position)(0)
                cbObra.SelectedValue = BindingSource1.Item(BindingSource1.Position)(2)
                cbResponsable.SelectedValue = BindingSource1.Item(BindingSource1.Position)(4)
                cbSerie.SelectedValue = BindingSource1.Item(BindingSource1.Position)(6)

            End If
        End If
    End Sub

    ''' <summary>
    ''' enlaza los controles del form con la grilla Detalle Caja (A-B)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextDetalle()
        If dgDetalleCaja.RowCount = 0 Then
            txtCaja.Clear()
            txtSaldo.Text = "0.0"
            Exit Sub
        Else
            If BindingSource5.Position >= 0 Then
                txtCaja.Text = BindingSource5.Item(BindingSource5.Position)(1)
                cbMoneda.SelectedValue = BindingSource5.Item(BindingSource5.Position)(2)
                txtSaldo.Text = BindingSource5.Item(BindingSource5.Position)(5)
                If BindingSource5.Item(BindingSource5.Position)(6) = 1 Then
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
    ''' valida el registro de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCajaDetalle() As Boolean
        If validaCampoVacio(txtCaja.Text) Then
            MessageBox.Show("Por favor ingrese una Descripción", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtCaja.Focus()

            Return True
        End If

    End Function

    Private Function consultaMovimientoCaja(ByVal cod As Integer) As Integer

        Dim consulta As String = "select count(*) from TMovimientoCaja where codSC=" & cod

        Return CInt(oDataManager.consultarTabla(consulta, CommandType.Text))

    End Function

#End Region



#Region "Eventos Form"




#End Region




    Private Sub MantCajaChicaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim wait As New waitForm
        'wait.Show()
        'Me.Cursor = Cursors.WaitCursor

        configurarColorControl()

        DatosIniciales()

        ModificarColumnasDGV()

        'wait.Close()
        'Me.Cursor = Cursors.Default
    End Sub

    Private Sub MantCajaChicaForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        dgCaja.Dispose()

    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            'limpiarText()
            cbSerie.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else

           
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

            Dim resp As String = MessageBox.Show("¿Está seguro de crear esté registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                cbSerie.Focus()
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()


                comandoInsertar()
                cmInserTable1.Transaction = myTrans

                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                vfCampo1 = cmInserTable1.Parameters("@Identity").Value.ToString()


                myTrans.Commit()

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMytrans = True

               

                'Insertando la Caja por defecto

                ComandoInsertarDetalle("CAJA CHICA", 30, vfCampo1)
                Dim myTrans2 As SqlTransaction = Cn.BeginTransaction
                cmInserTableDetalle.Transaction = myTrans2

                If cmInserTableDetalle.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    myTrans2.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se creo la descripción de caja chica...", nomNegocio, Nothing, MessageBoxIcon.Error)

                End If
                myTrans2.Commit()

                'If dsAlmacen.Tables.Contains("TCajas") Then
                dsAlmacen.Tables("TCajas").Clear()
                'End If
                'Actualizando el Dataset
                dsAlmacen.Tables("TCajaChica").Clear()


                daTabla1.Fill(dsAlmacen, "TCajaChica")
                daTabla5.Fill(dsAlmacen, "TCajas")

                btnCancelar.PerformClick()

                'Ubicando el ítem registrado en la grilla
                BindingSource1.Position = BindingSource1.Find("codCC", vfCampo1)

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
            cbSerie.Focus()
            
            StatusBarClass.messageBarraEstado("")

            Me.AcceptButton = Me.btnModificar

        Else
            'Validar Controles


            vfCampo1 = dgCaja.Rows(BindingSource1.Position).Cells(2).Value
            If vfCampo1 <> cbObra.SelectedValue Then
                If BindingSource1.Find("codigo", cbObra.SelectedValue) >= 0 Then
                    MessageBox.Show("Ya existe una caja chica para la Obra/Ubicación: " & cbObra.Text & Chr(13) & "Cambie de Obra/Lugar... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    cbObra.Focus()
                    Exit Sub
                End If
            End If

            vfCampo1 = dgCaja.Rows(BindingSource1.Position).Cells(4).Value
            If vfCampo1 <> cbResponsable.SelectedValue Then
                If BindingSource1.Find("codPers", cbResponsable.SelectedValue) >= 0 Then
                    MessageBox.Show("La persona : " & cbResponsable.Text & Chr(13) & "ya es responsable de otra caja chica, cambie de persona... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    cbResponsable.Focus()
                    Exit Sub
                End If
            End If
            'consultando movimientos de caja
            If BindingSource5.Position = -1 Then
                vMovimientos = 0
            Else
                vMovimientos = consultaMovimientoCaja(BindingSource5.Item(BindingSource5.Position)(0))
            End If


            If vMovimientos > 0 Then
                MessageBox.Show("Sólo se actualizará los datos de responsable y estado", nomNegocio, Nothing, MessageBoxIcon.Information)

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

                dsAlmacen.Tables("TCajas").Clear()
                'End If
                'Actualizando el Dataset
                dsAlmacen.Tables("TCajaChica").Clear()


                daTabla1.Fill(dsAlmacen, "TCajaChica")
                daTabla5.Fill(dsAlmacen, "TCajas")


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


        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        If dgCaja.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If BindingSource5.Count > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CAJA CHICA TIENE DEPENDENCIAS REGISTRADAS...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            cbSerie.Focus()
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
            dsAlmacen.Tables("TCajas").Clear()
            'End If
            'Actualizando el Dataset
            dsAlmacen.Tables("TCajaChica").Clear()


            daTabla1.Fill(dsAlmacen, "TCajaChica")
            daTabla5.Fill(dsAlmacen, "TCajas")


            enlazarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
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

    Private Sub MantCajaChicaForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.Close()
    End Sub

    Private Sub txtSaldo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSaldo.KeyPress
        ValidarNumeroDecimal(txtSaldo, e)

    End Sub

    Private Sub RbInactivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbInactivo.CheckedChanged
        If RbInactivo.Checked = True Then
            If BindingSource5.Item(BindingSource5.Position)(5) > 0 Then

                StatusBarClass.messageBarraEstado("  PROCESO DENEGADO... EXISTE SALDO MAYOR A CERO...")
                RbActivo.Checked = True
                Exit Sub
            End If

        Else
            StatusBarClass.messageBarraEstado(" ")
        End If
    End Sub

    Private Sub btnNuevoD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoD.Click


        If vfNuevoD = "nuevo" Then
            vfNuevoD = "guardar"
            Me.btnNuevoD.Text = "Guardar"
            desactivarControlesDetalle()
            limpiarText()
            txtCaja.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevoD
        Else

            If ValidarCajaDetalle() Then
                Exit Sub
            End If

            If BindingSource5.Find("caja", txtCaja.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe está descripción: " & txtCaja.Text & Chr(13) & "Cambie de descripción... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtCaja.Focus()
                Exit Sub
            End If

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()
                Dim vCaja As String = txtCaja.Text.Trim()

                ComandoInsertarDetalle(txtCaja.Text.Trim(), cbMoneda.SelectedValue, txtCodCaja.Text.Trim())
                cmInserTableDetalle.Transaction = myTrans

                If cmInserTableDetalle.ExecuteNonQuery() < 1 Then

                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub

                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                'actualizando la información
                dsAlmacen.Tables("TCajas").Clear()
                daTabla5.Fill(dsAlmacen, "TCajas")

                btnCancelarD.PerformClick()

                'ubicando la descripcion de caja ingresada
                BindingSource5.Position = BindingSource5.Find("caja", vCaja)

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

    Private Sub btnModificarD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarD.Click
        If vfModificarD = "modificar" Then
            If dgDetalleCaja.RowCount = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If

            vfModificarD = "actualizar"
            btnModificarD.Text = "Actualizar"
            desactivarControlesDetalle()
            enlazarTextDetalle()
            txtCaja.Focus()
            RbActivo.Enabled = True
            RbInactivo.Enabled = True

            StatusBarClass.messageBarraEstado("")

            Me.AcceptButton = btnModificarD

        Else

            'validar Controles
            If ValidarCajaDetalle() Then
                Exit Sub

            End If

            'consultando movimientos de caja
            vMovimientos = consultaMovimientoCaja(BindingSource5.Item(BindingSource5.Position)(0))

            If vMovimientos > 0 Then
                MessageBox.Show("Sólo se actualizará la descripción de caja", nomNegocio, Nothing, MessageBoxIcon.Information)

            End If

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try

                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

                comandoUpdateDetalle()
                cmUpdateTableDetalle.Transaction = myTrans

                If cmUpdateTableDetalle.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()

                End If
                Dim vCaja As String = txtCaja.Text.Trim()

                myTrans.Commit()
                finalMyTrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON ÉXITO...")

                'Actualizando la info de tcajas
                'actualizando la información
                dsAlmacen.Tables("TCajas").Clear()
                daTabla5.Fill(dsAlmacen, "TCajas")

                btnCancelarD.PerformClick()

                'Buscando por descripción 
                BindingSource5.Position = BindingSource5.Find("caja", vCaja)

                StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")


            Catch f As Exception
                If finalMyTrans Then
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

    Private Sub btnCancelarD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelarD.Click

        vfNuevoD = "nuevo"
        btnNuevoD.Text = "Nuevo"
        vfModificarD = "modificar"
        btnModificarD.Text = "Modificar"
        activarControlesDetalle()
        enlazarTextDetalle()

        RbActivo.Enabled = False
        RbInactivo.Enabled = False

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")

    End Sub

    Private Sub btnEliminarD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarD.Click

        If dgDetalleCaja.RowCount = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If consultaMovimientoCaja(BindingSource5.Item(BindingSource5.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CAJA CHICA TIENE MOVIMIENTOS DE CAJA REGISTRADOS...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtCaja.Focus()
            Exit Sub
        End If

        Dim finalMyTrans As Boolean = False

        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            comandoDeleteDetalle()
            cmDeleteTableDetalle.Transaction = myTrans
            If cmDeleteTableDetalle.ExecuteNonQuery() < 1 Then
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar caja chica porque está actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()
            'confirma la transacción 
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
            finalMyTrans = True

            'actualizando la tabla
            'actualizando la información
            dsAlmacen.Tables("TCajas").Clear()
            daTabla5.Fill(dsAlmacen, "TCajas")

            enlazarTextDetalle()
            StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")


        Catch f As Exception
            If finalMyTrans Then
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

    Private Sub dgDetalleCaja_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDetalleCaja.CurrentCellChanged
        enlazarTextDetalle()
    End Sub

    Private Sub dgCaja_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCaja.CellClick, dgCaja.CellEnter


        If BindingSource1.Position > -1 Then
            Try
                enlazarText()

                '    Dim sele As String = "select codCaj,caja,codMon,moneda,simbolo,saldo,codEstado,estado,codCC from VCajas Where codCC=" & BindingSource1.Item(BindingSource1.Position)(0)
                '    crearDataAdapterTable(daTabla5, sele)

                '    If dsAlmacen.Tables.Contains("TCajas") Then
                '        dsAlmacen.Tables("TCajas").Clear()
                '    End If


                '    daTabla5.Fill(dsAlmacen, "TCajas")
                '    BindingSource5.DataSource = dsAlmacen
                '    BindingSource5.DataMember = "TCajas"

                '    dgDetalleCaja.DataSource = BindingSource5
                '    BindingNavigator2.BindingSource = BindingSource5

                ModificarColumnasDetalleCaja()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
