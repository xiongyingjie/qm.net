﻿render(function(){
var cfg=[];
cfg.push(
    group([
input('机构id', 'orgnization-orgnization_id'),
showinput('上级', 'orgnization-father_id'),
input('名称', 'orgnization-name', '', '4', { min: 1, max: 100 }),
select('类型', 'orgnization-orgnization_type_id', 'qx.permmision.v2.orgnization_type@items&name=name', '', '4'),
input('子系统', 'orgnization-sub_system', '-', '4'),
select('机构级别', 'orgnization-organization_level_id', 'qx.permmision.v2.organization_level@items&name=name', '', '4'),
area('备注', 'orgnization-note'),
area('描述', 'orgnization-descripe')
],'标题'));
return cfg;
},'qx.permmision.v2.orgnization@update&id='+q.id,'qx.permmision.v2.orgnization@find&id='+q.id,'编辑');