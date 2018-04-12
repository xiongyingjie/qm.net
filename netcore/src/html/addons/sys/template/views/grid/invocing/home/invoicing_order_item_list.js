


function dazhe() {
    $.confirm('<div><input type="text" id="discount-value" placeholder="请输入折扣值(%)"/></div>', function () {
        var v = $("#discount-value").val();
        if (_c.hasValue(v) && v >= 0 && v <= 100) {
            ("/Invocing/Home/discount_sell_order").submit({ id: q.id, percent: v},
                function(data, code, msg) {
                    $.msg(msg);
                    $.refresh();
                });
        } else {
            $.alert("请输入正确的折扣值(0-100)");
        }
        
    });
}

function queren() {
    ("/Invocing/Home/confirm_sell_order?id=" + q.id).submit();
}



function onFirstLoad(dom) {
    render([
        panel([
            hide('invoicing_order_item-invoicing_order_item_id', "#id"),
            hide('invoicing_order_item-invoicing_order_id',q.id),
            input('商品编号', 'invoicing_order_item-product_id', '', '4', { min: 1, max: 50 }),
            input('商品名称', 'invoicing_order_item-product_name', '', '4', { min: 1, max: 50 }),
            input('商品单价', 'invoicing_order_item-price', '', '4', { float: true }),
            input('订购数量', 'invoicing_order_item-ordered_num', '1', '4', { int: true }),
            input('折扣率(%)', 'invoicing_order_item-discount', '100', '4'),
            input('订单金额', 'invoicing_order_item-ordered_price', '', '4', { readonly:true}),
            input('折扣金额', 'invoicing_order_item-discount_price', '0', '4', { readonly: true }),
            input('应付总额', 'invoicing_order_item-total_pay_price', '', '4', { readonly: true }),
            html('<div id="tb-products"></div>'),
            hide('invoicing_order_item-total_payed_price', '0'),
            hide('invoicing_order_item-return_num','0'),
            hide('invoicing_order_item-return_price','0'),
            hide('invoicing_order_item-returned_num','0'),
            hide('invoicing_order_item-outed_num','0'),
            hide('invoicing_order_item-ordered_note'),
            hide('invoicing_order_item-selled_note'),
            hide('invoicing_order_item-discount_note'),
            hide('invoicing_order_item-outed_note'),
            hide('invoicing_order_item-return_note'),
            hide('invoicing_order_item-note'),
            button('确认添加', '1:6', Color.orange, function () {
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
        ], '添加商品')], '', '', '添加商品到销售单',true,dom);
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
        head: ["商品编号", "商品名称", "单价", "供应商", "操作"],
        body: function (o) {
            return [o.product_id, o.name, o.out_price, o["provider-name"],
                [{ url: "*dchoose(\"" + o.product_id + "\",\"" + o.name + "\",\"" + o.out_price + "\")", text: "选择" }]];
        },
        store: "erp.invoicing.product@list".jn("provider.provider_id").lk("product_id", $("#invoicing_order_item-product_id").val())
    });
}
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
    dom_ordered_num.focus();
    $.msg("请输入销售数量",0);
}