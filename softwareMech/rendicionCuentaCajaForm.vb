Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class rendicionCuentaCajaForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource

    Private Sub rendicionCuentaCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
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

    Private Sub rendicionCuentaCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSC,nro,fechaSol,nomSoli,montoSol,imprevisto,salAnt,totPar,est,nomObra,nomSede,estSol,codObra,codSede,codPers,requerimiento from VSolicitudCajaCuenta where codPers=@codPers"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codPers", SqlDbType.VarChar, 10).Value = vPass

        sele = "select codTipM,tipoM from TTipoMat"
        crearDataAdapterTable(daTTipo, sele)

        sele = "select codDetSol,tipoM,cant1,insumo,prec1,totPar,comp,fecha,comp1,nroOtros,cant2,prec2,totReal,estRen1,nom,obsRen,ingre,areaM,codRen,codMat,codAreaM,codTipM,codSC,codDC,compCheck,estRen,compRen,ingreso,obsSol from VDetSolCajaCuenta where (codSC=@cod1 and codTipM=@codT) or (codSC=@cod2)"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod1", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@codT", SqlDbType.Int, 0).Value = 0
        daDetDoc.SelectCommand.Parameters.Add("@cod2", SqlDbType.Int, 0).Value = 0

        sele = "select codAreaM,areaM from TAreaMat"
        crearDataAdapterTable(daTArea, sele)

        sele = "select codUni,unidad from TUnidad where codUni>1 order by unidad"  '1=""
        crearDataAdapterTable(daTUni, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolicitudCajaCuenta")
            daTTipo.Fill(dsAlmacen, "TTipoMat")
            daTArea.Fill(dsAlmacen, "TAreaMat")
            daTUni.Fill(dsAlmacen, "TUnidad")
            daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolicitudCajaCuenta"
            lbSol.DataSource = BindingSource1
            lbSol.DisplayMember = "nro"
            lbSol.ValueMember = "codSC"
            BindingSource1.Sort = "codSC"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TTipoMat"
            cbTip1.DataSource = BindingSource2
            cbTip1.DisplayMember = "tipoM"
            cbTip1.ValueMember = "codTipM"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "TTipoMat"
            cbTipo.DataSource = BindingSource5
            cbTipo.DisplayMember = "tipoM"
            cbTipo.ValueMember = "codTipM"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TAreaMat"
            cbArea.DataSource = BindingSource0
            cbArea.DisplayMember = "areaM"
            cbArea.ValueMember = "codAreaM"

            cbUni.DataSource = dsAlmacen
            cbUni.DisplayMember = "TUnidad.unidad"
            cbUni.ValueMember = "TUnidad.codUni"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VDetSolCajaCuenta"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource3.Sort = "codTipM,codDetSol"
            ModificarColumnasDGV()

            configurarColorControl()
            cbCompro1.SelectedIndex = 0
            cbCompro.SelectedIndex = 0
            cbVis.Checked = True

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

    Private Sub leerMontos()
        If BindingSource1.Position <> -1 Then
            txtSol.Text = BindingSource1.Item(lbSol.SelectedIndex)(3)
            txtTotIns.Text = BindingSource1.Item(lbSol.SelectedIndex)(4)
            date1.Value = BindingSource1.Item(lbSol.SelectedIndex)(2)
            txtObra.Text = BindingSource1.Item(lbSol.SelectedIndex)(9)
            txtImpre.Text = BindingSource1.Item(lbSol.SelectedIndex)(5)
            txtSalAnt.Text = BindingSource1.Item(lbSol.SelectedIndex)(6)
            txtTotReq.Text = Format(BindingSource1.Item(lbSol.SelectedIndex)(7), "0,0.00")
            txtReq.Text = Format(BindingSource1.Item(lbSol.SelectedIndex)(15), "0,0.00")

            txtTotEgr.Text = Format(recuperarTotReal(lbSol.SelectedValue), "0,0.00")
            txtSalAct.Text = Format((txtTotReq.Text - txtTotEgr.Text) + txtSalAnt.Text, "0,0.00")
        End If
    End Sub

    Private Sub dgTabla2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla2.CurrentCellChanged
        capturarCampoDetDoc()
    End Sub

    Private Sub capturarCampoDetDoc()
        If BindingSource3.Position = -1 Then
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(7).ToString().Trim() = "" Then
        Else
            date2.Value = BindingSource3.Item(BindingSource3.Position)(7)
        End If

        If BindingSource3.Item(BindingSource3.Position)(26) = 1 Then 'factura
            cbCompro1.SelectedIndex = 0
        End If
        If BindingSource3.Item(BindingSource3.Position)(26) = 2 Then 'boleta
            cbCompro1.SelectedIndex = 1
        End If
        If BindingSource3.Item(BindingSource3.Position)(26) = 3 Then 'honorarios
            cbCompro1.SelectedIndex = 2
        End If
        If BindingSource3.Item(BindingSource3.Position)(26) = 4 Then 'otros
            cbCompro1.SelectedIndex = 3
        End If

        txtNro.Text = BindingSource3.Item(BindingSource3.Position)(9)
        txtCan.Text = BindingSource3.Item(BindingSource3.Position)(10)
        txtPre.Text = BindingSource3.Item(BindingSource3.Position)(11)
        txtNota.Text = BindingSource3.Item(BindingSource3.Position)(28)
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
        colorear()
        sumTotal()
        Me.Cursor = Cursors.Default
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
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(25) = 1 Then 'OK
                dgTabla2.Rows(j).Cells(13).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(13).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(25) = 2 Then 'Observado
                dgTabla2.Rows(j).Cells(13).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(13).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource3.Count - 1
            dgTabla2.Rows(j).Cells(10).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(11).Style.BackColor = Color.AliceBlue
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource3.Position = -1 Then
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

        If BindingSource3.Find("compRen", 1) <> -1 Then 'Factura
            txtFac.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=1")), "0,0.00")
        Else
            txtFac.Text = "0.00"
        End If

        If BindingSource3.Find("compRen", 2) <> -1 Then 'Factura
            txtBol.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=2")), "0,0.00")
        Else
            txtBol.Text = "0.00"
        End If

        If BindingSource3.Find("compRen", 3) <> -1 Then 'Factura
            txtHon.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=3")), "0,0.00")
        Else
            txtHon.Text = "0.00"
        End If

        If BindingSource3.Find("compRen", 4) <> -1 Then 'Factura
            txtOtro.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=4")), "0,0.00")
        Else
            txtOtro.Text = "0.00"
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 80
            .Columns(1).HeaderText = "Tipo_Insumo"
            .Columns(1).ReadOnly = True 'NO editable
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Cant"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 280
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "PreUni"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).Width = 55
            .Columns(5).HeaderText = "TotParc."
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "#,##0.00"
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Entrega"
            .Columns(6).Width = 55
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).HeaderText = "Fec_Doc."
            .Columns(7).Width = 75
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
            .Columns(11).Width = 55
            .Columns(11).HeaderText = "PrecUni"
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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
        Label17.ForeColor = ForeColorLabel
        Label18.ForeColor = ForeColorLabel
        Label19.ForeColor = ForeColorLabel
        Label20.ForeColor = ForeColorLabel
        Label21.ForeColor = ForeColorLabel
        Label22.ForeColor = ForeColorLabel
        Label23.ForeColor = ForeColorLabel
        Label24.ForeColor = ForeColorLabel
        Label25.ForeColor = ForeColorLabel
        Label26.ForeColor = ForeColorLabel
        cbVis.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
        btnProcesa.ForeColor = ForeColorButtom
        btnCrear.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
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

    Private Sub txtCan_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.GotFocus, txtCan.MouseClick
        txtCan.SelectAll()
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

    'Evento  Se produce cuando la celda termina de validar
    Private Sub dgTabla2_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla2.CellValidated
        dgTabla2.Rows(e.RowIndex).ErrorText = Nothing   'Borrar el error
    End Sub

    Private Sub dgTabla2_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgTabla2.CellValidating
        Try
            If dgTabla2.Columns(e.ColumnIndex).Name = "cant2" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    Else
                        BindingSource3.Item(BindingSource3.Position)(12) = Format(valor * CDbl(BindingSource3.Item(BindingSource3.Position)(11)), "0,0.00")
                        'If BindingSource3.Item(BindingSource3.Position)(12) > 0 Then
                        '    BindingSource3.Item(BindingSource3.Position)(9) = "OK"
                        'End If
                    End If
                End If
            End If

            If dgTabla2.Columns(e.ColumnIndex).Name = "prec2" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    Else
                        BindingSource3.Item(BindingSource3.Position)(12) = Format(CDbl(BindingSource3.Item(BindingSource3.Position)(10)) * valor, "0,0.00")
                    End If
                End If
            End If

        Catch f As Exception
            MessageBox.Show("Tipo de exception: " & f.Message, nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    Private Function ValidarCampos2() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        If ValidaNroMayorOigualCero(txtPre.Text) Then
            txtPre.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE LINEA DE INSUMO A PROCESAR...")
            Exit Sub
        End If

        If recuperarEstRen(BindingSource3.Item(BindingSource3.Position)(0)) = 1 Then '0=pendiente, 1=OK, 2=observado
            MessageBox.Show("Proceso denegado, esta en el estado de comprobado [OK]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End If

        If date2.Value < date1.Value Then
            MessageBox.Show("Proceso denegado, fecha seleccionada tiene que ser MAYOR a [" & date1.Value & "]", nomNegocio, Nothing, MessageBoxIcon.Error)
            date2.Focus()
            Exit Sub
        End If

        If ValidarCampos2() Then
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de ACTUALIZAR comprobante en el insumo: " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            Dim compro As Short
            If cbCompro1.SelectedIndex = 0 Then 'factura
                compro = 1
            End If
            If cbCompro1.SelectedIndex = 1 Then 'boleta
                compro = 2
            End If
            If cbCompro1.SelectedIndex = 2 Then 'honorarios
                compro = 3
            End If
            If cbCompro1.SelectedIndex = 3 Then 'otros
                compro = 4
            End If

            'actualizando TDetalleSol
            comandoUpdate1(txtNota.Text.Trim(), txtCan.Text, txtPre.Text, txtNro.Text.Trim(), date2.Value.Date, compro, BindingSource3.Item(BindingSource3.Position)(0))
            cmdUpdateTable1.Transaction = myTrans
            If cmdUpdateTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim position As Short = BindingSource3.Position

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

            visualizarDet()
            leerMontos()

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1  '
            End If

            txtNro.Focus()

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

    Dim cmdUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal insumo As String, ByVal can As Decimal, ByVal pre As Decimal, ByVal nro As String, ByVal fecha As String, ByVal comp As Short, ByVal codDetS As Integer)
        cmdUpdateTable1 = New SqlCommand
        cmdUpdateTable1.CommandType = CommandType.Text
        cmdUpdateTable1.CommandText = "update TDetSolCaja set obsSol=@obs,cant2=@can2,prec2=@pre2,nroOtros=@nro,fecha=@fec,compRen=@comp where codDetSol=@cod"
        cmdUpdateTable1.Connection = Cn
        cmdUpdateTable1.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = insumo
        cmdUpdateTable1.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable1.Parameters.Add("@pre2", SqlDbType.Decimal, 0).Value = pre
        cmdUpdateTable1.Parameters.Add("@nro", SqlDbType.VarChar, 30).Value = nro
        cmdUpdateTable1.Parameters.Add("@fec", SqlDbType.VarChar, 10).Value = fecha
        cmdUpdateTable1.Parameters.Add("@comp", SqlDbType.Int, 0).Value = comp
        cmdUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetS
    End Sub

    Private Sub txtNro_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNro.TextChanged
        Me.AcceptButton = Me.btnNuevo
    End Sub

    Private Sub btnAnula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnula.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE LINEA DE INSUMO A DESHACER...")
            Exit Sub
        End If

        If recuperarEstRen(BindingSource3.Item(BindingSource3.Position)(0)) = 1 Then '0=pendiente, 1=OK, 2=observado
            MessageBox.Show("Proceso denegado, esta en el estado de comprobado [OK]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(27) = 1 Then '1=Improvisado, 0=normal
            MessageBox.Show("Proceso denegado, ingreso de este insumo fue de [IMPREVISTO]" & Chr(13) & "Opción [Modificar o Eliminar]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de DESHACER registros de comprobante del insumo: " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'actualizando TDetalleSol
            comandoUpdate1("", BindingSource3.Item(BindingSource3.Position)(2), BindingSource3.Item(BindingSource3.Position)(4), "", "", BindingSource3.Item(BindingSource3.Position)(24), BindingSource3.Item(BindingSource3.Position)(0))
            cmdUpdateTable1.Transaction = myTrans
            If cmdUpdateTable1.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim position As Short = BindingSource3.Position

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

            visualizarDet()
            leerMontos()

            If position < BindingSource3.Count - 1 Then
                BindingSource3.Position = position + 1  '
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
            'Navigator1.BindingSource = BindingSource4
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
        txtCan1.ReadOnly = False
        cbUni.Enabled = True
        txtBuscar.ReadOnly = False
        txtPre1.ReadOnly = False
        txtNota1.ReadOnly = False
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
            .Columns(1).Width = 600
            .Columns(2).Width = 65
            .Columns(2).HeaderText = "Stock"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Unidad"
            .Columns(4).Width = 60
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

    Private Sub txtCan1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan1.KeyPress
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

    Private Sub txtPre1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre1.GotFocus, txtPre1.MouseClick
        txtPre.SelectAll()
    End Sub

    Private Sub txtPre1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPre1.KeyPress
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

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        vfOpc = 2 'Agrega requerimiento estructurado
        ejecutarRequerimiento()
    End Sub

    Dim vfOpc As Short
    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click
        vfOpc = 1  'Agrega requerimiento digitado
        ejecutarRequerimiento()
    End Sub

    Private Function recuperarEstado(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select estSol from TSolicitudCaja where codSC=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function ValidarCampos1() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan1.Text) Then
            txtCan1.errorProv()
            Return True
        End If

        If ValidarCantMayorCero(txtPre1.Text) Then
            txtPre1.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarCodPro(ByVal cod As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isNull(max(codApro),1) from TDetSolCaja where codSC=" & cod
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub ejecutarRequerimiento()
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Solicitud NO APERTURADA...")
            Exit Sub
        End If

        If recuperarEstado(BindingSource1.Item(BindingSource1.Position)(0)) = 4 Then '2=pROCESADO egreso caja
            MessageBox.Show("Proceso Denegado, Solicitud ya se Rindio Cuenta [OK]", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Me.Close()
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
                MessageBox.Show("Ya exíste requerimiento: " & BindingSource4.Item(BindingSource4.Position)(1), nomNegocio, Nothing, MessageBoxIcon.Information)
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
            comandoInsert2(txtCan1.Text, unidad, insumo, txtPre1.Text, recuperarCodPro(BindingSource1.Item(BindingSource1.Position)(0), myTrans), codMat, cbArea.SelectedValue, codTipM, compro)
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

            cbTip1.SelectedIndex = cbTipo.SelectedIndex
            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource3.Position = BindingSource3.Find("codDetSol", codDetSol)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            txtBuscar.Clear()
            txtPre1.Text = "0.0"
            txtNota1.Clear()
            txtCan1.Focus()
            txtCan1.SelectAll()
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
    Private Sub comandoInsert2(ByVal cant As Decimal, ByVal uni As String, ByVal insumo As String, ByVal precio As Decimal, ByVal codApro As Integer, ByVal codMat As Integer, ByVal codAreaM As Integer, ByVal codTipM As Integer, ByVal compro As Integer)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.StoredProcedure
        cmInserTable2.CommandText = "PA_InsertDetSolCaja"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@can1", SqlDbType.Decimal, 0).Value = 0
        cmInserTable2.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = cant
        cmInserTable2.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable2.Parameters.Add("@ins", SqlDbType.VarChar, 200).Value = insumo
        cmInserTable2.Parameters.Add("@ing", SqlDbType.Int, 0).Value = 1 '0=normal, 1=improvisado
        cmInserTable2.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = 0
        cmInserTable2.Parameters.Add("@pre2", SqlDbType.Decimal, 0).Value = precio
        cmInserTable2.Parameters.Add("@obsSol", SqlDbType.VarChar, 200).Value = txtNota1.Text
        cmInserTable2.Parameters.Add("@codApro", SqlDbType.Int, 0).Value = codApro
        cmInserTable2.Parameters.Add("@estDet", SqlDbType.Int, 0).Value = 1 'Aprobado
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
        cmInserTable2.Parameters.Add("@fec", SqlDbType.VarChar, 10).Value = ""
        cmInserTable2.Parameters.Add("@comp1", SqlDbType.Int, 0).Value = compro
        'configurando direction output = parametro de solo salida
        cmInserTable2.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable2.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Function recuperarEstRen(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select estRen from TDetSolCaja where codDetSol=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarEstRen1(ByVal cod As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select estRen from TDetSolCaja where codDetSol=" & cod
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub TSModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSModificar.Click
        If BindingSource3.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE REGISTROS A PROCESAR...")
            Exit Sub
        End If

        Dim vCom As Boolean = False
        For j As Short = 0 To BindingSource3.Count - 1
            If recuperarEstRen(BindingSource3.Item(j)(0)) = 0 Or recuperarEstRen(BindingSource3.Item(j)(0)) = 2 Then '0=pendiente 2=observado
                vCom = True
                Exit For
            End If
        Next

        If vCom = False Then
            MessageBox.Show("Proceso denegado en actualizar modificaciones, por estar en el estado de [OK - Rendido]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
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
                If recuperarEstRen1(BindingSource3.Item(j)(0), myTrans) = 0 Or recuperarEstRen1(BindingSource3.Item(j)(0), myTrans) = 2 Then '0=pendiente  2=Observado
                    'actualizando TDetalleSol
                    comandoUpdate15(BindingSource3.Item(j)(10), BindingSource3.Item(j)(11), BindingSource3.Item(j)(0))
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
    Private Sub comandoUpdate15(ByVal can As Decimal, ByVal pre As Decimal, ByVal codDetS As Integer)
        cmdUpdateTable15 = New SqlCommand
        cmdUpdateTable15.CommandType = CommandType.Text
        cmdUpdateTable15.CommandText = "update TDetSolCaja set cant2=@can2,prec2=@pre2 where codDetSol=@cod"
        cmdUpdateTable15.Connection = Cn
        cmdUpdateTable15.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable15.Parameters.Add("@pre2", SqlDbType.Decimal, 0).Value = pre
        cmdUpdateTable15.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetS
    End Sub

    Private Sub btnCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Dim crea As New crearMaterialForm
        crea.ShowDialog()
    End Sub

    Private Sub TSEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSEliminar.Click
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No Existe Insumo a Eliminar...")
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(27) = 0 Then '1=Improvisado, 0=normal
            MessageBox.Show("Proceso denegado, solo se puede eliminar insumos ingresados como [IMPREVISTO]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If recuperarEstRen(BindingSource3.Item(BindingSource3.Position)(0)) = 1 Then '0=pendiente, 1=OK, 2=observado
            MessageBox.Show("Proceso denegado, esta en el estado de comprobado [OK]", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Solicitud a imprimir...")
            Exit Sub
        End If

        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        'vParam1 = BindingSource6.Item(BindingSource6.Position)(18) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year
        Dim informe As New ReportViewerCajaCuentaForm
        informe.ShowDialog()
    End Sub
End Class
