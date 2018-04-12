
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_unit
        {

        public product_unit()
        {
            
products = new HashSet<product>();
product_deposits = new HashSet<product_deposit>();
         }

           
[Key]
public string product_unit_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<product> products { get; set; }
public virtual ICollection<product_deposit> product_deposits { get; set; }  
        }
    }