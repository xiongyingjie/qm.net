
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class buy_order_state
        {

        public buy_order_state()
        {
            
buy_orders = new HashSet<buy_order>();
         }

           
[Key]
public string buy_order_state_id { get; set; }


public string name { get; set; }


             

           
public virtual ICollection<buy_order> buy_orders { get; set; }  
        }
    }