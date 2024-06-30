using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class CountryController : Controller
    {
        public IConfiguration Configuration;

        public CountryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CountryList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Country_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Country",dt);
        }

        public IActionResult Insert(CountryModel modelCountry)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (modelCountry.CountryID == null)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Country_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Country_Update]";
                sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCountry.CountryID;

            }

            sqlCmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelCountry.CountryName;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("CountryList");
        }

        public IActionResult AddEdit(int? CountryID)
        {
            if (CountryID != null)
            {
                SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

                sqlconnection.Open();

                SqlCommand sqlcmd = sqlconnection.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "[dbo].[PR_Admin_Country_SelectByID]";
                sqlcmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                CountryModel CTM = new CountryModel();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        CTM.CountryName = sqlDataReader["CountryName"].ToString();
                        CTM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);

                    }
                }
                return View("AddEditCountry", CTM);

            }

            return View("AddEditCountry");
        }

        public IActionResult Delete(int CountryID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Country_Delete]";
            sqlcmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("CountryList");
        }

        public IActionResult FilterCountries(string countryName)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Country_Filter]";
            sqlcmd.Parameters.AddWithValue("countryName", countryName);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Country", dt);
        }
    }
}
