using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Trip2.Areas.Hotel.Controllers
{
    [Area("Hotel")]
    [Route("[action]")]
    public class HotelController : Controller
    {
        public IConfiguration Configuration;
        public HotelController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Hotel GET ALL
        public IActionResult Hotel()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Hotel_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Hotel", dt);
        }
		#endregion

		#region Filter : Rating

		[HttpPost]
		public IActionResult HotelRatingFilter(IFormCollection frm)
		{
			SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
			DataTable dt = new DataTable();
			sqlconnection.Open();

			SqlCommand sqlcmd = sqlconnection.CreateCommand();
			sqlcmd.CommandType = CommandType.StoredProcedure;
			sqlcmd.CommandText = "[dbo].[PR_Filter_Rating_Hotel]";
			sqlcmd.Parameters.AddWithValue("rating", frm["Rating"].ToString());
			SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
			dt.Load(sqlDataReader);

			return View("Hotel", dt);
		}

		#endregion
	}
}
