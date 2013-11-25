Imports System.Globalization.CultureInfo
Imports System
Imports System.Collections

Public Class cConfigFormControls

    Public Sub ConfigGrilla(ByVal grilla As DataGridView)

        'seleción por filas completas
        grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'seleccion una a la vez
        grilla.MultiSelect = False

        'For index As Integer = 0 To grilla.Rows.Count - 1
        '    grilla.Rows(index).HeaderCell.Value = index + 1
        'Next

        ' grilla.Rows(0).HeaderCell.Value = 1

    End Sub

    ''' <summary>
    ''' Configurar color de controles seleccionados
    ''' </summary>
    ''' <param name="pControl">Nombre de Control Ej. Label</param>
    ''' <param name="contenedor">Nombre de contenedor GroupBox</param>
    ''' <param name="pColor">Color</param>
    ''' <remarks></remarks>
    Public Sub configurarColorControl(ByVal pControl As String, ByVal contenedor As GroupBox, ByVal pColor As Color)

        For index As Integer = 0 To contenedor.Controls.Count - 1


            Dim tipo As String = contenedor.Controls(index).GetType().ToString()
            If contenedor.Controls(index).GetType().ToString() = "System.Windows.Forms." & pControl Then

                contenedor.Controls(index).ForeColor = pColor

            End If
        Next

    End Sub
    ''' <summary>
    ''' Condigurar Color de controles seleccionados
    ''' </summary>
    ''' <param name="pControl">Nombre de Control Ej. Label </param>
    ''' <param name="container">Nombre de contenedor Ej. GroupBox</param>
    ''' <param name="pColor">Color</param>
    ''' <remarks></remarks>
    Public Sub configurarColorControl2(ByVal pControl As String, ByVal container As Object, ByVal pColor As Color)

        For index As Integer = 0 To CType(container, ContainerControl).Controls.Count - 1

            Dim tipo As String = CType(container, ContainerControl).Controls(index).GetType().ToString()
            If CType(container, ContainerControl).Controls(index).GetType().ToString() = "System.Windows.Forms." & pControl Then
                CType(container, ContainerControl).Controls(index).ForeColor = pColor
            End If

        Next

    End Sub


    ''' <summary>
    ''' Configurar color de controles seleccionados
    ''' </summary>
    ''' <param name="pControl">Nombre de Control Ej. Label</param>
    ''' <param name="contenedor">Nombre de contenedor panel</param>
    ''' <param name="pColor">Color</param>
    ''' <remarks></remarks>
    Public Sub configurarColorControlPanel(ByVal pControl As String, ByVal contenedor As Panel, ByVal pColor As Color)
        For index As Integer = 0 To contenedor.Controls.Count - 1


            Dim tipo As String = contenedor.Controls(index).GetType().ToString()
            If contenedor.Controls(index).GetType().ToString() = "System.Windows.Forms." & pControl Then

                contenedor.Controls(index).ForeColor = pColor

            End If
        Next



    End Sub

    ''' <summary>
    ''' Da formato numerico Ej 1,200.00
    ''' </summary>
    ''' <param name="numero"></param>
    ''' <remarks></remarks>
    Public Sub FormatoContabilidad(ByVal numero As TextBox)

        If String.IsNullOrEmpty(numero.Text) = False Then
            Dim valor As Double = CDbl(numero.Text)
            numero.Text = valor.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture)
            'Return numero
        End If

    End Sub

    ''' <summary>
    ''' Da color a las filas de un data grid view
    ''' </summary>
    ''' <param name="grilla">DataGridView </param>
    ''' <param name="columna">Columna a Colorear </param>
    ''' <param name="criterio">Criterio que debe cumplir para que se coloree </param>
    ''' <param name="pBackColor">color de fondo</param>
    ''' <param name="pForeColor">color de texto</param>
    ''' <remarks></remarks>
    Public Sub colorearFilasDGV(ByVal grilla As DataGridView, ByVal columna As Integer, ByVal criterio As Object, ByVal pBackColor As Color, ByVal pForeColor As Color)
        For j As Short = 0 To grilla.Rows.Count - 1
            If grilla(columna, j).Value = criterio Then 'Aprobado
                grilla.Rows(j).Cells(columna).Style.BackColor = pBackColor 'Color.YellowGreen
                grilla.Rows(j).Cells(columna).Style.ForeColor = pForeColor
            End If
        Next
    End Sub

    ''' <summary>
    ''' Da color a las filas de un data grid view
    ''' </summary>
    ''' <param name="grilla">DataGridView </param>
    ''' <param name="columna">Columna a Colorear </param>
    ''' <param name="criterio">Criterio que debe cumplir para que se coloree </param>
    ''' <param name="pBackColor">color de fondo</param>
    ''' <param name="pForeColor">color de texto</param>
    ''' <remarks></remarks>
    Public Sub colorearFilasDGV(ByVal grilla As DataGridView, ByVal columna As String, ByVal criterio As Object, ByVal pBackColor As Color, ByVal pForeColor As Color)
        For j As Short = 0 To grilla.Rows.Count - 1

            Dim obj As Object = grilla(columna, j).Value
            If grilla(columna, j).Value = criterio Then 'Aprobado
                grilla.Rows(j).Cells(columna).Style.BackColor = pBackColor 'Color.YellowGreen
                grilla.Rows(j).Cells(columna).Style.ForeColor = pForeColor
            End If
        Next
    End Sub
    ''' <summary>
    ''' Da color a las filas de un data grid view
    ''' </summary>
    ''' <param name="grilla">DataGridView </param>
    ''' <param name="columna">Columna a Colorear </param>
    ''' <param name="columnaCriterio">Columna a Evaluar</param>
    ''' <param name="criterio">Criterio que debe cumplir para que se coloree </param>
    ''' <param name="pBackColor">color de fondo</param>
    ''' <param name="pForeColor">color de texto</param>
    ''' <remarks></remarks>
    Public Sub colorearFilasDGV(ByVal grilla As DataGridView, ByVal columna As String, ByVal columnaCriterio As String, ByVal criterio As Object, ByVal pBackColor As Color, ByVal pForeColor As Color)
        For j As Short = 0 To grilla.Rows.Count - 1

            Dim obj As Object = grilla(columna, j).Value
            If grilla(columnaCriterio, j).Value = criterio Then 'Aprobado
                grilla.Rows(j).Cells(columna).Style.BackColor = pBackColor 'Color.YellowGreen
                grilla.Rows(j).Cells(columna).Style.ForeColor = pForeColor
            End If
        Next
    End Sub
    ''' <summary>
    ''' Da color a las filas de un data grid view
    ''' </summary>
    ''' <param name="grilla">DataGridView </param>
    ''' <param name="columna">Columna a Colorear </param>
    ''' <param name="pBackColor">color de fondo</param>
    ''' <param name="pForeColor">color de texto</param>
    ''' <remarks></remarks>
    Public Sub colorearFilasDGV(ByVal grilla As DataGridView, ByVal columna As String, ByVal pBackColor As Color, ByVal pForeColor As Color)
        For j As Short = 0 To grilla.Rows.Count - 1

            Dim obj As Object = grilla(columna, j).Value
            grilla.Rows(j).Cells(columna).Style.BackColor = pBackColor 'Color.YellowGreen
            grilla.Rows(j).Cells(columna).Style.ForeColor = pForeColor
        Next
    End Sub


    ''' <summary>
    ''' Configurar el Estilo (Font) de la columna por cada fila
    ''' </summary>
    ''' <param name="grilla">DataGridView</param>
    ''' <param name="columna">nombre de Columna</param>
    ''' <param name="estilo">Estilo</param>
    ''' <remarks></remarks>
    Public Sub EstiloColumnaDGV(ByVal grilla As DataGridView, ByVal columna As String, ByVal estilo As Font)
        For j As Short = 0 To grilla.Rows.Count - 1

            'Dim obj As Object = grilla(columna, j).Value
            'If grilla(columna, j).Value = criterio Then 'Aprobado
            grilla.Rows(j).Cells(columna).Style.Font = estilo  'Color.YellowGreen
            'grilla.Rows(j).Cells(columna).Style.ForeColor = pForeColor
            'End If
        Next
    End Sub

    ''' <summary>
    ''' Suma una Columna de DataGridView
    ''' </summary>
    ''' <param name="Dgv">DataGridView</param>
    ''' <param name="columnaSuma">Columa a Suma</param>
    ''' <param name="columnaCondicion">Columna condicional</param>
    ''' <param name="condicion">Condicion</param>
    ''' <returns>Suma total</returns>
    ''' <remarks></remarks>
    Public Function SumarColumnaGrilla(ByVal Dgv As DataGridView, ByVal columnaSuma As String, ByVal columnaCondicion As String, ByVal condicion As String) As Double

        Dim total As Double = 0.0
        Try
            For i As Integer = 0 To Dgv.RowCount - 1
                If Dgv.Item(columnaCondicion, i).Value.ToString = condicion Then
                    total = total + CDbl(Dgv.Item(columnaSuma, i).Value)
                End If
            Next
        Catch ex As Exception
        End Try
        Return total
    End Function

    Public Function SumarColumnaGrillaArray(ByVal Dgv As DataGridView, ByVal columnaSuma As String, ByVal columnaCondicion() As String, ByVal condicion() As String) As Double
        Dim total As Double = 0.0
        Dim verifica As Boolean = True

        If columnaCondicion.Count() = condicion.Count() Then

            Try
                'recorriendo la grilla
                For i As Integer = 0 To Dgv.RowCount - 1
                    'recorriendo el arreglo de condicionales
                    For j As Integer = 0 To columnaCondicion.Count - 1
                        'evaluando la condición
                        If Dgv.Item(columnaCondicion(j), i).Value.ToString = condicion(j) Then
                            verifica = True
                        Else
                            verifica = False
                            'sale del for de condicional
                            Exit For
                        End If
                    Next

                    'Revisando si cumple la condición
                    If verifica = True Then
                        total = total + CDbl(Dgv.Item(columnaSuma, i).Value)
                    End If
                    'Reestableciendo la verificación 
                    verifica = True

                Next
            Catch ex As Exception
            End Try
        End If

        Return total
    End Function

    ''' <summary>
    ''' Suma una Columna de DataGridView
    ''' </summary>
    ''' <param name="Dgv">DataGridView</param>
    ''' <param name="columnaSuma">Columa a Suma</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SumarColumnaGrilla(ByVal Dgv As DataGridView, ByVal columnaSuma As String) As Double

        Dim total As Double = 0.0
        Try
            For i As Integer = 0 To Dgv.RowCount - 1
                total = total + CDbl(Dgv.Item(columnaSuma, i).Value)
            Next
        Catch ex As Exception
        End Try
        Return total
    End Function
End Class
