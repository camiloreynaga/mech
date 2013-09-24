Public Class ReportViewerAprobacionDesembolso


    Private _datosReporte As DataSetInformesCr

    Private Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Public Sub New(ByVal datos As DataSetInformesCr)

        Me.New()
        _datosReporte = datos
    End Sub

    Private Sub ReportViewerAprobacionDesembolso_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim informe As New CrAprobacionDesembolso

        informe.SetDataSource(_datosReporte)
        CrystalReportViewer1.ReportSource = informe
    End Sub
End Class