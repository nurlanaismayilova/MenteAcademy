using Microsoft.AspNetCore.Mvc;

namespace sitee.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
