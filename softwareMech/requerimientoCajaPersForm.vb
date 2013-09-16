Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class requerimientoCajaPersForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

    Private Sub requerimientoCajaPersForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
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

    Private Sub requerimientoCajaPersForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTUbi, sele)

        sele = "select codSC,nroSol,nro,fechaSol,codPers,nom,estSol,est,salAnt,montoSol,imprevisto,codObra,codSede from VSolicitudCaja where codSede=@cod and codPers=@codP"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo
        daTabla1.SelectCommand.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass

        sele = "select codTipM,tipoM from TTipoMat"
        crearDataAdapterTable(daTTipo, sele)

        sele = "select codAreaM,areaM from TAreaMat"
        crearDataAdapterTable(daTArea, sele)

        sele = "select codUni,unidad from TUnidad where codUni>1 order by unidad"  '1=""
        crearDataAdapterTable(daTUni, sele)

        sele = "select codDetSol,cant1,uniMed,insumo,prec1,totPar,comp,areaM,tipoM,obsSol,estApro,nom,obsApro,codApro,codMat,codAreaM,codTipM,codSC,codDC,nroOtros,compCheck,estDet from VDetSolCaja where codSC=@cod"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")
            daTabla1.Fill(dsAlmacen, "VSolicitudCaja")
            daTTipo.Fill(dsAlmacen, "TTipoMat")
            daTArea.Fill(dsAlmacen, "TAreaMat")
            daTUni.Fill(dsAlmacen, "TUnidad")
            daDetDoc.Fill(dsAlmacen, "VDetSolCaja")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolicitudCaja"
            lbSol.DataSource = BindingSource1
            lbSol.DisplayMember = "nro"
            lbSol.ValueMember = "codSC"
            BindingSource1.Sort = "nroSol"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VLugarTrabajoLogin"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            cbTipo.DataSource = dsAlmacen
            cbTipo.DisplayMember = "TTipoMat.tipoM"
            cbTipo.ValueMember = "TTipoMat.codTipM"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            cbUni.DataSource = dsAlmacen
            cbUni.DisplayMember = "TUnidad.unidad"
            cbUni.ValueMember = "TUnidad.codUni"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VDetSolCaja"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource3.Sort = "codAreaM,codDetSol"
            ModificarColumnasDGV()

            configurarColorControl()

            recuperarUltimoNro()

            cbCompro.SelectedIndex = 0

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

    Private Sub requerimientoCajaPersForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        BindingSource1.MoveLast()
        vfVAn1 = True
        visualizarDet()
        leerMontos()
    End Sub

    Private Sub leerMontos()
        If BindingSource1.Position <> -1 Then
            sumTotal()
            txtTotIns.Text = txtTotal.Text
            date1.Value = BindingSource1.Item(lbSol.SelectedIndex)(3)
            cbObra.SelectedValue = BindingSource1.Item(lbSol.SelectedIndex)(11)
            'txtTotIns.Text = BindingSource1.Item(lbSol.SelectedIndex)(9)
            txtImpre.Text = BindingSource1.Item(lbSol.SelectedIndex)(10)
            txtSalAnt.Text = BindingSource1.Item(lbSol.SelectedIndex)(8)
            txtTotReq.Text = Format((CDbl(txtTotIns.Text) + CDbl(txtImpre.Text) - CDbl(txtSalAnt.Text)), "0,0.00")
            txtEst.Text = BindingSource1.Item(lbSol.SelectedIndex)(7)

            If BindingSource1.Item(BindingSource1.Position)(6) = 1 Then 'aprobado
                txtEst.BackColor = Color.Green
            End If
        End If
    End Sub

    Dim vfVAn1 As Boolean = False
    Private Sub lbSol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSol.SelectedIndexChanged
        If vfVAn1 Then
            visualizarDet()
            leerMontos()
        End If
    End Sub

    Private Sub visualizarDet()
        If BindingSource1.Position = -1 Then
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VDetSolCaja").Clear()
        daDetDoc.SelectCommand.Parameters("@cod").Value = lbSol.SelectedValue 'BindingSource1.Item(BindingSource1.Position)(0)
        daDetDoc.Fill(dsAlmacen, "VDetSolCaja")
        colorearFila()
        colorear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(21) = 2 Then 'Aprobado
                dgTabla2.Rows(j).Cells(10).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(10).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(21) = 2 Then 'Observado
                dgTabla2.Rows(j).Cells(10).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(10).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource3.Count - 1
            dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(3).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(4).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(9).Style.BackColor = Color.AliceBlue
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource3.Position = -1 Then
            txtTotal.Text = "0.00"
            Exit Sub
        End If
        txtTotal.Text = Format((dsAlmacen.Tables("VDetSolCaja").Compute("Sum(totPar)", Nothing)), "0,0.00")
    End Sub

    Private Sub ModificarColumnasDGV()
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
            .Columns(8).HeaderText = "Tipo_Insumo"
            .Columns(8).Width = 100
            .Columns(8).ReadOnly = True 'NO editable
            .Columns(9).Width = 200
            .Columns(9).HeaderText = "Observacion solicitante"
            .Columns(10).HeaderText = "Estado"
            .Columns(10).Width = 75
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
        Label16.ForeColor = ForeColorLabel
        lblMon1.ForeColor = ForeColorLabel
        lblMon2.ForeColor = ForeColorLabel
        lblMon3.ForeColor = ForeColorLabel
        lblMon4.ForeColor = ForeColorLabel
        btnModificar.ForeColor = ForeColorButtom
        btnElimina.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        btnNuevo.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
    End Sub

    Private Sub recuperarUltimoNro()
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroSol),0)+1 from TSolicitudCaja"
        cmdMaxCodigo.Connection = Cn
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

    Private Sub asignarNro(ByVal max As Integer)
        Select Case CInt(max)
            Case Is < 100
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
        'dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub desactivarControles1()
        lbSol.Enabled = False
        Panel4.Enabled = False
        If vfNuevo1 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = False
        btnElimina.FlatStyle = FlatStyle.Flat
        btnAnula.Enabled = False
        btnAnula.FlatStyle = FlatStyle.Flat
        btnImprimir.Enabled = False
        btnImprimir.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        lbSol.Enabled = True
        Panel4.Enabled = True
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = True
        btnElimina.FlatStyle = FlatStyle.Standard
        btnAnula.Enabled = True
        btnAnula.FlatStyle = FlatStyle.Standard
        btnImprimir.Enabled = True
        btnImprimir.FlatStyle = FlatStyle.Standard
        date1.Enabled = False
        cbObra.Enabled = False
        txtEst.ReadOnly = False
        txtImpre.ReadOnly = True
    End Sub

    Private Sub limpiarText1()
        txtEst.ReadOnly = True
        txtImpre.ReadOnly = False
        date1.Enabled = True
        cbObra.Enabled = True
        If vfNuevo1 = "guardar" Then
            date1.Value = Now.Date
            txtEst.Clear()
            txtTotIns.Text = "0"
            txtImpre.Text = "0"
            txtSalAnt.Text = "0"
            txtTotReq.Text = "0"
        End If
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidaNroMayorOigualCero(txtImpre.Text) Then
            txtImpre.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarCodSC(ByVal codPers As Integer, ByVal codSede As String, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codSC),0) as codSC from TSolicitudCaja where codPers=" & codPers & " and codSede='" & codSede & "'"
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSalAnt(ByVal codSC As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select salAnt from TSolicitudCaja where codSC=" & codSC
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            limpiarText1()
            txtImpre.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else
            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If ValidarCampos() Then
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta Segúro de Aperturar Requerimiento de" & Chr(13) & "CAJA CHICA Nº " & txtNro.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                Exit Sub
            End If

            recuperarUltimoNro()
            'Dim campo As Integer = CInt(txtNro.Text)

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            Me.Cursor = Cursors.WaitCursor
            wait.Show()
            'estableciendo una transaccion
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
                Me.Refresh()

                Dim codSC As Integer = recuperarCodSC(vPass, vSCodigo, myTrans)
                Dim salAnt As Decimal
                If codSC = 0 Then 'Primer requerimiento de esta persona
                    salAnt = 0
                Else
                    salAnt = recuperarSalAnt(codSC, myTrans)
                End If

                'TSolicitudCaja
                comandoInsert1(salAnt)
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
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
                vfVAn1 = False   'para leerMontos

                'Actualizando el dataTable
                dsAlmacen.Tables("VSolicitudCaja").Clear()
                daTabla1.Fill(dsAlmacen, "VSolicitudCaja")

                recuperarUltimoNro()

                BindingSource1.MoveLast()

                vfVAn1 = True
                btnCancelar.PerformClick()
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
        End If
    End Sub

    Private Sub txtImpre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImpre.GotFocus, txtImpre.MouseClick
        txtImpre.SelectAll()
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal salAnt As Decimal)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert into TSolicitudCaja(fechaSol,nroSol,codPers,estSol,salAnt,montoSol,imprevisto,montoRen,codObra,codSede) values(@fec,@nro,@codP,@est,@sal,@monS,@imp,@monR,@codO,@codS)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = txtNro.Text
        cmInserTable1.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0 '0=pendiente
        cmInserTable1.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = salAnt
        cmInserTable1.Parameters.Add("@monS", SqlDbType.Decimal, 0).Value = txtTotIns.Text
        cmInserTable1.Parameters.Add("@imp", SqlDbType.Decimal, 0).Value = txtImpre.Text
        cmInserTable1.Parameters.Add("@monR", SqlDbType.Decimal, 0).Value = 0
        cmInserTable1.Parameters.Add("@codO", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        cmInserTable1.Parameters.Add("@codS", SqlDbType.VarChar, 10).Value = vSCodigo
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        activarControles1()
        visualizarDet()
        leerMontos()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Dim vfModificar As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso a actualizar...")
            Exit Sub
        End If

        If vfModificar = "modificar" Then
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            limpiarText1()
            leerMontos()
            txtImpre.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de GUARDAR MODIFICACIONES en Requerimiento Nº " & BindingSource1.Item(BindingSource1.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, GUARDANDO INFORMACION....")
                Dim codSC As Integer = BindingSource1.Item(BindingSource1.Position)(0)

                'TSolicitudCaja
                comandoUpdate1()
                cmUpdateTable1.Transaction = myTrans
                If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
                vfVAn1 = False   'para leerMontos

                'Actualizando el dataTable
                dsAlmacen.Tables("VSolicitudCaja").Clear()
                daTabla1.Fill(dsAlmacen, "VSolicitudCaja")

                recuperarUltimoNro()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("codSC", codSC)

                vfVAn1 = True
                btnCancelar.PerformClick()

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
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
        End If
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TSolicitudCaja set fechaSol=@fec,montoSol=@monto,imprevisto=@imp,codObra=@codO where codSC=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable1.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = txtTotIns.Text
        cmUpdateTable1.Parameters.Add("@imp", SqlDbType.Decimal, 0).Value = txtImpre.Text
        cmUpdateTable1.Parameters.Add("@codO", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Function recuperarUltimoDoc() As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroSol) from TSolicitudCaja"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetSolCaja where codSC=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc() <> CInt(BindingSource1.Item(BindingSource1.Position)(1)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima solicitud ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource1.Item(BindingSource1.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Solicitud tiene registros en detalle solicitud...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar solicitud Nº " & BindingSource1.Item(BindingSource1.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            'Tabla TSolicitudCaja
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
            vfVAn1 = False

            dsAlmacen.Tables("VSolicitudCaja").Clear()
            daTabla1.Fill(dsAlmacen, "VSolicitudCaja")

            recuperarUltimoNro()

            vfVAn1 = True
            visualizarDet()
            leerMontos()

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
        cmDeleteTable1.CommandText = "delete from TSolicitudCaja where codSC=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Function ValidarCampos1() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        If ValidarCantMayorCero(txtPre.Text) Then
            txtPre.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        vfOpc = 2 'Agrega requerimiento estructurado
        ejecutarRequerimiento()
    End Sub

    Dim vfOpc As Short
    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click
        vfOpc = 1  'Agrega requerimiento digitado
        ejecutarRequerimiento()
    End Sub

    Private Sub ejecutarRequerimiento()
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Solicitud NO APERTURADA...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(6) > 1 Then
            MessageBox.Show("Proceso denegado, en agregar requerimiento por estar en el estado de <APROBADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If ValidarCampos1() Then
            Exit Sub
        End If

        If vfOpc = 1 Then 'insumo manual
            If validaCampoVacioMinCaracNoNumer(txtBuscar.Text.Trim, 3) Then
                MessageBox.Show("Digite requerimiento valido", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtBuscar.Focus()
                Exit Sub
            End If
        End If

        If vfOpc = 2 Then 'Insumo estructurado
            If BindingSource3.Find("codMat", BindingSource4.Item(BindingSource4.Position)(0)) >= 0 Then
                MessageBox.Show("Ya exíste requerimiento: " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, Nothing, MessageBoxIcon.Information)
                txtBuscar.Focus()
                txtBuscar.SelectAll()
                Exit Sub
            End If
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            Dim compro As Short
            If cbCompro.SelectedIndex = 0 Then 'factura
                compro = 1
            End If
            If cbCompro.SelectedIndex = 1 Then 'boleta
                compro = 2
            End If
            If cbCompro.SelectedIndex = 2 Then 'honorarios
                compro = 3
            End If
            If cbCompro.SelectedIndex = 3 Then 'otros
                compro = 4
            End If

            Dim unidad As String
            Dim insumo As String
            Dim codMat As Integer
            Dim codTipM As Integer
            If vfOpc = 1 Then 'insumo manual
                unidad = cbUni.Text.Trim()
                insumo = txtBuscar.Text.Trim()
                codMat = 0
                codTipM = cbTipo.SelectedValue
            End If
            If vfOpc = 2 Then 'Insumo estructurado
                unidad = BindingSource4.Item(BindingSource4.Position)(3)
                insumo = BindingSource4.Item(BindingSource4.Position)(1)
                codMat = BindingSource4.Item(BindingSource4.Position)(0)
                codTipM = BindingSource4.Item(BindingSource4.Position)(6)
            End If

            'TDetSolCaja
            comandoInsert2(txtCan.Text, unidad, insumo, txtPre.Text, codMat, cbArea.SelectedValue, codTipM, compro)
            cmInserTable2.Transaction = myTrans
            If cmInserTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim codDetSol As Integer = cmInserTable2.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            visualizarDet()
            leerMontos()

            'TSolicitudCaja actualizando monto de detalle de solicitud 
            comandoUpdate2()
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource3.Position = BindingSource3.Find("codDetSol", codDetSol)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            txtBuscar.Clear()
            txtPre.Text = "0.0"
            txtNota.Clear()
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

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal cant As Decimal, ByVal uni As String, ByVal insumo As String, ByVal precio As Decimal, ByVal codMat As Integer, ByVal codAreaM As Integer, ByVal codTipM As Integer, ByVal compro As Integer)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.StoredProcedure
        cmInserTable2.CommandText = "PA_InsertDetSolCaja"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@can1", SqlDbType.Decimal, 0).Value = cant
        cmInserTable2.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = 0 'precio real
        cmInserTable2.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable2.Parameters.Add("@ins", SqlDbType.VarChar, 200).Value = insumo
        cmInserTable2.Parameters.Add("@ing", SqlDbType.Int, 0).Value = 0 '0=normal, 1=improvisado
        cmInserTable2.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = precio
        cmInserTable2.Parameters.Add("@pre2", SqlDbType.Decimal, 0).Value = 0
        cmInserTable2.Parameters.Add("@obsSol", SqlDbType.VarChar, 200).Value = txtNota.Text
        cmInserTable2.Parameters.Add("@codApro", SqlDbType.Int, 0).Value = 0 'No se aprobo todabia
        cmInserTable2.Parameters.Add("@estDet", SqlDbType.Int, 0).Value = 0 'pendiente
        cmInserTable2.Parameters.Add("@obsApro", SqlDbType.VarChar, 200).Value = ""
        cmInserTable2.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = codMat
        cmInserTable2.Parameters.Add("@codAreaM", SqlDbType.Int, 0).Value = codAreaM
        cmInserTable2.Parameters.Add("@codTipM", SqlDbType.Int, 0).Value = codTipM
        cmInserTable2.Parameters.Add("@codSC", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
        cmInserTable2.Parameters.Add("@estRen", SqlDbType.Int, 0).Value = 0 'pendiente
        cmInserTable2.Parameters.Add("@codRen", SqlDbType.Int, 0).Value = 0  'no se rindio
        cmInserTable2.Parameters.Add("@obsRen", SqlDbType.VarChar, 200).Value = ""
        cmInserTable2.Parameters.Add("@codDC", SqlDbType.Int, 0).Value = 0
        cmInserTable2.Parameters.Add("@nroO", SqlDbType.VarChar, 30).Value = ""
        cmInserTable2.Parameters.Add("@comp", SqlDbType.Int, 0).Value = compro
        'configurando direction output = parametro de solo salida
        cmInserTable2.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable2.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2()
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TSolicitudCaja set montoSol=@monto where codSC=@cod"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = txtTotIns.Text
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource4.RemoveFilter()
        If vFClear1 Then
            dsAlmacen.Tables("fn_MatStockObra").Clear()
            daVMat.Fill(dsAlmacen, "fn_MatStockObra")

            colorearFila1()
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,stock,uniBase,preBase,tipoM,codTipM,codUni,codigo from fn_MatStockObra(@cod)" 'material
            crearDataAdapterTable(daVMat, sele)
            daVMat.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

            daVMat.Fill(dsAlmacen, "fn_MatStockObra")

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "fn_MatStockObra"
            Navigator1.BindingSource = BindingSource4
            dgTabla1.DataSource = BindingSource4
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource4.Sort = "material"
            ModificarColumnasDGV1()

            vFClear1 = True
            colorearFila1()
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        activarControlReq()
        cbTipo.Focus()
    End Sub

    Private Sub activarControlReq()
        cbTipo.Enabled = True
        cbArea.Enabled = True
        txtCan.ReadOnly = False
        cbUni.Enabled = True
        txtBuscar.ReadOnly = False
        txtPre.ReadOnly = False
        txtNota.ReadOnly = False
        cbCompro.Enabled = True
        btnAgrega.Enabled = True
    End Sub

    Private Sub colorearFila1()
        For j As Short = 0 To BindingSource4.Count - 1
            If BindingSource4.Item(j)(2) >= 0 Then 'Resaltando Stock
                dgTabla1.Rows(j).Cells(2).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 547
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
        Dim campo As String = "material"

        BindingSource4.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%'"

        If BindingSource4.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnAgrega
            colorearFila1()
        Else
            StatusBarClass.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
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

    Private Sub txtPre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre.GotFocus, txtPre.MouseClick
        txtPre.SelectAll()
    End Sub

    Private Sub txtPre_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPre.KeyPress
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

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If BindingSource3.Item(BindingSource3.Position)(21) = 1 Then '1=Aprobado 0=Pendiente 2=Observado
            MessageBox.Show("Proceso denegado, por estar en el estado de [APROBADO]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de eliminar: " & BindingSource3.Item(BindingSource3.Position)(3) & "  Si elimina no podra deshacer...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDetSolCaja
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
            leerMontos()

            'TSolicitudCaja actualizando monto de detalle de solicitud 
            comandoUpdate2()
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            wait.Close()
            Me.Cursor = Cursors.Default
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
        cmDeleteTable12.CommandText = "delete from TDetSolCaja where codDetSol=@cod"
        cmDeleteTable12.Connection = Cn
        cmDeleteTable12.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Sub TSModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSModificar.Click
        If BindingSource1.Item(BindingSource1.Position)(6) = 1 Then
            MessageBox.Show("No se puede actualizar requerimientos por estar en el estado de <APROBADO>", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If BindingSource3.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE REGISTROS A PROCESAR...")
            Exit Sub
        End If

        Dim vCom As Boolean = False
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(21) = 0 Or BindingSource3.Item(j)(21) = 2 Then '0=pendiente 2=observado
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
                If BindingSource3.Item(j)(21) = 0 Or BindingSource3.Item(j)(21) = 2 Then '0=pendiente  2=Observado
                    'actualizando TDetalleSol
                    comandoUpdate15(BindingSource3.Item(j)(3).ToString().Trim(), BindingSource3.Item(j)(1), BindingSource3.Item(j)(4), BindingSource3.Item(j)(9).ToString().Trim(), BindingSource3.Item(j)(0))
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
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

            visualizarDet()
            leerMontos()

            'TSolicitudCaja actualizando monto de detalle de solicitud 
            comandoUpdate2()
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

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
    Private Sub comandoUpdate15(ByVal insumo As String, ByVal can As Decimal, ByVal pre As Decimal, ByVal obs As String, ByVal codDetS As Integer)
        cmdUpdateTable15 = New SqlCommand
        cmdUpdateTable15.CommandType = CommandType.Text
        cmdUpdateTable15.CommandText = "update TDetSolCaja set insumo=@ins,cant1=@can,prec1=@pre,obsSol=@obs where codDetSol=@cod"
        cmdUpdateTable15.Connection = Cn
        cmdUpdateTable15.Parameters.Add("@ins", SqlDbType.VarChar, 200).Value = insumo
        cmdUpdateTable15.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable15.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = pre
        cmdUpdateTable15.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmdUpdateTable15.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetS
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Solicitud a imprimir...")
            Exit Sub
        End If

        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        'vParam1 = BindingSource6.Item(BindingSource6.Position)(18) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year
        Dim informe As New ReportViewerReqCajaForm
        informe.ShowDialog()
    End Sub

End Class
