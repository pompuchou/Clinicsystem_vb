Public Class DashBoard
    Private dc As New MISDataContext

    Private Sub DashBoard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call Renew()
    End Sub

    Private Sub Renew()
        Me.dgvAdm.DataSource = From p In dc.log_Adm Select p.regdate, p.operation_name, p.description Where operation_name <> "Log in" And operation_name <> "Log out" _
                                                                                                      And operation_name <> "update opd" And operation_name <> "update opd order" _
                                                                                                      And operation_name <> "OPD file format" _
                                                                                                      And operation_name <> "Change order data" And operation_name <> "Add a new order" _
                                                                                                      And operation_name <> "Lab file format" _
                                                                                                      And operation_name <> "Change patient data" And operation_name <> "Add a new patient"
                               Order By regdate Descending Take 10000
        Me.dgvOPD.DataSource = From p1 In dc.log_Adm Select p1.regdate, p1.operation_name, p1.description Where operation_name = "update opd" Or operation_name = "update opd order" Order By regdate Descending Take 10000
        Me.dgvOrder.DataSource = From p2 In dc.log_Adm Select p2.regdate, p2.operation_name, p2.description Where operation_name = "Change order data" Or operation_name = "Add a new order" Order By regdate Descending Take 10000
        Me.dgvPT.DataSource = From p3 In dc.log_Adm Select p3.regdate, p3.operation_name, p3.description Where operation_name = "Change patient data" Or operation_name = "Add a new patient" Order By regdate Descending Take 10000
        Me.dgvErr.DataSource = From q In dc.log_Err Select q.error_date, q.error_message Order By error_date Descending Take 1000
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call Renew()
    End Sub
End Class