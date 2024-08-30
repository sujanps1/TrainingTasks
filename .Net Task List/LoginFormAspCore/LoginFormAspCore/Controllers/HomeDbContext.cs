using Microsoft.AspNetCore.Mvc;

namespace LoginFormAspCore.Controllers
{
    public class HomeDbContext : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
