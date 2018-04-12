
function queren() {
    ("/Invocing/Home/confirm_inout_order?id=" + q.id).submit();
}

function onFirstLoad(dom) {
    render([
        panel([
            input('商品编号', 'invoicing_order_item-product_id', '', '4', { readonly: true }),//js赋值
            input('商品名称', 'invoicing_order_item-product_name', '', '4', { readonly: true }),//js赋值
            select('出库仓库', 'in_out_order_record_item-org_storage_id', 'erp.invoicing.org_storage@items&name=name', '', '4'),
            input('出库数量', 'in_out_order_record_item-num', '1', '4', { int: true }),
            hide('in_out_order_record_item-invoicing_order_item_id'),//js赋值
            hide('in_out_order_record_item-in_out_order_record_item_id'),//js赋值 _c.random("ckmx")
            hide('in_out_order_record_item-in_out_order_record_id', q.id),
   
            button('确认添加', '1:6', Color.orange, function () {
                if (!_c.hasValue(dom_product_id.val())) {
                    $.alert("请先选择要添加的商品");
                    return;
                }
                submitForm("erp.invoicing.in_out_order_record_item@add", function (data, code, msg) {
                    if (code === 7) {
                        $.refresh();
                        $.msg("已保存");
                    } else {
                        $.alert(msg);
                    }
                });
            }), html('<br/><div id="tb-products"></div>')
        ], '添加商品')], '', '', '添加商品到出库单', true, dom);
    loadTable();
}

var dom_product_id,
    dom_product_name,
    dom_num,
    dom_invoicing_order_item_id,
    dom_in_out_order_record_item_id;

function formReady() {
    dom_product_id = $("#invoicing_order_item-product_id");
    dom_product_name = $("#invoicing_order_item-product_name");
    dom_num = $("#in_out_order_record_item-num");
    dom_invoicing_order_item_id = $("#in_out_order_record_item-invoicing_order_item_id");
    dom_in_out_order_record_item_id = $("#in_out_order_record_item-in_out_order_record_item_id");
}

function loadTable() {
    renderTable({
        id: "tb-products",
        head: ["商品编号", "商品名称", "单价", "订购数量", "已出库数量", "退货数量", "退货入库数量", "待出库数量", "操作"],
                body: function (o) {
                    return [o.product_id, o.product_name, o.price, o.ordered_num, o.outed_num, o.return_num, o.returned_num, (o.ordered_num - o.return_num) - (o.outed_num - o.returned_num),
                [{
                    url: "*dchoose(\"" + o.product_id + "\",\"" +
                    o.product_name + "\",\"" + o.price + "\",\"" +
                    o.invoicing_order_item_id + "\")", text: "选择"
                }]];
        },
        store: "erp.invoicing.invoicing_order_item@list".eq("invoicing_order_item.invoicing_order_id",q.param)
    });
}

function choose(pid, name, price,iid) {
    dom_product_id.val(pid);
    dom_product_name.val(name);

    dom_invoicing_order_item_id.val(iid);
    dom_in_out_order_record_item_id.val(_c.random("ckmx"));
    dom_num.focus();
    $.msg("请输入出库数量",0);
}