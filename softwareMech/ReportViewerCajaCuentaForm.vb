Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.Data.SqlClient
Public Class ReportViewerCajaCuentaForm
    Private Sub ReportViewerCajaCuentaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport10
        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VDetSolCajaCuentaImprimirTableAdapter1.Connection = Cn

        DataSetAlmacen1.Clear()
        VDetSolCajaCuentaImprimirTableAdapter1.Fill(DataSetAlmacen1.Tables("VDetSolCajaCuentaImprimir"), vCodDoc) '0=Ingreso Normal 1=Improvisado
        reporte.SetDataSource(DataSetAlmacen1)

        reporte.SetParameterValue("vCFac", recuperarTotal(vCodDoc, 1))     '   1=Factura
        reporte.SetParameterValue("vCBol", recuperarTotal(vCodDoc, 2))     '   1=Factura
        reporte.SetParameterValue("vCHon", recuperarTotal(vCodDoc, 3))     '   1=Factura
        reporte.SetParameterValue("vCOtr", recuperarTotal(vCodDoc, 4))     '   1=Factura

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function recuperarTotal(ByVal codSC As Integer, ByVal comp As Short) As Double
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(sum(totReal),0) from VDetSolCajaCuentaImprimir where codSC=" & codSC & " and compCheck=" & comp
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
End Class