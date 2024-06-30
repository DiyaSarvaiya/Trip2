using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Trip2.Areas.Destination.Models;
using NuGet.Protocol.Plugins;
using Trip2.Areas.Admin.Models;
using Trip2.BAL;

namespace Trip2.Areas.Destination.Controllers
{
    [Area("Destination")]
    [Route("[controller]/[action]")]
    public class DestinationController : Controller
    {
        public IConfiguration Configuration;
        private bool LoadFilter = true;
        public DestinationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {

            return View();
        }
        #region LoadPlaceFilter
        public void LoadPlaceFilter()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.CommandText = "[dbo].[PR_Filter_CityName_City]";
            SqlDataReader sqlDataReader1 = sqlcmd1.ExecuteReader();
            DataTable cityDt = new DataTable();

            cityDt.Load(sqlDataReader1);
            List<CityModel> CityList = new List<CityModel>();
            foreach (DataRow dr in cityDt.Rows)
            {
                CityModel cityObj = new CityModel();
                cityObj.CityID = Convert.ToInt32(dr["CityID"]);
                cityObj.CityName = dr["CityName"].ToString();
                CityList.Add(cityObj);
            }
            ViewBag.CityList = CityList;

            SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            sqlcmd2.CommandText = "[dbo].[PR_Filter_StateName_State]";
            SqlDataReader sqlDataReader2 = sqlcmd2.ExecuteReader();
            DataTable stateDt = new DataTable();
            stateDt.Load(sqlDataReader2);
            List<StateModel> StateList = new List<StateModel>();
            foreach (DataRow dr in stateDt.Rows)
            {
                StateModel stateObj = new StateModel();
                stateObj.StateID = Convert.ToInt32(dr["StateID"]);
                stateObj.StateName = dr["StateName"].ToString();
                StateList.Add(stateObj);
            }
            ViewBag.StateList = StateList;


            SqlCommand sqlcmd3 = sqlconnection.CreateCommand();
            sqlcmd3.CommandType = CommandType.StoredProcedure;
            sqlcmd3.CommandText = "[dbo].[PR_Filter_CountryName_Country]";
            SqlDataReader sqlDataReader3 = sqlcmd3.ExecuteReader();
            DataTable countryDt = new DataTable();
            countryDt.Load(sqlDataReader3);
            List<CountryModel> CountryList = new List<CountryModel>();
            foreach (DataRow dr in countryDt.Rows)
            {
                CountryModel countryObj = new CountryModel();
                countryObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryObj.CountryName = dr["CountryName"].ToString();
                CountryList.Add(countryObj);
            }


            ViewBag.CountryList = CountryList;
            LoadFilter = false;
        }
        #endregion

        #region Deatination Detail
        public IActionResult DestinationDetail(int? DestinationID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_DestinationDetail_SelectAll]";
            sqlcmd.Parameters.AddWithValue("destinationID", DestinationID.ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("DestinationDetail", dt);



        }
        #endregion

        #region Destination GET ALL
        public IActionResult Destination()
        {

            LoadPlaceFilter();

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();
            /* using (SqlCommand command1 = new SqlCommand("[dbo].[PR_Destination_SelectAll]", sqlconnection))
                 command1.CommandType = CommandType.StoredProcedure;


             using (SqlCommand command2 = new SqlCommand("[dbo].[PR_Select_City_State_Country]", sqlconnection))
                 command2.CommandType = CommandType.StoredProcedure;*/

            /*DataTable[] dataTables = new DataTable[2];*/

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Destination_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            /*SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            sqlcmd2.CommandText = "[dbo].[PR_Select_City_State_Country]";
            SqlDataReader sqlDataReader2 = sqlcmd2.ExecuteReader();
            dt.Load(sqlDataReader2);*/

            return View("Destination", dt);
        }
        #endregion

        #region Filter : Rating

        [HttpPost]
        public IActionResult DestinationRatingFilter(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Rating_Destination]";
            sqlcmd.Parameters.AddWithValue("rating", frm["Rating"].ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            return View("Destination", dt);
        }

        #endregion

        #region Filter : Climate

        [HttpPost]
        public IActionResult ClimateFilter(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Climate_Destination]";
            sqlcmd.Parameters.AddWithValue("climate", frm["Climate"].ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            return View("Destination", dt);
        }

        #endregion

        #region Filter : Place

        [HttpPost]
        public IActionResult PlaceFilter(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Place_Destination]";
            sqlcmd.Parameters.AddWithValue("cityId", frm["CityID"].ToString());
            sqlcmd.Parameters.AddWithValue("stateId", frm["StateID"].ToString());
            sqlcmd.Parameters.AddWithValue("countryId", frm["CountryID"].ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Destination", dt);
        }

        #endregion

        #region Add To Package
        [CheckAccess]
        public IActionResult AddDestiToPackage(int? DestinationID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_UserDestination_Insert]";
            sqlcmd.Parameters.AddWithValue("destiID", DestinationID.ToString());
            sqlcmd.Parameters.AddWithValue("userID", id);
            try {
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                dt.Load(sqlDataReader);
                TempData["Desti_SuccessMessage"] = "Added into package successfully.";
            }
            catch (SqlException ex)
            {
                TempData["Desti_Error"] = "Destination Saved Alredy !";
            }
           
           
            return RedirectToAction("DestinationDetail", new { DestinationID = DestinationID.ToString() });
        }
        #endregion


    }
}
