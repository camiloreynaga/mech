Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class salidaAlmacenGuiaMechForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource4 As New BindingSource
    Dim BindingSource11 As New BindingSource

    Private Sub salidaAlmacenGuiaMechForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub salidaAlmacenGuiaMechForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta tipoM si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codGuiaE,fecIni,nro,razon,ruc,partida,llegada,motivo,nroFact,nomPers,empTra,rucTra,marcaNro,nroConst,nroLic,nomTra,DNI,obs,hist,talon,nroGuia,codVeh,codT,codMotG,codPers,codObraOri,codObraDes,codET,codSerS,codIde,codUbiOri,codUbiDes from VGuiaRemEmpEnt where codObraOri=@cod"
        crearDataAdapterTable(daTabla1, sele)
        daTabla1.SelectCommand.Parameters.Add("@cod", SqlDbType.VarChar, 10).Value = vSCodigo

        sele = "select codDGE,codigo,cant,unidad,detalle,peso,entre,entregado,codGuiaE,codMat,recib,nomRec,obsR,recibido,codPers from VDetGuiaE where codGuiaE=@nro"
        crearDataAdapterTable(daDetDoc, sele)
        daDetDoc.SelectCommand.Parameters.Add("@nro", SqlDbType.Int, 0).Value = 0

        sele = "select nroNota,tipo,fecha,material,cantEnt,preUniEnt,cantSal,preUniSal,saldo,unidad,nroGuia,nroDoc,veri,almObra,nomObraDes,obs,nomRecibe,provee,ruc,usuario,codMat,idMU,codUbi,codigo,codGuia,codDoc,codTrans,codPers,codSal,vanET,codUbiDes,ubicacion,nombre,codUsu from VKardex1 where codMat=@codMat and codUbi=@codUbi" 'order by nroNota"
        crearDataAdapterTable(daVKardex, sele)
        daVKardex.SelectCommand.Parameters.Add("@codMat", SqlDbType.Int, 0).Value = 0
        daVKardex.SelectCommand.Parameters.Add("@codUbi", SqlDbType.Int, 0).Value = 0

        sele = "select idMU,stock,obra+' - '+ubicacion as ubi,codigo,codUbi,codMat,color from VStockUbi"
        crearDataAdapterTable(daVStock, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()

            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "VGuiaRemEmpEnt")
            daDetDoc.Fill(dsAlmacen, "VDetGuiaE")
            daVKardex.Fill(dsAlmacen, "VKardex1")
            daVStock.Fill(dsAlmacen, "VStockUbi")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VGuiaRemEmpEnt"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            BindingSource1.Sort = "codGuiaE"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VDetGuiaE"
            Navigator3.BindingSource = BindingSource2
            dgTabla3.DataSource = BindingSource2
            BindingSource2.Sort = "codDGE"

            BindingSource11.DataSource = dsAlmacen
            BindingSource11.DataMember = "VStockUbi"
            dgStock.DataSource = BindingSource11

            BindingSource4.DataSource = dsAlmacen
            BindingSource4.DataMember = "VKardex1"
            Navigator2.BindingSource = BindingSource4
            dgTabla2.DataSource = BindingSource4
            'dgTabla2.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource4.Sort = "nroNota"
            ModificarColumnasDGV()

            configurarColorControl()

            'Enlace Guia
            txtRazon.DataBindings.Add("Text", BindingSource1, "empTra")
            txtRuc.DataBindings.Add("Text", BindingSource1, "rucTra")
            txtMar.DataBindings.Add("Text", BindingSource1, "marcaNro")
            txtNom.DataBindings.Add("Text", BindingSource1, "nomTra")
            txtLic.DataBindings.Add("Text", BindingSource1, "nroLic")
            txtMot.DataBindings.Add("Text", BindingSource1, "motivo")
            txtFac.DataBindings.Add("Text", BindingSource1, "nroFact")
            txtObs.DataBindings.Add("Text", BindingSource1, "obs")

            'Enlace Kardex
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

    Private Sub salidaAlmacenGuiaMechForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'BindingSource1.MoveLast()
        vfVan3 = True
        visualizarDet()
    End Sub

    Private Sub dgTabla3_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla3.CurrentCellChanged
        If BindingSource2.Position = -1 Then 'Sele Stock
            Exit Sub
        End If
        BindingSource11.Filter = "codMat=" & BindingSource2.Item(BindingSource2.Position)(9) 'Sele Stock
        dsAlmacen.Tables("VKardex1").Clear()
    End Sub

    Private Sub dgTabla1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgTabla1.CurrentCellChanged
        visualizarDet()
    End Sub

    Dim vfVan3 As Boolean = False
    Private Sub visualizarDet()
        If vfVan3 Then
            If BindingSource1.Position = -1 Then
                dsAlmacen.Tables("VDetGuiaE").Clear()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("VDetGuiaE").Clear()
            daDetDoc.SelectCommand.Parameters("@nro").Value = BindingSource1.Item(BindingSource1.Position)(0)
            daDetDoc.Fill(dsAlmacen, "VDetGuiaE")
            colorear1()
            'BindingSource11.Filter = "codMat=" & BindingSource2.Item(BindingSource2.Position)(9) 'Sele Stock
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub colorear1()
        For j As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(j)(7) = 1 Then 'Recibido
                dgTabla3.Rows(j).Cells(6).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla3.Rows(j).Cells(6).Style.ForeColor = Color.White
            End If
            If BindingSource2.Item(j)(13) = 1 Then 'Recibido
                dgTabla3.Rows(j).Cells(10).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla3.Rows(j).Cells(10).Style.ForeColor = Color.White
            End If
            If BindingSource2.Item(j)(13) = 2 Then 'Incompleto
                dgTabla3.Rows(j).Cells(10).Style.BackColor = Color.Red
                dgTabla3.Rows(j).Cells(10).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Fecha"
            .Columns(1).Width = 70
            .Columns(2).HeaderText = "NºGuia"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = "Destinatario"
            .Columns(3).Width = 180
            .Columns(4).HeaderText = "RUC"
            .Columns(4).Width = 75
            .Columns(5).HeaderText = "Punto de Partida"
            .Columns(5).Width = 300
            .Columns(6).HeaderText = "Punto de Llegada"
            .Columns(6).Width = 500
            .Columns(7).Width = 80
            .Columns(7).HeaderText = "Motivo"
            .Columns(8).HeaderText = "NºFact"
            .Columns(8).Width = 80
            .Columns(9).HeaderText = "Usuario"
            .Columns(9).Width = 100
            .Columns(10).Width = 200
            .Columns(10).HeaderText = "Empresa Transporte"
            .Columns(11).HeaderText = "RUC"
            .Columns(11).Width = 75
            .Columns(12).Width = 100
            .Columns(12).HeaderText = "Marca_Placa"
            .Columns(13).HeaderText = "NºConstancia"
            .Columns(13).Width = 100
            .Columns(14).HeaderText = "Licencia_Cond."
            .Columns(14).Width = 100
            .Columns(15).Width = 120
            .Columns(15).HeaderText = "Nom_Conductor"
            .Columns(16).HeaderText = "DNI"
            .Columns(16).Width = 70
            .Columns(17).Width = 400
            .Columns(17).HeaderText = "Observacion"
            .Columns(18).Width = 400
            .Columns(18).HeaderText = ""
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
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        With dgTabla3
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Cant."
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 50
            .Columns(3).HeaderText = "Unid."
            .Columns(4).HeaderText = "Descripción Insumo"
            .Columns(4).Width = 440
            .Columns(5).Width = 50
            .Columns(5).HeaderText = "Peso"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 60
            .Columns(6).HeaderText = "Entreg."
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Width = 60
            .Columns(10).HeaderText = "Recibido"
            .Columns(11).Width = 120
            .Columns(11).HeaderText = "Pers. Recibe"
            .Columns(12).Width = 300
            .Columns(12).HeaderText = "Nota Recibe"
            .Columns(13).Visible = False
            .Columns(14).Visible = False
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
            .Columns(11).Width = 60
            .Columns(11).HeaderText = "NºFact"
            .Columns(12).Width = 60
            .Columns(12).HeaderText = "Salida"
            .Columns(12).Visible = False
            .Columns(13).HeaderText = "Recepción"
            .Columns(13).Width = 110
            .Columns(14).HeaderText = "Recepción Obra/Sede"
            .Columns(14).Width = 300
            .Columns(15).Width = 400
            .Columns(15).HeaderText = "Nota"
            .Columns(16).Width = 130
            .Columns(16).HeaderText = "Personal_Recibe"
            .Columns(17).Width = 240
            .Columns(17).HeaderText = "Proveedor"
            .Columns(18).Width = 75
            .Columns(18).HeaderText = "Ruc"
            .Columns(19).Width = 130
            .Columns(19).HeaderText = "Usuario"
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
        GroupBox3.ForeColor = ForeColorLabel
        Label6.ForeColor = ForeColorLabel
        btnDes.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        dgTabla2.Dispose()
        dgTabla3.Dispose()
        Me.Close()
    End Sub

    Private Function recuperarNroNota1(ByVal codMat As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        'cmdCampo.CommandText = "select MAX(nroNota) from TEntradaSalida where codUbi=" & codUbi & " and codMat=" & codMat
        cmdCampo.CommandText = "select MAX(nroNota) from TEntradaSalida where codMat=" & codMat
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

    Private Sub btnDes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDes.Click
        If dgTabla2.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  NO Existe registro de KARDEX a deshacer...")
            Exit Sub
        End If

        Dim nroNota As Integer = recuperarNroNota1(BindingSource4.Item(BindingSource4.Position)(20))  'codUbi  codMat

        If BindingSource4.Item(BindingSource4.Position)(0) <> nroNota Then
            MessageBox.Show("Seleccione Ultimo Movimiento. Solo Ultimo Movimiento se puede deshacer...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        If BindingSource4.Item(BindingSource4.Position)(33) <> vPass Then
            MessageBox.Show("Proceso denegado, usuario no es el mismo que registro SALIDA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        'si es salida Mech=2 Y Nro Guia = Nro Guia entonces deshacer
        If BindingSource4.Item(BindingSource4.Position)(26) = 2 And BindingSource4.Item(BindingSource4.Position)(24) = BindingSource2.Item(BindingSource2.Position)(0) Then  'Salida Mech >0 con guia
        Else
            MessageBox.Show("Proceso denegado, Movimiento NO es proceso de SALIDA de MECH con la misma Guia...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de DESHACER linea de stock?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        nroNota = recuperarNroNota1(BindingSource4.Item(BindingSource4.Position)(20))  'codUbi  codMat
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

            '2=SALIDA
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

            'TDetalleGuiaEmp
            comandoUpdate1(0, BindingSource2.Item(BindingSource2.Position)(0)) '0=pendiente
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim puntero As Integer = BindingSource2.Item(BindingSource2.Position)(0)
            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS
            StatusBarClass.messageBarraEstado("  REGISTRO DE KARDEX FUE DESHECHO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VStockUbi").Clear()
            daVStock.Fill(dsAlmacen, "VStockUbi")
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource2.Position = BindingSource2.Find("codDGE", puntero)

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

        vCodProd = BindingSource4.Item(BindingSource4.Position)(20)
        vCodUbi = BindingSource4.Item(BindingSource4.Position)(22)
        'vParam1 = "Nº " & BindingSource6.Item(BindingSource6.Position)(2) & "-MECH-" & CDate(BindingSource6.Item(BindingSource6.Position)(4)).Year

        Dim informe As New ReportViewerKardex1Form
        informe.ShowDialog()
    End Sub

    Private Sub btnVisKardex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisKardex.Click
        visualizarKardex()
    End Sub

    Private Sub visualizarKardex()
        If BindingSource2.Position = -1 Then
            StatusBarClass.messageBarraEstado("  Seleccione Insumo a Generar KARDEX...")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        dsAlmacen.Tables("VKardex1").Clear()
        daVKardex.SelectCommand.Parameters("@codMat").Value = BindingSource2.Item(BindingSource2.Position)(9)
        daVKardex.SelectCommand.Parameters("@codUbi").Value = BindingSource1.Item(BindingSource1.Position)(30)
        daVKardex.Fill(dsAlmacen, "VKardex1")
        colorear()
        BindingSource4.MoveLast()  'MOVER AL ULTIMO REGISTRO
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub colorear()
        For j As Short = 0 To BindingSource4.Count - 1
            If BindingSource4.Item(j)(26) = 1 Then 'ENTRADA
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Green
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            If BindingSource4.Item(j)(26) = 2 Then 'SALIDA
                dgTabla2.Rows(j).Cells(1).Style.BackColor = Color.Red
                dgTabla2.Rows(j).Cells(1).Style.ForeColor = Color.White
            End If
            dgTabla2.Rows(j).Cells(4).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(6).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dgTabla2.Rows(j).Cells(8).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Next
    End Sub
    '--------------------------------------------------------------------------------------------------------
    '--------------------------------------------------------------------------------------------------------
    Private Function recuperarStock(ByVal codMat As Integer, ByVal codUbi As Integer) As Double
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isNull(max(stock),-1) as stock from TMatUbi where codMat=" & codMat & " and codUbi=" & codUbi
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarAlmacenado(ByVal codMat As Integer, ByVal codUbi As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select isnull(max(idMU),0) from TMatUbi where codMat=" & codMat & " and codUbi=" & codUbi
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarSaldo(ByVal idMU As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select stock from TMatUbi where idMU=" & idMU
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Function recuperarPreBase(ByVal codMat As Integer, ByVal myTrans As SqlTransaction) As Decimal
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select preBase from TMaterial where codMat=" & codMat
        cmdCampo.Connection = Cn
        cmdCampo.Transaction = myTrans
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPro.Click
        If BindingSource2.Position = -1 Then
            MessageBox.Show("Seleccione insumo...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If BindingSource2.Item(BindingSource2.Position)(7) = 1 Then
            MessageBox.Show("Proceso denegado, ya fue ENTREGADO y procesado en KARDEX...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CDbl(BindingSource2.Item(BindingSource2.Position)(2)) > CDbl(recuperarStock(BindingSource2.Item(BindingSource2.Position)(9), BindingSource1.Item(BindingSource1.Position)(30))) Then
            MessageBox.Show("Proceso denegado, NO existe STOCK...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim resp As Short = MessageBox.Show("Esta segúro de procesar SALIDA de  " & BindingSource2.Item(BindingSource2.Position)(2) & " " & BindingSource2.Item(BindingSource2.Position)(3) & Chr(13) & BindingSource2.Item(BindingSource2.Position)(4) & Chr(13) & "De =>" & BindingSource1.Item(BindingSource1.Position)(5) & Chr(13), nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        If CDbl(BindingSource2.Item(BindingSource2.Position)(2)) > recuperarStock(BindingSource2.Item(BindingSource2.Position)(9), BindingSource1.Item(BindingSource1.Position)(30)) Then
            MessageBox.Show("Proceso denegado, NO existe STOCK...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Dim indice As Integer = BindingSource1.Item(BindingSource1.Position)(0)
            BindingSource11.Filter = "codMat=" & BindingSource2.Item(BindingSource2.Position)(9) 'Sele Stock
            Exit Sub
        End If

        Dim existe As Single = recuperarAlmacenado(BindingSource2.Item(BindingSource2.Position)(9), BindingSource1.Item(BindingSource1.Position)(30))
        If existe = 0 Then
            MsgBox("NO EXISTE STOCK")
            Exit Sub
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

            Dim idMU As Integer = existe
            'TMatUbi
            comandoUpdate3(BindingSource2.Item(BindingSource2.Position)(2), idMU)
            cmUpdateTable3.Transaction = myTrans
            If cmUpdateTable3.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim saldo As Decimal = recuperarSaldo(idMU, myTrans) 'saldo de almacen
            'TSaldo
            comandoInsert11(saldo, BindingSource1.Item(BindingSource1.Position)(25))
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

            Dim cantEnt As Decimal = 0
            Dim precio As Decimal = recuperarPreBase(BindingSource2.Item(BindingSource2.Position)(9), myTrans)
            Dim cantSal As Decimal = BindingSource2.Item(BindingSource2.Position)(2)

            'TEntradaSalida
            comandoInsert2(Now.Date, BindingSource2.Item(BindingSource2.Position)(9), idMU, BindingSource1.Item(BindingSource1.Position)(30), cantEnt, 0, cantSal, precio, BindingSource2.Item(BindingSource2.Position)(0), "R " & BindingSource1.Item(BindingSource1.Position)(2), 0, BindingSource1.Item(BindingSource1.Position)(8), "", 2, vPass, 0, "", codSal, BindingSource1.Item(BindingSource1.Position)(31), 0, BindingSource1.Item(BindingSource1.Position)(29)) '2=Salida 0=pendiente
            cmdInserTable2.Transaction = myTrans
            cmdInserTable2.ExecuteNonQuery()
            Dim nroNota As Integer = cmdInserTable2.Parameters("@Identity").Value

            'TDetalleGuiaEmp
            comandoUpdate1(1, BindingSource2.Item(BindingSource2.Position)(0)) '1=entregado
            cmUpdateTable1.Transaction = myTrans
            If cmUpdateTable1.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim puntero As Integer = BindingSource2.Item(BindingSource2.Position)(0)

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("VStockUbi").Clear()
            daVStock.Fill(dsAlmacen, "VStockUbi")
            visualizarDet()

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource2.Position = BindingSource2.Find("codDGE", puntero)

            visualizarKardex()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro de SALIDAS fué procesado con exito...")
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

    Dim cmUpdateTable3 As SqlCommand
    Private Sub comandoUpdate3(ByVal cant As Decimal, ByVal idMU As Integer)
        cmUpdateTable3 = New SqlCommand
        cmUpdateTable3.CommandType = CommandType.Text
        cmUpdateTable3.CommandText = "update TMatUbi set stock=stock-@can where idMU=@cod"
        cmUpdateTable3.Connection = Cn
        cmUpdateTable3.Parameters.Add("@can", SqlDbType.Decimal, 0).Value = cant
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
    Private Sub comandoInsert2(ByVal fecha As Date, ByVal codMat As Integer, ByVal idMU As Integer, ByVal codUbi As Integer, ByVal canEnt As Decimal, ByVal valor1 As Decimal, ByVal canSal As Decimal, ByVal valor2 As Decimal, ByVal codGuia As Integer, ByVal nroGuia As String, ByVal codDoc As Integer, ByVal nroDoc As String, ByVal otroDoc As String, ByVal codTrans As Integer, ByVal codUsu As Integer, ByVal codPers As Integer, ByVal obs As String, ByVal codSal As Integer, ByVal codUbiD As Integer, ByVal vanET As Integer, ByVal codProv As Integer)
        cmdInserTable2 = New SqlCommand
        cmdInserTable2.CommandType = CommandType.StoredProcedure
        cmdInserTable2.CommandText = "PA_InsertTEntradaSalida1"
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
        cmdInserTable2.Parameters.Add("@otroDoc", SqlDbType.VarChar, 100).Value = otroDoc
        cmdInserTable2.Parameters.Add("@codTrans", SqlDbType.Int, 0).Value = codTrans
        cmdInserTable2.Parameters.Add("@codUsu", SqlDbType.Int, 0).Value = codUsu
        cmdInserTable2.Parameters.Add("@codPers", SqlDbType.Int, 0).Value = codPers
        cmdInserTable2.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs
        cmdInserTable2.Parameters.Add("@codSal", SqlDbType.Int, 0).Value = codSal
        cmdInserTable2.Parameters.Add("@codUbiD", SqlDbType.Int, 0).Value = codUbiD
        cmdInserTable2.Parameters.Add("@vanET", SqlDbType.Int, 0).Value = vanET
        cmdInserTable2.Parameters.Add("@codProv", SqlDbType.Int, 0).Value = codProv
        'configurando direction output = parametro de solo salida
        cmdInserTable2.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmdInserTable2.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmUpdateTable1 As SqlCommand
    Private Sub comandoUpdate1(ByVal ent As Integer, ByVal cod As Integer)
        cmUpdateTable1 = New SqlCommand
        cmUpdateTable1.CommandType = CommandType.Text
        cmUpdateTable1.CommandText = "update TDetalleGuiaEmp set entregado=@ent where codDGE=@cod"
        cmUpdateTable1.Connection = Cn
        cmUpdateTable1.Parameters.Add("@ent", SqlDbType.Int, 0).Value = ent
        cmUpdateTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = cod
    End Sub

    Private Sub btnTermi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTermi.Click
        If BindingSource2.Position = -1 Then
            MessageBox.Show("NO existe insumos entregados...", nomNegocio, Nothing, MessageBoxIcon.Information)
            Exit Sub
        End If

        For i As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(i)(7) = 0 Then '0=No entregado
                MessageBox.Show("Proceso denegado, Hay Insumos NO ENTREGADOs NI procesados en KARDEX...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        For i As Short = 0 To BindingSource2.Count - 1
            If BindingSource2.Item(i)(13) = 0 Then '0=No entregado
                MessageBox.Show("Proceso denegado, Hay Insumos NO RECIBIDOS NI procesados en KARDEX...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        Dim resp As Short = MessageBox.Show("Esta segúro de TERMINAR Guia de Remisión..", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

            'TGuiaRemEmp
            comandoUpdate33(1, BindingSource1.Item(BindingSource1.Position)(0)) '1=Terminado
            cmUpdateTable33.Transaction = myTrans
            If cmUpdateTable33.ExecuteNonQuery() < 1 Then
                'deshace la transaccion
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON PROCESADOS CON EXITO...")
            finalMytrans = True
            vfVan3 = False

            'Actualizando el dataSet 
            dsAlmacen.Tables("VStockUbi").Clear()
            dsAlmacen.Tables("VGuiaRemEmpEnt").Clear()

            daVStock.Fill(dsAlmacen, "VStockUbi")
            daTabla1.Fill(dsAlmacen, "VGuiaRemEmpEnt")

            vfVan3 = True
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Los Datos fueron procesados con exito...")
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

    Dim cmUpdateTable33 As SqlCommand
    Private Sub comandoUpdate33(ByVal estado As Integer, ByVal codGuia As Integer)
        cmUpdateTable33 = New SqlCommand
        cmUpdateTable33.CommandType = CommandType.Text
        cmUpdateTable33.CommandText = "update TGuiaRemEmp set estado=@est,hist=@hist where codGuiaE=@cod"
        cmUpdateTable33.Connection = Cn
        cmUpdateTable33.Parameters.Add("@est", SqlDbType.Int, 0).Value = estado
        cmUpdateTable33.Parameters.Add("@hist", SqlDbType.VarChar, 500).Value = BindingSource1.Item(BindingSource1.Position)(18) & "  Termino " & Now.Date & " " & vPass & "-" & vSUsuario
        cmUpdateTable33.Parameters.Add("@cod", SqlDbType.Int, 0).Value = codGuia
    End Sub
End Class
