
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class in_out_order_record_type
        {

        public in_out_order_record_type()
        {
            
in_out_order_records = new HashSet<in_out_order_record>();
         }

           
[Key]
public string in_out_order_record_type_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<in_out_order_record> in_out_order_records { get; set; }  
        }
    }