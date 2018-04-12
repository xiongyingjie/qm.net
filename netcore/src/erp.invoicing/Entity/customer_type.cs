
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class customer_type
        {

        public customer_type()
        {
            
customers = new HashSet<customer>();
         }

           
[Key]
public string customer_type_id { get; set; }


public string name { get; set; }


public string note { get; set; }


             

           
public virtual ICollection<customer> customers { get; set; }  
        }
    }