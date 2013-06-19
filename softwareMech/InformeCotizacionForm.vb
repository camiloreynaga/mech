Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class InformeCotizacionForm
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

    Private Sub InformeCotizacionForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub InformeCotizacionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1 from TIdentidad where estado=1 and idTipId=2" ' '2=proveedor
        crearDataAdapterTable(daTabla1, sele)

        sele = "select distinct codigo,nombre,lugar,color from VObra"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select distinct codGruC,grupo,descrip,nroGru,estGru from VCotOrden VGrupoCot order by codGruC"
        crearDataAdapterTable(daTabla3, sele)

        sele = "select codCot,nroCot,nro,codIde,fecCot,tiempoVig,atencion,plazo,codPag,lugarEnt,incluir,codPersS,codigo,codPers,obs,idSol,codGruC,estado,forma,nom,nom1,fono,email,isnull(NroSol,'') as NroSol,codMon,moneda from VCotOrden" 'order by nroCot"
        crearDataAdapterTable(daTabla4, sele)

        sele = "select codDetC,cant,unidad,descrip,precio,subTotal,est,codCot,codMat,estado from VDetCot where codCot=@idS" 'areaM,descrip
        crearDataAdapterTable(daTabla5, sele)
        daTabla5.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'llenar la tabla virtual con los dataAdapter
            daTabla1.Fill(dataTable1)
            daTabla2.Fill(dataTable2)
            daTabla3.Fill(dataTable3)
            daTabla4.Fill(dataTable4)
            daTabla5.Fill(dataTable5)

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

    Private Sub InformeCotizacionForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
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
            daTabla5.SelectCommand.Parameters("@idS").Value = bindingSource4.Item(lbCot.SelectedIndex)(0)
            daTabla5.Fill(dataTable5)
            colorearFila()
            sumTotal()
        End If
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource5.Count - 1
            If bindingSource5.Item(j)(9) = 1 Then 'Aprobado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
            If bindingSource5.Item(j)(9) = 2 Then 'Rechazado
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
        txtTotal.Text = dataTable5.Compute("Sum(subTotal)", Nothing)
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        vCodDoc = bindingSource4.Item(bindingSource4.Position)(0)
        vParam1 = bindingSource4.Item(bindingSource4.Position)(2) & "-MECH-" & CDate(bindingSource4.Item(bindingSource4.Position)(4)).Year
        Dim informe As New ReportViewerCotizacionForm
        informe.ShowDialog()
    End Sub

    Private Sub txtOrden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrden.Click

    End Sub
End Class
