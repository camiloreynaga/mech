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
        vSCodDia = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto CAJA CHICA Dia

        If vSCodDia = 0 Then
            Me.Text = "Cod.Lug=> " & vSCodigo & "   Lugar / Obra => " & vSNomSuc & "     " & vSTipoUsu & ": " & vSUsuario
        Else
            vSIdDia = " Fecha Caja Chica Aperturada => " & recuperarIdDia(1, vSCodigo) 'estado=1  Abierto
            vSFecCaja = recuperarFechaCaja(1, vSCodigo) 'estado=1  Abierto
            Me.Text = "Cod.Lug=> " & vSCodigo & "   Lugar / Obra => " & vSNomSuc & "     " & vSTipoUsu & ": " & vSUsuario & "  " & vSIdDia
        End If


        'Accesos a Caja Chica

        AccesoCajaChica()


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
        If vSCodTipoUsu = 11 Then    '5=tipo Almacenero
            TSMenu.Visible = True
            PermisosContabilidad()
            'DesactivarMenuAlmacen()
        End If
        If vSCodTipoUsu = 12 Then    '12=tipo CajaChica
            TSMenu.Visible = True
            permisosCajaChica()
            'DesactivarMenuAlmacen()
        End If
        'End If



        'Cambiando el formato de fecha
        dateFormat()
        bienvenida.Close()

    End Sub

    Private Sub pantallaInicialForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If vSCodTipoUsu = 3 Then  ' 3=gerencia de construcciones
            opcReqAprobacion.PerformClick()  'Aprueba por defecto su pantalla AprobarSolicitudReqForm.vb  Hubeer
        End If
        If vSCodTipoUsu = 2 Then  '2=gerencia general 
            opcOrdDesAprobacion.PerformClick()  'Aprueba por defecto su pantalla gerenciaOrdenDesembolsoForm.vb  Freddy
        End If
        If vSCodTipoUsu = 5 Then  '5=Tesoreria 
            opcOrdDesRegPagos.PerformClick()  'Aprueba por defecto su pantalla tesoreriaOrdenDesembolsoForm.vb  Yoel
        End If
    End Sub

    Private Sub AccesoCajaChica()

        'opcCaja4.Visible = False 'Mantenimiento de cajas
        'opcCaja6.Visible = True ' registro de requerimiento de caja

        'Consulta si el Usuario tiene acceso a Caja Chica

        'Caja Chica
        Dim oDataManager As New cDataManager

        If oDataManager.consultarTabla("select COUNT(*)  from tcajaChica where codPers =" & vPass, CommandType.Text) > 0 Then
            opcCaja1.Visible = True 'apertura orden de desembolso
            opcCaja2.Visible = True 'Abrir fecha de caja
            opcCaja3.Visible = True 'modificar fecha de caja
            opcCaja5.Visible = True 'ingreso de dinero con Ord. Desembolso
            opcCaja8.Visible = True 'Egreso Dinero Caja Solicitante
            opcCaja10.Visible = True 'Aprobar rendicion de cuentas

            ToolStripSeparator21.Visible = True
            ToolStripSeparator22.Visible = True
        Else
            opcCaja1.Visible = False 'apertura orden de desembolso
            opcCaja2.Visible = False 'Abrir fecha de caja
            opcCaja3.Visible = False 'modificar fecha de caja
            opcCaja5.Visible = False 'ingreso de dinero con Ord. Desembolso
            opcCaja8.Visible = False  'Egreso Dinero Caja Solicitante
            opcCaja10.Visible = False  'Aprobar rendicion de cuentas
            ToolStripSeparator21.Visible = False
            ToolStripSeparator22.Visible = False
        End If


    End Sub


    Private Sub PermisosAlmacenero()
        PermisosResidente()
        opcInsumoTipo.Visible = False
        opcInsumoArea.Visible = False

        opcGuiaRem.Visible = True 'Menú guia de remision
        opcAlmE.Visible = True ' Menú Entradas (Almacen)
        ToolStripSeparator17.Visible = True 'raya
        opcAlmS.Visible = True ' Menú Salidas (Almacen)

        opcGuiaRem2.Visible = False ' Mantenimiento de Motivos GR
        ToolStripMenuItem2.Visible = False 'Mantenimiento de Vehiculos / Chofer

        ToolStripSeparator7.Visible = True
        ToolStripSeparator12.Visible = True

        ToolStripSeparator2_1.Visible = False
    End Sub
    ''' <summary>
    ''' Permisos para Administrador de Obra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosAdmObra()
        PermisosResidente()
        opcInsumoTipo.Visible = False
        opcInsumoArea.Visible = False
        ToolStripSeparator2_1.Visible = False
    End Sub

    ''' <summary>
    ''' Permisos para Jefe de Equipo Mecánico
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosJefeEquipoMecanico()
        PermisosResidente()
        opcInsumoTipo.Visible = False
        opcInsumoArea.Visible = False
        ToolStripSeparator2_1.Visible = False
    End Sub
    ''' <summary>
    ''' Permisos para Jefe de Seguridad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosJefeSeguridad()
        PermisosResidente()
        opcInsumoTipo.Visible = False
        opcInsumoArea.Visible = False
        ToolStripSeparator2_1.Visible = False

    End Sub
    ''' <summary>
    ''' Permisos de acceso para Residente
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosResidente()
        opcClien_Prov.Visible = False
        opcInsumoArea.Visible = False 'Mantenimiento Área Insumo
        opcReqAprobacion.Visible = False

        opcCotizacion.Visible = False 'Cotizacion
        opcOrdenCompra.Visible = False ' Orde de compra
        opcOrdDesembolso.Visible = False ' Orden de Desembolso
        opcDocCompra.Visible = False ' Documento de Compra

        ' opcCaja.Visible = False 'Caja Chica

        opcGuiaRem.Visible = False ' Guia de Remision
        opcPersonal.Visible = False ' Personal
        opcConfObra.Visible = False 'Lugar de Trabajo
        opcConfSeriePersonal.Visible = False 'Asignar Serie de Orden de desembolso
        opcConfSerieDesem.Visible = False 'Mantenimiento de series de orden de desembolso
        opcConfSerieGuia.Visible = False 'Mantenimienro de Serie Guia de remision por persona

        opcAlmE.Visible = False 'Entrada Almacen
        opcAlmS.Visible = False 'Salida Almacen
        infT2.Visible = False ' informe de gastos por dia

        ToolStripSeparator5.Visible = False
        ToolStripSeparator1.Visible = False
        ToolStripSeparator3.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator6.Visible = False
        ToolStripSeparator7.Visible = False
        ToolStripSeparator8.Visible = False

        'ToolStripSeparator16.Visible = False
        ToolStripSeparator12.Visible = False
        ToolStripSeparator17.Visible = False

        'Separadores sub menú Configuración
        ToolStripSeparator13.Visible = False
        ToolStripSeparator14.Visible = False
        ToolStripSeparator10_1.Visible = False

    End Sub
    ''' <summary>
    ''' Permisos de acceso para tesoreria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosTesoreria()
        'Menus
        'opcRequerimientoObra.Visible = False
        opcInsumoArea.Visible = False
        opcInsumoTipo.Visible = False

        opcReqAprobacion.Visible = False

        opcCotizacion.Visible = False
        opcOrdenCompra.Visible = False
        opcDocCompra.Visible = False
        'opcCaja.Visible = False 'Caja Chica

        opcOrdDesAprobacion.Visible = False
        opcOrdDesConta.Visible = False

        opcGuiaRem.Visible = False 'Guia de remision
        opcPersonal.Visible = False
        opcConfObra.Visible = False
        opcConfSeriePersonal.Visible = False 'Asignar Serie de Orden de desembolso
        opcConfSerieDesem.Visible = False 'Mantenimiento de series de orden de desembolso
        opcConfSerieGuia.Visible = False 'Mantenimienro de Serie Guia de remision por persona

        opcAlmE.Visible = False 'Entrada Almacen
        opcAlmS.Visible = False 'Salida Almacen


        'separadores
        'ToolStripSeparator1.Visible = False
        ToolStripSeparator3.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator6.Visible = False
        ToolStripSeparator7.Visible = False
        ToolStripSeparator8.Visible = False
        ToolStripSeparator10.Visible = False

        'ToolStripSeparator16.Visible = False
        ToolStripSeparator12.Visible = False
        ToolStripSeparator17.Visible = False

        'Separadores sub menú Configuración
        ToolStripSeparator13.Visible = False
        ToolStripSeparator14.Visible = False
        ToolStripSeparator10_1.Visible = False
    End Sub

    ''' <summary>
    ''' Permisos de acceso para Logistica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosLogistica()
        opcReqAprobacion.Visible = False
        opcOrdDesAprobacion.Visible = False
        opcOrdDesRegPagos.Visible = False
        opcOrdDesConta.Visible = False
        opcOrdDesModPago.Visible = False
        opcOrdDesCtasBco.Visible = False

        'opcCaja.Visible = False 'Caja Chica

        opcConfSerieGuia.Visible = False 'Mantenimienro de Serie Guia de remision por persona

        opcPersonal.Visible = False

        opcConfObra.Visible = False
        opcConfSerieDesem.Visible = False
        opcConfSeriePersonal.Visible = False

        infT2.Visible = False ' informe de gastos por dia

        ToolStripSeparator9.Visible = False

        'ToolStripSeparator16.Visible = False

        'Separadores sub menú Configuración
        ToolStripSeparator13.Visible = False
        ToolStripSeparator14.Visible = False
        ToolStripSeparator10_1.Visible = False

    End Sub
    ''' <summary>
    ''' Permisos de acceso para Gerencia de Construcciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosGerenciaConstrucciones()

        opcCotizacion.Visible = False
        opcOrdenCompra.Visible = False


        opcOrdDesAprobacion.Visible = False
        opcOrdDesRegPagos.Visible = False
        opcOrdDesConta.Visible = False
        opcOrdDesModPago.Visible = False
        opcOrdDesCtasBco.Visible = False

        opcDocCompra.Visible = False

        'opcCaja.Visible = False 'Caja Chica
        opcGuiaRem.Visible = True ' Guia de remision

        opcPersonal.Visible = False

        opcConfSerieDesem.Visible = False
        opcConfSeriePersonal.Visible = False

        opcConfSerieGuia.Visible = False  'Mantenimienro de Serie Guia de remision por persona

        opcGuiaRem1.Visible = False 'registro GR MECH
        opcGuiaRem3.Visible = False 'Registro GR Proveedor
        opcGuiaRem2.Visible = False 'Mant motivo GR
        ToolStripMenuItem2.Visible = False 'Mant Vehiculo / Chofer

        opcAlmE.Visible = False 'Entrada Almacen
        opcAlmS.Visible = False 'Salida Almacen

        'infT2.Visible = False ' informe de gastos por dia

        ToolStripSeparator9.Visible = True

        ToolStripSeparator20.Visible = False

        ToolStripSeparator3.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator7.Visible = False
        ToolStripSeparator8.Visible = False
        ToolStripSeparator9.Visible = False


        ToolStripSeparator17.Visible = False



        'Separadores sub menú Configuración
        ToolStripSeparator14.Visible = False
        ToolStripSeparator10_1.Visible = False

    End Sub
    ''' <summary>
    ''' permisos de acceso para Gerencia General
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosGerenciaGeneral()

        opcOrdDesRegPagos.Visible = False
        opcOrdDesConta.Visible = False

        'Administración de caja chica
        opcCaja4.Visible = True 'Mantenimiento de cajas
        opcCaja7.Visible = True 'Parobar requerimiento

    End Sub

    Private Sub PermisosContabilidad()
        'Menus
        'opcRequerimientoObra.Visible = False
        opcInsumoArea.Visible = False
        opcInsumoTipo.Visible = False

        opcReqAprobacion.Visible = False

        opcCotizacion.Visible = False
        opcOrdenCompra.Visible = False

        opcOrdDesAprobacion.Visible = False
        opcOrdDesRegPagos.Visible = False

        opcDocCompra.Visible = False
        opcGuiaRem.Visible = False
        opcPersonal.Visible = False
        opcConfObra.Visible = False
        opcConfSeriePersonal.Visible = False 'Asignar Serie de Orden de desembolso
        opcConfSerieDesem.Visible = False 'Mantenimiento de series de orden de desembolso

        infT2.Visible = False ' informe de gastos por dia

        'separadores
        'ToolStripSeparator1.Visible = False
        ToolStripSeparator3.Visible = False
        ToolStripSeparator4.Visible = False
        ToolStripSeparator6.Visible = False
        ToolStripSeparator7.Visible = False
        ToolStripSeparator8.Visible = False
        ToolStripSeparator10.Visible = False
    End Sub

    Private Sub permisosCajaChica()
        PermisosTesoreria()

        opcOrdDesCtasBco.Visible = False
        opcOrdDesModPago.Visible = False
        opcOrdDesRegPagos.Visible = False

        'Opciones

        


        infT2.Visible = False ' informe de gastos por dia
        ToolStripSeparator16.Visible = True  'Linea encima de caja Chica
        opcCaja.Visible = True ' Menu Caja Chica

    End Sub

    ''' <summary>
    ''' permisos de acceso apra Administrador del sistema
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PermisosAdmSistema()
        'Administración de caja chica

        opcCaja7.Visible = True 'Aprobar requerimiento cajas
        opcCaja4.Visible = True 'Mantenimiento de cajas
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

    Private Sub opcConf1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfColorPantalla.Click
        Dim mant As New configuracionColorForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcConf2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfCambiarContra.Click
        Dim mant As New CambiarContraseñaUsuForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcConf6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfObra.Click
        Dim mant As New mantLugarForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcEnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcClien_Prov.Click
        Dim mant As New MantIdentidadForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcInsumoUnd.Click
        Dim mant As New MantUniMedForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcInsumoArea.Click
        Dim mant As New MantAreaMatForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcInsumo.ButtonClick
        Dim mant As New MantMaterialForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcPers3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcPers3.Click
        Dim mant As New MantCargoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcMat3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcInsumoTipo.Click
        Dim mant As New MantTipoMatForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCot1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCot1.Click
        Dim cot As New registraCotizacion1Form
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

    Private Function recuperarCodSerO(ByVal codPers As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codSerO),0) from VSerieOrdenPers where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSerie(ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select serie from VSerieOrdenPers where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarIniNro(ByVal codPers As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select max(iniNroDoc) from VSerieOrdenPers where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodSerCaja(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codSerO),0) from VCajaSerie where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSerieCaja(ByVal codigo As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select max(serie) from VCajaSerie where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarIniNroCaja(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select max(iniNroDoc) from VCajaSerie where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodPersCaja(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select max(codPers) from VCajaSerie where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub opcOrdDes1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesApertura.Click
        vSCodSerO = recuperarCodSerO(vPass)
        If vSCodSerO = 0 Then
            MessageBox.Show("Proceso denegado, Usuario NO tiene asignado Serie de Orden de Desembolso...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        vSSerie = recuperarSerie(vPass)
        vSIniNroDoc = recuperarIniNro(vPass)

        Dim mant As New MantOrdenDesembolsoForm1
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcReq1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcReqSolicitud.Click
        Dim mant As New MantSolicitudReqForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcReq2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcReqSeguimiento.Click
        Dim seg As New chekeoSolicitudReqForm
        seg.MdiParent = Me
        seg.Show()
    End Sub

    Private Sub opcCot3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCot3.Click
        Dim mant As New MantFormaPagoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesModPago.Click
        Dim mant As New MantTipoPagoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesAprobacion.Click
        Dim mant As New gerencia1OrdenDesembolsoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesRegPagos.Click
        Dim mant As New tesoreriaOrdenDesembolsoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcReq3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcReqAprobacion.Click
        Dim seg As New AprobaSolicitudReqForm
        seg.MdiParent = Me
        seg.Show()
    End Sub

    Private Sub opcConf4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfSerieDesem.Click
        Dim mant As New ConfiguracionSerieDocForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrden3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrden3.Click
        Dim Emp As New MantenimientoTransporteForm
        Emp.MdiParent = Me
        Emp.Show()

    End Sub

    Private Sub opcConf5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfSeriePersonal.Click
        Dim mant As New AsignarSerieOrdenPersForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcOrdDes6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesSeguimiento.Click
        Dim ODesem As New SeguimientoOrdenDesembolsoForm
        ODesem.MdiParent = Me
        ODesem.Show()
    End Sub

    Private Sub opcOrdDes7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesCtasBco.Click
        Dim Cta As New MantCuentas
        Cta.MdiParent = Me
        Cta.Show()

    End Sub

    Private Sub opcOrdDes5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcOrdDesConta.Click
        Dim mant As New contaOrdenDesembolsoForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcConfSerieGuia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcConfSerieGuia.Click
        Dim mant As New ConfiguracionSerieEmpForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcGuiaRem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcGuiaRem2.Click
        Dim mant As New MantMotivoGuiaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcGuiaRem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcGuiaRem1.Click
        If recuperarSerieGuiaRemEmp(75) = 0 Then  '75=Guia de remision
            MessageBox.Show("Acceso denegado, el administrador no le asigno serie de guias de remision", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim guia As New registraGuiaRemEmpForm
        guia.MdiParent = Me
        guia.Show()
    End Sub

    Private Sub opcAlmS1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmS1.Click
        'If vSCodigo <> "00-00" Then 'Sede principal
        '    MessageBox.Show("Acceso denegado, Este tipo de salidas de almacén solo se pueden procesar en SEDE PRINCIPAL", nomNegocio, Nothing, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        Dim ent As New salidaAlmacenGuiaMechForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

    Private Sub opcAlmS2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmS2.Click
        If vSCodigo <> "00-00" Then 'Sede principal
            MessageBox.Show("Acceso denegado, Este tipo de salidas de almacén solo se pueden procesar en SEDE PRINCIPAL", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim ent As New salidaAlmacenMechForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

  
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim mantVeh As New MantVehiculoTransportistaFrm
        mantVeh.MdiParent = Me
        mantVeh.Show()
    End Sub

    Private Sub opcGuiaRem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcGuiaRem3.Click
        Dim guia As New registraGuiaRemProvForm
        guia.MdiParent = Me
        guia.Show()
    End Sub

    Private Sub opcGuiaRem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcGuiaRem4.Click
        Dim SegGr As New SeguimientoGRform
        SegGr.MdiParent = Me
        SegGr.Show()
    End Sub

    Private Sub opcAlmE2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmE2.Click
        'If vSCodigo <> "00-00" Then 'Sede principal
        '    MessageBox.Show("Acceso denegado, Este tipo de entradas a almacén solo se pueden procesar en SEDE PRINCIPAL", nomNegocio, Nothing, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        Dim ent As New entradaAlmacenMechForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

    Private Sub opcAlmE1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmE1.Click
        'If vSCodigo <> "00-00" Then 'Sede principal
        '    MessageBox.Show("Acceso denegado, Este tipo de entradas a almacén solo se pueden procesar en SEDE PRINCIPAL", nomNegocio, Nothing, MessageBoxIcon.Error)
        '    Exit Sub
        'End If
        Dim ent As New entradaAlmacenGuiaMechForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

    Private Sub opcAlmE3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmE3.Click
        Dim ent As New entradaAlmacenGuiaObraForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

    Private Sub opcGuiaRem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcGuiaRem5.Click
        Dim segProv As New SeguimientoGRProvform
        segProv.MdiParent = Me
        segProv.Show()
    End Sub

    Private Sub opcAlmS3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcAlmS3.Click
        Dim ent As New salidaAlmacenPersForm
        ent.MdiParent = Me
        ent.Show()
    End Sub

    Private Sub infT1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles infT1.Click
        Dim frmKardex As New ReporteCardexForm
        frmKardex.MdiParent = Me
        frmKardex.Show()
    End Sub

    Private Sub infT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles infT2.Click
        Dim frmGastDia As New GastosPorDiaForm
        frmGastDia.MdiParent = Me
        frmGastDia.Show()
    End Sub

    Private Sub opcCaja4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja4.Click
        Dim mant As New MantCajaChicaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCaja1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja1.Click
        vSCodSerO = recuperarCodSerCaja(vSCodigo)
        If vSCodSerO = 0 Then
            MessageBox.Show("Proceso denegado, Esta Sede / Obra NO tiene asignado Serie de Orden de Desembolso Caja...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        vSSerie = recuperarSerieCaja(vSCodigo)
        vSIniNroDoc = recuperarIniNroCaja(vSCodigo)

        If recuperarCodPersCaja(vSCodigo) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es Administrador de Caja Chica...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim mant As New MantOrdenDesembCajaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCaja5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja5.Click
        If vSCodDia = 0 Then
            MessageBox.Show("ACCESO DENEGADO, DIA SESION CAJA NO FUE APERTURADO...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim codDiaAux As Integer = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto
        If vSCodDia <> codDiaAux Then
            If codDiaAux = 0 Then
                MessageBox.Show("PROCESO DENEGADO, FUE CERRADO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            Else
                MessageBox.Show("PROCESO DENEGADO, FUE APERTURADO OTRO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            End If
        End If

        vSCodSerO = recuperarCodSerCaja(vSCodigo)
        If vSCodSerO = 0 Then
            MessageBox.Show("Proceso denegado, Esta Sede / Obra NO tiene asignado Serie de Orden de Desembolso Caja...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        vSSerie = recuperarSerieCaja(vSCodigo)
        vSIniNroDoc = recuperarIniNroCaja(vSCodigo)

        If recuperarCodPersCaja(vSCodigo) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es Administrador de Caja Chica...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim mant As New procesarIngresoCajaChicaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCaja6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja6.Click
        Dim mant As New requerimientoCajaPersForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub infT3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles infT3.Click
        Dim oInfSotck As New reporteStockForm
        oInfSotck.MdiParent = Me
        oInfSotck.Show()
    End Sub

    Private Sub opcCaja7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja7.Click
        Dim mant As New aprobarSolicitudCajaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCaja8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja8.Click
        If vSCodDia = 0 Then
            MessageBox.Show("ACCESO DENEGADO, DIA SESION CAJA NO FUE APERTURADO...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim codDiaAux As Integer = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto
        If vSCodDia <> codDiaAux Then
            If codDiaAux = 0 Then
                MessageBox.Show("PROCESO DENEGADO, FUE CERRADO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            Else
                MessageBox.Show("PROCESO DENEGADO, FUE APERTURADO OTRO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            End If
        End If

        vSCodSerO = recuperarCodSerCaja(vSCodigo)
        If vSCodSerO = 0 Then
            MessageBox.Show("Proceso denegado, Esta Sede / Obra NO tiene asignado Serie de Orden de Desembolso Caja...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        vSSerie = recuperarSerieCaja(vSCodigo)
        vSIniNroDoc = recuperarIniNroCaja(vSCodigo)

        If recuperarCodPersCaja(vSCodigo) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es Administrador de Caja Chica...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim mant As New procesarEgresoCajaChicaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub opcCaja2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja2.Click
        Dim dia As New AperturaDiaCajaForm
        dia.MdiParent = Me
        dia.Show()
    End Sub

    Private Sub opcCaja3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja3.Click
        Dim dia As New modificarDiaCajaForm
        dia.MdiParent = Me
        dia.Show()
    End Sub

    Private Sub opcCaja9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opcCaja9.Click
        Dim mant As New rendicionCuentaCajaForm
        mant.MdiParent = Me
        mant.Show()
    End Sub

    Private Sub ClasificacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClasificacionToolStripMenuItem.Click
        Dim frmClas As New MantClasificacionEgresosForm
        frmClas.MdiParent = Me
        frmClas.Show()

    End Sub
End Class