<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntervaloFecha
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.dtpInicio = New System.Windows.Forms.DateTimePicker
        Me.dtpFin = New System.Windows.Forms.DateTimePicker
        Me.lblAl = New System.Windows.Forms.Label
        Me.lblDel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'dtpInicio
        '
        Me.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInicio.Location = New System.Drawing.Point(35, 4)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(96, 20)
        Me.dtpInicio.TabIndex = 0
        '
        'dtpFin
        '
        Me.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFin.Location = New System.Drawing.Point(162, 4)
        Me.dtpFin.Name = "dtpFin"
        Me.dtpFin.Size = New System.Drawing.Size(96, 20)
        Me.dtpFin.TabIndex = 1
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(137, 9)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(19, 13)
        Me.lblAl.TabIndex = 2
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(3, 9)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(26, 13)
        Me.lblDel.TabIndex = 2
        Me.lblDel.Text = "Del:"
        '
        'IntervaloFecha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblDel)
        Me.Controls.Add(Me.lblAl)
        Me.Controls.Add(Me.dtpFin)
        Me.Controls.Add(Me.dtpInicio)
        Me.Name = "IntervaloFecha"
        Me.Size = New System.Drawing.Size(263, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents dtpInicio As System.Windows.Forms.DateTimePicker
    Public WithEvents dtpFin As System.Windows.Forms.DateTimePicker
    Public WithEvents lblAl As System.Windows.Forms.Label
    Public WithEvents lblDel As System.Windows.Forms.Label

End Class
