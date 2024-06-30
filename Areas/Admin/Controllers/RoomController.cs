using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;
using Trip2.Areas.Hotel.Models;
using Microsoft.AspNetCore.Http;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
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
        public IActionResult RoomList(int HotelID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Room_SelectAll]";
            sqlcmd.Parameters.AddWithValue("hotelID", HotelID);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);

            HttpContext.Session.SetInt32("HotelID", HotelID);

            return View("Room", dt);
        }
        public IActionResult Insert(RoomModel modelRoom)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region Upload Image 
            if (modelRoom.File != null)
            {
                string FilePath = "wwwroot\\room";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelRoom.File.FileName);
                modelRoom.Photo = FilePath.Replace("wwwroot\\", "/") + "/" + modelRoom.File.FileName;
                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelRoom.File.CopyTo(fileStream);
                }
            }
            #endregion

            if (modelRoom.RoomID == null || modelRoom.RoomID == 0)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Room_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Room_Update]";
                sqlCmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = modelRoom.RoomID;

            }

            sqlCmd.Parameters.Add("@HotelID", SqlDbType.Int).Value = modelRoom.HotelID;
            sqlCmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = modelRoom.Type;
            sqlCmd.Parameters.Add("@Rating", SqlDbType.Decimal).Value = modelRoom.Rating;
            sqlCmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = modelRoom.Price;
            sqlCmd.Parameters.Add("@Photo", SqlDbType.VarChar).Value = modelRoom.Photo;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("RoomList", new { HotelID = modelRoom.HotelID });
        }

        public IActionResult AddEdit(int? RoomID, int HotelID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();

            #region DropDown : Hotel

            SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.CommandText = "[dbo].[PR_DropDown_Hotel]";
            SqlDataReader sqlDataReader1 = sqlcmd1.ExecuteReader();
            DataTable hotelDt = new DataTable();
            hotelDt.Load(sqlDataReader1);
            List<HotelModel> HotelList = new List<HotelModel>();
            foreach (DataRow dr in hotelDt.Rows)
            {
                HotelModel hotelObj = new HotelModel();
                hotelObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                hotelObj.Name = dr["Name"].ToString();
                HotelList.Add(hotelObj);
            }
            ViewBag.HotelList = HotelList;

            #endregion
            try
            {

                if (RoomID != null)
                {
                    SqlCommand sqlcmd = sqlconnection.CreateCommand();
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "[dbo].[PR_Admin_Room_SelectByID]";
                    sqlcmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = RoomID;
                    DataTable dt = new DataTable();
                    SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                    RoomModel CTM = new RoomModel();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CTM.HotelID = Convert.ToInt32(sqlDataReader["HotelID"]);
                            CTM.HotelName = sqlDataReader["HotelName"].ToString();
                            CTM.Type = sqlDataReader["Type"].ToString();
                            CTM.Rating = Convert.ToDouble(sqlDataReader["Rating"]);
                            CTM.Price = Convert.ToDouble(sqlDataReader["Price"]);
                            CTM.Photo = sqlDataReader["Photo"].ToString();

                        }
                    }
                    return View("AddEditRoom", CTM);
                     
                }
                else
                {
                    RoomModel CTM = new RoomModel();
                    CTM.HotelID = HotelID;
                    return View("AddEditRoom", CTM);
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        #region GET BY ID
        public IActionResult RoomByID(int? RoomID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Room_SelectByID]";
            sqlcmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = RoomID;

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("RoomByID", dt);
        }
        #endregion

        public IActionResult Delete(int RoomID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Room_Delete]";
            sqlcmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = RoomID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("RoomList");
        }

        public IActionResult FilterRooms(string type, string price, double rating)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Room_Filter]";
            sqlcmd.Parameters.AddWithValue("type", type);
            sqlcmd.Parameters.AddWithValue("price", price);
            sqlcmd.Parameters.AddWithValue("rating", rating);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Room", dt);
        }
    }
}
