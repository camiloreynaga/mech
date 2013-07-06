Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class registraOrdenCompraForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource
    Dim BindingSource6 As New BindingSource
    Dim BindingSource7 As New BindingSource

    Private Sub registraOrdenCompraForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
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

    Private Sub registraOrdenCompraForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1,cuentaBan from TIdentidad where estado=1 and idTipId=2" ' '2=proveedor
        crearDataAdapterTable(daTProvee, sele)

        sele = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTUbi, sele)

        sele = "select codPers,nombre+' '+apellido as nom from TPersonal where estado=1 order by nombre"
        crearDataAdapterTable(daTPers, sele)

        sele = "select codPag,forma from TFormaPago"
        crearDataAdapterTable(daTPago, sele)

        sele = "select codMon,moneda,simbolo from TMoneda"
        crearDataAdapterTable(daTMon, sele)

        sele = "select nroOrden,nroO,nro,codIde,fecOrden,igv,atiendeCom,celAti,plazoEnt,codMon,codPag,lugarEnt,transfe,codigo,codPers,obsFac,codCot,estado,hist,codPersO,calIGV,nroProf,idSol,codET from VOrdenComAper" 'order by nroO"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select codDetO,cant,unidad,descrip,precio,subTotal,nroOrden,codMat from VDetOrden where nroOrden=@nro"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        sele = "select codET,nombre,ruc,dir,fono,contacto from TEmpTransp order by nombre"
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTProvee.Fill(dsAlmacen, "TIdentidad")
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")
            daTPers.Fill(dsAlmacen, "TPersonal")
            daTPago.Fill(dsAlmacen, "TFormaPago")
            daTMon.Fill(dsAlmacen, "TMoneda")
            daTabla1.Fill(dsAlmacen, "VOrdenComAper")
            daDetDoc.Fill(dsAlmacen, "VDetOrden")
            daTabla2.Fill(dsAlmacen, "TEmpTransp")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TIdentidad"
            cbProv.DataSource = BindingSource1
            cbProv.DisplayMember = "razon"
            cbProv.ValueMember = "codIde"
            BindingSource1.Sort = "razon"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VLugarTrabajoLogin"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "TPersonal"
            cbPers.DataSource = BindingSource3
            cbPers.DisplayMember = "nom"
            cbPers.ValueMember = "codPers"

            cbPago.DataSource = dsAlmacen
            cbPago.DisplayMember = "TFormaPago.forma"
            cbPago.ValueMember = "codPag"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TMoneda"
            cbMoneda.DataSource = BindingSource0
            cbMoneda.DisplayMember = "moneda"
            cbMoneda.ValueMember = "codMon"

            BindingSource6.DataSource = dsAlmacen
            BindingSource6.DataMember = "VOrdenComAper"
            lbOrden.DataSource = BindingSource6
            lbOrden.DisplayMember = "nro"
            lbOrden.ValueMember = "nroOrden"
            BindingSource6.Sort = "nroO"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "VDetOrden"
            Navigator2.BindingSource = BindingSource5
            dgTabla2.DataSource = BindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            BindingSource7.DataSource = dsAlmacen
            BindingSource7.DataMember = "TEmpTransp"
            cbTrans.DataSource = BindingSource7
            cbTrans.DisplayMember = "nombre"
            cbTrans.ValueMember = "codET"

            configurarColorControl()

            vfVan1 = True
            leerRuc()
            recuperarUltimoNro(vSCodigo)

            rb1.Checked = True
            vfVan2 = True
            'enlazarText()  'en un inicio no enlazar pa NUEVO

            vfVan3 = True
            'visualizarDet()  'en un inicio no enlazar pa NUEVO

            cbBuscar.SelectedIndex = 0

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

    Private Sub registraOrdenCompraForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'colorearFila()
        colorear()
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Unid."
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 417
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 75
            .Columns(4).HeaderText = "Prec_Unit"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).Width = 75
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).Visible = False
            .Columns(7).Visible = False
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
        Label27.ForeColor = ForeColorLabel
        Label28.ForeColor = ForeColorLabel
        Label29.ForeColor = ForeColorLabel
        Label30.ForeColor = ForeColorLabel
        Label31.ForeColor = ForeColorLabel
        CheckBoxIGV.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        btnAperturar.ForeColor = ForeColorButtom
        btnCierra.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnElimina.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        btnProcesa.ForeColor = ForeColorButtom
        btnCot.ForeColor = ForeColorButtom
        btnSol.ForeColor = ForeColorButtom
        btnCrear.ForeColor = ForeColorButtom
    End Sub

    Private Sub recuperarUltimoNro(ByVal cod As String)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroO),0)+1 from TOrdenCompra"
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

    Dim vfVan1 As Boolean = False
    Private Sub leerRuc()
        If BindingSource1.Position <> -1 Then
            If vfVan1 Then
                BindingSource1.Position = BindingSource1.Find("codIde", cbProv.SelectedValue)
                txtRuc.Text = BindingSource1.Item(BindingSource1.Position)(2)
                txtFono.Text = BindingSource1.Item(BindingSource1.Position)(9)
                txtEma.Text = BindingSource1.Item(BindingSource1.Position)(7)
                txtAte.Text = BindingSource1.Item(BindingSource1.Position)(8)
                txtCel.Text = BindingSource1.Item(BindingSource1.Position)(6)
                txtTran.Text = BindingSource1.Item(BindingSource1.Position)(10)
            End If
        End If
    End Sub

    Private Sub leerTransp()
        If BindingSource7.Position <> -1 Then
            If vfVan1 Then
                txtRuc1.Text = BindingSource7.Item(cbTrans.SelectedIndex)(2)
                txtFono2.Text = BindingSource7.Item(cbTrans.SelectedIndex)(4)
                txtDir.Text = BindingSource7.Item(cbTrans.SelectedIndex)(3)
                txtCont.Text = BindingSource7.Item(cbTrans.SelectedIndex)(5)
            End If
        End If
    End Sub

    Private Sub cbProv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProv.SelectedIndexChanged
        leerRuc()
    End Sub

    Private Sub cbTrans_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTrans.SelectedIndexChanged
        leerTransp()
    End Sub

    Private Sub txtRuc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRuc.GotFocus, txtRuc.MouseClick
        txtRuc.SelectAll()
    End Sub

    Private Sub cbObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObra.SelectedIndexChanged
        txtLug.Text = BindingSource2.Item(cbObra.SelectedIndex)(2)
    End Sub

    Private Sub lbOrden_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbOrden.SelectedValueChanged
        enlazarText()
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub enlazarText()
        If vfVan2 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource6.Count = 0 Then
                'desEnlazarText()
            Else
                cbProv.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(3)
                txtObsFac.Text = BindingSource6.Item(lbOrden.SelectedIndex)(15)
                date1.Value = BindingSource6.Item(lbOrden.SelectedIndex)(4)
                txtAte.Text = BindingSource6.Item(lbOrden.SelectedIndex)(6)
                txtCel.Text = BindingSource6.Item(lbOrden.SelectedIndex)(7)
                cbObra.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(13)
                txtLug.Text = BindingSource6.Item(lbOrden.SelectedIndex)(11)
                cbPers.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(19)
                txtTran.Text = BindingSource6.Item(lbOrden.SelectedIndex)(12)
                txtPro.Text = BindingSource6.Item(lbOrden.SelectedIndex)(21)
                txtPla.Text = BindingSource6.Item(lbOrden.SelectedIndex)(8)
                cbPago.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(10)
                cbMoneda.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(9)
                txtRem.Text = recuperarCampo("nombre+' '+apellido", BindingSource6.Item(lbOrden.SelectedIndex)(14))
                txtFono1.Text = recuperarCampo("fono", BindingSource6.Item(lbOrden.SelectedIndex)(14))
                txtEma1.Text = recuperarCampo("email", BindingSource6.Item(lbOrden.SelectedIndex)(14))

                cbTrans.SelectedValue = BindingSource6.Item(lbOrden.SelectedIndex)(23)

                If BindingSource6.Item(lbOrden.SelectedIndex)(16) > 0 Then 'con cotizacion
                    txtNroCot.Text = recuperarNroCot(BindingSource6.Item(lbOrden.SelectedIndex)(16))
                Else
                    txtNroCot.Clear()
                End If

                If BindingSource6.Item(lbOrden.SelectedIndex)(22) > 0 Then 'con solicitud
                    txtNroSol.Text = recuperarIdSol(BindingSource6.Item(lbOrden.SelectedIndex)(22))
                Else
                    txtNroSol.Clear()
                End If

                If BindingSource6.Item(lbOrden.SelectedIndex)(5) = 0 Then 'Sin IGV
                    CheckBoxIGV.Checked = True
                Else 'Con IGV
                    CheckBoxIGV.Checked = False
                    If BindingSource6.Item(lbOrden.SelectedIndex)(20) = 1 Then 'calculos igv
                        rb1.Checked = True
                    Else  'calculo 2
                        rb2.Checked = True
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If BindingSource6.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetOrden").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource6.Item(lbOrden.SelectedIndex)(0)
            daDetDoc.Fill(dsAlmacen, "VDetOrden")
            'colorearFila()
            colorear()
            calcularSubTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource5.Count - 1
            dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(4).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(5).Style.BackColor = Color.AliceBlue
        Next
    End Sub

    Private Sub cbMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoneda.SelectedIndexChanged
        calcularSubTotal()
    End Sub

    Dim vfIGV As Double = vSIGV
    Private Sub calcularSubTotal()
        If BindingSource5.Position = -1 Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        If rb1.Checked Then  'tipo 1
            txtTotal.Text = Format((dsAlmacen.Tables("VDetOrden").Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0.00")
            txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0.00")
        Else  'Tipo 2
            txtSub.Text = Format((dsAlmacen.Tables("VDetOrden").Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0.00")
            txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0.00")
        End If
        lblTotal.Text = "TOTAL  " & BindingSource0.Item(cbMoneda.SelectedIndex)(2) 'cbMoneda.Text.Trim()
        cambiarNroTotalLetra()
    End Sub

    Private Sub CheckBoxIGV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxIGV.CheckedChanged
        If CheckBoxIGV.Checked Then 'Sin IGV
            vfIGV = 0
            GroupBox1.Visible = False
        Else 'Con IGV
            vfIGV = vSIGV
            GroupBox1.Visible = True
        End If
        calcularSubTotal()
    End Sub

    Private Sub rb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb1.CheckedChanged
        calcularSubTotal()
    End Sub

    Private Sub rb2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb2.CheckedChanged
        calcularSubTotal()
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        If cbMoneda.SelectedValue = 30 Then    '30=Nuevos solesl
            'If BindingSource10.Item(BindingSource10.Position)(7) = 30 Then
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(txtTotal.Text) > 0 Then
            cALetra.Numero = Val(txtTotal.Text)
            txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
        End If
    End Sub

    Private Function recuperarCampo(ByVal campo As String, ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select " & campo & " from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnAperturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAperturar.Click
        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de aperturar Orden de Compra Nº " & txtNro.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        recuperarUltimoNro(vSCodigo)
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

            'TIdentidad
            comandoUpdate1()
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

            'TOrdenCompra
            comandoInsert(0, 0) '0=sin Cotizacion  sin solicitud
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim nroOrden As Integer = cmInserTable.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
            vfVan3 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenComAper").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenComAper")

            'visualizarDet()
            recuperarUltimoNro(vSCodigo)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource6.Position = BindingSource6.Find("nroOrden", nroOrden)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan2 = True
            vfVan3 = True
            enlazarText()
            visualizarDet()

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
    Private Sub comandoInsert(ByVal codCot As Integer, ByVal idSol As Integer)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertOrdenCompra"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@nroO", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@fecO", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmInserTable.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@codPag", SqlDbType.Int, 0).Value = cbPago.SelectedValue
        cmInserTable.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = 18
        cmInserTable.Parameters.Add("@calIGV", SqlDbType.Int, 0).Value = 1   'tipo calculo IGV normal
        cmInserTable.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        cmInserTable.Parameters.Add("@atiendeCom", SqlDbType.VarChar, 50).Value = txtAte.Text.Trim() '& " " & txtCel.Text.Trim()
        cmInserTable.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmInserTable.Parameters.Add("@plazoEnt", SqlDbType.VarChar, 40).Value = txtPla.Text.Trim()
        cmInserTable.Parameters.Add("@transfe", SqlDbType.VarChar, 100).Value = txtTran.Text.Trim()
        cmInserTable.Parameters.Add("@nroProf", SqlDbType.VarChar, 40).Value = txtPro.Text.Trim()
        cmInserTable.Parameters.Add("@obsFac", SqlDbType.VarChar, 200).Value = txtObsFac.Text.Trim()
        cmInserTable.Parameters.Add("@estado", SqlDbType.Int, 0).Value = 0 'abierto
        cmInserTable.Parameters.Add("@codCot", SqlDbType.Int, 0).Value = codCot
        cmInserTable.Parameters.Add("@idSol", SqlDbType.Int, 0).Value = idSol
        cmInserTable.Parameters.Add("@codPersO", SqlDbType.Int, 0).Value = cbPers.SelectedValue
        cmInserTable.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmInserTable.Parameters.Add("@lugar", SqlDbType.VarChar, 100).Value = txtLug.Text.Trim()
        cmInserTable.Parameters.Add("@codET", SqlDbType.Int, 0).Value = cbTrans.SelectedValue
        cmInserTable.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = "Creo " & Now.Date & " " & vPass & "-" & vSUsuario
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TIdentidad set celRpm=@cel,repres=@rep,cuentaBan=@cue where codIde=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmUpdateTable1.Parameters.Add("@rep", SqlDbType.VarChar, 60).Value = txtAte.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cue", SqlDbType.VarChar, 60).Value = txtTran.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = cbProv.SelectedValue
    End Sub

    Private Function recuperarUltimoDoc() As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroO) from TOrdenCompra"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleOrden where nroOrden=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDesOrden where nroOrden=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc() <> CInt(BindingSource6.Item(BindingSource6.Position)(1)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima ORDEN DE COMPRA ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Orden de Compra tiene registros en detalle orden...")
            Exit Sub
        End If

        If (recuperarCount1(BindingSource6.Item(BindingSource6.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Orden de Compra tiene enlazado Orden de Desembolso...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar orden de compra Nº " & lbOrden.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            'Tabla TOrdenCompra
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
            vfVan2 = False  'Enlazar Text
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenComAper").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenComAper")

            recuperarUltimoNro(vSCodigo)
            'BindingSource0.Position = BindingSource0.Find("codGruC", codGruC)  'NO ubicar por grupo borrado
            vfVan2 = True  'Enlazar Text
            vfVan3 = True
            enlazarText()
            visualizarDet()

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
        cmDeleteTable1.CommandText = "delete from TOrdenCompra where nroOrden=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe orden de compra a actualizar...")
            Exit Sub
        End If

        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            date1.Focus()
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de GUARDAR cambios a Orden de Compra Nº " & lbOrden.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            Dim campo As Integer = BindingSource6.Item(BindingSource6.Position)(1)

            'TIdentidad
            comandoUpdate1()
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

            'TOrdenCompra
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataSet 
            dsAlmacen.Tables("VOrdenComAper").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenComAper")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource6.Position = BindingSource6.Find("nroO", campo)

            vfVan2 = True  'Enlazar Text
            vfVan3 = True
            enlazarText()
            visualizarDet()

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

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2()
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TOrdenCompra set fecOrden=@fec,codIde=@codI,codPag=@codPa,igv=@igv,calIGV=@cal,codMon=@codM,atiendeCom=@ate,celAti=@cel,plazoEnt=@pla,transfe=@tra,nroProf=@nroProf,obsFac=@obs,codPersO=@codPersO,codigo=@cod,lugarEnt=@lug,hist=@hist,codET=@codET where nroOrden=@nro"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable2.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmUpdateTable2.Parameters.Add("@codPa", SqlDbType.Int, 0).Value = cbPago.SelectedValue
        If Not CheckBoxIGV.Checked Then  'Con IGV
            If rb1.Checked Then
                cmUpdateTable2.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
                cmUpdateTable2.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 1    '1=tipo 1, 2=tipo 2...
            Else
                cmUpdateTable2.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
                cmUpdateTable2.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 2    '1=tipo 1, 2=tipo 2...
            End If
        Else 'Sin IGV
            cmUpdateTable2.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = 0
            cmUpdateTable2.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 0    '1=tipo 1, 2=tipo 2, 0=boleta otros
        End If
        cmUpdateTable2.Parameters.Add("@codM", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        cmUpdateTable2.Parameters.Add("@ate", SqlDbType.VarChar, 50).Value = txtAte.Text.Trim()
        cmUpdateTable2.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmUpdateTable2.Parameters.Add("@pla", SqlDbType.VarChar, 40).Value = txtPla.Text.Trim()
        cmUpdateTable2.Parameters.Add("@tra", SqlDbType.VarChar, 100).Value = txtTran.Text.Trim()
        cmUpdateTable2.Parameters.Add("@nroProf", SqlDbType.VarChar, 40).Value = txtPro.Text.Trim()
        cmUpdateTable2.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObsFac.Text.Trim()
        cmUpdateTable2.Parameters.Add("@codPersO", SqlDbType.Int, 0).Value = cbPers.SelectedValue
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmUpdateTable2.Parameters.Add("@lug", SqlDbType.VarChar, 100).Value = txtLug.Text.Trim()
        cmUpdateTable2.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = BindingSource6.Item(BindingSource6.Position)(18) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable2.Parameters.Add("@codET", SqlDbType.Int, 0).Value = cbTrans.SelectedValue
        cmUpdateTable2.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource4.RemoveFilter()
        If vFClear1 Then
            dsAlmacen.Tables("VMaterialSele").Clear()
            daVMat.Fill(dsAlmacen, "VMaterialSele")
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,uniBase,preBase,tipoM,hist from VMaterialSele" 'material
            crearDataAdapterTable(daVMat, sele)
            daVMat.Fill(dsAlmacen, "VMaterialSele")

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VMaterialSele"
            'Navigator1.BindingSource = BindingSource4
            dgTabla1.DataSource = BindingSource4
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource4.Sort = "material"
            ModificarColumnasDGV1()

            vFClear1 = True
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        txtBuscar.Focus()
        txtBuscar.SelectAll()
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Width = 35
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 440
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Unid."
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Precio"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderText = "Tipo Insumo"
            .Columns(4).Width = 120
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 270
            '.Columns(5).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub txtPre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre.GotFocus, txtPre.MouseClick
        txtPre.SelectAll()
    End Sub

    Private Sub txtCan_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.GotFocus, txtCan.MouseClick
        txtCan.SelectAll()
    End Sub

    Private Sub txtCan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan.TextChanged
        Me.AcceptButton = Me.btnAgrega
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
            BindingSource4.Filter = campo & " like '" & txtBuscar.Text.Trim() & "%'"
        Else
            If Not IsNumeric(txtBuscar.Text.Trim()) Then
                StatusBarClass.messageBarraEstado(" INGRESE DATO NUMERICO...")
                txtBuscar.SelectAll()
                Exit Sub
            End If
            BindingSource4.Filter = campo & "=" & txtBuscar.Text.Trim()
        End If
        If BindingSource4.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnAgrega
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

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        'If ValidaNroMayorOigualCero(txtPre.Text) Then
        If ValidarCantMayorCero(txtPre.Text) Then
            txtPre.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        btnAgrega.PerformClick()
    End Sub

    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a agregar...")
            Exit Sub
        End If

        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Orden de Compra NO APERTURADA...")
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If BindingSource5.Find("codMat", BindingSource4.Item(BindingSource4.Position)(0)) >= 0 Then
            MessageBox.Show("Ya exíste insumo: " & BindingSource4.Item(BindingSource4.Position)(1), nomNegocio, Nothing, MessageBoxIcon.Information)
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

            'TOrdenCompra
            comandoUpdate25()
            cmUpdateTable25.Transaction = myTrans
            If cmUpdateTable25.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            'TMaterial
            comandoUpdate26(txtPre.Text, BindingSource4.Item(BindingSource4.Position)(0))
            cmUpdateTable26.Transaction = myTrans
            If cmUpdateTable26.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            'TDetalleOrden
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
            Dim codDetO As Integer = cmInserTable1.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource5.Position = BindingSource5.Find("codDetO", codDetO)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

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

    Dim cmUpdateTable25 As SqlCommand
    Private Sub comandoUpdate25()
        cmUpdateTable25 = New SqlCommand
        cmUpdateTable25.CommandType = CommandType.Text
        cmUpdateTable25.CommandText = "update TOrdenCompra set igv=@igv,calIGV=@cal,codMon=@codM where nroOrden=@nro"
        cmUpdateTable25.Connection = Cn
        If Not CheckBoxIGV.Checked Then  'Con IGV
            If rb1.Checked Then
                cmUpdateTable25.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
                cmUpdateTable25.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 1    '1=tipo 1, 2=tipo 2...
            Else
                cmUpdateTable25.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
                cmUpdateTable25.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 2    '1=tipo 1, 2=tipo 2...
            End If
        Else 'Sin IGV
            cmUpdateTable25.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = 0
            cmUpdateTable25.Parameters.Add("@cal", SqlDbType.Int, 0).Value = 0    '1=tipo 1, 2=tipo 2, 0=boleta otros
        End If
        cmUpdateTable25.Parameters.Add("@codM", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        cmUpdateTable25.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Dim cmUpdateTable26 As SqlCommand
    Private Sub comandoUpdate26(ByVal prec As Decimal, ByVal codMat As Integer)
        cmUpdateTable26 = New SqlCommand
        cmUpdateTable26.CommandType = CommandType.Text
        cmUpdateTable26.CommandText = "update TMaterial set preBase=@pre where codMat=@cod"
        cmUpdateTable26.Connection = Cn
        cmUpdateTable26.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = prec
        cmUpdateTable26.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codMat
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertDetalleOrden"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = txtCan.Text
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = BindingSource4.Item(BindingSource4.Position)(2)
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = BindingSource4.Item(BindingSource4.Position)(1)
        cmInserTable1.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = txtPre.Text
        cmInserTable1.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = CDbl(txtCan.Text) * CDbl(txtPre.Text)
        cmInserTable1.Parameters.Add("@codM", SqlDbType.Int, 0).Value = BindingSource4.Item(BindingSource4.Position)(0)
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub TSModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSModificar.Click
        If BindingSource5.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO EXISTE REGISTROS A PROCESAR...")
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

            'TOrdenCompra
            comandoUpdate25()
            cmUpdateTable25.Transaction = myTrans
            If cmUpdateTable25.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            For j As Short = 0 To BindingSource5.Count - 1
                'TMaterial
                comandoUpdate26(BindingSource5.Item(j)(4), BindingSource5.Item(j)(7))
                cmUpdateTable26.Transaction = myTrans
                If cmUpdateTable26.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'actualizando TDetalleOrden
                comandoUpdate3(BindingSource5.Item(j)(1), BindingSource5.Item(j)(4), BindingSource5.Item(j)(5), BindingSource5.Item(j)(0))
                cmdUpdateTable3.Transaction = myTrans
                If cmdUpdateTable3.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
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

    Dim cmdUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal can As Decimal, ByVal precio As Decimal, ByVal subTotal As Decimal, ByVal codDetO As Integer)
        cmdUpdateTable3 = New SqlCommand
        cmdUpdateTable3.CommandType = CommandType.Text
        cmdUpdateTable3.CommandText = "update TDetalleOrden set cant=@can,precio=@pre,subTotal=@sub where codDetO=@cod"
        cmdUpdateTable3.Connection = Cn
        cmdUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable3.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = precio
        cmdUpdateTable3.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = subTotal
        cmdUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetO
    End Sub

    'Evento  Se produce cuando la celda termina de validar
    Private Sub dgTabla2_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla2.CellValidated
        dgTabla2.Rows(e.RowIndex).ErrorText = Nothing   'Borrar el error
    End Sub

    Private Sub dgTabla2_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgTabla2.CellValidating
        Try
            If dgTabla2.Columns(e.ColumnIndex).Name = "cant" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue 'cant
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    Else
                        BindingSource5.Item(BindingSource5.Position)(5) = Format(CDbl(BindingSource5.Item(BindingSource5.Position)(4)) * valor, "0.00")
                    End If
                End If
            End If

            If dgTabla2.Columns(e.ColumnIndex).Name = "precio" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue 'precio
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    Else
                        BindingSource5.Item(BindingSource5.Position)(5) = Format(CDbl(BindingSource5.Item(BindingSource5.Position)(1)) * valor, "0.00")
                    End If
                End If
            End If

            If dgTabla2.Columns(e.ColumnIndex).Name = "subTotal" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue 'cant
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    End If
                End If
            End If
        Catch f As Exception
            MessageBox.Show("Tipo de exception: " & f.Message, nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    'Eliminar linea insumo
    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe linea de insumo a eliminar...")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de eliminar: " & BindingSource5.Item(BindingSource5.Position)(3) & "  Si elimina no podra deshacer...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDetalleOrden
            comandoDelete3()
            cmDeleteTable3.Transaction = myTrans
            If cmDeleteTable3.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
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

    Dim cmDeleteTable3 As SqlCommand
    Private Sub comandoDelete3()
        cmDeleteTable3 = New SqlCommand
        cmDeleteTable3.CommandType = CommandType.Text
        cmDeleteTable3.CommandText = "delete from TDetalleOrden where codDetO=@cod"
        cmDeleteTable3.Connection = Cn
        cmDeleteTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Private Sub btnCierraGru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCierra.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe orden de compra a CERRAR...")
            Exit Sub
        End If

        If recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) = 0 Then
            MessageBox.Show("PROCESO DENEGADO, orden de compra Nº" & BindingSource6.Item(BindingSource6.Position)(2) & " NO tiene registros en detalle orden...", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de CERRAR orden de compra Nº " & BindingSource6.Item(BindingSource6.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            Dim campo As Integer = BindingSource6.Item(BindingSource6.Position)(1)

            'TOrdenCompra
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
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
            vfVan3 = False  'Enlazar DetalleOrden TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenComAper").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenComAper")

            recuperarUltimoNro(vSCodigo)
            'BindingSource0.Position = BindingSource0.Find("codGruC", codGruC)  'NO ubicar por grupo borrado
            vfVan2 = True  'Enlazar Text
            vfVan3 = True
            enlazarText()
            dsAlmacen.Tables("VDetOrden").Clear()
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Orden de Compra fúe cerrado con exito...")
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

    Dim cmUpdateTable23 As SqlCommand
    Private Sub comandoUpdate23(ByVal estado As Short)
        cmUpdateTable23 = New SqlCommand
        cmUpdateTable23.CommandType = CommandType.Text
        cmUpdateTable23.CommandText = "update TOrdenCompra set estado=@est where nroOrden=@nro"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        If BindingSource4.Position <> -1 Then
            txtPre.Text = BindingSource4.Item(BindingSource4.Position)(3)
        End If
    End Sub

    Private Sub btnCot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCot.Click
        'vCod1 = lbOrden.Text.Trim()
        'vNroOrden = lbOrden.SelectedValue
        vCod2 = "0"  'retornar el codCot

        Dim jala As New jalarCotizacionForm
        jala.ShowDialog()

        If CInt(vCod2) = 0 Then 'se cancelo
            'MsgBox("SE CANCELO")
            Exit Sub
        Else
        End If

        Dim wait As New waitForm
        Me.Cursor = Cursors.WaitCursor
        wait.Show()

        Dim nroOrden As Integer = CInt(vCod2)

        StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
        vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
        vfVan3 = False

        'Actualizando el dataTable
        dsAlmacen.Tables("VOrdenComAper").Clear()
        daTabla1.Fill(dsAlmacen, "VOrdenComAper")

        'visualizarDet()
        recuperarUltimoNro(vSCodigo)

        'Buscando por nombre de campo y luego pocisionarlo con el indice
        BindingSource6.Position = BindingSource6.Find("nroOrden", nroOrden)

        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

        vfVan2 = True
        vfVan3 = True
        enlazarText()
        visualizarDet()

        wait.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function recuperarNroCot(ByVal codCot As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select case when nroCot<100 then '000'+ltrim(str(nroCot)) when nroCot>=100 and nroCot<1000 then '00'+ltrim(str(nroCot)) when nroCot>=1000 and nroCot<10000 then '0'+ltrim(str(nroCot)) else ltrim(str(nroCot)) end + ' - ' + ltrim(str(year(fecCot))) from TCotizacion where codCot=" & codCot
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarIdSol(ByVal idSol As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select case when nroS<100 then '000'+ltrim(str(nroS)) when nroS>=100 and nroS<1000 then '00'+ltrim(str(nroS)) else '0'+ltrim(str(nroS)) end + ' - ' + ltrim(str(year(fecSol))) from TSolicitud where idSol=" & idSol
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        vCodDoc = BindingSource6.Item(BindingSource6.Position)(0)
        vParam1 = BindingSource6.Item(BindingSource6.Position)(2) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year
        vParam2 = txtLetraTotal.Text.Trim()
        vParam3 = txtSub.Text.Trim()
        vParam4 = txtIGV.Text.Trim()
        vParam5 = txtTotal.Text.Trim()
        Dim informe As New ReportViewerOrdenCompraForm
        informe.ShowDialog()
    End Sub

    Private Sub btnSol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSol.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Cotización...")
            Exit Sub
        End If

        vCod3 = BindingSource6.Item(BindingSource6.Position)(13) 'Codigo de obra
        vCod1 = lbOrden.Text.Trim()
        vNroOrden = lbOrden.SelectedValue
        vCod2 = "0"  'retornar el idSol de solicitud

        Dim jala As New jalarSolicitud1Form
        jala.ShowDialog()

        visualizarDet()

        If CInt(vCod2) > 0 Then 'con solicitud
            txtNroSol.Text = recuperarIdSol(vCod2)
        Else

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
End Class
