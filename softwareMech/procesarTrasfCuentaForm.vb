Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class procesarTrasfCuentaForm
    Dim BindingSource1 As New BindingSource
    Dim BindingSource2 As New BindingSource
    Dim BindingSource3 As New BindingSource
    Dim BindingSource4 As New BindingSource

    Private Sub procesarTrasfCuentaForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub procesarTrasfCuentaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
        Dim sele As String = "select idCue,cuenta,saldoBan,simbolo,codBan,banco,nroCue,codMon from VCuentaBanco"
        crearDataAdapterTable(daTabla2, sele)

        Try
            'procedimiento para instanciar el dataSet - DatasetAlmacenModule.vb
            crearDSAlmacen()
            'llenat el dataSet con los dataAdapter
            daTabla2.Fill(dsAlmacen, "VCuentaBanco")

            BindingSource2.DataSource = dsAlmacen
            BindingSource2.DataMember = "VCuentaBanco"
            cbCuenta1.DataSource = BindingSource2
            cbCuenta1.DisplayMember = "cuenta"
            cbCuenta1.ValueMember = "idCue"

            BindingSource3.DataSource = dsAlmacen
            BindingSource3.DataMember = "VCuentaBanco"
            cbCuenta2.DataSource = BindingSource3
            cbCuenta2.DisplayMember = "cuenta"
            cbCuenta2.ValueMember = "idCue"

            configurarColorControl()

            lblMon1.DataBindings.Add("Text", BindingSource2, "simbolo")
            lblMon2.DataBindings.Add("Text", BindingSource2, "simbolo")
            lblMon3.DataBindings.Add("Text", BindingSource3, "simbolo")
            lblMon4.DataBindings.Add("Text", BindingSource3, "simbolo")

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
        Label4.ForeColor = ForeColorLabel
        Label5.ForeColor = ForeColorLabel
        Label6.ForeColor = ForeColorLabel
        Label7.ForeColor = ForeColorLabel
        Label8.ForeColor = ForeColorLabel
        lblMon1.ForeColor = ForeColorLabel
        lblMon2.ForeColor = ForeColorLabel
        lblMon3.ForeColor = ForeColorLabel
        lblMon4.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
        btnPro.ForeColor = ForeColorButtom
        btnVis.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub
End Class
