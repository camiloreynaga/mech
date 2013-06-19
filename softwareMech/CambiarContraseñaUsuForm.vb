Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class CambiarContraseñaUsuForm

    Private Sub CambiarContraseñaUsuForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub CambiarContraseñaUsuForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        configurarColorControl()
        'cbTipo.SelectedIndex = 1  'Tipo Usuario
        txtUsu.Text = vSUsuario.Trim()
        txtUsu1.Text = vSUsuario.Trim()
    End Sub

    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        Label1.ForeColor = ForeColorLabel
        Label2.ForeColor = ForeColorLabel
        Label3.ForeColor = ForeColorLabel
        Label4.ForeColor = ForeColorLabel
        Label5.ForeColor = ForeColorLabel
        btnAceptar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
    End Sub

    Private Function ValidarCampos() As Boolean
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtUsu1.Text.Trim(), 3) Then
            txtUsu1.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 3 caracteres...")
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtCon.Text.Trim(), 5) Then
            txtCon.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 5 caracteres y alfanumericos...")
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtCon1.Text.Trim(), 5) Then
            txtCon1.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 5 caracteres y alfanumericos...")
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtCon2.Text.Trim(), 5) Then
            txtCon2.errorProv()
            StatusBarClass.messageBarraEstado("  Digite minimo 5 caracteres y alfanumericos...")
            Return True
        End If
        If ValidaContraseñaRepetidaIgual(txtCon1.Text.Trim(), txtCon2.Text.Trim()) Then
            MessageBox.Show("Error contraseñas diferentes, digite contraseñas iguales...", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtCon1.Focus()
            Return True
        End If
        'Todo OK
        Return False
    End Function

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Funcion recuperarUsu... creado en el modulo varFuncPublicasModule
        Dim codigo As Object = recuperarUsu(txtUsu.Text.Trim(), txtCon.Text.Trim())
        If codigo >= 1 Then
            'Usuario valido
        Else
            MessageBox.Show("CONTRASEÑA ACTUAL INVALIDA", nomNegocio, Nothing, MessageBoxIcon.Error)
            txtCon.Focus()
            txtCon.SelectAll()
            Exit Sub
        End If
        If ValidarCampos() Then
            Exit Sub
        End If
        'Funcion recuperarUsu... creado en el modulo varFuncPublicasModule
        Dim codigo1 As Object = recuperarUsu1(txtUsu1.Text.Trim(), txtCon1.Text.Trim())
        If codigo1 >= 1 Then       'Ya existe usuario con la contrasseña
            MessageBox.Show("YA EXISTE USUARIO CON LA CONTRASEÑA ASIGNADA" & Chr(13) & "CAMBIE NOMBRE DE USUARIO O CONTRASEÑA", "SOLUCIONES SOFTWARE PERU", Nothing, MessageBoxIcon.Information)
            txtUsu1.Focus()
            Exit Sub
        End If
        Dim resp As Short = MessageBox.Show("Esta segúro de cambiar contraseña de usuario?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            'llamando al procedimiento k crea el comando Update
            comandoUpdate(txtUsu1.Text.Trim(), txtCon1.Text.Trim(), vPass)
            cmUpdateTable.Transaction = myTrans
            If cmUpdateTable.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("FUE CAMBIADO CON EXITO USUARIO Y CONTRASEÑA...")
            MessageBox.Show("En la siguiente sesion entre con su usuario y contraseña nueva...", nomNegocio, Nothing, MessageBoxIcon.Information)
        Catch f As Exception
            'deshace la transaccion
            myTrans.Rollback()
            MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
        End Try

        Me.Close()
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate(ByVal usuario As String, ByVal pas1 As String, ByVal codUsu As Integer)
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TPersonal set usuario=@usu,pass=@pas1 where codPers=@codUsu"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@usu", SqlDbType.VarChar, 20).Value = usuario
        cmUpdateTable.Parameters.Add("@pas1", SqlDbType.VarChar, 20).Value = pas1
        cmUpdateTable.Parameters.Add("@codUsu", SqlDbType.Int, 0).Value = codUsu
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class
