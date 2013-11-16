Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class informeSolicitudCajaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub informeSolicitudCajaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub informeSolicitudCajaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSC,nro,fechaSol,nomSoli,montoSol,imprevisto,salAnt,totPar,requerimiento,montoRen,salAct,est,nomObra,estSol,codObra,codSede,codPers from VSolCajaTodo where codPers=@codP or codObra=@codO or (codPers=@codP1 and codObra=@codO1)"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codP", SqlDbType.Int, 0).Value = 0
        daTabla1.SelectCommand.Parameters.Add("@codO", SqlDbType.VarChar, 10).Value = "ras1313"
        daTabla1.SelectCommand.Parameters.Add("@codP1", SqlDbType.Int, 0).Value = 0
        daTabla1.SelectCommand.Parameters.Add("@codO1", SqlDbType.VarChar, 10).Value = "ras1313"

        sele = "select distinct codPers,nomSoli from VSolCajaTodo TPersonal order by nomSoli"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select distinct codObra,nomObra from VSolCajaTodo TObra"
        crearDataAdapterTable(daTabla3, sele)

        sele = "select codDetSol,tipoM,insumo,cant1,prec1,totPar,comp,fecha,comp1,nroOtros,cant2,prec2,totReal,estRen1,nom,obsRen,ingre,areaM,codRen,codMat,codAreaM,codTipM,codSC,codDC,compCheck,estRen,compRen,ingreso,obsSol from VDetSolCajaCuenta where codSC=@cod"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@cod", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VSolCajaTodo")
            daTabla2.Fill(dsAlmacen, "TPersonal")
            daTabla3.Fill(dsAlmacen, "TObra")
            daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VSolCajaTodo"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "codSC"

            cbSol.DataSource = dsAlmacen
            cbSol.DisplayMember = "TPersonal.nomSoli"
            cbSol.ValueMember = "codPers"

            cbObra.DataSource = dsAlmacen
            cbObra.DisplayMember = "TObra.nomObra"
            cbObra.ValueMember = "codObra"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetSolCajaCuenta"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "codTipM,codDetSol"
            ModificarColumnasDGV()

            configurarColorControl()

            RB1.Checked = True

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try
    End Sub

    Private Sub colorearFila1()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(13) = 1 Then 'Aprobado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(11).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(13) = 2 Then 'Procesado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(11).Style.ForeColor = Color.Red
            End If
            If BindingSource1.Item(j)(13) = 3 Then 'Anulado
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Red
                dgTabla1.Rows(j).Cells(11).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(13) = 4 Then 'Ok Rendido
                dgTabla1.Rows(j).Cells(11).Style.BackColor = Color.Black
                dgTabla1.Rows(j).Cells(11).Style.ForeColor = Color.White
            End If
            dgTabla1.Rows(j).Cells(7).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla1.Rows(j).Cells(8).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla1.Rows(j).Cells(9).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºSol"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fech_Sol"
            .Columns(2).Width = 70
            .Columns(3).Width = 120
            .Columns(3).HeaderText = "Solicitante"
            .Columns(4).HeaderText = "TotInsumo"
            .Columns(4).Width = 60
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Format = "N2"
            .Columns(5).HeaderText = "Imprevisto"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "N2"
            .Columns(6).HeaderText = "SaldAnt."
            .Columns(6).Width = 53
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "N2"
            .Columns(7).HeaderText = "CajaEgreso"
            .Columns(7).Width = 80
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "N2"
            .Columns(8).HeaderText = "Requer.Gasto"
            .Columns(8).Width = 80
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "N2"
            .Columns(9).HeaderText = "TotRendido"
            .Columns(9).Width = 80
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "N2"
            .Columns(10).HeaderText = "SaldAct."
            .Columns(10).Width = 53
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Format = "N2"
            .Columns(11).Width = 80
            .Columns(11).HeaderText = "Estado"
            .Columns(12).Width = 198
            .Columns(12).HeaderText = "Obra Gasto"
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
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
        GroupBox1.ForeColor = ForeColorLabel
        GroupBox2.ForeColor = ForeColorLabel
        GroupBox3.ForeColor = ForeColorLabel
        btnProcesar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged, RB2.CheckedChanged, RB3.CheckedChanged
        If RB1.Checked = True Then 'por solicitante y obra
            Label1.Visible = True
            cbObra.Visible = True
            cbSol.Visible = True
        End If
        If RB2.Checked = True Then 'solicitante
            Label1.Visible = True
            Label1.Text = "Solicitante:"
            cbObra.Visible = False
            cbSol.Visible = True
        End If
        If RB3.Checked = True Then 'Obra
            Label1.Visible = True
            Label1.Text = "Obra:"
            cbObra.Visible = True
            cbSol.Visible = False
        End If
    End Sub

    Dim vfVan1 As Boolean = False
    Dim codPers As Integer
    Dim codPers1 As Integer
    Dim codigo As String
    Dim codigo1 As String
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        vfVan1 = False

        If RB1.Checked = True Then 'obra y solicitante
            codPers = 0
            codigo = "ras1313"
            codPers1 = cbSol.SelectedValue
            codigo1 = cbObra.SelectedValue
        End If
        If RB2.Checked = True Then 'solicitante
            codPers = cbSol.SelectedValue
            codigo = "ras1313"
            codPers1 = 0
            codigo1 = "ras1313"
        End If
        If RB3.Checked = True Then 'Obra
            codPers = 0
            codigo = cbObra.SelectedValue
            codPers1 = 0
            codigo1 = "ras1313"
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VSolCajaTodo").Clear()
        daTabla1.SelectCommand.Parameters("@codP").Value = codPers
        daTabla1.SelectCommand.Parameters("@codO").Value = codigo
        daTabla1.SelectCommand.Parameters("@codP1").Value = codPers1
        daTabla1.SelectCommand.Parameters("@codO1").Value = codigo1
        daTabla1.Fill(dsAlmacen, "VSolCajaTodo")

        If BindingSource1.Count = 0 Then
            MessageBox.Show("NO EXISTE SOLICITUDES...", nomNegocio, Nothing, MessageBoxIcon.Information)
        Else
        End If
        BindingSource1.MoveLast()  'MOVER AL ULTIMO REGISTRO

        vfVan1 = True
        visualizarDet()
        colorearFila1() 'Tabla maestro
        'colorearFila()  'Tabla detalle

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                dsAlmacen.Tables("VDetSolCajaCuenta").Clear()
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetSolCajaCuenta").Clear()
            daDetDoc.SelectCommand.Parameters("@cod").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VDetSolCajaCuenta")
            colorearFila()
            sumTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(25) = 1 Then 'OK
                dgTabla2.Rows(j).Cells(13).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(13).Style.ForeColor = Color.White
            End If
            If BindingSource2.Item(j)(25) = 2 Then 'Observado
                dgTabla2.Rows(j).Cells(13).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(13).Style.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
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

        If BindingSource2.Find("compRen", 1) <> -1 Then 'Factura
            txtFac.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=1")), "0,0.00")
        Else
            txtFac.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 2) <> -1 Then 'Factura
            txtBol.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=2")), "0,0.00")
        Else
            txtBol.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 3) <> -1 Then 'Factura
            txtHon.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=3")), "0,0.00")
        Else
            txtHon.Text = "0.00"
        End If

        If BindingSource2.Find("compRen", 4) <> -1 Then 'Factura
            txtOtro.Text = Format((dsAlmacen.Tables("VDetSolCajaCuenta").Compute("Sum(totReal)", "compRen=4")), "0,0.00")
        Else
            txtOtro.Text = "0.00"
        End If
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
