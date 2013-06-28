Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008


Public Class SeguimientoOrdenDesembolsoForm

#Region "Variables"
    Dim BindingSource0 As New BindingSource
#End Region

#Region "Métodos"


    ''' <summary>
    ''' Metodo que carga los datos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosIniciales()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        wait.Show()
        Dim sele As String = "Select idOP,nroDes,serie,nro,fecDes,est,hist,monto,montoDet,montoDif,estado,codigo,codIde,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,codMon from VOrdenDesebolsoSeguimiento"
        crearDataAdapterTable(daVDetOrden, sele)


        Try
            crearDSAlmacen()
            daVDetOrden.Fill(dsAlmacen, "VDesembolsoSeguimiento")

            BindingSource0.DataSource = dsAlmacen
            BindingSource0.DataMember = "VDesembolsoSeguimiento"
            dgDesembolso.DataSource = BindingSource0



        Catch f As Exception
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Finally
            wait.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub ModificandoColumnasDGV()

        Dim oGrilla As New cConfigFormControls
        oGrilla.ConfigGrilla(dgDesembolso)

        With dgDesembolso
            .Columns("idOP").HeaderText = "Cod"
            .Columns("nroDes").HeaderText = "Nro"
            .Columns("serie").HeaderText = ""

            .Columns("fecDes").HeaderText = "Fecha"
            .Columns("est").HeaderText = "Estado"
            .Columns("monto").HeaderText = "Monto"
            .Columns("montoDet").HeaderText = "Detracción"
            .Columns("codigo").HeaderText = "Obra/Lugar"
            .Columns("codIde").HeaderText = "Proveedor"
            .Columns("banco").HeaderText = "Forma de Pago"
            .Columns("datoReq").HeaderText = "Motivo"
            'columnas no visibles
            .Columns("idOP").Visible = False
            .Columns("nro").Visible = False
            .Columns("hist").Visible = False
            .Columns("montoDif").Visible = False
            .Columns("nroCta").Visible = False
            .Columns("nroDet").Visible = False
            .Columns("factCheck").Visible = False
            .Columns("bolCheck").Visible = False
            .Columns("guiaCheck").Visible = False
            .Columns("vouCheck").Visible = False
            .Columns("vouDCheck").Visible = False
            .Columns("reciCheck").Visible = False
            .Columns("otroCheck").Visible = False
            .Columns("descOtro").Visible = False
            .Columns("nroConfor").Visible = False
            .Columns("fecEnt").Visible = False










        End With





    End Sub

#End Region




    Private Sub SeguimientoOrdenDesembolsoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DatosIniciales()
        ModificandoColumnasDGV()


    End Sub


End Class
