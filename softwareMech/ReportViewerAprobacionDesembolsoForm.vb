Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource


Public Class ReportViewerAprobacionDesembolsoForm

    ''' <summary>
    ''' estado de Aprobacion, falso para mostrar sólo los pendientes, true para mostrar todos.  
    ''' mientras no se hayan pagado por Yoel
    ''' </summary>
    ''' <remarks></remarks>
    Public todos As Boolean = False

    Public serie As String


    Private Sub ReportViewerAprobacionDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor

        Dim reporte As New CrystalReport10
        'Haciendo Conexion
        VerificaConexion()

        Dim wait As New waitForm
        wait.Show()

        VOrdenDesemGerencia1TableAdapter1.Connection = Cn
        DataSetAlmacen1.Clear()
        If todos = False Then
            VOrdenDesemGerencia1TableAdapter1.Fill(DataSetAlmacen1.Tables("VOrdenDesemGerencia1"), 0, 0, 2) ', "100")
        Else
            VOrdenDesemGerencia1TableAdapter1.Fill(DataSetAlmacen1.Tables("VOrdenDesemGerencia1"), 0, 1, 2) ', serie)

        End If

        reporte.SetDataSource(DataSetAlmacen1)

        ' CReportViewer1.DisplayGroupTree = False
        CReportViewer1.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub
End Class