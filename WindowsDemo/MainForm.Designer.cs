namespace WindowsDemo
{
    partial class MainForm
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
            this.plContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddCompanyList = new System.Windows.Forms.Button();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnAddCompany = new System.Windows.Forms.Button();
            this.btnEmployeeSearch = new System.Windows.Forms.Button();
            this.btnCompanySearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plContainer
            // 
            this.plContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContainer.Location = new System.Drawing.Point(118, 0);
            this.plContainer.Name = "plContainer";
            this.plContainer.Size = new System.Drawing.Size(791, 655);
            this.plContainer.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.btnAddCompanyList);
            this.panel1.Controls.Add(this.btnAddEmployee);
            this.panel1.Controls.Add(this.btnAddCompany);
            this.panel1.Controls.Add(this.btnEmployeeSearch);
            this.panel1.Controls.Add(this.btnCompanySearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(118, 655);
            this.panel1.TabIndex = 2;
            // 
            // btnAddCompanyList
            // 
            this.btnAddCompanyList.Location = new System.Drawing.Point(0, 443);
            this.btnAddCompanyList.Name = "btnAddCompanyList";
            this.btnAddCompanyList.Size = new System.Drawing.Size(118, 100);
            this.btnAddCompanyList.TabIndex = 4;
            this.btnAddCompanyList.Text = "批量新增公司";
            this.btnAddCompanyList.UseVisualStyleBackColor = true;
            this.btnAddCompanyList.Click += new System.EventHandler(this.btnAddCompanyList_Click);
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(0, 348);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(118, 100);
            this.btnAddEmployee.TabIndex = 3;
            this.btnAddEmployee.Text = "新增员工";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // btnAddCompany
            // 
            this.btnAddCompany.Location = new System.Drawing.Point(0, 172);
            this.btnAddCompany.Name = "btnAddCompany";
            this.btnAddCompany.Size = new System.Drawing.Size(118, 176);
            this.btnAddCompany.TabIndex = 2;
            this.btnAddCompany.Text = "新增公司          (事务提交)";
            this.btnAddCompany.UseVisualStyleBackColor = true;
            this.btnAddCompany.Click += new System.EventHandler(this.btnAddCompany_Click);
            // 
            // btnEmployeeSearch
            // 
            this.btnEmployeeSearch.Location = new System.Drawing.Point(0, 92);
            this.btnEmployeeSearch.Name = "btnEmployeeSearch";
            this.btnEmployeeSearch.Size = new System.Drawing.Size(118, 84);
            this.btnEmployeeSearch.TabIndex = 1;
            this.btnEmployeeSearch.Text = "员工查询";
            this.btnEmployeeSearch.UseVisualStyleBackColor = true;
            this.btnEmployeeSearch.Click += new System.EventHandler(this.btnEmployeeSearch_Click);
            // 
            // btnCompanySearch
            // 
            this.btnCompanySearch.Location = new System.Drawing.Point(0, 0);
            this.btnCompanySearch.Name = "btnCompanySearch";
            this.btnCompanySearch.Size = new System.Drawing.Size(118, 95);
            this.btnCompanySearch.TabIndex = 0;
            this.btnCompanySearch.Text = "公司查询";
            this.btnCompanySearch.UseVisualStyleBackColor = true;
            this.btnCompanySearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 655);
            this.Controls.Add(this.plContainer);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnAddCompany;
        private System.Windows.Forms.Button btnEmployeeSearch;
        private System.Windows.Forms.Button btnCompanySearch;
        private System.Windows.Forms.Button btnAddCompanyList;
    }
}