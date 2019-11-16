Imports Microsoft.Office.Interop
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports AutoItX3Lib
Imports System.ComponentModel
Imports System.Net

Public Class Main
#Region "宣告變數"
    '    Public strCn As String = My.Settings.alConnectionString
    Private dc As New MISDataContext
    Private Aform As New Allform
    Private savepath As String
    Private loadpath As String
    Private aut As New AutoItX3
#End Region

#Region "程式開啟與關閉"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '顯示名單
        Dim frmD As New DashBoard
        Me.Aform.frmDash = frmD
        Me.chkDash.Checked = True
        Me.Text += " V" + My.Application.Info.Version.ToString
        Refresh_data()

        Record_adm("Log in", "")
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            Record_adm("Log out", "")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
#End Region

#Region "按扭區"
    Private Sub BtnOPD_auto_Click(sender As Object, e As EventArgs) Handles btnOPD_auto.Click
#Region "Declaration"
        ' 20190608 created
        ' 20190608 add try, record_adm, record_err
        ' 目的是自動惠入門診資料
        Dim begin_date As String = Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
        Dim end_date As String = begin_date
        Dim strYM As String = ""
#End Region

#Region "ASK for begin_date end_date"
        Try
            '詢問起訖日
            '起訖日需在同一個月
            begin_date = InputBox("請輸入開始日期, 格式YYYYMMDD", "開始日期", begin_date)
            end_date = InputBox("請輸入結束日期, 格式YYYYMMDD", "結束日期", begin_date)
            '檢查格式
            If Not (IsNumeric(begin_date) And IsNumeric(end_date)) Then
                MessageBox.Show("格式不對")
                Exit Sub
            ElseIf Not (begin_date.Length = 8 And end_date.Length = 8) Then
                MessageBox.Show("格式不對")
                Exit Sub
            ElseIf Not (IsDate(begin_date.Substring(0, 4) + "/" + begin_date.Substring(4, 2) + "/" + begin_date.Substring(6, 2)) And
             IsDate(end_date.Substring(0, 4) + "/" + end_date.Substring(4, 2) + "/" + end_date.Substring(6, 2))) Then
                MessageBox.Show("格式不對")
                Exit Sub
            ElseIf end_date.Substring(4, 2) <> begin_date.Substring(4, 2) Then
                MessageBox.Show("起訖需在同一個月")
                Exit Sub
            Else
                strYM = begin_date.Substring(0, 6)
            End If
        Catch ex As Exception
            Record_error(ex.ToString)
        End Try
#End Region

        ' 呼叫製作XML函數
        loadpath = ProduceOPDXML(begin_date, end_date)
        '呼叫匯入門診副程式
        Import_OPD(loadpath)
        Record_adm("add opd", "匯入" + begin_date.ToString + "~" + end_date.ToString + " (Auto)")
    End Sub

    Private Sub BtnPatient_auto_Click(sender As Object, e As EventArgs) Handles btnPatient_auto.Click
#Region "Declaration"
        ' 20190610 created
        ' 目的是自動匯入病患資料
        Dim MyExcel As New Excel.Application
#End Region

#Region "Environment"
        '殺掉所有的EXCEL
        For Each p As Process In Process.GetProcessesByName("EXCEL")
            p.Kill()
        Next
        '營造環境
        Dim isClud As Process() = Process.GetProcessesByName("THCludSuit")  '主目錄
        Dim isCust As Process() = Process.GetProcessesByName("THCustomerFilter")   '處方清單
        If isCust.Length = 0 Then    '如果沒有打開
            '測試"看診清單"是否有打開
            LogINThesis()
            '' 打開"各類特殊 追蹤與紀錄查詢"
            Shell("C:\Program Files (x86)\THESE\杏雲醫療資訊系統\THCustomerFilter.exe", AppWinStyle.MaximizedFocus, False)
            Threading.Thread.Sleep(2000)
        End If
        ' 準備好
        aut.WinActivate("各類特殊 追蹤與紀錄查詢")
        aut.WinWaitActive("各類特殊 追蹤與紀錄查詢")
        aut.ControlClick("各類特殊 追蹤與紀錄查詢", "", "[NAME:chk允許完整筆數呈現]")
        aut.Sleep(300)
        '[NAME:btn病歷號查詢]
        aut.ControlClick("各類特殊 追蹤與紀錄查詢", "", "[NAME:btn病歷號查詢]")
        aut.Sleep(300)
        ' 病歷號查詢
        ' [NAME:TextBox]
        ' [NAME:OKButton]
        aut.ControlSend("病歷號查詢", "", "[NAME:TextBox]", "0000000001~9999999999")
        aut.ControlClick("病歷號查詢", "", "[NAME:OKButton]")
        aut.Sleep(4000)
#End Region

        '20190610 模仿昨天成功的經驗
        '[NAME:btn匯出EXCEL]
        aut.ControlClick("各類特殊 追蹤與紀錄查詢", "", "[NAME:btn匯出EXCEL]")
        'aut.WinWait("[dlgPrintMethodAsk]",, 1000)
        aut.Sleep(1000)
        'aut.ControlClick("[dlgPrintMethodAsk]", "", "[NAME:OK_Button]")
        Do Until aut.WinExists("活頁簿")
            aut.Sleep(100)
        Loop
        'aut.Sleep(10000), 用等的,等10秒大多有效,但不能保證,且也許不用10秒,這樣就浪費了, 應該要個別化
        '好在發現visibility可以有效等到整個檔案製作完成
        MyExcel = GetObject(, "Excel.Application")
        Do Until MyExcel.Visible
            aut.Sleep(100)
        Loop

        Import_Pt(MyExcel)
    End Sub

    Private Sub BtnCDep_Click(sender As Object, e As EventArgs) Handles btnCDep.Click
        '20190606 created, 目的再深化自動化
        '20190608 加好了try, record_adm, record_err
        '目前穩定,已經使用了大約一年
        Me.BackColor = Color.LightPink

#Region "Declaration"
        '20190607 created
        Dim strYM As String = (Year(Now) - 1911).ToString + (Month(Now) + 100).ToString.Substring(1, 2)
#End Region

#Region "ASK for YM"
        Try
            strYM = InputBox("請輸入費用年月, 格式YYYMM, 民國", "詢問", strYM)
            If Not IsNumeric(strYM) Or strYM.Length <> 5 Then
                MessageBox.Show("格式錯誤")
                Exit Sub
            ElseIf CInt(strYM.Substring(3)) < 1 Or CInt(strYM.Substring(3)) > 12 Then
                MessageBox.Show("格式錯誤")
                Exit Sub
            End If
        Catch ex As Exception
            Record_error(ex.ToString)
        End Try
#End Region

        Dim output As DEP_return = Change_DEP(strYM)
        MessageBox.Show("修改了" + output.m.ToString + "筆, 請匯入門診資料")
        Me.BackColor = SystemColors.Control

    End Sub

    Private Sub BtnLabXML_Click(sender As Object, e As EventArgs) Handles btnLabXML.Click
        ' 2019/5/30 開始撰寫, 第一個制做XML檔案的程式
        Me.BackColor = Color.LightPink

#Region "Declaration"
        Dim xdoc As XmlDocument     'TOTFA.xml
        Dim xElement As XmlElement  'patient
        Dim xChildElement As XmlElement
        Dim xElement2 As XmlElement
        Dim xChildElement2 As XmlElement
        Dim savepath As String = ""
        Dim strYM As String = (Year(Now) - 1911).ToString + (Month(Now) + 100).ToString.Substring(1, 2)
        Dim dsMIS As New MISDataContext

#End Region

#Region "ASK for YM"
        strYM = InputBox("請輸入費用年月", "詢問", strYM)
        If Not IsNumeric(strYM) Or strYM.Length <> 5 Then
            MessageBox.Show("格式錯誤")
            Exit Sub
        ElseIf CInt(strYM.Substring(3)) < 1 Or CInt(strYM.Substring(3)) > 12 Then
            MessageBox.Show("格式錯誤")
            Exit Sub
        End If
#End Region

#Region "寫入檔案路徑"
        ' 讀取要輸入的位置
        ' 從杏翔病患資料輸入, 只有一種xml格式
        ' Xml格式的index=2
        Me.SaveFileDialog1.FilterIndex = 2
        Me.SaveFileDialog1.FileName = "TOTFA.xml"
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            savepath = Me.SaveFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

        Try
            Dim query = From pt In dsMIS.sp_get_hdata(strYM).AsEnumerable
                        Select pt
            '建立一個 XmlDocument 物件並加入 Declaration
            xdoc = New XmlDocument
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", "big5", ""))
            '建立根節點物件並加入 XmlDocument 中 (第0層)
            xElement = xdoc.CreateElement("patient")
            xChildElement = xElement '這個舉動毫無意義,但可以避免錯誤訊息
            xdoc.AppendChild(xElement)
            '在sections下寫入一個節點名稱為section(第1層)
            For Each p In query
                If p.r1 = 1 Then
                    xChildElement = xdoc.CreateElement("hdata")
                    xElement.AppendChild(xChildElement)     'patient下加個hdata
                    '第2層節點
                    xElement2 = xdoc.CreateElement("h1")
                    xElement2.InnerText = p.h1               'h1 報告類別, 1:檢體檢驗報告
                    xChildElement.AppendChild(xElement2)     'hdata下加個h1
                    xElement2 = xdoc.CreateElement("h2")
                    xElement2.InnerText = p.h2              'h2 醫事機構代碼
                    xChildElement.AppendChild(xElement2)    'hdata下加個h2
                    xElement2 = xdoc.CreateElement("h3")
                    xElement2.InnerText = p.h3              'h3 醫事類別, 11:門診西醫診所
                    xChildElement.AppendChild(xElement2)    'hdata下加個h3
                    xElement2 = xdoc.CreateElement("h4")
                    xElement2.InnerText = p.h4             'h4 費用年月
                    xChildElement.AppendChild(xElement2)    'hdata下加個h4
                    xElement2 = xdoc.CreateElement("h5")
                    xElement2.InnerText = p.h5               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h6")
                    xElement2.InnerText = p.h6               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h7")
                    xElement2.InnerText = p.h7               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h8")
                    xElement2.InnerText = p.h8               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h9")
                    xElement2.InnerText = p.h9               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h10")
                    xElement2.InnerText = p.h10               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h11")
                    xElement2.InnerText = p.h11               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h17")
                    xElement2.InnerText = p.h17               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h18")
                    xElement2.InnerText = p.h18               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h19")
                    xElement2.InnerText = p.h19               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h20")
                    xElement2.InnerText = p.h20               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h22")
                    xElement2.InnerText = p.h22               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h23")
                    xElement2.InnerText = p.h23               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h25")
                    xElement2.InnerText = p.h25               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    xElement2 = xdoc.CreateElement("h26")
                    xElement2.InnerText = p.h26               'h5 申報類別, 1:送核
                    xChildElement.AppendChild(xElement2)    'hdata下加個h5
                    '第3層節點
                    xChildElement2 = xdoc.CreateElement("rdata") 'rdata
                    xChildElement.AppendChild(xChildElement2)   'under hdata add rdata 
                    xElement2 = xdoc.CreateElement("r1")
                    xElement2.InnerText = p.r1
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r2")
                    xElement2.InnerText = p.r2
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r3")
                    xElement2.InnerText = p.r3
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r4")
                    xElement2.InnerText = p.r4
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r5")
                    xElement2.InnerText = p.r5
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r6-1")
                    xElement2.InnerText = p.r6a
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r6-2")
                    xElement2.InnerText = p.r6b
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r9")
                    xElement2.InnerText = p.r9
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r10")
                    xElement2.InnerText = p.r10
                    xChildElement2.AppendChild(xElement2)
                Else
                    '第3層節點
                    xChildElement2 = xdoc.CreateElement("rdata") 'rdata
                    xChildElement.AppendChild(xChildElement2)   'under hdata add rdata 
                    xElement2 = xdoc.CreateElement("r1")
                    xElement2.InnerText = p.r1
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r2")
                    xElement2.InnerText = p.r2
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r3")
                    xElement2.InnerText = p.r3
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r4")
                    xElement2.InnerText = p.r4
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r5")
                    xElement2.InnerText = p.r5
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r6-1")
                    xElement2.InnerText = p.r6a
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r6-2")
                    xElement2.InnerText = p.r6b
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r9")
                    xElement2.InnerText = p.r9
                    xChildElement2.AppendChild(xElement2)
                    xElement2 = xdoc.CreateElement("r10")
                    xElement2.InnerText = p.r10
                    xChildElement2.AppendChild(xElement2)
                End If
            Next
            xdoc.Save(savepath)
        Catch ex As Exception
            MessageBox.Show(ex.Message & System.Environment.NewLine & ex.StackTrace)
        End Try

        Me.BackColor = SystemColors.Control
    End Sub

    Private Sub BtnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
#Region "Declaration"
        ' 2019/5/27 打算一周內完成上傳檢驗報告壯舉, 第一個關卡是匯入申報上傳資料, 第二是醫令與檢驗結果配對,第三是XML輸出技術
        Dim dc As New MISDataContext    '用來寫入SQL server
#End Region

#Region "讀取檔案路徑"
        ' 讀取要輸入的位置
        Dim loadpath As String = ""
        ' 從杏翔病患資料輸入, 只有一種xml格式
        ' Xml格式的index=2
        Me.OpenFileDialog1.FilterIndex = 2
        Me.OpenFileDialog1.FileName = ""
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            loadpath = Me.OpenFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

#Region "外表"
        Me.BackColor = Color.LightPink
        Me.ProgressBar1.Visible = True
#End Region

#Region "進行讀取資料"
        '2019/5/27 讀取申報檔
        If Path.GetExtension(loadpath).ToLower = ".xml" Then
            Dim xdoc As XmlDocument = New XmlDocument
            Dim xOutpatient As XmlNode  'root of the xml
            Dim xTDATA As XmlNodeList   '用來放tDATA
            Dim xDDATA As XmlNodeList   '用來放dDATA
            Dim xNodeTemp As XmlNode    '臨時的xml node操作
            Dim keyT3 As String = ""    '當key值, 費用年月
            Dim keyD1 As String = ""    '當key值, 案件分類
            Dim keyD2 As String = ""    '當key值,流水編號

            Try
                '讀取XML
                ' 20190615 revisited: 原本想說建立防呆機制, 結果一看才發現有天然的防呆機制,就是primary key的設置, 三個表都有,有重複值自然就不會讀了
                xdoc.Load(loadpath)
                ' root node就是outpatient, outpatient下面就是兩個node: tdata, 
                xOutpatient = CType(xdoc.DocumentElement, XmlNode)
                '選擇section
                xTDATA = xOutpatient.SelectNodes("tdata")   '這應該只有一個
                xDDATA = xOutpatient.SelectNodes("ddata")   '這應該有很多個
                Me.ProgressBar1.Minimum = 1
                Me.ProgressBar1.Maximum = xDDATA.Count
                'TDATA只有一個item
                xNodeTemp = xTDATA.Item(0)
                '這個唯一的item下面有42個child node/item, 就是總表了
                '20190527 我已經搞懂總表了,可以寫入SQL了
                '以下寫入SQL
#Region "總表重複12次, xml_tdata"
                Dim newT As New xml_tdata   '宣告新的一行
                With newT
                    .t1 = xNodeTemp.SelectSingleNode("t1").InnerText
                    .t2 = xNodeTemp.SelectSingleNode("t2").InnerText
                    .t3 = xNodeTemp.SelectSingleNode("t3").InnerText
                    keyT3 = xNodeTemp.SelectSingleNode("t3").InnerText
                    .t4 = xNodeTemp.SelectSingleNode("t4").InnerText
                    .t5 = xNodeTemp.SelectSingleNode("t5").InnerText
                    .t6 = xNodeTemp.SelectSingleNode("t6").InnerText
                    .t37 = xNodeTemp.SelectSingleNode("t37").InnerText
                    .t38 = xNodeTemp.SelectSingleNode("t38").InnerText
                    .t39 = xNodeTemp.SelectSingleNode("t39").InnerText
                    .t40 = xNodeTemp.SelectSingleNode("t40").InnerText
                    .t41 = xNodeTemp.SelectSingleNode("t41").InnerText
                    .t42 = xNodeTemp.SelectSingleNode("t42").InnerText
                End With
                dc.xml_tdata.InsertOnSubmit(newT)
                dc.SubmitChanges()
                '20190527 完成
#End Region
                '=====================================================================================
                '2019/5/27 完成的
                'DDATA有很多個item
                For intI = 0 To xDDATA.Count - 1
                    Me.ProgressBar1.Value = intI + 1
                    '取得ddata, 下面應有dhead, dbody兩個node, dhead下有d1, d2, dbody下有30欄位
                    '------------> as ddata
                    Dim newD As New xml_ddata   '宣告新的一行
                    With newD
#Region "xml_ddata"
                        .t3 = keyT3
                        '取得節點[dhead]
                        xNodeTemp = xDDATA.Item(intI).SelectSingleNode("dhead")
                        .d1 = xNodeTemp.SelectSingleNode("d1").InnerText
                        keyD1 = xNodeTemp.SelectSingleNode("d1").InnerText
                        .d2 = xNodeTemp.SelectSingleNode("d2").InnerText
                        keyD2 = xNodeTemp.SelectSingleNode("d2").InnerText
                        '取得節點[dbody]
                        xNodeTemp = xDDATA.Item(intI).SelectSingleNode("dbody")
                        .d3 = xNodeTemp.SelectSingleNode("d3").InnerText
                        If xNodeTemp.SelectNodes("d4").Count <> 0 Then
                            .d4 = xNodeTemp.SelectSingleNode("d4").InnerText
                        End If
                        .d8 = xNodeTemp.SelectSingleNode("d8").InnerText
                        .d9 = xNodeTemp.SelectSingleNode("d9").InnerText
                        .d11 = xNodeTemp.SelectSingleNode("d11").InnerText
                        If xNodeTemp.SelectNodes("d14").Count <> 0 Then
                            .d14 = xNodeTemp.SelectSingleNode("d14").InnerText
                        End If
                        .d15 = xNodeTemp.SelectSingleNode("d15").InnerText
                        If xNodeTemp.SelectNodes("d16").Count <> 0 Then
                            .d16 = xNodeTemp.SelectSingleNode("d16").InnerText
                        End If
                        .d17 = xNodeTemp.SelectSingleNode("d17").InnerText
                        .d18 = xNodeTemp.SelectSingleNode("d18").InnerText
                        If xNodeTemp.SelectNodes("d19").Count <> 0 Then
                            .d19 = xNodeTemp.SelectSingleNode("d19").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d20").Count <> 0 Then
                            .d20 = xNodeTemp.SelectSingleNode("d20").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d21").Count <> 0 Then
                            .d21 = xNodeTemp.SelectSingleNode("d21").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d22").Count <> 0 Then
                            .d22 = xNodeTemp.SelectSingleNode("d22").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d23").Count <> 0 Then
                            .d23 = xNodeTemp.SelectSingleNode("d23").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d27").Count <> 0 Then
                            .d27 = xNodeTemp.SelectSingleNode("d27").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d28").Count <> 0 Then
                            .d28 = xNodeTemp.SelectSingleNode("d28").InnerText
                        End If
                        .d29 = xNodeTemp.SelectSingleNode("d29").InnerText
                        .d30 = xNodeTemp.SelectSingleNode("d30").InnerText
                        If xNodeTemp.SelectNodes("d32").Count <> 0 Then
                            .d32 = xNodeTemp.SelectSingleNode("d32").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d33").Count <> 0 Then
                            .d33 = xNodeTemp.SelectSingleNode("d33").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d34").Count <> 0 Then
                            .d34 = xNodeTemp.SelectSingleNode("d34").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d35").Count <> 0 Then
                            .d35 = xNodeTemp.SelectSingleNode("d35").InnerText
                        End If
                        If xNodeTemp.SelectNodes("d36").Count <> 0 Then
                            .d36 = xNodeTemp.SelectSingleNode("d36").InnerText
                        End If
                        .d39 = xNodeTemp.SelectSingleNode("d39").InnerText
                        .d40 = xNodeTemp.SelectSingleNode("d40").InnerText
                        .d41 = xNodeTemp.SelectSingleNode("d41").InnerText
                        If xNodeTemp.SelectNodes("d49").Count <> 0 Then
                            .d49 = xNodeTemp.SelectSingleNode("d49").InnerText
                        End If
#End Region
                    End With
                    dc.xml_ddata.InsertOnSubmit(newD)
                    dc.SubmitChanges()
                    '取得[dbody]下的節點[pdata],這可能有很多個,也可能沒有半個, 要有個if, 要有個迴圈for next
                    If xNodeTemp.SelectNodes("pdata").Count <> 0 Then
                        Dim xPDATA As XmlNodeList   '用來放pDATA
                        xPDATA = xNodeTemp.SelectNodes("pdata")
                        For intJ As Integer = 0 To xPDATA.Count - 1
                            Dim newP As New xml_pdata
                            With newP
                                .t3 = keyT3
                                newP.d1 = keyD1
                                newP.d2 = keyD2
                                If xPDATA.Item(intJ).SelectNodes("p1").Count <> 0 Then
                                    If IsNumeric(xPDATA.Item(intJ).SelectSingleNode("p1").InnerText) Then
                                        .p1 = xPDATA.Item(intJ).SelectSingleNode("p1").InnerText
                                    End If
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p2").Count <> 0 Then
                                    .p2 = xPDATA.Item(intJ).SelectSingleNode("p2").InnerText
                                End If
                                .p3 = xPDATA.Item(intJ).SelectSingleNode("p3").InnerText
                                .p4 = xPDATA.Item(intJ).SelectSingleNode("p4").InnerText
                                If xPDATA.Item(intJ).SelectNodes("p5").Count <> 0 Then
                                    If IsNumeric(xPDATA.Item(intJ).SelectSingleNode("p5").InnerText) Then
                                        .p5 = xPDATA.Item(intJ).SelectSingleNode("p5").InnerText
                                    End If
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p6").Count <> 0 Then
                                    .p6 = xPDATA.Item(intJ).SelectSingleNode("p6").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p7").Count <> 0 Then
                                    .p7 = xPDATA.Item(intJ).SelectSingleNode("p7").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p8").Count <> 0 Then
                                    .p8 = xPDATA.Item(intJ).SelectSingleNode("p8").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p9").Count <> 0 Then
                                    .p9 = xPDATA.Item(intJ).SelectSingleNode("p9").InnerText
                                End If
                                .p10 = xPDATA.Item(intJ).SelectSingleNode("p10").InnerText
                                If xPDATA.Item(intJ).SelectNodes("p2").Count <> 0 Then
                                    .p2 = xPDATA.Item(intJ).SelectSingleNode("p2").InnerText
                                End If
                                .p12 = xPDATA.Item(intJ).SelectSingleNode("p12").InnerText
                                .p13 = xPDATA.Item(intJ).SelectSingleNode("p13").InnerText
                                If xPDATA.Item(intJ).SelectNodes("p14").Count <> 0 Then
                                    .p14 = xPDATA.Item(intJ).SelectSingleNode("p14").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p15").Count <> 0 Then
                                    .p15 = xPDATA.Item(intJ).SelectSingleNode("p15").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p16").Count <> 0 Then
                                    .p16 = xPDATA.Item(intJ).SelectSingleNode("p16").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p17").Count <> 0 Then
                                    .p17 = xPDATA.Item(intJ).SelectSingleNode("p17").InnerText
                                End If
                                If xPDATA.Item(intJ).SelectNodes("p20").Count <> 0 Then
                                    .p20 = xPDATA.Item(intJ).SelectSingleNode("p20").InnerText
                                End If
                            End With
                            dc.xml_pdata.InsertOnSubmit(newP)
                            dc.SubmitChanges()
                        Next
                    End If
                Next
            Catch ex As Exception
                Record_error(ex.Message)
            End Try
        Else
            Exit Sub
        End If
#End Region

#Region "進行配對"
        '20190615 連結tbl_opd
        Dim q = From cs In dc.sp_match_xml().AsEnumerable Select cs
        Dim n As String = q(0).rows_affected.ToString
        Record_adm("健保上傳XML檔配對", n + "筆配對成功")
        MessageBox.Show("健保上傳XML檔配對: " + n + "筆配對成功")
#End Region

#Region "外表復原"
        Me.BackColor = SystemColors.Control
        Me.ProgressBar1.Visible = False
#End Region

    End Sub

    Private Sub BtnOPD_Click(sender As Object, e As EventArgs) Handles btnOPD.Click
#Region "讀取檔案路徑"
        ' 讀取要輸入的位置
        Dim loadpath As String = ""
        ' 從杏翔病患資料輸入, 只有一種xml格式
        ' Xml格式的index=2
        Me.OpenFileDialog1.FilterIndex = 2
        Me.OpenFileDialog1.FileName = ""
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            loadpath = Me.OpenFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

        Import_OPD(loadpath)
        Record_adm("add opd", "匯入門診檔案 Manual")
    End Sub

    Private Sub BtnPatient_Click(sender As Object, e As EventArgs) Handles btnPatient.Click
#Region "讀取檔案路徑"
        ' 讀取要輸入的位置
        Dim loadpath As String = ""
        ' 從杏翔病患資料輸入, 只有一種excel格式
        ' Excel格式的index=1
        Me.OpenFileDialog1.FilterIndex = 1
        Me.OpenFileDialog1.FileName = ""
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            loadpath = Me.OpenFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

        Dim app As Excel.Application = New Excel.Application
        Dim wb As Excel.Workbook = app.Workbooks.Open(loadpath)
        Import_Pt(app)
    End Sub

    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
#Region "讀取檔案路徑"
        ' 讀取要輸入的位置
        Dim loadpath As String = ""
        ' 從杏翔計價標準檔輸入, 只有一種excel格式
        ' Excel格式的index=1
        Me.OpenFileDialog1.FilterIndex = 1
        Me.OpenFileDialog1.FileName = ""
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            loadpath = Me.OpenFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

#Region "進行讀取資料"
        Dim app As Excel.Application = New Excel.Application
        Dim wb As Excel.Workbook = app.Workbooks.Open(loadpath)
        Import_Order(app)
#End Region
    End Sub

    Private Sub BtnLab_Click(sender As Object, e As EventArgs) Handles btnLab.Click
#Region "讀取檔案路徑"
        ' 讀取要輸入的位置
        Dim loadpath As String = ""
        ' 從常誠資料輸入, 有一種excel格式, text格式
        ' Excel, text格式的index=3
        Me.OpenFileDialog1.FilterIndex = 3
        Me.OpenFileDialog1.FileName = ""
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            loadpath = Me.OpenFileDialog1.FileName
        Else
            ' 取消, 什麼也沒有做
            Exit Sub
        End If
#End Region

#Region "外表"
        Me.BackColor = Color.LightPink
        Me.ProgressBar1.Visible = True
#End Region

#Region "進行讀取資料"
        If Path.GetExtension(loadpath).ToLower = ".xls" Or
            Path.GetExtension(loadpath).ToLower = ".xlsx" Then
#Region "xls"
            '如果是xls檔
            '宣告
            Dim app As Excel.Application = New Excel.Application
            Dim ws As Excel.Worksheet
            Dim wb As Excel.Workbook

            Try '嘗試打開xls檔
                wb = app.Workbooks.Open(loadpath)
                ws = wb.ActiveSheet

                '檢查檔案格式
                '檢查第一行的標題,看看是否符合
                Dim strT() As String = {"", "身份證字號", "病患姓名", "出生日期", "性別", "原病歷號碼", "原就醫日期",
                                        "檢驗單工號", "開單日(收件日)", "開單時間", "檢驗日期", "報告日期", "報告時間",
                                        "就醫序號"}
                For i = 1 To strT.Length
                    If ws.Cells(1, i).value <> strT(i - 1) Then
                        ' 寫入Error Log
                        Record_error(" 輸入的常誠檢驗資料檔案格式不對")
                        Exit Try
                    End If
                Next

                '通過測試
                Record_adm("Lab file format", "correct")

                Dim totalN As Integer = ws.UsedRange.Rows.Count - 1
                Me.ProgressBar1.Minimum = 1
                Me.ProgressBar1.Maximum = totalN

                ' 要有迴路, 來讀一行一行的xls, 能夠判斷
                ' 檔案結構複雜, 不好用for next, 應該用while
                ' 一次性讀檔, 不用update
                ' totalN+1 是excel檔的總rows數
                Dim ind As Integer = 1   ' index, 從第二行開始
                Dim strUid As String = ""
                Dim strLid As String = ""
                Dim dL05 As Date
                Dim dc As New MISDataContext
                While ind <= totalN
                    Me.ProgressBar1.Value = ind
                    ind += 1    'next line
                    If ws.Cells(ind, 1).value = "***" Then
                        ' 檢驗單工號, 第8欄, 檢查是否空白, 空白不行
                        If ws.Cells(ind, 8).value.ToString.Length = 0 Then
                            strLid = ""
                            Record_error("輸入檢驗資料時,缺少檢驗單工號")
                            Continue While ' continue while就可以跳下一行
                        Else
                            ' 檢查檢驗單工號是否存在,如果有就不要存了
                            strLid = RTrim(ws.Cells(ind, 8).value)
                            Dim La = From l In dc.tbl_lab Where l.lid = strLid Select l ' a query for searching duplicates
                            If La.Count <> 0 Then '如果有重複,不但這行不要讀了, 連帶後面也都不要讀(strLid=""), 直到下次"***"
                                strLid = ""
                                Continue While '跳下一行
                            End If
                        End If
                        ' 身分證字號, 第2欄, 檢查是否空白, 空白不行
                        If ws.Cells(ind, 2).value.ToString.Length = 0 Then
                            Record_error("輸入檢驗資料時,缺少身分證字號")
                            Continue While
                        Else
                            strUid = RTrim(ws.Cells(ind, 2).value)
                        End If
                        ' 報告日期, 第12欄, 檢查是否空白, 空白不行
                        If IsDate(ws.Cells(ind, 12).value) Then
                            dL05 = CDate(ws.Cells(ind, 12).value)
                        Else
                            ' 寫入Error Log
                            Record_error(strUid + ": " + strLid + "輸入檢驗資料時,沒有報告日期")
                            ' Continue While
                        End If
                        ' 寫入資料庫tbl_Lab, uid, lid, cname, bd, mf, cid, l01, l02, l03, l04, l05, l06
                        ' 有些變數共用uid, lid, l05
                        'l01, 原就醫日期,刻意留白, 第7欄
                        Dim newLb As New tbl_lab With {
                            .uid = strUid,  '身分證字號,第2欄
                            .cname = RTrim(ws.Cells(ind, 3).value),  '病患姓名, 第3欄
                            .mf = RTrim(ws.Cells(ind, 5).value),   '性別,第5欄
                            .cid = RTrim(ws.Cells(ind, 6).value),  '原病歷號碼,第6欄
                            .lid = strLid,  '檢驗單工號, 第8欄
                            .l03 = RTrim(ws.Cells(ind, 10).value), '開單時間, 第10欄
                            .l05 = dL05,    '報告日期, 第12欄 
                            .l06 = RTrim(ws.Cells(ind, 13).value)  '報告時間,第13欄
                            }
                        If IsDate(ws.Cells(ind, 4).value) Then  '出生日期, 第4欄
                            newLb.bd = CDate(ws.Cells(ind, 4).value)
                        End If
                        If IsDate(ws.Cells(ind, 9).value) Then  '開單日(收件日), 第9欄
                            newLb.l02 = CDate(ws.Cells(ind, 9).value)
                        End If
                        If IsDate(ws.Cells(ind, 11).value) Then  '檢驗日期, 第11欄
                            newLb.l04 = CDate(ws.Cells(ind, 11).value)
                        End If
                        dc.tbl_lab.InsertOnSubmit(newLb)
                        dc.SubmitChanges()
                    Else
                        '如果沒讀過"***"就略過,以防檔案有錯
                        If (strLid.Length = 0 Or strUid.Length = 0) Then
                            Continue While
                        End If
                        ' 寫入資料庫tbl_Lab_record: uid, lid, l05, iid, l07
                        Dim newLbrd As New tbl_lab_record With {
                            .uid = strUid,    '身分證字號
                            .lid = strLid,    '檢驗單工號
                            .l05 = dL05,  '報告日期
                            .iid = RTrim(ws.Cells(ind, 1).value),    '檢驗代碼, 第1欄
                            .l07 = RTrim(ws.Cells(ind, 4).value),    '檢驗值, 第4欄
                            .l09 = RTrim(ws.Cells(ind, 5).value)   '異常, 第5欄
                            }
                        dc.tbl_lab_record.InsertOnSubmit(newLbrd)
                        ' 寫入資料庫p_lab_temp: l05, iid, l08, l09, l10, l11
                        Dim newTemp As New p_lab_temp With {
                            .l05 = dL05,  '報告日期
                            .iid = RTrim(ws.Cells(ind, 1).value),    '檢驗代碼, 第1欄
                            .l08 = RTrim(ws.Cells(ind, 2).value),   '檢驗名稱, 第2欄
                            .l10 = RTrim(ws.Cells(ind, 6).value),   '單位, 第6欄
                            .l11 = RTrim(ws.Cells(ind, 7).value)   '參考值, 第7欄
                            }
                        dc.p_lab_temp.InsertOnSubmit(newTemp)
                        dc.SubmitChanges()
                    End If
                End While
            Catch ex As Exception
                ' 寫入錯誤訊息
                Record_error(ex.Message)
            End Try
            app.Quit()
#End Region
        ElseIf Path.GetExtension(loadpath).ToLower = ".txt" Then
#Region "txt"
            Dim fr = My.Computer.FileSystem.OpenTextFileReader(loadpath, System.Text.Encoding.Default)
            Dim strL As String = fr.ReadLine '第一行
            Dim strLid As String = ""
            Dim tempstr As String = ""
            Dim dc As New MISDataContext
            Do While strL IsNot Nothing
                Dim temp_b() As Byte = System.Text.Encoding.Default.GetBytes(strL)
                '先對新的Lid做檢查
                Dim newLid As String = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(58).Take(12).ToArray))  '檢驗單工號 58-69    12
                Dim La = From l In dc.tbl_lab Where l.lid = newLid Select l ' a new query
                If newLid Is Nothing Then ' 1. 是否為空值,如果是空的就離開吧
                    strLid = ""
                    strL = fr.ReadLine  '跳下一行
                    Continue Do
                ElseIf newLid.Length = 0 Then    ' 1. 是否為空值,如果是空的就離開吧
                    strLid = ""
                    strL = fr.ReadLine  '跳下一行
                    Continue Do
                    ' 20190528 修改好了,txt無法輸入的問題
                    ' 比較新舊Lid,以決定是否換人了
                    '2. 換人的情形下,要檢查Lid是否已經在資料庫了, 2019/5/28
                ElseIf strLid <> newLid And La.Count <> 0 Then ' 換人了,又在資料庫內=>結果是跳掉, 2019/5/28
                    strLid = ""
                    strL = fr.ReadLine  '跳下一行
                    Continue Do
                ElseIf strLid <> newLid Then '換人了,又不在資料庫內,所以要加一筆tbl_lab, tbl_lab_record, p_lab_temp, 2019/5/28
                    ' 確實不一樣,以新的取代舊的
                    strLid = newLid
                    ' 寫入資料庫tbl_Lab, uid, lid, cname, bd, mf, cid, l01, l02, l03, l04, l05, l06
                    ' 有些變數共用uid, lid, l05
                    'l01, 原就醫日期,刻意留白
                    Dim newLb As New tbl_lab With {
                        .uid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Take(10).ToArray)),  '身分證字號 0-9  10
                        .cname = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(10).Take(10).ToArray)),  '病患姓名 10-19  10
                        .mf = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(28).Take(2).ToArray)),   '性別 28-29   2
                        .cid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(30).Take(20).ToArray)),  '原病歷號 30-49 20
                        .lid = strLid,  '檢驗單工號 58-69    12
                        .l03 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(102).Take(5).ToArray)), '開單日時間 78-85    8
                        .l06 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(102).Take(5).ToArray))  '報告時間 102-106   5
                        }
                    tempstr = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(20).Take(8).ToArray))
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '出生日期 20-27 8
                        newLb.bd = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(70).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '開單日70-77   8
                        newLb.l02 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(86).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '檢驗日期 86-93 8
                        newLb.l04 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(94).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '報告日 94-101 8
                        newLb.l05 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    dc.tbl_lab.InsertOnSubmit(newLb)
                    dc.SubmitChanges()
                    ' 寫入資料庫tbl_Lab_record: uid, lid, l05, iid, l07
                    Dim newLbrd As New tbl_lab_record With {
                        .uid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Take(10).ToArray)),  '身分證字號 0-9  10
                        .lid = strLid,    '檢驗單工號
                        .iid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(107).Take(10).ToArray)),    '檢驗代號 107-116   10
                        .l07 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(167).Take(20).ToArray)),    '檢驗結果 167-186   20
                        .l09 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(187).Take(10).ToArray))   '檢驗判斷 187-196   10
                        }
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(94).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '報告日 94-101 8
                        newLbrd.l05 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    dc.tbl_lab_record.InsertOnSubmit(newLbrd)
                    dc.SubmitChanges()
                    ' 寫入資料庫p_lab_temp: l05, iid, l08, l09, l10, l11
                    Dim newTemp As New p_lab_temp With {
                        .iid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(107).Take(10).ToArray)),    '檢驗代號 107-116   10
                        .l08 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(117).Take(50).ToArray)),   '檢驗名稱 117-166   50
                        .l10 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(197).Take(10).ToArray)),   '檢驗單位 197-206   10
                        .l11 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(207).Take(100).ToArray))   '檢驗參考值 207-306  100
                        }
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(94).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '報告日 94-101 8
                        newTemp.l05 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    dc.p_lab_temp.InsertOnSubmit(newTemp)
                    dc.SubmitChanges()
                Else    '沒換人,加一筆 2019/5/28
                    ' 寫入資料庫tbl_Lab_record: uid, lid, l05, iid, l07
                    Dim newLbrd As New tbl_lab_record With {
                        .uid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Take(10).ToArray)),  '身分證字號 0-9  10
                        .lid = strLid,    '檢驗單工號
                        .iid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(107).Take(10).ToArray)),    '檢驗代號 107-116   10
                        .l07 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(167).Take(20).ToArray)),    '檢驗結果 167-186   20
                        .l09 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(187).Take(10).ToArray))   '檢驗判斷 187-196   10
                        }
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(94).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '報告日 94-101 8
                        newLbrd.l05 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    dc.tbl_lab_record.InsertOnSubmit(newLbrd)
                    dc.SubmitChanges()
                    ' 寫入資料庫p_lab_temp: l05, iid, l08, l09, l10, l11
                    Dim newTemp As New p_lab_temp With {
                        .iid = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(107).Take(10).ToArray)),    '檢驗代號 107-116   10
                        .l08 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(117).Take(50).ToArray)),   '檢驗名稱 117-166   50
                        .l10 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(197).Take(10).ToArray)),   '檢驗單位 197-206   10
                        .l11 = RTrim(System.Text.Encoding.Default.GetString(temp_b.Skip(207).Take(100).ToArray))   '檢驗參考值 207-306  100
                        }
                    tempstr = System.Text.Encoding.Default.GetString(temp_b.Skip(94).Take(8).ToArray)
                    If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then  '報告日 94-101 8
                        newTemp.l05 = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                    End If
                    dc.p_lab_temp.InsertOnSubmit(newTemp)
                    dc.SubmitChanges()
                End If
                strL = fr.ReadLine  '跳下一行, 這樣才能確保不會有錯誤
            Loop

#End Region
        End If
#End Region

#Region "外表復原"
        Me.BackColor = SystemColors.Control
        Me.ProgressBar1.Visible = False
#End Region

    End Sub

    Private Sub BtnPijia_Click(sender As Object, e As EventArgs) Handles btnPijia.Click
        '20190608 created, 現在要匯入批價檔
#Region "Declaration"
        '20190608 created
        '定義
        Dim begin_date As String = Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
        Dim end_date As String = begin_date
        Dim strYM As String = ""
#End Region

#Region "ASK for begin_date end_date"
        '詢問起訖日
        '起訖日需在同一個月
        begin_date = InputBox("請輸入開始日期, 格式YYYYMMDD", "開始日期", begin_date)
        end_date = InputBox("請輸入結束日期, 格式YYYYMMDD", "結束日期", begin_date)
        '檢查格式
        If Not (IsNumeric(begin_date) And IsNumeric(end_date)) Then
            MessageBox.Show("格式不對")
            Exit Sub
        ElseIf Not (begin_date.Length = 8 And end_date.Length = 8) Then
            MessageBox.Show("格式不對")
            Exit Sub
        ElseIf Not (IsDate(begin_date.Substring(0, 4) + "/" + begin_date.Substring(4, 2) + "/" + begin_date.Substring(6, 2)) And
             IsDate(end_date.Substring(0, 4) + "/" + end_date.Substring(4, 2) + "/" + end_date.Substring(6, 2))) Then
            MessageBox.Show("格式不對")
            Exit Sub
        ElseIf end_date.Substring(4, 2) <> begin_date.Substring(4, 2) Then
            MessageBox.Show("起訖需在同一個月")
            Exit Sub
        Else
            '            strYM = begin_date.Substring(0, 6)
            strYM = (CInt(begin_date.Substring(0, 4)) - 1911).ToString + begin_date.Substring(4, 2)
        End If
#End Region

        Import_Pijia(begin_date, end_date)
    End Sub

    Private Sub BtnOrder_auto_Click(sender As Object, e As EventArgs) Handles btnOrder_auto.Click
#Region "Declaration"
        ' 20190610 created
        ' 目的是自動匯入批價項目資料
        Dim MyExcel As New Excel.Application
#End Region

#Region "Environment"
        '殺掉所有的EXCEL
        For Each p As Process In Process.GetProcessesByName("EXCEL")
            p.Kill()
        Next
        '營造環境
        ' 各類資料維護
        ' 計價標準維護      (這就是我們的標的)
        If aut.WinExists("計價標準檔維護") Then '如果直接存在就直接叫用
            aut.WinActivate("計價標準檔維護")
        Else
            If aut.WinExists("各類資料維護") Then
                aut.WinActivate("各類資料維護")
            Else
                LogINThesis()
                ' 從"杏雲雲端醫療服務"叫用"各類資料維護"
                '' 打開"處方清單", 找不到control,只好用mouse去按
                aut.WinActivate("杏雲雲端醫療服務")
                ' 先maximize
                aut.WinSetState("杏雲雲端醫療服務", "", 3)  '0 close; 1 @SW_RESTORE; 2 @SW_MINIMIZE; 3 @SW_MAXIMIZE
                aut.MouseClick("LEFT", aut.WinGetPosX("杏雲雲端醫療服務") + 200, aut.WinGetPosY("杏雲雲端醫療服務") + 175)
                aut.Sleep(500)
                aut.ControlClick("杏雲雲端醫療服務", "", "[NAME:btnDBaseMaint]")
            End If
            ' 從"各類資料維護"叫用"計價標準檔維護"
            aut.Sleep(18000)
            aut.ControlSetText("各類資料維護", "", "[NAME:txbQuery]", "計價標準檔維護")
            'aut.ControlSend("各類資料維護", "", "[NAME:txbQuery]", "計價標準檔維護")
            aut.ControlClick("各類資料維護", "", "[NAME:btnQuery]")
            aut.MouseClick("LEFT", aut.WinGetPosX("各類資料維護") + 100, aut.WinGetPosY("各類資料維護") + 135, 2)
            aut.Sleep(2000)
            aut.WinActivate("計價標準檔維護")
        End If
#End Region

        ''20190610 模仿昨天成功的經驗
        '打開EXCEL檔
        aut.Send("{Alt}")
        aut.Send("{Down}")
        aut.Send("{Down}")
        aut.Send("{Down}")
        aut.Send("{Down}")
        aut.Send("{Down}")
        aut.Send("{Down}")
        aut.Send("{Enter}")
        aut.Sleep(5000)
        Do Until aut.WinExists("活頁簿")
            aut.Sleep(100)
        Loop
        MyExcel = GetObject(, "Excel.Application")
        Do Until MyExcel.Visible
            aut.Sleep(100)
        Loop
        Import_Order(MyExcel)
    End Sub

    Private Sub BtnCombo_Click(sender As Object, e As EventArgs) Handles btnCombo.Click
#Region "Declaration"
        ' 20190611 created
        ' 組合了門診資料,改變科別,門診資料,批價資料
        Dim strYM As String = (Year(Now) - 1911).ToString + (Month(Now) + 100).ToString.Substring(1, 2)
#End Region

#Region "ASK for YM"
        strYM = InputBox("請輸入費用年月, 格式YYYMM, 民國", "詢問", strYM)
        If Not IsNumeric(strYM) Or strYM.Length <> 5 Then
            MessageBox.Show("格式錯誤")
            Exit Sub
        ElseIf CInt(strYM.Substring(3)) < 1 Or CInt(strYM.Substring(3)) > 12 Then
            MessageBox.Show("格式錯誤")
            Exit Sub
        End If
#End Region

        '修改科別
        MessageBox.Show("修改科別中")
        Dim output As DEP_return = Change_DEP(strYM)
        If output.m > 0 Then
            MessageBox.Show("修改了" + output.m.ToString + "筆, 請匯入門診資料")

            '門診
            MessageBox.Show("匯入門診資料中")
            ' 呼叫製作XML函數
            loadpath = ProduceOPDXML(output.minDate, output.maxDate)
            '呼叫匯入門診副程式
            Import_OPD(loadpath)

            '批價
            MessageBox.Show("匯入批價資料中")
            Import_Pijia(output.minDate, output.maxDate)
        End If
    End Sub

    Private Sub BtnCombine_Click(sender As Object, e As EventArgs) Handles btnCombine.Click
        Try
            Me.Aform.frmLab.Select()
        Catch ex As Exception
            Dim lm As New LabMatch
            Me.Aform.frmLab = lm
            Me.Aform.frmLab.Show()
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Refresh_data()
    End Sub
#End Region

    Private Sub ChkDash_CheckedChanged(sender As Object, e As EventArgs) Handles chkDash.CheckedChanged
        If chkDash.Checked Then
            Me.Aform.frmDash.Visible = True
        Else
            Me.Aform.frmDash.Visible = False
        End If
    End Sub

    Private Sub Refresh_data()
        ' 20190928 created
        Me.dgvAdm.DataSource = From p In dc.log_Adm Select p.regdate, p.operation_name Where operation_name = "Log in" Or operation_name = "Log out" Order By regdate Descending Take 100
        Me.dgvOPD.DataSource = From p1 In dc.log_Adm Select p1.regdate, p1.operation_name Where operation_name = "add opd" Order By regdate Descending Take 100
        Me.dgvPT.DataSource = From p2 In dc.log_Adm Select p2.regdate, p2.operation_name Where operation_name = "病患檔案格式" Order By regdate Descending Take 100
        Me.dgvOrder.DataSource = From p3 In dc.log_Adm Select p3.regdate, p3.operation_name Where operation_name = "計價檔格式" Order By regdate Descending Take 100
        Me.dgvUpload.DataSource = From p4 In dc.log_Adm Select p4.regdate, p4.operation_name Where operation_name = "健保上傳XML檔配對" Order By regdate Descending Take 100
        Me.dgvPijia.DataSource = From p5 In dc.log_Adm Select p5.regdate, p5.operation_name Where operation_name = "新增批價檔: " Order By regdate Descending Take 100
        Me.dgvCD.DataSource = From p6 In dc.log_Adm Select p6.regdate, p6.operation_name Where operation_name = "change department" Order By regdate Descending Take 100
        Me.dgvLab.DataSource = From p7 In dc.log_Adm Select p7.regdate, p7.operation_name Where operation_name = "Lab file format" Order By regdate Descending Take 100
        '' 最後匯入病患日期
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "SELECT TOP (1) regdate FROM [al].[dbo].[log_Adm] where [operation_name]='病患檔案格式' order by [regdate] desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_pt_last.Text = dr.GetDateTime(0).ToString.Split(" ")(0) + " "
        'End Using
        '' 最後病歷號
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "select top (1) [cid] from [al].[dbo].[tbl_patients] where [cid] <1000000000 order by [cid] desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_pt_last.Text += dr.GetInt64(0).ToString
        'End Using
        '' 最後匯入門診日
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "SELECT TOP (1) SDATE, VIST +' '+ right(CASENO,3) FROM [al].[dbo].[tbl_opd] order by [CASENO] desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_OPD_last.Text = dr.GetDateTime(0).ToString.Split(" ")(0) + " " + dr.GetString(1)
        'End Using
        '' 最後匯入項目
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "SELECT top (1) regdate FROM [al].[dbo].[log_Adm] where operation_name='計價檔格式' order by regdate desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_item_last.Text = dr.GetDateTime(0).ToString
        'End Using
        '' 最後匯入批價
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "SELECT TOP (1) regdate, substring(description,19,19) FROM [al].[dbo].[log_Adm] where operation_name='新增批價檔: ' order by regdate desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_pijia_last.Text = dr.GetDateTime(0).ToString + " " + dr.GetString(1)
        'End Using
        '' 最後匯入檢驗
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "select top (1) l02 from al.dbo.tbl_lab order by l02 desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_lab_last.Text = dr.GetDateTime(0).ToString.Split(" ")(0)
        'End Using
        '' 最後申報年月
        'Using cn As New SqlConnection(My.Settings.alConnectionString)
        '    Dim cmd As SqlCommand = cn.CreateCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "select top (1) t3 from al.dbo.xml_tdata order by	t3 desc"
        '    cn.Open()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    dr.Read()
        '    lbl_upload_last.Text = dr.GetString(0)
        'End Using
    End Sub

End Class