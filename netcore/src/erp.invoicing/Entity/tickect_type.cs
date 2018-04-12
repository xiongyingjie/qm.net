
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class tickect_type
        {

        public tickect_type()
        {
            
customers = new HashSet<customer>();
providers = new HashSet<provider>();
         }

           
[Key]
public string tickect_type_id { get; set; }


public string name { get; set; }


public string note { get; set; }


             

           
public virtual ICollection<customer> customers { get; set; }
public virtual ICollection<provider> providers { get; set; }  
        }
    }