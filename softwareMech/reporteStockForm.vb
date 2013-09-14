Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class reporteStockForm

#Region "Variables"
    ''' <summary>
    ''' Stock
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' instancia de objeto para cConfigFormControls
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' instancia de objeto para DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager


#End Region

#Region "Métodos"

    Private Sub configurarColorControl()

        Me.BackColor = BackColorP
        'Color para los labels del contenedor principal
        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then 'LABELS
                Me.Controls(i).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(i) Is CheckBox Then 'CHECKBOX
                Me.Controls(i).ForeColor = ForeColorLabel
            End If

            If TypeOf Me.Controls(i) Is GroupBox Then 'TEXTBOX
                For c As Integer = 0 To Me.Controls(i).Controls.Count - 1
                    oGrilla.configurarColorControl("Label", Me.Controls(i), ForeColorLabel)
                Next
            End If
        Next

    End Sub

    ''' <summary>
    ''' modifica la configuración de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()

        dgInsumos.ReadOnly = True
        dgInsumos.AllowUserToAddRows = False
        dgInsumos.AllowUserToDeleteRows = False
        With dgInsumos
            .Columns("idMU").Visible = False
            .Columns("codUbi").Visible = False

            .Columns("material").HeaderText = "Insumo"
            .Columns("material").Width = 570
            .Columns("stock").HeaderText = "Stock"
            .Columns("stock").Width = 70
            .Columns("unidad").HeaderText = "Unidad"
            .Columns("unidad").Width = 50
            .Columns("tipoM").HeaderText = "Tipo"
            .Columns("tipoM").Width = 120

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With

    End Sub


    Private Sub ConsultarStock()
        Dim consulta As String = "select idMU,codUbi,material,stock,unidad,tipoM from vStockAlmacen where codUbi=" & cbAlmacen.SelectedValue
        oDataManager.CargarGrilla(consulta, CommandType.Text, dgInsumos, BindingSource0)

        BindingNavigator1.BindingSource = BindingSource0

        ModificarColumnasDGV()
    End Sub

#End Region

#Region "Eventos"

#End Region

    Private Sub dgInsumos_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub dgInsumos_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    Private Sub dgInsumos_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub reporteStockForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub reporteStockForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim wait As New waitForm
        wait.Show()

        'Cargar datos de combo
        'oDataManager.CargarCombo("PA_LugarTrabajo", CommandType.StoredProcedure, cbObras, "codigo", "nombre")

        configurarColorControl()

        If vSCodigo = "00-00" Then
            oDataManager.CargarCombo("PA_LugarTrabajo", CommandType.StoredProcedure, cbObras, "codigo", "nombre")
        Else
            Dim sele1 As String = "select distinct codigo,nombre from VLugarUbiStoc TObra where codigo='" & vSCodigo & "'"
            oDataManager.CargarCombo(sele1, CommandType.Text, cbObras, "codigo", "nombre")
        End If



        ConsultarStock()

        wait.Close()
    End Sub

    Private Sub cbObras_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbObras.SelectedIndexChanged

        Try
            If TypeOf cbObras.SelectedValue Is String Then
                Dim cod As String = cbObras.SelectedValue
                Dim consulta As String = "select codUbi,ubicacion,codigo from TUbicacion where codigo ='" & cod & "'"
                oDataManager.CargarCombo(consulta, CommandType.Text, cbAlmacen, "codUbi", "ubicacion")
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnVis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVis.Click

        ConsultarStock()

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Datos...")
            Exit Sub
        End If



        'vCodDoc = BindingSource0.Item(BindingSource0.Position)(0)
        'vParam1 = cambiarNroTotalLetra()
        'If String.IsNullOrEmpty(txtOrdCompra.Text) = False Then
        '    vParam2 = txtOrdCompra.Text.Trim() 'recuperarNroOrdenCompra()
        'Else
        '    vParam2 = ""
        'End If

        Dim informe As New ReportViewerStockForm
        informe.vCodUbicacion = cbAlmacen.SelectedValue
        informe.ShowDialog()
    End Sub
End Class
