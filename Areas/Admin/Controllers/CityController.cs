using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class CityController : Controller
    {
        public IConfiguration Configuration;

        public CityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CityList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_City_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("City", dt);
        }

        [HttpPost]
        public IActionResult Insert(CityModel modelCity)
        {
            
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (modelCity.CityID == null)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_City_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_City_Update]";
                sqlCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCity.CityID;

            }

            sqlCmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = modelCity.CityName;
            sqlCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCity.StateID;
            sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCity.CountryID;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();


            return RedirectToAction("CityList");
        }

        public IActionResult AddEdit(int? CityID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();

            #region State DropDown

            SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            sqlcmd2.CommandText = "[dbo].[PR_DropDown_State]";
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
            #endregion

            #region Country DropDown


            SqlCommand sqlcmd3 = sqlconnection.CreateCommand();
            sqlcmd3.CommandType = CommandType.StoredProcedure;
            sqlcmd3.CommandText = "[dbo].[PR_DropDown_Country]";
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
            #endregion

            if (CityID != null)
            {

                SqlCommand sqlcmd = sqlconnection.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "[dbo].[PR_Admin_City_SelectByID]";
                sqlcmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                CityModel CTM = new CityModel();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        CTM.CityName = sqlDataReader["CityName"].ToString();
                        CTM.StateID = Convert.ToInt32(sqlDataReader["StateID"]);
                        CTM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);

                    }
                }
                return View("AddEditCity", CTM);

            }

            return View("AddEditCity");
        }

        public IActionResult Delete(int CityID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_City_Delete]";
            sqlcmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("CityList");
        }

        public IActionResult FilterCities(string cityName, string stateName, string countryName)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_City_Filter]";
            sqlcmd.Parameters.AddWithValue("cityName", cityName);
            sqlcmd.Parameters.AddWithValue("stateName", stateName);
            sqlcmd.Parameters.AddWithValue("countryName", countryName);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("City", dt);
        }
    }
}

