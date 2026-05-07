using System;
using System.Collections.Generic;
using System.Text;
using ReservationLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace ReservationLibrary.Repos
{
    public class EFReservationRepository : IReservationRepository {
        ReservationDBContext context = new ReservationDBContext();
        public async Task AddReservationAsync(Reservation reservation)
        {
            try
            {
                await context.Reservations.AddAsync(reservation);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
        public async Task AddScheduleAsync(FlightSchedule schedule)
        {
            try
            {
                await context.FlightSchedules.AddAsync(schedule);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }

        public async Task DeleteReservationAsync(string pnr)
        {
            Reservation res2del = await GetReservationAsync(pnr);
            try
            {
                context.Reservations.Remove(res2del);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            List<Reservation> reservations = await context.Reservations.ToListAsync();
            return reservations;
        }
        public async Task<Reservation> GetReservationAsync(string pnr)
        {
            try
            {
                Reservation reservation = await (from res in context.Reservations where res.PNR == pnr select res).FirstAsync();
                return reservation;
            }
            catch
            {
                throw new ReservationException("No such PNR");
            }
        }
        public async Task<List<Reservation>> GetReservationsByScheduleAsync(string fno, DateTime fdate)
        {
            List<Reservation> reservations = await (from res in context.Reservations where res.FlightNo == fno && res.FlightDate == fdate select res).ToListAsync();
            if (reservations.Count != 0)
                return reservations;
            else
                throw new ReservationException("No reservations for this schedule");
        }
        public async Task UpdateReservationAsync(string pnr, Reservation reservation)
        {
            Reservation res2edit = await GetReservationAsync(pnr);
            try
            {
                res2edit.FlightNo = reservation.FlightNo;
                res2edit.FlightDate = reservation.FlightDate;
                res2edit.ReservationDate = reservation.ReservationDate;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
    }

}
