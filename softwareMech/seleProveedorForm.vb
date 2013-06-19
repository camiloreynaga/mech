Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class seleProveedorForm
    'Instanciar una tabla de datos en memoria en ves de dataSet
    Dim dataTable1 As New DataTable()
    Dim bindingSource1 As New BindingSource()

    Private Sub seleProveedorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codIde,razon,ruc,dir,fono,fax,celRpm,email,repres,fono+'  '+fax as fono1 from TIdentidad where estado=1 and idTipId=2 and codIde<>@codP"  ' '2=proveedor
        crearDataAdapterTable(dTable1, sele)
        dTable1.SelectCommand.Parameters.Add("@codP", SqlDbType.Int, 0).Value = vCodIde

        Try
            'llenar la tabla virtual con los dataAdapter
            dTable1.Fill(dataTable1)

            bindingSource1.DataSource = dataTable1
            cbList.DataSource = bindingSource1
            cbList.DisplayMember = "razon"
            cbList.ValueMember = "codIde"
            bindingSource1.Sort = "razon"

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

    Private Sub configurarColorControl()
        Me.BackColor = BackColorP
        Me.lblTitulo.BackColor = TituloBackColorP
        Me.lblTitulo.ForeColor = HeaderForeColorP
        Me.lblDerecha.BackColor = TituloBackColorP
        Me.lblDerecha.ForeColor = HeaderForeColorP
        Me.Text = nomNegocio
        'Label1.ForeColor = ForeColorLabel
        btnProcesar.ForeColor = ForeColorButtom
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla2.Dispose()
        Me.Close()
    End Sub

    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim vfVan As Boolean = False
        For j As Short = 0 To bindingSource1.Count - 1
            If cbList.GetItemChecked(j) Then 'true si esta chekeado
                vfVan = True
                Exit For
            End If
        Next

        If vfVan = False Then
            MessageBox.Show("Proceso denegado, CHEKEE algun proveedor...", nomNegocio, Nothing, MessageBoxIcon.Stop)
            Exit Sub
        End If

        vX1 = 0 'guardara el total de elementos guardados en la matriz
        For j As Short = 0 To bindingSource1.Count - 1
            If cbList.GetItemChecked(j) Then 'True si esta chekeado
                matriz(vX1) = bindingSource1.Item(j)(0) 'codIde
                vX1 = vX1 + 1
            End If
        Next

        Dim resp As String = MessageBox.Show("Esta segúro de aperturar Cotizaciónes en diferentes Proveedores?", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If

        vCod2 = "1"

        Me.Close()
    End Sub
End Class
