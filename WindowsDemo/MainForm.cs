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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private QueryCompanyForm m_QueryCompanyForm;
        private QueryEmployeeForm m_QueryEmployeeForm;
        
        private AddCompanyForm m_AddCompanyForm;
        private AddEmployeeForm m_AddEmployeeForm;




        private void btnSearch_Click(object sender, EventArgs e)
        {
            CloseForm();
            this.IsMdiContainer = true;
            m_QueryCompanyForm = new QueryCompanyForm();//实例化子窗体
            m_QueryCompanyForm.MdiParent = this;//设置窗体的父子关系
            m_QueryCompanyForm.Parent = plContainer;//设置子窗体的容器为父窗体中的Panel
            m_QueryCompanyForm.Dock = DockStyle.Fill;
            m_QueryCompanyForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void btnEmployeeSearch_Click(object sender, EventArgs e)
        {
            CloseForm();
            this.IsMdiContainer = true;
            m_QueryEmployeeForm = new QueryEmployeeForm();//实例化子窗体
            m_QueryEmployeeForm.MdiParent = this;//设置窗体的父子关系
            m_QueryEmployeeForm.Parent = plContainer;//设置子窗体的容器为父窗体中的Panel
            m_QueryEmployeeForm.Dock = DockStyle.Fill;
            m_QueryEmployeeForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            CloseForm();
            this.IsMdiContainer = true;
            m_AddCompanyForm = new AddCompanyForm();//实例化子窗体
            m_AddCompanyForm.MdiParent = this;//设置窗体的父子关系
            m_AddCompanyForm.Parent = plContainer;//设置子窗体的容器为父窗体中的Panel
            m_AddCompanyForm.Dock = DockStyle.Fill;
            m_AddCompanyForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            CloseForm();
            this.IsMdiContainer = true;
            m_AddEmployeeForm = new AddEmployeeForm();//实例化子窗体
            m_AddEmployeeForm.MdiParent = this;//设置窗体的父子关系
            m_AddEmployeeForm.Parent = plContainer;//设置子窗体的容器为父窗体中的Panel
            m_AddEmployeeForm.Dock = DockStyle.Fill;
            m_AddEmployeeForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void CloseForm()
        {
            if (m_QueryCompanyForm != null)
            {
                m_QueryCompanyForm.Close();
                m_QueryCompanyForm = null;
            }

            if (m_QueryEmployeeForm != null)
            {
                m_QueryEmployeeForm.Close();
                m_QueryEmployeeForm = null;
            }

            if (m_AddCompanyForm != null)
            {
                m_AddCompanyForm.Close();
                m_AddCompanyForm = null;
            }

            if (m_AddEmployeeForm != null)
            {
                m_AddEmployeeForm.Close();
                m_AddEmployeeForm = null;
            }

        }
    }
}
