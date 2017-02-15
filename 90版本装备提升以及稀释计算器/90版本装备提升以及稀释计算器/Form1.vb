Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1
    Public 装备属性(17), 自身属性(17), 提升率(17), 怪物属性(6) As Double
    Dim 装备提升系数, 自身提升系数, 总提升率 As Double
    Public 属性名称() As String = {"百分比还是独立", "力智", "双攻独立", "无视锻造", "属强", "暴击率", "黄字", "黄增", "暴伤", "暴增", "白字", "属白", "粉字", "技能攻击力", "百分比双攻独立", "百分比力智", "百分比减防", "无视防御"}
    Public 怪物属性名称() As String = {"防御", "属抗", "无视减伤系数", "破招", "暴击抗性", "精英系数", "减伤率下限"}

    '    Structure 属性
    '    Dim 百分比还是独立 As Boolean  '0为百分比, 1为独立
    '    Dim 力智 As Double
    '    Dim 双攻独立 As Double
    '    Dim 无视锻造 As Double
    '    Dim 属强 As Double
    '    Dim 暴击率 As Double
    '    Dim 黄字 As Double
    '    Dim 黄增 As Double
    '    Dim 暴伤 As Double
    '    Dim 暴增 As Double
    '    Dim 白字 As Double
    '    Dim 属白 As Double
    '    Dim 粉字 As Double
    '    Dim 技能攻击力 As Double
    '    Dim 百分比双攻独立 As Double
    '    Dim 百分比力智 As Double
    '    Dim 百分比减防 As Double
    '    Dim 无视防御 As Double
    '    End Structure

    '    Structure 怪物属性
    '    Dim 防御 As Double
    '    Dim 属抗 As Double
    '    Dim 无视减伤系数 As Double'0.6raid/王者, 0.8尼/勇士
    '    Dim 破招 As Double       '1不破, 1.25破
    '    Dim 暴击抗性 As Double   '默认-0.03
    '    Dim 精英系数 As Double   '1普通, 0.75精英, 0.5领主
    '    Dim 减伤率下限 As Double   '0%普通, 77.57%安徒尼, 81.12%暴走, 89.04%卢克尼
    '    End Structure

    '    Dim 装备, 自身, 提升 As 属性
    '    Dim 怪物 As 怪物属性

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load    '施工中
        '加载指令
        Dim ctrl As Control
        For Each ctrl In Controls   '施工单位位置
            If TypeOf ctrl Is TextBox Then
                AddHandler ctrl.TextChanged, AddressOf textboxupdate
                AddHandler ctrl.Validating, AddressOf textboxvalidation
            ElseIf TypeOf ctrl Is TrackBar Then
                AddHandler CType(ctrl, TrackBar).Scroll, AddressOf trackbarupdate
            End If
        Next
        OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        '初始化属性
        重置数据()
        AddHandler RadioButton1.CheckedChanged, AddressOf 计算

    End Sub


    Public Sub updatedatatoform() '施工完成
        Dim i As Integer
        If 自身属性(0) = 0 Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        For i = 1 To 4
            CType(Controls("装备" & 属性名称(i)), TextBox).Text = Math.Round(装备属性(i), 2).ToString()
            CType(Controls("自身" & 属性名称(i)), TextBox).Text = Math.Round(自身属性(i), 2).ToString()
        Next
        CType(Controls("装备" & 属性名称(5)), TextBox).Text = Math.Round(装备属性(5) * 100.0R, 2).ToString()
        CType(Controls("自身" & 属性名称(5)), TextBox).Text = Math.Round(自身属性(5) * 100.0R, 2).ToString()
        For i = 6 To 17
            Dim a, b As Double
            a = Math.Round(装备属性(i) * 100.0R, 2)
            b = Math.Round(自身属性(i) * 100.0R, 2)
            CType(Controls("装备" & 属性名称(i)), TextBox).Text = a.ToString()
            CType(Controls("自身" & 属性名称(i)), TextBox).Text = b.ToString()
            If a <= 100 And a >= 0 Then
                CType(Controls("装备" & 属性名称(i) & "滑块"), TrackBar).Value = CInt(a)
            End If
            If b <= 100 And b >= 0 Then
                CType(Controls("自身" & 属性名称(i) & "滑块"), TrackBar).Value = CInt(b)
            End If
        Next
        计算()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged    '施工完成
        If RadioButton1.Checked = True Then
            装备属性(0) = 0
            自身属性(0) = 0
            Label2.Text = "基础双攻"
            Label3.Text = "无视"
            Label24.Text = "百分比双攻"
        Else
            装备属性(0) = 1
            自身属性(0) = 1
            Label2.Text = "基础独立"
            Label3.Text = "锻造独立"
            Label24.Text = "百分比独立"
        End If
        '计算()   加这一条会在显示提升率()的Controls("提升" & 属性名称(i)).Text出现null reference exception弄得我一脸懵懂
    End Sub

    Public Sub textboxupdate(sender As Object, e As EventArgs)  '#吐血 施工完成
        Dim i As Integer
        If IsNumeric(CType(sender, TextBox).Text) Then
            If sender.name.substring(0, 2) = "装备" Then
                For i = 1 To 17
                    If 属性名称(i) = sender.name.substring(2) Then
                        If i > 4 Then
                            装备属性(i) = CDbl(CType(sender, TextBox).Text) / 100.0R
                            If i > 5 Then
                                CType(Controls("装备" & 属性名称(i) & "滑块"), TrackBar).Value = Math.Max(0, Math.Min(100, CInt(CType(sender, TextBox).Text)))
                            End If
                        Else
                            装备属性(i) = CDbl(CType(sender, TextBox).Text)
                        End If
                    End If
                Next
            Else
                For i = 1 To 17
                    If 属性名称(i) = sender.name.substring(2) Then
                        If i > 4 Then
                            自身属性(i) = CDbl(CType(sender, TextBox).Text) / 100.0R
                            If i > 5 Then
                                CType(Controls("自身" & 属性名称(i) & "滑块"), TrackBar).Value = Math.Max(0, Math.Min(100, CInt(CType(sender, TextBox).Text)))
                            End If
                        Else
                            自身属性(i) = CDbl(CType(sender, TextBox).Text)
                        End If
                    End If
                Next
            End If
            计算()
        End If
    End Sub

    Public Sub textboxvalidation(sender As Object, e As System.ComponentModel.CancelEventArgs)  '施工完成
        If CType(sender, TextBox).Text = "" Then
            CType(sender, TextBox).Text = "0"
        End If
        If Not IsNumeric(CType(sender, TextBox).Text) Then
            MessageBox.Show("不能输入非数值数据")
            e.Cancel = True
        End If
    End Sub

    Public Sub trackbarupdate(sender As Object, e As EventArgs) '施工完成
        Dim i As Integer
        Dim name As String = sender.name
        If name.Substring(0, 2) = "装备" Then
            For i = 6 To 17
                If name.Substring(2, name.Length - 4) = 属性名称(i) Then
                    装备属性(i) = CDbl(CType(sender, TrackBar).Value) / 100.0R
                End If
            Next
        Else
            For i = 6 To 17
                If name.Substring(2, name.Length - 4) = 属性名称(i) Then
                    自身属性(i) = CDbl(CType(sender, TrackBar).Value) / 100.0R
                End If
            Next
        End If
        Controls(name.Substring(0, name.Length - 2)).Text = CType(sender, TrackBar).Value.ToString()
    End Sub

    Public Sub 计算() '施工完成, 破bug弄了我半天结果用dim 数列()=另一个数列是byref不是byval, 用array.copy才是正解
        Dim i As Integer
        Dim 之后属性(17) As Double
        自身提升系数 = 计算系数(自身属性, 怪物属性)
        之后属性(0) = 自身属性(0)
        For i = 1 To 17
            Dim 单项提升(17) As Double
            Array.Copy(自身属性, 单项提升, 18)
            Select Case i
                Case 1, 2, 3, 4, 5, 7, 9, 10, 11, 12, 14, 15
                    单项提升(i) = 单项提升(i) + 装备属性(i)
                    之后属性(i) = 单项提升(i)
                    提升率(i) = 计算系数(单项提升, 怪物属性) / 自身提升系数 - 1.0R
                Case 6, 8, 17
                    If 装备属性(i) > 单项提升(i) Then
                        单项提升(i) = 装备属性(i)
                        之后属性(i) = 单项提升(i)
                        提升率(i) = 计算系数(单项提升, 怪物属性) / 自身提升系数 - 1.0R
                    Else
                        之后属性(i) = 单项提升(i)
                        提升率(i) = 0
                    End If
                Case 13
                    之后属性(i) = (单项提升(i) + 1) * (装备属性(i) + 1) - 1
                    提升率(i) = 装备属性(i)
                Case 16
                    单项提升(i) = 1 - (1 - 单项提升(i)) * (1 - 装备属性(i))
                    之后属性(i) = 单项提升(i)
                    提升率(i) = 计算系数(单项提升, 怪物属性) / 自身提升系数 - 1.0R
            End Select
        Next
        装备提升系数 = 计算系数(之后属性, 怪物属性)
        总提升率 = 装备提升系数 / 自身提升系数 - 1
        显示提升率()
    End Sub

    Private Sub 重置ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重置ToolStripMenuItem.Click   '这个需要施工?
        重置数据()
    End Sub

    Public Function 计算系数(属性() As Double, 怪物() As Double) As Double '施工完成, 比想象中的容易
        '自身属性 0"百分比还是独立", 1"力智", 2"双攻独立", 3"无视锻造", 4"属强", 5"暴击率", 6"黄字", 7"黄增", 8"暴伤", 9"暴增", 10"白字", 11"属白", 12"粉字", 13"技能攻击力", 14"百分比双攻独立", 15"百分比力智", 16"百分比减防", 17"无视防御"
        '怪物属性 0"防御", 1"属抗", 2"无视减伤", 3"破招", 4"暴击抗性", 5"精英系数", 6"减伤率下限"

        '共享计算
        Dim 实际暴击 As Double = Math.Max(0, Math.Min(1, 属性(5) - 怪物(4)))
        Dim 白字暴击率 As Double = Math.Max(0, Math.Min(1, -怪物(4)))
        Dim 实际暴击提升 As Double = (1 + 属性(8) + 属性(9)) * 1.5 * 实际暴击 + (1 - 实际暴击)
        Dim 实际白爆提升 As Double = 1 + 0.5 * 白字暴击率
        Dim 实际属强 As Double = 属性(4) - 怪物(1)
        Dim 实际属强提升 As Double = 1.05 + 实际属强 / 222.0R
        Dim 实际防御 As Double = 怪物(0) * (1 - 怪物属性(5) * Math.Min(1, 属性(16))) * (1 - Math.Min(1, 属性(17)))
        Dim 实际减伤系数 As Double = Math.Min(18000.0R / (18000.0R + 实际防御), 1 - 怪物(6))
        Dim 实际白字提升 As Double = (1 + 怪物(3) * 实际白爆提升 * (属性(10) + 属性(11) * 实际属强提升))
        Dim 实际力智提升 As Double = (1 + 属性(15)) * 属性(1) / 250.0R + 1.0R
        Dim 提升 As Double = 怪物(3) * (1 + 属性(13)) * (1 + 属性(12)) * (1 + 属性(6) + 属性(7)) * 实际暴击提升 * 实际白字提升

        Select Case 属性(0)
            Case 0  '百分比
                Dim 非无视部分系数 As Double = 实际属强提升 * 实际力智提升 * 实际减伤系数 * 属性(2) * (1 + 属性(14))
                Dim 无视部分系数 As Double = 属性(3) * 怪物(2)
                提升 = 提升 * (非无视部分系数 + 无视部分系数)
            Case 1  '固伤
                Dim 非锻造部分系数 As Double = 属性(2) * (1 + 属性(14))
                Dim 锻造部分系数 As Double = 属性(3)
                提升 = 提升 * 实际属强提升 * 实际力智提升 * 实际减伤系数 * (非锻造部分系数 + 锻造部分系数)
        End Select
        Return 提升
    End Function

    Public Sub 显示提升率()  '施工完成
        Dim i As Integer
        For i = 1 To 17
            Controls("提升" & 属性名称(i)).Text = Math.Round(提升率(i) * 100.0R, 2).ToString() & "%"
        Next
        系数之后.Text = 装备提升系数.ToString("E4")
        系数之前.Text = 自身提升系数.ToString("E4")
        系数总提升.Text = "总提升率: " & Math.Round(总提升率 * 100.0R, 2).ToString() & "%"
    End Sub

    Private Sub 退出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 退出ToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub 加载属性ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 加载属性ToolStripMenuItem.Click   '施工完成, 未测试
        Dim 载入文件 As StreamReader
        OpenFileDialog1.Title = "加载怪物人物属性档案..."
        OpenFileDialog1.Filter = "人物怪物属性档案 (*.rbq)|*.rbq|所有文件 (*.*)|*.*"
        Try
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                载入文件 = File.OpenText(OpenFileDialog1.FileName)
            Else
                Throw New Exception("无法打开文件")
            End If
            If 载入文件.EndOfStream Then
                Throw New Exception("空白文件")
            End If
            Dim strline As String
            Dim status As Integer = 0
            Do Until 载入文件.EndOfStream
                strline = 载入文件.ReadLine()
                If strline = "" Then
                    Continue Do
                End If
                If strline(0) = "'" Then
                    Continue Do
                End If
                If strline = "endofsection" Then
                    status = 0
                End If
                If status <> 0 Then
                    Dim i As Integer
                    Dim strmatch As Match
                    Select Case status
                        Case 1  '人物属性
                            For i = 0 To 17
                                strmatch = Regex.Match(strline, "^" & 属性名称(i) & ":? *(-?\d+(.\d+)?)$")
                                If strmatch.Success Then
                                    自身属性(i) = CDbl(strmatch.Groups(1).Value)
                                End If
                            Next
                        Case 2  '怪物属性
                            For i = 0 To 6
                                strmatch = Regex.Match(strline, "^" & 怪物属性名称(i) & ":? *(-?\d+(.\d+)?)$")
                                If strmatch.Success Then
                                    怪物属性(i) = CDbl(strmatch.Groups(1).Value)
                                End If
                            Next
                    End Select
                End If
                If strline = "人物属性" Then
                    status = 1
                ElseIf strline = "怪物属性" Then
                    status = 2
                End If
            Loop
            载入文件.Close()
            updatedatatoform()
            MessageBox.Show("载入成功!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub 保存属性ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存属性ToolStripMenuItem.Click
        Dim 保存文件 As StreamWriter
        SaveFileDialog1.Title = "保存怪物人物属性档案..."
        SaveFileDialog1.Filter = "人物怪物属性档案 (*.rbq)|*.rbq|所有文件 (*.*)|*.*"
        Try
            Dim i As Integer
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                保存文件 = File.CreateText(SaveFileDialog1.FileName)
                保存文件.WriteLine("人物属性")
                For i = 0 To 17
                    保存文件.WriteLine(属性名称(i) & ": " & 自身属性(i).ToString())
                Next
                保存文件.WriteLine("endofsection")
                保存文件.WriteLine("怪物属性")
                For i = 0 To 6
                    保存文件.WriteLine(怪物属性名称(i) & ": " & 怪物属性(i).ToString())
                Next
                保存文件.WriteLine("endofsection")
                保存文件.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("保存失败")
        End Try
        MessageBox.Show("保存成功!")
    End Sub

    Private Sub 增加预设ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 增加预设ToolStripMenuItem.Click   '施工完成, 未测试
        Dim 载入文件 As StreamReader
        OpenFileDialog1.Title = "添加属性档案..."
        OpenFileDialog1.Filter = "装备属性档案 (*.qbr)|*.qbr|所有文件 (*.*)|*.*"
        Try
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                载入文件 = File.OpenText(OpenFileDialog1.FileName)
            Else
                Throw New Exception("无法打开文件")
            End If
            If 载入文件.EndOfStream Then
                Throw New Exception("空白文件")
            End If
            Dim strline As String
            Do Until 载入文件.EndOfStream
                strline = 载入文件.ReadLine()
                If strline = "" Then
                    Continue Do
                End If
                If strline(0) = "'" Or strline = "装备属性" Then
                    Continue Do
                End If
                If strline = "endofsection" Then
                    Exit Do
                End If
                Dim i As Integer
                Dim strmatch As Match
                For i = 0 To 17
                    strmatch = Regex.Match(strline, "^" & 属性名称(i) & ":? *(-?\d+(.\d+)?)$")
                    If strmatch.Success Then
                        Dim dbl数值 As Double = CDbl(strmatch.Groups(1).Value)
                        Select Case i
                            Case 1, 2, 3, 4, 5, 7, 9, 10, 11, 12, 14, 15
                                装备属性(i) = 装备属性(i) + dbl数值
                            Case 6, 8, 17
                                装备属性(i) = Math.Max(装备属性(i), dbl数值)
                            Case 13
                                装备属性(i) = (1 + 装备属性(i)) * (1 + dbl数值) - 1
                            Case 16
                                装备属性(i) = 1 - (1 - 装备属性(i)) * (1 - dbl数值)
                        End Select
                    End If
                Next
            Loop
            载入文件.Close()
            updatedatatoform()
            MessageBox.Show("载入成功!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub 重置数据()
        Dim i As Integer = 0
        For i = 0 To 17
            装备属性(i) = 0
            提升率(i) = 0
        Next
        自身属性 = {0, 2500, 1600, 1000, 222, 0.97, 0.15, 0, 0.15, 0, 0.15, 0, 0, 0, 0, 0, 0, 0}
        怪物属性 = {189914, 0, 0.6, 1, -0.03, 0.5, 0.8904}
        updatedatatoform()

        '        With 装备
        '        .百分比还是独立 = 0
        '        .力智 = 0
        '        .双攻独立 = 0
        '        .无视锻造 = 0
        '        .属强 = 0
        '        .暴击率 = 0
        '        .黄字 = 0
        '        .黄增 = 0
        '        .暴伤 = 0
        '        .暴增 = 0
        '        .白字 = 0
        '        .属白 = 0
        '        .粉字 = 0
        '        .技能攻击力 = 0
        '        .百分比双攻独立 = 0
        '        .百分比力智 = 0
        '        .百分比减防 = 0
        '        End With
        '        With 自身
        '        .百分比还是独立 = 0
        '        .力智 = 2500
        '        .双攻独立 = 1600
        '        .无视锻造 = 1000
        '        .属强 = 222
        '        .暴击率 = 0.97
        '        .黄字 = 0.15
        '        .黄增 = 0
        '        .暴伤 = 0.15
        '        .暴增 = 0
        '        .白字 = 0.15
        '        .属白 = 0
        '        .粉字 = 0
        '        .技能攻击力 = 0
        '        .百分比双攻独立 = 0
        '        .百分比力智 = 0
        '        .百分比减防 = 0
        '        End With
        '        With 提升
        '        .百分比还是独立 = 0
        '        .力智 = 0
        '        .双攻独立 = 0
        '        .无视锻造 = 0
        '        .属强 = 0
        '        .暴击率 = 0
        '        .黄字 = 0
        '        .黄增 = 0
        '        .暴伤 = 0
        '        .暴增 = 0
        '        .白字 = 0
        '        .属白 = 0
        '        .粉字 = 0
        '        .技能攻击力 = 0
        '        .百分比双攻独立 = 0
        '        .百分比力智 = 0
        '        .百分比减防 = 0
        '        End With
        '       With 怪物
        '       .防御 = 189914
        '       .属抗 = 0
        '       .无视减伤系数 = 0.6
        '       .破招 = 1
        '       .暴击抗性 = -0.03
        '       .精英系数 = 0.5
        '       End With

    End Sub

    Private Sub 怪物属性ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 怪物属性ToolStripMenuItem.Click
        Dim 填写怪物属性 As New 怪物属性编辑
        填写怪物属性.Show()
    End Sub

    Private Sub 基础双攻计算ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 基础双攻计算ToolStripMenuItem.Click
        Dim 计算基础双攻 As New 基础双攻计算
        计算基础双攻.Show()
    End Sub

    Private Sub 基础独立计算ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 基础独立计算ToolStripMenuItem.Click
        Dim 计算基础独立 As New 基础独立计算
        计算基础独立.Show()
    End Sub

    Private Sub 关于ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 关于ToolStripMenuItem.Click
        Dim 于关 As New 关于
        于关.Show()
    End Sub
End Class
