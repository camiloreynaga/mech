Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class ReportViewerInf1CajaForm
    Private Sub ReportViewerInf1CajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport9
        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VMovimientoCajaTableAdapter1.Connection = Cn

        DataSetAlmacen1.Clear()
        VMovimientoCajaTableAdapter1.Fill(DataSetAlmacen1.Tables("VMovimientoCaja"), vX3, vX4, vX2) '0=Ingreso Normal 1=Improvisado
        reporte.SetDataSource(DataSetAlmacen1)

        reporte.SetParameterValue("param1", vParam1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param2", vFec1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param3", vFec2)     'parametro creado en el diseñador

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub
End Class