Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports DocumentFormat.OpenXml
Imports ClosedXML.Excel

Public Class reporteVentasToContaForm

    ''' <summary>
    ''' customiza la presentación de columnas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Overrides Sub ModificandoColumnaDGV()
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False

        With dgv

            .Columns("codDocV").Visible = False
            .Columns("nombre").HeaderText = "Obra"
            .Columns("nombre").Width = 200
            .Columns("fecDoc").HeaderText = "Emisión"
            .Columns("fecDoc").Width = 70
            .Columns("fecCan").HeaderText = "Venc."
            .Columns("fecCan").Width = 70
            .Columns("tipoDoc").HeaderText = "codDoc"
            .Columns("tipoDoc").Width = 70
            .Columns("doc").HeaderText = "Doc"
            .Columns("doc").Width = 70
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").Width = 35
            .Columns("nroDoc").HeaderText = "N°"
            .Columns("nroDoc").Width = 50
            .Columns("ruc").HeaderText = "RUC"
            .Columns("ruc").Width = 75
            .Columns("razon").HeaderText = "Cliente"
            .Columns("razon").Width = 200
            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").Width = 30
            .Columns("PV").HeaderText = "Precio_venta."
            .Columns("PV").Width = 70
            .Columns("PV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("PV").DefaultCellStyle.Format = "N2"
            .Columns("codIde").Visible = False
            .Columns("codigo").Visible = False
            .Columns("codSers").Visible = False
        End With

    End Sub

    Private Sub reporteVentasToContaForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        'estableciendo el aceptbutton
        Me.AcceptButton = btnVer

        'estableciendo el check en el RadioButton Obra 
        rdoFecha.Checked = True

        'configurar color control
        configurarColorControl()

        'Cargar los combos 
        'combo de proveedores /Clientes

        Dim query As String = "select codIde,razon from TIdentidad where idTipId =1 order by razon asc"

        'combo clientes
        oDataManager.CargarCombo(query, CommandType.Text, cbClient, "codIde", "razon")

        'combo de serie
        Dim query2 As String = "select codSerS, serie from TSerieSede where codTipDE=70"
        '"select serie from TSerieOrden where estado=1 order by serie"
        oDataManager.CargarCombo(query2, CommandType.Text, cbSerie, "codSerS", "serie")

        wait.Close()
        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click

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

        'consultando 
        Dim query As String = "select codDocV,nombre,fecDoc,fecCan,tipoDoc,doc,serie,nroDoc,ruc,razon,simbolo,PV,codIde,codigo,codSers  from vReporteVentasConta"

        'filtrando por fecha de emisión
        If rdoFecha.Checked Then
            query += " WHERE fecDoc between @fechaInicio and @fechaFin"
            oDataManager.CargarGrilla(query, parametros, CommandType.Text, dgv, BindingSource0)

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
            oDataManager.CargarGrilla(query, CommandType.Text, dgv, BindingSource0)
        End If

        'oDataManager.CargarGrilla(query, CommandType.Text, dgv, BindingSource0)

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
            oDataManager.CargarGrilla(query, CommandType.Text, dgv, BindingSource0)
        End If

        BindingNavigator1.BindingSource = BindingSource0

        'modificar columnas
        ModificandoColumnaDGV()

        wait.Close()
        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim wb As New XLWorkbook()
        Dim ws As IXLWorksheet

        ws = wb.Worksheets.Add("Contacts")
        For index As Integer = 0 To dgv.Columns.Count() - 1
            If dgv.Columns(index).Visible Then
                ws.Cell(2, index + 2).Value = dgv.Columns(index).HeaderText
            End If


        Next

        wb.SaveAs("Excel01.xlsx")



    End Sub
End Class