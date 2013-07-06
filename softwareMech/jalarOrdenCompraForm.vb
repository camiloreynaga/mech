Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class jalarOrdenCompraForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable4 As New DataTable()
    Dim dataTable5 As New DataTable()

    Dim bindingSource4 As New BindingSource()
    Dim bindingSource5 As New BindingSource()

    Private Sub jalarOrdenCompraForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select nroOrden,nroO,nro,fecOrden,razon,ruc,cuentaBan,nroSol,moneda,obra,obsFac,estado,codigo,idSol,codMon,simbolo,igv,calIGV from VOrdenTodoCad"
        crearDataAdapterTable(dTable4, sele)
        'dTable4.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vCod3  'codigo obra

        sele = "select codDetO,cant,unidad,descrip,precio,subTotal,nroOrden,codMat from VDetOrden where nroOrden=@idS"
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
            bindingSource4.Sort = "nroOrden"

            bindingSource5.DataSource = dataTable5
            Navigator2.BindingSource = bindingSource5
            dgTabla2.DataSource = bindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            configurarColorControl()

            bindingSource4.MoveLast()

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

    Private Sub jalarOrdenCompraForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To bindingSource4.Count - 1
            If recuperarNroOrdenDes(bindingSource4.Item(j)(0)).Trim() <> "" Then 'Sin Enlace de Desembolso
                dgTabla1.Rows(j).DefaultCellStyle.BackColor = Color.YellowGreen
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Width = 50
            .Columns(2).HeaderText = "NºOrd."
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).HeaderText = "Fecha"
            .Columns(3).Width = 70
            .Columns(4).Width = 200
            .Columns(4).HeaderText = "Proveedor"
            .Columns(5).Width = 80
            .Columns(5).HeaderText = "RUC"
            .Columns(6).Width = 100
            .Columns(6).HeaderText = "Cuenta"
            .Columns(7).Width = 80
            .Columns(7).HeaderText = "NºSol."
            .Columns(8).Width = 100
            .Columns(8).HeaderText = "Moneda"
            .Columns(9).HeaderText = "Obra / Lugar"
            .Columns(9).Width = 300
            .Columns(10).HeaderText = "Observac."
            .Columns(10).Width = 300
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(1).HeaderText = "Cant."
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Unid."
            .Columns(2).ReadOnly = True 'NO editable
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 417
            .Columns(3).ReadOnly = True 'NO editable
            .Columns(4).Width = 75
            .Columns(4).HeaderText = "Prec_Unit"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).Width = 75
            .Columns(5).HeaderText = "Total"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).Visible = False
            .Columns(7).Visible = False
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
        Label6.ForeColor = ForeColorLabel
        Label15.ForeColor = ForeColorLabel
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
            'colorearFila()

            calcularSubTotal()

            txtOrden.Text = recuperarNroOrdenDes(bindingSource4.Item(bindingSource4.Position)(0))
        End If
    End Sub

    Private Function recuperarNroOrdenDes(ByVal nroOrden As Integer) As String
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select mech.FN_ConcaNroOrdenDes(" & nroOrden & ")"
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfIGV As Double = vSIGV
    Private Sub calcularSubTotal()
        If bindingSource5.Position = -1 Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        vfIGV = bindingSource4.Item(bindingSource4.Position)(16)

        If bindingSource4.Item(bindingSource4.Position)(17) = 1 Then  'tipo 1
            txtTotal.Text = Format((dataTable5.Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0.00")
            txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0.00")
        Else  'Tipo 2
            txtSub.Text = Format((dataTable5.Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0.00")
            txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0.00")
        End If
        lblTotal.Text = "TOTAL  " & bindingSource4.Item(bindingSource4.Position)(15) 'cbMoneda.Text.Trim()
        cambiarNroTotalLetra()
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        If bindingSource4.Item(bindingSource4.Position)(14) = 30 Then    '30=Nuevos solesl
            cALetra.Moneda = "Nuevos Soles"
        Else    'dolares
            cALetra.Moneda = "Dólares Americanos"
        End If
        'Inicia el Proceso para identificar la cantidad a convertir
        If Val(txtTotal.Text) > 0 Then
            cALetra.Numero = Val(txtTotal.Text)
            txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
        End If
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        If bindingSource5.Position = -1 Then
            MessageBox.Show("No Existe Detalle de Orden...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If vCod3.Trim() <> "" Then
            MessageBox.Show("Proceso denegado, Orden Desembolso Nº " & vCod1 & " ya esta enlazado con Orden de Compra Nº " & vCod3.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Esta segúro de relacionar con" & Chr(13) & "Orden Desembolso Nº " & vCod1, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TDesOrden
            comandoInsert1(vNroOrden, bindingSource4.Item(bindingSource4.Position)(0))
            cmInserTable1.Transaction = myTrans
            If cmInserTable1.ExecuteNonQuery() < 1 Then
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

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal idOP As Integer, ByVal nroOrden As Integer)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert into TDesOrden(idOP,nroOrden) values(@id,@nro)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@id", SqlDbType.Int, 0).Value = idOP
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = nroOrden
    End Sub
End Class
