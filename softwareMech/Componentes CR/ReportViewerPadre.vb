Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

''' <summary>
''' Clase para la configuración de reportes
''' </summary>
''' <remarks></remarks>
Public Class ReportViewerPadre

#Region "Variables"

    ''' <summary>
    ''' DataSet CrInformes
    ''' </summary>
    ''' <remarks></remarks>
    Private _datosReporte As DataSetInformesCr


#End Region

#Region "Métodos"

    ''' <summary>
    ''' constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    ''' <summary>
    ''' constructor con parametros
    ''' </summary>
    ''' <param name="datos">DataTable del DataSet CrInformes</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal datos As DataSetInformesCr)
        Me.New()
        _datosReporte = datos

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

    ''' <summary>
    ''' Guarda en Formato PDF el archivo
    ''' </summary>
    ''' <param name="fileName">nombre del archivo</param>
    ''' <remarks></remarks>
    Public Sub GuardarPdf(ByVal fileName)
        Dim grabar As SaveFileDialog = New SaveFileDialog
        'Título del cuadro de diálogo.
        grabar.AddExtension = True
        grabar.Title = "Exportar Informe a PDF"
        grabar.FileName = fileName
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

    End Sub

#End Region

#Region "Eventos"

#End Region

    
End Class