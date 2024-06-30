using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class PackageController : Controller
    {
        public IConfiguration Configuration;

        public PackageController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult PackageList()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            DataTable dt = new DataTable();
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Package_SelectAll]";
            SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("Package",dt);
        }
       /* public IActionResult Insert(PackageModel modelPackage)
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            sqlconnection.Open();

            SqlCommand sqlCmd = sqlconnection.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (modelPackage.PackageID == null || modelPackage.PackageID == 0)
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Package_Insert]";
            }
            else
            {
                sqlCmd.CommandText = "[dbo].[PR_Admin_Package_Update]";
                sqlCmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = modelPackage.PackageID;

            }

            sqlCmd.Parameters.Add("@DestinationID", SqlDbType.Int).Value = modelPackage.DestinationID;
            sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = modelPackage.UserID;
            sqlCmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = modelPackage.StartDate;
            sqlCmd.Parameters.Add("@Duration", SqlDbType.VarChar).Value = modelPackage.Duration;
            sqlCmd.Parameters.Add("@Nights", SqlDbType.Int).Value = modelPackage.Nights;
            sqlCmd.Parameters.Add("@Adults", SqlDbType.Decimal).Value = modelPackage.Adults;
            sqlCmd.Parameters.Add("@UserHotelID", SqlDbType.VarChar).Value = modelPackage.UserHotelID;
            sqlCmd.Parameters.Add("@UserFlightID", SqlDbType.VarChar).Value = modelPackage.UserFlightID;
            sqlCmd.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("PackageList");
        }

        public IActionResult AddEdit(int? PackageID)
        {
            if (PackageID != null)
            {
                SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

                sqlconnection.Open();

                SqlCommand sqlcmd = sqlconnection.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "[dbo].[PR_Admin_Package_SelectByID]";
                sqlcmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = PackageID;
                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
                PackageModel CTM = new PackageModel();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        CTM.Name = sqlDataReader["Name"].ToString();
                        CTM.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
                        CTM.StateID = Convert.ToInt32(sqlDataReader["StateID"]);
                        CTM.CountryID = Convert.ToInt32(sqlDataReader["CountryID"]);
                        CTM.PricePerNight = Convert.ToDouble(sqlDataReader["PricePerNight"]);
                        CTM.Review = Convert.ToInt32(sqlDataReader["Review"]);
                        CTM.Rating = Convert.ToDouble(sqlDataReader["Rating"]);
                        CTM.Photo1 = sqlDataReader["Photo1"].ToString();
                        CTM.AmenitiesCount = Convert.ToInt32(sqlDataReader["AmenitiesCount"]);

                    }
                }
                return View("AddEditPackage", CTM);

            }

            return View("AddEditPackage");
        }*/

        public IActionResult Delete(int PackageID)
        {

            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            SqlCommand sqlcmd = sqlconnection.CreateCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[dbo].[PR_Admin_Package_Delete]";
            sqlcmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = PackageID;
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("PackageList");
        }
    }
}
