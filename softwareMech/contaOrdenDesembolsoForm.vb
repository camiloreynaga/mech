Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class contaOrdenDesembolsoForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub contaOrdenDesembolsoForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub contaOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idOP,fecDes,serie,nro,simbolo,monto,montoDet,montoDif,nombre,est,nomTes,nomGer,nomSol,datoReq,ruc,razon,banco,nroCta,nroDet,hist,estado,codMon from VOrdenDesemConta" 'order by idOP
        crearDataAdapterTable(daTabla1, sele)
        'daTabla1.SelectCommand.Parameters.Add("@ser", SqlDbType.VarChar, 5).Value = vSerie

        sele = "select codPagD,fecPago,tipoP,nroP,pagoDet,simbolo,montoPago,montoD,clasif,codTipP,codMon,idOP,idCue,codCla from VPagoDesemTesoreria where idOP=@idOP"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VOrdenDesemConta")
            daDetDoc.Fill(dsAlmacen, "VPagoDesemTesoreria")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VOrdenDesemConta"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "idOP"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VPagoDesemTesoreria"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            ModificarColumnasDGV()

            configurarColorControl()

            txtProv.DataBindings.Add("Text", BindingSource1, "razon")
            txtRuc.DataBindings.Add("Text", BindingSource1, "ruc")
            txtForma.DataBindings.Add("Text", BindingSource1, "banco")
            txtNC.DataBindings.Add("Text", BindingSource1, "nroCta")
            txtND.DataBindings.Add("Text", BindingSource1, "nroDet")
            txtMot.DataBindings.Add("Text", BindingSource1, "datoReq")

            vfVan1 = True
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

    Private Sub contaOrdenDesembolsoForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        calcularTotales()
        BindingSource1.MoveLast()
    End Sub

    Private Sub calcularTotales()
        If BindingSource1.Position = -1 Then
            txtTotal0.Text = "0.00"
            txtTotal1.Text = "0.00"
            txtTotal2.Text = "0.00"
            txtTotal3.Text = "0.00"
            Exit Sub
        End If
        Try
            txtTotal0.Text = dsAlmacen.Tables("VOrdenDesemConta").Compute("Sum(monto)", "codMon=30").ToString()  '30=soles
            txtTotal1.Text = dsAlmacen.Tables("VOrdenDesemConta").Compute("Sum(monto)", "codMon=35").ToString()  '35=dolares
            txtTotal2.Text = dsAlmacen.Tables("VOrdenDesemConta").Compute("Sum(montoDet)", "codMon=30").ToString()  '30=soles
            txtTotal3.Text = dsAlmacen.Tables("VOrdenDesemConta").Compute("Sum(montoDet)", "codMon=35").ToString()  '35=dolares

            If txtTotal0.Text.Trim() = "" Then txtTotal0.Text = "0.00"
            If txtTotal1.Text.Trim() = "" Then txtTotal1.Text = "0.00"
            If txtTotal2.Text.Trim() = "" Then txtTotal2.Text = "0.00"
            If txtTotal3.Text.Trim() = "" Then txtTotal3.Text = "0.00"

        Catch f As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(20) = 1 Then 'Terminado
                dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(9).Style.ForeColor = Color.White
            End If
            dgTabla1.Rows(j).Cells(5).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Width = 70
            .Columns(1).HeaderText = "Fecha"
            .Columns(2).HeaderText = "Serie"
            .Columns(2).Width = 40
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).HeaderText = "NºOrden"
            .Columns(3).Width = 50
            .Columns(4).HeaderText = ""
            .Columns(4).Width = 30
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "Monto"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 70
            .Columns(6).HeaderText = "Detracción"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).Width = 75
            .Columns(7).HeaderText = "Monto_Dif."
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).Width = 235
            .Columns(8).HeaderText = "Lugar / Obra"
            .Columns(9).Width = 75
            .Columns(9).HeaderText = "Estado"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).Width = 110
            .Columns(10).HeaderText = "Tesoreria_Aprobo"
            .Columns(11).Width = 110
            .Columns(11).HeaderText = "Gerencia_Aprobo"
            .Columns(12).Width = 110
            .Columns(12).HeaderText = "Solicitante"
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 75
            .Columns(1).HeaderText = "Fecha"
            .Columns(2).Width = 140
            .Columns(2).HeaderText = "Medio Pago"
            .Columns(3).Width = 90
            .Columns(3).HeaderText = "Nº"
            .Columns(4).HeaderText = "Descripción"
            .Columns(4).Width = 260
            .Columns(5).HeaderText = ""
            .Columns(5).Width = 30
            .Columns(6).Width = 70
            .Columns(6).HeaderText = "Monto"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).Width = 70
            .Columns(7).HeaderText = "Detracc."
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).HeaderText = "Clasificación"
            .Columns(8).Width = 100
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
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
        Label4.ForeColor = ForeColorLabel
        Label8.ForeColor = ForeColorLabel
        Label9.ForeColor = ForeColorLabel
        Label10.ForeColor = ForeColorLabel
        Label11.ForeColor = ForeColorLabel
        Label12.ForeColor = ForeColorLabel
        Label13.ForeColor = ForeColorLabel
        Label14.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                dsAlmacen.Tables("VPagoDesemTesoreria").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VPagoDesemTesoreria").Clear()
            daDetDoc.SelectCommand.Parameters("@idOP").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VPagoDesemTesoreria")
            'colorear()
            sumTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub sumTotal()
        If BindingSource2.Position = -1 Then
            txtTotal4.Text = "0.00"
            txtTotal5.Text = "0.00"
            txtTotal6.Text = "0.00"
            txtTotal7.Text = "0.00"
            Exit Sub
        End If
        txtTotal4.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoPago)", "codMon=30").ToString()  '30=soles
        txtTotal5.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoPago)", "codMon=35").ToString()  '30=soles
        txtTotal6.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoD)", "codMon=30").ToString()  '30=soles
        txtTotal7.Text = dsAlmacen.Tables("VPagoDesemTesoreria").Compute("Sum(montoD)", "codMon=35").ToString()  '30=soles

        If txtTotal4.Text.Trim() = "" Then txtTotal4.Text = "0.00"
        If txtTotal5.Text.Trim() = "" Then txtTotal5.Text = "0.00"
        If txtTotal6.Text.Trim() = "" Then txtTotal6.Text = "0.00"
        If txtTotal7.Text.Trim() = "" Then txtTotal7.Text = "0.00"
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If validaCampoVacioMinCaracNoNumer(txtNro.Text.Trim, 3) Then
            txtNro.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If BindingSource1.Item(BindingSource1.Position)(20) = 0 Then  '1=Terminado  0=Pendiente
            MessageBox.Show("Proceso denegado, Estado no esta en [TERMINADO]", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está seguro de dar V.B. a ORDEN de DESEMBOLSO Nº " & BindingSource1.Item(BindingSource1.Position)(3) & Chr(13) & " Si procede, no podra deshacer proceso de cierre", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TPersDesem
            comandoInsert2(BindingSource1.Item(BindingSource1.Position)(0), vPass, 1, 4, "")  '1=aprobado  4=contabilidad
            cmInserTable2.Transaction = myTrans
            If cmInserTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'TOrdenDesembolso
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

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON CERRADOS CON EXITO...")
            finalMytrans = True
            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesemConta").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesemConta")

            vfVan1 = True
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué cerrado con exito...")
            wait.Close()
            Me.Cursor = Cursors.Default

            colorearFila()
            calcularTotales()
            BindingSource1.MoveLast()
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

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal idOP As Integer, ByVal codPers As Integer, ByVal estado As Integer, ByVal tipo As Integer, ByVal obs As String)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "insert into TPersDesem(idOP,codPers,estDesem,tipoA,obserDesem,fecFir) values(@id,@codP,@est,@tipo,@obs,@fec)"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@id", SqlDbType.Int, 0).Value = idOP
        cmInserTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPers 'vPass
        cmInserTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado '1=Aprobado
        cmInserTable2.Parameters.Add("@tipo", SqlDbType.Int, 0).Value = tipo '1=Solicitante
        cmInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmInserTable2.Parameters.Add("@fec", SqlDbType.Date).Value = Now.Date
    End Sub

    Dim cmUpdateTable23 As SqlCommand
    Private Sub comandoUpdate23(ByVal estado As Short)
        cmUpdateTable23 = New SqlCommand
        cmUpdateTable23.CommandType = CommandType.Text
        cmUpdateTable23.CommandText = "update TOrdenDesembolso set estado=@est,nroConfor=@nroC,fecEnt=@fec where idOP=@nro"
        cmUpdateTable23.Connection = Cn
        cmUpdateTable23.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable23.Parameters.Add("@nroC", SqlDbType.VarChar, 30).Value = txtNro.Text.Trim()
        cmUpdateTable23.Parameters.Add("@fec", SqlDbType.VarChar, 10).Value = date1.Value.Date
        cmUpdateTable23.Parameters.Add("@nro", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
    End Sub
End Class
