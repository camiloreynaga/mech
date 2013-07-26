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
    Dim cmUpdateTable15 As SqlCommand
    Dim cmInserTable1 As SqlCommand
    Dim cmUpdateTable As SqlCommand
    Dim cmDeleteTable As SqlCommand
    Dim cmDeleteTable1 As SqlCommand
    Dim cmInserTable13 As SqlCommand
    Dim cmUpdateTable1 As SqlCommand
    Dim cmUpdateTable13 As SqlCommand

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
        Dim sele As String '= "Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,moneda,simbolo,solicitante,ruc,fono,email,codObra,codIde from VOrdenDesembolsoSeguimiento"
        'sele = "select "
        'crearDataAdapterTable(daVDetOrden, sele)

        
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
        With dgCaja
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



        'Me.lblTitulo.BackColor = TituloBackColorP
        'Me.lblTitulo.ForeColor = HeaderForeColorP
        'Me.lblDerecha.BackColor = TituloBackColorP
        'Me.lblDerecha.ForeColor = HeaderForeColorP
        'Me.Text = nomNegocio

        oGrilla.configurarColorControl("Label", GroupBox1, ForeColorLabel)
        ' oGrilla.configurarColorControl("TextBox", GroupBox1, ForeColorLabel)
        oGrilla.configurarColorControl("CheckBox", GroupBox1, ForeColorLabel)


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
        'Panel2.Enabled = False
        ' Panel3.Enabled = False

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
        ' Panel2.Enabled = True
        '    Panel3.Enabled = True

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
        'lbTabla2.Enabled = False

        'txtNom.ReadOnly = True
        'txtApe.ReadOnly = True
        'txtDirec.ReadOnly = True
        'txtDni.ReadOnly = True
        'txtEmail.ReadOnly = True
        'txtFono.ReadOnly = True

        'cbCargo.Enabled = False
        ' cbUbi.Enabled = False
        'cbTipoUser.Enabled = True
    End Sub

#End Region



   

    Private Sub MantCajaChicaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        configurarColorControl()
        DatosIniciales()
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgCaja.Dispose()
        Me.Close()

    End Sub

    Private Sub MantCajaChicaForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        dgCaja.Dispose()

    End Sub
End Class
