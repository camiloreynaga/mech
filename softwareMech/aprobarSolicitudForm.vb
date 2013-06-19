Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class aprobarSolicitudForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource

    Private Sub aprobarSolicitudForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub aprobarSolicitudForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nro,fecSol,est,nombres,obs,lugar,estado,codigo,codPers from VSolTodo where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,tipoM,nombres,obs1,nombres1,obs2,idSol,codEstS,codAreaM from VDetSol where idSol=@idS and (codAreaM=@codA or codAreaM>@nro)" 'areaM,descrip
        crearDataAdapterTable(daVMat, sele)
        daVMat.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0
        daVMat.SelectCommand.Parameters.Add("@codA", SqlDbType.Int, 0).Value = 0
        daVMat.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        sele = "select codEstS,estSol from TEstSol"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select codAreaM,areaM from TAreaMat"
        crearDataAdapterTable(daTabla3, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolTodo")
            daVMat.Fill(dsAlmacen, "VDetSol")
            daTabla2.Fill(dsAlmacen, "TEstSol")
            daTabla3.Fill(dsAlmacen, "TAreaMat")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolTodo"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetSol"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "areaM,descrip"
            ModificarColumnasDGV()

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TEstSol"
            cbEstado.DataSource = BindingSource3
            cbEstado.DisplayMember = "estSol"
            cbEstado.ValueMember = "codEstS"

            configurarColorControl()

            If BindingSource1.Position <> -1 Then
                BindingSource1.Position = BindingSource1.Count - 1
            End If

            vfNro = 100
            vfVan1 = True
            visualizarDet()
            cbEstado.SelectedIndex = 1  'Aprobado

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub aprobarSolicitudForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(13) = 2 Then 'Aprobado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.YellowGreen
                dgTabla2.Rows(j).DefaultCellStyle.ForeColor = Color.DarkBlue
            End If
            If BindingSource2.Item(j)(13) = 3 Then 'Observado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.Red
            End If
            If BindingSource2.Item(j)(13) = 4 Then 'Rechazado
                dgTabla2.Rows(j).DefaultCellStyle.BackColor = Color.Yellow
            End If
        Next
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Private Sub cbArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbArea.SelectedIndexChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSol").Clear()
            daVMat.SelectCommand.Parameters("@idS").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daVMat.SelectCommand.Parameters("@codA").Value = cbArea.SelectedValue
            daVMat.SelectCommand.Parameters("@nro").Value = vfNro '100
            daVMat.Fill(dsAlmacen, "VDetSol")
            colorearFila()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub dgTabla2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla2.CurrentCellChanged
        enlazarText()
    End Sub

    Private Sub enlazarText()
        If BindingSource2.Count = 0 Then
            'desEnlazarText()
        Else
            txtObs.Text = BindingSource2.Item(BindingSource2.Position)(11)
            cbEstado.SelectedValue = BindingSource2.Item(BindingSource2.Position)(13)
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

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºSol"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fech_Sol"
            .Columns(2).Width = 70
            .Columns(3).Width = 70
            .Columns(3).HeaderText = "Estado"
            .Columns(4).Width = 120
            .Columns(4).HeaderText = "Solicitante"
            .Columns(5).Width = 360
            .Columns(5).HeaderText = "Observacion Solicitante"
            .Columns(6).Width = 280
            .Columns(6).HeaderText = "Lugar/Obra"
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Necesid."
            .Columns(1).Width = 65
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 280
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Cant."
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 45
            .Columns(4).HeaderText = "Unid."
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).HeaderText = "Estado"
            .Columns(5).Width = 75
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Area_Insumo"
            .Columns(6).Width = 100
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).HeaderText = "Tipo_Insumo"
            .Columns(7).Width = 100
            .Columns(7).Visible = False
            .Columns(7).ReadOnly = True 'NO editable
            .Columns(8).Width = 100
            .Columns(8).HeaderText = "Solicitante"
            .Columns(8).ReadOnly = True 'NO editable
            .Columns(9).Width = 300
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).Width = 100
            .Columns(10).HeaderText = "Verificador"
            .Columns(11).Width = 400
            .Columns(11).HeaderText = "Observacion verificador"
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
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
        Label3.ForeColor = ForeColorLabel
        Label11.ForeColor = ForeColorLabel
        btnAperturar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        cbVis.ForeColor = ForeColorLabel
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleSol where idSol=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarUltimoDoc(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroS) from TSolicitud where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnAperturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAperturar.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Seleccione Solicitud...")
            Exit Sub
        End If

        If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) = 0) Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Solicitud no tiene requerimientos...")
            Exit Sub
        End If

        If recuperarUltimoDoc(vSCodigo) <> CInt(BindingSource1.Item(BindingSource1.Position)(1)) Then
            MessageBox.Show("No se puede CAMBIAR por no ser la ultima solicitud procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        'If BindingSource2.Item(BindingSource2.Position)(13) = cbEstado.SelectedValue Then
        'StatusBarClass.messageBarraEstado(" Proceso denegado, Estados IGUALES")
        'Exit Sub
        'End If

        Dim resp As Short = MessageBox.Show("Esta segúro de cambiar Requerimiento a: " & cbEstado.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Cursor = Cursors.AppStarting
        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TDetalleSol
            comandoUpdate1(cbEstado.SelectedValue) '3=Anulado
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            Dim codDetS As Integer = BindingSource2.Item(BindingSource2.Position)(0)
            Dim idSol As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False
            'Actualizando el dataTable
            dsAlmacen.Tables("VSolTodo").Clear()
            daTabla1.Fill(dsAlmacen, "VSolTodo")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("idSol", idSol)
            vfVan1 = True
            visualizarDet()
            BindingSource2.Position = BindingSource2.Find("codDetS", codDetS)

            wait.Close()
            Me.Cursor = Cursors.Default
            'accionesIniciales()
            StatusBarClass.messageBarraEstado("  REQUERIMIENTO FUE CAMBIADO CON EXITO...")
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ALMACENO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal est As Short)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TDetalleSol set codPersA=@codP,codEstS=@anu,obs2=@obs where codDetS=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable1.Parameters.Add("@anu", SqlDbType.Int, 0).Value = est
        cmUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        vParam1 = "REQUERIMIENTOS SEMANALES OBRA: " & vSCodigo & " " & vSNomSuc
        Dim informe As New ReportViewerSolicitudForm
        informe.ShowDialog()
    End Sub
End Class
