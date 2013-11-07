Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008
Imports CrystalDecisions.Shared



Public Class reporteAprobacionDesembolso

    ''' <summary>
    ''' instancia de objeto DataManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim oDataManager As New cDataManager

    ''' <summary>
    ''' estado desembolso
    ''' </summary>
    ''' <remarks></remarks>
    Dim bindingSource As New BindingSource

    ''' <summary>
    ''' instancia de objeto cConfigControls
    ''' </summary>
    ''' <remarks></remarks>
    Dim oGrilla As New cConfigFormControls


#Region "métodos"

    Private Sub ModificandoColumnaDGV()

    End Sub

    Private Sub configurarColorControl()

    End Sub

    Private Function AddCriterioFiltro(ByVal criterio As String, ByVal filtro As String) As String
        If filtro.Length > 0 Then
            filtro &= " and " & criterio
        Else
            filtro &= " " & criterio
        End If
        Return filtro

    End Function

    Private Sub filtrando()

    End Sub

#End Region



End Class
