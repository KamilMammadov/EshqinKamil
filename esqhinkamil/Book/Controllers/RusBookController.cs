using Microsoft.AspNetCore.Mvc;

namespace esqhinkamil.Book.Controllers
{
    public class RusBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
