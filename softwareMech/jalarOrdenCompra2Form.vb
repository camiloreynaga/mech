Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class jalarOrdenCompra2Form
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable4 As New DataTable()
    Dim dataTable5 As New DataTable()

    Dim bindingSource4 As New BindingSource()
    Dim bindingSource5 As New BindingSource()

    Private Sub jalarOrdenCompra2Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select nroOrden,nroO,nro,fecOrden,razon,ruc,cuentaBan,nroSol,moneda,obra,obsFac,estado,codigo,idSol,codMon,simbolo,igv,calIGV,codIde,cuentaDet,forma from VOrdenTodoCad where nroOrden=@cod"
        crearDataAdapterTable(dTable4, sele)
        dTable4.SelectCommand.Parameters.Add("@cod", SqlDbType.Int, 0).Value = vNroOrden  'codigo obra

        sele = "select codDetO,cant,unidad,descrip,precio,subTotal,nroOrden,codMat from VDetOrden where nroOrden=@idS"
        crearDataAdapterTable(dTable5, sele)
        dTable5.SelectCommand.Parameters.Add("@idS", SqlDbType.Int, 0).Value = 0

        Try
            'llenar la tabla virtual con los dataAdapter
            dTable4.Fill(dataTable4)
            dTable5.Fill(dataTable5)

            bindingSource4.DataSource = dataTable4
            bindingSource4.Sort = "nroOrden"

            bindingSource5.DataSource = dataTable5
            Navigator2.BindingSource = bindingSource5
            dgTabla2.DataSource = bindingSource5
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            bindingSource5.Sort = "descrip"
            ModificarColumnasDGV()

            configurarColorControl()

            txtNro.DataBindings.Add("Text", bindingSource4, "nro")
            txtFec.DataBindings.Add("Text", bindingSource4, "fecOrden")
            txtRuc.DataBindings.Add("Text", bindingSource4, "ruc")
            txtRaz.DataBindings.Add("Text", bindingSource4, "razon")
            txtCue.DataBindings.Add("Text", bindingSource4, "cuentaBan")
            txtDet.DataBindings.Add("Text", bindingSource4, "cuentaDet")
            txtFor.DataBindings.Add("Text", bindingSource4, "forma")
            txtObs.DataBindings.Add("Text", bindingSource4, "obsFac")
            txtObra.DataBindings.Add("Text", bindingSource4, "obra")

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
        Label15.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
            txtRel.Text = txtOrden.Text
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
            cALetra.Numero = Val(CDbl(txtTotal.Text))
            txtLetraTotal.Text = "SON: " & cALetra.ALetra.ToUpper()
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If bindingSource4.Position = -1 Then
            StatusBarClass1.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        vCodDoc = bindingSource4.Item(bindingSource4.Position)(0)
        vParam1 = "Nº " & bindingSource4.Item(bindingSource4.Position)(2) & "-MECH-" & CDate(bindingSource4.Item(bindingSource4.Position)(3)).Year
        vParam2 = txtLetraTotal.Text.Trim()
        vParam3 = txtSub.Text.Trim()
        vParam4 = txtIGV.Text.Trim()
        vParam5 = txtTotal.Text.Trim()
        Dim informe As New ReportViewerOrdenCompraForm
        informe.ShowDialog()
    End Sub
End Class
