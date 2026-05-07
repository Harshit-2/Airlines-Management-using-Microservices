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
    public class PassengerController : ControllerBase
    {
        IPassengerRespository passRepo;
        public PassengerController(IPassengerRespository passengerRespository)
        {
            passRepo = passengerRespository;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Insert(Passenger passenger)
        {
            try
            {
                await passRepo.AddPassengerAsync(passenger);
                return Created($"api/passenger/{passenger.PNR}/{passenger.PassengerNo}", passenger);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Passenger> passengers = await passRepo.GetAllPassengersAsync();
            return Ok(passengers);
        }
        [HttpGet("{pnr}/{passNo}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string pnr, int passNo)
        {
            try
            {
                Passenger passenger = await passRepo.GetPassengerAsync(pnr, passNo);
                return Ok(passenger);
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{pnr}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByPNR(string pnr)
        {
            try
            {
                List<Passenger> passengers = await passRepo.GetPassengersByReservationAsync(pnr);
                return Ok(passengers);
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{pnr}/{passNo}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(string pnr, int passNo, Passenger passenger)
        {
            try
            {
                await passRepo.UpdatePassengerAsync(pnr, passNo, passenger);
                return Ok(passenger);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{pnr}/{passNo}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(string pnr, int passNo)
        {
            try
            {
                await passRepo.DeletePassengerAsync(pnr, passNo);
                return Ok();
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
