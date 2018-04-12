using System.Collections.Generic;

using xyj.acs.Entity;
using xyj.acs.Models;
using xyj.core.Interfaces;

namespace xyj.acs.Interfaces
{
    public interface IPermissionProvider : IAutoInject
    {
        IPermmisionService Services();
        //ͨ���û�id��ȡ�û��˵�
        List<MenuItem> GetMenuByUserId(string userId);
        //ͨ���û��û�id��ȡ��ֹ��ť
        List<button> GetForbidenButtonByUserId(string userId);
        IEnumerable<Navbar> GetNavbarByUserId(string userId);
        //�ҵ��ò˵��ĸ����˵�
        List<menu> FindFather(string menuid);
        //�û���¼
        bool Login(string userId, string userPwd);
        //�û�ע��
        bool Regist(string userId, string userPwd,  string nickName,string email="",string phone="", string userTypeId="0");
        //ͨ���û�id��ȡ�û�������Ϣ
        permission_user UserInfo(string userId);
        //ͨ���û�id���û��������û������ܶ�����
        List<permission_user> FindUsers(List<string> userIdOrUserNameArray);
        //�����û���Ϣ
        bool UpdateUser(string userId, string nickName, string userPwd = "", string email = "", string phone = "", string note = "");
        //ɾ���û�
        bool DeleteUser(string userid);
        //��ӻ���
        bool AddOrgnization(string father_id, string name, string orgnization_type_id, string orgnization_id="");
        //ͨ���û�id��ȡ��ȡ���ڻ���
        orgnization FindOrgnizationByUserId(string userid);
        //ͨ������id��ȡ��û����й����Ļ���
        List<orgnization> FindRelationByOrgnizationId(string orgnization_id);
        //ͨ���û�id��ȡ�û�ְλ
        position FindPositionByUserId(string userid);
        //��ȡһ�������µ�����ְλ
        List<position> FindPositionsByOgnizationId(string orgnization_id);
        //�û����ŵ�����
        string AddUserToOrgnization(string user_id, string orgnization_id);
        //���ְλ
        bool AddPosition(string position_id,string position_type_id, string name,string descripe="",string note="");
        //�������ְλ
        bool AddPositionToOgnization( string orgnization_id, string position_id);
        //Ϊ�û���ӽ�ɫ
        bool AddRoleToUser(string user_id,string role_id);
        //Ϊ�û�����ְλ
        bool AddPositionToUser(string orgnization_position_id,string user_id);

    }
}