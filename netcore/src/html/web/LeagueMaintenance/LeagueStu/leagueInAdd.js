﻿
var random1 = $.random();
render([
    group([
showInput('学号', 'league-league_member_id','', '4'),
showInput('姓名', 'league-name', '', '4', { min: 1, max: 50 }),
showInput('性别', 'league-sex', '', '4', { min: 1, max: 50 }),
showInput('民族', 'league-nation', '', '4', { min: 1, max: 50 }),
showInput('籍贯', 'league-birthplace', '', '4', { min: 1, max: 50 }),
showInput('年级', 'league-grade', '', '4', { min: 1, max: 50 }),
showInput('专业', 'league-professional', '', '4', { min: 1, max: 50 }),
showInput('班级', 'league-league_organization', '', '4', { min: 1, max: 20 }),
time('入团时间', 'league-joinleague_time', '', '4', '请选择入团时间'),
hide('immigrationapply-immigrationapply_id', random1),
hide('immigrationapply-league_member_id','#uid'),
hideTime('immigrationapply-apply_time', '#now'),
hide('immigrationapply-apply_state', 0),
//showTime('迁入时间', 'immigrationapply-immigration_time', '', '4', '请选择迁入时间'),('迁入单位', 'immigrationapply-immigrate_to_organization', '', '4', {min:1,max:50}),
input('来源单位', 'immigrationapply-immigrate_from_organization', '', '4', { min: 1, max: 50 }),
select('迁入单位', 'immigrationapply-immigrate_to_organization', 'ecampus.twxt2.league_branch@items&name=name'),
area('迁入原因', 'immigrationapply-immigration_reason', '', '1', { min: 1, max: 50 }),
file('档案', 'league-record_file'),
button("提交申请", '1:2', Color.green, function () {
    $('#league-league_member_id').val();
    $('#immigrationapply-league_member_id').val();
    $('#immigrationapply-immigrationapply_id').val(random1);
    $('#immigrationapply-apply_state').val(0);
    $('#immigrationapply-apply_time').val();
    $('#immigrationapply-immigrate_to_organization').val();
    $('#immigrationapply-immigration_reason').val();
    $('#league-joinleague_time').val();
    $('#immigrationapply-immigration_reason').val();
    $('#league-record_file').val();
    $('#immigrationapply-immigrate_from_organization').val();
    submitForm("ecampus.twxt2.league@update-_uid|immigrationapply@add");   
}),
    ], '申请迁入')], '', 'ecampus.twxt2.league@find&id=_uid', '申请迁入');

