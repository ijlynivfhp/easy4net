using Easy4net.Common;
using Easy4net.DBUtility;
using Easy4net.Entity;
using Easy4net.Context;
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
    public partial class QueryEmployeeForm : Form
    {
        public QueryEmployeeForm()
        {
            InitializeComponent();
        }

        private int m_Page = 1;
        private int m_Limit = 5;
        private List<Employee> m_EmployeeList = null;
        private void QueryEmployeeForm_Load(object sender, EventArgs e)
        {
            dgEmployee.AutoGenerateColumns = false;

            FindByPage(1, m_Limit);
        }

        private void FindByPage(int page, int limit)
        {
            //DBHelper dbHelper = DBHelper.getInstance();
            //dbHelper.BeginTransaction();

            //新的写法，兼容DbHelper写法
            Session session = SessionFactory.GetSession();
            session.ConnectDB("MySQLString");

            string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id";
            ParamMap param = ParamMap.newMap();

            //以下分页参数设置已过时，请使用 setPageParamters方法
            //param.setPageIndex(page);
            //param.setPageSize(limit);

            param.setPageParamters(page, limit);
            
            //分页时使用的排序字段，必填，请带上SQL表名的别名，如employee的为: e
            param.setOrderFields("e.id", true);

            m_EmployeeList = session.Find<Employee>(strSql, param);
            dgEmployee.DataSource = m_EmployeeList;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            m_Page = Convert.ToInt32(txtPage.Text.Trim());
            m_Page--;
            if (m_Page <= 0) m_Page = 1;

            txtPage.Text = Convert.ToString(m_Page);

            FindByPage(m_Page, m_Limit);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            m_Page = Convert.ToInt32(txtPage.Text.Trim());
            m_Page++;
            if (m_Page <= 0) m_Page = 1;

            txtPage.Text = Convert.ToString(m_Page);

            FindByPage(m_Page, m_Limit);
        }
    }
}
