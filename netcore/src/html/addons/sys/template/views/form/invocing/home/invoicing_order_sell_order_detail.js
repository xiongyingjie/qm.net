render(function(model){
var cfg=[];
cfg.push(
    group([
hide('invoicing_order-invoicing_order_id'),
select('订单类型', 'invoicing_order-invoicing_order_type_id', 'erp.invoicing.invoicing_order_type@items&name=name', '', '4'),
hide('sell_order-sell_order_id'),
select('客户名称', 'sell_order-customer_id', 'erp.invoicing.customer@items&name=name', '', '4'),
time('业务日期', 'sell_order-bll_time', '', '4', '请选择业务日期'),
input('业务员', 'sell_order-bllor', '', '4'),
select('销售类型', 'sell_order-sell_order_type_id', 'erp.invoicing.sell_order_type@items&name=name', '', '4'),
input('订单折扣率(%)', 'sell_order-discount', '', '4', {float:true}),
input('折扣金额', 'sell_order-discount_price', '', '4'),
hide('sell_order-sell_order_state_id'),
input('订金', 'sell_order-pre_pay_price', '', '4', {float:true}),
input('订单备注', 'invoicing_order-note', '', '4'),
hide('sell_order-operator'),
hideTime('sell_order-operat_time'),
input('操作理由', 'sell_order-operator_note', '', '4'),
hide('sell_order-checkor'),
hideTime('sell_order-check_time'),
hide('sell_order-check_note')
],'标题'));
return cfg;
},'','erp.invoicing.invoicing_order@find|sell_order@find&id='+q.id,'详情');