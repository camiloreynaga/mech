Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports ComponentesSolucion2008
Public Class configuracionColorForm
    Dim bindingSource1 As New BindingSource()

    Private Sub ConfiguracionColorForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Private Sub ConfiguracionColorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta linea si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()   'varFuncPublicasModule
        'instanciando los dataAdapter con sus comandos select - DatasetAmerinkaModule.vb
        Dim sele As String = "select * from TTipoUsu"
        crearDataAdapterTable(daTabla1, sele)

        'Instanciar una tabla de datos en memoria en ves de dataSet
        Dim dataTable1 As New DataTable()

        'llenar la tabla virtual con los dataAdapter
        daTabla1.Fill(dataTable1)

        'Enlazando el componente bindingSource a la tabla virtual
        bindingSource1.DataSource = dataTable1
        dgTabla1.DataSource = bindingSource1
        ModificarColumnasDGV()
        Navigator1.BindingSource = bindingSource1

        configurarColorControl()

        If File.Exists("SavedColor.bin") Then
            Dim myFileStream As Stream = File.OpenRead("SavedColor.bin")
            Dim deserializer As New BinaryFormatter
            'Objeto myColor instanciado en el modulo varFuncPublicasModule
            myColor = CType(deserializer.Deserialize(myFileStream), ColorClass)
            myFileStream.Close()
        End If
        txtCam.Text = myColor.autor

        bcForm = myColor.backColorFormularios
        bcTit = myColor.BackColorTituloPrincipal
        bcTitDG = myColor.backColorTituloDGrid
        fcTxtTit = myColor.foreColorTituloPrincipalDG
        fcTxtLabel = myColor.foreColorLabel
        fcTxtButtom = myColor.foreColorButtom

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ModificarColumnasDGV()
        With dgTabla1
            .Columns(0).Width = 60
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderText = "Tipo"
            .Columns(1).Width = 220
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
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
        btnAceptar.ForeColor = ForeColorButtom
        btnCancelar.ForeColor = ForeColorButtom
        btnFondoForm.ForeColor = ForeColorButtom
        btnTitulo.ForeColor = ForeColorButtom
        btnTituloDG.ForeColor = ForeColorButtom
        btnTxtTitulos.ForeColor = ForeColorButtom
        btnTxtLabel.ForeColor = ForeColorButtom
        btnTxtButtom.ForeColor = ForeColorButtom
        btnRestablecer.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim resp As Short
        resp = MessageBox.Show("Está segúro de guardar nueva configuración?" & Chr(13) & "Para que la nueva configuracion surta efecto," & Chr(13) & "debe de salir de la solución y volver a ejecutarla.", nomNegocio, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp <> 6 Then
            Exit Sub
        End If
        myColor.autor = CType(txtCam.Text, String)
        myColor.backColorFormularios = bcForm
        myColor.BackColorTituloPrincipal = bcTit
        myColor.backColorTituloDGrid = bcTitDG
        myColor.foreColorTituloPrincipalDG = fcTxtTit
        myColor.foreColorLabel = fcTxtLabel
        myColor.foreColorButtom = fcTxtButtom
        Dim myFileStream As Stream = File.Create("SavedColor.bin")
        Dim serializer As New BinaryFormatter
        serializer.Serialize(myFileStream, myColor)
        myFileStream.Close()

        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Dim bcForm As Color
    Private Sub btnFondoForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFondoForm.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.BackColor = ColorDialog1.Color
            bcForm = ColorDialog1.Color
        End If
    End Sub

    Dim bcTit As Color
    Private Sub btnTitulo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTitulo.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.lblTitulo.BackColor = ColorDialog1.Color
            Me.lblDerecha.BackColor = ColorDialog1.Color
            bcTit = ColorDialog1.Color
        End If
    End Sub

    Dim bcTitDG As Color
    Private Sub btnTituloDG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTituloDG.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            dgTabla1.ColumnHeadersDefaultCellStyle.BackColor = ColorDialog1.Color
            dgTabla1.RowHeadersDefaultCellStyle.BackColor = ColorDialog1.Color
            bcTitDG = ColorDialog1.Color
        End If
    End Sub

    Dim fcTxtTit As Color
    Private Sub btnTxtTitulos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTxtTitulos.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            dgTabla1.ColumnHeadersDefaultCellStyle.ForeColor = ColorDialog1.Color
            dgTabla1.RowHeadersDefaultCellStyle.ForeColor = ColorDialog1.Color
            Me.lblTitulo.ForeColor = ColorDialog1.Color
            Me.lblDerecha.ForeColor = ColorDialog1.Color
            fcTxtTit = ColorDialog1.Color
        End If
    End Sub

    Dim fcTxtLabel As Color
    Private Sub btnTxtLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTxtLabel.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Label1.ForeColor = ColorDialog1.Color
            Label2.ForeColor = ColorDialog1.Color
            'GroupBox3.ForeColor = ColorDialog1.Color
            fcTxtLabel = ColorDialog1.Color
        End If
    End Sub

    Dim fcTxtButtom As Color
    Private Sub btnTxtButtom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTxtButtom.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            btnFondoForm.ForeColor = ColorDialog1.Color
            btnAceptar.ForeColor = ColorDialog1.Color
            btnTitulo.ForeColor = ColorDialog1.Color
            btnTituloDG.ForeColor = ColorDialog1.Color
            btnTxtTitulos.ForeColor = ColorDialog1.Color
            btnTxtLabel.ForeColor = ColorDialog1.Color
            btnTxtButtom.ForeColor = ColorDialog1.Color
            btnRestablecer.ForeColor = ColorDialog1.Color
            btnCancelar.ForeColor = ColorDialog1.Color
            fcTxtButtom = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnRestablecer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestablecer.Click
        txtCam.Text = "SOLUCIONES SOFTWARE PERU S.A.C."
        Me.BackColor = Color.DarkOrange
        bcForm = Color.DarkOrange
        Me.lblTitulo.BackColor = Color.Khaki
        Me.lblDerecha.BackColor = Color.Khaki
        bcTit = Color.Khaki
        dgTabla1.ColumnHeadersDefaultCellStyle.BackColor = Color.Khaki
        dgTabla1.RowHeadersDefaultCellStyle.BackColor = Color.Khaki
        bcTitDG = Color.Khaki
        dgTabla1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgTabla1.RowHeadersDefaultCellStyle.ForeColor = Color.White
        Me.lblTitulo.ForeColor = Color.Navy
        Me.lblDerecha.ForeColor = Color.Navy
        fcTxtTit = Color.Navy
        Label1.ForeColor = Color.White
        Label2.ForeColor = Color.White
        'GroupBox2.ForeColor = Color.Olive
        fcTxtLabel = Color.White
        btnFondoForm.ForeColor = Color.Navy
        btnAceptar.ForeColor = Color.Navy
        btnTitulo.ForeColor = Color.Navy
        btnTituloDG.ForeColor = Color.Navy
        btnTxtTitulos.ForeColor = Color.Navy
        btnTxtLabel.ForeColor = Color.Navy
        btnTxtButtom.ForeColor = Color.Navy
        btnRestablecer.ForeColor = Color.Navy
        btnCancelar.ForeColor = Color.Navy
        fcTxtButtom = Color.Navy
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub
End Class
