Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class jalarSolicitud2Form
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable4 As New DataTable()
    Dim dataTable5 As New DataTable()

    Dim bindingSource4 As New BindingSource()
    Dim bindingSource5 As New BindingSource()

    Private Sub jalarSolicitud2Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nroS,nro,fecSol,nroSoliConca,nombres,obs,est,lugar,codigo,codPers,estado from VSolTodoCad1 where codigo=@cod"
        crearDataAdapterTable(dTable4, sele)
        dTable4.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vCod3  'codigo obra

        sele = "select codDetS,prioridad,cant,unidad,descrip,areaM,estSol,nombres,obs1,nombres1,obs2,idSol,codMat,codPers,codTipM,codAreaM,codEstS from VDetSol where codEstS=2 and idSol=@idS"   '2=Aprobado
        crearDataAdapterTable(dTable5, sele)
        dTable5.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'llenar la tabla virtual con los dataAdapter
            dTable4.Fill(dataTable4)
            dTable5.Fill(dataTable5)

            bindingSource4.DataSource = dataTable4
            Navigator1.BindingSource = bindingSource4
            dgTabla1.DataSource = bindingSource4
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource4.Sort = "idSol"

            dataTable5.Columns.Add("Check", Type.GetType("System.Boolean")) 'añadiendo una columna check al final del grid

            bindingSource5.DataSource = dataTable5
            Navigator2.BindingSource = bindingSource5
            dgTabla2.DataSource = bindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            configurarColorControl()

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

    Private Sub jalarSolicitud2Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVan3 = True
        visualizarDet()
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "NºSol."
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).HeaderText = "Fecha"
            .Columns(3).Width = 70
            .Columns(4).Width = 90
            .Columns(4).HeaderText = "Solicitud"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).Width = 160
            .Columns(5).HeaderText = "Residente"
            .Columns(6).HeaderText = "Obser."
            .Columns(6).Width = 200
            .Columns(7).HeaderText = "Estado"
            .Columns(7).Width = 80
            .Columns(8).HeaderText = "Obra"
            .Columns(8).Width = 260
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Necesid."
            .Columns(1).Width = 65
            .Columns(1).ReadOnly = True 'NO editable
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "Cant."
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Unid."
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).HeaderText = "Descripción Insumo"
            .Columns(4).Width = 348
            .Columns(4).ReadOnly = True 'NO editable
            .Columns(5).HeaderText = "Area_Insumo"
            .Columns(5).Width = 100
            .Columns(5).ReadOnly = True 'NO editable
            .Columns(6).HeaderText = "Estado"
            .Columns(6).Width = 75
            .Columns(6).ReadOnly = True 'NO editable
            .Columns(7).Width = 100
            .Columns(7).HeaderText = "Solicitante"
            .Columns(7).ReadOnly = True 'NO editable
            .Columns(8).Visible = False
            .Columns(8).Width = 300
            .Columns(8).HeaderText = "Observacion solicitante"
            .Columns(9).Visible = False
            .Columns(9).Width = 100
            .Columns(9).HeaderText = "Verificador"
            .Columns(10).Visible = False
            .Columns(10).Width = 400
            .Columns(10).HeaderText = "Observacion verificador"
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).HeaderText = "Check"
            .Columns(17).Width = 45
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
        Label6.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        Me.Cursor = Cursors.WaitCursor
        visualizarDet()
        Me.Cursor = Cursors.Default
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If bindingSource4.Position = -1 Then
                Exit Sub
            End If
            dataTable5.Clear()
            dTable5.SelectCommand.Parameters("@idS").Value = bindingSource4.Item(bindingSource4.Position)(0)
            dTable5.Fill(dataTable5)
            colorearFila()

            For j As Short = 0 To bindingSource5.Count - 1 'Refrescando check a True
                bindingSource5.Item(j)(17) = False
            Next
        End If
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To bindingSource5.Count - 1
            If bindingSource5.Item(j)(16) = 2 Then 'Aprobado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
            If bindingSource5.Item(j)(16) = 3 Then 'Observado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Yellow
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.Red
            End If
            If bindingSource5.Item(j)(16) = 4 Then 'Rechazado
                dgTabla2.Rows(j).Cells(6).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Function recuperarMontoSol(ByVal codSC As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select CAST(sum((cant1*prec1)) as decimal(8,2)) from TDetSolCaja where codSC=" & codSC
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        ToolStripTextBox1.Focus() ' con el objetivo de que cambie el grid de modo edicion a a modo recorrer

        If bindingSource5.Position = -1 Then
            MessageBox.Show("No Existe Detalle de Solicitud...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim vFSigue As Boolean = True
        For j As Short = 0 To bindingSource5.Count - 1
            If bindingSource5.Item(j)(17) = True Then 'chekeados solo agregar  
                vFSigue = False
                Exit For
            End If
        Next

        If vFSigue Then
            MessageBox.Show("Proceso denegado, ningun insumo esta CHEKEADO", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de agregar requerimientos" & Chr(13) & " a solicitud de caja Nº " & vCod1, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass1.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            'TSolicitudCaja
            comandoUpdate3(0, vNroOrden) '0=PENDIENTE
            cmUpdateTable3.Transaction = myTrans
            If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("ERROR DE CONCURRENCIA, VUELVA A EJECUTAR LA INTERFAZ." & Chr(13) & "No se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            For j As Short = 0 To bindingSource5.Count - 1
                If bindingSource5.Item(j)(17) = True Then 'solo los Check
                    'TDetSolCaja
                    comandoInsert2(bindingSource5.Item(j)(2), bindingSource5.Item(j)(3), bindingSource5.Item(j)(4), 0, bindingSource5.Item(j)(12), bindingSource5.Item(j)(15), bindingSource5.Item(j)(14), vNroOrden, 1) '1=Factura x defecto
                    cmInserTable2.Transaction = myTrans
                    If cmInserTable2.ExecuteNonQuery() < 1 Then
                        wait.Close()
                        Me.Cursor = Cursors.Default
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                    Dim codDetSol As Integer = cmInserTable2.Parameters("@Identity").Value
                End If
            Next

            'TSolicitudCaja actualizando monto de detalle de solicitud 
            comandoUpdate2(recuperarMontoSol(vNroOrden, myTrans), vNroOrden)
            cmUpdateTable2.Transaction = myTrans
            If cmUpdateTable2.ExecuteNonQuery() < 1 Then
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

            StatusBarClass1.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            vCod2 = bindingSource4.Item(bindingSource4.Position)(0)

            wait.Close()
            Me.Cursor = Cursors.Default

            Me.Close() 'retornar
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

    Dim cmUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal est As Short, ByVal codSC As Integer)
        cmUpdateTable3 = New SqlCommand
        cmUpdateTable3.CommandType = CommandType.Text
        cmUpdateTable3.CommandText = "update TSolicitudCaja set estSol=@est where codSC=@cod"
        cmUpdateTable3.Connection = Cn
        cmUpdateTable3.Parameters.Add("@est", SqlDbType.Int, 0).Value = est
        cmUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codSC
    End Sub

    Dim cmInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal cant As Decimal, ByVal uni As String, ByVal insumo As String, ByVal precio As Decimal, ByVal codMat As Integer, ByVal codAreaM As Integer, ByVal codTipM As Integer, ByVal codSC As Integer, ByVal compro As Integer)
        cmInserTable2 = New SqlCommand
        cmInserTable2.CommandType = CommandType.StoredProcedure
        cmInserTable2.CommandText = "PA_InsertDetSolCaja"
        cmInserTable2.Connection = Cn
        cmInserTable2.Parameters.Add("@can1", SqlDbType.Decimal, 0).Value = cant
        cmInserTable2.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = cant
        cmInserTable2.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable2.Parameters.Add("@ins", SqlDbType.VarChar, 200).Value = insumo
        cmInserTable2.Parameters.Add("@ing", SqlDbType.Int, 0).Value = 0 '0=normal, 1=improvisado
        cmInserTable2.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = precio
        cmInserTable2.Parameters.Add("@pre2", SqlDbType.Decimal, 0).Value = precio
        cmInserTable2.Parameters.Add("@obsSol", SqlDbType.VarChar, 200).Value = ""
        cmInserTable2.Parameters.Add("@codApro", SqlDbType.Int, 0).Value = 0 'No se aprobo todabia
        cmInserTable2.Parameters.Add("@estDet", SqlDbType.Int, 0).Value = 0 'pendiente
        cmInserTable2.Parameters.Add("@obsApro", SqlDbType.VarChar, 200).Value = ""
        cmInserTable2.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = codMat
        cmInserTable2.Parameters.Add("@codAreaM", SqlDbType.Int, 0).Value = codAreaM
        cmInserTable2.Parameters.Add("@codTipM", SqlDbType.Int, 0).Value = codTipM
        cmInserTable2.Parameters.Add("@codSC", SqlDbType.Int, 0).Value = codSC
        cmInserTable2.Parameters.Add("@estRen", SqlDbType.Int, 0).Value = 0 'pendiente
        cmInserTable2.Parameters.Add("@codRen", SqlDbType.Int, 0).Value = 0  'no se rindio
        cmInserTable2.Parameters.Add("@obsRen", SqlDbType.VarChar, 200).Value = ""
        cmInserTable2.Parameters.Add("@codDC", SqlDbType.Int, 0).Value = 0
        cmInserTable2.Parameters.Add("@nroO", SqlDbType.VarChar, 30).Value = ""
        cmInserTable2.Parameters.Add("@comp", SqlDbType.Int, 0).Value = compro
        cmInserTable2.Parameters.Add("@fec", SqlDbType.VarChar, 10).Value = ""
        cmInserTable2.Parameters.Add("@comp1", SqlDbType.Int, 0).Value = compro
        'configurando direction output = parametro de solo salida
        cmInserTable2.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable2.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal montoSol As Decimal, ByVal codSC As Integer)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TSolicitudCaja set montoSol=@monto where codSC=@cod"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@monto", SqlDbType.Decimal, 0).Value = montoSol
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codSC
    End Sub
End Class
