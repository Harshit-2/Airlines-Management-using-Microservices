using AirlinesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesMVC.Controllers
{
    [Authorize]
    public class FlightScheduleController : Controller
    {
        static HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5010/FlightScheduleSvc/")
        };

        // GET: FlightScheduleController
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
     
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<FlightSchedule> schedules = await http.GetFromJsonAsync<List<FlightSchedule>>("");
            return View(schedules);
        }

        // GET: FlightScheduleController/Details/5
        public async Task<ActionResult> Details(string fno, string fdate)
        {
            FlightSchedule? schedule = await http.GetFromJsonAsync<FlightSchedule>(fno + "/" + fdate);
            return View(schedule);
        }

        // GET: FlightScheduleController/Create
        public ActionResult Create()
        {
            FlightSchedule schedule = new FlightSchedule();
            return View(schedule);
        }

        // POST: FlightScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FlightSchedule schedule)
        {
            try
            {
                await http.PostAsJsonAsync<FlightSchedule>("", schedule);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightScheduleController/Edit/5
        [Route("FlightSchedule/Edit/{fno}/{fdate}")]
        public async Task<ActionResult> Edit(string fno, string fdate)
        {
            FlightSchedule? schedule = await http.GetFromJsonAsync<FlightSchedule>(fno + "/" + fdate);
            return View(schedule);
        }

        // POST: FlightScheduleController/Edit/5
        [HttpPost]
        [Route("FlightSchedule/Edit/{fno}/{fdate}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string fno, string fdate, FlightSchedule schedule)
        {
            try
            {
                await http.PutAsJsonAsync<FlightSchedule>(fno + "/" + fdate, schedule);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightScheduleController/Delete/5
        [Route("FlightSchedule/Delete/{fno}/{fdate}")]
        public async Task<ActionResult> Delete(string fno, string fdate)
        {
            FlightSchedule? schedule = await http.GetFromJsonAsync<FlightSchedule>(fno + "/" + fdate);
            return View(schedule);
        }

        // POST: FlightScheduleController/Delete/5
        [HttpPost]
        [Route("FlightSchedule/Delete/{fno}/{fdate}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string fno, string fdate, IFormCollection collection)
        {
            try
            {
                await http.DeleteAsync(fno + "/" + fdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> GetByFlight(string fno)
        {
            List<FlightSchedule>? schedules = await http.GetFromJsonAsync<List<FlightSchedule>>(fno);
            return View(schedules);
        }

        public async Task<ActionResult> GetByDate(string fdate)
        {
            List<FlightSchedule>? schedules = await http.GetFromJsonAsync<List<FlightSchedule>>("ByDate/" + fdate);
            return View(schedules);
        }
    }
}
