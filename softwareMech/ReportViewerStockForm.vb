Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared


Public Class ReportViewerStockForm

    Public vCodUbicacion As Integer

    Private Sub ReportViewerStockForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport8

        'Haciendo la conexion
        VerificaConexion()

        Dim wait As New waitForm
        wait.Show()


        VStockAlmacen1TableAdapter1.Connection = Cn
        DataSetAlmacen1.Clear()
        VStockAlmacen1TableAdapter1.Fill(DataSetAlmacen1.Tables("vStockAlmacen1"), vCodUbicacion)
        reporte.SetDataSource(DataSetAlmacen1)

        'reporte.SetParameterValue()

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CReportViewer.Load



    End Sub
End Class