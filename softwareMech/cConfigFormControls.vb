

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



End Class
