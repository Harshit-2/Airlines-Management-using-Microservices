using AirlinesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesMVC.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        static HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5010/flightSvc/")
        };

        // GET: FlightController
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Flight>? flights = await http.GetFromJsonAsync<List<Flight>>("");
            return View(flights);
        }

        // GET: FlightController/Details/5
        public async Task<ActionResult> Details(string fno)
        {
            Flight? flight = await http.GetFromJsonAsync<Flight>(fno);
            return View(flight);
        }

        // GET: FlightController/Create
        public ActionResult Create()
        {
            Flight flight = new Flight();
            return View(flight);
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Flight flight)
        {
            try
            {
                await http.PostAsJsonAsync("", flight);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        [Route("Flight/Edit/{fno}")]
        public async Task<ActionResult> Edit(string fno)
        {
            Flight? flight = await http.GetFromJsonAsync<Flight>(fno);
            return View(flight);
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [Route("Flight/Edit/{fno}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string fno, Flight flight)
        {
            try
            {
                await http.PutAsJsonAsync(fno, flight);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("Flight/Delete/{fno}")]
        public async Task<ActionResult> Delete(string fno)
        {
            Flight? flight = await http.GetFromJsonAsync<Flight>(fno);
            return View(flight);
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [Route("Flight/Delete/{fno}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string fno, IFormCollection collection)
        {
            try
            {
                await http.DeleteAsync(fno);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}