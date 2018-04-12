
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class org_storage
        {

        public org_storage()
        {
            
in_out_order_record_items = new HashSet<in_out_order_record_item>();
org_storage_products = new HashSet<org_storage_product>();
products = new HashSet<product>();
         }

           
[Key]
public string org_storage_id { get; set; }


public string name { get; set; }


public string contactor { get; set; }


public string phone { get; set; }


public string address { get; set; }


public string email { get; set; }


public string capacity { get; set; }


public string note { get; set; }


public string org_id { get; set; }


             

           
public virtual ICollection<in_out_order_record_item> in_out_order_record_items { get; set; }
public virtual ICollection<org_storage_product> org_storage_products { get; set; }
public virtual ICollection<product> products { get; set; }  
        }
    }