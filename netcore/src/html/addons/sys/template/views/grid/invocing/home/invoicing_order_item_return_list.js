




function confirm_return() {
    ("/Invocing/Home/confirm_return_buy_order?id=" + q.id).submit();
}



function onFirstLoad(dom) {
    render([
        panel([
            hide('invoicing_order_item-invoicing_order_item_id', "#id"),
            hide('invoicing_order_item-invoicing_order_id', q.id),
            input('商品编号', 'invoicing_order_item-product_id', '', '4', { min: 1, max: 50 }),
            input('商品名称', 'invoicing_order_item-product_name', '', '4', { min: 1, max: 50 }),
            input('商品单价', 'invoicing_order_item-price', '', '4', { float: true }),
            input('订购数量', 'invoicing_order_item-ordered_num', '1', '4', { int: true }),
            input('折扣率(%)', 'invoicing_order_item-discount', '100', '4'),
            input('订单金额', 'invoicing_order_item-ordered_price', '', '4', { readonly: true }),
            input('折扣金额', 'invoicing_order_item-discount_price', '0', '4', { readonly: true }),
            input('应付总额', 'invoicing_order_item-total_pay_price', '', '4', { readonly: true }),
            html('<div id="tb-products"></div>'),
            hide('invoicing_order_item-total_payed_price', '0'),
            hide('invoicing_order_item-return_num', '0'),
            hide('invoicing_order_item-return_price', '0'),
            hide('invoicing_order_item-returned_num', '0'),
            hide('invoicing_order_item-outed_num', '0'),
            hide('invoicing_order_item-ordered_note'),
            hide('invoicing_order_item-selled_note'),
            hide('invoicing_order_item-discount_note'),
            hide('invoicing_order_item-outed_note'),
            hide('invoicing_order_item-return_note'),
            hide('invoicing_order_item-note'),
            button('确认退货', '1:6', Color.orange, function () {
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
        ], '添加退货商品')], '', '', '添加商品到退货单', true, dom);
    // dom.html('<div id="tb-products"></div>');
    loadTable();
}





var dom_price, dom_ordered_num, dom_discount, dom_product_id,
    dom_product_name, dom_total_pay_price, dom_ordered_price,
    dom_discount_price;
function formReady() {
    dom_price = $("#invoicing_order_item-price");
    dom_ordered_num = $("#invoicing_order_item-ordered_num");
    dom_ordered_price = $("#invoicing_order_item-ordered_price");
    dom_discount = $("#invoicing_order_item-discount");
    dom_discount_price = $("#invoicing_order_item-discount_price");
    dom_product_id = $("#invoicing_order_item-product_id");
    dom_product_name = $("#invoicing_order_item-product_name");
    dom_total_pay_price = $("#invoicing_order_item-total_pay_price");

    dom_price.change(jisuan);
    dom_ordered_num.change(jisuan);
    dom_discount.change(jisuan);
    dom_product_id.keyup(loadTable);

}
function loadTable() {
    renderTable({
        id: "tb-products",
        head: ["商品编号", "商品名称", "单价", "数量", "折扣率", "折扣金额", "应付金额", "操作"],
        body: function (o) {
            return [o.product_id, o.product_name, o.price, o.ordered_num, o.discount+'%', o.discount_price, o.total_pay_price,
                [{ url: "*dchoose(\"" + o.product_id + "\",\"" + o.product_name + "\",\"" + o.price + "\")", text: "退货" }]];
        },
        store: 'erp.invoicing.invoicing_order_item@list'.eq("invoicing_order_id","0289faaa-e8e9-535b-f3ec-f08f1ac058de")//注意：此处应改为关联订单id
    });
}

$.msg("此处应改为关联订单");
function jisuan() {
    var num = dom_ordered_num.val();
    var price = dom_price.val();
    var orderedPrice = num * price;
    var discount = (100 - dom_discount.val()) / 100.0;
    var discountPrice = orderedPrice * discount;
    //debugger
    //订单金额
    dom_ordered_price.val(orderedPrice);
    //折扣金额
    dom_discount_price.val(discountPrice);
    //应付总额
    dom_total_pay_price.val(orderedPrice - discountPrice);
}

function choose(pid, name, price) {
    dom_product_id.val(pid);
    dom_product_name.val(name);
    dom_price.val(price);
    jisuan();
}