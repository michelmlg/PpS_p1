using Microsoft.AspNetCore.Mvc;
using P1_pps.Models;
using System.Diagnostics;

namespace P1_pps.Controllers
{
    public class HomeController : Controller
    {
        private List<Funcionario> fs = new List<Funcionario>();
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
