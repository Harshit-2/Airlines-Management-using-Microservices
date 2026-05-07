using System;
using System.Collections.Generic;
using System.Text;
using ReservationLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace ReservationLibrary.Repos
{
    public class EFPassengerRepository : IPassengerRespository
    {
        ReservationDBContext context = new ReservationDBContext();
        public async Task AddPassengerAsync(Passenger passenger)
        {
            try
            {
                await context.Passengers.AddAsync(passenger);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
        public async Task DeletePassengerAsync(string pnr, int passNo)
        {
            Passenger pass2del = await GetPassengerAsync(pnr, passNo);
            try
            {
                context.Passengers.Remove(pass2del);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
        public async Task<List<Passenger>> GetAllPassengersAsync()
        {
            List<Passenger> passengers = await context.Passengers.ToListAsync();
            return passengers;
        }
        public async Task<Passenger> GetPassengerAsync(string pnr, int passNo)
        {
            try
            {
                Passenger passenger = await (from pass in context.Passengers where pass.PNR == pnr && pass.PassengerNo == passNo select pass).FirstAsync();
                return passenger;
            }
            catch
            {
                throw new ReservationException("No passenger with those details");
            }
        }
        public async Task<List<Passenger>> GetPassengersByReservationAsync(string pnr)
        {
            List<Passenger> passengers = await (from pass in context.Passengers where pass.PNR == pnr select pass).ToListAsync();
            if (passengers.Count != 0)
                return passengers;
            else
                throw new ReservationException("No passengers in this reservation");
        }
        public async Task UpdatePassengerAsync(string pnr, int passNo, Passenger passenger)
        {
            Passenger pass2edit = await GetPassengerAsync(pnr, passNo);
            try
            {
                pass2edit.PassengerName = passenger.PassengerName;
                pass2edit.Gender = passenger.Gender;
                pass2edit.Age = passenger.Age;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }
    }

}
