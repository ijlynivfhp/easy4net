using Easy4net.Common;
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
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private List<Company> m_CompanyList = null;

        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            InitCompanySelections();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Company company = m_CompanyList[cbCompany.SelectedIndex];
            
            Employee employee = new Employee();
            employee.Name = txtName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtAge.Text))
            {
                employee.Age = Convert.ToInt32(txtAge.Text.Trim());
            }

            employee.Address = txtAddress.Text.Trim();
            employee.Created = DateTime.Now;
            employee.CompanyId = company.Id;

            DBHelper dbHelper = DBHelper.getInstance();
            dbHelper.Insert<Employee>(employee);
            if (employee.Id > 0)
            {
                MessageBox.Show("新增员工成功！");
            }
        }

        private void InitCompanySelections()
        {
            DBHelper dbHelper = DBHelper.getInstance();

            string strSql = "SELECT * FROM company";

            ParamMap param = ParamMap.newMap();
            param.setPageParamters(1, 20);
            param.setOrderFields("id", true);

            m_CompanyList = dbHelper.Find<Company>(strSql, param);
            cbCompany.DataSource = m_CompanyList;
            cbCompany.ValueMember = "Id";
            cbCompany.DisplayMember = "CompanyName";

            cbCompany.SelectedIndex = 0;
        }

        
    }
}
