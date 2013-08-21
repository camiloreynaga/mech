Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class tesoreriaOrdenDesembolsoForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource

    Private Sub tesoreriaOrdenDesembolsoForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub tesoreriaOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idOP,fecDes,serie,nro,simbolo,monto,montoDet,montoDif,nombre,estApro,nom,datoReq,ruc,razon,banco,nroCta,nroDet,hist,estDesem,codPersDes,estado,codMon,nomSol,codPersSol,codSerO from VOrdenDesemTesoreria where codSerO>@codSer1 or codSerO=@codSer2" 'order by idOP
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codSer1", SqlDbType.Int, 0).Value = 0 'Todos
        daTabla1.SelectCommand.Parameters.Add("@codSer2", SqlDbType.Int, 0).Value = 0

        sele = "select codPagD,fecPago,tipoP,nroP,pagoDet,simbolo,montoPago,montoD,clasif,codTipP,codMon,idOP,idCue,codCla from VPagoDesemTesoreria where idOP=@idOP"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = 0

        sele = "select codMon,moneda,simbolo from TMoneda"
        crearDataAdapterTable(daTMon, sele)

        sele = "select codTipP,tipoP from TTipoPago"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select idCue,banmon,banco,nroCue,simbolo,moneda,codBan,codMon from VBancoCuenta order by banco,simbolo"
        crearDataAdapterTable(daTabla3, sele)

        sele = "select codCla,clasif from TClasifPago"
        crearDataAdapterTable(daTabla4, sele)

        sele = "select codSerO,serie from TSerieOrden where estado=1 order by serie"
        crearDataAdapterTable(daTabla5, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VOrdenDesemTesoreria")
            daDetDoc.Fill(dsAlmacen, "VPagoDesemTesoreria")
            daTMon.Fill(dsAlmacen, "TMoneda")
            daTabla2.Fill(dsAlmacen, "TTipoPago")
            daTabla3.Fill(dsAlmacen, "VBancoCuenta")
            daTabla4.Fill(dsAlmacen, "TClasifPago")
            daTabla5.Fill(dsAlmacen, "TSerieOrden")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VOrdenDesemTesoreria"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "idOP"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VPagoDesemTesoreria"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TMoneda"
            cbMon.DataSource = BindingSource0
            cbMon.DisplayMember = "simbolo"
            cbMon.ValueMember = "codMon"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TTipoPago"
            cbMod.DataSource = BindingSource3
            cbMod.DisplayMember = "tipoP"
            cbMod.ValueMember = "codTipP"

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VBancoCuenta"
            cbCue.DataSource = BindingSource4
            cbCue.DisplayMember = "banmon"
            cbCue.ValueMember = "idCue"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "TClasifPago"
            cbCla.DataSource = BindingSource5
            cbCla.DisplayMember = "clasif"
            cbCla.ValueMember = "codCla"

            cbSerie.DataSource = dsAlmacen
            cbSerie.DisplayMember = "TSerieOrden.serie"
            cbSerie.ValueMember = "codSerO"

            configurarColorControl()

            txtProv.DataBindings.Add("Text", BindingSource1, "razon")
            txtRuc.DataBindings.Add("Text", BindingSource1, "ruc")
            txtForma.DataBindings.Add("Text", BindingSource1, "banco")
            txtNC.DataBindings.Add("Text", BindingSource1, "nroCta")
            txtND.DataBindings.Add("Text", BindingSource1, "nroDet")
            txtMot.DataBindings.Add("Text", BindingSource1, "datoReq")

            vfVan1 = True
            visualizarDet()

            rb1.Checked = True

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

    Private Sub tesoreriaOrdenDesembolsoForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        calcularTotales()
        BindingSource1.MoveLast()
    End Sub

    Private Sub calcularTotales()
        If BindingSource1.Position = -1 Then
            txtTotal0.Text = "0.00"
            txtTotal1.Text = "0.00"
            txtTotal2.Text = "0.00"
            txtTotal3.Text = "0.00"
            Exit Sub
        End If
        Try
            txtTotal0.Text = dsAlmacen.Tables("VOrdenDesemTesoreria").Compute("Sum(monto)", "codMon=30").ToString()  '30=soles
            txtTotal1.Text = dsAlmacen.Tables("VOrdenDesemTesoreria").Compute("Sum(monto)", "codMon=35").ToString()  '35=dolares
            txtTotal2.Text = dsAlmacen.Tables("VOrdenDesemTesoreria").Compute("Sum(montoDet)", "codMon=30").ToString()  '30=soles
            txtTotal3.Text = dsAlmacen.Tables("VOrdenDesemTesoreria").Compute("Sum(montoDet)", "codMon=35").ToString()  '35=dolares

            If txtTotal0.Text.Trim() = "" Then txtTotal0.Text = "0.00"
            If txtTotal1.Text.Trim() = "" Then txtTotal1.Text = "0.00"
            If txtTotal2.Text.Trim() = "" Then txtTotal2.Text = "0.00"
            If txtTotal3.Text.Trim() = "" Then txtTotal3.Text = "0.00"

            If txtTotal0.Text.Trim() = "" Then
                txtTotal0.Text = "0.00"
            Else
                txtTotal0.Text = Format(CDbl(txtTotal0.Text), "0,0.00")
            End If

            If txtTotal1.Text.Trim() = "" Then
                txtTotal1.Text = "0.00"
            Else
                txtTotal1.Text = Format(CDbl(txtTotal1.Text), "0,0.00")
            End If

            If txtTotal2.Text.Trim() = "" Then
                txtTotal2.Text = "0.00"
            Else
                txtTotal2.Text = Format(CDbl(txtTotal2.Text), "0,0.00")
            End If

            If txtTotal3.Text.Trim() = "" Then
                txtTotal3.Text = "0.00"
            Else
                txtTotal3.Text = Format(CDbl(txtTotal3.Text), "0,0.00")
            End If

        Catch f As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub cbCue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCue.SelectedIndexChanged
        cbMon.SelectedValue = BindingSource4.Item(cbCue.SelectedIndex)(7)
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(18) = 1 Then 'Aprobado
                dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(9).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(18) = 2 Then 'Observado
                dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(9).Style.ForeColor = Color.Red
            End If
            If BindingSource1.Item(j)(18) = 3 Then 'Rechazado
                dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Red
                dgTabla1.Rows(j).Cells(9).Style.ForeColor = Color.White
            End If
            dgTabla1.Rows(j).Cells(5).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Width = 70
            .Columns(1).HeaderText = "Fecha"
            .Columns(2).HeaderText = "Serie"
            .Columns(2).Width = 40
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).HeaderText = "NºOrden"
            .Columns(3).Width = 50
            .Columns(4).HeaderText = ""
            .Columns(4).Width = 30
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "Monto"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "N2"
            .Columns(6).Width = 70
            .Columns(6).HeaderText = "Detracción"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "N2"
            .Columns(7).Width = 75
            .Columns(7).HeaderText = "Diferencia"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "N2"
            .Columns(8).Width = 260
            .Columns(8).HeaderText = "Lugar / Obra"
            .Columns(9).Width = 50
            .Columns(9).HeaderText = "Estado"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).Width = 110
            .Columns(10).HeaderText = "Aprobo"
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Width = 110
            .Columns(22).HeaderText = "Solicito"
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 70
            .Columns(1).HeaderText = "Fecha"
            .Columns(2).Width = 206
            .Columns(2).HeaderText = "Medio Pago"
            .Columns(3).Width = 90
            .Columns(3).HeaderText = "Nº"
            .Columns(4).HeaderText = "Descripción"
            .Columns(4).Width = 320
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 30
            .Columns(6).Width = 75
            .Columns(6).HeaderText = "Monto"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "N2"
            .Columns(7).Width = 65
            .Columns(7).HeaderText = "Detracción"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "N2"
            .Columns(8).HeaderText = "Clasificación"
            .Columns(8).Width = 100
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
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
        CheckBox1.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                dsAlmacen.Tables("VPagoDesemTesoreria").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VPagoDesemTesoreria").Clear()
            daDetDoc.SelectCommand.Parameters("@idOP").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VPagoDesemTesoreria")
            'colorear()
            sumTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
            txtTotal4.Text = "0.00"
            txtTotal5.Text = "0.00"
            txtTotal6.Text = "0.00"
            txtTotal7.Text = "0.00"
            Exit Sub
        End If
        txtTotal4.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoPago)", "codMon=30").ToString()  '30=soles
        txtTotal5.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoPago)", "codMon=35").ToString()  '30=soles
        txtTotal6.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoD)", "codMon=30").ToString()  '30=soles
        txtTotal7.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoD)", "codMon=35").ToString()  '30=soles

        If txtTotal4.Text.Trim() = "" Then txtTotal4.Text = "0.00"
        If txtTotal5.Text.Trim() = "" Then txtTotal5.Text = "0.00"
        If txtTotal6.Text.Trim() = "" Then txtTotal6.Text = "0.00"
        If txtTotal7.Text.Trim() = "" Then txtTotal7.Text = "0.00"
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        If vfNuevo2 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub activarText()
        txtDes.ReadOnly = False
        cbMod.Enabled = True
        cbCue.Enabled = True
        txtNro.ReadOnly = False
        cbCla.Enabled = True
        txtMon.ReadOnly = False
    End Sub

    Private Sub desActivarText()
        cbMod.Enabled = False
        cbCue.Enabled = False
        txtNro.ReadOnly = True
        cbCla.Enabled = False
        txtDes.ReadOnly = True
        txtMon.ReadOnly = True
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If validaCampoVacioMinCaracNoNumer(txtDes.Text.Trim, 3) Then
            txtDes.errorProv()
            Return True
        End If
        If ValidarCantMayorCero(txtMon.Text) Then
            txtMon.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Dim vfNuevo2 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso...")
            Exit Sub
        End If
        If vfNuevo2 = "nuevo" Then
            vfNuevo2 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            activarText()
            txtDes.Clear()
            txtNro.Clear()
            cbMod.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo

            cbMon.SelectedValue = BindingSource1.Item(BindingSource1.Position)(21)
            txtMon.Text = BindingSource1.Item(BindingSource1.Position)(5)
            txtDes.Text = BindingSource1.Item(BindingSource1.Position)(11)
        Else   ' guardar
            'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
            If ValidarCampos() Then
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

                'TPagoDesembolso
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
                Dim codPagD As Integer = cmInserTable.Parameters("@Identity").Value

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan1 = False

                Me.btnCancelar.PerformClick()

                vfVan1 = True
                visualizarDet()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource2.Position = BindingSource2.Find("codPagD", codPagD)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
                wait.Close()
                Me.Cursor = Cursors.Default
                colorearFila()

            Catch f As Exception
                wait.Close()
                Me.Cursor = Cursors.Default
                If finalMytrans Then
                    MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show("tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End Try
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo2 = "nuevo"
        Me.btnNuevo.Text = "Pagar"
        vfModificar2 = "modificar"
        Me.btnModificar.Text = "Modificar"
        activarControles1()
        desActivarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
        enlazarText()

        cbCue.SelectedIndex = 0
        txtNro.Clear()
        cbCla.SelectedIndex = 0
        txtDes.Clear()
        txtMon.Clear()
        CheckBox1.Checked = False
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertTPagoDesembolso"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbMod.SelectedValue
        cmInserTable.Parameters.Add("@nro", SqlDbType.VarChar, 20).Value = txtNro.Text
        cmInserTable.Parameters.Add("@pago", SqlDbType.VarChar, 100).Value = txtDes.Text
        cmInserTable.Parameters.Add("@codC", SqlDbType.Int, 0).Value = cbCla.SelectedValue
        cmInserTable.Parameters.Add("@codM", SqlDbType.Int, 0).Value = cbMon.SelectedValue
        If CheckBox1.Checked = False Then
            cmInserTable.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = txtMon.Text
            cmInserTable.Parameters.Add("@montoD", SqlDbType.Decimal, 0).Value = 0
        Else 'Detraccion
            cmInserTable.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = 0
            cmInserTable.Parameters.Add("@montoD", SqlDbType.Decimal, 0).Value = txtMon.Text
        End If
        cmInserTable.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
        cmInserTable.Parameters.Add("@idC", SqlDbType.Int, 0).Value = cbCue.SelectedValue
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal idOP As Integer, ByVal codPers As Integer, ByVal estado As Integer, ByVal tipo As Integer, ByVal obs As String)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "insert into TPersDesem(idOP,codPers,estDesem,tipoA,obserDesem,fecFir) values(@id,@codP,@est,@tipo,@obs,@fec)"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@id", SqlDbType.Int, 0).Value = idOP
        cmInserTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPers 'vPass
        cmInserTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado '1=Aprobado
        cmInserTable2.Parameters.Add("@tipo", SqlDbType.Int, 0).Value = tipo '1=Solicitante
        cmInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmInserTable2.Parameters.Add("@fec", SqlDbType.Date).Value = Now.Date
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("Proceso denegado, No existe registro de pagos...")
            Exit Sub
        End If

        Dim total As Double = (CDbl(txtTotal4.Text) + CDbl(txtTotal5.Text) + CDbl(txtTotal6.Text) + CDbl(txtTotal7.Text))
        If total <> CDbl(BindingSource1.Item(BindingSource1.Position)(5)) Then
            MessageBox.Show("Proceso denegado, Montos Diferentes en Transacción [" & Format(total, "0,0.00") & "<>" & BindingSource1.Item(BindingSource1.Position)(5) & "]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de CERRAR ORDEN de DESEMBOLSO Nº " & BindingSource1.Item(BindingSource1.Position)(3) & Chr(13) & " Si cierra no podra deshacer proceso...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TPersDesem
            comandoInsert2(BindingSource1.Item(BindingSource1.Position)(0), vPass, 1, 3, "")  '1=aprobado  3=tesoreria
            cmInserTable2.Transaction = myTrans
            If cmInserTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'TOrdenDesembolso
            comandoUpdate23(1) '1=Terminado 2=cerrado
            cmUpdateTable23.Transaction = myTrans
            If cmUpdateTable23.ExecuteNonQuery() < 1 Then
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

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON CERRADOS CON EXITO...")
            finalMytrans = True
            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesemTesoreria").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesemTesoreria")

            vfVan1 = True
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué cerrado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            colorearFila()
            calcularTotales()
            BindingSource1.MoveLast()
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

    Dim cmUpdateTable23 As SqlCommand
    Private Sub comandoUpdate23(ByVal estado As Short)
        cmUpdateTable23 = New SqlCommand
        cmUpdateTable23.CommandType = CommandType.Text
        cmUpdateTable23.CommandText = "update TOrdenDesembolso set estado=@est where idOP=@nro"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla2.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")

            'Tabla TPagoDesembolso
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("ERROR..No se puede eliminar producto...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True
            vfVan1 = False


            vfVan1 = True
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()
            colorearFila()
        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("tipoM de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TPagoDesembolso where codPagD=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Dim vfModificar2 As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar2 = "modificar" Then
            If dgTabla2.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            enlazarText()
            vfModificar2 = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            activarText()
            date1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
            If ValidarCampos() Then
                Exit Sub
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TPagoDesembolso
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                Dim codPagD As Integer = BindingSource2.Item(BindingSource2.Position)(0)

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan1 = False

                Me.btnCancelar.PerformClick()

                vfVan1 = True
                visualizarDet()

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource2.Position = BindingSource2.Find("codPagD", codPagD)

                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
                'colorearFila()
            Catch f As Exception
                wait.Close()
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

    Private Sub enlazarText()
        If BindingSource2.Count = 0 Then
            'desEnlazarText()
        Else
            date1.Value = BindingSource2.Item(BindingSource2.Position)(1)
            cbMod.SelectedValue = BindingSource2.Item(BindingSource2.Position)(9)
            cbCue.SelectedValue = BindingSource2.Item(BindingSource2.Position)(12)
            txtNro.Text = BindingSource2.Item(BindingSource2.Position)(3)
            cbCla.SelectedValue = BindingSource2.Item(BindingSource2.Position)(13)
            txtDes.Text = BindingSource2.Item(BindingSource2.Position)(4)
            cbMon.SelectedValue = BindingSource2.Item(BindingSource2.Position)(10)
            If BindingSource2.Item(BindingSource2.Position)(6) > 0 Then
                txtMon.Text = BindingSource2.Item(BindingSource2.Position)(6)
                CheckBox1.Checked = False
            Else 'detraccion monto
                txtMon.Text = BindingSource2.Item(BindingSource2.Position)(7)
                CheckBox1.Checked = True
            End If
        End If
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TPagoDesembolso set fecPago=@fec,codTipP=@codT,nroP=@nro,pagoDet=@det,codCla=@codC,codMon=@codM,montoPago=@monto,montoD=@montoD,idCue=@idC where codPagD=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbMod.SelectedValue
        cmUpdateTable.Parameters.Add("@nro", SqlDbType.VarChar, 20).Value = txtNro.Text.Trim()
        cmUpdateTable.Parameters.Add("@det", SqlDbType.VarChar, 100).Value = txtDes.Text.Trim()
        cmUpdateTable.Parameters.Add("@codC", SqlDbType.Int, 0).Value = cbCla.SelectedValue
        cmUpdateTable.Parameters.Add("@codM", SqlDbType.Int, 0).Value = cbMon.SelectedValue
        If CheckBox1.Checked = False Then
            cmUpdateTable.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = txtMon.Text.Trim()
            cmUpdateTable.Parameters.Add("@montoD", SqlDbType.Decimal, 0).Value = 0
        Else 'Detraccion
            cmUpdateTable.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = 0
            cmUpdateTable.Parameters.Add("@montoD", SqlDbType.Decimal, 0).Value = txtMon.Text.Trim()
        End If
        cmUpdateTable.Parameters.Add("@idC", SqlDbType.Int, 0).Value = cbCue.SelectedValue
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Private Sub txtMon_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMon.GotFocus, txtMon.MouseClick
        txtMon.SelectAll()
    End Sub

    Dim codSer1 As Integer
    Dim codSer2 As Integer
    Private Sub rb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb1.CheckedChanged, rb2.CheckedChanged
        If rb1.Checked Then 'Todos
            cbSerie.Visible = False
            btnF1.Visible = False
            codSer1 = 0
            codSer2 = 0

            vfVan1 = False
            visualizarOrd()
            BindingSource1.MoveLast()
            vfVan1 = True
            visualizarDet()
            colorearFila()
            calcularTotales()
        End If
        If rb2.Checked Then 'Por serie
            cbSerie.Visible = True
            btnF1.Visible = True
            codSer1 = 100
            codSer2 = cbSerie.SelectedValue
        End If
    End Sub

    Private Sub visualizarOrd()
        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VOrdenDesemTesoreria").Clear()
        daTabla1.SelectCommand.Parameters("@codSer1").Value = codSer1
        daTabla1.SelectCommand.Parameters("@codSer2").Value = codSer2
        daTabla1.Fill(dsAlmacen, "VOrdenDesemTesoreria")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnF1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF1.Click
        codSer1 = 100
        codSer2 = cbSerie.SelectedValue
        vfVan1 = False
        visualizarOrd()
        BindingSource1.MoveLast()
        vfVan1 = True
        visualizarDet()
        colorearFila()
        calcularTotales()
    End Sub

    Private Sub txtMon_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMon.KeyPress
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Desembolso...")
            Exit Sub
        End If

        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        'vParam1 = cambiarNroTotalLetra()
        cambiarNroTotalLetra()
        vParam2 = "" 'txtOrden.Text.Trim()

        Dim informe As New ReportViewerOrdenDesembolsoForm
        informe.ShowDialog()
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        '30=Nuevos solesl
        If BindingSource1.Item(BindingSource1.Position)(21) = 30 Then
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(BindingSource1.Item(BindingSource1.Position)(5)) > 0 Then
            cALetra.Numero = Val(CDbl(BindingSource1.Item(BindingSource1.Position)(5)))
            vParam1 = "SON: " & cALetra.ALetra.ToUpper()
        End If
    End Sub
End Class
