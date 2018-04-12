
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_combine
        {

        public product_combine()
        {
            
product_combine_details = new HashSet<product_combine_detail>();
         }

           
[Key]
public string product_combine_id { get; set; }


public string name { get; set; }


public double price { get; set; }


public string main_code { get; set; }


public DateTime create_time { get; set; }


public string note { get; set; }


             

           
public virtual ICollection<product_combine_detail> product_combine_details { get; set; }  
        }
    }