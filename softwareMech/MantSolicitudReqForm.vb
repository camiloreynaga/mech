Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class MantSolicitudReqForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource

    Private Sub MantSolicitudReqForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        ' Primero capturamos la tecla pulsada, ... 
        If keyData = Keys.F5 Then
            btnProcesa.PerformClick()
        End If

        ' ... y después llamamos al procedimiento de la clase base. 
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub MantSolicitudReqForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nro+' '+est as nro,fecSol,est,nombres,lugar,obs,estado,codigo,codPers,nro as nroS from VSolAper where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,tipoM,nombres,obs1,nombres1,obs2,idSol,codEstS,codAreaM,codPers,codMat from VDetSol where idSol=@idS and (codAreaM=@codA or codAreaM>@nro)" 'areaM,descrip
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
            daTabla1.Fill(dsAlmacen, "VSolAper")
            daDetDoc.Fill(dsAlmacen, "VDetSol")
            daTabla2.Fill(dsAlmacen, "TAreaMat")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolAper"
            lbSol.DataSource = BindingSource1
            lbSol.DisplayMember = "nro"
            lbSol.ValueMember = "idSol"
            BindingSource1.Sort = "nroS"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VDetSol"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource3.Sort = "areaM,descrip"
            ModificarColumnasDGV()

            configurarColorControl()
            recuperarUltimoNro(vSCodigo)

            vfVan1 = True 'enlazar texto solicitud
            cbBuscar.SelectedIndex = 0
            BindingSource1.Position = BindingSource1.Count - 1  'VISUALIZAR ULTIMA SOLICITUD
            enlazarText()

            vfNro = 100
            vfVan2 = True  'Filtrar Detalle Solicitud
            visualizarDet()
            cbNec.SelectedIndex = 0

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

    Private Sub MantSolicitudReqForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        colorear()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(13) = 2 Then 'Aprobado
                dgTabla2.Rows(j).Cells(5).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(5).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(13) = 3 Then 'Observado
                dgTabla2.Rows(j).Cells(5).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(5).Style.ForeColor = Color.Red
            End If
            If BindingSource3.Item(j)(13) = 4 Then 'Rechazado
                dgTabla2.Rows(j).Cells(5).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(5).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub cbArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbArea.SelectedIndexChanged
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub visualizarDet()
        If vfVan2 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSol").Clear()
            daDetDoc.SelectCommand.Parameters("@idS").Value = lbSol.SelectedValue 'BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.SelectCommand.Parameters("@codA").Value = cbArea.SelectedValue
            daDetDoc.SelectCommand.Parameters("@nro").Value = vfNro '100
            daDetDoc.Fill(dsAlmacen, "VDetSol")
            colorearFila()
            colorear()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfNro As Short
    Private Sub cbVis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVis.CheckedChanged
        If cbVis.Checked Then 'Todas las areas
            vfNro = 0
            btnAgrega.Enabled = False
        Else  'por area
            vfNro = 100
            btnAgrega.Enabled = True
        End If
        visualizarDet()
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource3.Count - 1
            dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(3).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(9).Style.BackColor = Color.AliceBlue
        Next
    End Sub

    Private Sub lbSol_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSol.SelectedValueChanged
        enlazarText()
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub enlazarText()
        If vfVan1 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource1.Count = 0 Then
                'desEnlazarText()
            Else
                date1.Value = BindingSource1.Item(lbSol.SelectedIndex)(2)
                txtObs.Text = BindingSource1.Item(lbSol.SelectedIndex)(6)
                txtPers.Text = BindingSource1.Item(lbSol.SelectedIndex)(4)
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Necesid."
            .Columns(1).Width = 65
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 340
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
            .Columns(15).Visible = False
            .Columns(16).Visible = False
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
        Label4.ForeColor = ForeColorLabel
        Label5.ForeColor = ForeColorLabel
        Label6.ForeColor = ForeColorLabel
        Label7.ForeColor = ForeColorLabel
        Label8.ForeColor = ForeColorLabel
        Label9.ForeColor = ForeColorLabel
        Label10.ForeColor = ForeColorLabel
        Label11.ForeColor = ForeColorLabel
        btnAperturar.ForeColor = ForeColorButtom
        btnCrear.ForeColor = ForeColorButtom
        btnProcesa.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        cbVis.ForeColor = ForeColorLabel
    End Sub

    Private Sub recuperarUltimoNro(ByVal cod As String)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroS),0)+1 from TSolicitud where codigo='" & cod & "'"
        cmdMaxCodigo.Connection = Cn
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

    Private Sub asignarNro(ByVal max As Integer)
        Select Case CInt(max)
            Case Is < 99
                txtNro.Text = "000" & max
            Case 100 To 999
                txtNro.Text = "00" & max
            Case 1000 To 9999
                txtNro.Text = "0" & max
            Case Is > 9999
                txtNro.Text = max
        End Select
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

    Private Sub btnAperturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAperturar.Click
        If BindingSource1.Position <> -1 Then
            BindingSource1.Position = BindingSource1.Count - 1
            If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) = 0) Then
                StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud NO tiene registros en detalle solicitud...")
                Exit Sub
            End If
        End If

        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de aperturar solicitud de requerimiento Nº " & txtNro.Text, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        recuperarUltimoNro(vSCodigo)
        Dim campo As String = txtNro.Text

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TSolicitud  cambiando abierto=0 a cerrado=1
            comandoUpdate13()
            cmUpdateTable13.Transaction = myTrans
            cmUpdateTable13.ExecuteNonQuery()

            'TSolicitud
            comandoInsert()
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan1 = False
            vfVan2 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("nroS", campo)

            vfVan1 = True
            vfVan2 = True
            enlazarText()
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

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
                MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TSolicitud(nroS,fecSol,codPers,codigo,estado,obs) values(@nro,@fec,@codP,@cod,0,@obs)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo
        cmInserTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
    End Sub

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TSolicitud set estado=1 where estado=0 and codigo='" & vSCodigo & "'"
        cmUpdateTable13.Connection = Cn
    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
            Exit Sub
        End If
        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            MessageBox.Show("No se puede modificar solicitud por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta seguro de modificar Solicitud Nº " & BindingSource1.Item(BindingSource1.Position)(10), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TSolicitud
            comandoUpdate1()
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            Dim idSol As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False
            vfVan2 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            BindingSource1.Position = BindingSource1.Find("idSol", idSol)

            vfVan1 = True
            vfVan2 = True
            enlazarText()
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
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
            Exit Sub
        End Try
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TSolicitud set fecSol=@fec,codPers=@codP,obs=@obs where idSol=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable1.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Function recuperarUltimoDoc(ByVal codigo As String) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroS) from TSolicitud where codigo='" & codigo & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCountCotizacion(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TCotizacion where idSol=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc(BindingSource1.Item(BindingSource1.Position)(8)) <> CInt(BindingSource1.Item(BindingSource1.Position)(10)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima solicitud ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud tiene registros en detalle solicitud...")
            Exit Sub
        End If

        If recuperarCountCotizacion(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud tiene Instancia en Cotización...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar solicitud Nº " & BindingSource1.Item(BindingSource1.Position)(10), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TSolicitud
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True
            vfVan1 = False
            vfVan2 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VSolAper").Clear()
            daTabla1.Fill(dsAlmacen, "VSolAper")

            vfVan1 = True
            vfVan2 = True
            enlazarText()
            visualizarDet()
            recuperarUltimoNro(vSCodigo)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()

        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TSolicitud where idSol=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub txtObs_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObs.GotFocus, txtObs.MouseClick
        txtObs.SelectAll()
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
        For Each column As DataGridViewColumn In dgTabla2.Columns()
            column.ContextMenuStrip = strip
            column.ContextMenuStrip.Items.Add(toolStripItem1)
            column.ContextMenuStrip.Items.Add(toolStripItem2)
            column.ContextMenuStrip.Items.Add(toolStripItem3)
            column.ContextMenuStrip.Items.Add(toolStripItem4)
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

        If recuperarUltimoDoc(vSCodigo) <> CInt(BindingSource1.Item(BindingSource1.Position)(10)) Then
            MessageBox.Show("No se puede CAMBIAR por no ser la ultima solicitud procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) = 2 Then  '1=pendiente  3=Observado
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

            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.BackColor = Color.Green 'Color.YellowGreen
            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.ForeColor = Color.White

            BindingSource3.Item(BindingSource3.Position)(13) = 2
            BindingSource3.Item(BindingSource3.Position)(5) = "APROBADO"
            BindingSource3.Item(BindingSource3.Position)(10) = recuperarPers(vPass)

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
        cmUpdateTable17.CommandText = "update TDetalleSol set codPersA=@codP,codEstS=@anu,obs2=@obs where codDetS=@cod"
        cmUpdateTable17.Connection = Cn
        cmUpdateTable17.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable17.Parameters.Add("@anu", SqlDbType.Int, 0).Value = est
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

        If recuperarUltimoDoc(vSCodigo) <> CInt(BindingSource1.Item(BindingSource1.Position)(10)) Then
            MessageBox.Show("No se puede CAMBIAR por no ser la ultima solicitud procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) = 3 Then  '2=aprobado  3=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA COMPROBADO...")
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

            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.BackColor = Color.Yellow
            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.ForeColor = Color.Red

            BindingSource3.Item(BindingSource3.Position)(13) = 3
            BindingSource3.Item(BindingSource3.Position)(11) = vObs
            BindingSource3.Item(BindingSource3.Position)(5) = "OBSERVADO"
            BindingSource3.Item(BindingSource3.Position)(10) = recuperarPers(vPass)

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1
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
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If recuperarUltimoDoc(vSCodigo) <> CInt(BindingSource1.Item(BindingSource1.Position)(10)) Then
            MessageBox.Show("No se puede CAMBIAR por no ser la ultima solicitud procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) = 4 Then  '2=aprobado  3=Observado
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
            Dim position As Short = BindingSource3.Position

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

            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.BackColor = Color.Red
            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.ForeColor = Color.White

            BindingSource3.Item(BindingSource3.Position)(13) = 4
            BindingSource3.Item(BindingSource3.Position)(11) = vObs
            BindingSource3.Item(BindingSource3.Position)(5) = "RECHAZADO"
            BindingSource3.Item(BindingSource3.Position)(10) = recuperarPers(vPass)

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1
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
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO...")
            Exit Sub
        End If

        If recuperarUltimoDoc(vSCodigo) <> CInt(BindingSource1.Item(BindingSource1.Position)(10)) Then
            MessageBox.Show("No se puede CAMBIAR por no ser la ultima solicitud procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) = 1 Then  '1=pendiente  3=Observado
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
            Dim position As Short = BindingSource3.Position

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

            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.BackColor = Color.White
            dgTabla2.Rows(BindingSource3.Position).Cells(5).Style.ForeColor = Color.Black

            BindingSource3.Item(BindingSource3.Position)(13) = 1
            BindingSource3.Item(BindingSource3.Position)(5) = "PENDIENTE"
            BindingSource3.Item(BindingSource3.Position)(10) = recuperarPers(vPass)

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

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            MessageBox.Show("No se puede actualizar requerimientos por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource3.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE REGISTROS A PROCESAR...")
            Exit Sub
        End If

        Dim vCom As Boolean = False
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(13) = 1 Or BindingSource3.Item(j)(13) = 3 Then '1=pendiente 3=observado
                vCom = True
                Exit For
            End If
        Next

        If vCom = False Then
            MessageBox.Show("Proceso denegado en actualizar modificaciones, por estar en el estado de [APROBADO]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("ESTA SEGURO DE GUARDAR MODIFICACIONES?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        Dim finalMytrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")

            For j As Short = 0 To BindingSource3.Count - 1
                If BindingSource3.Item(j)(13) = 1 Or BindingSource3.Item(j)(13) = 3 Then '1=pendiente  3=Observado
                    If BindingSource3.Item(j)(15) = vPass Then
                        'actualizando TDetalleSol
                        comandoUpdate15(BindingSource3.Item(j)(1).ToString().Trim(), BindingSource3.Item(j)(3), BindingSource3.Item(j)(9).ToString().Trim(), BindingSource3.Item(j)(0))
                        cmdUpdateTable15.Transaction = myTrans
                        If cmdUpdateTable15.ExecuteNonQuery() < 1 Then
                            wait.Close()
                            Me.Cursor = Cursors.Default
                            'deshace la transaccion
                            myTrans.Rollback()
                            MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                            Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

            visualizarDet()

            StatusBarClass.messageBarraEstado("  Registros fué actualizado con exito...")
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
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmdUpdateTable15 As SqlCommand
    Private Sub comandoUpdate15(ByVal pri As String, ByVal can As Decimal, ByVal obs As String, ByVal codDetS As Integer)
        cmdUpdateTable15 = New SqlCommand
        cmdUpdateTable15.CommandType = CommandType.Text
        cmdUpdateTable15.CommandText = "update TDetalleSol set prioridad=@pri,cant=@can,obs1=@obs where codDetS=@cod"
        cmdUpdateTable15.Connection = Cn
        cmdUpdateTable15.Parameters.Add("@pri", SqlDbType.VarChar, 20).Value = pri
        cmdUpdateTable15.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable15.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmdUpdateTable15.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetS
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource2.RemoveFilter()
        If vFClear1 Then
            dsAlmacen.Tables("fn_MatStockObra").Clear()
            daVMat.Fill(dsAlmacen, "fn_MatStockObra")

            colorearFila1()
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,stock,uniBase,preBase,tipoM,codTipM,codUni,codigo from fn_MatStockObra(@cod)" 'material
            crearDataAdapterTable(daVMat, sele)
            daVMat.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

            daVMat.Fill(dsAlmacen, "fn_MatStockObra")

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "fn_MatStockObra"
            Navigator1.BindingSource = BindingSource2
            dgTabla1.DataSource = BindingSource2
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "material"
            ModificarColumnasDGV1()

            vFClear1 = True
            colorearFila1()
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        txtBuscar.Focus()
        txtBuscar.SelectAll()
    End Sub

    Private Sub colorearFila1()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(2) >= 0 Then 'Resaltando Stock
                dgTabla1.Rows(j).Cells(2).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 555
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Stock"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Unidad"
            .Columns(4).Width = 55
            .Columns(4).HeaderText = "PrecS/."
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).HeaderText = "Tipo Insumo"
            .Columns(5).Width = 120
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub txtBuscar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus, txtBuscar.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        Dim campo As String
        If cbBuscar.SelectedIndex = 0 Then
            campo = "material"
        End If
        If cbBuscar.SelectedIndex = 1 Then
            campo = "codMat"
        End If

        If cbBuscar.SelectedIndex = 0 Then
            'Tipo String
            BindingSource2.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%'"
        Else
            If Not IsNumeric(txtBuscar.Text.Trim()) Then
                StatusBarClass.messageBarraEstado(" INGRESE DATO NUMERICO...")
                txtBuscar.SelectAll()
                Exit Sub
            End If
            BindingSource2.Filter = campo & "=" & txtBuscar.Text.Trim()
        End If
        If BindingSource2.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnAgrega
            colorearFila1()
        Else
            'txtBuscar.Focus()
            'txtBuscar.SelectAll()
            StatusBarClass.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
    End Sub

    Private Sub btnCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Dim crea As New crearMaterialForm
        crea.ShowDialog()
    End Sub

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        btnAgrega.PerformClick()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a agregar...")
            Exit Sub
        End If

        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Solicitud NO APERTURADA...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            MessageBox.Show("No se puede agregar requerimiento por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If BindingSource3.Find("codMat", BindingSource2.Item(BindingSource2.Position)(0)) >= 0 Then
            MessageBox.Show("Ya exíste requerimiento: " & BindingSource3.Item(BindingSource3.Position)(2), nomNegocio, Nothing, MessageBoxIcon.Information)
            txtBuscar.Focus()
            txtBuscar.SelectAll()
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

            'TDetalleSol
            comandoInsert19()
            cmInserTable19.Transaction = myTrans
            If cmInserTable19.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim codDetS As Integer = cmInserTable19.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource3.Position = BindingSource3.Find("codDetS", codDetS)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            txtObs1.Clear()
            txtCan.Focus()
            txtCan.SelectAll()
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("tipoM de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable19 As SqlCommand
    Private Sub comandoInsert19()
        cmInserTable19 = New SqlCommand
        cmInserTable19.CommandType = CommandType.StoredProcedure
        cmInserTable19.CommandText = "PA_InsertDetalleSol"
        cmInserTable19.Connection = Cn
        cmInserTable19.Parameters.Add("@pri", SqlDbType.VarChar, 20).Value = cbNec.Text.Trim()
        cmInserTable19.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = txtCan.Text
        cmInserTable19.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = BindingSource2.Item(BindingSource2.Position)(1)
        cmInserTable19.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = BindingSource2.Item(BindingSource2.Position)(3)
        cmInserTable19.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmInserTable19.Parameters.Add("@obs1", SqlDbType.VarChar, 100).Value = txtObs1.Text.Trim()
        cmInserTable19.Parameters.Add("@codP1", SqlDbType.Int, 0).Value = 0 'sin verificador
        cmInserTable19.Parameters.Add("@codE", SqlDbType.Int, 0).Value = 1 'Pendiente
        cmInserTable19.Parameters.Add("@obs2", SqlDbType.VarChar, 200).Value = ""
        cmInserTable19.Parameters.Add("@codM", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
        cmInserTable19.Parameters.Add("@codA", SqlDbType.Int, 0).Value = cbArea.SelectedValue
        cmInserTable19.Parameters.Add("@idS", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
        'configurando direction output = parametro de solo salida
        cmInserTable19.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable19.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub txtCan_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.GotFocus, txtCan.MouseClick
        txtCan.SelectAll()
    End Sub

    Private Sub txtCan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan.TextChanged
        Me.AcceptButton = Me.btnAgrega
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then
            MessageBox.Show("No se puede eliminar requerimiento por estar en el estado de <CERRADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(15) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro requerimiento...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(13) <> 1 Then
            MessageBox.Show("NO se debe eliminar por NO estar en el estado de [PENDIENTE]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de eliminar: " & BindingSource3.Item(BindingSource3.Position)(2) & "  Si elimina no podra deshacer...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            comandoDelete12()
            cmDeleteTable12.Transaction = myTrans
            If cmDeleteTable12.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  ELIMINACION CON EXITO...")
            finalMytrans = True

            'Actualizando el dataTable
            visualizarDet()

            wait.Close()
            Me.Cursor = Cursors.Default
            'accionesIniciales()
            StatusBarClass.messageBarraEstado("  ELIMINACION CON EXITO...")
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

    Dim cmDeleteTable12 As SqlCommand
    Private Sub comandoDelete12()
        cmDeleteTable12 = New SqlCommand
        cmDeleteTable12.CommandType = CommandType.Text
        cmDeleteTable12.CommandText = "delete from TDetalleSol where codDetS=@cod"
        cmDeleteTable12.Connection = Cn
        cmDeleteTable12.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        vParam1 = "REQUERIMIENTOS SEMANALES OBRA: " & vSCodigo & " " & vSNomSuc
        Dim informe As New ReportViewerSolicitudForm
        informe.ShowDialog()
    End Sub

    Private Sub cbNec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNec.SelectedIndexChanged
        txtCan.Focus()
    End Sub

    Private Sub txtCan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then  'te deja escribir digitos
            e.Handled = False
        Else
            If e.KeyChar.IsControl(e.KeyChar) Then  'te deja escribir enter, backSpace (controles)
                e.Handled = False
            Else
                If e.KeyChar = "." Then   'te deja escribir punto
                    e.Handled = False
                Else    'lo demas no te deja escribir ASNOOOO
                    e.Handled = True
                End If
            End If
        End If
    End Sub
End Class
