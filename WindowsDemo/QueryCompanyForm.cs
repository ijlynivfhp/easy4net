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
    public partial class QueryCompanyForm : Form
    {
        public QueryCompanyForm()
        {
            InitializeComponent();
        }

        private int m_Page = 1;
        private int m_Limit = 5;
        private List<Company> companyList = null;

        private void QueryCompanyForm_Load(object sender, EventArgs e)
        {
            dgCompany.AutoGenerateColumns = false;

            /*Session session = SessionFactory.GetSession("MySQLString");
            Company company = new Company();
            company.Id = 47;
            company.Industry = "Industry";
            company.CompanyName = "CompanyName";
            company.Address = "Address";
            session.Update<Company>(company);*/

            FindByPage(1, m_Limit);
        }

        private void FindByPage(int page, int limit)
        {
            //Session session = SessionFactory.GetSession();

            //旧的写法，新的写法为Session，但是保持了兼容性
            Session session = SessionFactory.GetSession();
            session.ConnectDB("MySQLString");


            //string strSql = "SELECT * FROM company where company_name=@companyName";
            string strSql = "SELECT * FROM COMPANY";
            ParamMap param = ParamMap.newMap();

            //以下分页参数设置已过时，请使用 setPageParamters方法
            //param.setPageIndex(page);
            //param.setPageSize(limit);

            param.setPageParamters(page, limit);
            param.setOrderFields("id", true);
            //param.setParameter("companyName", "上海巨人网络信息科技");

            companyList = session.Find<Company>(strSql, param);
            dgCompany.DataSource = companyList;
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

        private void dgCompany_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
