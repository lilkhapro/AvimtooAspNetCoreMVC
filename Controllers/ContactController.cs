using Microsoft.AspNetCore.Mvc;

namespace WebAppVide.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
