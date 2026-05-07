using FlightScheduleLibrary.Models;
using FlightScheduleLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightScheduleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightScheduleController : ControllerBase
    {
        IFlightScheduleRepository scheduleRepo;
        public FlightScheduleController(IFlightScheduleRepository scheduleRepository)
        {
            scheduleRepo = scheduleRepository;
        }
        [HttpPost("Flight")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> InsertFlight(Flight flight)
        {
            try
            {
                await scheduleRepo.AddFlightAsync(flight);
                return Created();
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Insert(FlightSchedule schedule)
        {
            try
            {
                await scheduleRepo.AddScheduleAsync(schedule);
                HttpClient http = new HttpClient() { BaseAddress = new Uri("http://localhost:5022/api/Reservation/") };
                await http.PostAsJsonAsync("FlightSchedule", new { FlightNo = schedule.FlightNo, FlightDate = schedule.FlightDate });
                return Created($"api/flightschedule/{schedule.FlightNo}/{schedule.FlightDate}", schedule);
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<FlightSchedule> schedules = await scheduleRepo.GetAllSchedulesAsync();
            return Ok(schedules);
        }
        [HttpGet("{fno}/{fdate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string fno, DateTime fdate)
        {
            try
            {
                FlightSchedule schedule = await scheduleRepo.GetScheduleAsync(fno, fdate);
                return Ok(schedule);
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByFlight/{fno}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByFlight(string fno)
        {
            try
            {
                List<FlightSchedule> schedules = await scheduleRepo.GetSchedulesByFlightAsync(fno);
                return Ok(schedules);
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByDate/{fdate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByDate(DateTime fdate)
        {
            try
            {
                List<FlightSchedule> schedules = await scheduleRepo.GetSchedulesByDateAsync(fdate);
                return Ok(schedules);
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{fno}/{fdate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(string fno, DateTime fdate, FlightSchedule schedule)
        {
            try
            {
                await scheduleRepo.UpdateScheduleAsync(fno, fdate, schedule);
                return Ok(schedule);
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{fno}/{fdate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(string fno, DateTime fdate)
        {
            try
            {
                await scheduleRepo.DeleteScheduleAsync(fno, fdate);
                return Ok();
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
