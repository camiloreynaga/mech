Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports CrystalDecisions.Shared



Public Class reporteAprobacionDesembolso

    ''' <summary>
    ''' instancia de objeto DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' estado desembolso
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource As New BindingSource

    ''' <summary>
    ''' instancia de objeto cConfigControls
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls


#Region "métodos"

    Private Sub ModificandoColumnaDGV()

        DgDesembolsos.ReadOnly = True
        DgDesembolsos.AllowUserToAddRows = False
        DgDesembolsos.AllowUserToDeleteRows = False

        Try

            With DgDesembolsos

                .Columns("idOP").Visible = False
                .Columns("fecDes").HeaderText = "Fecha"
                .Columns("fecDes").Width = 70
                .Columns("serie").HeaderText = "Serie"
                .Columns("serie").Width = 35
                .Columns("nroDes").HeaderText = "N°"
                .Columns("nroDes").Width = 55
                .Columns("monto").HeaderText = "Monto"
                .Columns("monto").Width = 75
                .Columns("monto").DefaultCellStyle.Format = "N2"
                .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("montoPagado").HeaderText = "Pagado"
                .Columns("montoPagado").Width = 75
                .Columns("montoPagado").DefaultCellStyle.Format = "N2"
                .Columns("montoPagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                .Columns("diferencia").HeaderText = "Pendiente"
                .Columns("diferencia").Width = 75
                .Columns("diferencia").DefaultCellStyle.Format = "N2"
                .Columns("diferencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                .Columns("montoDet").Visible = False  'HeaderText = "Detracción"
                '.Columns("montoDet").Width = 75
                .Columns("pagoDetraccion").Visible = False  'HeaderText = "Pago Detracción"
                '.Columns("pagoDetraccion").Width = 75
                .Columns("diferenciaDetra").Visible = False 'HeaderText = "Pendiente Detracción"
                .Columns("simbolo").HeaderText = ""
                .Columns("simbolo").Width = 30
                .Columns("obra").HeaderText = "Obra"
                .Columns("obra").Width = 250
                .Columns("datoReq").HeaderText = "concepto"
                .Columns("datoReq").Width = 220
                .Columns("solicitante").HeaderText = "Solicitante"
                .Columns("solicitante").Width = 220

            End With


        Catch ex As Exception

        End Try

    End Sub

    Private Sub configurarColorControl()

        Me.BackColor = BackColorP

        'Color para los labels del contenedor principal
        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then 'LABELS
                Me.Controls(i).ForeColor = ForeColorLabel

            End If

            If TypeOf Me.Controls(i) Is RadioButton Then 'CHECKBOX
                Me.Controls(i).ForeColor = ForeColorLabel

            End If

            'If TypeOf Me.Controls(i) Is GroupBox Then 'TEXTBOX
            '    For c As Integer = 0 To Me.Controls(i).Controls.Count - 1
            '        oGrilla.configurarColorControl("Label", Me.Controls(i), ForeColorLabel)
            '    Next
            'End If
        Next

        'btnCerrar.ForeColor = ForeColorButtom
        btnVer.ForeColor = ForeColorButtom

    End Sub

    Private Function AddCriterioFiltro(ByVal criterio As String, ByVal filtro As String) As String
        If filtro.Length > 0 Then
            filtro &= " and " & criterio
        Else
            filtro &= " " & criterio
        End If
        Return filtro

    End Function

    Private Sub filtrando()

        'If BindingSource4.Position >= 0 And BindingSource5.Position >= 0 Then
        bindingSource.Filter = ""
        Dim pFiltro As String = bindingSource.Filter
        Dim pCriterio As String
        If bindingSource.Position >= 0 Then

            If rdoTodos.Checked = False Then
                If String.IsNullOrEmpty(cbProveedor.SelectedValue) = False Then
                    pCriterio = "codIde ='" & cbProveedor.SelectedValue & "'"
                    pFiltro = AddCriterioFiltro(pCriterio, pFiltro)
                End If
                '  DgDesembolsos.Columns("nombre").Visible = False
            Else
                'DgDesembolsos.Columns("nombre").Visible = True
            End If

            bindingSource.Filter = pFiltro

        End If

    End Sub

    ' 
#End Region

    Private Sub reporteAprobacionDesembolso_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub

    Private Sub reporteAprobacionDesembolso_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDataManager.CargarCombo("PA_Proveedores", CommandType.StoredProcedure, cbProveedor, "codIde", "razon")
        configurarColorControl()

    End Sub

    Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click

        Dim sele As String = "select fecDes,idOP,serie,nroDes,simbolo,monto,montoPagado,diferencia,montoDet,pagoDetraccion,diferenciaDetra,obra,datoReq,solicitante,proveedor,codIde from vseguimientoDesembolsoPagos where firmaTesoreria is null and firmaGerencia =2"
        oDataManager.CargarGrilla(sele, CommandType.Text, DgDesembolsos, bindingSource)
        'enlazando con el navigator
        BindingNavigator1.BindingSource = bindingSource
        filtrando()

        ModificandoColumnaDGV()


    End Sub


    Private Sub rdoTodos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTodos.CheckedChanged
        If rdoTodos.Checked = True Then
            cbProveedor.Visible = False
        Else
            cbProveedor.Visible = True
        End If
    End Sub
End Class
