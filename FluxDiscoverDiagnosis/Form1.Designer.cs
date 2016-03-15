namespace FluxDiscoverDiagnosis
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ST1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ST2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.multicastStBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multicastStBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UUID,
            this.ST1,
            this.ST2,
            this.IPAddr,
            this.TEXT});
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(819, 372);
            this.dataGridView1.TabIndex = 2;
            // 
            // UUID
            // 
            this.UUID.DataPropertyName = "UUID";
            this.UUID.HeaderText = "UUID";
            this.UUID.MinimumWidth = 300;
            this.UUID.Name = "UUID";
            this.UUID.ReadOnly = true;
            this.UUID.Width = 300;
            // 
            // ST1
            // 
            this.ST1.DataPropertyName = "ST1";
            this.ST1.HeaderText = "ST1";
            this.ST1.Name = "ST1";
            this.ST1.ReadOnly = true;
            // 
            // ST2
            // 
            this.ST2.DataPropertyName = "ST2";
            dataGridViewCellStyle1.NullValue = "N/A";
            this.ST2.DefaultCellStyle = dataGridViewCellStyle1;
            this.ST2.HeaderText = "ST2";
            this.ST2.Name = "ST2";
            // 
            // IPAddr
            // 
            this.IPAddr.DataPropertyName = "IPAddr";
            this.IPAddr.FillWeight = 130F;
            this.IPAddr.HeaderText = "IPAddr";
            this.IPAddr.Name = "IPAddr";
            this.IPAddr.Width = 130;
            // 
            // TEXT
            // 
            this.TEXT.DataPropertyName = "TEXT";
            this.TEXT.HeaderText = "TEXT";
            this.TEXT.Name = "TEXT";
            this.TEXT.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Poke ipaddr";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(203, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 5;
            this.textBox1.Tag = "";
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(FluxDiscoverDiagnosis.Form1);
            // 
            // multicastStBindingSource
            // 
            this.multicastStBindingSource.DataSource = typeof(FluxDiscoverDiagnosis.UdpStatus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(386, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "*Require  device firmware version 1.0b14 or newer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 423);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = " Flux Discover Diagnosis Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multicastStBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.BindingSource multicastStBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ST1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ST2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEXT;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

