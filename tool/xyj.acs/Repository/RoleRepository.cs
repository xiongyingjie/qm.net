using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using xyj.core.CommonExtendMethods;
using xyj.core.Interfaces;
using xyj.acs.Entity;
using xyj.acs.Services;


namespace xyj.acs.Repository
{


    public class RoleRepository : BaseRepository, IRepository<role>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            return Db.role.ToItems( v => v.role_id, t => t.name);
        }

        public string Add(role model)
        {
            model.role_id = Pk;
            return Find(model.role_id) == null ? (Db.SaveAdd(model) ? Pk : null) : "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(role model, string note = "")
        {
            Db.role.AddOrUpdate(model);
            return Db.Saved();
        }

        public role Find(object id)
        {
            return Db.role.NoTrackingFind(a => a.role_id == (string)id);
        }

        public List<role> All()
        {
            return Db.role.NoTrackingToList();
        }

        public List<role> All(Func<role, bool> filter)
        {
            return Db.role.NoTrackingWhere(filter);
        }
    }
}
