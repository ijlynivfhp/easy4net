using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Easy4net;
using Easy4net.DBUtility;
using Entiry;
using Easy4net.Common;

namespace WindowsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DBHelper DB = new DBHelper();
        private Student updateStudent = new Student();
        private List<Student> m_stuList = new List<Student>();

        #region "初始化datagridview数据"
        private void Form1_Load(object sender, EventArgs e)
        {
            refreshData();
        }
        #endregion

        #region "查询所有数据"
        void refreshData()
        {
            int pageIndex = 1;
            int pageSize = 3;

            string strsql = "SELECT * FROM student where age < 500";

            ParamMap param = ParamMap.newMap();
            param.setPageParamters(pageIndex, pageSize);
            param.setOrderFields("id", true);

            m_stuList = DB.Find<Student>(strsql, param);
            dataGridView1.DataSource = m_stuList;
        }
        #endregion

        #region "新增学员信息"
        private void btnOK_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.Name = txtName.Text;

            String age = txtAge.Text.Trim();
            if (age.Length == 0) stu.Age = null;
            if (age.Length > 0) stu.Age = Convert.ToInt64(txtAge.Text);
            stu.Gender = txtGender.Text;
            stu.Address = txtAddress.Text;

            int id = DB.Insert<Student>(stu);
            if (id > 0)
            {
                MessageBox.Show("新增成功！新增的数据ID="+id);
            }

            refreshData();
        }
        #endregion

        #region "修改学员信息"
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateStudent.Name = txtName.Text;
            updateStudent.Age = Convert.ToInt32(txtAge.Text);
            updateStudent.Gender = txtGender.Text;
            updateStudent.Address = txtAddress.Text;
            DB.Update<Student>(updateStudent);

            refreshData();
        }
        #endregion

        #region "删除学员信息"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                object value = dataGridView1.Rows[i].Cells["selectedRow"].Value;
                if (value != null && value.Equals(true))
                {
                    Student student = m_stuList[i];

                    //删除 2中方式删除，可根据ID删除，DB.Remove<Student>(student.UserID);
                    DB.Delete<Student>(student.Id);
                }
            }

            refreshData();
        }
        #endregion

        #region "选择行，并填充到表单中，然后可做修改和删除操作"
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//当单击复选框，同时处于组合编辑状态时 
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool ifcheck1 = Convert.ToBoolean(cell.FormattedValue);
                bool ifcheck2 = Convert.ToBoolean(cell.EditedFormattedValue);

                if (ifcheck1 != ifcheck2)
                {
                    Student student = m_stuList[e.RowIndex];
                    txtName.Text = student.Name;
                    txtAge.Text = student.Age.ToString();
                    txtGender.Text = student.Gender;
                    txtAddress.Text = student.Address;
                    updateStudent.Id = student.Id;
                }
            }
        }
        #endregion

        #region "根据字段名称和值查询"
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int pageIndex = Convert.ToInt32(txtPageIndex.Text);
            int pageSize = Convert.ToInt32(txtPageSize.Text);

            /*string sql = "SELECT * FROM student where age < 20";
            m_stuList = DB.FindBySql<Student>(sql, pageIndex, pageSize);
            dataGridView1.DataSource = m_stuList;*/

            //string sql = "SELECT * FROM student where age < @age or address= @address";

            string sql = "SELECT * FROM (select * from student where age < @age or address= @address order by id desc) as v";
            ParamMap param = ParamMap.newMap();
            param.setParameter("age",500);
            param.setParameter("address", "上海市");
            // order by id ASC
            param.setOrderFields("id", false);

            m_stuList = DB.Find<Student>(sql, param);

            dataGridView1.DataSource = m_stuList;
        }
        #endregion
    }
}
