
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class invoicing_order_item
        {

        public invoicing_order_item()
        {
            
in_out_order_record_items = new HashSet<in_out_order_record_item>();
         }

           
[Key]
public string invoicing_order_item_id { get; set; }


public string invoicing_order_id { get; set; }


public string product_id { get; set; }


public string product_name { get; set; }


public double price { get; set; }


public int ordered_num { get; set; }


public double ordered_price { get; set; }


public double discount { get; set; }


public double discount_price { get; set; }


public double total_pay_price { get; set; }


public double total_payed_price { get; set; }


public int outed_num { get; set; }


public double return_price { get; set; }


public int return_num { get; set; }


public int returned_num { get; set; }


public string ordered_note { get; set; }


public string selled_note { get; set; }


public string discount_note { get; set; }


public string outed_note { get; set; }


public string return_note { get; set; }


public string note { get; set; }


           
public virtual invoicing_order invoicing_order { get; set; }
public virtual product product { get; set; }  

           
public virtual ICollection<in_out_order_record_item> in_out_order_record_items { get; set; }  
        }
    }