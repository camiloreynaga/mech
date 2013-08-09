Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class registraGuiaRemEmpForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource
    Dim BindingSource6 As New BindingSource
    Dim BindingSource7 As New BindingSource
    Dim BindingSource8 As New BindingSource
    Dim BindingSource9 As New BindingSource
    Dim BindingSource10 As New BindingSource
    Dim BindingSource11 As New BindingSource
    Dim BindingSource12 As New BindingSource
    Dim BindingSource13 As New BindingSource

    Private Sub registraGuiaRemEmpForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        ' Primero capturamos la tecla pulsada, ... 
        If keyData = Keys.Down Then 'Flecha abajo
            If txtBuscar.Focused = True Then
                cbProv.Focus()
            End If
        End If

        ' Primero capturamos la tecla pulsada, ... 
        If keyData = Keys.F5 Then
            btnProcesa.PerformClick()
        End If

        ' ... y después llamamos al procedimiento de la clase base. 
        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private Sub registraGuiaRemEmpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSerS,serie,iniNroDoc,finNroDoc from TSerieSede where estado=1 and codSerS>1 and codTipDE=75"  '75=Guia Remision 70=Factura 1=Reservado Guia Remision provvedor
        crearDataAdapterTable(daVSerie, sele)

        sele = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1,cuentaBan,cuentaDet from TIdentidad where estado=1" ' 'clientes y proveedores 
        crearDataAdapterTable(daTProvee, sele)

        sele = "select distinct codigo,nombre,lugar,color from VLugarObraAlmacen"
        crearDataAdapterTable(daVObra, sele)
        sele = "select codUbi,ubicacion,codigo,color from VLugarObraAlmacen TAlmacen"
        crearDataAdapterTable(daTUbi, sele)

        sele = "select distinct codET,razon,ruc from VTransporte TEmpTransp order by razon"
        crearDataAdapterTable(daVNeg, sele)
        sele = "select distinct codVeh,marcaNro,nroConst,codET from VTransporte TVehiculo order by marcaNro"
        crearDataAdapterTable(daTUni, sele)
        sele = "select distinct codT,nroLic,nombre,DNI,codET from VTransporte TTransportista order by nroLic"
        crearDataAdapterTable(daTPers, sele)

        sele = "select codMotG,motivo from TMotivoGuia order by motivo"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select codGuiaE,nro,talon,nroGuia,fecIni,codSerS,codIde,codUbiOri,codUbiDes,partida,llegada,codVeh,codT,codMotG,nroFact,obs,codPers,codObraOri,codObraDes,codET,hist from VGuiaRemEmpAper where codSerS=@codSer" 'order by nroGuia"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codSer", SqlDbType.Int, 0).Value = 0

        sele = "select codDGE,cant,descrip,linea1,unidad,peso,detalle,codGuiaE,codMat,entre,entregado from VDetGuiaE where codGuiaE=@nro"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daVSerie.Fill(dsAlmacen, "TSerieSede")
            daTProvee.Fill(dsAlmacen, "TIdentidad")
            daVObra.Fill(dsAlmacen, "VLugarObraAlmacen")
            daTUbi.Fill(dsAlmacen, "TAlmacen")
            daVNeg.Fill(dsAlmacen, "TEmpTransp")
            daTUni.Fill(dsAlmacen, "TVehiculo")
            daTPers.Fill(dsAlmacen, "TTransportista")
            daTabla2.Fill(dsAlmacen, "TMotivoGuia")
            daTabla1.Fill(dsAlmacen, "VGuiaRemEmpAper")
            daDetDoc.Fill(dsAlmacen, "VDetGuiaE")

            AgregarRelacion()

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TSerieSede"
            cbSerie.DataSource = BindingSource1
            cbSerie.DisplayMember = "serie"
            cbSerie.ValueMember = "codSerS"
            BindingSource1.Sort = "serie"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TIdentidad"
            cbProv.DataSource = BindingSource2
            cbProv.DisplayMember = "razon"
            cbProv.ValueMember = "codIde"
            BindingSource2.Sort = "razon"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VLugarObraAlmacen"
            cbObra1.DataSource = BindingSource3
            cbObra1.DisplayMember = "nombre"
            cbObra1.ValueMember = "codigo"
            BindingSource4.DataSource = BindingSource3
            BindingSource4.DataMember = "Relacion1"
            cbAlm1.DataSource = BindingSource4
            cbAlm1.DisplayMember = "ubicacion"
            cbAlm1.ValueMember = "codUbi"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "VLugarObraAlmacen"
            cbObra2.DataSource = BindingSource5
            cbObra2.DisplayMember = "nombre"
            cbObra2.ValueMember = "codigo"

            BindingSource6.DataSource = BindingSource5
            BindingSource6.DataMember = "Relacion1"
            cbAlm2.DataSource = BindingSource6
            cbAlm2.DisplayMember = "ubicacion"
            cbAlm2.ValueMember = "codUbi"

            BindingSource7.DataSource = dsAlmacen
            BindingSource7.DataMember = "TEmpTransp"
            cbRazon.DataSource = BindingSource7
            cbRazon.DisplayMember = "razon"
            cbRazon.ValueMember = "codET"

            BindingSource8.DataSource = BindingSource7
            BindingSource8.DataMember = "Relacion2"
            cbMarca.DataSource = BindingSource8
            cbMarca.DisplayMember = "marcaNro"
            cbMarca.ValueMember = "codVeh"

            BindingSource9.DataSource = BindingSource7
            BindingSource9.DataMember = "Relacion3"
            cbLic.DataSource = BindingSource9
            cbLic.DisplayMember = "nroLic"
            cbLic.ValueMember = "codT"

            cbMot.DataSource = dsAlmacen
            cbMot.DisplayMember = "TMotivoGuia.motivo"
            cbMot.ValueMember = "codMotG"

            BindingSource12.DataSource = dsAlmacen
            BindingSource12.DataMember = "VGuiaRemEmpAper"
            lbOrden.DataSource = BindingSource12
            lbOrden.DisplayMember = "nro"
            lbOrden.ValueMember = "codGuiaE"
            BindingSource12.Sort = "nroGuia"

            BindingSource13.DataSource = dsAlmacen
            BindingSource13.DataMember = "VDetGuiaE"
            Navigator2.BindingSource = BindingSource13
            dgTabla2.DataSource = BindingSource13
            BindingSource13.Sort = "codDGE"
            ModificarColumnasDGV()

            configurarColorControl()

            txtRuc.DataBindings.Add("Text", BindingSource2, "ruc")
            txtRuc1.DataBindings.Add("Text", BindingSource7, "ruc")

            vfVan3 = False
            vFVan = True
            parametrosSerieDoc()

            vfVan4 = True
            visualizarGuia()

            vfVan2 = True
            enlazarText()

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

    Private Sub registraGuiaRemEmpForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVan3 = True
        visualizarDet()
    End Sub

    Private Sub cbSerie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSerie.SelectedIndexChanged
        vfVan3 = False  'no filtrar detalle
        parametrosSerieDoc()
        visualizarGuia()
        vfVan3 = True  'filtrar detalle
        visualizarDet()
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 400
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).Width = 207
            .Columns(3).HeaderText = ""
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "Unid."
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).Width = 50
            .Columns(5).HeaderText = "Peso"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Width = 60
            .Columns(9).HeaderText = "Estado"
            .Columns(9).ReadOnly = True 'NO editable
            .Columns(10).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Dim vfVan4 As Boolean = False
    Private Sub visualizarGuia()
        If vfVan4 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VGuiaRemEmpAper").Clear()
            daTabla1.SelectCommand.Parameters("@codSer").Value = cbSerie.SelectedValue
            daTabla1.Fill(dsAlmacen, "VGuiaRemEmpAper")
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("VLugarObraAlmacen").Columns("codigo"), dsAlmacen.Tables("TAlmacen").Columns("codigo"))
        dsAlmacen.Relations.Add(relation1)
        'agregando una relacion entre la tablaS
        Dim relation2 As New DataRelation("Relacion2", dsAlmacen.Tables("TEmpTransp").Columns("codET"), dsAlmacen.Tables("TVehiculo").Columns("codET"))
        dsAlmacen.Relations.Add(relation2)
        'agregando una relacion entre la tablaS
        Dim relation3 As New DataRelation("Relacion3", dsAlmacen.Tables("TEmpTransp").Columns("codET"), dsAlmacen.Tables("TTransportista").Columns("codET"))
        dsAlmacen.Relations.Add(relation3)
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
        Label10.ForeColor = ForeColorLabel
        Label18.ForeColor = ForeColorLabel
        Label19.ForeColor = ForeColorLabel
        Label20.ForeColor = ForeColorLabel
        Label21.ForeColor = ForeColorLabel
        Label22.ForeColor = ForeColorLabel
        Label23.ForeColor = ForeColorLabel
        Label24.ForeColor = ForeColorLabel
        Label25.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        GroupBox2.ForeColor = ForeColorLabel
        GroupBox3.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
        btnCierra.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnProcesa.ForeColor = ForeColorButtom
        btnCrear.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        'dgTabla2.Dispose()
        Me.Close()
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
        cmdMaxCodigo.CommandText = "select max(nroGuia) from TGuiaRemEmp where codSerS=" & codSer
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

    Private Sub txtBuscar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus, txtBuscar.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        BindingSource2.Filter = "razon like '" & txtBuscar.Text.Trim() & "%'"
        If cbProv.SelectedIndex <> -1 Then
            cbProv.SelectedIndex = 0
        End If
        'lbMenu.DroppedDown = True 'Se despliega el comboBox
    End Sub

    Private Sub cbProv_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbProv.GotFocus
        cbProv.DroppedDown = True 'Se despliega el comboBox
    End Sub

    Private Sub enlazarPartida()
        If BindingSource4.Count = 0 Then
            txtPar.Text = ""
            Exit Sub
        End If
        'txtPar.Text = BindingSource4.Item(cbAlm1.SelectedIndex)(1) + " - " + BindingSource3.Item(cbObra1.SelectedIndex)(2)
        txtPar.Text = BindingSource3.Item(cbObra1.SelectedIndex)(2)
    End Sub

    Private Sub enlazarLlegada()
        If BindingSource6.Count = 0 Then
            txtLleg.Text = ""
            Exit Sub
        End If
        'txtLleg.Text = BindingSource6.Item(cbAlm2.SelectedIndex)(1) + " - " + BindingSource5.Item(cbObra2.SelectedIndex)(2)
        txtLleg.Text = BindingSource5.Item(cbObra2.SelectedIndex)(2)
    End Sub

    Private Sub cbAlm1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAlm1.SelectedValueChanged
        enlazarPartida()
    End Sub

    Private Sub cbAlm2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAlm2.SelectedValueChanged
        enlazarLlegada()
    End Sub

    Private Sub lbOrden_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbOrden.SelectedValueChanged
        enlazarText()
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub enlazarText()
        If vfVan2 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource12.Count = 0 Then
                'desEnlazarText()
            Else
                date1.Value = BindingSource12.Item(lbOrden.SelectedIndex)(4)
                cbProv.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(6)
                cbObra1.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(17)
                cbAlm1.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(7)
                txtPar.Text = BindingSource12.Item(lbOrden.SelectedIndex)(9)
                cbObra2.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(18)
                cbAlm2.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(8)
                txtLleg.Text = BindingSource12.Item(lbOrden.SelectedIndex)(10)
                cbRazon.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(19)
                cbMarca.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(11)
                cbLic.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(12)
                cbMot.SelectedValue = BindingSource12.Item(lbOrden.SelectedIndex)(13)
                txtFac.Text = BindingSource12.Item(lbOrden.SelectedIndex)(14)
                txtObs.Text = BindingSource12.Item(lbOrden.SelectedIndex)(15)
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If BindingSource12.Position = -1 Then
                dsAlmacen.Tables("VDetGuiaE").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetGuiaE").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource12.Item(lbOrden.SelectedIndex)(0)
            daDetDoc.Fill(dsAlmacen, "VDetGuiaE")
            'colorearFila()
            colorear()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource13.Count - 1
            dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(3).Style.BackColor = Color.AliceBlue
            dgTabla2.Rows(j).Cells(5).Style.BackColor = Color.AliceBlue
            If BindingSource13.Item(j)(10) = 1 Then 'Recibido
                dgTabla2.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(9).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnProcesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesa.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource10.RemoveFilter()
        If vFClear1 Then
            dsAlmacen.Tables("VStockUbi").Clear()
            dsAlmacen.Tables("VMaterialStock").Clear()

            daVMat.Fill(dsAlmacen, "VMaterialStock")
            daVStock.Fill(dsAlmacen, "VStockUbi")

            BindingSource11.Filter = "codMat=" & BindingSource10.Item(BindingSource10.Position)(0)

            colorearFila()
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,stock,uniBase,preBase,tipoM,hist,estado,codTipM,codUni from VMaterialStock" 'material
            crearDataAdapterTable(daVMat, sele)

            sele = "select idMU,stock,ubicacion+' - '+obra as ubi,codigo,codUbi,codMat,color from VStockUbi"
            crearDataAdapterTable(daVStock, sele)
            'daVStock.SelectCommand.Parameters.Add("@codArt", SqlDbType.Int, 0).Value = 0

            daVMat.Fill(dsAlmacen, "VMaterialStock")
            daVStock.Fill(dsAlmacen, "VStockUbi")

            BindingSource10.DataSource = dsAlmacen
            BindingSource10.DataMember = "VMaterialStock"
            'Navigator1.BindingSource = BindingSource10
            dgTabla1.DataSource = BindingSource10
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource10.Sort = "material"

            BindingSource11.DataSource = dsAlmacen
            BindingSource11.DataMember = "VStockUbi"
            dgStock.DataSource = BindingSource11
            ModificarColumnasDGV1()

            BindingSource11.Filter = "codMat=" & BindingSource10.Item(BindingSource10.Position)(0)

            vFClear1 = True
            colorearFila()
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        txtBus.Focus()
        txtBus.SelectAll()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource10.Count - 1
            If BindingSource10.Item(j)(2) >= 0 Then
                'dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(2).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Width = 40
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 420
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Stock"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Unid."
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "PrecS/."
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).HeaderText = "Tipo Insumo"
            .Columns(5).Width = 120
            .Columns(5).Visible = False
            .Columns(6).Width = 1000
            .Columns(6).HeaderText = ""
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgStock
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Stock"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).HeaderText = "Ubicacion"
            .Columns(2).Width = 500
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub txtPeso_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPeso.GotFocus, txtPeso.MouseClick
        txtPeso.SelectAll()
    End Sub

    Private Sub txtCan_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.GotFocus, txtCan.MouseClick
        txtCan.SelectAll()
    End Sub

    Private Sub txtCan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan.TextChanged
        Me.AcceptButton = Me.btnAgrega
    End Sub

    Private Sub txtBus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBus.GotFocus, txtBus.MouseClick
        txtBuscar.SelectAll()
    End Sub

    Private Sub txtBus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBus.TextChanged
        Dim campo As String
        If cbBuscar.SelectedIndex = 0 Then
            campo = "material"
        End If
        If cbBuscar.SelectedIndex = 1 Then
            campo = "codMat"
        End If

        If cbBuscar.SelectedIndex = 0 Then
            'Tipo String
            BindingSource10.Filter = campo & " like '" & txtBus.Text.Trim() & "%'"
        Else
            If Not IsNumeric(txtBus.Text.Trim()) Then
                StatusBarClass.messageBarraEstado(" INGRESE DATO NUMERICO...")
                txtBus.SelectAll()
                Exit Sub
            End If
            BindingSource10.Filter = campo & "=" & txtBus.Text.Trim()
        End If
        If BindingSource10.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnAgrega
            colorearFila()
        Else
            'txtBus.Focus()
            'txtBus.SelectAll()
            StatusBarClass.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
    End Sub

    Private Sub btnCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Dim crea As New crearMaterialForm
        crea.ShowDialog()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        If BindingSource10.Position >= 0 Then
            BindingSource11.Filter = "codMat=" & BindingSource10.Item(BindingSource10.Position)(0)
        Else
            BindingSource11.Filter = "codMat=" & "0"
        End If
    End Sub

    Private Sub desactivarControles()
        date1.Enabled = False
        txtBuscar.ReadOnly = True
        cbProv.Enabled = False
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        cbMot.Enabled = False
        txtFac.ReadOnly = True
        txtObs.ReadOnly = True
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        Panel3.Enabled = False
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
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        Panel3.Enabled = True
        Panel4.Enabled = True
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

    Private Sub activarControles()
        date1.Enabled = True
        txtBuscar.ReadOnly = False
        cbProv.Enabled = True
        GroupBox1.Enabled = True
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
        cbMot.Enabled = True
        txtFac.ReadOnly = False
        txtObs.ReadOnly = False
    End Sub

    Private Sub limpiarText()
        date1.Value = Now.Date
        txtFac.Clear()
        txtObs.Clear()
    End Sub

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If CDbl(txtCan.Text.Trim()) > CDbl(txtSto.Text) Then
        'MessageBox.Show("Proceso denegado, NO existe STOCK...", nomNegocio, Nothing, MessageBoxIcon.Information)
        'Exit Sub
        'End If
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            activarControles()
            limpiarText()
            txtBuscar.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
            dsAlmacen.Tables("VDetGuiaE").Clear()
        Else   ' guardar
            If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
                MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If BindingSource2.Position = -1 Then
                MessageBox.Show("selecione destinatario valido...", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtBuscar.Focus()
                Exit Sub
            End If

            If cbAlm1.SelectedValue = cbAlm2.SelectedValue Then
                MessageBox.Show("Proceso denegado, almacén de salida tiene que ser diferente con almacén de llegada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de aperturar Guia de Remisión Nº " & cbSerie.Text & " - " & txtNro.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

                'TGuiaRemEmp
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
                Dim codGuiaE As Integer = cmInserTable.Parameters("@Identity").Value

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan2 = False 'Enlazar Text
                vfVan3 = False 'Detalle

                'Actualizando el dataTable
                parametrosSerieDoc()
                visualizarGuia()

                BindingSource2.RemoveFilter()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource12.Position = BindingSource12.Find("codGuiaE", codGuiaE)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan2 = True
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
        cmInserTable.CommandText = "PA_InsertGuiaRemEmp"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@tal", SqlDbType.VarChar, 5).Value = cbSerie.Text.Trim()
        cmInserTable.Parameters.Add("@nroG", SqlDbType.Int, 0).Value = txtNro.Text.Trim()
        cmInserTable.Parameters.Add("@fecI", SqlDbType.Date).Value = date1.Value.Date
        cmInserTable.Parameters.Add("@codSer", SqlDbType.Int, 0).Value = cbSerie.SelectedValue
        cmInserTable.Parameters.Add("@codIde", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0 'abierto
        cmInserTable.Parameters.Add("@codUbiO", SqlDbType.Int, 0).Value = cbAlm1.SelectedValue
        cmInserTable.Parameters.Add("@codUbiD", SqlDbType.Int, 0).Value = cbAlm2.SelectedValue
        cmInserTable.Parameters.Add("@par", SqlDbType.VarChar, 100).Value = txtPar.Text.Trim()
        cmInserTable.Parameters.Add("@lleg", SqlDbType.VarChar, 100).Value = txtLleg.Text.Trim()
        cmInserTable.Parameters.Add("@codET", SqlDbType.Int, 0).Value = cbRazon.SelectedValue
        cmInserTable.Parameters.Add("@codV", SqlDbType.Int, 0).Value = cbMarca.SelectedValue
        cmInserTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbLic.SelectedValue
        cmInserTable.Parameters.Add("@codMot", SqlDbType.Int, 0).Value = cbMot.SelectedValue
        cmInserTable.Parameters.Add("@nroFact", SqlDbType.VarChar, 30).Value = txtFac.Text.Trim()
        cmInserTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmInserTable.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = vPass
        cmInserTable.Parameters.Add("@hist", SqlDbType.VarChar, 200).Value = "Creo " & Now.Date & " " & vPass & "-" & vSUsuario
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If validaCampoVacioMinCaracNoNumer(txtPar.Text.Trim, 3) Then
            'txtPar.errorProv()
            MessageBox.Show("Digite partida valida...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Return True
        End If
        If validaCampoVacioMinCaracNoNumer(txtLleg.Text.Trim, 3) Then
            MessageBox.Show("Digite llegada valida...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Dim vfModificar1 As String = "modificar"
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If vfModificar1 = "modificar" Then
            If BindingSource12.Position = -1 Then
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
            'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
            If ValidarCampos() Then
                Exit Sub
            End If

            If BindingSource2.Position = -1 Then
                MessageBox.Show("selecione destinatario valido...", nomNegocio, Nothing, MessageBoxIcon.Error)
                txtBuscar.Focus()
                Exit Sub
            End If

            If cbAlm1.SelectedValue = cbAlm2.SelectedValue Then
                MessageBox.Show("Proceso denegado, almacén de salida tiene que ser diferente con almacén de llegada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TGuiaRemEmp
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
                Dim codGuiaE As Integer = BindingSource12.Item(BindingSource12.Position)(0)

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True
                vfVan2 = False 'Enlazar Text
                vfVan3 = False

                'Actualizando el dataTable
                parametrosSerieDoc()
                visualizarGuia()

                BindingSource2.RemoveFilter()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource12.Position = BindingSource12.Find("codGuiaE", codGuiaE)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

                vfVan2 = True
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

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TGuiaRemEmp set fecIni=@fec,codIde=@codI,codUbiOri=@codUbi1,codUbiDes=@codUbi2,partida=@par,llegada=@lleg,codET=@codE,codVeh=@codV,codT=@codT,codMotG=@codM,nroFact=@nroFac,obs=@obs,hist=@hist where codGuiaE=@nro"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@fec", SqlDbType.Date).Value = date1.Value.Date
        cmUpdateTable.Parameters.Add("@codI", SqlDbType.Int, 0).Value = cbProv.SelectedValue
        cmUpdateTable.Parameters.Add("@codUbi1", SqlDbType.Int, 0).Value = cbAlm1.SelectedValue
        cmUpdateTable.Parameters.Add("@codUbi2", SqlDbType.Int, 0).Value = cbAlm2.SelectedValue
        cmUpdateTable.Parameters.Add("@par", SqlDbType.VarChar, 100).Value = txtPar.Text.Trim()
        cmUpdateTable.Parameters.Add("@lleg", SqlDbType.VarChar, 100).Value = txtLleg.Text.Trim()
        cmUpdateTable.Parameters.Add("@codE", SqlDbType.Int, 0).Value = cbRazon.SelectedValue
        cmUpdateTable.Parameters.Add("@codV", SqlDbType.Int, 0).Value = cbMarca.SelectedValue
        cmUpdateTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = cbLic.SelectedValue
        cmUpdateTable.Parameters.Add("@codM", SqlDbType.Int, 0).Value = cbMot.SelectedValue
        cmUpdateTable.Parameters.Add("@nroFac", SqlDbType.VarChar, 30).Value = txtFac.Text.Trim()
        cmUpdateTable.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = txtObs.Text.Trim()
        cmUpdateTable.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = BindingSource12.Item(BindingSource12.Position)(20) & "  Modifico " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource12.Item(BindingSource12.Position)(0)
    End Sub

    Private Function recuperarUltimoDoc(ByVal codSer As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroGuia) from TGuiaRemEmp where codSerS=" & codSer
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCount(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TDetalleGuiaEmp where codGuiaE=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If BindingSource12.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarUltimoDoc(BindingSource12.Item(BindingSource12.Position)(5)) <> CInt(BindingSource12.Item(BindingSource12.Position)(3)) Then
            MessageBox.Show("No se puede ELIMINAR por no ser la ultima GUIA DE REMISION ingresada...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If (recuperarCount(BindingSource12.Item(BindingSource12.Position)(0)) > 0) Then
            StatusBarClass.messageBarraEstado("  PROCESO DENEGADO, Guia de remisión tiene registros en detalle guia...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de eliminar Guia de Remisión Nº " & BindingSource12.Item(BindingSource12.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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
            'Tabla TGuiaRemEmp
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
            vfVan2 = False 'Enlazar Text
            vfVan3 = False

            'Actualizando el dataTable
            parametrosSerieDoc()
            visualizarGuia()

            BindingSource2.RemoveFilter()

            vfVan2 = True
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

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TGuiaRemEmp where codGuiaE=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource12.Item(BindingSource12.Position)(0)
    End Sub

    Private Function ValidarCampos1() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan.Text) Then
            txtCan.errorProv()
            Return True
        End If

        If ValidaNroMayorOigualCero(txtPeso.Text) Then
            txtPeso.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Sub btnAgrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgrega.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a agregar...")
            Exit Sub
        End If

        If BindingSource12.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso denegado, Guia de remisión NO APERTURADA...")
            Exit Sub
        End If

        If ValidarCampos1() Then
            Exit Sub
        End If

        If BindingSource13.Find("codMat", BindingSource10.Item(BindingSource10.Position)(0)) >= 0 Then
            MessageBox.Show("Ya exíste insumo: " & BindingSource10.Item(BindingSource10.Position)(1), nomNegocio, Nothing, MessageBoxIcon.Information)
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

            'TDetalleGuiaEmp
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
            Dim codDGE As Integer = cmInserTable1.Parameters("@Identity").Value

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource13.Position = BindingSource13.Find("codDGE", codDGE)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            txtLinea.Clear()
            txtBus.Focus()
            txtBus.SelectAll()
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
        cmInserTable1.CommandText = "PA_InsertDetalleGuiaEmp"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@cod", SqlDbType.VarChar, 20).Value = ""
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = txtCan.Text
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = BindingSource10.Item(BindingSource10.Position)(1)
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = BindingSource10.Item(BindingSource10.Position)(3)
        cmInserTable1.Parameters.Add("@peso", SqlDbType.Decimal, 0).Value = txtPeso.Text
        cmInserTable1.Parameters.Add("@codG", SqlDbType.Int, 0).Value = BindingSource12.Item(BindingSource12.Position)(0)
        cmInserTable1.Parameters.Add("@codM", SqlDbType.Int, 0).Value = BindingSource10.Item(BindingSource10.Position)(0)
        cmInserTable1.Parameters.Add("@linea", SqlDbType.VarChar, 300).Value = txtLinea.Text.Trim()
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        If BindingSource13.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe linea de insumo a eliminar...")
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de eliminar: " & BindingSource13.Item(BindingSource13.Position)(2) & "  Si elimina no podra deshacer...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDetalleGuiaEmp
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
        cmDeleteTable3.CommandText = "delete from TDetalleGuiaEmp where codDGE=@cod"
        cmDeleteTable3.Connection = Cn
        cmDeleteTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource13.Item(BindingSource13.Position)(0)
    End Sub

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        btnAgrega.PerformClick()
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
                        'BindingSource5.Item(BindingSource5.Position)(5) = Format(CDbl(BindingSource5.Item(BindingSource5.Position)(4)) * valor, "0.00")
                    End If
                End If
            End If

            If dgTabla2.Columns(e.ColumnIndex).Name = "peso" Then
                If Not IsNumeric(e.FormattedValue) Then
                    dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO"
                    e.Cancel = True
                Else
                    Dim valor As Double = e.FormattedValue 'peso
                    If CDbl(e.FormattedValue) < 0 Then
                        dgTabla2.Rows(e.RowIndex).ErrorText = "ERROR, DIGITE VALOR NUMERICO > 0"
                        e.Cancel = True
                    Else
                        'BindingSource5.Item(BindingSource5.Position)(5) = Format(CDbl(BindingSource5.Item(BindingSource5.Position)(1)) * valor, "0.00")
                    End If
                End If
            End If
        Catch f As Exception
            MessageBox.Show("Tipo de exception: " & f.Message, nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    Private Sub TSModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSModificar.Click
        If BindingSource13.Count = 0 Then
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

            For j As Short = 0 To BindingSource13.Count - 1
                'actualizando TDetalleGuiaEmp
                comandoUpdate3(BindingSource13.Item(j)(1), BindingSource13.Item(j)(5), BindingSource13.Item(j)(3).ToString(), BindingSource13.Item(j)(0))
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
    Private Sub comandoUpdate3(ByVal can As Decimal, ByVal peso As Decimal, ByVal linea As String, ByVal codDGE As Integer)
        cmdUpdateTable3 = New SqlCommand
        cmdUpdateTable3.CommandType = CommandType.Text
        cmdUpdateTable3.CommandText = "update TDetalleGuiaEmp set cant=@can,peso=@peso,linea1=@lin where codDGE=@cod"
        cmdUpdateTable3.Connection = Cn
        cmdUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = can
        cmdUpdateTable3.Parameters.Add("@peso", SqlDbType.Decimal, 0).Value = peso
        cmdUpdateTable3.Parameters.Add("@lin", SqlDbType.VarChar, 300).Value = linea
        cmdUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codDGE
    End Sub

    Private Sub btnAnula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnula.Click
        If BindingSource12.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe Guia de Remision a ANULAR...")
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de ANULAR Guia de Remisión" & Chr(13) & " Nº " & BindingSource12.Item(BindingSource12.Position)(1), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TGuiaRemEmp
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
            vfVan2 = False 'Enlazar Text
            vfVan3 = False

            'Actualizando el dataTable
            parametrosSerieDoc()
            visualizarGuia()

            BindingSource2.RemoveFilter()

            vfVan2 = True
            enlazarText()

            vfVan3 = True
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
    Private Sub comandoUpdate13()
        cmUpdateTable13 = New SqlCommand
        cmUpdateTable13.CommandType = CommandType.Text
        cmUpdateTable13.CommandText = "update TGuiaRemEmp set estado=@est,hist=@hist where codGuiaE=@cod"
        cmUpdateTable13.Connection = Cn
        cmUpdateTable13.Parameters.Add("@est", SqlDbType.Int, 0).Value = 3 '3 = anulado
        cmUpdateTable13.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = BindingSource12.Item(BindingSource12.Position)(20) & " ANULO " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable13.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource12.Item(BindingSource12.Position)(0)
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource12.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Guia de Remisión...")
            Exit Sub
        End If

        vCodDoc = BindingSource12.Item(BindingSource12.Position)(0)
      
        Dim informe As New ReportViewerGuiaRemEForm
        informe.ShowDialog()
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

    Private Sub txtPeso_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPeso.KeyPress
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
