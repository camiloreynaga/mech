<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambiarContraseñaUsuForm
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtCon2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtCon1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtUsu1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtCon = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtUsu = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnAceptar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(349, 23)
        Me.lblTitulo.Text = "Cambiar contraseña de usuario"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 190)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtCon2)
        Me.Panel1.Controls.Add(Me.txtCon1)
        Me.Panel1.Controls.Add(Me.txtUsu1)
        Me.Panel1.Controls.Add(Me.txtCon)
        Me.Panel1.Controls.Add(Me.txtUsu)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(326, 145)
        Me.Panel1.TabIndex = 6
        '
        'txtCon2
        '
        Me.txtCon2.Location = New System.Drawing.Point(128, 113)
        Me.txtCon2.Name = "txtCon2"
        Me.txtCon2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(120)
        Me.txtCon2.Size = New System.Drawing.Size(177, 20)
        Me.txtCon2.TabIndex = 14
        '
        'txtCon1
        '
        Me.txtCon1.Location = New System.Drawing.Point(129, 87)
        Me.txtCon1.Name = "txtCon1"
        Me.txtCon1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(120)
        Me.txtCon1.Size = New System.Drawing.Size(177, 20)
        Me.txtCon1.TabIndex = 13
        '
        'txtUsu1
        '
        Me.txtUsu1.Location = New System.Drawing.Point(128, 59)
        Me.txtUsu1.Name = "txtUsu1"
        Me.txtUsu1.Size = New System.Drawing.Size(177, 20)
        Me.txtUsu1.TabIndex = 12
        '
        'txtCon
        '
        Me.txtCon.Location = New System.Drawing.Point(128, 33)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.PasswordChar = Global.Microsoft.VisualBasic.ChrW(120)
        Me.txtCon.Size = New System.Drawing.Size(177, 20)
        Me.txtCon.TabIndex = 11
        '
        'txtUsu
        '
        Me.txtUsu.Location = New System.Drawing.Point(128, 6)
        Me.txtUsu.Name = "txtUsu"
        Me.txtUsu.ReadOnly = True
        Me.txtUsu.Size = New System.Drawing.Size(177, 20)
        Me.txtUsu.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Repita contraseña :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Nueva contraseña :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Nombre usuario :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Contraseña anterior:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Nombre usuario :"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnAceptar)
        Me.Panel2.Location = New System.Drawing.Point(14, 170)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(326, 41)
        Me.Panel2.TabIndex = 7
        '
        'btnCancelar
        '
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(215, 8)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(90, 23)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAceptar.Location = New System.Drawing.Point(104, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(90, 23)
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'CambiarContraseñaUsuForm
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(349, 235)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "CambiarContraseñaUsuForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUsu As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtCon2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtCon1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtUsu1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtCon As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAceptar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar As System.Windows.Forms.Button

End Class
