using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.BAL;

namespace Trip2.Areas.Room.Controllers
{
    [Area("Room")]
    [Route("[controller]/[action]")]
    public class RoomController : Controller
    {
        public IConfiguration Configuration;
        public RoomController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Room GET ALL
        public IActionResult GetAllRoom(int? HotelID, string Name)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Room_SelectAll]";
            sqlcmd.Parameters.AddWithValue("HotelID", HotelID.ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Room", dt);
        }
        #endregion

        #region Filter : Rating

        [HttpPost]
        public IActionResult RoomRatingFilter(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Rating_Room]";
            sqlcmd.Parameters.AddWithValue("rating", frm["Rating"].ToString());
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            return View("Room", dt);
        }

        #endregion

        #region Room Form
        public IActionResult RoomForm()
        {
            return View("BookRoom");
        }
        #endregion

        #region Search Room
        public IActionResult SearchRoom(IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Filter_Room]";
            sqlcmd.Parameters.AddWithValue("@type", frm["Type"].ToString());

            #region Validations

            DateTime checkInDate;
            if (!DateTime.TryParse(frm["CheckIn"], out checkInDate) || checkInDate.Date < DateTime.Today)
                ViewBag.RoomCheckInDateError = "Date should be after today";

            DateTime checkOutDate;
            if (!DateTime.TryParse(frm["CheckOut"], out checkOutDate) || checkOutDate.Date < DateTime.Today)
                ViewBag.RoomCheckOutDateError = "Date should be after today";

            if (int.Parse(frm["Children"]) > 25)
                ViewBag.BookChildrenError = "Add Children and More than 25 children are not allowed";

            if (int.Parse(frm["Adults"]) > 50)
                ViewBag.BookAdultsError = "Add Adults and More than 50 Adults are not allowed";
            #endregion

            TempData["Room_Children"] = int.Parse(frm["Children"]);
            TempData["Room_Adults"] = int.Parse(frm["Adults"]);
            TempData["Room_CheckIn"] = frm["CheckIn"].ToString();
            TempData["Room_CheckOut"] = frm["CheckOut"].ToString();

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);



            return View("Room", dt);
        }
        #endregion

        #region Add To Package
        [CheckAccess]
        public IActionResult AddHotelToPackage(int? RoomID, int HotelID, IFormCollection frm)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_UserHotel_Insert]";
            sqlcmd.Parameters.AddWithValue("@hotelID", HotelID);
            sqlcmd.Parameters.AddWithValue("@roomID", RoomID);
            sqlcmd.Parameters.AddWithValue("@userID", id);
            sqlcmd.Parameters.AddWithValue("@checkIn", Convert.ToDateTime(TempData["Room_CheckIn"]));
            sqlcmd.Parameters.AddWithValue("@checkOut", Convert.ToDateTime(TempData["Room_CheckOut"]));
            sqlcmd.Parameters.AddWithValue("@adults", TempData["Room_Adults"]);
            sqlcmd.Parameters.AddWithValue("@children", TempData["Room_Children"]);
            try
            {
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                dt.Load(sqlDataReader);
                TempData["Hotel_SuccessMessage"] = "Added into package successfully.";
            }
            catch (SqlException ex)
            {
                TempData["Hotel_Error"] = "Hotel is Booked Alredy !";
            }
            return RedirectToAction("GetAllRoom", new { HotelID = HotelID });
        }
        #endregion
    }
}
