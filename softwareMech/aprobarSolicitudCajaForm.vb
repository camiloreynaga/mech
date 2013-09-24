Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class aprobarSolicitudCajaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub aprobarSolicitudCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub aprobarSolicitudCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSC,nro,fechaSol,nomSoli,montoSol,imprevisto,salAnt,totPar,est,nomObra,nomSede,estSol,codObra,codSede,codPers from VSolCajaApro where codSede=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetSol,cant1,uniMed,insumo,prec1,totPar,comp,areaM,estApro,obsSol,tipoM,nom,obsApro,codApro,codMat,codAreaM,codTipM,codSC,codDC,nroOtros,compCheck,estDet from VDetSolCaja where codSC=@cod"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolCajaApro")
            daDetDoc.Fill(dsAlmacen, "VDetSolCaja")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolCajaApro"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "codSC"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetSolCaja"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "codAreaM,codDetSol"
            ModificarColumnasDGV()

            configurarColorControl()

            BindingSource1.MoveLast()

            vfVan1 = True
            visualizarDet()

            If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Or vSCodTipoUsu = 3 Then  '1=administrador de sistema 2=gerencia general 3=gerencia de construcciones
                'Solo administrador puede realizar este proceso
                AddContextMenu() 'Agregando menu antiClick
            End If

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    'eVENTO DE FORM QUE SE DISPARA CUANDO YA ESTA PINTADO EN FORMULARIO
    Private Sub aprobarSolicitudCajaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila1() 'Tabla maestro
        colorearFila()  'Tabla detalle
    End Sub

    Private Sub colorearFila1()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(11) = 1 Then 'Aprobado
                dgTabla1.Rows(j).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(8).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(11) = 2 Then 'Cerrado
                dgTabla1.Rows(j).Cells(8).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(8).Style.ForeColor = Color.Red
            End If
            If BindingSource1.Item(j)(11) = 3 Then 'Anulado
                dgTabla1.Rows(j).Cells(8).Style.BackColor = Color.Red
                dgTabla1.Rows(j).Cells(8).Style.ForeColor = Color.White
            End If
            dgTabla1.Rows(j).Cells(7).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(21) = 1 Then 'Aprobado
                dgTabla2.Rows(j).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(8).Style.ForeColor = Color.White
            End If
            If BindingSource2.Item(j)(21) = 2 Then 'Observado
                dgTabla2.Rows(j).Cells(8).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(8).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
            txtTotal.Text = "0.00"
            Exit Sub
        End If
        txtTotal.Text = Format(dsAlmacen.Tables("VDetSolCaja").Compute("Sum(totPar)", Nothing), "0,0.00")
        'txtTotal.Text = Format(CDbl(txtTotal.Text), "0,0.00")
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSolCaja").Clear()
            daDetDoc.SelectCommand.Parameters("@cod").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VDetSolCaja")
            colorearFila()
            sumTotal()
            Me.Cursor = Cursors.Default
        End If
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
            .Columns(3).Width = 120
            .Columns(3).HeaderText = "Solicitante"
            .Columns(4).HeaderText = "TotInsumo"
            .Columns(4).Width = 60
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Format = "N2"
            .Columns(5).HeaderText = "Imprevisto"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "N2"
            .Columns(6).HeaderText = "SaldoAnt."
            .Columns(6).Width = 60
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "N2"
            .Columns(7).HeaderText = "TotParcial"
            .Columns(7).Width = 80
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "N2"
            .Columns(8).Width = 70
            .Columns(8).HeaderText = "Estado"
            .Columns(9).Width = 360
            .Columns(9).HeaderText = "Obra Gasto"
            .Columns(10).Width = 280
            .Columns(10).HeaderText = "Requerimiento de caja:"
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 50
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Unid."
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 340
            .Columns(4).Width = 60
            .Columns(4).HeaderText = "PrecUni"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 70
            .Columns(5).HeaderText = "TotParcial"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Comprob."
            .Columns(6).Width = 70
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).HeaderText = "Area_Insumo"
            .Columns(7).Width = 100
            .Columns(7).ReadOnly = True 'NO editable
            .Columns(8).HeaderText = "Estado"
            .Columns(8).Width = 75
            .Columns(8).ReadOnly = True 'NO editable
            .Columns(9).Width = 200
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).HeaderText = "Tipo_Insumo"
            .Columns(10).Width = 100
            .Columns(10).ReadOnly = True 'NO editable
            .Columns(11).Width = 100
            .Columns(11).HeaderText = "Verificador"
            .Columns(11).ReadOnly = True 'NO editable
            .Columns(12).Width = 300
            .Columns(12).HeaderText = "Observacion verificador"
            .Columns(12).ReadOnly = True 'NO editable
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
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
        'Label1.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    '-----------------------------------------
    '------Menu antiClick Anular---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "APROBADO"
        toolStripItem1.BackColor = Color.Green
        toolStripItem2.Text = "OBSERVADO"
        toolStripItem2.BackColor = Color.Yellow
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

    Private Function recuperarCountEst(ByVal codSC As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetSolCaja where codSC=" & codSC & " and estDet<>1" '1=Aprobado
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(21) = 1 Then  '0=pendiente  2=Observado
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, YA ESTA APROBADO...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de APROBAR insumo: " & BindingSource2.Item(BindingSource2.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            Dim position As Short = BindingSource2.Position

            'TDetSolCaja
            comandoUpdate1(1, "", BindingSource2.Item(BindingSource2.Position)(0)) '1=aprobado
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

            If recuperarCountEst(BindingSource1.Item(BindingSource1.Position)(0), myTrans) = 0 Then  'Todos aprobados
                MsgBox("Aprobado todo")
                'TSolicitudCaja
                comandoUpdate2(1) '1=aprobado
                cmUpdateTable2.Transaction = myTrans
                If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                End If
                dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.ForeColor = Color.White

                BindingSource1.Item(BindingSource1.Position)(11) = 1
                BindingSource1.Item(BindingSource1.Position)(8) = "APROBADO"
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.ForeColor = Color.White

            BindingSource2.Item(BindingSource2.Position)(21) = 1
            BindingSource2.Item(BindingSource2.Position)(8) = "APROBADO"
            BindingSource2.Item(BindingSource2.Position)(11) = recuperarPers(vPass)

            If position < BindingSource2.Count - 1 Then
                BindingSource2.Position = position + 1  '
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

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal est As Short, ByVal obs As String, ByVal codDet As Integer)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TDetSolCaja set codApro=@codP,estDet=@est,obsApro=@obs where codDetSol=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDet
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal est As Short)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TSolicitudCaja set estSol=@est where codSC=@cod"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Function recuperarPers(ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select nombre+' '+apellido from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A OBSERVAR...")
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(21) = 2 Then  '0=pendiente  2=Observado
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
            Dim position As Short = BindingSource2.Position

            'TDetSolCaja
            comandoUpdate1(2, vObs, BindingSource2.Item(BindingSource2.Position)(0)) '2=OBSERVADO
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

            'TSolicitudCaja
            comandoUpdate2(0) '0=PENDIENTE
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If
            dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.BackColor = Color.White
            dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.ForeColor = Color.Black

            BindingSource1.Item(BindingSource1.Position)(11) = 0
            BindingSource1.Item(BindingSource1.Position)(8) = "PENDIENTE"

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.BackColor = Color.Yellow
            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.ForeColor = Color.Red

            BindingSource2.Item(BindingSource2.Position)(21) = 2
            BindingSource2.Item(BindingSource2.Position)(12) = vObs
            BindingSource2.Item(BindingSource2.Position)(8) = "OBSERVADO"
            BindingSource2.Item(BindingSource2.Position)(11) = recuperarPers(vPass)

            If position < BindingSource2.Count - 1 Then
                BindingSource2.Position = position + 1
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
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO...")
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(21) = 0 Then  '0=pendiente  2=Observado
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
            Dim position As Short = BindingSource2.Position

            'TDetSolCaja
            comandoUpdate1(0, "", BindingSource2.Item(BindingSource2.Position)(0)) '1=pendiente
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

            'TSolicitudCaja
            comandoUpdate2(0) '0=PENDIENTE
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If
            dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.BackColor = Color.White
            dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.ForeColor = Color.Black

            BindingSource1.Item(BindingSource1.Position)(11) = 0
            BindingSource1.Item(BindingSource1.Position)(8) = "PENDIENTE"

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.BackColor = Color.White
            dgTabla2.Rows(BindingSource2.Position).Cells(8).Style.ForeColor = Color.Black

            BindingSource2.Item(BindingSource2.Position)(21) = 0
            BindingSource2.Item(BindingSource2.Position)(8) = "PENDIENTE"
            BindingSource2.Item(BindingSource2.Position)(11) = recuperarPers(vPass)

            If position < BindingSource2.Count - 1 Then
                BindingSource2.Position = position + 1  '
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

    Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Or vSCodTipoUsu = 3 Then  '1=administrador de sistema 2=gerencia general 3=gerencia de construcciones
            'Solo administrador puede realizar este proceso
        Else
            MessageBox.Show("Proceso Denegado, Solo Administradores pueden [APROBAR]", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de APROBAR TODOS LOS INSUMOS", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            Dim position As Short = BindingSource2.Position

            For j As Short = 0 To BindingSource2.Count - 1
                'TDetSolCaja
                comandoUpdate1(1, "", BindingSource2.Item(j)(0)) '1=aprobado
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
            Next

            If recuperarCountEst(BindingSource1.Item(BindingSource1.Position)(0), myTrans) = 0 Then  'Todos aprobados
                'TSolicitudCaja
                comandoUpdate2(1) '1=aprobado
                cmUpdateTable2.Transaction = myTrans
                If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                End If
                dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(BindingSource1.Position).Cells(8).Style.ForeColor = Color.White

                BindingSource1.Item(BindingSource1.Position)(11) = 1
                BindingSource1.Item(BindingSource1.Position)(8) = "APROBADO"
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            For j As Short = 0 To BindingSource2.Count - 1
                dgTabla2.Rows(j).Cells(8).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(8).Style.ForeColor = Color.White

                BindingSource2.Item(j)(21) = 1
                BindingSource2.Item(j)(8) = "APROBADO"
                BindingSource2.Item(j)(11) = recuperarPers(vPass)
            Next

            BindingSource2.MoveLast()
            
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
End Class
