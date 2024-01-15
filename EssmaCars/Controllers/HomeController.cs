using EssmaCars.Models;
using Microsoft.AspNetCore.Mvc;
using SweetEssma.ViewModels;

namespace SweetEssma.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var piesOfTheWeek = _pieRepository.PiesOfTheWeek;
            var homViewModel = new HomeViewModel(piesOfTheWeek);
            return View(homViewModel);  
        }
    }
}
