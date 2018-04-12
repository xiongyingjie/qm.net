
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class buy_order
        {

        public buy_order()
        {
            
         }

           
[Key]
public string buy_order_id { get; set; }


public string provider_id { get; set; }


public string org_storage_id { get; set; }


public string buy_order_state_id { get; set; }


public string operat_or { get; set; }


public DateTime? operat_time { get; set; }


public string operator_note { get; set; }


public string checkor { get; set; }


public DateTime? check_time { get; set; }


public string check_note { get; set; }


           
public virtual provider provider { get; set; }
public virtual buy_order_state buy_order_state { get; set; }  

             
        }
    }