Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class ReporteDocVenta


#Region "variables"

    ''' <summary>
    ''' Buffer de datos para ventas
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' buffer de datos para detalle de ventas
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource


    ''' <summary>
    ''' instancia de objeto para customizar Grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' Instancia de Objetos para customizar controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim oFControl As New cConfigFormControls

    ''' <summary>
    ''' Instancia de objeto para administrar datos contra la BD
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    '-Variables para el control de fechas

    Dim _fechaIni As String

    Dim _fechaFin As String


#End Region

#Region "Métodos"
    ''' <summary>
    ''' configura los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()

        Me.BackColor = BackColorP

        'Color para los labels del contenedor principal
        oGrilla.configurarColorControl2("Label", Me, ForeColorLabel)
        'Color para el groupBox
        GroupBox2.ForeColor = ForeColorLabel
        'oGrilla.configurarColorControl("GroupBox", Me, ForeColorLabel)
        'Color para botón
        'oGrilla.configurarColorControl2("CheckBox", Me, )
        'chkObra.ForeColor = ForeColorLabel
        chkObra.BackColor = System.Drawing.SystemColors.Control

        oGrilla.configurarColorControl2("Label", GroupBox3, ForeColorLabel)

        btnVer.ForeColor = ForeColorButtom

    End Sub


    Private Sub ModificandoColumnaDGV()

        dgvVentas.ReadOnly = True
        dgvVentas.AllowUserToAddRows = False
        dgvVentas.AllowUserToDeleteRows = False

        With dgvVentas

            .Columns("codDocV").Visible = False
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").Width = 40

            .Columns("nroDoc").HeaderText = "Nro"
            .Columns("nroDoc").Width = 40

            .Columns("fecDoc").HeaderText = "Fecha"
            .Columns("fecDoc").Width = 70

            .Columns("ruc").HeaderText = "RUC"
            .Columns("ruc").Width = 80

            .Columns("razon").HeaderText = "Cliente"
            .Columns("razon").Width = 250
            .Columns("dir").HeaderText = "Dirección"
            .Columns("dir").Width = 250

            .Columns("codIde").Visible = False

            .Columns("codigo").Visible = False
            '.Columns("nombre").Visible = False
            .Columns("nombre").HeaderText = "Obra"
            .Columns("nombre").Width = 250
            .Columns("estado").HeaderText = "Estado"
            .Columns("estado").Width = 70

            .Columns("simbolo").Visible = False
            ' .Columns("simbolo").HeaderText = ""
            '.Columns("simbolo").Width = 30
            '.Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("camD").Visible = False
            .Columns("codSerS").Visible = False
            .Columns("calIGV").Visible = False
            .Columns("codMon").Visible = False
            .Columns("obs").Visible = False
            '.Columns("camD").HeaderText = "cam"
            '.Columns("camD").Width = 120

        End With

    End Sub

    Private Sub ModificandoColumnaDetalleDGV()
        dgDetalleVenta.ReadOnly = True
        dgDetalleVenta.AllowUserToAddRows = False
        dgDetalleVenta.AllowUserToDeleteRows = False

        With dgDetalleVenta
            .Columns("codDV").Visible = False
            .Columns("cant").HeaderText = "Cantidad"
            .Columns("Cant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cant").Width = 70
            .Columns("unidad").HeaderText = "Und."
            .Columns("detalle").HeaderText = "Detalle"
            .Columns("detalle").Width = 150
            .Columns("linea").HeaderText = ""
            .Columns("linea").Width = 360
            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

            .Columns("simbolo").Width = 30
            .Columns("preUni").HeaderText = "P.U."
            .Columns("preUni").Width = 90
            .Columns("preUni").DefaultCellStyle.Format = "N2"
            .Columns("preUni").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With


    End Sub

    ''' <summary>
    ''' calcula el total, subtotal e igv
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub calcularTotal()
        If dgvVentas.RowCount = 0 Then

        Else
            Dim simbolo As String = BindingSource0.Item(BindingSource0.Position)(11)
            Dim tipoCalculo As Integer = BindingSource0.Item(BindingSource0.Position)(14)
            Dim total As Decimal = 0
            Dim subTotal As Decimal = 0
            Dim Igv As Decimal = 0

            Select Case tipoCalculo
                'igv incluido
                Case 1
                    total = oGrilla.SumarColumnaGrilla(dgDetalleVenta, "preUni")
                    subTotal = total / 1.18
                    Igv = total - subTotal
                    'igv por incluir
                Case 2
                    subTotal = oGrilla.SumarColumnaGrilla(dgDetalleVenta, "preUni")
                    Igv = subTotal * 0.18
                    total = subTotal * 1.18

                    'sin igv
                Case 0
                    total = oGrilla.SumarColumnaGrilla(dgDetalleVenta, "preUni")

            End Select
            'asignando valores a textbox
            txtTotal.Text = Format(total, "0,0.00")
            txtSubtotal.Text = Format(subTotal, "0,0.00")
            txtIgv.Text = Format(Igv, "0,0.00")
            'asignando simbolo de moneda
            lblMonedaSubtotal.Text = simbolo
            lblMonedaIGV.Text = simbolo
            lblMonedaTotal.Text = simbolo

        End If

    End Sub


#End Region

#Region "Eventos"



#End Region



    Private Sub ReporteDocVenta_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub

    Private Sub ReporteDocVenta_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo el aceptbutton
        Me.AcceptButton = btnVer

        'estableciendo el check en el RadioButton Obra 
        rdoFecha.Checked = True
        'estableciendo en check del 

        'configurar color control
        configurarColorControl()

        'Cargar los combos 

        'combo de proveedores /Clientes

        Dim query As String = "select codIde,razon from TIdentidad where idTipId =1 order by razon asc"

        'oDataManager.CargarCombo("PA_Proveedores", CommandType.StoredProcedure, cbProv, "codIde", "razon")

        'combo de proveedores
        oDataManager.CargarCombo(query, CommandType.Text, cbClient, "codIde", "razon")

        'combo de serie
        Dim query2 As String = "select codSerS, serie from TSerieSede where codTipDE=70"
        '"select serie from TSerieOrden where estado=1 order by serie"
        oDataManager.CargarCombo(query2, CommandType.Text, cbSerie, "codSerS", "serie")

        Me.Cursor = Cursors.Default
        wait.Close()

    End Sub



    Private Sub rdoFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoFecha.CheckedChanged
        If rdoFecha.Checked Then
            'Ocultando los controles para obra
            Dim ocultar() As String = {cbSerie.Name, cbClient.Name, lblSerie.Name, lblProv.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Obra

            Dim mostrar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name}


            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgDetalleVenta.DataSource = ""
            dgvVentas.DataSource = ""
        End If


    End Sub

    Private Sub rdoClient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoClient.CheckedChanged
        If rdoClient.Checked Then
            'Ocultando los controles para Proveedor
            Dim ocultar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, cbSerie.Name, lblSerie.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Proveedor
            Dim mostrar() As String = {lblProv.Name, cbClient.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgDetalleVenta.DataSource = ""
            dgvVentas.DataSource = ""
        End If
    End Sub

    Private Sub rdoSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSerie.CheckedChanged

        If rdoSerie.Checked Then
            'Ocultando los controles para Serie
            Dim ocultar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, lblProv.Name, cbClient.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Serie
            Dim mostrar() As String = {cbSerie.Name, lblSerie.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgDetalleVenta.DataSource = ""
            dgvVentas.DataSource = ""
        End If
    End Sub



    Private Sub btnVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVer.Click

        
      

        'Mostrando ventana y cursos de trabajo en proceso
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

     


        'estableciendo los parametros de fecha
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

        Dim query As String = "select codDocV,serie,nroDoc,fecDoc,ruc,razon,dir,codIde,codigo,nombre,estado,simbolo,camD,codSerS,calIGV,codMon,obs from vDocumentosVentas "

        'filtrando la consulta

        'filtrando por fecha
        If rdoFecha.Checked Then
            query += " WHERE fecDoc between @fechaInicio and @fechaFin"
            oDataManager.CargarGrilla(query, parametros, CommandType.Text, dgvVentas, BindingSource0)

        End If

        'filtrando por cliente
        If rdoClient.Checked Then
            'validando selección de cliente
            If (cbClient.SelectedIndex = -1) Then
                MessageBox.Show("Por favor seleccione un valor valido.", nomNegocio, Nothing, MessageBoxIcon.Error)

                cbClient.Focus()
                wait.Close()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            'evaluando si existe valor seleccionado
            query += " WHERE codIde=" & cbClient.SelectedValue
            oDataManager.CargarGrilla(query, CommandType.Text, dgvVentas, BindingSource0)
        End If

        'filtrando por serie
        If rdoSerie.Checked Then

            'validando ingreso de serie
            If (cbSerie.SelectedIndex = -1) Then
                MessageBox.Show("Por favor seleccione un valor valido.", nomNegocio, Nothing, MessageBoxIcon.Error)

                cbSerie.Focus()
                wait.Close()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            query += " WHERE codSerS=" & cbSerie.SelectedValue
            oDataManager.CargarGrilla(query, CommandType.Text, dgvVentas, BindingSource0)
        End If

        BindingNavigator1.BindingSource = BindingSource0

        'Aplicar filtro 

        'modificar columnas
        ModificandoColumnaDGV()

        'limpiando grilla detalle
        If dgvVentas.RowCount = 0 Then
            dgvVentas.DataSource = ""
            dgDetalleVenta.DataSource = ""
        End If

        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

    

    Private Sub dgvVentas_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvVentas.CellClick, dgvVentas.CellEnter
        If dgvVentas.RowCount = 0 Then
            dgvVentas.DataSource = ""
            dgDetalleVenta.DataSource = ""
        Else

            Dim query As String = "select codDV,cant,unidad,detalle,linea,simbolo, preUni from TDetalleVenta TD inner join vDocumentosVentas TDV on TDV.codDocV =  TD.codDocV where TD.codDocV =" & BindingSource0.Item(BindingSource0.Position)(0)

            oDataManager.CargarGrilla(query, CommandType.Text, dgDetalleVenta, BindingSource1)

            BindingNavigator2.BindingSource = BindingSource1

            'mostrar Calculos
            calcularTotal()

            ModificandoColumnaDetalleDGV()


        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

   
    
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe datos")
            Exit Sub
        End If

        'obteninedo datos para parametros
        vCliente = BindingSource0.Item(BindingSource0.Position)(5) 'Cliente
        vDir = BindingSource0.Item(BindingSource0.Position)(6)
        vRuc = BindingSource0.Item(BindingSource0.Position)(4)
        vObs = BindingSource0.Item(BindingSource0.Position)(16)
        vFec1 = BindingSource0.Item(BindingSource0.Position)(3)
        If chkObra.Checked Then
            vObra = "OBRA: " & BindingSource0.Item(BindingSource0.Position)(9)
        Else
            vObra = " "
        End If
        vSerie = BindingSource0.Item(BindingSource0.Position)(1) & "-" & concatenarNro(BindingSource0.Item(BindingSource0.Position)(2))

        'detalle 
        vCan1 = Format(BindingSource1.Item(0)(1), "0,0.00")
        vDet1 = BindingSource1.Item(0)(3)
        vPre1 = Format(BindingSource1.Item(0)(6), "0,0.00")
        vTot1 = Format((vCan1 * vPre1), "0,0.00")
        vDes1 = BindingSource1.Item(0)(4)

        vCan2 = Format(BindingSource1.Item(1)(1), "0,0.00")
        vDet2 = BindingSource1.Item(1)(3)
        vPre2 = Format(BindingSource1.Item(1)(6), "0,0.00")
        vTot2 = Format((vCan2 * vPre2), "0,0.00")
        vDes2 = BindingSource1.Item(1)(4)

        ''Totales
        Dim moneda As String
        If BindingSource0.Item(BindingSource0.Position)(15) = 30 Then
            moneda = "Nuevos Soles"
        Else
            moneda = "Dólares Americanos"
        End If


        vLetra = cambiarNroTotalLetra(txtTotal.Text.Trim(), moneda) 'txtLetraTotal.Text.Trim()

        vSub = txtSubtotal.Text.Trim()
        vIgv1 = txtIgv.Text.Trim()
        vTot = txtTotal.Text.Trim()
        vMon = "TOTAL " & lblMonedaTotal.Text


        Dim informe As New ReportViewerDocVentaForm
        informe.ShowDialog()

    End Sub

    Private Function cambiarNroTotalLetra(ByVal numero As Object, ByVal moneda As String) As String
        If IsNumeric(numero) Then
            Dim cALetra As New Num2LetEsp  'clase definida por el usuario
            cALetra.Moneda = moneda
            numero = CDbl(numero)
            'Inicia el Proceso para identificar la cantidad a convertir
            If Val(numero) > 0 Then
                cALetra.Numero = Val(CDbl(numero))
            End If
            Return "SON: " & cALetra.ALetra.ToUpper()
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Concatena una nro precediendole de ceros
    ''' </summary>
    ''' <param name="nro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function concatenarNro(ByVal nro As Object) As String
        Dim value As String = ""
        If IsNumeric(nro) Then

            Dim contador As Integer = nro.ToString().Count()
            For i As Integer = 1 To 5 - contador
                value &= "0"
            Next
            value &= nro

        End If
        Return value
    End Function

End Class