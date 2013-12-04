Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class ReportViewerDocVentaForm
    Private Sub ReportViewerDocVentaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport11

        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        reporte.SetParameterValue("param1", vCliente)     'parametro creado en el diseñador
        reporte.SetParameterValue("param2", vDir)     'parametro creado en el diseñador
        reporte.SetParameterValue("param3", vRuc)     'parametro creado en el diseñador
        reporte.SetParameterValue("param4", vObs)     'parametro creado en el diseñador
        reporte.SetParameterValue("param5", vFec1)     'parametro creado en el diseñador

        reporte.SetParameterValue("param6", vCan1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param7", vDet1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param8", vPre1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param9", vTot1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param10", vDes1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param11", vObra)     'parametro creado en el diseñador

        If vDet2.Trim() <> "" Then
            reporte.SetParameterValue("param12", vCan2)     'parametro creado en el diseñador
            reporte.SetParameterValue("param13", vDet2)     'parametro creado en el diseñador
            reporte.SetParameterValue("param14", vPre2)     'parametro creado en el diseñador
            reporte.SetParameterValue("param15", vTot2)     'parametro creado en el diseñador
            reporte.SetParameterValue("param16", vDes2)     'parametro creado en el diseñador
        Else
            reporte.SetParameterValue("param12", "")     'parametro creado en el diseñador
            reporte.SetParameterValue("param13", "")     'parametro creado en el diseñador
            reporte.SetParameterValue("param14", "")     'parametro creado en el diseñador
            reporte.SetParameterValue("param15", "")     'parametro creado en el diseñador
            reporte.SetParameterValue("param16", vDes2)     'parametro creado en el diseñador
        End If

        reporte.SetParameterValue("param17", vLetra)     'parametro creado en el diseñador
        reporte.SetParameterValue("param18", vSub)     'parametro creado en el diseñador
        reporte.SetParameterValue("param19", vIgv1)     'parametro creado en el diseñador
        reporte.SetParameterValue("param20", vTot)     'parametro creado en el diseñador
        reporte.SetParameterValue("param21", vMon)     'parametro creado en el diseñador
        reporte.SetParameterValue("param21", vMon)     'parametro creado en el diseñador
        reporte.SetParameterValue("param22", vSerie)     'parametro creado en el diseñador

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub
End Class