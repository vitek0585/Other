using System.Configuration;
using System.Web.Caching;
using System.Web.Configuration;

namespace WorkDataMVC.Models.CacheModel
{
    public static class SqlDependencyAdmin
    {
        public static void InitialSqlDependencyDataBase()
        {
            var conStr = ConfigurationManager.ConnectionStrings["WorkDbContext"].ConnectionString;
            SqlCacheDependencyAdmin.EnableNotifications(conStr);
            SqlCacheDependencyAdmin.EnableTableForNotifications(conStr,"Employee");
        }
    }
}