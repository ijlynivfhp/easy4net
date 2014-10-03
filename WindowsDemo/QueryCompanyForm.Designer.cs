namespace WindowsDemo
{
    partial class QueryCompanyForm
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
            this.dgCompany = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectedRow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompany)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCompany
            // 
            this.dgCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCompany.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCompany.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectedRow,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column7});
            this.dgCompany.Location = new System.Drawing.Point(1, 5);
            this.dgCompany.MultiSelect = false;
            this.dgCompany.Name = "dgCompany";
            this.dgCompany.RowTemplate.Height = 23;
            this.dgCompany.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCompany.Size = new System.Drawing.Size(853, 570);
            this.dgCompany.TabIndex = 12;
            this.dgCompany.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCompany_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtPage);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPre);
            this.panel1.Location = new System.Drawing.Point(3, 581);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 70);
            this.panel1.TabIndex = 13;
            // 
            // txtPage
            // 
            this.txtPage.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPage.Location = new System.Drawing.Point(357, 21);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(62, 29);
            this.txtPage.TabIndex = 2;
            this.txtPage.Text = "1";
            this.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnNext.Location = new System.Drawing.Point(434, 16);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 41);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPre.Location = new System.Drawing.Point(262, 14);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(75, 41);
            this.btnPre.TabIndex = 0;
            this.btnPre.Text = "上一页";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Address";
            this.Column7.HeaderText = "公司地址";
            this.Column7.Name = "Column7";
            this.Column7.Width = 200;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Industry";
            this.Column4.HeaderText = "公司行业";
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CompanyName";
            this.Column2.HeaderText = "公司名称";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // selectedRow
            // 
            this.selectedRow.HeaderText = "";
            this.selectedRow.Name = "selectedRow";
            this.selectedRow.Width = 40;
            // 
            // QueryCompanyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 653);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgCompany);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QueryCompanyForm";
            this.Text = "QueryCompanyForm";
            this.Load += new System.EventHandler(this.QueryCompanyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCompany)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCompany;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectedRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}