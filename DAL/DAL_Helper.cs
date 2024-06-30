using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Trip2.DAL
{
    public class DAL_Helper
    {
        public static string connStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnectionString");
    }
}
