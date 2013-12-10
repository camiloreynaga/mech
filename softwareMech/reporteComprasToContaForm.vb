Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports DocumentFormat.OpenXml
Imports ClosedXML.Excel



Public Class reporteComprasToContaForm



    Protected Friend Overrides Sub ModificandoColumnaDGV()

        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False

        With dgv

            .Columns("idOP").Visible = False
            .Columns("fechaEmision").HeaderText = "Emisión"
            .Columns("fechaEmision").Width = 50
            .Columns("fechaVenc").HeaderText = "Venc."
            .Columns("fechaVenc").Width = 50
            .Columns("codigoDoc").HeaderText = "codDoc"
            .Columns("codigoDoc").Width = 50
            .Columns("Doc").HeaderText = "Doc"
            .Columns("Doc").Width = 70
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").Width = 40
            .Columns("nroFact").HeaderText = "N°"
            .Columns("nroFact").Width = 60
            .Columns("ruc").HeaderText = "RUC"
            .Columns("ruc").Width = 75
            .Columns("razon").HeaderText = "Cliente"
            .Columns("razon").Width = 200
            .Columns("codGravado").HeaderText = "codGrav."
            .Columns("codGravado").Width = 50
            .Columns("gravado").HeaderText = "Gravado"
            .Columns("gravado").Width = 50
            .Columns("monto").HeaderText = "MontoVenta"
            .Columns("monto").Width = 70
            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("monto").DefaultCellStyle.Format = "N2"
            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").Width = 30
            .Columns("montoPago").HeaderText = "Pagado"
            .Columns("montoPago").Width = 70
            .Columns("montoPago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("montoPago").DefaultCellStyle.Format = "N2"
            .Columns("percep1").HeaderText = "Percep._1 "
            .Columns("percep1").Width = 50
            .Columns("percep2").HeaderText = "Percep._2"
            .Columns("percep2").Width = 50
            .Columns("fechaEmiD").HeaderText = "Emi._Detrac."
            .Columns("fechaEmiD").Width = 50
            .Columns("codigoD").HeaderText = "CodD"
            .Columns("codigoD").Width = 50
            .Columns("nroD").HeaderText = "N°D"
            .Columns("nroD").Width = 50
            .Columns("montoD").HeaderText = "MontoD"
            .Columns("montoD").Width = 50
            .Columns("codCuenta").HeaderText = "codCTA"
            .Columns("codCuenta").Width = 50
            .Columns("descrCuenta").HeaderText = "DescCTA"
            .Columns("descrCuenta").Width = 50
            .Columns("codTipoPago").HeaderText = "CodPago"
            .Columns("codTipoPago").Width = 50
            .Columns("tipoPago").HeaderText = "MedioPago"
            .Columns("tipoPago").Width = 100
            .Columns("nroP").HeaderText = "N°_Op."
            .Columns("nroP").Width = 50
            .Columns("fecPago").HeaderText = "fecha_Pago"
            .Columns("fecPago").Width = 70
            .Columns("codigoCuenta").HeaderText = "codCta"
            .Columns("codigoCuenta").Width = 50
            .Columns("nroCta").HeaderText = "N°_Cuenta"
            .Columns("nroCta").Width = 150
            .Columns("codIde").Visible = False

        End With

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
        Dim query As String = "select idOP,fechaEmision,fechaVenc,codigoDoc,Doc,serie,nroFact,ruc,razon,codGravado,gravado,monto,simbolo,montoPago,percep1,percep2,fechaEmiD,codigoD,nroD,montoD,codCuenta,descrCuenta,codTipoPago,tipoPago,nroP,fecPago,codigoCuenta,nroCta,codIde from vReporteComprasConta"

        'filtrando por fecha de pago
        If rdoFecha.Checked Then
            query += " WHERE fecPago between @fechaInicio and @fechaFin"
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
        'If rdoSerie.Checked Then

        '    'validando ingreso de serie
        '    If (cbSerie.SelectedIndex = -1) Then
        '        MessageBox.Show("Por favor seleccione un valor valido.", nomNegocio, Nothing, MessageBoxIcon.Error)

        '        cbSerie.Focus()
        '        wait.Close()
        '        Me.Cursor = Cursors.Default
        '        Exit Sub
        '    End If

        '    query += " WHERE codSerS=" & cbSerie.SelectedValue
        '    oDataManager.CargarGrilla(query, CommandType.Text, dgv, BindingSource0)
        'End If

        BindingNavigator1.BindingSource = BindingSource0

        'modificar columnas
        ModificandoColumnaDGV()

        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub reporteComparaToContaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo el aceptbutton
        Me.AcceptButton = btnVer

        'estableciendo el check en el RadioButton Obra 
        rdoFecha.Checked = True
        'estableciendo en check del 

        'configurar el color de cada control
        configurarColorControl()

        'Cargar los combos 

        'combo de proveedores /Clientes

        Dim query As String = "select codIde,razon from TIdentidad where idTipId =2 order by razon asc"

        'oDataManager.CargarCombo("PA_Proveedores", CommandType.StoredProcedure, cbProv, "codIde", "razon")

        'combo de proveedores
        oDataManager.CargarCombo(query, CommandType.Text, cbClient, "codIde", "razon")

        ''combo de serie
        ' Dim query2 As String = "select codSerS, serie from TSerieSede where codTipDE=70"
        ''"select serie from TSerieOrden where estado=1 order by serie"
        'oDataManager.CargarCombo(query2, CommandType.Text, cbSerie, "codSerS", "serie")

        Me.Cursor = Cursors.Default
        wait.Close()

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim _xls As New cExportXlsx
        _xls.export2Excel(2, 2, "Compras 02", dgv)
    End Sub
End Class