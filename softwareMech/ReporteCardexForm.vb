Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class ReporteCardexForm

#Region "Variables"
    ''' <summary>
    ''' Materiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource
    ''' <summary>
    ''' Kardex
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    Dim oGrilla As New cConfigFormControls

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Carga un ComboBox Usando un data reader
    ''' </summary>
    ''' <param name="storedProcedure">Nombre del procedimiento almacenado</param>
    ''' <param name="combo">ComboBox</param>
    ''' <param name="ValueMember">Valor del Item</param>
    ''' <param name="DisplayMember">Descripcion del Item</param>
    ''' <remarks></remarks>
    Public Sub CargarCombo(ByVal storedProcedure As String, ByVal Type As CommandType, ByVal combo As ComboBox, ByVal ValueMember As String, ByVal DisplayMember As String)
        Dim con As SqlConnection = Cn
        Dim command As New SqlCommand(storedProcedure, con)
        Dim dataR As SqlDataReader

        command.CommandType = Type 'CommandType.StoredProcedure
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        dataR = command.ExecuteReader(CommandBehavior.CloseConnection)
        Dim lista As New List(Of Dato)

        Try
            While dataR.Read()
                Dim item As New Dato
                item.Id = CStr(dataR(ValueMember))
                item.Desc = CStr(dataR(DisplayMember))
                lista.Add(item)
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()
        End Try

        combo.DataSource = lista
        combo.ValueMember = "Id"
        combo.DisplayMember = "Desc"

    End Sub

    ''' <summary>
    ''' Llenar un Datagrid de datos desde una BD
    ''' </summary>
    ''' <param name="consulta">consulta o stored procedure</param>
    ''' <param name="type">tipo </param>
    ''' <param name="grilla">DatagridView</param>
    ''' <param name="dataView">DataView</param>
    ''' <remarks></remarks>
    Public Sub CargarGrilla(ByVal consulta As String, ByVal type As CommandType, ByVal grilla As DataGridView, ByVal dataView As DataView)
        Dim con As SqlConnection = Cn
        Dim storedProcedure As String = consulta '
        Dim command As New SqlCommand(consulta, con)
        Dim dataR As SqlDataReader

        command.CommandType = type  ' tipo de Comando
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim oTabla As New DataTable

        dataR = command.ExecuteReader() 'CommandBehavior.CloseConnection)
        'Dim lista As New List(Of Dato)
        ' oTabla.Rows.Add(New DataRow(
        Try

            oTabla.Load(dataR, LoadOption.OverwriteChanges)

            dataView = oTabla.DefaultView
            'While dataR.Read()
            '    grilla.Rows.Add(dataR.GetInt32(0), dataR.GetString(1), dataR.GetString(2), dataR.GetDecimal(3), dataR.GetString(4), dataR.GetString(5))
            'End While


            grilla.DataSource = dataView  'oTabla
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Llenar un Datagrid de datos desde una BD
    ''' </summary>
    ''' <param name="consulta">consulta o stored procedure</param>
    ''' <param name="type">tipo </param>
    ''' <param name="grilla">DatagridView</param>
    ''' <param name="bindingSource">BindingSource</param>
    ''' <remarks></remarks>
    Public Function CargarGrilla(ByVal consulta As String, ByVal type As CommandType, ByVal grilla As DataGridView, ByVal bindingSource As BindingSource) As Integer
        Dim con As SqlConnection = Cn
        Dim storedProcedure As String = consulta '
        Dim command As New SqlCommand(consulta, con)
        Dim dataR As SqlDataReader

        command.CommandType = type  ' tipo de Comando
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim oTabla As New DataTable

        dataR = command.ExecuteReader() 'CommandBehavior.CloseConnection)
        'Dim lista As New List(Of Dato)
        ' oTabla.Rows.Add(New DataRow(
        Try

            oTabla.Load(dataR, LoadOption.OverwriteChanges)

            'llena la grilla con el binding source
            bindingSource.DataSource = oTabla '.DefaultView

            'While dataR.Read()
            '    grilla.Rows.Add(dataR.GetInt32(0), dataR.GetString(1), dataR.GetString(2), dataR.GetDecimal(3), dataR.GetString(4), dataR.GetString(5))
            'End While


            grilla.DataSource = bindingSource  'oTabla
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()

        End Try
        Return oTabla.Rows.Count
    End Function


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
        sele = "select  codGuiaE, talon, nroGuia, fecIni, codSerS,razon,  codIde, codestado, Estado,Origen,Destino,codUbiOri,codUbiDes,partida, llegada, codET, empTrans,marcaNro,codVeh,nombre,  codT, motivo,   codMotG, nroFact, obs, Personal,  codPers,ruc from VSeguimientoGR"
        crearDataAdapterTable(daTabla1, sele)

        'sele = "PA_SeguimientoComprobantes"

        sele = "Select codigo,nombre from tLugarTrabajo" '"PA_LugarTrabajo" '"Select codigo,nombre from tLugarTrabajo"
        crearDataAdapterTable(daTabla3, sele)

        sele = "select codSerS,serie from VSeguimientoGRSerie "
        crearDataAdapterTable(daTabla5, sele)
        Try
            'daTabla5.Fill(dsAlmacen, "TIdentidad")
            'BindingSource5.DataSource = dsAlmacen
            'BindingSource5.DataMember = "TIdentidad"
            '' BindingSource5.Filter = "idTipId=2"
            'BindingSource5.Sort = "razon asc"
            'cbProveedor.DataSource = BindingSource5
            'cbProveedor.DisplayMember = "razon"
            'cbProveedor.ValueMember = "codIde"

            'daTabla6.Fill(dsAlmacen, "TSolicitante")
            'BindignSource6.DataSource = dsAlmacen
            'BindignSource6.DataMember = "TSolicitante"
            'BindignSource6.Sort = "solicitante ASC"
            'cbSolicitante.ComboBox.DataSource = BindignSource6
            'cbSolicitante.ComboBox.DisplayMember = "solicitante"
            'cbSolicitante.ComboBox.ValueMember = "solicitante"

        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    ''' <summary>
    ''' configura los colores de los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()

        Me.BackColor = BackColorP
        'Color para los labels del contenedor principal
        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then 'LABELS
                Me.Controls(i).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(i) Is CheckBox Then 'CHECKBOX
                Me.Controls(i).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(i) Is GroupBox Then 'TEXTBOX
                For c As Integer = 0 To Me.Controls(i).Controls.Count - 1
                    oGrilla.configurarColorControl("Label", Me.Controls(i), ForeColorLabel)
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Da color a columnas especificas en la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ColorearGrilla()
        'Encabezado
        oGrilla.colorearFilasDGV(dgCardex, "tipo", "INGRESO", Color.Green, Color.White)
        oGrilla.colorearFilasDGV(dgCardex, "tipo", "SALIDA", Color.Red, Color.White)
    End Sub

    ''' <summary>
    ''' Establece un estilo de fuente a las columnas de la grila, 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FuenteColumnaGrilla()
        'pone en negrita la columna stock
        oGrilla.EstiloColumnaDGV(dgInsumos, "stock", New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    End Sub

    ''' <summary>
    ''' Customiza la grilla  Detalle GR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV_Insumo()
        'oGrilla.ConfigGrilla(dgInsumos)
        dgInsumos.ReadOnly = True
        dgInsumos.AllowUserToAddRows = False
        dgInsumos.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try
            With dgInsumos
                'codigo material
                .Columns("codmat").Visible = False
                ' .Columns("codmat").Width = 50
                'Material
                .Columns("material").HeaderText = "Insumo"
                .Columns("material").Width = 550
                'Codigo de Serie
                .Columns("unidad").HeaderText = "Und"
                .Columns("unidad").Width = 60
                'razon
                .Columns("preBase").HeaderText = "Precio"
                .Columns("preBase").Width = 80
                .Columns("preBase").DefaultCellStyle.Format = "N2"
                .Columns("preBase").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("tipoM").HeaderText = "Tipo"
                .Columns("tipoM").Width = 120

                'Stock
                .Columns("stock").HeaderText = "Stock"
                .Columns("stock").Width = 80
                .Columns("stock").DefaultCellStyle.Format = "N2"
                .Columns("stock").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("stock").DefaultCellStyle.Font = New Font("Arial", FontStyle.Bold)


                'entregado,personal,recibido,obsR

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub ModificandoColumnasDGV_kardex()
        'oGrilla.ConfigGrilla(dgInsumos)
        dgCardex.ReadOnly = True
        dgCardex.AllowUserToAddRows = False
        dgCardex.AllowUserToDeleteRows = False
        With dgCardex
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Transac."
            .Columns(1).Width = 70
            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 340
            .Columns(3).Visible = False
            .Columns(4).Width = 60
            .Columns(4).HeaderText = "CantEnt"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 50
            .Columns(5).HeaderText = "Ent.S/."
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 60
            .Columns(6).HeaderText = "CantSal"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).Width = 50
            .Columns(7).HeaderText = "Sal.S/."
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).Width = 65
            .Columns(8).HeaderText = "Saldo"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).Width = 40
            .Columns(9).HeaderText = "Unid."
            .Columns(10).Width = 80
            .Columns(10).HeaderText = "NºGuia"
            .Columns(11).Width = 60
            .Columns(11).HeaderText = "NºFact"
            .Columns(12).Width = 60
            .Columns(12).HeaderText = "Salida"
            .Columns(12).Visible = False
            .Columns(13).HeaderText = "Recepción"
            .Columns(13).Width = 110
            .Columns(14).HeaderText = "Recepción Obra/Sede"
            .Columns(14).Width = 300
            .Columns(15).Width = 400
            .Columns(15).HeaderText = "Nota"
            .Columns(16).Width = 130
            .Columns(16).HeaderText = "Personal_Recibe"
            .Columns(17).Width = 240
            .Columns(17).HeaderText = "Proveedor"
            .Columns(18).Width = 75
            .Columns(18).HeaderText = "Ruc"
            .Columns(19).Width = 130
            .Columns(19).HeaderText = "Usuario"
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .Columns(25).Visible = False
            .Columns(26).Visible = False
            .Columns(27).Visible = False
            .Columns(28).Visible = False
            .Columns(29).Visible = False
            .Columns(30).Visible = False
            .Columns(31).Visible = False
            .Columns(32).Visible = False
            .Columns(33).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With

    End Sub

#End Region

#Region "Eventos"

#End Region

    Private Sub ReporteCardexForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub

    Private Sub ReporteCardexForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        configurarColorControl()
        If vSCodigo = "00-00" Then
            CargarCombo("PA_LugarTrabajo", CommandType.StoredProcedure, cbObras, "codigo", "nombre")
        Else
            Dim sele1 As String = "select distinct codigo,nombre from VLugarUbiStoc TObra where codigo='" & vSCodigo & "'"
            CargarCombo(sele1, CommandType.Text, cbObras, "codigo", "nombre")
        End If
        'Consulta
        'parametro de consulta
        ' filtro 
        Dim sele As String = "select codmat,material,stock,unidad,preBase,tipoM from VMaterialObra where codUbi = " & cbAlmacen.SelectedValue
        CargarGrilla(sele, CommandType.Text, dgInsumos, BindingSource0)
        BindingNavigator1.BindingSource = BindingSource0

        ModificandoColumnasDGV_Insumo()

    End Sub

    Private Sub cbObras_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbObras.SelectedIndexChanged
        Try
            If TypeOf cbObras.SelectedValue Is String Then
                Dim cod As String = cbObras.SelectedValue
                Dim consulta As String = "select codUbi,ubicacion,codigo from TUbicacion where codigo ='" & cod & "'"
                CargarCombo(consulta, CommandType.Text, cbAlmacen, "codUbi", "ubicacion")
            End If
        Catch ex As Exception
        End Try
        'filtrando()
    End Sub

    Private Sub txtInsumo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsumo.TextChanged
        Dim criterio As String = txtInsumo.Text.Trim
        ' DataView0.Sort = "material"
        'DataView0.RowFilter = "material like '" & criterio & "%'"
        BindingSource0.Filter = "material like '" & criterio & "%'"
    End Sub

    
    Private Sub btnVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVis.Click
        If dgInsumos.RowCount > 0 Then

            Dim codMaterial As Integer = BindingSource0.Item(BindingSource0.Position)(0)
            Dim codUbicacion As Integer = cbAlmacen.SelectedValue
            Dim sele As String = "select nroNota,tipo,fecha,material,cantEnt,preUniEnt,cantSal,preUniSal,saldo,unidad,nroGuia,nroDoc,veri,almObra,nomObraDes,obs,nomRecibe,provee,ruc,usuario,codMat,idMU,codUbi,codigo,codGuia,codDoc,codTrans,codPers,codSal,vanET,codUbiDes,ubicacion,nombre,codUsu from VKardex1 where codMat=" & codMaterial & " and codUbi=" & codUbicacion
            CargarGrilla(sele, CommandType.Text, dgCardex, BindingSource1)
            BindingNavigator2.BindingSource = BindingSource1

            ColorearGrilla()
            ModificandoColumnasDGV_kardex()
        End If
    End Sub

    Private Sub cbAlmacen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAlmacen.SelectedIndexChanged
        Try
            If TypeOf cbAlmacen.SelectedValue Is String Then
                Dim sele As String = "select codmat,material,stock,unidad,preBase,tipoM from VMaterialObra where codUbi = " & cbAlmacen.SelectedValue
                If CargarGrilla(sele, CommandType.Text, dgInsumos, BindingSource0) > 0 Then
                    'configurando la fuente de las columnas (FONT)
                    FuenteColumnaGrilla()

                    btnVis.Enabled = True

                Else
                    btnVis.Enabled = False
                    BindingSource1.DataSource = ""
                End If

            End If


        Catch ex As Exception

        End Try


    End Sub

    Private Sub ReporteCardexForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ColorearGrilla()
        FuenteColumnaGrilla()
    End Sub

    Private Sub dgCardex_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCardex.Sorted
        ColorearGrilla()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub dgInsumos_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgInsumos.CellDoubleClick
        btnVis.PerformClick()
    End Sub

    Private Sub dgInsumos_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgInsumos.Sorted
        FuenteColumnaGrilla()
    End Sub

    Private Sub dgInsumos_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgInsumos.CurrentCellChanged
        dgCardex.DataSource = ""
    End Sub
End Class



''' <summary>
''' Representa un Dato de ítemd de lista (valor,representación)
''' </summary>
''' <remarks></remarks>
Public Class Dato

    Private _id As Object
    Private _desc As Object

    ''' <summary>
    ''' Codigo (valueMember)
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property Id() As Object
        Get
            Return _id
        End Get
        Set(ByVal value As Object)
            _id = value
        End Set
    End Property
    ''' <summary>
    ''' Descripcion (DisplayMember)
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property Desc() As Object
        Get
            Return _desc
        End Get
        Set(ByVal value As Object)
            _desc = value
        End Set
    End Property

End Class



