using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using xyj.acs.Entity;
using xyj.acs.Services;
using xyj.core.CommonExtendMethods;
using xyj.core.Interfaces;

namespace xyj.acs.Repository
{


    public class UserRoleRepository : BaseRepository, IRepository<user_role>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            return Db.user_role.ToItems(v => v.user_role_id, t => t.user_role_id);
        }

        public string Add(user_role model)
        {
            model.user_role_id = model.user_id+"-"+ model.role_id;
            return Find(model.user_role_id) == null ? (Db.SaveAdd(model) ? Pk : null) : "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(user_role model, string note = "")
        {
            Db.user_role.AddOrUpdate(model);
            return Db.Saved();
        }

        public user_role Find(object id)
        {
            return Db.user_role.NoTrackingFind(a => a.user_role_id == (string)id);
        }

        public List<user_role> All()
        {
            return Db.user_role.NoTrackingToList();
        }

        public List<user_role> All(Func<user_role, bool> filter)
        {
            return Db.user_role.NoTrackingWhere(filter);
        }
    }
}
