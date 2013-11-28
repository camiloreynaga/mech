Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoFacturasForm


#Region "Variables"
    ''' <summary>
    ''' buffer de datos para Desembolsos
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' Instancia de objeto para Customizar grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' instancia de Objeto de la clase datamanager, para administración de datos
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager


    'variable temporales para fecha de inicio y fin

    Dim _fechaIni As String
    Dim _fechaFin As String
#End Region

#Region "Métodos"

    Private Sub configurarColorControl()

        Me.BackColor = BackColorP

        'Color para los labels del contenedor principal
        oGrilla.configurarColorControl2("Label", Me, ForeColorLabel)
        'Color para el groupBox
        GroupBox2.ForeColor = ForeColorLabel
        'oGrilla.configurarColorControl("GroupBox", Me, ForeColorLabel)
        'Color para botón
        oGrilla.configurarColorControl2("CheckBox", Me, ForeColorLabel)

        btnVer.ForeColor = ForeColorButtom

    End Sub

    ''' <summary>
    ''' customiza las columnas de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV()

        dgDesembolso.ReadOnly = True
        dgDesembolso.AllowUserToAddRows = False
        dgDesembolso.AllowUserToDeleteRows = False

        With dgDesembolso

            .Columns("idOP").Visible = False
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").DisplayIndex = 0
            .Columns("Serie").Width = 40

            .Columns("nroDes").HeaderText = "Nro"
            .Columns("nroDes").DisplayIndex = 1
            .Columns("nroDes").Width = 40

            .Columns("fecDes").HeaderText = "Fecha"
            .Columns("fecDes").DisplayIndex = 2
            .Columns("fecDes").Width = 70

            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").Width = 30
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("monto").HeaderText = "Monto"
            .Columns("monto").Width = 78
            .Columns("monto").DefaultCellStyle.Format = "N2"
            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("montoDet").HeaderText = "Detracción"
            .Columns("montoDet").Width = 78
            .Columns("montoDet").DefaultCellStyle.Format = "N2"
            .Columns("montoDet").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("nombre").HeaderText = "Obra/Lugar"
            .Columns("nombre").Width = 270

            .Columns("razon").HeaderText = "Proveedor"
            .Columns("razon").Width = 270

            .Columns("concepto").HeaderText = "Concepto"
            .Columns("concepto").Width = 250

            .Columns("nroConfor").HeaderText = "N°_Factura"
            .Columns("nroConfor").Width = 60

            .Columns("fecEnt").HeaderText = "Fecha_entrega"
            .Columns("fecEnt").Width = 70

            .Columns("aprobador").HeaderText = "Aprobador"
            .Columns("aprobador").Width = 100

            .Columns("codObra").Visible = False
            .Columns("codProv").Visible = False

        End With

    End Sub


    ''' <summary>
    ''' muestra una colección de controles dento de un contenedor
    ''' </summary>
    ''' <param name="container">contenedor</param>
    ''' <param name="_controls">controles a evaluar</param>
    ''' <param name="action">visible true or false</param>
    ''' <remarks></remarks>
    Protected Sub mostrarOcultarControles(ByVal container As Object, ByVal _controls As List(Of String), ByVal action As Boolean)

        'convertir el objeto container en contenedor
        For index As Integer = 0 To _controls.Count - 1
            'estableciendo la propiedad visible de los controles de la lista
            CType(container, ContainerControl).Controls(_controls(index)).Visible = action
        Next

    End Sub

    ''' <summary>
    ''' Añade criterios a los filtros
    ''' </summary>
    ''' <param name="criterio"></param>
    ''' <param name="filtro"></param>
    ''' <remarks></remarks>
    Private Function AddCriterioFiltro(ByVal criterio As String, ByVal filtro As String) As String
        If filtro.Length > 0 Then
            filtro &= " and " & criterio
        Else
            filtro &= " " & criterio
        End If
        Return filtro
    End Function
    ''' <summary>
    ''' Filtra Desembolso 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub filtrando()
        ''comprobando que exista items
        If cbObras.Items.Count > 0 Then

            BindingSource0.Filter = ""
            Dim pFiltro As String = BindingSource0.Filter
            Dim pCriterio As String
            'filtro para obra
            If chkObra.Checked = False Then
                pCriterio = "codObra='" & cbObras.SelectedValue & "'"
                pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
            End If

            BindingSource0.Filter = pFiltro

            BindingSource0.Sort = "NroDes Desc"
        End If

    End Sub

#End Region

#Region "Eventos"

#End Region


    Private Sub SeguimientoFacturasForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub


    Private Sub SeguimientoFacturasForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo el aceptbutton
        Me.AcceptButton = btnVer

        'estableciendo el check en el RadioButton Obra 
        rdoObra.Checked = True
        'estableciendo en check del 
        chkObra.Checked = True

        'configurar color control
        configurarColorControl()


        'Cargar los combos 
        'Combo de obras
        oDataManager.CargarCombo("PA_LugarTrabajo", CommandType.StoredProcedure, cbObras, "codigo", "nombre")
        'combo de proveedores
        oDataManager.CargarCombo("PA_Proveedores", CommandType.StoredProcedure, cbProv, "codIde", "razon")
        'combo de serie
        Dim query As String = "select serie from TSerieOrden where estado=1 order by serie"
        oDataManager.CargarCombo(query, CommandType.Text, cbSerie, "serie", "serie")

        Me.Cursor = Cursors.Default
        wait.Close()

    End Sub




    Private Sub chkObra_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkObra.CheckedChanged

        'Muetra / Oculta los controles de la lista, de acuerdo al estado del check
        Dim controles As String() = {cbObras.Name}

        If chkObra.Checked Then
            mostrarOcultarControles(Me, New List(Of String)(controles), False)

        Else
            mostrarOcultarControles(Me, New List(Of String)(controles), True)

        End If



    End Sub

    Private Sub rdoObra_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoObra.CheckedChanged

        'chkObra.Checked = True

        If rdoObra.Checked Then
            'Ocultando los controles para obra
            Dim ocultar() As String = {"cbSerie", "cbProv", "lblSerie", "lblProv"}
            mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Obra

            Dim mostrar() As String = {chkObra.Name, cbObras.Name, lblObra.Name, lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name}


            mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'evaluando si chkObra está chekeado 
            If chkObra.Checked Then
                chkObra_CheckedChanged(sender, e)
            End If

        End If

    End Sub

    Private Sub rdoProv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoProv.CheckedChanged
        If rdoProv.Checked Then
            'Ocultando los controles para Proveedor
            Dim ocultar() As String = {chkObra.Name, cbObras.Name, lblObra.Name, lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, cbSerie.Name, lblSerie.Name}
            mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Proveedor
            Dim mostrar() As String = {lblProv.Name, cbProv.Name}
            mostrarOcultarControles(Me, New List(Of String)(mostrar), True)
        End If
    End Sub

    Private Sub rdoSerie_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSerie.CheckedChanged

        If rdoSerie.Checked Then
            'Ocultando los controles para Serie
            Dim ocultar() As String = {chkObra.Name, cbObras.Name, lblObra.Name, lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, lblProv.Name, cbProv.Name}
            mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Serie
            Dim mostrar() As String = {cbSerie.Name, lblSerie.Name}
            mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

        End If

    End Sub



    Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click

        'mostrando ventana y cursos que indica trabajo en proceso
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        'Estableciendo los parametros de fecha
        Dim fechaInicio As New SqlParameter("@fechaInicio", SqlDbType.Date)
        fechaInicio.Value = dtpInicio.Value

        Dim fechaFin As New SqlParameter("@fechaFin", SqlDbType.Date)
        fechaFin.Value = dtpFin.Value

        Dim parametros(1) As SqlParameter

        parametros(0) = fechaInicio
        parametros(1) = fechaFin
        'obteniendo la fecha para reportes
        _fechaIni = dtpInicio.Text
        _fechaFin = dtpFin.Text

        Dim query As String = "select idOp,serie,nroDes,fecDes,simbolo,monto,montoDet, nombre,razon,concepto,nroConfor,fecEnt,aprobador,codObra,codProv from vFacturaOrdenDesembolso "

        'filtrando la consulta de acuerdo a la elección hecha

        'mostrando datos para Obra
        If rdoObra.Checked Then
            query += "WHERE fecDes between @fechaInicio and @fechaFin"
            oDataManager.CargarGrilla(query, parametros, CommandType.Text, dgDesembolso, BindingSource0)
        End If
        'mostrando datos para Proveedor
        If rdoProv.Checked Then

            'validando selección de proveedor
            If (cbProv.SelectedIndex = -1) Then
                MessageBox.Show("Por favor seleccione un valor valido.", nomNegocio, Nothing, MessageBoxIcon.Error)

                cbProv.Focus()
                wait.Close()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            query += "WHERE codProv =" & cbProv.SelectedValue
            oDataManager.CargarGrilla(query, CommandType.Text, dgDesembolso, BindingSource0)
        End If
        'mostrando datos para serie
        If rdoSerie.Checked Then
            'validando ingreso de serie
            If (cbSerie.SelectedIndex = -1) Then
                MessageBox.Show("Por favor seleccione un valor valido.", nomNegocio, Nothing, MessageBoxIcon.Error)

                cbSerie.Focus()
                wait.Close()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            query += "WHERE serie = " & cbSerie.SelectedValue
            oDataManager.CargarGrilla(query, CommandType.Text, dgDesembolso, BindingSource0)
        End If
        'mostrando el navegador
        BindingNavigator1.BindingSource = BindingSource0

        'filtrar 
        filtrando()
        'modificando columnas
        ModificandoColumnasDGV()

        'Mostrando Suma suma de importes por columna
        txtTotalSoles.Text = oGrilla.SumarColumnaGrilla(dgDesembolso, "monto", "simbolo", "S/.").ToString()
        txtTotalDolares.Text = oGrilla.SumarColumnaGrilla(dgDesembolso, "monto", "simbolo", "US$").ToString()

        txtDetraccionesSoles.Text = oGrilla.SumarColumnaGrilla(dgDesembolso, "montoDet", "simbolo", "S/.").ToString()
        txtDetraccionesDolares.Text = oGrilla.SumarColumnaGrilla(dgDesembolso, "montoDet", "simbolo", "US$").ToString()

        'dando Formato a los textBox totales
        txtTotalSoles.Text = Format(CDbl(txtTotalSoles.Text), "0,0.00")
        txtTotalDolares.Text = Format(CDbl(txtTotalDolares.Text), "0,0.00")
        txtDetraccionesSoles.Text = Format(CDbl(txtDetraccionesSoles.Text), "0,0.00")
        txtDetraccionesDolares.Text = Format(CDbl(txtDetraccionesDolares.Text), "0,0.00")

        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

   
    Private Sub btnImp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImp.Click
        'Validando que existan datos en la grilla
        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe datos...")
            Exit Sub
        End If
        'creando un wait form y estableciendo el cursor en wait
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'cargando los datos del reportes
        Dim datos As DataSetInformesCr = CargarDatos()
        Dim frm As New ReportViewerFacturas(datos)

        'cerrando wait y reestableciendo cursor
        wait.Close()
        Me.Cursor = Cursors.Default

        frm.ShowDialog()


    End Sub
    ''' <summary>
    ''' Carga los datos para el reporte, tomando como  fuente la grilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarDatos() As DataSetInformesCr
        Dim ds As New DataSetInformesCr

        For Each row As DataGridViewRow In dgDesembolso.Rows
            Dim rowInf As DataSetInformesCr.DatosFacturaRow = ds.DatosFactura.NewDatosFacturaRow
            rowInf.fecDes = CDate(row.Cells("fecDes").Value)
            rowInf.serie = CStr(row.Cells("serie").Value)
            rowInf.nroDes = CStr(row.Cells("nroDes").Value)
            rowInf.simbolo = CStr(row.Cells("simbolo").Value)
            rowInf.monto = CDbl(row.Cells("monto").Value)
            rowInf.montoD = CDbl(row.Cells("montoDet").Value)
            rowInf.obra = CStr(row.Cells("nombre").Value)
            rowInf.proveedor = CStr(row.Cells("razon").Value)
            rowInf.concepto = CStr(row.Cells("concepto").Value)
            rowInf.nroFactura = CStr(row.Cells("nroConfor").Value)
            rowInf.fecEntrega = CDate(row.Cells("fecEnt").Value)
            rowInf.aprobador = CStr(row.Cells("aprobador").Value)

            ds.DatosFactura.AddDatosFacturaRow(rowInf)

        Next

        Return ds

    End Function

End Class
