namespace Trip2.BAL
{
    public static class CommonVar
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static CommonVar()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static string? Username()
        {
            string? Username = null;
            if(_httpContextAccessor.HttpContext.Session.GetString("Username") != null) {
                Username = _httpContextAccessor.HttpContext.Session.GetString("Username");
            }
            return Username;
        }
        public static int? UserID()
        {
            int? UserID = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
            }
            return UserID;
        }
        public static string? Password()
        {
            string? Password = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("Password") != null)
            {
                Password = _httpContextAccessor.HttpContext.Session.GetString("Password");
            }
            return Password;
        }

        public static string? IsAdmin()
        {
            string? IsAdmin = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("IsAdmin") != null)
            {
                IsAdmin = _httpContextAccessor.HttpContext.Session.GetString("IsAdmin");
            }
            return IsAdmin;
        }

        public static string? HotelName()
        {
            string? HotelName = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("HotelName") != null)
            {
                HotelName = _httpContextAccessor.HttpContext.Session.GetString("HotelName");
            }
            return HotelName;
        }

        public static int? HotelID()
        {
            int? HotelID = null;
            if (_httpContextAccessor.HttpContext.Session.GetInt32("HotelID") != null)
            {
                HotelID = _httpContextAccessor.HttpContext.Session.GetInt32("HotelID");
            }
            return HotelID;
        }
    }
}
