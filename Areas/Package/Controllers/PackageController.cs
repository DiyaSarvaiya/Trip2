using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Package.Models;
using Trip2.BAL;
namespace Trip2.Areas.Package.Controllers
{
    [Area("Package")]
    [Route("[controller]/[action]")]
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

        [CheckAccess]
        public IActionResult Package()
        {
            var dataForPartialView1 = GetDataFromTable1();
            var dataForPartialView2 = GetDataFromTable2();
            var dataForPartialView3 = GetDataFromTable3();
            var totalCost = CalculateTotalCost(dataForPartialView2, "FlightTotalCost", dataForPartialView3 , "HotelTotalCost");


            var packageModel = new PackageModel
            {
                userDestiModel = dataForPartialView1,
                userFlightModel = dataForPartialView2,
                userHotelModel = dataForPartialView3,
                TotalCost = totalCost
            };

            return View("Package", packageModel);

        }

        private DataTable GetDataFromTable1()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt1 = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");

            SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.CommandText = "[dbo].[PR_UserDestination_SelectAll]";
            sqlcmd1.Parameters.Add("@userID", SqlDbType.Int).Value = id;
            SqlDataReader sqlDataReader1 = sqlcmd1.ExecuteReader();
            dt1.Load(sqlDataReader1);
            sqlconnection.Close();
            return dt1;
        }
        private DataTable GetDataFromTable2()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt2 = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");

            SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            sqlcmd2.CommandText = "[dbo].[PR_UserFlight_SelectAll]";
            sqlcmd2.Parameters.Add("@userID", SqlDbType.Int).Value = id;
            SqlDataReader sqlDataReader2 = sqlcmd2.ExecuteReader();
            dt2.Load(sqlDataReader2);
            sqlconnection.Close();
            return dt2;
        }
        private DataTable GetDataFromTable3()
        {
            SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlconnection.Open();
            DataTable dt3 = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("UserID");

            SqlCommand sqlcmd3 = sqlconnection.CreateCommand();
            sqlcmd3.CommandType = CommandType.StoredProcedure;
            sqlcmd3.CommandText = "[dbo].[PR_UserHotel_selectAll]";
            sqlcmd3.Parameters.Add("@userID", SqlDbType.Int).Value = id;
            SqlDataReader sqlDataReader3 = sqlcmd3.ExecuteReader();
            dt3.Load(sqlDataReader3);
            sqlconnection.Close();
            return dt3;
        }

        private decimal CalculateTotalCost(DataTable dataForPartialView2,string costColumn2, DataTable dataForPartialView3,string costColumn3)
        {
            decimal totalCost = 0;

            totalCost += CalculateCostFromTable(dataForPartialView2,costColumn2);
            totalCost += CalculateCostFromTable(dataForPartialView3,costColumn3);

            return totalCost;
        }

        private decimal CalculateCostFromTable(DataTable dataTable,string costColumn)
        {
            decimal cost = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                if (decimal.TryParse(row[costColumn].ToString(), out decimal rowCost))
                {
                    cost += rowCost;
                }
                
            }

            return cost;
        }
        /* public async Task<IActionResult> Package()
         {
             SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
             PackageModel pkm = new PackageModel();
             int id = (int)HttpContext.Session.GetInt32("UserID");
             await sqlconnection.OpenAsync();


             SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
             sqlcmd1.CommandType = CommandType.StoredProcedure;
            /*sqlcmd1.CommandText = "[dbo].[PR_UserDestination_SelectAll]";
            sqlcmd1.Parameters.Add("@userID", SqlDbType.Int).Value = id;

            using (SqlDataReader reader1 =  sqlcmd1.ExecuteReader())
            {
                string name = reader1["DestiName"].ToString();
                // Process data reader
                // Example:
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        pkm.userDestiModel.DestiName = reader1["DestiName"].ToString();
                        pkm.userDestiModel.CityName = reader1["CityName"].ToString();
                        pkm.userDestiModel.StateName = reader1["StateName"].ToString();
                        pkm.userDestiModel.CountryName = reader1["CountryName"].ToString();
                        pkm.userDestiModel.DestiPhoto = reader1["DestiPhoto"].ToString();
                        // Read data and populate PackageModel or do whatever you need
                    }
                }

            }


            SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            sqlcmd2.CommandText = "[dbo].[PR_UserFlight_SelectAll]";
            sqlcmd2.Parameters.Add("@userID", SqlDbType.Int).Value = id;
            using (SqlDataReader reader2 = await sqlcmd2.ExecuteReaderAsync())
            {
                // Process data reader
                // Example:
                while (await reader2.ReadAsync())
                {
                    pkm.userFlightModel.AirlineName = reader2["AirlineName"].ToString();
                    pkm.userFlightModel.FlightNo = reader2["FlightNo"].ToString();
                    pkm.userFlightModel.FlightPhoto = reader2["FlightPhoto"].ToString();
                    pkm.userFlightModel.DepartureCity = reader2["DepartureCity"].ToString();
                    pkm.userFlightModel.DestinationCity = reader2["DestinationCity"].ToString();
                    pkm.userFlightModel.FlightType = reader2["FlightType"].ToString();
                    pkm.userFlightModel.Class = reader2["Class"].ToString();
                    pkm.userFlightModel.FlightPrice = Convert.ToDouble(reader2["FlightPrice"]);
                    pkm.userFlightModel.FlightAdults = Convert.ToInt32(reader2["FlightAdults"]);
                    pkm.userFlightModel.FlightChildren = Convert.ToInt32(reader2["FlightChildren"]);
                    // Read data and populate PackageModel or do whatever you need
                }

            }

            SqlCommand sqlcmd3 = sqlconnection.CreateCommand();
            sqlcmd3.CommandType = CommandType.StoredProcedure;
            sqlcmd3.CommandText = "[dbo].[PR_UserHotel_SelectAll]";
            sqlcmd3.Parameters.Add("@userID", SqlDbType.Int).Value = id;
            using (SqlDataReader reader3 = await sqlcmd2.ExecuteReaderAsync())
            {
                // Process data reader
                // Example:
                while (await reader3.ReadAsync())
                {
                    pkm.userHotelModel.HotelName = reader3["HotelName"].ToString();
                    pkm.userHotelModel.HotelPhoto = reader3["HotelPhoto"].ToString();
                    pkm.userHotelModel.RoomType = reader3["RoomType"].ToString();
                    pkm.userHotelModel.HotelPrice = Convert.ToDouble(reader3["HotelPrice"]);
                    pkm.userHotelModel.CheckIn = Convert.ToDateTime(reader3["CheckIn"]);
                    pkm.userHotelModel.CheckOut = Convert.ToDateTime(reader3["CheckOut"]);
                    pkm.userHotelModel.HotelAdults = Convert.ToInt32(reader3["HotelAdults"]);
                    pkm.userHotelModel.HotelChildren = Convert.ToInt32(reader3["HotelChildren"]);
                    // Read data and populate PackageModel or do whatever you need
                }

            }
            return View("Package", pkm);*/
        /*SqlCommand sqlcmd = sqlconnection.CreateCommand();
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.CommandText = "[dbo].[PR_Package_SelectAll]";
        sqlcmd.Parameters.Add("@userID", SqlDbType.Int).Value = id;
        SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                pkm.DestiName = sqlDataReader["DestiName"].ToString();
                pkm.CityName = sqlDataReader["CityName"].ToString();
                pkm.StateName =sqlDataReader["StateName"].ToString();
                pkm.CountryName = sqlDataReader["CountryName"].ToString();
                pkm.DestiPhoto = sqlDataReader["DestiPhoto"].ToString();
                pkm.AirlineName = sqlDataReader["AirlineName"].ToString();
                pkm.FlightNo = sqlDataReader["FlightNo"].ToString();
                pkm.FlightPhoto = sqlDataReader["FlightPhoto"].ToString();
                pkm.DepartureCity = sqlDataReader["DepartureCity"].ToString();
                pkm.DestinationCity = sqlDataReader["DestinationCity"].ToString();
                pkm.FlightType = sqlDataReader["FlightType"].ToString();
                pkm.Class = sqlDataReader["Class"].ToString();

                pkm.FlightPrice = Convert.ToDouble(sqlDataReader["FlightPrice"]);
                pkm.FlightAdults = Convert.ToInt32(sqlDataReader["FlightAdults"]);
                pkm.FlightChildren = Convert.ToInt32(sqlDataReader["FlightChildren"]);
                pkm.HotelName = sqlDataReader["HotelName"].ToString();
                pkm.HotelPhoto = sqlDataReader["HotelPhoto"].ToString();
                pkm.RoomType = sqlDataReader["RoomType"].ToString();
                pkm.HotelPrice = Convert.ToDouble(sqlDataReader["HotelPrice"]);
                pkm.CheckIn = Convert.ToDateTime(sqlDataReader["CheckIn"]);
                pkm.CheckOut = Convert.ToDateTime(sqlDataReader["CheckIn"]);
                pkm.HotelAdults = Convert.ToInt32(sqlDataReader["HotelAdults"]);
                pkm.HotelChildren = Convert.ToInt32(sqlDataReader["HotelChildren"]);
                pkm.Class = sqlDataReader["Class"].ToString();

            }
        }*/
        /*return View("Package",pkm);*/
        /*}*/
        public IActionResult Payment()
        {
            return View("Payment");
        }
    }
}
