<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantCuentas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantCuentas))
        Me.cboMoneda = New System.Windows.Forms.ComboBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnEliminarBco = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarBco = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevoBco = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.dgBancos = New System.Windows.Forms.DataGridView
        Me.txtBanco = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dgCuentas = New System.Windows.Forms.DataGridView
        Me.txtCuenta = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnNuevoCta = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarCta = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarCta = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.lsEstado = New System.Windows.Forms.ListBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.dgMedios = New System.Windows.Forms.DataGridView
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtMedioPago = New System.Windows.Forms.TextBox
        Me.btnNuevoMedio = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarMedio = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarMedio = New ComponentesSolucion2008.BottomSSP(Me.components)
        CType(Me.dgBancos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgMedios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(910, 23)
        Me.lblTitulo.Text = "Mantenimientos de Cuentas"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 375)
        '
        'cboMoneda
        '
        Me.cboMoneda.FormattingEnabled = True
        Me.cboMoneda.Location = New System.Drawing.Point(202, 18)
        Me.cboMoneda.Name = "cboMoneda"
        Me.cboMoneda.Size = New System.Drawing.Size(121, 21)
        Me.cboMoneda.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(183, 68)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(84, 23)
        Me.btnCerrar.TabIndex = 5
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'btnEliminarBco
        '
        Me.btnEliminarBco.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarBco.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarBco.Image = CType(resources.GetObject("btnEliminarBco.Image"), System.Drawing.Image)
        Me.btnEliminarBco.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarBco.Location = New System.Drawing.Point(183, 44)
        Me.btnEliminarBco.Name = "btnEliminarBco"
        Me.btnEliminarBco.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarBco.TabIndex = 3
        Me.btnEliminarBco.Text = "Eliminar"
        Me.btnEliminarBco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarBco.UseVisualStyleBackColor = False
        '
        'btnModificarBco
        '
        Me.btnModificarBco.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificarBco.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarBco.Image = CType(resources.GetObject("btnModificarBco.Image"), System.Drawing.Image)
        Me.btnModificarBco.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarBco.Location = New System.Drawing.Point(93, 44)
        Me.btnModificarBco.Name = "btnModificarBco"
        Me.btnModificarBco.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarBco.TabIndex = 2
        Me.btnModificarBco.Text = "Actualizar"
        Me.btnModificarBco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarBco.UseVisualStyleBackColor = False
        '
        'btnNuevoBco
        '
        Me.btnNuevoBco.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevoBco.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoBco.Image = CType(resources.GetObject("btnNuevoBco.Image"), System.Drawing.Image)
        Me.btnNuevoBco.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoBco.Location = New System.Drawing.Point(3, 44)
        Me.btnNuevoBco.Name = "btnNuevoBco"
        Me.btnNuevoBco.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoBco.TabIndex = 1
        Me.btnNuevoBco.Text = "Guardar"
        Me.btnNuevoBco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoBco.UseVisualStyleBackColor = False
        '
        'dgBancos
        '
        Me.dgBancos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBancos.Location = New System.Drawing.Point(3, 96)
        Me.dgBancos.Name = "dgBancos"
        Me.dgBancos.Size = New System.Drawing.Size(264, 263)
        Me.dgBancos.TabIndex = 30
        '
        'txtBanco
        '
        Me.txtBanco.Location = New System.Drawing.Point(58, 18)
        Me.txtBanco.Name = "txtBanco"
        Me.txtBanco.Size = New System.Drawing.Size(209, 20)
        Me.txtBanco.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Banco:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "lista de Bancos:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(199, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Moneda:"
        '
        'dgCuentas
        '
        Me.dgCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCuentas.Location = New System.Drawing.Point(6, 65)
        Me.dgCuentas.Name = "dgCuentas"
        Me.dgCuentas.Size = New System.Drawing.Size(599, 79)
        Me.dgCuentas.TabIndex = 30
        '
        'txtCuenta
        '
        Me.txtCuenta.Location = New System.Drawing.Point(6, 18)
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(190, 20)
        Me.txtCuenta.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Número de Cuenta:"
        '
        'btnNuevoCta
        '
        Me.btnNuevoCta.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevoCta.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoCta.Image = CType(resources.GetObject("btnNuevoCta.Image"), System.Drawing.Image)
        Me.btnNuevoCta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoCta.Location = New System.Drawing.Point(341, 39)
        Me.btnNuevoCta.Name = "btnNuevoCta"
        Me.btnNuevoCta.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoCta.TabIndex = 2
        Me.btnNuevoCta.Text = "Guardar"
        Me.btnNuevoCta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoCta.UseVisualStyleBackColor = False
        '
        'btnModificarCta
        '
        Me.btnModificarCta.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificarCta.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarCta.Image = CType(resources.GetObject("btnModificarCta.Image"), System.Drawing.Image)
        Me.btnModificarCta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarCta.Location = New System.Drawing.Point(431, 39)
        Me.btnModificarCta.Name = "btnModificarCta"
        Me.btnModificarCta.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarCta.TabIndex = 3
        Me.btnModificarCta.Text = "Actualizar"
        Me.btnModificarCta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarCta.UseVisualStyleBackColor = False
        '
        'btnEliminarCta
        '
        Me.btnEliminarCta.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarCta.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarCta.Image = CType(resources.GetObject("btnEliminarCta.Image"), System.Drawing.Image)
        Me.btnEliminarCta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarCta.Location = New System.Drawing.Point(521, 39)
        Me.btnEliminarCta.Name = "btnEliminarCta"
        Me.btnEliminarCta.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarCta.TabIndex = 4
        Me.btnEliminarCta.Text = "Eliminar"
        Me.btnEliminarCta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarCta.UseVisualStyleBackColor = False
        '
        'lsEstado
        '
        Me.lsEstado.FormattingEnabled = True
        Me.lsEstado.Items.AddRange(New Object() {"Activo", "Inactivo"})
        Me.lsEstado.Location = New System.Drawing.Point(382, 5)
        Me.lsEstado.Name = "lsEstado"
        Me.lsEstado.Size = New System.Drawing.Size(69, 30)
        Me.lsEstado.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(326, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Estado:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "lista de Cuentas:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.txtBanco)
        Me.Panel1.Controls.Add(Me.dgBancos)
        Me.Panel1.Controls.Add(Me.btnEliminarBco)
        Me.Panel1.Controls.Add(Me.btnModificarBco)
        Me.Panel1.Controls.Add(Me.btnNuevoBco)
        Me.Panel1.Location = New System.Drawing.Point(17, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(275, 362)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lsEstado)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.btnNuevoCta)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.btnModificarCta)
        Me.Panel2.Controls.Add(Me.txtCuenta)
        Me.Panel2.Controls.Add(Me.btnEliminarCta)
        Me.Panel2.Controls.Add(Me.dgCuentas)
        Me.Panel2.Controls.Add(Me.cboMoneda)
        Me.Panel2.Location = New System.Drawing.Point(296, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(611, 148)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgMedios)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.txtMedioPago)
        Me.Panel3.Controls.Add(Me.btnNuevoMedio)
        Me.Panel3.Controls.Add(Me.btnModificarMedio)
        Me.Panel3.Controls.Add(Me.btnEliminarMedio)
        Me.Panel3.Location = New System.Drawing.Point(296, 180)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(611, 205)
        Me.Panel3.TabIndex = 3
        '
        'dgMedios
        '
        Me.dgMedios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMedios.Location = New System.Drawing.Point(6, 51)
        Me.dgMedios.Name = "dgMedios"
        Me.dgMedios.Size = New System.Drawing.Size(599, 150)
        Me.dgMedios.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Medio de pago:"
        '
        'txtMedioPago
        '
        Me.txtMedioPago.Location = New System.Drawing.Point(6, 25)
        Me.txtMedioPago.Name = "txtMedioPago"
        Me.txtMedioPago.Size = New System.Drawing.Size(329, 20)
        Me.txtMedioPago.TabIndex = 0
        '
        'btnNuevoMedio
        '
        Me.btnNuevoMedio.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevoMedio.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoMedio.Image = CType(resources.GetObject("btnNuevoMedio.Image"), System.Drawing.Image)
        Me.btnNuevoMedio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoMedio.Location = New System.Drawing.Point(341, 23)
        Me.btnNuevoMedio.Name = "btnNuevoMedio"
        Me.btnNuevoMedio.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoMedio.TabIndex = 2
        Me.btnNuevoMedio.Text = "Guardar"
        Me.btnNuevoMedio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoMedio.UseVisualStyleBackColor = False
        '
        'btnModificarMedio
        '
        Me.btnModificarMedio.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificarMedio.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarMedio.Image = CType(resources.GetObject("btnModificarMedio.Image"), System.Drawing.Image)
        Me.btnModificarMedio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarMedio.Location = New System.Drawing.Point(431, 23)
        Me.btnModificarMedio.Name = "btnModificarMedio"
        Me.btnModificarMedio.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarMedio.TabIndex = 3
        Me.btnModificarMedio.Text = "Actualizar"
        Me.btnModificarMedio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarMedio.UseVisualStyleBackColor = False
        '
        'btnEliminarMedio
        '
        Me.btnEliminarMedio.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarMedio.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarMedio.Image = CType(resources.GetObject("btnEliminarMedio.Image"), System.Drawing.Image)
        Me.btnEliminarMedio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarMedio.Location = New System.Drawing.Point(521, 23)
        Me.btnEliminarMedio.Name = "btnEliminarMedio"
        Me.btnEliminarMedio.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarMedio.TabIndex = 4
        Me.btnEliminarMedio.Text = "Eliminar"
        Me.btnEliminarMedio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarMedio.UseVisualStyleBackColor = False
        '
        'MantCuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(910, 420)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "MantCuentas"
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        CType(Me.dgBancos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgMedios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnEliminarBco As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarBco As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevoBco As ComponentesSolucion2008.BottomSSP
    Friend WithEvents dgBancos As System.Windows.Forms.DataGridView
    Friend WithEvents txtBanco As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgCuentas As System.Windows.Forms.DataGridView
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoCta As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarCta As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarCta As ComponentesSolucion2008.BottomSSP
    Friend WithEvents lsEstado As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtMedioPago As System.Windows.Forms.TextBox
    Friend WithEvents dgMedios As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoMedio As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarMedio As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarMedio As ComponentesSolucion2008.BottomSSP

End Class
