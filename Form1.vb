Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form1

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=DESKTOP-FA7RF2S;Initial Catalog=LibraryDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        disp_data()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "insert into Book values('" + txtTitle.Text + "','" + txtAuthor.Text + "','" + txtYearPublished.Text + "','" + txtGenre.Text + "')"
        cmd.ExecuteNonQuery()
        disp_data()
        MsgBox("Record Inserted Succesfully")


    End Sub
    Public Sub disp_data()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Book"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try

            If con.State = ConnectionState.Open Then
                con.Close()

            End If
            con.Open()
            i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Book where ID =" & i & ""
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                txtTitle.Text = dr.GetString(1).ToString()
                txtAuthor.Text = dr.GetString(2).ToString()
                txtYearPublished.Text = dr.GetInt32(3).ToString()
                txtGenre.Text = dr.GetString(4).ToString()
            End While

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update Book set Title = '" + txtTitle.Text + "',Author = '" + txtAuthor.Text + "', YearPublished = '" + txtYearPublished.Text + "',Genre = '" + txtGenre.Text + "'where id =" & i & ""
        cmd.ExecuteNonQuery()
        disp_data()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Book where Title ='" + txtTitle.Text + " '"
        cmd.ExecuteNonQuery()
        disp_data()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Book "
        cmd.ExecuteNonQuery()
        disp_data()
    End Sub
End Class
