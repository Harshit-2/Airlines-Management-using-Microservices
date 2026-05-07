using System;
using System.Collections.Generic;
using System.Text;
using ReservationLibrary.Models;

namespace ReservationLibrary.Repos
{
    public interface IPassengerRespository
    {
        Task AddPassengerAsync(Passenger passenger);
        Task UpdatePassengerAsync(string pnr, int passNo, Passenger passenger);
        Task DeletePassengerAsync(string pnr, int passNo);
        Task<List<Passenger>> GetAllPassengersAsync();
        Task<Passenger> GetPassengerAsync(string pnr, int passNo);
        Task<List<Passenger>> GetPassengersByReservationAsync(string pnr);
    }
}
