using Microsoft.AspNetCore.Mvc;
using WebAppVide.Models;
using WebAppVide.ViewModels;

namespace WebAppVide.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository PieRepository;

        public HomeController(IPieRepository piesOfTheWeek)
        {
            PieRepository = piesOfTheWeek;
        }

        public IActionResult Index()
        {
            var piesOfTheWeek = PieRepository.PiesOfTheWeek;
            var homeViewModel = new HomeViewModel(piesOfTheWeek, "message");
            return View(homeViewModel);
        }
    }
}
