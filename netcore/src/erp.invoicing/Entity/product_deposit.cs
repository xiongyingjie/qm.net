
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class product_deposit
        {

        public product_deposit()
        {
            
         }

           
[Key]
public string product_deposit_id { get; set; }


public string name { get; set; }


public string main_code { get; set; }


public double out_price { get; set; }


public double batch_out_price { get; set; }


public string product_unit_id { get; set; }


public double in_price { get; set; }


public string product_type_id { get; set; }


public string brand_id { get; set; }


public string provider_id { get; set; }


public string pinyin_code { get; set; }


public string category_id { get; set; }


public string detail { get; set; }


public string satatus { get; set; }


           
public virtual product_unit product_unit { get; set; }
public virtual product_type product_type { get; set; }
public virtual brand brand { get; set; }
public virtual provider provider { get; set; }
public virtual category category { get; set; }  

             
        }
    }