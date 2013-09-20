Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared



Public Class ReportViewerGastosDiaForm

    Public fecIni As Date
    Public fecFin As Date

    Private Sub ReportViewerGastosDiaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport9

        'Haciendo la conexion
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VGastosPorDiaTableAdapter1.Connection = Cn
        DataSetAlmacen1.Clear()
        VGastosPorDiaTableAdapter1.Fill(DataSetAlmacen1.Tables("vGastosPorDia"), fecIni, fecFin)
        reporte.SetDataSource(DataSetAlmacen1)

        CReportViewer1.DisplayGroupTree = False
        CReportViewer1.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub
End Class