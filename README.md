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

DBHelper dbHelper = DBHelper.getInstance();
List<Employee> emList = dbHelper.Find<Employee>(strSql, param);

```

**查询单条记录：**
***

```c#

string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id WHERE e.name = @name";
ParamMap param = ParamMap.newMap();
param.setParameter("name", "LiYang");

Employee em = dbHelper.FindOne<Employee>(strSql, param);

```

**普通查询：**
***
```c#

string strSql = "SELECT e.*, c.company_name FROM employee e INNER JOIN company c ON e.company_id = c.id";

List<Employee> emList = dbHelper.Find<Employee>(strSql);

```


**新增：**
* 1. 新增后返回新增记录的主键id值
* 2. 主键id值已经自动填充到新增的对象entity中

***

```c#

Student entity = new Student();
entity.Name = "Lily";
entity.Gender = "女";
entity.Age = 23;
entity.Address = "上海市徐汇区中山南二路918弄";
int id = dbHelper.Insert(entity);
```


**批量新增：**
* 1. 主键id值已经自动填充到新增的对象entity中
* 2. 批量新增方法比手动循环多个对象然后调用新增性能高。

***

```c#

List<Student> studentList = ...;
dbHelper.Insert(studentList);
```


**修改：**

***

```c#
Student entity = new Student();
entity.UserID = 1;
entity.Name = "Andy";
entity.Age = 22;
dbHelper.Update(entity);
```


**批量修改：**
* 1. 批量修改方法比手动循环多个对象然后调用修改性能高。

***

```c#
DBHelper db = DBHelper.getInstance();
List<Student> studentList = ...;
dbHelper.Update(studentList);
```


**删除：**
* 1. 按对象方式删除数据
* 2. 按主键id方式删除数据

***

```c#
Student student = m_stuList[i];
//remove a object
dbHelper.Delete(student);

//remove by id
dbHelper.Delete(student.UserID);
```

**批量删除：**
* 1. 按对象方式删除数据
* 2. 按主键id方式删除数据
* 3. 批量删除比手动循环调用删除性能要高

***

```c#

//remove by object
List<Student> studentList = ...;
dbHelper.Delete(studentList);

//remove by id
object[] ids = new object[]{1,2,3,4,5};
dbHelper.Delete(ids);
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

**数据库连接配置 web.config中： **
* dbType中配置sqlserver, mysql, oracle来支持不同的数据库

***

```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <!--<add key="DbType" value="sqlserver"/>
    <add key="connectionString" value="Data Source=121.199.9.217;Initial Catalog=test;User ID=test;Password=test123;Trusted_Connection=no;Min Pool Size=10;Max Pool Size=100;"/>-->

    <!--<add key="DbType" value="mysql"/>
    <add key="connectionString" value="Data Source=121.199.34.41;port=8001;User ID=test;Password=123456;DataBase=test;Min Pool Size=10;Max Pool Size=100;"/>-->

    <!--<add key="DbType" value="access"/>
    <add key="connectionString" value="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\tj.mdb"/>-->

    <!--<add key="DbType" value="sqlserver"/>
    <add key="DbHost" value="8QHMQCAJBCOOHW2\SQLEXPRESS" />
    <add key="DbName" value="test"/>
    <add key="DbUser" value="sa"/>
    <add key="DbPassword" value="111111"/>
    <add key="DbMinPoolSize" value="10"/>
    <add key="DbMaxPoolSize" value="100"/>-->

    <add key="DbType" value="mysql"/>
    <add key="DbHost" value="localhost" />
    <add key="DbName" value="test_db"/>
    <add key="DbUser" value="user_test"/>
    <add key="DbPassword" value="111111"/>
    <add key="DbPort" value="3306"/>
    <add key="DbMinPoolSize" value="10"/>
    <add key="DbMaxPoolSize" value="100"/>

  </appSettings>
<startup><supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/></startup></configuration>
```