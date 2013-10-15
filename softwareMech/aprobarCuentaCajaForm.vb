Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class aprobarCuentaCajaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource

    Private Sub aprobarCuentaCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub aprobarCuentaCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSC,nro,fechaSol,nomSoli,montoSol,imprevisto,salAnt,totPar,est,nomObra,nomSede,estSol,codObra,codSede,codPers from VSolicitudCajaCuenta"
        crearDataAdapterTable(daTabla1, sele)
        'daTabla1.SelectCommand.Parameters.Add("@codPers", SqlDbType.VarChar, 10).Value = vPass

        sele = "select codDetSol,tipoM,insumo,cant1,prec1,totPar,comp,fecha,comp1,nroOtros,cant2,prec2,totReal,estRen1,nom,obsRen,ingre,areaM,codRen,codMat,codAreaM,codTipM,codSC,codDC,compCheck,estRen,compRen,ingreso,obsSol from VDetSolCajaCuenta where (codSC=@cod1 and codTipM=@codT) or (codSC=@cod2)"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod1", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@codT", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@cod2", SqlDbType.Int, 0).Value = 0

        sele = "select codTipM,tipoM from TTipoMat"
        crearDataAdapterTable(daTTipo, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolicitudCajaCuenta")
            daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")
            daTTipo.Fill(dsAlmacen, "TTipoMat")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolicitudCajaCuenta"
            lbSol.DataSource = BindingSource1
            lbSol.DisplayMember = "nro"
            lbSol.ValueMember = "codSC"
            BindingSource1.Sort = "codSC"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetSolCajaCuenta"
            Navigator1.BindingSource = BindingSource2
            dgTabla1.DataSource = BindingSource2
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "codTipM,codDetSol"
            ModificarColumnasDGV()

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TTipoMat"
            cbTip1.DataSource = BindingSource3
            cbTip1.DisplayMember = "tipoM"
            cbTip1.ValueMember = "codTipM"

            configurarColorControl()

            cbVis.Checked = True

            If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Or vSCodTipoUsu = 3 Or vSCodTipoUsu = 12 Then  '1=administrador de sistema 2=gerencia general 3=gerencia de construcciones
                'Solo administrador puede realizar este proceso Y 12 USUARIO CAJA CHICA
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

    Private Sub rendicionCuentaCajaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        BindingSource1.MoveLast()
        vfNro = 1000 'filtro por tipo insumo
        vfVAn1 = True
        visualizarDet()
        leerMontos()
    End Sub

    Dim vfVAn1 As Boolean = False
    Private Sub lbSol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSol.SelectedIndexChanged
        If vfVAn1 Then
            visualizarDet()
            leerMontos()
        End If
    End Sub

    Private Sub cbTip1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTip1.SelectedIndexChanged
        If vfVAn1 Then
            visualizarDet()
            leerMontos()
        End If
    End Sub

    Private Sub visualizarDet()
        If BindingSource1.Position = -1 Then
            dsAlmacen.Tables("VDetSolCajaCuenta").Clear()
            Exit Sub
        End If

        If cbVis.Checked Then 'Todas las areas
            vfNro = lbSol.SelectedValue
        Else  'por tipo insumo
            vfNro = 1000
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VDetSolCajaCuenta").Clear()
        daDetDoc.SelectCommand.Parameters("@cod1").Value = lbSol.SelectedValue
        daDetDoc.SelectCommand.Parameters("@codT").Value = cbTip1.SelectedValue
        daDetDoc.SelectCommand.Parameters("@cod2").Value = vfNro  'por tipo insumo
        daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")
        colorearFila()
        sumTotal()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub leerMontos()
        If BindingSource1.Position <> -1 Then
            txtSol.Text = BindingSource1.Item(lbSol.SelectedIndex)(3)
            txtTotIns.Text = BindingSource1.Item(lbSol.SelectedIndex)(4)
            date1.Value = BindingSource1.Item(lbSol.SelectedIndex)(2)
            txtObra.Text = BindingSource1.Item(lbSol.SelectedIndex)(9)
            txtImpre.Text = BindingSource1.Item(lbSol.SelectedIndex)(5)
            txtSalAnt.Text = BindingSource1.Item(lbSol.SelectedIndex)(6)
            txtTotReq.Text = Format(BindingSource1.Item(lbSol.SelectedIndex)(7), "0,0.00")

            txtTotEgr.Text = Format(recuperarTotReal(lbSol.SelectedValue), "0,0.00")
            'txtSalAct.Text = Format(txtTotReq.Text - txtTotEgr.Text, "0,0.00")
            txtSalAct.Text = Format((txtTotReq.Text - txtTotEgr.Text) + txtSalAnt.Text, "0,0.00")
        End If
    End Sub

    Private Function recuperarTotReal(ByVal cod As Integer) As Double
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select SUM(totReal) from VDetSolCajaCuenta where codSC=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfNro As Short
    Private Sub cbVis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVis.CheckedChanged
        visualizarDet()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(25) = 1 Then 'OK
                dgTabla1.Rows(j).Cells(13).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(13).Style.ForeColor = Color.White
            End If
            If BindingSource2.Item(j)(25) = 2 Then 'Observado
                dgTabla1.Rows(j).Cells(13).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(13).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
            txtTotal.Text = "0.00"
            txtTotal1.Text = "0.00"
            txtFac.Text = "0.00"
            txtBol.Text = "0.00"
            txtHon.Text = "0.00"
            txtOtro.Text = "0.00"
            Exit Sub
        End If

        txtTotal.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totPar)", Nothing)), "0,0.00")
        txtTotal1.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", Nothing)), "0,0.00")

        If BindingSource2.Find("compRen", 1) <> -1 Then 'Factura
            txtFac.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=1")), "0,0.00")
        Else
            txtFac.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 2) <> -1 Then 'Factura
            txtBol.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=2")), "0,0.00")
        Else
            txtBol.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 3) <> -1 Then 'Factura
            txtHon.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=3")), "0,0.00")
        Else
            txtHon.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 4) <> -1 Then 'Factura
            txtOtro.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=4")), "0,0.00")
        Else
            txtOtro.Text = "0.00"
        End If
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
        Label12.ForeColor = ForeColorLabel
        Label13.ForeColor = ForeColorLabel
        Label14.ForeColor = ForeColorLabel
        Label15.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        GroupBox2.ForeColor = ForeColorLabel
        cbVis.ForeColor = ForeColorLabel
        btnApro.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Width = 80
            .Columns(1).HeaderText = "Tipo_Insumo"
            .Columns(1).ReadOnly = True 'NO editable
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 280
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Cant"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "PreUni"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).Width = 55
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "#,##0.00"
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Entrega"
            .Columns(6).Width = 55
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(6).Visible = False
            .Columns(7).HeaderText = "Fec_Doc."
            .Columns(7).Width = 70
            .Columns(7).ReadOnly = True 'NO editable
            .Columns(8).HeaderText = "Comprob."
            .Columns(8).Width = 70
            .Columns(8).ReadOnly = True 'NO editable
            .Columns(9).HeaderText = "Nº Doc."
            .Columns(9).Width = 100
            .Columns(9).ReadOnly = True 'NO editable
            .Columns(10).Width = 45
            .Columns(10).HeaderText = "Cant"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).ReadOnly = True 'NO editable
            .Columns(11).Width = 55
            .Columns(11).HeaderText = "PrecUni"
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(11).ReadOnly = True 'NO editable
            .Columns(12).Width = 65
            .Columns(12).HeaderText = "TotalReal"
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(12).DefaultCellStyle.Format = "#,##0.00"
            .Columns(12).ReadOnly = True 'NO editable
            .Columns(13).HeaderText = "Estado"
            .Columns(13).Width = 75
            .Columns(13).ReadOnly = True 'NO editable
            .Columns(14).Width = 100
            .Columns(14).HeaderText = "Verificador"
            .Columns(14).ReadOnly = True 'NO editable
            .Columns(15).Width = 200
            .Columns(15).HeaderText = "Observacion Verificador"
            .Columns(15).ReadOnly = True 'NO editable
            .Columns(16).HeaderText = "Ingreso"
            .Columns(16).Width = 75
            .Columns(16).ReadOnly = True 'NO editable
            .Columns(17).HeaderText = "Area_Insumo"
            .Columns(17).Width = 100
            .Columns(17).ReadOnly = True 'NO editable
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .Columns(25).Visible = False
            .Columns(26).Visible = False
            .Columns(27).Visible = False
            .Columns(28).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub

    '-----------------------------------------
    '------Menu antiClick Anular---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "OK APROBADO"
        toolStripItem1.BackColor = Color.Green
        toolStripItem2.Text = "OBSERVADO"
        toolStripItem2.BackColor = Color.Yellow
        toolStripItem3.Text = "PENDIENTE"
        toolStripItem3.BackColor = Color.White
        Dim strip As New ContextMenuStrip()
        For Each column As DataGridViewColumn In dgTabla1.Columns()
            column.ContextMenuStrip = strip
            column.ContextMenuStrip.Items.Add(toolStripItem1)
            column.ContextMenuStrip.Items.Add(toolStripItem2)
            column.ContextMenuStrip.Items.Add(toolStripItem3)
        Next
    End Sub

    Private mouseLocation As DataGridViewCellEventArgs
    Private Sub dgTabla1_CellMouseEnter(ByVal sender As Object, ByVal location As DataGridViewCellEventArgs) Handles dgTabla1.CellMouseEnter
        mouseLocation = location
    End Sub

    Private Function recuperarPers(ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select nombre+' '+apellido from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(25) = 1 Then  '0=pendiente  2=Observado
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, YA ESTA APROBADO...")
            Exit Sub
        End If

        If Not IsDate(BindingSource2.Item(BindingSource2.Position)(7)) Then
            MessageBox.Show("Agregue Fecha Doc. en Rendicion...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If
        If BindingSource2.Item(BindingSource2.Position)(9).ToString.Trim() = "" Then
            MessageBox.Show("Agregue Nº Doc. en Rendicion...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de APROBAR insumo: " & BindingSource2.Item(BindingSource2.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.BackColor = Color.Green 'Color.YellowGreen
            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.ForeColor = Color.White

            BindingSource2.Item(BindingSource2.Position)(25) = 1
            BindingSource2.Item(BindingSource2.Position)(13) = "APROBADO"
            BindingSource2.Item(BindingSource2.Position)(14) = recuperarPers(vPass)

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
        cmUpdateTable1.CommandText = "update TDetSolCaja set codRen=@codP,estRen=@est,obsRen=@obs where codDetSol=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDet
    End Sub

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A OBSERVAR...")
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(25) = 2 Then  '0=pendiente  2=Observado
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.BackColor = Color.Yellow
            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.ForeColor = Color.Red

            BindingSource2.Item(BindingSource2.Position)(25) = 2
            BindingSource2.Item(BindingSource2.Position)(15) = vObs
            BindingSource2.Item(BindingSource2.Position)(13) = "OBSERVADO"
            BindingSource2.Item(BindingSource2.Position)(14) = recuperarPers(vPass)

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

        If BindingSource2.Item(BindingSource2.Position)(25) = 0 Then  '0=pendiente  2=Observado
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.BackColor = Color.White
            dgTabla1.Rows(BindingSource2.Position).Cells(13).Style.ForeColor = Color.Black

            BindingSource2.Item(BindingSource2.Position)(25) = 0
            BindingSource2.Item(BindingSource2.Position)(13) = "PENDIENTE"
            BindingSource2.Item(BindingSource2.Position)(14) = recuperarPers(vPass)

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

    Private Sub btnApro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApro.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A COMPROBAR...")
            Exit Sub
        End If

        If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Or vSCodTipoUsu = 3 Or vSCodTipoUsu = 12 Then  '1=administrador de sistema 2=gerencia general 3=gerencia de construcciones
            'Solo administrador puede realizar este proceso Y 12 USUARIO CAJA CHICA
        Else
            MessageBox.Show("Proceso Denegado, Solo Administradores pueden [APROBAR]", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        For j As Short = 0 To BindingSource2.Count - 1
            BindingSource2.Position = j
            If Not IsDate(BindingSource2.Item(j)(7)) Then
                MessageBox.Show("Agregue Fecha Doc. en Rendicion...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If
            If BindingSource2.Item(j)(9).ToString.Trim() = "" Then
                MessageBox.Show("Agregue Nº Doc. en Rendicion...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        Dim resp As String = MessageBox.Show("Esta segúro de APROBAR TODOS LOS INSUMOS" & Chr(13) & "y Cerrar Solicitud...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        txtTotEgr.Text = Format(recuperarTotReal(lbSol.SelectedValue), "0,0.00")
        'txtSalAct.Text = Format(txtTotReq.Text - txtTotEgr.Text, "0,0.00")
        txtSalAct.Text = Format((txtTotReq.Text - txtTotEgr.Text) + txtSalAnt.Text, "0,0.00")

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

            'TSolicitudCaja
            comandoUpdate2(4, txtTotEgr.Text, txtSalAct.Text) '4=OK Rendido
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVAn1 = False

            dsAlmacen.Tables("VSolicitudCajaCuenta").Clear()
            daTabla1.Fill(dsAlmacen, "VSolicitudCajaCuenta")

            vfVAn1 = True

            visualizarDet()
            leerMontos()

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

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal est As Short, ByVal monRen As Decimal, ByVal saldo As Decimal)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TSolicitudCaja set estSol=@est,montoRen=@mon,salAct=@sal where codSC=@cod"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable2.Parameters.Add("@mon", SqlDbType.Decimal, 0).Value = monRen
        cmUpdateTable2.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = saldo
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub
End Class
