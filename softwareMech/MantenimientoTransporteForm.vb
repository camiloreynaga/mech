Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008



Public Class MantenimientoTransporteForm

#Region "Variables"

    ''' <summary>
    ''' Empresa de tranportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim BindingSource0 As New BindingSource

    ''' <summary>
    ''' command insert para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmInsertEmpresa As SqlCommand

    ''' <summary>
    ''' command Update para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmUpdateEmpresa As SqlCommand

    ''' <summary>
    ''' command Delete para empresa de transportes
    ''' </summary>
    ''' <remarks></remarks>
    Dim cmDelteEmpresa As SqlCommand


    Dim vfNuevo As String = "nuevo"

    Dim vfModificar As String = "modificar"

    Dim vfRazonSocial As String

#End Region


#Region "Métodos"

    ''' <summary>
    ''' Carga los datos Iniciales para el form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosIniciales()
        Me.Cursor = Cursors.WaitCursor

        VerificaConexion()

        Dim wait As New waitForm
        wait.Show()

        Dim sele As String = "Select codET,nombre,ruc,dir,fono,contacto from TEmpTransp"
        crearDataAdapterTable(daTabla1, sele)

        Try

            crearDSAlmacen()
            daTabla1.Fill(dsAlmacen, "TEmpresaTransportes")

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "TEmpresaTransportes"

            dgTransportes.DataSource = BindingSource0



        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default

        End Try



    End Sub



    Private Sub comandoInsertTransporte()

        cmInsertEmpresa = New SqlCommand
        cmInsertEmpresa.CommandType = CommandType.Text
        cmInsertEmpresa.CommandText = "insert TEmpTransp values (@nombre,@ruc,@dir,@fono,@contacto)"
        cmInsertEmpresa.Connection = Cn
        cmInsertEmpresa.Parameters.Add("@nombre", SqlDbType.VarChar, 60).Value = txtRazonSocial.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = txtRuc.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@dir", SqlDbType.VarChar, 120).Value = txtDireccion.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@fono", SqlDbType.VarChar, 60).Value = txtTelefono.Text.Trim()
        cmInsertEmpresa.Parameters.Add("@contacto", SqlDbType.VarChar, 60).Value = txtContacto.Text.Trim

    End Sub


#End Region




    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        If vfNuevo = "nuevo" Then
            vfNuevo = "guardar"
            Me.btnNuevo.Text = "Guardar"

            'metodo para desactivar controles

            'método para limpiar 

            txtRazonSocial.Focus()
            StatusBarClass.messageBarraEstado("")
            Me.AcceptButton = Me.btnNuevo

        Else 'Guardar
            'If ValidarCamposModule() Then
            '    Exit Sub

            'End If


            'valida duplicidad
            If BindingSource0.Find("nombre", txtRazonSocial.Text.Trim()) >= 0 Then
                MessageBox.Show("Ya existe Empresa de Transporte: " & txtRazonSocial.Text.Trim() & Chr(13) & "Cambie de nombre... o cancele el proceso", nomNegocio, Nothing, MessageBoxIcon.Information)
                txtRazonSocial.Focus()
                txtRazonSocial.SelectAll()
                Exit Sub

            End If

            Dim finalMyTrans As Boolean = False
            Dim myTrans As SqlTransaction = Cn.BeginTransaction
            Dim wait As New waitForm
            wait.Show()

            Try

                StatusBarClass.messageBarraEstado(" GUARDANDO DATOS...")
                Me.Refresh()
                vfRazonSocial = txtRazonSocial.Text.Trim

                'obteniendo datos de empresa de transporte
                comandoInsertTransporte()
                cmInsertEmpresa.Transaction = myTrans
                If cmInsertEmpresa.ExecuteNonQuery < 1 Then
                    wait.Close()
                    myTrans.Rollback()
                    MessageBox.Show("Ocurrio un error, por lo tanto no se guardo la información procesada...", nomNegocio, Nothing, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End If

                'Confirma transacción
                myTrans.Commit()
                StatusBarClass.messageBarraEstado("  LOS DATOS FUERON GUARDADOS CON EXITO...")
                finalMyTrans = True

                'actualizando el DataSet
                dsAlmacen.Tables("TEmpresaTransportes").Clear()
                daTabla1.Fill(dsAlmacen, "TEmpresaTransportes")

                btnCancelar.PerformClick()
                'ubicando al ítem agregado en la grilla
                BindingSource0.Position = BindingSource0.Find("nombre", vfRazonSocial)

                'mostrando mensaje de estado
                StatusBarClass.messageBarraEstado("  Registro fue guardado con éxito...")
                wait.Close()

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

        End If


    End Sub

    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

    Private Sub MantenimientoTransporteForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DatosIniciales()

    End Sub
End Class
