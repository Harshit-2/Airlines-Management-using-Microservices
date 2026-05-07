using System.Diagnostics;
using AirlinesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace AirlinesMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {HttpClient authHelp = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5010/authSvc/")
            };
            string username = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secrtKey = "I am Bond, James Bond. I am the best spy in the world. I am invincible.";
            string token = await authHelp.GetStringAsync($"{username}/{role}/{secrtKey}");

                HttpContext.Session.SetString("token", token);
            }
            return View();
        }

        public IActionResult Privacy()
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
