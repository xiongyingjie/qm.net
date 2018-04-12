
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class invoicing_log_type
        {

        public invoicing_log_type()
        {
            
invoicing_logs = new HashSet<invoicing_log>();
         }

           
[Key]
public string invoicing_log_type_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<invoicing_log> invoicing_logs { get; set; }  
        }
    }