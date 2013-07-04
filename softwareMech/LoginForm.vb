Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class LoginForm
    Dim bindingSource1 As New BindingSource()

    Private Sub LoginForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        btnFecha.Text = Now.Date.ToLongDateString
        Me.Refresh()
        txtUsu.Focus()
    End Sub

    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Me.Text = " Accéso a la Solución..."

        Dim sele As String = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTabla1, sele)

        'Instanciar una tabla de datos en memoria en ves de dataSet
        Dim dataTable1 As New DataTable()

        'llenar la tabla virtual con los dataAdapter
        daTabla1.Fill(dataTable1)

        bindingSource1.DataSource = dataTable1
        Navigator1.BindingSource = bindingSource1
        dgTabla2.DataSource = bindingSource1
        ModificarColumnasDGV()

        dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgTabla2.AllowUserToOrderColumns = False
        dgTabla2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
        dgTabla2.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable

        'Ocultando el botón Acceder
        btnAcceder.Visible = False

        Me.Cursor = Cursors.Default

    End Sub

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub LoginForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'MsgBox("pintando formulario")
        'colorear()
    End Sub

    Private Sub colorear()
        For j As Short = 0 To bindingSource1.Count - 1
            'dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.FromName(bindingSource1.Item(j)(3))
            dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.FromName(bindingSource1.Item(j)(3))
            dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            dgTabla2.Refresh()
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).HeaderText = "Código"
            .Columns(0).Width = 50
            .Columns(1).Width = 370
            .Columns(1).HeaderText = "Obra / Ubicación"
            .Columns(2).Width = 300
            .Columns(2).HeaderText = "Lugar / Dirección"
            .Columns(3).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If txtUsu.Text.Trim() = "" Then
            MessageBox.Show("DATO INVALIDO, DIGITE USUARIO VALIDO...", "SSP SAC", Nothing, MessageBoxIcon.Error)
            txtUsu.Focus()
            Exit Sub
        End If

        If txtCon.Text.Trim() = "" Then
            MessageBox.Show("DATO INVALIDO, DIGITE CONTRASEÑA VALIDA...", "SSP SAC", Nothing, MessageBoxIcon.Error)
            txtCon.Focus()
            Exit Sub
        End If

        'Funcion recuperarUsu... creado en el modulo varFuncPublicasModule
        Dim codigo As Object = recuperarUsu(txtUsu.Text.Trim(), txtCon.Text.Trim())
        vSUsuario = txtUsu.Text.Trim()
        If codigo >= 1 Then
            vPass = codigo
            vSCodTipoUsu = recuperarCodTipoUsu(vPass)   '1=admi     2=subAdministrador
            'recupara y valida el tipo de cargo
            Dim _tipoCargo As Integer = recuperarTipoCargo(vPass) ' 0 =coporativo 1 =Obra

            'If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Then
            If _tipoCargo = 0 Then
               

                ' Configurando el tamaño y localización de botones
                Me.Size = New Size(480, 400)
                'OK.Location = New Point(224, 339)
                OK.Visible = False

                btnAcceder.Visible = True
                btnAcceder.Location = New Point(307, 339)
                Cancel.Location = New Point(393, 339)

                'seleccionar sucursal
                dgTabla2.Visible = True
                Label1.Visible = True
                Navigator1.Visible = True
                colorear()


                dgTabla2.Enabled = True
                btnAcceder.Enabled = True
                btnAcceder.FlatStyle = FlatStyle.Standard
                OK.Enabled = False
                OK.FlatStyle = FlatStyle.Flat
                txtUsu.ReadOnly = True
                txtCon.ReadOnly = True
                Me.AcceptButton = Me.btnAcceder
                btnAcceder.Focus()
                Exit Sub
            Else    'usuario
                vSCodigo = recuperarCodigo(vPass)
                bindingSource1.Position = bindingSource1.Find("codigo", vSCodigo)
                vSNomSuc = bindingSource1.Item(bindingSource1.Position)(1)
                vSDirSuc = bindingSource1.Item(bindingSource1.Position)(2)
            End If
        Else
            vPass = 0
        End If

        If vPass = 0 Then
            If verificaSiExisteNomUsuario(txtUsu.Text.Trim()) = 0 Then
                MessageBox.Show("DATO INVALIDO, DIGITE USUARIO VALIDO...", "SSP SAC", Nothing, MessageBoxIcon.Error)
                txtUsu.Focus()
            Else
                MessageBox.Show("DATO INVALIDO, DIGITE CONTRASEÑA VALIDA...", "SSP SAC", Nothing, MessageBoxIcon.Error)
                txtCon.Focus()
            End If
            Exit Sub
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        vPass = 0
        Me.Close()
    End Sub

    Private Sub btnAcceder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcceder.Click
        If vPass = 0 Then
            MessageBox.Show("DATOS INVALIDOS, DIGITE USUARIO Y CONTRASEÑA VALIDOS...", "SSP SAC", Nothing, MessageBoxIcon.Information)
            txtUsu.Focus()
            Exit Sub
        Else
            vSCodigo = bindingSource1.Item(bindingSource1.Position)(0)
            vSNomSuc = bindingSource1.Item(bindingSource1.Position)(1)
            vSDirSuc = bindingSource1.Item(bindingSource1.Position)(2)
            Me.Close()
        End If
    End Sub

    Private Sub btnFecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFecha.Click

        Process.Start("timedate.cpl")

    End Sub

    Private Sub txtCon_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCon.GotFocus
        txtCon.SelectAll()
    End Sub

    Private Sub txtCon_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtCon.MouseClick
        txtCon.SelectAll()
    End Sub

    Private Sub txtUsu_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsu.GotFocus
        txtUsu.SelectAll()
    End Sub

    Private Sub txtUsu_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtUsu.MouseClick
        txtUsu.SelectAll()
    End Sub

    Private Sub dgTabla2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla2.CellClick
        btnAcceder.Focus()
    End Sub

#Region "metodos"
    ''' <summary>
    ''' recupera el tipo de cargo 
    ''' </summary>
    ''' <param name="codUsu">codigo de personal</param>
    ''' <returns>0= corporativo 1 = obra</returns>
    ''' <remarks></remarks>
    Public Function recuperarTipoCargo(ByVal codUsu As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select TTU.tipoCargo from TTipoUsu TTU join TPersonal TU on TTU.codTipU=TU.codTipU where codPers=" & codUsu
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
#End Region

    ''' <summary>
    ''' Dando Acceso al doble Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgTabla2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla2.CellDoubleClick

        btnAcceder.PerformClick() '(sender, e)
    End Sub
End Class
