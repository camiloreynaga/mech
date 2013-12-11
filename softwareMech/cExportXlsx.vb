Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports DocumentFormat.OpenXml
Imports ClosedXML.Excel
Imports System.Windows.Forms


Public Class cExportXlsx

    ''' <summary>
    ''' Exporta una Grilla a Excel
    ''' </summary>
    ''' <param name="_filaInicio">fila inicial</param>
    ''' <param name="_columnaInicio">columna inicial</param>
    ''' <param name="_fileName">nombre del archivo excel (usa un savefiledialog)</param>
    ''' <param name="_sheetName">nombre de la hoja excel</param>
    ''' <param name="dgv">DataGridView que se consume</param>
    ''' <param name="title">Titulo de la hoja, va en la celda C1 </param>
    ''' <remarks></remarks>
    Public Sub export2Excel(ByVal _filaInicio As Integer, ByVal _columnaInicio As Integer, ByVal _fileName As String, ByVal _sheetName As String, ByVal dgv As DataGridView, ByVal title As String)
        'Validando _

        If _filaInicio <= 1 Or _columnaInicio <= 1 Then
            _filaInicio = 2
            _columnaInicio = 2
        End If




        'creando el objeto XLSX
        Dim wb As New XLWorkbook()
        Dim ws As IXLWorksheet

        ws = wb.Worksheets.Add(_sheetName)

        'rango de hoja
        Dim rngHoja As IXLRange = ws.Range(_filaInicio, _columnaInicio, dgv.RowCount + _filaInicio, dgv.ColumnCount)

        'rngHoja = ws.Range(_filaInicio, _columnaInicio + 1, _filaInicio + dgv.RowCount, _rngColumna)

        'obteniendo las columnas del tipo númerico
        Dim tipoDato As New List(Of Type)

        'Titulo de la hoja Excel
        ws.Cell("c1").Value = title
        ws.Cell("c1").Style.Font.Bold = True
        ws.Cell("c1").Style.Font.FontSize = 12



        'obteniendo encabezados de cada columna
        For index As Integer = 0 To dgv.Columns.Count() - 1
            If dgv.Columns(index).Visible Then
                'Obteniendo el valor de los Encabezados de columna
                ws.Cell(_filaInicio, index + _columnaInicio).Value = dgv.Columns(index).HeaderText


            End If
            'obteniendo el tipo de dato para cada columna
            tipoDato.Add(dgv.Columns(index).ValueType)
        Next


        'obteniendo el detalle de la grilla
        'Recorriendo la filas
        For i As Integer = 0 To dgv.RowCount - 1
            'recorriendo las columnas
            For j As Integer = 0 To dgv.ColumnCount - 1
                'Validando si es una columna visible
                If dgv.Columns(j).Visible Then
                    'obteniendo el valor de la celda (fila,columna)
                    ws.Cell((_filaInicio + 1) + i, (_columnaInicio) + j).Value = dgv.Rows(i).Cells(j).Value

                    'Dando formato a los número decimales
                    If dgv.Columns(j).ValueType.Name = "Decimal" Then
                        ws.Cell((_filaInicio + 1) + i, (_columnaInicio) + j).Style.NumberFormat.Format = "#,##0.00"
                    End If
                End If
            Next
        Next





        'Dando formato a las celdas
        'obteniendo el rango de las celdas

        Dim rngData As IXLRange = rngHoja.Range(rngHoja.FirstCellUsed(), rngHoja.LastCellUsed()) '(_filaInicio + 1, _columnaInicio, _filaInicio + dgv.RowCount, _rngColumna)
        rngData.Style.Font.FontSize = 11

        'Dando Bordes        
        rngData.Style.Border.BottomBorder = XLBorderStyleValues.Dashed 'inferior
        rngData.Style.Border.RightBorder = XLBorderStyleValues.Dashed 'derecha

        rngData.FirstRow().Style.Border.TopBorder = XLBorderStyleValues.Thin
        rngData.LastRow().Style.Border.BottomBorder = XLBorderStyleValues.Thin
        'contorno
        rngHoja.FirstColumnUsed(False).Style.Border.LeftBorder = XLBorderStyleValues.Thin
        rngHoja.LastColumnUsed(False).Style.Border.RightBorder = XLBorderStyleValues.Thin

        'rngHoja.FirstRowUsed(False).Style.Border.TopBorder = XLBorderStyleValues.Thin
        'rngHoja.LastRowUsed(False).Style.Border.BottomBorder = XLBorderStyleValues.Thin

        'Ajustando las columnas segun su contenido

        'Dando formato a los encabezado de celda
        'obteniendo los valores del rango
        Dim _r1 As Integer = rngHoja.FirstRowUsed().RangeAddress.FirstAddress.RowNumber()
        Dim _c1 As String = rngHoja.FirstColumnUsed().RangeAddress.LastAddress.ColumnLetter()
        Dim _cLast As String = rngHoja.LastColumnUsed().RangeAddress.LastAddress.ColumnLetter

        Dim _celdaIni As String = _c1 & _r1.ToString()
        Dim _celdaFin As String = _cLast & _r1.ToString()

        Dim rngHeaders As IXLRange = ws.Range(_celdaIni, _celdaFin)

        'Customizando el formato de los encabezados
        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        rngHeaders.Style.Font.Bold = True
        rngHeaders.Style.Font.FontSize = 12
        'Borde de la primera fila
        rngHeaders.Style.Border.LeftBorder = XLBorderStyleValues.Thin


        'rngHoja.ColumnsUsed().AdjustToContents()
        ws.Columns(rngHeaders.RangeAddress.FirstAddress.ColumnNumber, rngHeaders.RangeAddress.LastAddress.ColumnNumber).AdjustToContents()

        'saveFileXlsx()

        '_fileName += ".xlsx"
        wb.SaveAs(_fileName)

        MessageBox.Show("Exportación finalizada")

    End Sub
    ''' <summary>
    ''' SaveFileDialog para guardar hojas excel
    ''' </summary>
    ''' <returns>path(ruta) del archivo más nombre con extension xlsx</returns>
    ''' <remarks>SaveFileDialog para guardar hojas excel</remarks>
    Public Function saveFileXlsx() As String

        'Dim myStream As System.IO.Stream

        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Excel |*.xlsx"
        saveFileDialog.Title = "Guardar archivo Excel"
        saveFileDialog.AddExtension = True

        saveFileDialog.RestoreDirectory = True
        saveFileDialog.OverwritePrompt = True
        saveFileDialog.ValidateNames = True

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Return saveFileDialog.FileName
        Else
            Return ""
        End If

    End Function

End Class
