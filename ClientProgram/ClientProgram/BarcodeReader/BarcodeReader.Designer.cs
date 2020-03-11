namespace ClientProgram
{
    partial class BarcodeReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRecvMsg = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRecvMsg
            // 
            this.txtRecvMsg.Font = new System.Drawing.Font("굴림", 20F);
            this.txtRecvMsg.Location = new System.Drawing.Point(12, 24);
            this.txtRecvMsg.Name = "txtRecvMsg";
            this.txtRecvMsg.ReadOnly = true;
            this.txtRecvMsg.Size = new System.Drawing.Size(389, 38);
            this.txtRecvMsg.TabIndex = 0;
            this.txtRecvMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 99);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(413, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(33, 17);
            this.tslStatus.Text = "Load";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "바코드 수신";
            // 
            // btnEnter
            // 
            this.btnEnter.Enabled = false;
            this.btnEnter.Location = new System.Drawing.Point(303, 68);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(98, 23);
            this.btnEnter.TabIndex = 4;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // BarcodeReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 121);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtRecvMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BarcodeReader";
            this.Text = "Barcode Reader";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRecvMsg;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
    }
}