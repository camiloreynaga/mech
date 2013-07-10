Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class AsignarSerieOrdenPersForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

    Private Sub AsignarSerieOrdenPersForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub AsignarSerieOrdenPersForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codPers,nombre+' '+apellido as nom from TPersonal where estado=1"
        crearDataAdapterTable(daTabla1, sele)

        'sele = "select codSerO,serie,iniNroDoc,descrip from TSerieOrden where estado=1 and not codSerO in(select codSerO from TSeriePers) order by serie"
        sele = "select codSerO,serie,iniNroDoc,descrip from TSerieOrden where estado=1 order by serie"
        crearDataAdapterTable(daTabla3, sele)

        sele = "select codSP,serie,iniNroDoc,descrip,codPers,codSerO from VTSerie order by codSP"
        crearDataAdapterTable(daTabla4, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetRestaurantModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "TPersonal")
            daTabla3.Fill(dsAlmacen, "TSerieOrden")
            daTabla4.Fill(dsAlmacen, "VTSerie")

            AgregarRelacion()

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TPersonal"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TSerieOrden"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa

            BindingSource4.DataSource = BindingSource1
            BindingSource4.DataMember = "Relacion2"
            dgTabla3.DataSource = BindingSource4
            ModificarColumnasDGV()

            configurarColorControl()

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation2 As New DataRelation("Relacion2", dsAlmacen.Tables("TPersonal").Columns("codPers"), dsAlmacen.Tables("VTSerie").Columns("codPers"))
        dsAlmacen.Relations.Add(relation2)
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "CodPers"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nombres"
            .Columns(1).Width = 300
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nºser"
            .Columns(1).Width = 45
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Nºdoc_ini"
            .Columns(2).Width = 75
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 160
            .Columns(3).HeaderText = "Descripción"
            '.Columns(5).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla3
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nºserie"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Nºdoc_inic."
            .Columns(2).Width = 75
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).Width = 180
            .Columns(3).HeaderText = "Descripción"
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
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
        Label4.ForeColor = ForeColorLabel
        btnAsignar.ForeColor = ForeColorButtom
        btnQuitar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        dgTabla3.Dispose()
        Me.Close()
    End Sub

    Private Function recuperarCount(ByVal codser As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TSeriePers where codSerO=" & codser
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount1(ByVal codPers As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TSeriePers where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnAsignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsignar.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE PERSONAL...")
            Exit Sub
        End If
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE SERIE DE ORDEN PARA ASIGNAR A PERSONAL...")
            Exit Sub
        End If

        'If recuperarCount(BindingSource3.Item(BindingSource3.Position)(0)) > 0 Then
        '    StatusBarClass.messageBarraEstado("  YA SE ASIGNO SERIE DE ORDEN DE DESEMBOLSO...")
        '    Exit Sub
        'End If

        If recuperarCount1(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            MessageBox.Show("Proceso denegado, Usuario solo puede tener una serie de orden de desembolso asignada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta Segúro de Asignar Serie Nº " & BindingSource3.Item(BindingSource3.Position)(1) & " de Orden de Desembolso" & Chr(13) & "a Personal: " & BindingSource1.Item(BindingSource1.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            'TSeriePers
            comandoInsert(BindingSource1.Item(BindingSource1.Position)(0), BindingSource3.Item(BindingSource3.Position)(0))
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("TSerieOrden").Clear()
            dsAlmacen.Tables("VTSerie").Clear()

            daTabla3.Fill(dsAlmacen, "TSerieOrden")
            daTabla4.Fill(dsAlmacen, "VTSerie")

            BindingSource4.Position = BindingSource4.Count - 1
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        If BindingSource4.Position = -1 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE SERIE DE ORDEN A QUITAR...")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de quitar serie de orden asignado a personal?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TSeriePers
            comandoDelete(BindingSource4.Item(BindingSource4.Position)(0))
            cmDeleteTable.Transaction = myTrans
            cmDeleteTable.ExecuteNonQuery()

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("TSerieOrden").Clear()
            dsAlmacen.Tables("VTSerie").Clear()

            daTabla3.Fill(dsAlmacen, "TSerieOrden")
            daTabla4.Fill(dsAlmacen, "VTSerie")

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué procesado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert(ByVal cod As String, ByVal codSer As Integer)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TSeriePers(codPers,codSerO) values(@cod,@codSer)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = cod
        cmInserTable.Parameters.Add("@codSer", SqlDbType.Int, 0).Value = codSer
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete(ByVal codSS As Short)
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TSeriePers where codSP=@nro"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = codSS
    End Sub
End Class
