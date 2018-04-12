
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class sell_order
        {

        public sell_order()
        {
            
         }

           
[Key]
public string sell_order_id { get; set; }


public string customer_id { get; set; }


public DateTime? bll_time { get; set; }


public string bllor { get; set; }


public string sell_order_type_id { get; set; }


public double? ordered_price { get; set; }


public double discount { get; set; }


public double? discount_price { get; set; }


public double? total_pay_price { get; set; }


public double? total_payed_price { get; set; }


public string sell_order_state_id { get; set; }


public double pre_pay_price { get; set; }


public string operat_or { get; set; }


public DateTime operat_time { get; set; }


public string operator_note { get; set; }


public string checkor { get; set; }


public DateTime? check_time { get; set; }


public string check_note { get; set; }


           
public virtual customer customer { get; set; }
public virtual sell_order_type sell_order_type { get; set; }
public virtual sell_order_state sell_order_state { get; set; }  

             
        }
    }