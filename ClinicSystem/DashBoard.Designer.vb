<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashBoard
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DashBoard))
        Me.dgvAdm = New System.Windows.Forms.DataGridView()
        Me.dgvErr = New System.Windows.Forms.DataGridView()
        Me.dgvOPD = New System.Windows.Forms.DataGridView()
        Me.err_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.err_message = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvOrder = New System.Windows.Forms.DataGridView()
        Me.dgvPT = New System.Windows.Forms.DataGridView()
        Me.lbl01 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.regdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.operation_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvAdm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvErr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOPD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvAdm
        '
        Me.dgvAdm.AllowUserToAddRows = False
        Me.dgvAdm.AllowUserToDeleteRows = False
        Me.dgvAdm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAdm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.regdate, Me.operation_name, Me.description})
        Me.dgvAdm.Location = New System.Drawing.Point(13, 30)
        Me.dgvAdm.Name = "dgvAdm"
        Me.dgvAdm.ReadOnly = True
        Me.dgvAdm.RowHeadersVisible = False
        Me.dgvAdm.RowTemplate.Height = 24
        Me.dgvAdm.Size = New System.Drawing.Size(560, 240)
        Me.dgvAdm.TabIndex = 0
        '
        'dgvErr
        '
        Me.dgvErr.AllowUserToAddRows = False
        Me.dgvErr.AllowUserToDeleteRows = False
        Me.dgvErr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErr.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.err_date, Me.err_message})
        Me.dgvErr.Location = New System.Drawing.Point(579, 30)
        Me.dgvErr.Name = "dgvErr"
        Me.dgvErr.ReadOnly = True
        Me.dgvErr.RowHeadersVisible = False
        Me.dgvErr.RowTemplate.Height = 24
        Me.dgvErr.Size = New System.Drawing.Size(440, 240)
        Me.dgvErr.TabIndex = 1
        '
        'dgvOPD
        '
        Me.dgvOPD.AllowUserToAddRows = False
        Me.dgvOPD.AllowUserToDeleteRows = False
        Me.dgvOPD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOPD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column1, Me.DataGridViewTextBoxColumn3})
        Me.dgvOPD.Location = New System.Drawing.Point(13, 308)
        Me.dgvOPD.Name = "dgvOPD"
        Me.dgvOPD.ReadOnly = True
        Me.dgvOPD.RowHeadersVisible = False
        Me.dgvOPD.RowTemplate.Height = 24
        Me.dgvOPD.Size = New System.Drawing.Size(440, 240)
        Me.dgvOPD.TabIndex = 2
        '
        'err_date
        '
        Me.err_date.DataPropertyName = "error_date"
        Me.err_date.HeaderText = "日期"
        Me.err_date.Name = "err_date"
        Me.err_date.ReadOnly = True
        Me.err_date.Width = 120
        '
        'err_message
        '
        Me.err_message.DataPropertyName = "error_message"
        Me.err_message.HeaderText = "錯誤訊息"
        Me.err_message.Name = "err_message"
        Me.err_message.ReadOnly = True
        Me.err_message.Width = 300
        '
        'dgvOrder
        '
        Me.dgvOrder.AllowUserToAddRows = False
        Me.dgvOrder.AllowUserToDeleteRows = False
        Me.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.Column3, Me.DataGridViewTextBoxColumn4})
        Me.dgvOrder.Location = New System.Drawing.Point(905, 308)
        Me.dgvOrder.Name = "dgvOrder"
        Me.dgvOrder.ReadOnly = True
        Me.dgvOrder.RowHeadersVisible = False
        Me.dgvOrder.RowTemplate.Height = 24
        Me.dgvOrder.Size = New System.Drawing.Size(440, 240)
        Me.dgvOrder.TabIndex = 3
        '
        'dgvPT
        '
        Me.dgvPT.AllowUserToAddRows = False
        Me.dgvPT.AllowUserToDeleteRows = False
        Me.dgvPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.Column2, Me.DataGridViewTextBoxColumn6})
        Me.dgvPT.Location = New System.Drawing.Point(459, 308)
        Me.dgvPT.Name = "dgvPT"
        Me.dgvPT.ReadOnly = True
        Me.dgvPT.RowHeadersVisible = False
        Me.dgvPT.RowTemplate.Height = 24
        Me.dgvPT.Size = New System.Drawing.Size(440, 240)
        Me.dgvPT.TabIndex = 4
        '
        'lbl01
        '
        Me.lbl01.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lbl01.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl01.Location = New System.Drawing.Point(12, 285)
        Me.lbl01.Name = "lbl01"
        Me.lbl01.Size = New System.Drawing.Size(124, 20)
        Me.lbl01.TabIndex = 14
        Me.lbl01.Text = "門診修改紀錄"
        Me.lbl01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(462, 285)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "病患新增修改紀錄"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(907, 285)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "醫令新增修改紀錄"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'regdate
        '
        Me.regdate.DataPropertyName = "regdate"
        Me.regdate.HeaderText = "日期"
        Me.regdate.Name = "regdate"
        Me.regdate.ReadOnly = True
        Me.regdate.Width = 120
        '
        'operation_name
        '
        Me.operation_name.DataPropertyName = "operation_name"
        Me.operation_name.HeaderText = "操作"
        Me.operation_name.MinimumWidth = 120
        Me.operation_name.Name = "operation_name"
        Me.operation_name.ReadOnly = True
        Me.operation_name.Width = 120
        '
        'description
        '
        Me.description.DataPropertyName = "description"
        Me.description.HeaderText = "說明"
        Me.description.MinimumWidth = 300
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 300
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(580, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(145, 20)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "錯誤訊息"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label4.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 20)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "維護紀錄"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(1038, 8)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(55, 23)
        Me.btnRefresh.TabIndex = 44
        Me.btnRefresh.Text = "更新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn1.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "operation_name"
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "說明"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 300
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 300
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn5.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "operation_name"
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "description"
        Me.DataGridViewTextBoxColumn6.HeaderText = "說明"
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 300
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 300
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn2.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "operation_name"
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "description"
        Me.DataGridViewTextBoxColumn4.HeaderText = "說明"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 300
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 300
        '
        'DashBoard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1352, 555)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl01)
        Me.Controls.Add(Me.dgvPT)
        Me.Controls.Add(Me.dgvOrder)
        Me.Controls.Add(Me.dgvOPD)
        Me.Controls.Add(Me.dgvErr)
        Me.Controls.Add(Me.dgvAdm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DashBoard"
        Me.Text = "面板"
        CType(Me.dgvAdm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvErr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOPD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvAdm As DataGridView
    Friend WithEvents dgvErr As DataGridView
    Friend WithEvents err_date As DataGridViewTextBoxColumn
    Friend WithEvents err_message As DataGridViewTextBoxColumn
    Friend WithEvents dgvOPD As DataGridView
    Friend WithEvents dgvOrder As DataGridView
    Friend WithEvents dgvPT As DataGridView
    Friend WithEvents regdate As DataGridViewTextBoxColumn
    Friend WithEvents operation_name As DataGridViewTextBoxColumn
    Friend WithEvents description As DataGridViewTextBoxColumn
    Friend WithEvents lbl01 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
End Class
