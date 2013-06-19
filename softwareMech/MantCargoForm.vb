Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantCargoForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim BindingSource1 As New BindingSource
    Private Sub MantCargoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "select codCar,cargo from TCargo order by cargo"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'llenar la tabla virtual con los dataAdapter
            daTabla1.Fill(dataTable1)

            BindingSource1.DataSource = dataTable1
            Navigator1.BindingSource = BindingSource1
            lbTabla1.DataSource = BindingSource1
            lbTabla1.DisplayMember = "cargo"
            lbTabla1.ValueMember = "codCar"

            configurarColorControl()

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub MantCargoForm_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.Close()
    End Sub

    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        Label1.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub


    Private Sub lbTabla1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTabla1.SelectedIndexChanged
        txtCar.Text = lbTabla1.Text.Trim()

    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Me.AcceptButton = Me.btnNuevo
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtCar.Text.Trim, 2) Then
            txtCar.errorProv()
            Exit Sub
        End If

        If BindingSource1.Find("cargo", txtCar.Text.Trim()) >= 0 Then
            MessageBox.Show("Ya existe cargo: " & txtCar.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCar.Focus()
            txtCar.SelectAll()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()
            Dim campo As String = txtCar.Text.Trim()
            'llamando al procedimiento k crea el comando insert
            comandoInsert()
            cmInserTable.ExecuteNonQuery()

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("cargo", campo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue guardado con exito...")
            txtCar.Focus()
            txtCar.SelectAll()
            txtCar.Text = lbTabla1.Text.Trim()
            wait.Close()
        Catch
            wait.Close()
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                'myTrans.Rollback()
                MessageBox.Show("NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim vfCampo As String
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If lbTabla1.SelectedIndex = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a actualizar...")
            Exit Sub
        End If

        Me.AcceptButton = Me.btnModificar
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtCar.Text.Trim, 2) Then
            txtCar.errorProv()
            Exit Sub
        End If
        vfCampo = lbTabla1.Text.Trim()
        If vfCampo.ToUpper() <> txtCar.Text.ToUpper().Trim() Then
            If BindingSource1.Find("cargo", txtCar.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe cargo: " & txtCar.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtCar.Focus()
                txtCar.SelectAll()
                Exit Sub
            End If
        End If
        Dim resp As String = MessageBox.Show("Esta seguro de actualizar cargo... ?" & Chr(13) & "Si actualiza todas sus dependencias heredaran" & Chr(13) & "este nombre...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtCar.Focus()
            Exit Sub
        End If

        Me.Refresh()
        Dim wait As New waitForm
        wait.Show()
        Try
            Dim campo As String = txtCar.Text.Trim()
            'llamando al procedimiento k crea el comando Update
            comandoUpdate()
            cmUpdateTable.ExecuteNonQuery()
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("cargo", campo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS2005
            StatusBarClass.messageBarraEstado("  Registro fue actualizado con exito...")
            lbTabla1.Focus()
            txtCar.Text = lbTabla1.Text.Trim()
            wait.Close()
        Catch f As Exception
            wait.Close()
            'myTrans.Rollback()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If lbTabla1.SelectedIndex = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount1(lbTabla1.SelectedValue) > 0 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, hay personal asignado a este cargo...")
            Exit Sub
        End If

        Dim resp As Integer = MessageBox.Show("Esta seguro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = 6 Then
            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTRO...")
                Me.Refresh()
                'llamando al procedimiento k crea el comando Delete
                comandoDelete()
                cmDeleteTable.ExecuteNonQuery()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
                finalMytrans = True
                'Actualizando el dataTable
                dataTable1.Clear()
                daTabla1.Fill(dataTable1)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue eliminado con exito...")
                txtCar.Text = lbTabla1.Text.Trim()
                wait.Close()
            Catch
                wait.Close()
                If finalMytrans Then
                    MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    'MessageBox.Show("ACCESO DENEGADO...NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    StatusBarClass.messageBarraEstado("  ACCESO DENEGADO...NO SE ELIMINO EL REGISTRO SELECCIONADO...")
                    'Me.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TPersonal where codCar=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TCargo(cargo) values(@car)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@car", SqlDbType.VarChar, 40).Value = txtCar.Text.Trim()
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TCargo set cargo=@car where codCar=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@car", SqlDbType.VarChar, 40).Value = txtCar.Text.Trim()
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TCargo where codCar=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
    End Sub
End Class
