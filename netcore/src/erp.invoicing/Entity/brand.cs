
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class brand
        {

        public brand()
        {
            
products = new HashSet<product>();
product_deposits = new HashSet<product_deposit>();
         }

           
[Key]
public string brand_id { get; set; }


public string name { get; set; }


public string sequence { get; set; }


public string url { get; set; }


public string logo { get; set; }


public string descripe { get; set; }


public string note { get; set; }


             

           
public virtual ICollection<product> products { get; set; }
public virtual ICollection<product_deposit> product_deposits { get; set; }  
        }
    }