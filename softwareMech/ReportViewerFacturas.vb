Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared


Public Class ReportViewerFacturas

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

    Private Sub ReportViewerFacturas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Cursor = Cursors.WaitCursor
        Dim wait As New waitForm
        wait.Show()

        Dim informe As New CrFacturasConsolidado
        informe.SetDataSource(_datosReporte)
        CrystalReportViewer1.ReportSource = informe

        CrystalReportViewer1.DisplayGroupTree = False

        wait.Close()
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Exporta a PDF
    ''' </summary>
    ''' <param name="rpt"></param>
    ''' <param name="NombreArchivo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim grabar As SaveFileDialog = New SaveFileDialog
        'Título del cuadro de diálogo.
        grabar.AddExtension = True
        grabar.Title = "Exportar Informe a PDF"
        grabar.FileName = "Facturas"
        'Filtro para el tipo de archivo a guardar.
        '
        grabar.Filter = "Adobe Acrobat (*.pdf)|*.pdf" '"Microsoft Excel (*.xls)|*.xls" '& _
        '"|Microsoft Excel (Sólo Datos)|*.xls|Adobe Acrobat (*.pdf)|*.pdf|Microsoft Word (*.doc)|*.doc"

        'Mostrar como predeterminado el tipo # 1
        grabar.FilterIndex = 1
        'Si se presiona botón Guardar
        If (grabar.ShowDialog = Windows.Forms.DialogResult.OK) Then

            ExportToPDF(CrystalReportViewer1.ReportSource, grabar.FileName)


            MessageBox.Show("La exportación ha finalizado correctamente.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' End If
    End Sub
End Class