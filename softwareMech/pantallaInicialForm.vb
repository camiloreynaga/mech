Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports ComponentesSolucion2008

Public Class pantallaInicialForm

    Private Sub pantallaInicialForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bienvenida As New SplashScreen
        bienvenida.Show()
        bienvenida.Refresh()

        'Realizando la conexion con SQL Server - ConexionModule.vb
        conexion()

        If File.Exists("SavedColor.bin") Then
            Dim myFileStream As Stream = File.OpenRead("SavedColor.bin")
            Dim deserializer As New BinaryFormatter
            'Objeto myColor instanciado en el modulo varFuncPublicasModule
            myColor = CType(deserializer.Deserialize(myFileStream), ColorClass)
            myFileStream.Close()
        End If
        'procedimiento AsignarColoresFormControles creado en el modulo varFuncPublicasModule
        AsignarColoresFormControles()

        Me.BackColor = BackColorP
        Me.Text = nomNegocio

        Dim pantalla As New LoginForm
        bienvenida.Close()
        pantalla.ShowDialog()

        bienvenida = New SplashScreen
        If vPass >= 1 Then
            'Entrada accedida
            bienvenida.Show()
        Else
            'Acceso denegado
            Me.Close()
            Exit Sub
        End If
        'Funciones recuperarTipoUsu... creado en el modulo varFuncPublicasModule
        vSTipoUsu = recuperarTipoUsu(vPass)          'Administrador almacen

        Me.Text = "Cod.Lug=> " & vSCodigo & "   Lugar / Obra => " & vSNomSuc & "     " & vSTipoUsu & ": " & vSUsuario & "     "

        'If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Then
        If vSCodTipoUsu = 1 Then    '1=tipo administrador Sistema
            TSMenu.Visible = True
            PermisosAdmSistema()
        End If
        If vSCodTipoUsu = 2 Then    '2=tipo Gerencia General
            TSMenu.Visible = True
            PermisosGerenciaGeneral()
        End If
        If vSCodTipoUsu = 3 Then    '3=tipo Gerencia de Construcciones
            TSMenu.Visible = True
            PermisosGerenciaConstrucciones()
        End If
        If vSCodTipoUsu = 4 Then    '4=tipo Logistica
            TSMenu.Visible = True
            PermisosLogistica()
        End If
        If vSCodTipoUsu = 5 Then    '5=tipo Tesoreria
            TSMenu.Visible = True
            PermisosTesoreria()
        End If
        If vSCodTipoUsu = 6 Then    '6=tipo residente
            TSMenu.Visible = True
            PermisosResidente()
        End If
        If vSCodTipoUsu = 7 Then    '5=tipo Jefe Seguridad
            TSMenu.Visible = True
            PermisosJefeSeguridad()
            'DesactivarMenuAlmacen()
        End If
        If vSCodTipoUsu = 8 Then    '5=tipo jefe Equipo Mecánico
            TSMenu.Visible = True
            PermisosJefeEquipoMecanico()
            'DesactivarMenuAlmacen()
        End If
        If vSCodTipoUsu = 9 Then    '5=tipo Administrador Obra
            TSMenu.Visible = True
            PermisosAdmObra()
            'DesactivarMenuAlmacen()
        End If
        If vSCodTipoUsu = 10 Then    '5=tipo Almacenero
            TSMenu.Visible = True
            PermisosAlmacenero()
            'DesactivarMenuAlmacen()
        End If
        'End If

        'Cambiando el formato de fecha
        dateFormat()
        bienvenida.Close()

    End Sub

    Private Sub PermisosAlmacenero()

    End Sub

    Private Sub PermisosAdmObra()
        PermisosResidente()
        opcMat3.Visible = False
        ToolStripSeparator9.Visible = False
    End Sub

    Private Sub PermisosJefeEquipoMecanico()
        PermisosResidente()
        opcMat3.Visible = False
        ToolStripSeparator9.Visible = False
    End Sub

    Private Sub PermisosJefeSeguridad()
        PermisosResidente()
        opcMat3.Visible = False
        ToolStripSeparator9.Visible = False

    End Sub

    Private Sub PermisosResidente()
        opcEnt.Visible = False
        opcMat1.Visible = False
        opcCot.Visible = False
        opcOrden.Visible = False
        opcOrdDes.Visible = False
        opcDoc.Visible = False
        opcGuia.Visible = False
        opcPers.Visible = False
        opcConf6.Visible = False

        ToolStripSeparator10.Visible = False
        ToolStripSeparator8.Visible = False
        ToolStripSeparator32.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator5.Visible = False
        ToolStripSeparator6.Visible = False
        ToolStripSeparator2.Visible = False

    End Sub
    ''' <summary>
    ''' Permisos de acceso para tesoreria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosTesoreria()
        'Menus
        opcEnt.Visible = False
        opcMat.Visible = False
        opcReq.Visible = False
        opcCot.Visible = False
        opcOrden.Visible = False
        opcDoc.Visible = False
        opcGuia.Visible = False
        opcPers.Visible = False
        opcConf6.Visible = False

        'separadores
        ToolStripSeparator8.Visible = False
        ToolStripSeparator3.Visible = False
        ToolStripSeparator32.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator5.Visible = False
        ToolStripSeparator6.Visible = False
        ToolStripSeparator2.Visible = False
        ToolStripSeparator34.Visible = False
    End Sub

    ''' <summary>
    ''' Permisos de acceso para Logistica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosLogistica()
        TSMenu.Items("opcPers").Visible = False
        opcConf6.Visible = False

    End Sub
    ''' <summary>
    ''' Permisos de acceso para Gerencia de Construcciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosGerenciaConstrucciones()

    End Sub
    ''' <summary>
    ''' permisos de acceso para Gerencia General
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosGerenciaGeneral()

    End Sub
    ''' <summary>
    ''' permisos de acceso apra Administrador del sistema
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosAdmSistema()

    End Sub

    Private Sub DesactivarMenuSudAdm()
        'opcPers.Visible = False   'Menu Personal
        'opcConf6.Visible = False    'asignar serie
    End Sub

    Private Sub DesactivarMenuAlmacen()
        'opc1.Visible = True   'Menu Producto
        'opcConf3.Visible = False   'Sub Menu Sucursal Almacenes
    End Sub

    Private Sub dateFormat()
        'Obtener un único valor de una base de datos
        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SET DATEFORMAT dmy"
        cmd.Connection = Cn
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub opcAce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAce.Click
        Dim acerca As New AcercaDeForm
        acerca.MdiParent = Me
        acerca.Show()
    End Sub

    Private Sub opcPers1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcPers1.Click
        Dim mant As New MantPersonalForm
        mant.MdiParent = Me
        mant.Show()


    End Sub

    Private Sub opcPers2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim mant As New asignarAccesoUsuarioForm
        'mant.MdiParent = Me
        'mant.Show()
    End Sub

    Private Sub opcConf1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConf1.Click
        Dim mant As New configuracionColorForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcConf2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConf2.Click
        Dim mant As New CambiarContraseñaUsuForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcConf6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConf6.Click
        Dim mant As New mantLugarForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcEnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcEnt.Click
        Dim mant As New MantIdentidadForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcMat2.Click
        Dim mant As New MantUniMedForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcMat1.Click
        Dim mant As New MantAreaMatForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcMat.ButtonClick
        Dim mant As New MantMaterialForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcPers3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcPers3.Click
        Dim mant As New MantCargoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcMat3.Click
        Dim mant As New MantTipoMatForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCot1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCot1.Click
        Dim cot As New registraCotizacionForm
        cot.MdiParent = Me
        cot.Show()
    End Sub

    Private Sub opcOrden1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrden1.Click
        Dim mant As New registraOrdenCompraForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrden2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrden2.Click
        Dim inf As New InformeOrdenCompraForm
        inf.MdiParent = Me
        inf.Show()
    End Sub

    Private Sub opcCot2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCot2.Click
        Dim inf As New InformeCotizacionForm
        inf.MdiParent = Me
        inf.Show()
    End Sub

    Private Sub opcOrdDes1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDes1.Click
        Dim mant As New MantOrdenDesembolsoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcReq1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcReq1.Click
        Dim mant As New MantSolicitudReqForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcReq2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcReq2.Click
        Dim seg As New chekeoSolicitudReqForm
        seg.MdiParent = Me
        seg.Show()
    End Sub

    Private Sub opcCot3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCot3.Click
        Dim mant As New MantFormaPagoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDes4.Click
        Dim mant As New MantTipoPagoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDes2.Click
        Dim mant As New gerenciaOrdenDesembolsoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub CuentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CuentasToolStripMenuItem.Click
        Dim mant As New MantCuentas
        mant.MdiParent = Me
        mant.Show()
    End Sub
End Class