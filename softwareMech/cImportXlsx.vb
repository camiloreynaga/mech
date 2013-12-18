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

    Public Sub unzipFile(ByVal zipFileName As String, ByVal directoryTarget As String)

        Dim z As FastZip = New FastZip()
        z.ExtractZip(zipFileName, directoryTarget, "")

    End Sub

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

    Private Sub createColumn(ByVal data As DataTable)
        data.Columns.Add(New DataColumn())


    End Sub

    Public Sub readWorkSheet(ByVal input As Stream, ByVal stringTable As IList(Of String), ByVal data As DataTable)
        Using reader As XmlReader = XmlReader.Create(input)

            Dim columns As New List(Of String)
            Dim nroCol As Integer = 0
            Dim recuentoCol As Integer = 0


            Dim row As DataRow = Nothing
            Dim columnIndex As Integer = 0
            Dim type As String
            Dim value As Integer

            While reader.Read()

             

                'XmlNodeType.

                If reader.NodeType = XmlNodeType.Element Then

                    'reader.na()

                    'data.Columns.Add( 


                    Select Case reader.Name

                        Case "row"
                            nroCol = reader.GetAttribute("r")


                            
                            columnIndex = 0
                        Case "c"

                            If nroCol = 1 Then
                                createColumn(data)

                            Else
                                If recuentoCol < columnIndex Then
                                    createColumn(data)
                                End If

                            End If

                            If columnIndex = 0 Then
                                'añadiendo fila
                                row = data.NewRow
                                data.Rows.Add(row)
                            End If

                            'evaluando el tipo de valor
                            type = reader.GetAttribute("t")
                            reader.Read()

                            value = CInt(reader.ReadElementString())

                            If type = "s" Then
                                row(columnIndex) = stringTable(value)
                            Else
                                row(columnIndex) = value
                            End If
                            columnIndex += 1
                            'obteniendo la última cantidad de columnas
                            recuentoCol = columnIndex
                    End Select

                End If
            End While

        End Using

    End Sub

End Class
