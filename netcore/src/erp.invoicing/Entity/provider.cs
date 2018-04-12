
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class provider
        {

        public provider()
        {
            
buy_orders = new HashSet<buy_order>();
products = new HashSet<product>();
product_deposits = new HashSet<product_deposit>();
         }

           
[Key]
public string provider_id { get; set; }


public string name { get; set; }


public string contactor { get; set; }


public string phone { get; set; }


public string tickect_type_id { get; set; }


public string area { get; set; }


public string fax { get; set; }


public string address { get; set; }


public string zip_code { get; set; }


public string provider_type_id { get; set; }


public string pinyin { get; set; }


public string email { get; set; }


public string bank { get; set; }


public string bank_no { get; set; }


public string tax_no { get; set; }


public string checkout_method { get; set; }


public string checkout_period { get; set; }


public int? checkout_day { get; set; }


public string charge_account_batch { get; set; }


public string credit_limit { get; set; }


public string salesman { get; set; }


public string price_level { get; set; }


public string stop_intercourse { get; set; }


public string note1 { get; set; }


public string note2 { get; set; }


public int? is_lock { get; set; }


public int? status { get; set; }


           
public virtual tickect_type tickect_type { get; set; }
public virtual provider_type provider_type { get; set; }  

           
public virtual ICollection<buy_order> buy_orders { get; set; }
public virtual ICollection<product> products { get; set; }
public virtual ICollection<product_deposit> product_deposits { get; set; }  
        }
    }