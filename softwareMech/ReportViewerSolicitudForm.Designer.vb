<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewerSolicitudForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportViewerSolicitudForm))
        Me.CReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.DataSetAlmacen1 = New softwareMech.DataSetAlmacen
        Me.VSolDetSolTableAdapter1 = New softwareMech.DataSetAlmacenTableAdapters.VSolDetSolTableAdapter
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.DataSetAlmacen1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CReportViewer
        '
        Me.CReportViewer.ActiveViewIndex = -1
        Me.CReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CReportViewer.Location = New System.Drawing.Point(0, 0)
        Me.CReportViewer.Name = "CReportViewer"
        Me.CReportViewer.SelectionFormula = ""
        Me.CReportViewer.Size = New System.Drawing.Size(447, 354)
        Me.CReportViewer.TabIndex = 0
        Me.CReportViewer.ViewTimeSelectionFormula = ""
        '
        'DataSetAlmacen1
        '
        Me.DataSetAlmacen1.DataSetName = "DataSetAlmacen"
        Me.DataSetAlmacen1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'VSolDetSolTableAdapter1
        '
        Me.VSolDetSolTableAdapter1.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(350, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(53, 25)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "PDF"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ReportViewerSolicitudForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 354)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CReportViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReportViewerSolicitudForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SSP SAC"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataSetAlmacen1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents DataSetAlmacen1 As softwareMech.DataSetAlmacen
    Friend WithEvents VSolDetSolTableAdapter1 As softwareMech.DataSetAlmacenTableAdapters.VSolDetSolTableAdapter
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
