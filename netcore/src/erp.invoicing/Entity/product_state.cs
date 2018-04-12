
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_state
        {

        public product_state()
        {
            
products = new HashSet<product>();
         }

           
[Key]
public string product_state_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<product> products { get; set; }  
        }
    }