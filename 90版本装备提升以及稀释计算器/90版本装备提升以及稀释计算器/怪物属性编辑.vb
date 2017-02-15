Public Class 怪物属性编辑

    Dim 怪物属性(6) As Double

    Private Sub 怪物属性编辑_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Array.Copy(Form1.怪物属性, 怪物属性, 7)
        显示数据()
    End Sub

    Private Sub 显示数据()
        TextBox1.Text = 怪物属性(0).ToString()
        TextBox2.Text = 怪物属性(1).ToString()
        TextBox3.Text = (怪物属性(2) * 100.0R).ToString()
        If 怪物属性(3) = 1.25 Then
            CheckBox1.Checked = True
        End If
        TextBox4.Text = (怪物属性(4) * 100.0R).ToString()
        Select Case 怪物属性(5)
            Case 1
                RadioButton1.Checked = True
            Case 0.75
                RadioButton2.Checked = True
            Case 0.5
                RadioButton3.Checked = True
        End Select
        Select Case 怪物属性(6)
            Case 0
                RadioButton4.Checked = True
            Case 0.7757
                RadioButton5.Checked = True
            Case 0.8112
                RadioButton6.Checked = True
            Case 0.8904
                RadioButton7.Checked = True
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            怪物属性(0) = CDbl(TextBox1.Text)
            怪物属性(1) = CDbl(TextBox2.Text)
            怪物属性(2) = CDbl(TextBox3.Text) / 100.0R
            If CheckBox1.Checked Then
                怪物属性(3) = 1.25
            Else
                怪物属性(3) = 1
            End If
            怪物属性(4) = CDbl(TextBox4.Text) / 100.0R
            If RadioButton1.Checked Then
                怪物属性(5) = 1
            ElseIf RadioButton2.Checked Then
                怪物属性(5) = 0.75
            Else
                怪物属性(5) = 0.5
            End If
            If RadioButton4.Checked Then
                怪物属性(6) = 0
            ElseIf RadioButton5.Checked = True Then
                怪物属性(6) = 0.7757
            ElseIf RadioButton6.Checked = True Then
                怪物属性(6) = 0.8112
            Else
                怪物属性(6) = 0.8904
            End If
            Array.Copy(怪物属性, Form1.怪物属性, 7)
            Form1.updatedatatoform()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("存在非数值数据")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = "189914"
        TextBox2.Text = "0"
        TextBox3.Text = "60"
        CheckBox1.Checked = False
        TextBox4.Text = "-3"
        RadioButton3.Checked = True
        RadioButton7.Checked = True
    End Sub
End Class