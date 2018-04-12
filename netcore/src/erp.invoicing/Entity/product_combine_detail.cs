
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_combine_detail
        {

        public product_combine_detail()
        {
            
         }

           
[Key]
public string product_combine_detail_id { get; set; }


public string product_combine_id { get; set; }


public string product_id { get; set; }


public int num { get; set; }


public string note { get; set; }


           
public virtual product_combine product_combine { get; set; }
public virtual product product { get; set; }  

             
        }
    }