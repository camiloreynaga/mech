Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class InformeMovCajaChicaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

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

        sele = "select nroMC,movimiento,fecha,nroOrd,simbolo,montoEnt,nroSol,simbolo,montoSal,saldoMov,tipoP,nroP,nomPers,descrip,nombre,nomCaja,codDia,codTM,codigo,codCC,idOP,codUsu,codMon,codSC,codPers,codPagD,codCaj from VMovimientoCaja where codDia>=@codDia1 and codDia<=@codDia2 and codCaj=@codCC"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@codDia1", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codDia2", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codCC", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VDiaCaja")
            daTabla2.Fill(dsAlmacen, "VCajaObra")
            daVKardex.Fill(dsAlmacen, "VMovimientoCaja")

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
            ModificarColumnasDGV()

            configurarColorControl()

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
            .Columns(10).Width = 100
            .Columns(10).HeaderText = "Tipo_Pago"
            .Columns(11).Width = 60
            .Columns(11).HeaderText = "NºOper."
            .Columns(12).Width = 100
            .Columns(12).HeaderText = "Pers._Solicita"
            .Columns(13).Width = 300
            .Columns(13).HeaderText = "Nota"
            .Columns(14).Width = 300
            .Columns(14).HeaderText = "Sede / Obra"
            .Columns(15).Width = 120
            .Columns(15).HeaderText = "Usuario_Caja"
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
        visualizarKardex()
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource3.Count - 1
            If BindingSource3.Item(j)(17) = 1 Then 'INGRESO
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource3.Item(j)(17) = 2 Then 'EGRESO
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
End Class
