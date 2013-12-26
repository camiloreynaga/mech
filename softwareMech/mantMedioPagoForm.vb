Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008



Public Class mantMedioPagoForm


#Region "variables"

    ''' <summary>
    ''' Instancia de objeto para configuraciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' Instancia de objeto para DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager


    Dim oConfigForms As New cConfigFormControls

    ''' <summary>
    ''' Cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' Medios de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource

    ''' <summary>
    ''' command Insert Medio Pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertTableMedio As SqlCommand

    ''' <summary>
    ''' command Update medio pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateTableMedio As SqlCommand

    ''' <summary>
    ''' command delete medio pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteTableMedio As SqlCommand



#End Region

#Region "métodos"

    ''' <summary>
    ''' configura el color de los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarColorControl()

        Me.BackColor = BackColorP

        oConfigForms.configurarColorControl2("Label", GroupBox1, ForeColorLabel)
        oConfigForms.configurarColorControl2("Label", GroupBox2, ForeColorLabel)
        oConfigForms.configurarColorControl2("Button", GroupBox2, ForeColorButtom)

    End Sub

    ''' <summary>
    ''' modifica la presentación de la grilla Cuentas 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGVCuenta()

        dgCuentas.AllowDrop = False
        dgCuentas.AllowUserToAddRows = False
        dgCuentas.AllowUserToDeleteRows = False
        dgCuentas.ReadOnly = True

        With dgCuentas
            .Columns("idCue").Visible = False


        End With


    End Sub


    ''' <summary>
    ''' modifica la configuración de la grilla Medios de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGVMedioPago()

        dgMedioPago.AllowDrop = False
        dgMedioPago.AllowUserToAddRows = False
        dgMedioPago.AllowUserToDeleteRows = False
        dgMedioPago.ReadOnly = True

        With dgMedioPago
            .Columns("codMP").Visible = False


        End With



    End Sub

    ''' <summary>
    ''' habilita (enable) el groupBox para controles de edicon medio pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HabilitarControles()

        If dgCuentas.RowCount = 0 Then

            GroupBox2.Enabled = False

        Else
            GroupBox2.Enabled = True
        End If

    End Sub

    Private Function ValidarMedioPago() As Boolean

        Dim retorna As Boolean = True


        If validaCampoVacio(txtMedioPago.Text.Trim()) Then
            MessageBox.Show("Ingrese un medio de pago valido: ", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtMedioPago.Focus()
            txtMedioPago.SelectAll()
            retorna = False
        End If

        If BindingSource2.Find("medioP", txtMedioPago.Text.Trim()) > 0 Then
            MessageBox.Show("Ya existe el medio de pago: " & txtMedioPago.Text.Trim() & Chr(13) & "Cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtMedioPago.Focus()
            txtMedioPago.SelectAll()
            retorna = False
        End If
        Return retorna


    End Function



#End Region

#Region "Eventos"

#End Region


    Private Sub mantMedioPagoForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub

    Private Sub mantMedioPagoForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        'Cargando combo banco
        Dim query As String = "select codBan,banco from tBanco"
        oDataManager.CargarCombo(query, CommandType.Text, cbBanco, "codBan", "banco")

        HabilitarControles()

        wait.Close()
        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnEliminarBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

    End Sub
    Private Sub btnModificarBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click

    End Sub
    Private Sub btnNuevoBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

    Private Sub cbBanco_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBanco.SelectedIndexChanged

        'validando que no esté vacio el combo
        If TypeOf cbBanco.SelectedValue Is String Then
            'mostrar data en grilla
            Dim query As String = "select idCue,nroCue,codMon,codBan,estado,saldoBan  from TCuentaBan where codBan = " & cbBanco.SelectedValue
            oDataManager.CargarGrilla(query, CommandType.Text, dgCuentas, BindingSource1)
            'customiza las columnas de la grilla 
            ModificarColumnasDGVCuenta()
        End If

        'habilitando los controles de groupbox para editar info de medios de pago
        HabilitarControles()


    End Sub

    Private Sub dgCuentas_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCuentas.CellClick

        'estableciendo el foco en el textbox medio de pago
        txtMedioPago.Focus()



    End Sub
End Class