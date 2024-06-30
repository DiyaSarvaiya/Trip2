using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class AdminController : Controller
    {
        public IConfiguration Configuration;
        public AdminController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_OverAll_Count]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader); 
            return View("Admin",dt);
        }

        
    }
}
