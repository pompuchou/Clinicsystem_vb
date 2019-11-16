<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LabMatch
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LabMatch))
        Me.dgvDataNoOrder = New System.Windows.Forms.DataGridView()
        Me.lid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cname2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.l05 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.iid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nhi_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.l082 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.l07 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.l09 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvOrderNoData = New System.Windows.Forms.DataGridView()
        Me.CASENO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Od_idx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.l08 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnMatch = New System.Windows.Forms.Button()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgvDataNoOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrderNoData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDataNoOrder
        '
        Me.dgvDataNoOrder.AllowUserToAddRows = False
        Me.dgvDataNoOrder.AllowUserToDeleteRows = False
        Me.dgvDataNoOrder.AllowUserToResizeColumns = False
        Me.dgvDataNoOrder.AllowUserToResizeRows = False
        Me.dgvDataNoOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDataNoOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.lid, Me.uid2, Me.cname2, Me.l05, Me.iid, Me.nhi_code, Me.l082, Me.l07, Me.l09})
        Me.dgvDataNoOrder.Location = New System.Drawing.Point(8, 68)
        Me.dgvDataNoOrder.Name = "dgvDataNoOrder"
        Me.dgvDataNoOrder.ReadOnly = True
        Me.dgvDataNoOrder.RowHeadersVisible = False
        Me.dgvDataNoOrder.RowTemplate.Height = 24
        Me.dgvDataNoOrder.Size = New System.Drawing.Size(600, 440)
        Me.dgvDataNoOrder.TabIndex = 0
        '
        'lid
        '
        Me.lid.DataPropertyName = "lid"
        Me.lid.HeaderText = "lid"
        Me.lid.Name = "lid"
        Me.lid.ReadOnly = True
        '
        'uid2
        '
        Me.uid2.DataPropertyName = "uid"
        Me.uid2.HeaderText = "身分證號"
        Me.uid2.Name = "uid2"
        Me.uid2.ReadOnly = True
        Me.uid2.Width = 80
        '
        'cname2
        '
        Me.cname2.DataPropertyName = "cname"
        Me.cname2.HeaderText = "姓名"
        Me.cname2.Name = "cname2"
        Me.cname2.ReadOnly = True
        Me.cname2.Width = 70
        '
        'l05
        '
        Me.l05.DataPropertyName = "l05"
        Me.l05.HeaderText = "日期"
        Me.l05.Name = "l05"
        Me.l05.ReadOnly = True
        Me.l05.Width = 60
        '
        'iid
        '
        Me.iid.DataPropertyName = "iid"
        Me.iid.HeaderText = "iid"
        Me.iid.Name = "iid"
        Me.iid.ReadOnly = True
        Me.iid.Visible = False
        '
        'nhi_code
        '
        Me.nhi_code.DataPropertyName = "nhi_code"
        Me.nhi_code.HeaderText = "醫令"
        Me.nhi_code.Name = "nhi_code"
        Me.nhi_code.ReadOnly = True
        Me.nhi_code.Width = 60
        '
        'l082
        '
        Me.l082.DataPropertyName = "l08"
        Me.l082.HeaderText = "醫令名稱"
        Me.l082.Name = "l082"
        Me.l082.ReadOnly = True
        Me.l082.Width = 120
        '
        'l07
        '
        Me.l07.DataPropertyName = "l07"
        Me.l07.HeaderText = "結果"
        Me.l07.Name = "l07"
        Me.l07.ReadOnly = True
        Me.l07.Width = 60
        '
        'l09
        '
        Me.l09.DataPropertyName = "l09"
        Me.l09.HeaderText = ""
        Me.l09.Name = "l09"
        Me.l09.ReadOnly = True
        Me.l09.Width = 30
        '
        'dgvOrderNoData
        '
        Me.dgvOrderNoData.AllowUserToAddRows = False
        Me.dgvOrderNoData.AllowUserToDeleteRows = False
        Me.dgvOrderNoData.AllowUserToResizeColumns = False
        Me.dgvOrderNoData.AllowUserToResizeRows = False
        Me.dgvOrderNoData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrderNoData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CASENO, Me.uid, Me.cname, Me.SDATE, Me.Od_idx, Me.rid, Me.l08})
        Me.dgvOrderNoData.Location = New System.Drawing.Point(614, 68)
        Me.dgvOrderNoData.Name = "dgvOrderNoData"
        Me.dgvOrderNoData.ReadOnly = True
        Me.dgvOrderNoData.RowHeadersVisible = False
        Me.dgvOrderNoData.RowTemplate.Height = 24
        Me.dgvOrderNoData.Size = New System.Drawing.Size(540, 440)
        Me.dgvOrderNoData.TabIndex = 1
        '
        'CASENO
        '
        Me.CASENO.DataPropertyName = "CASENO"
        Me.CASENO.HeaderText = "CASENO"
        Me.CASENO.Name = "CASENO"
        Me.CASENO.ReadOnly = True
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "身分證號"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Width = 80
        '
        'cname
        '
        Me.cname.DataPropertyName = "cname"
        Me.cname.HeaderText = "姓名"
        Me.cname.Name = "cname"
        Me.cname.ReadOnly = True
        Me.cname.Width = 70
        '
        'SDATE
        '
        Me.SDATE.DataPropertyName = "SDATE"
        Me.SDATE.HeaderText = "日期"
        Me.SDATE.Name = "SDATE"
        Me.SDATE.ReadOnly = True
        Me.SDATE.Width = 60
        '
        'Od_idx
        '
        Me.Od_idx.DataPropertyName = "Od_idx"
        Me.Od_idx.HeaderText = "序"
        Me.Od_idx.Name = "Od_idx"
        Me.Od_idx.ReadOnly = True
        Me.Od_idx.Width = 25
        '
        'rid
        '
        Me.rid.DataPropertyName = "rid"
        Me.rid.HeaderText = "醫令"
        Me.rid.Name = "rid"
        Me.rid.ReadOnly = True
        Me.rid.Width = 60
        '
        'l08
        '
        Me.l08.DataPropertyName = "l08"
        Me.l08.HeaderText = "醫令名稱"
        Me.l08.Name = "l08"
        Me.l08.ReadOnly = True
        Me.l08.Width = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(737, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 47)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "有處方沒檢驗結果"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(98, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(316, 47)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "有檢驗結果沒處方"
        '
        'btnMatch
        '
        Me.btnMatch.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnMatch.Location = New System.Drawing.Point(628, 16)
        Me.btnMatch.Name = "btnMatch"
        Me.btnMatch.Size = New System.Drawing.Size(75, 35)
        Me.btnMatch.TabIndex = 4
        Me.btnMatch.Text = "配對"
        Me.btnMatch.UseVisualStyleBackColor = True
        '
        'txtFrom
        '
        Me.txtFrom.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(482, 7)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.ReadOnly = True
        Me.txtFrom.Size = New System.Drawing.Size(52, 53)
        Me.txtFrom.TabIndex = 5
        Me.txtFrom.Text = "0"
        '
        'txtTo
        '
        Me.txtTo.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(565, 7)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(52, 53)
        Me.txtTo.TabIndex = 6
        Me.txtTo.Text = "3"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(530, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 45)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "~"
        '
        'LabMatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1164, 517)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.txtFrom)
        Me.Controls.Add(Me.btnMatch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvOrderNoData)
        Me.Controls.Add(Me.dgvDataNoOrder)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1180, 555)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1180, 555)
        Me.Name = "LabMatch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "檢驗值配對"
        CType(Me.dgvDataNoOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrderNoData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvDataNoOrder As DataGridView
    Friend WithEvents dgvOrderNoData As DataGridView
    Friend WithEvents CASENO As DataGridViewTextBoxColumn
    Friend WithEvents uid As DataGridViewTextBoxColumn
    Friend WithEvents cname As DataGridViewTextBoxColumn
    Friend WithEvents SDATE As DataGridViewTextBoxColumn
    Friend WithEvents Od_idx As DataGridViewTextBoxColumn
    Friend WithEvents rid As DataGridViewTextBoxColumn
    Friend WithEvents l08 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnMatch As Button
    Friend WithEvents txtFrom As TextBox
    Friend WithEvents txtTo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lid As DataGridViewTextBoxColumn
    Friend WithEvents uid2 As DataGridViewTextBoxColumn
    Friend WithEvents cname2 As DataGridViewTextBoxColumn
    Friend WithEvents l05 As DataGridViewTextBoxColumn
    Friend WithEvents iid As DataGridViewTextBoxColumn
    Friend WithEvents nhi_code As DataGridViewTextBoxColumn
    Friend WithEvents l082 As DataGridViewTextBoxColumn
    Friend WithEvents l07 As DataGridViewTextBoxColumn
    Friend WithEvents l09 As DataGridViewTextBoxColumn
End Class
