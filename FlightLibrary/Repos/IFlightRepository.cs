using System;
using System.Collections.Generic;
using System.Text;
using FlightLibrary.Models;

namespace FlightLibrary.Repos
{
    public interface IFlightRepository
    {
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(string fno, Flight flight);
        Task DeleteFlightAsync(string fno);
        Task<List<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightAsync(string fno);
    }
}
