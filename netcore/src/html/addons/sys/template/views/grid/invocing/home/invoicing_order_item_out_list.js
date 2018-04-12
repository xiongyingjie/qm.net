function onFirstLoad(dom) {
    render([
        panel([
            input('流水号', 'in_out_stotage_record-in_out_stotage_record_id', '', '4', { min: 1, max: 250 }),
            select('订单明细单号', 'in_out_stotage_record-invoicing_order_item_id', 'erp.invoicing.invoicing_order_item@items&name=product_name', '', '4'),
            input('出库数量', 'in_out_stotage_record-num', '', '4', { int: true }),
            input('发货单号', 'in_out_stotage_record-send_no', '', '4'),
            input('操作人', 'in_out_stotage_record-operat_or', '', '4'),
            time('操作日期', 'in_out_stotage_record-operat_time', '', '4', '请选择操作日期'),
            input('操作理由', 'in_out_stotage_record-operator_note', '', '4'),
            input('审核人', 'in_out_stotage_record-checkor', '', '4'),
            time('审核日期', 'in_out_stotage_record-check_time', '', '4', '请选择审核日期'),
            input('审核意见', 'in_out_stotage_record-check_note', '', '4'),
            select('机构仓库id', 'in_out_stotage_record-org_storage_id', 'erp.invoicing.org_storage@items&name=name', '', '4'),
            select('出入库类型编号', 'in_out_stotage_record-in_out_order_record_type_id', 'erp.invoicing.in_out_order_record_type@items&name=name', '', '4'),
            button('确认出库', '1:6', Color.orange, function () {
                if (!_c.hasValue(dom_total_pay_price.val())) {
                    $.alert("请先选择要添加的商品");
                    return;
                }
                submitForm("erp.invoicing.invoicing_order_item@add", function (data, code, msg) {
                    if (code === 7) {
                        $.refresh();
                        $.msg("已保存");
                    } else {
                        $.alert(msg);
                    }
                });

            })

        ], '新增出库单')], '', '', '出库信息', true, dom);
    
}
//erp.invoicing.in_out_stotage_record@add
function submit_check(url) {
    $.confirm('<div><input type="text" id="input-note" placeholder="请输入出库备注"/></div>', function () {
          
            var note = $("#input-note").val();
            if (_c.hasValue(note)) {
                ("/Invocing/Home/" + url).submit({ id: q.id,  note: note});
        } else {
                $.alert("出库备注不能为空");
        }
    });
}

function out_all() {
    submit_check("outall_buy_order");
}

