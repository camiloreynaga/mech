Imports ICSharpCode.SharpZipLib.Zip
Imports System.Xml
Imports System.Text
Imports System.IO
Imports System.Data




Public Class ImportXlsxForm

    Dim oImport As New cImportXlsx


    Dim tempDir As String = Path.GetTempPath()



    '  Private ReadOnly data As New DataTable(Settings.[Default].DataTableName, Settings.[Default].DataTableNamespace)


#Region "Métodos"

    Private Function createDataTable(ByVal nroColumnas As Integer) As DataTable

        Dim table As New DataTable

        For i As Integer = 0 To nroColumnas - 1
            table.Columns.Add(New DataColumn())
        Next

        'table.Columns.Add(New DataColumn("id"))
        'table.Columns.Add(New DataColumn("name"))
        'table.Columns.Add(New DataColumn())

        Return table


    End Function


    Private Function showOpenFileDialog() As String

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Excel (*.xlsx)|"
        'openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Return openFileDialog1.FileName
        Else
            Return ""

            'Try
            '    myStream = openFileDialog1.OpenFile()
            '    If (myStream IsNot Nothing) Then
            '        ' Insert code to read the stream here.
            '    End If
            'Catch Ex As Exception
            '    MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            'Finally
            '    ' Check this again, since we need to make sure we didn't throw an exception on open.
            '    If (myStream IsNot Nothing) Then
            '        myStream.Close()
            '    End If
            'End Try
        End If


    End Function


#End Region

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click

        Dim fileName As String = showOpenFileDialog()

        If String.IsNullOrEmpty(fileName) Then
            Exit Sub
        End If

        Dim table As DataTable ' = createDataTable()


        'oImport.DeleteDirectoryContents(tempDir)

        oImport.unzipFile(fileName, tempDir)

        Dim stringTable As IList(Of String)
        ' Open XML file with table of all unique strings used in the workbook..
        Using stream = New FileStream(Path.Combine(tempDir, "xl\sharedStrings.xml"), FileMode.Open, FileAccess.Read)
            ' ..and call helper method that parses that XML and returns an array of strings.
            stringTable = oImport.readStringTable(stream)
        End Using

        ' Open XML file with worksheet data..
        Using stream = New FileStream(Path.Combine(tempDir, "xl\worksheets\sheet1.xml"), FileMode.Open, FileAccess.Read)
            ' ..and call helper method that parses that XML and fills DataTable with values.
            'oImport.readWorkSheet(stream, stringTable, createDataTable())
            table = createDataTable(oImport.nroColumnas(stream).Max())

        End Using



        Using stream = New FileStream(Path.Combine(tempDir, "xl\worksheets\sheet1.xml"), FileMode.Open, FileAccess.Read)
            ' ..and call helper method that parses that XML and fills DataTable with values.
            'oImport.readWorkSheet(stream, stringTable, createDataTable())
            oImport.readWorkSheet(stream, stringTable, table)
        End Using


        dgv.DataSource = table

    End Sub
End Class