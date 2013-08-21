Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class GastosPorDiaForm

    ''' <summary>
    ''' instancia de objeto DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' Gastos (egresos)
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource0 As New BindingSource

    ''' <summary>
    ''' instancia de objeto ConfigControls
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

#Region "métodos"

    ''' <summary>
    ''' Customiza la grilla  Detalle GR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificandoColumnasDGV()
        'oGrilla.ConfigGrilla(dgInsumos)
        dgReporte.ReadOnly = True
        dgReporte.AllowUserToAddRows = False
        dgReporte.AllowUserToDeleteRows = False

        'dgPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Try

            'If dgReporte.Columns("banco").Visible = True Then
            '    dgReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            'Else
            '    dgReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            'End If

            If dgReporte.Columns("nroCue").Visible = True Then
                dgReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            Else
                dgReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

            With dgReporte
                'codigo material
                .Columns("fecPago").HeaderText = "Fecha"
                .Columns("fecPago").Width = 75
                'Material
                '.Columns("serie").HeaderText = ""
                '.Columns("serie").Width = 30
                'Codigo de Serie
                .Columns("nroDes").HeaderText = "N°_Des"
                .Columns("nroDes").Width = 55

                .Columns("nroOperacion").HeaderText = "N°"
                .Columns("nroOperacion").Width = 50

                .Columns("ruc").HeaderText = "RUC"
                .Columns("ruc").Width = 75

                .Columns("razon").HeaderText = "Razon Social"
                .Columns("razon").Width = 300

                .Columns("banco").HeaderText = "Banco"
                .Columns("banco").Width = 70

                .Columns("nroCue").HeaderText = "N°_Cuenta"
                .Columns("nroCue").Width = 130

                .Columns("simbolo").HeaderText = ""
                .Columns("simbolo").Width = 30

                .Columns("montoPago").HeaderText = "Monto"
                .Columns("montoPago").Width = 80
                .Columns("montoPago").DefaultCellStyle.Format = "N2"
                .Columns("montoPago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("montoD").HeaderText = "Detracción"
                .Columns("montoD").Width = 80
                .Columns("montoD").DefaultCellStyle.Format = "N2"
                .Columns("montoD").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("codBan").Visible = False
                .Columns("codMon").Visible = False
                .Columns("idCue").Visible = False
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)

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

        btnCerrar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnMostrar.ForeColor = ForeColorButtom

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
        'If BindingSource4.Position >= 0 And BindingSource5.Position >= 0 Then
        bindingSource0.Filter = ""
        Dim pFiltro As String = BindingSource0.Filter
        Dim pCriterio As String
        If bindingSource0.Position >= 0 Then

            If chkBanco.Checked = False Then
                'Dim vAlmacen As Integer = cbAlmacen.SelectedValue
                'If String.IsNullOrEmpty(cbAlmacen.SelectedValue) Then
                '    cbAlmacen.SelectedIndex = 0
                'End If
                pCriterio = "codBan =" & cbBanco.SelectedValue
                pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
                'ocultando la columna banco

                dgReporte.Columns("banco").Visible = False
            Else
                dgReporte.Columns("banco").Visible = True
            End If

            If chkCuenta.Checked = False Then
                If String.IsNullOrEmpty(cbCuenta.SelectedValue) = False Then
                    pCriterio = "idCue=" & cbCuenta.SelectedValue
                    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
                End If


                dgReporte.Columns("nroCue").Visible = False
            Else
                dgReporte.Columns("nroCue").Visible = True

            End If
            bindingSource0.Filter = pFiltro

        End If

    End Sub

#End Region

    Private Sub GastosPorDiaForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        configurarColorControl()

        oDataManager.CargarCombo("select codBan,banco from TBanco", CommandType.Text, cbBanco, "codBan", "banco")

    End Sub

    Private Sub btnMostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMostrar.Click
        'SET DATEFORMAT dmy  da formato dd/mm/yyyy para las fechas
        Dim sele As String = "SET DATEFORMAT dmy select fecPago,nroOperacion,ruc,razon,simbolo,montoPago,montoD,banco,nroCue,(serie +'-'+cast(nroDes as varchar)) nroDes,codBan,codMon,idCue from VGastosPorDia where fecPago between '" & dtpInicio.Text & "' and '" & dtpFin.Text & "'"
        Dim oTabla As DataTable = oDataManager.CargarGrilla2(sele, CommandType.Text, dgReporte, bindingSource0)

        'enlanzando con el binding navigator
        BindingNavigator1.BindingSource = bindingSource0


        filtrando()
        ModificandoColumnasDGV()
        'Sumando MontoPago
        txtTotalSoles.Text = oGrilla.SumarColumnaGrilla(dgReporte, "montoPago", "codMon", "30") ' oTabla.Compute("Sum(montoPago)", "codMon=30").ToString()
        txtTotalDolares.Text = oGrilla.SumarColumnaGrilla(dgReporte, "montoPago", "codMon", "35") 'oTabla.Compute("Sum(montoPago)", "codMon=35").ToString()

        txtTotalSoles.Text = Format(CDbl(txtTotalSoles.Text), "0,0.00")
        txtTotalDolares.Text = Format(CDbl(txtTotalDolares.Text), "0,0.00")

        'Suma Detracciones
        txtTotalDetraccion.Text = oGrilla.SumarColumnaGrilla(dgReporte, "montoD", "codMon", "30") ' oTabla.Compute("Sum(montoPago)", "codMon=30").ToString()
        txtDetraccionDolares.Text = oGrilla.SumarColumnaGrilla(dgReporte, "montoD", "codMon", "35") ' oTabla.Compute("Sum(montoPago)", "codMon=30").ToString()

        txtTotalDetraccion.Text = Format(CDbl(txtTotalDetraccion.Text), "0,0.00")
        txtDetraccionDolares.Text = Format(CDbl(txtDetraccionDolares.Text), "0,0.00")


    End Sub

    Private Sub GastosPorDiaForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.Close()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If bindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        'vParam1 = dtpInicio.Text
        'vParam2 = dtpFin.Text
        'If String.IsNullOrEmpty(txtOrdCompra.Text) = False Then
        '    vParam2 = txtOrdCompra.Text.Trim() 'recuperarNroOrdenCompra()
        'Else
        '    vParam2 = ""
        'End If

        'Dim informe As New ReportViewerGastoPorDiaForm

        'informe.ShowDialog()

    End Sub

    Private Sub cbBanco_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBanco.SelectedIndexChanged
        Try
            If TypeOf cbBanco.SelectedValue Is String Then
                Dim selec As String = "select idCue,nroCue from TCuentaBan where codBan =" & cbBanco.SelectedValue
                oDataManager.CargarCombo(selec, CommandType.Text, cbCuenta, "idCue", "nroCue")

                If cbCuenta.Items.Count = 1 Then
                   
                    chkCuenta.Checked = False
                    
                End If

            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub chkBanco_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBanco.CheckedChanged
        If chkBanco.Checked Then
            cbBanco.Visible = False
            chkCuenta.Visible = False
            lblCta.Visible = False
            chkCuenta.Checked = True
        Else

            cbBanco.Visible = True
            If cbCuenta.Items.Count = 1 Then
                chkCuenta.Checked = False

            End If
            chkCuenta.Visible = True
            lblCta.Visible = True
        End If
    End Sub

    Private Sub chkCuenta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCuenta.CheckedChanged
        If chkCuenta.Checked Then
            cbCuenta.Visible = False
        Else
            cbCuenta.Visible = True
        End If
    End Sub

    Private Sub dtpFin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFin.ValueChanged, dtpInicio.ValueChanged

        If dtpInicio.Value > dtpFin.Value Then


            MessageBox.Show("Fechas Invalidas: desde " & dtpInicio.Text.Trim() & " hasta " & dtpFin.Text.Trim & ". Por favor corrija el rango de fechas: ", nomNegocio, Nothing, MessageBoxIcon.Error)
            dtpInicio.Focus()
        End If

    End Sub
End Class
