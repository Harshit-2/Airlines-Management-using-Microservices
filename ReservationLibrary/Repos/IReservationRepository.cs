using System;
using System.Collections.Generic;
using System.Text;
using ReservationLibrary.Models;

namespace ReservationLibrary.Repos
{
    public interface IReservationRepository
    {
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(string pnr, Reservation reservation);
        Task DeleteReservationAsync(string pnr);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationAsync(string pnr);
        Task<List<Reservation>> GetReservationsByScheduleAsync(string fno, DateTime fdate);
        Task AddScheduleAsync(FlightSchedule schedule);
    }
}
