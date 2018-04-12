
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class invoicing_order
        {

        public invoicing_order()
        {
            
in_out_order_records = new HashSet<in_out_order_record>();
invoicing_order_items = new HashSet<invoicing_order_item>();
pay_records = new HashSet<pay_record>();
         }

           
[Key]
public string invoicing_order_id { get; set; }


public string invoicing_order_type_id { get; set; }


public string relation_invoicing_order_id { get; set; }


public int? has_return { get; set; }


public string note { get; set; }


           
public virtual invoicing_order_type invoicing_order_type { get; set; }  

           
public virtual ICollection<in_out_order_record> in_out_order_records { get; set; }
public virtual ICollection<invoicing_order_item> invoicing_order_items { get; set; }
public virtual ICollection<pay_record> pay_records { get; set; }  
        }
    }