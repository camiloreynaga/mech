<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class crearMaterialForm
    Inherits ComponentesSolucion2008.plantillaForm2

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(crearMaterialForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.cbBuscar = New System.Windows.Forms.ComboBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Navigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cbTipo = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtProd = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.cbUni1 = New System.Windows.Forms.ComboBox
        Me.txtPre1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnEliminar2 = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelar2 = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar2 = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevo2 = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.lbEstado = New System.Windows.Forms.ListBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(769, 23)
        Me.lblTitulo.Text = "Mantenimiento de Insumos"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 523)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.dgTabla1)
        Me.Panel1.Controls.Add(Me.txtBuscar)
        Me.Panel1.Controls.Add(Me.cbBuscar)
        Me.Panel1.Controls.Add(Me.btnCerrar)
        Me.Panel1.Controls.Add(Me.Navigator1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(749, 433)
        Me.Panel1.TabIndex = 1
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(0, 26)
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTabla1.Size = New System.Drawing.Size(747, 380)
        Me.dgTabla1.TabIndex = 1
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(233, 3)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(182, 20)
        Me.txtBuscar.TabIndex = 2
        '
        'cbBuscar
        '
        Me.cbBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbBuscar.FormattingEnabled = True
        Me.cbBuscar.Items.AddRange(New Object() {"INSUMO", "CODIGO"})
        Me.cbBuscar.Location = New System.Drawing.Point(145, 2)
        Me.cbBuscar.Name = "cbBuscar"
        Me.cbBuscar.Size = New System.Drawing.Size(81, 21)
        Me.cbBuscar.TabIndex = 1
        Me.cbBuscar.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(306, 180)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 13)
        Me.btnCerrar.TabIndex = 172
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.CountItem = Me.ToolStripLabel1
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator3})
        Me.Navigator1.Location = New System.Drawing.Point(0, 406)
        Me.Navigator1.MoveFirstItem = Me.ToolStripButton1
        Me.Navigator1.MoveLastItem = Me.ToolStripButton4
        Me.Navigator1.MoveNextItem = Me.ToolStripButton3
        Me.Navigator1.MovePreviousItem = Me.ToolStripButton2
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.ToolStripTextBox1
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(747, 25)
        Me.Navigator1.TabIndex = 171
        Me.Navigator1.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel1.Text = "de {0}"
        Me.ToolStripLabel1.ToolTipText = "Número total de elementos"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton1.Size = New System.Drawing.Size(106, 22)
        Me.ToolStripButton1.Text = "Mover primero"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(105, 22)
        Me.ToolStripButton2.Text = "Mover anterior"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Posición"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.ReadOnly = True
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(50, 23)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolStripTextBox1.ToolTipText = "Posición actual"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripButton3.Text = "Mover siguiente"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripButton4.Text = "Mover último"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Seleccione Insumo:"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cbTipo)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtProd)
        Me.Panel2.Controls.Add(Me.cbUni1)
        Me.Panel2.Controls.Add(Me.txtPre1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.btnEliminar2)
        Me.Panel2.Controls.Add(Me.btnCancelar2)
        Me.Panel2.Controls.Add(Me.btnModificar2)
        Me.Panel2.Controls.Add(Me.btnNuevo2)
        Me.Panel2.Controls.Add(Me.lbEstado)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(14, 456)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(748, 89)
        Me.Panel2.TabIndex = 2
        '
        'cbTipo
        '
        Me.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbTipo.FormattingEnabled = True
        Me.cbTipo.Location = New System.Drawing.Point(5, 15)
        Me.cbTipo.Name = "cbTipo"
        Me.cbTipo.Size = New System.Drawing.Size(156, 21)
        Me.cbTipo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(147, 13)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "Seleccione Tipo Insumo:"
        '
        'txtProd
        '
        Me.txtProd.Location = New System.Drawing.Point(167, 17)
        Me.txtProd.Name = "txtProd"
        Me.txtProd.ReadOnly = True
        Me.txtProd.Size = New System.Drawing.Size(363, 20)
        Me.txtProd.TabIndex = 2
        '
        'cbUni1
        '
        Me.cbUni1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUni1.Enabled = False
        Me.cbUni1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbUni1.FormattingEnabled = True
        Me.cbUni1.Location = New System.Drawing.Point(548, 16)
        Me.cbUni1.Name = "cbUni1"
        Me.cbUni1.Size = New System.Drawing.Size(91, 21)
        Me.cbUni1.TabIndex = 3
        '
        'txtPre1
        '
        Me.txtPre1.Location = New System.Drawing.Point(642, 16)
        Me.txtPre1.Name = "txtPre1"
        Me.txtPre1.ReadOnly = True
        Me.txtPre1.Size = New System.Drawing.Size(52, 20)
        Me.txtPre1.TabIndex = 4
        Me.txtPre1.Text = "0"
        Me.txtPre1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(639, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Precio S/."
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(378, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 13)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnEliminar2
        '
        Me.btnEliminar2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar2.Image = CType(resources.GetObject("btnEliminar2.Image"), System.Drawing.Image)
        Me.btnEliminar2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar2.Location = New System.Drawing.Point(642, 52)
        Me.btnEliminar2.Name = "btnEliminar2"
        Me.btnEliminar2.Size = New System.Drawing.Size(85, 23)
        Me.btnEliminar2.TabIndex = 9
        Me.btnEliminar2.Text = "Eliminar"
        Me.btnEliminar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminar2.UseVisualStyleBackColor = True
        '
        'btnCancelar2
        '
        Me.btnCancelar2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar2.Enabled = False
        Me.btnCancelar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar2.Image = CType(resources.GetObject("btnCancelar2.Image"), System.Drawing.Image)
        Me.btnCancelar2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar2.Location = New System.Drawing.Point(530, 52)
        Me.btnCancelar2.Name = "btnCancelar2"
        Me.btnCancelar2.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelar2.TabIndex = 8
        Me.btnCancelar2.Text = "Cancelar"
        Me.btnCancelar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar2.UseVisualStyleBackColor = True
        '
        'btnModificar2
        '
        Me.btnModificar2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar2.Image = CType(resources.GetObject("btnModificar2.Image"), System.Drawing.Image)
        Me.btnModificar2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar2.Location = New System.Drawing.Point(415, 52)
        Me.btnModificar2.Name = "btnModificar2"
        Me.btnModificar2.Size = New System.Drawing.Size(84, 23)
        Me.btnModificar2.TabIndex = 7
        Me.btnModificar2.Text = "Modificar"
        Me.btnModificar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificar2.UseVisualStyleBackColor = True
        '
        'btnNuevo2
        '
        Me.btnNuevo2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo2.Image = CType(resources.GetObject("btnNuevo2.Image"), System.Drawing.Image)
        Me.btnNuevo2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo2.Location = New System.Drawing.Point(306, 52)
        Me.btnNuevo2.Name = "btnNuevo2"
        Me.btnNuevo2.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevo2.TabIndex = 6
        Me.btnNuevo2.Text = "Nuevo"
        Me.btnNuevo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevo2.UseVisualStyleBackColor = True
        '
        'lbEstado
        '
        Me.lbEstado.Enabled = False
        Me.lbEstado.FormattingEnabled = True
        Me.lbEstado.Items.AddRange(New Object() {"Activo", "Inactivo"})
        Me.lbEstado.Location = New System.Drawing.Point(167, 45)
        Me.lbEstado.Name = "lbEstado"
        Me.lbEstado.Size = New System.Drawing.Size(73, 30)
        Me.lbEstado.TabIndex = 5
        Me.lbEstado.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(113, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Estado :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(545, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Unidad Base:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(164, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Descripción de Insumo:"
        '
        'crearMaterialForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(769, 568)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "crearMaterialForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Navigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents cbBuscar As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtProd As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents cbUni1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtPre1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnEliminar2 As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar2 As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar2 As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevo2 As ComponentesSolucion2008.BottomSSP
    Friend WithEvents lbEstado As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
