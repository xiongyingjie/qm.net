
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_publish_house
        {

        public product_publish_house()
        {
            
products = new HashSet<product>();
         }

           
[Key]
public string product_publish_house_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<product> products { get; set; }  
        }
    }