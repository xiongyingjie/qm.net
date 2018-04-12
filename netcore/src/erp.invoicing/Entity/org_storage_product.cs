
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class org_storage_product
        {

        public org_storage_product()
        {
            
         }

           
[Key]
public string org_storage_product_id { get; set; }


public string org_storage_id { get; set; }


public string product_id { get; set; }


public int start_num { get; set; }


public int ordered_num { get; set; }


public int borrowed_num { get; set; }


public int onroad_num { get; set; }


public int real_num { get; set; }


public int enable_num { get; set; }


public DateTime update_time { get; set; }


           
public virtual org_storage org_storage { get; set; }
public virtual product product { get; set; }  

             
        }
    }