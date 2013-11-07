<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class registraDocVentaEmpForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(registraDocVentaEmpForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnImprimir = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnAnula = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCierra = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.lbDoc = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNro = New System.Windows.Forms.TextBox
        Me.cbSerie = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(915, 23)
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 630)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lbDoc)
        Me.Panel1.Controls.Add(Me.btnImprimir)
        Me.Panel1.Controls.Add(Me.btnAnula)
        Me.Panel1.Controls.Add(Me.btnCierra)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtNro)
        Me.Panel1.Controls.Add(Me.cbSerie)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(116, 306)
        Me.Panel1.TabIndex = 3
        '
        'btnImprimir
        '
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.Location = New System.Drawing.Point(19, 278)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(73, 23)
        Me.btnImprimir.TabIndex = 323
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnAnula
        '
        Me.btnAnula.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAnula.Image = CType(resources.GetObject("btnAnula.Image"), System.Drawing.Image)
        Me.btnAnula.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnula.Location = New System.Drawing.Point(21, 230)
        Me.btnAnula.Name = "btnAnula"
        Me.btnAnula.Size = New System.Drawing.Size(71, 23)
        Me.btnAnula.TabIndex = 322
        Me.btnAnula.TabStop = False
        Me.btnAnula.Text = "Anular."
        Me.btnAnula.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnula.UseVisualStyleBackColor = True
        '
        'btnCierra
        '
        Me.btnCierra.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCierra.Image = CType(resources.GetObject("btnCierra.Image"), System.Drawing.Image)
        Me.btnCierra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCierra.Location = New System.Drawing.Point(19, 253)
        Me.btnCierra.Name = "btnCierra"
        Me.btnCierra.Size = New System.Drawing.Size(73, 24)
        Me.btnCierra.TabIndex = 321
        Me.btnCierra.TabStop = False
        Me.btnCierra.Text = "Cerrar."
        Me.btnCierra.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCierra.UseVisualStyleBackColor = True
        '
        'lbDoc
        '
        Me.lbDoc.FormattingEnabled = True
        Me.lbDoc.Location = New System.Drawing.Point(1, 56)
        Me.lbDoc.Name = "lbDoc"
        Me.lbDoc.Size = New System.Drawing.Size(111, 173)
        Me.lbDoc.TabIndex = 306
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 307
        Me.Label3.Text = "NºDocVenta:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(62, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Nº"
        '
        'txtNro
        '
        Me.txtNro.Location = New System.Drawing.Point(62, 17)
        Me.txtNro.Name = "txtNro"
        Me.txtNro.ReadOnly = True
        Me.txtNro.Size = New System.Drawing.Size(41, 20)
        Me.txtNro.TabIndex = 2
        Me.txtNro.TabStop = False
        Me.txtNro.Text = "1000"
        '
        'cbSerie
        '
        Me.cbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSerie.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbSerie.FormattingEnabled = True
        Me.cbSerie.Location = New System.Drawing.Point(6, 16)
        Me.cbSerie.Name = "cbSerie"
        Me.cbSerie.Size = New System.Drawing.Size(50, 21)
        Me.cbSerie.TabIndex = 1
        Me.cbSerie.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Serie:"
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(39, 64)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 21)
        Me.btnCerrar.TabIndex = 276
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'registraDocVentaEmpForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(915, 675)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "registraDocVentaEmpForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnImprimir As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnAnula As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCierra As ComponentesSolucion2008.BottomSSP
    Friend WithEvents lbDoc As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNro As System.Windows.Forms.TextBox
    Friend WithEvents cbSerie As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button

End Class
