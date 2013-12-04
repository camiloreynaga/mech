Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class registraDocVentaEmpForm
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource
    Dim BindingSource6 As New BindingSource

    Private Sub registraDocVentaEmpForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub registraDocVentaEmpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSerS,serie,iniNroDoc,finNroDoc from TSerieSede where estado=1 and codTipDE=70"  '70=Factura
        crearDataAdapterTable(daVSerie, sele)

        sele = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1,cuentaBan,cuentaDet from TIdentidad where estado=1 and idTipId=1" ' '2=proveedor
        crearDataAdapterTable(daTCli, sele)

        sele = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTUbi, sele)

        sele = "select idSesM,mes,ano,estado,idMes from VMesAbierto where codigo=@cod order by idSesM"
        crearDataAdapterTable(daTSesion, sele)
        daTSesion.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codMon,moneda,simbolo from TMoneda"
        crearDataAdapterTable(daTMon, sele)

        sele = "select codDocV,serie+' - '+nro as nroD,nro,codIde,fecDoc,fecCan,igv,calIGV,idSesM,codSerS,codMon,camD,obs,estado,hist,codigo,nroDoc from VDocVenta where codSerS=@codSer"  'order by codDocV"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codSer", SqlDbType.Int, 0).Value = 0

        sele = "select codDV,cant,detalle,unidad,linea,preUni,subtotal,codDocV from VDetalleVenta where codDocV=@nro order by codDV"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daVSerie.Fill(dsAlmacen, "TSerieSede")
            daTCli.Fill(dsAlmacen, "TIdentidad")
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")
            daTSesion.Fill(dsAlmacen, "VMesAbierto")
            daTMon.Fill(dsAlmacen, "TMoneda")
            daTabla1.Fill(dsAlmacen, "VDocVenta")
            daDetDoc.Fill(dsAlmacen, "VDetalleVenta")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TSerieSede"
            cbSerie.DataSource = BindingSource1
            cbSerie.DisplayMember = "serie"
            cbSerie.ValueMember = "codSerS"
            BindingSource1.Sort = "codSerS"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TIdentidad"
            cbCli.DataSource = BindingSource2
            cbCli.DisplayMember = "razon"
            cbCli.ValueMember = "codIde"
            BindingSource2.Sort = "razon"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VLugarTrabajoLogin"
            cbObra.DataSource = BindingSource3
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VMesAbierto"
            cbMes.DataSource = BindingSource4
            cbMes.DisplayMember = "mes"
            cbMes.ValueMember = "idSesM"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TMoneda"
            cbMoneda.DataSource = BindingSource0
            cbMoneda.DisplayMember = "moneda"
            cbMoneda.ValueMember = "codMon"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "VDocVenta"
            lbDoc.DataSource = BindingSource5
            lbDoc.DisplayMember = "nroD"
            lbDoc.ValueMember = "codDocV"
            BindingSource5.Sort = "codDocV"

            BindingSource6.DataSource = dsAlmacen
            BindingSource6.DataMember = "VDetalleVenta"
            BindingSource6.Sort = "codDV"

            configurarColorControl()

            txtRuc.DataBindings.Add("Text", BindingSource2, "ruc")
            txtDir.DataBindings.Add("Text", BindingSource2, "dir")

            rb1.Checked = True
            BindingSource4.MoveLast() 'Moviendo al ultimo mes periodo

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

    Private Sub registraDocVentaEmpForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vFVan = True
        parametrosSerieDoc()
        vfVan2 = True
        visualizarDocVenta()
        enlazarText()
        vfVan3 = True
        visualizarDet()
        vfVan4 = True   'lbDoc_SelectedValueChanged
    End Sub

    Private Sub cbSerie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSerie.SelectedIndexChanged
        vfVan4 = False 'lbDoc_SelectedValueChanged
        parametrosSerieDoc()
        visualizarDocVenta()
        enlazarText()
        visualizarDet()
        vfVan4 = True  'lbDoc_SelectedValueChanged
    End Sub

    Dim vfVan4 As Boolean = False
    Private Sub lbDoc_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDoc.SelectedValueChanged
        If vfVan4 Then
            enlazarText()
            visualizarDet()
        End If
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub visualizarDocVenta()
        If vfVan2 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDocVenta").Clear()
            daTabla1.SelectCommand.Parameters("@codSer").Value = cbSerie.SelectedValue
            daTabla1.Fill(dsAlmacen, "VDocVenta")
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub enlazarText()
        If vfVan2 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource5.Count = 0 Then
                'desEnlazarText()
            Else
                cbMes.SelectedValue = BindingSource5.Item(lbDoc.SelectedIndex)(8)
                date1.Value = BindingSource5.Item(lbDoc.SelectedIndex)(4)
                cbCli.SelectedValue = BindingSource5.Item(lbDoc.SelectedIndex)(3)

                txtDato.Text = BindingSource5.Item(lbDoc.SelectedIndex)(12)
                txtHist.Text = BindingSource5.Item(lbDoc.SelectedIndex)(14)

                cbObra.SelectedValue = BindingSource5.Item(lbDoc.SelectedIndex)(15)
                cbMoneda.SelectedValue = BindingSource5.Item(lbDoc.SelectedIndex)(10)

                If BindingSource5.Item(lbDoc.SelectedIndex)(6) = 0 Then 'Sin IGV
                    CheckBoxIGV.Checked = True
                Else 'Con IGV
                    CheckBoxIGV.Checked = False
                    If BindingSource5.Item(lbDoc.SelectedIndex)(7) = 1 Then 'calculos igv
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
            If BindingSource5.Position = -1 Then
                dsAlmacen.Tables("VDetalleVenta").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetalleVenta").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource5.Item(lbDoc.SelectedIndex)(0)
            daDetDoc.Fill(dsAlmacen, "VDetalleVenta")
            Me.Cursor = Cursors.Default

            txtCan1.Text = Format(BindingSource6.Item(0)(1), "0,0.00")
            txtDet1.Text = BindingSource6.Item(0)(2)
            txtPre1.Text = Format(BindingSource6.Item(0)(5), "0,0.00")
            txtTot1.Text = Format(BindingSource6.Item(0)(6), "0,0.00")
            txtDes1.Text = BindingSource6.Item(0)(4)

            txtCan2.Text = Format(BindingSource6.Item(1)(1), "0,0.00")
            txtDet2.Text = BindingSource6.Item(1)(2)
            txtPre2.Text = Format(BindingSource6.Item(1)(5), "0,0.00")
            txtTot2.Text = Format(BindingSource6.Item(1)(6), "0,0.00")
            txtDes2.Text = BindingSource6.Item(1)(4)
        End If
    End Sub

    Private Sub txtBuscar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus, txtBuscar.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        BindingSource2.Filter = "razon like '" & txtBuscar.Text.Trim() & "%'"
        If cbCli.SelectedIndex <> -1 Then
            cbCli.SelectedIndex = 0
        End If
        'lbMenu.DroppedDown = True 'Se despliega el comboBox
    End Sub

    Private Sub cbCli_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCli.GotFocus
        cbCli.DroppedDown = True 'Se despliega el comboBox
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
        lblTotal.ForeColor = ForeColorLabel
        GB3.ForeColor = ForeColorLabel
        CheckBox1.ForeColor = ForeColorLabel
        CheckBoxIGV.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
        btnCierra.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
    End Sub

    Dim vFVan As Boolean = False
    Private Sub parametrosSerieDoc()
        If vFVan Then
            vIniNroDoc = recuperarVariosTSerie(cbSerie.SelectedValue, "iniNroDoc")
            vFinNroDoc = recuperarVariosTSerie(cbSerie.SelectedValue, "finNroDoc")

            recuperarUltimoCodigo(cbSerie.SelectedValue)
        End If
    End Sub

    Private Sub recuperarUltimoCodigo(ByVal codSer As Integer)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        Dim maxCodigo As Object
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select max(nroDoc) from TDocVenta where codSerS=" & codSer
        cmdMaxCodigo.Connection = Cn
        maxCodigo = cmdMaxCodigo.ExecuteScalar
        asignarCodigo(maxCodigo)
    End Sub

    Private Sub asignarCodigo(ByVal max As Object)
        If IsNumeric(max) Then
            max = max + 1
            Select Case CInt(max)
                Case Is < 10
                    txtNro.Text = "0000" & max
                Case 10 To 99
                    txtNro.Text = "000" & max
                Case 100 To 999
                    txtNro.Text = "00" & max
                Case 1000 To 9999
                    txtNro.Text = "0" & max
                Case Is > 9999
                    txtNro.Text = max
            End Select
        Else
            'en caso de k no haya registros en la tabla su valor es Null
            txtNro.Text = vIniNroDoc
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        If vfNuevo1 = "guardar" Then
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

        btnAnula.Enabled = False
        btnAnula.FlatStyle = FlatStyle.Flat
        btnCierra.Enabled = False
        btnCierra.FlatStyle = FlatStyle.Flat
        btnImprimir.Enabled = False
        btnImprimir.FlatStyle = FlatStyle.Flat
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

        btnAnula.Enabled = True
        btnAnula.FlatStyle = FlatStyle.Standard
        btnCierra.Enabled = True
        btnCierra.FlatStyle = FlatStyle.Standard
        btnImprimir.Enabled = True
        btnImprimir.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub activarControles()
        cbMes.Enabled = True
        date1.Enabled = True
        txtBuscar.ReadOnly = False
        cbCli.Enabled = True
        txtDato.ReadOnly = False
        cbObra.Enabled = True
        cbMoneda.Enabled = True
        txtCan1.ReadOnly = False
        txtDet1.ReadOnly = False
        txtPre1.ReadOnly = False
        txtDes1.ReadOnly = False
        txtCan2.ReadOnly = False
        txtDet2.ReadOnly = False
        txtPre2.ReadOnly = False
        txtDes2.ReadOnly = False
    End Sub

    Private Sub desactivarControles()
        cbMes.Enabled = False
        date1.Enabled = False
        txtBuscar.ReadOnly = True
        cbCli.Enabled = False
        txtDato.ReadOnly = True
        cbObra.Enabled = False
        cbMoneda.Enabled = False
        txtCan1.ReadOnly = True
        txtDet1.ReadOnly = True
        txtPre1.ReadOnly = True
        txtDes1.ReadOnly = True
        txtCan2.ReadOnly = True
        txtDet2.ReadOnly = True
        txtPre2.ReadOnly = True
        txtDes2.ReadOnly = True
    End Sub

    Private Sub limpiarText()
        date1.Value = Now.Date
        txtCan1.Text = "1"
        txtDet1.Clear()
        txtPre1.Text = "0"
        txtTot1.Text = "0"
        txtDes1.Clear()
        txtCan2.Text = "0"
        txtDet2.Clear()
        txtPre2.Text = "0"
        txtTot2.Text = "0"
        txtDes2.Clear()
        txtDato.Clear()
        txtHist.Clear()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan1.Text) Then
            txtCan1.errorProv()
            Return True
        End If

        If validaCampoVacioMinCaracNoNumer(txtDet1.Text.Trim, 3) Then
            MessageBox.Show("Registre detalle valido...", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            txtDet1.Focus()
            Return True
        End If

        If ValidarCantMayorCero(txtPre1.Text) Then
            txtPre1.errorProv()
            Return True
        End If

        If ValidaNroMayorOigualCero(txtCan2.Text) Then
            txtCan2.errorProv()
            Return True
        End If

        If Not IsNumeric(txtPre2.Text) Then
            txtPre2.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            activarControles()
            limpiarText()
            txtBuscar.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo

            BindingSource4.MoveLast() 'Moviendo al ultimo mes periodo
        Else   ' guardar
            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If BindingSource2.Position = -1 Then
                MessageBox.Show("selecione Cliente valido...", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtBuscar.Focus()
                Exit Sub
            End If

            If ValidarCampos() Then
                Exit Sub
            End If

            If txtTot2.Text.Trim() <> 0 Then
                If validaCampoVacioMinCaracNoNumer(txtDet2.Text.Trim, 3) Then
                    MessageBox.Show("Registre detalle valido...", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                    txtDet2.Focus()
                    Exit Sub
                End If
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de aperturar FACTURA Nº " & cbSerie.Text & " - " & txtNro.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                Exit Sub
            End If

            parametrosSerieDoc()

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            Me.Cursor = Cursors.WaitCursor
            wait.Show()
            'estableciendo una transaccion
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
                Me.Refresh()

                'TDocVenta
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
                Dim codDocV As Integer = cmInserTable.Parameters("@Identity").Value

                'TDetalleVenta
                comandoInsert1(txtCan1.Text, "", txtDet1.Text.Trim(), txtDes1.Text, txtPre1.Text, codDocV)
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                Dim codDV As Integer = cmInserTable1.Parameters("@Identity").Value

                'TDetalleVenta
                comandoInsert1(txtCan2.Text, "", txtDet2.Text.Trim(), txtDes2.Text, txtPre2.Text, codDocV)
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                codDV = cmInserTable1.Parameters("@Identity").Value

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan3 = False 'Detalle

                'Actualizando el dataTable
                parametrosSerieDoc()
                visualizarDocVenta()

                BindingSource2.RemoveFilter()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource5.Position = BindingSource5.Find("codDocV", codDocV)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan3 = True
                btnCancelar.PerformClick()

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
        Me.btnNuevo.Text = "Nuevo"
        vfModificar1 = "modificar"
        Me.btnModificar.Text = "Modificar"
        activarControles1()
        desactivarControles()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
        enlazarText()
        visualizarDet()
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertDocVenta"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@ser", SqlDbType.VarChar, 5).Value = cbSerie.Text.Trim()
        cmInserTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@fecD", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@fecC", SqlDbType.VarChar, 10).Value = ""
        cmInserTable.Parameters.Add("@idSesM", SqlDbType.Int, 0).Value = cbMes.SelectedValue
        cmInserTable.Parameters.Add("@codSerS", SqlDbType.Int, 0).Value = cbSerie.SelectedValue
        cmInserTable.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = cbCli.SelectedValue
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0 'abierto
        If rb1.Checked Then
            cmInserTable.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
            cmInserTable.Parameters.Add("@calIGV", SqlDbType.Int, 0).Value = 1   'tipo calculo IGV normal
        Else
            cmInserTable.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = vfIGV
            cmInserTable.Parameters.Add("@calIGV", SqlDbType.Int, 0).Value = 2    '1=tipo 1, 2=tipo 2...
        End If
        cmInserTable.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        cmInserTable.Parameters.Add("@cam", SqlDbType.Decimal, 0).Value = 0
        cmInserTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtDato.Text.Trim()
        cmInserTable.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = "Creo " & Now.Date & " " & vPass & "-" & vSUsuario
        cmInserTable.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal cant As Decimal, ByVal uni As String, ByVal detalle As String, ByVal linea As String, ByVal precio As Decimal, ByVal codDocV As Integer)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertDetalleVenta"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable1.Parameters.Add("@det", SqlDbType.VarChar, 100).Value = detalle
        cmInserTable1.Parameters.Add("@lin", SqlDbType.VarChar, 200).Value = linea
        cmInserTable1.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = precio
        cmInserTable1.Parameters.Add("@codD", SqlDbType.Int, 0).Value = codDocV
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Function recuperarUltimoDoc(ByVal codSer As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroDoc) from TDocVenta where codSerS=" & codSer
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc(BindingSource5.Item(BindingSource5.Position)(9)) <> CInt(BindingSource5.Item(BindingSource5.Position)(16)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser el ultima DOC. VENTA ingresado...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar Doc. de Venta Nº " & BindingSource5.Item(BindingSource5.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'Tabla TDetalleVenta
            comandoDelete0()
            cmDeleteTable0.Transaction = myTrans
            If cmDeleteTable0.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'Tabla TDocVenta
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
            vfVan3 = False

            'Actualizando el dataTable
            parametrosSerieDoc()
            visualizarDocVenta()

            BindingSource2.RemoveFilter()

            enlazarText()

            vfVan3 = True
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

    Dim cmDeleteTable0 As SqlCommand
    Private Sub comandoDelete0()
        cmDeleteTable0 = New SqlCommand
        cmDeleteTable0.CommandType = CommandType.Text
        cmDeleteTable0.CommandText = "delete from TDetalleVenta where codDocV=@cod"
        cmDeleteTable0.Connection = Cn
        cmDeleteTable0.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TDocVenta where codDocV=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Private Sub txtCan1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan1.GotFocus, txtCan1.MouseClick
        txtCan1.SelectAll()
    End Sub

    Private Sub txtCan2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan2.GotFocus, txtCan2.MouseClick
        txtCan2.SelectAll()
    End Sub

    Private Sub txtPre1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre1.GotFocus, txtPre1.MouseClick
        txtPre1.SelectAll()
    End Sub

    Private Sub txtPre2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPre2.GotFocus, txtPre2.MouseClick
        txtPre2.SelectAll()
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

    Private Sub txtCan2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan2.KeyPress
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

    Private Sub txtPre2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPre2.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then  'te deja escribir digitos
            e.Handled = False
        Else
            If e.KeyChar.IsControl(e.KeyChar) Then  'te deja escribir enter, backSpace (controles)
                e.Handled = False
            Else
                If e.KeyChar = "." Or e.KeyChar = "-" Then   'te deja escribir punto o negativo
                    e.Handled = False
                Else    'lo demas no te deja escribir ASNOOOO
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    Private Sub txtCan1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan1.TextChanged, txtPre1.TextChanged
        If Not IsNumeric(txtCan1.Text) Then
            txtTot1.Text = "0"
            Exit Sub
        End If
        If Not IsNumeric(txtPre1.Text) Then
            txtTot1.Text = "0"
            Exit Sub
        End If

        txtTot1.Text = Format((CDbl(txtCan1.Text) * CDbl(txtPre1.Text)), "0,0.00")

        calcularSubTotal()
    End Sub

    Private Sub txtCan2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan2.TextChanged, txtPre2.TextChanged
        If Not IsNumeric(txtCan2.Text) Then
            txtTot2.Text = "0"
            Exit Sub
        End If
        If Not IsNumeric(txtPre2.Text) Then
            txtTot2.Text = "0"
            Exit Sub
        End If

        txtTot2.Text = Format((CDbl(txtCan2.Text) * CDbl(txtPre2.Text)), "0,0.00")

        calcularSubTotal()
    End Sub

    Dim vfIGV As Double = vSIGV
    Private Sub calcularSubTotal()
        If (Not IsNumeric(txtTot1.Text)) Or (Not IsNumeric(txtTot2.Text)) Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        If rb1.Checked Then  'tipo 1
            txtTotal.Text = Format((CDbl(txtTot1.Text) + CDbl(txtTot2.Text)), "0,0.00")
            txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0,0.00")
            txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0,0.00")
        Else  'Tipo 2
            txtSub.Text = Format((CDbl(txtTot1.Text) + CDbl(txtTot2.Text)), "0,0.00")
            txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0,0.00")
            txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0,0.00")
        End If
        If cbMoneda.SelectedIndex <> -1 Then
            lblTotal.Text = "TOTAL  " & BindingSource0.Item(cbMoneda.SelectedIndex)(2) 'cbMoneda.Text.Trim()
            cambiarNroTotalLetra()
        End If
    End Sub

    Private Sub cambiarNroTotalLetra()
        If IsNumeric(cbMoneda.SelectedValue) Then
            Dim cALetra As New Num2LetEsp  'clase definida por el usuario
            If cbMoneda.SelectedValue = 30 Then    '30=Nuevos solesl
                'If BindingSource10.Item(BindingSource10.Position)(7) = 30 Then
                cALetra.Moneda = "Nuevos Soles"
            Else    'dolares
                cALetra.Moneda = "Dólares Americanos"
            End If
            'Inicia el Proceso para identificar la cantidad a convertir
            If Val(txtTotal.Text) > 0 Then
                cALetra.Numero = Val(CDbl(txtTotal.Text))
                txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
            End If
        End If
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

    Private Sub cbMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoneda.SelectedIndexChanged
        calcularSubTotal()
    End Sub

    Dim vfModificar1 As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar1 = "modificar" Then
            If BindingSource5.Position = -1 Then
                StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
                Exit Sub
            End If
            enlazarText()
            vfModificar1 = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            activarControles()
            txtBuscar.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If BindingSource2.Position = -1 Then
                MessageBox.Show("selecione Cliente valido...", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtBuscar.Focus()
                Exit Sub
            End If

            If ValidarCampos() Then
                Exit Sub
            End If

            If txtTot2.Text.Trim() <> 0 Then
                If validaCampoVacioMinCaracNoNumer(txtDet2.Text.Trim, 3) Then
                    MessageBox.Show("Registre detalle valido...", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                    txtDet2.Focus()
                    Exit Sub
                End If
            End If


            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TDocVenta
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

                'actualizando TDetalleVenta
                comandoUpdate3(txtCan1.Text, "", txtDet1.Text.Trim(), txtDes1.Text.Trim(), txtPre1.Text, BindingSource6.Item(0)(0)) 'primera fila
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

                'actualizando TDetalleVenta
                comandoUpdate3(txtCan2.Text, "", txtDet2.Text.Trim(), txtDes2.Text.Trim(), txtPre2.Text, BindingSource6.Item(1)(0)) 'segunda fila
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

                Dim codDocV As Integer = BindingSource5.Item(BindingSource5.Position)(0)

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan3 = False 'Detalle

                'Actualizando el dataTable
                parametrosSerieDoc()
                visualizarDocVenta()

                BindingSource2.RemoveFilter()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource5.Position = BindingSource5.Find("codDocV", codDocV)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan3 = True
                btnCancelar.PerformClick()

                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
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

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2()
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TDocVenta set fecDoc=@fec,idSesM=@idS,codIde=@codI,igv=@igv,calIGV=@cal,codMon=@codM,obs=@obs,hist=@hist,codigo=@cod where codDocV=@nro"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable2.Parameters.Add("@idS", SqlDbType.Int, 0).Value = cbMes.SelectedValue
        cmUpdateTable2.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbCli.SelectedValue
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
        cmUpdateTable2.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtDato.Text.Trim()
        cmUpdateTable2.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = BindingSource5.Item(BindingSource5.Position)(14) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmUpdateTable2.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Dim cmdUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal can As Decimal, ByVal uni As String, ByVal det As String, ByVal lin As String, ByVal precio As Decimal, ByVal codDV As Integer)
        cmdUpdateTable3 = New SqlCommand
        cmdUpdateTable3.CommandType = CommandType.Text
        cmdUpdateTable3.CommandText = "update TDetalleVenta set cant=@can,unidad=@uni,detalle=@det,linea=@lin,preUni=@pre where codDV=@cod"
        cmdUpdateTable3.Connection = Cn
        cmdUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable3.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmdUpdateTable3.Parameters.Add("@det", SqlDbType.VarChar, 100).Value = det
        cmdUpdateTable3.Parameters.Add("@lin", SqlDbType.VarChar, 200).Value = lin
        cmdUpdateTable3.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = precio
        cmdUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDV
    End Sub

    Private Sub btnAnula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnula.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Guia de Remision a ANULAR...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de ANULAR Doc. Venta" & Chr(13) & " Nº " & BindingSource5.Item(BindingSource5.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDocVenta
            comandoUpdate13(2, " ANULO " & Now.Date & " " & vPass & "-" & vSUsuario) '2=Anulado
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
            vfVan3 = False 'Detalle

            'Actualizando el dataTable
            parametrosSerieDoc()
            visualizarDocVenta()

            BindingSource2.RemoveFilter()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan3 = True
            enlazarText()
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué anulado con exito...")
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
    Private Sub comandoUpdate13(ByVal estado As Short, ByVal texto As String)
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TDocVenta set estado=@est,hist=@hist where codDocV=@cod"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado '2 = anulado
        cmUpdateTable13.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = BindingSource5.Item(BindingSource5.Position)(14) & texto
        cmUpdateTable13.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Private Sub btnCierra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCierra.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Guia de Remision a CERRAR...")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de CERRAR Doc. Venta Nº " & BindingSource5.Item(BindingSource5.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDocVenta
            comandoUpdate13(1, " CERRO " & Now.Date & " " & vPass & "-" & vSUsuario) '1=Cerrado
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
            vfVan3 = False 'Detalle

            'Actualizando el dataTable
            parametrosSerieDoc()
            visualizarDocVenta()

            BindingSource2.RemoveFilter()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan3 = True
            enlazarText()
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué CERRADO con exito...")
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Guia de Remisión...")
            Exit Sub
        End If

        vCliente = cbCli.Text.Trim()
        vDir = txtDir.Text.Trim()
        vRuc = txtRuc.Text.Trim()
        vObs = txtDato.Text.Trim()
        vFec1 = date1.Value

        vCan1 = txtCan1.Text.Trim()
        vDet1 = txtDet1.Text.Trim()
        vPre1 = txtPre1.Text.Trim()
        vTot1 = txtTot1.Text.Trim()
        vDes1 = txtDes1.Text.Trim()
        If CheckBox1.Checked Then
            vObra = ""
        Else
            vObra = "OBRA: " & cbObra.Text.Trim()
        End If

        vCan2 = txtCan2.Text.Trim()
        vDet2 = txtDet2.Text.Trim()
        vPre2 = txtPre2.Text.Trim()
        vTot2 = txtTot2.Text.Trim()
        vDes2 = txtDes2.Text.Trim()

        vLetra = txtLetraTotal.Text.Trim()
        vSub = txtSub.Text.Trim()
        vIgv1 = txtIGV.Text.Trim()
        vTot = txtTotal.Text.Trim()
        vMon = lblTotal.Text.Trim()

        vSerie = lbDoc.Text.Trim()

        Dim informe As New ReportViewerDocVentaForm
        informe.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class
