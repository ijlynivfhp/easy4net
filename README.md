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
namespace Entiry
{
    [Serializable]
    [Table(Name = "U_Student")]
    public class Student
    {
        //主键自增长
        [Id(Name = "UserID", Strategy = GenerationType.INDENTITY)]
        public int UserID { get; set; }

        //数据库字段U_Name
        [Column(Name = "U_Name")]
        public string Name { get; set; }

        [Column(Name = "U_Age")] // int? 允许int类型为空
        public int? Age { get; set; }

        [Column(Name = "U_Gender")]
        public string Gender { get; set; }

        [Column(Name = "U_Address")]
        public string Address { get; set; }

        [Column(Name = "U_CreateTime")]
        public DateTime? CreateTime { get; set; }

        [Column(Name = "ClassID")]
        public int? ClassID { get; set; }
 
        // 不保存该属性值到数据库库，忽略新增和修改
        [Column(Name = "ClassName",IsInsert=false,IsUpdate=false)]
        public string ClassName { get; set; }
 
        // 不保存该属性值到数据库库，忽略新增和修改
        [Column(Name = "Teacher", IsInsert = false, IsUpdate = false)]
        public string Teacher { get; set; }
    }
}
```

**数据库连接配置 web.config中： **
* dbType中配置sqlserver, mysql, oracle来支持不同的数据库

***

```xml
<configuration>
  <appSettings>
    <add key="DbType" value="sqlserver"/>
    <add key="connectionString" value="Data Source=127.0.0.1;Initial Catalog=test;User ID=test;Password=test123;Trusted_Connection=no;Min Pool Size=10;Max Pool Size=100;"/>

    <!--<add key="DbType" value="mysql"/>
    <add key="connectionString" value="Data Source=127.0.0.1;port=8001;User ID=test;Password=123456;DataBase=test;Min Pool Size=10;Max Pool Size=100;"/>-->

    <!--<add key="DbType" value="access"/>
    <add key="connectionString" value="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\tj.mdb"/>-->
  </appSettings>
</configuration>
```