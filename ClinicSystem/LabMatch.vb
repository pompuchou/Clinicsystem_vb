Public Class LabMatch
    Private dc As New MISDataContext

    Private Sub DashBoard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.txtFrom.Text = 0
        Me.txtFrom.ReadOnly = True
        Me.txtTo.ReadOnly = True
        Me.btnMatch.Select()
        Call Renew()
    End Sub

    Private Sub Renew()
        Me.dgvDataNoOrder.DataSource = From p In dc.v_labdata_not_match_with_opd_order Select p Order By p.uid, p.l05
        Me.dgvOrderNoData.DataSource = From q In dc.v_opdorder_not_match_with_lab_record Select q Order By q.uid, q.SDATE
    End Sub

    Private Sub TxtFrom_DoubleClick(sender As Object, e As EventArgs) Handles txtFrom.DoubleClick
        Dim strFrom As String = InputBox("處方前幾日起?前一日為-1", "何時開始?", txtFrom.Text)
        If Not IsNumeric(strFrom) Then
            txtFrom.Text = "0"
        ElseIf CInt(strFrom) > CInt(txtTo.Text) Then
            txtFrom.Text = txtTo.Text
        Else
            txtFrom.Text = strFrom
        End If
        Me.btnMatch.Select()
    End Sub

    Private Sub TxtTo_DoubleClick(sender As Object, e As EventArgs) Handles txtTo.DoubleClick
        Dim strTo As String = InputBox("至處方後幾日?", "何時結束?", txtTo.Text)
        If Not IsNumeric(strTo) Then
            txtFrom.Text = "7"
        ElseIf CInt(strTo) < CInt(txtFrom.Text) Then
            txtTo.Text = txtFrom.Text
        Else
            txtTo.Text = strTo
        End If
        Me.btnMatch.Select()
    End Sub

    Private Sub BtnMatch_Click(sender As Object, e As EventArgs) Handles btnMatch.Click

#Region "進行配對"
        '20190615 tbl_lab_record連結tbl_opd_order
        Dim q = From cs In dc.sp_match_lab(CInt(txtFrom.Text), CInt(txtTo.Text)).AsEnumerable Select cs
        Dim n As String = q(0).rows_affected.ToString
        Record_adm("檢驗檔配對", n + "筆配對成功")
        MessageBox.Show("檢驗檔配對: " + n + "筆配對成功")
#End Region

        Call Renew()
    End Sub

    Private Sub BtnMatch_KeyDown(sender As Object, e As KeyEventArgs) Handles btnMatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call BtnMatch_Click(Nothing, Nothing)
        End If
    End Sub
End Class