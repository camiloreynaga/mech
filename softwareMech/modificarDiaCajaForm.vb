Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class modificarDiaCajaForm

    Private Sub modificarDiaCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub modificarDiaCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacen.vb

        Try
            configurarColorControl()

        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try

        txtFecha.Text = recuperarFecha1(vSCodDia)

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Public Function recuperarFecha1(ByVal idSesion As Integer) As Object
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select fecha from TDiaCaja where codDia=" & idSesion
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        Label1.ForeColor = ForeColorLabel
        Label3.ForeColor = ForeColorLabel
        btnMod.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Function ValidarCampos() As Boolean
        'Valida fecha mayorigual a 2012
        If ValidaFechaMayorXXXX(Date1.Value.Date, 2013) Then
            StatusBarClass.messageBarraEstado("  INGRESE FECHA MAYOR AL AÑO 2012...")
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Public Function VerificarFecha(ByVal fecha As String, ByVal codigo As String) As Short
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDiaCaja where fecha='" & fecha & "' and codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMod.Click
        If txtFecha.Text.Trim() = "" Then
            MessageBox.Show("NO EXISTE DIA APERTURADO A MODIFICAR...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim idSesionAux As Integer = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto
        If vSCodDia <> idSesionAux Then
            If idSesionAux = -1 Then
                MessageBox.Show("PROCESO DENEGADO, FUE CERRADO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            Else
                MessageBox.Show("PROCESO DENEGADO, FUE APERTURADO OTRO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            End If
        End If

        If VerificarFecha(Date1.Value.Date, vSCodigo) > 0 Then
            MessageBox.Show(Date1.Value.Date & " YA EXISTE EN DIAS APERTURADOS ANTERIORMENTE...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de modificar fecha a => " & Date1.Value.Date, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Refresh()
        Dim wait As New waitForm
        wait.Show()
        Try
            'llamando al procedimiento k crea el comando Update
            comandoUpdate13(vSCodDia)
            cmUpdateTable13.ExecuteNonQuery()

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            wait.Close()
            MessageBox.Show("FECHA DE CAJA FUE MODIFICADO CON EXITO..." & Chr(13) & "VUELVA A INGRESAR AL SISTEMA.", nomNegocio, Nothing, MessageBoxIcon.Information)
            End
            Exit Sub
        Catch f As Exception
            wait.Close()
            'myTrans.Rollback()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13(ByVal idDia As Integer)
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TDiaCaja set fecha=@cam where codDia=@cod"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@cam", SqlDbType.Date).Value = Date1.Value.Date
        cmUpdateTable13.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idDia
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class
