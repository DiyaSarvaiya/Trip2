using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class FlightController : Controller
    {
        public IConfiguration Configuration;

        public FlightController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FlightList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Flight_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Flight",dt);
        }

        #region GET BY ID
        public IActionResult FlightByID(int? FlightID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Flight_SelectByID]";
            sqlcmd.Parameters.Add("@FlightID", SqlDbType.Int).Value = FlightID;

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("FlightByID", dt);
        }
        #endregion

        public IActionResult Insert(FlightModel modelFlight)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region Upload Image 
            if (modelFlight.File != null)
            {
                string FilePath = "wwwroot\\flight";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelFlight.File.FileName);
                modelFlight.Photo = FilePath.Replace("wwwroot\\", "/") + "/" + modelFlight.File.FileName;
                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelFlight.File.CopyTo(fileStream);
                }
            }
            #endregion

            if (modelFlight.FlightID == null || modelFlight.FlightID == 0)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Flight_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Flight_Update]";
                sqlCmd.Parameters.Add("@FlightID", SqlDbType.Int).Value = modelFlight.FlightID;
            }

            sqlCmd.Parameters.Add("@AirlineName", SqlDbType.VarChar).Value = modelFlight.AirlineName;
            sqlCmd.Parameters.Add("@FlightNo", SqlDbType.VarChar).Value = modelFlight.FlightNo;
            sqlCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelFlight.CityID;
            sqlCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelFlight.StateID;
            sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelFlight.CountryID;
            sqlCmd.Parameters.Add("@DepartureCity", SqlDbType.VarChar).Value = modelFlight.DepartureCity;
            sqlCmd.Parameters.Add("@Stops", SqlDbType.Int).Value = modelFlight.Stops;
            sqlCmd.Parameters.Add("@DepartureTime", SqlDbType.DateTime).Value = modelFlight.DepartureTime;
            sqlCmd.Parameters.Add("@ArrivalTime", SqlDbType.DateTime).Value = modelFlight.ArrivalTime;
            sqlCmd.Parameters.Add("@DestinationCity", SqlDbType.VarChar).Value = modelFlight.DestinationCity;
            sqlCmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = modelFlight.Type;
            sqlCmd.Parameters.Add("@Class", SqlDbType.VarChar).Value = modelFlight.Class;
            sqlCmd.Parameters.Add("@Duration", SqlDbType.DateTime).Value = modelFlight.Duration;
            sqlCmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = modelFlight.Price;
            sqlCmd.Parameters.Add("@Photo", SqlDbType.VarChar).Value = modelFlight.Photo;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("FlightList");
        }

        public IActionResult AddEdit(int? FlightID)
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

            if (FlightID != null)
            {
                SqlCommand sqlcmd = sqlconnection.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "[dbo].[PR_Admin_Flight_SelectByID]";
                sqlcmd.Parameters.Add("@FlightID", SqlDbType.Int).Value = FlightID;
                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                FlightModel CTM = new FlightModel();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        CTM.AirlineName = sqlDataReader["AirlineName"].ToString();
                        CTM.FlightNo = sqlDataReader["FlightNo"].ToString();
                        CTM.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
                        CTM.StateID = Convert.ToInt32(sqlDataReader["StateID"]);
                        CTM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);
                        CTM.DepartureCity = sqlDataReader["DepartureCity"].ToString();
                        CTM.Stops = Convert.ToInt32(sqlDataReader["Stops"]);
                        CTM.DepartureTime = Convert.ToDateTime(sqlDataReader["DepartureTime"]);
                        CTM.ArrivalTime = Convert.ToDateTime(sqlDataReader["ArrivalTime"]);
                        CTM.DestinationCity = sqlDataReader["DestinationCity"].ToString();
                        CTM.Type = sqlDataReader["Type"].ToString();
                        CTM.Class = sqlDataReader["Class"].ToString();
                        CTM.Duration = Convert.ToDateTime(sqlDataReader["Duration"]);
                        CTM.Price = Convert.ToDouble(sqlDataReader["Price"]);
                        CTM.Photo = sqlDataReader["Photo"].ToString();

                    }
                }
                return View("AddEditFlight", CTM);

            }

            return View("AddEditFlight");
        }

        public IActionResult Delete(int FlightID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Flight_Delete]";
            sqlcmd.Parameters.Add("@FlightID", SqlDbType.Int).Value = FlightID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("FlightList");
        }

        public IActionResult FilterFlights(string airlineName, string departureCity, string destinationCity,string Class)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Flight_Filter]";
            sqlcmd.Parameters.AddWithValue("airlineName", airlineName);
            sqlcmd.Parameters.AddWithValue("departureCity", departureCity);
            sqlcmd.Parameters.AddWithValue("destinationCity", destinationCity);
            sqlcmd.Parameters.AddWithValue("class", Class);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Flight", dt);
        }
    }
}
