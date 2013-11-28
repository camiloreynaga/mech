Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class InformeMovCajaChicaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource5 As New BindingSource
    Dim BindingSource6 As New BindingSource

    Private Sub InformeMovCajaChicaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub InformeMovCajaChicaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb

        Dim sele As String = "select codDia,ltrim(str(codDia))+' - '+convert(varchar(10),fecha,103) as sesion,fecha,estado,codigo from VDiaCaja where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codCaj,codMon,simbolo,saldo,codPers,codSerO,codCC,caja from VCajaObra where codigo=@cod" 'order by nroDes"
        crearDataAdapterTable(daTabla2, sele)
        daTabla2.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select nroMC,movimiento,fecha,nroOrd,simbolo,montoEnt,nroSol,simbolo,montoSal,saldoMov,banco,tipoP,nroP,nomPers,descrip,nombre,nomCaja,codDia,codTM,codigo,codCC,idOP,codUsu,codMon,codSC,codPers,codPagD,codCaj,codBan from VMovimientoCaja where codDia>=@codDia1 and codDia<=@codDia2 and codCaj=@codCC"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@codDia1", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codDia2", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codCC", SqlDbType.Int, 0).Value = 0

        sele = "select codSC,nro,fechaSol,nomSoli,montoSol,imprevisto,salAnt,totPar,requerimiento,montoRen,salAct,est,nomObra,estSol,codObra,codSede,codPers from VSolCajaTodo where codSC=@codSC"
        crearDataAdapterTable(daTabla3, sele)
        daTabla3.SelectCommand.Parameters.Add("@codSC", SqlDbType.Int, 0).Value = 0

        sele = "select codDetSol,tipoM,insumo,cant1,prec1,totPar,comp,fecha,comp1,nroOtros,cant2,prec2,totReal,estRen1,nom,obsRen,ingre,areaM,codRen,codMat,codAreaM,codTipM,codSC,codDC,compCheck,estRen,compRen,ingreso,obsSol from VDetSolCajaCuenta where codSC=@cod"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VDiaCaja")
            daTabla2.Fill(dsAlmacen, "VCajaObra")
            daVKardex.Fill(dsAlmacen, "VMovimientoCaja")

            daTabla3.Fill(dsAlmacen, "VSolCajaTodo")
            daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")

            BindingSource6.DataSource = dsAlmacen
            BindingSource6.DataMember = "VSolCajaTodo"

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VDiaCaja"
            lbIni.DataSource = BindingSource1
            lbIni.DisplayMember = "sesion"
            lbIni.ValueMember = "codDia"
            BindingSource1.Sort = "codDia"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDiaCaja"
            lbFin.DataSource = BindingSource2
            lbFin.DisplayMember = "sesion"
            lbFin.ValueMember = "codDia"
            BindingSource2.Sort = "codDia"

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VCajaObra"
            cbCaja.DataSource = BindingSource4
            cbCaja.DisplayMember = "caja"
            cbCaja.ValueMember = "codCaj"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VMovimientoCaja"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            BindingSource3.Sort = "nroMC"

            BindingSource5.DataSource = dsAlmacen
            BindingSource5.DataMember = "VDetSolCajaCuenta"
            Navigator3.BindingSource = BindingSource5
            dgTabla3.DataSource = BindingSource5
            BindingSource5.Sort = "codTipM,codDetSol"
            ModificarColumnasDGV()

            configurarColorControl()

            txtNro.DataBindings.Add("Text", BindingSource6, "nro")
            txtFec.DataBindings.Add("Text", BindingSource6, "fechaSol")
            txtSol.DataBindings.Add("Text", BindingSource6, "nomSoli")
            txtObra.DataBindings.Add("Text", BindingSource6, "nomObra")
            txtTotIns.DataBindings.Add("Text", BindingSource6, "montoSol")
            txtImpre.DataBindings.Add("Text", BindingSource6, "imprevisto")
            txtSalAnt.DataBindings.Add("Text", BindingSource6, "salAnt")
            txtTotReq.DataBindings.Add("Text", BindingSource6, "totPar")
            txtReq.DataBindings.Add("Text", BindingSource6, "requerimiento")
            txtTotEgr.DataBindings.Add("Text", BindingSource6, "montoRen")
            txtSalAct.DataBindings.Add("Text", BindingSource6, "salAct")

            BindingSource1.MoveLast()
            BindingSource2.MoveLast()

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

    Private Sub InformeMovCajaChicaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVAn1 = True
        Panel3.Visible = False  'ocultando el datagrid de detalle de requrimientos
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Movimiento"
            .Columns(1).Width = 140
            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = "NºDes"
            .Columns(3).Width = 60
            .Columns(4).HeaderText = ""
            .Columns(4).Width = 25
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "MonIngreso"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "#,##0.00"
            .Columns(6).HeaderText = "NºSol"
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).HeaderText = ""
            .Columns(7).Width = 25
            .Columns(8).Width = 80
            .Columns(8).HeaderText = "MonEgreso"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "#,##0.00"
            .Columns(9).Width = 80
            .Columns(9).HeaderText = "Saldo_Fecha"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "#,##0.00"
            .Columns(10).Width = 70
            .Columns(10).HeaderText = "Banco"
            .Columns(11).Width = 80
            .Columns(11).HeaderText = "Tipo_Pago"
            .Columns(11).Visible = False
            .Columns(12).Width = 60
            .Columns(12).HeaderText = "NºOper."
            .Columns(13).Width = 100
            .Columns(13).HeaderText = "Pers._Solicita"
            .Columns(14).Width = 300
            .Columns(14).HeaderText = "Nota"
            .Columns(15).Width = 300
            .Columns(15).HeaderText = "Sede / Obra"
            .Columns(16).Width = 120
            .Columns(16).HeaderText = "Usuario_Caja"
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
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With

        With dgTabla3
            .Columns(0).Visible = False
            .Columns(1).Width = 80
            .Columns(1).HeaderText = "Tipo_Insumo"
            .Columns(1).ReadOnly = True 'NO editable
            .Columns(2).HeaderText = "Descripción Insumo"
            .Columns(2).Width = 280
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Cant"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "PreUni"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).Width = 55
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "#,##0.00"
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Entrega"
            .Columns(6).Width = 55
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(6).Visible = False
            .Columns(7).HeaderText = "Fec_Doc."
            .Columns(7).Width = 70
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
            .Columns(10).ReadOnly = True 'NO editable
            .Columns(11).Width = 55
            .Columns(11).HeaderText = "PrecUni"
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(11).ReadOnly = True 'NO editable
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
        GroupBox2.ForeColor = ForeColorLabel
        GroupBox3.ForeColor = ForeColorLabel
        btnProcesar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub visualizarKardex()
        If lbIni.SelectedValue > lbFin.SelectedValue Then
            MessageBox.Show("ACCESO DENEGADO, ID FECHA => " & lbIni.Text.Trim() & " TIENE QUE SER MAYOR O IGUAL A ID FECHA => " & lbFin.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
            lbIni.Focus()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VMovimientoCaja").Clear()
        daVKardex.SelectCommand.Parameters("@codDia1").Value = lbIni.SelectedValue
        daVKardex.SelectCommand.Parameters("@codDia2").Value = lbFin.SelectedValue
        daVKardex.SelectCommand.Parameters("@codCC").Value = BindingSource4.Item(BindingSource4.Position)(0)
        daVKardex.Fill(dsAlmacen, "VMovimientoCaja")

        If BindingSource3.Count = 0 Then
            MessageBox.Show("NO EXISTE MOVIMIENTOS EN FECHAS SELECCIONADAS...", nomNegocio, Nothing, MessageBoxIcon.Information)
        Else
        End If

        vParam1 = cbCaja.Text.Trim()
        vFec1 = BindingSource1.Item(BindingSource1.Position)(2)
        vFec2 = BindingSource2.Item(BindingSource2.Position)(2)
        vX3 = lbIni.SelectedValue
        vX4 = lbFin.SelectedValue
        vX2 = BindingSource4.Item(BindingSource4.Position)(0)

        BindingSource3.MoveLast()  'MOVER AL ULTIMO REGISTRO
        colorear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        vfVAn1 = False
        Panel3.Visible = False  'ocultando el datagrid de detalle de requrimientos
        visualizarKardex()
        vfVAn1 = True
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(18) = 1 Then 'INGRESO
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(18) = 2 Then 'EGRESO
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            dgTabla2.Rows(j).Cells(5).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(8).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(9).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub btnImp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImp.Click
        If BindingSource3.Count = 0 Then
            MessageBox.Show("NO EXISTE MOVIMIENTOS...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim informe As New ReportViewerInf1CajaForm
        informe.ShowDialog()
    End Sub

    Dim vfVAn1 As Boolean = False
    Dim codSC As Integer
    Private Sub visualizarDet()
        If BindingSource3.Position = -1 Then
            Exit Sub
        End If

        If IsNumeric(BindingSource3.Item(BindingSource3.Position)(24)) Then
            codSC = BindingSource3.Item(BindingSource3.Position)(24)
        Else
            codSC = 0
        End If

        If codSC = 0 Then
            MessageBox.Show("Solo operaciones de Egreso se pueden visualizar...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If
        Panel3.Visible = True
        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VSolCajaTodo").Clear()
        daTabla3.SelectCommand.Parameters("@codSC").Value = codSC
        daTabla3.Fill(dsAlmacen, "VSolCajaTodo")

        dsAlmacen.Tables("VDetSolCajaCuenta").Clear()
        daDetDoc.SelectCommand.Parameters("@cod").Value = codSC
        daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")

        colorearFila()
        sumTotal()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource5.Count - 1
            If BindingSource5.Item(j)(25) = 1 Then 'OK
                dgTabla3.Rows(j).Cells(13).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla3.Rows(j).Cells(13).Style.ForeColor = Color.White
            End If
            If BindingSource5.Item(j)(25) = 2 Then 'Observado
                dgTabla3.Rows(j).Cells(13).Style.BackColor = Color.Yellow
                dgTabla3.Rows(j).Cells(13).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
            txtTotal.Text = "0.00"
            txtTotal1.Text = "0.00"
            Exit Sub
        End If

        txtTotal.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totPar)", Nothing)), "0,0.00")
        txtTotal1.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", Nothing)), "0,0.00")
    End Sub

    Private Sub ToolStripButton9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        Panel3.Visible = False  'ocultando el datagrid de detalle de requrimientos
    End Sub

    Private Sub dgTabla2_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla2.CellDoubleClick
        If vfVAn1 Then
            visualizarDet()
        End If
    End Sub
End Class
