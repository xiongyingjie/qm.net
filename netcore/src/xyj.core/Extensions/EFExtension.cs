using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using xyj.core.Config;
using xyj.core.Utility;

namespace xyj.core.Extensions
{
  public static class EFExtension
    {
        public static void InitConnection(this DbContextOptionsBuilder optionsBuilder,string dbName = "")
        {
            switch (Setting.DbClientType)
            {
                case ClientType.MySql:
                    optionsBuilder.UseMySQL(DbUtility.ConnString(dbName));break;
                case ClientType.SqlSever:
                    optionsBuilder.UseSqlServer(DbUtility.ConnString(dbName)); break;
                case ClientType.Oracle:
                    throw new NotSupportedException();
                    //optionsBuilder.UseOracle(DbUtility.ConnString(dbName)); break;
                default: throw new NotSupportedException();
            }
           
        }
    }
}
