Imports System.Data
Imports System.Data.SqlClient


Public Class cDataManager

    ''' <summary>
    ''' Carga un ComboBox Usando un data reader
    ''' </summary>
    ''' <param name="storedProcedure">Nombre del procedimiento almacenado</param>
    ''' <param name="combo">ComboBox</param>
    ''' <param name="ValueMember">Valor del Item</param>
    ''' <param name="DisplayMember">Descripcion del Item</param>
    ''' <remarks></remarks>
    Public Sub CargarCombo(ByVal storedProcedure As String, ByVal Type As CommandType, ByVal combo As ComboBox, ByVal ValueMember As String, ByVal DisplayMember As String)
        Dim con As SqlConnection = Cn
        Dim command As New SqlCommand(storedProcedure, con)
        Dim dataR As SqlDataReader

        command.CommandType = Type 'CommandType.StoredProcedure
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        dataR = command.ExecuteReader(CommandBehavior.CloseConnection)
        Dim lista As New List(Of Dato)

        Try
            While dataR.Read()
                Dim item As New Dato
                item.Id = CStr(dataR(ValueMember))
                item.Desc = CStr(dataR(DisplayMember))
                lista.Add(item)
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()
        End Try

        combo.DataSource = lista
        combo.ValueMember = "Id"
        combo.DisplayMember = "Desc"

    End Sub

    ''' <summary>
    ''' Llenar un Datagrid de datos desde una BD
    ''' </summary>
    ''' <param name="consulta">consulta o stored procedure</param>
    ''' <param name="type">tipo </param>
    ''' <param name="grilla">DatagridView</param>
    ''' <param name="dataView">DataView</param>
    ''' <remarks></remarks>
    Public Sub CargarGrilla(ByVal consulta As String, ByVal type As CommandType, ByVal grilla As DataGridView, ByVal dataView As DataView)
        Dim con As SqlConnection = Cn
        Dim storedProcedure As String = consulta '
        Dim command As New SqlCommand(consulta, con)
        Dim dataR As SqlDataReader

        command.CommandType = type  ' tipo de Comando
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim oTabla As New DataTable

        dataR = command.ExecuteReader() 'CommandBehavior.CloseConnection)
        'Dim lista As New List(Of Dato)
        ' oTabla.Rows.Add(New DataRow(
        Try

            oTabla.Load(dataR, LoadOption.OverwriteChanges)

            dataView = oTabla.DefaultView
            'While dataR.Read()
            '    grilla.Rows.Add(dataR.GetInt32(0), dataR.GetString(1), dataR.GetString(2), dataR.GetDecimal(3), dataR.GetString(4), dataR.GetString(5))
            'End While


            grilla.DataSource = dataView  'oTabla
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Llenar un Datagrid de datos desde una BD
    ''' </summary>
    ''' <param name="consulta">consulta o stored procedure</param>
    ''' <param name="type">tipo </param>
    ''' <param name="grilla">DatagridView</param>
    ''' <param name="bindingSource">BindingSource</param>
    ''' <remarks></remarks>
    Public Function CargarGrilla(ByVal consulta As String, ByVal type As CommandType, ByVal grilla As DataGridView, ByVal bindingSource As BindingSource) As Integer
        Dim con As SqlConnection = Cn
        Dim storedProcedure As String = consulta '
        Dim command As New SqlCommand(consulta, con)
        Dim dataR As SqlDataReader

        command.CommandType = type  ' tipo de Comando
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim oTabla As New DataTable

        dataR = command.ExecuteReader() 'CommandBehavior.CloseConnection)
        'Dim lista As New List(Of Dato)
        ' oTabla.Rows.Add(New DataRow(
        Try

            oTabla.Load(dataR, LoadOption.OverwriteChanges)

            'llena la grilla con el binding source
            bindingSource.DataSource = oTabla '.DefaultView

            'While dataR.Read()
            '    grilla.Rows.Add(dataR.GetInt32(0), dataR.GetString(1), dataR.GetString(2), dataR.GetDecimal(3), dataR.GetString(4), dataR.GetString(5))
            'End While


            grilla.DataSource = bindingSource  'oTabla
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()

        End Try
        Return oTabla.Rows.Count
    End Function

    ''' <summary>
    ''' Llenar un Datagrid de datos desde una BD
    ''' </summary>
    ''' <param name="consulta">consulta o stored procedure</param>
    ''' <param name="type">tipo </param>
    ''' <param name="grilla">DatagridView</param>
    ''' <param name="bindingSource">BindingSource</param>
    ''' <remarks></remarks>
    Public Function CargarGrilla2(ByVal consulta As String, ByVal type As CommandType, ByVal grilla As DataGridView, ByVal bindingSource As BindingSource) As DataTable
        Dim con As SqlConnection = Cn
        Dim storedProcedure As String = consulta '
        Dim command As New SqlCommand(consulta, con)
        Dim dataR As SqlDataReader

        command.CommandType = type  ' tipo de Comando
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim oTabla As New DataTable

        dataR = command.ExecuteReader() 'CommandBehavior.CloseConnection)
        'Dim lista As New List(Of Dato)
        ' oTabla.Rows.Add(New DataRow(
        Try

            oTabla.Load(dataR, LoadOption.OverwriteChanges)

            'llena la grilla con el binding source
            bindingSource.DataSource = oTabla '.DefaultView

            'While dataR.Read()
            '    grilla.Rows.Add(dataR.GetInt32(0), dataR.GetString(1), dataR.GetString(2), dataR.GetDecimal(3), dataR.GetString(4), dataR.GetString(5))
            'End While


            grilla.DataSource = bindingSource  'oTabla
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dataR.Close()

        End Try
        Return oTabla
    End Function
End Class
