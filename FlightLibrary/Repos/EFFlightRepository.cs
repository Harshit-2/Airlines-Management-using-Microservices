using System;
using System.Collections.Generic;
using System.Text;
using FlightLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace FlightLibrary.Repos
{
    public class EFFlightRepository : IFlightRepository
    {
        FlightDBContext context = new FlightDBContext();
        public async Task AddFlightAsync(Flight flight)
        {
            try
            {
                await context.Flights.AddAsync(flight);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightException(ex.Message);
            }
        }
        public async Task DeleteFlightAsync(string fno)
        {
            Flight flt2del = await GetFlightAsync(fno);
            try
            {
                context.Flights.Remove(flt2del);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightException(ex.Message);
            }
        }
        public async Task<List<Flight>> GetAllFlightsAsync()
        {
            List<Flight> flights = await context.Flights.ToListAsync();
            return flights;
        }
        public async Task<Flight> GetFlightAsync(string fno)
        {
            try
            {
                Flight flight = await (from flt in context.Flights where flt.FlightNo == fno select flt).FirstAsync();
                return flight;
            }
            catch
            {
                throw new FlightException("No such flight number");
            }
        }
        public async Task UpdateFlightAsync(string fno, Flight flight)
        {
            Flight flt2edit = await GetFlightAsync(fno);
            try
            {
                flt2edit.FromCity = flight.FromCity;
                flt2edit.ToCity = flight.ToCity;
                flt2edit.TotalSeats = flight.TotalSeats;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightException(ex.Message);
            }
        }
    }

}
