Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class registraDocVentaEmpForm
    Dim BindingSource1 As New BindingSource

    Private Sub registraDocVentaEmpForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub registraDocVentaEmpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select codSerS,serie,iniNroDoc,finNroDoc from TSerieSede where estado=1 and codTipDE=70"  '70=Factura
        crearDataAdapterTable(daVSerie, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daVSerie.Fill(dsAlmacen, "TSerieSede")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "TSerieSede"
            cbSerie.DataSource = BindingSource1
            cbSerie.DisplayMember = "serie"
            cbSerie.ValueMember = "codSerS"
            BindingSource1.Sort = "serie"

            configurarColorControl()

            vFVan = True
            parametrosSerieDoc()

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
        Label1.ForeColor = ForeColorLabel
        Label2.ForeColor = ForeColorLabel
        Label3.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnAnula.ForeColor = ForeColorButtom
        btnCierra.ForeColor = ForeColorButtom
        btnImprimir.ForeColor = ForeColorButtom
    End Sub

    Dim vFVan As Boolean = False
    Private Sub parametrosSerieDoc()
        If vFVan Then
            vIniNroDoc = recuperarVariosTSerie(cbSerie.SelectedValue, "iniNroDoc")
            vFinNroDoc = recuperarVariosTSerie(cbSerie.SelectedValue, "finNroDoc")

            recuperarUltimoCodigo(cbSerie.SelectedValue)
        End If
    End Sub

    Private Sub recuperarUltimoCodigo(ByVal codSer As Integer)
        Dim cmdMaxCodigo As SqlCommand = New SqlCommand
        Dim maxCodigo As Object
        cmdMaxCodigo.CommandType = CommandType.Text
        cmdMaxCodigo.CommandText = "select max(nroDoc) from TDocVenta where codSerS=" & codSer
        cmdMaxCodigo.Connection = Cn
        maxCodigo = cmdMaxCodigo.ExecuteScalar
        asignarCodigo(maxCodigo)
    End Sub

    Private Sub asignarCodigo(ByVal max As Object)
        If IsNumeric(max) Then
            max = max + 1
            Select Case CInt(max)
                Case Is < 10
                    txtNro.Text = "0000" & max
                Case 10 To 99
                    txtNro.Text = "000" & max
                Case 100 To 999
                    txtNro.Text = "00" & max
                Case 1000 To 9999
                    txtNro.Text = "0" & max
                Case Is > 9999
                    txtNro.Text = max
            End Select
        Else
            'en caso de k no haya registros en la tabla su valor es Null
            txtNro.Text = vIniNroDoc
        End If
    End Sub

    Private Sub cbSerie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSerie.SelectedIndexChanged
        'vfVan3 = False  'no filtrar detalle
        parametrosSerieDoc()
        'visualizarGuia()
        'vfVan3 = True  'filtrar detalle
        'visualizarDet()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        'dgTabla2.Dispose()
        Me.Close()
    End Sub
End Class
