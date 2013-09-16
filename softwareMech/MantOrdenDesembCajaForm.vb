Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class MantOrdenDesembCajaForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

    Private Sub MantOrdenDesembCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantOrdenDesembCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin where codigo=@cod"
        crearDataAdapterTable(daTUbi, sele)
        daTUbi.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo  'Caja chica por Destino Obra

        txtSer.Text = vSSerie
        sele = "select idOP,nroDes,serie,nro,fecDes,simbolo,monto,montoDet,montoDif,banco,nroCta,est,nroDet,datoReq,hist,estado,codigo,codIde,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,codMon,codSerO from VOrdenDesembolso where codSerO=@ser" 'order by nroDes"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@ser", SqlDbType.Int, 0).Value = vSCodSerO

        sele = "select codMon,moneda,simbolo from TMoneda"
        crearDataAdapterTable(daTMon, sele)

        sele = "select codPersDes,codPers,nom,est,obserDesem,estDesem,tipoA,idOP from VPersVerificado where idOP=@idOP"
        crearDataAdapterTable(daTPers, sele)
        daTPers.SelectCommand.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")
            daTabla1.Fill(dsAlmacen, "VOrdenDesembolso")
            daTMon.Fill(dsAlmacen, "TMoneda")
            daTPers.Fill(dsAlmacen, "VPersVerificado")

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VPersVerificado"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VLugarTrabajoLogin"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VOrdenDesembolso"
            Navigator1.BindingSource = BindingSource3
            dgTabla1.DataSource = BindingSource3
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource3.Sort = "nroDes"
            ModificarColumnasDGV()

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TMoneda"
            cbMon.DataSource = BindingSource0
            cbMon.DisplayMember = "simbolo"
            cbMon.ValueMember = "codMon"

            configurarColorControl()

            recuperarUltimoNro(vSCodSerO)
            BindingSource3.MoveLast()

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

    Private Sub MantOrdenDesembCajaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVan1 = True   'para selePersDesem() se llama dentro de enlazarText()
        vfVan2 = True
        enlazarText()
        colorearFila()
        calcularTotales()
    End Sub

    Private Sub calcularTotales()
        If BindingSource3.Position = -1 Then
            txtTotal.Text = "0.00"
            txtTotal1.Text = "0.00"
            txtTotal2.Text = "0.00"
            txtTotal3.Text = "0.00"
            Exit Sub
        End If
        Try
            txtTotal.Text = Format(dsAlmacen.Tables("VOrdenDesembolso").Compute("Sum(monto)", "codMon=30"), "0,0.00")  '30=soles
            txtTotal1.Text = Format(dsAlmacen.Tables("VOrdenDesembolso").Compute("Sum(monto)", "codMon=35"), "0,0.00")  '35=dolares
            txtTotal2.Text = Format(dsAlmacen.Tables("VOrdenDesembolso").Compute("Sum(montoDet)", "codMon=30"), "0,0.00")  '30=soles
            txtTotal3.Text = Format(dsAlmacen.Tables("VOrdenDesembolso").Compute("Sum(montoDet)", "codMon=35"), "0,0.00")  '35=dolares

            If txtTotal.Text.Trim() = "" Then txtTotal.Text = "0.00"
            If txtTotal1.Text.Trim() = "" Then txtTotal1.Text = "0.00"
            If txtTotal2.Text.Trim() = "" Then txtTotal2.Text = "0.00"
            If txtTotal3.Text.Trim() = "" Then txtTotal3.Text = "0.00"
        Catch f As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(15) = 1 Then 'Terminado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Green
            End If
            If BindingSource3.Item(j)(15) = 2 Then 'Cerrado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.AliceBlue
            End If
            If BindingSource3.Item(j)(15) = 3 Then 'Anulado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Yellow
            End If
            dgTabla1.Rows(j).Cells(6).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).HeaderText = "Ser"
            .Columns(2).Width = 30
            .Columns(3).HeaderText = "NºOrd."
            .Columns(3).Width = 50
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderText = "Fec_Ord"
            .Columns(4).Width = 70
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 30
            .Columns(6).Width = 80
            .Columns(6).HeaderText = "Monto_Tot"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "#,##0.00"
            .Columns(7).Width = 70
            .Columns(7).HeaderText = "Detraccion"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "#,##0.00"
            .Columns(7).Visible = False
            .Columns(8).Width = 70
            .Columns(8).HeaderText = "Monto_Dif."
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "#,##0.00"
            .Columns(8).Visible = False
            .Columns(9).HeaderText = "Forma Pago"
            .Columns(9).Width = 100
            .Columns(10).HeaderText = "Nº Cuenta"
            .Columns(10).Width = 160
            .Columns(10).Visible = False
            .Columns(11).Width = 75
            .Columns(11).HeaderText = "Estado"
            .Columns(12).HeaderText = "Nº Detracción"
            .Columns(12).Width = 160
            .Columns(12).Visible = False
            .Columns(13).Width = 500
            .Columns(13).HeaderText = "Motivo"
            .Columns(14).Width = 800
            .Columns(14).HeaderText = ""
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
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
            .Columns(29).Visible = False
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
        Label14.ForeColor = ForeColorLabel
        Label18.ForeColor = ForeColorLabel
        Label19.ForeColor = ForeColorLabel
        Label32.ForeColor = ForeColorLabel
        Label33.ForeColor = ForeColorLabel
        Label35.ForeColor = ForeColorLabel
        checkB1.ForeColor = ForeColorLabel
        checkB2.ForeColor = ForeColorLabel
        checkB3.ForeColor = ForeColorLabel
        checkB4.ForeColor = ForeColorLabel
        checkB5.ForeColor = ForeColorLabel
        checkB6.ForeColor = ForeColorLabel
        checkB7.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnElimina.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub recuperarUltimoNro(ByVal codSerO As Integer)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroDes),0)+1 from TOrdenDesembolso where codSerO=" & codSerO
        cmdMaxCodigo.Connection = Cn
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

    Private Sub asignarNro(ByVal max As Integer)
        If CInt(max) = 1 Then  'Incio de serie primer nro
            max = vSIniNroDoc
        End If
        Select Case CInt(max)
            Case Is <= 99
                txtNro.Text = "000" & max
            Case 100 To 999
                txtNro.Text = "00" & max
            Case 1000 To 9999
                txtNro.Text = "0" & max
            Case Is > 9999
                txtNro.Text = max
        End Select
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        enlazarText()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub enlazarText()
        If vfVan2 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource3.Count = 0 Then
                Me.Cursor = Cursors.Default
                'desEnlazarText()
            Else
                date1.Value = BindingSource3.Item(BindingSource3.Position)(4)
                txtMon.Text = BindingSource3.Item(BindingSource3.Position)(6)
                txtDet.Text = BindingSource3.Item(BindingSource3.Position)(7)
                txtTot.Text = BindingSource3.Item(BindingSource3.Position)(8)
                cbMon.SelectedValue = BindingSource3.Item(BindingSource3.Position)(28)
                cambiarNroTotalLetra()
                cbObra.SelectedValue = BindingSource3.Item(BindingSource3.Position)(16)
                txtBan.Text = BindingSource3.Item(BindingSource3.Position)(9)
                txtDato.Text = BindingSource3.Item(BindingSource3.Position)(13)

                txtOtro.Text = BindingSource3.Item(BindingSource3.Position)(25)

                If BindingSource3.Item(BindingSource3.Position)(18) = 1 Then 'Fact check
                    checkB1.Checked = True
                Else 'NO chekeado
                    checkB1.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(19) = 1 Then 'Boleta check
                    checkB2.Checked = True
                Else 'NO chekeado
                    checkB2.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(20) = 1 Then 'guia remision check
                    checkB3.Checked = True
                Else 'NO chekeado
                    checkB3.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(23) = 1 Then 'Recibo check
                    checkB4.Checked = True
                Else 'NO chekeado
                    checkB4.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(21) = 1 Then 'Voucher check
                    checkB5.Checked = True
                Else 'NO chekeado
                    checkB5.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(22) = 1 Then 'Voucher detraccion check
                    checkB6.Checked = True
                Else 'NO chekeado
                    checkB6.Checked = False
                End If
                If BindingSource3.Item(BindingSource3.Position)(24) = 1 Then 'Otro check
                    checkB7.Checked = True
                Else 'NO chekeado
                    checkB7.Checked = False
                End If

                selePersDesem() 'select a todo el personal que esta autorizando

                'Dim estado As Integer
                'extraer a SOLICITANTE
                BindingSource4.Filter = "tipoA=1" '1=solicitante
                If BindingSource4.Position <> -1 Then
                    txtNom1.Text = BindingSource4.Item(BindingSource4.Position)(2)
                    txtEst1.Text = BindingSource4.Item(BindingSource4.Position)(3)
                    txtObs1.Text = BindingSource4.Item(BindingSource4.Position)(4)
                    txtEst1.ForeColor = Color.White
                    If BindingSource4.Item(BindingSource4.Position)(5) = 1 Then 'aprobado
                        txtEst1.BackColor = Color.Green
                    End If
                    If BindingSource4.Item(BindingSource4.Position)(5) = 2 Then 'Observado
                        txtEst1.BackColor = Color.Yellow
                    End If
                    If BindingSource4.Item(BindingSource4.Position)(5) = 3 Then 'denegado
                        txtEst1.BackColor = Color.Red
                    End If
                End If


                'extraer a GERENTE
                BindingSource4.Filter = "tipoA=2" '1=gerencia
                If BindingSource4.Position <> -1 Then
                    txtGer.Text = BindingSource4.Item(BindingSource4.Position)(2)
                    txtEst.Text = BindingSource4.Item(BindingSource4.Position)(3)
                    txtObs.Text = BindingSource4.Item(BindingSource4.Position)(4)
                    txtEst.ForeColor = Color.White
                    If BindingSource4.Item(BindingSource4.Position)(5) = 1 Then 'aprobado
                        txtEst.BackColor = Color.Green
                    End If
                    If BindingSource4.Item(BindingSource4.Position)(5) = 2 Then 'Observado
                        txtEst.ForeColor = Color.Blue
                        txtEst.BackColor = Color.Yellow
                    End If
                    If BindingSource4.Item(BindingSource4.Position)(5) = 3 Then 'denegado
                        txtEst.BackColor = Color.Red
                    End If
                Else
                    txtGer.Clear()
                    txtEst.Clear()
                    txtObs.Clear()
                    txtEst.BackColor = Color.White
                End If

                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub selePersDesem()
        If vfVan1 Then
            If BindingSource3.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VPersVerificado").Clear()
            daTPers.SelectCommand.Parameters("@idOP").Value = BindingSource3.Item(BindingSource3.Position)(0)
            daTPers.Fill(dsAlmacen, "VPersVerificado")
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        If cbMon.SelectedValue = 30 Then    '30=Nuevos solesl
            'If BindingSource10.Item(BindingSource10.Position)(7) = 30 Then
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(txtMon.Text) > 0 Then
            cALetra.Numero = Val(CDbl(txtMon.Text))
            txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
        End If
    End Sub

    Private Sub checkB7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkB7.CheckedChanged
        txtOtro.Focus()
        txtOtro.SelectAll()
    End Sub

    Private Function recuperarCodPers(ByVal codPersDes As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPers),0) from TPersDesem where codPersDes=" & codPersDes
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodPersDesem(ByVal idOp As Integer, ByVal tipo As Short) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPersDes),0) from TPersDesem where idOP=" & idOp & " and tipoA=" & tipo
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal estado As Short, ByVal obs As String, ByVal codPersDes As Integer)
        cmUpdateTable3 = New SqlCommand
        cmUpdateTable3.CommandType = CommandType.Text
        cmUpdateTable3.CommandText = "update TPersDesem set estDesem=@est,obserDesem=@obs where codPersDes=@cod"
        cmUpdateTable3.Connection = Cn
        cmUpdateTable3.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable3.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codPersDes
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtMon.Text) Then
            txtMon.errorProv()
            Return True
        End If
        If ValidaNroMayorOigualCero(txtDet.Text) Then
            txtDet.errorProv()
            Return True
        End If
        If ValidaNroMayorOigualCero(txtTot.Text) Then
            txtTot.errorProv()
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtDato.Text.Trim, 3) Then
            MessageBox.Show("Registre Motivo de Desembolso...", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            txtDato.Focus()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Sub txtMon_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMon.GotFocus, txtMon.MouseClick
        txtMon.SelectAll()
    End Sub

    Private Sub txtDet_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDet.GotFocus, txtDet.MouseClick
        txtDet.SelectAll()
    End Sub

    Private Sub txtTot_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTot.GotFocus, txtTot.MouseClick
        txtTot.SelectAll()
    End Sub

    Private Sub txtMon_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMon.TextChanged, txtDet.TextChanged
        If Not IsNumeric(txtDet.Text) Or Not IsNumeric(txtMon.Text) Then
            Exit Sub
        End If
        Dim cero As Double = CDbl(txtDet.Text)
        If cero = 0 Then
            txtTot.Text = "0"
        Else
            If cero > 0 Then
                txtTot.Text = txtMon.Text - txtDet.Text
            Else
                txtTot.Text = "0"
            End If
        End If
    End Sub

    Private Sub desactivarControles1()
        Panel0.Enabled = False
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
        Panel0.Enabled = True
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
        cbMon.Enabled = False
        txtMon.ReadOnly = True
        txtDet.ReadOnly = True
        txtTot.ReadOnly = True
        txtBan.ReadOnly = True
        txtDato.ReadOnly = True
        txtOtro.ReadOnly = True
    End Sub

    Private Sub limpiarText1()
        txtMon.ReadOnly = False
        txtDet.ReadOnly = False
        txtTot.ReadOnly = False
        txtBan.ReadOnly = False
        txtDato.ReadOnly = False
        txtOtro.ReadOnly = False
        cbMon.Enabled = True
        If vfNuevo1 = "guardar" Then
            txtMon.Clear()
            txtDet.Text = "0"
            txtTot.Text = "0"
            txtBan.Clear()
            txtDato.Clear()
            txtOtro.Clear()
            txtNom1.Clear()
            txtEst1.Clear()
            txtGer.Clear()
            txtEst.Clear()
        End If
    End Sub

    Dim vfNuevo1 As String = "nuevo"
    Dim vfCampo1 As String
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            limpiarText1()
            txtMon.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else
            If ValidaFechaMayorXXXX(Now.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If ValidarCampos() Then
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de aperturar ORDEN de DESEMBOLSO" & Chr(13) & "Serie: " & txtSer.Text & "  Nº " & txtNro.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                Exit Sub
            End If

            recuperarUltimoNro(vSCodSerO)
            Dim campo As Integer = CInt(txtNro.Text)

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            Me.Cursor = Cursors.WaitCursor
            wait.Show()
            'estableciendo una transaccion
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
                Me.Refresh()

                'TOrdenDesembolso
                comandoInsert1()
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                Dim idOP As Integer = cmInserTable1.Parameters("@Identity").Value

                'TPersDesem
                comandoInsert2(idOP, vPass, 1, 1, txtObs1.Text.Trim(), date1.Value.Date)  '1=aprobado  1=solicitante
                cmInserTable2.Transaction = myTrans
                If cmInserTable2.ExecuteNonQuery() < 1 Then
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
                vfVan1 = False   'para selePersDesem() se llama dentro de enlazarText()
                vfVan2 = False  'Enlazar Text  TRUE en boton cancelar

                'Actualizando el dataTable
                dsAlmacen.Tables("VOrdenDesembolso").Clear()
                daTabla1.Fill(dsAlmacen, "VOrdenDesembolso")

                recuperarUltimoNro(vSCodSerO)

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource3.Position = BindingSource3.Find("idOP", idOP)

                vfVan1 = True
                vfVan2 = True
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

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        btnNuevo.Text = "Nuevo"
        vfModificar = "modificar"
        btnModificar.Text = "Modificar"
        activarControles1()
        enlazarText()
        colorearFila()
        calcularTotales()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertOrdenDesembolso"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@ser", SqlDbType.VarChar, 5).Value = txtSer.Text.Trim()
        cmInserTable1.Parameters.Add("@nroD", SqlDbType.Int, 0).Value = txtNro.Text
        cmInserTable1.Parameters.Add("@fecD", SqlDbType.Date).Value = Now.Date 'date1.Value.Date
        cmInserTable1.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMon.SelectedValue
        cmInserTable1.Parameters.Add("@mon", SqlDbType.Decimal, 0).Value = txtMon.Text
        cmInserTable1.Parameters.Add("@mon1", SqlDbType.Decimal, 0).Value = txtDet.Text
        cmInserTable1.Parameters.Add("@mon2", SqlDbType.Decimal, 0).Value = txtTot.Text
        cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0   'pendiente
        cmInserTable1.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmInserTable1.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = 1 'por defecto proveedor constructora Mech
        cmInserTable1.Parameters.Add("@ban", SqlDbType.VarChar, 60).Value = txtBan.Text.Trim()
        cmInserTable1.Parameters.Add("@nroC", SqlDbType.VarChar, 50).Value = ""
        cmInserTable1.Parameters.Add("@nroDE", SqlDbType.VarChar, 30).Value = ""
        cmInserTable1.Parameters.Add("@dato", SqlDbType.VarChar, 200).Value = txtDato.Text.Trim()

        If checkB1.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 0
        End If

        If checkB2.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 0
        End If

        If checkB3.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 0
        End If

        If checkB5.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 0
        End If

        If checkB6.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 0
        End If

        If checkB4.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 0
        End If

        If checkB7.Checked Then 'Chekeado = 1
            cmInserTable1.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmInserTable1.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 0
        End If
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 60).Value = txtOtro.Text.Trim()

        cmInserTable1.Parameters.Add("@nroCF", SqlDbType.VarChar, 30).Value = "" 'txtNroCon.Text.Trim()
        cmInserTable1.Parameters.Add("@fec", SqlDbType.VarChar, 10).Value = "" 'txtFec.Text.Trim()
        cmInserTable1.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = "Aperturo " & Now.Date & " " & vPass & "-" & vSUsuario
        cmInserTable1.Parameters.Add("@codSerO", SqlDbType.Int, 0).Value = vSCodSerO
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal idOP As Integer, ByVal codPers As Integer, ByVal estado As Integer, ByVal tipo As Integer, ByVal obs As String, ByVal fecha As String)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "insert into TPersDesem(idOP,codPers,estDesem,tipoA,obserDesem,fecFir) values(@id,@codP,@est,@tipo,@obs,@fec)"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@id", SqlDbType.Int, 0).Value = idOP
        cmInserTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPers 'vPass
        cmInserTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado '1=Aprobado
        cmInserTable2.Parameters.Add("@tipo", SqlDbType.Int, 0).Value = tipo '1=Solicitante
        cmInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmInserTable2.Parameters.Add("@fec", SqlDbType.Date).Value = fecha
    End Sub

    Private Function recuperarModi(ByVal idOP As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TPersDesem where estDesem=1 and tipoA=2 and idOP=" & idOP '1=Aprobado  2=Gerencia
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfModificar As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso a actualizar...")
            Exit Sub
        End If

        If txtNom1.Text.Trim() = "" Then
            StatusBarClass.messageBarraEstado("  Seleccione Orden de Desembolso a actualizar...")
            Exit Sub
        End If

        If (recuperarCount1(BindingSource3.Item(BindingSource3.Position)(0)) > 0) Then
            MessageBox.Show("Actualización denegada, Orden de Desembolso tiene registros en Pago Desembolso...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If vfModificar = "modificar" Then
            If dgTabla1.Rows.Count = 0 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            limpiarText1()
            enlazarText()
            txtMon.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If

            Dim opAprobo As Short
            If recuperarModi(BindingSource3.Item(BindingSource3.Position)(0)) > 0 Then 'Ya se aprobo por gerencia
                opAprobo = 1  'Ya se aprobo
            Else 'NO se aprobo por gerencia, modificar todo
                opAprobo = 0  'NO se aprobo
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de GUARDAR MODIFICACIONES en Orden de Desembolso" & Chr(13) & "Serie: " & BindingSource3.Item(BindingSource3.Position)(2) & "  Nº " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
                Dim idOP As Integer = BindingSource3.Item(BindingSource3.Position)(0)

                If opAprobo = 0 Then  'NO se aprobo por gerencia, hacer todas las modificaciones
                    'TOrdenDesembolso
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
                Else   'SE aprobo por gerencia solo hacer modificaciones parciales
                    'TOrdenDesembolso
                    comandoUpdate11()
                    cmUpdateTable11.Transaction = myTrans
                    If cmUpdateTable11.ExecuteNonQuery() < 1 Then
                        'deshace la transaccion
                        wait.Close()
                        Me.Cursor = Cursors.Default
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                    End If
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
                vfVan1 = False   'para selePersDesem() se llama dentro de enlazarText()
                vfVan2 = False  'Enlazar Text  TRUE en boton cancelar

                'Actualizando el dataTable
                dsAlmacen.Tables("VOrdenDesembolso").Clear()
                daTabla1.Fill(dsAlmacen, "VOrdenDesembolso")

                recuperarUltimoNro(vSCodSerO)

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource3.Position = BindingSource3.Find("idOP", idOP)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan1 = True
                vfVan2 = True
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
        cmUpdateTable1.CommandText = "update TOrdenDesembolso set codMon=@codMon,monto=@mon,montoDet=@mon1,montoDif=@mon2,codigo=@cod,codIde=@codIde,banco=@ban,nroCta=@nroC,nroDet=@nroDE,datoReq=@dato,factCheck=@fact,bolCheck=@bol,guiaCheck=@guia,vouCheck=@vou,vouDCheck=@vouD,reciCheck=@reci,otroCheck=@otro,descOtro=@des,hist=@hist where idOP=@idOP"
        cmUpdateTable1.Connection = Cn
        'cmUpdateTable1.Parameters.Add("@fecD", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable1.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMon.SelectedValue
        cmUpdateTable1.Parameters.Add("@mon", SqlDbType.Decimal, 0).Value = txtMon.Text
        cmUpdateTable1.Parameters.Add("@mon1", SqlDbType.Decimal, 0).Value = txtDet.Text
        cmUpdateTable1.Parameters.Add("@mon2", SqlDbType.Decimal, 0).Value = txtTot.Text
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmUpdateTable1.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = 1 'Provvedor Mech x defecto
        cmUpdateTable1.Parameters.Add("@ban", SqlDbType.VarChar, 60).Value = txtBan.Text.Trim()
        cmUpdateTable1.Parameters.Add("@nroC", SqlDbType.VarChar, 50).Value = ""
        cmUpdateTable1.Parameters.Add("@nroDE", SqlDbType.VarChar, 30).Value = ""
        cmUpdateTable1.Parameters.Add("@dato", SqlDbType.VarChar, 200).Value = txtDato.Text.Trim()

        If checkB1.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 0
        End If

        If checkB2.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 0
        End If

        If checkB3.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 0
        End If

        If checkB5.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 0
        End If

        If checkB6.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 0
        End If

        If checkB4.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 0
        End If

        If checkB7.Checked Then 'Chekeado = 1
            cmUpdateTable1.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable1.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable1.Parameters.Add("@des", SqlDbType.VarChar, 60).Value = txtOtro.Text.Trim()
        cmUpdateTable1.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = BindingSource3.Item(BindingSource3.Position)(14) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable1.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Dim cmUpdateTable11 As SqlCommand  'MOdificacion parcial
    Private Sub comandoUpdate11()
        cmUpdateTable11 = New SqlCommand
        cmUpdateTable11.CommandType = CommandType.Text
        cmUpdateTable11.CommandText = "update TOrdenDesembolso set banco=@ban,nroCta=@nroC,nroDet=@nroDE,datoReq=@dato,factCheck=@fact,bolCheck=@bol,guiaCheck=@guia,vouCheck=@vou,vouDCheck=@vouD,reciCheck=@reci,otroCheck=@otro,descOtro=@des,hist=@hist where idOP=@idOP"
        cmUpdateTable11.Connection = Cn
        cmUpdateTable11.Parameters.Add("@ban", SqlDbType.VarChar, 60).Value = txtBan.Text.Trim()
        cmUpdateTable11.Parameters.Add("@nroC", SqlDbType.VarChar, 50).Value = ""
        cmUpdateTable11.Parameters.Add("@nroDE", SqlDbType.VarChar, 30).Value = ""
        cmUpdateTable11.Parameters.Add("@dato", SqlDbType.VarChar, 200).Value = txtDato.Text.Trim()

        If checkB1.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@fact", SqlDbType.Int, 0).Value = 0
        End If

        If checkB2.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@bol", SqlDbType.Int, 0).Value = 0
        End If

        If checkB3.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@guia", SqlDbType.Int, 0).Value = 0
        End If

        If checkB5.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@vou", SqlDbType.Int, 0).Value = 0
        End If

        If checkB6.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@vouD", SqlDbType.Int, 0).Value = 0
        End If

        If checkB4.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@reci", SqlDbType.Int, 0).Value = 0
        End If

        If checkB7.Checked Then 'Chekeado = 1
            cmUpdateTable11.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 1
        Else 'No Chekeado
            cmUpdateTable11.Parameters.Add("@otro", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable11.Parameters.Add("@des", SqlDbType.VarChar, 60).Value = txtOtro.Text.Trim()
        cmUpdateTable11.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = BindingSource3.Item(BindingSource3.Position)(14) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable11.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Function recuperarNroOrden(ByVal idOP As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) when nroO>=1000 and nroO<10000 then '0'+ltrim(str(nroO)) else ltrim(str(nroO)) end + '-MECH-' + ltrim(str(year(fecOrden))) from TOrdenCompra TOC join TDesOrden TD on TOC.nroOrden=TD.nroOrden where idOP=" & idOP
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarUltimoDoc(ByVal codSer As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroDes) from TOrdenDesembolso where codSerO=" & codSer
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TPagoDesembolso where idOP=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc(BindingSource3.Item(BindingSource3.Position)(29)) <> CInt(BindingSource3.Item(BindingSource3.Position)(1)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima ORDEN DE DESEMBOLSO registrada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If recuperarModi(BindingSource3.Item(BindingSource3.Position)(0)) > 0 Then 'Ya se aprobo por gerencia
            MessageBox.Show("No se puede ELIMINAR por que ya se [APROBO] por Gerencia", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If txtNom1.Text.Trim() = "" Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso a Eliminar...")
            Exit Sub
        End If

        If (recuperarCount1(BindingSource3.Item(BindingSource3.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Orden de Desembolso tiene registros en Pago Desembolso...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar orden de desembolso Nº " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Try

            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TDesOrden
            comandoDelete2()
            cmDeleteTable2.Transaction = myTrans
            cmDeleteTable2.ExecuteNonQuery()

            'Tabla TPersDesem
            comandoDelete3()
            cmDeleteTable3.Transaction = myTrans
            cmDeleteTable3.ExecuteNonQuery()

            'Tabla TOrdenDesembolso
            comandoDelete1()
            cmDeleteTable1.Transaction = myTrans
            If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
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
            vfVan1 = False   'para selePersDesem() se llama dentro de enlazarText()
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesembolso").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesembolso")

            recuperarUltimoNro(vSCodSerO)

            vfVan1 = True
            vfVan2 = True
            enlazarText()

            colorearFila()
            calcularTotales()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
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
        cmDeleteTable1.CommandText = "delete from TOrdenDesembolso where idOP=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Dim cmDeleteTable2 As SqlCommand
    Private Sub comandoDelete2()
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "delete from TDesOrden where idOP=@cod"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Dim cmDeleteTable3 As SqlCommand
    Private Sub comandoDelete3()
        cmDeleteTable3 = New SqlCommand
        cmDeleteTable3.CommandType = CommandType.Text
        cmDeleteTable3.CommandText = "delete from TPersDesem where idOP=@cod"
        cmDeleteTable3.Connection = Cn
        cmDeleteTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Sub btnAnula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnula.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso a ANULAR...")
            Exit Sub
        End If

        If recuperarModi(BindingSource3.Item(BindingSource3.Position)(0)) > 0 Then 'Ya se aprobo por gerencia
            MessageBox.Show("Anulación denegada, por que ya se [APROBO] por Gerencia", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If txtNom1.Text.Trim() = "" Then
            StatusBarClass.messageBarraEstado("  No existe Orden de Desembolso a Anular...")
            Exit Sub
        End If

        If (recuperarCount1(BindingSource3.Item(BindingSource3.Position)(0)) > 0) Then
            MessageBox.Show("Anulación denegada, Orden de Desembolso tiene registros en Pago Desembolso. Si Desea anular de todas formas, quitese Pagos Fisicos de Desembolso [TEsoreria]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de ANULAR Orden de Desembolso" & Chr(13) & "Serie: " & BindingSource3.Item(BindingSource3.Position)(2) & "  Nº " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            StatusBarClass.messageBarraEstado("  ESPERE PROCESANDO INFORMACION....")
            Dim idOP As Integer = BindingSource3.Item(BindingSource3.Position)(0)

            'Tabla TDesOrden
            comandoDelete2()
            cmDeleteTable2.Transaction = myTrans
            cmDeleteTable2.ExecuteNonQuery()

            'TOrdenDesembolso
            comandoUpdate13()
            cmUpdateTable13.Transaction = myTrans
            If cmUpdateTable13.ExecuteNonQuery() < 1 Then
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
            vfVan1 = False   'para selePersDesem() se llama dentro de enlazarText()
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesembolso").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesembolso")

            recuperarUltimoNro(vSCodSerO)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource3.Position = BindingSource3.Find("idOP", idOP)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan1 = True
            vfVan2 = True
            enlazarText()

            colorearFila()
            calcularTotales()
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
    End Sub

    Dim cmUpdateTable13 As SqlCommand
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TOrdenDesembolso set estado=@est,hist=@hist where idOP=@idOP"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@est", SqlDbType.Int, 0).Value = 3 '3 = anulado
        cmUpdateTable13.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = BindingSource3.Item(BindingSource3.Position)(14) & " ANULO " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable13.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = BindingSource3.Item(BindingSource3.Position)(0)
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        vCodDoc = BindingSource3.Item(BindingSource3.Position)(0)
        vParam1 = txtLetraTotal.Text.Trim()
        vParam2 = "" 'txtOrden.Text.Trim()

        Dim informe As New ReportViewerOrdenDesembolsoForm
        informe.ShowDialog()
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

    Private Sub txtDet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDet.KeyPress
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

    Private Sub txtTot_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTot.KeyPress
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

    Private Function recuperarNroOrden1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select nroOrden from TDesOrden where idOP=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub dgTabla1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellContentClick

    End Sub
End Class
