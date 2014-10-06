using Easy4net.CustomAttributes;
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

