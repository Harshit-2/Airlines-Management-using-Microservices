using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReservationLibrary.Models
{
    public class ReservationDBContext : DbContext
    {
        public ReservationDBContext()
        {
        }
        public ReservationDBContext(DbContextOptions<ReservationDBContext> options) : base(options)
        {
        }
        public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=LTIN732805\\SQLEXPRESS; database=IOT002ReservationDB; integrated security=true; trust server certificate=yes");
        }
    }
}
