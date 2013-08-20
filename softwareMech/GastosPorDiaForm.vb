Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class GastosPorDiaForm

    Dim oDataManager As New cDataManager
    Dim bindingSource0 As New BindingSource

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
            With dgReporte
                'codigo material
                .Columns("fecPago").HeaderText = "Fecha"
                .Columns("fecPago").Width = 80
                'Material
                '.Columns("serie").HeaderText = ""
                '.Columns("serie").Width = 30
                'Codigo de Serie
                .Columns("nroDes").HeaderText = "N°_Des"
                .Columns("nroDes").Width = 55
                'razon
                .Columns("ruc").HeaderText = "RUC"
                .Columns("ruc").Width = 75

                .Columns("razon").HeaderText = "Razon Social"
                .Columns("razon").Width = 250

                .Columns("banco").HeaderText = "Banco"
                .Columns("banco").Width = 70

                .Columns("nroCue").HeaderText = "N°_Cuenta"
                .Columns("nroCue").Width = 100

                .Columns("nroOperacion").HeaderText = "N°"
                .Columns("nroOperacion").Width = 50

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

    

#End Region

    Private Sub GastosPorDiaForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        configurarColorControl()

        oDataManager.CargarCombo("select codBan,banco from TBanco", CommandType.Text, cbBanco, "codBan", "banco")

    End Sub

    Private Sub btnMostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMostrar.Click
        Dim sele As String = "select fecPago,ruc,razon,simbolo,montoPago,montoD,banco,nroCue,nroOperacion,(serie +'-'+cast(nroDes as varchar)) nroDes,codBan,codMon from VGastosPorDia where fecPago between '" & dtpInicio.Text & "' and '" & dtpFin.Text & "'"
        Dim oTabla As DataTable = oDataManager.CargarGrilla2(sele, CommandType.Text, dgReporte, bindingSource0)
        ModificandoColumnasDGV()
        bindingSource0.Filter = "codBan =" & cbBanco.SelectedValue

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

        Dim informe As New ReportViewerGastoPorDiaForm

        informe.ShowDialog()

    End Sub
End Class
