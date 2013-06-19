Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class chekeoSolicitudReqForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource3 As New BindingSource

    Private Sub chekeoSolicitudReqForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub chekeoSolicitudReqForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nro,fecSol,nroCotiConca,est,nombres,obs,lugar,estDet,codigo,codPers from VSolAperAprobado where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetS,cant,unidad,descrip,estRec,areaM,nombres1,obs3,nombres,obs1,idSol,codEstS,codAreaM,estRecep,codPers,codPersR from VDetSolAprobado where idSol=@idS and (codAreaM=@codA or codAreaM>@nro)" 'areaM,descrip
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@codA", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        sele = "select codAreaM,areaM from TAreaMat"
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolAperAprobado")
            daDetDoc.Fill(dsAlmacen, "VDetSolAprobado")
            daTabla2.Fill(dsAlmacen, "TAreaMat")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolAperAprobado"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "idSol"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VDetSolAprobado"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource3.Sort = "areaM,descrip"
            ModificarColumnasDGV()

            configurarColorControl()

            BindingSource1.Position = BindingSource1.Count - 1  'VISUALIZAR ULTIMA SOLICITUD

            'vfNro = 100 'POR AREA
            vfNro = 0 'todas las areas
            vfVan2 = True  'Filtrar Detalle Solicitud
            visualizarDet()

            AddContextMenu() 'Agregando menu antiClick

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    Private Sub chekeoSolicitudReqForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(13) = 1 Then 'Completo
                dgTabla2.Rows(j).Cells(4).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(4).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(13) = 2 Then 'Incompleto
                dgTabla2.Rows(j).Cells(4).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(4).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Width = 50
            .Columns(1).HeaderText = "NºSol."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 70
            .Columns(3).Width = 90
            .Columns(3).HeaderText = "Relac.Cotiz."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderText = "Est.Check"
            .Columns(4).Width = 70
            .Columns(5).Width = 100
            .Columns(5).HeaderText = "Solicitante"
            .Columns(6).HeaderText = "Observacion"
            .Columns(6).Width = 200
            .Columns(7).HeaderText = "Obra"
            .Columns(7).Width = 250
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 50
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Unid."
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 340
            .Columns(4).HeaderText = "Recibido"
            .Columns(4).Width = 75
            .Columns(5).HeaderText = "Area_Insumo"
            .Columns(5).Width = 100
            .Columns(6).Width = 100
            .Columns(6).HeaderText = "Recepcionista"
            .Columns(7).Width = 450
            .Columns(7).HeaderText = "Observacion Recepcionista"
            .Columns(8).Width = 100
            .Columns(8).HeaderText = "Solicitante"
            .Columns(9).Width = 400
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
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
        Label3.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        cbVis.ForeColor = ForeColorLabel
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub visualizarDet()
        If vfVan2 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSolAprobado").Clear()
            daDetDoc.SelectCommand.Parameters("@idS").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.SelectCommand.Parameters("@codA").Value = cbArea.SelectedValue
            daDetDoc.SelectCommand.Parameters("@nro").Value = vfNro '100
            daDetDoc.Fill(dsAlmacen, "VDetSolAprobado")
            colorearFila()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfNro As Short
    Private Sub cbVis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVis.CheckedChanged
        If cbVis.Checked Then 'Todas las areas
            vfNro = 0
        Else  'por area
            vfNro = 100
        End If
        visualizarDet()
    End Sub

    Private Sub cbArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbArea.SelectedIndexChanged
        visualizarDet()
    End Sub

    '-----------------------------------------
    '------Menu antiClick Anular---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "COMPLETO"
        toolStripItem1.BackColor = Color.Green
        toolStripItem2.Text = "INCOMPLETO"
        toolStripItem2.BackColor = Color.Red
        toolStripItem3.Text = "PENDIENTE"
        toolStripItem3.BackColor = Color.White
        Dim strip As New ContextMenuStrip()
        For Each column As DataGridViewColumn In dgTabla2.Columns()
            column.ContextMenuStrip = strip
            column.ContextMenuStrip.Items.Add(toolStripItem1)
            column.ContextMenuStrip.Items.Add(toolStripItem2)
            column.ContextMenuStrip.Items.Add(toolStripItem3)
        Next
    End Sub

    Private mouseLocation As DataGridViewCellEventArgs
    Private Sub dgTabla2_CellMouseEnter(ByVal sender As Object, ByVal location As DataGridViewCellEventArgs) Handles dgTabla2.CellMouseEnter
        mouseLocation = location
    End Sub

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(8) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) = 1 Then  '1=completo
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA COMPROBADO...")
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()
            Dim position As Short = BindingSource3.Position

            'TDetalleSol
            comandoUpdate17(1, "") '1=completo
            cmUpdateTable17.Transaction = myTrans
            If cmUpdateTable17.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.BackColor = Color.Green 'Color.YellowGreen
            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.ForeColor = Color.White

            BindingSource3.Item(BindingSource3.Position)(13) = 1
            BindingSource3.Item(BindingSource3.Position)(4) = "COMPLETO"
            BindingSource3.Item(BindingSource3.Position)(6) = recuperarPers(vPass)

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1  '
            End If

            StatusBarClass.messageBarraEstado("  INSUMO FUE COMPROBADO CON EXITO...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If
        End Try
    End Sub

    Dim cmUpdateTable17 As SqlCommand
    Private Sub comandoUpdate17(ByVal est As Short, ByVal obs As String)
        cmUpdateTable17 = New SqlCommand
        cmUpdateTable17.CommandType = CommandType.Text
        cmUpdateTable17.CommandText = "update TDetalleSol set codPersR=@codP,estRecep=@est,obs3=@obs where codDetS=@cod"
        cmUpdateTable17.Connection = Cn
        cmUpdateTable17.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable17.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable17.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmUpdateTable17.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Function recuperarPers(ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select nombre+' '+apellido from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(8) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        Dim comentario As New CometarioForm
        comentario.ShowDialog()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()
            Dim position As Short = BindingSource3.Position

            'TDetalleSol
            comandoUpdate17(2, vObs) '2=INCOMPLETO
            cmUpdateTable17.Transaction = myTrans
            If cmUpdateTable17.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.BackColor = Color.Red
            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.ForeColor = Color.White

            BindingSource3.Item(BindingSource3.Position)(13) = 2
            BindingSource3.Item(BindingSource3.Position)(7) = vObs
            BindingSource3.Item(BindingSource3.Position)(4) = "INCOMPLETO"
            BindingSource3.Item(BindingSource3.Position)(6) = recuperarPers(vPass)

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1
            End If

            StatusBarClass.messageBarraEstado("  CHEKEADO CON EXITO...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If
        End Try
    End Sub

    Private Sub toolStripItem3_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem3.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(8) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        'If BindingSource3.Item(BindingSource3.Position)(13) = 0 Then  '0=pendiente
        'StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA PENDIENTE...")
        'Exit Sub
        'End If

        Dim comentario As New CometarioForm
        comentario.ShowDialog()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()
            Dim position As Short = BindingSource3.Position

            'TDetalleSol
            comandoUpdate17(0, vObs) '0=pendiente
            cmUpdateTable17.Transaction = myTrans
            If cmUpdateTable17.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.BackColor = Color.White
            dgTabla2.Rows(BindingSource3.Position).Cells(4).Style.ForeColor = Color.Black

            BindingSource3.Item(BindingSource3.Position)(13) = 0
            BindingSource3.Item(BindingSource3.Position)(4) = "PENDIENTE"
            BindingSource3.Item(BindingSource3.Position)(6) = recuperarPers(vPass)

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1  '
            End If

            'StatusBarClass.messageBarraEstado("  INSUMO FUE COMPROBADO CON EXITO...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If
        End Try
    End Sub

    Private Sub btnCerrarSol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrarSol.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Solicitud a CERRAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(8) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Ya esta <CERRADO>")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de CERRAR Solicitud Nº " & BindingSource1.Item(BindingSource1.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Refresh()
        Dim finalMytrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Try
            StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, PROCESANDO INFORMACION....")
            Dim campo As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            'TSolicitud
            comandoUpdate1(1) '1=cerrado
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            vfVan2 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAperAprobado").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAperAprobado")

            BindingSource1.Position = BindingSource1.Find("idSol", campo)
            vfVan2 = True  'Filtrar Detalle Solicitud
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Grupo fúe cerrado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal estado As Short)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TSolicitud set estDet=@est where idSol=@idSol"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable1.Parameters.Add("@idSol", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub
End Class
