
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class pay_record_type
        {

        public pay_record_type()
        {
            
pay_records = new HashSet<pay_record>();
         }

           
[Key]
public string pay_record_type_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<pay_record> pay_records { get; set; }  
        }
    }