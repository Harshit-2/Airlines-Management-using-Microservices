using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleLibrary.Models
{
    public class FlightScheduleDBContext : DbContext
    {
        public FlightScheduleDBContext()
        {
        }
        public FlightScheduleDBContext(DbContextOptions<FlightScheduleDBContext> options) : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSchedule> FlightSchedules { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=LTIN732805\\SQLEXPRESS; database=IOT002FlightScheduleDB; integrated security=true; trust server certificate=yes");
        }
    }
}
