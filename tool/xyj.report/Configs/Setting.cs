using System.Configuration;

namespace xyj.report.Configs
{
    public static class Setting
    {
        public static readonly string ReportRuleConnectionString =
            ConfigurationManager.ConnectionStrings["qx.system"].ConnectionString;
    }
}