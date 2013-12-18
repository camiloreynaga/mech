Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class procesarIngresoFacturaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

    Private Sub procesarIngresoFacturaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub procesarIngresoFacturaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codDocV,serie+'-'+nro as nroD,fecDoc,est,razon,ruc,nombre,obs,hist,igv,calIGV,idSesM,codSerS,nro,codIde,codMon,camD,estado,codigo,nroDoc,simbolo from VDocVentaIngreso"  'order by codDocV"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@codSer", SqlDbType.Int, 0).Value = 0

        sele = "select codDV,cant,detalle,preUni,subtotal,linea,codDocV from VDetalleVenta where codDocV=@nro" 'order by codDV"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        sele = "select idCue,cuenta,saldoBan,simbolo,codBan,banco,nroCue,codMon from VCuentaBanco"
        crearDataAdapterTable(daTabla2, sele)
        'daTabla2.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select idMov,tipoMov,cuenta,fecDoc,serie+' - '+nroD as nroD,simbolo,montoIng,nroOrd,simbolo,montoEgr,saldoMov,medioP,nroP,nomReg,descrip,nombre,banco,nroCue,codMP,codigo,idCue,idOP,codPers,codMon,codDocV,codPagD,idTM from VMovimientoMech where idCue=@idCue"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@idCue", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VDocVentaIngreso")
            daDetDoc.Fill(dsAlmacen, "VDetalleVenta")
            daTabla2.Fill(dsAlmacen, "VCuentaBanco")
            daVKardex.Fill(dsAlmacen, "VMovimientoMech")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VDocVentaIngreso"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "codDocV"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetalleVenta"
            dgTabla2.DataSource = BindingSource2
            BindingSource2.Sort = "codDV"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VCuentaBanco"
            cbCuenta.DataSource = BindingSource3
            cbCuenta.DisplayMember = "cuenta"
            cbCuenta.ValueMember = "idCue"

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VMovimientoMech"
            Navigator3.BindingSource = BindingSource4
            dgTabla3.DataSource = BindingSource4
            BindingSource4.Sort = "idMov"
            ModificarColumnasDGV()

            configurarColorControl()

            lblMon1.DataBindings.Add("Text", BindingSource3, "simbolo")
            lblMon2.DataBindings.Add("Text", BindingSource3, "simbolo")
            'txtSal.DataBindings.Add("Text", BindingSource3, "saldoBan")

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

    Private Sub procesarIngresoFacturaForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVan3 = True
        visualizarDet()
        vfVan2 = True
        calcularSubTotal()

        colorearFila()
    End Sub

    Private Sub cbCuenta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCuenta.SelectedIndexChanged
        If BindingSource3.Position <> -1 Then
            txtSal.Text = Format(BindingSource3.Item(cbCuenta.SelectedIndex)(2), "0,0.00")
        End If
        dsAlmacen.Tables("VMovimientoMech").Clear()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(17) = 3 Then 'Procesado
                dgTabla1.Rows(j).Cells(3).Style.BackColor = Color.Green
            End If
            'dgTabla1.Rows(j).Cells(4).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
        calcularSubTotal()
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If BindingSource1.Position = -1 Then
                dsAlmacen.Tables("VDetalleVenta").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetalleVenta").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VDetalleVenta")
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºDoc."
            .Columns(1).Width = 80
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fec_Doc"
            .Columns(2).Width = 70
            .Columns(3).Width = 70
            .Columns(3).HeaderText = "Estado"
            .Columns(4).Width = 260
            .Columns(4).HeaderText = "Cliente"
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "R.U.C."
            .Columns(6).Width = 300
            .Columns(6).HeaderText = "Sede/Obra"
            .Columns(7).Width = 300
            .Columns(7).HeaderText = "Nota"
            .Columns(8).Width = 500
            .Columns(8).HeaderText = ""
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Cant."
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).Width = 500
            .Columns(2).HeaderText = "Detalle"
            .Columns(3).HeaderText = "Prec_Unit"
            .Columns(3).Width = 80
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).DefaultCellStyle.Format = "#,##0.00"
            .Columns(4).HeaderText = "Importe"
            .Columns(4).Width = 80
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Format = "#,##0.00"
            .Columns(5).HeaderText = "Descripción"
            .Columns(5).Width = 800
            .Columns(6).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla3
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Movim."
            .Columns(1).Width = 60
            .Columns(2).HeaderText = "Cuenta"
            .Columns(2).Width = 140
            .Columns(3).HeaderText = "Fecha"
            .Columns(3).Width = 70
            .Columns(4).HeaderText = "NºDoc."
            .Columns(4).Width = 70
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 30
            .Columns(6).Width = 95
            .Columns(6).HeaderText = "MonIngreso"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "#,##0.00"
            .Columns(7).HeaderText = "NºDes"
            .Columns(7).Width = 50
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).HeaderText = ""
            .Columns(8).Width = 30
            .Columns(9).Width = 80
            .Columns(9).HeaderText = "MonEgreso"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "#,##0.00"
            .Columns(10).Width = 95
            .Columns(10).HeaderText = "Saldo_Fecha"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Format = "#,##0.00"
            .Columns(11).Width = 100
            .Columns(11).HeaderText = "Medio_Pago"
            .Columns(12).Width = 60
            .Columns(12).HeaderText = "NºOper."
            .Columns(13).Width = 100
            .Columns(13).HeaderText = "Pers._Reg."
            .Columns(14).Width = 300
            .Columns(14).HeaderText = "Nota"
            .Columns(15).Width = 300
            .Columns(15).HeaderText = "Sede / Obra"
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
        lblTotal.ForeColor = ForeColorLabel
        CheckBoxIGV.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnPro.ForeColor = ForeColorButtom
        btnVis.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Dim vfIGV As Double = vSIGV
    Dim vfVan2 As Boolean = False
    Private Sub calcularSubTotal()
        If BindingSource1.Position = -1 Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        If vfVan2 Then
            If BindingSource1.Item(BindingSource1.Position)(9) = 0 Then 'Sin IGV
                vfIGV = 0
            Else 'Con IGV
                CheckBoxIGV.Checked = False
                If BindingSource1.Item(BindingSource1.Position)(10) = 1 Then 'calculos igv
                    vfIGV = vSIGV
                Else  'calculo 2
                    vfIGV = vSIGV
                End If
            End If

            If BindingSource1.Item(BindingSource1.Position)(10) = 1 Then  'tipo 1
                txtTotal.Text = Format((dsAlmacen.Tables("VDetalleVenta").Compute("Sum(subTotal)", Nothing)), "0,0.00")
                txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0,0.00")
                txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0,0.00")
            Else  'Tipo 2
                txtSub.Text = Format((dsAlmacen.Tables("VDetalleVenta").Compute("Sum(subTotal)", Nothing)), "0,0.00")
                txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0,0.00")
                txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0,0.00")
            End If
            txtMon.Text = txtTotal.Text
            lblTotal.Text = "TOTAL  " & BindingSource1.Item(BindingSource1.Position)(20) 'cbMoneda.Text.Trim()
            cambiarNroTotalLetra()
        End If
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        'If cbMoneda.SelectedValue = 30 Then    '30=Nuevos solesl
        If BindingSource1.Item(BindingSource1.Position)(15) = 30 Then
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(txtTotal.Text) > 0 Then
            cALetra.Numero = Val(CDbl(txtTotal.Text))
            txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
        End If
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
        If BindingSource3.Position = -1 Then
            StatusBarClass.messageBarraEstado("  No existe cuenta a Generar MOVIMIENTOS CUENTA...")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VMovimientoMech").Clear()
        daVKardex.SelectCommand.Parameters("@idCue").Value = BindingSource3.Item(BindingSource3.Position)(0)
        daVKardex.Fill(dsAlmacen, "VMovimientoMech")

        BindingSource4.MoveLast()  'MOVER AL ULTIMO REGISTRO
        colorear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVis.Click
        visualizarKardex()
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource4.Count - 1
            If BindingSource4.Item(j)(26) = 1 Then 'EGRESO
                dgTabla3.Rows(j).Cells(1).Style.BackColor = Color.Green
                dgTabla3.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource4.Item(j)(26) = 2 Then 'INGRESO
                dgTabla3.Rows(j).Cells(1).Style.BackColor = Color.Red
                dgTabla3.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            dgTabla3.Rows(j).Cells(6).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla3.Rows(j).Cells(9).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla3.Rows(j).Cells(10).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Function recuperarEstFact(ByVal codDocV As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select estado from TDocVenta where codDocV=" & codDocV
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidaNroDesdeRangoMinimoRangoMaximoEstablecido(txtMon.Text, 1, 9999999) Then
            txtMon.errorProv()
            StatusBarClass.messageBarraEstado("  Ingrese cant en el rango de 1...9'999,999")
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarSaldo(ByVal idCue As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select saldoBan from TCuentaBan where idCue=" & idCue
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPro.Click
        If BindingSource1.Position = -1 Then
            MessageBox.Show("Seleccione FACTURA...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If recuperarEstFact(BindingSource1.Item(BindingSource1.Position)(0)) > 2 Then  '3=Procesado Ingreso  4=Archivado
            MessageBox.Show("Proceso de Ingreso ya Fue Procesado...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If cbCuenta.Text.Trim() = "" Then
            MessageBox.Show("Seleccione Cuenta Valida!!!", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As Short
        If BindingSource1.Item(BindingSource1.Position)(15) <> BindingSource3.Item(BindingSource3.Position)(7) Then
            resp = MessageBox.Show("Moneda de Factura: " & BindingSource1.Item(BindingSource1.Position)(20) & " es diferente a Moneda de Cuenta: " & BindingSource3.Item(BindingSource3.Position)(3) & Chr(13) & " Esta correcto el monto de: " & txtMon.Text.Trim() & " en " & BindingSource3.Item(BindingSource3.Position)(3), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                txtMon.Focus()
                txtMon.SelectAll()
                Exit Sub
            End If
        End If

        resp = MessageBox.Show("Esta segúro de Prosesar INGRESO de Dinero de " & lblMon2.Text.Trim() & " " & txtMon.Text.Trim() & Chr(13) & " a Cuenta: " & cbCuenta.Text.Trim(), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TCajas Actualizando saldo TCuentaBan
            comandoUpdate1(txtMon.Text, BindingSource3.Item(BindingSource3.Position)(0))
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim saldo As Decimal = recuperarSaldo(BindingSource3.Item(BindingSource3.Position)(0), myTrans) 'saldo de almacen
            Dim montoEnt As Decimal = txtMon.Text
            Dim montoSal As Decimal = 0

            'TMovimientoMech     2=Ingreso  1=primer mes  
            comandoInsert(date1.Value.Date, 1, 2, BindingSource1.Item(BindingSource1.Position)(18), BindingSource3.Item(BindingSource3.Position)(0), 0, BindingSource1.Item(BindingSource1.Position)(0), montoEnt, montoSal, saldo, vPass, txtNota.Text.Trim())
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'TDocVenta  actualizar estado  
            comandoUpdate2(3, BindingSource1.Item(BindingSource1.Position)(0))  '3=Procesado
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim idCue As Short = cbCuenta.SelectedValue

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan3 = False
            vfVan2 = False

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCuentaBanco").Clear()
            dsAlmacen.Tables("VDocVentaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VDocVentaIngreso")
            daTabla2.Fill(dsAlmacen, "VCuentaBanco")

            BindingSource1.MoveLast()
            colorearFila()

            vfVan3 = True
            visualizarDet()
            vfVan2 = True
            calcularSubTotal()

            BindingSource3.Position = BindingSource3.Find("idCue", idCue)
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
    Private Sub comandoUpdate1(ByVal saldo As Decimal, ByVal idCue As Integer)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TCuentaBan set saldoBan=saldoBan+@sal where idCue=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = saldo
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idCue
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert(ByVal fec As String, ByVal idS As Integer, ByVal idTM As Integer, ByVal codSuc As String, ByVal idCue As Integer, ByVal codPagD As Integer, ByVal codDocC As Integer, ByVal monEnt As Decimal, ByVal monSal As Decimal, ByVal sal As Decimal, ByVal codUsu As Integer, ByVal descrip As String)
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert TMovimientoMech(fecDoc,idSesM,idTM,codigo,idCue,codPagD,codDocC,montoIng,montoEgr,saldoMov,codPers,descrip) values(@fec,@idS,@idTM,@cod,@idCue,@codP,@codDocC,@monto1,@monto2,@saldo,@codPers,@desc)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@fec", SqlDbType.Date).Value = fec
        cmInserTable.Parameters.Add("@idS", SqlDbType.Int, 0).Value = idS
        cmInserTable.Parameters.Add("@idTM", SqlDbType.Int, 0).Value = idTM
        cmInserTable.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = codSuc
        cmInserTable.Parameters.Add("@idCue", SqlDbType.Int, 0).Value = idCue
        cmInserTable.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPagD
        cmInserTable.Parameters.Add("@codDocC", SqlDbType.Int, 0).Value = codDocC
        cmInserTable.Parameters.Add("@monto1", SqlDbType.Decimal, 0).Value = monEnt
        cmInserTable.Parameters.Add("@monto2", SqlDbType.Decimal, 0).Value = monSal
        cmInserTable.Parameters.Add("@saldo", SqlDbType.Decimal, 0).Value = sal
        cmInserTable.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = codUsu
        cmInserTable.Parameters.Add("@desc", SqlDbType.VarChar, 200).Value = descrip
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal est As Integer, ByVal codD As Integer)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TDocVenta set estado=@est where codDocV=@codD"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable2.Parameters.Add("@codD", SqlDbType.Int, 0).Value = codD
    End Sub

    Private Function recuperarIdMov(ByVal idCue As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(idMov) from TMovimientoMech where idCue=" & idCue
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodDocV(ByVal idMov As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select codDocC from TMovimientoMech where idMov=" & idMov
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarMonEnt(ByVal idMov As Integer) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select montoIng+montoEgr from TMovimientoMech where idMov=" & idMov
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnDes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDes.Click
        If dgTabla3.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO Existe registro de MOVIMIENTOS a deshacer...")
            Exit Sub
        End If

        Dim idMov As Integer = recuperarIdMov(BindingSource3.Item(BindingSource3.Position)(0))  'idCue cuenta
        If BindingSource4.Item(BindingSource4.Position)(0) <> idMov Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource4.Item(BindingSource4.Position)(22) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro INGRESO...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource4.Item(BindingSource4.Position)(26) = 2 Then  'iNGRESO=2 
        Else
            MessageBox.Show("Proceso denegado, Movimiento NO es proceso de INGRESO de Cuenta.", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de DESHACER Cuenta: " & BindingSource4.Item(BindingSource4.Position)(2) & Chr(13) & " por el monto de " & BindingSource4.Item(BindingSource4.Position)(5) & " " & BindingSource4.Item(BindingSource4.Position)(6), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        idMov = recuperarIdMov(BindingSource3.Item(BindingSource3.Position)(0))   'idCue cuenta
        If BindingSource4.Item(BindingSource4.Position)(0) <> idMov Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim codDocV As Integer = recuperarCodDocV(idMov)
        Dim monEnt As Decimal = recuperarMonEnt(idMov)

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TMovimientoMech
            comandoDelete2(idMov)
            cmDeleteTable2.Transaction = myTrans
            If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar registro por qué ocurrio un error...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'MsgBox("modificando DISMINUIR...")
            'TCuentaBan
            comandoUpdate5(monEnt, BindingSource3.Item(BindingSource3.Position)(0))
            cmUpdateTable5.Transaction = myTrans
            If cmUpdateTable5.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            'TDocVenta  
            comandoUpdate4(1, codDocV)  '1=CERRADo Sin procesar
            cmUpdateTable4.Transaction = myTrans
            If cmUpdateTable4.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            Dim idCue As Short = cbCuenta.SelectedValue

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO DE KARDEX FUE DESHECHO CON EXITO...")
            finalMytrans = True

            vfVan3 = False
            vfVan2 = False

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCuentaBanco").Clear()
            dsAlmacen.Tables("VDocVentaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VDocVentaIngreso")
            daTabla2.Fill(dsAlmacen, "VCuentaBanco")

            BindingSource1.MoveLast()
            colorearFila()

            vfVan3 = True
            visualizarDet()
            vfVan2 = True
            calcularSubTotal()

            BindingSource3.Position = BindingSource3.Find("idCue", idCue)
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
    Private Sub comandoDelete2(ByVal idMov As Integer)
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "delete from TMovimientoMech where idMov=@cod"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idMov
    End Sub

    Dim cmUpdateTable5 As SqlCommand
    Private Sub comandoUpdate5(ByVal sal As Decimal, ByVal idCue As Integer)
        cmUpdateTable5 = New SqlCommand
        cmUpdateTable5.CommandType = CommandType.Text
        cmUpdateTable5.CommandText = "update TCuentaBan set saldoBan=saldoBan-@sal where idCue=@nro"
        cmUpdateTable5.Connection = Cn
        cmUpdateTable5.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = sal
        cmUpdateTable5.Parameters.Add("@nro", SqlDbType.Int, 0).Value = idCue
    End Sub

    Dim cmUpdateTable4 As SqlCommand
    Private Sub comandoUpdate4(ByVal est As Short, ByVal codDoc As Integer)
        cmUpdateTable4 = New SqlCommand
        cmUpdateTable4.CommandType = CommandType.Text
        cmUpdateTable4.CommandText = "update TDocVenta set estado=@est where codDocV=@nro"
        cmUpdateTable4.Connection = Cn
        cmUpdateTable4.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable4.Parameters.Add("@nro", SqlDbType.Int, 0).Value = codDoc
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("Proceso denegado, No existe Doc. Venta...")
            Exit Sub
        End If

        Dim resp As String
        If recuperarEstFact(BindingSource1.Item(BindingSource1.Position)(0)) < 2 Then  '2=anulado  3=Procesado Ingreso  4=Archivado
            resp = MessageBox.Show("Proceso de Ingreso NO Fue Procesado. Esta seguro de Archivar Doc. Venta" & Chr(13) & "Si Archiva no podra deshacer proceso...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                txtMon.Focus()
                txtMon.SelectAll()
                Exit Sub
            End If
            Exit Sub
        End If

        resp = MessageBox.Show("Está segúro de ARCHIVAR Doc. Venta Nº " & BindingSource1.Item(BindingSource1.Position)(1) & Chr(13) & " Si Archiva no podra deshacer proceso...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDocVenta
            comandoUpdate23(4) '3=Procesado 4=archivado
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

            Dim idCue As Short = cbCuenta.SelectedValue

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON CERRADOS CON EXITO...")
            finalMytrans = True
            vfVan3 = False
            vfVan2 = False

            'Actualizando el dataSet 
            dsAlmacen.Tables("VCuentaBanco").Clear()
            dsAlmacen.Tables("VDocVentaIngreso").Clear()

            daTabla1.Fill(dsAlmacen, "VDocVentaIngreso")
            daTabla2.Fill(dsAlmacen, "VCuentaBanco")

            BindingSource1.MoveLast()
            colorearFila()

            vfVan3 = True
            visualizarDet()
            vfVan2 = True
            calcularSubTotal()

            BindingSource3.Position = BindingSource3.Find("idCue", idCue)
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
        cmUpdateTable23.CommandText = "update TDocVenta set estado=@est where codDocV=@nro"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub
End Class
