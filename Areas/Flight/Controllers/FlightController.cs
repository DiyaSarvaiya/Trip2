using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;
using Trip2.BAL;
using System.Drawing;

namespace Trip2.Areas.Flight.Controllers
{
    [Area("Flight")]
    [Route("[controller]/[action]")]
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

        public IActionResult Flight()
        {
            LoadCity();
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Flight_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Flight", dt);
        }

        #region Search Flight
        /*public IActionResult SearchFlight(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Flight]";

            if (frm["DepartureCity"].ToString() == null)
            {
                ModelState.AddModelError(frm["DepartureCity"].ToString(), "Enter Departure city");
            }

            sqlcmd.Parameters.AddWithValue("@departureCity", frm["DepartureCity"].ToString());
            sqlcmd.Parameters.AddWithValue("@destinationCity", frm["DestinationCity"].ToString());
            sqlcmd.Parameters.AddWithValue("@type", frm["Type"].ToString());
            sqlcmd.Parameters.AddWithValue("@class", frm["Class"].ToString());

            TempData["Flight_Children"] = int.Parse(frm["Children"]);
            TempData["Flight_Adults"] = int.Parse(frm["Adults"]);

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            return View("Flight", dt);
        }*/

        public IActionResult SearchFlight(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Flight]";

            #region Validations

            if (frm["DepartureCity"] == "Flying From")
                ViewBag.DepartureCityError = "Departure city is required.";

            if (frm["DestinationCity"] == "Flying To")
                ViewBag.DestinationCityError = "Destination city is required.";

            if (frm["Class"] == "Class")
                ViewBag.FlightClassError = "Flight Class is required.";

           /* if (int.Parse(frm["Children"]) == null || int.Parse(frm["Children"]) == 0 || int.Parse(frm["Children"]) > 25 )
                ViewBag.FlightChildrenError = "Add Children and More than 25 children are not allowed";

            if (int.Parse(frm["Adults"]) > 50)
                ViewBag.FlightAdultsError = "Add Adults and More than 50 Adults are not allowed";*/

            DateTime departureDate;
            if (!DateTime.TryParse(frm["DepartureDate"], out departureDate) || departureDate.Date < DateTime.Today)
                ViewBag.FlightDepartureDateError = "Date should be after today";

            DateTime arrivalDate;
            if (!DateTime.TryParse(frm["ArrivalDate"], out arrivalDate) || arrivalDate.Date < DateTime.Today)
                ViewBag.FlightArrivalDateDateError = "Date should be after today";

            // Return to the form view to display validation errors
            if (ViewBag.DepartureCityError != null || ViewBag.DestinationCityError != null || ViewBag.FlightClassError != null || ViewBag.FlightChildrenError != null || ViewBag.FlightAdultsError != null)
                return View("Flight");

            #endregion

            sqlcmd.Parameters.AddWithValue("@departureCity", frm["DepartureCity"].ToString());
            sqlcmd.Parameters.AddWithValue("@destinationCity", frm["DestinationCity"].ToString());
            sqlcmd.Parameters.AddWithValue("@type", frm["Type"].ToString());
            sqlcmd.Parameters.AddWithValue("@class", frm["Class"].ToString());

            TempData["Flight_Children"] = int.Parse(frm["Children"]);
            TempData["Flight_Adults"] = int.Parse(frm["Adults"]);

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            return View("Flight", dt);
        }
        #endregion

        public void LoadCity()
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
        }

        #region Add To Package
        /*[CheckAccess]
        public IActionResult AddFlightToPackage(int? FlightID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_UserFlight_Insert]";
            sqlcmd.Parameters.AddWithValue("@userID", id);
            sqlcmd.Parameters.AddWithValue("@flightID", FlightID);
            sqlcmd.Parameters.AddWithValue("@adults", TempData["Flight_Adults"]);
            sqlcmd.Parameters.AddWithValue("@children", TempData["Flight_Children"]);
            try
            {
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                dt.Load(sqlDataReader);
                TempData["Flight_SuccessMessage"] = "Added into package successfully.";
            }
            catch (SqlException ex)
            {
                TempData["Flight_Error"] = "Flight Booked Alredy !";
            } 
            
            return RedirectToAction("Flight", new { FlightID = FlightID });
        }*/

        [CheckAccess]
        public IActionResult AddFlightToPackage(int? FlightID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();
            FlightModel flightModel = new FlightModel();
            int id = (int)HttpContext.Session.GetInt32("UserID");
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_UserFlight_Insert]";
            sqlcmd.Parameters.AddWithValue("@userID", id);
            sqlcmd.Parameters.AddWithValue("@flightID", FlightID);
            sqlcmd.Parameters.AddWithValue("@adults", TempData["Flight_Adults"]);
            sqlcmd.Parameters.AddWithValue("@children", TempData["Flight_Children"]);
            try
            {
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                dt.Load(sqlDataReader);
                TempData["Flight_SuccessMessage"] = "Added into package successfully.";
            }
            catch (SqlException ex)
            {
                TempData["Flight_Error"] = "Flight Booked Alredy !";
            }

            return RedirectToAction("Flight", new { FlightID = FlightID });
        }
        #endregion
    }
}
