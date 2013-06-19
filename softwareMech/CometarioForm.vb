Public Class CometarioForm

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        'If validaCampoVacioMinCaracNoNumer(txtObs.Text.Trim(), 3) Then
        '    MessageBox.Show("Digite comentario valido!!", nomNegocio, Nothing, MessageBoxIcon.Error)
        '    txtObs.Focus()
        '    txtObs.SelectAll()
        '    Exit Sub
        'End If
        vObs = txtObs.Text
        Me.Close()
    End Sub

End Class