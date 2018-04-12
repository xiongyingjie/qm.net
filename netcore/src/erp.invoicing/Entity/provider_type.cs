
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class provider_type
        {

        public provider_type()
        {
            
providers = new HashSet<provider>();
         }

           
[Key]
public string provider_type_id { get; set; }


public string name { get; set; }


public string note { get; set; }


             

           
public virtual ICollection<provider> providers { get; set; }  
        }
    }