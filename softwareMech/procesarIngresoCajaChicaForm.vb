Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class procesarIngresoCajaChicaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource

    Private Sub procesarIngresoCajaChicaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub procesarIngresoCajaChicaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codPagD,nro,fecPago,simbolo,montoPago,vCaja,tipoP,nroP,datoReq,nomGer,nomSol,vanCaja,estado,idOP,codMon,codSerO,codPersGer,codPersSol,codTipP,nroDes from VOrdenDesembCajaIngreso where codSerO=@ser" 'order by nroDes"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@ser", SqlDbType.Int, 0).Value = vSCodSerO

        sele = "select codCaj,codMon,simbolo,saldo,codPers,codSerO,codCC,caja from VCajaObra where codigo=@cod" 'order by nroDes"
        crearDataAdapterTable(daTabla2, sele)
        daTabla2.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select nroMC,movimiento,fecha,nroOrd,simbolo,montoEnt,nroSol,simbolo,montoSal,saldoMov,tipoP,nroP,nomPers,descrip,nombre,nomCaja,codDia,codTM,codigo,codCC,idOP,codUsu,codMon,codSC,codPers,codPagD,codCaj from VMovimientoCaja where codDia=@codDia and codCaj=@codCC"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@codDia", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codCC", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VOrdenDesembCajaIngreso")
            daTabla2.Fill(dsAlmacen, "VCajaObra")
            daVKardex.Fill(dsAlmacen, "VMovimientoCaja")

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VCajaObra"
            cbCaja.DataSource = BindingSource2
            cbCaja.DisplayMember = "caja"
            cbCaja.ValueMember = "codCaj"

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VOrdenDesembCajaIngreso"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource1.Sort = "nroDes,codPagD"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VMovimientoCaja"
            Navigator2.BindingSource = BindingSource3
            dgTabla2.DataSource = BindingSource3
            BindingSource3.Sort = "nroMC"
            ModificarColumnasDGV()

            configurarColorControl()

            BindingSource1.MoveLast()

            txtDet.DataBindings.Add("Text", BindingSource1, "datoReq")
            'txtMon.DataBindings.Add("Text", BindingSource1, "montoPago")
            lblMon1.DataBindings.Add("Text", BindingSource2, "simbolo")
            lblMon2.DataBindings.Add("Text", BindingSource2, "simbolo")
            txtSal.DataBindings.Add("Text", BindingSource2, "saldo")

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

    Private Sub procesarIngresoCajaChicaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        calcularTotales()

        txtFec.Text = vSFecCaja
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        If BindingSource1.Position <> -1 Then
            txtMon.Text = BindingSource1.Item(BindingSource1.Position)(4)
        End If
    End Sub

    Private Sub calcularTotales()
        If BindingSource1.Position = -1 Then
            txtTotal.Text = "0.00"
            txtTotal1.Text = "0.00"
            Exit Sub
        End If
        Try
            txtTotal.Text = Format(dsAlmacen.Tables("VOrdenDesembCajaIngreso").Compute("Sum(montoPago)", "codMon=30"), "0,0.00")  '30=soles
            txtTotal1.Text = Format(dsAlmacen.Tables("VOrdenDesembCajaIngreso").Compute("Sum(montoPago)", "codMon=35"), "0,0.00")  '35=dolares

            If txtTotal.Text.Trim() = "" Then txtTotal.Text = "0.00"
            If txtTotal1.Text.Trim() = "" Then txtTotal1.Text = "0.00"
        Catch f As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(11) = 1 Then 'Procesado
                dgTabla1.Rows(j).Cells(5).Style.BackColor = Color.Green
            End If
            If BindingSource1.Item(j)(11) = 2 Then 'Cerrado
                dgTabla1.Rows(j).Cells(5).Style.BackColor = Color.AliceBlue
            End If
            dgTabla1.Rows(j).Cells(4).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºOrd."
            .Columns(1).Width = 60
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fec_Ord"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = ""
            .Columns(3).Width = 25
            .Columns(4).Width = 80
            .Columns(4).HeaderText = "Monto_Tot"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Format = "#,##0.00"
            .Columns(5).Width = 75
            .Columns(5).HeaderText = "Estado"
            .Columns(6).Width = 120
            .Columns(6).HeaderText = "Tipo_Pago"
            .Columns(7).Width = 60
            .Columns(7).HeaderText = "NºOper."
            .Columns(8).Width = 400
            .Columns(8).HeaderText = "Detalle"
            .Columns(9).Width = 120
            .Columns(9).HeaderText = "Aprobo"
            .Columns(10).Width = 120
            .Columns(10).HeaderText = "Solicito"
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
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
        Label5.ForeColor = ForeColorLabel
        Label6.ForeColor = ForeColorLabel
        Label7.ForeColor = ForeColorLabel
        lblMon1.ForeColor = ForeColorLabel
        lblMon2.ForeColor = ForeColorLabel
        btnPro.ForeColor = ForeColorButtom
        btnVis.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidaNroDesdeRangoMinimoRangoMaximoEstablecido(txtMon.Text, 1, 99999) Then
            txtMon.errorProv()
            StatusBarClass.messageBarraEstado("  Ingrese cant en el rango de 1...99999")
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarVanCaja(ByVal codPagD As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select vanCaja from TPagoDesembolso where codPagD=" & codPagD
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSaldo(ByVal codCaj As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select saldo from TCajas where codCaj=" & codCaj
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPro.Click
        If BindingSource1.Position = -1 Then
            MessageBox.Show("Seleccione Desembolso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(11) > 0 Then
            MessageBox.Show("Proceso denegado, Orden de Desembolso ya fue INGRESADO A CAJA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        Dim codDiaAux As Integer = recuperarCodDia(1, vSCodigo) 'estado=1  Abierto
        If vSCodDia <> codDiaAux Then
            If codDiaAux = 0 Then
                MessageBox.Show("PROCESO DENEGADO, FUE CERRADO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            Else
                MessageBox.Show("PROCESO DENEGADO, FUE APERTURADO OTRO DIA SESION...", nomNegocio, Nothing, MessageBoxIcon.Error)
                End
                Exit Sub
            End If
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de Prosesar INGRESO de Dinero de " & lblMon2.Text.Trim() & " " & txtMon.Text.Trim() & Chr(13) & " a " & cbCaja.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            If recuperarVanCaja(BindingSource1.Item(BindingSource1.Position)(0), myTrans) > 0 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Proceso denegado,Orden de Desembolso ya fue procesado...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'TCajas Actualizando saldo
            comandoUpdate1(txtMon.Text, BindingSource2.Item(BindingSource2.Position)(0))
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim saldo As Decimal = recuperarSaldo(BindingSource2.Item(BindingSource2.Position)(0), myTrans) 'saldo de almacen
            Dim montoEnt As Decimal = txtMon.Text
            Dim montoSal As Decimal = 0

            'TMovimientoCaja     1=Ingreso    
            comandoInsert(vSCodDia, 1, vSCodigo, BindingSource2.Item(BindingSource2.Position)(0), BindingSource1.Item(BindingSource1.Position)(0), 0, montoEnt, montoSal, saldo, vPass, txtNota.Text.Trim()) '0=Solicitante solo egreso
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'TPagoDesembolso Actualizando vanCaja  
            comandoUpdate2(1, BindingSource1.Item(BindingSource1.Position)(0))  '1=Procesado
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim codCaj As Short = cbCaja.SelectedValue

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCajaObra").Clear()
            dsAlmacen.Tables("VOrdenDesembCajaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VOrdenDesembCajaIngreso")
            daTabla2.Fill(dsAlmacen, "VCajaObra")

            BindingSource1.MoveLast()
            colorearFila()
            calcularTotales()
            txtFec.Text = vSFecCaja

            BindingSource2.Position = BindingSource2.Find("codCaj", codCaj)
            visualizarKardex()
            txtNota.Clear()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro de Ingresos y/o Egresos fué procesado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal saldo As Decimal, ByVal codCaj As Integer)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TCajas set saldo=saldo+@sal where codCaj=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = saldo
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codCaj
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert(ByVal codDia As Integer, ByVal codTM As Integer, ByVal codSuc As String, ByVal codCaj As Integer, ByVal codPagD As Integer, ByVal codSC As Integer, ByVal monEnt As Decimal, ByVal monSal As Decimal, ByVal sal As Decimal, ByVal codUsu As Integer, ByVal descrip As String)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert TMovimientoCaja(codDia,codTM,codigo,codCaj,codPagD,codSC,montoEnt,montoSal,saldoMov,codUsu,descrip) values(@codDia,@codTM,@cod,@codCaj,@codP,@codSC,@monto1,@monto2,@saldo,@codUsu,@desc)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@codDia", SqlDbType.Int, 0).Value = codDia
        cmInserTable.Parameters.Add("@codTM", SqlDbType.Int, 0).Value = codTM
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = codSuc
        cmInserTable.Parameters.Add("@codCaj", SqlDbType.Int, 0).Value = codCaj
        cmInserTable.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPagD
        cmInserTable.Parameters.Add("@codSC", SqlDbType.Int, 0).Value = codSC
        cmInserTable.Parameters.Add("@monto1", SqlDbType.Decimal, 0).Value = monEnt
        cmInserTable.Parameters.Add("@monto2", SqlDbType.Decimal, 0).Value = monSal
        cmInserTable.Parameters.Add("@saldo", SqlDbType.Decimal, 0).Value = sal
        cmInserTable.Parameters.Add("@codUsu", SqlDbType.Int, 0).Value = codUsu
        cmInserTable.Parameters.Add("@desc", SqlDbType.VarChar, 200).Value = descrip
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal vanCaja As Integer, ByVal codPagD As Integer)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TPagoDesembolso set vanCaja=@van where codPagD=@codP"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@van", SqlDbType.Int, 0).Value = vanCaja
        cmUpdateTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPagD
    End Sub

    Private Sub txtMon_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMon.KeyPress
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

    Private Sub visualizarKardex()
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe caja a generar a Generar MOVIMIENTOS CAJA...")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VMovimientoCaja").Clear()
        daVKardex.SelectCommand.Parameters("@codDia").Value = vSCodDia
        daVKardex.SelectCommand.Parameters("@codCC").Value = BindingSource2.Item(BindingSource2.Position)(0)
        daVKardex.Fill(dsAlmacen, "VMovimientoCaja")

        BindingSource3.MoveLast()  'MOVER AL ULTIMO REGISTRO
        colorear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVis.Click
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

    Private Function recuperarNroMC(ByVal codCaj As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroMC) from TMovimientoCaja where codCaj=" & codCaj
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodPagD(ByVal nroMC As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select codPagD from TMovimientoCaja where nroMC=" & nroMC
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarMonEnt(ByVal nroMC As Integer) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select montoEnt+montoSal from TMovimientoCaja where nroMC=" & nroMC
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnDes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDes.Click
        If dgTabla2.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO Existe registro de MOVIMIENTOS a deshacer...")
            Exit Sub
        End If

        Dim nroMC As Integer = recuperarNroMC(BindingSource2.Item(BindingSource2.Position)(0))  'codCaj caja
        If BindingSource3.Item(BindingSource3.Position)(0) <> nroMC Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(21) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro INGRESO...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource3.Item(BindingSource3.Position)(17) = 1 Then  'iNGRESO=1 
        Else
            MessageBox.Show("Proceso denegado, Movimiento NO es proceso de INGRESO de Caja.", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de DESHACER: " & BindingSource3.Item(BindingSource3.Position)(1) & Chr(13) & " por el monto de " & BindingSource3.Item(BindingSource3.Position)(4) & " " & BindingSource3.Item(BindingSource3.Position)(5), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        nroMC = recuperarNroMC(BindingSource2.Item(BindingSource2.Position)(0))  'codCaj
        If BindingSource3.Item(BindingSource3.Position)(0) <> nroMC Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim codPagD As Integer = recuperarCodPagD(nroMC)
        Dim monEnt As Decimal = recuperarMonEnt(nroMC)

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TMovimientoCaja
            comandoDelete2(nroMC)
            cmDeleteTable2.Transaction = myTrans
            If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar registro por qué ocurrio un error...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'MsgBox("modificando DISMINUIR TCajaChica")
            'TCajas
            comandoUpdate5(monEnt, BindingSource2.Item(BindingSource2.Position)(0))
            cmUpdateTable5.Transaction = myTrans
            If cmUpdateTable5.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            'TPagoDesembolso  
            comandoUpdate4(0, codPagD)  '0=Sin procesar
            cmUpdateTable4.Transaction = myTrans
            If cmUpdateTable4.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            Dim codCaj As Short = cbCaja.SelectedValue

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO DE KARDEX FUE DESHECHO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCajaObra").Clear()
            dsAlmacen.Tables("VOrdenDesembCajaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VOrdenDesembCajaIngreso")
            daTabla2.Fill(dsAlmacen, "VCajaObra")

            BindingSource1.MoveLast()
            colorearFila()
            calcularTotales()
            txtFec.Text = vSFecCaja

            BindingSource2.Position = BindingSource2.Find("codCaj", codCaj)
            visualizarKardex()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué Deshecho con exito...")
            wait.Close()
        Catch f As Exception
            wait.Close()
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

    Dim cmDeleteTable2 As SqlCommand
    Private Sub comandoDelete2(ByVal nroMC As Integer)
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "delete from TMovimientoCaja where nroMC=@cod"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = nroMC
    End Sub

    Dim cmUpdateTable5 As SqlCommand
    Private Sub comandoUpdate5(ByVal sal As Decimal, ByVal codCaj As Integer)
        cmUpdateTable5 = New SqlCommand
        cmUpdateTable5.CommandType = CommandType.Text
        cmUpdateTable5.CommandText = "update TCajas set saldo=saldo-@sal where codCaj=@nro"
        cmUpdateTable5.Connection = Cn
        cmUpdateTable5.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = sal
        cmUpdateTable5.Parameters.Add("@nro", SqlDbType.Int, 0).Value = codCaj
    End Sub

    Dim cmUpdateTable4 As SqlCommand
    Private Sub comandoUpdate4(ByVal vanCaja As Short, ByVal codPagD As Integer)
        cmUpdateTable4 = New SqlCommand
        cmUpdateTable4.CommandType = CommandType.Text
        cmUpdateTable4.CommandText = "update TPagoDesembolso set vanCaja=@van where codPagD=@nro"
        cmUpdateTable4.Connection = Cn
        cmUpdateTable4.Parameters.Add("@van", SqlDbType.Int, 0).Value = vanCaja
        cmUpdateTable4.Parameters.Add("@nro", SqlDbType.Int, 0).Value = codPagD
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("Proceso denegado, No existe orden de desembolso...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(11) = 0 Then
            MessageBox.Show("Proceso denegado, Sin Procesar Ingreso a Caja...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de CERRAR ORDEN de DESEMBOLSO Nº " & BindingSource1.Item(BindingSource1.Position)(1) & Chr(13) & " Si cierra no podra deshacer proceso...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TPagoDesembolso
            comandoUpdate23(2) '1=Terminado 2=cerrado
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

            Dim codCaj As Short = cbCaja.SelectedValue

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON CERRADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCajaObra").Clear()
            dsAlmacen.Tables("VOrdenDesembCajaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VOrdenDesembCajaIngreso")
            daTabla2.Fill(dsAlmacen, "VCajaObra")

            BindingSource1.MoveLast()
            colorearFila()
            calcularTotales()
            txtFec.Text = vSFecCaja

            BindingSource2.Position = BindingSource2.Find("codCaj", codCaj)
            visualizarKardex()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué cerrado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default
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

    Dim cmUpdateTable23 As SqlCommand
    Private Sub comandoUpdate23(ByVal estado As Short)
        cmUpdateTable23 = New SqlCommand
        cmUpdateTable23.CommandType = CommandType.Text
        cmUpdateTable23.CommandText = "update TPagoDesembolso set vanCaja=@est where codPagD=@nro"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub

    Private Sub cbCaja_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCaja.SelectedIndexChanged
        dsAlmacen.Tables("VMovimientoCaja").Clear()
    End Sub
End Class
