﻿render(function(){
var cfg=[];
cfg.push(
    group([
input('社团名', 'association-name', '', '4', {min:1,max:50}),
input('相关文件', 'association-related_file', '', '4', {min:1,max:200}),
input('创建人', 'association-creator', '', '4', {min:1,max:50}),
time('创建时间', 'association-createTime', '', '4', '请选择创建时间'),
input('状态', 'association-status', '', '4', {int:true}),
area('描述', 'association-describe')
],'标题'));
return cfg;
},'ecampus.twxt2.association@update&id='+q.id,'ecampus.twxt2.association@find&id='+q.id,'编辑');