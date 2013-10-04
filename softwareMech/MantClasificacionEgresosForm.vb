Imports System.Data.SqlClient
Imports System.Data
Imports ComponentesSolucion2008

Public Class MantClasificacionEgresosForm

#Region "variables"

    ''' <summary>
    ''' Clasificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource0 As New BindingSource

    ''' <summary>
    ''' subClasificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource1 As New BindingSource

    ''' <summary>
    ''' instancia de objeto de la clase DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager


    ''' <summary>
    ''' instancia de objeto de la clase cConfigForm
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    'comandos para Clasificacion
    Dim cmInsertClasif As SqlCommand
    Dim cmUpdateClasif As SqlCommand
    Dim cmDeleteClasif As SqlCommand

    'comandos para Subclasificacion
    Dim cmInsertSubClasif As SqlCommand
    Dim cmUpdateSubClasif As SqlCommand
    Dim cmDeleteSubClasif As SqlCommand



#End Region

#Region "Métodos"

    ''' <summary>
    ''' configura el color de los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarColorControl()

        Me.BackColor = BackColorP

        For index As Integer = 0 To Panel1.Controls.Count - 1
            If TypeOf Panel1.Controls(index) Is Label Then
                Panel1.Controls(index).ForeColor = ForeColorLabel

            End If

            If TypeOf Panel1.Controls(index) Is Button Then
                Panel1.Controls(index).ForeColor = ForeColorButtom

            End If

        Next


        oGrilla.configurarColorControlPanel("Label", Panel2, ForeColorLabel)
        oGrilla.configurarColorControlPanel("Button", Panel2, ForeColorButtom)

    End Sub

    ''' <summary>
    ''' modifica la configuracion de los DataGridView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()




        'Configuración Dg Clasificación
        dgClasificacion.AllowDrop = False
        dgClasificacion.AllowUserToAddRows = False
        dgClasificacion.AllowUserToDeleteRows = False
        dgClasificacion.ReadOnly = True
        'dgClasificacion.AutoSizeRowsMode = 
        'configuración Dg SubClasificación

        dgSubClasif.AllowDrop = False
        dgSubClasif.AllowUserToAddRows = False
        dgSubClasif.AllowUserToDeleteRows = False
        dgSubClasif.ReadOnly = True

        If bindingSource0.Count > 0 Then



            With dgClasificacion
                .Columns("codCla").Visible = False
                .Columns("clasificacion").HeaderText = "Clasificación"
                .Columns("clasificacion").Width = 220
                .Columns("idTM").Visible = False

            End With
        End If

        If bindingSource1.Count > 0 Then



            With dgSubClasif
                .Columns("codTipCla").Visible = False
                .Columns("tipoClasif").HeaderText = "SubTipo"
                .Columns("tipoClasif").Width = 250
                .Columns("estado").HeaderText = "Estado"
                .Columns("estado").Width = 80
                .Columns("codCla").Visible = False
                .Columns("codEstado").Visible = False
            End With

        End If


    End Sub

    ''' <summary>
    ''' enlaza la grilla clasificación con los controles relacionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextClasificacion()
        If bindingSource0.Count = 0 Then

        Else
            txtClasificacion.Text = bindingSource0.Item(bindingSource0.Position)(1)

        End If

    End Sub

    ''' <summary>
    ''' enlaza la grilla subClasificacion con los controles relacionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazarTextSubClasificacion()
        If bindingSource1.Count = 0 Then

        Else
            txtSubClasificacion.Text = bindingSource1.Item(bindingSource1.Position)(1)

            If bindingSource1.Item(bindingSource1.Position)(2) = 1 Then
                rbActivo.Checked = True
                rbInactivo.Checked = False
            Else
                rbInactivo.Checked = True
                rbActivo.Checked = False
            End If

        End If

    End Sub

    ''' <summary>
    ''' activa los controles asociados a la clasificación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ActivarControlesClasificacion()

        Panel1.Enabled = True
        'Panel2.Enabled = True
        btnNuevoCla.Enabled = True
        btnNuevoCla.FlatStyle = FlatStyle.Standard
        btnModificarCla.Enabled = True
        btnModificarCla.FlatStyle = FlatStyle.Standard
        btnEliminarCla.Enabled = True
        btnEliminarCla.FlatStyle = FlatStyle.Standard


    End Sub
    ''' <summary>
    ''' desactiva los controles asociados a Subclasificación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub desactivarControlesSubClasificacion()

        txtSubClasificacion.Enabled = False
        rbActivo.Enabled = False
        rbInactivo.Enabled = False

        btnNuevoSub.Enabled = False
        btnEliminarSub.Enabled = False
        btnModificarSub.Enabled = False
        'Panel2.Enabled = False

    End Sub
    ''' <summary>
    ''' activa los controles asociados a Subclasificación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activarControlesSubClasificacion()
        txtSubClasificacion.Enabled = True
        rbActivo.Enabled = True
        rbInactivo.Enabled = True

        btnNuevoSub.Enabled = True
        btnEliminarSub.Enabled = True
        btnModificarSub.Enabled = True
    End Sub


    ''' <summary>
    ''' comando que llena los datos para insertar
    ''' </summary>
    ''' <param name="clasifi"></param>
    ''' <param name="idTM"></param>
    ''' <remarks></remarks>
    Private Sub comandoInsertClasificacion(ByVal clasifi As String, ByVal idTM As Integer)

        cmInsertClasif = New SqlCommand
        cmInsertClasif.CommandType = CommandType.Text
        cmInsertClasif.CommandText = "insert TClasificacion values (@clasif,@idTM) "
        cmInsertClasif.Connection = Cn
        cmInsertClasif.Parameters.Add("@clasif", SqlDbType.VarChar, 30).Value = clasifi
        cmInsertClasif.Parameters.Add("@idTM", SqlDbType.Int).Value = idTM

    End Sub


    Private Sub comandoUpdateClasificacion(ByVal clasifi As String, ByVal tipoMovimiento As Integer, ByVal codigo As Integer)

        cmUpdateClasif = New SqlCommand
        cmUpdateClasif.CommandType = CommandType.Text
        cmUpdateClasif.CommandText = "update TClasificacion set clasificacion =@clasif, idTM=@idTM where codCla=@codCla"
        cmUpdateClasif.Connection = Cn
        cmUpdateClasif.Parameters.Add("@clasif", SqlDbType.VarChar, 30).Value = clasifi
        cmUpdateClasif.Parameters.Add("@idTM", SqlDbType.Int).Value = tipoMovimiento
        cmUpdateClasif.Parameters.Add("@codCla", SqlDbType.Int).Value = codigo

    End Sub

    Private Sub comandoDeleteClasificacion(ByVal codigo As Integer)
        cmDeleteClasif = New SqlCommand
        cmDeleteClasif.CommandType = CommandType.Text
        cmDeleteClasif.CommandText = "Delete from TClasificacion where codCla=@codCla"
        cmDeleteClasif.Connection = Cn
        cmDeleteClasif.Parameters.Add("@codCla", SqlDbType.Int).Value = codigo

    End Sub

    Private Sub comandoInsertSubClasificacion(ByVal SubClasif As String, ByVal codCla As Integer)
        cmInsertSubClasif = New SqlCommand
        cmInsertSubClasif.CommandType = CommandType.Text
        cmInsertSubClasif.CommandText = "insert TTipoClasif values (@tipoClasif,1,@codCla)"
        cmInsertSubClasif.Connection = Cn
        cmInsertSubClasif.Parameters.Add("@tipoClasif", SqlDbType.VarChar, 30).Value = SubClasif
        cmInsertSubClasif.Parameters.Add("@codCla", SqlDbType.Int).Value = codCla
    End Sub

    Private Sub comandoUpdateSubClasificacion(ByVal SubClasif As String, ByVal codCla As Integer, ByVal estado As Integer)

        cmUpdateSubClasif = New SqlCommand
        cmUpdateSubClasif.CommandType = CommandType.Text
        cmUpdateSubClasif.CommandText = "update TTipoClasif set tipoClasif=@tipoClasif,codCla=@codCla,estado=@estado where codTipCla=@codTipCla"
        cmUpdateSubClasif.Connection = Cn
        cmUpdateSubClasif.Parameters.Add("@tipoClasif", SqlDbType.VarChar, 30).Value = SubClasif
        cmUpdateSubClasif.Parameters.Add("@codCla", SqlDbType.Int).Value = codCla
        cmUpdateSubClasif.Parameters.Add("@estado", SqlDbType.Int).Value = estado
        cmUpdateSubClasif.Parameters.Add("@codTipCla", SqlDbType.Int).Value = bindingSource1.Item(bindingSource1.Position)(0)


    End Sub

    Private Sub comandoDeleteSubClasificacion(ByVal codigo As Integer)
        cmDeleteSubClasif = New SqlCommand
        cmDeleteSubClasif.CommandType = CommandType.Text
        cmDeleteSubClasif.CommandText = "delete TTipoClasif where codTipCla = @codTipCla"
        cmDeleteSubClasif.Connection = Cn
        cmDeleteSubClasif.Parameters.Add("@codTipCla", SqlDbType.Int).Value = codigo


    End Sub

    Private Function validarSubClasificacion() As Boolean
        Dim retorna As Boolean = True

        If validaCampoVacioMinCaracNoNumer(txtSubClasificacion.Text.Trim(), 3) Then
            MessageBox.Show("Ingreso una Subclasificación valida, mínimo tres caracteres: ", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtClasificacion.Focus()
            txtClasificacion.SelectAll()
            retorna = False

        End If

        If bindingSource1.Find("tipoClasif", txtSubClasificacion.Text.Trim()) > 0 Then
            MessageBox.Show("Ya existe: " & txtSubClasificacion.Text.Trim() & Chr(13) & "cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtSubClasificacion.Focus()
            txtSubClasificacion.SelectAll()
            retorna = False
        End If
        Return retorna
    End Function


    ''' <summary>
    ''' valida el registro de clasificación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validarClasificacion() As Boolean
        Dim retorna As Boolean = True

        If validaCampoVacioMinCaracNoNumer(txtClasificacion.Text.Trim(), 3) Then
            MessageBox.Show("Ingreso una clasificación valida, mínimo tres caracteres: ", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtClasificacion.Focus()
            txtClasificacion.SelectAll()
            retorna = False
        End If
        Dim Clas As String = txtClasificacion.Text.Trim()
        Dim i As Integer = bindingSource0.Find("clasificacion", txtClasificacion.Text.Trim())


        Dim j As Integer = bindingSource0.Count

        If i > 0 Then
            MessageBox.Show("Ya existe: " & txtClasificacion.Text.Trim() & Chr(13) & "cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtClasificacion.Focus()
            txtClasificacion.SelectAll()
            retorna = False

        End If

        Return retorna

    End Function

#End Region

#Region "Eventos"

    Private Sub MantClasificacionEgresosForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Datos iniciales de bd

        oDataManager.CargarCombo("Select idTM,tipoMov from TTipoMovimiento", CommandType.Text, cbMovimiento, "idTM", "tipoMov")


        ' cargando Grilla

        oDataManager.CargarGrilla("Select codCla,clasificacion,idTM from TClasificacion where codCla>1 ", CommandType.Text, dgClasificacion, bindingSource0)



        'Modificar Columnas Grilla
        ModificarColumnasDGV()

        'Configuración de Colores para el form
        ConfigurarColorControl()

        'configurar Botones
        btnNuevoCla.Enabled = True
        btnModificarCla.Enabled = False
        btnEliminarCla.Enabled = False

        'Subclasificación 
        'Panel2.Enabled = False
        desactivarControlesSubClasificacion()


        btnModificarSub.Enabled = False
        btnEliminarSub.Enabled = False
        btnNuevoSub.Enabled = False
        rbActivo.Enabled = False
        rbInactivo.Enabled = False



    End Sub



    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub


#End Region

    Private Sub btnNuevoCla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCla.Click

        'Validacion 
        If validarClasificacion() = False Then
            Exit Sub
        End If

        Dim finalMyTrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try

            StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
            Me.Refresh()

            Dim _clasif As String = txtClasificacion.Text.Trim()

            comandoInsertClasificacion(txtClasificacion.Text.Trim(), cbMovimiento.SelectedValue)
            cmInsertClasif.Transaction = myTrans


            If cmInsertClasif.ExecuteNonQuery() < 1 Then
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub

            End If


            myTrans.Commit()
            StatusBarClass.messageBarraEstado(" ")

            StatusBarClass.messageBarraEstado(" LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMyTrans = True

            'Actualizando grilla desde bd

            'limpiando grilla
            dgClasificacion.DataSource = ""
            'consultando grilla
            oDataManager.CargarGrilla("Select codCla,clasificacion,idTM from TClasificacion where codCla>1 ", CommandType.Text, dgClasificacion, bindingSource0)
            ModificarColumnasDGV()


            bindingSource0.Position = bindingSource0.Find("clasificacion", _clasif)

            StatusBarClass.messageBarraEstado("  Registro fue agregado con éxito...")


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


    End Sub

    Private Sub btnModificarCla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarCla.Click

        Dim _clasif As String = bindingSource0.Item(bindingSource0.Position)(1)

        If _clasif.ToUpper() <> txtClasificacion.Text.Trim().ToUpper() Then
            If validarClasificacion() = False Then
                Exit Sub

            Else

                Me.Refresh()
                Dim FinalMyTrans As Boolean = False

                Dim myTrans As SqlTransaction = Cn.BeginTransaction()

                Dim wait As New waitForm
                wait.Show()

                Try

                    StatusBarClass.messageBarraEstado(" ESPERE POR FAVOR, GUARDANDO INFORMACION....")

                    comandoUpdateClasificacion(txtClasificacion.Text.Trim(), cbMovimiento.SelectedValue, bindingSource0.Item(bindingSource0.Position)(0))
                    cmUpdateClasif.Transaction = myTrans

                    If cmUpdateClasif.ExecuteNonQuery() < 1 Then
                        myTrans.Rollback()
                        MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                        Me.Close()
                    End If

                    myTrans.Commit()
                    FinalMyTrans = True

                    'Actualizando la información

                    'limpiando grilla
                    dgClasificacion.DataSource = ""
                    'consultando grilla
                    oDataManager.CargarGrilla("Select codCla,clasificacion,idTM from TClasificacion where codCla>1 ", CommandType.Text, dgClasificacion, bindingSource0)
                    ModificarColumnasDGV()

                    _clasif = txtClasificacion.Text.Trim()

                    bindingSource0.Position = bindingSource0.Find("clasificacion", _clasif)

                    StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")



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

    Private Sub btnEliminarCla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCla.Click

        If dgClasificacion.RowCount = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        'valida la relación con registros de subclasificaciones

        If oDataManager.consultarTabla("select count(*) from TTipoClasif where codCla= " & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CLASIFICACIÓN TIENE SUB CLASIFICACIONES ASIGNADAS...")
            Exit Sub

        End If

        Dim resultado As DialogResult = MessageBox.Show("Está seguro de eliminar esté registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If resultado = Windows.Forms.DialogResult.Yes Then
            Dim finalMyTrans As Boolean = False

            Dim myTrans As SqlTransaction = Cn.BeginTransaction()

            Dim wait As New waitForm
            wait.Show()

            Try

                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
                comandoDeleteClasificacion(bindingSource0.Item(bindingSource0.Position)(0))
                cmDeleteClasif.Transaction = myTrans

                If cmDeleteClasif.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar Clasificación porque esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                Me.Refresh()

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
                finalMyTrans = True

                'Actualizando grilla desde bd

                'limpiando grilla
                dgClasificacion.DataSource = ""
                'consultando grilla
                oDataManager.CargarGrilla("Select codCla,clasificacion,idTM from TClasificacion where codCla>1 ", CommandType.Text, dgClasificacion, bindingSource0)
                ModificarColumnasDGV()


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
        Else
            Exit Sub
        End If

    End Sub

    Private Sub dgClasificacion_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgClasificacion.CellClick

        enlazarTextClasificacion()

        Dim id = bindingSource0.Item(bindingSource0.Position)(0)

        If dgClasificacion.RowCount > 0 Then
            oDataManager.CargarGrilla("Select codTipCla,tipoClasif,estado as codEstado,codCla,case when estado=1 then 'ACTIVO' else 'INACTIVO' end as estado from TTipoClasif where codTipCla >1 and codCla =" & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text, dgSubClasif, bindingSource1)
            ModificarColumnasDGV()
        End If

        btnModificarCla.Enabled = True
        btnEliminarCla.Enabled = True

        btnNuevoSub.Enabled = True
        txtSubClasificacion.Clear()
        rbActivo.Enabled = True
        rbInactivo.Enabled = True
        txtSubClasificacion.Enabled = True
        txtSubClasificacion.Focus()


    End Sub

    Private Sub dgClasificacion_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgClasificacion.DataBindingComplete
        DirectCast(sender, DataGridView).ClearSelection()
    End Sub

    Private Sub btnNuevoSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoSub.Click

        'Validar cuenta
        If validarSubClasificacion() = False Then
            Exit Sub

        Else
            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm

            wait.Show()

            Try
                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()

                Dim _subClasif As String = txtSubClasificacion.Text.Trim()
                comandoInsertSubClasificacion(txtSubClasificacion.Text.Trim(), bindingSource0.Item(bindingSource0.Position)(0))

                cmInsertSubClasif.Transaction = myTrans

                If cmInsertSubClasif.ExecuteNonQuery() < 1 Then
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub

                End If

                myTrans.Commit()
                StatusBarClass.messageBarraEstado(" LOS DATOS FUERON GUARDADOS CON ÉXITO...")
                finalMyTrans = True

                'Actualizando información
                dgSubClasif.DataSource = ""
                'Consultando grilla
                oDataManager.CargarGrilla("Select codTipCla,tipoClasif,estado as codEstado,codCla,case when estado=1 then 'ACTIVO' else 'INACTIVO' end as estado from TTipoClasif where codTipCla >1 and codCla =" & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text, dgSubClasif, bindingSource1)
                ModificarColumnasDGV()

                bindingSource1.Position = bindingSource1.Find("tipoClasif", _subClasif)

                StatusBarClass.messageBarraEstado("  Registro fue agregado con éxito...")


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

    Private Sub btnModificarSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarSub.Click

        Dim _subClasif As String = bindingSource1.Item(bindingSource1.Position)(1) 'txtSubClasificacion.Text.Trim()
        If _subClasif.ToUpper() <> txtSubClasificacion.Text.Trim().ToUpper() Then
            If validarSubClasificacion() = False Then
                Exit Sub
            End If
        End If

        Me.Refresh()

        Dim finalMyTrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction

        Dim wait As New waitForm
        wait.Show()

        Try

            StatusBarClass.messageBarraEstado(" ESPERE POR FAVOR, GUARDANDO INFORMACIÓN....")

            'obteniendo el estado de la subclasificación
            Dim _estado As Integer = 1

            If rbInactivo.Checked = True Then
                _estado = 0
            Else
                _estado = 1
            End If

            comandoUpdateSubClasificacion(txtSubClasificacion.Text.Trim, bindingSource0.Item(bindingSource0.Position)(0), _estado)
            cmUpdateSubClasif.Transaction = myTrans

            If cmUpdateSubClasif.ExecuteNonQuery() < 1 Then
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
            End If

            myTrans.Commit()
            finalMyTrans = True

            'Actualizando información
            dgSubClasif.DataSource = ""
            'Consultando grilla
            oDataManager.CargarGrilla("Select codTipCla,tipoClasif,estado as codEstado,codCla,case when estado=1 then 'ACTIVO' else 'INACTIVO' end as estado from TTipoClasif where codTipCla >1 and codCla =" & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text, dgSubClasif, bindingSource1)
            ModificarColumnasDGV()

            bindingSource1.Position = bindingSource1.Find("tipoClasif", txtSubClasificacion.Text.Trim())

            StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")


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

    End Sub

    Private Sub btnEliminarSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarSub.Click

        If dgSubClasif.RowCount = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        'Validando las subclasificaciones relacionadas

        If oDataManager.consultarTabla("select Count(*) from TOrdenDesembolso where vanCaja = " & bindingSource1.Item(bindingSource1.Position)(0), CommandType.Text) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CUENTA TIENE ORDENES DE DESEMBOLSO ASIGNADAS...")
            Exit Sub
        End If

        If oDataManager.consultarTabla("select Count(*) from TPagoDesembolso where codTipCla = " & bindingSource1.Item(bindingSource1.Position)(0), CommandType.Text) > 0 Then
            StatusBarClass.messageBarraEstado("  ACCESO DENEGADO... CUENTA TIENE PAGOS DE DESEMBOLSO ASIGNADAS...")
            Exit Sub
        End If

        Dim resultado As DialogResult = MessageBox.Show("Está seguro de eliminar esté registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If resultado = Windows.Forms.DialogResult.Yes Then

            Dim finalMyTrans As Boolean = False

            Dim myTrans As SqlTransaction = Cn.BeginTransaction()

            Dim wait As New waitForm
            wait.Show()

            Try
                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTROS...")
                comandoDeleteSubClasificacion(bindingSource1.Item(bindingSource1.Position)(0))
                cmDeleteSubClasif.Transaction = myTrans

                If cmDeleteSubClasif.ExecuteNonQuery() < 1 Then

                    myTrans.Rollback()
                    MessageBox.Show("No se puede eliminar Subclasificación porque esta actualmente compartiendo...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                Me.Refresh()

                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON ÉXITO...")
                finalMyTrans = True

                'Actualizando información
                dgSubClasif.DataSource = ""
                'Consultando grilla
                oDataManager.CargarGrilla("Select codTipCla,tipoClasif,estado as codEstado,codCla,case when estado=1 then 'ACTIVO' else 'INACTIVO' end as estado from TTipoClasif where codTipCla >1 and codCla =" & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text, dgSubClasif, bindingSource1)
                ModificarColumnasDGV()

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

        Else
            Exit Sub
        End If

    End Sub

    Private Sub dgSubClasif_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSubClasif.CellClick

        enlazarTextSubClasificacion()

        'If dgClasificacion.RowCount > 0 Then
        '    oDataManager.CargarGrilla("select codTipCla,tipoClasif,estado,codCla from TTipoClasif where codTipCla >1 and codCla =" & bindingSource0.Item(bindingSource0.Position)(0), CommandType.Text, dgSubClasif, bindingSource1)
        '    ModificarColumnasDGV()
        'End If

        'HABILITANDO CONTROLES DE DATOS DE SUBCLASIFICACION

        activarControlesSubClasificacion()
        txtSubClasificacion.Focus()

    End Sub
End Class
