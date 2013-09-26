Public Class cReporting

    ''' <summary>
    ''' Llena los datos de la grilla en un dataTable del dataSet
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarDatos(ByVal grilla As DataGridView) As DataSetInformesCr
        Dim ds As New DataSetInformesCr

        For Each row As DataGridViewRow In grilla.Rows

            'Dim fila As DataSetInformesCr.  

            Dim rowInf As DataSetInformesCr.DatosAprobacionDesemRow = ds.DatosAprobacionDesem.NewDatosAprobacionDesemRow
            ' rowInf. = CDate(row.Cells("fecDes").Value)
            rowInf.simbolo = CStr(row.Cells("simbolo").Value)
            rowInf.monto = CDbl(row.Cells("monto").Value)
            rowInf.nombre = CStr(row.Cells("nombre").Value)
            rowInf.razon = CStr(row.Cells("razon").Value)
            rowInf.nro = CStr(row.Cells("nro").Value)
            If IsDBNull(row.Cells("nom").Value) Then
                rowInf.nom = ""
            Else
                rowInf.nom = CStr(row.Cells("nom").Value)
            End If

            rowInf.serie = CStr(row.Cells("serie").Value)
            If IsDBNull(row.Cells("estApro").Value) Then
                rowInf.estApro = "PENDIENTE"
            Else
                rowInf.estApro = CStr(row.Cells("estApro").Value)
            End If


            ds.DatosAprobacionDesem.AddDatosAprobacionDesemRow(rowInf)

        Next

        Return ds

    End Function

End Class
