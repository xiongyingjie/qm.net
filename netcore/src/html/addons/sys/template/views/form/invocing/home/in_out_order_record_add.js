var id = _c.random("ck");
var id2 = q.id;
render([
    group([
        hide('in_out_order_record-in_out_order_record_id', id),
        hide('in_out_order_record-invoicing_order_id', q.id),
        hide('in_out_order_record-in_out_order_record_type_id', 30),//30销售出库(待审核)
        hide('in_out_order_record-num', 0),//出库数量
        input('拣货人', 'in_out_order_record-send_or', '', '4'),
        input('发货单号', 'in_out_order_record-send_no', '', '4'),
        hide('in_out_order_record-operat_or', "#uid"),
        hideTime('in_out_order_record-operat_time',"#now"),
        input('出库备注', 'in_out_order_record-operator_note', '', '4'),
        hide('in_out_order_record-checkor'),
        hideTime('in_out_order_record-check_time'),
        hide('in_out_order_record-check_note')
    ], '出库单信息')], 'erp.invoicing.in_out_order_record@add', '', '新增出库单', '', '', function (data, code, msg) {
        $.msg("请继续为出库单添加商品");
        _c.go("*r/Invocing/Home/in_out_order_record_item_list?id=" + id + "&id2=" + id2, 1);
});