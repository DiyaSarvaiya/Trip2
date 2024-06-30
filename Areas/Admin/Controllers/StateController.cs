using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class StateController : Controller
    {
		public IConfiguration Configuration;

		public StateController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IActionResult Index()
        {
            return View();
        }
        public IActionResult StateList()
        {
			SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
			DataTable dt = new DataTable();
			sqlconnection.Open();
			SqlCommand sqlcmd = sqlconnection.CreateCommand();
			sqlcmd.CommandType = CommandType.StoredProcedure;
			sqlcmd.CommandText = "[dbo].[PR_Admin_State_SelectAll]";
			SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
			dt.Load(sqlDataReader);
			return View("State",dt);
        }

		[HttpPost]
		public IActionResult Insert(StateModel modelState)
		{
			SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

			sqlconnection.Open();

			SqlCommand sqlCmd = sqlconnection.CreateCommand();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			if (modelState.StateID == null)
			{
				sqlCmd.CommandText = "[dbo].[PR_Admin_State_Insert]";
			}
			else
			{
				sqlCmd.CommandText = "[dbo].[PR_Admin_State_Update]";
				sqlCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelState.StateID;

			}

			sqlCmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = modelState.StateName;
			sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelState.CountryID;
			sqlCmd.ExecuteNonQuery();
			sqlconnection.Close();
			return RedirectToAction("StateList");
		}

		public IActionResult AddEdit(int? StateID)
		{
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();

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

            if (StateID != null)
			{
				

				SqlCommand sqlcmd = sqlconnection.CreateCommand();
				sqlcmd.CommandType = CommandType.StoredProcedure;
				sqlcmd.CommandText = "[dbo].[PR_Admin_State_SelectByID]";
				sqlcmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
				DataTable dt = new DataTable();
				SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
				StateModel STM = new StateModel();


				if (sqlDataReader.HasRows)
				{
					while (sqlDataReader.Read())
					{
						STM.StateName = sqlDataReader["StateName"].ToString();
						STM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);

					}
				}
				return View("AddEditState", STM);

			}

			return View("AddEditState");
		}
		public IActionResult Delete(int StateID)
		{

			SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
			sqlconnection.Open();
			SqlCommand sqlcmd = sqlconnection.CreateCommand();
			sqlcmd.CommandType = CommandType.StoredProcedure;
			sqlcmd.CommandText = "[dbo].[PR_Admin_State_Delete]";
			sqlcmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
			sqlcmd.ExecuteNonQuery();
			return RedirectToAction("StateList");
		}

        public IActionResult FilterStates(string stateName, string countryName)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_State_Filter]";
            sqlcmd.Parameters.AddWithValue("stateName", stateName);
            sqlcmd.Parameters.AddWithValue("countryName", countryName);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("State", dt);
        }
    }
}
