Public Class CometarioCantForm

    Private Sub CometarioCantForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If CDbl(txtCant.Text) >= vCant1 Then
            MessageBox.Show("ERROR!! Digite cant. menor a: " & vCant1, nomNegocio, Nothing, MessageBoxIcon.Error)
            txtCant.Focus()
            txtCant.SelectAll()
            Exit Sub
        End If

        If validaCampoVacioMinCaracNoNumer(txtObs.Text.Trim(), 3) Then
            MessageBox.Show("Digite comentario valido!!", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtObs.Focus()
            txtObs.SelectAll()
            Exit Sub
        End If

        vObs = txtObs.Text
        vCant = txtCant.Text.Trim()
        Me.Close()
    End Sub

    Private Sub txtCant_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCant.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then  'te deja escribir digitos
            e.Handled = False
        Else
            If e.KeyChar.IsControl(e.KeyChar) Then  'te deja escribir enter, backSpace (controles)
                e.Handled = False
            Else
                If e.KeyChar = "." Then   'te deja escribir punto
                    e.Handled = False
                Else    'lo demas no te deja escribir ASNOOOO
                    e.Handled = True
                End If
            End If
        End If
    End Sub
End Class