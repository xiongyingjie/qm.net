using System.Configuration;

namespace xyj.acs.Configs
{
   public static class Setting
   {
       public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["sys.core"].ConnectionString;
   }
}
