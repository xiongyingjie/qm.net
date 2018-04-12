
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class in_out_order_record_item
        {

        public in_out_order_record_item()
        {
            
         }

           
[Key]
public string in_out_order_record_item_id { get; set; }


public string in_out_order_record_id { get; set; }


public string invoicing_order_item_id { get; set; }


public int num { get; set; }


public string org_storage_id { get; set; }


           
public virtual in_out_order_record in_out_order_record { get; set; }
public virtual invoicing_order_item invoicing_order_item { get; set; }
public virtual org_storage org_storage { get; set; }  

             
        }
    }