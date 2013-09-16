Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.Data.SqlClient
Public Class ReportViewerReqCajaForm
    Private Sub ReportViewerReqCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport7
        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VDetSolCajaImprimirTableAdapter1.Connection = Cn

        DataSetAlmacen1.Clear()
        VDetSolCajaImprimirTableAdapter1.Fill(DataSetAlmacen1.Tables("VDetSolCajaImprimir"), vCodDoc, 0) '0=Ingreso Normal 1=Improvisado
        reporte.SetDataSource(DataSetAlmacen1)

        reporte.SetParameterValue("vCFac", recuperarTotal(vCodDoc, 0, 1))     '0=Normal   1=Factura
        reporte.SetParameterValue("vCBol", recuperarTotal(vCodDoc, 0, 2))     '0=Normal   1=Factura
        reporte.SetParameterValue("vCHon", recuperarTotal(vCodDoc, 0, 3))     '0=Normal   1=Factura
        reporte.SetParameterValue("vCOtr", recuperarTotal(vCodDoc, 0, 4))     '0=Normal   1=Factura

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function recuperarTotal(ByVal codSC As Integer, ByVal ing As Short, ByVal comp As Short) As Double
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(sum(totPar),0) from VDetSolCajaImprimir where codSC=" & codSC & " and ingreso=" & ing & " and compCheck=" & comp
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
End Class