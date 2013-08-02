<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MantVehiculoTransportistaFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MantVehiculoTransportistaFrm))
        Me.dgVehiculo = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCodVehiculo = New System.Windows.Forms.TextBox
        Me.txtMarcaPlaca = New System.Windows.Forms.TextBox
        Me.txtConstancia = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtLicencia = New System.Windows.Forms.TextBox
        Me.txtDni = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCodConductor = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.dgConductor = New System.Windows.Forms.DataGridView
        Me.cbEmpTrans = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnNuevoC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnCancelarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarC = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnModificarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnEliminarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnCancelarV = New ComponentesSolucion2008.BottomSSP(Me.components)
        Me.btnNuevoV = New ComponentesSolucion2008.BottomSSP(Me.components)
        CType(Me.dgVehiculo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgConductor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Size = New System.Drawing.Size(946, 23)
        '
        'lblDerecha
        '
        Me.lblDerecha.Size = New System.Drawing.Size(14, 443)
        '
        'dgVehiculo
        '
        Me.dgVehiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgVehiculo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgVehiculo.Location = New System.Drawing.Point(3, 16)
        Me.dgVehiculo.Name = "dgVehiculo"
        Me.dgVehiculo.Size = New System.Drawing.Size(392, 240)
        Me.dgVehiculo.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCodVehiculo)
        Me.GroupBox1.Controls.Add(Me.txtMarcaPlaca)
        Me.GroupBox1.Controls.Add(Me.txtConstancia)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(401, 96)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Cod Vehiculo:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Marca / Placa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(222, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "N° Constancia:"
        '
        'txtCodVehiculo
        '
        Me.txtCodVehiculo.Location = New System.Drawing.Point(103, 19)
        Me.txtCodVehiculo.Name = "txtCodVehiculo"
        Me.txtCodVehiculo.ReadOnly = True
        Me.txtCodVehiculo.Size = New System.Drawing.Size(100, 20)
        Me.txtCodVehiculo.TabIndex = 2
        '
        'txtMarcaPlaca
        '
        Me.txtMarcaPlaca.Location = New System.Drawing.Point(12, 64)
        Me.txtMarcaPlaca.Name = "txtMarcaPlaca"
        Me.txtMarcaPlaca.Size = New System.Drawing.Size(191, 20)
        Me.txtMarcaPlaca.TabIndex = 0
        '
        'txtConstancia
        '
        Me.txtConstancia.Location = New System.Drawing.Point(225, 64)
        Me.txtConstancia.Name = "txtConstancia"
        Me.txtConstancia.Size = New System.Drawing.Size(163, 20)
        Me.txtConstancia.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgVehiculo)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 184)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(398, 259)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.txtLicencia)
        Me.GroupBox3.Controls.Add(Me.txtDni)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtNombre)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtCodConductor)
        Me.GroupBox3.Location = New System.Drawing.Point(427, 53)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(512, 96)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Cod Conductor:"
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(362, 64)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Size = New System.Drawing.Size(87, 20)
        Me.txtLicencia.TabIndex = 2
        '
        'txtDni
        '
        Me.txtDni.Location = New System.Drawing.Point(18, 64)
        Me.txtDni.MaxLength = 8
        Me.txtDni.Name = "txtDni"
        Me.txtDni.Size = New System.Drawing.Size(83, 20)
        Me.txtDni.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(115, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Nombre:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(359, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "N° Licencia:"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(115, 64)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(234, 20)
        Me.txtNombre.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "DNI:"
        '
        'txtCodConductor
        '
        Me.txtCodConductor.Location = New System.Drawing.Point(115, 19)
        Me.txtCodConductor.Name = "txtCodConductor"
        Me.txtCodConductor.ReadOnly = True
        Me.txtCodConductor.Size = New System.Drawing.Size(100, 20)
        Me.txtCodConductor.TabIndex = 3
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dgConductor)
        Me.GroupBox4.Location = New System.Drawing.Point(427, 184)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(512, 259)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'dgConductor
        '
        Me.dgConductor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgConductor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgConductor.Location = New System.Drawing.Point(3, 16)
        Me.dgConductor.Name = "dgConductor"
        Me.dgConductor.Size = New System.Drawing.Size(506, 240)
        Me.dgConductor.TabIndex = 3
        '
        'cbEmpTrans
        '
        Me.cbEmpTrans.FormattingEnabled = True
        Me.cbEmpTrans.Location = New System.Drawing.Point(179, 26)
        Me.cbEmpTrans.Name = "cbEmpTrans"
        Me.cbEmpTrans.Size = New System.Drawing.Size(452, 21)
        Me.cbEmpTrans.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Empresa de Transportes"
        '
        'btnNuevoC
        '
        Me.btnNuevoC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoC.Image = CType(resources.GetObject("btnNuevoC.Image"), System.Drawing.Image)
        Me.btnNuevoC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoC.Location = New System.Drawing.Point(504, 155)
        Me.btnNuevoC.Name = "btnNuevoC"
        Me.btnNuevoC.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoC.TabIndex = 16
        Me.btnNuevoC.Text = "Nuevo"
        Me.btnNuevoC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoC.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(864, 155)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
        Me.btnCerrar.TabIndex = 20
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnCancelarC
        '
        Me.btnCancelarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelarC.Enabled = False
        Me.btnCancelarC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelarC.Image = CType(resources.GetObject("btnCancelarC.Image"), System.Drawing.Image)
        Me.btnCancelarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelarC.Location = New System.Drawing.Point(684, 155)
        Me.btnCancelarC.Name = "btnCancelarC"
        Me.btnCancelarC.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelarC.TabIndex = 18
        Me.btnCancelarC.Text = "Cancelar"
        Me.btnCancelarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelarC.UseVisualStyleBackColor = True
        '
        'btnEliminarC
        '
        Me.btnEliminarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarC.Image = CType(resources.GetObject("btnEliminarC.Image"), System.Drawing.Image)
        Me.btnEliminarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarC.Location = New System.Drawing.Point(774, 155)
        Me.btnEliminarC.Name = "btnEliminarC"
        Me.btnEliminarC.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarC.TabIndex = 19
        Me.btnEliminarC.Text = "Eliminar"
        Me.btnEliminarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarC.UseVisualStyleBackColor = True
        '
        'btnModificarC
        '
        Me.btnModificarC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarC.Image = CType(resources.GetObject("btnModificarC.Image"), System.Drawing.Image)
        Me.btnModificarC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarC.Location = New System.Drawing.Point(594, 155)
        Me.btnModificarC.Name = "btnModificarC"
        Me.btnModificarC.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarC.TabIndex = 17
        Me.btnModificarC.Text = "Modificar"
        Me.btnModificarC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarC.UseVisualStyleBackColor = True
        '
        'btnModificarV
        '
        Me.btnModificarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificarV.Image = CType(resources.GetObject("btnModificarV.Image"), System.Drawing.Image)
        Me.btnModificarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarV.Location = New System.Drawing.Point(157, 155)
        Me.btnModificarV.Name = "btnModificarV"
        Me.btnModificarV.Size = New System.Drawing.Size(84, 23)
        Me.btnModificarV.TabIndex = 17
        Me.btnModificarV.Text = "Modificar"
        Me.btnModificarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarV.UseVisualStyleBackColor = True
        '
        'btnEliminarV
        '
        Me.btnEliminarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminarV.Image = CType(resources.GetObject("btnEliminarV.Image"), System.Drawing.Image)
        Me.btnEliminarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarV.Location = New System.Drawing.Point(337, 155)
        Me.btnEliminarV.Name = "btnEliminarV"
        Me.btnEliminarV.Size = New System.Drawing.Size(84, 23)
        Me.btnEliminarV.TabIndex = 19
        Me.btnEliminarV.Text = "Eliminar"
        Me.btnEliminarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarV.UseVisualStyleBackColor = True
        '
        'btnCancelarV
        '
        Me.btnCancelarV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelarV.Enabled = False
        Me.btnCancelarV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelarV.Image = CType(resources.GetObject("btnCancelarV.Image"), System.Drawing.Image)
        Me.btnCancelarV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelarV.Location = New System.Drawing.Point(247, 155)
        Me.btnCancelarV.Name = "btnCancelarV"
        Me.btnCancelarV.Size = New System.Drawing.Size(84, 23)
        Me.btnCancelarV.TabIndex = 18
        Me.btnCancelarV.Text = "Cancelar"
        Me.btnCancelarV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelarV.UseVisualStyleBackColor = True
        '
        'btnNuevoV
        '
        Me.btnNuevoV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoV.Image = CType(resources.GetObject("btnNuevoV.Image"), System.Drawing.Image)
        Me.btnNuevoV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevoV.Location = New System.Drawing.Point(67, 155)
        Me.btnNuevoV.Name = "btnNuevoV"
        Me.btnNuevoV.Size = New System.Drawing.Size(84, 23)
        Me.btnNuevoV.TabIndex = 16
        Me.btnNuevoV.Text = "Nuevo"
        Me.btnNuevoV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevoV.UseVisualStyleBackColor = True
        '
        'MantVehiculoTransportistaFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(946, 488)
        Me.Controls.Add(Me.btnNuevoV)
        Me.Controls.Add(Me.btnNuevoC)
        Me.Controls.Add(Me.btnCancelarV)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnEliminarV)
        Me.Controls.Add(Me.btnCancelarC)
        Me.Controls.Add(Me.btnModificarV)
        Me.Controls.Add(Me.btnEliminarC)
        Me.Controls.Add(Me.btnModificarC)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbEmpTrans)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MantVehiculoTransportistaFrm"
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.GroupBox4, 0)
        Me.Controls.SetChildIndex(Me.cbEmpTrans, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Controls.SetChildIndex(Me.lblDerecha, 0)
        Me.Controls.SetChildIndex(Me.btnModificarC, 0)
        Me.Controls.SetChildIndex(Me.btnEliminarC, 0)
        Me.Controls.SetChildIndex(Me.btnModificarV, 0)
        Me.Controls.SetChildIndex(Me.btnCancelarC, 0)
        Me.Controls.SetChildIndex(Me.btnEliminarV, 0)
        Me.Controls.SetChildIndex(Me.btnCerrar, 0)
        Me.Controls.SetChildIndex(Me.btnCancelarV, 0)
        Me.Controls.SetChildIndex(Me.btnNuevoC, 0)
        Me.Controls.SetChildIndex(Me.btnNuevoV, 0)
        CType(Me.dgVehiculo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.dgConductor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgVehiculo As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents dgConductor As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCodVehiculo As System.Windows.Forms.TextBox
    Friend WithEvents txtMarcaPlaca As System.Windows.Forms.TextBox
    Friend WithEvents txtConstancia As System.Windows.Forms.TextBox
    Friend WithEvents cbEmpTrans As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDni As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCodConductor As System.Windows.Forms.TextBox
    Friend WithEvents txtLicencia As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnCancelarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarC As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnModificarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnEliminarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnCancelarV As ComponentesSolucion2008.BottomSSP
    Friend WithEvents btnNuevoV As ComponentesSolucion2008.BottomSSP

End Class
