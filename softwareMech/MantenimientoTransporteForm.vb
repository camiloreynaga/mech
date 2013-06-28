Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008



Public Class MantenimientoTransporteForm

#Region "Variables"

    ''' <summary>
    ''' Empresa de tranportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' command insert para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertEmpresa As SqlCommand

    ''' <summary>
    ''' command Update para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateEmpresa As SqlCommand

    ''' <summary>
    ''' command Delete para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteEmpresa As SqlCommand


    Dim vfNuevo As String = "nuevo"

    Dim vfModificar As String = "modificar"

    ''' <summary>
    ''' obtiene la razon social , de forma temporal
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfRazonSocial As String

    ''' <summary>
    ''' obtiene el ruc, de forma temporal
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfRuc As String


#End Region


#Region "Métodos"

    ''' <summary>
    ''' Carga los datos Iniciales para el form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosIniciales()
        Me.Cursor = Cursors.WaitCursor

        VerificaConexion()

        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "Select codET,nombre,ruc,dir,fono,contacto from TEmpTransp where codET > 1"
        crearDataAdapterTable(daTabla1, sele)

        Try

            crearDSAlmacen()
            daTabla1.Fill(dsAlmacen, "TEmpTransp")

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TEmpTransp"

            dgTransportes.DataSource = BindingSource0
            BindingNavigator1.BindingSource = BindingSource0


        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default

        End Try



    End Sub


    ''' <summary>
    ''' Inserta datos en la tabla TEmpTransp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoInsertTransporte()

        cmInsertEmpresa = New SqlCommand
        cmInsertEmpresa.CommandType = CommandType.Text
        cmInsertEmpresa.CommandText = "insert TEmpTransp values (@nombre,@ruc,@dir,@fono,@contacto)"
        cmInsertEmpresa.Connection = Cn
        cmInsertEmpresa.Parameters.Add("@nombre", SqlDbType.VarChar, 60).Value = txtRazonSocial.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDireccion.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtTelefono.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@contacto", SqlDbType.VarChar, 60).Value = txtContacto.Text.Trim

    End Sub

    ''' <summary>
    ''' Actualiza datos de la tabla TEmpTransp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoUpdateTransporte()
        cmUpdateEmpresa = New SqlCommand
        cmUpdateEmpresa.CommandType = CommandType.Text
        cmUpdateEmpresa.CommandText = "update TEmpTransp set nombre=@nombre, ruc=@ruc,dir=@dir,fono=@fono,contacto=@contacto Where codET=@codET"
        cmUpdateEmpresa.Connection = Cn
        cmUpdateEmpresa.Parameters.Add("@nombre", SqlDbType.VarChar, 60).Value = txtRazonSocial.Text.Trim()
        cmUpdateEmpresa.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmUpdateEmpresa.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDireccion.Text.Trim()
        cmUpdateEmpresa.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtTelefono.Text.Trim()
        cmUpdateEmpresa.Parameters.Add("@contacto", SqlDbType.VarChar, 60).Value = txtContacto.Text.Trim
        'Obteniendo el código (id) de la empresa de transportes
        cmUpdateEmpresa.Parameters.Add("@codET", SqlDbType.Int).Value = dgTransportes.Rows(BindingSource0.Position).Cells("codET").Value

    End Sub

    ''' <summary>
    ''' Elimina un registro de la tabla TEmpTransp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub comandoDeleteTransporte()
        cmDeleteEmpresa = New SqlCommand
        cmDeleteEmpresa.CommandType = CommandType.Text
        cmDeleteEmpresa.CommandText = "Delete from TEmpTransp Where codET=@codET"
        cmDeleteEmpresa.Connection = Cn
        'Obteniendo el código (id) de la empresa de transportes
        cmDeleteEmpresa.Parameters.Add("@codET", SqlDbType.Int).Value = dgTransportes.Rows(BindingSource0.Position).Cells("codET").Value

    End Sub


    ''' <summary>
    ''' Enlazar los datos de la Grilla con los controles del form 
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub enlazartext()
        If dgTransportes.Rows.Count = 0 Then
            'If BindingSource0.Position = -1 Then
            Exit Sub
        Else
            txtRazonSocial.Text = BindingSource0.Item(BindingSource0.Position)(1) ' Razon Social
            txtRuc.Text = BindingSource0.Item(BindingSource0.Position)(2) 'Ruc
            txtDireccion.Text = BindingSource0.Item(BindingSource0.Position)(3) ' Direccion
            txtTelefono.Text = BindingSource0.Item(BindingSource0.Position)(4) 'Telefono
            txtContacto.Text = BindingSource0.Item(BindingSource0.Position)(5) ' contacto
        End If

    End Sub

    ''' <summary>
    ''' limpia los datos de los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimpiarText()

        txtRazonSocial.Clear()
        txtRuc.Clear()
        txtDireccion.Clear()
        txtTelefono.Clear()
        txtContacto.Clear()

    End Sub

    ''' <summary>
    ''' valida el registro de datos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarCampos() As Boolean

        If validaCampoVacioMinCaracNoNumer(txtRazonSocial.Text.Trim, 3) Then
            MessageBox.Show("Debe ingresar un Nombre de Empresa de transporte valido")
            txtRazonSocial.Focus()
            txtRazonSocial.SelectAll()

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
        End If

        Return False
    End Function

    ''' <summary>
    ''' desactiva los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DesactivarControles()
        'GroupBox2.Enabled = False
        dgTransportes.Enabled = False

        For index As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(index) Is TextBox Then

                GroupBox1.Controls(index).Enabled = True


            End If

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
    ''' <summary>
    ''' activa los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ActivarControles()
        dgTransportes.Enabled = True

        'GroupBox1.Enabled = False

        For index As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(index) Is TextBox Then
                GroupBox1.Controls(index).Enabled = False
            End If
        Next

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

    ''' <summary>
    ''' Configura los colores del Form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()

        Me.BackColor = BackColorP
        'dando color a los labels
        For index As Integer = 0 To GroupBox1.Controls.Count - 1
            If TypeOf GroupBox1.Controls(index) Is Label Then

                GroupBox1.Controls(index).ForeColor = ForeColorLabel

            End If
        Next
        'Dando color a los botones
        For index As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(index) Is Button Then

                Me.Controls(index).ForeColor = ForeColorButtom
            End If
        Next

    End Sub

    Private Sub ModificarColumnasDGV()
        Dim oConfigGrilla As New cConfigFormControls

        'dgTransportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        With dgTransportes
            .Columns("codET").Visible = False
            .Columns("nombre").HeaderText = "Razon Social"
            .Columns("nombre").Width = 160
            .Columns("ruc").HeaderText = "RUC"
            .Columns("ruc").Width = 90
            .Columns("dir").HeaderText = "Dirección"
            .Columns("dir").Width = 260
            .Columns("fono").HeaderText = "Teléfonos"
            .Columns("fono").Width = 120
            .Columns("contacto").HeaderText = "Contacto"
            .Columns("contacto").Width = 160
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        oConfigGrilla.ConfigGrilla(dgTransportes)





        dgTransportes.AllowDrop = False
        dgTransportes.AllowUserToAddRows = False
        dgTransportes.AllowUserToDeleteRows = False
        dgTransportes.ReadOnly = True



    End Sub


    Private Function recuperarOrdenCompra(ByVal cod As Integer) As Integer

        Dim cmdComando As SqlCommand = New SqlCommand
        cmdComando.CommandType = CommandType.Text
        cmdComando.CommandText = "SELECT count(nroOrden) from TOrdenCompra where codET =" & cod
        cmdComando.Connection = Cn
        Return cmdComando.ExecuteScalar


    End Function


    ''' <summary>
    ''' Recupera la cantidad de Vehiculos relacionados con la empresa de transporte
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarVehiculo(ByVal cod As Integer) As Integer

        Dim cmdComando As SqlCommand = New SqlCommand
        cmdComando.CommandType = CommandType.Text
        cmdComando.CommandText = "SELECT count(codVeh) from TVehiculo where codET =" & cod
        cmdComando.Connection = Cn
        Return cmdComando.ExecuteScalar

    End Function
    ''' <summary>
    ''' Recupera la cantidad de Transportistas relacionados con la empresa de transporte
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function recuperarTransportista(ByVal cod As Integer) As Integer

        Dim cmdComando As SqlCommand = New SqlCommand
        cmdComando.CommandType = CommandType.Text
        cmdComando.CommandText = "SELECT count(codT) from TTransportista Where codET=" & cod
        cmdComando.Connection = Cn
        Return cmdComando.ExecuteScalar

    End Function


#End Region




    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        If vfNuevo = "nuevo" Then
            vfNuevo = "guardar"
            Me.btnNuevo.Text = "Guardar"

            'metodo para desactivar controles
            DesactivarControles()

            'método para limpiar 
            LimpiarText()

            txtRazonSocial.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo

        Else 'Guardar
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


            'MessageBox.Show(BindingSource0.Item(1).ToString())
            If BindingSource0.Find("nombre", txtRazonSocial.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe Empresa de Transporte: " & txtRazonSocial.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtRazonSocial.Focus()
                txtRazonSocial.SelectAll()
                Exit Sub

            End If

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try

                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()
                vfRazonSocial = txtRazonSocial.Text.Trim

                'obteniendo datos de empresa de transporte
                comandoInsertTransporte()
                cmInsertEmpresa.Transaction = myTrans
                If cmInsertEmpresa.ExecuteNonQuery < 1 Then
                    ' wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'Confirma transacción
                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMyTrans = True

                'actualizando el DataSet
                dsAlmacen.Tables("TEmpTransp").Clear()
                daTabla1.Fill(dsAlmacen, "TEmpTransp")

                btnCancelar.PerformClick()
                'ubicando al ítem agregado en la grilla
                BindingSource0.Position = BindingSource0.Find("nombre", vfRazonSocial)

                'mostrando mensaje de estado
                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")
                wait.Close()

            Catch f As Exception
                wait.Close()
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
            If dgTransportes.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"

            'Desactivar controles
            DesactivarControles()

            'limpiarText
            'LimpiarText()

            'Enlazartext
            txtRazonSocial.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar

        Else
            'If ValidarCampos() Then
            '    Exit Sub
            'End If

            'revisa la duplicidad de ruc

            If txtRuc.Text.Length > 0 Then
                vfRuc = dgTransportes.Rows(BindingSource0.Position).Cells(2).Value
                If vfRuc <> txtRuc.Text.Trim() Then
                    If BindingSource0.Find("ruc", txtRuc.Text.Trim()) >= 0 Then
                        MessageBox.Show("Ya existe ese número de RUC: " & txtRuc.Text.Trim() & Chr(13) & "Cambie de RUC... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                        txtRuc.Focus()
                        txtRuc.SelectAll()
                        Exit Sub
                    End If
                End If
            End If


            'revisa la duplicidad de Nombre
            vfRazonSocial = dgTransportes.Rows(BindingSource0.Position).Cells(1).Value
            If vfRazonSocial.ToUpper() <> txtRazonSocial.Text.ToUpper.Trim() Then
                If BindingSource0.Find("nombre", txtRazonSocial.Text.Trim()) >= 0 Then
                    MessageBox.Show("Ya existe Empresa de Transporte: " & txtRazonSocial.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                    txtRazonSocial.Focus()
                    txtRazonSocial.SelectAll()
                    Exit Sub
                End If
            End If



            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")
                comandoUpdateTransporte()
                cmUpdateEmpresa.Transaction = myTrans
                If cmUpdateEmpresa.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    'wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                vfRazonSocial = txtRazonSocial.Text.Trim
                myTrans.Commit()
                finalMyTrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el Dataset

                dsAlmacen.Tables("TEmpTransp").Clear()
                daTabla1.Fill(dsAlmacen, "TEmpTransp")

                btnCancelar.PerformClick()

                'ubicando al ítem agregado en la grilla
                BindingSource0.Position = BindingSource0.Find("nombre", vfRazonSocial)

                'mostrando mensaje de estado
                StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")
                ' wait.Close()

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



    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        If dgTransportes.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        ' validación de eliminacione

        'validar relacion con Orden de Compra
        If recuperarOrdenCompra(dgTransportes.Rows(BindingSource0.Position).Cells("codET").Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... EMPRESA DE TRANSPORTES TIENE COMPRAS ASIGNADOS...")
            Exit Sub
        End If

        ' Validad relación con vehiculos
        If recuperarVehiculo(dgTransportes.Rows(BindingSource0.Position).Cells("codET").Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... EMPRESA DE TRANSPORTES TIENE VEHICULOS ASIGNADOS...")
            Exit Sub
        End If
        'Valida relación con Transportistas
        If recuperarTransportista(dgTransportes.Rows(BindingSource0.Position).Cells("codET").Value) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... EMPRESA DE TRANSPORTES TIENE TRANSPORTISTAS ASIGNADOS...")
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
                'obtienedo datos
                comandoDeleteTransporte()
                cmDeleteEmpresa.Transaction = myTrans
                If cmDeleteEmpresa.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar Empresa de transporte porque está compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                Me.Refresh()

                'Confirma Transacción
                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
                finalMytrans = True

                'Actualizando DataSet

                dsAlmacen.Tables("TEmpTransp").Clear()
                daTabla1.Fill(dsAlmacen, "TEmpTransp")
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

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTransportes.Dispose()
        Me.Close()




    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        ActivarControles()
        enlazartext()

        StatusBarClass.messageBarraEstado("  Proceso cancelado...")

    End Sub

    Private Sub MantenimientoTransporteForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DatosIniciales()
        ActivarControles()
        enlazartext()

        ModificarColumnasDGV()
        configurarColorControl()


    End Sub

    Private Sub txtRuc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRuc.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub dgTransportes_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTransportes.CellClick
        enlazartext()
        'txt.Text = ""
        'txtCon1.Text = ""
    End Sub

    Private Sub dgTransportes_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTransportes.CellEnter
        enlazartext()
    End Sub

    Private Sub MantenimientoTransporteForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.Close()
    End Sub
End Class
