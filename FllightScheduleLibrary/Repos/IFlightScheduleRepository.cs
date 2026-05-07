using System;
using System.Collections.Generic;
using System.Text;
using FlightScheduleLibrary.Models;

namespace FlightScheduleLibrary.Repos
{
    public interface IFlightScheduleRepository
    {
        Task AddScheduleAsync(FlightSchedule schedule);
        Task UpdateScheduleAsync(string fno, DateTime fdate, FlightSchedule schedule);
        Task DeleteScheduleAsync(string fno, DateTime fdate);
        Task<List<FlightSchedule>> GetAllSchedulesAsync();
        Task<FlightSchedule> GetScheduleAsync(string fno, DateTime fdate);
        Task<List<FlightSchedule>> GetSchedulesByFlightAsync(string fno);
        Task<List<FlightSchedule>> GetSchedulesByDateAsync(DateTime fdate);
        Task AddFlightAsync(Flight flight);
    }
}
