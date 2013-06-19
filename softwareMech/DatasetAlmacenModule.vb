Imports System.Data
Imports System.Data.SqlClient
Module DatasetAlmacenModule
    'Declarando variables publicas para todos los formularios
    Public dsAlmacen As DataSet

    Public daTabla1 As SqlDataAdapter
    Public daTabla2 As SqlDataAdapter
    Public daTabla3 As SqlDataAdapter
    Public daTabla4 As SqlDataAdapter
    Public daTabla5 As SqlDataAdapter
    Public daTabla6 As SqlDataAdapter
    Public daTPers As SqlDataAdapter
    Public daTProd As SqlDataAdapter
    Public daTProvee As SqlDataAdapter
    Public daTCliente As SqlDataAdapter
    Public daTAlm As SqlDataAdapter
    Public daVNeg As SqlDataAdapter
    Public daVDoc As SqlDataAdapter
    Public daDetDoc As SqlDataAdapter
    Public daTOrden As SqlDataAdapter
    Public daVDetOrden As SqlDataAdapter
    Public daVMes As SqlDataAdapter
    Public daTUbi As SqlDataAdapter
    Public daVKardex As SqlDataAdapter
    Public daVSuc As SqlDataAdapter
    Public daTTipo As SqlDataAdapter
    Public daTArea As SqlDataAdapter
    Public daVMat As SqlDataAdapter
    Public daTUni As SqlDataAdapter
    Public daTUni1 As SqlDataAdapter
    Public daTPago As SqlDataAdapter
    Public daTMon As SqlDataAdapter

    'RESERVADO PARA DataTable() asno cututu esto solo utilizar en form secundarios
    Public dTable1 As SqlDataAdapter
    Public dTable2 As SqlDataAdapter
    Public dTable3 As SqlDataAdapter
    Public dTable4 As SqlDataAdapter
    Public dTable5 As SqlDataAdapter
    Public dTable6 As SqlDataAdapter
    Public dTable7 As SqlDataAdapter

    Public Sub crearDSAlmacen()
        'creando la instancia dataSet
        dsAlmacen = New DataSet
    End Sub

    Public Sub crearDataAdapterTable(ByRef DATable As SqlDataAdapter, ByVal sele As String)
        DATable = New SqlDataAdapter
        Dim cmSele As New SqlCommand
        cmSele.CommandType = CommandType.Text
        cmSele.CommandText = sele
        cmSele.Connection = Cn
        'Agregando el comado select al dataAdapter
        DATable.SelectCommand = cmSele
    End Sub
End Module
