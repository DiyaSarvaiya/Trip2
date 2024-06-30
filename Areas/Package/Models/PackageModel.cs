using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace Trip2.Areas.Package.Models
{
    public class PackageModel{

        public DataTable userDestiModel { get; set; }

        public DataTable userFlightModel { get; set; }

        public DataTable userHotelModel { get; set; }

        public decimal TotalCost { get; set; }

        /* public UserDestination userDestiModel { get; set; }

         public UserFlight userFlightModel { get; set; }

         public UserHotel userHotelModel { get; set; }
 */
        /* public string DestiName {  get; set; }
         public string CityName { get; set; }
         public string StateName { get; set; }
         public string CountryName { get; set; }
         public string DestiPhoto { get; set; }
         public string AirlineName { get; set; }
         public string FlightNo { get; set; }
         public string FlightPhoto { get; set; }
         public string DepartureCity { get; set; }
         public string DestinationCity { get; set; }
         public string FlightType { get; set; }
         public string Class { get; set; }
         public double FlightPrice { get; set; }
         public int FlightAdults { get; set; }
         public int FlightChildren { get; set; }
         public string HotelName { get; set; }
         public string HotelPhoto { get; set; }
         public string RoomType { get; set; }
         public double HotelPrice { get; set; }
         public DateTime CheckIn { get; set; }
         public DateTime CheckOut { get; set; }
         public int HotelAdults { get; set; }
         public int HotelChildren { get; set; }

         public int UserID { get; set; }
         public int CityID { get; set; }
         public int StateID { get; set; }
         public int CountryID { get; set; }
         public int DestinationID { get; set; }
         public int UserDestinationID { get; set; }
         public int FlightID { get; set; }
         public int UserFlightID { get; set; }
         public int HotelID { get; set; }
         public int UserHotelID { get; set; }*/

    }
   

   
}
