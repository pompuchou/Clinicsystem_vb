<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.lbl03 = New System.Windows.Forms.Label()
        Me.lbl02 = New System.Windows.Forms.Label()
        Me.lbl01 = New System.Windows.Forms.Label()
        Me.btnCDep = New System.Windows.Forms.Button()
        Me.btnOPD_auto = New System.Windows.Forms.Button()
        Me.btnLabXML = New System.Windows.Forms.Button()
        Me.btnCombine = New System.Windows.Forms.Button()
        Me.btnXML = New System.Windows.Forms.Button()
        Me.btnOrder = New System.Windows.Forms.Button()
        Me.btnPatient_auto = New System.Windows.Forms.Button()
        Me.btnLab = New System.Windows.Forms.Button()
        Me.btnOPD = New System.Windows.Forms.Button()
        Me.btnPatient = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btnPijia = New System.Windows.Forms.Button()
        Me.btnOrder_auto = New System.Windows.Forms.Button()
        Me.btnCombo = New System.Windows.Forms.Button()
        Me.chkDash = New System.Windows.Forms.CheckBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvAdm = New System.Windows.Forms.DataGridView()
        Me.regdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.operation_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvOPD = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvOrder = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvPT = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvCD = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvLab = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgvPijia = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvUpload = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvAdm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOPD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPijia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl03
        '
        Me.lbl03.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lbl03.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl03.Location = New System.Drawing.Point(224, 9)
        Me.lbl03.Name = "lbl03"
        Me.lbl03.Size = New System.Drawing.Size(100, 20)
        Me.lbl03.TabIndex = 15
        Me.lbl03.Text = "手動"
        Me.lbl03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl02
        '
        Me.lbl02.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lbl02.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl02.Location = New System.Drawing.Point(118, 9)
        Me.lbl02.Name = "lbl02"
        Me.lbl02.Size = New System.Drawing.Size(100, 20)
        Me.lbl02.TabIndex = 14
        Me.lbl02.Text = "每月工作"
        Me.lbl02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl01
        '
        Me.lbl01.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lbl01.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl01.Location = New System.Drawing.Point(12, 9)
        Me.lbl01.Name = "lbl01"
        Me.lbl01.Size = New System.Drawing.Size(100, 20)
        Me.lbl01.TabIndex = 13
        Me.lbl01.Text = "每日工作"
        Me.lbl01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCDep
        '
        Me.btnCDep.Location = New System.Drawing.Point(12, 148)
        Me.btnCDep.Name = "btnCDep"
        Me.btnCDep.Size = New System.Drawing.Size(100, 23)
        Me.btnCDep.TabIndex = 25
        Me.btnCDep.Text = "調整科別"
        Me.btnCDep.UseVisualStyleBackColor = True
        '
        'btnOPD_auto
        '
        Me.btnOPD_auto.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnOPD_auto.Location = New System.Drawing.Point(12, 32)
        Me.btnOPD_auto.Name = "btnOPD_auto"
        Me.btnOPD_auto.Size = New System.Drawing.Size(100, 23)
        Me.btnOPD_auto.TabIndex = 24
        Me.btnOPD_auto.Text = "門診(自動)"
        Me.btnOPD_auto.UseVisualStyleBackColor = True
        '
        'btnLabXML
        '
        Me.btnLabXML.Location = New System.Drawing.Point(118, 32)
        Me.btnLabXML.Name = "btnLabXML"
        Me.btnLabXML.Size = New System.Drawing.Size(100, 23)
        Me.btnLabXML.TabIndex = 23
        Me.btnLabXML.Text = "製做檢驗上傳"
        Me.btnLabXML.UseVisualStyleBackColor = True
        '
        'btnCombine
        '
        Me.btnCombine.Location = New System.Drawing.Point(330, 32)
        Me.btnCombine.Name = "btnCombine"
        Me.btnCombine.Size = New System.Drawing.Size(100, 23)
        Me.btnCombine.TabIndex = 22
        Me.btnCombine.Text = "檢驗配對"
        Me.btnCombine.UseVisualStyleBackColor = True
        '
        'btnXML
        '
        Me.btnXML.Location = New System.Drawing.Point(118, 61)
        Me.btnXML.Name = "btnXML"
        Me.btnXML.Size = New System.Drawing.Size(100, 23)
        Me.btnXML.TabIndex = 21
        Me.btnXML.Text = "申報匯入"
        Me.btnXML.UseVisualStyleBackColor = True
        '
        'btnOrder
        '
        Me.btnOrder.Location = New System.Drawing.Point(224, 90)
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(100, 23)
        Me.btnOrder.TabIndex = 20
        Me.btnOrder.Text = "醫令"
        Me.btnOrder.UseVisualStyleBackColor = True
        '
        'btnPatient_auto
        '
        Me.btnPatient_auto.Location = New System.Drawing.Point(12, 61)
        Me.btnPatient_auto.Name = "btnPatient_auto"
        Me.btnPatient_auto.Size = New System.Drawing.Size(100, 23)
        Me.btnPatient_auto.TabIndex = 19
        Me.btnPatient_auto.Text = "病患(自動)"
        Me.btnPatient_auto.UseVisualStyleBackColor = True
        '
        'btnLab
        '
        Me.btnLab.Location = New System.Drawing.Point(224, 119)
        Me.btnLab.Name = "btnLab"
        Me.btnLab.Size = New System.Drawing.Size(100, 23)
        Me.btnLab.TabIndex = 18
        Me.btnLab.Text = "檢驗"
        Me.btnLab.UseVisualStyleBackColor = True
        '
        'btnOPD
        '
        Me.btnOPD.Location = New System.Drawing.Point(224, 32)
        Me.btnOPD.Name = "btnOPD"
        Me.btnOPD.Size = New System.Drawing.Size(100, 23)
        Me.btnOPD.TabIndex = 17
        Me.btnOPD.Text = "門診"
        Me.btnOPD.UseVisualStyleBackColor = True
        '
        'btnPatient
        '
        Me.btnPatient.Location = New System.Drawing.Point(224, 61)
        Me.btnPatient.Name = "btnPatient"
        Me.btnPatient.Size = New System.Drawing.Size(100, 23)
        Me.btnPatient.TabIndex = 16
        Me.btnPatient.Text = "病患"
        Me.btnPatient.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 347)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(506, 23)
        Me.ProgressBar1.TabIndex = 26
        Me.ProgressBar1.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Excel|*.xls;*.xlsx|XML|*.xml|Text|*.txt;*.xls;*.xlsx|All Files|*.*"
        '
        'btnPijia
        '
        Me.btnPijia.Location = New System.Drawing.Point(12, 119)
        Me.btnPijia.Name = "btnPijia"
        Me.btnPijia.Size = New System.Drawing.Size(100, 23)
        Me.btnPijia.TabIndex = 27
        Me.btnPijia.Text = "匯入批價檔"
        Me.btnPijia.UseVisualStyleBackColor = True
        '
        'btnOrder_auto
        '
        Me.btnOrder_auto.Location = New System.Drawing.Point(12, 90)
        Me.btnOrder_auto.Name = "btnOrder_auto"
        Me.btnOrder_auto.Size = New System.Drawing.Size(100, 23)
        Me.btnOrder_auto.TabIndex = 28
        Me.btnOrder_auto.Text = "醫令(自動)"
        Me.btnOrder_auto.UseVisualStyleBackColor = True
        '
        'btnCombo
        '
        Me.btnCombo.Location = New System.Drawing.Point(330, 61)
        Me.btnCombo.Name = "btnCombo"
        Me.btnCombo.Size = New System.Drawing.Size(100, 23)
        Me.btnCombo.TabIndex = 29
        Me.btnCombo.Text = "組合拳"
        Me.btnCombo.UseVisualStyleBackColor = True
        '
        'chkDash
        '
        Me.chkDash.AutoSize = True
        Me.chkDash.Location = New System.Drawing.Point(331, 9)
        Me.chkDash.Name = "chkDash"
        Me.chkDash.Size = New System.Drawing.Size(48, 16)
        Me.chkDash.TabIndex = 30
        Me.chkDash.Text = "面板"
        Me.chkDash.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(375, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(55, 23)
        Me.btnRefresh.TabIndex = 43
        Me.btnRefresh.Text = "更新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgvAdm
        '
        Me.dgvAdm.AllowUserToAddRows = False
        Me.dgvAdm.AllowUserToDeleteRows = False
        Me.dgvAdm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAdm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.regdate, Me.operation_name})
        Me.dgvAdm.Location = New System.Drawing.Point(467, 6)
        Me.dgvAdm.Name = "dgvAdm"
        Me.dgvAdm.ReadOnly = True
        Me.dgvAdm.RowHeadersVisible = False
        Me.dgvAdm.RowTemplate.Height = 24
        Me.dgvAdm.Size = New System.Drawing.Size(200, 180)
        Me.dgvAdm.TabIndex = 44
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
        Me.operation_name.MinimumWidth = 60
        Me.operation_name.Name = "operation_name"
        Me.operation_name.ReadOnly = True
        Me.operation_name.Width = 60
        '
        'dgvOPD
        '
        Me.dgvOPD.AllowUserToAddRows = False
        Me.dgvOPD.AllowUserToDeleteRows = False
        Me.dgvOPD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOPD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column1})
        Me.dgvOPD.Location = New System.Drawing.Point(673, 36)
        Me.dgvOPD.Name = "dgvOPD"
        Me.dgvOPD.ReadOnly = True
        Me.dgvOPD.RowHeadersVisible = False
        Me.dgvOPD.RowTemplate.Height = 24
        Me.dgvOPD.Size = New System.Drawing.Size(140, 150)
        Me.dgvOPD.TabIndex = 45
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(673, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "門診匯入"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(965, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 20)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "醫令匯入"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvOrder
        '
        Me.dgvOrder.AllowUserToAddRows = False
        Me.dgvOrder.AllowUserToDeleteRows = False
        Me.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.dgvOrder.Location = New System.Drawing.Point(965, 36)
        Me.dgvOrder.Name = "dgvOrder"
        Me.dgvOrder.ReadOnly = True
        Me.dgvOrder.RowHeadersVisible = False
        Me.dgvOrder.RowTemplate.Height = 24
        Me.dgvOrder.Size = New System.Drawing.Size(140, 150)
        Me.dgvOrder.TabIndex = 47
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn2.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(819, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "病患匯入"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvPT
        '
        Me.dgvPT.AllowUserToAddRows = False
        Me.dgvPT.AllowUserToDeleteRows = False
        Me.dgvPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        Me.dgvPT.Location = New System.Drawing.Point(819, 36)
        Me.dgvPT.Name = "dgvPT"
        Me.dgvPT.ReadOnly = True
        Me.dgvPT.RowHeadersVisible = False
        Me.dgvPT.RowTemplate.Height = 24
        Me.dgvPT.Size = New System.Drawing.Size(140, 150)
        Me.dgvPT.TabIndex = 49
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn4.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label4.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(819, 193)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "調整科別"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvCD
        '
        Me.dgvCD.AllowUserToAddRows = False
        Me.dgvCD.AllowUserToDeleteRows = False
        Me.dgvCD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7})
        Me.dgvCD.Location = New System.Drawing.Point(819, 220)
        Me.dgvCD.Name = "dgvCD"
        Me.dgvCD.ReadOnly = True
        Me.dgvCD.RowHeadersVisible = False
        Me.dgvCD.RowTemplate.Height = 24
        Me.dgvCD.Size = New System.Drawing.Size(140, 150)
        Me.dgvCD.TabIndex = 55
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn6.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label5.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(965, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 20)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "檢驗匯入"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvLab
        '
        Me.dgvLab.AllowUserToAddRows = False
        Me.dgvLab.AllowUserToDeleteRows = False
        Me.dgvLab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLab.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.dgvLab.Location = New System.Drawing.Point(965, 220)
        Me.dgvLab.Name = "dgvLab"
        Me.dgvLab.ReadOnly = True
        Me.dgvLab.RowHeadersVisible = False
        Me.dgvLab.RowTemplate.Height = 24
        Me.dgvLab.Size = New System.Drawing.Size(140, 150)
        Me.dgvLab.TabIndex = 53
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn8.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label6.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(673, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 20)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "批價匯入"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvPijia
        '
        Me.dgvPijia.AllowUserToAddRows = False
        Me.dgvPijia.AllowUserToDeleteRows = False
        Me.dgvPijia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPijia.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11})
        Me.dgvPijia.Location = New System.Drawing.Point(673, 220)
        Me.dgvPijia.Name = "dgvPijia"
        Me.dgvPijia.ReadOnly = True
        Me.dgvPijia.RowHeadersVisible = False
        Me.dgvPijia.RowTemplate.Height = 24
        Me.dgvPijia.Size = New System.Drawing.Size(140, 150)
        Me.dgvPijia.TabIndex = 51
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn10.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 120
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(527, 193)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 20)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "申報檔匯入"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvUpload
        '
        Me.dgvUpload.AllowUserToAddRows = False
        Me.dgvUpload.AllowUserToDeleteRows = False
        Me.dgvUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUpload.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn17})
        Me.dgvUpload.Location = New System.Drawing.Point(527, 220)
        Me.dgvUpload.Name = "dgvUpload"
        Me.dgvUpload.ReadOnly = True
        Me.dgvUpload.RowHeadersVisible = False
        Me.dgvUpload.RowTemplate.Height = 24
        Me.dgvUpload.Size = New System.Drawing.Size(140, 150)
        Me.dgvUpload.TabIndex = 57
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "regdate"
        Me.DataGridViewTextBoxColumn16.HeaderText = "日期"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 120
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "operation_name"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1117, 379)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dgvUpload)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dgvCD)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dgvLab)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dgvPijia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvPT)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvOrder)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvOPD)
        Me.Controls.Add(Me.dgvAdm)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.chkDash)
        Me.Controls.Add(Me.btnCombo)
        Me.Controls.Add(Me.btnOrder_auto)
        Me.Controls.Add(Me.btnPijia)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnCDep)
        Me.Controls.Add(Me.btnOPD_auto)
        Me.Controls.Add(Me.btnLabXML)
        Me.Controls.Add(Me.btnCombine)
        Me.Controls.Add(Me.btnXML)
        Me.Controls.Add(Me.btnOrder)
        Me.Controls.Add(Me.btnPatient_auto)
        Me.Controls.Add(Me.btnLab)
        Me.Controls.Add(Me.btnOPD)
        Me.Controls.Add(Me.btnPatient)
        Me.Controls.Add(Me.lbl03)
        Me.Controls.Add(Me.lbl02)
        Me.Controls.Add(Me.lbl01)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "周孫元診所管理系統"
        CType(Me.dgvAdm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOPD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPijia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUpload, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbl03 As Label
    Friend WithEvents lbl02 As Label
    Friend WithEvents lbl01 As Label
    Friend WithEvents btnCDep As Button
    Friend WithEvents btnOPD_auto As Button
    Friend WithEvents btnLabXML As Button
    Friend WithEvents btnCombine As Button
    Friend WithEvents btnXML As Button
    Friend WithEvents btnOrder As Button
    Friend WithEvents btnPatient_auto As Button
    Friend WithEvents btnLab As Button
    Friend WithEvents btnOPD As Button
    Friend WithEvents btnPatient As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents btnPijia As Button
    Friend WithEvents btnOrder_auto As Button
    Friend WithEvents btnCombo As Button
    Friend WithEvents chkDash As CheckBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvAdm As DataGridView
    Friend WithEvents regdate As DataGridViewTextBoxColumn
    Friend WithEvents operation_name As DataGridViewTextBoxColumn
    Friend WithEvents dgvOPD As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents dgvOrder As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvPT As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents Label4 As Label
    Friend WithEvents dgvCD As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents Label5 As Label
    Friend WithEvents dgvLab As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents Label6 As Label
    Friend WithEvents dgvPijia As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents Label9 As Label
    Friend WithEvents dgvUpload As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
End Class
