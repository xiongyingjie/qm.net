
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class pay_record
        {

        public pay_record()
        {
            
         }

           
[Key]
public string pay_record_id { get; set; }


public string invoicing_order_id { get; set; }


public string pay_record_type_id { get; set; }


public double payed_price { get; set; }


public string operat_or { get; set; }


public DateTime? operat_time { get; set; }


public string operator_note { get; set; }


public string checkor { get; set; }


public DateTime? check_time { get; set; }


public string check_note { get; set; }


           
public virtual invoicing_order invoicing_order { get; set; }
public virtual pay_record_type pay_record_type { get; set; }  

             
        }
    }