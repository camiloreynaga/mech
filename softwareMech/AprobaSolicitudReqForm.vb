Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class AprobaSolicitudReqForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub AprobaSolicitudReqForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub AprobaSolicitudReqForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,nro,fecSol,dias,lugar,nombres,obs1,nombres1,obs2,nomResid,obs,idSol,codEstS,codAreaM,codPers,codMat,codPersRe,estado,codigo from VSolDetSolAproba where (codigo=@co1 or codigo>=@co2) and (codAreaM=@codA1 or codAreaM>@codA2)" 'areaM,descrip
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@co1", SqlDbType.VarChar, 10).Value = "00-00"
        daDetDoc.SelectCommand.Parameters.Add("@co2", SqlDbType.VarChar, 10).Value = "00-00"  'VIsualizar todo
        daDetDoc.SelectCommand.Parameters.Add("@codA1", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@codA2", SqlDbType.Int, 0).Value = 0

        sele = "select codAreaM,areaM from TAreaMat"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTUbi, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daDetDoc.Fill(dsAlmacen, "VSolDetSolAproba")
            daTabla2.Fill(dsAlmacen, "TAreaMat")
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolDetSolAproba"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource1.Sort = "idSol,codDetS"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VLugarTrabajoLogin"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"
            ModificarColumnasDGV()

            configurarColorControl()

            cbObra.SelectedValue = vSCodigo

            cbVis1.Checked = True
            cbVis2.Checked = True

            vfVan2 = True  'Filtrar Detalle Solicitud
            'visualizarDet()

            If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Or vSCodTipoUsu = 3 Then  '1=administrador de sistema 2=gerencia general 3=gerencia de construcciones
                'Solo administrador puede realizar este proceso
                AddContextMenu() 'Agregando menu antiClick
            End If

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

    Private Sub AprobaSolicitudReqForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(18) = 2 Then 'Aprobado
                dgTabla1.Rows(j).Cells(5).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(5).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(18) = 3 Then 'Observado
                dgTabla1.Rows(j).Cells(5).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(5).Style.ForeColor = Color.Red
            End If
            If BindingSource1.Item(j)(18) = 4 Then 'Rechazado
                dgTabla1.Rows(j).Cells(5).Style.BackColor = Color.Red
                dgTabla1.Rows(j).Cells(5).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub cbArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbArea.SelectedIndexChanged, cbObra.SelectedIndexChanged
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub visualizarDet()
        If vfVan2 Then
           
            AsignarValoresFiltro()

            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VSolDetSolAproba").Clear()
            daDetDoc.SelectCommand.Parameters("@co1").Value = codigo1
            daDetDoc.SelectCommand.Parameters("@co2").Value = codigo2
            daDetDoc.SelectCommand.Parameters("@codA1").Value = codArea1
            daDetDoc.SelectCommand.Parameters("@codA2").Value = codArea2
            daDetDoc.Fill(dsAlmacen, "VSolDetSolAproba")
            colorearFila()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim codigo1 As String
    Dim codigo2 As String
    Dim codArea1 As Short
    Dim codArea2 As Short
    Private Sub cbVis2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVis2.CheckedChanged, cbVis1.CheckedChanged
        visualizarDet()
    End Sub

    Private Sub AsignarValoresFiltro()
        If (cbVis2.Checked = True) And (cbVis1.Checked = True) Then 'Todas las obras y Todas las areas
            codigo1 = "00-00"
            codigo2 = "00-00"
            codArea1 = 0
            codArea2 = 0
        End If
        If (cbVis2.Checked = False) And (cbVis1.Checked = False) Then 'Una sola obra y UNa sola area
            codigo1 = cbObra.SelectedValue
            codigo2 = "00-99"
            codArea1 = cbArea.SelectedValue
            codArea2 = 99
        End If
        If (cbVis2.Checked = False) And (cbVis1.Checked = True) Then 'Solo Por obra y Todas las areas
            codigo1 = cbObra.SelectedValue
            codigo2 = "00-99"
            codArea1 = 0
            codArea2 = 0
        End If
        If (cbVis2.Checked = True) And (cbVis1.Checked = False) Then 'Todas las Obras y una sola Area
            codigo1 = "00-00"
            codigo2 = "00-00"
            codArea1 = cbArea.SelectedValue
            codArea2 = 99
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Neces."
            .Columns(1).Width = 60
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 340
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Cant."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 45
            .Columns(4).HeaderText = "Unid."
            .Columns(5).HeaderText = "Estado"
            .Columns(5).Width = 75
            .Columns(6).HeaderText = "Area_Insumo"
            .Columns(6).Width = 80
            .Columns(7).HeaderText = "NºSol"
            .Columns(7).Width = 40
            .Columns(8).HeaderText = "Fec_Sol"
            .Columns(8).Width = 70
            .Columns(9).HeaderText = "Dias"
            .Columns(9).Width = 35
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).HeaderText = "Obra / Lugar"
            .Columns(10).Width = 300
            .Columns(11).Width = 100
            .Columns(11).HeaderText = "Solicitante"
            .Columns(12).Width = 400
            .Columns(12).HeaderText = "Observacion solicitante"
            .Columns(13).Width = 100
            .Columns(13).HeaderText = "Verificador"
            .Columns(14).Width = 400
            .Columns(14).HeaderText = "Observacion verificador"
            .Columns(15).Width = 100
            .Columns(15).HeaderText = "Residente"
            .Columns(16).Width = 400
            .Columns(16).HeaderText = "Observacion Residente"
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
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
        cbVis1.ForeColor = ForeColorLabel
        cbVis2.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    '-----------------------------------------
    '------Menu antiClick Anular---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    WithEvents toolStripItem4 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "APROBADO"
        toolStripItem1.BackColor = Color.Green
        toolStripItem2.Text = "OBSERVADO"
        toolStripItem2.BackColor = Color.Yellow
        toolStripItem3.Text = "RECHAZADO"
        toolStripItem3.BackColor = Color.Red
        toolStripItem4.Text = "PENDIENTE"
        toolStripItem4.BackColor = Color.White
        Dim strip As New ContextMenuStrip()
        For Each column As DataGridViewColumn In dgTabla1.Columns()
            column.ContextMenuStrip = strip
            column.ContextMenuStrip.Items.Add(toolStripItem1)
            column.ContextMenuStrip.Items.Add(toolStripItem2)
            column.ContextMenuStrip.Items.Add(toolStripItem3)
            column.ContextMenuStrip.Items.Add(toolStripItem4)
        Next
    End Sub

    Private mouseLocation As DataGridViewCellEventArgs
    Private Sub dgTabla1_CellMouseEnter(ByVal sender As Object, ByVal location As DataGridViewCellEventArgs) Handles dgTabla1.CellMouseEnter
        mouseLocation = location
    End Sub

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(18) = 2 Then  '1=pendiente  3=Observado
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, YA ESTA APROBADO...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de APROBAR insumo: " & BindingSource1.Item(BindingSource1.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
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
            Dim position As Short = BindingSource1.Position

            'TDetalleSol
            comandoUpdate17(2, "") '2=aprobado
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

            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.BackColor = Color.Green 'Color.YellowGreen
            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.ForeColor = Color.White

            BindingSource1.Item(BindingSource1.Position)(18) = 2
            BindingSource1.Item(BindingSource1.Position)(5) = "APROBADO"
            BindingSource1.Item(BindingSource1.Position)(13) = recuperarPers(vPass)

            If position < BindingSource1.Count - 1 Then
                BindingSource1.Position = position + 1  '
            End If

            StatusBarClass.messageBarraEstado("  INSUMO FUE APROBADO CON EXITO...")
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
        cmUpdateTable17.CommandText = "update TDetalleSol set codPersA=@codP,codEstS=@anu,obs2=@obs where codDetS=@cod"
        cmUpdateTable17.Connection = Cn
        cmUpdateTable17.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable17.Parameters.Add("@anu", SqlDbType.Int, 0).Value = est
        cmUpdateTable17.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmUpdateTable17.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Function recuperarPers(ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select nombre+' '+apellido from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A OBSERVAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(18) = 3 Then  '2=aprobado  3=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA OBSERVADO...")
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
            Dim position As Short = BindingSource1.Position

            'TDetalleSol
            comandoUpdate17(3, vObs) '3=OBSERVADO
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

            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.BackColor = Color.Yellow
            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.ForeColor = Color.Red

            BindingSource1.Item(BindingSource1.Position)(18) = 3
            BindingSource1.Item(BindingSource1.Position)(14) = vObs
            BindingSource1.Item(BindingSource1.Position)(5) = "OBSERVADO"
            BindingSource1.Item(BindingSource1.Position)(13) = recuperarPers(vPass)

            If position < BindingSource1.Count - 1 Then
                BindingSource1.Position = position + 1
            End If

            StatusBarClass.messageBarraEstado("  GASTO FUE OBSERVADO CON EXITO...")
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
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A RECHAZAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(18) = 4 Then  '2=aprobado  3=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA RECHAZADO...")
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
            Dim position As Short = BindingSource1.Position

            'TDetalleSol
            comandoUpdate17(4, vObs) '4=rechazado
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

            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.BackColor = Color.Red
            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.ForeColor = Color.White

            BindingSource1.Item(BindingSource1.Position)(18) = 4
            BindingSource1.Item(BindingSource1.Position)(14) = vObs
            BindingSource1.Item(BindingSource1.Position)(5) = "RECHAZADO"
            BindingSource1.Item(BindingSource1.Position)(13) = recuperarPers(vPass)

            If position < BindingSource1.Count - 1 Then
                BindingSource1.Position = position + 1
            End If

            StatusBarClass.messageBarraEstado("  GASTO FUE RECHAZADO CON EXITO...")
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

    Private Sub toolStripItem4_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem4.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(18) = 1 Then  '1=pendiente  3=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA PENDIENTE...")
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
            Dim position As Short = BindingSource1.Position

            'TDetalleSol
            comandoUpdate17(1, "") '1=pendiente
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

            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.BackColor = Color.White
            dgTabla1.Rows(BindingSource1.Position).Cells(5).Style.ForeColor = Color.Black

            BindingSource1.Item(BindingSource1.Position)(18) = 1
            BindingSource1.Item(BindingSource1.Position)(5) = "PENDIENTE"
            BindingSource1.Item(BindingSource1.Position)(13) = recuperarPers(vPass)

            If position < BindingSource1.Count - 1 Then
                BindingSource1.Position = position + 1  '
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
End Class
