Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantAreaMatForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim BindingSource1 As New BindingSource

    Private Sub MantAreaMatForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantAreaMatForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "select codAreaM,areaM from TAreaMat order by areaM"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'llenar la tabla virtual con los dataAdapter
            daTabla1.Fill(dataTable1)

            BindingSource1.DataSource = dataTable1
            Navigator1.BindingSource = BindingSource1
            lbTabla1.DataSource = BindingSource1
            lbTabla1.DisplayMember = "areaM"
            lbTabla1.ValueMember = "codAreaM"

            configurarColorControl()

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub lbTabla1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTabla1.SelectedIndexChanged
        txtCam.Text = lbTabla1.Text.Trim()
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

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Me.AcceptButton = Me.btnNuevo
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtCam.Text.Trim, 2) Then
            txtCam.errorProv()
            Exit Sub
        End If

        If BindingSource1.Find("areaM", txtCam.Text.Trim()) >= 0 Then
            MessageBox.Show("Ya exíste area insumos: " & txtCam.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCam.Focus()
            txtCam.SelectAll()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()
            Dim campo As String = txtCam.Text.Trim()
            'llamando al procedimiento k crea el comando insert
            comandoInsert()
            cmInserTable.ExecuteNonQuery()

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("areaM", campo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            txtCam.Focus()
            txtCam.SelectAll()
            txtCam.Text = lbTabla1.Text.Trim()
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
        If validaCampoVacioMinCaracNoNumer(txtCam.Text.Trim, 2) Then
            txtCam.errorProv()
            Exit Sub
        End If
        vfCampo = lbTabla1.Text.Trim()
        If vfCampo.ToUpper() <> txtCam.Text.ToUpper().Trim() Then
            If BindingSource1.Find("areaM", txtCam.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste area insumos: " & txtCam.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtCam.Focus()
                txtCam.SelectAll()
                Exit Sub
            End If
        End If
        Dim resp As String = MessageBox.Show("Esta segúro de actualizar area de insumos... ?" & Chr(13) & "Si actualiza todas sus dependencias heredaran" & Chr(13) & "este nombre...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtCam.Focus()
            Exit Sub
        End If

        Me.Refresh()
        Dim wait As New waitForm
        wait.Show()
        Try
            Dim campo As String = txtCam.Text.Trim()
            'llamando al procedimiento k crea el comando Update
            comandoUpdate()
            cmUpdateTable.ExecuteNonQuery()
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("areaM", campo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS2005
            StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
            lbTabla1.Focus()
            txtCam.Text = lbTabla1.Text.Trim()
            wait.Close()
        Catch f As Exception
            wait.Close()
            'myTrans.Rollback()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TMaterial where codAreaM=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If lbTabla1.SelectedIndex = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount1(lbTabla1.SelectedValue) > 0 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, area de insumo tiene insumos...")
            Exit Sub
        End If

        Dim resp As Integer = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
                StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
                txtCam.Text = lbTabla1.Text.Trim()
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

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TAreaMat(areaM) values(@cam)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@cam", SqlDbType.VarChar, 40).Value = txtCam.Text.Trim()
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TAreaMat set areaM=@cam where codAreaM=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@cam", SqlDbType.VarChar, 40).Value = txtCam.Text.Trim()
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TAreaMat where codAreaM=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbTabla1.SelectedValue
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub
End Class
