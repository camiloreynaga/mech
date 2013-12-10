Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class mantRegimenLabForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource

    Private Sub mantRegimenLabForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub mantRegimenLabForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codReg,ltrim(str(diaLab))+' / '+ltrim(str(diaDes)) as dia,diaLab,diaDes from TRegLab"
        crearDataAdapterTable(daTabla1, sele)

        sele = "select idNro,nro,codReg from TNroReg where codReg=@codReg"
        crearDataAdapterTable(daTabla2, sele)
        daTabla2.SelectCommand.Parameters.Add("@codReg", SqlDbType.Int, 0).Value = 0

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb 
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla1.Fill(dsAlmacen, "TRegLab")
            daTabla2.Fill(dsAlmacen, "TNroReg")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TRegLab"
            lbReg.DataSource = BindingSource1
            lbReg.DisplayMember = "dia"
            lbReg.ValueMember = "codReg"
            'BindingSource1.Sort = "codSerS"

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "TNroReg"
            lbDia.DataSource = BindingSource2
            lbDia.DisplayMember = "nro"
            lbDia.ValueMember = "idNro"

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

    Private Sub mantRegimenLabForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        vfVan1 = True
        visualizarDet()
    End Sub

    Private Sub lbReg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbReg.SelectedIndexChanged
        visualizarDet()
    End Sub

    Dim vfVan1 As Boolean = False
    Private Sub visualizarDet()
        If vfVan1 Then
            If BindingSource1.Position = -1 Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            dsAlmacen.Tables("TNroReg").Clear()
            daTabla2.SelectCommand.Parameters("@codReg").Value = lbReg.SelectedValue
            daTabla2.Fill(dsAlmacen, "TNroReg")
            Me.Cursor = Cursors.Default
        End If
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
        btnNuevo.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub txtCan1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCan1.KeyPress, txtCan2.KeyPress
        If e.KeyChar.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Function ValidarCampos() As Boolean
        'Todas las funciones estan creadas en el module ValidarCamposModule.vb
        If ValidarCantMayorCero(txtCan1.Text) Then
            txtCan1.errorProv()
            Return True
        End If

        If ValidarCantMayorCero(txtCan2.Text) Then
            txtCan2.errorProv()
            Return True
        End If

        'Todo OK RAS
        Return False
    End Function

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If ValidarCampos() Then
            Exit Sub
        End If

        If BindingSource1.Find("dia", txtCan1.Text.Trim() & " / " & txtCan2.Text.Trim()) >= 0 Then
            MessageBox.Show("Ya exíste Regimen: " & lbReg.Text.Trim() & Chr(13) & "Cambie de regimen o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCan1.Focus()
            txtCan1.SelectAll()
            Exit Sub
        End If

        'If (CInt(txtCan1.Text) = CInt(BindingSource1.Item(BindingSource1.Position)(2))) And (CInt(txtCan2.Text) = CInt(BindingSource1.Item(BindingSource1.Position)(3))) Then
        '    MessageBox.Show("Ya existe Regimen: " & txtCan1.Text.Trim() & " TIENE QUE SER MAYOR A Nº " & txtCan2.Text.Trim(), nomNegocio, Nothing, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        Dim resp As String = MessageBox.Show("Esta segúro de configurar Regimen Laboral...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        Me.Cursor = Cursors.WaitCursor
        wait.Show()
        'estableciendo una transaccion
        Dim myTrans As SqlTransaction = Cn.BeginTransaction()
        Try
            StatusBarClass.messageBarraEstado("  PROCESANDO DATOS...")
            Me.Refresh()

            'TRegLab
            comandoInsert()
            cmInserTable.Transaction = myTrans
            If cmInserTable.ExecuteNonQuery() < 1 Then
                wait.Close()
                Me.Cursor = Cursors.Default
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
            Dim codReg As Integer = cmInserTable.Parameters("@Identity").Value

            For j As Short = 1 To (CInt(txtCan1.Text) + CInt(txtCan2.Text))
                'TNroReg
                comandoInsert1(j, codReg)
                cmInserTable1.Transaction = myTrans
                If cmInserTable1.ExecuteNonQuery() < 1 Then
                    wait.Close()
                    Me.Cursor = Cursors.Default
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If
            Next

            'confirma la transaccion
            myTrans.Commit()    'con exito RAS

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
            finalMytrans = True
            vfVan1 = False 'Detalle

            'Actualizando el dataSet 
            dsAlmacen.Tables("TRegLab").Clear()
            daTabla1.Fill(dsAlmacen, "TRegLab")

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("codReg", codReg)

            'Actualizando el dataTable
            vfVan1 = True
            visualizarDet()

            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fué guardado con exito...")

            txtCan1.Clear()
            txtCan2.Clear()
            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            If finalMytrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            Else
                myTrans.Rollback()
                MessageBox.Show(f.Message & Chr(13) & "NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Dim cmInserTable As SqlCommand
    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.StoredProcedure
        cmInserTable.CommandText = "PA_InsertRegLab"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@dia1", SqlDbType.Int, 0).Value = txtCan1.Text.Trim()
        cmInserTable.Parameters.Add("@dia2", SqlDbType.Int, 0).Value = txtCan2.Text.Trim()
        'configurando direction output = parametro de solo salida
        cmInserTable.Parameters.Add("@Identity", SqlDbType.Int, 0)
        cmInserTable.Parameters("@Identity").Direction = ParameterDirection.Output
    End Sub

    Dim cmInserTable1 As SqlCommand
    Private Sub comandoInsert1(ByVal nro As Integer, ByVal codReg As Integer)
        cmInserTable1 = New SqlCommand
        cmInserTable1.CommandType = CommandType.Text
        cmInserTable1.CommandText = "insert into TNroReg(nro,codReg) values(@nro,@codReg)"
        cmInserTable1.Connection = Cn
        cmInserTable1.Parameters.Add("@nro", SqlDbType.Int, 0).Value = nro
        cmInserTable1.Parameters.Add("@codReg", SqlDbType.Int, 0).Value = codReg
    End Sub

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(*) from TCotizacion where codPag=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If lbReg.SelectedIndex = -1 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        'If recuperarCount1(lbReg.SelectedValue) > 0 Then
        '    StatusBarClass.messageBarraEstado(" Proceso denegado, forma de pago tiene cotizaciones...")
        '    Exit Sub
        'End If

        Dim resp As Integer = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = 6 Then
            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTRO...")
                Me.Refresh()

                'TNroReg
                comandoDelete()
                cmDeleteTable.ExecuteNonQuery()

                'TRegLab
                comandoDelete1()
                cmDeleteTable1.ExecuteNonQuery()

                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
                finalMytrans = True
                vfVan1 = False 'Detalle

                'Actualizando el dataSet 
                dsAlmacen.Tables("TRegLab").Clear()
                daTabla1.Fill(dsAlmacen, "TRegLab")

                'Actualizando el dataTable
                vfVan1 = True
                visualizarDet()

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fué eliminado con exito...")
                wait.Close()
            Catch
                wait.Close()
                If finalMytrans Then
                    MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    'MessageBox.Show("ACCESO DENEGADO...NO SE ELIMINO EL REGISTRO SELECCIONADO...", nomNegocio, Nothing, MessageBoxIcon.Information)
                    StatusBarClass.messageBarraEstado("  ACCESO DENEGADO...NO SE ELIMINO EL REGISTRO SELECCIONADO...")
                    'Me.Close()
                End If
            End Try
        End If
    End Sub

    Dim cmDeleteTable As SqlCommand
    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TNroReg where codReg=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbReg.SelectedValue
    End Sub

    Dim cmDeleteTable1 As SqlCommand
    Private Sub comandoDelete1()
        cmDeleteTable1 = New SqlCommand
        cmDeleteTable1.CommandType = CommandType.Text
        cmDeleteTable1.CommandText = "delete from TRegLab where codReg=@cod"
        cmDeleteTable1.Connection = Cn
        cmDeleteTable1.Parameters.Add("@cod", SqlDbType.Int, 0).Value = lbReg.SelectedValue
    End Sub
End Class
