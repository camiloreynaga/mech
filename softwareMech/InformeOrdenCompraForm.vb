Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class InformeOrdenCompraForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub InformeOrdenCompraForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub InformeOrdenCompraForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select nroOrden,nro,fecOrden,est,nroCotCad,nroSolCad,igv,calIGV,atiendeCom,repres,celAti,plazoEnt,lugarEnt,transfe,obsFac,estado,codIde,razon,ruc,dir,fono1,celRpm,emailProv,cuentaBan,codPers,nomRem,fono,email,codPersO,nomAte,codPag,forma,codMon,moneda,simbolo,codigo,nombre,codCot,nroCot,hist,nroProf from VOrdenInforme"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select codDetO,cant,unidad,descrip,precio,subTotal,nroOrden,codMat from VDetOrden where nroOrden=@nro"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VOrdenInforme")
            daDetDoc.Fill(dsAlmacen, "VDetOrden")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VOrdenInforme"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            'dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource1.Sort = "nroOrden"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetOrden"
            Navigator2.BindingSource = BindingSource2
            dgTabla2.DataSource = BindingSource2
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource2.Sort = "descrip"
            ModificarColumnasDGV()
          

            configurarColorControl()

            rb1.Checked = True
            vfVan2 = True
            enlazarText()

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

    Private Sub InformeOrdenCompraForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        colorearFila()
        BindingSource1.MoveLast()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(15) = 1 Then 'Terminado
                dgTabla1.Rows(j).Cells(3).Style.BackColor = Color.Green
            End If
            If BindingSource1.Item(j)(15) = 2 Then 'Cerrado
                dgTabla1.Rows(j).Cells(3).Style.BackColor = Color.AliceBlue
            End If
            If BindingSource1.Item(j)(15) = 3 Then 'Anulado
                dgTabla1.Rows(j).Cells(3).Style.BackColor = Color.Yellow
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "NºOrd."
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Fec_Ord"
            .Columns(2).Width = 70
            .Columns(3).Width = 75
            .Columns(3).HeaderText = "Estado"
            .Columns(4).Width = 60
            .Columns(4).HeaderText = "NºCotiz."
            .Columns(5).Width = 60
            .Columns(5).HeaderText = "NºSolic."
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
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
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .Columns(25).Visible = False
            .Columns(26).Visible = False
            .Columns(27).Visible = False
            .Columns(28).Visible = False
            .Columns(29).Visible = False
            .Columns(30).Visible = False
            .Columns(31).Visible = False
            .Columns(32).Visible = False
            .Columns(33).Visible = False
            .Columns(34).Visible = False
            .Columns(35).Visible = False
            .Columns(36).Visible = False
            .Columns(37).Visible = False
            .Columns(38).Visible = False
            .Columns(39).Width = 800
            .Columns(39).HeaderText = ""
            .Columns(40).Visible = False
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
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 417
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
        Label15.ForeColor = ForeColorLabel
        CheckBoxIGV.ForeColor = ForeColorLabel
        GroupBox1.ForeColor = ForeColorLabel
        GroupBox2.ForeColor = ForeColorLabel
        GroupBox3.ForeColor = ForeColorLabel
        GroupBox4.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        enlazarText()
        visualizarDet()
    End Sub

    Dim vfVan2 As Boolean = False
    Private Sub enlazarText()
        If vfVan2 Then
            Me.Cursor = Cursors.WaitCursor
            If BindingSource1.Count = 0 Then
                'desEnlazarText()
            Else
                txtProv.Text = BindingSource1.Item(BindingSource1.Position)(17)
                txtRuc.Text = BindingSource1.Item(BindingSource1.Position)(18)
                txtFono.Text = BindingSource1.Item(BindingSource1.Position)(20)
                txtAte.Text = BindingSource1.Item(BindingSource1.Position)(8)
                txtCel.Text = BindingSource1.Item(BindingSource1.Position)(21)
                txtEma.Text = BindingSource1.Item(BindingSource1.Position)(22)
                txtRem.Text = BindingSource1.Item(BindingSource1.Position)(25)
                txtFono1.Text = BindingSource1.Item(BindingSource1.Position)(26)
                txtEma1.Text = BindingSource1.Item(BindingSource1.Position)(27)
                txtPla.Text = BindingSource1.Item(BindingSource1.Position)(11)
                txtPago.Text = BindingSource1.Item(BindingSource1.Position)(31)
                txtTran.Text = BindingSource1.Item(BindingSource1.Position)(23)
                txtPro.Text = BindingSource1.Item(BindingSource1.Position)(40)
                txtObra.Text = BindingSource1.Item(BindingSource1.Position)(36)
                txtMon.Text = BindingSource1.Item(BindingSource1.Position)(34)
                txtLug.Text = BindingSource1.Item(BindingSource1.Position)(12)
                txtPers.Text = BindingSource1.Item(BindingSource1.Position)(29)
                txtObs.Text = BindingSource1.Item(BindingSource1.Position)(14)
                
                If BindingSource1.Item(BindingSource1.Position)(6) = 0 Then 'Sin IGV
                    CheckBoxIGV.Checked = True
                Else 'Con IGV
                    CheckBoxIGV.Checked = False
                    If BindingSource1.Item(BindingSource1.Position)(7) = 1 Then 'calculos igv
                        rb1.Checked = True
                    Else  'calculo 2
                        rb2.Checked = True
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetOrden").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VDetOrden")
            'colorearFila()
            calcularSubTotal()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Dim vfIGV As Double = vSIGV
    Private Sub calcularSubTotal()
        If BindingSource2.Position = -1 Then
            txtTotal.Text = ""
            txtIGV.Text = ""
            txtSub.Text = ""
            txtLetraTotal.Text = "SON:"
            Exit Sub
        End If

        If rb1.Checked Then  'tipo 1
            txtTotal.Text = Format((dsAlmacen.Tables("VDetOrden").Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format(((txtTotal.Text * vfIGV) / (100 + vfIGV)), "0.00")
            txtSub.Text = Format((txtTotal.Text - txtIGV.Text), "0.00")
        Else  'Tipo 2
            txtSub.Text = Format((dsAlmacen.Tables("VDetOrden").Compute("Sum(subTotal)", Nothing)), "0.00")
            txtIGV.Text = Format((txtSub.Text * vfIGV) / 100, "0.00")
            txtTotal.Text = Format((CDbl(txtSub.Text) + CDbl(txtIGV.Text)), "0.00")
        End If
        lblTotal.Text = "TOTAL  " & BindingSource1.Item(BindingSource1.Position)(34) 'cbMoneda.Text.Trim()
        cambiarNroTotalLetra()
    End Sub

    Private Sub CheckBoxIGV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxIGV.CheckedChanged
        If CheckBoxIGV.Checked Then 'Sin IGV
            vfIGV = 0
            GroupBox1.Visible = False
        Else 'Con IGV
            vfIGV = vSIGV
            GroupBox1.Visible = True
        End If
        calcularSubTotal()
    End Sub

    Private Sub rb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb1.CheckedChanged
        calcularSubTotal()
    End Sub

    Private Sub rb2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb2.CheckedChanged
        calcularSubTotal()
    End Sub

    Private Sub cambiarNroTotalLetra()
        Dim cALetra As New Num2LetEsp  'clase definida por el usuario
        If BindingSource1.Item(BindingSource1.Position)(32) = 30 Then    '30=Nuevos solesl
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

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Orden de Compra...")
            Exit Sub
        End If

        vCodDoc = BindingSource1.Item(BindingSource1.Position)(0)
        vParam1 = BindingSource1.Item(BindingSource1.Position)(1) & "-MECH-" & CDate(BindingSource1.Item(BindingSource1.Position)(2)).Year
        vParam2 = txtLetraTotal.Text.Trim()
        vParam3 = txtSub.Text.Trim()
        vParam4 = txtIGV.Text.Trim()
        vParam5 = txtTotal.Text.Trim()
        Dim informe As New ReportViewerOrdenCompraForm
        informe.ShowDialog()
    End Sub
End Class
