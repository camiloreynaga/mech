<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewerAprobacionDesembolsoForm
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
        Me.CReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.VOrdenDesemGerencia1TableAdapter1 = New softwareMech.DataSetAlmacenTableAdapters.VOrdenDesemGerencia1TableAdapter
        Me.DataSetAlmacen1 = New softwareMech.DataSetAlmacen
        Me.DataSetInformesCr1 = New softwareMech.DataSetInformesCr
        CType(Me.DataSetAlmacen1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSetInformesCr1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CReportViewer1
        '
        Me.CReportViewer1.ActiveViewIndex = -1
        Me.CReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CReportViewer1.Name = "CReportViewer1"
        Me.CReportViewer1.SelectionFormula = ""
        Me.CReportViewer1.Size = New System.Drawing.Size(390, 398)
        Me.CReportViewer1.TabIndex = 0
        Me.CReportViewer1.ViewTimeSelectionFormula = ""
        '
        'VOrdenDesemGerencia1TableAdapter1
        '
        Me.VOrdenDesemGerencia1TableAdapter1.ClearBeforeFill = True
        '
        'DataSetAlmacen1
        '
        Me.DataSetAlmacen1.DataSetName = "DataSetAlmacen"
        Me.DataSetAlmacen1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataSetInformesCr1
        '
        Me.DataSetInformesCr1.DataSetName = "DataSetInformesCr"
        Me.DataSetInformesCr1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewerAprobacionDesembolsoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 398)
        Me.Controls.Add(Me.CReportViewer1)
        Me.Name = "ReportViewerAprobacionDesembolsoForm"
        Me.Text = "ReportViewerAprobacionDesembolsoForm"
        CType(Me.DataSetAlmacen1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSetInformesCr1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents VOrdenDesemGerencia1TableAdapter1 As softwareMech.DataSetAlmacenTableAdapters.VOrdenDesemGerencia1TableAdapter
    Friend WithEvents DataSetAlmacen1 As softwareMech.DataSetAlmacen
    Friend WithEvents DataSetInformesCr1 As softwareMech.DataSetInformesCr
End Class
