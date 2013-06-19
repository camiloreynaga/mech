Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class jalarSolicitud1Form
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable4 As New DataTable()
    Dim dataTable5 As New DataTable()

    Dim bindingSource4 As New BindingSource()
    Dim bindingSource5 As New BindingSource()

    Private Sub jalarSolicitud1Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idSol,nroS,nro,fecSol,nroSoliConca,nombres,obs,est,lugar,codigo,codPers,estado from VSolTodoCad1 where codigo=@cod"
        crearDataAdapterTable(dTable4, sele)
        dTable4.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vCod3  'codigo obra

        sele = "select codDetS,prioridad,cant,unidad,descrip,areaM,estSol,nombres,obs1,nombres1,obs2,idSol,codMat,codPers,codPersA,codAreaM,codEstS from VDetSol where codEstS=2 and idSol=@idS"   '2=Aprobado
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
                bindingSource5.Item(j)(17) = True
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

        Dim resp As String = MessageBox.Show("Esta segúro de agregar requerimientos" & Chr(13) & " a orden de compra Nº " & vCod1, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TOrdenCompra
            comandoUpdate1()
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            For j As Short = 0 To bindingSource5.Count - 1
                'If dataTable5.Rows(j)(17) = True Then
                'End If
                If bindingSource5.Item(j)(17) = True Then 'solo los Check  
                    'TDetalleOrden
                    comandoInsert1(bindingSource5.Item(j)(2), bindingSource5.Item(j)(3), bindingSource5.Item(j)(4), bindingSource5.Item(j)(12), vNroOrden)
                    cmInserTable1.Transaction = myTrans
                    If cmInserTable1.ExecuteNonQuery() < 1 Then
                        wait.Close()
                        Me.Cursor = Cursors.Default
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                    Dim codDetC As Integer = cmInserTable1.Parameters("@Identity").Value
                End If
            Next

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

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1()
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TOrdenCompra set idSol=@codC where nroOrden=@nro"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@codC", SqlDbType.Int, 0).Value = bindingSource4.Item(bindingSource4.Position)(0)
        cmUpdateTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = vNroOrden
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal cant As Decimal, ByVal uni As String, ByVal des As String, ByVal codmat As Integer, ByVal nroOrden As Integer)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.StoredProcedure
        cmInserTable1.CommandText = "PA_InsertDetalleOrden"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmInserTable1.Parameters.Add("@uni", SqlDbType.VarChar, 20).Value = uni
        cmInserTable1.Parameters.Add("@des", SqlDbType.VarChar, 100).Value = des
        cmInserTable1.Parameters.Add("@pre", SqlDbType.Decimal, 0).Value = 0
        cmInserTable1.Parameters.Add("@sub", SqlDbType.Decimal, 0).Value = 0
        cmInserTable1.Parameters.Add("@codM", SqlDbType.Int, 0).Value = codmat
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = nroOrden
        'configurando direction output = parametro de solo salida
        cmInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub
End Class
