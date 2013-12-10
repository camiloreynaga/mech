Public Class Num2LetEsp
    Private astrConversor(2, 9) As String
    Private mvarNumero As Object ' copia local
    Private mstrMoneda As String ' copia local

    Public Property Numero() As Object
        Get
            Numero = mvarNumero
        End Get
        Set(ByVal Value As Object)
            If IsNumeric(Value) Then
                mvarNumero = Value
            End If
        End Set
    End Property


    Public Property Moneda() As String
        Get
            Moneda = mstrMoneda
        End Get
        Set(ByVal Value As String)
            mstrMoneda = Value
        End Set
    End Property

    Public Function ALetra() As String
        Dim i As Short
        Dim intProceder As Short
        Dim intPosNumero As Short
        Dim intLongNumero As Short
        Dim strNumero As String
        Dim strLetraNumero As String

        ' Establece los valores iniciales para las Variables
        mvarNumero = System.Math.Abs(mvarNumero) 'Deberá ser positivo
        strNumero = CStr(Fix(mvarNumero)) 'A Cadena
        intLongNumero = Len(strNumero)
        intPosNumero = intLongNumero

        ' Cicle por el total de caracteres del número
        For i = 1 To intLongNumero
            intProceder = True
            If (intPosNumero Mod 3) = 1 Then
                If intLongNumero > intPosNumero Then
                    Select Case Mid(strNumero, i - 1, 2)
                        Case "00"
                            If UCase(Right(strLetraNumero, 7)) = "CIENTO " Then
                                strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 7)
                                strLetraNumero = strLetraNumero & "Cien "
                            End If
                        Case "11"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 5)
                            strLetraNumero = strLetraNumero & "Once "
                            intProceder = False
                        Case "12"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 5)
                            strLetraNumero = strLetraNumero & "Doce "
                            intProceder = False
                        Case "13"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 5)
                            strLetraNumero = strLetraNumero & "Trece "
                            intProceder = False
                        Case "14"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 5)
                            strLetraNumero = strLetraNumero & "Catorce "
                            intProceder = False
                        Case "15"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 5)
                            strLetraNumero = strLetraNumero & "Quince "
                            intProceder = False
                        Case "16", "17", "18", "19"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 2)
                            strLetraNumero = strLetraNumero & "ci "
                        Case "21" To "29"
                            strLetraNumero = Left(strLetraNumero, Len(strLetraNumero) - 2)
                            strLetraNumero = strLetraNumero & "i "
                        Case Else
                            If Val(Mid(strNumero, i, 1)) > 0 And Val(Mid(strNumero, i - 1, 1)) > 0 Then
                                strLetraNumero = strLetraNumero & "y "
                            End If
                    End Select
                End If
            End If

            If Val(Mid(strNumero, i, 1)) > 0 And intProceder Then
                strLetraNumero = strLetraNumero & astrConversor(intPosNumero Mod 3, Val(Mid(strNumero, i, 1))) & " "
            End If

            Select Case intPosNumero
                Case 4
                    If Right(strLetraNumero, 9) <> "millones " And Right(strLetraNumero, 13) <> "mil millones " And Right(strLetraNumero, 9) <> "billones " Then
                        'strLetraNumero = strLetraNumero & "mil "
                        If strLetraNumero.Trim() <> "Uno" Then
                            strLetraNumero = strLetraNumero & "mil "
                        Else
                            strLetraNumero = "mil "
                        End If
                    End If
                Case 7
                    If Right(strLetraNumero, 13) <> "mil millones " And Right(strLetraNumero, 9) <> "billones " Then
                        strLetraNumero = strLetraNumero & "millones "
                    End If
                Case 10
                    If Right(strLetraNumero, 9) <> "billones " Then
                        strLetraNumero = strLetraNumero & "millardos "
                    End If
                Case 13
                    strLetraNumero = strLetraNumero & "billones "
                Case Else
            End Select
            intPosNumero = intPosNumero - 1
        Next i

        strLetraNumero = Left(strLetraNumero, 1) & LCase(Mid(strLetraNumero, 2))
        If Numero <> Fix(Numero) Then
            i = (Numero - Fix(Numero)) * 100
            strLetraNumero = strLetraNumero & "con" & Str(i) & "/100 " & mstrMoneda
        Else
            strLetraNumero = strLetraNumero & "con" & " 00/100 " & mstrMoneda
        End If

        If UCase(mstrMoneda) = "NUEVOS SOLES" Then
            'strLetraNumero = "**" & strLetraNumero & " M. N.**"
        End If

        ALetra = strLetraNumero
    End Function

    Private Sub Asigna()
        ' Asigna los Valores al arreglo astrConversor
        astrConversor(0, 1) = "Ciento"
        astrConversor(0, 2) = "Doscientos"
        astrConversor(0, 3) = "Trescientos"
        astrConversor(0, 4) = "Cuatrocientos"
        astrConversor(0, 5) = "Quinientos"
        astrConversor(0, 6) = "Seiscientos"
        astrConversor(0, 7) = "Setecientos"
        astrConversor(0, 8) = "Ochocientos"
        astrConversor(0, 9) = "Novecientos"
        astrConversor(1, 1) = "Uno"
        astrConversor(1, 2) = "Dos"
        astrConversor(1, 3) = "Tres"
        astrConversor(1, 4) = "Cuatro"
        astrConversor(1, 5) = "Cinco"
        astrConversor(1, 6) = "Seis"
        astrConversor(1, 7) = "Siete"
        astrConversor(1, 8) = "Ocho"
        astrConversor(1, 9) = "Nueve"
        astrConversor(2, 1) = "Diez"
        astrConversor(2, 2) = "Veinte"
        astrConversor(2, 3) = "Treinta"
        astrConversor(2, 4) = "Cuarenta"
        astrConversor(2, 5) = "Cincuenta"
        astrConversor(2, 6) = "Sesenta"
        astrConversor(2, 7) = "Setenta"
        astrConversor(2, 8) = "Ochenta"
        astrConversor(2, 9) = "Noventa"
    End Sub

    Private Sub Class_Initialize_Renamed()
        Asigna()
        mstrMoneda = "Nuevos Soles"
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Private Sub Class_Terminate_Renamed()
        System.Array.Clear(astrConversor, 0, astrConversor.Length)
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class
