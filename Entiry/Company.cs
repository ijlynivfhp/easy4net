using Easy4net.CustomAttributes;
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

