using Microsoft.AspNetCore.Mvc;

namespace WebFilmApi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
