Public Class 基础独立计算
    Dim 独立 As Double
    Private Sub 基础独立计算_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox4.Text = Form1.自身属性(14).ToString() * 100.0R
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            独立 = (CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) / (1 + CDbl(TextBox3.Text) / 100.0R) / (1 + CDbl(TextBox4.Text) / 100.0R)
            Label6.Text = "独立: " & Math.Round(独立, 2).ToString()
        Catch ex As Exception
            MessageBox.Show("计算出错")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With Form1
            .自身属性(0) = 1
            .自身属性(2) = 独立
            .自身属性(3) = CDbl(TextBox2.Text)
            .自身属性(14) = CDbl(TextBox4.Text) / 100.0R
            .updatedatatoform()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class