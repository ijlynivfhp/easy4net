using Easy4net.Common;
using Easy4net.Entity;
using Easy4net.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //新的写法，兼容DbHelper写法
            Session session = SessionFactory.GetSession("MySQLString");

            session.Insert<Employee>(employee);
            if (employee.Id > 0)
            {
                MessageBox.Show("新增员工成功！");
            }
        }

        private void InitCompanySelections()
        {
            Session session = SessionFactory.GetSession("MySQLString");

            string strSql = "SELECT * FROM company";

            ParamMap param = ParamMap.newMap();
            param.setPageParamters(1, 20);
            param.setOrderFields("id", true);

            m_CompanyList = session.Find<Company>(strSql, param);
            cbCompany.DataSource = m_CompanyList;
            cbCompany.ValueMember = "Id";
            cbCompany.DisplayMember = "CompanyName";

            cbCompany.SelectedIndex = 0;
        }
        
    }
}
