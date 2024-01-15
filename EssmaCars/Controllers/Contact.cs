using Microsoft.AspNetCore.Mvc;

namespace SweetEssma.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
