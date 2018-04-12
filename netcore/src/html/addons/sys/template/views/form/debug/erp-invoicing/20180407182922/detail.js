render(function(model){
var cfg=[];
cfg.push(
    group([
input('流水号', 'in_out_stotage_record-in_out_stotage_record_id', '', '4', {min:1,max:250}),
select('订单明细单号', 'in_out_stotage_record-invoicing_order_item_id', 'erp.invoicing.invoicing_order_item@items&name=product_name', '', '4'),
input('出库数量', 'in_out_stotage_record-num', '', '4', {int:true}),
input('发货单号', 'in_out_stotage_record-send_no', '', '4'),
input('操作人', 'in_out_stotage_record-operat_or', '', '4'),
time('操作日期', 'in_out_stotage_record-operat_time', '', '4', '请选择操作日期'),
input('操作理由', 'in_out_stotage_record-operator_note', '', '4'),
input('审核人', 'in_out_stotage_record-checkor', '', '4'),
time('审核日期', 'in_out_stotage_record-check_time', '', '4', '请选择审核日期'),
input('审核意见', 'in_out_stotage_record-check_note', '', '4'),
select('机构仓库id', 'in_out_stotage_record-org_storage_id', 'erp.invoicing.org_storage@items&name=name', '', '4'),
select('出入库类型编号', 'in_out_stotage_record-in_out_stotage_record_type_id', 'erp.invoicing.in_out_stotage_record_type@items&name=name', '', '4')
],'标题'));
return cfg;
},'','erp.invoicing.in_out_stotage_record@find&id='+q.id,'详情');