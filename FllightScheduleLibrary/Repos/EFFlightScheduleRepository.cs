using System;
using System.Collections.Generic;
using System.Text;
using FlightScheduleLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleLibrary.Repos
{
    public class EFFlightScheduleRepository : IFlightScheduleRepository
    {
        FlightScheduleDBContext context = new FlightScheduleDBContext();

        public async Task AddFlightAsync(Flight flight)
        {
            try
            {
                await context.Flights.AddAsync(flight);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightScheduleException(ex.Message);
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
                throw new FlightScheduleException(ex.Message);
            }
        }
        public async Task DeleteScheduleAsync(string fno, DateTime fdate)
        {
            FlightSchedule fs2del = await GetScheduleAsync(fno, fdate);
            try
            {
                context.FlightSchedules.Remove(fs2del);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightScheduleException(ex.Message);
            }
        }
        public async Task<List<FlightSchedule>> GetAllSchedulesAsync()
        {
            List<FlightSchedule> schedules = await context.FlightSchedules.ToListAsync();
            return schedules;
        }
        public async Task<FlightSchedule> GetScheduleAsync(string fno, DateTime fdate)
        {
            try
            {
                FlightSchedule schedule = await (from fs in context.FlightSchedules where fs.FlightNo == fno && fs.FlightDate == fdate select fs).FirstAsync();
                return schedule;
            }
            catch
            {
                throw new FlightScheduleException("The flight is not scheduled on this date");
            }
        }
        public async Task<List<FlightSchedule>> GetSchedulesByDateAsync(DateTime fdate)
        {
            List<FlightSchedule> schedules = await (from fs in context.FlightSchedules where fs.FlightDate == fdate select fs).ToListAsync();
            if (schedules.Count != 0)
                return schedules;
            else
                throw new   ("No flights scheduled on this date");
        }
        public async Task<List<FlightSchedule>> GetSchedulesByFlightAsync(string fno)
        {
            List<FlightSchedule> schedules = await (from fs in context.FlightSchedules where fs.FlightNo == fno select fs).ToListAsync();
            if (schedules.Count != 0)
                return schedules;
            else
                throw new FlightScheduleException("This flight is not yet scheduled");
        }
        public async Task UpdateScheduleAsync(string fno, DateTime fdate, FlightSchedule schedule)
        {
            FlightSchedule fs2edit = await GetScheduleAsync(fno, fdate);
            try
            {
                fs2edit.DepartTime = schedule.DepartTime;
                fs2edit.ArriveTime = schedule.ArriveTime;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightScheduleException(ex.Message);
            }
        }
    }

}
