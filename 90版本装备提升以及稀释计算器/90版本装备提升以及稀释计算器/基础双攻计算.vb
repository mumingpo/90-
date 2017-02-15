Public Class 基础双攻计算

    Dim 双攻 As Double

    Private Sub 基础双攻计算_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = Form1.自身属性(3).ToString()
        TextBox4.Text = Form1.自身属性(14).ToString() * 100.0R
        TextBox5.Text = (Form1.自身属性(1) * (1 + Form1.自身属性(15))).ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            双攻 = (CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) / (1 + CDbl(TextBox3.Text) / 100.0R) / (1 + CDbl(TextBox4.Text) / 100.0R) / (CDbl(TextBox5.Text) / 250.0R + 1)
            Label6.Text = "双攻: " & Math.Round(双攻, 2).ToString
        Catch ex As Exception
            MessageBox.Show("计算出错")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With Form1
            .自身属性(0) = 0
            .自身属性(2) = 双攻
            .自身属性(3) = CDbl(TextBox2.Text)
            .自身属性(14) = CDbl(TextBox4.Text) / 100.0R
            .updatedatatoform()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class