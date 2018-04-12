
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class sell_order_state
        {

        public sell_order_state()
        {
            
sell_orders = new HashSet<sell_order>();
         }

           
[Key]
public string sell_order_state_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<sell_order> sell_orders { get; set; }  
        }
    }