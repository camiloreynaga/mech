Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Public Class AcercaDeForm

    Private Sub AcercaDeForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub AcercaDeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Acerca de..."
        StatusBarClass.messageBarraEstado("  RAS")
        lblNegocio.Text = nomNegocio
        Me.lblTitulo.Text = vSTitulo & ". Versión 1.0"
        configurarColorControl()
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
        lblLeft.BackColor = TituloBackColorP
        lblLeft.ForeColor = HeaderForeColorP
        'lblNegocio.ForeColor = ForeColorLabel
        Label5.ForeColor = ForeColorLabel
        btnAceptar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'dgTabla1.Dispose()
        Me.Close()
    End Sub
End Class
