<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantClasificacionEgresosForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantClasificacionEgresosForm))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txtClasificacion = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgClasificacion = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbMovimiento = New System.Windows.Forms.ComboBox
        Me.btnEliminarCla = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevoCla = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarCla = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rbInactivo = New System.Windows.Forms.RadioButton
        Me.rbActivo = New System.Windows.Forms.RadioButton
        Me.dgSubClasif = New System.Windows.Forms.DataGridView
        Me.txtSubClasificacion = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnEliminarSub = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnNuevoSub = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarSub = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.dgClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.dgSubClasif, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(800, 23)
        Me.lblTitulo.Text = "Mantenimiento de clasificación de egresos"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 408)
        '
        'txtClasificacion
        '
        Me.txtClasificacion.Location = New System.Drawing.Point(113, 39)
        Me.txtClasificacion.Name = "txtClasificacion"
        Me.txtClasificacion.Size = New System.Drawing.Size(190, 20)
        Me.txtClasificacion.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgClasificacion)
        Me.Panel1.Controls.Add(Me.txtClasificacion)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cbMovimiento)
        Me.Panel1.Controls.Add(Me.btnEliminarCla)
        Me.Panel1.Controls.Add(Me.btnNuevoCla)
        Me.Panel1.Controls.Add(Me.btnModificarCla)
        Me.Panel1.Location = New System.Drawing.Point(20, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(308, 392)
        Me.Panel1.TabIndex = 0
        '
        'dgClasificacion
        '
        Me.dgClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgClasificacion.Location = New System.Drawing.Point(3, 96)
        Me.dgClasificacion.Name = "dgClasificacion"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgClasificacion.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgClasificacion.Size = New System.Drawing.Size(300, 280)
        Me.dgClasificacion.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Clasificación:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Tipo Movimiento:"
        '
        'cbMovimiento
        '
        Me.cbMovimiento.FormattingEnabled = True
        Me.cbMovimiento.Location = New System.Drawing.Point(113, 6)
        Me.cbMovimiento.Name = "cbMovimiento"
        Me.cbMovimiento.Size = New System.Drawing.Size(190, 21)
        Me.cbMovimiento.TabIndex = 0
        '
        'btnEliminarCla
        '
        Me.btnEliminarCla.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarCla.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarCla.Image = CType(resources.GetObject("btnEliminarCla.Image"), System.Drawing.Image)
        Me.btnEliminarCla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarCla.Location = New System.Drawing.Point(219, 67)
        Me.btnEliminarCla.Name = "btnEliminarCla"
        Me.btnEliminarCla.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarCla.TabIndex = 3
        Me.btnEliminarCla.Text = "Eliminar"
        Me.btnEliminarCla.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarCla.UseVisualStyleBackColor = False
        '
        'btnNuevoCla
        '
        Me.btnNuevoCla.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevoCla.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoCla.Image = CType(resources.GetObject("btnNuevoCla.Image"), System.Drawing.Image)
        Me.btnNuevoCla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoCla.Location = New System.Drawing.Point(39, 67)
        Me.btnNuevoCla.Name = "btnNuevoCla"
        Me.btnNuevoCla.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoCla.TabIndex = 1
        Me.btnNuevoCla.Text = "Guardar"
        Me.btnNuevoCla.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoCla.UseVisualStyleBackColor = False
        '
        'btnModificarCla
        '
        Me.btnModificarCla.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificarCla.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarCla.Image = CType(resources.GetObject("btnModificarCla.Image"), System.Drawing.Image)
        Me.btnModificarCla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarCla.Location = New System.Drawing.Point(129, 67)
        Me.btnModificarCla.Name = "btnModificarCla"
        Me.btnModificarCla.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarCla.TabIndex = 2
        Me.btnModificarCla.Text = "Actualizar"
        Me.btnModificarCla.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarCla.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbInactivo)
        Me.Panel2.Controls.Add(Me.rbActivo)
        Me.Panel2.Controls.Add(Me.dgSubClasif)
        Me.Panel2.Controls.Add(Me.txtSubClasificacion)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.btnEliminarSub)
        Me.Panel2.Controls.Add(Me.btnCerrar)
        Me.Panel2.Controls.Add(Me.btnNuevoSub)
        Me.Panel2.Controls.Add(Me.btnModificarSub)
        Me.Panel2.Location = New System.Drawing.Point(334, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(454, 392)
        Me.Panel2.TabIndex = 1
        '
        'rbInactivo
        '
        Me.rbInactivo.AutoSize = True
        Me.rbInactivo.Location = New System.Drawing.Point(371, 33)
        Me.rbInactivo.Name = "rbInactivo"
        Me.rbInactivo.Size = New System.Drawing.Size(71, 17)
        Me.rbInactivo.TabIndex = 2
        Me.rbInactivo.TabStop = True
        Me.rbInactivo.Text = "Inactivo"
        Me.rbInactivo.UseVisualStyleBackColor = True
        '
        'rbActivo
        '
        Me.rbActivo.AutoSize = True
        Me.rbActivo.Location = New System.Drawing.Point(371, 11)
        Me.rbActivo.Name = "rbActivo"
        Me.rbActivo.Size = New System.Drawing.Size(61, 17)
        Me.rbActivo.TabIndex = 1
        Me.rbActivo.TabStop = True
        Me.rbActivo.Text = "Activo"
        Me.rbActivo.UseVisualStyleBackColor = True
        '
        'dgSubClasif
        '
        Me.dgSubClasif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSubClasif.Location = New System.Drawing.Point(3, 96)
        Me.dgSubClasif.Name = "dgSubClasif"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgSubClasif.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgSubClasif.Size = New System.Drawing.Size(439, 280)
        Me.dgSubClasif.TabIndex = 6
        '
        'txtSubClasificacion
        '
        Me.txtSubClasificacion.Location = New System.Drawing.Point(6, 33)
        Me.txtSubClasificacion.Name = "txtSubClasificacion"
        Me.txtSubClasificacion.Size = New System.Drawing.Size(314, 20)
        Me.txtSubClasificacion.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(315, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Estado:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Subclasificación:"
        '
        'btnEliminarSub
        '
        Me.btnEliminarSub.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarSub.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarSub.Image = CType(resources.GetObject("btnEliminarSub.Image"), System.Drawing.Image)
        Me.btnEliminarSub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarSub.Location = New System.Drawing.Point(268, 67)
        Me.btnEliminarSub.Name = "btnEliminarSub"
        Me.btnEliminarSub.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarSub.TabIndex = 5
        Me.btnEliminarSub.Text = "Eliminar"
        Me.btnEliminarSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarSub.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(358, 67)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(84, 23)
        Me.btnCerrar.TabIndex = 6
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'btnNuevoSub
        '
        Me.btnNuevoSub.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevoSub.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoSub.Image = CType(resources.GetObject("btnNuevoSub.Image"), System.Drawing.Image)
        Me.btnNuevoSub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoSub.Location = New System.Drawing.Point(88, 67)
        Me.btnNuevoSub.Name = "btnNuevoSub"
        Me.btnNuevoSub.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoSub.TabIndex = 3
        Me.btnNuevoSub.Text = "Guardar"
        Me.btnNuevoSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoSub.UseVisualStyleBackColor = False
        '
        'btnModificarSub
        '
        Me.btnModificarSub.BackColor = System.Drawing.SystemColors.Control
        Me.btnModificarSub.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarSub.Image = CType(resources.GetObject("btnModificarSub.Image"), System.Drawing.Image)
        Me.btnModificarSub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarSub.Location = New System.Drawing.Point(178, 67)
        Me.btnModificarSub.Name = "btnModificarSub"
        Me.btnModificarSub.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarSub.TabIndex = 4
        Me.btnModificarSub.Text = "Actualizar"
        Me.btnModificarSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarSub.UseVisualStyleBackColor = False
        '
        'MantClasificacionEgresosForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(800, 453)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "MantClasificacionEgresosForm"
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgSubClasif, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtClasificacion As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbMovimiento As System.Windows.Forms.ComboBox
    Friend WithEvents btnEliminarCla As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevoCla As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarCla As ComponentesSolucion2008.BottomSSP
    Friend WithEvents dgClasificacion As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgSubClasif As System.Windows.Forms.DataGridView
    Friend WithEvents txtSubClasificacion As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnEliminarSub As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevoSub As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarSub As ComponentesSolucion2008.BottomSSP
    Friend WithEvents rbInactivo As System.Windows.Forms.RadioButton
    Friend WithEvents rbActivo As System.Windows.Forms.RadioButton
    Friend WithEvents btnCerrar As System.Windows.Forms.Button

End Class
