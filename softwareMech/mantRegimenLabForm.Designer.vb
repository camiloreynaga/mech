<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mantRegimenLabForm
    Inherits ComponentesSolucion2008.plantillaForm1

    'Form invalida a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mantRegimenLabForm))
        Me.lbReg = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCan1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtCan2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnNuevo = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.TextBoxSSP1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.lbDia = New System.Windows.Forms.ListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(313, 23)
        Me.lblTitulo.Text = "Configuración de Regimen Laboral"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 506)
        '
        'lbReg
        '
        Me.lbReg.FormattingEnabled = True
        Me.lbReg.Location = New System.Drawing.Point(16, 35)
        Me.lbReg.Name = "lbReg"
        Me.lbReg.Size = New System.Drawing.Size(111, 134)
        Me.lbReg.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 310
        Me.Label1.Text = "Regimen Lab."
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(54, 43)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 309
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(152, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 311
        Me.Label2.Text = "Dia Lab.:"
        '
        'txtCan1
        '
        Me.txtCan1.Location = New System.Drawing.Point(217, 32)
        Me.txtCan1.MaxLength = 2
        Me.txtCan1.Name = "txtCan1"
        Me.txtCan1.Size = New System.Drawing.Size(32, 20)
        Me.txtCan1.TabIndex = 1
        Me.txtCan1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCan2
        '
        Me.txtCan2.Location = New System.Drawing.Point(217, 58)
        Me.txtCan2.MaxLength = 2
        Me.txtCan2.Name = "txtCan2"
        Me.txtCan2.Size = New System.Drawing.Size(32, 20)
        Me.txtCan2.TabIndex = 2
        Me.txtCan2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(152, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 313
        Me.Label3.Text = "Dia Des.:"
        '
        'btnNuevo
        '
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(135, 146)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(76, 23)
        Me.btnNuevo.TabIndex = 3
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(222, 146)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(76, 23)
        Me.btnEliminar.TabIndex = 4
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'TextBoxSSP1
        '
        Me.TextBoxSSP1.Location = New System.Drawing.Point(-50, 200)
        Me.TextBoxSSP1.Name = "TextBoxSSP1"
        Me.TextBoxSSP1.ReadOnly = True
        Me.TextBoxSSP1.Size = New System.Drawing.Size(52, 20)
        Me.TextBoxSSP1.TabIndex = 315
        Me.TextBoxSSP1.Text = "1"
        Me.TextBoxSSP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbDia
        '
        Me.lbDia.FormattingEnabled = True
        Me.lbDia.Location = New System.Drawing.Point(16, 185)
        Me.lbDia.Name = "lbDia"
        Me.lbDia.Size = New System.Drawing.Size(111, 342)
        Me.lbDia.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 172)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 319
        Me.Label4.Text = "Dias Generados"
        '
        'mantRegimenLabForm
        '
        Me.AcceptButton = Me.btnNuevo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(313, 551)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbDia)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.TextBoxSSP1)
        Me.Controls.Add(Me.txtCan2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCan1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbReg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCerrar)
        Me.Name = "mantRegimenLabForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.btnCerrar, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.lbReg, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtCan1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtCan2, 0)
        Me.Controls.SetChildIndex(Me.TextBoxSSP1, 0)
        Me.Controls.SetChildIndex(Me.btnEliminar, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.lbDia, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbReg As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCan1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtCan2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnNuevo As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents TextBoxSSP1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents lbDia As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
