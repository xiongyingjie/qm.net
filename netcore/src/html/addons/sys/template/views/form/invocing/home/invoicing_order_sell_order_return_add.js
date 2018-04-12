
var id = _c.random('th');
render([
    group([
        hide('invoicing_order-invoicing_order_id', id),
        hide('sell_order-sell_order_id', id),
        hide('invoicing_order-invoicing_order_type_id', '2'),//2为销售单
  
        select('客户', 'sell_order-customer_id', 'erp.invoicing.customer@items&name=name', '', '4'),
        select('销售类型', 'sell_order-sell_order_type_id', 'erp.invoicing.sell_order_type@items&name=name', 1, '4'),
        time('业务日期', 'sell_order-bll_time', '', '4', '请选择业务日期'),
        input('业务员', 'sell_order-bllor', '', '4'),
        hide('sell_order-discount', '100'), //订单折扣率(%)100
        hide('sell_order-discount_price', '0'), //折扣金额0
        hide('sell_order-sell_order_state_id', '1'),//1为销售定单
        input('订金', 'sell_order-pre_pay_price', 0, '4', { float: true }),
        input('订单备注', 'invoicing_order-note', '', '4'),
        hide('sell_order-operator', "#uid"),
        hideTime('sell_order-operat_time',"#now"),
        input('操作理由', 'sell_order-operator_note', '', '4'),

        hide('sell_order-checkor'),
        hideTime('sell_order-check_time'),
        hide('sell_order-check_note')
    ], '退货单信息')], 'erp.invoicing.invoicing_order@add|sell_order@add', '', '新增退货单');