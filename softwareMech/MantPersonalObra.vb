Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class mantPersonalObra

#Region "Variables"

    ''' <summary>
    ''' instancia de objeto para cofiguración de grilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls

    ''' <summary>
    ''' instancia de objetos para manejo de datos
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' instancia de objetos para la configuración de objetos
    ''' </summary>
    ''' <remarks></remarks>
    Dim oFControl As New cConfigFormControls

    ''' <summary>
    ''' Buffer para personal
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource0 As New BindingSource

    ''' <summary>
    ''' Buffer para Personal por Obra
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource1 As New BindingSource

    Dim cmInsertPersonal As SqlCommand
    Dim cmDeletePersonal As SqlCommand

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Asignando la tecla delete para el quitado de personal obra
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="keyData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        'primera capturamos la tecla Delete
        If keyData = Keys.Delete Then
            btnQuitar.PerformClick()
        End If

        If keyData = Keys.Space Then
            btnAsignar.PerformClick()
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ''' <summary>
    ''' Customiza la presentación de la grilla Personal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModificarColumnaDGVPersonal()
        dgPersonal.ReadOnly = True
        dgPersonal.AllowUserToAddRows = False
        dgPersonal.AllowUserToDeleteRows = False

        With dgPersonal
            .Columns("codPer").Visible = False
            .Columns("dni").HeaderText = "DNI"
            .Columns("dni").Width = 60
            .Columns("nombre").HeaderText = "Nombre"
            .Columns("nombre").Width = 270
            .Columns("sexo").HeaderText = "Sexo"
            .Columns("sexo").Width = 38

        End With

    End Sub
    Private Sub ModificarColumnaDGVPersonalObra()

        dgPersonalObra.ReadOnly = True
        dgPersonalObra.AllowUserToAddRows = False
        dgPersonalObra.AllowUserToDeleteRows = False

        With dgPersonalObra
            .Columns("codPO").Visible = False
            .Columns("codPer").Visible = False
            .Columns("dni").HeaderText = "DNI"
            .Columns("dni").Width = 60
            .Columns("nombre").HeaderText = "Nombre"
            .Columns("nombre").Width = 270
            .Columns("sexo").HeaderText = "Sexo"
            .Columns("sexo").Width = 38
            .Columns("codigo").Visible = False

        End With

    End Sub


    ''' <summary>
    ''' condigurar color de los controles de form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarColorControl()

        Me.BackColor = BackColorP

        oFControl.configurarColorControl2("Label", Me, ForeColorLabel)

        oFControl.configurarColorControl2("Button", Me, ForeColorButtom)



    End Sub


    ''' <summary>
    ''' inserta datos en la tabla 
    ''' </summary>
    ''' <param name="personal">codigo personal</param>
    ''' <param name="obra">codigo de obra</param>
    ''' <remarks></remarks>
    Private Sub cmdInsertPersonalObra(ByVal personal As Integer, ByVal obra As String)

        cmInsertPersonal = New SqlCommand
        cmInsertPersonal.CommandType = CommandType.Text
        cmInsertPersonal.Connection = Cn

        cmInsertPersonal.CommandText = "Insert Into TPersObra values (@codPer,@codigo)"
        cmInsertPersonal.Parameters.Add("@codPer", SqlDbType.Int).Value = personal
        cmInsertPersonal.Parameters.Add("@codigo", SqlDbType.VarChar, 10).Value = obra

    End Sub
    ''' <summary>
    ''' Elimina datos en la tabla
    ''' </summary>
    ''' <param name="cod">codigo</param>
    ''' <remarks></remarks>
    Private Sub cmdDeletePersonalObra(ByVal cod As Integer)
        cmDeletePersonal = New SqlCommand
        cmDeletePersonal.CommandType = CommandType.Text
        cmDeletePersonal.Connection = Cn
        cmDeletePersonal.CommandText = "delete TPersObra WHERE codPO = " & cod

    End Sub
    ''' <summary>
    ''' varifica la existencia de un personal en una obra
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificarPersonalObra(ByVal cod As Integer) As String

        Dim query As String = "select codigo from TPersObra WHERE codPer =" & cod
        Return oDataManager.consultarTabla(query, CommandType.Text)

    End Function

    ''' <summary>
    ''' Carga datos en la grila personal por obra
    ''' </summary>
    ''' <param name="codObra"></param>
    ''' <remarks></remarks>
    Private Sub CargarGrillaPersonalObra(ByVal codObra As String)
        If String.IsNullOrEmpty(codObra) = False Then

            'limpiando grilla
            If dgPersonalObra.RowCount > 0 Then
                dgPersonalObra.DataSource = ""
            End If
            'consultando 
            Dim queryObraPersonal As String = "select codPO,codPer,dni,nombre,sexo,codigo  from vPersonalObra WHERE codigo='" & codObra & "'"
            oDataManager.CargarGrilla(queryObraPersonal, CommandType.Text, dgPersonalObra, bindingSource1)

            bindingSource1.Sort = "nombre"

            'modificar presentación grilla
            ModificarColumnaDGVPersonalObra()

            BindingNavigator2.BindingSource = bindingSource1
        End If



    End Sub

#End Region

#Region "Eventos"



    Private Sub mantPersonalObra_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub

    Private Sub mantPersonalObra_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Cargando pantalla
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        'Cargar combos
        Dim queryObra As String = "select codigo,nombre  from TLugarTrabajo where estado =1 order by nombre asc"
        oDataManager.CargarCombo(queryObra, CommandType.Text, cbObra, "codigo", "nombre")

        'Cargar Grilla Personal
        Dim queryPersonal As String = "select TP.codPer,tp.dni,(TP.nombre +' '+ TP.apePat +' '+TP.apeMat) nombre,tp.sexo  from tpersona TP"
        oDataManager.CargarGrilla(queryPersonal, CommandType.Text, dgPersonal, bindingSource0)

        'Modificar presentación de grilla personal
        ModificarColumnaDGVPersonal()

        'ordenadno binding por nombre
        bindingSource0.Sort = "nombre"

        'Vinculando con navegador
        BindingNavigator1.BindingSource = bindingSource0

        'Dando Color al form 
        configurarColorControl()

        'configurando grilla 


        'Cerrando carga
        Me.Cursor = Cursors.Default
        wait.Close()



    End Sub

    Private Sub cbObra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObra.SelectedIndexChanged

        'validando ingreso de datos

        Try

            If TypeOf cbObra.SelectedValue Is String Then
                'Dim cod As String = cbObra.SelectedValue
                'Dim queryObraPersonal As String = "select codPer,dni,nombre,sexo,codigo  from vPersonalObra WHERE codigo=" & cod
                'oDataManager.CargarGrilla(queryObraPersonal, CommandType.Text, dgPersonalObra, bindingSource1)

                CargarGrillaPersonalObra(cbObra.SelectedValue)

            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtBuscaPersona_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscaPersona.TextChanged
        'haciendo el filtro de nombre
        bindingSource0.Filter = "nombre like '%" & txtBuscaPersona.Text.Trim & "%'"

    End Sub

    Private Sub btnAsignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsignar.Click

        'validar campos
        'Obteniendo el nombre de la persona
        Dim _nombre As String = bindingSource0.Item(bindingSource0.Position)(2)

        'obteniendo el codigo de obra para una persona
        Dim _codObra As String = verificarPersonalObra(bindingSource0.Item(bindingSource0.Position)(0))

        'validando si la persona fue asignado a una obra
        If String.IsNullOrEmpty(_codObra) = False Then

            ' cambiando el combo box
            cbObra.SelectedValue = _codObra

            'mostrando mensajes
            StatusBarClass.messageBarraEstado(_nombre & " está asignado a la obra: " & cbObra.Text)

            MessageBox.Show(_nombre & Chr(13) & " está asignado a la obra: " & Chr(13) & cbObra.Text _
                            , nomNegocio, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'ubicando persona
            bindingSource1.Position = bindingSource1.Find("codPer", bindingSource0.Item(bindingSource0.Position)(0))

            Exit Sub
        Else
            StatusBarClass.messageBarraEstado("")
        End If

        'mensaje 
        Dim resp As String = MessageBox.Show("¿Está seguro de asignar a: " & bindingSource0.Item(bindingSource0.Position)(2) & Chr(13) _
                                            & "a la obra: " & cbObra.Text & " ?" _
                                            , nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then

            Exit Sub
        End If

        Dim finalMyTrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try

            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()


            'insertar datos en TPersonal
            cmdInsertPersonalObra(bindingSource0.Item(bindingSource0.Position)(0), cbObra.SelectedValue)
            cmInsertPersonal.Transaction = myTrans
            If cmInsertPersonal.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transacción
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMyTrans = True

            'refresca datos de grilla
            CargarGrillaPersonalObra(cbObra.SelectedValue)


        Catch f As Exception
            wait.Close()
            If finalMyTrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
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

    Private Sub btnQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitar.Click

        Dim resp As String = MessageBox.Show("¿Está seguro de quitar a: " & bindingSource1.Item(bindingSource1.Position)(3) & Chr(13) _
                                             & "de la obra: " & cbObra.Text & " ?" _
                                             , nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then

            Exit Sub
        End If


        Dim finalMyTrans As Boolean = False
        Dim myTrans As SqlTransaction = Cn.BeginTransaction
        Dim wait As New waitForm
        wait.Show()

        Try

            StatusBarClass.messageBarraEstado("  GUARDANDO DATOS...")
            Me.Refresh()


            'Quitar datos en TPersonalObra
            cmdDeletePersonalObra(bindingSource1.Item(bindingSource1.Position)(0))
            cmDeletePersonal.Transaction = myTrans
            If cmDeletePersonal.ExecuteNonQuery() < 1 Then
                wait.Close()
                myTrans.Rollback()
                MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            'confirma la transacción
            myTrans.Commit()
            StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON ÉXITO...")
            finalMyTrans = True

            'refresca datos de grilla
            CargarGrillaPersonalObra(cbObra.SelectedValue)


        Catch f As Exception
            wait.Close()
            If finalMyTrans Then
                MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
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

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()

    End Sub
#End Region

    Private Sub dgPersonal_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgPersonal.CellClick, dgPersonal.CellEnter

        'validar campos
        Dim _nombre As String = bindingSource0.Item(bindingSource0.Position)(2)

        'obteniendo el codigo de obra para una persona
        Dim _codObra As String = verificarPersonalObra(bindingSource0.Item(bindingSource0.Position)(0))

        'validando si la persona fue asignado a una obra
        If String.IsNullOrEmpty(_codObra) = False Then

            ' cambiando el combo box
            cbObra.SelectedValue = _codObra

            'mostrando mensaje
            StatusBarClass.messageBarraEstado(_nombre & " está asignado a la obra: " & cbObra.Text)
            'ubicando persona
            bindingSource1.Position = bindingSource1.Find("codPer", bindingSource0.Item(bindingSource0.Position)(0))



            Exit Sub
        Else
            StatusBarClass.messageBarraEstado("")
        End If

    End Sub
End Class