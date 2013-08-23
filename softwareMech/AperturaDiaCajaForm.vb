Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class AperturaDiaCajaForm

    Private Sub AperturaDiaCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub AperturaDiaCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        If recuperarFecha(vSCodigo).Date = "01/01/1900" Then
            MsgBox("PRIMERA FECHA")
            Date1.Value = Now.Date
        Else
            Date1.Value = DateAdd(DateInterval.Day, 1, recuperarFecha(vSCodigo).Date) 'Aumenta la fecha en 1
        End If

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Public Function recuperarFecha(ByVal codigo As String) As Date
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(fecha),'') as fecha from TDiaCaja where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

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
        btnApertura.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Valida fecha mayorigual a 2012
        If ValidaFechaMayorXXXX(Date1.Value.Date, 2013) Then
            StatusBarClass.messageBarraEstado("  INGRESE FECHA MAYOR AL AÑO 2013...")
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Function recuperardiacero(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TDiaCaja where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarMov(ByVal idSes As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TMovimientoCaja TD join TDiaCaja TS on TD.codDia=TS.codDia where TD.codDia=" & idSes
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Public Function VerificarFecha(ByVal fecha As String, ByVal codigo As String) As Short
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDiaCaja where fecha='" & fecha & "' and codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnApertura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApertura.Click
        Dim idSesionAux As Integer = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto
        If vSCodDia <> idSesionAux Then
            If idSesionAux = -1 Then
                MessageBox.Show("PRIMER DIA A APERTURAR SUERTEEEE...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Else
                MessageBox.Show("PROCESO DENEGADO, FUE APERTURADO OTRO DIA VENTA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            End If
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If recuperardiacero(vSCodigo) > 0 Then
            If recuperarMov(vSCodDia) = 0 Then
                MessageBox.Show("Proceso denegado, Esta Caja Chica NO tiene MOVIMIENTOS PROCESADOS...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        If VerificarFecha(Date1.Value.Date, vSCodigo) > 0 Then
            MessageBox.Show(Date1.Value.Date & " YA EXISTE EN DIAS APERTURADOS ANTERIORMENTE...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta seguro de APERTURAR dia => " & Date1.Value.Date & Chr(13) & "y CERRAR dia de caja => " & txtFecha.Text, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Cursor = Cursors.AppStarting
        Dim finalMytrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            'TDiaCaja
            comandoInsert()
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'actualizando TDiaCaja
            comandoUpdate33(2, vSCodDia)   '2=Cerrado
            cmdUpdateTable33.Transaction = myTrans
            cmdUpdateTable33.ExecuteNonQuery()

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            MessageBox.Show("FUE APERTURADO CON EXITO SESION DE VENTAS...", nomNegocio, Nothing, MessageBoxIcon.Information)
            wait.Close()
            Me.Close()
            End
        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TDiaCaja(fecha,estado,horaAbrio,HoraCerro,codPersA,codPersC,codigo) values(@fec,@est,@h1,@h2,@p1,@p2,@cod)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = Date1.Value.Date
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 1     '1=Abierto
        cmInserTable.Parameters.Add("@h1", SqlDbType.VarChar, 10).Value = Now.Hour.ToString & ":" & Now.Minute.ToString
        cmInserTable.Parameters.Add("@h2", SqlDbType.VarChar, 10).Value = ""
        cmInserTable.Parameters.Add("@p1", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@p2", SqlDbType.Int, 0).Value = 0
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo
    End Sub

    Dim cmdUpdateTable33 As SqlCommand
    Private Sub comandoUpdate33(ByVal estado As Short, ByVal idDia As Integer)
        cmdUpdateTable33 = New SqlCommand
        cmdUpdateTable33.CommandType = CommandType.Text
        cmdUpdateTable33.CommandText = "update TDiaCaja set estado=@est,horaCerro=@hor,codPersC=@codP where codDia=@idDia"
        cmdUpdateTable33.Connection = Cn
        cmdUpdateTable33.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmdUpdateTable33.Parameters.Add("@hor", SqlDbType.VarChar, 10).Value = Now.Hour.ToString & ":" & Now.Minute.ToString
        cmdUpdateTable33.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmdUpdateTable33.Parameters.Add("@idDia", SqlDbType.Int, 0).Value = idDia
    End Sub
End Class
