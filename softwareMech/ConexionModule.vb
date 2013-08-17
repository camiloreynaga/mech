Imports System.Data
Imports System.Data.SqlClient
Module ConexionModule
    ''creando la instancia de la conexion de tipo connection
    Public Cn As SqlConnection
    Public Sub conexion()
        Cn = New SqlConnection
        ' Cn.ConnectionString = "Data Source=sv32.dbsqlserver.com,8888;Initial Catalog=BD_ConstrucMech;User Id=mech;password=mechcusco2013;Connection Timeout=59"
        Cn.ConnectionString = "Data Source=(local);Initial Catalog=BD_ConstrucMech;User Id=mech;password=mechcusco2013;Connection Timeout=59"
        Cn.Open()
    End Sub

    Public Sub VerificaConexion()
        If Cn.State = ConnectionState.Closed Then
            Dim resp As Short = MessageBox.Show("LA CONEXION A LA BASE DE DATOS SE CERRO. DESEA ABRIR LA CONEXION?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                End
            End If
            Cn.Open()
        End If
    End Sub
End Module
