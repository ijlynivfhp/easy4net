using Easy4net.DBUtility;
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
    public partial class AddCompanyListForm : Form
    {
        public AddCompanyListForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DBHelper dbHelper = DBHelper.getInstance();
            List<Company> compList = new List<Company>();

            int count = Convert.ToInt32(txtCount.Text.Trim());
            for(int i = 0; i < count; i++)
            { 
                Company company = new Company();
                company.CompanyName = txtName.Text.Trim() + "-" + i;
                company.Industry = txtIndustry.Text.Trim() + "-" + i;
                company.Address = txtAddress.Text.Trim() + "-" + i;
                compList.Add(company);
            }

            dbHelper.Insert<Company>(compList);

            MessageBox.Show("批量新增成功！");
        }
    }
}
