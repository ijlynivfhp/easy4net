easy4net是一个轻量级orm框架，灵活在于可以自己编写复杂的SQL语句查询，简单在于几分钟内便能上手使用，并支持mysql, mssql, oracle, access数据库. easy4net技术QQ 群：162695864

**分页查询：**
* 1. 命名参数， ParamMap传参方式：
* 2. 支持多层嵌套查询自动分页功能。

***

```c#

int pageIndex = 1;
int pageSize = 3;
string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id WHERE e.name = @name";

ParamMap param = ParamMap.newMap();
param.setPageParamters(page, limit);
//分页时使用的排序字段，必填，请带上SQL表名的别名，如employee的为: e
param.setOrderFields("e.id", true);
param.setParameter("name", "LiYang");

*MySQLString是app.config或web.config中配置的连接数据库字符串，
*应部分用户需要可以切换多个数据库要求
Session session = SessionFactory.GetSession("MySQLString");
List<Employee> emList = session.Find<Employee>(strSql, param);

```

**分页查询：**
* 1. 命名参数， ParamMap传参方式：
* 2. 返回总条数和数据集合的对象PageResult

***

```c#
public PageResult<Store> findByPage(int page, int limit)
{
    Session session = SessionFactory.GetSession("MySQLString");

    String sql = "select * from store";

    ParamMap param = ParamMap.newMap();
    param.setPageParamters(page, limit);
    param.setOrderFields("id", true);

    PageResult<Store> pageResult = session.FindPage<Store>(sql, param);

    return pageResult;
}


public class PageResult<T>
{
    /// <summary>
    /// 分页查询中总记录数
    /// </summary>
    public int Total {get; set;}

    /// <summary>
    /// 分页查询中结果集合
    /// </summary>
    public List<T> DataList {get; set;}
}
```


**查询单条记录：**
***

```c#

string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id WHERE e.name = @name";
ParamMap param = ParamMap.newMap();
param.setParameter("name", "LiYang");

Employee em = session.Get<Employee>(strSql, param);

```

**普通查询：**
***
```c#

string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id";

List<Employee> emList = session.Find<Employee>(strSql);

```


**新增：**
* 1. 新增后返回新增记录的主键id值
* 2. 主键id值已经自动填充到新增的对象entity中

***

```c#

//创建公司
Company company = new Company();
company.CompanyName = txtName.Text.Trim();
company.Industry = txtIndustry.Text.Trim();
company.Address = txtAddress.Text.Trim();

Session session = SessionFactory.GetSession("MySQLString");
session.Insert<Company>(company);

if (company.Id > 0) {
    MessageBox.Show("创建公司成功！");
}


//新增员工
Company company = m_CompanyList[cbCompany.SelectedIndex]; 
Employee employee = new Employee();
employee.Name = txtName.Text.Trim();
mployee.Age = Convert.ToInt32(txtAge.Text.Trim());
employee.Address = txtAddress.Text.Trim();
employee.Created = DateTime.Now;
employee.CompanyId = company.Id;

Session session = SessionFactory.GetSession("MySQLString");
session.Insert<Employee>(employee);
if (employee.Id > 0)
{
    MessageBox.Show("新增员工成功！");
}

```


**批量新增：**
* 1. 主键id值已经自动填充到新增的对象entity中
* 2. 批量新增方法比手动循环多个对象然后调用新增性能高。

***

```c#

List<Company> companyList = ...;
Session session = SessionFactory.GetSession("MySQLString");
session.Insert(companyList);
```


**修改：**

***

```c#
Company entity = new Company();
entity.Id = 1;
entity.Name = "百度";

Session session = SessionFactory.GetSession("MySQLString");
session.Update(entity);
```


**批量修改：**
* 1. 批量修改方法比手动循环多个对象然后调用修改性能高。

***

```c#
DBHelper db = DBHelper.getInstance();
List<Company> companyList = ...;

Session session = SessionFactory.GetSession("MySQLString");
session.Update(companyList);
```


**删除：**
* 1. 按对象方式删除数据
* 2. 按主键id方式删除数据

***

```c#
Company company = m_companyList[i];
//remove a object

Session session = SessionFactory.GetSession("MySQLString");
session.Delete(company);

//remove by id
session.Delete(company.Id);
```

**批量删除：**
* 1. 按对象方式删除数据
* 2. 按主键id方式删除数据
* 3. 批量删除比手动循环调用删除性能要高

***

```c#

//remove by object
List<Company> companyList = ...;

Session session = SessionFactory.GetSession("MySQLString");
session.Delete(companyList);

//remove by id
object[] ids = new object[]{1,2,3,4,5};
session.Delete(ids);
```



**数据库与C#对象映射关系配置：**

***

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
 
namespace Easy4net.Entity  
{  
     [Table(Name = "company")] 
	 public class Company
	 { 
		[Id(Name = "id", Strategy = GenerationType.INDENTITY)]
		public int? Id{ get; set; } 

		[Column(Name = "company_name")]
		public String CompanyName{ get; set; }

        [Column(Name = "industry")]
        public String Industry { get; set; }

        [Column(Name = "address")]
        public String Address { get; set; } 

	 } 
}  


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;  
namespace Easy4net.Entity  
{  
     [Table(Name = "employee")] 
	 public class Employee
	 { 
		[Id(Name = "id", Strategy = GenerationType.INDENTITY)]
		public int? Id{ get; set; } 

		[Column(Name = "name")]
		public String Name{ get; set; } 

		[Column(Name = "age")]
		public int? Age{ get; set; } 

		[Column(Name = "address")]
		public String Address{ get; set; } 

		[Column(Name = "created")]
		public DateTime? Created{ get; set; } 

		[Column(Name = "company_id")]
		public int? CompanyId{ get; set; }

        [Column(Name = "company_name", IsInsert = false, IsUpdate = false)]
        public String CompanyName { get; set; } 

	 } 
}   

```

**数据库连接配置 web.config中：**
* dbType中配置sqlserver, mysql, oracle来支持不同的数据库

***

```xml
<?xml version="1.0"?>
<configuration>
  
  <connectionStrings>

    <add name="MySQLString"
        connectionString="Data Source=localhost;Initial Catalog=test_db;Persist Security Info=True;User ID=root;Password=123456;Min Pool Size=1;Max Pool Size=3;"
        providerName="MySql.Data.MySqlClient" />

    
    <add name="SqlServerString" 
         connectionString="Data Source=(local);Initial Catalog=Study;User ID=sa;Password=sa;" 
         providerName="System.Data.SqlClient" />

   
    <add name="OracleString"
         connectionString="Data Source=192.168.0.2;Initial Catalog=test;Persist Security Info=True;User ID=user1;Password=pass1"
         providerName="System.Data.OracleClient" />

    <add name="SQLiteString" connectionString="Data Source=|DataDirectory|\testdb.sqlite;Pooling=true;"
       providerName="System.Data.SQLite" />

  </connectionStrings>

  <startup>
  <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
</startup>
</configuration>
```