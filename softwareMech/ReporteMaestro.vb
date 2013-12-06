Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class ReporteMaestro


#Region "variables"

    ''' <summary>
    ''' Buffer de datos para ventas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend BindingSource0 As New BindingSource

    ''' <summary>
    ''' buffer de datos para detalle de ventas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend BindingSource1 As New BindingSource


    ''' <summary>
    ''' instancia de objeto para customizar Grilla
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend oGrilla As New cConfigFormControls

    ''' <summary>
    ''' Instancia de Objetos para customizar controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend oFControl As New cConfigFormControls

    ''' <summary>
    ''' Instancia de objeto para administrar datos contra la BD
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend oDataManager As New cDataManager

    '-Variables para el control de fechas

    Protected Friend _fechaIni As String

    Protected Friend _fechaFin As String


#End Region

#Region "Métodos"

    ''' <summary>
    ''' Concatena una nro precediendole de ceros
    ''' </summary>
    ''' <param name="nro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function concatenarNro(ByVal nro As Object) As String
        Dim value As String = ""
        If IsNumeric(nro) Then

            Dim contador As Integer = nro.ToString().Count()
            For i As Integer = 1 To 5 - contador
                value &= "0"
            Next
            value &= nro

        End If
        Return value
    End Function

    ''' <summary>
    ''' configura los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Overridable Sub configurarColorControl()

        Me.BackColor = BackColorP

        'Color para los labels del contenedor principal
        oGrilla.configurarColorControl2("Label", Me, ForeColorLabel)
        'Color para el groupBox
        GroupBox2.ForeColor = ForeColorLabel
        'oGrilla.configurarColorControl("GroupBox", Me, ForeColorLabel)
        'Color para botón
        'oGrilla.configurarColorControl2("CheckBox", Me, )
        'chkObra.ForeColor = ForeColorLabel


        btnVer.ForeColor = ForeColorButtom

    End Sub

    ''' <summary>
    ''' customiza la presentación del las columnas de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Overridable Sub ModificandoColumnaDGV()

        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False



    End Sub
   

    


#End Region

#Region "Eventos"

    Private Sub ReporteDocVenta_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()

    End Sub


    Protected Friend Sub rdoFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoFecha.CheckedChanged
        If rdoFecha.Checked Then
            'Ocultando los controles para obra
            Dim ocultar() As String = {cbSerie.Name, cbClient.Name, lblSerie.Name, lblProv.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Obra

            Dim mostrar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name}


            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgv.DataSource = ""
        End If


    End Sub

    Protected Friend Sub rdoClient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoClient.CheckedChanged
        If rdoClient.Checked Then
            'Ocultando los controles para Proveedor
            Dim ocultar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, cbSerie.Name, lblSerie.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Proveedor
            Dim mostrar() As String = {lblProv.Name, cbClient.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgv.DataSource = ""
        End If
    End Sub

    Protected Friend Sub rdoSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSerie.CheckedChanged

        If rdoSerie.Checked Then
            'Ocultando los controles para Serie
            Dim ocultar() As String = {lblDel.Name, lblAl.Name, dtpInicio.Name, dtpFin.Name, lblProv.Name, cbClient.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(ocultar), False)

            'Mostrando los controles para Serie
            Dim mostrar() As String = {cbSerie.Name, lblSerie.Name}
            oFControl.mostrarOcultarControles(Me, New List(Of String)(mostrar), True)

            'limpiando grilla
            dgv.DataSource = ""
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region
End Class