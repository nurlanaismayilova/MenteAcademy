using Microsoft.AspNetCore.Mvc;

namespace sitee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
