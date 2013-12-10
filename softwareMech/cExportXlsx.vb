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
    ''' <param name="_filaInicio"></param>
    ''' <param name="_columnaInicio"></param>
    ''' <param name="_fileName"></param>
    ''' <remarks></remarks>
    Public Sub export2Excel(ByVal _filaInicio As Integer, ByVal _columnaInicio As Integer, ByVal _fileName As String, ByVal dgv As DataGridView)

        'Dim _filaInicio As Integer = 2
        'Dim _columnaInicio As Integer = 2

        'creando el objeto XLSX
        Dim wb As New XLWorkbook()
        Dim ws As IXLWorksheet

        ws = wb.Worksheets.Add(_fileName)

        'rango de Columna , columnas visibles
        Dim _rngColumna As Integer = dgv.ColumnCount - _columnaInicio  '0

        'rango de encabezados
        Dim rngHeaders As IXLRange '= ws.Range(0, 0, 0, 0)

        'Rango de detalle
        Dim rngData As IXLRange '= ws.Range(0, 0, 0, 0)

        'rango de hoja
        Dim rngHoja As IXLRange '= ws.Range(_filaInicio, _columnaInicio, dgv.RowCount, _rngColumna)

        'obteniendo las columnas del tipo númerico
        Dim tipoDato As New List(Of Type)

        '= dgv.Columns(1).ValueType.Name




        'obteniendo encabezados de cada columna
        For index As Integer = 0 To dgv.Columns.Count() - 1
            If dgv.Columns(index).Visible Then
                'Obteniendo el valor de los Encabezados de columna
                ws.Cell(_filaInicio, index + _columnaInicio).Value = dgv.Columns(index).HeaderText


            End If
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


        'Dando formato a los encabezado de celda
        'obteniendo los valores del rango
        rngHeaders = ws.Range(_filaInicio + 1, _columnaInicio, _filaInicio, dgv.ColumnCount - 1)
        'Customizando el formato de los encabezados
        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        rngHeaders.Style.Font.Bold = True
        rngHeaders.Style.Font.FontSize = 12


        'Dando formato a las celdas
        'obteniendo el rango de las celdas
        rngData = ws.Range(_filaInicio + 1, _columnaInicio, _filaInicio + dgv.RowCount, _rngColumna)
        rngData.Style.Font.FontSize = 11

        'Dando Bordes
        rngHoja = ws.Range(_filaInicio, _columnaInicio + 1, _filaInicio + dgv.RowCount, _rngColumna) '(_filaInicio, _columnaInicio + 1, dgv.RowCount, _rngColumna)

        rngHoja.Style.Border.BottomBorder = XLBorderStyleValues.Dashed 'inferior
        rngHoja.Style.Border.RightBorder = XLBorderStyleValues.Dashed 'derecha
        'rngHoja.Style.Border.LeftBorder = XLBorderStyleValues.Dashed 'izquierda

        'contorno
        rngHoja.FirstColumn().Style.Border.LeftBorder = XLBorderStyleValues.Thin
        rngHoja.LastColumn().Style.Border.RightBorder = XLBorderStyleValues.Thin
        rngHoja.FirstRow().Style.Border.TopBorder = XLBorderStyleValues.Thin
        rngHoja.LastRow().Style.Border.BottomBorder = XLBorderStyleValues.Thin

        'Ajustando las columnas segun su contenido
        ws.Columns(_columnaInicio + 1, _rngColumna).AdjustToContents()

        _fileName += ".xlsx"
        wb.SaveAs(_fileName)

        MessageBox.Show("Exportación finalizada")

    End Sub
End Class
