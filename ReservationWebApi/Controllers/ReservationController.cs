using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationLibrary.Models;
using ReservationLibrary.Repos;

namespace ReservationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        IReservationRepository resRepo;
        public ReservationController(IReservationRepository reservationRepository)
        {
            resRepo = reservationRepository;
        }
        [HttpPost("FlightSchedule")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> InsertSchedule(FlightSchedule schedule)
        {
            try
            {
                await resRepo.AddScheduleAsync(schedule);
                return Created();
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Insert(Reservation reservation)
        {
            try
            {
                await resRepo.AddReservationAsync(reservation);
                return Created($"api/reservation/{reservation.PNR}", reservation);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Reservation> reservations = await resRepo.GetAllReservationsAsync();
            return Ok(reservations);
        }
        [HttpGet("{pnr}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string pnr)
        {
            try
            {
                Reservation reservation = await resRepo.GetReservationAsync(pnr);
                return Ok(reservation);
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{fno}/{fdate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetBySchedule(string fno, DateTime fdate)
        {
            try
            {
                List<Reservation> reservations = await resRepo.GetReservationsByScheduleAsync(fno, fdate);
                return Ok(reservations);
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{pnr}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(string pnr, Reservation reservation)
        {
            try
            {
                await resRepo.UpdateReservationAsync(pnr, reservation);
                return Ok(reservation);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{pnr}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(string pnr)
        {
            try
            {
                await resRepo.DeleteReservationAsync(pnr);
                return Ok();
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
