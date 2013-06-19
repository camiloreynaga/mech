Imports System.Data.SqlClient
Module varFuncPublicasModule
    'instanciando la clase ColorClass que guarda los colores de configuracion
    Public myColor As New ColorClass

    'color de fondo del titulo principal
    Public TituloBackColorP As Color
    'color de texto del titulo principal y del titulo del dataGrid
    Public HeaderForeColorP As Color
    'color de fondo del titulo del dataGrid
    Public HeaderBackColorP As Color
    'color de fondo de los formularios
    Public BackColorP As Color 'DarkGreen,DarkKhaki,DarkOrange'DarkSalmon,DimGray,DodgerBlue,Gainsboro,Gold,Goldenrod,IndianRed,Ivory
    'Khaki,LavenderBlush,LightCoral,LightGray,LightPink,LightSalmon
    Public CaptionForeColorP As Color
    Public nomNegocio As String = " Constructora MECH S.R.L."
    Public vSTitulo As String = "Software de Abastecim., Logistica y Almacén"
    'color de texto de los label y groupBox
    Public ForeColorLabel As Color
    'color de texto de los buttom
    Public ForeColorButtom As Color

    Public Sub AsignarColoresFormControles()
        TituloBackColorP = myColor.BackColorTituloPrincipal
        HeaderForeColorP = myColor.foreColorTituloPrincipalDG
        HeaderBackColorP = myColor.backColorTituloDGrid
        BackColorP = myColor.backColorFormularios
        CaptionForeColorP = Color.Red
        ForeColorLabel = myColor.foreColorLabel
        ForeColorButtom = myColor.foreColorButtom
    End Sub

    'declaracion de varibles publicas k pueden ver todos los Forms
    Public vPass As Integer    'Variable para usuario no cambiar su valor
    Public vSUsuario As String    'Variable para usuario no cambiar su valor
    Public vSTipoUsu As String 'Variable para usuario no cambiar su valor
    Public vSCodTipoUsu As Integer    'Variable de sistema no cambiar su valor
    Public vSCodigo As String    'Variable de sistema no cambiar su valor codigo sucursal
    Public vSNomSuc As String    'Variable de sistema no cambiar su valor codigo sucursal
    Public vSDirSuc As String 'Variable de sistema no cambiar su valor
    Public vSIGV As Double = 18 'IGV Variable de sistema no cambiar su valor

    Public vNroOrden As Integer
    Public vSerie As String
    Public vIniNroDoc As Integer
    Public vFinNroDoc As Integer
    Public vFec1 As Date
    Public vFec2 As Date
    Public vSubT As Double
    Public vIGV As Double
    Public vTotal As Double
    Public vStock As Decimal
    Public vPrec As Decimal
    Public vProd As String
    Public vUnidad As String
    Public vX1 As Integer
    Public vX2 As Integer
    Public vX3 As Integer
    Public vX4 As Integer
    Public vOps As Integer
    Public vCodSer As Integer
    Public vCodIde As Integer
    Public vCod1 As String
    Public vCod2 As String
    Public vCod3 As String
    Public vCant As Double
    Public vIdTip As Integer
    Public vCodDoc As Integer
    Public vCodTipo As Integer
    Public vCont As Integer
    Public vCodProd As Integer
    Public vCodGuia As Integer
    Public vObs As String

    Public vParam1 As String
    Public vParam2 As String
    Public vParam3 As String
    Public vParam4 As String
    Public vParam5 As String

    Public matriz(10) As String
    Public arreglo(14, 4) As String 'Arreglo de 15 filas x 5 columnas
    Public arreglo1(9, 1) As String 'Arreglo de 10 filas x 2 columnas
    'LITO
    Public Function recuperarUsu(ByVal usuario As String, ByVal pas1 As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPers),0) from TPersonal where usuario='" & usuario & "' and pass='" & pas1 & "' and estado=1" '1=Activo
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    'LITO
    Public Function recuperarCodTipoUsu(ByVal codUsu As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select TTU.codTipU from TTipoUsu TTU join TPersonal TU on TTU.codTipU=TU.codTipU where codPers=" & codUsu
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    'LITO
    Public Function recuperarCodigo(ByVal codUsu As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select codigo from TPersLugar where codPers=" & codUsu
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    'LITO
    Public Function verificaSiExisteNomUsuario(ByVal usuario As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPers),0) from TPersonal where usuario='" & usuario & "' and estado=1"   '1=Activo
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    'LITO
    Public Function recuperarTipoUsu(ByVal codUsu As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select tipo from TTipoUsu TTU join TPersonal TU on TTU.codTipU=TU.codTipU where codPers=" & codUsu
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
    'LITO
    'Recupera usuarios activos e inactivos
    Public Function recuperarUsu1(ByVal usuario As String, ByVal pas1 As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPers),0) from TPersonal where usuario='" & usuario & "' and pass='" & pas1 & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Public Function recuperarVariosTSerie(ByVal codTipo As Integer, ByVal campo As String) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        Dim sele As String = "select " & campo & " from TSerie where estado=1 and codTipD=" & codTipo
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = sele
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function
End Module
