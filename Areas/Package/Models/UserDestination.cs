using System.Data;

namespace Trip2.Areas.Package.Models
{
    public class UserDestination
    {
        public string? DestiName { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string CountryName { get; set; }
        public string DestiPhoto { get; set; }

        public int UserID { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public int DestinationID { get; set; }
        public int UserDestinationID { get; set; }

       
    }
}
