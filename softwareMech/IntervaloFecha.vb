Public Class IntervaloFecha

 
  
    Private Sub dtpInicio_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpInicio.ValueChanged

        'Validando el intervalo de fechas
        If dtpInicio.Value > dtpFin.Value Then

            'si es mayor, iguala el valor con dtpFin
            dtpInicio.Value = dtpFin.Value

        End If


    End Sub
End Class
