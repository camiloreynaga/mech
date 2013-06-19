Imports System
Imports System.Drawing
Imports System.Windows.Forms


Public Class cColorComboBox
    Inherits ComboBox

    'Colores por defecto
    Private arr_MyColors As String() = {"CadetBlue", "Chocolate", "CornflowerBlue", "DarkOliveGreen"}
    Protected inMargin As Integer
    Protected boxWidth As Integer
    Private c As Color

    'int col_numbers = value.Length;
    'arr_MyColors = new string[col_numbers];
    'for (int i = 0; i < col_numbers; i++)
    '    arr_MyColors[i] = value[i];
    'this.Items.Clear();
    'InitCombo();

    Public Property MyColors() As String()
        Get
            Return arr_MyColors
        End Get
        Set(ByVal value As String())
            Dim col_numbers As Integer = value.Length
            arr_MyColors = New String(col_numbers - 1) {}
            For i As Integer = 0 To col_numbers - 1
                arr_MyColors(i) = value(i)
            Next
            InitCombo()

        End Set
    End Property

    Public Sub New() 'constructor
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        DropDownStyle = ComboBoxStyle.DropDownList
        inMargin = 2
        boxWidth = 3
        BeginUpdate()
        InitCombo()
        EndUpdate()


    End Sub

    Private Sub InitCombo()
        If arr_MyColors.Length = 0 Then
            Return
            For Each _color As String In arr_MyColors
                Try
                    If Color.FromName(_color).IsKnownColor Then
                        Me.Items.Add(_color)
                    End If
                Catch
                    Throw New Exception("invalid Color Name: " + _color)

                End Try
            Next
        Else

        End If
    End Sub

    'Private Sub InitCombo()
    '    'add items 
    '    If arr_MyColors Is Nothing Then
    '        Return
    '    End If
    '    For Each color__1 As String In arr_MyColors
    '        Try
    '            If Color.FromName(color__1).IsKnownColor Then
    '                Me.Items.Add(color__1)
    '            End If
    '        Catch
    '            Throw New Exception("Invalid Color Name: " & color__1)
    '        End Try
    '    Next
    'End Sub

    'Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)

    '    MyBase.OnDrawItem(e)
    '    If (e.State And DrawItemState.ComboBoxEdit) <> DrawItemState.ComboBoxEdit Then
    '        e.DrawBackground()
    '    End If

    '    'Dim g As Graphics = e.Graphics
    '    'If e.Index = -1 Then
    '    '    if index is -1 do nothing
    '    '        Return
    '    'End If
    '    'c = Color.FromName(DirectCast(MyBase.Items(e.Index), String))

    '    Dim s As Object = Color.FromName(DirectCast(MyBase.Items(e.Index), String))

    '    ''the color rectangle
    '    ''g.FillRectangle(new SolidBrush(c),e.Bounds.X

    '    'g.FillRectangle(New SolidBrush(c), e.Bounds.X + Me.inMargin, e.Bounds.Y + Me.inMargin, e.Bounds.Width / Me.boxWidth - 2 * Me.inMargin, e.Bounds.Height - 2 * Me.inMargin)

    '    ''draw border around color rectangle
    '    'g.DrawRectangle(Pens.Black, e.Bounds.X + Me.inMargin, e.Bounds.Y + Me.inMargin, e.Bounds.Width / Me.boxWidth - 2 * Me.inMargin, e.Bounds.Height - 2 * Me.inMargin)
    '    ''draw strings
    '    'g.DrawString(c.Name, e.Font, New SolidBrush(ForeColor), CSng(e.Bounds.Width / Me.boxWidth + 5 * Me.inMargin), CSng(e.Bounds.Y))

    '    If e.Index = -1 Then
    '        Exit Sub
    '    End If

    '    ' Get the name of the current item to be drawn, and make a brush of it 
    '    'Dim s As String = DirectCast(Me.Items(e.Index), String)
    '    Dim b As New SolidBrush(Color.FromName(s))
    '    ' Draw a rectangle and fill it with the current color 
    '    ' and add the name to the right of the color 
    '    e.Graphics.DrawRectangle(Pens.Black, 2, e.Bounds.Top + 1, 20, 11)
    '    e.Graphics.FillRectangle(b, 3, e.Bounds.Top + 2, 19, 10)
    '    e.Graphics.DrawString(s, Me.Font, Brushes.Black, 25, e.Bounds.Top)
    '    b.Dispose()
    'End Sub
    Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        If (e.State And DrawItemState.ComboBoxEdit) <> DrawItemState.ComboBoxEdit Then
            e.DrawBackground()
        End If

        Dim g As Graphics = e.Graphics
        If e.Index = -1 Then
            'if index is -1 do nothing
            Return
        End If
        c = Color.FromName(DirectCast(MyBase.Items(e.Index), String))

        'the color rectangle
        g.FillRectangle(New SolidBrush(c), e.Bounds.X + Me.inMargin, e.Bounds.Y + Me.inMargin, e.Bounds.Width \ Me.boxWidth - 2 * Me.inMargin, e.Bounds.Height - 2 * Me.inMargin)
        'draw border around color rectangle
        g.DrawRectangle(Pens.Black, e.Bounds.X + Me.inMargin, e.Bounds.Y + Me.inMargin, e.Bounds.Width \ Me.boxWidth - 2 * Me.inMargin, e.Bounds.Height - 2 * Me.inMargin)
        'draw strings
        g.DrawString(c.Name, e.Font, New SolidBrush(ForeColor), CSng(e.Bounds.Width \ Me.boxWidth + 5 * Me.inMargin), CSng(e.Bounds.Y))
     

    End Sub



End Class
