﻿render([
    group([
showinput('上级', 'orgnization-father_id', q.father_id),
input('机构id', 'orgnization-orgnization_id', q.father_id+'.'+_c.random().substring(0, 3)),
input('名称', 'orgnization-name', '', '4', { min: 1, max: 100 }),
select('类型', 'orgnization-orgnization_type_id', 'sys.core.orgnization_type@items&name=name', 'deafalt', '4'),
select('机构级别', 'orgnization-organization_level_id', 'sys.core.organization_level@items&name=name', '0', '4'),
input('子系统', 'orgnization-sub_system', '-', '4'),
input('备注', 'orgnization-note'),
input('描述', 'orgnization-descripe')
],'标题')],'sys.core.orgnization@add','','添加');