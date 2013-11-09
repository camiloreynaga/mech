Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008




Public Class MantProveedor

#Region "varianbles"



    ''' <summary>
    ''' Binding Source para proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' instancia de la clase datamanager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' instancia de la clase cConfigFormsControls
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' comando insert proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertProveedor As SqlCommand
    ''' <summary>
    ''' comando update proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateProveedor As SqlCommand
    ''' <summary>
    ''' comando delete proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteProveedor As SqlCommand

    Dim vfNuevo As String = "nuevo"

    Dim vfModificar As String = "modificar"

    Dim vfRazon As String

    Dim vfRuc As String





#End Region

#Region "métodos"


    'dando color al form

    ''' <summary>
    ''' Configura los controles de form con colores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarColorControl()

        BackColor = BackColorP
        'Dando color a los controles de groupBox
        oGrilla.configurarColorControl("Label", GroupBox1, ForeColorLabel)
        oGrilla.configurarColorControl("RadioButton", GroupBox1, ForeColorLabel)

        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        'btnAceptar.ForeColor = ForeColorButtom
        'btnQuitar.ForeColor = ForeColorButtom


    End Sub

    Private Sub ModificarColumnaDGV()

        dgProveedor.ReadOnly = True
        dgProveedor.AllowDrop = False
        dgProveedor.AllowUserToAddRows = False
        dgProveedor.AllowUserToDeleteRows = False

        With dgProveedor

            .Columns("codProv").Visible = False

            .Columns("razon").HeaderText = "Proveedor"
            .Columns("razon").Width = 300
            .Columns("ruc").HeaderText = "RUC"
            .Columns("ruc").Width = 100
            .Columns("dir").HeaderText = "Dirección"
            .Columns("dir").Width = 200
            .Columns("fono").HeaderText = "Teléfono"
            .Columns("fono").Width = 150
            .Columns("email").HeaderText = "Email"
            .Columns("email").Width = 100
            .Columns("repres").HeaderText = "Representante"
            .Columns("repres").Width = 120
            .Columns("estado").HeaderText = "Estado"
            .Columns("estado").Width = 70
            .Columns("codEstado").Visible = False
            .Columns("cuentaBan").HeaderText = "Cta. Bancaria"
            .Columns("cuentaBan").Width = 150
            .Columns("cuentaDet").HeaderText = "Cta. Detracciones"
            .Columns("cuentaDet").Width = 150


        End With


    End Sub


    ''' <summary>
    ''' inserta datos en la tabla proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ComandoInsertProveedor()

        cmInsertProveedor = New SqlCommand
        cmInsertProveedor.CommandType = CommandType.Text
        cmInsertProveedor.CommandText = "Insert TProveedor values (@razon,@ruc,@dir,@fono,@email,@repres,@estado,@cuentaBan,@cuentaDet)"
        cmInsertProveedor.Connection = Cn
        cmInsertProveedor.Parameters.Add("@razon", SqlDbType.VarChar, 60).Value = txtRazon.Text.Trim()
        cmInsertProveedor.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmInsertProveedor.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDireccion.Text.Trim()
        cmInsertProveedor.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmInsertProveedor.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = txtEmail.Text.Trim()
        cmInsertProveedor.Parameters.Add("@repres", SqlDbType.VarChar, 60).Value = txtRepresentante.Text.Trim()

        If rdActivo.Checked = True Then
            cmInsertProveedor.Parameters.Add("@estado", SqlDbType.Int).Value = 1
        Else
            cmInsertProveedor.Parameters.Add("@estado", SqlDbType.Int).Value = 0
        End If
        cmInsertProveedor.Parameters.Add("@cuentaBan", SqlDbType.VarChar, 60).Value = txtCtaBan.Text.Trim()
        cmInsertProveedor.Parameters.Add("@cuentaDet", SqlDbType.VarChar, 60).Value = txtDetraccion.Text.Trim

    End Sub
    ''' <summary>
    ''' actualiza los datos de la tabla proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ComandoUpdateProveedor()
        cmUpdateProveedor = New SqlCommand
        cmUpdateProveedor.CommandType = CommandType.Text
        cmUpdateProveedor.CommandText = "update TProveedor set razon=@razon,ruc=@ruc,dir=@dir,fono=@fono,email=@email,repres=@repres,estado=@estado,cuentaBan=@cuentaBan,cuentaDet=@cuentaDet where codProv=@codProv"
        cmUpdateProveedor.Connection = Cn
        cmUpdateProveedor.Parameters.Add("@razon", SqlDbType.VarChar, 60).Value = txtRazon.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDireccion.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtFono.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = txtEmail.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@repres", SqlDbType.VarChar, 60).Value = txtRepresentante.Text.Trim()

        If rdActivo.Checked = True Then
            cmUpdateProveedor.Parameters.Add("@estado", SqlDbType.Int).Value = 1
        Else
            cmUpdateProveedor.Parameters.Add("@estado", SqlDbType.Int).Value = 0
        End If
        cmUpdateProveedor.Parameters.Add("@cuentaBan", SqlDbType.VarChar, 60).Value = txtCtaBan.Text.Trim()
        cmUpdateProveedor.Parameters.Add("@cuentaDet", SqlDbType.VarChar, 60).Value = txtDetraccion.Text.Trim
        cmUpdateProveedor.Parameters.Add("@codProv", SqlDbType.Int).Value = BindingSource0.Item(BindingSource0.Position)(0)


    End Sub
    ''' <summary>
    ''' elimina un ítem de la tabla proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ComandoDeleteProveedor()
        cmDeleteProveedor = New SqlCommand
        cmDeleteProveedor.CommandType = CommandType.Text
        cmDeleteProveedor.CommandText = "Delete from TProveedor where codProv = @codProv "
        cmDeleteProveedor.Connection = Cn
        cmDeleteProveedor.Parameters.Add("@codProv", SqlDbType.Int).Value = BindingSource0.Item(BindingSource0.Position)(0)
    End Sub

    Private Sub EnlazarText()

        If dgProveedor.RowCount = 0 Then

            Exit Sub
        Else
            txtRazon.Text = BindingSource0.Item(BindingSource0.Position)(1)
            txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(2)
            txtDireccion.Text = BindingSource0.Item(BindingSource0.Position)(3)
            txtFono.Text = BindingSource0.Item(BindingSource0.Position)(4)
            txtEmail.Text = BindingSource0.Item(BindingSource0.Position)(5)
            txtRepresentante.Text = BindingSource0.Item(BindingSource0.Position)(6)

            If BindingSource0.Item(BindingSource0.Position)(8) = 1 Then
                rdActivo.Checked = True
            Else
                rdInactivo.Checked = True
            End If
            txtCtaBan.Text = BindingSource0.Item(BindingSource0.Position)(9)
            txtDetraccion.Text = BindingSource0.Item(BindingSource0.Position)(10)
        End If

    End Sub
    ''' <summary>
    ''' reestablece los controles al estado inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimpiarText()

        For i As Integer = 0 To GroupBox1.Controls.Count - 1

            If TypeOf GroupBox1.Controls(i) Is TextBox Then
                GroupBox1.Controls(i).Text = ""
            End If
        Next
        rdActivo.Checked = True
    End Sub

    Private Function ValidarCampos() As Boolean

        If validaCampoVacioMinCaracNoNumer(txtRazon.Text.Trim(), 3) Then
            MessageBox.Show("Debe ingresar un Nombre de Empresa de valido")
            txtRazon.Focus()
            txtRazon.SelectAll()

            Return True
        End If

        If ValidaRUC(txtRuc.Text.Trim) Then
            MessageBox.Show("Debe ingresar un RUC valido")
            txtRuc.Focus()
            txtRuc.SelectAll()

            Return True

        End If

        If validaCampoVacioMinCaracNoNumer(txtDireccion.Text.Trim(), 3) Then
            MessageBox.Show("Debe ingresar una Dirección valida")
            txtDireccion.Focus()
            txtDireccion.SelectAll()
            Return True
        End If

        Return False
    End Function



    ''' <summary>
    ''' desactiva los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DesactivarControles()
        dgProveedor.Enabled = False

        For index As Integer = 0 To GroupBox1.Controls.Count - 1

            If TypeOf GroupBox1.Controls(index) Is TextBox Then
                GroupBox1.Controls(index).Enabled = True

            End If
            rdActivo.Enabled = True
            rdInactivo.Enabled = True

        Next

        If vfNuevo = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat

        Else
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
        End If

        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat

        GroupBox1.Enabled = True

    End Sub

    Private Sub ActivarControles()

        dgProveedor.Enabled = True
        For index As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(index) Is TextBox Then
                GroupBox1.Controls(index).Enabled = False
            End If
        Next
        rdActivo.Enabled = False
        rdInactivo.Enabled = False

        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard

    End Sub


    Private Sub ConsultarProveedor()
        Dim query As String = "select codProv,razon,ruc,dir,fono,email,repres, estado=Case when estado=1 then 'ACTIVO' else 'INACTIVO'  end,estado as codEstado,cuentaBan,cuentaDet from tProveedor"
        oDataManager.CargarGrilla(query, CommandType.Text, dgProveedor, BindingSource0)
        ModificarColumnaDGV()
    End Sub

#End Region





#Region "Eventos"

    Private Sub MantProveedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Iniciando Form
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        'Dando color
        ConfigurarColorControl()

        'consulta Db 
        'Dim query As String = "select codProv,razon,ruc,dir,fono,email,repres, estado=Case when estado=0 then 'ACTIVO' else 'INACTIVO'  end,estado as codEstado,cuentaBan,cuentaDet from tProveedor"
        'oDataManager.CargarGrilla(query, CommandType.Text, dgProveedor, BindingSource0)

        ConsultarProveedor()

        EnlazarText()
        'activa los controles de frm necesarios
        ActivarControles()


        'Cerrando form de espera y reestableciendo cursor
        wait.Close()
        Me.Cursor = Cursors.Default



    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

#End Region


    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        If vfNuevo = "nuevo" Then
            vfNuevo = "guardar"
            Me.btnNuevo.Text = "Guardar"
            'Desactiva controles
            DesactivarControles()

            'limpiando controles
            LimpiarText()
            txtRazon.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else

            If ValidarCampos() Then
                Exit Sub
            End If

            'valida duplicidad
            If txtRuc.Text.Length > 0 Then
                If BindingSource0.Find("ruc", txtRuc.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya existe el número de RUC: " & txtRuc.Text.Trim() & Chr(13) & "Cambie de RUC... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtRuc.Focus()
                    txtRuc.SelectAll()
                    Exit Sub
                End If

            End If

            If BindingSource0.Find("razon", txtRazon.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe Empresa: " & txtRazon.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtRazon.Focus()
                txtRazon.SelectAll()
                Exit Sub
            End If


            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()
                vfRazon = txtRazon.Text.Trim

                ComandoInsertProveedor()

                cmInsertProveedor.Transaction = myTrans
                If cmInsertProveedor.ExecuteNonQuery < 1 Then

                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub

                End If

                myTrans.Commit()

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMyTrans = True

                'actualizar Grilla
                dgProveedor.DataSource = ""
                ConsultarProveedor()
                'llamando al btn cancelar
                btnCancelar.PerformClick()
                BindingSource0.Position = BindingSource0.Find("razon", vfRazon)
            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Finally
                wait.Close()
            End Try
        End If



    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click

        If vfModificar = "modificar" Then
            If dgProveedor.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"


            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"

            'desactivar controles
            DesactivarControles()

            txtRazon.Focus()

            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar

        Else

            If txtRuc.Text.Length > 0 Then
                vfRuc = dgProveedor.Rows(BindingSource0.Position).Cells(2).Value
                If vfRuc <> txtRuc.Text.Trim() Then
                    If BindingSource0.Find("ruc", txtRuc.Text.Trim()) >= 0 Then
                        MessageBox.Show("Ya existe ese número de RUC: " & txtRuc.Text.Trim() & Chr(13) & "Cambie de RUC... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                        txtRuc.Focus()
                        txtRuc.SelectAll()
                        Exit Sub
                    End If
                End If
            End If

            vfRazon = dgProveedor.Rows(BindingSource0.Position).Cells(1).Value
            If vfRazon.ToUpper() <> txtRazon.Text.ToUpper.Trim() Then
                If BindingSource0.Find("razon", txtRazon.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya existe Empresa de Transporte: " & txtRazon.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtRazon.Focus()
                    txtRazon.SelectAll()
                    Exit Sub
                End If
            End If

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")
                ComandoUpdateProveedor()
                cmUpdateProveedor.Transaction = myTrans

                If cmUpdateProveedor.ExecuteNonQuery < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()

                End If

                vfRazon = txtRazon.Text.Trim
                myTrans.Commit()
                finalMyTrans = True

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'actualizando grilla
                dgProveedor.DataSource = ""
                ConsultarProveedor()

                btnCancelar.PerformClick()

                'ubicando item modificado
                BindingSource0.Position = BindingSource0.Find("razon", vfRazon)
                '
                StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")

            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

            Finally
                wait.Close()

            End Try


        End If


    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        ActivarControles()
        EnlazarText()

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        If dgProveedor.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim resultado As DialogResult = MessageBox.Show("Está seguro de eliminar esté registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resultado = Windows.Forms.DialogResult.Yes Then

            Dim finalMytrans As Boolean = False
            'creando una instancia de transaccion 
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")

                ComandoDeleteProveedor()
                cmDeleteProveedor.Transaction = myTrans
                If cmDeleteProveedor.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar Empresa de transporte porque está compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                Me.Refresh()

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
                finalMytrans = True

                dgProveedor.DataSource = ""
                ConsultarProveedor()
                StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
            Catch f As Exception
                If finalMytrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
                End If
            Finally
                wait.Close()

            End Try
        Else
            Exit Sub



        End If




    End Sub

    Private Sub rdActivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdActivo.CheckedChanged, rdInactivo.CheckedChanged
        If rdActivo.Checked = True Then
            rdInactivo.Checked = False
        End If

        If rdInactivo.Checked = True Then
            rdActivo.Checked = False
        End If

    End Sub

    Private Sub dgProveedor_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgProveedor.CellClick, dgProveedor.CellEnter
        EnlazarText()
    End Sub
End Class
