Imports AutoItX3Lib
Imports Microsoft.Office.Interop
Imports System.Net

Module SharedFunctions
    Private aut As New AutoItX3

    Public Sub Record_error(ByVal er As String)
        '寫入錯誤訊息
        Dim dc As New MISDataContext
        Dim newErr As New log_Err With {
            .error_date = Now,
            .application_name = My.Application.Info.ProductName + " V" + My.Application.Info.Version.ToString,
            .machine_name = Dns.GetHostName,
            .ip_address = Dns.GetHostEntry(Dns.GetHostName).AddressList(0).ToString(),
            .userid = "Nadia",
            .error_message = er
        }
        dc.log_Err.InsertOnSubmit(newErr)
        dc.SubmitChanges()
    End Sub

    Public Sub Record_adm(ByVal op As String, ByVal des As String)
        '寫入作業訊息
        Dim dc As New MISDataContext
        Dim newLog As New log_Adm With {
            .regdate = Now,
            .application_name = My.Application.Info.ProductName + " V" + My.Application.Info.Version.ToString,
            .machine_name = Dns.GetHostName,
            .ip_address = Dns.GetHostEntry(Dns.GetHostName).AddressList(0).ToString(),
            .userid = "Nadia",
            .operation_name = op,
            .description = des
        }
        dc.log_Adm.InsertOnSubmit(newLog)
        dc.SubmitChanges()
    End Sub

    Public Class Prescription
        Public CASENO As String
        Public rid As String
        Public TIMES_DAY As String
        Public METHOD As String
        Public TIME_QTY1 As String
        Public DAYS As String
        Public BILL_QTY As String
        Public HC As String
        Public PRICE As String
        Public AMT As String
        Public CLAS As String
        Public CHRONIC As String
    End Class

    Public Function Exact(ByVal oldPr As Prescription(), ByVal newPr As Prescription()) As String
        '宣告與設定
        Dim oldP As List(Of Prescription) = oldPr.ToList
        Dim newP As List(Of Prescription) = newPr.ToList
        Dim i As Integer = 0    '選old第一個
        Dim j As Integer = -1    '選new第一個,這裡有點怪,不過也沒有辦法,必須從-1開始
#Region "第一輪 移除兩邊完全相同的"
        Do While True
            While True
                ' 第一個newP或這個newP不完全一樣的處理, 選定j
                If j = newP.Count - 1 Then '沒有移除因此最後一個newP的index是newP.count - 1
                    '已經是最有一個newP了
                    ' 到這裡的意思就是這個oldP找不到完全相同的newP匹配
                    '這一輪找不到,要跳到下一個i 試試看, 但是要檢查是否oldP已經沒有了
                    Exit While
                Else
                    ' 下一個試試看
                    j += 1
                End If
                If newP(j).rid = oldP(i).rid Then '相同才比較,增加效率,不同的話,就直接下一個newP
                    '找到後,比較後面的欄位
                    With oldP(i)
                        'TIMES_DAY
                        If .TIMES_DAY <> newP(j).TIMES_DAY Then
                            Continue While
                        End If
                        'METHOD
                        If .METHOD <> newP(j).METHOD Then
                            Continue While
                        End If
                        'TIME_QTY1
                        If .TIME_QTY1 <> newP(j).TIME_QTY1 Then
                            Continue While
                        End If
                        'DAYS
                        If .DAYS <> newP(j).DAYS Then
                            Continue While
                        End If
                        'BILL_QTY
                        If .BILL_QTY <> newP(j).BILL_QTY Then
                            Continue While
                        End If
                        'HC
                        If .HC <> newP(j).HC Then
                            Continue While
                        End If
                        'PRICE
                        If .PRICE <> newP(j).PRICE Then
                            Continue While
                        End If
                        'AMT
                        If .TIMES_DAY <> newP(j).TIMES_DAY Then
                            Continue While
                        End If
                        'CLASS
                        If .CLAS <> newP(j).CLAS Then
                            Continue While
                        End If
                        'CHRONIC
                        If .CHRONIC <> newP(j).CHRONIC Then
                            Continue While
                        End If
                    End With
                    '通過這些檢測,表示***完全一樣***,這時候各自移除相同的這個
                    newP.Remove(newP(j))
                    oldP.Remove(oldP(i))
                    ' old 是否是最後一個?兩種情形: old是空了, old沒空
                    If i = oldP.Count Then  'old空了, count=0, i must also be 0, 若沒空 count <>0, i 是index, 原本count-1, 但remove後 i=count
                        ' 是==>總結,離開整個迴圈, 如果newP也被清零就非得要離開了
                        Exit Do
                    Else
                        ' 否, 下一個old, 從頭來
                        ' i, 維持原位就好了, 移除後已經是下一個了i不用加1, 但是j要從頭
                        j = -1  '從頭來要-1
                        Continue Do                     ' 後跳下一巡迴, 下一個oldP
                    End If
                End If
            End While
            ' old 是否是最後一個?只有一種情形沒有remove動作,不可能空的
            If i = oldP.Count - 1 Then  'count-1是最後一個的index
                ' 是==>總結,離開整個迴圈
                Exit Do
            Else
                ' 否, 下一個old, 從頭來
                ' i 需要加1,沒有移除, 但是j要從頭
                i += 1
                j = -1  '從頭來要-1
                'Continue Do                     ' 後跳下一巡迴, 下一個oldP, 不用continue do
            End If
        Loop
        '第一輪測試完成!成功!
        If newP.Count = 0 And oldP.Count = 0 Then
            Return ""
        End If
#End Region

#Region "第二輪"
        Dim strReturn As String = ""
        ' 停掉
        If oldP.Count <> 0 Then
            strReturn += "DC: "
            For i = 0 To oldP.Count - 1
                strReturn += Display_by_code(oldP(i))
            Next
        End If
        ' 新增
        If newP.Count <> 0 Then
            strReturn += "Add: "
            For i = 0 To newP.Count - 1
                strReturn += Display_by_code(newP(i))
            Next
        End If
        Return strReturn
#End Region
    End Function

    Public Function Display_by_code(ByVal Pr As Prescription) As String
        Dim strReturn As String = ""
        With Pr
            If .CLAS = "藥品" Then
                ' CODE, rid
                strReturn += .rid + ", "
                ' TIME_QTY1
                strReturn += .TIME_QTY1 + "# "
                ' TIMES_DAY
                strReturn += .TIMES_DAY + " "
                ' METHOD
                strReturn += .METHOD + " x"
                ' DAYS
                strReturn += .DAYS + "D; "
            Else
                strReturn += .rid + "; "
            End If
        End With
        Return strReturn
    End Function

    Public Sub LogINThesis()
        Dim isClud As Process() = Process.GetProcessesByName("THCludSuit")  '主目錄
        '測試"看診清單"是否有打開
        If isClud.Length = 0 Then    '沒開就打開
            aut.Run("C:\Program Files (x86)\THESE\杏雲醫療資訊系統\THCloudStarter.exe")

            '; Wait for the Notepad to become active. The classname "Notepad" Is monitored instead of the window title
            aut.WinWaitActive("登入畫面")

            ''; Now that the Notepad window Is active type some text
            If aut.ControlGetText("登入畫面", "", "[NAME:txtHospitalExtensionCode]") <> "A" Then
                aut.ControlClick("登入畫面", "", "[NAME:txtHospitalExtensionCode]", "LEFT", 2)
                aut.ControlSend("登入畫面", "", "[NAME:txtHospitalExtensionCode]", "A")
            End If
            aut.ControlSend("登入畫面", "", "[NAME:txtPassword]", "IlovePierce4926")
            aut.ControlClick("登入畫面", "", "[NAME:picLogin]")

            aut.WinActivate("杏雲雲端醫療服務")
            aut.WinWaitActive("杏雲雲端醫療服務")
            aut.Sleep(2000)
        Else
            aut.WinActivate("杏雲雲端醫療服務")
        End If
        aut.Sleep(500)
    End Sub

    Public Sub Import_Order(ByVal myEX As Excel.Application)
        '20190611 created
        'Purpose: import orders in Excel form into DATABASE al
#Region "外表"
        Main.BackColor = Color.LightPink
        Main.ProgressBar1.Visible = True
#End Region

#Region "Main Part"
        '現在開始excel 的處理
        Try
            Dim wb As Excel.Workbook = myEX.ActiveWorkbook
            '要刪除什麼欄位,合計等等資料
            ' ====================================================================================================================================
            Dim ws As Excel.Worksheet = wb.ActiveSheet

            '檢查檔案格式
            '2019/8/1 又改版本了
            Dim strT() As String = {"醫令碼", "英文規格", "生效日期", "截止日期", "健保碼", "醫令簡碼", "中文規格", "學名", "類別",
                                        "健保價", "自費價", "成本價", "院內收費項", "批價單位", "批價比率", "使用單位", "頻率", "途徑", "天數",
                                        "調劑方式", "最小劑量", "最大總量", "最大天數", "展開方式", "集合醫令明細", "劑型", "副作用", "用途",
                                        "用藥指示", "外觀", "成分含量", "廠牌", "用藥/排程說明", "藥品備註", "許可證字號", "安全存量", "臨界存量",
                                        "給付類別", "疫苗給付類別", "特定治療項目", "檢驗代碼", "案件註記", "服務機構代號", "處置碼", "檢查儀器",
                                        "停用日期", "有效醫令", "管制藥品", "磨粉", "病摘", "療程", "診斷書", "門診使用", "門診缺藥", "替換代碼", "常用",
                                        "列印", "檢核類型", "檢核起", "檢核迄", "檢核性別", "異動人員", "異動日期"}
            '2018-2019/7/31 的版本
            'Dim strT() As String = {"醫令碼", "英文規格", "生效日期", "截止日期", "健保碼", "醫令簡碼", "中文規格", "學名", "類別",
            '                            "健保價", "自費價", "成本價", "院內收費項", "批價單位", "批價比率", "使用單位", "頻率", "途徑", "天數",
            '                            "調劑方式", "最小劑量", "最大總量", "最大天數", "展開方式", "集合醫令明細", "劑型", "副作用", "用途",
            '                            "用藥指示", "外觀", "成分含量", "廠牌", "用藥/排程說明", "藥品備註", "許可證字號", "安全存量", "臨界存量",
            '                            "給付類別", "疫苗給付類別", "特定治療項目", "檢驗代碼", "案件註記", "服務機構代號", "處置碼", "檢查儀器",
            '                            "停用日期", "有效醫令", "管制藥品", "磨粉", "病摘", "療程", "診斷書", "門診缺藥", "門診使用", "常用",
            '                            "列印", "檢核類型", "檢核起", "檢核迄", "檢核性別", "異動人員", "異動日期"}
            '2018年版本,不知道何時改了版本?
            'Dim strT() As String = {"醫令碼", "英文規格", "生效日期", "截止日期", "健保碼", "醫令簡碼", "中文規格", "學名", "類別",
            '                            "健保價", "自費價", "成本價", "院內收費項", "使用單位", "批價單位", "頻率", "途徑", "天數", "調劑方式",
            '                            "批價比率", "最小劑量", "最大總量", "最大天數", "展開方式", "集合醫令明細", "劑型", "副作用", "用途",
            '                            "用藥指示", "外觀", "成分含量", "廠牌", "用藥/排程說明", "藥品備註", "許可證字號", "安全存量", "臨界存量",
            '                            "給付類別", "疫苗給付類別", "特定治療項目", "檢驗代碼", "案件註記", "服務機構代號", "處置碼", "檢查儀器",
            '                            "國際商品碼", "停用日期", "有效醫令", "管制藥品", "病摘", "療程", "診斷書", "門診缺藥", "門診使用", "常用",
            '                            "列印", "檢核類型", "檢核起", "檢核迄", "檢核性別", "異動人員", "異動日期"}
            For i = 1 To strT.Length
                If ws.Cells(1, i).value <> strT(i - 1) Then
                    ' 寫入Error Log
                    Record_error(" 輸入的標準計價檔檔案格式不對")
                    Exit Try
                End If
            Next

            '通過測試
            Record_adm("計價檔格式", "correct")

            Dim totalN As Integer = ws.UsedRange.Rows.Count - 1
            Main.ProgressBar1.Minimum = 1
            Main.ProgressBar1.Maximum = totalN
            ' ====================================================================================================================================
            '製作自動檔名
            Dim temp_filepath As String = "C:\vpn\odr"
            '存放目錄,不存在就要建立一個
            If Not (System.IO.Directory.Exists(temp_filepath)) Then
                System.IO.Directory.CreateDirectory(temp_filepath)
            End If
            '自動產生名字
            temp_filepath += "\odr_" + Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
            temp_filepath += "_" + Now.TimeOfDay.ToString.Replace(":", "").Replace(".", "")
            temp_filepath += ".xlsx"
            wb.SaveAs(temp_filepath, Excel.XlFileFormat.xlOpenXMLWorkbook)

            ' 要有迴路, 來讀一行一行的xls, 能夠判斷
            For i = 2 To (totalN + 1)
                ' 先判斷是否已經在資料表中, 如果不是就insert否則判斷要不要update
                Dim dc As New MISDataContext
                Dim strRID As String = ""

                If ws.Cells(i, 1).Value.ToString.Length = 0 Then
                    ' 寫入Error Log
                    ' 沒有醫令代碼是不行的
                    Record_error("醫令代碼是空的")
                Else
                    strRID = ws.Cells(i, 1).value    '醫令代碼,第1欄
                    Dim od = From d In dc.p_order Where d.rid = strRID Select d    ' this is a querry

                    If od.Count = 0 Then
                        'insert
                        ' 沒這個醫令可以新增這個醫令
                        ' 填入資料
                        Try
                            Dim newOd As New p_order With {
                                    .rid = strRID,
                                    .r01 = ws.Cells(i, 2).value,  '英文規格, 第2欄
                                    .r02 = ws.Cells(i, 3).value,     '生效日期,第3欄
                                    .r03 = ws.Cells(i, 4).value,  '截止日期,第4欄
                                    .r04 = ws.Cells(i, 5).value, ' 健保碼, 第5欄
                                    .r06 = ws.Cells(i, 7).value,  '中文規格, 第7欄
                                    .r07 = ws.Cells(i, 8).value,  '學名, 第8欄
                                    .r08 = ws.Cells(i, 9).value,  '類別,第9欄
                                    .r09 = ws.Cells(i, 10).value,  '健保價,第10欄
                                    .r10 = ws.Cells(i, 11).value,  '自費價, 第11欄
                                    .r11 = ws.Cells(i, 12).value,  '成本價, 第12欄
                                    .r12 = ws.Cells(i, 13).value,  '院內收費項, 第13欄
                                    .r13 = ws.Cells(i, 16).value,  '使用單位, 第14欄, 20190611 改成第16欄
                                    .r14 = ws.Cells(i, 14).value,  '批價單位, 第15欄, 20190611 改成第14欄
                                    .r15 = ws.Cells(i, 17).value,  '頻率, 第16欄, 20190611 改成第17欄
                                    .r16 = ws.Cells(i, 18).value,  '途徑, 第17欄, 20190611 改成第18欄
                                    .r18 = ws.Cells(i, 20).value,  '調劑方式, 第19欄, 20190611 改成第20欄
                                    .r19 = ws.Cells(i, 15).value,  '批價比率, 第20欄, 20190611 改成第15欄
                                    .r25 = ws.Cells(i, 26).value,  '劑型, 第26欄
                                    .r26 = ws.Cells(i, 27).value,  '副作用, 第27欄
                                    .r27 = ws.Cells(i, 28).value,  '用途, 第28欄
                                    .r28 = ws.Cells(i, 29).value,  '用藥指示, 第29欄
                                    .r29 = ws.Cells(i, 30).value,  '外觀, 第30欄
                                    .r30 = ws.Cells(i, 31).value,  '程分含量, 第31欄
                                    .r31 = ws.Cells(i, 32).value,  '廠牌, 第32欄
                                    .r32 = ws.Cells(i, 33).value,  '用藥/排程說明, 第33欄
                                    .r33 = ws.Cells(i, 34).value,  '藥品備註, 第34欄
                                    .r34 = ws.Cells(i, 35).value,  '許可證字號, 第35欄
                                    .r40 = ws.Cells(i, 41).value,  '檢驗代碼, 第41欄
                                    .r48 = ws.Cells(i, 48).value,  '管制藥品, 第49欄, 20190611 改成第48欄
                                    .r52 = ws.Cells(i, 54).value,  '門診缺藥, 第53欄, 20190929 改成第54欄
                                    .r60 = ws.Cells(i, 62).value,  '異動人員, 第61欄, 20190929 改成第62欄
                                    .r61 = ws.Cells(i, 63).value,  '異動日期, 第62欄, 20190929 改成第63欄
                                    .r62 = Now
                                    }
                            dc.p_order.InsertOnSubmit(newOd)
                            dc.SubmitChanges()

                            Record_adm("Add a new order", strRID)
                        Catch ex As Exception
                            Record_error(ex.Message)
                        End Try
                    Else
                        ' update
                        ' 有此醫令喔, 走update方向
                        ' 拿oldOd比較ws.cells(i),如果不同就修改,並且記錄
                        Dim oldOd As p_order = (From d In dc.p_order Where d.rid = strRID Select d).ToList()(0)     ' this is a record
                        Dim strChange As String = ""
                        Dim bChange As Boolean = False
                        Try
                            '英文規格, 第2欄
                            If oldOd.r01 <> ws.Cells(i, 2).value Then
                                strChange += "改英文規格: " + oldOd.r01 + "=>" + ws.Cells(i, 2).value + "; "
                                bChange = True
                                oldOd.r01 = ws.Cells(i, 2).value
                            End If
                            '生效日期,第3欄
                            If oldOd.r02 <> ws.Cells(i, 3).value Then
                                strChange += "改生效日期: " + oldOd.r02 + "=>" + ws.Cells(i, 3).value + "; "
                                bChange = True
                                oldOd.r02 = ws.Cells(i, 3).value
                            End If
                            '截止日期,第4欄
                            If oldOd.r03 <> ws.Cells(i, 4).value Then
                                strChange += "改截止日期: " + oldOd.r03 + "=>" + ws.Cells(i, 4).value + "; "
                                bChange = True
                                oldOd.r03 = ws.Cells(i, 4).value
                            End If
                            '健保碼, 第5欄
                            If oldOd.r04 <> ws.Cells(i, 5).value Then
                                strChange += "改健保碼: " + oldOd.r04 + "=>" + ws.Cells(i, 5).value + "; "
                                bChange = True
                                oldOd.r04 = ws.Cells(i, 5).value
                            End If
                            '中文規格, 第7欄
                            If oldOd.r06 <> ws.Cells(i, 7).value Then
                                strChange += "改中文規格: " + oldOd.r06 + "=>" + ws.Cells(i, 7).value + "; "
                                bChange = True
                                oldOd.r06 = ws.Cells(i, 7).value
                            End If
                            '學名, 第8欄
                            If oldOd.r07 <> ws.Cells(i, 8).value Then
                                strChange += "改學名: " + oldOd.r07 + "=>" + ws.Cells(i, 8).value + "; "
                                bChange = True
                                oldOd.r07 = ws.Cells(i, 8).value
                            End If
                            '類別,第9欄
                            If oldOd.r08 <> ws.Cells(i, 9).value Then
                                strChange += "改類別: " + oldOd.r08 + "=>" + ws.Cells(i, 9).value + "; "
                                bChange = True
                                oldOd.r08 = ws.Cells(i, 9).value
                            End If
                            '健保價,第10欄
                            If oldOd.r09 <> ws.Cells(i, 10).value Then
                                strChange += "改健保價: " + oldOd.r09 + "=>" + ws.Cells(i, 10).value + "; "
                                bChange = True
                                oldOd.r09 = ws.Cells(i, 10).value
                            End If
                            '自費價, 第11欄
                            If oldOd.r10 <> ws.Cells(i, 11).value Then
                                strChange += "改自費價: " + oldOd.r10 + "=>" + ws.Cells(i, 11).value + "; "
                                bChange = True
                                oldOd.r10 = ws.Cells(i, 11).value
                            End If
                            '成本價, 第12欄
                            If oldOd.r11 <> ws.Cells(i, 12).value Then
                                strChange += "改成本價: " + oldOd.r11 + "=>" + ws.Cells(i, 12).value + "; "
                                bChange = True
                                oldOd.r11 = ws.Cells(i, 12).value
                            End If
                            '院內收費項, 第13欄
                            If oldOd.r12 <> ws.Cells(i, 13).value Then
                                strChange += "改院內收費項: " + oldOd.r12 + "=>" + ws.Cells(i, 13).value + "; "
                                bChange = True
                                oldOd.r12 = ws.Cells(i, 13).value
                            End If
                            '使用單位, 第14欄, 20190611 改成第16欄
                            If oldOd.r13 <> ws.Cells(i, 16).value Then
                                strChange += "改使用單位: " + oldOd.r13 + "=>" + ws.Cells(i, 16).value + "; "
                                bChange = True
                                oldOd.r13 = ws.Cells(i, 16).value
                            End If
                            '批價單位, 第15欄, 20190611 改成第14欄
                            If oldOd.r14 <> ws.Cells(i, 14).value Then
                                strChange += "改批價單位: " + oldOd.r14 + "=>" + ws.Cells(i, 14).value + "; "
                                bChange = True
                                oldOd.r14 = ws.Cells(i, 14).value
                            End If
                            '頻率, 第16欄, 20190611 改成第17欄
                            If oldOd.r15 <> ws.Cells(i, 17).value Then
                                strChange += "改頻率: " + oldOd.r15 + "=>" + ws.Cells(i, 17).value + "; "
                                bChange = True
                                oldOd.r15 = ws.Cells(i, 17).value
                            End If
                            '途徑, 第17欄, 20190611 改成第18欄
                            If oldOd.r16 <> ws.Cells(i, 18).value Then
                                strChange += "改途徑: " + oldOd.r16 + "=>" + ws.Cells(i, 18).value + "; "
                                bChange = True
                                oldOd.r16 = ws.Cells(i, 18).value
                            End If
                            '調劑方式, 第19欄, 20190611 改成第20欄
                            If oldOd.r18 <> ws.Cells(i, 20).value Then
                                strChange += "改調劑方式: " + oldOd.r18 + "=>" + ws.Cells(i, 20).value + "; "
                                bChange = True
                                oldOd.r18 = ws.Cells(i, 20).value
                            End If
                            '批價比率, 第20欄, 20190611 改成第15欄
                            If oldOd.r19 <> ws.Cells(i, 15).value Then
                                strChange += "改批價比率: " + oldOd.r19 + "=>" + ws.Cells(i, 15).value + "; "
                                bChange = True
                                oldOd.r19 = ws.Cells(i, 15).value
                            End If
                            '劑型, 第26欄
                            If oldOd.r25 <> ws.Cells(i, 26).value Then
                                strChange += "改劑型: " + oldOd.r25 + "=>" + ws.Cells(i, 26).value + "; "
                                bChange = True
                                oldOd.r25 = ws.Cells(i, 26).value
                            End If
                            '副作用, 第27欄
                            If oldOd.r26 <> ws.Cells(i, 27).value Then
                                strChange += "改副作用: " + oldOd.r26 + "=>" + ws.Cells(i, 27).value + "; "
                                bChange = True
                                oldOd.r26 = ws.Cells(i, 27).value
                            End If
                            '用途, 第28欄
                            If oldOd.r27 <> ws.Cells(i, 28).value Then
                                strChange += "改用途: " + oldOd.r27 + "=>" + ws.Cells(i, 28).value + "; "
                                bChange = True
                                oldOd.r27 = ws.Cells(i, 28).value
                            End If
                            '用藥指示, 第29欄
                            If oldOd.r28 <> ws.Cells(i, 29).value Then
                                strChange += "改用藥指示: " + oldOd.r28 + "=>" + ws.Cells(i, 29).value + "; "
                                bChange = True
                                oldOd.r28 = ws.Cells(i, 29).value
                            End If
                            '外觀, 第30欄
                            If oldOd.r29 <> ws.Cells(i, 30).value Then
                                strChange += "改外觀: " + oldOd.r29 + "=>" + ws.Cells(i, 30).value + "; "
                                bChange = True
                                oldOd.r29 = ws.Cells(i, 30).value
                            End If
                            '成分含量, 第31欄
                            If oldOd.r30 <> ws.Cells(i, 31).value Then
                                strChange += "改成分含量: " + oldOd.r30 + "=>" + ws.Cells(i, 31).value + "; "
                                bChange = True
                                oldOd.r30 = ws.Cells(i, 31).value
                            End If
                            '廠牌, 第32欄
                            If oldOd.r31 <> ws.Cells(i, 32).value Then
                                strChange += "改廠牌: " + oldOd.r31 + "=>" + ws.Cells(i, 32).value + "; "
                                bChange = True
                                oldOd.r31 = ws.Cells(i, 32).value
                            End If
                            '用藥/排程說明, 第33欄
                            If oldOd.r32 <> ws.Cells(i, 33).value Then
                                strChange += "改用藥排程說明: " + oldOd.r32 + "=>" + ws.Cells(i, 33).value + "; "
                                bChange = True
                                oldOd.r32 = ws.Cells(i, 33).value
                            End If
                            '藥品備註, 第34欄
                            If oldOd.r33 <> ws.Cells(i, 34).value Then
                                strChange += "改藥品備註: " + oldOd.r33 + "=>" + ws.Cells(i, 34).value + "; "
                                bChange = True
                                oldOd.r33 = ws.Cells(i, 34).value
                            End If
                            '許可證字號, 第35欄
                            If oldOd.r34 <> ws.Cells(i, 35).value Then
                                strChange += "改許可證字號: " + oldOd.r34 + "=>" + ws.Cells(i, 35).value + "; "
                                bChange = True
                                oldOd.r34 = ws.Cells(i, 35).value
                            End If
                            '檢驗代碼, 第41欄
                            If oldOd.r40 <> ws.Cells(i, 41).value Then
                                strChange += "改檢驗代碼: " + oldOd.r40 + "=>" + ws.Cells(i, 41).value + "; "
                                bChange = True
                                oldOd.r40 = ws.Cells(i, 41).value
                            End If
                            '管制藥品, 第49欄, 20190611 改成第48欄
                            If oldOd.r48 <> ws.Cells(i, 48).value Then
                                strChange += "改管制藥品: " + oldOd.r48 + "=>" + ws.Cells(i, 48).value + "; "
                                bChange = True
                                oldOd.r48 = ws.Cells(i, 48).value
                            End If
                            '門診缺藥, 第53欄, 20190929 改成第54欄
                            If oldOd.r52 <> ws.Cells(i, 54).value Then
                                strChange += "改門診缺藥: " + oldOd.r52 + "=>" + ws.Cells(i, 54).value + "; "
                                bChange = True
                                oldOd.r52 = ws.Cells(i, 54).value
                            End If
                            '異動人員, 第61欄, 20190929 改成第62欄
                            If oldOd.r60 <> ws.Cells(i, 62).value Then
                                strChange += "改異動人員: " + oldOd.r60 + "=>" + ws.Cells(i, 62).value + "; "
                                bChange = True
                                oldOd.r60 = ws.Cells(i, 62).value
                            End If
                            '異動日期, 第62欄, 20190929 改成第63欄
                            If oldOd.r61 <> ws.Cells(i, 63).value Then
                                strChange += "改異動日期: " + oldOd.r61 + "=>" + ws.Cells(i, 63).value + "; "
                                bChange = True
                                oldOd.r61 = ws.Cells(i, 63).value
                            End If

                            If bChange = True Then
                                ' 做實改變
                                dc.SubmitChanges()
                                '做記錄
                                Record_adm("Change order data", (strRID + ": " + strChange))
                            End If
                        Catch ex As Exception
                            Record_error(ex.Message)
                        End Try
                    End If
                End If

                Main.ProgressBar1.Value = i - 1
            Next
            wb.Close()
            '殺掉所有的EXCEL
            For Each p As Process In Process.GetProcessesByName("EXCEL")
                p.Kill()
            Next
            aut.WinClose("計價標準檔維護")
            aut.WinClose("各類資料維護")
        Catch ex As Exception
            ' 寫入錯誤訊息
            Record_error(ex.Message)
        End Try
#End Region

#Region "外表復原"
        Main.BackColor = SystemColors.Control
        Main.ProgressBar1.Visible = False
#End Region
    End Sub

    Public Sub Import_Pt(ByVal myEX As Excel.Application)
        '20190611 created
        'Purpose: import patient data in Excel form into DATABASE al
#Region "外表"
        Main.BackColor = Color.LightPink
        Main.ProgressBar1.Visible = True
#End Region

#Region "Main Part"
        '現在開始excel 的處理
        Try
            Dim wb As Excel.Workbook = myEX.ActiveWorkbook
            '要刪除什麼欄位,合計等等資料
            ' ====================================================================================================================================
            Dim ws As Excel.Worksheet = wb.ActiveSheet

            '檢查檔案格式
            ' 可以算出總筆數,第一行是標題,不算
            Dim strT() As String = {"病歷號", "姓名", "性別", "室內電話", "手機門號", "電子郵件", "傳送日期", "身分證號", "生日", "地址", "提醒"}
            For i = 1 To strT.Length
                If ws.Cells(1, i).value <> strT(i - 1) Then
                    ' 寫入Error Log
                    Record_error(" 輸入的病患資料檔案格式不對")
                    MessageBox.Show("檔案格式不對")
                    Exit Try
                End If
            Next

            '通過測試
            Record_adm("病患檔案格式", "correct")

            Dim totalN As Integer = ws.UsedRange.Rows.Count - 1
            Main.ProgressBar1.Minimum = 1
            Main.ProgressBar1.Maximum = totalN
            ' ====================================================================================================================================
            '製作自動檔名
            Dim temp_filepath As String = "C:\vpn\pt"
            ' 20190609 因為不小心多一個空格, 搞了好久除錯, 很辛苦啊
            ' System.Runtime.InteropServices.COMException '發生例外狀況於 HRESULT: 0x800A03EC'
            '存放目錄,不存在就要建立一個
            If Not (System.IO.Directory.Exists(temp_filepath)) Then
                System.IO.Directory.CreateDirectory(temp_filepath)
            End If
            '自動產生名字
            temp_filepath += "\pt_" + Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
            temp_filepath += "_" + Now.TimeOfDay.ToString.Replace(":", "").Replace(".", "")
            temp_filepath += ".xlsx"
            'wb.SaveAs(temp_filepath, Excel.XlFileFormat.xlCSV, vbNull, vbNull, False, False, Excel.XlSaveAsAccessMode.xlNoChange, vbNull, vbNull, vbNull, vbNull, vbNull)
            wb.SaveAs(temp_filepath, Excel.XlFileFormat.xlOpenXMLWorkbook)

            ' 要有迴路, 來讀一行一行的xls, 能夠判斷
            For i = 2 To (totalN + 1)
                ' 先判斷是否已經在資料表中, 如果不是就insert否則判斷要不要update
                ' 如何判斷是否已經在資料表中?
                Dim dc As New MISDataContext
                Dim strUID As String = ""
                '先判斷身分證字號是否空白
                If ws.Cells(i, 8).Value.ToString.Length = 0 Then
                    ' 寫入Error Log
                    ' 沒有身分證字號是不行的
                    Record_error("身分證字號是空的")
                Else
                    '再判斷是否已在資料表中
                    strUID = ws.Cells(i, 8).value    '身分證號,第8欄
                    Dim pt = From p In dc.tbl_patients Where p.uid = strUID Select p    ' this is a querry
                    If pt.Count = 0 Then
                        'insert
                        ' 沒這個人可以新增這個人
                        ' 填入資料
                        Try
                            Dim newPt As New tbl_patients
                            If ws.Cells(i, 1).value.ToString.Length = 0 Then
                                ' 寫入Error Log
                                Record_error(strUID + " 沒有病歷號碼")
                                Exit Try
                            Else
                                newPt.cid = CDbl(ws.Cells(i, 1).value)  '病歷號, 第1欄
                            End If
                            newPt.uid = strUID     '身分證號,第8欄
                            If ws.Cells(i, 2).value.ToString.Length = 0 Then
                                ' 寫入Error Log
                                Record_error(strUID + " 沒有姓名")
                                Exit Try
                            Else
                                newPt.cname = ws.Cells(i, 2).value  '姓名,第2欄
                            End If
                            newPt.mf = ws.Cells(i, 3).value ' 性別, 第3欄
                            If ws.Cells(i, 9).value.ToString.Length = 0 Then
                                ' 寫入Error Log
                                Record_error(strUID + " 沒有生日資料")
                                Exit Try
                            Else
                                Dim strD As String = ws.Cells(i, 9).value   ' 生日, 第9欄
                                newPt.bd = CDate(strD.Substring(0, 4) + "/" + strD.Substring(4, 2) + "/" + strD.Substring(6, 2))
                            End If
                            newPt.p01 = ws.Cells(i, 4).value  '市內電話, 第4欄
                            newPt.p02 = ws.Cells(i, 5).value  '手機電話, 第5欄
                            newPt.p03 = ws.Cells(i, 10).value  '地址,第10欄
                            newPt.p04 = ws.Cells(i, 11).value  '提醒,第11欄

                            dc.tbl_patients.InsertOnSubmit(newPt)
                            dc.SubmitChanges()

                            '20190929 加姓名, 病歷號
                            Record_adm("Add a new patient", ws.Cells(i, 1).value.ToString + " " + strUID + " " + ws.Cells(i, 2).value.ToString)
                        Catch ex As Exception
                            Record_error(ex.Message)
                        End Try
                    Else
                        ' update
                        ' 有此人喔, 走update方向
                        ' 拿pt比較ws.cells(i),如果不同就修改,並且記錄
                        Dim oldPt As tbl_patients = (From p In dc.tbl_patients Where p.uid = strUID Select p).ToList()(0)     ' this is a record
                        Dim strChange As String = ""
                        Dim bChange As Boolean = False
                        Try
                            '姓名
                            If ws.Cells(i, 2).value.ToString.Length = 0 Then
                                ' 寫入Error Log
                                Record_error(strUID + " 沒有姓名")
                                Exit Try
                            Else
                                If oldPt.cname <> ws.Cells(i, 2).value Then
                                    strChange += "改名: " + oldPt.cname + "=>" + ws.Cells(i, 2).value + "; "
                                    bChange = True
                                    oldPt.cname = ws.Cells(i, 2).value  '姓名,第2欄
                                End If
                            End If
                            '性別
                            If oldPt.mf <> ws.Cells(i, 3).value Then
                                strChange += "改性別: " + oldPt.mf + "=>" + ws.Cells(i, 3).value + "; "
                                bChange = True
                                oldPt.mf = ws.Cells(i, 3).value  ' 性別, 第3欄
                            End If
                            '生日
                            If ws.Cells(i, 9).value.ToString.Length = 0 Then
                                ' 寫入Error Log
                                Record_error(strUID + " 沒有生日資料")
                                Exit Try
                            Else
                                Dim strBD As String = ws.Cells(i, 9).value   ' 生日, 第9欄
                                Dim dBD As Date = CDate(strBD.Substring(0, 4) + "/" + strBD.Substring(4, 2) + "/" + strBD.Substring(6, 2))
                                If oldPt.bd <> dBD Then
                                    strChange += "改生日: " + oldPt.bd + "=>" + dBD.ToString + "; "
                                    bChange = True
                                    oldPt.bd = dBD   '生日,第9欄
                                End If
                            End If
                            '市內電話
                            If oldPt.p01 <> ws.Cells(i, 4).value Then
                                strChange += "改市內電話: " + oldPt.p01 + "=>" + ws.Cells(i, 4).value + "; "
                                bChange = True
                                oldPt.p01 = ws.Cells(i, 4).value  '市內電話,第4欄
                            End If
                            '手機電話
                            If oldPt.p02 <> ws.Cells(i, 5).value Then
                                strChange += "改手機電話: " + oldPt.p02 + "=>" + ws.Cells(i, 5).value + "; "
                                bChange = True
                                oldPt.p02 = ws.Cells(i, 5).value  '手機電話,第5欄
                            End If
                            '地址
                            If oldPt.p03 <> ws.Cells(i, 10).value Then
                                strChange += "改地址: " + oldPt.p03 + "=>" + ws.Cells(i, 10).value + "; "
                                bChange = True
                                oldPt.p03 = ws.Cells(i, 10).value  '地址,第10欄
                            End If
                            '提醒
                            If oldPt.p04 <> ws.Cells(i, 11).value Then
                                strChange += "改提醒: " + oldPt.p04 + "=>" + ws.Cells(i, 11).value + "; "
                                bChange = True
                                oldPt.p04 = ws.Cells(i, 11).value  '提醒,第11欄
                            End If

                            If bChange = True Then
                                ' 做實改變
                                dc.SubmitChanges()
                                '做記錄
                                '20190929 加姓名, 病歷號
                                Record_adm("Change patient data", (ws.Cells(i, 1).value.ToString + " " + strUID + " " + ws.Cells(i, 2).value.ToString + ": " + strChange))
                            End If
                        Catch ex As Exception
                            Record_error(ex.Message)
                        End Try
                    End If
                End If
                Main.ProgressBar1.Value = i - 1
            Next
            wb.Close()
            '殺掉所有的EXCEL
            For Each p As Process In Process.GetProcessesByName("EXCEL")
                p.Kill()
            Next
            aut.WinClose("各類特殊 追蹤與紀錄查詢")
        Catch ex As Exception
            ' 寫入錯誤訊息
            Record_error(ex.Message)
        End Try
#End Region

#Region "外表復原"
        Main.BackColor = SystemColors.Control
        Main.ProgressBar1.Visible = False
#End Region
    End Sub

    Public Function ProduceOPDXML(ByVal begin_date As String, ByVal end_date As String) As String
        Dim output As String
#Region "Environment"
        Try
            '營造環境
            If aut.WinExists("處方清單") Then '如果直接存在就直接叫用
                aut.WinActivate("處方清單")
            Else
                LogINThesis()
                '' 打開"處方清單", 找不到control,只好用mouse去按
                aut.WinActivate("杏雲雲端醫療服務")
                ' 先maximize
                aut.WinSetState("杏雲雲端醫療服務", "", 3)  '0 close; 1 @SW_RESTORE; 2 @SW_MINIMIZE; 3 @SW_MAXIMIZE
                aut.MouseMove(280, 280)
                aut.MouseClick()
                aut.Sleep(500)
                aut.ControlClick("杏雲雲端醫療服務", "", "[NAME:btnPrescription]")
                Threading.Thread.Sleep(10000)
            End If

            ' 打開備份
            aut.WinWaitActive("處方清單")
            aut.ControlClick("處方清單", "", "[NAME:btnBackup]")
            aut.WinActivate("處方清單備份選項")
            aut.WinWaitActive("處方清單備份選項")   '
            aut.ControlClick("處方清單備份選項", "", "[NAME:txbBackupPath]", "LEFT", 2)
            aut.Send("{Tab}")
            aut.Send("{Tab}")
            aut.Send("{Enter}") 'first choice Desktop
            ' 這裡的等待很重要, 太短來不及讀, 500可以, 100 不行, 200 一半一半, 250 100%
            aut.Sleep(300)
            ' 尋找XML, 若有就刪除
            output = aut.ControlGetText("處方清單備份選項", "", "[NAME:txbBackupPath]")
            output += "\" + begin_date.Substring(0, 4) + "\" + begin_date.Substring(0, 6) + ".xml"
            If System.IO.File.Exists(output) Then
                System.IO.File.Delete(output)
            End If
            'aut.ControlSend("處方清單備份選項", "", "[NAME:txbBackupPath]", "C:\vpn")
        Catch ex As Exception
            output = ""
            Record_error(ex.ToString)
        End Try
#End Region

#Region "Producing XML"
        Shell("C:\vpn\exe\changePresDTP.exe " + begin_date + end_date, AppWinStyle.Hide, True)
        ' 檢查XML做好了嗎?
        Do Until System.IO.File.Exists(output)
            Threading.Thread.Sleep(100)
        Loop
        ' XML好了就把頁面關掉
        aut.ControlClick("處方清單備份選項", "", "[NAME:Cancel_Button]")
        Threading.Thread.Sleep(200)

        '        aut.ControlClick("處方清單", "", "[NAME:BtnEXIT]")
#End Region
        Return output
    End Function

    Public Sub Import_OPD(ByVal loadpath As String)
#Region "外表"
        Main.BackColor = Color.LightPink
        Main.ProgressBar1.Visible = True
#End Region

#Region "進行讀取資料"
        Dim ds As DataSet = New DataSet
        Dim dtO As DataTable = New DataTable
        Dim dtP As DataTable = New DataTable

        '整理datatable, 分拆成兩個, 一旦可以通過,那這個檔案應該沒有問題,如果有問題,就不是正確的檔案
#Region "整理datatable"
        Try
            ds.ReadXml(loadpath, XmlReadMode.ReadSchema)
            dtP = ds.Tables(0)  'dtP for tbl_opd_order, P stands for prescription
            dtP.Columns.Remove("STATUS")
            dtP.Columns.Remove("REGNO")
            dtP.Columns.Remove("PNAME")
            dtP.Columns.Remove("SEX")
            dtP.Columns.Remove("BIRTH")
            dtP.Columns.Remove("ORI_TOTAL")
            dtP.Columns.Remove("TOTAL")
            dtP.Columns.Remove("AMT8")
            dtP.Columns.Remove("RECT_NO")
            dtO = dtP.Copy
            '移除dtO不必要欄位, 先轉移給暫存檔, 因為要distinct
            dtO.Columns.Remove("CODE")
            dtO.Columns.Remove("ENAME")
            dtO.Columns.Remove("TIMES_DAY")
            dtO.Columns.Remove("METHODE")
            dtO.Columns.Remove("TIME_QTY1")
            dtO.Columns.Remove("DAYS")
            dtO.Columns.Remove("BILL_QTY")
            dtO.Columns.Remove("CHRONIC")
            dtO.Columns.Remove("PUT_TYPE")
            dtO.Columns.Remove("HC")
            dtO.Columns.Remove("PRICE")
            dtO.Columns.Remove("AMT")
            dtO.Columns.Remove("ORI_AMT")
            dtO.Columns.Remove("CLASS")
            dtO.Columns.Remove("PRN_CODE")
            dtO.Columns.Remove("RESULT")
            ' 移除dtP不需要的欄位(for tbl_opd_order)
            dtP.Columns.Remove("VIST")
            dtP.Columns.Remove("RMNO")
            dtP.Columns.Remove("DEPTNAME")
            dtP.Columns.Remove("DOCTNAME")
            dtP.Columns.Remove("POSINAME")
            dtP.Columns.Remove("PAYNO")
            dtP.Columns.Remove("HEATH_CARD")
            dtP.Columns.Remove("STEXT")
            dtP.Columns.Remove("OTEXT")
            dtP.Columns.Remove("ICDCODE1")
            dtP.Columns.Remove("ICDCODE2")
            dtP.Columns.Remove("ICDCODE3")
            dtO = dtO.DefaultView.ToTable(True, {"CASENO", "SDATE", "VIST", "RMNO", "DEPTNAME", "DOCTNAME",
                                          "IDNO", "POSINAME", "PAYNO", "HEATH_CARD", "STEXT", "OTEXT", "ICDCODE1", "ICDCODE2",
                                          "ICDCODE3"})    ' true stands for distinct
        Catch ex As Exception
            Record_error(ex.Message)
        End Try
#End Region

        '通過測試
        Record_adm("OPD file format", "correct")

        Dim totalN As Integer = dtO.Rows.Count
        Main.ProgressBar1.Minimum = 1
        Main.ProgressBar1.Maximum = totalN
        Dim dc As New MISDataContext

        '開始回圈
        For i = 0 To (totalN - 1)   'row index 0~(totalN-1)
            Main.ProgressBar1.Value = i + 1  '顯示一下進度

            Try
                ' 檢查案號是否已經在資料庫中, dtO.CASENO, tbl_opd.CASENO
                Dim strCASENO As String = dtO.Rows(i)("CASENO")
                If strCASENO Is Nothing Then
                    Record_error("在輸入門診資料時, 缺少案號CASENO")
                    Continue For
                ElseIf strCASENO.Length = 0 Then    'no CASENO
                    Record_error("在輸入門診資料時, 缺少案號CASENO")
                    Continue For
                Else
                    Dim q1 = From o In dc.tbl_opd Where o.CASENO = strCASENO Select o
                    If q1.Count = 0 Then '資料庫裡面沒有 INSERT
                        Dim tempdate As Date
                        Dim tempstr As String
                        Dim newOPD As New tbl_opd
                        With newOPD
                            .CASENO = strCASENO 'CASENO
                            tempstr = dtO.Rows(i)("SDATE").ToString
                            If IsDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2)) Then
                                tempdate = CDate(tempstr.Substring(0, 4) + "/" + tempstr.Substring(4, 2) + "/" + tempstr.Substring(6, 2))
                                .SDATE = tempdate  'SDATE
                            End If
                            .VIST = dtO.Rows(i)("VIST") 'VIST
                            .RMNO = CInt(dtO.Rows(i)("RMNO")) 'RMNO
                            .uid = dtO.Rows(i)("IDNO") 'uid
                            .DEPTNAME = dtO.Rows(i)("DEPTNAME") 'DEPTNAME
                            .DOCTNAME = dtO.Rows(i)("DOCTNAME") 'DOCTNAME
                            .POSINAME = dtO.Rows(i)("POSINAME") 'POSINAME
                            .PAYNO = dtO.Rows(i)("PAYNO")  'PAYNO
                            .HEATH_CARD = dtO.Rows(i)("HEATH_CARD") 'HEATH_CARD
                            .ICDCODE1 = dtO.Rows(i)("ICDCODE1") 'ICDCODE1
                            .ICDCODE2 = dtO.Rows(i)("ICDCODE2") 'ICDCODE2
                            .ICDCODE3 = dtO.Rows(i)("ICDCODE3") 'ICDCODE3
                            .INS_CODE = "A" 'INS_CODE, default value "A"
                            .STEXT = dtO.Rows(i)("STEXT") 'STEXT
                            .OTEXT = dtO.Rows(i)("OTEXT") 'OTEXT
                        End With
                        dc.tbl_opd.InsertOnSubmit(newOPD)
                        dc.SubmitChanges()
                        ' tbl_opd沒有資料, tbl_opd_order就一定沒有資料, 所以要加入, 這裡的挑戰是要加上醫令序
                        ' datatable 此時不能使用LINQ查詢
                        Dim q2 As List(Of DataRow) = dtP.Select("CASENO='" + strCASENO + "'").ToList
                        ' 這個r.count一定大於等於1

                        ' 處理tbl_opd_order部分
                        Dim totalP As Integer = q2.Count
                        For j = 0 To totalP - 1
                            Dim newPr As New tbl_opd_order
                            With newPr
                                .CASENO = strCASENO
                                .uid = dtO.Rows(i)("IDNO")
                                If IsDate(tempdate) Then
                                    .SDATE = tempdate
                                End If
                                .OD_idx = (j + 1)
                                .rid = q2(j)("CODE") 'CODE
                                .TIMES_DAY = q2(j)("TIMES_DAY") 'TIMES_DAY
                                .METHOD = q2(j)("METHODE") 'METHOD
                                .TIME_QTY1 = q2(j)("TIME_QTY1") 'TIME_QTY1
                                .DAYS = q2(j)("DAYS") 'DAYS
                                .BILL_QTY = q2(j)("BILL_QTY") 'BILL_QTY
                                .HC = q2(j)("HC") 'HC
                                .PRICE = q2(j)("PRICE") 'PRICE
                                .AMT = q2(j)("AMT") 'AMT
                                .CLASS = q2(j)("CLASS") 'CLASS
                                .CHRONIC = q2(j)("CHRONIC") 'CHRONIC
                            End With
                            dc.tbl_opd_order.InsertOnSubmit(newPr)
                            dc.SubmitChanges()
                        Next
                    Else    '資料庫裡已經有了, 檢查是否有異,有異UPDATE
                        ' 先處理tbl_opd部分
                        Dim oldOPD As tbl_opd = (From o In dc.tbl_opd Where o.CASENO = strCASENO Select o).ToList()(0)     ' this is a record
                        Dim strChange As String = ""
                        Dim bChange As Boolean = False
                        Try
                            Dim tempstr As String = ""
                            With oldOPD
                                If .DEPTNAME <> dtO.Rows(i)("DEPTNAME") Then
                                    strChange += "改科別: " + .DEPTNAME.ToString + "=>" + dtO.Rows(i)("DEPTNAME")
                                    bChange = True
                                    .DEPTNAME = dtO.Rows(i)("DEPTNAME") 'DEPTNAME
                                End If
                                If .DOCTNAME <> dtO.Rows(i)("DOCTNAME") Then
                                    strChange += "改醫師: " + .DOCTNAME.ToString + "=>" + dtO.Rows(i)("DOCTNAME")
                                    bChange = True
                                    .DOCTNAME = dtO.Rows(i)("DOCTNAME") 'DOCTNAME
                                End If
                                If .POSINAME <> dtO.Rows(i)("POSINAME") Then
                                    strChange += "改身分: " + .POSINAME.ToString + "=>" + dtO.Rows(i)("POSINAME")
                                    bChange = True
                                    .POSINAME = dtO.Rows(i)("POSINAME") 'POSINAME
                                End If
                                If .PAYNO <> dtO.Rows(i)("PAYNO") Then
                                    strChange += "改負擔: " + .PAYNO.ToString + "=>" + dtO.Rows(i)("PAYNO")
                                    bChange = True
                                    .PAYNO = dtO.Rows(i)("PAYNO")  'PAYNO
                                End If
                                If .HEATH_CARD <> dtO.Rows(i)("HEATH_CARD") Then
                                    strChange += "改卡號: " + .HEATH_CARD.ToString + "=>" + dtO.Rows(i)("HEATH_CARD")
                                    bChange = True
                                    .HEATH_CARD = dtO.Rows(i)("HEATH_CARD") 'HEATH_CARD
                                End If
                                If .ICDCODE1 <> dtO.Rows(i)("ICDCODE1") Then
                                    strChange += "改診斷1: " + .ICDCODE1.ToString + "=>" + dtO.Rows(i)("ICDCODE1")
                                    bChange = True
                                    .ICDCODE1 = dtO.Rows(i)("ICDCODE1") 'ICDCODE1
                                End If
                                If .ICDCODE2 <> dtO.Rows(i)("ICDCODE2") Then
                                    strChange += "改診斷2: " + .ICDCODE2.ToString + "=>" + dtO.Rows(i)("ICDCODE2")
                                    bChange = True
                                    .ICDCODE2 = dtO.Rows(i)("ICDCODE2") 'ICDCODE2
                                End If
                                If .ICDCODE3 <> dtO.Rows(i)("ICDCODE3") Then
                                    strChange += "改診斷3: " + .ICDCODE3.ToString + "=>" + dtO.Rows(i)("ICDCODE3")
                                    bChange = True
                                    .ICDCODE3 = dtO.Rows(i)("ICDCODE3") 'ICDCODE3
                                End If
                                ' 無法比較病歷, 所以不比較好了
                                'If .STEXT <> dtO.Rows(i)("STEXT") Then
                                '    strChange += "改主訴: " + .STEXT
                                '    bChange = True
                                '    .STEXT = dtO.Rows(i)("STEXT") 'STEXT
                                'End If
                                'If .OTEXT <> dtO.Rows(i)("OTEXT") Then
                                '    strChange += "改客訴: " + .OTEXT
                                '    bChange = True
                                '    .OTEXT = dtO.Rows(i)("OTEXT") 'OTEXT
                                'End If
                            End With

                            If bChange = True Then
                                ' 做實改變
                                dc.SubmitChanges()
                                '做記錄
                                Record_adm("update opd", (strCASENO + ": " + strChange))
                            End If
                        Catch ex As Exception
                            Record_error(strCASENO + ex.Message)
                        End Try
                        ' 再處理tbl_opd_order部分
                        ' 先製造兩個list of tbl_opd_order
                        Dim oldPre = (From d In dc.tbl_opd_order Where d.CASENO = strCASENO Order By d.rid, d.TIMES_DAY
                                      Select New Prescription With {.CASENO = d.CASENO, .rid = d.rid, .TIMES_DAY = d.TIMES_DAY,
                                          .METHOD = d.METHOD, .TIME_QTY1 = d.TIME_QTY1, .DAYS = d.DAYS, .BILL_QTY = d.BILL_QTY,
                                          .HC = d.HC, .PRICE = d.PRICE, .AMT = d.AMT, .CLAS = d.CLASS, .CHRONIC = d.CHRONIC}).ToList()
                        Dim newPre As New List(Of Prescription)
                        Dim q2 As List(Of DataRow) = dtP.Select("CASENO='" + strCASENO + "'", "CODE, TIMES_DAY").ToList
                        ' 這個r.count一定大於等於1

                        ' 處理tbl_opd_order部分
                        Dim totalP As Integer = q2.Count
                        For j = 0 To totalP - 1
                            Dim newP As New Prescription
                            With newP
                                .CASENO = strCASENO
                                .rid = q2(j)("CODE") 'CODE
                                .TIMES_DAY = q2(j)("TIMES_DAY") 'TIMES_DAY
                                .METHOD = q2(j)("METHODE") 'METHOD
                                .TIME_QTY1 = q2(j)("TIME_QTY1") 'TIME_QTY1
                                .DAYS = q2(j)("DAYS") 'DAYS
                                .BILL_QTY = q2(j)("BILL_QTY") 'BILL_QTY
                                .HC = q2(j)("HC") 'HC
                                .PRICE = q2(j)("PRICE") 'PRICE
                                .AMT = q2(j)("AMT") 'AMT
                                .CLAS = q2(j)("CLASS") 'CLASS
                                .CHRONIC = q2(j)("CHRONIC") 'CHRONIC
                            End With
                            newPre.Add(newP)
                        Next
                        'Now we have 2 lists now, but lists are only references
                        ' 先比較兩者是否相同, 相同則跳下一筆
                        Dim strT As String = Exact(oldPre.ToArray, newPre.ToArray)
                        If strT.Length <> 0 Then ' "" stands for identical
                            ' 若不同則找出哪裡不同, 記錄下來
                            Record_adm("update opd order", strCASENO + ": " + strT)
                            ' 最後把舊的刪掉, 插入新的

                            ' 刪掉舊的
                            Dim q3 = From pr In dc.tbl_opd_order Where pr.CASENO = strCASENO Select pr
                            For Each pr In q3
                                dc.tbl_opd_order.DeleteOnSubmit(pr)
                            Next
                            dc.SubmitChanges()
                            ' 插入新的
                            ' datatable 此時不能使用LINQ查詢
                            Dim q4 As List(Of DataRow) = dtP.Select("CASENO='" + strCASENO + "'").ToList

                            ' 處理tbl_opd_order部分
                            Dim totalPr As Integer = q4.Count
                            For j = 0 To totalPr - 1
                                Dim newPr As New tbl_opd_order
                                With newPr
                                    .CASENO = strCASENO
                                    .uid = oldOPD.uid
                                    .SDATE = oldOPD.SDATE
                                    .OD_idx = (j + 1)
                                    .rid = q4(j)("CODE") 'CODE
                                    .TIMES_DAY = q4(j)("TIMES_DAY") 'TIMES_DAY
                                    .METHOD = q4(j)("METHODE") 'METHOD
                                    .TIME_QTY1 = q4(j)("TIME_QTY1") 'TIME_QTY1
                                    .DAYS = q4(j)("DAYS") 'DAYS
                                    .BILL_QTY = q4(j)("BILL_QTY") 'BILL_QTY
                                    .HC = q4(j)("HC") 'HC
                                    .PRICE = q4(j)("PRICE") 'PRICE
                                    .AMT = q4(j)("AMT") 'AMT
                                    .CLASS = q4(j)("CLASS") 'CLASS
                                    .CHRONIC = q4(j)("CHRONIC") 'CHRONIC
                                End With
                                dc.tbl_opd_order.InsertOnSubmit(newPr)
                                dc.SubmitChanges()
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                Record_error(ex.Message)
            End Try

        Next
        ' 這樣的add opd沒什麼用
        '        Record_adm("add opd", dtO.TableName)

        dtO.Dispose()
        dtP.Dispose()
        ds.Dispose()

#End Region

#Region "外表復原"
        Main.BackColor = SystemColors.Control
        Main.ProgressBar1.Visible = False
#End Region
    End Sub

    Public Sub Import_Pijia(ByVal begin_date As String, ByVal end_date As String)
#Region "Declaration"
        '20190608 created
        '定義
        Dim dc As New MISDataContext
        Dim MyExcel As Excel.Application
        Dim filepath As New List(Of String) '存放pijia檔
        Dim strYM As String = (CInt(begin_date.Substring(0, 4)) - 1911).ToString + begin_date.Substring(4, 2)
#End Region

#Region "Environment"
        Try
            '殺掉所有的EXCEL
            For Each p As Process In Process.GetProcessesByName("EXCEL")
                p.Kill()
            Next
            '營造環境
            Dim isAdvn As Process() = Process.GetProcessesByName("THCAdvancedBillingReport")
            If isAdvn.Length = 0 Then    '測試"日報表清單"是否有打開
                '有無登入系統?

                LogINThesis()
                '如果沒有打開就打開"日報表清單"
                Shell("C:\Program Files (x86)\THESE\杏雲醫療資訊系統\THCAdvancedBillingReport.exe", AppWinStyle.NormalFocus, False)
                Threading.Thread.Sleep(300)
            End If
            '[FrmMain] v1.0.0.67
            aut.WinWaitActive("[FrmMain] v")
            '[NAME:btnDailyIncome]
            '按下去
            aut.ControlClick("[FrmMain] v", "", "[NAME:btnDailyIncome]")
            aut.Sleep(500)
            '日收入報表A
            '[NAME:chk允許完整筆數呈現]
            aut.ControlClick("日收入報表A", "", "[NAME:chk允許完整筆數呈現]")
            '[NAME:chkIncludeInvalid]
            aut.ControlClick("日收入報表A", "", "[NAME:chkIncludeInvalid]")
            Shell("C:\vpn\exe\changeBillDTP.exe " + begin_date + end_date, AppWinStyle.Hide, True)
        Catch ex As Exception
            Record_error(ex.ToString)
        End Try
#End Region

#Region "The Loop of 讀取檔案"
        '20190609 今天竟然完成了最難的部分
        ' a FOR loop, LIST of A, B, C, D, E, F, G, H, I, J, K, L, M
        ' Making a list
        'A 周孫元診所; B 聖愛; C 啟智; D 由根; E 方舟; F 景仁; G 香園; H 觀音; I 桃園; J 誠信; K 祥育; L 春暉; M 世美
        Dim lArea As New List(Of String) From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"}
        For Each a In lArea
            '[NAME:cmbArea]
            'aut.ControlFocus("日收入報表A", "", "[NAME:cmbArea]")
            'aut.Send(a)
            aut.Sleep(1000) '這裡等一下
            aut.ControlSend("日收入報表A", "", "[NAME:cmbArea]", a)
            ' execute AutoIT
            '日收入報表A
            aut.Sleep(500) '這裡等一下
            '[NAME:dtpStart]    input begin_date
            '[NAME:dtpEnd]      input end_date
            '[NAME:btnExcel]    click
            aut.ControlClick("日收入報表A", "", "[NAME:btnExcel]")
            ' EXCEL management
            ' ? 怎麼判斷有EXCEL
            ' 查看有沒有EXCEL的process
            'Dim i As Int16
            'Do Until Process.GetProcessesByName("EXCEL").Length > 0
            '    aut.Sleep(10)
            '    i += 1
            'Loop
            'MessageBox.Show(i)
            'MessageBox.Show("hi")
            aut.Sleep(2000) '20190614 這個點的等待真的很重要, 1000已經無法成功, 1500有9成成功, 選用2000
            '20190609 這個點的等待很重要, <600 都找不到EXCEL; 700 可成功; 經過測試大約是200個循環左右
            ' 20190609 原本還擔心這個方法沒效, 原來要等700ms以上, 就可以正常, 這也是未來可能出錯的地方, 如果有其它原因造成EXCEL開啟延後,就會錯誤
            Dim pr As Process() = Process.GetProcessesByName("EXCEL")
            If pr.Count > 0 Then
                ' 有的話,excel.application, getobject, 存檔, 存檔案位置, 供匯入用
                ' winwait實驗可行, 可偵測excel已經完成
                'aut.WinWaitActive("活頁簿"), 實際測試失敗, 改用DO Loop
                ' 後來發現process建立後, 一段時間才會建立windows
                Do Until aut.WinExists("活頁簿")
                    aut.Sleep(100)
                Loop
                'aut.Sleep(10000), 用等的,等10秒大多有效,但不能保證,且也許不用10秒,這樣就浪費了, 應該要個別化
                '好在發現visibility可以有效等到整個檔案製作完成
                MyExcel = GetObject(, "Excel.Application")
                Do Until MyExcel.Visible
                    aut.Sleep(100)
                Loop
                '現在開始excel 的處理
                Try
                    Dim wb As Excel.Workbook = MyExcel.ActiveWorkbook
                    Dim ws As Excel.Worksheet = wb.ActiveSheet
                    '要刪除什麼欄位,合計等等資料
                    ' ====================================================================================================================================
                    '檢查欄位, 如果欄位不對, 就不要處理了
                    '要有: 狀態 收據號 批價人員 作廢日期 看診日期 午別 診別 科別 醫師 身分 就醫序號 優免 部分負擔 身分證號 患者姓名 醫療費用 掛號費用 部分負擔 押金 自付金額	藥費加重
                    '      欠收 折扣 應收金額 實收金額 收據說明	說明
                    '刪除: 項次 病歷號 性別 生日 年齡 還款金額 電話 地址 國籍

                    '檢查檔案格式
                    ' 可以算出總筆數,第一行是標題,不算
                    Dim listToAdd As New List(Of String) From {"狀態", "收據號", "批價人員", "作廢日期", "看診日期", "午別", "診別", "科別", "醫師", "身分", "就醫序號", "優免", "部分負擔",
                    "身分證號", "患者姓名", "醫療費用", "掛號費用", "部分負擔", "押金", "自付金額", "藥費加重", "欠收", "折扣", "應收金額", "實收金額", "收據說明", "說明"}
                    Dim listToDel As New List(Of String) From {"項次", "病歷號", "性別", "生日", "年齡", "還款金額", "電話", "地址", "國籍"}
                    ' 檢查是否有充足欄位?
                    Dim j As Int16 = 1
                    Dim x As Boolean = False
                    Do
                        If ws.Cells(1, j).value = "" Then
                            x = True
                        Else
                            listToAdd.Remove(ws.Cells(1, j).value)
                            j += 1
                        End If
                    Loop Until x
                    Dim totalColumn As Int16 = j - 1
                    If listToAdd.Count = 0 Then
                        '                    Record_adm("匯入批價檔", "檔案格式正確")
                        ' 格式正確
                    Else
                        Dim output As String = ""
                        For j = 1 To listToAdd.Count
                            output += listToAdd.Item(j - 1) + ", "
                        Next
                        Record_error("匯入批價檔格式不合,缺「" + output.Substring(0, output.Length - 2) + "」欄位")
                        ' 格式不合,缺欄位
                    End If
                    ' 刪除欄位
                    x = False
                    Dim colToDel As New List(Of Int16)
                    For j = 1 To totalColumn
                        If listToDel.Remove(ws.Cells(1, j).value) Then
                            colToDel.Add(j)
                        End If
                    Next
                    For j = 1 To colToDel.Count
                        ws.Columns(colToDel(colToDel.Count - j)).delete
                    Next
                    ' ====================================================================================================================================
                    '製作自動檔名, 並存檔
                    Dim temp_filepath As String = "C:\vpn\bills"
                    ' 20190609 因為不小心多一個空格, 搞了好久除錯, 很辛苦啊
                    ' System.Runtime.InteropServices.COMException '發生例外狀況於 HRESULT: 0x800A03EC'
                    '存放目錄,不存在就要建立一個
                    If Not (System.IO.Directory.Exists(temp_filepath)) Then
                        System.IO.Directory.CreateDirectory(temp_filepath)
                    End If
                    '自動產生名字
                    temp_filepath += "\bill_" + a + "_" + begin_date + "_" + end_date
                    temp_filepath += "_" + Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
                    temp_filepath += Now.TimeOfDay.ToString.Replace(":", "").Replace(".", "")
                    temp_filepath += ".csv"
                    filepath.Add(temp_filepath)
                    'wb.SaveAs(temp_filepath, Excel.XlFileFormat.xlCSV, vbNull, vbNull, False, False, Excel.XlSaveAsAccessMode.xlNoChange, vbNull, vbNull, vbNull, vbNull, vbNull)
                    wb.SaveAs(temp_filepath, Excel.XlFileFormat.xlCSV)
                    wb.Close()
                    ' 殺掉這個process
                    pr(0).Kill()
                    '修剪csv
                    Dim Lines As New List(Of String)(System.IO.File.ReadAllLines(temp_filepath, System.Text.Encoding.Default))
                    For LinesIndex As Integer = Lines.Count - 1 To 0 Step -1
                        Dim Line As String = Lines(LinesIndex)
                        If Line.Substring(0, 5) = ",,,,," Or Line.Substring(0, 2) = "狀態" Then
                            '該行有包含7的內容，則刪除 
                            Lines.RemoveAt(LinesIndex)
                        End If
                    Next
                    '覆寫整個檔案 
                    System.IO.File.WriteAllLines(temp_filepath, Lines.ToArray(), System.Text.Encoding.Default)
                Catch ex As Exception
                    Record_error(ex.ToString)
                End Try
            Else
                ' 沒有的話,下一個
                'do nothing
                aut.Sleep(500)
            End If
            ' loop back, NEXT, 怎麼知道可以下一步了?
        Next
        ' close windows
        '日收入報表A
        '[NAME:Cancel_Button]
        aut.ControlClick("日收入報表A", "", "[NAME:Cancel_Button]")
        '[FrmMain] v1.0.0.67
        '[NAME:btnExit]
        '        aut.ControlClick("[FrmMain] v", "", "[NAME:btnExit]")
#End Region

#Region "外表"
        Main.BackColor = Color.LightPink
        Main.ProgressBar1.Visible = True
#End Region

#Region "進行讀取資料"
        '20190609 created
        '模仿匯入opd
        'add及update, update要清空CASENO, G
        '20190612 重大修改,key值改為三個YM, bid, uid, 原因是bid在同一個月內重複太多了
        '用varchar, 不要用varchar,不然比較時會出錯, VDATE空值仍會有8個空白
        Try
            Dim totalN As Integer = filepath.Count
            Main.ProgressBar1.Minimum = 1
            Main.ProgressBar1.Maximum = totalN + 1
            Main.ProgressBar1.Value = 1
            '開始回圈
            '讀取每一筆檔案
            For Each f As String In filepath
                Dim Lines As New List(Of String)(System.IO.File.ReadAllLines(f, System.Text.Encoding.Default))
                For Each Line As String In Lines
                    ' 用","分隔, 這也是CSV的意義
                    Dim Item As String() = Line.Split(",")
                    ' 找到KEY值, YM, bid: YM=strYM, bid=Item(1), 第二個值就是bid
                    ' 查詢,看看是否有重複
                    ' 沒有重複就是新增, 有重複就是修改
                    Dim q = From o In dc.tbl_pijia Where o.YM = strYM And o.bid = Item(1) And o.uid = Item(13) Select o
                    If q.Count = 0 Then '資料庫裡面沒有 INSERT
                        Dim newPijia As New tbl_pijia With {
                                                    .YM = strYM,
                                                    .STATUS = Item(0),
                                                    .bid = Item(1),
                                                    .op = Item(2),
                                                    .VDATE = Item(3),
                                                    .SDATE = Item(4),
                                                    .VIST = Item(5),
                                                    .RMNO = Item(6),
                                                    .DEPTNAME = Item(7),
                                                    .DOCTNAME = Item(8),
                                                    .POSINAME = Item(9),
                                                    .HEATH_CARD = Item(10),
                                                    .Youmian = Item(11),
                                                    .PAYNO = Item(12),
                                                    .uid = Item(13),
                                                    .cname = Item(14),
                                                    .MedFee = Item(15),
                                                    .RegFee = Item(16),
                                                    .Copay = Item(17),
                                                    .Deposit = Item(18),
                                                    .SelfPay = Item(19),
                                                    .PharmW = Item(20),
                                                    .Arrears = Item(21),
                                                    .Discount = Item(22),
                                                    .AMTreceivable = Item(23),
                                                    .AMTreceived = Item(24),
                                                    .bremark = Item(25),
                                                    .remark = Item(26)
                            }
                        dc.tbl_pijia.InsertOnSubmit(newPijia)
                        dc.SubmitChanges()
                    Else    '資料庫裡已經有了, 檢查是否有異,有異UPDATE
                        Dim oldPijia As tbl_pijia = q.ToList()(0)     ' this is a record
                        Dim strChange As String = ""
                        Dim bChange As Boolean = False
                        With oldPijia
                            If .STATUS <> Item(0) Then
                                strChange += ";改狀態: " + .STATUS.ToString + "=>" + Item(0)
                                bChange = True
                                .STATUS = Item(0)
                            End If
                            If .op <> Item(2) Then
                                strChange += ";改批價人員: " + .op.ToString + "=>" + Item(2)
                                bChange = True
                                .op = Item(2)
                            End If
                            If .VDATE <> Item(3) Then
                                strChange += ";改作廢日期: " + .VDATE.ToString + "=>" + Item(3)
                                bChange = True
                                .VDATE = Item(3)
                            End If
                            If .SDATE <> Item(4) Then
                                strChange += ";改看診日期: " + .SDATE.ToString + "=>" + Item(4)
                                bChange = True
                                .SDATE = Item(4)
                            End If
                            If .VIST <> Item(5) Then
                                strChange += ";改午別: " + .VIST.ToString + "=>" + Item(5)
                                bChange = True
                                .VIST = Item(5)
                            End If
                            If .RMNO <> Item(6) Then
                                strChange += ";改診別: " + .RMNO.ToString + "=>" + Item(6)
                                bChange = True
                                .RMNO = Item(6)
                            End If
                            If .DEPTNAME <> Item(7) Then
                                strChange += ";改科別: " + .DEPTNAME.ToString + "=>" + Item(7)
                                bChange = True
                                .DEPTNAME = Item(7)
                            End If
                            If .DOCTNAME <> Item(8) Then
                                strChange += ";改醫師: " + .DOCTNAME.ToString + "=>" + Item(8)
                                bChange = True
                                .DOCTNAME = Item(8)
                            End If
                            If .POSINAME <> Item(9) Then
                                strChange += ";改身分: " + .POSINAME.ToString + "=>" + Item(9)
                                bChange = True
                                .POSINAME = Item(9)
                            End If
                            If .HEATH_CARD <> Item(10) Then
                                strChange += ";改就醫序號: " + .HEATH_CARD.ToString + "=>" + Item(10)
                                bChange = True
                                .HEATH_CARD = Item(10)
                            End If
                            If .Youmian <> Item(11) Then
                                strChange += ";改優免: " + .Youmian.ToString + "=>" + Item(11)
                                bChange = True
                                .Youmian = Item(11)
                            End If
                            If .PAYNO <> Item(12) Then
                                strChange += ";改部分負擔: " + .PAYNO.ToString + "=>" + Item(12)
                                bChange = True
                                .PAYNO = Item(12)
                            End If
                            If .cname <> Item(14) Then
                                strChange += ";改患者姓名: " + .cname.ToString + "=>" + Item(14)
                                bChange = True
                                .cname = Item(14)
                            End If
                            If .MedFee <> Item(15) Then
                                strChange += ";改醫療費用: " + .MedFee.ToString + "=>" + Item(15)
                                bChange = True
                                .MedFee = Item(15)
                            End If
                            If .RegFee <> Item(16) Then
                                strChange += ";改掛號費用: " + .RegFee.ToString + "=>" + Item(16)
                                bChange = True
                                .RegFee = Item(16)
                            End If
                            If .Copay <> Item(17) Then
                                strChange += ";改部分負擔: " + .Copay.ToString + "=>" + Item(17)
                                bChange = True
                                .Copay = Item(17)
                            End If
                            If .Deposit <> Item(18) Then
                                strChange += ";改押金: " + .Deposit.ToString + "=>" + Item(18)
                                bChange = True
                                .Deposit = Item(18)
                            End If
                            If .SelfPay <> Item(19) Then
                                strChange += ";改自付金額: " + .SelfPay.ToString + "=>" + Item(19)
                                bChange = True
                                .SelfPay = Item(19)
                            End If
                            If .PharmW <> Item(20) Then
                                strChange += ";改藥費加重: " + .PharmW.ToString + "=>" + Item(20)
                                bChange = True
                                .PharmW = Item(20)
                            End If
                            If .Arrears <> Item(21) Then
                                strChange += ";改欠收: " + .Arrears.ToString + "=>" + Item(21)
                                bChange = True
                                .Arrears = Item(21)
                            End If
                            If .Discount <> Item(22) Then
                                strChange += ";改折扣: " + .Discount.ToString + "=>" + Item(22)
                                bChange = True
                                .Discount = Item(22)
                            End If
                            If .AMTreceivable <> Item(23) Then
                                strChange += ";改應收金額: " + .AMTreceivable.ToString + "=>" + Item(23)
                                bChange = True
                                .AMTreceivable = Item(23)
                            End If
                            If .AMTreceived <> Item(24) Then
                                strChange += ";改實收金額: " + .AMTreceived.ToString + "=>" + Item(24)
                                bChange = True
                                .AMTreceived = Item(24)
                            End If
                            If .bremark <> Item(25) Then
                                strChange += ";改收據說明: " + .bremark.ToString + "=>" + Item(25)
                                bChange = True
                                .bremark = Item(25)
                            End If
                            If .remark <> Item(26) Then
                                strChange += ";改說明: " + .remark.ToString + "=>" + Item(26)
                                bChange = True
                                .remark = Item(26)
                            End If
                        End With
                        If bChange = True Then
                            ' tbl_opd的Pijia欄位也要歸零
                            Dim r = From opd In dc.tbl_opd Where opd.CASENO = oldPijia.CASENO Select opd
                            Dim opdOPD As tbl_opd = r.ToList(0)
                            opdOPD.Pijia = Nothing
                            ' CASENO, G要歸零
                            oldPijia.CASENO = Nothing
                            oldPijia.G = Nothing
                            ' 做實改變
                            dc.SubmitChanges()
                            '做記錄
                            Record_adm("修改批價資料", (strYM + "-" + Item(13) + ": " + strChange))
                        End If
                    End If
                Next
                Main.ProgressBar1.Value += 1  '顯示一下進度
                Record_adm("新增批價檔: ", f)
            Next

            ' 現再來配對, 使用Stored Procedure
            ' 第一步Pijia配上CASENO
            ' 第二步檢查CASENO是否1to1配上Pijia, 若是進行配對,並顯示正確,若否回傳錯誤幾筆,並且紀錄下來
        Catch ex As Exception
            Record_error(ex.Message)
        End Try
#End Region

#Region "外表復原"
        Main.BackColor = SystemColors.Control
        Main.ProgressBar1.Visible = False
#End Region

#Region "進行配對"
        '20190614 created
        '目的是將tbl_pijia和tbl_opd配對起來
        '分為兩步
        '第一步將tbl_pijia配上CASENO
        Dim q1 = From cs In dc.sp_CASENO_for_pijia().AsEnumerable Select cs
        Record_adm("批價檔配對STEP1 Pijia", begin_date + "_" + end_date + ": " + q1(0).rows_affected.ToString + "筆配對")
        Dim q2 = From pj In dc.sp_PIJIA_for_opd().AsEnumerable Select pj
        Dim strOutput As String = ""
        For Each q In q2
            strOutput += q.CASENO + " " + q.SDATE + " " + q.VIST + " " + q.RMNO + " " + q.bid + " " + q.cname + ";"
        Next
        If strOutput = "" Then
            Record_adm("批價檔配對STEP2 OPD", "沒有重複")
        Else
            strOutput += ";請修正後再上傳"
            Record_adm("批價檔配對STEP2 OPD", strOutput)
            MessageBox.Show(strOutput.Replace(";", vbCrLf), "CASE有重複值")
        End If
#End Region
    End Sub

    Public Structure DEP_return
        Public m As Int16
        Public maxDate As String
        Public minDate As String
    End Structure

    Public Function Change_DEP(ByVal strYM As String) As DEP_return
#Region "Declaration"
        '20190607 created
        '定義
        Dim savepath As String = "C:\vpn\change_dep"
        Dim return_value As DEP_return
        Dim dc As New MISDataContext
#End Region

#Region "Making CSV"
        '呼叫SQL stored procedure
        Try
            Dim output As List(Of sp_change_depResult) = dc.sp_change_dep(strYM).ToList
            '存放目錄,不存在就要建立一個
            If Not (System.IO.Directory.Exists(savepath)) Then
                System.IO.Directory.CreateDirectory(savepath)
            End If
            '自動產生名字
            savepath += "\change_dep_" + strYM + "_"
            savepath += Year(Now).ToString + (Month(Now) + 100).ToString.Substring(1, 2) + (DatePart("d", Now) + 100).ToString.Substring(1, 2)
            savepath += Now.TimeOfDay.ToString.Replace(":", "").Replace(".", "")
            savepath += ".csv"
            '製作csv檔 writing to csv
            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(savepath)
            Dim i As Integer = 1
            return_value.m = output.Count
            If return_value.m = 0 Then
                MessageBox.Show("沒有什麼需要修改的")
                Record_adm("change department", "沒有什麼需要修改的")
                return_value.minDate = ""
                return_value.maxDate = ""
                Return return_value
                Exit Function
            Else
                Dim minD As String = "99999999"
                Dim maxD As String = "00000000"
                For Each out In output
                    sw.Write(out.o) '欄位名叫o
                    If i < return_value.m Then
                        sw.Write(sw.NewLine)
                    End If
                    Dim tempD = out.o.Substring(0, 8)
                    '找尋最大的值
                    If CInt(tempD) > CInt(maxD) Then
                        maxD = tempD
                    End If
                    '找尋最小的值
                    If CInt(tempD) < CInt(minD) Then
                        minD = tempD
                    End If
                    i += 1
                Next
                return_value.minDate = minD
                return_value.maxDate = maxD
                sw.Close()
            End If
        Catch ex As Exception
            return_value.m = 0
            return_value.minDate = ""
            return_value.maxDate = ""
            Record_error(ex.ToString)
        End Try
#End Region

#Region "Environment"
        Try
            '營造環境
            Dim isClud As Process() = Process.GetProcessesByName("THCludSuit")
            Dim isClin As Process() = Process.GetProcessesByName("THCClinic")
            If isClin.Length = 0 Then    '如果沒有打開
                '測試"看診清單"是否有打開
                If isClud.Length = 0 Then    '沒開就打開
                    aut.Run("C:\Program Files (x86)\THESE\杏雲醫療資訊系統\THCloudStarter.exe")

                    '; Wait for the Notepad to become active. The classname "Notepad" Is monitored instead of the window title
                    aut.WinWaitActive("登入畫面")

                    ''; Now that the Notepad window Is active type some text
                    If aut.ControlGetText("登入畫面", "", "[NAME:txtHospitalExtensionCode]") <> "A" Then
                        aut.ControlClick("登入畫面", "", "[NAME:txtHospitalExtensionCode]", "LEFT", 2)
                        aut.ControlSend("登入畫面", "", "[NAME:txtHospitalExtensionCode]", "A")
                    End If
                    aut.ControlSend("登入畫面", "", "[NAME:txtPassword]", "IlovePierce4926")
                    aut.ControlClick("登入畫面", "", "[NAME:picLogin]")

                    aut.WinActivate("杏雲雲端醫療服務")
                    aut.WinWaitActive("杏雲雲端醫療服務")
                    aut.Sleep(2000)
                End If

                '' 打開"看診清單"
                Shell("C:\Program Files (x86)\THESE\杏雲醫療資訊系統\THCClinic.exe", AppWinStyle.MinimizedNoFocus, False)
                Threading.Thread.Sleep(10000)
            End If
        Catch ex As Exception
            Record_error(ex.ToString)
        End Try
#End Region

#Region "Execute change department"
        Try
            aut.WinWaitActive("看診清單")
            Shell("C:\vpn\exe\changeDP.exe " + savepath, AppWinStyle.Hide, True)
            '            MessageBox.Show("修改了" + m.ToString + "筆, 請匯入門診資料")
            Record_adm("change department", "修改了" + return_value.m.ToString + "筆")
            Return return_value
        Catch ex As Exception
            Record_error(ex.ToString)
            Return return_value
        End Try
#End Region
    End Function

    Public Class Allform
        Public frmDash As DashBoard
        Public frmLab As LabMatch
    End Class
End Module