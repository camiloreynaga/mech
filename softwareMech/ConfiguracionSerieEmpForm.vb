Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class ConfiguracionSerieEmpForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub ConfiguracionSerieEmpForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub ConfiguracionSerieEmpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codTipDE,tipoDE,estado from TTipoDocEmp where estado=1"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select codSerS,serie,iniNroDoc,finNroDoc,'est'=case when estado=1 then 'Activo' else 'INACTIVO' end,descrip,codTipDE,estado from TSerieSede  where codSerS>2 order by serie"   '1=Nota Venta
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetRestaurantModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "TTipoDocEmp")
            daTabla2.Fill(dsAlmacen, "TSerieSede")

            AgregarRelacion()

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TTipoDocEmp"
            lbTabla.DataSource = BindingSource1
            lbTabla.DisplayMember = "tipoDE"
            lbTabla.ValueMember = "codTipDE"

            BindingSource2.DataSource = BindingSource1
            BindingSource2.DataMember = "Relacion1"
            Navigator1.BindingSource = BindingSource2
            dgTabla1.DataSource = BindingSource2
            ModificarColumnasDGV()

            configurarColorControl()
            txtCod.DataBindings.Add("Text", BindingSource2, "codSerS")

            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub AgregarRelacion()
        'agregando una relacion entre la tablaS
        Dim relation1 As New DataRelation("Relacion1", dsAlmacen.Tables("TTipoDocEmp").Columns("codTipDE"), dsAlmacen.Tables("TSerieSede").Columns("codTipDE"))
        dsAlmacen.Relations.Add(relation1)
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Nºserie"
            .Columns(1).Width = 60
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(2).HeaderText = "Nºdoc_inic."
            .Columns(2).Width = 80
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).HeaderText = "Nºdoc_final"
            .Columns(3).Width = 80
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).Width = 70
            .Columns(4).HeaderText = "Estado"
            .Columns(5).Width = 180
            .Columns(5).HeaderText = "Descripción"
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
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub enlazarText()
        If BindingSource2.Count = 0 Then
            txtSerie.Text = ""
            txtNroDoc1.Text = ""
            txtNroDoc2.Text = ""
            txtDes.Text = ""
            lbEstado.SelectedIndex = 0
            Exit Sub
        End If
        txtSerie.Text = BindingSource2.Item(BindingSource2.Position)(1)
        txtNroDoc1.Text = BindingSource2.Item(BindingSource2.Position)(2)
        txtNroDoc2.Text = BindingSource2.Item(BindingSource2.Position)(3)
        txtDes.Text = BindingSource2.Item(BindingSource2.Position)(5)
        If BindingSource2.Item(BindingSource2.Position)(7) = 1 Then  'Activo
            lbEstado.SelectedIndex = 0
        Else
            lbEstado.SelectedIndex = 1
        End If
    End Sub

    Private Sub txtCod_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCod.TextChanged
        enlazarText()
    End Sub

    Private Sub desactivarControles1()
        Panel1.Enabled = False
        If vfNuevo1 = "guardar" Then
            btnModificar.Enabled = False
            btnModificar.FlatStyle = FlatStyle.Flat
        Else    'Se presiono <Modificar>
            btnNuevo.Enabled = False
            btnNuevo.FlatStyle = FlatStyle.Flat
        End If
        btnCancelar.Enabled = True
        btnCancelar.FlatStyle = FlatStyle.Standard
        btnEliminar.Enabled = False
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnCerrar.Enabled = False
        btnCerrar.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub activarControles1()
        Panel1.Enabled = True
        btnNuevo.Enabled = True
        btnNuevo.FlatStyle = FlatStyle.Standard
        btnModificar.Enabled = True
        btnModificar.FlatStyle = FlatStyle.Standard
        btnCancelar.Enabled = False
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnEliminar.Enabled = True
        btnEliminar.FlatStyle = FlatStyle.Standard
        btnCerrar.Enabled = True
        btnCerrar.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub activarText()
        txtSerie.ReadOnly = False
        txtNroDoc1.ReadOnly = False
        txtNroDoc2.ReadOnly = False
        txtDes.ReadOnly = False
        If vfNuevo1 = "guardar" Then
            'cbUnidad.Enabled = True
        Else    'Se presiono <Modificar>
            lbEstado.Enabled = True
        End If
    End Sub

    Private Sub desActivarText()
        txtSerie.ReadOnly = True
        txtNroDoc1.ReadOnly = True
        txtNroDoc2.ReadOnly = True
        txtDes.ReadOnly = True
        lbEstado.Enabled = False
    End Sub

    Private Sub limpiarText()
        txtSerie.Clear()
        txtNroDoc1.Clear()
        txtNroDoc2.Clear()
        txtDes.Clear()
    End Sub

    Private Function ValidarCampos() As Boolean
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If ValidarCantMayorCero(txtSerie.Text.Trim) Then
            txtSerie.errorProv()
            Return True
        End If
        If ValidarCantMayorCero(txtNroDoc1.Text.Trim) Then
            txtNroDoc1.errorProv()
            Return True
        End If
        If ValidarCantMayorCero(txtNroDoc2.Text.Trim) Then
            txtNroDoc2.errorProv()
            Return True
        End If
        'Todo OK
        Return False
    End Function

    Dim vfNuevo1 As String = "nuevo"
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If vfNuevo1 = "nuevo" Then
            vfNuevo1 = "guardar"
            Me.btnNuevo.Text = "Guardar"
            desactivarControles1()
            txtCod.DataBindings.Clear()
            activarText()
            limpiarText()
            txtSerie.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo
        Else   ' guardar
            If ValidarCampos() Then
                Exit Sub
            End If

            If CInt(txtNroDoc1.Text) >= CInt(txtNroDoc2.Text) Then
                MessageBox.Show("Nº " & txtNroDoc2.Text.Trim() & " TIENE QUE SER MAYOR A Nº " & txtNroDoc1.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            wait.Show()
            Me.Cursor = Cursors.WaitCursor
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Try
                StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
                Me.Refresh()

                'TSerieSede
                comandoInsert()
                cmInserTable.Transaction = myTrans
                If cmInserTable.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    'deshace la transaccion
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS

                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMytrans = True

                'Actualizando el dataSet 
                dsAlmacen.Tables("TSerieSede").Clear()
                daTabla2.Fill(dsAlmacen, "TSerieSede")

                Me.btnCancelar.PerformClick()

                BindingSource2.MoveLast()
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")
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
                    MessageBox.Show("tipoDE de exception: " & f.Message & Chr(13) & "NO SE GUARDO LA INFORMACION PROCESADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        vfNuevo1 = "nuevo"
        Me.btnNuevo.Text = "Nuevo"
        vfModificar1 = "modificar"
        Me.btnModificar.Text = "Modificar"
        activarControles1()
        txtCod.DataBindings.Add("Text", BindingSource2, "codSerS")
        desActivarText()
        'Clase definida y con miembros shared en la biblioteca ComponentesRAS
        StatusBarClass.messageBarraEstado("  Proceso cancelado...")
        enlazarText()
    End Sub

    Private Function recuperarCount(ByVal codTipo As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TSerieSede where estado=1 and codTipDE=" & codTipo
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Dim vfModificar1 As String = "modificar"
    Dim vfCampo1 As Integer
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a modificar...")
            Exit Sub
        End If
        If vfModificar1 = "modificar" Then
            enlazarText()
            vfModificar1 = "actualizar"
            btnModificar.Text = "Actualizar"
            desactivarControles1()
            txtCod.DataBindings.Clear()
            activarText()
            txtSerie.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnModificar
        Else    'Actualizar
            If ValidarCampos() Then
                Exit Sub
            End If

            'If BindingSource2.Item(BindingSource2.Position)(7) = 0 Then  'Inactivo
            '    If lbEstado.Text.Trim() = "Activo" Then
            '        If recuperarCount(lbTabla.SelectedValue) > 0 Then
            '            MessageBox.Show("PROCESO DENEGADO, Desactive series que estan en estado [Activo]" & Chr(13) & "Solo puede estar activo una serie..", nomNegocio, Nothing, MessageBoxIcon.Error)
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If CInt(txtNroDoc1.Text) >= CInt(txtNroDoc2.Text) Then
                MessageBox.Show("Nº " & txtNroDoc2.Text.Trim() & " TIENE QUE SER MAYOR A Nº " & txtNroDoc1.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim resp As String = MessageBox.Show("Esta segúro de modificar documento...?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> 6 Then
                txtSerie.Focus()
                Exit Sub
            End If

            Me.Refresh()
            Dim finalMytrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction()
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ESPERE POR FAVOR, ACTUALIZANDO INFORMACION....")
                'TSerieSede
                comandoUpdate()
                cmUpdateTable.Transaction = myTrans
                If cmUpdateTable.ExecuteNonQuery() < 1 Then
                    'deshace la transaccion
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                vfCampo1 = BindingSource2.Item(BindingSource2.Position)(0)

                'confirma la transaccion
                myTrans.Commit()    'con exito RAS
                finalMytrans = True
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON EXITO...")

                'Actualizando el dataSet 
                dsAlmacen.Tables("TSerieSede").Clear()
                daTabla2.Fill(dsAlmacen, "TSerieSede")

                Me.btnCancelar.PerformClick()
                'Buscando por nombre de campo y luego pocisionarlo con el indice
                Dim j As Short = BindingSource2.Find("codSerS", vfCampo1)
                BindingSource2.Position = j
                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué actualizado con exito...")
                wait.Close()
            Catch f As Exception
                wait.Close()
                If finalMytrans Then
                    MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                Else
                    myTrans.Rollback()
                    MessageBox.Show(f.Message & Chr(13) & "NO SE ACTUALIZO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                End If
            End Try
        End If
    End Sub

    Public Function recuperarCount1(ByVal codSerS As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TGuiaRemEmp where codSerS=" & codSerS
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgTabla1.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount1(BindingSource2.Item(BindingSource2.Position)(0)) > 0 Then
            MessageBox.Show("Proceso denegado, Serie tiene Guias de Remision ya procesadas...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resp As String = MessageBox.Show("Está segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtSerie.Focus()
            Exit Sub
        End If
        Dim finalMytrans As Boolean = False
        'creando una instancia de transaccion 
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
            'Tabla TSerieSede
            comandoDelete()
            cmDeleteTable.Transaction = myTrans
            If cmDeleteTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("No se puede eliminar Habitacion por qué esta actualmente en uso...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Me.Refresh()

            'confirma la transaccion
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
            finalMytrans = True

            'Actualizando el dataSet 
            dsAlmacen.Tables("TSerieSede").Clear()
            daTabla2.Fill(dsAlmacen, "TSerieSede")

            enlazarText()
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
            wait.Close()
        Catch f As Exception
            wait.Close()
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
            Else
                'deshace la transaccion
                myTrans.Rollback()
                MessageBox.Show("tipoDE de exception: " & f.Message & Chr(13) & "NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Dim cmDeleteTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TSerieSede(serie,iniNroDoc,finNroDoc,descrip,estado,codTipDE) values(@ser,@ini,@fin,@des,@est,@codT)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@ser", SqlDbType.VarChar, 10).Value = txtSerie.Text.Trim()
        cmInserTable.Parameters.Add("@ini", SqlDbType.Int, 0).Value = txtNroDoc1.Text.Trim()
        cmInserTable.Parameters.Add("@fin", SqlDbType.Int, 0).Value = txtNroDoc2.Text.Trim()
        cmInserTable.Parameters.Add("@des", SqlDbType.VarChar, 40).Value = txtDes.Text.Trim()
        cmInserTable.Parameters.Add("@est", SqlDbType.Int, 0).Value = 0 '"Inactivo"
        cmInserTable.Parameters.Add("@codT", SqlDbType.Int, 0).Value = lbTabla.SelectedValue
    End Sub

    Dim cmUpdateTable As SqlCommand
    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TSerieSede set serie=@var,iniNroDoc=@var1,finNroDoc=@var2,descrip=@var3,estado=@var4 where codSerS=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@var", SqlDbType.VarChar, 10).Value = txtSerie.Text.Trim()
        cmUpdateTable.Parameters.Add("@var1", SqlDbType.Int, 0).Value = txtNroDoc1.Text.Trim()
        cmUpdateTable.Parameters.Add("@var2", SqlDbType.Int, 0).Value = txtNroDoc2.Text.Trim()
        cmUpdateTable.Parameters.Add("@var3", SqlDbType.VarChar, 40).Value = txtDes.Text.Trim()
        If lbEstado.SelectedIndex = 0 Then 'Activo
            cmUpdateTable.Parameters.Add("@var4", SqlDbType.Int, 0).Value = 1
        Else  'Inactivo
            cmUpdateTable.Parameters.Add("@var4", SqlDbType.Int, 0).Value = 0
        End If
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TSerieSede where codSerS=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource2.Item(BindingSource2.Position)(0)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub txtNroDoc1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroDoc1.KeyPress, txtNroDoc2.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtSerie_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerie.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
