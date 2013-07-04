Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class MantTipoPagoForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim BindingSource1 As New BindingSource

    ''' <summary>
    ''' variable temporal para Tipo de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfCampo As String

    ''' <summary>
    ''' variable temporal para codigo (nro) de tipo de pago 
    ''' </summary>
    ''' <remarks></remarks>
    Dim vfNro As String


    Dim cmInserTable As SqlCommand


    Dim cmUpdateTable As SqlCommand


    Dim cmDeleteTable As SqlCommand
#Region "métodos"

    ''' <summary>
    ''' customiza los colores para el control 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        Label1.ForeColor = ForeColorLabel
        Label2.ForeColor = ForeColorLabel
        btnNuevo.ForeColor = ForeColorButtom
        btnModificar.ForeColor = ForeColorButtom
        btnEliminar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub


    Private Sub comandoInsert()
        cmInserTable = New SqlCommand
        cmInserTable.CommandType = CommandType.Text
        cmInserTable.CommandText = "insert into TTipoPago(tipoP,nro) values(@cam,@nro)"
        cmInserTable.Connection = Cn
        cmInserTable.Parameters.Add("@cam", SqlDbType.VarChar, 30).Value = txtCam.Text.Trim()
        cmInserTable.Parameters.Add("@nro", SqlDbType.VarChar, 10).Value = txtCodigo.Text.Trim()
    End Sub

    Private Sub comandoUpdate()
        cmUpdateTable = New SqlCommand
        cmUpdateTable.CommandType = CommandType.Text
        cmUpdateTable.CommandText = "update TTipoPago set tipoP=@cam,nro=@nro where codTipP=@cod"
        cmUpdateTable.Connection = Cn
        cmUpdateTable.Parameters.Add("@cam", SqlDbType.VarChar, 30).Value = txtCam.Text.Trim()
        cmUpdateTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0)
        cmUpdateTable.Parameters.Add("@nro", SqlDbType.VarChar, 10).Value = txtCodigo.Text.Trim()
    End Sub

    Private Sub comandoDelete()
        cmDeleteTable = New SqlCommand
        cmDeleteTable.CommandType = CommandType.Text
        cmDeleteTable.CommandText = "delete from TTipoPago where codTipP=@cod"
        cmDeleteTable.Connection = Cn
        cmDeleteTable.Parameters.Add("@cod", SqlDbType.Int, 0).Value = BindingSource1.Item(BindingSource1.Position)(0) '.SelectedValue
    End Sub

    Private Function recuperarCount1(ByVal cod As Integer) As Integer
        Dim cmdCampo As SqlCommand = New SqlCommand
        cmdCampo.CommandType = CommandType.Text
        cmdCampo.CommandText = "select count(codPagD) from TPagoDesembolso where codTipP=" & cod
        cmdCampo.Connection = Cn
        Return cmdCampo.ExecuteScalar
    End Function

    ''' <summary>
    ''' Enlaza datos de grilla con controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub enlazatText()
        If dgModPagos.Rows.Count = 0 Then
            Exit Sub
        Else
            txtCam.Text = BindingSource1.Item(BindingSource1.Position)(1)
            txtCodigo.Text = BindingSource1.Item(BindingSource1.Position)(2)
            txtCodigo.Focus()
        End If


    End Sub

    ''' <summary>
    ''' customiza las columnas de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnasDGV()
        Dim oConf As New cConfigFormControls
        oConf.ConfigGrilla(dgModPagos)

        With dgModPagos

            .Columns("codTipP").Visible = False
            .Columns("tipoP").HeaderText = "Medio de Pago"
            .Columns("nro").HeaderText = "Código"

            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
        dgModPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgModPagos.AllowDrop = False
        dgModPagos.AllowUserToAddRows = False
        dgModPagos.AllowUserToDeleteRows = False
        dgModPagos.ReadOnly = True

    End Sub

#End Region

    Private Sub MantTipoPagoForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub MantTipoPagoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "select codTipP,tipoP,nro from TTipoPago order by tipoP"
        crearDataAdapterTable(daTabla1, sele)

        Try
            'llenar la tabla virtual con los dataAdapter
            daTabla1.Fill(dataTable1)

            BindingSource1.DataSource = dataTable1

            Navigator1.BindingSource = BindingSource1
            dgModPagos.DataSource = BindingSource1
            'dgModPagos.daMember = "tipoP"
            'lbTabla1.ValueMember = "codTipP"

            configurarColorControl()
            ModificarColumnasDGV()


            wait.Close()
        Catch f As Exception
            wait.Close()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        End Try



    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Me.AcceptButton = Me.btnNuevo
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtCam.Text.Trim, 2) Then
            txtCam.errorProv()
            Exit Sub
        End If

        If validaMinCaracOvacio(txtCodigo.Text.Trim, 1) Then
            txtCodigo.errorProv()
            Exit Sub
        End If

        If BindingSource1.Find("tipoP", txtCam.Text.Trim()) >= 0 Then
            MessageBox.Show("Ya existe modalidad de pago: " & txtCam.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCam.Focus()
            txtCam.SelectAll()
            Exit Sub
        End If

        If BindingSource1.Find("nro", txtCodigo.Text.Trim()) >= 0 Then
            MessageBox.Show("Ya existe código de pago: " & txtCodigo.Text.Trim() & Chr(13) & "Cambie de código o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
            txtCodigo.Focus()
            txtCodigo.SelectAll()
            Exit Sub
        End If

        Dim finalMytrans As Boolean = False
        Dim wait As New waitForm
        wait.Show()
        Try
            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()
            vfCampo = txtCam.Text.Trim()
            'llamando al procedimiento k crea el comando insert
            comandoInsert()
            cmInserTable.ExecuteNonQuery()

            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMytrans = True
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("tipoP", vfCampo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS
            StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")
            txtCam.Focus()
            txtCam.SelectAll()
            wait.Close()
        Catch
            wait.Close()
            If finalMytrans Then
                MessageBox.Show("NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            Else
                'myTrans.Rollback()
                MessageBox.Show("NO SE GUARDO EL REGISTRO...PROBLEMAS DE RED...", nomNegocio, Nothing, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If
        End Try
    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If dgModPagos.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a actualizar...")
            Exit Sub
        End If

        Me.AcceptButton = Me.btnModificar
        'validaCampoVacio... creado en el Module ValidarCamposModule.vb, 3=minimo de caractres
        If validaCampoVacioMinCaracNoNumer(txtCam.Text.Trim, 2) Then
            txtCam.errorProv()
            Exit Sub
        End If

        If validaMinCaracOvacio(txtCodigo.Text.Trim, 1) Then
            txtCodigo.errorProv()
            Exit Sub
        End If


        vfCampo = txtCam.Text.Trim()
        If vfCampo.ToUpper() <> txtCam.Text.ToUpper().Trim() Then
            If BindingSource1.Find("tipoP", txtCam.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste modalidad de pago: " & txtCam.Text.Trim() & Chr(13) & "Cambie de nombre o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtCam.Focus()
                txtCam.SelectAll()
                Exit Sub
            End If
        End If

        vfNro = txtCodigo.Text.Trim() '  lbTabla1.Text.Trim()
        If vfNro.ToUpper() <> txtCodigo.Text.ToUpper().Trim() Then
            If BindingSource1.Find("nro", txtCodigo.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya exíste modalidad de pago: " & txtCodigo.Text.Trim() & Chr(13) & "Cambie de código o cancele el proceso...", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtCodigo.Focus()
                txtCodigo.SelectAll()
                Exit Sub
            End If
        End If


        Dim resp As String = MessageBox.Show("Esta segúro de actualizar modalidad de pago... ?" & Chr(13) & "Si actualiza todas sus dependencias heredaran" & Chr(13) & "este nombre...", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            txtCam.Focus()
            Exit Sub
        End If

        Me.Refresh()
        Dim wait As New waitForm
        wait.Show()
        Try
            ' Dim campo As String = txtCam.Text.Trim()
            'llamando al procedimiento k crea el comando Update
            comandoUpdate()
            cmUpdateTable.ExecuteNonQuery()
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON ACTUALIZADOS CON ÉXITO...")
            'Actualizando el dataTable
            dataTable1.Clear()
            daTabla1.Fill(dataTable1)

            'Buscando por nombre de campo y luego pocisionarlo con el indice
            BindingSource1.Position = BindingSource1.Find("tipoP", vfCampo)
            'Clase definida y con miembros shared en la biblioteca ComponentesRAS2005
            StatusBarClass.messageBarraEstado("  Registro fue actualizado con éxito...")


            wait.Close()
        Catch f As Exception
            wait.Close()
            'myTrans.Rollback()
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If dgModPagos.Rows.Count = 0 Then
            StatusBarClass.messageBarraEstado("  No existe registro a eliminar...")
            Exit Sub
        End If

        If recuperarCount1(BindingSource1.Item(BindingSource1.Position)(0)) > 0 Then
            StatusBarClass.messageBarraEstado(" Proceso denegado, modalidad de pago tiene desembolsos...")
            Exit Sub
        End If

        Dim resp As Integer = MessageBox.Show("Esta segúro de eliminar registro?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = 6 Then
            Dim finalMytrans As Boolean = False
            Dim wait As New waitForm
            wait.Show()
            Try
                StatusBarClass.messageBarraEstado("  ELIMINANDO REGISTRO...")
                Me.Refresh()
                'llamando al procedimiento k crea el comando Delete
                comandoDelete()
                cmDeleteTable.ExecuteNonQuery()
                StatusBarClass.messageBarraEstado("  REGISTRO FUE ELIMINADO CON EXITO...")
                finalMytrans = True
                'Actualizando el dataTable
                dataTable1.Clear()
                daTabla1.Fill(dataTable1)

                'Clase definida y con miembros shared en la biblioteca ComponentesRAS
                StatusBarClass.messageBarraEstado("  Registro fue eliminado con éxito...")
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

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub

    Private Sub dgModPagos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgModPagos.CellClick
        enlazatText()

    End Sub

    Private Sub dgModPagos_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgModPagos.CellEnter
        If dgModPagos.CurrentRow.Selected = True Then
            enlazatText()
        End If

    End Sub

    ''' <summary>
    ''' Quita la primera seleccion de la grilla, al cargarse le form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgModPagos_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgModPagos.DataBindingComplete
        DirectCast(sender, DataGridView).ClearSelection()
    End Sub
End Class
