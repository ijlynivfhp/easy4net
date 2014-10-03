﻿using Easy4net.DBUtility;
using Easy4net.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsDemo
{
    public partial class AddCompanyForm : Form
    {
        public AddCompanyForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.CompanyName = txtName.Text.Trim();
            company.Industry = txtIndustry.Text.Trim();
            company.Address = txtAddress.Text.Trim();

            DBHelper dbHelper = DBHelper.getInstance();
            dbHelper.Save<Company>(company);

            if (company.Id > 0) {
                MessageBox.Show("创建公司成功！");
            }

        }
    }
}
