<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewerPadre
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportViewerPadre))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.btnPdf = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(457, 365)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'btnPdf
        '
        Me.btnPdf.BackColor = System.Drawing.Color.Transparent
        Me.btnPdf.Image = CType(resources.GetObject("btnPdf.Image"), System.Drawing.Image)
        Me.btnPdf.Location = New System.Drawing.Point(350, 3)
        Me.btnPdf.Name = "btnPdf"
        Me.btnPdf.Size = New System.Drawing.Size(53, 25)
        Me.btnPdf.TabIndex = 7
        Me.btnPdf.Text = "PDF"
        Me.btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPdf.UseVisualStyleBackColor = False
        '
        'ReportViewerPadre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 365)
        Me.Controls.Add(Me.btnPdf)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Name = "ReportViewerPadre"
        Me.Text = "ReportViewerPadre"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnPdf As System.Windows.Forms.Button
End Class
