namespace Trip2.Areas.Destination.Models
{
	public class DestinationModel
	{
		public int DestinationID { get; set; }
		public string Name { get; set; }
		public int CityID { get; set; }
		public string CityName { get; set; }
        public int StateID { get; set; }
        public int ContryID { get; set; }
        public string Detail { get; set; }

		public string Climate { get; set; }

		public string BestTimeToVisit { get; set; }

		public string Currency { get; set; }

		public string Language { get; set; }

		public string TravelTips { get; set; }

        public string Photo1 { get; set; }

		public double Rating { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }
	}

}
