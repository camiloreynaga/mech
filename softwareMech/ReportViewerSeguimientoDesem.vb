Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Public Class ReportViewerSeguimientoDesem

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


    Private Sub ReportViewerSeguimientoDesem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor
        Dim wait As New waitForm
        wait.Show()

        Dim informe As New CrSeguimientoDesembolso
        informe.SetDataSource(_datosReporte)
        CrystalReportViewer1.ReportSource = informe


        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub



    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim grabar As New SaveFileDialog

        grabar.AddExtension = True
        grabar.Title = "Exportar Informe PDF"
        grabar.FileName = "Seguimiento Desembolsos"

        'Filtro para el tipo de archivo a guardar

        grabar.Filter = "Adobe Acrobat (*.pdf)|*.pdf"

        grabar.FilterIndex = 1
        If (grabar.ShowDialog = Windows.Forms.DialogResult.OK) Then

            ExportToPDF(CrystalReportViewer1.ReportSource, grabar.FileName)
            MessageBox.Show("La exportación ha finalizado correctamente.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If


    End Sub

    Public Function ExportToPDF(ByVal rpt As ReportDocument, ByVal NombreArchivo As String) As String

        Dim vFileName As String
        Dim diskOpts As New DiskFileDestinationOptions

        Try

            With rpt.ExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat

            End With

            vFileName = NombreArchivo
            diskOpts.DiskFileName = vFileName
            rpt.ExportOptions.DestinationOptions = diskOpts
            rpt.Export()

        Catch ex As Exception
            Throw ex
        End Try

        Return vFileName
    End Function


End Class