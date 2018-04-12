
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class invoicing_log
        {

        public invoicing_log()
        {
            
         }

           
[Key]
public string invoicing_log_id { get; set; }


public string invoicing_log_type_id { get; set; }


public string operat_or { get; set; }


public DateTime? operat_time { get; set; }


public string operator_note { get; set; }


public string relation_key { get; set; }


public string relation_key2 { get; set; }


           
public virtual invoicing_log_type invoicing_log_type { get; set; }  

             
        }
    }