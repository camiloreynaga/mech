<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReporteTestCR
    Inherits ComponentesSolucion2008.plantillaForm1
    ' System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.IntervaloFecha1 = New softwareMech.IntervaloFecha
        Me.SuspendLayout()
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 515)
        '
        'IntervaloFecha1
        '
        Me.IntervaloFecha1.Location = New System.Drawing.Point(20, 26)
        Me.IntervaloFecha1.Name = "IntervaloFecha1"
        Me.IntervaloFecha1.Size = New System.Drawing.Size(307, 31)
        Me.IntervaloFecha1.TabIndex = 3
        '
        'ReporteTestCR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 560)
        Me.Controls.Add(Me.IntervaloFecha1)
        Me.Name = "ReporteTestCR"
        Me.Text = "ReporteTestCR"
        Me.Controls.SetChildIndex(Me.IntervaloFecha1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IntervaloFecha1 As softwareMech.IntervaloFecha
End Class
