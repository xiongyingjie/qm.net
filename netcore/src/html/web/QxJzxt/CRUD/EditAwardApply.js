﻿render(
    function () {
        var cfg = [];
        var groupContent = [];
        var groupContent1 = [];
        var tabContent = [];
        var valueArray = [];//该奖项已经添加的材料
        for (var m = 0; m < model.ApplyMaterialList.length; m++) {
            valueArray.push(model.ApplyMaterialList[m].usermaterialid);
        }
        $.log(valueArray);
        groupContent.push(hide("applyid", q.applyid));
        groupContent.push(hide("current_instance_id", q.current_instance_id));
        groupContent.push(hide("BaseInfoValueListLength", model.BaseInfoValueList.length));
        groupContent.push(hide("MaterialListLength", model.MaterialList.length));
        for (var i = 0; i < model.BaseInfoValueList.length; i++) {//基本信息项
            var item = model.BaseInfoValueList[i];
            groupContent.push(hide("awardinstancevalueid_" + i, item.awardinstancevalueid));
            switch (item.infodatatype) {
                case "001"://文本
                    groupContent.push(input(item.infoname, "baseinfoValue_" + i, item.value, 4));
                    break;
                case "002"://文本域
                    groupContent.push(area(item.infoname, "baseinfoValue_" + i, item.value, 1));
                    break;
                default://时间
                    groupContent.push(time(item.infoname, "baseinfoValue_" + i, item.value, 4));
                    break;
            }
        }
        tabContent.push({ title: "基本信息", content: groupContent });
        for (var k = 0; k < model.MaterialList.length; k++) { //该奖项要求的材料类型  Tab页
            groupContent1[k] = [];
            var item = model.MaterialList[k];
            var objArray = [];
            for (var j = 0; j < model.UserMaterial.length; j++) { //用户有申请审核通过的材料
                if (item.materialtypeid == model.UserMaterial[j].materialtypeid) {
                    objArray.push({
                        text: '<a href="' + $.parseurl("/QxJzxt/CRUD/DetailMaterialApply?id=" + model.UserMaterial[j].usermaterialid, "f").destUrl + '">' +
                            model.UserMaterial[j].material_name + '</a>',
                        value: model.UserMaterial[j].usermaterialid
                    });
                } else {
                    continue;
                }
            }
            groupContent1[k].push(checkbox(item.typename, "usermaterialid_" + k, objArray, 1,false, valueArray));
            tabContent.push({ title: item.typename, content: groupContent1[k] });
        }
        cfg.push(group([tab(tabContent, 1)], "申请奖项（请仔细浏览完该页面所有内容，一旦提交，不可修改）"));
        cfg.push(button("保存草稿", '1:5', Color.orange, function () {
            submitForm("/QxJzxt/CRUD/Draft_EditUserApplyAward");
        }));
        cfg.push(button("提交", '6:0', Color.green, function () {
            submitForm("/QxJzxt/CRUD/Submit_EditUserApplyAward");
        }));
        return cfg;
    }
   ,
"",
"/QxJzxt/CRUD/EditUserApplyAward",
"*");