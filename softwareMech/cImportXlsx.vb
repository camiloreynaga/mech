Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Xml
Imports ICSharpCode.SharpZipLib.Zip


Public Class cImportXlsx

    Public Sub DeleteDirectoryContents(ByVal directory As String)

        Dim info As DirectoryInfo = New DirectoryInfo(directory)

        For Each file As FileInfo In info.GetFiles()
            file.Delete()
        Next

        For Each dir As DirectoryInfo In info.GetDirectories()
            dir.Delete(True)
        Next

    End Sub
    ''' <summary>
    ''' descomprime el archvio excel
    ''' </summary>
    ''' <param name="zipFileName"></param>
    ''' <param name="directoryTarget"></param>
    ''' <remarks></remarks>
    Public Sub unzipFile(ByVal zipFileName As String, ByVal directoryTarget As String)

        Dim z As FastZip = New FastZip()
        z.ExtractZip(zipFileName, directoryTarget, "")

    End Sub

    ''' <summary>
    ''' lee los valores del tipo string del archivo excel
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function readStringTable(ByVal input As Stream) As List(Of String)

        Dim stringTable As New List(Of String)

        Using reader As XmlReader = XmlReader.Create(input)
            While reader.Read()
                If reader.NodeType = XmlNodeType.Element And reader.Name = "t" Then
                    stringTable.Add(reader.ReadElementString())
                End If
            End While
        End Using

        Return stringTable

    End Function

    ''' <summary>
    ''' leer los estilos para las celdas del excel
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function readStyleTable(ByVal input As Stream) As List(Of Integer)
        Dim styleTable As New List(Of Integer)
        Dim _loop As Integer = 0
        Dim i As Integer = 0
        Using reader As XmlReader = XmlReader.Create(input)
            While reader.Read
                If reader.NodeType = XmlNodeType.Element Then
                    Select Case reader.Name


                        Case "cellXfs"
                            _loop = reader.GetAttribute("count")

                        Case "xf"
                            If _loop > 0 And i < _loop Then
                                Dim style As Integer = reader.GetAttribute("numFmtId")
                                styleTable.Add(style)
                                i += 1
                            End If
                            'Case Else
                            '    _loop = 0

                    End Select
                End If
            End While
        End Using

        Return styleTable

    End Function

    ''' <summary>
    ''' create una columna en un datatable
    ''' </summary>
    ''' <param name="data">datatable</param>
    ''' <remarks></remarks>
    Private Sub createColumn(ByVal data As DataTable)
        data.Columns.Add(New DataColumn())


    End Sub

    'condor de columnas
    ''' <summary>
    ''' calcula el número de columnas usadas
    ''' </summary>
    ''' <param name="input">archivo origen </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function nroColumnas(ByVal input As Stream) As List(Of Integer)

        Using reader As XmlReader = XmlReader.Create(input)

            Dim columns As New List(Of Integer)
            Dim columCount As Integer = 0

            While reader.Read()

                If reader.NodeType = XmlNodeType.Element Then

                    Select Case reader.Name

                        Case "row"
                            columns.Add(columCount)
                            columCount = 0

                        Case "c"

                            reader.Read()
                            columCount += 1

                    End Select

                End If

            End While


            Return columns
        End Using

    End Function



    Public Sub readWorkSheet(ByVal input As Stream, ByVal stringTable As IList(Of String), ByVal styleTable As IList(Of Integer), ByVal data As DataTable)

        Using reader As XmlReader = XmlReader.Create(input)


            Dim row As DataRow = Nothing
            Dim columnIndex As Integer = 0
            Dim type As String
            Dim style As Integer
            Dim value As Integer



            While reader.Read()

                If reader.NodeType = XmlNodeType.Element Then

                    Select Case reader.Name

                        Case "row"

                            'añadiendo fila
                            row = data.NewRow
                            data.Rows.Add(row)
                            columnIndex = 0
                        Case "c"

                            'evaluando el tipo de valor
                            type = reader.GetAttribute("t")
                            style = reader.GetAttribute("s")
                            'reader.Read()
                        Case "v"


                            Dim g As String = reader.ReadElementString()
                            If Not String.IsNullOrEmpty(g.Trim()) Then

                                If IsNumeric(g) Then
                                    value = CInt(g) 'CInt(reader.ReadElementString())

                                    If type = "s" Then
                                        row(columnIndex) = stringTable(value)
                                    Else

                                        row(columnIndex) = value
                                    End If
                                    'Obteniendo el estilo de fecha 
                                    If styleTable(style) >= 14 AndAlso styleTable(style) <= 22 Then
                                        'row(columnIndex) = DateTime.Parse("1900-01-01").AddDays(Integer.Parse(value) - 2).ToString("yyyy-MM-dd")
                                        row(columnIndex) = DateTime.Parse("01/01/1900").AddDays(Integer.Parse(value) - 2).ToString("dd/MM/yyyy")
                                    End If

                                End If
                            End If




                            columnIndex += 1
                    End Select

                End If
            End While

        End Using

    End Sub

End Class
