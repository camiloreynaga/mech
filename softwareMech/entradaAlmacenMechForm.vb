﻿Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class entradaAlmacenMechForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource11 As New BindingSource

    Private Sub entradaAlmacenMechForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        ' Primero capturamos la tecla pulsada, ... 
        If keyData = Keys.F5 Then
            btnExtraer.PerformClick()
        End If

        ' ... y después llamamos al procedimiento de la clase base. 
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub entradaAlmacenMechForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta tipoM si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select distinct codigo,nombre from VLugarUbiStoc TObra where codigo=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codUbi,ubicacion,codigo from VLugarUbiStoc TUbicacion where codigo=@cod"
        crearDataAdapterTable(daTUbi, sele)
        daTUbi.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 0).Value = vSCodigo

        sele = "select codTrans,tipo from TTipoTransac"
        crearDataAdapterTable(daTabla2, sele)

        sele = "select nroNota,tipo,fecha,material,cantEnt,preUniEnt,cantSal,preUniSal,saldo,unidad,nroGuia,nroDoc,otroDoc,ubicacion,nombre,nom,obs,codMat,idMU,codUbi,codigo,codGuia,codDoc,codTrans,codPers,codSal from VKardex where codMat=@codMat and codUbi=@codUbi" 'order by nroNota"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codUbi", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "TObra")
            daTUbi.Fill(dsAlmacen, "TUbicacion")
            daTabla2.Fill(dsAlmacen, "TTipoTransac")
            daVKardex.Fill(dsAlmacen, "VKardex")

            AgregarRelacion()

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TObra"
            cbObra.DataSource = BindingSource2
            cbObra.DisplayMember = "nombre"
            cbObra.ValueMember = "codigo"

            BindingSource3.DataSource = BindingSource2
            BindingSource3.DataMember = "Relacion1"
            cbUbi.DataSource = BindingSource3
            cbUbi.DisplayMember = "ubicacion"
            cbUbi.ValueMember = "codUbi"

            cbPro.DataSource = dsAlmacen
            cbPro.DisplayMember = "TTipoTransac.tipo"
            cbPro.ValueMember = "codTrans"

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VKardex"
            Navigator2.BindingSource = BindingSource4
            dgTabla2.DataSource = BindingSource4
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource4.Sort = "nroNota"
            ModificarColumnasDGV()

            configurarColorControl()

            txtIns1.DataBindings.Add("Text", BindingSource4, "material")
            txtUbi.DataBindings.Add("Text", BindingSource4, "ubicacion")
            txtObra.DataBindings.Add("Text", BindingSource4, "nombre")

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub entradaAlmacenMechForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        BindingSource2.Position = BindingSource2.Find("codigo", vSCodigo)
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("TObra").Columns("codigo"), dsAlmacen.Tables("TUbicacion").Columns("codigo"))
        dsAlmacen.Relations.Add(relation1)
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla2
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Transac."
            .Columns(1).Width = 70
            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = "Descripción Insumo"
            .Columns(3).Width = 340
            .Columns(3).Visible = False
            .Columns(4).Width = 60
            .Columns(4).HeaderText = "CantEnt"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 50
            .Columns(5).HeaderText = "Ent.S/."
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 60
            .Columns(6).HeaderText = "CantSal"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).Width = 50
            .Columns(7).HeaderText = "Sal.S/."
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).Width = 65
            .Columns(8).HeaderText = "Saldo"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).Width = 40
            .Columns(9).HeaderText = "Unid."
            .Columns(10).Width = 80
            .Columns(10).HeaderText = "NºGuia"
            .Columns(11).Width = 80
            .Columns(11).HeaderText = "NºFact"
            .Columns(12).Width = 60
            .Columns(12).HeaderText = "OtroDoc"
            .Columns(12).Visible = False
            .Columns(13).HeaderText = "Ubicación"
            .Columns(13).Width = 100
            .Columns(13).Visible = False
            .Columns(14).HeaderText = "Sede/Obra"
            .Columns(14).Width = 180
            .Columns(14).Visible = False
            .Columns(15).Width = 90
            .Columns(15).HeaderText = "Usuario"
            .Columns(16).Width = 205
            .Columns(16).HeaderText = "Observacion"
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .Columns(25).Visible = False
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
        Label13.ForeColor = ForeColorLabel
        Label14.ForeColor = ForeColorLabel
        Label15.ForeColor = ForeColorLabel
        Label16.ForeColor = ForeColorLabel
        Label17.ForeColor = ForeColorLabel
        btnCrear.ForeColor = ForeColorButtom
        btnPro.ForeColor = ForeColorButtom
        btnDes.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub cbUbi_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbUbi.SelectedValueChanged, cbObra.SelectedValueChanged
        dsAlmacen.Tables("VKardex").Clear()
        seleStock()
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnExtraer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtraer.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource1.RemoveFilter()
        If vFClear1 Then
            vFVan1 = False
            dsAlmacen.Tables("VStockUbi").Clear()
            dsAlmacen.Tables("VMaterialStock").Clear()

            daVMat.Fill(dsAlmacen, "VMaterialStock")
            daVStock.Fill(dsAlmacen, "VStockUbi")

            BindingSource11.Filter = "codMat=" & BindingSource1.Item(BindingSource1.Position)(0)

            colorearFila()
            vFVan1 = True
            seleStock()
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,stock,uniBase,preBase,tipoM,hist,estado,codTipM,codUni from VMaterialStock" 'material
            crearDataAdapterTable(daVMat, sele)

            sele = "select idMU,stock,ubicacion+' - '+obra as ubi,codigo,codUbi,codMat,color from VStockUbi"
            crearDataAdapterTable(daVStock, sele)

            'crearDSAlmacen()
            daVMat.Fill(dsAlmacen, "VMaterialStock")
            daVStock.Fill(dsAlmacen, "VStockUbi")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VMaterialStock"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource1.Sort = "material"

            txtMat.DataBindings.Add("Text", BindingSource1, "material")
            txtUni.DataBindings.Add("Text", BindingSource1, "uniBase")

            BindingSource11.DataSource = dsAlmacen
            BindingSource11.DataMember = "VStockUbi"
            dgStock.DataSource = BindingSource11
            ModificarColumnasDGV1()

            BindingSource11.Filter = "codMat=" & BindingSource1.Item(BindingSource1.Position)(0)

            vFClear1 = True
            colorearFila()
            vFVan1 = True
            seleStock()
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        txtIns.Focus()
        txtIns.SelectAll()
    End Sub

    Dim vFVan1 As Boolean = False
    Private Sub seleStock()
        If vFVan1 Then
            If BindingSource1.Position <> -1 Then
                txtSto.Text = recuperarStock(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue)
                txtPre.Text = BindingSource1.Item(BindingSource1.Position)(4)
            End If
        End If
    End Sub

    Private Function recuperarStock(ByVal codMat As Integer, ByVal codUbi As Integer) As Double
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isNull(max(stock),-1) as stock from TMatUbi where codMat=" & codMat & " and codUbi=" & codUbi
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(2) >= 0 Then 'Aprobado
                'dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(2).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Width = 40
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 478
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Stock"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Unidad"
            .Columns(4).Width = 55
            .Columns(4).HeaderText = "PrecS/."
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).HeaderText = "Tipo Insumo"
            .Columns(5).Width = 120
            .Columns(5).Visible = False
            .Columns(6).Width = 1000
            .Columns(6).HeaderText = ""
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgStock
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Stock"
            .Columns(1).Width = 50
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).HeaderText = "Ubicacion"
            .Columns(2).Width = 500
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub

    Private Sub txtIns_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIns.TextChanged
        Dim campo As String
        campo = "material"

        'Tipo String
        BindingSource1.Filter = campo & " like '" & txtIns.Text.Trim() & "%'"

        If BindingSource1.Count > 0 Then
            'dgTabla1.Focus()
            StatusBarClass.messageBarraEstado("")
            'Me.AcceptButton = Me.btnAgrega
        Else
            txtSto.Clear()
            StatusBarClass.messageBarraEstado(" NO EXISTE INSUMO CON ESA CARACTERISTICA DE BUSQUEDA...")
        End If
    End Sub

    Private Sub txtIns_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIns.GotFocus, txtIns.MouseDown
        txtIns.SelectAll()
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidaFechaMayorXXXX(date1.Value.Date, 2013) Then
            MessageBox.Show("Ingrese fecha mayor al año 2012", nomNegocio, Nothing, MessageBoxIcon.Asterisk)
            Return True
        End If
        If ValidaNroDesdeRangoMinimoRangoMaximoEstablecido(txtCan.Text, 1, 9999) Then
            txtCan.errorProv()
            StatusBarClass.messageBarraEstado("  Ingrese cant en el rango de 1...9999")
            Return True
        End If
        If ValidaNroMayorOigualCero(txtPre.Text) Then
            txtPre.errorProv()
            Return True
        End If
        'Todo OK RAS
        Return False
    End Function

    Private Function recuperarAlmacenado(ByVal codMat As Integer, ByVal codUbi As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(idMU),0) from TMatUbi where codMat=" & codMat & " and codUbi=" & codUbi
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSaldo(ByVal codArt As Integer, ByVal codUbi As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(sum(stock),0) from TMatUbi where codMat=" & codArt & " and codUbi=" & codUbi
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPro.Click
        If BindingSource1.Position = -1 Then
            MessageBox.Show("Seleccione insumo...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ValidarCampos() Then
            Exit Sub
        End If

        If cbPro.SelectedValue = 2 Then 'SALIDA
            If CDbl(txtCan.Text.Trim()) > CDbl(txtSto.Text) Then
                MessageBox.Show("Proceso denegado, NO existe STOCK...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de [" & cbPro.Text.Trim() & "] de insumos a =>" & cbUbi.Text & Chr(13) & " Sede/Obra =>" & cbObra.Text, nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        If cbPro.SelectedValue = 2 Then 'SALIDA
            If CDbl(txtCan.Text.Trim()) > recuperarStock(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue) Then
                MessageBox.Show("Proceso denegado, NO existe STOCK...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Dim indice As Integer = BindingSource1.Item(BindingSource1.Position)(0)
                btnExtraer.PerformClick() 'F5  cargar de nuevo insumos
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                BindingSource1.Position = BindingSource1.Find("codMat", indice)
                Exit Sub
            End If
        End If

        Dim existe As Single = recuperarAlmacenado(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue)
        If existe = 0 Then
            'MsgBox("NO EXISTE STOCK")
        Else
            'MsgBox("SI EXISTE STOCK")
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()

            If cbPro.SelectedValue = 1 Then 'ENTRADA  solo hay se actualiza precio
                'TMaterial
                comandoUpdate1(CDbl(txtPre.Text), BindingSource1.Item(BindingSource1.Position)(0))
                cmUpdateTable1.Transaction = myTrans
                If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            End If

            Dim idMU As Integer
            If cbPro.SelectedValue = 1 Then 'ENTRADA
                If existe = 0 Then 'No existe stock crear...
                    'TMatUbi
                    comandoInsert1(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue, CDbl(txtCan.Text))
                    cmdInserTable1.Transaction = myTrans
                    If cmdInserTable1.ExecuteNonQuery() < 1 Then
                        wait.Close()
                        Me.Cursor = Cursors.Default
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                    idMU = cmdInserTable1.Parameters("@Identity").Value
                Else 'existe = 1 si hay stock Aumentar
                    'TMatUbi
                    comandoUpdate2(existe)
                    cmUpdateTable2.Transaction = myTrans
                    If cmUpdateTable2.ExecuteNonQuery() < 1 Then
                        'deshace la transaccion
                        wait.Close()
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                    idMU = existe
                End If
            Else  'SALIDA
                'TMatUbi
                comandoUpdate3(existe)
                cmUpdateTable3.Transaction = myTrans
                If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
                idMU = existe
            End If

            Dim saldo As Decimal = recuperarSaldo(BindingSource1.Item(BindingSource1.Position)(0), cbUbi.SelectedValue, myTrans) 'saldo de almacen
            'TSaldo
            comandoInsert11(saldo, vSCodigo)
            cmInserTable11.Transaction = myTrans
            If cmInserTable11.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim codSal As Integer = cmInserTable11.Parameters("@Identity").Value

            Dim cantEnt As Decimal
            Dim preEnt As Decimal
            Dim cantSal As Decimal
            Dim preSal As Decimal
            If cbPro.SelectedValue = 1 Then 'ENTRADA
                cantEnt = txtCan.Text
                preEnt = txtPre.Text
                cantSal = 0
                preSal = 0
            Else  '2=Salida
                cantSal = 0
                preEnt = 0
                cantSal = txtCan.Text
                preSal = txtPre.Text
            End If

            'TEntradaSalida
            comandoInsert2(date1.Value.Date, BindingSource1.Item(BindingSource1.Position)(0), idMU, cbUbi.SelectedValue, cantEnt, preEnt, cantSal, preSal, 0, txtGuia.Text.Trim(), 0, txtFac.Text.Trim(), txtOtro.Text.Trim(), cbPro.SelectedValue, vPass, 0, txtObs.Text.Trim(), codSal)
            cmdInserTable2.Transaction = myTrans
            cmdInserTable2.ExecuteNonQuery()
            Dim nroNota As Integer = cmdInserTable2.Parameters("@Identity").Value


            Dim puntero As Integer = BindingSource1.Item(BindingSource1.Position)(0)

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            btnExtraer.PerformClick() 'F5  cargar de nuevo insumos

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("codMat", puntero)
            visualizarKardex()

            limpiarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro de entradas y/o salidas fué procesado con exito...")
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
    Private Sub comandoUpdate1(ByVal precioC As Decimal, ByVal codArt As Integer)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TMaterial set preBase=@pre1 where codMat=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@pre1", SqlDbType.Decimal, 0).Value = precioC
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codArt
    End Sub

    Dim cmdInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal codArt As Integer, ByVal codUbi As Integer, ByVal can As Decimal)
        cmdInserTable1 = New SqlCommand
        cmdInserTable1.CommandType = CommandType.StoredProcedure
        cmdInserTable1.CommandText = "PA_InsertTMatUbi"
        cmdInserTable1.Connection = Cn
        cmdInserTable1.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = codArt
        cmdInserTable1.Parameters.Add("@codUbi", SqlDbType.Int, 0).Value = codUbi
        cmdInserTable1.Parameters.Add("@stock", SqlDbType.Decimal, 0).Value = can
        'configurando direction output = parametro de solo salida
        cmdInserTable1.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmdInserTable1.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable2 As SqlCommand
    Private Sub comandoUpdate2(ByVal idMU As Integer)
        cmUpdateTable2 = New SqlCommand
        cmUpdateTable2.CommandType = CommandType.Text
        cmUpdateTable2.CommandText = "update TMatUbi set stock=stock+@can where idMU=@cod"
        cmUpdateTable2.Connection = Cn
        cmUpdateTable2.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = CDbl(txtCan.Text)
        cmUpdateTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idMU
    End Sub

    Dim cmUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal idMU As Integer)
        cmUpdateTable3 = New SqlCommand
        cmUpdateTable3.CommandType = CommandType.Text
        cmUpdateTable3.CommandText = "update TMatUbi set stock=stock-@can where idMU=@cod"
        cmUpdateTable3.Connection = Cn
        cmUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = CDbl(txtCan.Text)
        cmUpdateTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idMU
    End Sub

    Dim cmInserTable11 As SqlCommand
    Private Sub comandoInsert11(ByVal saldo As Decimal, ByVal codSuc As String)
        cmInserTable11 = New SqlCommand
        cmInserTable11.CommandType = CommandType.StoredProcedure
        cmInserTable11.CommandText = "PA_InsertTSaldo"
        cmInserTable11.Connection = Cn
        cmInserTable11.Parameters.Add("@sal", SqlDbType.Decimal, 0).Value = saldo
        cmInserTable11.Parameters.Add("@codS", SqlDbType.VarChar, 10).Value = codSuc
        'configurando direction output = parametro de solo salida
        cmInserTable11.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable11.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmdInserTable2 As SqlCommand
    Private Sub comandoInsert2(ByVal fecha As Date, ByVal codMat As Integer, ByVal idMU As Integer, ByVal codUbi As Integer, ByVal canEnt As Decimal, ByVal valor1 As Decimal, ByVal canSal As Decimal, ByVal valor2 As Decimal, ByVal codGuia As Integer, ByVal nroGuia As String, ByVal codDoc As Integer, ByVal nroDoc As String, ByVal otroDoc As String, ByVal codTrans As Integer, ByVal codUsu As Integer, ByVal codPers As Integer, ByVal obs As String, ByVal codSal As Integer)
        cmdInserTable2 = New SqlCommand
        cmdInserTable2.CommandType = CommandType.StoredProcedure
        cmdInserTable2.CommandText = "PA_InsertTEntradaSalida"
        cmdInserTable2.Connection = Cn
        cmdInserTable2.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha
        cmdInserTable2.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = codMat
        cmdInserTable2.Parameters.Add("@idMU", SqlDbType.Int, 0).Value = idMU
        cmdInserTable2.Parameters.Add("@codUbi", SqlDbType.Int, 0).Value = codUbi
        cmdInserTable2.Parameters.Add("@can1", SqlDbType.Decimal, 0).Value = canEnt
        cmdInserTable2.Parameters.Add("@valor1", SqlDbType.Decimal, 0).Value = valor1
        cmdInserTable2.Parameters.Add("@can2", SqlDbType.Decimal, 0).Value = canSal
        cmdInserTable2.Parameters.Add("@valor2", SqlDbType.Decimal, 0).Value = valor2
        cmdInserTable2.Parameters.Add("@codGuia", SqlDbType.Int, 0).Value = codGuia
        cmdInserTable2.Parameters.Add("@nroGuia", SqlDbType.VarChar, 30).Value = nroGuia
        cmdInserTable2.Parameters.Add("@codDoc", SqlDbType.Int, 0).Value = codDoc
        cmdInserTable2.Parameters.Add("@nroDoc", SqlDbType.VarChar, 30).Value = nroDoc
        cmdInserTable2.Parameters.Add("@otroDoc", SqlDbType.VarChar, 30).Value = otroDoc
        cmdInserTable2.Parameters.Add("@codTrans", SqlDbType.Int, 0).Value = codTrans
        cmdInserTable2.Parameters.Add("@codUsu", SqlDbType.Int, 0).Value = codUsu
        cmdInserTable2.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = codPers
        cmdInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmdInserTable2.Parameters.Add("@codSal", SqlDbType.Int, 0).Value = codSal
        'configurando direction output = parametro de solo salida
        cmdInserTable2.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmdInserTable2.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Private Sub visualizarKardex()
        If BindingSource1.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a Generar KARDEX...")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VKardex").Clear()
        daVKardex.SelectCommand.Parameters("@codMat").Value = BindingSource1.Item(BindingSource1.Position)(0)
        daVKardex.SelectCommand.Parameters("@codUbi").Value = cbUbi.SelectedValue
        daVKardex.Fill(dsAlmacen, "VKardex")
        colorear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVis.Click
        visualizarKardex()
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource4.Count - 1
            If BindingSource4.Item(j)(23) = 1 Then 'ENTRADA
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource4.Item(j)(23) = 2 Then 'SALIDA
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            dgTabla2.Rows(j).Cells(4).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(6).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(8).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub

    Private Sub limpiarText()
        txtCan.Text = "0"
        txtGuia.Clear()
        txtFac.Clear()
        txtOtro.Clear()
        txtObs.Clear()
    End Sub

    Private Sub txtCan_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCan.GotFocus, txtCan.MouseClick
        txtCan.SelectAll()
    End Sub

    Private Sub txtCan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan.KeyPress
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

    Private Sub txtPre_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPre.KeyPress
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

    Private Function recuperarNroNota(ByVal codUbi As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroNota) from TEntradaSalida where codUbi=" & codUbi
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarNroNota1(ByVal codUbi As Integer, ByVal codMat As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select MAX(nroNota) from TEntradaSalida where codUbi=" & codUbi & " and codMat=" & codMat
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodSal(ByVal nroNota As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select codSal from TEntradaSalida where nroNota=" & nroNota
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCodTrans(ByVal nroNota As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select codTrans from TEntradaSalida where nroNota=" & nroNota
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarIdMU(ByVal nroNota As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select idMU from TEntradaSalida where nroNota=" & nroNota
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarCant(ByVal nroNota As Integer) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select cantEnt+cantSal from TEntradaSalida where nroNota=" & nroNota
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarVan(ByVal idMU As Integer, ByVal myTrans As SqlTransaction) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select COUNT(*) from TEntradaSalida where idMU=" & idMU
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnDes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDes.Click
        If dgTabla2.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO Existe registro de KARDEX a deshacer...")
            Exit Sub
        End If

        Dim nroNota As Integer = recuperarNroNota1(BindingSource4.Item(BindingSource4.Position)(19), BindingSource4.Item(BindingSource4.Position)(17))  'codUbi  codMat
        If BindingSource4.Item(BindingSource4.Position)(0) <> nroNota Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource4.Item(BindingSource4.Position)(24) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro ENTRADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de DESHACER linea de stock?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        'nroNota = recuperarNroNota(BindingSource4.Item(BindingSource4.Position)(19))  'codUbi
        nroNota = recuperarNroNota1(BindingSource4.Item(BindingSource4.Position)(19), BindingSource4.Item(BindingSource4.Position)(17))  'codUbi  codMat
        If BindingSource4.Item(BindingSource4.Position)(0) <> nroNota Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim codSal As Integer = recuperarCodSal(nroNota)
        Dim codTrans As Integer = recuperarCodTrans(nroNota)
        Dim idMU As Integer = recuperarIdMU(nroNota)
        Dim cant As Decimal = recuperarCant(nroNota)

        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TEntradaSalida
            comandoDelete2(nroNota)
            cmDeleteTable2.Transaction = myTrans
            If cmDeleteTable2.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar registro por qué ocurrio un error...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'Tabla TSaldo
            comandoDelete3(codSal)
            cmDeleteTable3.Transaction = myTrans
            If cmDeleteTable3.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar registro por qué ocurrio un error...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim vFBorrar As Integer = recuperarVan(idMU, myTrans)

            If codTrans = 1 Then  'ENTRADA
                If vFBorrar = 0 Then
                    'MsgBox("Eliminando TMATUBI")
                    'Tabla TMatUbi
                    comandoDelete1(idMU)
                    cmDeleteTable1.Transaction = myTrans
                    If cmDeleteTable1.ExecuteNonQuery() < 1 Then
                        wait.Close()
                        myTrans.Rollback()
                        MessageBox.Show("No se puede eliminar Representante por qué esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                Else
                    'MsgBox("modificando DISMINUIR TMATUBI")
                    'TMatUbi
                    comandoUpdate5(cant, idMU)
                    cmUpdateTable5.Transaction = myTrans
                    If cmUpdateTable5.ExecuteNonQuery() < 1 Then
                        'deshace la transaccion
                        wait.Close()
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                    End If
                End If

            Else '2=SALIDA
                'MsgBox("modificando AUMENTAR TMATUBI")
                'TMatUbi
                comandoUpdate4(cant, idMU)
                cmUpdateTable4.Transaction = myTrans
                If cmUpdateTable4.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If
            End If

            Dim puntero As Integer = BindingSource1.Item(BindingSource1.Position)(0)
            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO DE KARDEX FUE DESHECHO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            btnExtraer.PerformClick() 'F5  cargar de nuevo insumos

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("codMat", puntero)
            visualizarKardex()

            BindingSource4.MoveLast()  'MOVER AL ULTIMO REGISTRO
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
    Private Sub comandoDelete2(ByVal nroNota As Integer)
        cmDeleteTable2 = New SqlCommand
        cmDeleteTable2.CommandType = CommandType.Text
        cmDeleteTable2.CommandText = "delete from TEntradaSalida where nroNota=@cod"
        cmDeleteTable2.Connection = Cn
        cmDeleteTable2.Parameters.Add("@cod", SqlDbType.Int, 0).Value = nroNota
    End Sub

    Dim cmDeleteTable3 As SqlCommand
    Private Sub comandoDelete3(ByVal codSal As Integer)
        cmDeleteTable3 = New SqlCommand
        cmDeleteTable3.CommandType = CommandType.Text
        cmDeleteTable3.CommandText = "delete from TSaldo where codSal=@cod"
        cmDeleteTable3.Connection = Cn
        cmDeleteTable3.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codSal
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1(ByVal idMU As Integer)
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TMatUbi where idMU=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = idMU
    End Sub

    Dim cmUpdateTable5 As SqlCommand
    Private Sub comandoUpdate5(ByVal cant As Decimal, ByVal idMU As Integer)
        cmUpdateTable5 = New SqlCommand
        cmUpdateTable5.CommandType = CommandType.Text
        cmUpdateTable5.CommandText = "update TMatUbi set stock=stock-@can where idMU=@nro"
        cmUpdateTable5.Connection = Cn
        cmUpdateTable5.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmUpdateTable5.Parameters.Add("@nro", SqlDbType.Int, 0).Value = idMU
    End Sub

    Dim cmUpdateTable4 As SqlCommand
    Private Sub comandoUpdate4(ByVal cant As Decimal, ByVal idMU As Integer)
        cmUpdateTable4 = New SqlCommand
        cmUpdateTable4.CommandType = CommandType.Text
        cmUpdateTable4.CommandText = "update TMatUbi set stock=stock+@can where idMU=@nro"
        cmUpdateTable4.Connection = Cn
        cmUpdateTable4.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
        cmUpdateTable4.Parameters.Add("@nro", SqlDbType.Int, 0).Value = idMU
    End Sub

    Private Sub btnImp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImp.Click
        If BindingSource4.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Proceso Denegado, No existe Kardex Procesado...")
            Exit Sub
        End If

        vCodProd = BindingSource4.Item(BindingSource4.Position)(17)
        vCodUbi = BindingSource4.Item(BindingSource4.Position)(19)
        'vParam1 = "Nº " & BindingSource6.Item(BindingSource6.Position)(2) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year

        Dim informe As New ReportViewerKardex1Form
        informe.ShowDialog()
    End Sub

    Private Sub txtCan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCan.TextChanged
        Me.AcceptButton = Me.btnPro
    End Sub

    Private Sub dgTabla1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTabla1.CellDoubleClick
        btnPro.PerformClick()
    End Sub

    Private Sub btnCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Dim crea As New crearMaterialForm
        crea.ShowDialog()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        If BindingSource1.Position >= 0 Then
            BindingSource11.Filter = "codMat=" & BindingSource1.Item(BindingSource1.Position)(0)
        Else
            BindingSource11.Filter = "codMat=" & "0"
        End If

        dsAlmacen.Tables("VKardex").Clear()
        seleStock()
    End Sub

    Private Sub cbUbi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUbi.SelectedIndexChanged

    End Sub
End Class
