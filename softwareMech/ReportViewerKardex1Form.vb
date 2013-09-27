Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class ReportViewerKardex1Form

    Private Sub ReportViewerKardex1Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim reporte As New CrystalReport5
        'Realizando la conexion con SQL Server ConexionModule.vb
        'conexion()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        VKardexTableAdapter1.Connection = Cn

        DataSetAlmacen1.Clear()
        VKardexTableAdapter1.Fill(DataSetAlmacen1.Tables("VKardex"), vCodProd, vCodUbi)
        reporte.SetDataSource(DataSetAlmacen1)

        'reporte.SetParameterValue("param1", vParam1)     'parametro creado en el diseñador

        CReportViewer.DisplayGroupTree = False
        CReportViewer.ReportSource = reporte
        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim grabar As SaveFileDialog = New SaveFileDialog
        'Título del cuadro de diálogo.
        grabar.AddExtension = True
        grabar.Title = "Exportar Informe a PDF"
        grabar.FileName = "KARDEX "
        'Filtro para el tipo de archivo a guardar.
        '
        grabar.Filter = "Adobe Acrobat (*.pdf)|*.pdf" '"Microsoft Excel (*.xls)|*.xls" '& _
        '"|Microsoft Excel (Sólo Datos)|*.xls|Adobe Acrobat (*.pdf)|*.pdf|Microsoft Word (*.doc)|*.doc"

        'Mostrar como predeterminado el tipo # 1
        grabar.FilterIndex = 1
        'Si se presiona botón Guardar
        If (grabar.ShowDialog = Windows.Forms.DialogResult.OK) Then

            ExportToPDF(CReportViewer.ReportSource, grabar.FileName)


            MessageBox.Show("La exportación ha finalizado correctamente.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' End If
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
End Class