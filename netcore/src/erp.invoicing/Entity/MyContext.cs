using Microsoft.EntityFrameworkCore;
using erp.invoicing;
using xyj.core.Extensions;
using xyj.core.Utility;
namespace erp.invoicing.Entity
{
 public partial class MyContext : DbContext
    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.InitConnection("erp.invoicing");
        }
        
public virtual DbSet<brand> brand { get; set; }
public virtual DbSet<buy_order> buy_order { get; set; }
public virtual DbSet<buy_order_state> buy_order_state { get; set; }
public virtual DbSet<category> category { get; set; }
public virtual DbSet<category_attr> category_attr { get; set; }
public virtual DbSet<category_attr_option> category_attr_option { get; set; }
public virtual DbSet<category_attr_type> category_attr_type { get; set; }
public virtual DbSet<customer> customer { get; set; }
public virtual DbSet<customer_type> customer_type { get; set; }
public virtual DbSet<in_out_order_record> in_out_order_record { get; set; }
public virtual DbSet<in_out_order_record_item> in_out_order_record_item { get; set; }
public virtual DbSet<in_out_order_record_type> in_out_order_record_type { get; set; }
public virtual DbSet<invoicing_log> invoicing_log { get; set; }
public virtual DbSet<invoicing_log_type> invoicing_log_type { get; set; }
public virtual DbSet<invoicing_order> invoicing_order { get; set; }
public virtual DbSet<invoicing_order_item> invoicing_order_item { get; set; }
public virtual DbSet<invoicing_order_type> invoicing_order_type { get; set; }
public virtual DbSet<org_storage> org_storage { get; set; }
public virtual DbSet<org_storage_product> org_storage_product { get; set; }
public virtual DbSet<pay_record> pay_record { get; set; }
public virtual DbSet<pay_record_type> pay_record_type { get; set; }
public virtual DbSet<product> product { get; set; }
public virtual DbSet<product_combine> product_combine { get; set; }
public virtual DbSet<product_combine_detail> product_combine_detail { get; set; }
public virtual DbSet<product_deposit> product_deposit { get; set; }
public virtual DbSet<product_extent_attr_value> product_extent_attr_value { get; set; }
public virtual DbSet<product_publish_house> product_publish_house { get; set; }
public virtual DbSet<product_state> product_state { get; set; }
public virtual DbSet<product_type> product_type { get; set; }
public virtual DbSet<product_unit> product_unit { get; set; }
public virtual DbSet<provider> provider { get; set; }
public virtual DbSet<provider_type> provider_type { get; set; }
public virtual DbSet<sell_order> sell_order { get; set; }
public virtual DbSet<sell_order_state> sell_order_state { get; set; }
public virtual DbSet<sell_order_type> sell_order_type { get; set; }
public virtual DbSet<tickect_type> tickect_type { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
  }
}
