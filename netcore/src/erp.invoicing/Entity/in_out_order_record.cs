
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class in_out_order_record
        {

        public in_out_order_record()
        {
            
in_out_order_record_items = new HashSet<in_out_order_record_item>();
         }

           
[Key]
public string in_out_order_record_id { get; set; }


public string invoicing_order_id { get; set; }


public string in_out_order_record_type_id { get; set; }


public string send_no { get; set; }


public int num { get; set; }


public string send_or { get; set; }


public string operat_or { get; set; }


public DateTime? operat_time { get; set; }


public string operator_note { get; set; }


public string checkor { get; set; }


public DateTime? check_time { get; set; }


public string check_note { get; set; }


           
public virtual invoicing_order invoicing_order { get; set; }
public virtual in_out_order_record_type in_out_order_record_type { get; set; }  

           
public virtual ICollection<in_out_order_record_item> in_out_order_record_items { get; set; }  
        }
    }