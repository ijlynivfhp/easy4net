using Easy4net.Entity;
using Easy4net.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Session session = SessionFactory.GetSession("SQLiteString");
            
            //session.ConnectDB("SqlServerString");


            try
            {
                session.BeginTransaction();

                List<Company> compList = new List<Company>();

                int count = Convert.ToInt32(txtCount.Text.Trim());
                for (int i = 0; i < count; i++)
                {
                    Company company = new Company();
                    company.CompanyName = txtName.Text.Trim() + "-" + i;
                    company.Industry = txtIndustry.Text.Trim() + "-" + i;
                    company.Address = txtAddress.Text.Trim() + "-" + i;
                    company.Desc = "Description-" + i;
                    company.Order = "Order-" + i;
                    company.Created = DateTime.Now;
                    compList.Add(company);
                }

                session.Insert<Company>(compList);
                session.Commit();
                MessageBox.Show("批量新增成功！");
            }
            catch (Exception)
            {
                session.Rollback();
                MessageBox.Show("批量新增失败！");
            }
        }
    }
}
