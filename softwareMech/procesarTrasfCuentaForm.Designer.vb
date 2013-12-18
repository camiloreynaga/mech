<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class procesarTrasfCuentaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(procesarTrasfCuentaForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtMon2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.lblMon4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSal2 = New System.Windows.Forms.TextBox
        Me.lblMon3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbCuenta2 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnVis = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnPro = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.txtNota = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtMon1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.lblMon2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSal1 = New System.Windows.Forms.TextBox
        Me.lblMon1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbCuenta1 = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.date1 = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(1000, 23)
        Me.lblTitulo.Text = "Procesar Transferencia de Dinero Entre Cuentas Bancarias Mech"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtMon2)
        Me.Panel1.Controls.Add(Me.lblMon4)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtSal2)
        Me.Panel1.Controls.Add(Me.lblMon3)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cbCuenta2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.btnVis)
        Me.Panel1.Controls.Add(Me.btnPro)
        Me.Panel1.Controls.Add(Me.txtNota)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtMon1)
        Me.Panel1.Controls.Add(Me.lblMon2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtSal1)
        Me.Panel1.Controls.Add(Me.lblMon1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cbCuenta1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.date1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(985, 115)
        Me.Panel1.TabIndex = 14
        '
        'txtMon2
        '
        Me.txtMon2.Location = New System.Drawing.Point(548, 61)
        Me.txtMon2.Name = "txtMon2"
        Me.txtMon2.Size = New System.Drawing.Size(99, 20)
        Me.txtMon2.TabIndex = 358
        Me.txtMon2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMon4
        '
        Me.lblMon4.AutoSize = True
        Me.lblMon4.Location = New System.Drawing.Point(521, 64)
        Me.lblMon4.Name = "lblMon4"
        Me.lblMon4.Size = New System.Drawing.Size(14, 13)
        Me.lblMon4.TabIndex = 357
        Me.lblMon4.Text = "$"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(544, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 13)
        Me.Label7.TabIndex = 356
        Me.Label7.Text = "Monto a Ingresar"
        '
        'txtSal2
        '
        Me.txtSal2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSal2.Location = New System.Drawing.Point(407, 61)
        Me.txtSal2.Name = "txtSal2"
        Me.txtSal2.ReadOnly = True
        Me.txtSal2.Size = New System.Drawing.Size(99, 22)
        Me.txtSal2.TabIndex = 354
        Me.txtSal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMon3
        '
        Me.lblMon3.AutoSize = True
        Me.lblMon3.Location = New System.Drawing.Point(379, 64)
        Me.lblMon3.Name = "lblMon3"
        Me.lblMon3.Size = New System.Drawing.Size(14, 13)
        Me.lblMon3.TabIndex = 355
        Me.lblMon3.Text = "$"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(404, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 13)
        Me.Label8.TabIndex = 353
        Me.Label8.Text = "Saldo Cuenta"
        '
        'cbCuenta2
        '
        Me.cbCuenta2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCuenta2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCuenta2.FormattingEnabled = True
        Me.cbCuenta2.Location = New System.Drawing.Point(122, 62)
        Me.cbCuenta2.Name = "cbCuenta2"
        Me.cbCuenta2.Size = New System.Drawing.Size(246, 21)
        Me.cbCuenta2.TabIndex = 352
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(119, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 351
        Me.Label1.Text = "Cuenta Destino:"
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(864, 5)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 350
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'btnVis
        '
        Me.btnVis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVis.Image = CType(resources.GetObject("btnVis.Image"), System.Drawing.Image)
        Me.btnVis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVis.Location = New System.Drawing.Point(719, 84)
        Me.btnVis.Name = "btnVis"
        Me.btnVis.Size = New System.Drawing.Size(122, 23)
        Me.btnVis.TabIndex = 349
        Me.btnVis.TabStop = False
        Me.btnVis.Text = "Ver Movimientos"
        Me.btnVis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnVis.UseVisualStyleBackColor = True
        '
        'btnPro
        '
        Me.btnPro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPro.Image = CType(resources.GetObject("btnPro.Image"), System.Drawing.Image)
        Me.btnPro.Location = New System.Drawing.Point(671, 81)
        Me.btnPro.Name = "btnPro"
        Me.btnPro.Size = New System.Drawing.Size(29, 28)
        Me.btnPro.TabIndex = 348
        Me.btnPro.TabStop = False
        Me.btnPro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPro.UseVisualStyleBackColor = True
        '
        'txtNota
        '
        Me.txtNota.Location = New System.Drawing.Point(40, 89)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(607, 20)
        Me.txtNota.TabIndex = 347
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 346
        Me.Label6.Text = "Nota:"
        '
        'txtMon1
        '
        Me.txtMon1.Location = New System.Drawing.Point(548, 18)
        Me.txtMon1.Name = "txtMon1"
        Me.txtMon1.Size = New System.Drawing.Size(99, 20)
        Me.txtMon1.TabIndex = 345
        Me.txtMon1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMon2
        '
        Me.lblMon2.AutoSize = True
        Me.lblMon2.Location = New System.Drawing.Point(521, 23)
        Me.lblMon2.Name = "lblMon2"
        Me.lblMon2.Size = New System.Drawing.Size(14, 13)
        Me.lblMon2.TabIndex = 344
        Me.lblMon2.Text = "$"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(544, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 343
        Me.Label4.Text = "Monto a Salir"
        '
        'txtSal1
        '
        Me.txtSal1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSal1.Location = New System.Drawing.Point(407, 18)
        Me.txtSal1.Name = "txtSal1"
        Me.txtSal1.ReadOnly = True
        Me.txtSal1.Size = New System.Drawing.Size(99, 22)
        Me.txtSal1.TabIndex = 341
        Me.txtSal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMon1
        '
        Me.lblMon1.AutoSize = True
        Me.lblMon1.Location = New System.Drawing.Point(379, 23)
        Me.lblMon1.Name = "lblMon1"
        Me.lblMon1.Size = New System.Drawing.Size(14, 13)
        Me.lblMon1.TabIndex = 342
        Me.lblMon1.Text = "$"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(404, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 340
        Me.Label5.Text = "Saldo Cuenta"
        '
        'cbCuenta1
        '
        Me.cbCuenta1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCuenta1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCuenta1.FormattingEnabled = True
        Me.cbCuenta1.Location = New System.Drawing.Point(122, 19)
        Me.cbCuenta1.Name = "cbCuenta1"
        Me.cbCuenta1.Size = New System.Drawing.Size(246, 21)
        Me.cbCuenta1.TabIndex = 339
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(119, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 283
        Me.Label3.Text = "Cuenta Origen:"
        '
        'date1
        '
        Me.date1.Enabled = False
        Me.date1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.date1.Location = New System.Drawing.Point(6, 20)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(107, 20)
        Me.date1.TabIndex = 281
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 282
        Me.Label2.Text = "Fecha:"
        '
        'procesarTrasfCuentaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(1000, 675)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "procesarTrasfCuentaForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnVis As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnPro As ComponentesSolucion2008.BottomSSP
    Friend WithEvents txtNota As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMon1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents lblMon2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSal1 As System.Windows.Forms.TextBox
    Friend WithEvents lblMon1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbCuenta1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents date1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents txtMon2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents lblMon4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSal2 As System.Windows.Forms.TextBox
    Friend WithEvents lblMon3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbCuenta2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
