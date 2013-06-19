<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class configuracionColorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(configuracionColorForm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgTabla1 = New System.Windows.Forms.DataGridView
        Me.Navigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCam = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnRestablecer = New System.Windows.Forms.Button
        Me.btnTxtButtom = New System.Windows.Forms.Button
        Me.btnTxtLabel = New System.Windows.Forms.Button
        Me.btnTxtTitulos = New System.Windows.Forms.Button
        Me.btnTituloDG = New System.Windows.Forms.Button
        Me.btnTitulo = New System.Windows.Forms.Button
        Me.btnFondoForm = New System.Windows.Forms.Button
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.Panel1.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(631, 23)
        Me.lblTitulo.Text = "Configuración de los colores del formulario y los controles"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 341)
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.dgTabla1)
        Me.Panel1.Controls.Add(Me.Navigator1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtCam)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(14, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(333, 296)
        Me.Panel1.TabIndex = 9
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(0, 57)
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        Me.dgTabla1.Size = New System.Drawing.Size(327, 209)
        Me.dgTabla1.TabIndex = 6
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.CountItem = Me.BindingNavigatorCountItem
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.Navigator1.Location = New System.Drawing.Point(0, 267)
        Me.Navigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.Navigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.Navigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.Navigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(329, 25)
        Me.Navigator1.TabIndex = 5
        Me.Navigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.BackColor = System.Drawing.SystemColors.Control
        Me.BindingNavigatorCountItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(45, 22)
        Me.BindingNavigatorCountItem.Text = "de {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Número total de elementos"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Mover primero"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Mover anterior"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Posición"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.ReadOnly = True
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(58, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.BindingNavigatorPositionItem.ToolTipText = "Posición actual"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Mover siguiente"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Mover último"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Seleccione registro :"
        '
        'txtCam
        '
        Me.txtCam.Location = New System.Drawing.Point(5, 19)
        Me.txtCam.Name = "txtCam"
        Me.txtCam.Size = New System.Drawing.Size(322, 20)
        Me.txtCam.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Ponga su nombre :"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.btnRestablecer)
        Me.Panel3.Controls.Add(Me.btnTxtButtom)
        Me.Panel3.Controls.Add(Me.btnTxtLabel)
        Me.Panel3.Controls.Add(Me.btnTxtTitulos)
        Me.Panel3.Controls.Add(Me.btnTituloDG)
        Me.Panel3.Controls.Add(Me.btnTitulo)
        Me.Panel3.Controls.Add(Me.btnFondoForm)
        Me.Panel3.Location = New System.Drawing.Point(348, 23)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(276, 296)
        Me.Panel3.TabIndex = 11
        '
        'btnRestablecer
        '
        Me.btnRestablecer.BackColor = System.Drawing.SystemColors.Control
        Me.btnRestablecer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRestablecer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnRestablecer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRestablecer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestablecer.Location = New System.Drawing.Point(6, 259)
        Me.btnRestablecer.Name = "btnRestablecer"
        Me.btnRestablecer.Size = New System.Drawing.Size(258, 29)
        Me.btnRestablecer.TabIndex = 6
        Me.btnRestablecer.Text = "Restablecer colores predeterminados"
        Me.btnRestablecer.UseVisualStyleBackColor = False
        '
        'btnTxtButtom
        '
        Me.btnTxtButtom.BackColor = System.Drawing.SystemColors.Control
        Me.btnTxtButtom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTxtButtom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnTxtButtom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTxtButtom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTxtButtom.Location = New System.Drawing.Point(6, 217)
        Me.btnTxtButtom.Name = "btnTxtButtom"
        Me.btnTxtButtom.Size = New System.Drawing.Size(258, 29)
        Me.btnTxtButtom.TabIndex = 5
        Me.btnTxtButtom.Text = "Cambiar color de texto de los buttom"
        Me.btnTxtButtom.UseVisualStyleBackColor = False
        '
        'btnTxtLabel
        '
        Me.btnTxtLabel.BackColor = System.Drawing.SystemColors.Control
        Me.btnTxtLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTxtLabel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnTxtLabel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTxtLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTxtLabel.Location = New System.Drawing.Point(6, 174)
        Me.btnTxtLabel.Name = "btnTxtLabel"
        Me.btnTxtLabel.Size = New System.Drawing.Size(258, 29)
        Me.btnTxtLabel.TabIndex = 4
        Me.btnTxtLabel.Text = "Cambiar color de texto de los label"
        Me.btnTxtLabel.UseVisualStyleBackColor = False
        '
        'btnTxtTitulos
        '
        Me.btnTxtTitulos.BackColor = System.Drawing.SystemColors.Control
        Me.btnTxtTitulos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTxtTitulos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnTxtTitulos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTxtTitulos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTxtTitulos.Location = New System.Drawing.Point(6, 131)
        Me.btnTxtTitulos.Name = "btnTxtTitulos"
        Me.btnTxtTitulos.Size = New System.Drawing.Size(258, 29)
        Me.btnTxtTitulos.TabIndex = 3
        Me.btnTxtTitulos.Text = "Cambiar color de texto de los titulos"
        Me.btnTxtTitulos.UseVisualStyleBackColor = False
        '
        'btnTituloDG
        '
        Me.btnTituloDG.BackColor = System.Drawing.SystemColors.Control
        Me.btnTituloDG.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTituloDG.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnTituloDG.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTituloDG.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTituloDG.Location = New System.Drawing.Point(6, 89)
        Me.btnTituloDG.Name = "btnTituloDG"
        Me.btnTituloDG.Size = New System.Drawing.Size(258, 29)
        Me.btnTituloDG.TabIndex = 2
        Me.btnTituloDG.Text = "Cambiar color de fondo del titulo dataGrid"
        Me.btnTituloDG.UseVisualStyleBackColor = False
        '
        'btnTitulo
        '
        Me.btnTitulo.BackColor = System.Drawing.SystemColors.Control
        Me.btnTitulo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTitulo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnTitulo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTitulo.Location = New System.Drawing.Point(6, 47)
        Me.btnTitulo.Name = "btnTitulo"
        Me.btnTitulo.Size = New System.Drawing.Size(258, 29)
        Me.btnTitulo.TabIndex = 1
        Me.btnTitulo.Text = "Cambiar color de fondo  del titulo principal"
        Me.btnTitulo.UseVisualStyleBackColor = False
        '
        'btnFondoForm
        '
        Me.btnFondoForm.BackColor = System.Drawing.SystemColors.Control
        Me.btnFondoForm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFondoForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnFondoForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFondoForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFondoForm.Location = New System.Drawing.Point(6, 6)
        Me.btnFondoForm.Name = "btnFondoForm"
        Me.btnFondoForm.Size = New System.Drawing.Size(258, 29)
        Me.btnFondoForm.TabIndex = 0
        Me.btnFondoForm.Text = "Cambiar color de fondo del formulario"
        Me.btnFondoForm.UseVisualStyleBackColor = False
        '
        'btnAceptar
        '
        Me.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAceptar.Location = New System.Drawing.Point(125, 327)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(222, 29)
        Me.btnAceptar.TabIndex = 12
        Me.btnAceptar.Text = "Aceptar configuración nueva"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(377, 327)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(248, 29)
        Me.btnCancelar.TabIndex = 13
        Me.btnCancelar.Text = "Cerrar sin guardar configuración nueva"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'configuracionColorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(631, 384)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "configuracionColorForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents Navigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCam As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnFondoForm As System.Windows.Forms.Button
    Friend WithEvents btnTxtLabel As System.Windows.Forms.Button
    Friend WithEvents btnTxtTitulos As System.Windows.Forms.Button
    Friend WithEvents btnTituloDG As System.Windows.Forms.Button
    Friend WithEvents btnTitulo As System.Windows.Forms.Button
    Friend WithEvents btnRestablecer As System.Windows.Forms.Button
    Friend WithEvents btnTxtButtom As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog

End Class
