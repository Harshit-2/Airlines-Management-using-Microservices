using FlightLibrary.Models;
using FlightLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightController : ControllerBase
    {
        IFlightRepository flightRepo;
        public FlightController(IFlightRepository flightRepository)
        {
            flightRepo = flightRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Flight> flights = await flightRepo.GetAllFlightsAsync();
            return Ok(flights);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Insert(Flight flight)
        {
            try
            {
                await flightRepo.AddFlightAsync(flight);
                HttpClient http = new HttpClient() { BaseAddress = new Uri("http://localhost:5131/api/FlightSchedule/") };
                await http.PostAsJsonAsync("Flight", new { FlightNo = flight.FlightNo });
                return Created($"api/flight/{flight.FlightNo}", flight);
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{fno}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string fno)
        {
            try
            {
                Flight flight = await flightRepo.GetFlightAsync(fno);
                return Ok(flight);
            }
            catch (FlightException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{fno}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(string fno, Flight flight)
        {
            try
            {
                await flightRepo.UpdateFlightAsync(fno, flight);
                return Ok(flight);
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{fno}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(string fno)
        {
            try
            {
                await flightRepo.DeleteFlightAsync(fno);
                return Ok();
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
