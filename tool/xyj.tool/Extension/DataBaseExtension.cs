﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using xyj.core.CommonExtendMethods;

namespace xyj.tool.Extension
{
   public static class DataBaseExtension
    {
       private static string connStr
       {
           get { return ConfigurationManager.ConnectionStrings["sys.core"].ConnectionString; }
       }
        /// <summary>
        /// 返回查询内容
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
       public static List<List<string>> ExecuteQuery(this string sql,string connStringKey="")
        {
            var result = new List<List<string>>();
            try
            {
                 result= sql.ExecuteReader2(connStringKey == "" ? connStr : ConfigurationManager.ConnectionStrings[connStringKey].ConnectionString);   
            }
            catch (Exception )
            {
                MessageBox.Show("未找到数据库" + connStringKey + "的ConnectionStrings");
            }
           return result ;

       }
    }
}
