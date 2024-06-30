using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
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

        #region Hotel List
        public IActionResult HotelList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Hotel_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Hotel",dt);
        }
        #endregion

        #region Insert
        public IActionResult Insert(HotelModel modelHotel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region Upload Image 
            if (modelHotel.File != null)
            {
                string FilePath = "wwwroot\\hotel";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelHotel.File.FileName);
                modelHotel.Photo1 = FilePath.Replace("wwwroot\\", "/") + "/" + modelHotel.File.FileName;
                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelHotel.File.CopyTo(fileStream);
                }
            }
            #endregion

            if (modelHotel.HotelID == null || modelHotel.HotelID == 0)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Hotel_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Hotel_Update]";
                sqlCmd.Parameters.Add("@HotelID", SqlDbType.Int).Value = modelHotel.HotelID;
            }

            sqlCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = modelHotel.Name;
            sqlCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelHotel.CityID;
            sqlCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelHotel.StateID;
            sqlCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelHotel.CountryID;
            sqlCmd.Parameters.Add("@PricePerNight", SqlDbType.VarChar).Value = modelHotel.PricePerNight;
            sqlCmd.Parameters.Add("@Review", SqlDbType.Int).Value = modelHotel.Review;
            sqlCmd.Parameters.Add("@Rating", SqlDbType.Decimal).Value = modelHotel.Rating;
            sqlCmd.Parameters.Add("@Photo1", SqlDbType.VarChar).Value = modelHotel.Photo1;
            sqlCmd.Parameters.Add("@AmenitiesCount", SqlDbType.VarChar).Value = modelHotel.AmenitiesCount;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("HotelList");
        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(int? HotelID)
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


            if (HotelID != null)
            {
                SqlCommand sqlcmd = sqlconnection.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "[dbo].[PR_Admin_Hotel_SelectByID]";
                sqlcmd.Parameters.Add("@HotelID", SqlDbType.Int).Value = HotelID;
                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                HotelModel CTM = new HotelModel();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        CTM.Name = sqlDataReader["Name"].ToString();
                        CTM.CityName = sqlDataReader["CityName"].ToString();
                        CTM.StateName = sqlDataReader["StateName"].ToString();
                        CTM.CountryName = sqlDataReader["CountryName"].ToString();
                        CTM.PricePerNight = Convert.ToDouble(sqlDataReader["PricePerNight"]);
                        CTM.Review = Convert.ToInt32(sqlDataReader["Review"]);
                        CTM.Rating = Convert.ToDouble(sqlDataReader["Rating"]);
                        CTM.Photo1 = sqlDataReader["Photo1"].ToString();
                        CTM.AmenitiesCount = Convert.ToInt32(sqlDataReader["AmenitiesCount"]);

                    }
                }
                return View("AddEditHotel", CTM);

            }

            return View("AddEditHotel");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int HotelID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Hotel_Delete]";
            sqlcmd.Parameters.Add("@HotelID", SqlDbType.Int).Value = HotelID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("HotelList");
        }
        #endregion

        #region GET BY ID
        public IActionResult HotelByID(int? HotelID)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Hotel_SelectByID]";
            sqlcmd.Parameters.Add("@HotelID", SqlDbType.Int).Value = HotelID;

            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("HotelByID", dt);
        }
        #endregion

        public IActionResult FilterHotels(string name, string city, string pricePerNight,double rating)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();

            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Hotel_Filter]";
            sqlcmd.Parameters.AddWithValue("name", name);
            sqlcmd.Parameters.AddWithValue("city", city);
            sqlcmd.Parameters.AddWithValue("pricePerNight", pricePerNight);
            sqlcmd.Parameters.AddWithValue("rating", rating);
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Hotel", dt);
        }
    }
}
