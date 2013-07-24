Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class registraCotizacion1Form
    Dim BindingSource0 As New BindingSource
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource
    Dim BindingSource6 As New BindingSource

    Private Sub registraCotizacion1Form_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
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

    Private Sub registraCotizacion1Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1 from TIdentidad where estado=1 and idTipId=2" ' '2=proveedor
        crearDataAdapterTable(daTProvee, sele)

        sele = "select distinct codigo,nombre,lugar,color from VLugarTrabajoLogin"
        crearDataAdapterTable(daTUbi, sele)

        sele = "select codPers,nombre+' '+apellido as nom from TPersonal where estado=1 order by nombre"
        crearDataAdapterTable(daTPers, sele)

        sele = "select codPag,forma from TFormaPago"
        crearDataAdapterTable(daTPago, sele)

        sele = "select codMon,moneda,simbolo from TMoneda"
        crearDataAdapterTable(daTMon, sele)

        sele = "select distinct codGruC,grupo,descrip,nroGru,estGru from VCotAper VGrupoCot order by codGruC"
        crearDataAdapterTable(daTabla4, sele)

        sele = "select codCot,nroCot,nro,codIde,fecCot,tiempoVig,atencion,plazo,codPag,lugarEnt,incluir,codPersS,codigo,codPers,obs,idSol,codGruC,estado,nro as nroCad,codMon from VCotAper" 'order by nroCot"
        crearDataAdapterTable(daTabla1, sele)
        'daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDetC,cant,unidad,descrip,precio,subTotal,est,codCot,codMat,estado from VDetCot where codCot=@idS" 'areaM,descrip
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTProvee.Fill(dsAlmacen, "TIdentidad")
            daTUbi.Fill(dsAlmacen, "VLugarTrabajoLogin")
            daTPers.Fill(dsAlmacen, "TPersonal")
            daTPago.Fill(dsAlmacen, "TFormaPago")
            daTMon.Fill(dsAlmacen, "TMoneda")
            daTabla4.Fill(dsAlmacen, "VGrupoCot")
            daTabla1.Fill(dsAlmacen, "VCotAper")
            daDetDoc.Fill(dsAlmacen, "VDetCot")

            AgregarRelacion()

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

            cbMoneda.DataSource = dsAlmacen
            cbMoneda.DisplayMember = "TMoneda.moneda"
            cbMoneda.ValueMember = "codMon"

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VGrupoCot"
            cbGrupo.DataSource = BindingSource0
            cbGrupo.DisplayMember = "grupo"
            cbGrupo.ValueMember = "codGruC"

            BindingSource6.DataSource = BindingSource0
            BindingSource6.DataMember = "Relacion1"
            lbCot.DataSource = BindingSource6
            lbCot.DisplayMember = "nro"
            lbCot.ValueMember = "codCot"
            BindingSource6.Sort = "nroCot"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "VDetCot"
            Navigator2.BindingSource = BindingSource5
            dgTabla2.DataSource = BindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            configurarColorControl()

            vfVan1 = True
            leerRuc()
            recuperarUltimoNro(vSCodigo)

            cbGrupo.SelectedIndex = cbGrupo.Items.Count - 1
            vfVan2 = True
            enlazarText()

            vfVan3 = True
            visualizarDet()

            cbBuscar.SelectedIndex = 0

            'If vSCodTipoUsu = 1 Or vSCodTipoUsu = 2 Then  '1=administrador 2=sud administrador
            'Solo administrador puede realizar este proceso
            AddContextMenu() 'Agregando menu antiClick
            'End If

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

    Private Sub registraCotizacion1Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        colorear()
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("VGrupoCot").Columns("codGruC"), dsAlmacen.Tables("VCotAper").Columns("codGruC"))
        dsAlmacen.Relations.Add(relation1)
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "Unid."
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 450
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 70
            .Columns(4).HeaderText = "Prec_Unit"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 70
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).HeaderText = "Estado"
            .Columns(6).Width = 90
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub activarBoton()
        cbGrupo.FlatStyle = FlatStyle.Flat
        cbGrupo.DropDownStyle = ComboBoxStyle.DropDownList
        cbGrupo.SelectedIndex = cbGrupo.Items.Count - 1

        btnAperturar.FlatStyle = FlatStyle.Flat
        btnAperturar.Enabled = False
        btnCierraGru.FlatStyle = FlatStyle.Standard
        btnCierraGru.Enabled = True
        btnDupli.FlatStyle = FlatStyle.Standard
        btnDupli.Enabled = True
        btnAprobar.FlatStyle = FlatStyle.Standard
        btnAprobar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnImprimir.FlatStyle = FlatStyle.Standard
        btnImprimir.Enabled = True
        btnElimina.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = True
        lbCot.Enabled = True
        Panel3.Enabled = True
        Panel4.Enabled = True
    End Sub

    Private Function recuperarGrupoNro() As Integer
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroGru),0) from TGrupoCot"
        cmdMaxCodigo.Connection = Cn
        Return cmdMaxCodigo.ExecuteScalar + 1
    End Function

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
        btnAperturar.ForeColor = ForeColorButtom
        btnCierraGru.ForeColor = ForeColorButtom
        btnDupli.ForeColor = ForeColorButtom
        btnAprobar.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnElimina.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
        btnProcesa.ForeColor = ForeColorButtom
        btnCrear.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnSol.ForeColor = ForeColorButtom
    End Sub

    Private Sub recuperarUltimoNro(ByVal cod As String)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroCot),0)+1 from TCotizacion"
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
            End If
        End If
    End Sub

    Private Sub cbProv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProv.SelectedIndexChanged
        leerRuc()
    End Sub

    Private Sub txtRuc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRuc.GotFocus, txtRuc.MouseClick
        txtRuc.SelectAll()
    End Sub

    Private Sub cbObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObra.SelectedIndexChanged
        txtLug.Text = BindingSource2.Item(cbObra.SelectedIndex)(2)
    End Sub

    Private Sub lbCot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCot.SelectedValueChanged
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
                cbProv.SelectedValue = BindingSource6.Item(lbCot.SelectedIndex)(3)
                txtInc.Text = BindingSource6.Item(lbCot.SelectedIndex)(10)
                txtObs.Text = BindingSource6.Item(lbCot.SelectedIndex)(14)
                date1.Value = BindingSource6.Item(lbCot.SelectedIndex)(4)
                txtAte.Text = BindingSource6.Item(lbCot.SelectedIndex)(6)
                cbObra.SelectedValue = BindingSource6.Item(lbCot.SelectedIndex)(12)
                txtLug.Text = BindingSource6.Item(lbCot.SelectedIndex)(9)
                cbPers.SelectedValue = BindingSource6.Item(lbCot.SelectedIndex)(11)
                txtTie.Text = BindingSource6.Item(lbCot.SelectedIndex)(5)
                txtPla.Text = BindingSource6.Item(lbCot.SelectedIndex)(7)
                cbPago.SelectedValue = BindingSource6.Item(lbCot.SelectedIndex)(8)
                cbMoneda.SelectedValue = BindingSource6.Item(lbCot.SelectedIndex)(19)
                txtRem.Text = recuperarCampo("nombre+' '+apellido", BindingSource6.Item(lbCot.SelectedIndex)(13))
                txtFono1.Text = recuperarCampo("fono", BindingSource6.Item(lbCot.SelectedIndex)(13))
                txtEma1.Text = recuperarCampo("email", BindingSource6.Item(lbCot.SelectedIndex)(13))
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
            dsAlmacen.Tables("VDetCot").Clear()
            daDetDoc.SelectCommand.Parameters("@idS").Value = BindingSource6.Item(lbCot.SelectedIndex)(0)
            daDetDoc.Fill(dsAlmacen, "VDetCot")
            colorearFila()
            colorear()
            sumTotal()
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

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource5.Count - 1
            If BindingSource5.Item(j)(9) = 1 Then 'Aprobado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
            If BindingSource5.Item(j)(9) = 2 Then 'Rechazado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource5.Position = -1 Then
            txtTotal.Text = "0.00"
            Exit Sub
        End If
        txtTotal.Text = dsAlmacen.Tables("VDetCot").Compute("Sum(subTotal)", Nothing)
    End Sub

    Private Function recuperarCampo(ByVal campo As String, ByVal codPers As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select " & campo & " from TPersonal where codPers=" & codPers
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCountGrupo(ByVal descrip As String) As Short
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TGrupoCot where descrip='" & descrip & "'"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim cmdInserGrupo As SqlCommand
    Private Sub procInsertGrupo(ByVal nro As Integer, ByVal grupo As String)
        cmdInserGrupo = New SqlCommand
        cmdInserGrupo.CommandType = CommandType.StoredProcedure
        cmdInserGrupo.CommandText = "PA_InsertTGrupo"
        cmdInserGrupo.Connection = Cn
        cmdInserGrupo.Parameters.Add("@nro", SqlDbType.Int, 0).Value = nro
        cmdInserGrupo.Parameters.Add("@des", SqlDbType.VarChar, 40).Value = grupo
        cmdInserGrupo.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0 'abierto
        'configurando direction output = parametro de solo salida
        cmdInserGrupo.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmdInserGrupo.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub btnDes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDes.Click
        If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion tiene registros en detalle cotizacion...")
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim codGruC As Integer = cbGrupo.SelectedValue

            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TCotizacion
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

            If recuperarCotGrup(codGruC, myTrans) = 0 Then
                'MsgBox("Eliminando grupo")
                'Tabla TGrupo
                comandoDelete2(codGruC)
                cmDeleteTable2.Transaction = myTrans
                If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True
            vfVan2 = False  'Enlazar Text
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VCotAper").Clear()
            dsAlmacen.Tables("VGrupoCot").Clear()

            daTabla4.Fill(dsAlmacen, "VGrupoCot")
            daTabla1.Fill(dsAlmacen, "VCotAper")

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


        activarBoton()
        desactivarText()
        enlazarText()

        btnAperturar.Text = "Nuevo Grupo"
        btnAperturar.FlatStyle = FlatStyle.Standard
        btnAperturar.Enabled = True
        mascaraOPS = 1 'Nuevo

        btnDes.FlatStyle = FlatStyle.Flat
        btnDes.Enabled = False
    End Sub

    Dim mascaraOPS As Short = 1
    Private Sub btnAperturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAperturar.Click
        If mascaraOPS = 1 Then '1=Nuevo
            If BindingSource6.Position <> -1 Then
                If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) = 0) Then
                    StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion NO tiene registros en detalle cotizacion...")
                    Exit Sub
                End If

                If recuperarCountOrdenCompra(BindingSource6.Item(BindingSource6.Position)(0)) > 0 Then
                    StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion tiene Instancia en ORDEN DE COMPRA...")
                    Exit Sub
                End If
            End If

            recuperarUltimoNro(vSCodigo)
            Dim campo As Integer = CInt(txtNro.Text)
            Dim nroGrupo As Integer = recuperarGrupoNro()

            cbGrupo.FlatStyle = FlatStyle.Flat
            cbGrupo.DropDownStyle = ComboBoxStyle.Simple
            cbGrupo.Text = "GP" & recuperarGrupoNro().ToString() & " " & Now.Date
            cbGrupo.Focus()

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            Me.Cursor = Cursors.WaitCursor
            wait.Show()
            'estableciendo una transaccion
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
                Me.Refresh()

                Dim codGruC As Integer
                'MsgBox("Agregando grupo nuevo")
                procInsertGrupo(nroGrupo, cbGrupo.Text.Trim())
                cmdInserGrupo.Transaction = myTrans
                cmdInserGrupo.ExecuteNonQuery()
                codGruC = cmdInserGrupo.Parameters("@Identity").Value

                'TIdentidad
                comandoUpdate1() 'codIde
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

                'TCotizacion
                comandoInsert(0, codGruC) '0=sin solicitud
                cmInserTable.Transaction = myTrans
                If cmInserTable.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                Dim codCot As Integer = cmInserTable.Parameters("@Identity").Value


                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
                vfVan3 = False

                'Actualizando el dataTable
                dsAlmacen.Tables("VCotAper").Clear()
                dsAlmacen.Tables("VGrupoCot").Clear()

                daTabla4.Fill(dsAlmacen, "VGrupoCot")
                daTabla1.Fill(dsAlmacen, "VCotAper")

                'visualizarDet()
                recuperarUltimoNro(vSCodigo)

                activarBoton()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource0.Position = BindingSource0.Find("codGruC", codGruC)
                'BindingSource6.Position = BindingSource6.Find("codCot", codCot)
                BindingSource6.Position = BindingSource6.Count - 1

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan2 = True
                vfVan3 = True
                enlazarText()
                visualizarDet()

                btnAperturar.Text = "Guardar Grupo"
                mascaraOPS = 2 'Guardar

                btnAperturar.FlatStyle = FlatStyle.Standard
                btnAperturar.Enabled = True
                btnDes.FlatStyle = FlatStyle.Standard
                btnDes.Enabled = True

                btnCierraGru.FlatStyle = FlatStyle.Flat
                btnCierraGru.Enabled = False
                btnDupli.FlatStyle = FlatStyle.Flat
                btnDupli.Enabled = False
                btnAprobar.FlatStyle = FlatStyle.Flat
                btnAprobar.Enabled = False
                btnModificar.FlatStyle = FlatStyle.Flat
                btnModificar.Enabled = False
                btnImprimir.FlatStyle = FlatStyle.Flat
                btnImprimir.Enabled = False
                btnElimina.FlatStyle = FlatStyle.Flat
                btnElimina.Enabled = False
                lbCot.Enabled = False
                Panel3.Enabled = True
                Panel4.Enabled = True

                activarText()
                txtRem.Clear()
                txtFono1.Clear()
                txtEma1.Clear()

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
            Exit Sub
        Else  '2=Procesar


            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If validaCampoVacioMinCaracNoNumer(cbGrupo.Text.Trim(), 3) Then
                MessageBox.Show("Agrege nombre de grupo valido...", nomNegocio, Nothing, MessageBoxIcon.Information)
                cbGrupo.Focus()
                Exit Sub
            End If

            'If recuperarCountGrupo(cbGrupo.Text.Trim) > 0 Then
            'MessageBox.Show("Nombre de grupo ya existe, cambie de nombre...", nomNegocio, Nothing, MessageBoxIcon.Information)
            'cbGrupo.Focus()
            'Exit Sub
            'End If

            If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) = 0) Then
                StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion NO tiene registros en detalle cotizacion...")
                Exit Sub
            End If

            'Dim resp As String = MessageBox.Show("Esta segúro de GUARDAR Cotización Nº " & lbCot.Text.Trim() & Chr(13) & "en el Grupo: " & cbGrupo.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If resp <> 6 Then
            'Exit Sub
            'End If

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

                'TCotizacion
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
                dsAlmacen.Tables("VCotAper").Clear()
                daTabla1.Fill(dsAlmacen, "VCotAper")

                vfVan2 = True
                vfVan3 = True

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


            activarBoton()
            desactivarText()
            enlazarText()

            btnAperturar.Text = "Nuevo Grupo"
            btnAperturar.FlatStyle = FlatStyle.Standard
            btnAperturar.Enabled = True
            mascaraOPS = 1 'Nuevo

            btnDes.FlatStyle = FlatStyle.Flat
            btnDes.Enabled = False
            Exit Sub
        End If


    End Sub

    Private Sub TEMO()
      

        recuperarUltimoNro(vSCodigo)
        Dim campo As Integer = CInt(txtNro.Text)
        Dim nroGrupo As Integer = recuperarGrupoNro()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        Me.Cursor = Cursors.WaitCursor
        wait.Show()
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            Dim codGruC As Integer
            'MsgBox("Agregando grupo nuevo")
            procInsertGrupo(nroGrupo, cbGrupo.Text.Trim())
            cmdInserGrupo.Transaction = myTrans
            cmdInserGrupo.ExecuteNonQuery()
            codGruC = cmdInserGrupo.Parameters("@Identity").Value

            codGruC = cbGrupo.SelectedValue


            'TIdentidad
            comandoUpdate1() 'codIde
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

            'TCotizacion
            comandoInsert(0, codGruC) '0=sin solicitud
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim codCot As Integer = cmInserTable.Parameters("@Identity").Value


            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
            vfVan3 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VCotAper").Clear()
            dsAlmacen.Tables("VGrupoCot").Clear()

            daTabla4.Fill(dsAlmacen, "VGrupoCot")
            daTabla1.Fill(dsAlmacen, "VCotAper")

            'visualizarDet()
            recuperarUltimoNro(vSCodigo)

            activarBoton()
            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource0.Position = BindingSource0.Find("codGruC", codGruC)
            'BindingSource6.Position = BindingSource6.Find("codCot", codCot)
            BindingSource6.Position = BindingSource6.Count - 1

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
    Private Sub comandoInsert(ByVal idS As Integer, ByVal codGruC As Integer)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertTCotizacion"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@tie", SqlDbType.VarChar, 20).Value = txtTie.Text.Trim()
        cmInserTable.Parameters.Add("@ate", SqlDbType.VarChar, 40).Value = txtAte.Text.Trim() '& " " & txtCel.Text.Trim()
        cmInserTable.Parameters.Add("@pla", SqlDbType.VarChar, 40).Value = txtPla.Text.Trim()
        cmInserTable.Parameters.Add("@codPa", SqlDbType.Int, 0).Value = cbPago.SelectedValue
        cmInserTable.Parameters.Add("@lug", SqlDbType.VarChar, 100).Value = txtLug.Text.Trim()
        cmInserTable.Parameters.Add("@inc", SqlDbType.VarChar, 100).Value = txtInc.Text.Trim()
        cmInserTable.Parameters.Add("@sol", SqlDbType.Int, 0).Value = cbPers.SelectedValue
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmInserTable.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmInserTable.Parameters.Add("@idS", SqlDbType.Int, 0).Value = idS
        cmInserTable.Parameters.Add("@codG", SqlDbType.Int, 0).Value = codGruC
        cmInserTable.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TIdentidad set celRpm=@cel,repres=@rep where codIde=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@cel", SqlDbType.VarChar, 50).Value = txtCel.Text.Trim()
        cmUpdateTable1.Parameters.Add("@rep", SqlDbType.VarChar, 60).Value = txtAte.Text.Trim()
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = cbProv.SelectedValue
    End Sub

    Private Function recuperarUltimoDoc() As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroCot) from TCotizacion"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleCot where codCot=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCotGrup(ByVal codGru As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TCotizacion where codGruC=" & codGru
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCountOrdenCompra(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TOrdenCompra where codCot=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc() <> CInt(BindingSource6.Item(BindingSource6.Position)(1)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima COTIZACION ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion tiene registros en detalle cotizacion...")
            Exit Sub
        End If

        If recuperarCountOrdenCompra(BindingSource6.Item(BindingSource6.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion tiene Instancia en ORDEN DE COMPRA...")
            Exit Sub
        End If


        Dim resp As String = MessageBox.Show("Esta segúro de eliminar cotizacion Nº " & lbCot.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            Dim codGruC As Integer = cbGrupo.SelectedValue

            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TCotizacion
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

            If recuperarCotGrup(codGruC, myTrans) = 0 Then
                'MsgBox("Eliminando grupo")
                'Tabla TGrupo
                comandoDelete2(codGruC)
                cmDeleteTable2.Transaction = myTrans
                If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True
            vfVan2 = False  'Enlazar Text
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VCotAper").Clear()
            dsAlmacen.Tables("VGrupoCot").Clear()

            daTabla4.Fill(dsAlmacen, "VGrupoCot")
            daTabla1.Fill(dsAlmacen, "VCotAper")

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
        cmDeleteTable1.CommandText = "delete from TCotizacion where codCot=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Dim cmDeleteTable2 As SqlCommand
    Private Sub comandoDelete2(ByVal codGruC As Integer)
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "delete from TGrupoCot where codGruC=@cod"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codGruC
    End Sub

    Private Sub activarText()
        cbProv.Enabled = True
        txtAte.ReadOnly = False
        txtCel.ReadOnly = False
        txtPla.ReadOnly = False
        cbPago.Enabled = True
        cbMoneda.Enabled = True
        txtTie.ReadOnly = False
        cbObra.Enabled = True
        txtLug.ReadOnly = False
        cbPers.Enabled = True
        txtInc.ReadOnly = False
        txtObs.ReadOnly = False
        date1.Enabled = True
    End Sub

    Private Sub desactivarControles1()
        Panel3.Enabled = False
        btnAperturar.Enabled = False
        btnAperturar.FlatStyle = FlatStyle.Flat
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = False
        btnElimina.FlatStyle = FlatStyle.Flat
        btnImprimir.Enabled = False
        btnImprimir.FlatStyle = FlatStyle.Flat
        btnDupli.Enabled = False
        btnDupli.FlatStyle = FlatStyle.Flat
        btnAprobar.Enabled = False
        btnAprobar.FlatStyle = FlatStyle.Flat
        btnCierraGru.Enabled = False
        btnCierraGru.FlatStyle = FlatStyle.Flat
        cbGrupo.Enabled = False
        lbCot.Enabled = False
        txtRem.Clear()
        txtFono1.Clear()
        txtEma1.Clear()
    End Sub

    Dim vfModificar As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar = "modificar" Then
            If BindingSource6.Position = -1 Then
                StatusBarClass.messageBarraEstado("  No existe cotizacion a modificar...")
                Exit Sub
            End If
            vfModificar = "actualizar"
            btnModificar.Text = "Actualizar"
            activarText()
            desactivarControles1()
            cbProv.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar

            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                date1.Focus()
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

                'TCotizacion
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
                dsAlmacen.Tables("VCotAper").Clear()
                daTabla1.Fill(dsAlmacen, "VCotAper")

                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource6.Position = BindingSource6.Find("nroCot", campo)

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

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2()
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TCotizacion set codIde=@codI,fecCot=@fec,tiempoVig=@tie,atencion=@ate,plazo=@pla,codPag=@codPa,lugarEnt=@lug,incluir=@inc,codPersS=@sol,codigo=@cod,codPers=@codP,obs=@obs,codMon=@codMon where codCot=@codCot"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmUpdateTable2.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable2.Parameters.Add("@tie", SqlDbType.VarChar, 20).Value = txtTie.Text.Trim()
        cmUpdateTable2.Parameters.Add("@ate", SqlDbType.VarChar, 40).Value = txtAte.Text.Trim() '& " " & txtCel.Text.Trim()
        cmUpdateTable2.Parameters.Add("@pla", SqlDbType.VarChar, 40).Value = txtPla.Text.Trim()
        cmUpdateTable2.Parameters.Add("@codPa", SqlDbType.Int, 0).Value = cbPago.SelectedValue
        cmUpdateTable2.Parameters.Add("@lug", SqlDbType.VarChar, 100).Value = txtLug.Text.Trim()
        cmUpdateTable2.Parameters.Add("@inc", SqlDbType.VarChar, 100).Value = txtInc.Text.Trim()
        cmUpdateTable2.Parameters.Add("@sol", SqlDbType.Int, 0).Value = cbPers.SelectedValue
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = cbObra.SelectedValue 'vSCodigo
        cmUpdateTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vPass
        cmUpdateTable2.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmUpdateTable2.Parameters.Add("@codMon", SqlDbType.Int, 0).Value = cbMoneda.SelectedValue
        cmUpdateTable2.Parameters.Add("@codCot", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
    End Sub

    Private Sub desactivarText()
        cbProv.Enabled = False
        txtAte.ReadOnly = True
        txtCel.ReadOnly = True
        txtPla.ReadOnly = True
        cbPago.Enabled = False
        cbMoneda.Enabled = False
        txtTie.ReadOnly = True
        cbObra.Enabled = False
        txtLug.ReadOnly = True
        cbPers.Enabled = False
        txtInc.ReadOnly = True
        txtObs.ReadOnly = True
        date1.Enabled = False
    End Sub

    Private Sub activarControles1()
        Panel3.Enabled = True
        Panel4.Enabled = True
        btnAperturar.Enabled = True
        btnAperturar.FlatStyle = FlatStyle.Standard
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnElimina.Enabled = True
        btnElimina.FlatStyle = FlatStyle.Standard
        btnImprimir.Enabled = True
        btnImprimir.FlatStyle = FlatStyle.Standard
        btnDupli.Enabled = True
        btnDupli.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnAprobar.Enabled = True
        btnAprobar.FlatStyle = FlatStyle.Standard
        btnCierraGru.Enabled = True
        btnCierraGru.FlatStyle = FlatStyle.Standard
        cbGrupo.Enabled = True
        lbCot.Enabled = True
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfModificar = "modificar"
        vfduplicar = "modificar"
        btnModificar.Text = "Modificar"
        btnDupli.Text = "Selec. Proveedores"
        desactivarText()
        activarControles1()
        vfVan2 = True  'Enlazar Text
        vfVan3 = True
        enlazarText()
        visualizarDet()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
    End Sub

    Private Sub desactivarControles2()
        Panel3.Enabled = False
        Panel4.Enabled = False
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnElimina.Enabled = False
        btnElimina.FlatStyle = FlatStyle.Flat
        btnModificar.Enabled = False
        btnModificar.FlatStyle = FlatStyle.Flat
        btnAprobar.Enabled = False
        btnAprobar.FlatStyle = FlatStyle.Flat
        btnCierraGru.Enabled = False
        btnCierraGru.FlatStyle = FlatStyle.Flat
        cbGrupo.Enabled = False
        lbCot.Enabled = False
        txtRem.Clear()
        txtFono1.Clear()
        txtEma1.Clear()
    End Sub

    Private Function recuperarIdSol(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select idSol from TCotizacion where codCot=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub recuperarUltimoNro1(ByVal cod As String, ByVal myTrans As SqlTransaction)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select isnull(max(nroCot),0)+1 from TCotizacion"
        cmdMaxCodigo.Connection = Cn
        cmdMaxCodigo.Transaction = myTrans
        asignarNro(cmdMaxCodigo.ExecuteScalar)
    End Sub

    Dim vfduplicar As String = "modificar"
    Private Sub btnDupli_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDupli.Click
        If BindingSource6.Position = -1 Then
            MessageBox.Show("Proceso Denegado, No existe Cotización Base...", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) = 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion NO tiene registros en detalle cotizacion...")
            Exit Sub
        End If

        vCodIde = cbProv.SelectedValue
        vCod2 = "0"  'retornar el idSol de solicitud

        'Else    'Actualizar
        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            date1.Focus()
            Exit Sub
        End If

        Dim prove As New seleProveedorForm
        prove.ShowDialog()

        If CInt(vCod2) > 0 Then 'con solicitud
            'MsgBox("PROCESAR")
        Else
            'MsgBox("CANCELADO")
            Exit Sub
        End If

        Dim idSol As Integer = recuperarIdSol(lbCot.SelectedValue)

        If vfduplicar = "modificar" Then
            vfduplicar = "actualizar"
            btnDupli.Text = "Ejecutar"
            activarText()
            desactivarControles2()
            cbProv.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnDupli

            'Dim resp As String = MessageBox.Show("Esta segúro de aperturar Cotización Nº " & txtNro.Text.Trim() & Chr(13) & "en el Grupo: " & cbGrupo.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If resp <> 6 Then
            'Exit Sub
            'End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Me.Cursor = Cursors.WaitCursor
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, PROCESANDO INFORMACION....")

                Dim codGruC As Integer
                For x As Short = 0 To vX1 - 1
                    recuperarUltimoNro1(vSCodigo, myTrans)
                    Dim campo As Integer = CInt(txtNro.Text)

                    cbProv.SelectedValue = matriz(x) 'codIde
                    'MsgBox("Proveedor " & matriz(x))
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

                    codGruC = cbGrupo.SelectedValue
                    'TCotizacion
                    comandoInsert(idSol, codGruC) '0=sin solicitud
                    cmInserTable.Transaction = myTrans
                    If cmInserTable.ExecuteNonQuery() < 1 Then
                        wait.Close()
                        Me.Cursor = Cursors.Default
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                    Dim codCot As Integer = cmInserTable.Parameters("@Identity").Value

                    For j As Short = 0 To BindingSource5.Count - 1
                        'TDetalleCot
                        comandoInsert11(BindingSource5.Item(j)(1), BindingSource5.Item(j)(2), BindingSource5.Item(j)(3), BindingSource5.Item(j)(4), BindingSource5.Item(j)(5), codCot, BindingSource5.Item(j)(8))
                        cmInserTable11.Transaction = myTrans
                        If cmInserTable11.ExecuteNonQuery() < 1 Then
                            wait.Close()
                            Me.Cursor = Cursors.Default
                            myTrans.Rollback()
                            MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                            Me.Close()
                            Exit Sub
                        End If
                    Next

                Next

                'confirma la transaccion 
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
                vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
                vfVan3 = False  'Enlazar DetalleCot en boton cancelar

                'Actualizando el dataSet 
                dsAlmacen.Tables("VCotAper").Clear()
                dsAlmacen.Tables("VGrupoCot").Clear()

                daTabla4.Fill(dsAlmacen, "VGrupoCot")
                daTabla1.Fill(dsAlmacen, "VCotAper")

                recuperarUltimoNro(vSCodigo)
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource0.Position = BindingSource0.Find("codGruC", codGruC)
                'BindingSource6.Position = BindingSource6.Find("codCot", codCot)
                BindingSource6.Position = BindingSource6.Count - 1
                btnCancelar.PerformClick()

                If mascaraOPS = 2 Then 'Estado Guardar Grupo
                    btnAperturar.Text = "Nuevo Grupo"
                    btnAperturar.FlatStyle = FlatStyle.Standard
                    btnAperturar.Enabled = True
                    mascaraOPS = 1 'Nuevo

                    btnDes.FlatStyle = FlatStyle.Flat
                    btnDes.Enabled = False
                End If

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
            .Columns(1).Width = 400
            .Columns(2).Width = 45
            .Columns(2).HeaderText = "Unid."
            .Columns(3).Width = 60
            .Columns(3).HeaderText = "Precio"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).HeaderText = "Tipo Insumo"
            .Columns(4).Width = 120
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 178
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

        If ValidaNroMayorOigualCero(txtPre.Text) Then
            txtCan.errorProv()
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
            StatusBarClass.messageBarraEstado("  Proceso denegado, Cotización NO APERTURADA...")
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

            'TDetalleCot
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
            Dim codDetC As Integer = cmInserTable1.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource5.Position = BindingSource5.Find("codDetC", codDetC)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            btnDupli.FlatStyle = FlatStyle.Standard
            btnDupli.Enabled = True

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

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1()
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertDetalleCot"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = txtCan.Text
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = BindingSource4.Item(BindingSource4.Position)(2)
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = BindingSource4.Item(BindingSource4.Position)(1)
        cmInserTable1.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = txtPre.Text
        cmInserTable1.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = CDbl(txtCan.Text) * CDbl(txtPre.Text)
        cmInserTable1.Parameters.Add("@codC", SqlDbType.Int, 0).Value = BindingSource6.Item(BindingSource6.Position)(0)
        cmInserTable1.Parameters.Add("@codM", SqlDbType.Int, 0).Value = BindingSource4.Item(BindingSource4.Position)(0)
        cmInserTable1.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0  '0=pendiente
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable11 As SqlCommand
    Private Sub comandoInsert11(ByVal cant As Decimal, ByVal uni As String, ByVal des As String, ByVal pre As Decimal, ByVal subTot As Decimal, ByVal codCot As Integer, ByVal codMat As Integer)
        cmInserTable11 = New SqlCommand
        cmInserTable11.CommandType = CommandType.Text
        cmInserTable11.CommandText = "insert into TDetalleCot(cant,unidad,descrip,precio,subTotal,codCot,codMat,estado) values(@can,@uni,@des,@pre,@sub,@codC,@codM,@est)"
        cmInserTable11.Connection = Cn
        cmInserTable11.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmInserTable11.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable11.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = des
        cmInserTable11.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = pre
        cmInserTable11.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = subTot
        cmInserTable11.Parameters.Add("@codC", SqlDbType.Int, 0).Value = codCot
        cmInserTable11.Parameters.Add("@codM", SqlDbType.Int, 0).Value = codMat
        cmInserTable11.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0  '0=pendiente
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

            For j As Short = 0 To BindingSource5.Count - 1
                'actualizando TDetalleCot
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
    Private Sub comandoUpdate3(ByVal can As Decimal, ByVal precio As Decimal, ByVal subTotal As Decimal, ByVal codDetC As Integer)
        cmdUpdateTable3 = New SqlCommand
        cmdUpdateTable3.CommandType = CommandType.Text
        cmdUpdateTable3.CommandText = "update TDetalleCot set cant=@can,precio=@pre,subTotal=@sub where codDetC=@cod"
        cmdUpdateTable3.Connection = Cn
        cmdUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable3.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = precio
        cmdUpdateTable3.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = subTotal
        cmdUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDetC
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

    'Eliminar linea cotizacion insumo
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

            'TDetalleCot
            comandoDelete3()
            cmDeleteTable3.Transaction = myTrans
            If cmDeleteTable3.ExecuteNonQuery() < 1 Then
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

    Dim cmDeleteTable3 As SqlCommand
    Private Sub comandoDelete3()
        cmDeleteTable3 = New SqlCommand
        cmDeleteTable3.CommandType = CommandType.Text
        cmDeleteTable3.CommandText = "delete from TDetalleCot where codDetC=@cod"
        cmDeleteTable3.Connection = Cn
        cmDeleteTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource5.Item(BindingSource5.Position)(0)
    End Sub

    Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe cotizacion a APROBAR...")
            Exit Sub
        End If

        If (recuperarCount(BindingSource6.Item(BindingSource6.Position)(0)) = 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Cotizacion NO tiene registros en detalle cotizacion...")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de APROBAR Cotizacion Nº " & BindingSource6.Item(BindingSource6.Position)(2), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            For j As Short = 0 To BindingSource5.Count - 1
                'TDetalleCot
                comandoUpdate22(1, BindingSource5.Item(j)(0)) '1=Aprobado
                cmUpdateTable22.Transaction = myTrans
                If cmUpdateTable22.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            finalMytrans = True
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")
            vfVan2 = False  'Enlazar Text  TRUE en boton cancelar
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCotAper").Clear()
            daTabla1.Fill(dsAlmacen, "VCotAper")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource6.Position = BindingSource6.Find("nroCot", campo)

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

    Dim cmUpdateTable22 As SqlCommand
    Private Sub comandoUpdate22(ByVal estado As Short, ByVal codDetC As Integer)
        cmUpdateTable22 = New SqlCommand
        cmUpdateTable22.CommandType = CommandType.Text
        cmUpdateTable22.CommandText = "update TDetalleCot set estado=@est where codDetC=@codD"
        cmUpdateTable22.Connection = Cn
        cmUpdateTable22.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable22.Parameters.Add("@codD", SqlDbType.Int, 0).Value = codDetC
    End Sub

    Private Sub btnCierraGru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCierraGru.Click
        If BindingSource0.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe grupo a CERRAR...")
            Exit Sub
        End If

        For p As Short = 0 To BindingSource6.Count - 1
            BindingSource6.Position = p
            If (recuperarCount(BindingSource6.Item(p)(0)) = 0) Then
                MessageBox.Show("  PROCESO DENEGADO, Cotizacion Nº" & BindingSource6.Item(p)(18) & " NO tiene registros en detalle cotizacion...", nomNegocio, Nothing, MessageBoxIcon.Stop)
                Exit Sub
            End If
        Next

        Dim resp As Short = MessageBox.Show("Esta segúro de CERRAR Grupo: " & BindingSource0.Item(BindingSource0.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TGrupoCot
            comandoUpdate23(1) '1=Aprobado
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
            vfVan3 = False  'Enlazar DetalleCot TRUE en boton cancelar

            'Actualizando el dataTable
            dsAlmacen.Tables("VCotAper").Clear()
            dsAlmacen.Tables("VGrupoCot").Clear()

            daTabla4.Fill(dsAlmacen, "VGrupoCot")
            daTabla1.Fill(dsAlmacen, "VCotAper")

            recuperarUltimoNro(vSCodigo)

            vfVan2 = True  'Enlazar Text
            vfVan3 = True
            enlazarText()
            dsAlmacen.Tables("VDetCot").Clear()
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Grupo fúe cerrado con exito...")
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
        cmUpdateTable23.CommandText = "update TGrupoCot set estGru=@est where codGruC=@codG"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@codG", SqlDbType.Int, 0).Value = BindingSource0.Item(BindingSource0.Position)(0)
    End Sub

    '-----------------------------------------
    '------Menu antiClick---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "APROBADO"
        toolStripItem1.BackColor = Color.YellowGreen
        toolStripItem2.Text = "RECHAZADO"
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

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A APROBAR...")
            Exit Sub
        End If

        If BindingSource5.Item(BindingSource5.Position)(9) = 1 Then  '1=aprobado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA APROBADO...")
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
            Dim position As Short = BindingSource5.Position

            'TDetalleCot
            comandoUpdate22(1, BindingSource5.Item(BindingSource5.Position)(0)) '1=Aprobado
            cmUpdateTable22.Transaction = myTrans
            If cmUpdateTable22.ExecuteNonQuery() < 1 Then
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
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.BackColor = Color.Green
            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.ForeColor = Color.White

            BindingSource5.Item(BindingSource5.Position)(9) = 1
            BindingSource5.Item(BindingSource5.Position)(6) = "APROBADO"

            If position < BindingSource5.Count - 1 Then
                BindingSource5.Position = position + 1  '
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

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO A RECHAZAR...")
            Exit Sub
        End If

        If BindingSource5.Item(BindingSource5.Position)(9) = 2 Then  '2=rechazado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA RECHAZADO...")
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
            Dim position As Short = BindingSource5.Position

            'TDetalleCot
            comandoUpdate22(2, BindingSource5.Item(BindingSource5.Position)(0)) '2=rechazado
            cmUpdateTable22.Transaction = myTrans
            If cmUpdateTable22.ExecuteNonQuery() < 1 Then
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
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.BackColor = Color.Red
            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.ForeColor = Color.White

            BindingSource5.Item(BindingSource5.Position)(9) = 2
            BindingSource5.Item(BindingSource5.Position)(6) = "RECHAZADO"

            If position < BindingSource5.Count - 1 Then
                BindingSource5.Position = position + 1  '
            End If

            StatusBarClass.messageBarraEstado("  INSUMO FUE RECHAZADO CON EXITO...")
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
        If BindingSource5.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE INSUMO...")
            Exit Sub
        End If

        If BindingSource5.Item(BindingSource5.Position)(9) = 0 Then  '0=pendiente
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, ESTADO PENDIENTE...")
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
            Dim position As Short = BindingSource5.Position

            'TDetalleCot
            comandoUpdate22(0, BindingSource5.Item(BindingSource5.Position)(0)) '0=PENDIENTE
            cmUpdateTable22.Transaction = myTrans
            If cmUpdateTable22.ExecuteNonQuery() < 1 Then
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
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.BackColor = Color.White
            dgTabla2.Rows(BindingSource5.Position).Cells(6).Style.ForeColor = Color.Black

            BindingSource5.Item(BindingSource5.Position)(9) = 0
            BindingSource5.Item(BindingSource5.Position)(6) = "PENDIENTE"

            If position < BindingSource5.Count - 1 Then
                BindingSource5.Position = position + 1  '
            End If

            'StatusBarClass.messageBarraEstado("  INSUMO FUE RECHAZADO CON EXITO...")
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

    Private Sub btnSol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSol.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Cotización...")
            Exit Sub
        End If

        vCod3 = cbObra.SelectedValue 'BindingSource6.Item(BindingSource6.Position)(12) 'Codigo de obra
        vCod1 = lbCot.Text.Trim()
        vNroOrden = lbCot.SelectedValue 'codCot para retorno
        vCod2 = "0"  'retornar el idSol de solicitud

        Dim jala As New jalarSolicitudForm
        jala.ShowDialog()

        visualizarDet()

        btnDupli.FlatStyle = FlatStyle.Standard
        btnDupli.Enabled = True

        'If CInt(vCod2) > 0 Then 'con solicitud
        'txtNroCot.Text = recuperarNroCot(vCod2)
        'Else

        'End If
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource6.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe cotizacion a imprimir...")
            Exit Sub
        End If

        vCodDoc = BindingSource6.Item(BindingSource6.Position)(0)
        vParam1 = BindingSource6.Item(BindingSource6.Position)(18) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year
        Dim informe As New ReportViewerCotizacionForm
        informe.ShowDialog()
    End Sub

    Private Sub lbCot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbCot.SelectedIndexChanged

    End Sub
End Class
