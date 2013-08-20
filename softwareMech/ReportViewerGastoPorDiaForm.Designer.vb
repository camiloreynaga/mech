<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewerGastoPorDiaForm
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
        Me.CReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.VGastosPorDiaTableAdapter1 = New softwareMech.DataSetAlmacenTableAdapters.VGastosPorDiaTableAdapter
        Me.DataSetAlmacen1 = New softwareMech.DataSetAlmacen
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
        Me.CReportViewer.Size = New System.Drawing.Size(552, 398)
        Me.CReportViewer.TabIndex = 2
        Me.CReportViewer.ViewTimeSelectionFormula = ""
        '
        'VGastosPorDiaTableAdapter1
        '
        Me.VGastosPorDiaTableAdapter1.ClearBeforeFill = True
        '
        'DataSetAlmacen1
        '
        Me.DataSetAlmacen1.DataSetName = "DataSetAlmacen"
        Me.DataSetAlmacen1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewerGastoPorDiaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 398)
        Me.Controls.Add(Me.CReportViewer)
        Me.Name = "ReportViewerGastoPorDiaForm"
        Me.Text = "ReportViewerGastoPorDiaForm"
        CType(Me.DataSetAlmacen1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents VGastosPorDiaTableAdapter1 As softwareMech.DataSetAlmacenTableAdapters.VGastosPorDiaTableAdapter
    Friend WithEvents DataSetAlmacen1 As softwareMech.DataSetAlmacen
End Class
