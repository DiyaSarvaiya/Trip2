using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;
using System.Data.SqlTypes;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class DestinationController : Controller
    {
        public IConfiguration Configuration;

        public DestinationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DestinationList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Destination_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Destination", dt);
        }

        #region GET BY ID
        public IActionResult DestinationByID(int? DestinationID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Destination_SelectByID]";
            sqlcmd.Parameters.Add("@DestinationID", SqlDbType.Int).Value = DestinationID;

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("DestinationByID", dt);
        }
        #endregion
        public IActionResult Insert(DestinationModel modelDestination)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
           
            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                #region Upload Image 
                if (modelDestination.File != null)
                {
                    string FilePath = "wwwroot\\destination";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileNameWithPath = Path.Combine(path, modelDestination.File.FileName);
                    modelDestination.Photo1 = FilePath.Replace("wwwroot\\", "/") + "/" + modelDestination.File.FileName;
                    using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        modelDestination.File.CopyTo(fileStream);
                    }
                }
                #endregion

                if (modelDestination.DestinationID == null || modelDestination.DestinationID == 0)
                {
                    sqlCmd.CommandText = "[dbo].[PR_Admin_Destination_Insert]";
                }
                else
                {
                    sqlCmd.CommandText = "[dbo].[PR_Admin_Destination_Update]";
                    sqlCmd.Parameters.Add("@DestinationID", SqlDbType.Int).Value = modelDestination.DestinationID;  
                }
                sqlCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = modelDestination.Name;
                sqlCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelDestination.CityID;
                sqlCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelDestination.StateID;
                sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelDestination.CountryID;
                sqlCmd.Parameters.Add("@Detail", SqlDbType.Text).Value = modelDestination.Detail;
                sqlCmd.Parameters.Add("@Climate", SqlDbType.VarChar).Value = modelDestination.Climate;
                sqlCmd.Parameters.Add("@BestTimeToVisit", SqlDbType.VarChar).Value = modelDestination.BestTimeToVisit;
                sqlCmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = modelDestination.Currency;
                sqlCmd.Parameters.Add("@Language", SqlDbType.VarChar).Value = modelDestination.Language;
                sqlCmd.Parameters.Add("@TravelTips", SqlDbType.Text).Value = modelDestination.TravelTips;
                sqlCmd.Parameters.Add("@Photo1", SqlDbType.VarChar).Value = modelDestination.Photo1;
                sqlCmd.Parameters.Add("@Rating", SqlDbType.Decimal).Value = modelDestination.Rating;
                sqlCmd.ExecuteNonQuery();
                sqlconnection.Close();
                ViewBag.Photo = modelDestination.Photo1;
                return RedirectToAction("DestinationList");
            }
            catch (Exception ex)
            {
                return null;
            }

            
        }

        public IActionResult AddEdit(int? DestinationID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();

            #region DropDown : City

            SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.CommandText = "[dbo].[PR_DropDown_City]";
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

            #endregion

            #region DropDown : State

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

            #region DropDown : Country

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

            try
            {

                if (DestinationID != null)
                {
                    SqlCommand sqlcmd = sqlconnection.CreateCommand();
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "[dbo].[PR_Admin_Destination_SelectByID]";
                    sqlcmd.Parameters.Add("@DestinationID", SqlDbType.Int).Value = DestinationID;
                    DataTable dt = new DataTable();
                    SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                    DestinationModel CTM = new DestinationModel();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CTM.Name = sqlDataReader["Name"].ToString();
                            CTM.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
                            CTM.StateID = Convert.ToInt32(sqlDataReader["StateID"]);
                            CTM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);
                            CTM.Detail = sqlDataReader["Detail"].ToString();
                            CTM.Climate = sqlDataReader["Climate"].ToString();
                            CTM.BestTimeToVisit = sqlDataReader["BestTimeToVisit"].ToString();
                            CTM.Currency = sqlDataReader["Currency"].ToString();
                            CTM.Language = sqlDataReader["Language"].ToString();
                            CTM.TravelTips = sqlDataReader["TravelTips"].ToString();
                            CTM.Photo1 = sqlDataReader["Photo1"].ToString();
                            CTM.Rating = Convert.ToDouble(sqlDataReader["Rating"]);

                        }
                    }
                    return View("AddEditDestination", CTM);

                }
                DestinationModel Des = new DestinationModel();
                return View("AddEditDestination", Des);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IActionResult Delete(int DestinationID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Destination_Delete]";
            sqlcmd.Parameters.Add("@DestinationID", SqlDbType.Int).Value = DestinationID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("DestinationList");
        }

        public IActionResult FilterDestinations(string name, string countryName,string currency , double rating)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Destination_Filter]";
            sqlcmd.Parameters.AddWithValue("name", name);
            sqlcmd.Parameters.AddWithValue("countryName", countryName);
            sqlcmd.Parameters.AddWithValue("currency", currency);
            sqlcmd.Parameters.AddWithValue("rating", rating);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Destination", dt);
        }
    }
}
