Imports System.Data
Imports System.Data.SqlClient
Imports ComponentesSolucion2008

Public Class entradaSalidaAlmacen1Form
    Dim BindingSource1 As New BindingSource

    Private Sub entradaSalidaAlmacen1Form_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        ' Primero capturamos la tecla pulsada, ... 
        If keyData = Keys.F5 Then
            btnExtraer.PerformClick()
        End If

        ' ... y después llamamos al procedimiento de la clase base. 
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub entradaSalidaAlmacen1Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Realizando la conexion con SQL Server - ConexionModule.vb
        'conexion() 'active esta tipoM si desea ejecutar el form independientemente RAS
        'AsignarColoresFormControles()
        VerificaConexion()
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor
        'instanciando los dataAdapter con sus comandos select - DatasetAlmacenModule.vb
       
        Try

            configurarColorControl()
            'txtLinea.DataBindings.Add("Text", BindingSource1, "tipoM")

            wait.Close()
            Me.Cursor = Cursors.Default
        Catch f As Exception
            wait.Close()
            Me.Cursor = Cursors.Default
            MessageBox.Show(f.Message & Chr(13) & "NO SE PUEDE EXTRAER LOS DATOS DE LA BD, LA RED ESTA SATURADA...", nomNegocio, Nothing, MessageBoxIcon.Error)
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
        'Label1.ForeColor = ForeColorLabel
        btnCerrar.ForeColor = ForeColorButtom
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        dgTabla1.Dispose()
        Me.Close()
    End Sub

    Dim vFClear1 As Boolean = False
    Private Sub btnExtraer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtraer.Click
        Dim wait As New waitForm
        wait.Show()
        Me.Cursor = Cursors.WaitCursor

        BindingSource1.RemoveFilter()
        If vFClear1 Then
            dsAlmacen.Tables("VMaterialStock").Clear()
            daVMat.Fill(dsAlmacen, "VMaterialStock")
            colorearFila()
        Else  'Primera ves Click
            Dim sele As String = "select codMat,material,stock,uniBase,preBase,tipoM,hist,estado,codTipM,codUni from VMaterialStock" 'material
            crearDataAdapterTable(daVMat, sele)

            crearDSAlmacen()
            daVMat.Fill(dsAlmacen, "VMaterialStock")

            BindingSource1.DataSource = dsAlmacen
            BindingSource1.DataMember = "VMaterialStock"
            Navigator1.BindingSource = BindingSource1
            dgTabla1.DataSource = BindingSource1
            dgTabla1.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'Seleccionar fila completa
            BindingSource1.Sort = "material"
            ModificarColumnasDGV1()

            vFClear1 = True
            colorearFila()
        End If
        Me.Cursor = Cursors.Default
        wait.Close()

        txtIns.Focus()
        txtIns.SelectAll()
    End Sub

    Private Sub colorearFila()
        For j As Short = 0 To BindingSource1.Count - 1
            If BindingSource1.Item(j)(2) >= 0 Then 'Aprobado
                'dgTabla1.Rows(j).Cells(9).Style.BackColor = Color.Green 'Color.YellowGreen
                dgTabla1.Rows(j).Cells(2).Style.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Next
    End Sub

    Private Sub ModificarColumnasDGV1()
        With dgTabla1
            .Columns(0).HeaderText = "Cod"
            .Columns(0).Width = 40
            .Columns(1).HeaderText = "Descripción Insumo"
            .Columns(1).Width = 460
            .Columns(2).Width = 60
            .Columns(2).HeaderText = "Stock"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 45
            .Columns(3).HeaderText = "Unidad"
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "PrecS/."
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).HeaderText = "Tipo Insumo"
            .Columns(5).Width = 120
            .Columns(6).Width = 1000
            .Columns(6).HeaderText = ""
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
            .RowHeadersDefaultCellStyle.BackColor = HeaderBackColorP
            .RowHeadersDefaultCellStyle.ForeColor = HeaderForeColorP
        End With
    End Sub
End Class
