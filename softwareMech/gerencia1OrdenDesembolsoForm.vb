Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class gerencia1OrdenDesembolsoForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub gerencia1OrdenDesembolsoForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub gerencia1OrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        'Dim sele As String = "select idOP,estApro,fecDes,serie,nro,simbolo,monto,razon,nom,obserDesem,est,nombre,hist,estDesem,codPersDes,estado,codMon,datoReq from VOrdenDesemGerencia1 where estDesem in(0,@est,2)" 
        Dim sele As String = "select idOP,estApro,fecDes,serie,nro,simbolo,monto,razon,nom,obserDesem,est,nombre,hist,estDesem,codPersDes,estado,codMon,datoReq from VOrdenDesemGerencia1 where estDesem in(@est1,@est,@est2) or serie=@ser" '0=NULL  1=Aprobado 2=Observado
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0
        daTabla1.SelectCommand.Parameters.Add("@est1", SqlDbType.Int, 0).Value = 0 'NULL Pendiente
        daTabla1.SelectCommand.Parameters.Add("@est2", SqlDbType.Int, 0).Value = 2 'Observado
        daTabla1.SelectCommand.Parameters.Add("@ser", SqlDbType.VarChar).Value = 100 'serie texto

        sele = "select codDetO,cant,unidad,descrip,precio,subTotal,dias,fecOrden,nro,nroOrden,igv,calIGV,codMon,simbolo,idOP,idSol from VOrdenCompraDetalle where idOP=@idOP"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@idOP", SqlDbType.Int, 0).Value = 0

        sele = "select codSerO,serie from TSerieOrden where estado=1 order by serie"
        crearDataAdapterTable(daTabla5, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VOrdenDesemGerencia1")
            daDetDoc.Fill(dsAlmacen, "VOrdenCompraDetalle")
            daTabla5.Fill(dsAlmacen, "TSerieOrden")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VOrdenDesemGerencia1"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "estDesem,fecDes"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VOrdenCompraDetalle"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "descrip"
            ModificarColumnasDGV()

            cbSerie.DataSource = dsAlmacen
            cbSerie.DisplayMember = "TSerieOrden.serie"
            cbSerie.ValueMember = "codSerO"

            configurarColorControl()

            vfVan1 = True
            visualizarDet()
            vfVan2 = True

            txtReq.DataBindings.Add("Text", BindingSource1, "datoReq")

            If vSCodTipoUsu = 2 Then  '1=administrador de sistema 2=gerencia general 
                'Solo administrador puede realizar este proceso
                AddContextMenu() 'Agregando menu antiClick
            End If

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

    Private Sub gerencia1OrdenDesembolsoForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        calcularTotales()

        rb3.Checked = True 'filtro pendientes
    End Sub

    Dim estado As Integer
    Dim estado1 As Integer
    Dim estado2 As Integer
    Dim estado3 As String
    Dim vfVan2 As Boolean = False
    Private Sub visualizarOrd()
        If vfVan2 Then
            vfVan1 = False
            'AsignarValoresFiltro()

            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VOrdenDesemGerencia1").Clear()
            daTabla1.SelectCommand.Parameters("@est").Value = estado
            daTabla1.SelectCommand.Parameters("@est1").Value = estado1 'NULL Pendiente
            daTabla1.SelectCommand.Parameters("@est2").Value = estado2 'Observado
            daTabla1.SelectCommand.Parameters("@ser").Value = estado3 'serie
            daTabla1.Fill(dsAlmacen, "VOrdenDesemGerencia1")

            colorearFila()
            calcularTotales()

            vfVan1 = True
            visualizarDet()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb1.CheckedChanged, rb2.CheckedChanged, rb3.CheckedChanged
        If rb3.Checked Then 'Pendientes
            cbSerie.Visible = False
            btnF1.Visible = False

            'If cbVis1.Checked = True Then 'Todos Aprobados, Observados NULL
            '    estado = 1
            'Else  'solo pendientes=NULL
            estado = 0
            estado1 = 0
            estado2 = 2
            estado3 = "100"

            visualizarOrd()
        End If
        If rb1.Checked Then 'Todos
            cbSerie.Visible = False
            btnF1.Visible = False

            estado = 1
            estado1 = 0
            estado2 = 2
            estado3 = "100"

            visualizarOrd()
        End If

        If rb2.Checked Then 'por serie
            cbSerie.Visible = True
            btnF1.Visible = True

            dsAlmacen.Tables("VOrdenDesemGerencia1").Clear()
            dsAlmacen.Tables("VOrdenCompraDetalle").Clear()
        End If
    End Sub

    Private Sub btnF1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF1.Click
        estado = 10
        estado1 = 10
        estado2 = 10
        estado3 = cbSerie.Text.Trim()  'Por numero de serie de orden de desembolso

        visualizarOrd()
    End Sub

    Private Sub calcularTotales()
        If BindingSource1.Position = -1 Then
            txtTotalSoles.Text = "0.00"
            txtTotalDolares.Text = "0.00"
            Exit Sub
        End If
        Try
            'txtTotalSoles.Text = Format(dsAlmacen.Tables("VOrdenDesemGerencia1").Compute("Sum(monto)", "codMon=30"), "0,0.00")
            'txtTotalDolares.Text = Format(dsAlmacen.Tables("VOrdenDesemGerencia1").Compute("Sum(monto)", "codMon=35"), "0,0.00")

            txtTotalSoles.Text = dsAlmacen.Tables("VOrdenDesemGerencia1").Compute("Sum(monto)", "codMon=30").ToString()
            txtTotalDolares.Text = dsAlmacen.Tables("VOrdenDesemGerencia1").Compute("Sum(monto)", "codMon=35").ToString()

            If txtTotalSoles.Text.Trim() = "" Then
                txtTotalSoles.Text = "0.00"
            Else
                txtTotalSoles.Text = Format(CDbl(txtTotalSoles.Text), "0,0.00")
            End If
            If txtTotalDolares.Text.Trim() = "" Then
                txtTotalDolares.Text = "0.00"
            Else
                txtTotalDolares.Text = Format(CDbl(txtTotalDolares.Text), "0,0.00")
            End If
        Catch f As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(13) = 1 Then 'Aprobado
                dgTabla1.Rows(j).Cells(1).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource1.Item(j)(13) = 2 Then 'Observado
                dgTabla1.Rows(j).Cells(1).Style.BackColor = Color.Yellow
                dgTabla1.Rows(j).Cells(1).Style.ForeColor = Color.Red
            End If
            If BindingSource1.Item(j)(13) = 3 Then 'Rechazado
                dgTabla1.Rows(j).Cells(1).Style.BackColor = Color.Red
                dgTabla1.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            dgTabla1.Rows(j).Cells(6).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    ''' <summary>
    ''' customiza la Grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        With dgTabla1
            'idOP(0)
            .Columns("idOP").Visible = False
            'estApro 1 
            .Columns("estApro").Width = 80
            .Columns("estApro").HeaderText = "Estado"
            .Columns("estApro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'fecDes 2
            .Columns("fecDes").Width = 70
            .Columns("fecDes").HeaderText = "Fecha"
            'serie 3
            .Columns("serie").HeaderText = "Serie"
            .Columns("serie").Width = 40
            .Columns("serie").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'nro 4
            .Columns("nro").HeaderText = "NºOrden"
            .Columns("nro").Width = 50
            .Columns("nro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'simbolo 5
            .Columns("simbolo").HeaderText = ""
            .Columns("simbolo").Width = 30
            'monto 6
            .Columns("monto").Width = 85
            .Columns("monto").HeaderText = "Monto"

            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("monto").DefaultCellStyle.Format = "N2"

            'razon 7
            .Columns("razon").Width = 200
            .Columns("razon").HeaderText = "Proveedor"
            'nom 8
            .Columns("nom").Width = 100
            .Columns("nom").HeaderText = "Gerente"
            'obserDesem 9
            .Columns("obserDesem").Width = 160
            .Columns("obserDesem").HeaderText = "Observación"
            'est 10
            .Columns("est").Width = 70
            .Columns("est").HeaderText = "Est.Orden"
            'nombre 11
            .Columns("nombre").Width = 200
            .Columns("nombre").HeaderText = "Lugar / Obra"
            'hist 12
            .Columns("hist").Width = 800
            .Columns("hist").HeaderText = ""
            'estDesem 13
            .Columns("estDesem").Visible = False
            'codPersDes 14
            .Columns("codPersDes").Visible = False
            'estado 15
            .Columns("estado").Visible = False

            .Columns("est").Visible = False
            'codMon 16
            .Columns("codMon").Visible = False
            'datoReq 17
            .Columns("datoReq").Visible = False

            'Orden en que se muestra las Columnas de la Grilla

            'estApro 1 
            .Columns("estApro").DisplayIndex = 0
            'fecDes 2
            .Columns("fecDes").DisplayIndex = 1

            .Columns("simbolo").DisplayIndex = 2
            .Columns("simbolo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'monto 6
            .Columns("monto").DisplayIndex = 3
            'razon 7
            .Columns("nombre").DisplayIndex = 4
            .Columns("razon").DisplayIndex = 5
            .Columns("serie").DisplayIndex = 6
            .Columns("nro").DisplayIndex = 7
            .Columns("nom").DisplayIndex = 8
            .Columns("obserDesem").DisplayIndex = 9
            .Columns("hist").DisplayIndex = 11



            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            'codDetO
            .Columns(0).Visible = False

            'cant
            .Columns(1).Width = 50
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cant").DefaultCellStyle.Format = "N2"
            'unidad
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "Und."
            'descrip
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 417
            'precio
            .Columns(4).Width = 75
            .Columns(4).HeaderText = "P.U."
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("precio").DefaultCellStyle.Format = "N2"
            'subTotal
            .Columns(5).Width = 75
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("subTotal").DefaultCellStyle.Format = "N2"
            'dias
            .Columns(6).Width = 40
            .Columns(6).HeaderText = "Dias"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            'fecOrden
            .Columns(7).Width = 70
            .Columns(7).HeaderText = "Fecha_Orden"
            'nro
            .Columns(8).Width = 50
            .Columns(8).HeaderText = "NºOrden"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'nroOrden
            .Columns(9).Visible = False
            'igv
            .Columns(10).Visible = False
            'calIGV
            .Columns(11).Visible = False
            'codMon
            .Columns(12).Visible = False
            'simbolo
            .Columns(13).Visible = False
            'idOP
            .Columns(14).Visible = False
            'idSol
            .Columns(15).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    ''' <summary>
    ''' Configura los colores para el form
    ''' </summary>
    ''' <remarks></remarks>
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
        GroupBox1.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    ''' <summary>
    ''' Muestra el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VOrdenCompraDetalle").Clear()
            daDetDoc.SelectCommand.Parameters("@idOP").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VOrdenCompraDetalle")
            'colorear()
            calcularSubTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Dim vfIGV As Double = vSIGV
    ''' <summary>
    ''' Calcular el Subtotal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub calcularSubTotal()
        If BindingSource2.Position = -1 Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        vfIGV = BindingSource2.Item(BindingSource2.Position)(10)

        If BindingSource2.Item(BindingSource2.Position)(11) = 1 Then  'tipo 1
            txtTotal.Text = Format((dsAlmacen.Tables("VOrdenCompraDetalle").Compute("Sum(subTotal)", Nothing)), "0,0.00")
            txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0,0.00")
            txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0,0.00")
        Else  'Tipo 2
            txtSub.Text = Format((dsAlmacen.Tables("VOrdenCompraDetalle").Compute("Sum(subTotal)", Nothing)), "0,0.00")
            txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0,0.00")
            txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0,0.00")
        End If
        lblTotal.Text = "TOTAL  " & BindingSource2.Item(BindingSource2.Position)(13) 'cbMoneda.Text.Trim()
        cambiarNroTotalLetra()
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        If BindingSource2.Item(BindingSource2.Position)(12) = 30 Then    '30=Nuevos solesl
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

    '-----------------------------------------
    '------Menu antiClick Anular---------------
    WithEvents toolStripItem1 As New ToolStripMenuItem()
    WithEvents toolStripItem2 As New ToolStripMenuItem()
    WithEvents toolStripItem3 As New ToolStripMenuItem()
    Private Sub AddContextMenu()
        toolStripItem1.Text = "APROBADO"
        toolStripItem1.BackColor = Color.Green
        toolStripItem2.Text = "OBSERVADO"
        toolStripItem2.BackColor = Color.Yellow
        toolStripItem3.Text = "DENEGADO"
        toolStripItem3.BackColor = Color.Red
        Dim strip As New ContextMenuStrip()
        For Each column As DataGridViewColumn In dgTabla1.Columns()
            column.ContextMenuStrip = strip
            column.ContextMenuStrip.Items.Add(toolStripItem1)
            column.ContextMenuStrip.Items.Add(toolStripItem2)
            column.ContextMenuStrip.Items.Add(toolStripItem3)
        Next
    End Sub

    Private mouseLocation As DataGridViewCellEventArgs
    Private Sub dgTabla1_CellMouseEnter(ByVal sender As Object, ByVal location As DataGridViewCellEventArgs) Handles dgTabla1.CellMouseEnter
        mouseLocation = location
    End Sub

    Private Function recuperarCodPersDesem(ByVal idOp As Integer, ByVal tipo As Short) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPersDes),0) from TPersDesem where idOP=" & idOp & " and tipoA=" & tipo
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodPers(ByVal codPersDes As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(codPers),0) from TPersDesem where codPersDes=" & codPersDes
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub toolStripItem1_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem1.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE ITEM A COMPROBAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(15) = 2 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(13) = 1 Then  '1=aprobado  2=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA APROBADO...")
            Exit Sub
        End If

        Dim codPersDes As Integer = recuperarCodPersDesem(BindingSource1.Item(BindingSource1.Position)(0), 2) '2=Gerencia
        If codPersDes > 0 Then 'Existe firma
            If recuperarCodPers(codPersDes) <> vPass Then 'Usurio no es de dirma inicial
                MessageBox.Show("Proceso Denegado, Usuario no es de la Firma Inicial...", nomNegocio, Nothing, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de [APROBAR] Orden de Desembolso Nº " & BindingSource1.Item(BindingSource1.Position)(3) & " - " & BindingSource1.Item(BindingSource1.Position)(4) & Chr(13) & "  de Monto de dinero de " & BindingSource1.Item(BindingSource1.Position)(5) & " " & BindingSource1.Item(BindingSource1.Position)(6) & Chr(13) & "SI APRUEBA NO PODRA DESHACER PROCESO...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
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

            Dim idOP As Integer = BindingSource1.Item(BindingSource1.Position)(0)


            If BindingSource1.Item(BindingSource1.Position)(14) = 0 Then 'no existe firma insertar
                'TPersDesem
                comandoInsert2(BindingSource1.Item(BindingSource1.Position)(0), vPass, 1, 2, "", Now.Date)  '1=aprobado  2=gerencia
                cmInserTable2.Transaction = myTrans
                If cmInserTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Else 'existe firma actualizar
                'TPersDesem
                comandoUpdate3(1, "", BindingSource1.Item(BindingSource1.Position)(14)) '1=aprobado
                cmUpdateTable3.Transaction = myTrans
                If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesemGerencia1").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesemGerencia1")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("idOP", idOP)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan1 = True
            visualizarDet()
            colorearFila()
            calcularTotales()

            StatusBarClass.messageBarraEstado("  DESEMBOLSO FUE APROBADO CON EXITO...")
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

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal idOP As Integer, ByVal codPers As Integer, ByVal estado As Integer, ByVal tipo As Integer, ByVal obs As String, ByVal fecha As String)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.Text
        cmInserTable2.CommandText = "insert into TPersDesem(idOP,codPers,estDesem,tipoA,obserDesem,fecFir) values(@id,@codP,@est,@tipo,@obs,@fec)"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@id", SqlDbType.Int, 0).Value = idOP
        cmInserTable2.Parameters.Add("@codP", SqlDbType.Int, 0).Value = codPers 'vPass
        cmInserTable2.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado '1=Aprobado
        cmInserTable2.Parameters.Add("@tipo", SqlDbType.Int, 0).Value = tipo '1=Solicitante
        cmInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmInserTable2.Parameters.Add("@fec", SqlDbType.Date).Value = fecha
    End Sub

    Dim cmUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal estado As Short, ByVal obs As String, ByVal codPersDes As Integer)
        cmUpdateTable3 = New SqlCommand
        cmUpdateTable3.CommandType = CommandType.Text
        cmUpdateTable3.CommandText = "update TPersDesem set estDesem=@est,obserDesem=@obs where codPersDes=@cod"
        cmUpdateTable3.Connection = Cn
        cmUpdateTable3.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable3.Parameters.Add("@obs", SqlDbType.VarChar, 100).Value = obs
        cmUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codPersDes
    End Sub

    Private Sub toolStripItem2_Click(ByVal sender As Object, ByVal args As EventArgs) Handles toolStripItem2.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE ITEM A OBSERVAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(15) = 2 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(13) = 2 Then  '1=aprobado  2=Observado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA OBSERVADO...")
            Exit Sub
        End If

        Dim codPersDes As Integer = recuperarCodPersDesem(BindingSource1.Item(BindingSource1.Position)(0), 2) '2=Gerencia
        If codPersDes > 0 Then 'Existe firma
            If recuperarCodPers(codPersDes) <> vPass Then 'Usurio no es de dirma inicial
                MessageBox.Show("Proceso Denegado, Usuario no es de la Firma Inicial...", nomNegocio, Nothing, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Dim comentario As New CometarioForm
        comentario.ShowDialog()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            Dim idOP As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            If BindingSource1.Item(BindingSource1.Position)(14) = 0 Then 'no existe firma insertar
                'TPersDesem
                comandoInsert2(BindingSource1.Item(BindingSource1.Position)(0), vPass, 2, 2, vObs, Now.Date)  '2=observado  2=gerencia
                cmInserTable2.Transaction = myTrans
                If cmInserTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Else 'existe firma actualizar
                'TPersDesem
                comandoUpdate3(2, vObs, BindingSource1.Item(BindingSource1.Position)(14)) '2=observado
                cmUpdateTable3.Transaction = myTrans
                If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesemGerencia1").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesemGerencia1")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("idOP", idOP)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan1 = True
            visualizarDet()
            colorearFila()
            calcularTotales()

            StatusBarClass.messageBarraEstado("  DESEMBOLSO FUE APROBADO CON EXITO...")
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
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  SELECCIONE ITEM A DENEGAR...")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(15) = 2 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, Estado CERRADO")
            Exit Sub
        End If

        If BindingSource1.Item(BindingSource1.Position)(13) = 3 Then  '1=aprobado  3=denegado
            StatusBarClass.messageBarraEstado("PROCESO DENEGADO, YA ESTA DENEGADO...")
            Exit Sub
        End If

        Dim codPersDes As Integer = recuperarCodPersDesem(BindingSource1.Item(BindingSource1.Position)(0), 2) '2=Gerencia
        If codPersDes > 0 Then 'Existe firma
            If recuperarCodPers(codPersDes) <> vPass Then 'Usurio no es de dirma inicial
                MessageBox.Show("Proceso Denegado, Usuario no es de la Firma Inicial...", nomNegocio, Nothing, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Dim comentario As New CometarioForm
        comentario.ShowDialog()

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            Dim idOP As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            If BindingSource1.Item(BindingSource1.Position)(14) = 0 Then 'no existe firma insertar
                'TPersDesem
                comandoInsert2(BindingSource1.Item(BindingSource1.Position)(0), vPass, 3, 2, vObs, Now.Date)  '3=denegado  2=gerencia
                cmInserTable2.Transaction = myTrans
                If cmInserTable2.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Else 'existe firma actualizar
                'TPersDesem
                comandoUpdate3(3, vObs, BindingSource1.Item(BindingSource1.Position)(14)) '3=denegado
                cmUpdateTable3.Transaction = myTrans
                If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True

            vfVan1 = False

            'Actualizando el dataTable
            dsAlmacen.Tables("VOrdenDesemGerencia1").Clear()
            daTabla1.Fill(dsAlmacen, "VOrdenDesemGerencia1")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("idOP", idOP)

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            vfVan1 = True
            visualizarDet()
            colorearFila()
            calcularTotales()

            StatusBarClass.messageBarraEstado("  DESEMBOLSO FUE APROBADO CON EXITO...")
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        Dim datos As DataSetInformesCr = CargarDatos()

        Dim frm As New ReportViewerAprobacionDesembolso(datos)
        frm.ShowDialog()



    End Sub

    Private Function CargarDatos() As DataSetInformesCr
        Dim ds As New DataSetInformesCr

        For Each row As DataGridViewRow In dgTabla1.Rows

            Dim rowInf As DataSetInformesCr.DatosAprobacionDesemRow = ds.DatosAprobacionDesem.NewDatosAprobacionDesemRow
            rowInf.fecDes = CDate(row.Cells("fecDes").Value)
            rowInf.simbolo = CStr(row.Cells("simbolo").Value)
            rowInf.monto = CDbl(row.Cells("monto").Value)
            rowInf.nombre = CStr(row.Cells("nombre").Value)
            rowInf.razon = CStr(row.Cells("razon").Value)
            rowInf.nro = CStr(row.Cells("nro").Value)
            If IsDBNull(row.Cells("nom").Value) Then
                rowInf.nom = ""
            Else
                rowInf.nom = CStr(row.Cells("nom").Value)
            End If

            rowInf.serie = CStr(row.Cells("serie").Value)
            If IsDBNull(row.Cells("estApro").Value) Then
                rowInf.estApro = "PENDIENTE"
            Else
                rowInf.estApro = CStr(row.Cells("estApro").Value)
            End If


            ds.DatosAprobacionDesem.AddDatosAprobacionDesemRow(rowInf)

        Next

        Return ds

    End Function

End Class
