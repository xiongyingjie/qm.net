﻿render(function () {
    subSetTitle("添加赛事项目");
    return [
   group([

input('项目名称', 'name', '', '4', { min: 1, max: 50 }),
area('简单描述', 'note'),

   ], '')]
}, '/sports/Admin/matchTypeAdd', '');

