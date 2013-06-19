Module ValidarCamposModule
    Public Function validaCampoVacio(ByVal cadena As String) As Boolean
        If Trim(cadena) = "" Then
            Return True
        Else
            Return False
        End If
    End Function

    'funcion k valida cadena vacia, minimo de caracteres y k no sea numerico
    Public Function validaCampoVacioMinCaracNoNumer(ByVal cadena As String, ByVal nroC As Short) As Boolean
        If Trim(cadena) = "" Then
            Return True
        Else
            If cadena.Length < nroC Or IsNumeric(cadena) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function ValidaNroMayorOigualCero(ByVal nro As String) As Boolean
        If (Not IsNumeric(nro)) Then
            Return True
        Else
            If Not (nro >= 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function ValidaDocIdent(ByVal cadena As String) As Boolean
        If Not ((IsNumeric(Trim(cadena))) And (Len(Trim(cadena)) = 8)) Then
            If Trim(cadena) = "" Then
                Return False
            Else
                Return True
            End If
        End If
        Return False
    End Function

    Public Function ValidaRUC(ByVal cadena As String) As Boolean
        If Not ((IsNumeric(Trim(cadena))) And (Len(Trim(cadena)) = 11)) Then
            If Trim(cadena) = "" Then
                Return False
            Else
                Return True
            End If
        End If
        Return False
    End Function

    'Ruc solo valido
    Public Function ValidaRUC1(ByVal cadena As String) As Boolean
        If Not ((IsNumeric(Trim(cadena))) And (Len(Trim(cadena)) = 11)) Then
            Return True
        End If
        Return False
    End Function

    'funcion k valida minimo de caracteres y puede ser numerico o campo vacio
    Public Function validaMinCaracOvacio(ByVal cadena As String, ByVal nroC As Short) As Boolean
        If Trim(cadena) = "" Then
            Return False
        Else
            If cadena.Length < nroC Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function ValidaContraseñaRepetidaIgual(ByVal cont1 As String, ByVal cont2 As String) As Boolean
        If cont1 <> cont2 Then
            Return True
        End If
        Return False
    End Function

    Public Function ValidaFecha(ByVal fecha As String) As Boolean
        If Not (IsDate(fecha)) Then
            Return True
        End If
        Return False
    End Function

    Public Function ValidaNroDesdeRangoMinimoRangoMaximoEstablecido(ByVal nro As String, ByVal RangoMin As Integer, ByVal RangoMax As Integer) As Boolean
        If (Not IsNumeric(nro)) Then
            Return True
        Else
            If Not ((nro >= RangoMin) And (nro <= RangoMax)) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function ValidarCantMayorCero(ByVal can As String) As Boolean
        If (Not IsNumeric(can)) Then
            Return True
        Else
            If Not (can > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function ValidaNroDesdeRangoMinMaxEstablecido(ByVal nro As String, ByVal RangoMin As Integer, ByVal RangoMax As Integer) As Boolean
        If (Not IsNumeric(nro)) Then
            Return True
        Else
            If Not ((nro >= RangoMin) And (nro <= RangoMax)) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function ValidaFechaMayorXXXX(ByVal fecha As String, ByVal ano As Integer) As Boolean
        Dim vFecha As Date
        vFecha = CDate(fecha)
        If vFecha.Year < ano Then
            Return True
        End If
        Return False
    End Function

    Public Function ValidaFechaMayorFecha(ByVal fecha1 As String, ByVal fecha2 As String) As Boolean
        If CDate(fecha2) < CDate(fecha1) Then
            Return True
        End If
        Return False
    End Function
End Module
