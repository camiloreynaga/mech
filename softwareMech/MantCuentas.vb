Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class MantCuentas

#Region "variables"
    ''' <summary>
    ''' banco
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource1 As New BindingSource
    ''' <summary>
    ''' Cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource2 As New BindingSource
    ''' <summary>
    ''' instancia de clase para config grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim cConfigGrilla As New cConfigFormControls

    ''' <summary>
    ''' comand insert para banco
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertTableBco As SqlCommand

    ''' <summary>
    ''' comand insert para cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertTableCta As SqlCommand

    ''' <summary>
    ''' comand update para banco
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateTableBco As SqlCommand

    ''' <summary>
    ''' comand update para cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateTableCta As SqlCommand

    ''' <summary>
    ''' comand Delete para Banco
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteTableBco As SqlCommand

    ''' <summary>
    ''' comand delete para Cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDeleteTableCta As SqlCommand

#End Region

#Region "metodos"
    ''' <summary>
    ''' activa los controles asociados a Banco
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ActivarControlesBco()
        Panel1.Enabled = True
        'Panel2.Enabled = True
        btnNuevoBco.Enabled = True
        btnNuevoBco.FlatStyle = FlatStyle.Standard
        btnModificarBco.Enabled = True
        btnModificarBco.FlatStyle = FlatStyle.Standard
        btnEliminarBco.Enabled = True
        btnEliminarBco.FlatStyle = FlatStyle.Standard



    End Sub
    ''' <summary>
    ''' desactiva los controles asociados a Banco
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControlesBco()

        Panel2.Enabled = False


    End Sub


    Private Sub AgregarRelacion()
        'Agregando una Relación entre tablas
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("TBanco").Columns("codBan"), dsAlmacen.Tables("TCuentaBancaria").Columns("codBan"))
        dsAlmacen.Relations.Add(relation1)
    End Sub

    Private Sub DatosIniciales()
        Me.Cursor = Cursors.WaitCursor

        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "Select idCue,nroCue,TM.moneda,TC.codMon,codBan,case when estado=0 then 'Inactivo' else 'Activo'end estado, estado as codEstado from TcuentaBan as TC INNER JOIN TMoneda as TM on TC.codMon=TM.codMon where codBan>1"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select codBan,banco from tBanco where codBan>1"

        crearDataAdapterTable(daTabla2, sele)

        sele = "select codMon,moneda from TMoneda"
        crearDataAdapterTable(daTMon, sele)


        Try

            crearDSAlmacen()

            daTabla1.Fill(dsAlmacen, "TCuentaBancaria")
            daTabla2.Fill(dsAlmacen, "TBanco")
            daTMon.Fill(dsAlmacen, "TMoneda")

            AgregarRelacion()

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TBanco"
            dgBancos.DataSource = BindingSource1

            BindingSource2.DataSource = BindingSource1
            BindingSource2.DataMember = "Relacion1"
            dgCuentas.DataSource = BindingSource2

            cboMoneda.DataSource = dsAlmacen
            cboMoneda.DisplayMember = "TMoneda.moneda"
            cboMoneda.ValueMember = "codMon"


        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try


    End Sub


    Private Sub ConfigurarColorControl()


    End Sub
    ''' <summary>
    ''' Modifica la configuración de las grillas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        'dgBancos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        ' dgCuentas.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        dgBancos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgBancos.AllowDrop = False
        dgBancos.AllowUserToAddRows = False
        dgBancos.AllowUserToDeleteRows = False
        dgBancos.ReadOnly = True

        dgCuentas.AllowDrop = False
        dgCuentas.AllowUserToAddRows = False
        dgCuentas.AllowUserToDeleteRows = False
        dgCuentas.ReadOnly = True


        With dgCuentas
            .Columns("nroCue").HeaderText = "Nro Cuenta"
            .Columns("nroCue").Width = 110
            .Columns("moneda").HeaderText = "Moneda"
            .Columns("moneda").Width = 100
            .Columns("estado").HeaderText = "Estado"
            .Columns("idCue").Visible = False
            .Columns("codMon").Visible = False
            .Columns("codBan").Visible = False
            .Columns("codEstado").Visible = False

        End With

        With dgBancos
            .Columns("codBan").HeaderText = "Cod"
            .Columns("banco").HeaderText = "Banco"

        End With

    End Sub

    ''' <summary>
    ''' Enlaza la Grilla de banco Con los controles relacionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextBanco()
        If BindingSource1.Count = 0 Then

        Else
            txtBanco.Text = BindingSource1.Item(BindingSource1.Position)(1)

        End If

    End Sub
    ''' <summary>
    ''' enlaza la grilla de cuenta con los controles relacionados 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextCuenta()
        If BindingSource2.Count = 0 Then

        Else
            txtCuenta.Text = BindingSource2.Item(BindingSource2.Position)(1)
            cboMoneda.SelectedValue = BindingSource2.Item(BindingSource2.Position)(3)
            'Dim dat As Object = 
            If BindingSource2.Item(BindingSource2.Position)(6) = 1 Then 'Activo
                lsEstado.SelectedIndex = 0
            Else
                lsEstado.SelectedIndex = 1 'Inactivo
            End If
        End If
    End Sub

    ''' <summary>
    ''' inserta un registro en tbanco
    ''' </summary>
    ''' <param name="banco"></param>
    ''' <remarks></remarks>
    Private Sub comandoInsertBco(ByVal banco As String)
        cmInsertTableBco = New SqlCommand
        cmInsertTableBco.CommandType = CommandType.Text
        cmInsertTableBco.CommandText = "insert TBanco (banco) values (@banco)"
        cmInsertTableBco.Connection = Cn
        cmInsertTableBco.Parameters.Add("@banco", SqlDbType.VarChar, 40).Value = banco

    End Sub
    ''' <summary>
    ''' inserta un registro en tCuentaBan
    ''' </summary>
    ''' <param name="cuenta"></param>
    ''' <param name="moneda"></param>
    ''' <param name="banco"></param>
    ''' <remarks></remarks>
    Private Sub comandoInsertCta(ByVal cuenta As String, ByVal moneda As Integer, ByVal banco As Integer)
        cmInsertTableCta = New SqlCommand
        cmInsertTableCta.CommandType = CommandType.Text
        cmInsertTableCta.CommandText = "insert TCuentaBan (nroCue,codMon,codBan,estado) values (@nroCue,@codMon,@codBan,1)"
        cmInsertTableCta.Connection = Cn
        cmInsertTableCta.Parameters.Add("@nroCue", SqlDbType.VarChar, 60).Value = cuenta
        cmInsertTableCta.Parameters.Add("@codMon", SqlDbType.Int).Value = moneda
        cmInsertTableCta.Parameters.Add("@codBan", SqlDbType.Int).Value = banco
    End Sub
    ''' <summary>
    ''' actualiza datos de tBanco
    ''' </summary>
    ''' <param name="banco"></param>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Private Sub comandoUpdateBco(ByVal banco As String, ByVal codigo As Integer)
        cmUpdateTableBco = New SqlCommand
        cmUpdateTableBco.CommandType = CommandType.Text
        cmUpdateTableBco.CommandText = "update TBanco set banco=@banco where codBan=@codBan"
        cmUpdateTableBco.Connection = Cn
        cmUpdateTableBco.Parameters.Add("@banco", SqlDbType.VarChar, 40).Value = banco
        cmUpdateTableBco.Parameters.Add("@codBan", SqlDbType.Int).Value = codigo

    End Sub
    ''' <summary>
    ''' Actualiza datos de TCuenta
    ''' </summary>
    ''' <param name="cuenta"></param>
    ''' <param name="codMon"></param>
    ''' <param name="codBan"></param>
    ''' <param name="idCuenta"></param>
    ''' <remarks></remarks>
    Private Sub comandoUpdateCta(ByVal cuenta As String, ByVal codMon As Integer, ByVal estado As Integer, ByVal codBan As Integer, ByVal idCuenta As Integer)

        cmUpdateTableCta = New SqlCommand
        cmUpdateTableCta.CommandType = CommandType.Text
        cmUpdateTableCta.CommandText = "update TCuentaBan set nroCue=@nroCue,codMon=@codMon, codBan=@codBan, estado=@estado where idCue=@idCue"
        cmUpdateTableCta.Connection = Cn
        cmUpdateTableCta.Parameters.Add("@nroCue", SqlDbType.VarChar, 60).Value = cuenta
        cmUpdateTableCta.Parameters.Add("@codMon", SqlDbType.Int).Value = codMon
        cmUpdateTableCta.Parameters.Add("@codBan", SqlDbType.Int).Value = codBan
        cmUpdateTableCta.Parameters.Add("@idCue", SqlDbType.Int).Value = idCuenta
        cmUpdateTableCta.Parameters.Add("@estado", SqlDbType.Int).Value = estado
    End Sub
    ''' <summary>
    ''' Elimina un registro de la tabla banco
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Private Sub comandoDeleteBco(ByVal codigo As Integer)
        cmDeleteTableBco = New SqlCommand
        cmDeleteTableBco.CommandType = CommandType.Text
        cmDeleteTableBco.CommandText = "Delete from TBanco Where codBan=@codBan"
        cmDeleteTableBco.Connection = Cn
        cmDeleteTableBco.Parameters.Add("@codBan", SqlDbType.Int).Value = codigo

    End Sub

    ''' <summary>
    ''' elimina un registro de la tabla Cuenta
    ''' </summary>
    ''' <param name="idCuenta"></param>
    ''' <remarks></remarks>
    Private Sub comandoDeleteCta(ByVal idCuenta As Integer)
        cmDeleteTableCta = New SqlCommand
        cmDeleteTableCta.CommandType = CommandType.Text
        cmDeleteTableCta.Connection = Cn
        cmDeleteTableCta.CommandText = "Delete from tCuentaBan where idCue=@idCue"
        cmDeleteTableCta.Parameters.Add("@idCue", SqlDbType.Int).Value = idCuenta

    End Sub
    ''' <summary>
    ''' Valida el registro de un nuevo banco
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarBco() As Boolean
        Dim retorna As Boolean = True
        If validaCampoVacioMinCaracNoNumer(txtBanco.Text().Trim, 3) Then
            MessageBox.Show("Ingreso un nombre validao, minimo tres caracteres: ", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtBanco.Focus()
            txtBanco.SelectAll()
            retorna = False
        End If

        If BindingSource1.Find("banco", txtBanco.Text.Trim()) > 0 Then
            MessageBox.Show("Ya existe Banco: " & txtBanco.Text.Trim() & Chr(13) & "Cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtBanco.Focus()
            txtBanco.SelectAll()
            retorna = False
        End If

        Return retorna
    End Function

    Private Function ValidarCta() As Boolean
        Dim retorna As Boolean = True

        If validaCampoVacio(txtCuenta.Text.Trim()) Then
            MessageBox.Show("Ingreso un numero de cuenta valido: ", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCuenta.Focus()
            txtCuenta.SelectAll()
            retorna = False
        End If
        If BindingSource2.Find("nroCue", txtCuenta.Text.Trim()) > 0 Then
            MessageBox.Show("Ya existe el Número de cuenta: " & txtCuenta.Text.Trim() & Chr(13) & "Cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCuenta.Focus()
            txtCuenta.SelectAll()
            retorna = False
        End If

        Return retorna


    End Function
#End Region




    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsEstado.SelectedIndexChanged

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()


    End Sub

    Private Sub MantCuentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cConfigGrilla.ConfigGrilla(dgBancos)
        cConfigGrilla.ConfigGrilla(dgCuentas)
        DatosIniciales()
        ModificarColumnasDGV()



        'Para Encapsular en otro metodo


        dgCuentas.RowHeadersVisible = False
        With dgCuentas
            .Columns("nroCue").Visible = False
            .Columns("moneda").Visible = False
            .Columns("estado").Visible = False
        End With
        btnNuevoBco.Enabled = True
        btnModificarBco.Enabled = False
        btnEliminarBco.Enabled = False
        Panel2.Enabled = False
        btnModificarCta.Enabled = False
        btnEliminarCta.Enabled = False
        btnNuevoCta.Enabled = False
        lsEstado.Enabled = False

        'txtBanco.Focus()

        dgBancos.ClearSelection()
        dgCuentas.ClearSelection()
        dgBancos.FirstDisplayedScrollingRowIndex = -1

        'dgCuentas.FirstDisplayedScrollingRowIndex = -1







    End Sub




    Private Sub dgBancos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgBancos.CellClick
        enlazarTextBanco()


        btnModificarBco.Enabled = True
        btnEliminarBco.Enabled = True
        Panel2.Enabled = True

        btnNuevoCta.Enabled = True

        With dgCuentas
            .Columns("nroCue").Visible = True
            .Columns("moneda").Visible = True
            .Columns("estado").Visible = True
        End With

        dgCuentas.ClearSelection()

        txtCuenta.Clear()
        cboMoneda.SelectedIndex = 0
        lsEstado.Enabled = False

        txtCuenta.Focus()

        'dgCuentas.FirstDisplayedScrollingRowIndex = -1

    End Sub

    Private Sub dgCuentas_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCuentas.CellClick

        enlazarTextCuenta()
        btnModificarCta.Enabled = True
        btnEliminarCta.Enabled = True
        lsEstado.Enabled = True

    End Sub

    Private Sub btnNuevoBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoBco.Click

        If ValidarBco() = False Then
            Exit Sub
        Else

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()
                Dim _bancoTemp As String = txtBanco.Text.Trim()
                comandoInsertBco(txtBanco.Text.Trim())
                cmInsertTableBco.Transaction = myTrans

                If cmInsertTableBco.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado(" LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                'Actualizando los dataSet
                dsAlmacen.Tables("TCuentaBancaria").Clear()
                dsAlmacen.Tables("TBanco").Clear()
                daTabla2.Fill(dsAlmacen, "TBanco")
                daTabla1.Fill(dsAlmacen, "TCuentaBancaria")
                'Ubicando el nuebo ítem en la grilla
                BindingSource1.Position = BindingSource1.Find("banco", _bancoTemp)
                '
                StatusBarClass.messageBarraEstado("  Registro fué agregado con exito...")


            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTÁ SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Finally
                wait.Close()
            End Try

        End If


    End Sub

    Private Sub btnModificarBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarBco.Click

        Dim _bancoTemp As String = BindingSource1.Item(BindingSource1.Position)(1)

        If _bancoTemp.ToUpper() <> txtBanco.Text.Trim().ToUpper() Then
            If ValidarBco() = False Then
                Exit Sub
            Else
                Me.Refresh()
                Dim FinalMyTrans As Boolean = False

                Dim myTrans As SqlTransaction = Cn.BeginTransaction()

                Dim wait As New waitForm
                wait.Show()

                Try
                    StatusBarClass.messageBarraEstado(" ESPERE POR FAVOR, GUARDANDO INFORMACION....")
                    comandoUpdateBco(txtBanco.Text.Trim(), BindingSource1.Item(BindingSource1.Position)(0))

                    cmUpdateTableBco.Transaction = myTrans


                    If cmUpdateTableBco.ExecuteNonQuery() < 1 Then
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                    End If

                    myTrans.Commit()
                    FinalMyTrans = True

                    'Actualizando los dataSet
                    dsAlmacen.Tables("TCuentaBancaria").Clear()
                    dsAlmacen.Tables("TBanco").Clear()
                    daTabla2.Fill(dsAlmacen, "TBanco")
                    daTabla1.Fill(dsAlmacen, "TCuentaBancaria")
                    'Ubicando el nuebo ítem en la grilla
                    BindingSource1.Position = BindingSource1.Find("banco", txtBanco.Text.Trim())
                    '
                    StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")




                Catch f As Exception

                    If FinalMyTrans Then
                        MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    Else
                        myTrans.Rollback()
                        MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If

                Finally
                    wait.Close()

                End Try


            End If
        End If

    End Sub

    Private Sub btnEliminarBco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarBco.Click

        If dgBancos.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim finalMyTrans As Boolean = False

        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            comandoDeleteBco(BindingSource1.Item(BindingSource1.Position)(0))

            cmDeleteTableBco.Transaction = myTrans
            If cmDeleteTableBco.ExecuteNonQuery() < 1 Then
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar banco porque esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub

            End If
            Me.Refresh()

            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMyTrans = True

            dsAlmacen.Tables("TCuentaBancaria").Clear()
            dsAlmacen.Tables("TBanco").Clear()
            daTabla2.Fill(dsAlmacen, "TBanco")
            daTabla1.Fill(dsAlmacen, "TCuentaBancaria")


        Catch f As Exception
            If finalMyTrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        Finally
            wait.Close()

        End Try

    End Sub

    Private Sub btnNuevoCta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCta.Click
        If ValidarCta() = False Then
            Exit Sub
        Else
            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm

            wait.Show()

            Try

                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()
                Dim _bancoTemp As String = txtCuenta.Text.Trim()
                comandoInsertCta(txtCuenta.Text.Trim(), cboMoneda.SelectedValue, BindingSource1.Item(BindingSource1.Position)(0))

                cmInsertTableCta.Transaction = myTrans

                If cmInsertTableCta.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado(" LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                dsAlmacen.Tables("TCuentaBancaria").Clear()
                daTabla1.Fill(dsAlmacen, "TCuentaBancaria")

                

                BindingSource2.Position = BindingSource2.Find("nroCue", txtCuenta.Text.Trim())
                StatusBarClass.messageBarraEstado("  Registro fue agregado con exito...")

            Catch f As Exception
                If finalMyTrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTÁ SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

            Finally
                wait.Close()


            End Try

        End If
    End Sub

    Private Sub btnModificarCta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarCta.Click

        Dim _nroCtaTemp As String = BindingSource2.Item(BindingSource2.Position)(1)
        If _nroCtaTemp.ToUpper() <> txtCuenta.Text.Trim().ToUpper() Then
            If ValidarCta() = False Then
                Exit Sub

            Else

                Me.Refresh()
                Dim finalMyTrans As Boolean = False

                Dim myTrans As SqlTransaction = Cn.BeginTransaction()

                Dim wait As New waitForm
                wait.Show()

                Try
                    StatusBarClass.messageBarraEstado(" ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                    Dim _estado As Integer

                    If lsEstado.SelectedIndex = 0 Then
                        _estado = 1
                    Else
                        _estado = 0
                    End If

                    comandoUpdateCta(txtCuenta.Text.Trim(), cboMoneda.SelectedValue, _estado, BindingSource1.Item(BindingSource1.Position)(0), BindingSource2.Item(BindingSource2.Position)(0))
                    cmUpdateTableCta.Transaction = myTrans

                    If cmUpdateTableCta.ExecuteNonQuery() < 1 Then
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                    End If

                    myTrans.Commit()
                    finalMyTrans = True

                    dsAlmacen.Tables("TCuentaBancaria").Clear()
                    daTabla1.Fill(dsAlmacen, "TCuentaBancaria")

                    BindingSource2.Position = BindingSource2.Find("nroCue", txtCuenta.Text.Trim())
                    StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")

                Catch f As Exception
                    If finalMyTrans Then
                        MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    Else
                        myTrans.Rollback()
                        MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If

                Finally
                    wait.Close()

                End Try

            End If
        End If

    End Sub

    Private Sub btnEliminarCta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCta.Click

        If dgCuentas.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        Dim finalMyTrans As Boolean = False

        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            comandoDeleteCta(BindingSource2.Item(BindingSource2.Position)(0))

            cmDeleteTableCta.Transaction = myTrans


            If cmDeleteTableCta.ExecuteNonQuery() < 1 Then
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar Cuenta porque esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub

            End If

            Me.Refresh()
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMyTrans = True

            dsAlmacen.Tables("TCuentaBancaria").Clear()
            daTabla1.Fill(dsAlmacen, "TCuentaBancaria")


        Catch f As Exception
            If finalMyTrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("Tipo de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        Finally
            wait.Close()

        End Try

    End Sub
End Class
