
function submit_check(url) {
    $.confirm('<div><input type="text" id="check-note" placeholder="请输入退货入库审核意见"/></div>', function () {
        var v = $("#check-note").val();

        if (_c.hasValue(v)) {
            ("/Invocing/Home/" + url).submit({ id: q.id, note: v });
        } else {
            $.alert("审核意见不能为空");
        }
    });
}

function ok() {
    submit_check("check_ok_inout_return_order");
}

function notok() {
    submit_check("check_notok_inout_return_order");
}
