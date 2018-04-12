
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace erp.invoicing.Entity
{  
        public partial class in_out_stotage_record
        {

        public in_out_stotage_record()
        {
            
         }

           
[Key]
public string in_out_stotage_record_id { get; set; }


public string invoicing_order_item_id { get; set; }


public int num { get; set; }


public string send_no { get; set; }


public string operat_or { get; set; }


public DateTime? operat_time { get; set; }


public string operator_note { get; set; }


public string checkor { get; set; }


public DateTime? check_time { get; set; }


public string check_note { get; set; }


public string org_storage_id { get; set; }


public string in_out_stotage_record_type_id { get; set; }


           
public virtual invoicing_order_item invoicing_order_item { get; set; }
public virtual org_storage org_storage { get; set; }
public virtual in_out_stotage_record_type in_out_stotage_record_type { get; set; }  

             
        }
    }