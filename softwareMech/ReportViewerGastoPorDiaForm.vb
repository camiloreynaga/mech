Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared


Public Class ReportViewerGastoPorDiaForm

    Private Sub ReportViewerGastoPorDiaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport7
        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VGastosPorDiaTableAdapter1.Connection = Cn

        DataSetAlmacen1.Clear()
        VGastosPorDiaTableAdapter1.Fill(DataSetAlmacen1.Tables("VGastosPorDia"))
        reporte.SetDataSource(DataSetAlmacen1)

        'reporte.SetParameterValue("param1", vParam1)     'parametro creado en el diseñador

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub
End Class