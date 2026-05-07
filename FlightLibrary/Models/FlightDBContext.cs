using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlightLibrary.Models
{
    public class FlightDBContext : DbContext
    {
        public FlightDBContext()
        {
        }
        public FlightDBContext(DbContextOptions<FlightDBContext> options) : base(options)
        {
        }
        public virtual DbSet<Flight> Flights { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=LTIN732805\\SQLEXPRESS; database=IOT002FlightDB; integrated security=true; trust server certificate=yes");
        }
    }
}
