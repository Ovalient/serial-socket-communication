namespace ServerProgram
{
    partial class ServerProgram
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvRecvMsg = new System.Windows.Forms.DataGridView();
            this.checkboxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecvMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 31);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.65957F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.34043F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtIpAddress, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtPortNo, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(343, 29);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP Address : Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIpAddress.Location = new System.Drawing.Point(124, 4);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.ReadOnly = true;
            this.txtIpAddress.Size = new System.Drawing.Size(125, 21);
            this.txtIpAddress.TabIndex = 1;
            this.txtIpAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPortNo
            // 
            this.txtPortNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPortNo.Location = new System.Drawing.Point(266, 4);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.ReadOnly = true;
            this.txtPortNo.Size = new System.Drawing.Size(73, 21);
            this.txtPortNo.TabIndex = 1;
            this.txtPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(253, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(9, 27);
            this.label5.TabIndex = 1;
            this.label5.Text = ":";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(345, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(33, 17);
            this.tslStatus.Text = "Load";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(345, 449);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbbFilter);
            this.groupBox2.Controls.Add(this.txtFilter);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 103);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add-ons";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "Filter";
            // 
            // cbbFilter
            // 
            this.cbbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFilter.FormattingEnabled = true;
            this.cbbFilter.Items.AddRange(new object[] {
            "Date",
            "Barcode",
            "IP Address"});
            this.cbbFilter.Location = new System.Drawing.Point(9, 39);
            this.cbbFilter.Name = "cbbFilter";
            this.cbbFilter.Size = new System.Drawing.Size(128, 20);
            this.cbbFilter.TabIndex = 29;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(143, 39);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(187, 21);
            this.txtFilter.TabIndex = 26;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(221, 74);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(109, 23);
            this.btnExport.TabIndex = 28;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvRecvMsg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 327);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Received client messages";
            // 
            // dgvRecvMsg
            // 
            this.dgvRecvMsg.AllowUserToAddRows = false;
            this.dgvRecvMsg.AllowUserToDeleteRows = false;
            this.dgvRecvMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecvMsg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkboxColumn});
            this.dgvRecvMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecvMsg.Location = new System.Drawing.Point(3, 17);
            this.dgvRecvMsg.Name = "dgvRecvMsg";
            this.dgvRecvMsg.RowHeadersVisible = false;
            this.dgvRecvMsg.RowTemplate.Height = 23;
            this.dgvRecvMsg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecvMsg.Size = new System.Drawing.Size(333, 307);
            this.dgvRecvMsg.TabIndex = 0;
            // 
            // checkboxColumn
            // 
            this.checkboxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.checkboxColumn.Frozen = true;
            this.checkboxColumn.HeaderText = "";
            this.checkboxColumn.Name = "checkboxColumn";
            this.checkboxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkboxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.checkboxColumn.Width = 19;
            // 
            // ServerProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 502);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Name = "ServerProgram";
            this.Text = "Server Program";
            this.Load += new System.EventHandler(this.ServerProgram_Load);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecvMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkboxColumn;
        public System.Windows.Forms.DataGridView dgvRecvMsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnExport;
    }
}

