using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
