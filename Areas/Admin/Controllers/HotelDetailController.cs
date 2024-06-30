using Microsoft.AspNetCore.Mvc;

namespace Trip2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class HotelDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HotelDetailList()
        {
            return View("HotelDetail");
        }
    }
}
