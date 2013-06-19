Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class jalarCotizacionForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim dataTable2 As New DataTable()
    Dim dataTable3 As New DataTable()
    Dim dataTable4 As New DataTable()
    Dim dataTable5 As New DataTable()

    Dim bindingSource1 As New BindingSource()
    Dim bindingSource2 As New BindingSource()
    Dim bindingSource3 As New BindingSource()
    Dim bindingSource4 As New BindingSource()
    Dim bindingSource5 As New BindingSource()

    Private Sub jalarCotizacionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1,cuentaBan from TIdentidad where idTipId=2" ' '2=proveedor
        crearDataAdapterTable(dTable1, sele)

        sele = "select distinct codigo,nombre,lugar,color from VObra"
        crearDataAdapterTable(dTable2, sele)

        sele = "select distinct codGruC,grupo,descrip,nroGru,estGru from VCotOrden VGrupoCot order by codGruC"
        crearDataAdapterTable(dTable3, sele)

        sele = "select codCot,nroCot,nro,codIde,fecCot,tiempoVig,atencion,plazo,codPag,lugarEnt,incluir,codPersS,codigo,codPers,obs,idSol,codGruC,estado,forma,nom,nom1,fono,email,isnull(NroSol,'') as NroSol,codMon,moneda from VCotOrden" 'order by nroCot"
        crearDataAdapterTable(dTable4, sele)

        sele = "select codDetC,cant,unidad,descrip,precio,subTotal,est,codCot,codMat,estado from VDetCot where codCot=@idS" 'areaM,descrip
        crearDataAdapterTable(dTable5, sele)
        dTable5.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'llenar la tabla virtual con los dataAdapter
            dTable1.Fill(dataTable1)
            dTable2.Fill(dataTable2)
            dTable3.Fill(dataTable3)
            dTable4.Fill(dataTable4)
            dTable5.Fill(dataTable5)

            bindingSource1.DataSource = dataTable1
            bindingSource1.Sort = "razon"

            bindingSource2.DataSource = dataTable2

            bindingSource3.DataSource = dataTable3
            cbGrupo.DataSource = bindingSource3
            cbGrupo.DisplayMember = "grupo"
            cbGrupo.ValueMember = "codGruC"

            bindingSource4.DataSource = dataTable4
            lbCot.DataSource = bindingSource4
            lbCot.DisplayMember = "nro"
            lbCot.ValueMember = "codCot"
            bindingSource4.Sort = "nroCot"

            bindingSource5.DataSource = dataTable5
            Navigator2.BindingSource = bindingSource5
            dgTabla2.DataSource = bindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            configurarColorControl()

            cbGrupo.SelectedIndex = cbGrupo.Items.Count - 1
            vfVan1 = True
            relacionGrupoCoti()

            vfVan2 = True
            enlazarText()

            vfVan3 = True
            visualizarDet()

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

    Private Sub cbGrupo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbGrupo.SelectedValueChanged
        relacionGrupoCoti()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub relacionGrupoCoti()
        If vfVan1 Then
            bindingSource4.Filter = "codGruC=" & cbGrupo.SelectedValue
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "Unid."
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 400
            .Columns(4).Width = 70
            .Columns(4).HeaderText = "Prec_Unit"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).Width = 70
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).HeaderText = "Estado"
            .Columns(6).Width = 80
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
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
        btnVol.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub lbCot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCot.SelectedValueChanged
        Me.Cursor = Cursors.WaitCursor
        enlazarText()
        visualizarDet()
        Me.Cursor = Cursors.Default
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub enlazarText()
        If vfVan2 Then
            If bindingSource4.Count = 0 Then
                'desEnlazarText()
            Else
                If bindingSource1.Position <> -1 Then
                    bindingSource1.Position = bindingSource1.Find("codIde", bindingSource4.Item(lbCot.SelectedIndex)(3))
                    txtProv.Text = bindingSource1.Item(bindingSource1.Position)(1)
                    txtRuc.Text = bindingSource1.Item(bindingSource1.Position)(2)
                    txtFono.Text = bindingSource1.Item(bindingSource1.Position)(9)
                    txtEma.Text = bindingSource1.Item(bindingSource1.Position)(7)
                    'txtAte.Text = BindingSource1.Item(BindingSource1.Position)(8)
                    txtCel.Text = bindingSource1.Item(bindingSource1.Position)(6)
                End If

                txtFec.Text = bindingSource4.Item(lbCot.SelectedIndex)(4)
                txtAte.Text = bindingSource4.Item(lbCot.SelectedIndex)(6)
                txtPla.Text = bindingSource4.Item(lbCot.SelectedIndex)(7)
                txtPag.Text = bindingSource4.Item(lbCot.SelectedIndex)(18)
                txtMon.Text = bindingSource4.Item(lbCot.SelectedIndex)(25)
                txtTie.Text = bindingSource4.Item(lbCot.SelectedIndex)(5)
                txtLug.Text = bindingSource4.Item(lbCot.SelectedIndex)(9)
                txtPers.Text = bindingSource4.Item(lbCot.SelectedIndex)(19)
                txtInc.Text = bindingSource4.Item(lbCot.SelectedIndex)(10)
                txtObs.Text = bindingSource4.Item(lbCot.SelectedIndex)(14)
                txtRem.Text = bindingSource4.Item(lbCot.SelectedIndex)(20)
                txtFono1.Text = bindingSource4.Item(lbCot.SelectedIndex)(21)
                txtEma1.Text = bindingSource4.Item(lbCot.SelectedIndex)(22)
                txtOrden.Text = bindingSource4.Item(lbCot.SelectedIndex)(23)

                If bindingSource2.Position <> -1 Then
                    bindingSource2.Position = bindingSource2.Find("codigo", bindingSource4.Item(lbCot.SelectedIndex)(12))
                    txtObra.Text = bindingSource2.Item(bindingSource2.Position)(1)
                End If
            End If
        End If
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If bindingSource4.Position = -1 Then
                Exit Sub
            End If
            dataTable5.Clear()
            dTable5.SelectCommand.Parameters("@idS").Value = bindingSource4.Item(lbCot.SelectedIndex)(0)
            dTable5.Fill(dataTable5)
            colorearFila()
            sumTotal()
        End If
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource5.Count - 1
            If bindingSource5.Item(j)(9) = 1 Then 'Aprobado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.YellowGreen
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.DarkBlue
            End If
            If BindingSource5.Item(j)(9) = 2 Then 'Rechazado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Yellow
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource5.Position = -1 Then
            txtTotal.Text = "0.00"
            Exit Sub
        End If
        txtTotal.Text = dataTable5.Compute("Sum(subTotal)", Nothing)
    End Sub

    Private Function recuperarUltimoNro() As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(nroO),0)+1 from TOrdenCompra"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnVol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVol.Click
        If bindingSource5.Position = -1 Then
            MessageBox.Show("No Existe Detalle de Cotizacion...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim vFSigue As Boolean = True
        For j As Short = 0 To bindingSource5.Count - 1
            If bindingSource5.Item(j)(9) = 1 Then '1=Aprobados solo agregar  
                vFSigue = False
                Exit For
            End If
        Next

        If vFSigue Then
            MessageBox.Show("Proceso denegado, ningun insumo esta APROBADO", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de aperturar Orden de Compra Nº 00" & recuperarUltimoNro(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim nroO As Integer = recuperarUltimoNro()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass1.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            'TOrdenCompra
            comandoInsert(nroO, bindingSource4.Item(bindingSource4.Position)(0), bindingSource4.Item(bindingSource4.Position)(15))
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

            For j As Short = 0 To bindingSource5.Count - 1
                If bindingSource5.Item(j)(9) = 1 Then '1=Aprobados solo agregar  
                    'TDetalleOrden
                    comandoInsert1(bindingSource5.Item(j)(1), bindingSource5.Item(j)(2), bindingSource5.Item(j)(3), bindingSource5.Item(j)(4), bindingSource5.Item(j)(5), bindingSource5.Item(j)(8), nroOrden)
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
                End If
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass1.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            vCod2 = nroOrden
            
            wait.Close()
            Me.Cursor = Cursors.Default

            Me.Close() 'retornar
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

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert(ByVal nroO As Integer, ByVal codCot As Integer, ByVal idSol As Integer)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertOrdenCompra"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@nroO", SqlDbType.Int, 0).Value = nroO
        cmInserTable.Parameters.Add("@fecO", SqlDbType.Date).Value = Now.Date
        cmInserTable.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = bindingSource4.Item(bindingSource4.Position)(3)
        cmInserTable.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@codPag", SqlDbType.Int, 0).Value = bindingSource4.Item(bindingSource4.Position)(8)
        cmInserTable.Parameters.Add("@igv", SqlDbType.Decimal, 0).Value = 18
        cmInserTable.Parameters.Add("@calIGV", SqlDbType.Int, 0).Value = 1   'tipo calculo IGV normal
        cmInserTable.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = bindingSource4.Item(bindingSource4.Position)(24)
        cmInserTable.Parameters.Add("@atiendeCom", SqlDbType.VarChar, 50).Value = txtAte.Text.Trim()
        cmInserTable.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmInserTable.Parameters.Add("@plazoEnt", SqlDbType.VarChar, 40).Value = txtPla.Text.Trim()
        cmInserTable.Parameters.Add("@transfe", SqlDbType.VarChar, 100).Value = bindingSource1.Item(bindingSource1.Position)(10)
        cmInserTable.Parameters.Add("@nroProf", SqlDbType.VarChar, 40).Value = ""
        cmInserTable.Parameters.Add("@obsFac", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmInserTable.Parameters.Add("@estado", SqlDbType.Int, 0).Value = 0 'abierto
        cmInserTable.Parameters.Add("@codCot", SqlDbType.Int, 0).Value = codCot
        cmInserTable.Parameters.Add("@idSol", SqlDbType.Int, 0).Value = idSol
        cmInserTable.Parameters.Add("@codPersO", SqlDbType.Int, 0).Value = bindingSource4.Item(bindingSource4.Position)(11)
        cmInserTable.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = bindingSource4.Item(bindingSource4.Position)(12)
        cmInserTable.Parameters.Add("@lugar", SqlDbType.VarChar, 100).Value = txtLug.Text.Trim()
        cmInserTable.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = "Creo " & Now.Date & " " & vPass & "-" & vSUsuario
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal cant As Decimal, ByVal unidad As String, ByVal descrip As String, ByVal precio As Decimal, ByVal subtotal As String, ByVal codMat As Integer, ByVal nroOrden As String)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertDetalleOrden"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = unidad
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = descrip
        cmInserTable1.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = precio
        cmInserTable1.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = subTotal
        cmInserTable1.Parameters.Add("@codM", SqlDbType.Int, 0).Value = codMat
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = nroOrden
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub
End Class
