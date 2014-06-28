namespace ATS.Service.WCFClient
{
    partial class LeaveForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StaffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Approved = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pending = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Leave Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.StaffName,
            this.Approved,
            this.Pending,
            this.Reject,
            this.Total});
            this.dataGridView1.Location = new System.Drawing.Point(26, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(547, 150);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Year";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Month";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(68, 17);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(100, 20);
            this.tbYear.TabIndex = 5;
            this.tbYear.Text = "2014";
            this.tbYear.TextChanged += new System.EventHandler(this.tbYear_TextChanged);
            // 
            // tbMonth
            // 
            this.tbMonth.Location = new System.Drawing.Point(231, 15);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.Size = new System.Drawing.Size(100, 20);
            this.tbMonth.TabIndex = 6;
            this.tbMonth.Text = "6";
            this.tbMonth.TextChanged += new System.EventHandler(this.tbMonth_TextChanged);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // StaffName
            // 
            this.StaffName.HeaderText = "Staff Name";
            this.StaffName.Name = "StaffName";
            // 
            // Approved
            // 
            this.Approved.HeaderText = "Approved";
            this.Approved.Name = "Approved";
            // 
            // Pending
            // 
            this.Pending.HeaderText = "Pending";
            this.Pending.Name = "Pending";
            // 
            // Reject
            // 
            this.Reject.HeaderText = "Reject";
            this.Reject.Name = "Reject";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // LeaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 285);
            this.Controls.Add(this.tbMonth);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "LeaveForm";
            this.Text = "LeaveForm";
            this.Load += new System.EventHandler(this.LeaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaffName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Approved;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}