<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfiguracionSerieEmpForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfiguracionSerieEmpForm))
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbTabla = New System.Windows.Forms.ListBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.txtNroDoc2 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtNroDoc1 = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.txtSerie = New ComponentesSolucion2008.TextBoxSSP(Me.components)
        Me.lbEstado = New System.Windows.Forms.ListBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCod = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnEliminar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificar = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevo = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Navigator1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(616, 23)
        Me.lblTitulo.Text = "Configuración de serie de documentos"
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 411)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Navigator1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbTabla)
        Me.Panel1.Controls.Add(Me.dgTabla1)
        Me.Panel1.Location = New System.Drawing.Point(13, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(470, 327)
        Me.Panel1.TabIndex = 6
        '
        'dgTabla1
        '
        Me.dgTabla1.AllowUserToAddRows = False
        Me.dgTabla1.AllowUserToDeleteRows = False
        Me.dgTabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTabla1.Location = New System.Drawing.Point(1, 103)
        Me.dgTabla1.Name = "dgTabla1"
        Me.dgTabla1.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTabla1.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTabla1.Size = New System.Drawing.Size(467, 199)
        Me.dgTabla1.TabIndex = 8
        '
        'Navigator1
        '
        Me.Navigator1.AddNewItem = Nothing
        Me.Navigator1.CountItem = Me.BindingNavigatorCountItem
        Me.Navigator1.DeleteItem = Nothing
        Me.Navigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Navigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.Navigator1.Location = New System.Drawing.Point(0, 302)
        Me.Navigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.Navigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.Navigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.Navigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.Navigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Navigator1.Size = New System.Drawing.Size(470, 25)
        Me.Navigator1.TabIndex = 7
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
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
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
        Me.Label2.Location = New System.Drawing.Point(4, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Seleccione serie :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione documento :"
        '
        'lbTabla
        '
        Me.lbTabla.FormattingEnabled = True
        Me.lbTabla.Location = New System.Drawing.Point(1, 17)
        Me.lbTabla.Name = "lbTabla"
        Me.lbTabla.Size = New System.Drawing.Size(469, 69)
        Me.lbTabla.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtDes)
        Me.Panel2.Controls.Add(Me.txtNroDoc2)
        Me.Panel2.Controls.Add(Me.txtNroDoc1)
        Me.Panel2.Controls.Add(Me.txtSerie)
        Me.Panel2.Controls.Add(Me.lbEstado)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtCod)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(13, 351)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(470, 81)
        Me.Panel2.TabIndex = 7
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(82, 53)
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(259, 20)
        Me.txtDes.TabIndex = 17
        '
        'txtNroDoc2
        '
        Me.txtNroDoc2.Location = New System.Drawing.Point(239, 18)
        Me.txtNroDoc2.Name = "txtNroDoc2"
        Me.txtNroDoc2.ReadOnly = True
        Me.txtNroDoc2.Size = New System.Drawing.Size(102, 20)
        Me.txtNroDoc2.TabIndex = 16
        Me.txtNroDoc2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNroDoc1
        '
        Me.txtNroDoc1.Location = New System.Drawing.Point(101, 18)
        Me.txtNroDoc1.Name = "txtNroDoc1"
        Me.txtNroDoc1.ReadOnly = True
        Me.txtNroDoc1.Size = New System.Drawing.Size(102, 20)
        Me.txtNroDoc1.TabIndex = 15
        Me.txtNroDoc1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(7, 18)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.ReadOnly = True
        Me.txtSerie.Size = New System.Drawing.Size(65, 20)
        Me.txtSerie.TabIndex = 14
        Me.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbEstado
        '
        Me.lbEstado.Enabled = False
        Me.lbEstado.FormattingEnabled = True
        Me.lbEstado.Items.AddRange(New Object() {"Activo", "Inactivo"})
        Me.lbEstado.Location = New System.Drawing.Point(370, 18)
        Me.lbEstado.Name = "lbEstado"
        Me.lbEstado.Size = New System.Drawing.Size(78, 30)
        Me.lbEstado.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(367, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Estado :"
        '
        'txtCod
        '
        Me.txtCod.Location = New System.Drawing.Point(45, 18)
        Me.txtCod.Name = "txtCod"
        Me.txtCod.ReadOnly = True
        Me.txtCod.Size = New System.Drawing.Size(20, 20)
        Me.txtCod.TabIndex = 12
        Me.txtCod.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Descripción :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(236, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Nº de doc. final"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(98, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Nº de doc. inicial"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Nº serie doc"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnCerrar)
        Me.Panel3.Controls.Add(Me.btnEliminar)
        Me.Panel3.Controls.Add(Me.btnCancelar)
        Me.Panel3.Controls.Add(Me.btnModificar)
        Me.Panel3.Controls.Add(Me.btnNuevo)
        Me.Panel3.Location = New System.Drawing.Point(484, 21)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(132, 413)
        Me.Panel3.TabIndex = 8
        '
        'btnCerrar
        '
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(23, 376)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(84, 23)
        Me.btnCerrar.TabIndex = 26
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(23, 274)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminar.TabIndex = 12
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(23, 205)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelar.TabIndex = 11
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.Image = CType(resources.GetObject("btnModificar.Image"), System.Drawing.Image)
        Me.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar.Location = New System.Drawing.Point(23, 138)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(84, 23)
        Me.btnModificar.TabIndex = 10
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(23, 75)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevo.TabIndex = 9
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'ConfiguracionSerieEmpForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(616, 456)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ConfiguracionSerieEmpForm"
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgTabla1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Navigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Navigator1.ResumeLayout(False)
        Me.Navigator1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
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
    Friend WithEvents lbTabla As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgTabla1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbEstado As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCod As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSerie As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtNroDoc2 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtNroDoc1 As ComponentesSolucion2008.TextBoxSSP
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnNuevo As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminar As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCerrar As System.Windows.Forms.Button

End Class
